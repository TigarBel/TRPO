using GRPO.Drawing.Interface;
using GRPO.Drawing.Property;
using GRPO.Figure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GRPO.Drawing
{
    /// <summary>
    /// Класс отрисовки фигуры - прямоугольник
    /// </summary>
    [Serializable]
    class DrawFigureRectangle : IDrawable, ILinePropertyble, IFillPropertyble
    {
        /// <summary>
        /// Объект прямоугольника
        /// </summary>
        private FigureRectangle _figureRectangle;
        /// <summary>
        /// Расширение для отрисовки линии
        /// </summary>
        private LineProperty _lineProperty;
        /// <summary>
        /// Расширение для отрисовки фигуры
        /// </summary>
        private FillProperty _fillProperty;
        /// <summary>
        /// Пустой класс Отрисовки прямоугольника
        /// </summary>
        public DrawFigureRectangle()
        {
            Rectangle = new FigureRectangle();
            LineProperty = new LineProperty();
            FillProperty = new FillProperty();
        }
        /// <summary>
        /// Класс Отрисовки прямоугольника
        /// </summary>
        /// <param name="pointA">Начальная угловая точка</param>
        /// <param name="pointB">Конечная угловая точка</param>
        /// <param name="lineProperty">Свойство линии</param>
        /// <param name="fillProperty">Свойство заливки</param>
        public DrawFigureRectangle(Point pointA, Point pointB, LineProperty lineProperty, FillProperty fillProperty)
        {
            Rectangle = new FigureRectangle(pointA, pointB);
            LineProperty = lineProperty;
            FillProperty = fillProperty;
        }
        /// <summary>
        /// Векторный объект прямоугольника
        /// </summary>
        public FigureRectangle Rectangle
        {
            get
            {
                return _figureRectangle;
            }
            set
            {
                _figureRectangle = value;
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
        /// Позиция фигуры
        /// </summary>
        public Point Position
        {
            get
            {
                return Rectangle.Position;
            }
            set
            {
                Rectangle.Position = value;
            }
        }
        /// <summary>
        /// Ширина фигуры
        /// </summary>
        public int Width
        {
            get
            {
                return Rectangle.WidthPolygon;
            }
            set
            {
                Rectangle.WidthPolygon = value;
            }
        }
        /// <summary>
        /// Высота фигуры
        /// </summary>
        public int Height
        {
            get
            {
                return Rectangle.HeightPolygon;
            }
            set
            {
                Rectangle.HeightPolygon = value;
            }
        }
        /// <summary>
        /// Нарисовать объект
        /// </summary>
        /// <param name="pictureBox">Холст на котором рисуют</param>
        public void Draw(PictureBox pictureBox)
        {
            if (pictureBox.Image != null)
            {
                Graphics graphics = Graphics.FromImage(pictureBox.Image);
                Pen pen = new Pen(LineProperty.LineColor, LineProperty.LineThickness);
                pen.DashStyle = LineProperty.LineType;
                graphics.FillPolygon(new SolidBrush(FillProperty.FillColor), Rectangle.Points.ToArray());

                graphics.DrawLine(pen, Rectangle.PointLeftUp, Rectangle.PointRightUp);
                graphics.DrawLine(pen, Rectangle.PointRightUp, Rectangle.PointRightDown);
                graphics.DrawLine(pen, Rectangle.PointRightDown, Rectangle.PointLeftDown);
                graphics.DrawLine(pen, Rectangle.PointLeftDown, Rectangle.PointLeftUp);

                graphics.Dispose();
                pictureBox.Invalidate();
            }
            else
            {
                throw new Exception("Не выбран холст!");
            }
        }
        /// <summary>
        /// Cписок точек
        /// </summary>
        public List<Point> Points
        {
            get { return Rectangle.Points; }
            set { Rectangle.Points = value; }
        }
        /// <summary>
        /// Клонировать объект
        /// </summary>
        /// <returns>Новая копия объекта</returns>
        public IDrawable Clone()
        {
            return new DrawFigureRectangle(new Point(Rectangle.PointLeftUp.X, Rectangle.PointLeftUp.Y),
                new Point(Rectangle.PointRightDown.X, Rectangle.PointRightDown.Y),
                new LineProperty(LineProperty.LineThickness, LineProperty.LineColor, LineProperty.LineType),
                new FillProperty(FillProperty.FillColor));
        }
    }
}
