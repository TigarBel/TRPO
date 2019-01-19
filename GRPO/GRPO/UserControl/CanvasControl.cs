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

namespace GRPO
{
    /// <summary>
    /// Пользовательский интерфейс полотна для рисования
    /// </summary>
    public partial class CanvasControl : UserControl
    {

        public delegate void Drag(IDrawable drawable);
        public event Drag DragProperty;

        public delegate void CanvasControChanged();
        public event CanvasControChanged SaveStep;

        /// <summary>
        /// Начальная точка / Точка в момент нажатия кнопки мыши
        /// </summary>
        private Point _pointA;
        /// <summary>
        /// Конечная точка / Точка в момент отжатия кнопки мыши
        /// </summary>
        private Point _pointB;
        /// <summary>
        /// Флаг устанавливаемый при зажатии кнопки мыши
        /// </summary>
        private bool _flagMouseDown;
        /// <summary>
        /// Флаг при создании полифигур
        /// </summary>
        private bool _flagPolyFigure;
        /// <summary>
        /// Список фигур хронящихся в памяти
        /// </summary>
        private List<IDrawable> _buferDraw = new List<IDrawable>();
        /// <summary>
        /// Список фигур
        /// </summary>
        private List<IDrawable> _drawables = new List<IDrawable>();
        /// <summary>
        /// Инструмент для рисования
        /// </summary>
        private Tools _selectTool;
        /// <summary>
        /// Свойство линии
        /// </summary>
        private LineProperty _lineProperty;
        /// <summary>
        /// Свойство заливки
        /// </summary>
        private FillProperty _fillProperty;
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
            SetSizeCanvas(100, 50);
            SelectTool = new Tools(DrawingTools.DrawFigureLine);
            LineProperty = new LineProperty();
            FillProperty = new FillProperty();
        }
        /// <summary>
        /// Список фигур
        /// </summary>
        public List<IDrawable> Drawables
        {
            get
            {
                return _drawables;
            }
            set
            {
                _drawables = value;
                if (_drawables != null)
                {
                    if (_drawables.Count > 0)
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
            get
            {
                return _selectTool;
            }
            set
            {
                _selectTool = value;
                
                if (value.DrawingTools != DrawingTools.CursorSelect)
                {
                    Interaction = null;
                }
                _flagMouseDown = false;
                _flagPolyFigure = false;
                RefreshCanvas();
            }
        }
        /// <summary>
        /// Свойство линии
        /// </summary>
        public LineProperty LineProperty
        {
            get
            {
                return _lineProperty;
            }
            set
            {
                _lineProperty = value;
            }
        }
        /// <summary>
        /// Свойство заливки
        /// </summary>
        public FillProperty FillProperty
        {
            get
            {
                return _fillProperty;
            }
            set
            {
                _fillProperty = value;
            }
        }
        /// <summary>
        /// Картинка с холста
        /// </summary>
        public Image Image
        {
            get
            {
                return new Bitmap(canvas.Image);
            }
            set
            {
                canvas.Image = new Bitmap(value);
            }
        }
        /// <summary>
        /// Объект взаимодействия с нарисованными фигурами
        /// </summary>
        public Interaction Interaction
        {
            get
            {
                return _interaction;
            }
            set
            {
                _interaction = value;
            }
        }
        /// <summary>
        /// Список фигур хронящихся в памяти
        /// </summary>
        public List<IDrawable> BuferDraw
        {
            get
            {
                return _buferDraw;
            }
            set
            {
                _buferDraw = value;
            }
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
            if(Drawables.Count > 0)
            {
                foreach(IDrawable draw in Drawables)
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
            canvas.Image = new Bitmap(canvas.Width,canvas.Height);
            foreach (IDrawable drawable in Drawables)
            {
                drawable.Draw(canvas);
            }

            if (Interaction != null)
            {
                Interaction.DrawSelcet(canvas);
            }
        }
        /// <summary>
        /// Очистка холста
        /// </summary>
        public void ClearCanvas()
        {
            Drawables.Clear();
            canvas.Image = new Bitmap(canvas.Width, canvas.Height);
        }

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            _flagMouseDown = true;
            _pointA = new Point(e.X, e.Y);

            if (SelectTool.TypeTools == TypeTools.SimpleFigure) 
            {
                Drawables.Add(DrawFigure(_pointA, _pointA, SelectTool.DrawingTools));
            }

            if (SelectTool.TypeTools == TypeTools.PolyFigure)
            {
                if (_flagPolyFigure)
                {
                    List<Point> points = Drawables[Drawables.Count - 1].GetPoints();
                    points.RemoveAt(points.Count - 1);
                    points.Add(_pointA);

                    Drawables.RemoveAt(Drawables.Count - 1);
                    Drawables.Add(DrawPolyFigure(points, SelectTool.DrawingTools));
                }
                if (!_flagPolyFigure)
                {
                    List<Point> points = new List<Point>() { new Point(_pointA.X, _pointA.Y), new Point(_pointA.X, _pointA.Y) };
                    Drawables.Add(DrawPolyFigure(points, SelectTool.DrawingTools));

                    _flagPolyFigure = true;
                }
                if (e.Button == MouseButtons.Right && _flagPolyFigure)
                {
                    List<Point> points = Drawables[Drawables.Count - 1].GetPoints();
                    points.RemoveAt(points.Count - 1);
                    points.Add(_pointA);

                    Drawables.RemoveAt(Drawables.Count - 1);
                    Drawables.Add(DrawPolyFigure(points, SelectTool.DrawingTools));

                    _flagPolyFigure = false;
                }
            }

            if (SelectTool.TypeTools == TypeTools.SelectFigure)
            {
                if (Interaction != null)
                {
                    if (SelectTool.DrawingTools == DrawingTools.CursorSelect)
                    {
                        if (Interaction.EnablePoints)
                        {
                            Interaction.SelectPoint = _pointA;
                        }
                        else if (Interaction.DrawableFigures[0].Position.X > _pointA.X ||
                            Interaction.DrawableFigures[0].Position.Y > _pointA.Y ||
                            Interaction.DrawableFigures[0].Position.X + Interaction.DrawableFigures[0].Width < _pointA.X ||
                            Interaction.DrawableFigures[0].Position.Y + Interaction.DrawableFigures[0].Height < _pointA.Y)
                        {
                            Interaction = null;
                            RefreshCanvas();
                        }

                    }
                    if (SelectTool.DrawingTools == DrawingTools.MassSelect)
                    {
                        List<Point> localPoints = new List<Point>();
                        foreach(IDrawable drawable in Interaction.DrawableFigures)
                        {
                            foreach(Point point in drawable.GetPoints())
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
            }
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            _pointB = new Point(e.X, e.Y);

            if (_flagMouseDown)
            {
                if (SelectTool.TypeTools == TypeTools.SimpleFigure)
                {
                    Drawables.RemoveAt(Drawables.Count - 1);
                    Drawables.Add(DrawFigure(_pointA, _pointB, SelectTool.DrawingTools));
                    RefreshCanvas();
                }

                if (SelectTool.TypeTools == TypeTools.SelectFigure)
                {
                    if (SelectTool.DrawingTools == DrawingTools.CursorSelect)
                    {
                        if (Interaction != null)
                        {
                            if (Interaction.EnablePoints)
                            {
                                Interaction.ChangePoint(_pointB);
                                RefreshCanvas();
                                _pointA = new Point(e.X, e.Y);
                            }
                            else
                            {
                                int x = Interaction.DrawableFigures[0].Position.X;
                                int y = Interaction.DrawableFigures[0].Position.Y;
                                Interaction.DrawableFigures[0].Position = new Point(x + (_pointB.X - _pointA.X), y + (_pointB.Y - _pointA.Y));
                                RefreshCanvas();
                                _pointA = new Point(e.X, e.Y);
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
                        else
                        {
                            foreach(IDrawable drawable in Interaction.DrawableFigures)
                            {
                                int x = drawable.Position.X;
                                int y = drawable.Position.Y;
                                drawable.Position = new Point(x + (_pointB.X - _pointA.X), y + (_pointB.Y - _pointA.Y));
                            }
                            RefreshCanvas();
                            _pointA = new Point(e.X, e.Y);
                        }
                    }
                }
            }

            if(_flagPolyFigure)
            {
                if (SelectTool.TypeTools == TypeTools.PolyFigure)
                {
                    List<Point> points = Drawables[Drawables.Count - 1].GetPoints();
                    points.RemoveAt(points.Count - 1);
                    points.Add(_pointB);

                    Drawables.RemoveAt(Drawables.Count - 1);
                    Drawables.Add(DrawPolyFigure(points, SelectTool.DrawingTools));
                    RefreshCanvas();
                }
            }
        }

        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            _pointB = new Point(e.X, e.Y);

            if (_flagMouseDown)
            {
                if (SelectTool.TypeTools == TypeTools.SimpleFigure)
                {
                    Drawables.RemoveAt(Drawables.Count - 1);
                    Drawables.Add(DrawFigure(_pointA, _pointB, SelectTool.DrawingTools));
                    RefreshCanvas();
                    if (SaveStep != null) SaveStep();
                }

                if (SelectTool.TypeTools == TypeTools.PolyFigure)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        List<Point> points = Drawables[Drawables.Count - 1].GetPoints();
                        points.Add(_pointB);
                        Drawables.RemoveAt(Drawables.Count - 1);
                        Drawables.Add(DrawPolyFigure(points, SelectTool.DrawingTools));
                        RefreshCanvas();
                        if (SaveStep != null) SaveStep();
                    }
                }

                if (SelectTool.TypeTools == TypeTools.SelectFigure) 
                {
                    if (SelectTool.DrawingTools == DrawingTools.CursorSelect)
                    {
                        Interaction = null;
                        _pointA = new Point(e.X, e.Y);
                        if (DragProperty != null) DragProperty(null);
                        for (int i = Drawables.Count - 1; i >= 0; i--)
                        {
                            int minX = Drawables[i].GetPoints().Min(point => point.X);
                            int maxX = Drawables[i].GetPoints().Max(point => point.X);
                            int minY = Drawables[i].GetPoints().Min(point => point.Y);
                            int maxY = Drawables[i].GetPoints().Max(point => point.Y);
                            if (_pointA.X >= minX && _pointA.X <= maxX && _pointA.Y >= minY && _pointA.Y <= maxY)
                            {
                                if (DragProperty != null) DragProperty(Drawables[i]);
                                if (e.Button == MouseButtons.Left)
                                {
                                    Interaction = new Interaction(Drawables[i], false);
                                    Interaction.DrawSelcet(canvas);
                                    //if (SaveStep != null) SaveStep();
                                }
                                else if (e.Button == MouseButtons.Right)
                                {
                                    Interaction = new Interaction(Drawables[i], true);
                                    Interaction.DrawSelcet(canvas);
                                    //if (SaveStep != null) SaveStep();
                                }
                                break;
                            }
                        }
                    }
                    if (SelectTool.DrawingTools == DrawingTools.MassSelect)
                    {
                        if (Interaction == null)
                        {
                            List<IDrawable> localDrawables = new List<IDrawable>();

                            foreach (IDrawable drawable in Drawables)
                            {
                                int X = drawable.GetPoints().Max(point => point.X) -
                                    (drawable.GetPoints().Max(point => point.X) - drawable.GetPoints().Min(point => point.X)) / 2;
                                int Y = drawable.GetPoints().Max(point => point.Y) -
                                    (drawable.GetPoints().Max(point => point.Y) - drawable.GetPoints().Min(point => point.Y)) / 2;

                                List<Point> points = new List<Point>();
                                points.Add(_pointA);
                                points.Add(_pointB);
                                if (X >= points.Min(point => point.X) &&
                                    X <= points.Max(point => point.X) &&
                                    Y >= points.Min(point => point.Y) &&
                                    Y <= points.Max(point => point.Y))
                                {
                                    localDrawables.Add(drawable);
                                }
                            }

                            RefreshCanvas();
                            if (localDrawables.Count > 0)
                            {
                                Interaction = new Interaction(localDrawables, false);
                                Interaction.DrawSelcet(canvas);
                                //if (SaveStep != null) SaveStep();
                            }
                        }
                        else
                        {
                            Interaction.EnablePoints = false;
                        }
                    }
                    if (SaveStep != null) SaveStep();
                }

                _flagMouseDown = false;
            }
        }
        /// <summary>
        /// Создать простую фигуру из двух точек
        /// </summary>
        /// <param name="pointA">Начальная точка</param>
        /// <param name="pointB">Конечная точка</param>
        /// <param name="selectTool">Иструмент(тип) выбранно фигуры</param>
        /// <returns></returns>
        private IDrawable DrawFigure(Point pointA, Point pointB, DrawingTools selectTool)
        {
            switch (selectTool)
            {
                case DrawingTools.DrawFigureLine:
                    {
                        DrawFigureLine drawFigure = new DrawFigureLine(pointA, pointB, LineProperty);
                        return drawFigure;
                    }
                case DrawingTools.DrawFigureRectangle:
                    {
                        DrawFigureRectangle drawFigure = new DrawFigureRectangle(pointA, pointB, LineProperty, FillProperty);
                        return drawFigure;
                    }
                case DrawingTools.DrawFigureCircle:
                    {
                        DrawFigureCircle drawFigure = new DrawFigureCircle(pointA,
                        Convert.ToInt32(Math.Sqrt(Convert.ToDouble(Math.Pow((pointB.X - pointA.X), 2) + Math.Pow((pointB.Y - pointA.Y), 2)))),/******/
                        LineProperty, FillProperty);
                        return drawFigure;
                    }
                case DrawingTools.DrawFigureEllipse:
                    {
                        DrawFigureEllipse drawFigure = new DrawFigureEllipse(pointA, pointB.X - pointA.X, pointB.Y - pointA.Y, LineProperty,
                            FillProperty);
                        return drawFigure;
                    }
            }
            return null;
        }
        /// <summary>
        /// Создать сложную фигуру из нескольких точек
        /// </summary>
        /// <param name="points">Список точек</param>
        /// <param name="selectTool">Иструмент(тип) выбранно фигуры</param>
        /// <returns></returns>
        private IDrawable DrawPolyFigure(List<Point> points, DrawingTools selectTool)
        {
            switch(selectTool)
            {
                case DrawingTools.DrawFigurePolyline:
                    {
                        DrawFigurePolyline drawFigure = new DrawFigurePolyline(points, false, LineProperty);
                        return drawFigure;
                    }
            }
            return null;
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
                    foreach (Point point in drawable.GetPoints())
                    {
                        localPoints.Add(point);
                    }
                }

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
                    Drawables.Add(drawable.Clone());
                }
                RefreshCanvas();
                Interaction = new Interaction(Drawables.GetRange(Drawables.Count - BuferDraw.Count, BuferDraw.Count), false);
                Interaction.DrawSelcet(canvas);
            }
        }
        /// <summary>
        /// Удалить фигуру(ы)
        /// </summary>
        public void Delete()
        {
            if (SelectTool.TypeTools == TypeTools.SelectFigure && Drawables.Count > 0 && Interaction != null)
            {
                foreach (IDrawable drawable in Interaction.DrawableFigures)
                {
                    Drawables.Remove(drawable);
                }

                RefreshCanvas();
                Interaction = null;
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
                    Drawables.Remove(drawable);
                }

                RefreshCanvas();
                Interaction = null;
                if (DragProperty != null) DragProperty(null);
            }
        }
    }
}