using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using GRPO.Figure;
using GRPO.Drawing.Property;
using GRPO.Drawing.Interface;

namespace GRPO.Drawing
{
    /// <summary>
    /// Класс отрисовки фигуры - эллипс
    /// </summary>
    [Serializable]
    public class DrawFigureEllipse : IDrawable, ILineProperty, IFillProperty
    {
        /// <summary>
        /// Объект эллипса
        /// </summary>
        private FigureEllipse _figureEllipse;

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
            LineProperty = new LineProperty();
            FillProperty = new FillProperty();
        }

        /// <summary>
        /// Класс Отрисовки эллипса
        /// </summary>
        /// <param name="pointA">Начальная точка</param>
        /// <param name="pointB">Конечная точка</param>
        /// <param name="lineProperty">Дополнительные свойства отрисовки линии</param>
        /// <param name="fillProperty">Дополнительные свойства отрисовки фигуры</param>
        public DrawFigureEllipse(Point pointA, Point pointB, LineProperty lineProperty, FillProperty fillProperty)
        {
            Ellipse = new FigureEllipse(pointA, pointB);
            LineProperty = new LineProperty(lineProperty.LineThickness, lineProperty.LineColor, lineProperty.LineType);
            FillProperty = new FillProperty(fillProperty.FillColor);
        }

        /// <summary>
        /// Векторный объект эллипса
        /// </summary>
        public FigureEllipse Ellipse
        {
            get { return _figureEllipse; }
            set { _figureEllipse = value; }
        }

        /// <summary>
        /// Расширение для отрисовки линии
        /// </summary>
        public LineProperty LineProperty
        {
            get { return _lineProperty; }
            set { _lineProperty = value; }
        }

        /// <summary>
        /// Расширение для отрисовки фигуры
        /// </summary>
        public FillProperty FillProperty
        {
            get { return _fillProperty; }
            set { _fillProperty = value; }
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
                    Ellipse.Position.X, Ellipse.Position.Y, Ellipse.Width, Ellipse.Height);
                graphics.DrawEllipse(pen, Ellipse.Position.X, Ellipse.Position.Y, Ellipse.Width,
                    Ellipse.Height);
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
            get { return Ellipse.Points; }
            set { Ellipse.Points = value; }
        }

        /// <summary>
        /// Позиция фигуры
        /// </summary>
        public Point Position
        {
            get { return Ellipse.Position; }
            set { Ellipse.Position = value; }
        }

        /// <summary>
        /// Ширина фигуры
        /// </summary>
        public int Width
        {
            get { return Ellipse.Width; }
            set { Ellipse.Width = value; }
        }

        /// <summary>
        /// Высота фигуры
        /// </summary>
        public int Height
        {
            get { return Ellipse.Height; }
            set { Ellipse.Height = value; }
        }

        /// <summary>
        /// Клонировать объект
        /// </summary>
        /// <returns>Новая копия объекта</returns>
        public IDrawable Clone()
        {
            return new DrawFigureEllipse(new Point(Ellipse.Position.X, Ellipse.Position.Y),
                new Point(Ellipse.Position.X + Ellipse.Width, Ellipse.Position.Y + Ellipse.Height),
                new LineProperty(LineProperty.LineThickness, LineProperty.LineColor, LineProperty.LineType),
                new FillProperty(FillProperty.FillColor));
        }
    }
}