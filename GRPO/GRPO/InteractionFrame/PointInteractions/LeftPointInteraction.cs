using GRPO.Drawing.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPO.InteractionFrame.PointInteractions
{
    public class LeftPointInteraction
    {

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

        public PointInteraction PointInteraction { get; set; }

        public List<IDrawable> Drawdrawable { get; set; }

        public int MinX { get; private set; }

        public int MaxX { get; private set; }

        public void ChangeUpSize(int initialX, int finalX)
        {
            if (MaxX - finalX - initialX > 10)
            {
                int resultMinX = finalX - initialX;
                int result = MaxX - MinX;
                foreach (IDrawable drawable in Drawdrawable)
                {
                    if (MaxX - drawable.Position.X == 0) throw new ArgumentException("Обалдеть, 0!");
                    drawable.Position = new Point(drawable.Position.X +
                                                  Convert.ToInt32(Convert.ToDouble(resultMinX) *
                                                                  (Convert.ToDouble(result) /
                                                                   Convert.ToDouble((MaxX - drawable.Position.X)))),
                        drawable.Position.Y);
                    drawable.Width = drawable.Width -
                                      Convert.ToInt32(Convert.ToDouble(resultMinX) *
                                                      (Convert.ToDouble(result) /
                                                       Convert.ToDouble((MaxX - drawable.Position.X))));
                }

                MinX = resultMinX;
            }
        }
    }
}
