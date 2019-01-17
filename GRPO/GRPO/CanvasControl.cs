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
        private IDrawable _buferDraw;
        private List<Bitmap> _bitmaps = new List<Bitmap>();

        private List<IDrawable> _draws = new List<IDrawable>();
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
        /// Список фигур
        /// </summary>
        public List<IDrawable> Drawables
        {
            get
            {
                return _draws;
            }
            set
            {
                _draws = value;
            }
        }
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

                if (_selectTool != DrawingTools.CursorSelect)
                {
                    _interaction = null;
                }
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
                return canvas.Image;
            }
            set
            {
                canvas.Image = new Bitmap(value);
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
                    draw.Draw();
                }
            }
        }
        /// <summary>
        /// Перерисовать фигуры из списка
        /// </summary>
        public void RefreshCanvas()
        {
            canvas.Image = new Bitmap(canvas.Width, canvas.Height);
            foreach (IDrawable drawable in Drawables)
            {
                drawable.Draw();
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

            Drawables.Add(DrawFigure(_pointA, _pointA, SelectTool));
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            _pointB = new Point(e.X, e.Y);

            if (_flagMouseDown)
            {
                Drawables.Remove(Drawables[Drawables.Count - 1]);
                Drawables.Add(DrawFigure(_pointA, _pointB, SelectTool));
                RefreshCanvas();
            }
        }

        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            _pointB = new Point(e.X, e.Y);
            if (_flagMouseDown)
            {
                Drawables.Remove(Drawables[Drawables.Count - 1]);
                Drawables.Add(DrawFigure(_pointA, _pointB, SelectTool));
                RefreshCanvas();
                _flagMouseDown = false;
            }
        }

        private void canvas_MouseClick(object sender, MouseEventArgs e)
        {
            /*if (SelectTool == DrawingTools.CursorSelect && _draws.Count > 0)
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
            }*/
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
                            points = Drawables[Drawables.Count - 1].GetPoints();
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
            if (SelectTool == DrawingTools.CursorSelect && Drawables.Count > 0 && _interaction != null)
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
                _buferDraw.Position = new Point(10, 10);
                Drawables.Add(_buferDraw);
                Drawables[Drawables.Count - 1].Draw();
                RefreshCanvas();
            }
        }
        /// <summary>
        /// Удалить фигуру(ы)
        /// </summary>
        public void Delete()
        {
            if (SelectTool == DrawingTools.CursorSelect && Drawables.Count > 0 && _interaction != null)
            {
                Drawables.Remove(_interaction.DrawableFigure);
                canvas.Image = new Bitmap(canvas.Width, canvas.Height);
                foreach(IDrawable drawable in Drawables)
                {
                    drawable.Draw();
                }
                _interaction = null;
                if (DragProperty != null) DragProperty(null);
                RefreshCanvas();
            }
        }
        /// <summary>
        /// Вырезать фигуру(ы)
        /// </summary>
        public void Cut()
        {
            if (SelectTool == DrawingTools.CursorSelect && Drawables.Count > 0 && _interaction != null)
            {
                _buferDraw = _interaction.DrawableFigure.Clone();
                Drawables.Remove(_interaction.DrawableFigure);
                canvas.Image = new Bitmap(canvas.Width, canvas.Height);
                foreach (IDrawable drawable in Drawables)
                {
                    drawable.Draw();
                }
                _interaction = null;
                if (DragProperty != null) DragProperty(null);
                RefreshCanvas();
            }
        }
    }
}