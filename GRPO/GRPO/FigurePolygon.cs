using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GRPO
{
    class FigurePolygon : Figure
    {
        /// <summary>
        /// Список точек многоугольника
        /// </summary>
        private List<Point> _points;
        /// <summary>
        /// Внести значения в класс фигура
        /// </summary>
        /// <param name="a">Начальная точка</param>
        /// <param name="b">Конечная точка</param>
        private void InitFigure(Point a, Point b)
        {
            if (a.X <= b.X)
            {
                X = a.X;
            }
            else
            {
                X = b.X;
            }
            if (a.Y <= b.Y)
            {
                Y = a.Y;
            }
            else
            {
                Y = b.Y;
            }
            Width = Math.Abs(a.X - b.X);
            Height = Math.Abs(a.Y - b.Y);
        }
        /// <summary>
        /// Пустой класс фигуры Многогранник
        /// </summary>
        public FigurePolygon()
        {
            X = 0;
            Y = 0;
            Width = 0;
            Height = 0;
            Points = new List<Point>();
        }
        /// <summary>
        /// Класс фигуры Многогранник
        /// </summary>
        /// <param name="position">Точка расположения многоугольника</param>
        /// <param name="width">Ширина многоугольника</param>
        /// <param name="height">Высота многоугольника</param>
        /// <param name="countAngle">Количество углов многоугольника</param>
        /// <param name="phase">Угол поворота многоугольника, в градусах</param>
        public FigurePolygon(Point position, int width, int height, int countAngle, int phase)
        {
            X = position.X;
            Y = position.Y;
            Width = width;
            Height = height;
            Points = new List<Point>();

            for (int i = 0; i < countAngle; i++)
            {
                //double angleInDegrees = 32.471192290848492;
                //double cos = Math.Cos(angleInDegrees * (Math.PI / 180.0));
                double cos = Math.Cos((i * (360 / countAngle) + phase) * (Math.PI / 180.0));
                double sin = Math.Sin((i * (360 / countAngle) + phase) * (Math.PI / 180.0));
                double x = X + Width / 2 + Width * cos / 2;
                double y = Y + Height / 2 + Height * sin / 2;
                Point point = new Point(Convert.ToInt32(x), Convert.ToInt32(y));
                Points.Add(point);
            }
        }
        /// <summary>
        /// Класс фигуры Многогранник
        /// </summary>
        /// <param name="a">Начальная точка</param>
        /// <param name="b">Конечная точка</param>
        /// <param name="countAngle">Количество углов многоугольника</param>
        /// <param name="phase">Угол поворота многоугольника, в градусах</param>
        public FigurePolygon(Point a, Point b,int countAngle,int phase)
        {
            InitFigure(a, b);
            Points = new List<Point>();

            for (int i = 0; i < countAngle; i++)
            {
                double cos = Math.Cos((i * (360 / countAngle) + phase) * (Math.PI / 180.0));
                double sin = Math.Sin((i * (360 / countAngle) + phase) * (Math.PI / 180.0));
                double x = X + Width / 2 + Width * cos / 2;
                double y = Y + Height / 2 + Height * sin / 2;
                Point point = new Point(Convert.ToInt32(x), Convert.ToInt32(y));
                Points.Add(point);
            }
        }
        /// <summary>
        /// Класс фигуры Многогранник
        /// </summary>
        /// <param name="points">Список существующих точек для многоугольника</param>
        public FigurePolygon(List<Point> points)
        {
            Points = points;
            X = Points.Min(point => point.X);
            Y = Points.Min(point => point.Y);
            Width = Points.Max(point => point.X) - Points.Min(point => point.X);
            Height = Points.Max(point => point.Y) - Points.Min(point => point.Y);
        }
        /// <summary>
        /// Список точек многоугольника
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
    }
}
