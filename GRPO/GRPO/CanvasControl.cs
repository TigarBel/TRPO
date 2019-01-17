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
        private Point _pointA;
        private Point _pointB;
        private bool _flagMouseDown;
        private bool _flagPolyline;
        private List<IDrawable> _draws = new List<IDrawable>();
        private IDrawable _buferDraw;
        private List<Bitmap> _bitmaps = new List<Bitmap>();
        private Image _backStep;
        private DrawingTools _selectTool = DrawingTools.DrawFigureLine;
        private LineProperty _lineProperty = new LineProperty();
        private FillProperty _fillProperty = new FillProperty();


        public Interaction _interaction;
        public MassSelect _massSelect;

        public delegate void Drag(IDrawable drawable);
        public event Drag DragProperty;
        /// <summary>
        /// Инициализация пользовательского интерфейса полотна для рисования
        /// </summary>
        public CanvasControl()
        {
            InitializeComponent();
            SetSizeCanvas(100, 50);
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
            if(_draws.Count > 0)
            {
                foreach(IDrawable draw in _draws)
                {
                    draw.Draw();
                }
            }
        }
        /// <summary>
        /// Взять список фигур
        /// </summary>
        public List<IDrawable> GetDrawables() { return _draws; }
        /// <summary>
        /// Вложить список фигур
        /// </summary>
        public void SetDrawables(List<IDrawable> drawables) { _draws = drawables; }
        /// <summary>
        /// Выбранный тип класса IDrawable
        /// </summary>
        public DrawingTools SelectTool
        {
            get
            {
                return _selectTool;
            }
            set
            {
                _selectTool = value;
                if (_backStep != null)
                {
                    _flagMouseDown = false;
                    _flagPolyline = false;
                    canvas.Image = new Bitmap(_backStep);
                }
                if(_selectTool != DrawingTools.CursorSelect)
                {
                    _interaction = null;
                }
            }
        }
        /// <summary>
        /// Очистка холста
        /// </summary>
        public void ClearCanvas()
        {
            _draws.Clear();
            canvas.Image = new Bitmap(canvas.Width, canvas.Height);
            _backStep = new Bitmap(canvas.Image);
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

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            _flagMouseDown = true;
            _pointA = new Point(e.X, e.Y);

            if (SelectTool != DrawingTools.CursorSelect && SelectTool != DrawingTools.MassSelect && _flagPolyline != true)
            {
                _backStep = new Bitmap(canvas.Image);
            }
            if (SelectTool == DrawingTools.CursorSelect && _interaction != null)
            {
                _interaction.SelectPoint = _pointA;
                if (e.Button == MouseButtons.Left)
                {
                    _interaction.EnablePoints = false;
                }
                if (e.Button == MouseButtons.Right)
                {
                    _interaction.EnablePoints = true;
                }
            }

            if (SelectTool == DrawingTools.MassSelect)
            {
                if (_massSelect != null)
                {
                    if (_pointA.X < _massSelect.PointsSize.Min(point => point.X) &&
                        _pointA.X > _massSelect.PointsSize.Max(point => point.X) &&
                        _pointA.Y < _massSelect.PointsSize.Min(point => point.Y) &&
                        _pointA.Y > _massSelect.PointsSize.Min(point => point.Y))
                    {
                        _massSelect = null;
                    }
                }
            }
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            _pointB = new Point(e.X, e.Y);

            if (SelectTool != DrawingTools.CursorSelect && SelectTool != DrawingTools.MassSelect && _flagMouseDown)
            {
                canvas.Image = new Bitmap(_backStep);
                DrawFigure(_pointA, _pointB, SelectTool).Draw();
            }
            if (SelectTool == DrawingTools.CursorSelect && _flagMouseDown && _interaction != null)
            {
                if (_interaction.EnablePoints)
                {
                    _interaction.ChangePoint(_pointB);
                    RefreshCanvas();
                    _pointA = new Point(e.X, e.Y);
                }
                else
                {
                    int x = _interaction.DrawableFigure.Position.X;
                    int y = _interaction.DrawableFigure.Position.Y;
                    _interaction.DrawableFigure.Position = new Point(x + (_pointB.X - _pointA.X), y + (_pointB.Y - _pointA.Y));
                    RefreshCanvas();
                    _pointA = new Point(e.X, e.Y);
                }
            }


            if (SelectTool == DrawingTools.MassSelect && _draws.Count > 0 && _flagMouseDown)
            {
                if (_massSelect == null)
                {
                    canvas.Image = new Bitmap(_backStep);
                    DrawFigureRectangle drawFigureRectangle = new DrawFigureRectangle(_pointA, _pointB, canvas,
                        new LineProperty(1, Color.Gray, DashStyle.Dash), new FillProperty(Color.Transparent));
                    drawFigureRectangle.Draw();
                }
                else
                {
                    foreach (IDrawable drawable in _massSelect.Drawables)
                    {
                        int x = drawable.Position.X;
                        int y = drawable.Position.Y;
                        drawable.Position = new Point(x + (_pointB.X - _pointA.X), y + (_pointB.Y - _pointA.Y));
                        RefreshCanvas();
                        _pointA = new Point(e.X, e.Y);
                    }
                }
            }
        }

        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            _pointB = new Point(e.X, e.Y);

            if (SelectTool != DrawingTools.CursorSelect && SelectTool != DrawingTools.MassSelect && _flagMouseDown)
            {
                //_pointB = new Point(e.X, e.Y);
                canvas.Image = new Bitmap(_backStep);
                DrawFigure(_pointA, _pointB, SelectTool).Draw();
                _draws.Add(DrawFigure(_pointA, _pointB, SelectTool));
                _backStep = new Bitmap(canvas.Image);
            }

            _flagMouseDown = false;

            if (SelectTool == DrawingTools.DrawFigurePolyline)
            {
                if(_flagPolyline)
                {
                    _draws.Remove(_draws[_draws.Count - 2]);
                }
                if(e.Button == MouseButtons.Left)
                {
                    _flagMouseDown = true;
                    _flagPolyline = true;
                    _pointA = new Point(e.X, e.Y);
                }
                else
                {
                    _flagPolyline = false;
                }
            }

            if (SelectTool == DrawingTools.MassSelect && _draws.Count > 0)
            {
                if(_massSelect == null)
                {
                    canvas.Image = new Bitmap(_backStep);
                    _massSelect = new MassSelect(_pointA, _pointB, _draws, canvas);
                    if (_massSelect.Drawables.Count > 0)
                    {
                        _massSelect.DrawInteraction();
                    }
                    else
                    {
                        _massSelect = null;
                    }
                }
            }
        }

        private void canvas_MouseClick(object sender, MouseEventArgs e)
        {
            if (SelectTool == DrawingTools.CursorSelect && _draws.Count > 0)
            {
                _interaction = null;
                canvas.Image = new Bitmap(_backStep);
                _pointA = new Point(e.X, e.Y);
                if (DragProperty != null) DragProperty(null);
                for (int i = _draws.Count - 1; i >= 0; i--)
                {
                    int minX = _draws[i].GetPoints().Min(point => point.X);
                    int maxX = _draws[i].GetPoints().Max(point => point.X);
                    int minY = _draws[i].GetPoints().Min(point => point.Y);
                    int maxY = _draws[i].GetPoints().Max(point => point.Y);
                    if (_pointA.X >= minX && _pointA.X <= maxX && _pointA.Y >= minY && _pointA.Y <= maxY)
                    {
                        if (DragProperty != null) DragProperty(_draws[i]);
                        if (e.Button == MouseButtons.Left)
                        {
                            _interaction = new Interaction(_draws[i], canvas, false);
                        }
                        if (e.Button == MouseButtons.Right)
                        {
                            _interaction = new Interaction(_draws[i], canvas, true);
                        }
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// Создать фигуру
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
                        DrawFigureLine drawFigure = new DrawFigureLine(pointA, pointB, canvas, LineProperty);
                        return drawFigure;
                    }
                case DrawingTools.DrawFigurePolyline:
                    {
                        if (_flagPolyline)
                        {
                            List<Point> points;
                            points = _draws[_draws.Count - 1].GetPoints();
                            points.Add(pointB);
                            DrawFigurePolyline drawFigure = new DrawFigurePolyline(points, false, canvas, LineProperty);
                            return drawFigure;
                        }
                        else
                        {
                            DrawFigureLine drawFigure = new DrawFigureLine(pointA, pointB, canvas, _lineProperty);
                            return drawFigure;
                        }
                    }
                case DrawingTools.DrawFigureRectangle:
                    {
                        DrawFigureRectangle drawFigure = new DrawFigureRectangle(pointA, pointB, canvas, LineProperty, FillProperty);
                        return drawFigure;
                    }
                case DrawingTools.DrawFigureCircle:
                    {
                        DrawFigureCircle drawFigure = new DrawFigureCircle(pointA,
                        Convert.ToInt32(Math.Sqrt(Convert.ToDouble(Math.Pow((pointB.X - pointA.X), 2) + Math.Pow((pointB.Y - pointA.Y), 2)))),/******/
                        canvas, LineProperty, FillProperty);
                        return drawFigure;
                    }
                case DrawingTools.DrawFigureEllipse:
                    {
                        DrawFigureEllipse drawFigure = new DrawFigureEllipse(pointA, pointB.X - pointA.X, pointB.Y - pointA.Y, canvas, LineProperty,
                            FillProperty);
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
            if (SelectTool == DrawingTools.CursorSelect && _draws.Count > 0 && _interaction != null)
            {
                _buferDraw = _interaction.DrawableFigure.Clone();
            }
        }
        /// <summary>
        /// Вставить фигуру(ы)
        /// </summary>
        public void Paste()
        {
            if (SelectTool == DrawingTools.CursorSelect && _buferDraw != null)
            {
                canvas.Image = new Bitmap(_backStep);
                _buferDraw.Position = new Point(10, 10);
                _draws.Add(_buferDraw);
                _draws[_draws.Count - 1].Draw();
                _backStep = new Bitmap(canvas.Image);
            }
        }
        /// <summary>
        /// Удалить фигуру(ы)
        /// </summary>
        public void Delete()
        {
            if (SelectTool == DrawingTools.CursorSelect && _draws.Count > 0 && _interaction != null)
            {
                _draws.Remove(_interaction.DrawableFigure);
                canvas.Image = new Bitmap(canvas.Width, canvas.Height);
                foreach(IDrawable drawable in _draws)
                {
                    drawable.Draw();
                }
                _backStep = new Bitmap(canvas.Image);
                _interaction = null;
                if (DragProperty != null) DragProperty(null);
            }
        }
        /// <summary>
        /// Вырезать фигуру(ы)
        /// </summary>
        public void Cut()
        {
            if (SelectTool == DrawingTools.CursorSelect && _draws.Count > 0 && _interaction != null)
            {
                _buferDraw = _interaction.DrawableFigure.Clone();
                _draws.Remove(_interaction.DrawableFigure);
                canvas.Image = new Bitmap(canvas.Width, canvas.Height);
                foreach (IDrawable drawable in _draws)
                {
                    drawable.Draw();
                }
                _backStep = new Bitmap(canvas.Image);
                _interaction = null;
                if (DragProperty != null) DragProperty(null);
            }
        }
        /// <summary>
        /// Перерисовать фигуры из списка
        /// </summary>
        public void RefreshCanvas()
        {
            canvas.Image = new Bitmap(canvas.Width, canvas.Height);
            foreach (IDrawable drawable in _draws)
            {
                drawable.Draw();
            }
            _backStep = new Bitmap(canvas.Image);
        }
    }
}