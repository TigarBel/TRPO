using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using GRPO.Drawing;
using GRPO.Drawing.Property;
using GRPO.Drawing.Interface;
using GRPO.Commands;
using GRPO.InteractionFrame;
using GRPO.UserControls.CanvasMouseActions;

namespace GRPO
{
    /// <summary>
    /// Пользовательский интерфейс полотна для рисования
    /// </summary>
    public partial class CanvasControl : MouseAction
    {
        /// <summary>
        /// Делегат для события загребсти
        /// </summary>
        /// <param name="drawable">Загребаемая фигура</param>
        public delegate void CanvasControlDragProperty(IDrawable drawable);

        /// <summary>
        /// Событие при вытягивании свойства из фигуры
        /// </summary>
        public event CanvasControlDragProperty DragProperty;

        /// <summary>
        /// Список фигур хронящихся в памяти
        /// </summary>
        private List<IDrawable> _buferDraw = new List<IDrawable>();

        /// <summary>
        /// Инициализация пользовательского интерфейса полотна для рисования
        /// </summary>
        public CanvasControl()
        {
            InitializeComponent();
            SetSizeCanvas(100, 100);
            SelectTool = new Tools(DrawingTools.DrawFigureLine);
            LineProperty = new LineProperty();
            FillProperty = new FillProperty();

            RefreshCanvasEvent += RefreshCanvas;
            DragPropertyEvent += Drag;

            canvas.MouseDown += canvas_MouseDown;
            canvas.MouseMove += canvas_MouseMove;
            canvas.MouseUp += canvas_MouseUp;

            SelectToolMouseAction = SelectTool;
            DrawablesMouseAction = Drawables;
            LinePropertyMouseAction = LineProperty;
            FillPropertyMouseAction = FillProperty;
            ControlUnitMouseAction = ControlUnit;
            InteractionMouseAction = Interaction;
            CnavasMouseAction = canvas;
        }

        public ControlUnit ControlUnit
        {
            get { return ControlUnitMouseAction; }
            set
            {
                ControlUnitMouseAction = value;
                DrawablesMouseAction = ControlUnitMouseAction.GraphicsEditor.Drawables;
            }
        }

        /// <summary>
        /// Список фигур
        /// </summary>
        public List<IDrawable> Drawables
        {
            get
            {
                return ControlUnitMouseAction.GraphicsEditor.Drawables; //_drawables;
            }
            set
            {
                DrawablesMouseAction = value;
                if (DrawablesMouseAction != null)
                {
                    if (DrawablesMouseAction.Count > 0)
                    {
                        RefreshCanvas();
                    }
                }
            }
        }

        /// <summary>
        /// Инструмент для рисования
        /// </summary>
        public Tools SelectTool
        {
            get { return SelectToolMouseAction; }
            set { SelectToolMouseAction = value; }
        }

        /// <summary>
        /// Свойство линии
        /// </summary>
        public LineProperty LineProperty
        {
            get { return LinePropertyMouseAction; }
            set { LinePropertyMouseAction = value; }
        }

        /// <summary>
        /// Свойство заливки
        /// </summary>
        public FillProperty FillProperty
        {
            get { return FillPropertyMouseAction; }
            set { FillPropertyMouseAction = value; }
        }

        /// <summary>
        /// Картинка с холста
        /// </summary>
        public Image Image
        {
            get { return new Bitmap(canvas.Image); }
        }

        /// <summary>
        /// Объект взаимодействия с нарисованными фигурами
        /// </summary>
        public Interaction Interaction
        {
            get { return InteractionMouseAction; }
            set { InteractionMouseAction = value; }
        }

        /// <summary>
        /// Список фигур хронящихся в памяти
        /// </summary>
        public List<IDrawable> BuferDraw
        {
            get { return _buferDraw; }
            set { _buferDraw = value; }
        }

        /// <summary>
        /// Задать размер полотна
        /// </summary>
        /// <param name="width">Ширина полотна</param>
        /// <param name="height">Высота полотна</param>
        public void SetSizeCanvas(int width, int height)
        {
            if (width <= 0)
            {
                throw new ArgumentException("Ширина полотна не может быть меньше 1!");
            }

            if (height <= 0)
            {
                throw new ArgumentException("Высота полотна не может быть меньше 1!");
            }

            canvas.Size = new Size(width, height);
            canvas.Image = new Bitmap(width, height);
            if (Drawables.Count > 0)
            {
                foreach (IDrawable draw in Drawables)
                {
                    draw.Draw(canvas);
                }
            }
        }

        /// <summary>
        /// Получить ширину холста
        /// </summary>
        /// <returns>Ширину холста</returns>
        public int GetWidthCanvas()
        {
            return canvas.Width;
        }

        /// <summary>
        /// Получить высоту холста
        /// </summary>
        /// <returns>Высоту холста</returns>
        public int GetHeightCanvas()
        {
            return canvas.Height;
        }

        /// <summary>
        /// Перерисовать фигуры из списка
        /// </summary>
        public void RefreshCanvas()
        {
            canvas.Image = new Bitmap(canvas.Width, canvas.Height);
            foreach (IDrawable drawable in Drawables)
            {
                drawable.Draw(canvas);
            }

            if (Interaction != null && !FlagMouseDown)
            {
                /*Interaction.DrawSelcet(canvas, Interaction.EnablePoints, Interaction.DrawableFigures);*/
            }
        }

        /// <summary>
        /// Очистка холста
        /// </summary>
        public void ClearCanvas()
        {
            ControlUnit.Clear(ControlUnit.GraphicsEditor.Keywords[4], Drawables, null);
            RefreshCanvas();
            canvas.Image = new Bitmap(canvas.Width, canvas.Height);
        }

        /// <summary>
        /// Скопировать фигуру(ы) в буфер
        /// </summary>
        public void Copy()
        {
            BuferDraw.Clear();
            if (SelectTool.TypeTools == TypeTools.SelectFigure && Drawables.Count > 0 && Interaction != null)
            {
                foreach (IDrawable drawable in Interaction.DrawableFigures)
                {
                    BuferDraw.Add(drawable.Clone());
                }
            }
        }

        /// <summary>
        /// Вставить фигуру(ы)
        /// </summary>
        public void Paste()
        {
            if (SelectTool.TypeTools == TypeTools.SelectFigure && BuferDraw != null)
            {
                List<Point> localPoints = new List<Point>();
                foreach (IDrawable drawable in BuferDraw)
                {
                    foreach (Point point in drawable.Points)
                    {
                        localPoints.Add(point);
                    }
                }

                List<IDrawable> drawables = new List<IDrawable>();
                foreach (IDrawable drawable in BuferDraw)
                {
                    if (PointB.X > 0 && PointB.X < GetWidthCanvas() && PointB.Y > 0 && PointB.Y < GetHeightCanvas())
                    {
                        int Width = drawable.Position.X - localPoints.Min(point => point.X);
                        int Height = drawable.Position.Y - localPoints.Min(point => point.Y);
                        drawable.Position = new Point(PointB.X + Width, PointB.Y + Height);
                    }
                    else
                    {
                        drawable.Position = new Point(10, 10);
                    }

                    drawables.Add(drawable.Clone());
                }

                ControlUnit.Reconstruction(ControlUnit.GraphicsEditor.Keywords[8], drawables,
                    GetIndexes(drawables, Drawables),
                    new Point(), new Point());

                RefreshCanvas();
                /*Interaction = new Interaction(Drawables.GetRange(Drawables.Count - BuferDraw.Count, BuferDraw.Count),
                    false);
                Interaction.DrawSelcet(canvas, Interaction.EnablePoints, Interaction.DrawableFigures);*/
            }
        }

        /// <summary>
        /// Удалить фигуру(ы)
        /// </summary>
        public void Delete()
        {
            if (SelectTool.TypeTools == TypeTools.SelectFigure && Drawables.Count > 0 && Interaction != null)
            {
                ControlUnit.Clear(ControlUnit.GraphicsEditor.Keywords[5], Interaction.DrawableFigures,
                    GetIndexes(Interaction.DrawableFigures, Drawables));
                Interaction = null;
                RefreshCanvas();
                if (DragProperty != null) DragProperty(null);
            }
        }

        /// <summary>
        /// Вырезать фигуру(ы)
        /// </summary>
        public void Cut()
        {
            BuferDraw.Clear();
            if (SelectTool.TypeTools == TypeTools.SelectFigure && Drawables.Count > 0 && Interaction != null)
            {
                foreach (IDrawable drawable in Interaction.DrawableFigures)
                {
                    BuferDraw.Add(drawable.Clone());
                }

                ControlUnit.Clear(ControlUnit.GraphicsEditor.Keywords[5], Interaction.DrawableFigures,
                    GetIndexes(Interaction.DrawableFigures, Drawables));
                Interaction = null;
                RefreshCanvas();
                if (DragProperty != null) DragProperty(null);
            }
        }

        /// <summary>
        /// Под метод для наследуемого класса
        /// </summary>
        /// <param name="drawable">Загребаемая фигура</param>
        private void Drag(IDrawable drawable)
        {
            if (DragProperty != null) DragProperty(drawable);
        }

        /*public void AddFigureInInteractive()
        {

        }*/
    }
}