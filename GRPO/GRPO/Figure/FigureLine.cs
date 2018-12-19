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
        /// <summary>
        /// Позиция фигуры
        /// </summary>
        public Point Position
        {
            get
            {
                return new Point(X, Y);
            }
            set
            {
                A = new Point(A.X - (X - value.X), A.Y - (Y - value.Y));
                B = new Point(B.X - (X - value.X), B.Y - (Y - value.Y));
                X = value.X;
                Y = value.Y;
            }
        }
        /// <summary>
        /// Ширина фигуры
        /// </summary>
        public int WidthLine
        {
            get
            {
                return Width;
            }
            set
            {
                if (value > 10)
                {
                    if (Width != 0)
                    {
                        A = new Point(X + Convert.ToInt32((float)(A.X - X) / (float)Width * (float)value), A.Y);
                        B = new Point(X + Convert.ToInt32((float)(B.X - X) / (float)Width * (float)value), B.Y);
                        Width = value;
                    }
                    else
                    {
                        throw new ArgumentException("Линия вертикальная!");
                    }
                }
            }
        }
        /// <summary>
        /// Высота фигуры
        /// </summary>
        public int HeightLine
        {
            get
            {
                return Height;
            }
            set
            {
                if (value > 10)
                {
                    if (Height != 0)
                    {
                        A = new Point(A.X, Y + Convert.ToInt32((float)(A.Y - Y) / (float)Height * (float)value));
                        B = new Point(B.X, Y + Convert.ToInt32((float)(B.Y - Y) / (float)Height * (float)value));
                        Height = value;
                    }
                    else
                    {
                        throw new ArgumentException("Линия горизонтальная!");
                    }
                }
            }
        }
    }
}
