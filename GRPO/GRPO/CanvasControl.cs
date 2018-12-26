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
        private ExtendedForLine _extendedForLine = new ExtendedForLine();
        private ExtendedForFigure _extendedForFigure = new ExtendedForFigure();


        public Interaction interaction;

        public delegate void Drag(IDrawable drawable);
        public event Drag DragExtended;

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
                {///////////////////////////////////////////////////
                    _flagMouseDown = false;
                    _flagPolyline = false;
                    canvas.Image = new Bitmap(_backStep);
                }///////////////////////////////////////////////////
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
        public ExtendedForLine ExtendedForLine
        {
            get
            {
                return _extendedForLine;
            }
            set
            {
                _extendedForLine = value;
            }
        }
        /// <summary>
        /// Свойство заливки
        /// </summary>
        public ExtendedForFigure ExtendedForFigure
        {
            get
            {
                return _extendedForFigure;
            }
            set
            {
                _extendedForFigure = value;
            }
        }

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (SelectTool != DrawingTools.CursorSelect)
            {
                _flagMouseDown = true;
                _pointA = new Point(e.X, e.Y);
                _backStep = new Bitmap(canvas.Image);
            }
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (_flagMouseDown)
            {
                _pointB = new Point(e.X, e.Y);
                canvas.Image = new Bitmap(_backStep);
                DrawFigure(_pointA, _pointB).Draw();
            }
        }

        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (_flagMouseDown)
            {
                _pointB = new Point(e.X, e.Y);
                canvas.Image = new Bitmap(_backStep);
                DrawFigure(_pointA, _pointB).Draw();
                _draws.Add(DrawFigure(_pointA, _pointB));
                _backStep = new Bitmap(canvas.Image);
                _flagMouseDown = false;
            }
            if (SelectTool == DrawingTools.DrawFigurePolyline)
            {
                if(_flagPolyline)
                {
                    _draws.Remove(_draws[_draws.Count - 2]);
                }
                _flagMouseDown = true;
                _flagPolyline = true;
                _pointA = new Point(e.X, e.Y);
            }
        }

        private IDrawable DrawFigure(Point pointA, Point pointB)
        {
            switch (_selectTool)
            {
                case DrawingTools.DrawFigureLine:
                    {
                        DrawFigureLine drawFigure = new DrawFigureLine(pointA, pointB, canvas, _extendedForLine);
                        return drawFigure;
                    }
                case DrawingTools.DrawFigurePolyline:
                    {
                        if (_flagPolyline)
                        {
                            List<Point> points;
                            points = _draws[_draws.Count - 1].GetPoints();
                            points.Add(pointB);
                            DrawFigurePolyline drawFigure = new DrawFigurePolyline(points, false, canvas, _extendedForLine);
                            return drawFigure;
                        }
                        else
                        {
                            DrawFigureLine drawFigure = new DrawFigureLine(pointA, pointB, canvas, _extendedForLine);
                            return drawFigure;
                        }
                    }
                case DrawingTools.DrawFigureRectangle:
                    {
                        List<Point> points = new List<Point>();
                        points.Add(pointA);
                        points.Add(new Point(pointA.X, pointB.Y));
                        points.Add(pointB);
                        points.Add(new Point(pointB.X, pointA.Y));
                        DrawFigurePolygon drawFigure = new DrawFigurePolygon(points, canvas, _extendedForLine, _extendedForFigure);
                        return drawFigure;
                    }
                case DrawingTools.DrawFigureCircle:
                    {
                        DrawFigureCircle drawFigure = new DrawFigureCircle(pointA,
                        Convert.ToInt32(Math.Sqrt(Convert.ToDouble(Math.Pow((_pointB.X - _pointA.X), 2) + Math.Pow((_pointB.Y - _pointA.Y), 2)))),/******/
                        canvas, _extendedForLine, _extendedForFigure);
                        return drawFigure;
                    }
                case DrawingTools.DrawFigureEllipse:
                    {
                        DrawFigureEllipse drawFigure = new DrawFigureEllipse(pointA, pointB.X - pointA.X, pointB.Y - pointA.Y, canvas, _extendedForLine,
                            _extendedForFigure);
                        return drawFigure;
                    }
            }
            return null;
        }

        private void canvas_MouseClick(object sender, MouseEventArgs e)
        {
            if (SelectTool == DrawingTools.CursorSelect && _draws.Count > 0)
            {
                canvas.Image = new Bitmap(_backStep);
                _pointA = new Point(e.X, e.Y);
                DragExtended(null);
                for (int i = _draws.Count - 1; i >= 0; i--)
                {
                    int minX = _draws[i].GetPoints().Min(point => point.X);
                    int maxX = _draws[i].GetPoints().Max(point => point.X);
                    int minY = _draws[i].GetPoints().Min(point => point.Y);
                    int maxY = _draws[i].GetPoints().Max(point => point.Y);
                    if (_pointA.X >= minX && _pointA.X <= maxX && _pointA.Y >= minY && _pointA.Y <= maxY)
                    {
                        interaction = new Interaction(_draws[i], canvas, false);
                        DragExtended(interaction.DrawableFigure);
                        break;
                    }
                }
            }
        }

        public void Copy()
        {
            if (SelectTool == DrawingTools.CursorSelect && _draws.Count > 0)
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
            if (SelectTool == DrawingTools.CursorSelect && _draws.Count > 0)
            {
                _draws.Remove(interaction.DrawableFigure);
                canvas.Image = new Bitmap(canvas.Width, canvas.Height);
                foreach(IDrawable drawable in _draws)
                {
                    drawable.Draw();
                }
                _backStep = new Bitmap(canvas.Image);
            }
        }

        public void Cut()
        {
            if (SelectTool == DrawingTools.CursorSelect && _draws.Count > 0)
            {
                _buferDraw = interaction.DrawableFigure.Clone();
                _draws.Remove(interaction.DrawableFigure);
                canvas.Image = new Bitmap(canvas.Width, canvas.Height);
                foreach (IDrawable drawable in _draws)
                {
                    drawable.Draw();
                }
                _backStep = new Bitmap(canvas.Image);
            }
        }

        public IDrawable ExtrndedDrawable
        {
            set
            {
                if (interaction.DrawableFigure != null)
                {
                    switch (value.GetType().Name)
                    {
                        case "DrawFigureLine":
                            {
                                ((DrawFigureLine)interaction.DrawableFigure).Extended = ((DrawFigureLine)value).Extended;
                                break;
                            }
                        case "DrawFigurePolyline":
                            {
                                ((DrawFigurePolyline)interaction.DrawableFigure).Extended = ((DrawFigurePolyline)value).Extended;
                                break;
                            }
                        case "DrawFigurePolygon":
                            {
                                ((DrawFigurePolygon)interaction.DrawableFigure).ExtendedLine = ((DrawFigurePolygon)value).ExtendedLine;
                                ((DrawFigurePolygon)interaction.DrawableFigure).ExtendedFigure = ((DrawFigurePolygon)value).ExtendedFigure;
                                break;
                            }
                        case "DrawFigureCircle":
                            {
                                ((DrawFigureCircle)interaction.DrawableFigure).ExtendedLine = ((DrawFigureCircle)value).ExtendedLine;
                                ((DrawFigureCircle)interaction.DrawableFigure).ExtendedFigure = ((DrawFigureCircle)value).ExtendedFigure;
                                break;
                            }
                        case "DrawFigureEllipse":
                            {
                                ((DrawFigureEllipse)interaction.DrawableFigure).ExtendedLine = ((DrawFigureEllipse)value).ExtendedLine;
                                ((DrawFigureEllipse)interaction.DrawableFigure).ExtendedFigure = ((DrawFigureEllipse)value).ExtendedFigure;
                                break;
                            }
                    }
                    foreach(IDrawable drawable in _draws)
                    {
                        drawable.Draw();
                    }
                }
            }

        }
    }
}