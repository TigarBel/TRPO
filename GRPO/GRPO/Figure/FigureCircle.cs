using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GRPO.Figure
{
    /// <summary>
    /// Класс фигуры - круг
    /// </summary>
    [Serializable]
    class FigureCircle : FigureEllipse
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
            X = 0;
            Y = 0;
            Width = 0;
            Height = 0;
            Radius = 0;
        }
        /// <summary>
        /// Класс фигуры Окружность
        /// </summary>
        /// <param name="pointA">Расположение окружности / начальная точка</param>
        /// <param name="pointB">Конечная точка</param>
        public FigureCircle(Point pointA, Point pointB)
        {
            Radius = Convert.ToInt32(Math.Sqrt(Convert.ToDouble(Math.Pow((pointB.X - pointA.X), 2) + Math.Pow((pointB.Y - pointA.Y), 2))));
            X = pointA.X - Radius;
            Y = pointA.Y - Radius;
            Radius = Radius;
            Width = Radius * 2;
            Height = Radius * 2;
        }
        /// <summary>
        /// Радиус фигуры Окружность
        /// </summary>
        public int Radius
        {
            get { return _radius; }
            set
            {
                Width = value * 2;
                Height = value * 2;
                _radius = value;
            }
        }
        /// <summary>
        /// Позиция фигуры
        /// </summary>
        public new Point Position
        {
            get { return new Point(X, Y); }
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }
        /// <summary>
        /// Ширина фигуры
        /// </summary>
        public int WidthCircle
        {
            get { return Width; }
            set
            {
                if (value > 5)
                {
                    Width = value;
                    Height = value;
                }
            }
        }
        /// <summary>
        /// Высота фигуры
        /// </summary>
        public int HeightCircle
        {
            get { return Height; }
            set
            {
                if (value > 5)
                {
                    Width = value;
                    Height = value;
                }
            }
        }
    }
}
