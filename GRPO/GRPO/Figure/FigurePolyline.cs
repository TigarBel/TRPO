using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GRPO
{
    class FigurePolyline : Figure
    {
        /// <summary>
        /// Список точек полилинии
        /// </summary>
        private List<Point> _points;
        /// <summary>
        /// Ложь: линейная; Истина: дуговая
        /// </summary>
        private bool _circular;
        /// <summary>
        /// Обновить значения для класса Фигура
        /// </summary>
        private void RefreshValues()
        {
            X = Points.Min(point => point.X);
            Y = Points.Min(point => point.Y);
            Width = Points.Max(point => point.X) - Points.Min(point => point.X);
            Height = Points.Max(point => point.Y) - Points.Min(point => point.Y);
        }
        /// <summary>
        /// Пустой класс фигуры Плилиния
        /// </summary>
        public FigurePolyline()
        {
            X = 0;
            Y = 0;
            Width = 0;
            Height = 0;
            Points = new List<Point>();
            Circular = false;
        }
        /// <summary>
        /// Класс фигуры Полилиния
        /// </summary>
        /// <param name="points">Список точек</param>
        /// <param name="circular">Ложь: линейная; Истина: дуговая</param>
        public FigurePolyline(List<Point> points, bool circular)
        {
            Points = points;
            Circular = circular;
        }
        /// <summary>
        /// Список точек полилинии
        /// </summary>
        public List<Point> Points
        {
            get
            {
                return _points;
            }
            set
            {
                if (value.Count >= 1)
                {
                    _points = value;
                    if (_points.Count > 1)
                    {
                        RefreshValues();
                    }
                }
                else
                {
                    throw new ArgumentException("Спиcок не содержит точки!");
                }
            }
        }
        /// <summary>
        /// Ложь: линейная; Истина: дуговая
        /// </summary>
        public bool Circular
        {
            get
            {
                return _circular;
            }
            set
            {
                _circular = value;
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
        public int WidthPolyline
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
                        for (int i = 0; i < Points.Count; i++)
                        {
                            Points[i] = new Point(X +
                                Convert.ToInt32((float)(Points[i].X - X) / (float)Width * (float)value),
                                Points[i].Y);
                        }
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
        public int HeightPolyline
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
                        for (int i = 0; i < Points.Count; i++)
                        {
                            Points[i] = new Point(Points[i].X,
                                Y + Convert.ToInt32((float)(Points[i].Y - Y) / (float)Height * (float)value));
                        }
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
