using GRPO.Drawing.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPO.InteractionFrame.PointInteractions
{
    public class DownPointInteraction
    {
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

        public PointInteraction PointInteraction { get; set; }

        public List<IDrawable> Drawdrawable { get; set; }

        public int MinY { get; private set; }

        public int MaxY { get; private set; }

        public void ChangeUpSize(int initialY, int finalY)
        {
            if (finalY - initialY - MinY > 10)
            {
                int resultMaxY = finalY - initialY;
                int result = MaxY - MinY;
                foreach (IDrawable drawable in Drawdrawable)
                {
                    if (result - drawable.Height == 0) throw new ArgumentException("Обалдеть, 0!");
                    drawable.Height = drawable.Height +
                                      Convert.ToInt32(Convert.ToDouble(resultMaxY) *
                                                      (Convert.ToDouble(result) / Convert.ToDouble(drawable.Height)));
                }

                MaxY = resultMaxY;
            }
        }
    }
}
