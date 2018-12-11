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
    }
}
