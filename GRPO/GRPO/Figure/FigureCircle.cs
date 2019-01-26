using System;
using System.Collections.Generic;
using System.Drawing;

namespace GRPO.Figure
{
    /// <summary>
    /// Класс фигуры - круг
    /// </summary>
    [Serializable]
    public class FigureCircle : FigureEllipse
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
            Radius = Pifagor(pointA, pointB);
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
                PointB = new Point(PointA.X + value * 2, PointA.Y + value * 2);
                _radius = value;
            }
        }

        /// <summary>
        /// Ширина фигуры
        /// </summary>
        public new int Width
        {
            get { return Radius * 2; }
            set { StandUpSize(value); }
        }

        /// <summary>
        /// Высота фигуры
        /// </summary>
        public new int Height
        {
            get { return Radius * 2; }
            set { StandUpSize(value); }
        }

        /// <summary>
        /// Установить значения размера круга
        /// </summary>
        /// <param name="value">Подаваемое значение</param>
        private void StandUpSize(int value)
        {
            if (value > 5)
            {
                PointA = new Point(PointA.X + Radius, PointA.Y + Radius);
                PointB = new Point(PointA.X + Radius * 2, PointA.Y + Radius * 2);
                Radius = value / 2;
            }
        }

        /// <summary>
        /// Список точек описывающих окружность
        /// </summary>
        public new List<Point> Points
        {
            get
            {
                List<Point> points = new List<Point>();
                points.Add(PointA);
                points.Add(PointB);
                return points;
            }
            set
            {
                if (value.Count == 2)
                {
                    if (PointA != value[0] && PointB != value[1])
                    {
                        PointA = value[0];
                        PointB = value[1];
                    }
                    else if (PointA != value[0])
                    {
                        StandPoint(value[0]);
                    }
                    else if (PointB != value[1])
                    {
                        StandPoint(value[1]);
                    }

                    Radius = Pifagor(PointA, PointB);
                }
                else
                {
                    throw new ArgumentException("Окружность описывает строго 2 точки!");
                }
            }
        }

        /// <summary>
        /// Установить положение точек при изменение точки
        /// </summary>
        /// <param name="point">Измененная точка</param>
        private void StandPoint(Point point)
        {
            PointA = new Point(PointA.X + Radius, PointA.Y + Radius);
            PointB = new Point(point.X, point.Y);
        }

        /// <summary>
        /// Нахождение гипатенузы
        /// </summary>
        /// <param name="PointA">Начальная точка</param>
        /// <param name="PointB">Конечная точка</param>
        /// <returns>Длина гипатенузы</returns>
        private int Pifagor(Point PointA, Point PointB)
        {
            return Convert.ToInt32(
                Math.Sqrt(Convert.ToDouble(Math.Pow((PointB.X - PointA.X), 2) + Math.Pow((PointB.Y - PointA.Y), 2))));
        }
    }
}
