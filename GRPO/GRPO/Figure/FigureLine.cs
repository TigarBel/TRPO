using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GRPO.Figure
{
    /// <summary>
    /// Класс фигуры - линия
    /// </summary>
    [Serializable]
    class FigureLine
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
        /// Точка расположения
        /// </summary>
        private Point _position;
        /// <summary>
        /// Изменение параметров: расположения, ширины и высоты фигуры
        /// </summary>
        private void Init()
        {
            List<Point> points = new List<Point>();
            points.Add(PointA);
            points.Add(PointB);
            _position.X = points.Min(point => point.X);
            _position.Y = points.Min(point => point.Y);
        }
        /// <summary>
        /// Пустой класс фигуры Линия
        /// </summary>
        public FigureLine()
        {
            Position = new Point(0, 0);
            Width = 0;
            Height = 0;
            PointA = new Point(Position.X, Position.Y);
            PointB = new Point(Position.X + Width, Position.Y + Height);
        }
        /// <summary>
        /// Класс фигуры Линия
        /// </summary>
        /// <param name="a">Первая точка линии</param>
        /// <param name="b">Вторая точка линии</param>
        public FigureLine(Point a, Point b)
        {
            PointA = a;
            PointB = b;
            Init();
        }
        /// <summary>
        /// Первая точка линии
        /// </summary>
        public Point PointA
        {
            get { return _pointA; }
            set
            {
                _pointA = value;
                Init();
            }
        }
        /// <summary>
        /// Вторая точка линии
        /// </summary>
        public Point PointB
        {
            get { return _pointB; }
            set
            {
                _pointB = value;
                Init();
            }
        }
        /// <summary>
        /// Позиция фигуры
        /// </summary>
        public Point Position
        {
            get { return _position; }
            set
            {
                _pointA = new Point(PointA.X - (Position.X - value.X), PointA.Y - (Position.Y - value.Y));
                _pointB = new Point(PointB.X - (Position.X - value.X), PointB.Y - (Position.Y - value.Y));
                Init();
            }
        }
        /// <summary>
        /// Ширина фигуры
        /// </summary>
        public int Width
        {
            get { return Math.Abs(PointA.X - PointB.X); }
            set
            {
                if (value > 10)
                {
                    if (Width != 0)
                    {
                        PointA = new Point(Position.X + Convert.ToInt32((float)(PointA.X - Position.X) / (float)Width * (float)value), PointA.Y);
                        PointB = new Point(Position.X + Convert.ToInt32((float)(PointB.X - Position.X) / (float)Width * (float)value), PointB.Y);
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
        public int Height
        {
            get { return Math.Abs(PointA.Y - PointB.Y); }
            set
            {
                if (value > 10)
                {
                    if (Height != 0)
                    {
                        PointA = new Point(PointA.X, Position.Y + Convert.ToInt32((float)(PointA.Y - Position.Y) / (float)Height * (float)value));
                        PointB = new Point(PointB.X, Position.Y + Convert.ToInt32((float)(PointB.Y - Position.Y) / (float)Height * (float)value));
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
