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


        public Interaction interaction;

        public delegate void Drag(IDrawable drawable);
        public event Drag DragProperty;

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

            if (SelectTool != DrawingTools.CursorSelect && _flagPolyline != true)
            {
                _pointA = new Point(e.X, e.Y);
                _backStep = new Bitmap(canvas.Image);
            }
            if (SelectTool == DrawingTools.CursorSelect && interaction != null)
            {
                _pointA = new Point(e.X, e.Y);
                interaction.SelectPoint = _pointA;
                if (e.Button == MouseButtons.Left)
                {
                    interaction.EnablePoints = false;
                }
                if (e.Button == MouseButtons.Right)
                {
                    interaction.EnablePoints = true;
                }
            }
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (SelectTool != DrawingTools.CursorSelect && _flagMouseDown)
            {
                _pointB = new Point(e.X, e.Y);
                canvas.Image = new Bitmap(_backStep);
                DrawFigure(_pointA, _pointB).Draw();
            }
            if (SelectTool == DrawingTools.CursorSelect && _flagMouseDown && interaction != null)
            {
                if (interaction.EnablePoints)
                {
                    _pointB = new Point(e.X, e.Y);
                    interaction.ChangePoint(_pointB);
                    RefreshCanvas();
                    _pointA = new Point(e.X, e.Y);
                }
                else
                {
                    _pointB = new Point(e.X, e.Y);
                    int x = interaction.DrawableFigure.Position.X;
                    int y = interaction.DrawableFigure.Position.Y;
                    interaction.DrawableFigure.Position = new Point(x + (_pointB.X - _pointA.X), y + (_pointB.Y - _pointA.Y));
                    RefreshCanvas();
                    _pointA = new Point(e.X, e.Y);
                }
            }
        }

        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (SelectTool != DrawingTools.CursorSelect && _flagMouseDown)
            {
                _pointB = new Point(e.X, e.Y);
                canvas.Image = new Bitmap(_backStep);
                DrawFigure(_pointA, _pointB).Draw();
                _draws.Add(DrawFigure(_pointA, _pointB));
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
        }

        private void canvas_MouseClick(object sender, MouseEventArgs e)
        {
            if (SelectTool == DrawingTools.CursorSelect && _draws.Count > 0)
            {
                interaction = null;
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
                            interaction = new Interaction(_draws[i], canvas, false);
                        }
                        if (e.Button == MouseButtons.Right)
                        {
                            interaction = new Interaction(_draws[i], canvas, true);
                        }
                        break;
                    }
                }
            }
        }

        private IDrawable DrawFigure(Point pointA, Point pointB)
        {
            switch (_selectTool)
            {
                case DrawingTools.DrawFigureLine:
                    {
                        DrawFigureLine drawFigure = new DrawFigureLine(pointA, pointB, canvas, _lineProperty);
                        return drawFigure;
                    }
                case DrawingTools.DrawFigurePolyline:
                    {
                        if (_flagPolyline)
                        {
                            List<Point> points;
                            points = _draws[_draws.Count - 1].GetPoints();
                            points.Add(pointB);
                            DrawFigurePolyline drawFigure = new DrawFigurePolyline(points, false, canvas, _lineProperty);
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
                        DrawFigureRectangle drawFigure = new DrawFigureRectangle(pointA, pointB, canvas, _lineProperty, _fillProperty);
                        return drawFigure;
                    }
                case DrawingTools.DrawFigureCircle:
                    {
                        DrawFigureCircle drawFigure = new DrawFigureCircle(pointA,
                        Convert.ToInt32(Math.Sqrt(Convert.ToDouble(Math.Pow((_pointB.X - _pointA.X), 2) + Math.Pow((_pointB.Y - _pointA.Y), 2)))),/******/
                        canvas, _lineProperty, _fillProperty);
                        return drawFigure;
                    }
                case DrawingTools.DrawFigureEllipse:
                    {
                        DrawFigureEllipse drawFigure = new DrawFigureEllipse(pointA, pointB.X - pointA.X, pointB.Y - pointA.Y, canvas, _lineProperty,
                            _fillProperty);
                        return drawFigure;
                    }
            }
            return null;
        }
        
        public void Copy()
        {
            if (SelectTool == DrawingTools.CursorSelect && _draws.Count > 0 && interaction != null)
            {
                _buferDraw = interaction.DrawableFigure.Clone();
            }
        }

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

        public void Delete()
        {
            if (SelectTool == DrawingTools.CursorSelect && _draws.Count > 0 && interaction != null)
            {
                _draws.Remove(interaction.DrawableFigure);
                canvas.Image = new Bitmap(canvas.Width, canvas.Height);
                foreach(IDrawable drawable in _draws)
                {
                    drawable.Draw();
                }
                _backStep = new Bitmap(canvas.Image);
                interaction = null;
                if (DragProperty != null) DragProperty(null);
            }
        }

        public void Cut()
        {
            if (SelectTool == DrawingTools.CursorSelect && _draws.Count > 0 && interaction != null)
            {
                _buferDraw = interaction.DrawableFigure.Clone();
                _draws.Remove(interaction.DrawableFigure);
                canvas.Image = new Bitmap(canvas.Width, canvas.Height);
                foreach (IDrawable drawable in _draws)
                {
                    drawable.Draw();
                }
                _backStep = new Bitmap(canvas.Image);
                interaction = null;
                if (DragProperty != null) DragProperty(null);
            }
        }
        
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