using System;
using System.Collections.Generic;
using System.Drawing;

namespace GRPO.Figure
{
    /// <summary>
    /// Класс фигуры - эллипс
    /// </summary>
    [Serializable]
    public class FigureEllipse : FigureLine
    {
        /// <summary>
        ///  Пустой класс фигуры Эллипс
        /// </summary>
        public FigureEllipse()
        {
        }

        /// <summary>
        /// Конструктор класс фигуры - эллипс
        /// </summary>
        /// <param name="pointA">Начальная точка</param>
        /// <param name="pointB">Конечная точка</param>
        public FigureEllipse(Point pointA, Point pointB)
        {
            PointA = pointA;
            PointB = pointB;
            Init();
        }
    }
}