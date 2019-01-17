using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPO
{
    /// <summary>
    /// Сортировщик точек
    /// </summary>
    public class PointSorter
    {

        public PointSorter()
        {

        }
        /// <summary>
        /// Сортировать точки в Прямоугольник
        /// </summary>
        /// <param name="points">Список точек</param>
        /// <returns>Список точек формой прямоугольника</returns>
        public List<Point> SortPointsInRectangle(List<Point> points)
        {
            int minX = points.Min(point => point.X);
            int maxX = points.Max(point => point.X);
            int minY = points.Min(point => point.Y);
            int maxY = points.Max(point => point.Y);

            points.Clear();
            points.Add(new Point(minX, minY));
            points.Add(new Point(maxX, minY));
            points.Add(new Point(maxX, maxY));
            points.Add(new Point(minX, maxY));
            return points;
        }
    }
}
