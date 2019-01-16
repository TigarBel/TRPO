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
        ///  Пустой класс фигуры Эллипс
        /// </summary>
        public FigureEllipse()
        {
            X = 0;
            Y = 0;
            Width = 0;
            Height = 0;
        }
        /// <summary>
        /// Класс фигуры Эллипс
        /// </summary>
        /// <param name="position">Расположения эллипса</param>
        /// <param name="width">Ширина эллипса</param>
        /// <param name="height">Высота эллипса</param>
        public FigureEllipse(Point position, int width, int height)
        {
            X = position.X;
            Y = position.Y;
            Width = width;
            Height = height;
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
        public int WidthEllipse
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
                }
            }
        }
        /// <summary>
        /// Высота фигуры
        /// </summary>
        public int HeightEllipse
        {
            get
            {
                return Height;
            }
            set
            {
                if (value > 5)
                {
                    Height = value;
                }
            }
        }
    }
}
