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
        /// <param name="a">Первая точка</param>
        /// <param name="b">Вторая точка</param>
        /// <param name="circular">Ложь: линейная; Истина: дуговая</param>
        public FigurePolyline(Point a, Point b, bool circular)
        {
            Points = new List<Point>();
            AddPoint(a);
            AddPoint(b);
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
                _points = value;
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
        /// Добавить точку для продолжения полилинии
        /// </summary>
        /// <param name="point">Точка</param>
        public void AddPoint(Point point)
        {
            _points.Add(point);
            RefreshValues();
        }
    }
}
