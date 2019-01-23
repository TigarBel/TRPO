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
                    RefreshCanvas();
                }

                _flagPolyFigure = value;
            }
        }

        /// <summary>
        /// Буфер для цвета линии фигуры
        /// </summary>
        private List<Color> _lineColor = new List<Color>();

        /// <summary>
        /// Буфер для цвета заливки фигуры
        /// </summary>
        private List<Color> _fillColor = new List<Color>();

        /// <summary>
        /// Список фигур хронящихся в памяти
        /// </summary>
        private List<IDrawable> _buferDraw = new List<IDrawable>();

        /// <summary>
        /// Управляющий элемент
        /// </summary>
        private ControlUnit _controlUnit = new ControlUnit();

        /// <summary>
        /// Список фигур
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

        /// <summary>
        /// Список фигур
        /// </summary>
        public List<IDrawable> Drawables
        {
            get
            {
                return _drawables; //ControlUnit.GraphicsEditor.Drawables;
            }
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
            canvas.Image = new Bitmap(_image);

            foreach (IDrawable drawable in ControlUnit.GraphicsEditor.Drawables)
            {
                drawable.Draw(canvas);
            }

            foreach (IDrawable drawable in Drawables)
            {
                drawable.Draw(canvas);
            }

            if (Interaction != null && !FlagMouseDown)
            {
                Interaction.DrawSelcet(canvas);
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
                    }
                }
                /*
                if (Interaction != null)
                {

                    if (SelectTool.DrawingTools == DrawingTools.CursorSelect)
                    {
                        if (Interaction.EnablePoints)
                        {
                            Checking checking = new Checking();
                            if (checking.GetNumberPoint(_pointA, Interaction.DrawableFigures[0],
                                    4 ) != -1 || _pointA.X == _pointB.X ||
                                _pointA.Y == _pointB.Y)
                            {
                                Interaction.SelectPoint = _pointA;
                                _flagSelectPoint = true;
                            }
                            else
                            {
                                Interaction = null;
                            }
                        }
                        else if (Interaction.DrawableFigures[0].Position.X > _pointA.X &&
                                 Interaction.DrawableFigures[0].Position.Y > _pointA.Y &&
                                 Interaction.DrawableFigures[0].Position.X + Interaction.DrawableFigures[0].Width <
                                 _pointA.X &&
                                 Interaction.DrawableFigures[0].Position.Y + Interaction.DrawableFigures[0].Height <
                                 _pointA.Y)
                        {
                            Interaction = null;
                            RefreshCanvas();
                        }

                    }

                    if (SelectTool.DrawingTools == DrawingTools.MassSelect)
                    {
                        List<Point> localPoints = new List<Point>();
                        foreach (IDrawable drawable in Interaction.DrawableFigures)
                        {
                            foreach (Point point in drawable.Points)
                            {
                                localPoints.Add(point);
                            }
                        }

                        if (localPoints.Min(point => point.X) > _pointA.X ||
                            localPoints.Min(point => point.Y) > _pointA.Y ||
                            localPoints.Max(point => point.X) < _pointA.X ||
                            localPoints.Max(point => point.Y) < _pointA.Y)
                        {
                            Interaction = null;
                            RefreshCanvas();
                        }
                    }
                }
            */}
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
                        if (Interaction == null)
                        {
                            DrawFigureRectangle rectangle = new DrawFigureRectangle(_pointA, _pointB,
                                new LineProperty(1, Color.Gray, DashStyle.Dash),
                                new FillProperty(Color.Transparent));
                            rectangle.Draw(canvas);
                        }
                    }
                    /*
                    if (SelectTool.DrawingTools == DrawingTools.CursorSelect)
                    {
                        if (Interaction != null)
                        {
                            if (Interaction.EnablePoints)
                            {
                                Interaction.ChangePoint(_pointB);
                                RefreshCanvas();
                            }
                            else if (_pointA.X != _pointB.X || _pointA.Y != _pointB.Y)
                            {
                                //DrawEmpty();
                            }
                        }
                    }

                    if (SelectTool.DrawingTools == DrawingTools.MassSelect)
                    {
                        if (Interaction == null)
                        {
                            RefreshCanvas();
                            DrawFigureRectangle drawFigureRectangle = new DrawFigureRectangle(_pointA, _pointB,
                                new LineProperty(1, Color.Gray, DashStyle.Dash), new FillProperty(Color.Transparent));
                            drawFigureRectangle.Draw(canvas);
                        }
                        else if (_pointA.X != _pointB.X || _pointA.Y != _pointB.Y)
                        {
                            //DrawEmpty();
                        }
                    }
                */}
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
                            if (Interaction.DrawableFigures.Count == 0) Interaction = null;
                        }
                    }
                    /*
                    if (Interaction == null)
                    {
                        if (SelectTool.DrawingTools == DrawingTools.CursorSelect)
                        {
                            Interaction = null;
                            _pointA = new Point(e.X, e.Y);
                            if (DragProperty != null) DragProperty(null);
                            for (int i = Drawables.Count - 1; i >= 0; i--)
                            {
                                int minX = Drawables[i].Points.Min(point => point.X);
                                int maxX = Drawables[i].Points.Max(point => point.X);
                                int minY = Drawables[i].Points.Min(point => point.Y);
                                int maxY = Drawables[i].Points.Max(point => point.Y);
                                if (_pointA.X >= minX && _pointA.X <= maxX && _pointA.Y >= minY && _pointA.Y <= maxY)
                                {
                                    if (DragProperty != null) DragProperty(Drawables[i]);
                                    if (e.Button == MouseButtons.Left)
                                    {
                                        //Interaction = new Interaction(Drawables[i], false);
                                        //Interaction.DrawSelcet(canvas, Interaction.EnablePoints, Interaction.DrawableFigures);
                                    }
                                    else if (e.Button == MouseButtons.Right)
                                    {
                                        //Interaction = new Interaction(Drawables[i], true);
                                        //Interaction.DrawSelcet(canvas, Interaction.EnablePoints, Interaction.DrawableFigures);
                                    }

                                    //break;
                                }
                            }
                        }

                        if (SelectTool.DrawingTools == DrawingTools.MassSelect)
                        {

                            List<Point> points = new List<Point>();
                            points.Add(_pointA);
                            points.Add(_pointB);
                            List<IDrawable> localDrawables = new List<IDrawable>();

                            foreach (IDrawable drawable in Drawables)
                            {
                                int X = drawable.Points.Max(point => point.X) -
                                        (drawable.Points.Max(point => point.X) -
                                         drawable.Points.Min(point => point.X)) / 2;
                                int Y = drawable.Points.Max(point => point.Y) -
                                        (drawable.Points.Max(point => point.Y) -
                                         drawable.Points.Min(point => point.Y)) / 2;

                                if (X >= points.Min(point => point.X) &&
                                    X <= points.Max(point => point.X) &&
                                    Y >= points.Min(point => point.Y) &&
                                    Y <= points.Max(point => point.Y))
                                {
                                    localDrawables.Add(drawable);
                                }
                            }

                            if (localDrawables.Count > 0)
                            {
                                //Interaction = new Interaction(localDrawables, false);
                                //Interaction.DrawSelcet(canvas, Interaction.EnablePoints, Interaction.DrawableFigures);
                                //
                            }

                            if (Interaction == null)
                            {
                                Interaction = new Interaction();
                                int Xmin = points.Min(point => point.X);
                                int Ymin = points.Min(point => point.Y);
                                int Xmax = points.Max(point => point.X);
                                int Ymax = points.Max(point => point.Y);
                                foreach (IDrawable drawable in Drawables)
                                {
                                    if (Xmin >= drawable.Points.Min(point => point.X) &&
                                        Xmax <= drawable.Points.Max(point => point.X) &&
                                        Ymin >= drawable.Points.Min(point => point.Y) &&
                                        Ymax <= drawable.Points.Max(point => point.Y))
                                    {
                                        Interaction.AddDrawableFigure(drawable);
                                    }
                                }

                                if (Interaction.DrawableFigures.Count == 0)
                                {
                                    Interaction = null;
                                }
                                else
                                {
                                    //Interaction.DrawSelcet(canvas, Interaction.EnablePoints, Interaction.DrawableFigures);
                                }
                            }
                        }

                    }
                    else
                    {
                        if (_lineColor.Count != 0)
                        {
                            int idLineColor = 0;
                            int idFillColor = 0;
                            foreach (IDrawable drawable in Interaction.DrawableFigures)
                            {
                                if (drawable is ILinePropertyble drawableWithLinePropertyble)
                                {
                                    drawableWithLinePropertyble.LineProperty.LineColor = _lineColor[idLineColor];
                                    idLineColor++;
                                }

                                if (drawable is IFillPropertyble drawableWithFillPropertyble)
                                {
                                    drawableWithFillPropertyble.FillProperty.FillColor = _fillColor[idFillColor];
                                    idFillColor++;
                                }
                            }

                            ControlUnit.Reconstruction(ControlUnit.GraphicsEditor.Keywords[6],
                                Interaction.DrawableFigures,
                                GetIndexes(Interaction.DrawableFigures, Drawables), _pointA, _pointB);
                            //Interaction.EnablePoints = false;
                            _lineColor = new List<Color>();
                            _fillColor = new List<Color>();
                        }

                        else if (_flagSelectPoint)
                        {
                            Interaction.SelectPoint = _pointB;
                            Interaction.ChangePoint(_pointA);

                            ControlUnit.Reconstruction(ControlUnit.GraphicsEditor.Keywords[7],
                                Interaction.DrawableFigures, GetIndexes(Interaction.DrawableFigures, Drawables),
                                _pointA, _pointB
                            );
                            RefreshCanvas();
                            _flagSelectPoint = false;
                        }
                    }

                    RefreshCanvas();

                */}

                FlagMouseDown = false;
                RefreshCanvas();
            }
        }

        private void DrawEmpty()
        {
            RefreshCanvas();
            List<IDrawable> drawables = new List<IDrawable>();
            foreach (IDrawable drawable in Interaction.DrawableFigures)
            {
                drawables.Add(drawable.Clone());
                if (drawable is ILinePropertyble drawableWithLinePropertyble)
                {
                    _lineColor.Add(drawableWithLinePropertyble.LineProperty.LineColor);
                    drawableWithLinePropertyble.LineProperty.LineColor = Color.Transparent;
                }

                if (drawable is IFillPropertyble drawableWithFillPropertyble)
                {
                    _fillColor.Add(drawableWithFillPropertyble.FillProperty.FillColor);
                    drawableWithFillPropertyble.FillProperty.FillColor = Color.Transparent;
                }
            }

            foreach (IDrawable drawable in drawables)
            {
                int idLineProperty = 0;
                int idFillProperty = 0;

                if (drawable is ILinePropertyble drawableWithLinePropertyble)
                {
                    drawableWithLinePropertyble.LineProperty.LineColor = _lineColor[idLineProperty];
                    idLineProperty++;
                }

                if (drawable is IFillPropertyble drawableWithFillPropertyble)
                {
                    drawableWithFillPropertyble.FillProperty.FillColor = _fillColor[idFillProperty];
                    idFillProperty++;
                }

                int x = drawable.Position.X;
                int y = drawable.Position.Y;
                drawable.Position = new Point(x + (_pointB.X - _pointA.X), y + (_pointB.Y - _pointA.Y));
                drawable.Draw(canvas);
            }
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
                }

                ControlUnit.Reconstruction(ControlUnit.GraphicsEditor.Keywords[8], drawables,
                    GetIndexes(drawables, Drawables),
                    new Point(), new Point());

                RefreshCanvas();
                //Interaction = new Interaction(Drawables.GetRange(Drawables.Count - BuferDraw.Count, BuferDraw.Count), false);
                //Interaction.DrawSelcet(canvas, Interaction.EnablePoints, Interaction.DrawableFigures);
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
                    indexes.Add(count + Drawables.Count);
                    count++;
                }

            }

            return indexes;
        }

        /*public void AddFigureInInteractive()
        {

        }*/
    }
}