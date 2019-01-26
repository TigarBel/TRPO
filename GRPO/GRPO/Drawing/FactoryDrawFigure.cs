using GRPO.Drawing.Interface;
using GRPO.Drawing.Property;
using System.Collections.Generic;
using System.Drawing;

namespace GRPO.Drawing
{
    /// <summary>
    /// Фабрика рисуемых фигур
    /// </summary>
    public class FactoryDrawFigure
    {
        /// <summary>
        /// Создание полифигур
        /// </summary>
        /// <param name="points">Список точек</param>
        /// <param name="lineProperty">Свойство линии</param>
        /// <param name="fillProperty">Свойство заливки</param>
        /// <param name="selectTool">Выбранный инструмент / фигура</param>
        /// <returns></returns>
        public IDrawable DrawFigure(List<Point> points, LineProperty lineProperty, FillProperty fillProperty, DrawingTools selectTool)
        {
            switch (selectTool)
            {
                case DrawingTools.DrawFigureLine:
                {
                    return new DrawFigureLine(points[0], points[1], lineProperty);
                }
                case DrawingTools.DrawFigureRectangle:
                {
                    return new DrawFigureRectangle(points[0], points[1], lineProperty, fillProperty);
                }
                case DrawingTools.DrawFigureCircle:
                {
                    return new DrawFigureCircle(points[0], points[1], lineProperty, fillProperty);
                }
                case DrawingTools.DrawFigureEllipse:
                {
                    return new DrawFigureEllipse(points[0], points[1], lineProperty, fillProperty);
                }
                case DrawingTools.DrawFigurePolyline:
                {
                    return new DrawFigurePolyline(points, lineProperty);
                }
                case DrawingTools.DrawFigurePolygon:
                {
                    return new DrawFigurePolygon(points, lineProperty, fillProperty);
                }
            }

            return null;
        }
    }
}
