using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GRPO
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
        /// <param name="position">Расположение окружности</param>
        /// <param name="radius"> Радиус окружности</param>
        public FigureCircle(Point position, int radius)
        {
            X = position.X - radius;
            Y = position.Y - radius;
            Radius = radius;
            Width = Radius * 2;
            Height = Radius * 2;
        }
        /// <summary>
        /// Радиус фигуры Окружность
        /// </summary>
        public int Radius
        {
            get
            {
                return _radius;
            }
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
        public Point Position
        {
            get
            {
                return new Point(X, Y);
            }
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
            get
            {
                return Width;
            }
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
            get
            {
                return Height;
            }
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
