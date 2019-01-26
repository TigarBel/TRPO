using GRPO.Drawing.Interface;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GRPO.InteractionFrame.PointInteractions
{
    /// <summary>
    /// Класс правой точки размера фигур
    /// </summary>
    public class RightPointInteraction
    {
        /// <summary>
        /// Конструктор класс правой точки размера фигур
        /// </summary>
        /// <param name="drawables">Список фигур</param>
        /// <param name="pointRadius">Радиус точки</param>
        public RightPointInteraction(List<IDrawable> drawables, int pointRadius)
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
            Point point = new Point(points.Max(localPoint => localPoint.X),
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
        /// <param name="initialY">Начальный параметр</param>
        /// <param name="finalY">Конечный параметр</param>
        public void ChangeRightSize(int initialX, int finalX)
        {
            if (finalX - MinX > 10)
            {
                int resultMaxX = finalX - initialX;
                foreach (IDrawable drawable in Drawdrawable)
                {
                    drawable.Width = drawable.Width + resultMaxX;
                }

                MaxX = MaxX + resultMaxX;
            }
        }
    }
}
