using System;
using System.Collections.Generic;
using System.Drawing;
using GRPO.Drawing.Interface;
using GRPO.InteractionFrame.PointInteractions;

namespace GRPO.InteractionFrame
{
    /// <summary>
    /// Класс проверяющего
    /// </summary>
    public class Checking
    {
        /// <summary>
        /// Получить номер выбранной габаритной точки фигуры
        /// </summary>
        /// <param name="point">Локальная точка</param>
        /// <param name="drawable">Проверяемая фигура</param>
        /// <param name="radiusPoint">Радиус точки / Погрешность</param>
        /// <returns>Номер габаритной точки фигуры / (-1) точка не найдена</returns>
        public int GetNumberPoint(Point point, IDrawable drawable, int radiusPoint)
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

        /// <summary>
        /// Взять номер точки размера интерактива
        /// </summary>
        /// <param name="point">Точка</param>
        /// <param name="drawables">Список фигур</param>
        /// <param name="radiusPoint">Радиус взаимодействия</param>
        /// <returns></returns>
        public int GetNumberSizePoint(Point point, List<IDrawable> drawables, int radiusPoint)
        {
            InteractionPoints interactionPoints = new InteractionPoints(drawables, radiusPoint);
            if (interactionPoints.UpPointInteraction.PointInteraction.GetInto(point))
            {
                return 0;
            }
            else if(interactionPoints.RightPointInteraction.PointInteraction.GetInto(point))
            {
                return 1;
            }
            else if (interactionPoints.DownPointInteraction.PointInteraction.GetInto(point))
            {
                return 2;
            }
            else if (interactionPoints.LeftPointInteraction.PointInteraction.GetInto(point))
            {
                return 3;
            }
            return -1;
        }
    }
}
