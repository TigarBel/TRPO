using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using GRPO.Drawing.Interface;

namespace GRPO.InteractionFrame.PointInteractions
{
    /// <summary>
    /// Класс верхней точки размера фигур
    /// </summary>
    public class UpPointInteraction
    {
        /// <summary>
        /// Конструктор класс верхней точки размера фигур
        /// </summary>
        /// <param name="drawables">Список фигур</param>
        /// <param name="pointRadius">Радиус точки</param>
        public UpPointInteraction(List<IDrawable> drawables, int pointRadius)
        {
            List<Point> points = new List<Point>();
            foreach (IDrawable drawable in drawables)
            {
                foreach (Point localPoint in drawable.Points)
                {
                    points.Add(localPoint);
                }
            }

            MinY = points.Min(localPoint => localPoint.Y);
            MaxY = points.Max(localPoint => localPoint.Y);
            Point point = new Point(
                points.Min(localPoint => localPoint.X) +
                (points.Max(localPoint => localPoint.X) - points.Min(localPoint => localPoint.X)) / 2,
                points.Min(localPoint => localPoint.Y));
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
        /// Минимальное значение точки по Y
        /// </summary>
        public int MinY { get; private set; }

        /// <summary>
        /// Максимальное значение точки по Y
        /// </summary>
        public int MaxY { get; private set; }

        /// <summary>
        /// Изменить размер
        /// </summary>
        /// <param name="initialY">Начальный параметр</param>
        /// <param name="finalY">Конечный параметр</param>
        public void ChangeUpSize(int initialY, int finalY)
        {
            if (MaxY - finalY > 10)
            {
                int resultMinY = finalY - initialY;
                int height = MaxY - MinY;
                foreach (IDrawable drawable in Drawdrawable)
                {
                    drawable.Position = new Point(drawable.Position.X,
                        drawable.Position.Y +
                        Convert.ToInt32(Convert.ToDouble(resultMinY * (MaxY - drawable.Position.Y)) /
                                        Convert.ToDouble(height)));
                    drawable.Height = drawable.Height -
                                      Convert.ToInt32(Convert.ToDouble(resultMinY * drawable.Height) /
                                                      Convert.ToDouble(height));
                }

                MinY = MinY + resultMinY;
            }
        }

        /// <summary>
        /// Проверить на нахождении точки в области интерактивной точки
        /// </summary>
        /// <param name="point">Проверяемая точка</param>
        /// <returns>Истина или ложь</returns>
        public bool GetInto(Point point)
        {
            if (PointInteraction.DrawCircle.Position.Y <= point.Y &&
                PointInteraction.DrawCircle.Position.Y + PointInteraction.DrawCircle.Height >= point.Y)
                return true;
            return false;
        }
    }
}