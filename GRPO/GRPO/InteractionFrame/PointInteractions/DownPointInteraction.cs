using GRPO.Drawing.Interface;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GRPO.InteractionFrame.PointInteractions
{
    /// <summary>
    /// Класс нижней точки размера фигур
    /// </summary>
    public class DownPointInteraction
    {
        /// <summary>
        /// Конструктор класс нижней точки размера фигур
        /// </summary>
        /// <param name="drawables">Список фигур</param>
        /// <param name="pointRadius">Радиус точки</param>
        public DownPointInteraction(List<IDrawable> drawables, int pointRadius)
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
                points.Max(localPoint => localPoint.Y));
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
        public void ChangeDownSize(int initialY, int finalY)
        {
            if (finalY - MinY > 10)
            {
                int resultMaxY = finalY - initialY;
                foreach (IDrawable drawable in Drawdrawable)
                {
                    drawable.Height = drawable.Height + resultMaxY;
                }
                MaxY = MaxY + resultMaxY;
            }
        }
    }
}
