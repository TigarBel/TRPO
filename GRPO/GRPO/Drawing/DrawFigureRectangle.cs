using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GRPO
{
    class DrawFigureRectangle : DrawFigurePolygon
    {
        /// <summary>
        /// Пустой класс Отрисовки прямоугольника
        /// </summary>
        public DrawFigureRectangle()
        {
            Polygon = new FigurePolygon();
            Canvas = new PictureBox();
            LineProperty = new LineProperty();
            FillProperty = new FillProperty();
        }
        /// <summary>
        /// Класс Отрисовки прямоугольника
        /// </summary>
        /// <param name="pointA">Начальная угловая точка</param>
        /// <param name="pointB">Конечная угловая точка</param>
        /// <param name="canvas">Полотно на котором рисуется фигура</param>
        /// <param name="lineProperty">Свойство линии</param>
        /// <param name="fillProperty">Свойство заливки</param>
        public DrawFigureRectangle(Point pointA, Point pointB, PictureBox canvas, LineProperty lineProperty, FillProperty fillProperty)
        {
            List<Point> points = new List<Point>();
            points.Add(pointA);
            points.Add(new Point(pointA.X, pointB.Y));
            points.Add(pointB);
            points.Add(new Point(pointB.X, pointA.Y));

            Polygon = new FigurePolygon(points);
            Canvas = canvas;
            LineProperty = lineProperty;
            FillProperty = fillProperty;
        }
    }
}
