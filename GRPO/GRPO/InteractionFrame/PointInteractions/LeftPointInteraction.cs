using GRPO.Drawing.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GRPO.InteractionFrame.PointInteractions
{
    /// <summary>
    /// Класс левой точки размера фигур
    /// </summary>
    public class LeftPointInteraction
    {
        /// <summary>
        /// Конструктор класс левой точки размера фигур
        /// </summary>
        /// <param name="drawables">Список фигур</param>
        /// <param name="pointRadius">Радиус точки</param>
        public LeftPointInteraction(List<IDrawable> drawables, int pointRadius)
        {
            List<Point> points = new List<Point>();
            foreach (IDrawable drawable in drawables)
            {
                foreach (Point localPoint in drawable.Points)
                {
                    points.Add(localPoint);
                }
            }

            MinX = points.Min(localPoint => localPoint.X);
            MaxX = points.Max(localPoint => localPoint.X);
            Point point = new Point(points.Min(localPoint => localPoint.X),
                points.Min(localPoint => localPoint.Y) +
                (points.Max(localPoint => localPoint.Y) - points.Min(localPoint => localPoint.Y)) / 2);
            PointInteraction = new PointInteraction(point, pointRadius);
            Drawdrawable = drawables;
        }

        /// <summary>
        /// Объект интерактивной точки
        /// </summary>
        public PointInteraction PointInteraction { get; set; }

        /// <summary>
        /// Список фигур
        /// </summary>
        public List<IDrawable> Drawdrawable { get; set; }

        /// <summary>
        /// Минимальное значение точки по X 
        /// </summary>
        public int MinX { get; private set; }

        /// <summary>
        /// Максимальное значение точки по X 
        /// </summary>
        public int MaxX { get; private set; }

        /// <summary>
        /// Изменить размер
        /// </summary>
        /// <param name="initialX">Начальный параметр</param>
        /// <param name="finalX">Конечный параметр</param>
        public void ChangeLeftSize(int initialX, int finalX)
        {
            if (MaxX - finalX > 10)
            {
                int resultMinX = finalX - initialX;
                int width = MaxX - MinX;
                foreach (IDrawable drawable in Drawdrawable)
                {
                    drawable.Position = new Point(drawable.Position.X +
                                                  Convert.ToInt32(
                                                      Convert.ToDouble(resultMinX * (MaxX - drawable.Position.X)) /
                                                      Convert.ToDouble(width)), drawable.Position.Y);
                    drawable.Width = drawable.Width -
                                     Convert.ToInt32(Convert.ToDouble(resultMinX * drawable.Width) /
                                                     Convert.ToDouble(width));
                }

                MinX = MinX + resultMinX;
            }
        }

        /// <summary>
        /// Проверить на нахождении точки в области интерактивной точки
        /// </summary>
        /// <param name="point">Проверяемая точка</param>
        /// <returns>Истина или ложь</returns>
        public bool GetInto(Point point)
        {
            if (PointInteraction.DrawCircle.Position.X <= point.X &&
                PointInteraction.DrawCircle.Position.X + PointInteraction.DrawCircle.Width >= point.X)
                return true;
            return false;
        }
    }
}