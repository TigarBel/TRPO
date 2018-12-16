using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GRPO
{
    class FigureLine : Figure
    {
        /// <summary>
        /// Первая точка линии
        /// </summary>
        private Point _pointA;
        /// <summary>
        /// Вторая точка линии
        /// </summary>
        private Point _pointB;
        /// <summary>
        /// Изменение параметров: расположения, ширины и высоты фигуры
        /// </summary>
        private void Init()
        {
            List<Point> points = new List<Point>();
            points.Add(A);
            points.Add(B);
            X = points.Min(point => point.X);
            Y = points.Min(point => point.Y);
            Width = points.Max(point => point.X) - points.Min(point => point.X);
            Height = points.Max(point => point.Y) - points.Min(point => point.Y);
        }
        /// <summary>
        /// Пустой класс фигуры Линия
        /// </summary>
        public FigureLine()
        {
            X = 0;
            Y = 0;
            Width = 0;
            Height = 0;
            A = new Point(X, Y);
            B = new Point(X + Width, Y + Height);
        }
        /// <summary>
        /// Класс фигуры Линия
        /// </summary>
        /// <param name="a">Первая точка линии</param>
        /// <param name="b">Вторая точка линии</param>
        public FigureLine(Point a, Point b)
        {
            A = a;
            B = b;
            Init();
        }
        /// <summary>
        /// Первая точка линии
        /// </summary>
        public Point A
        {
            get
            {
                return _pointA;
            }
            set
            {
                _pointA = value;
                Init();
            }
        }
        /// <summary>
        /// Вторая точка линии
        /// </summary>
        public Point B
        {
            get
            {
                return _pointB;
            }
            set
            {
                _pointB = value;
                Init();
            }
        }
    }
}
