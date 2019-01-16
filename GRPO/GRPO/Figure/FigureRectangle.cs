using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GRPO
{
    class FigureRectangle : FigurePolygon
    {
        /// <summary>
        /// Левая верхняя точка
        /// </summary>
        private Point _pointLeftUp;
        /// <summary>
        /// Правая верхняя точка
        /// </summary>
        private Point _pointRightUp;
        /// <summary>
        /// Правая нижняя
        /// </summary>
        private Point _pointRightDown;
        /// <summary>
        /// Левая нижняя
        /// </summary>
        private Point _pointLeftDown;
        /// <summary>
        /// Ограничение
        /// </summary>
        private int _restriction = 5;
        /// <summary>
        /// Изменить атрибуты фигуры
        /// </summary>
        private void SortList(List<Point> points)
        {
            if (points.Count == 4)
            {
                int x = points.Min(point => point.X);
                int y = points.Min(point => point.Y);
                int width = points.Max(point => point.X) - points.Min(point => point.X);
                int height = points.Max(point => point.Y) - points.Min(point => point.Y);

                _pointLeftUp = new Point(points.Min(point => point.X), points.Min(point => point.Y));
                _pointRightUp = new Point(points.Min(point => point.X) + width, points.Min(point => point.Y));
                _pointRightDown = new Point(points.Min(point => point.X) + width, points.Min(point => point.Y) + height);
                _pointLeftDown = new Point(points.Min(point => point.X), points.Min(point => point.Y) + height);
                Points.Clear();
                Points.Add(_pointLeftUp);
                Points.Add(_pointRightUp);
                Points.Add(_pointRightDown);
                Points.Add(_pointLeftDown);
                
                X = x;
                Y = y;
                Width = width;
                Height = height;
            }
        }
        /// <summary>
        /// Обновить список точек
        /// </summary>
        private void RefreshList()
        {
            List<Point> points = new List<Point>();
            points.Add(_pointLeftUp);
            points.Add(_pointRightUp);
            points.Add(_pointRightDown);
            points.Add(_pointLeftDown);

            Points = points;
            /*Position = PointLeftUp;
            WidthPolygon = PointRightDown.X - PointLeftDown.X;
            Height = PointRightDown.Y - PointRightUp.Y;*/
        }
        /// <summary>
        /// Пустой класс фигуры Прямоугольника
        /// </summary>
        public FigureRectangle()
        {
            X = 0;
            Y = 0;
            Width = 0;
            Height = 0;

            List<Point> points = new List<Point>();
            points.Add(new Point());
            points.Add(new Point());
            points.Add(new Point());
            points.Add(new Point());

            Points = points;

            PointLeftUp = new Point();
            PointRightUp = new Point();
            PointRightDown = new Point();
            PointLeftDown = new Point();
        }

        /// <summary>
        /// Класс фигуры Прямоугольника
        /// </summary>
        /// <param name="pointA">Начальная точка</param>
        /// <param name="pointB">Конечная точка</param>
        public FigureRectangle(Point pointA, Point pointB)
        {
            List<Point> points = new List<Point>();

            points.Add(new Point(pointA.X, pointA.Y));
            points.Add(new Point(pointB.X, pointA.Y));
            points.Add(new Point(pointB.X, pointB.Y));
            points.Add(new Point(pointA.X, pointB.Y));

            X = points.Min(point => point.X);
            Y = points.Min(point => point.Y);
            Width = points.Max(point => point.X) - points.Min(point => point.X);
            Height = points.Max(point => point.Y) - points.Min(point => point.Y);

            Points = points;
        }
        /// <summary>
        /// Левая верхняя точка
        /// </summary>
        public Point PointLeftUp
        {
            get
            {
                return new Point(_pointLeftUp.X, _pointLeftUp.Y);
            }
            set
            {
                if (value.X < PointRightUp.X - _restriction && value.Y < PointLeftDown.Y - _restriction)
                {
                    _pointLeftUp = new Point(value.X, value.Y);

                    _pointLeftDown = new Point(value.X, _pointLeftDown.Y);
                    _pointRightUp = new Point(_pointRightUp.X, value.Y);
                    RefreshList();
                }
            }
        }
        /// <summary>
        /// Правая верхняя точка
        /// </summary>
        public Point PointRightUp
        {
            get
            {
                return new Point(_pointRightUp.X, _pointRightUp.Y);
            }
            set
            {
                if (value.X > PointLeftUp.X + _restriction && value.Y < PointRightDown.Y - _restriction)
                {
                    _pointRightUp = new Point(value.X, value.Y);

                    _pointRightDown = new Point(value.X, _pointRightDown.Y);
                    _pointLeftUp = new Point(_pointLeftUp.X, value.Y);
                    RefreshList();
                }
            }
        }
        /// <summary>
        /// Правая нижняя
        /// </summary>
        public Point PointRightDown
        {
            get
            {
                return new Point(_pointRightDown.X, _pointRightDown.Y);
            }
            set
            {
                if (value.X > PointLeftDown.X + _restriction && value.Y > PointRightUp.Y + _restriction)
                {
                    _pointRightDown = new Point(value.X, value.Y);

                    _pointRightUp = new Point(value.X, _pointRightUp.Y);
                    _pointLeftDown = new Point(_pointLeftDown.X, value.Y);
                    RefreshList();
                }
            }
        }
        /// <summary>
        /// Левая нижняя
        /// </summary>
        public Point PointLeftDown
        {
            get
            {
                return new Point(_pointLeftDown.X, _pointLeftDown.Y);
            }
            set
            {
                if (value.X < PointRightDown.X - _restriction && value.Y > PointLeftUp.Y + _restriction)
                {
                    _pointLeftDown = new Point(value.X, value.Y);

                    _pointRightDown = new Point(_pointRightDown.X, value.Y);
                    _pointLeftUp = new Point(value.X, _pointLeftUp.Y);
                    RefreshList();
                }
            }
        }
        /// <summary>
        /// Список точек многоугольника
        /// </summary>
        public new List<Point> Points
        {
            get
            {
                List<Point> points = new List<Point>();
                points.Add(PointLeftUp);
                points.Add(PointRightUp);
                points.Add(PointRightDown);
                points.Add(PointLeftDown);
                return points;
            }
            set
            {
                if (value.Count == 4)
                {
                    if (value[0].X == value[3].X && value[0].Y == value[1].Y &&
                    value[1].X == value[2].X && value[2].Y == value[3].Y)
                    {
                        SortList(value);
                    }
                    else
                    {
                        throw new Exception("Положение точек не соответсвуют прямоугольнику!");
                    }
                }
                else
                {
                    throw new ArgumentException("Прямоугольник должен содержать строго 4 точки!");
                }
            }
        }
        /// <summary>
        /// Позиция фигуры
        /// </summary>
        public new Point Position
        {
            get
            {
                return new Point(X, Y);
            }
            set
            {
                List<Point> points = new List<Point>();
                for (int i = 0; i < 4; i++)
                {
                    points.Add(new Point(Points[i].X - (X - value.X), Points[i].Y - (Y - value.Y)));
                }
                Points = points;
                X = value.X;
                Y = value.Y;
            }
        }
    }
}