using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace GRPO
{
    class DrawFigureEllipse : IDrawable, ILinePropertyble, IFillPropertyble
    {
        /// <summary>
        /// Объект эллипса
        /// </summary>
        private FigureEllipse _figureEllipse;
        /// <summary>
        /// Холст на котором рисуют
        /// </summary>
        private PictureBox _pictureBox;
        /// <summary>
        /// Расширение для отрисовки линии
        /// </summary>
        private LineProperty _lineProperty;
        /// <summary>
        /// Расширение для отрисовки фигуры
        /// </summary>
        private FillProperty _fillProperty;
        /// <summary>
        /// Пустой класс Отрисовки эллипса
        /// </summary>
        public DrawFigureEllipse()
        {
            Ellipse = new FigureEllipse();
            Canvas = new PictureBox();
            LineProperty = new LineProperty();
            FillProperty = new FillProperty();
        }
        /// <summary>
        /// Класс Отрисовки эллипса
        /// </summary>
        /// <param name="position">Расположения эллипса</param>
        /// <param name="width">Ширина эллипса</param>
        /// <param name="height">Высота эллипса</param>
        /// <param name="canvas">Полотно на котором отрисовывается эллипс</param>
        /// <param name="extendedForLine">Дополнительные свойства отрисовки линии</param>
        /// <param name="extendedForFigure">Дополнительные свойства отрисовки фигуры</param>
        public DrawFigureEllipse(Point position, int width, int height, PictureBox canvas,
            LineProperty extendedForLine, FillProperty extendedForFigure)
        {
            Ellipse = new FigureEllipse(position, width, height);
            Canvas = canvas;
            LineProperty = extendedForLine;
            FillProperty = extendedForFigure;
        }
        /// <summary>
        /// Векторный объект эллипса
        /// </summary>
        public FigureEllipse Ellipse
        {
            get
            {
                return _figureEllipse;
            }
            set
            {
                _figureEllipse = value;
            }
        }
        /// <summary>
        /// Холст на котором рисуют
        /// </summary>
        public PictureBox Canvas
        {
            set
            {
                _pictureBox = value;
            }
        }
        /// <summary>
        /// Расширение для отрисовки линии
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
        /// Расширение для отрисовки фигуры
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
        /// Отрисовка последнюю часть многоугольника
        /// </summary>
        public void Draw()
        {
            if (_pictureBox.Image != null)
            {
                Graphics graphics = Graphics.FromImage(_pictureBox.Image);
                Pen pen = new Pen(LineProperty.LineColor, LineProperty.LineThickness);
                pen.DashStyle = LineProperty.LineType;
                graphics.FillEllipse(new SolidBrush(FillProperty.FillColor), 
                    Ellipse.Position.X, Ellipse.Position.Y, Ellipse.WidthEllipse, Ellipse.HeightEllipse);
                graphics.DrawEllipse(pen, Ellipse.Position.X, Ellipse.Position.Y, Ellipse.WidthEllipse, Ellipse.HeightEllipse);
                graphics.Dispose();
                _pictureBox.Invalidate();
            }
            else
            {
                throw new Exception("Не выбран холст!");
            }
        }
        /// <summary>
        /// Взять список точек
        /// </summary>
        /// <returns>Списко точек формирующих фигуру</returns>
        public List<Point> GetPoints()
        {
            List<Point> points = new List<Point>();
            points.Add(new Point(Ellipse.Position.X, Ellipse.Position.Y));
            points.Add(new Point(Ellipse.Position.X + Ellipse.WidthEllipse, Ellipse.Position.Y));
            points.Add(new Point(Ellipse.Position.X + Ellipse.WidthEllipse, Ellipse.Position.Y + Ellipse.HeightEllipse));
            points.Add(new Point(Ellipse.Position.X, Ellipse.Position.Y + Ellipse.HeightEllipse));
            return points;
        }
        /// <summary>
        /// Позиция фигуры
        /// </summary>
        public Point Position
        {
            get
            {
                return Ellipse.Position;
            }
            set
            {
                Ellipse.Position = value;
            }
        }
        /// <summary>
        /// Ширина фигуры
        /// </summary>
        public int Width
        {
            get
            {
                return Ellipse.WidthEllipse;
            }
            set
            {
                Ellipse.WidthEllipse = value;
            }
        }
        /// <summary>
        /// Высота фигуры
        /// </summary>
        public int Height
        {
            get
            {
                return Ellipse.HeightEllipse;
            }
            set
            {
                Ellipse.HeightEllipse = value;
            }
        }
        /// <summary>
        /// Клонировать объект
        /// </summary>
        /// <returns>Новая копия объекта</returns>
        public IDrawable Clone()
        {
            return new DrawFigureEllipse(Ellipse.Position, Ellipse.WidthEllipse, Ellipse.HeightEllipse, _pictureBox, LineProperty, FillProperty);
        }
    }
}
