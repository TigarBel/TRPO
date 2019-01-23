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

namespace GRPO
{
    /// <summary>
    /// Пользовательский интерфейс полотна для рисования
    /// </summary>
    public partial class CanvasControl : UserControl
    {

        public delegate void Drag(IDrawable drawable);

        public event Drag DragProperty;

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

        private Point _pointC;

        private bool _flagPolyFigure = false;

        /// <summary>
        /// Флаг устанавливаемый при зажатии кнопки мыши
        /// </summary>
        public bool FlagMouseDown { get; private set; }

        /// <summary>
        /// Флаг при создании полифигур
        /// </summary>
        public bool FlagPolyFigure
        {
            get { return _flagPolyFigure;}
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
            SetSizeCanvas(100, 100);
            SelectTool = new Tools(DrawingTools.DrawFigureLine);
            LineProperty = new LineProperty();
            FillProperty = new FillProperty();
        }

        public ControlUnit ControlUnit
        {
            get { return _controlUnit; }
            set { _controlUnit = value; }
        }

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
        /// Хранилище для фона
        /// </summary>
        private Image _image = new Bitmap(640, 480);

        /// <summary>
        /// Картинка с холста
        /// </summary>
        public Image Image
        {
            get { return new Bitmap(canvas.Image); }
            set { _image = new Bitmap(value); }
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
            RefreshCanvas();
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
            canvas.Image = new Bitmap(_image);

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
                /*foreach (IDrawable drawable in Interaction.DrawableFigures)
                {
                    drawable.Draw(canvas);
                }*/
            }
        }

        /// <summary>
        /// Очистка холста
        /// </summary>
        public void ClearCanvas()
        {
            ControlUnit.Clear(ControlUnit.GraphicsEditor.Keywords[4], ControlUnit.GraphicsEditor.Drawables, null);
            RefreshCanvas();
            canvas.Image = new Bitmap(canvas.Width, canvas.Height);
            Interaction = null;
            Drawables.Clear();
        }

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            FlagMouseDown = true;
            _pointA = new Point(e.X, e.Y);

            if (SelectTool.TypeTools == TypeTools.SimpleFigure)
            {
                Drawables.Add(_factoryDrawFigure.SimpleFigure(_pointA, _pointA, LineProperty, FillProperty,
                    SelectTool.DrawingTools));
            }

            if (SelectTool.TypeTools == TypeTools.PolyFigure)
            {
                if (FlagPolyFigure)
                {
                    List<Point> points = Drawables[Drawables.Count - 1].Points;
                    points.RemoveAt(points.Count - 1);
                    points.Add(_pointA);

                    Drawables.RemoveAt(Drawables.Count - 1);
                    if (e.Button == MouseButtons.Right)
                    {
                        ControlUnit.Drawing(ControlUnit.GraphicsEditor.Keywords[0], new Tools(SelectTool.DrawingTools),
                            points, LineProperty, FillProperty);
                        Drawables.Clear();

                        FlagPolyFigure = false;
                    }
                    else
                    {
                        Drawables.Add(_factoryDrawFigure.PolyFigure(points, LineProperty, FillProperty,
                            SelectTool.DrawingTools));
                    }
                }

                if (!FlagPolyFigure && e.Button == MouseButtons.Left)
                {
                    List<Point> points = new List<Point>()
                        {new Point(_pointA.X, _pointA.Y), new Point(_pointA.X, _pointA.Y)};
                    Drawables.Add(_factoryDrawFigure.PolyFigure(points, LineProperty, FillProperty,
                        SelectTool.DrawingTools));

                    FlagPolyFigure = true;
                }
            }

            if (SelectTool.TypeTools == TypeTools.SelectFigure)
            {
                if (SelectTool.DrawingTools == DrawingTools.MassSelect)
                {
                    if (Interaction != null)
                    {
                        if (Interaction.MinX > _pointA.X || Interaction.MaxX < _pointA.X || Interaction.MinY > _pointA.Y ||
                            Interaction.MaxY < _pointA.Y)
                        {
                            Interaction = null;
                        }
                        else
                        {
                            ControlUnit.Reconstruction(ControlUnit.GraphicsEditor.Keywords[6],
                                ControlUnit.GraphicsEditor.Drawables, Interaction.Indexes, 0, _pointA, _pointA);
                            _pointC = new Point(e.X, e.Y);
                        }
                    }
                }

                if (SelectTool.DrawingTools == DrawingTools.CursorSelect)
                {
                    if (Interaction != null)
                    {
                        if (Interaction.MinX > _pointA.X || Interaction.MaxX < _pointA.X || Interaction.MinY > _pointA.Y ||
                            Interaction.MaxY < _pointA.Y)
                        {
                            Interaction = null;
                        }
                        else
                        {
                            if (!Interaction.EnablePoints)
                            {
                                ControlUnit.Reconstruction(ControlUnit.GraphicsEditor.Keywords[6],
                                    ControlUnit.GraphicsEditor.Drawables, Interaction.Indexes, 0, _pointA, _pointA);
                                _pointC = new Point(e.X, e.Y);
                            }
                            else
                            {
                                Interaction.SelectPoint = _pointA;
                                ControlUnit.Reconstruction(ControlUnit.GraphicsEditor.Keywords[6],
                                    ControlUnit.GraphicsEditor.Drawables, Interaction.Indexes, 0, _pointA, _pointA);
                            }
                        }
                    }
                }
            }
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            _pointB = new Point(e.X, e.Y);
            RefreshCanvas();

            if (FlagMouseDown)
            {
                if (SelectTool.TypeTools == TypeTools.SimpleFigure)
                {
                    Drawables.RemoveAt(Drawables.Count - 1);
                    Drawables.Add(_factoryDrawFigure.SimpleFigure(_pointA, _pointB, LineProperty, FillProperty,
                        SelectTool.DrawingTools));
                }

                if (SelectTool.TypeTools == TypeTools.SelectFigure)
                {
                    if (SelectTool.DrawingTools == DrawingTools.MassSelect)
                    {
                        if (Interaction != null)
                        {
                            foreach (IDrawable drawable in Interaction.DrawableFigures)
                            {
                                drawable.Position = new Point(
                                    drawable.Position.X + _pointB.X - _pointC.X,
                                    drawable.Position.Y + _pointB.Y - _pointC.Y);
                            }
                            ControlUnit.Undo(1);
                            ControlUnit.Reconstruction(ControlUnit.GraphicsEditor.Keywords[6],
                                ControlUnit.GraphicsEditor.Drawables, Interaction.Indexes, 0, _pointA, _pointB);
                            _pointC = new Point(e.X, e.Y);
                        }
                        else
                        {
                            DrawFigureRectangle rectangle = new DrawFigureRectangle(_pointA, _pointB,
                                new LineProperty(1, Color.Gray, DashStyle.Dash),
                                new FillProperty(Color.Transparent));
                            rectangle.Draw(canvas);
                        }
                    }

                    if (SelectTool.DrawingTools == DrawingTools.CursorSelect)
                    {
                        if (Interaction != null)
                        {
                            if (!Interaction.EnablePoints)
                            {
                                Interaction.DrawableFigures[0].Position = new Point(
                                    Interaction.DrawableFigures[0].Position.X + _pointB.X - _pointC.X,
                                    Interaction.DrawableFigures[0].Position.Y + _pointB.Y - _pointC.Y);
                                ControlUnit.Undo(1);
                                ControlUnit.Reconstruction(ControlUnit.GraphicsEditor.Keywords[6],
                                    ControlUnit.GraphicsEditor.Drawables, Interaction.Indexes, 0, _pointA, _pointB);
                                _pointC = new Point(e.X, e.Y);
                            }
                            else
                            {
                                Interaction.ChangePoint(_pointB);
                                ControlUnit.Undo(1);
                                ControlUnit.Reconstruction(ControlUnit.GraphicsEditor.Keywords[7],
                                    ControlUnit.GraphicsEditor.Drawables,
                                    Interaction.Indexes,Interaction.IndexSelectPoint, _pointA, _pointB);
                            }
                        }
                    }
                }
            }

            if (FlagPolyFigure)
            {
                if (SelectTool.TypeTools == TypeTools.PolyFigure)
                {
                    List<Point> points = Drawables[Drawables.Count - 1].Points;
                    points.RemoveAt(points.Count - 1);
                    points.Add(_pointB);

                    Drawables.RemoveAt(Drawables.Count - 1);
                    Drawables.Add(_factoryDrawFigure.PolyFigure(points, LineProperty, FillProperty,
                        SelectTool.DrawingTools));
                }
            }
        }
        
        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            _pointB = new Point(e.X, e.Y);
            RefreshCanvas();
            if (FlagMouseDown)
            {
                if (SelectTool.TypeTools == TypeTools.SimpleFigure)
                {
                    Drawables.RemoveAt(Drawables.Count - 1);
                    List<Point> points = new List<Point>() {_pointA, _pointB};
                    ControlUnit.Drawing(ControlUnit.GraphicsEditor.Keywords[0], new Tools(SelectTool.DrawingTools),
                        points, LineProperty, FillProperty);
                    Drawables.Clear();
                }

                if (SelectTool.TypeTools == TypeTools.PolyFigure)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        List<Point> points = Drawables[Drawables.Count - 1].Points;
                        points.Add(_pointB);
                        Drawables.RemoveAt(Drawables.Count - 1);
                        Drawables.Add(_factoryDrawFigure.PolyFigure(points, LineProperty, FillProperty,
                            SelectTool.DrawingTools));
                    }
                }

                if (SelectTool.TypeTools == TypeTools.SelectFigure)
                {
                    if (SelectTool.DrawingTools == DrawingTools.MassSelect)
                    {
                        if (Interaction == null)
                        {
                            Interaction = new Interaction(ControlUnit.GraphicsEditor.Drawables, _pointA, _pointB);
                            if (Interaction.DrawableFigures.Count == 1)
                            {
                                if (DragProperty != null) DragProperty(Interaction.DrawableFigures[0]);
                            }

                            if (Interaction.DrawableFigures.Count == 0)
                            {
                                Interaction = null;
                                if (DragProperty != null) DragProperty(null);
                            }
                        }
                        else
                        {
                            ControlUnit.Undo(1);
                            if (_pointA.X != _pointB.X && _pointA.Y != _pointB.Y)
                            {
                                if (!Interaction.EnablePoints)
                                {
                                    ControlUnit.Reconstruction(ControlUnit.GraphicsEditor.Keywords[6],
                                        ControlUnit.GraphicsEditor.Drawables, Interaction.Indexes, 0, _pointA, _pointB);
                                    Interaction.GetMaxMinXY();

                                }
                            }
                        }
                    }

                    if (SelectTool.DrawingTools == DrawingTools.CursorSelect)
                    {
                        if (Interaction == null)
                        {
                            if (e.Button == MouseButtons.Left)
                            {
                                Interaction = new Interaction(ControlUnit.GraphicsEditor.Drawables, _pointB, false);
                                if (Interaction.DrawableFigures.Count == 0)
                                {
                                    Interaction = null;
                                    if (DragProperty != null) DragProperty(null);
                                }
                                else if (DragProperty != null) DragProperty(Interaction.DrawableFigures[0]);
                            }

                            if (e.Button == MouseButtons.Right)
                            {
                                Interaction = new Interaction(ControlUnit.GraphicsEditor.Drawables, _pointB, true);
                                if (Interaction.DrawableFigures.Count == 0)
                                {
                                    Interaction = null;
                                    if (DragProperty != null) DragProperty(null);
                                }
                                else if (DragProperty != null) DragProperty(Interaction.DrawableFigures[0]);
                            }
                        }
                        else
                        {
                            ControlUnit.Undo(1);
                            if (_pointA.X != _pointB.X && _pointA.Y != _pointB.Y)
                            {
                                if (!Interaction.EnablePoints)
                                {
                                    ControlUnit.Reconstruction(ControlUnit.GraphicsEditor.Keywords[6],
                                        ControlUnit.GraphicsEditor.Drawables, Interaction.Indexes, 0, _pointA, _pointB);
                                    Interaction.GetMaxMinXY();

                                }
                                else
                                {
                                    ControlUnit.Reconstruction(ControlUnit.GraphicsEditor.Keywords[7],
                                        ControlUnit.GraphicsEditor.Drawables, Interaction.Indexes,
                                        Interaction.IndexSelectPoint, _pointA, _pointB);
                                    Interaction.GetMaxMinXY();

                                }
                            }
                        }
                    }
                }

                FlagMouseDown = false;
                RefreshCanvas();
            }
        }

        /// <summary>
        /// Скопировать фигуру(ы) в буфер
        /// </summary>
        public void Copy()
        {
            BuferDraw.Clear();
            if (SelectTool.TypeTools == TypeTools.SelectFigure && ControlUnit.GraphicsEditor.Drawables.Count > 0 && Interaction != null)
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
                    if (_pointB.X > 0 && _pointB.X < GetWidthCanvas() && _pointB.Y > 0 && _pointB.Y < GetHeightCanvas())
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
                    GetIndexes(drawables, ControlUnit.GraphicsEditor.Drawables),0,
                    new Point(), new Point());

                RefreshCanvas();
            }
        }

        /// <summary>
        /// Удалить фигуру(ы)
        /// </summary>
        public void Delete()
        {
            if (SelectTool.TypeTools == TypeTools.SelectFigure && ControlUnit.GraphicsEditor.Drawables.Count > 0 && Interaction != null)
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
            if (SelectTool.TypeTools == TypeTools.SelectFigure && ControlUnit.GraphicsEditor.Drawables.Count > 0 && Interaction != null)
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

        public void AddFigureInInteractive()
        {
            Console.WriteLine("Ctrl");
        }
    }
}