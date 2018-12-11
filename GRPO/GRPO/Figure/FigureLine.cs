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
        /// Пустой класс фигуры Линия
        /// </summary>
        public FigureLine()
        {
            X = 0;
            Y = 0;
            Width = 0;
            Height = 0;
            A = new Point(0, 0);
            B = new Point(0, 0);
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

            int xOne = a.X;
            int yOne = a.Y;
            int xTwo = b.X;
            int yTwo = b.Y;

            if (xOne <= xTwo)
            {
                X = xOne;
                Width = xTwo - xOne;
            }
            else
            {
                X = xTwo;
                Width = xOne - xTwo;
            }

            if (yOne <= yTwo)
            {
                Y = yOne;
                Height = yTwo - yOne;
            }
            else
            {
                Y = yTwo;
                Height = yOne - yTwo;
            }
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
            }
        }
    }
}
