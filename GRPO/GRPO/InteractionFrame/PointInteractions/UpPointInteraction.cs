using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRPO.Drawing.Interface;

namespace GRPO.InteractionFrame.PointInteractions
{
    public class UpPointInteraction
    {

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

        public PointInteraction PointInteraction { get; set; }

        public List<IDrawable> Drawdrawable { get; set; }

        public int MinY { get; private set; }

        public int MaxY { get; private set; }

        public void ChangeUpSize(int initialY, int finalY)
        {
            if (MaxY - (finalY - initialY) > 10)
            {
                int resultMinY = finalY - initialY;
                /*int result = MaxY - MinY;
                foreach (IDrawable drawable in Drawdrawable)
                {
                    if (MaxY - drawable.Position.Y == 0) throw new ArgumentException("Обалдеть, 0!");
                    drawable.Position = new Point(drawable.Position.X,
                        drawable.Position.Y +
                        Convert.ToInt32(Convert.ToDouble(resultMinY) *
                                        (Convert.ToDouble(result) / Convert.ToDouble((MaxY - drawable.Position.Y)))));
                    drawable.Height = drawable.Height -
                                      Convert.ToInt32(Convert.ToDouble(resultMinY) *
                                                      (Convert.ToDouble(result) /
                                                       Convert.ToDouble((MaxY - drawable.Position.Y))));
                }*/
                foreach (IDrawable drawable in Drawdrawable)
                {
                    drawable.Position = new Point(drawable.Position.X, drawable.Position.Y + resultMinY);
                    drawable.Height = drawable.Height + resultMinY;
                }
                MinY = resultMinY;
            }
        }
    }
}
