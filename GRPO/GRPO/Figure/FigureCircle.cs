using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GRPO.Figure
{
    /// <summary>
    /// Класс фигуры - круг
    /// </summary>
    [Serializable]
    class FigureCircle : FigureEllipse
    {
        /// <summary>
        /// Радиус фигуры Окружность
        /// </summary>
        private int _radius;
        /// <summary>
        ///  Пустой класс фигуры Окружность
        /// </summary>
        public FigureCircle()
        {

        }
        /// <summary>
        /// Класс фигуры Окружность
        /// </summary>
        /// <param name="pointA">Расположение окружности / начальная точка</param>
        /// <param name="pointB">Конечная точка</param>
        public FigureCircle(Point pointA, Point pointB)
        {
            Position = new Point(0, 0);
            PointA = pointA;
            PointB = pointB;
            Radius = Convert.ToInt32(Math.Sqrt(Convert.ToDouble(Math.Pow((PointB.X - PointA.X), 2) + Math.Pow((PointB.Y - PointA.Y), 2))));
        }
        /// <summary>
        /// Радиус фигуры Окружность
        /// </summary>
        public int Radius
        {
            get { return _radius; }
            set
            {
                PointA = new Point(PointA.X - value, PointA.Y - value);
                PointB = new Point(PointA.X + value, PointA.Y + value);
                _radius = value;
            }
        }
        /// <summary>
        /// Ширина фигуры
        /// </summary>
        public new int Width
        {
            get { return Radius * 2; }
            set
            {
                if (value > 5)
                {
                    PointA = new Point(PointA.X + Radius, PointA.Y + Radius);
                    PointB = new Point(PointA.X + Radius * 2, PointA.Y + Radius * 2);
                    Radius = value / 2;
                }
            }
        }
        /// <summary>
        /// Высота фигуры
        /// </summary>
        public new int Height
        {
            get { return Radius * 2; }
            set
            {
                if (value > 5)
                {
                    PointA = new Point(PointA.X + Radius, PointA.Y + Radius);
                    PointB = new Point(PointA.X + Radius * 2, PointA.Y + Radius * 2);
                    Radius = value / 2;
                }
            }
        }
    }
}
