using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GRPO.Figure
{
    /// <summary>
    /// Класс фигуры - эллипс
    /// </summary>
    [Serializable]
    class FigureEllipse : FigureLine
    {
        /// <summary>
        ///  Пустой класс фигуры Эллипс
        /// </summary>
        public FigureEllipse()
        {

        }
        /// <summary>
        /// Класс фигуры Эллипс
        /// </summary>
        /// <param name="position">Расположения эллипса</param>
        /// <param name="width">Ширина эллипса</param>
        /// <param name="height">Высота эллипса</param>
        public FigureEllipse(Point pointA, Point pointB)
        {
            PointA = pointA;
            PointB = pointB;
            Init();
        }
        /// <summary>
        /// Ширина фигуры
        /// </summary>
        public int WidthEllipse
        {
            get { return Width; }
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
            get { return Height; }
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
