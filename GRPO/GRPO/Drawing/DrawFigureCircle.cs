using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using GRPO.Figure;
using GRPO.Drawing.Property;
using GRPO.Drawing.Interface;

namespace GRPO.Drawing
{
    /// <summary>
    /// Класс отрисовки фигуры - круг
    /// </summary>
    [Serializable]
    class DrawFigureCircle : IDrawable, ILinePropertyble, IFillPropertyble
    {
        /// <summary>
        /// Объект круга
        /// </summary>
        private FigureCircle _figureCircle;
        /// <summary>
        /// Расширение для отрисовки линии
        /// </summary>
        private LineProperty _lineProperty;
        /// <summary>
        /// Расширение для отрисовки фигуры
        /// </summary>
        private FillProperty _fillProperty;
        /// <summary>
        /// Пустой класс Отрисовки круга
        /// </summary>
        public DrawFigureCircle()
        {
            Circle = new FigureCircle();
            LineProperty = new LineProperty();
            FillProperty = new FillProperty();
        }
        /// <summary>
        /// Класс Отрисовки круга
        /// </summary>
        /// <param name="pointA">Расположение окружности / начальная точка</param>
        /// <param name="pointB">Конечная точка</param>
        /// <param name="extendedForLine">Дополнительные свойства отрисовки линии</param>
        /// <param name="extendedForFigure">Дополнительные свойства отрисовки фигуры</param>
        public DrawFigureCircle(Point pointA, Point pointB, LineProperty extendedForLine, FillProperty extendedForFigure)
        {
            Circle = new FigureCircle(pointA, pointB);
            LineProperty = extendedForLine;
            FillProperty = extendedForFigure;
        }
        /// <summary>
        /// Векторный объект круга
        /// </summary>
        public FigureCircle Circle
        {
            get
            {
                return _figureCircle;
            }
            set
            {
                _figureCircle = value;
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
        /// <param name="pictureBox">Холст на котором рисуют</param>
        public void Draw(PictureBox pictureBox)
        {
            if (pictureBox.Image != null)
            {
                Graphics graphics = Graphics.FromImage(pictureBox.Image);
                Pen pen = new Pen(LineProperty.LineColor, LineProperty.LineThickness);
                pen.DashStyle = LineProperty.LineType;
                graphics.FillEllipse(new SolidBrush(FillProperty.FillColor), 
                    Circle.Position.X, Circle.Position.Y, Circle.Width, Circle.Height);
                graphics.DrawEllipse(pen, Circle.Position.X, Circle.Position.Y, Circle.Width, Circle.Height);
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
            get { return Circle.Points; }
            set { Circle.Points = value; }
        }
        /// <summary>
        /// Позиция фигуры
        /// </summary>
        public Point Position
        {
            get
            {
                return Circle.Position;
            }
            set
            {
                Circle.Position = value;
            }
        }
        /// <summary>
        /// Ширина фигуры
        /// </summary>
        public int Width
        {
            get
            {
                return Circle.Width;
            }
            set
            {
                Circle.Width = value;
            }
        }
        /// <summary>
        /// Высота фигуры
        /// </summary>
        public int Height
        {
            get
            {
                return Circle.Height;
            }
            set
            {
                Circle.Height = value;
            }
        }
        /// <summary>
        /// Клонировать объект
        /// </summary>
        /// <returns>Новая копия объекта</returns>
        public IDrawable Clone()
        {
            return new DrawFigureCircle(new Point(Circle.Position.X, Circle.Position.Y),
                new Point(Circle.Position.X + Circle.Radius, Circle.Position.Y + Circle.Radius),
                new LineProperty(LineProperty.LineThickness, LineProperty.LineColor, LineProperty.LineType),
                new FillProperty(FillProperty.FillColor));
        }
    }
}
