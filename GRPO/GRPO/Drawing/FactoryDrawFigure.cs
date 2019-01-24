using GRPO.Drawing.Interface;
using GRPO.Drawing.Property;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPO.Drawing
{
    /// <summary>
    /// Фабрика рисуемых фигур
    /// </summary>
    class FactoryDrawFigure
    {
        /// <summary>
        /// Создание простой фигуры
        /// </summary>
        /// <param name="pointA">Начальная точка</param>
        /// <param name="pointB">Конечная точка</param>
        /// <param name="lineProperty">Свойство линии</param>
        /// <param name="fillProperty">Свойство заливки</param>
        /// <param name="selectTool">Выбранный инструмент / фигура</param>
        /// <returns></returns>
        public IDrawable SimpleFigure(Point pointA, Point pointB, LineProperty lineProperty, FillProperty fillProperty, DrawingTools selectTool)
        {
            switch (selectTool)
            {
                case DrawingTools.DrawFigureLine:
                    {
                        DrawFigureLine drawFigure = new DrawFigureLine(pointA, pointB, lineProperty);
                        return drawFigure;
                    }
                case DrawingTools.DrawFigureRectangle:
                    {
                        DrawFigureRectangle drawFigure = new DrawFigureRectangle(pointA, pointB, lineProperty, fillProperty);
                        return drawFigure;
                    }
                case DrawingTools.DrawFigureCircle:
                    {
                        DrawFigureCircle drawFigure = new DrawFigureCircle(pointA, pointB, lineProperty, fillProperty);
                        return drawFigure;
                    }
                case DrawingTools.DrawFigureEllipse:
                    {
                        DrawFigureEllipse drawFigure = new DrawFigureEllipse(pointA, pointB, lineProperty, fillProperty);
                        return drawFigure;
                    }
            }
            return null;
        }
        /// <summary>
        /// Создание полифигур
        /// </summary>
        /// <param name="points">Список точек</param>
        /// <param name="lineProperty">Свойство линии</param>
        /// <param name="fillProperty">Свойство заливки</param>
        /// <param name="selectTool">Выбранный инструмент / фигура</param>
        /// <returns></returns>
        public IDrawable PolyFigure(List<Point> points, LineProperty lineProperty, FillProperty fillProperty, DrawingTools selectTool)
        {
            switch (selectTool)
            {
                case DrawingTools.DrawFigurePolyline:
                {
                    DrawFigurePolyline drawFigure = new DrawFigurePolyline(points, lineProperty);
                    return drawFigure;
                }
                case DrawingTools.DrawFigurePolygon:
                {
                    DrawFigurePolygon drawFigure = new DrawFigurePolygon(points, lineProperty, fillProperty);
                    return drawFigure;
                }
            }

            return null;
        }
    }
}
