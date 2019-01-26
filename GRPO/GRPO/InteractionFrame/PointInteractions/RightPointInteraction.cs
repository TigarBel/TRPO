using GRPO.Drawing.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPO.InteractionFrame.PointInteractions
{
    public class RightPointInteraction
    {
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

        public PointInteraction PointInteraction { get; set; }

        public List<IDrawable> Drawdrawable { get; set; }

        public int MinX { get; private set; }

        public int MaxX { get; private set; }

        public void ChangeUpSize(int initialX, int finalX)
        {
            if (finalX - initialX - MinX > 10)
            {
                int resultMaxX = finalX - initialX;
                int result = MaxX - MinX;
                foreach (IDrawable drawable in Drawdrawable)
                {
                    if (result - drawable.Width == 0) throw new ArgumentException("Обалдеть, 0!");
                    drawable.Width = drawable.Width +
                                     Convert.ToInt32(Convert.ToDouble(resultMaxX) *
                                                     (Convert.ToDouble(result) / Convert.ToDouble(drawable.Width)));
                }

                MaxX = resultMaxX;
            }
        }
    }
}
