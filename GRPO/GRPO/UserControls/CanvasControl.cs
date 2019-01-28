using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using GRPO.Drawing;
using GRPO.Drawing.Property;
using GRPO.Drawing.Interface;
using GRPO.Commands;
using GRPO.UserControls.CanvasMouseStrategy;

namespace GRPO
{
    /// <summary>
    /// Пользовательский интерфейс полотна для рисования
    /// </summary>
    public partial class CanvasControl : UserControl
    {
        /// <summary>
        /// Делегат для события собрать
        /// </summary>
        /// <param name="drawable">Собираемая фигура</param>
        public delegate void DragEventHandler(IDrawable drawable);

        /// <summary>
        /// Событие собрать свойства
        /// </summary>
        public event DragEventHandler DragProperty;

        /// <summary>
        /// Фабрика фигур
        /// </summary>
        private FactoryDrawFigure _factoryDrawFigure = new FactoryDrawFigure();

        /// <summary>
        /// Начальная точка / Точка в момент нажатия кнопки мыши
        /// </summary>
        private Point _pointA;

        /// <summary>
        /// Конечная точка / Точка в момент отжатия кнопки мыши
        /// </summary>
        private Point _pointB;

        /// <summary>
        /// Промежуточная точка, для изменения положения
        /// </summary>
        private Point _pointC;

        private bool _flagMouseDown = false;

        /// <summary>
        /// Флаг полифигур, для составения полифигур
        /// </summary>
        private bool _flagPolyFigure = false;

        /// <summary>
        /// Список фигур хронящихся в памяти
        /// </summary>
        private List<IDrawable> _buferDraw = new List<IDrawable>();

        /// <summary>
        /// Управляющий элемент
        /// </summary>
        private ControlUnit _controlUnit = new ControlUnit();

        /// <summary>
        /// Локальный список фигур
        /// </summary>
        private List<IDrawable> _drawables = new List<IDrawable>();

        /// <summary>
        /// Инструмент для рисования
        /// </summary>
        private Tools _selectTool;

        /// <summary>
        /// Объект взаимодействия с нарисованными фигурами
        /// </summary>
        private Interaction _interaction;

        /// <summary>
        /// Инициализация пользовательского интерфейса полотна для рисования
        /// </summary>
        public CanvasControl()
        {
            InitializeComponent();
            SizeCanvas = new Point(100, 100);
            SelectTool = new Tools(DrawingTools.DrawFigureLine);
            LineProperty = new LineProperty();
            FillProperty = new FillProperty();
        }

        /// <summary>
        /// Флаг устанавливаемый при зажатии кнопки мыши
        /// </summary>
        public bool FlagMouseDown
        {
            get { return _flagMouseDown; }
            set { _flagMouseDown = value; }
        }

        /// <summary>
        /// Флаг при создании полифигур
        /// </summary>
        public bool FlagPolyFigure
        {
            get { return _flagPolyFigure; }
            set
            {
                if (!value)
                {
                    Drawables.Clear();
                }

                _flagPolyFigure = value;
            }
        }

        /// <summary>
        /// Пользовательский элемнт
        /// </summary>
        public ControlUnit ControlUnit
        {
            get { return _controlUnit; }
            set { _controlUnit = value; }
        }

        /// <summary>
        /// Локальный список фигур
        /// </summary>
        public List<IDrawable> Drawables
        {
            get { return _drawables; }
            set { _drawables = value; }
        }

        /// <summary>
        /// Инструмент для рисования
        /// </summary>
        public Tools SelectTool
        {
            get { return _selectTool; }
            set
            {
                Interaction = null;
                _selectTool = value;
                RefreshCanvas();
            }
        }

        /// <summary>
        /// Свойство линии
        /// </summary>
        public LineProperty LineProperty { get; set; }

        /// <summary>
        /// Свойство заливки
        /// </summary>
        public FillProperty FillProperty { get; set; }

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
            get { return _interaction; }
            set { _interaction = value; }
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
        /// Размер полотна, задается точкой
        /// </summary>
        public Point SizeCanvas
        {
            get { return new Point(canvas.Width, canvas.Height); }
            set
            {
                if (value.X <= 0)
                {
                    throw new ArgumentException("Ширина полотна не может быть меньше 1!");
                }

                if (value.Y <= 0)
                {
                    throw new ArgumentException("Высота полотна не может быть меньше 1!");
                }

                canvas.Size = new Size(value.X, value.Y);
                canvas.Image = new Bitmap(value.X, value.Y);
                RefreshCanvas();
            }
        }

        /// <summary>
        /// Перерисовать фигуры из списка
        /// </summary>
        public void RefreshCanvas()
        {
            canvas.Image = new Bitmap(canvas.Width, canvas.Height);

            foreach (IDrawable drawable in ControlUnit.GraphicsEditor.Drawables)
            {
                drawable.Draw(canvas);
            }

            foreach (IDrawable drawable in Drawables)
            {
                drawable.Draw(canvas);
            }

            if (Interaction != null)
            {
                if (!FlagMouseDown) Interaction.DrawSelcet(canvas);
            }
        }

        /// <summary>
        /// Очистка холста
        /// </summary>
        public void ClearCanvas()
        {
            if (ControlUnit.GraphicsEditor.Drawables.Count != 0)
            {
                ControlUnit.Clear(ControlUnit.GraphicsEditor.Keywords[4], ControlUnit.GraphicsEditor.Drawables, null);
            }

            canvas.Image = new Bitmap(canvas.Width, canvas.Height);
            Interaction = null;
            Drawables.Clear();
            FlagMouseDown = false;
            FlagPolyFigure = false;
            RefreshCanvas();
        }

        /// <summary>
        /// Событие при нажатии на холст
        /// </summary>
        /// <param name="sender">объект(холст)</param>
        /// <param name="e">событие(нажатие кнопки мыши)</param>
        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            FlagMouseDown = true;
            _pointA = new Point(e.X, e.Y);

            ContextMouse contextMouse = new ContextMouse(new MouseDown());
            contextMouse.MouseAction(ref _flagMouseDown, ref _flagPolyFigure, SelectTool, LineProperty, FillProperty, canvas,
                ref _drawables, ref _controlUnit, _pointA, _pointB, ref _pointC, ref _interaction, e);
        }

        /// <summary>
        /// Событие при движении мыши по холсту
        /// </summary>
        /// <param name="sender">объект(холст)</param>
        /// <param name="e">событие(движение мышью)</param>
        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            _pointB = new Point(e.X, e.Y);
            RefreshCanvas();

            ContextMouse contextMouse = new ContextMouse(new MouseMove());
            contextMouse.MouseAction(ref _flagMouseDown, ref _flagPolyFigure, SelectTool, LineProperty, FillProperty, canvas,
                ref _drawables, ref _controlUnit, _pointA, _pointB, ref _pointC, ref _interaction, e);
        }

        /// <summary>
        /// Событие при отжатия кнопки по холсту
        /// </summary>
        /// <param name="sender">объект(холст)</param>
        /// <param name="e">событие(отжатие кнопки мыши)</param>
        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            _pointB = new Point(e.X, e.Y);

            ContextMouse contextMouse = new ContextMouse(new MouseUp(DragProperty));
            contextMouse.MouseAction(ref _flagMouseDown, ref _flagPolyFigure, SelectTool, LineProperty, FillProperty, canvas,
                ref _drawables, ref _controlUnit, _pointA, _pointB, ref _pointC, ref _interaction, e);

            RefreshCanvas();
        }

        /// <summary>
        /// Скопировать фигуру(ы) в буфер
        /// </summary>
        public void Copy()
        {
            BuferDraw.Clear();
            if (SelectTool.TypeTools == TypeTools.SelectFigure && ControlUnit.GraphicsEditor.Drawables.Count > 0 &&
                Interaction != null)
            {
                foreach (int index in Interaction.Indexes)
                {
                    BuferDraw.Add(ControlUnit.GraphicsEditor.Drawables[index].Clone());
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

                if (Interaction != null) Interaction.DrawableFigures.Clear();
                else Interaction = new Interaction();
                List<IDrawable> drawables = new List<IDrawable>();
                foreach (IDrawable drawable in BuferDraw)
                {
                    if (_pointB.X > 0 && _pointB.X < SizeCanvas.X && _pointB.Y > 0 && _pointB.Y < SizeCanvas.Y)
                    {
                        int Width = drawable.Position.X - localPoints.Min(point => point.X);
                        int Height = drawable.Position.Y - localPoints.Min(point => point.Y);
                        drawable.Position = new Point(_pointB.X + Width, _pointB.Y + Height);
                    }
                    else
                    {
                        drawable.Position = new Point(10, 10);
                    }

                    drawables.Add(drawable.Clone());
                    Interaction.AddDrawableFigure(drawable);
                }

                Interaction.Indexes = GetIndexes(drawables, ControlUnit.GraphicsEditor.Drawables);
                Interaction.GetMaxMinXY();
                ControlUnit.Reconstruction(ControlUnit.GraphicsEditor.Keywords[8], drawables,
                    GetIndexes(drawables, ControlUnit.GraphicsEditor.Drawables), 0,
                    new Point(), new Point());

                RefreshCanvas();
            }
        }

        /// <summary>
        /// Удалить фигуру(ы)
        /// </summary>
        public void Delete()
        {
            if (SelectTool.TypeTools == TypeTools.SelectFigure && ControlUnit.GraphicsEditor.Drawables.Count > 0 &&
                Interaction != null)
            {
                ControlUnit.Clear(ControlUnit.GraphicsEditor.Keywords[5], Interaction.DrawableFigures,
                    Interaction.Indexes);
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
            if (SelectTool.TypeTools == TypeTools.SelectFigure && ControlUnit.GraphicsEditor.Drawables.Count > 0 &&
                Interaction != null)
            {
                foreach (IDrawable drawable in Interaction.DrawableFigures)
                {
                    BuferDraw.Add(drawable.Clone());
                }

                ControlUnit.Clear(ControlUnit.GraphicsEditor.Keywords[5], Interaction.DrawableFigures,
                    Interaction.Indexes);
                Interaction = null;
                RefreshCanvas();
                if (DragProperty != null) DragProperty(null);
            }
        }

        /// <summary>
        /// Метод для получения списка индексов фигур
        /// </summary>
        /// <param name="localDrawables">Проверяемы список фигур</param>
        /// <param name="globalDrawables">Глобальный список фигур</param>
        /// <returns>Список индексов фигур</returns>
        private List<int> GetIndexes(List<IDrawable> localDrawables, List<IDrawable> globalDrawables)
        {
            List<int> indexes = new List<int>();
            int count = 0;
            foreach (IDrawable drawable in localDrawables)
            {
                if (globalDrawables.IndexOf(drawable) != -1)
                    indexes.Add(globalDrawables.IndexOf(drawable));
                else
                {
                    indexes.Add(count + ControlUnit.GraphicsEditor.Drawables.Count);
                    count++;
                }

            }

            return indexes;
        }
    }
}