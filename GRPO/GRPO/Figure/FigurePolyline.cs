using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace GRPO.Figure
{
    /// <summary>
    /// Класс фигуры полилиния
    /// </summary>
    [Serializable]
    public class FigurePolyline : BaseFigure
    {
        /// <summary>
        /// Список точек полилинии
        /// </summary>
        private List<Point> _points;

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
        }

        /// <summary>
        /// Класс фигуры Полилиния
        /// </summary>
        /// <param name="points">Список точек</param>
        public FigurePolyline(List<Point> points)
        {
            Points = points;
        }

        /// <summary>
        /// Список точек полилинии
        /// </summary>
        public List<Point> Points
        {
            get { return _points; }
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
        public int WidthPolyline
        {
            get { return Width; }
            set
            {
                if (value > 10)
                {
                    if (Width != 0)
                    {
                        for (int i = 0; i < Points.Count; i++)
                        {
                            Points[i] = new Point(StandPoint(X, Points[i].X, Width, value), Points[i].Y);
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
            get { return Height; }
            set
            {
                if (value > 10)
                {
                    if (Height != 0)
                    {
                        for (int i = 0; i < Points.Count; i++)
                        {
                            Points[i] = new Point(Points[i].X, StandPoint(Y, Points[i].Y, Height, value));
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

        /// <summary>
        /// Установление точки в соответствии с изменение размера
        /// </summary>
        /// <param name="positionValue">Позация фигуры</param>
        /// <param name="pointValue">Позиция точки</param>
        /// <param name="sizeValue">Размер фигуры</param>
        /// <param name="value">Измененный размер фигуры</param>
        /// <returns></returns>
        private int StandPoint(int positionValue, int pointValue, int sizeValue, int value)
        {
            return positionValue +
                   Convert.ToInt32(((float) (pointValue - positionValue)) / ((float) sizeValue) * ((float) value));
        }
    }
}
