using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GRPO.Figure
{
    /// <summary>
    /// Класс фигуры - многоугольник
    /// </summary>
    [Serializable]
    class FigurePolygon : Figure
    {
        /// <summary>
        /// Количество углов многоугольника
        /// </summary>
        private int _countAngle;
        /// <summary>
        /// Пустой класс фигуры Многогранник
        /// </summary>
        public FigurePolygon()
        {
            X = 0;
            Y = 0;
            Width = 0;
            Height = 0;
            Points = new List<Point>();
        }
        /// <summary>
        /// Класс фигуры Многогранник
        /// </summary>
        /// <param name="position">Точка расположения многоугольника</param>
        /// <param name="width">Ширина многоугольника</param>
        /// <param name="height">Высота многоугольника</param>
        /// <param name="countAngle">Количество углов многоугольника</param>
        /// <param name="phase">Угол поворота многоугольника, в градусах</param>
        public FigurePolygon(Point position, int width, int height, int countAngle, int phase)
        {
            X = position.X;
            Y = position.Y;
            Width = width;
            Height = height;
            Points = new List<Point>();

            for (int i = 0; i < countAngle; i++)
            {
                double cos = Math.Cos((i * (360 / countAngle) + phase) * (Math.PI / 180.0));
                double sin = Math.Sin((i * (360 / countAngle) + phase) * (Math.PI / 180.0));
                double x = X + Width / 2 + Width * cos / 2 * Math.Sqrt(2);
                double y = Y + Height / 2 + Height * sin / 2 * Math.Sqrt(2);
                Point point = new Point(Convert.ToInt32(x), Convert.ToInt32(y));
                Points.Add(point);
            }
        }
        /// <summary>
        /// Класс фигуры Многогранник
        /// </summary>
        /// <param name="points">Список существующих точек для многоугольника</param>
        public FigurePolygon(List<Point> points)
        {
            Points = points;
            X = Points.Min(point => point.X);
            Y = Points.Min(point => point.Y);
            Width = Points.Max(point => point.X) - Points.Min(point => point.X);
            Height = Points.Max(point => point.Y) - Points.Min(point => point.Y);
        }
        /// <summary>
        /// Список точек многоугольника
        /// </summary>
        public List<Point> Points { get; set; }
        /// <summary>
        /// Позиция фигуры
        /// </summary>
        public Point Position
        {
            get { return new Point(X, Y); }
            set
            {
                for (int i = 0; i < Points.Count; i++)
                {
                    Points[i] = new Point(Points[i].X - (X - value.X), Points[i].Y - (Y - value.Y));
                }
                X = value.X;
                Y = value.Y;
            }
        }
        /// <summary>
        /// Ширина фигуры
        /// </summary>
        public int WidthPolygon
        {
            get { return Width; }
            set
            {
                if (value > 10)
                {
                    for (int i = 0; i < Points.Count; i++)
                    {
                        Points[i] = new Point(X +
                            Convert.ToInt32((float)(Points[i].X - X) / (float)Width * (float)value),
                            Points[i].Y);
                    }
                    Width = value;
                }
            }
        }
        /// <summary>
        /// Высота фигуры
        /// </summary>
        public int HeightPolygon
        {
            get { return Height; }
            set
            {
                if (value > 10)
                {
                    for (int i = 0; i < Points.Count; i++)
                    {
                        Points[i] = new Point(Points[i].X,
                            Y + Convert.ToInt32((float)(Points[i].Y - Y) / (float)Height * (float)value));
                    }
                    Height = value;
                }
            }
        }
    }
}
