using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRPO.Drawing.Interface;

namespace GRPO.InteractionFrame
{
    /// <summary>
    /// Класс проверяющего
    /// </summary>
    class Checking
    {
        /// <summary>
        /// Получить номер выбранной габаритной точки фигуры
        /// </summary>
        /// <param name="point">Локальная точка</param>
        /// <param name="drawable">Проверяемая фигура</param>
        /// <param name="radiusPoint">Радиус точки / Погрешность</param>
        /// <returns>Номер габаритной точки фигуры / (-1) точка не найдена</returns>
        public int GetNumberPoint(Point point,IDrawable drawable,int radiusPoint)
        {
            int number = 0;
            foreach (Point sizePoint in drawable.Points)
            {
                if (point.X >= sizePoint.X - radiusPoint && point.X <= sizePoint.X + radiusPoint &&
                    point.Y >= sizePoint.Y - radiusPoint && point.Y <= sizePoint.Y + radiusPoint)
                {
                    return number;
                }

                Console.WriteLine("point.X: " + point.X + " sizePoint.X: " + sizePoint.X + " point.Y: " + point.Y +
                                  " sizePoint.Y: " + sizePoint.Y);
                number++;
            }

            return -1;
        }
    }
}
