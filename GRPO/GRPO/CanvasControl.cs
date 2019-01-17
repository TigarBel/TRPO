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
        private bool _flagPolyFigure;

        private IDrawable _buferDraw;

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
                return new Bitmap(canvas.Image);
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

            if(SelectTool != DrawingTools.CursorSelect && SelectTool != DrawingTools.MassSelect && SelectTool != DrawingTools.DrawFigurePolyline)
            {
                Drawables.Add(DrawFigure(_pointA, _pointA, SelectTool));
            }

            if (SelectTool == DrawingTools.DrawFigurePolyline)
            {
                if (_flagPolyFigure)
                {
                    List<Point> points = Drawables[Drawables.Count - 1].GetPoints();
                    points.RemoveAt(points.Count - 1);
                    points.Add(_pointA);

                    Drawables.RemoveAt(Drawables.Count - 1);
                    Drawables.Add(DrawPolyFigure(points, SelectTool));
                    RefreshCanvas();
                }
                if (!_flagPolyFigure)
                {
                    List<Point> points = new List<Point>() { new Point(_pointA.X, _pointA.Y), new Point(_pointA.X, _pointA.Y) };
                    Drawables.Add(DrawPolyFigure(points, SelectTool));

                    _flagPolyFigure = true;
                }
                if (e.Button == MouseButtons.Right && _flagPolyFigure)
                {
                    List<Point> points = Drawables[Drawables.Count - 1].GetPoints();
                    points.RemoveAt(points.Count - 1);
                    points.Add(_pointA);

                    Drawables.RemoveAt(Drawables.Count - 1);
                    Drawables.Add(DrawPolyFigure(points, SelectTool));
                    RefreshCanvas();

                    _flagPolyFigure = false;
                }
            }
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            _pointB = new Point(e.X, e.Y);

            if (_flagMouseDown)
            {
                if (SelectTool != DrawingTools.CursorSelect && SelectTool != DrawingTools.MassSelect && SelectTool != DrawingTools.DrawFigurePolyline)
                {
                    Drawables.RemoveAt(Drawables.Count - 1);
                    Drawables.Add(DrawFigure(_pointA, _pointB, SelectTool));
                    RefreshCanvas();
                }
            }

            if(_flagPolyFigure)
            {
                if (SelectTool == DrawingTools.DrawFigurePolyline)
                {
                    List<Point> points = Drawables[Drawables.Count - 1].GetPoints();
                    points.RemoveAt(points.Count - 1);
                    points.Add(_pointB);

                    Drawables.RemoveAt(Drawables.Count - 1);
                    Drawables.Add(DrawPolyFigure(points, SelectTool));
                    RefreshCanvas();
                }
            }
        }

        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            _pointB = new Point(e.X, e.Y);

            if (_flagMouseDown)
            {
                if (SelectTool != DrawingTools.CursorSelect && SelectTool != DrawingTools.MassSelect && SelectTool != DrawingTools.DrawFigurePolyline)
                {
                    Drawables.RemoveAt(Drawables.Count - 1);
                    Drawables.Add(DrawFigure(_pointA, _pointB, SelectTool));
                    RefreshCanvas();
                }

                if (SelectTool == DrawingTools.DrawFigurePolyline)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        List<Point> points = Drawables[Drawables.Count - 1].GetPoints();
                        points.Add(_pointB);
                        Drawables.RemoveAt(Drawables.Count - 1);
                        Drawables.Add(DrawPolyFigure(points, SelectTool));
                        RefreshCanvas();
                    }
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
                        DrawFigureLine drawFigure = new DrawFigureLine(pointA, pointB, canvas, LineProperty);
                        return drawFigure;
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
                        DrawFigurePolyline drawFigure = new DrawFigurePolyline(points, false, canvas, LineProperty);
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