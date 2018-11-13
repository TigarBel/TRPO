using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GRPO
{
    class FigureEllipse : Figure
    {
        /// <summary>
        /// Наклон эллипса
        /// </summary>
        private int _angle;
        /// <summary>
        ///  Пустой класс фигуры Эллипс
        /// </summary>
        public FigureEllipse()
        {
            X = 0;
            Y = 0;
            Width = 0;
            Height = 0;
            Angle = 0;
        }
        /// <summary>
        /// Класс фигуры Эллипс
        /// </summary>
        /// <param name="position">Расположения эллипса</param>
        /// <param name="width">Ширина эллипса</param>
        /// <param name="height">Высота эллипса</param>
        /// <param name="angle">Наклон элипса</param>
        public FigureEllipse(Point position, int width, int height, int angle)
        {
            X = position.X;
            Y = position.Y;
            Width = width;
            Height = height;
            Angle = angle;
        }
        /// <summary>
        /// Наклон эллипса
        /// </summary>
        public int Angle
        {
            get
            {
                return _angle;
            }
            set
            {
                _angle = value;
            }
        }
    }
}
