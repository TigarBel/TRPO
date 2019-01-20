using GRPO.Drawing;
using GRPO.Drawing.Interface;
using GRPO.Drawing.Property;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPO.Commands
{
    public class GraphicsEditor
    {

        public List<IDrawable> _drawablesList = new List<IDrawable>();

        public void Operation(string keywords, List<Point> points, LineProperty lineProperty, FillProperty fillProperty)
        {
            switch (keywords)
            {
                case "Создать линию": _drawablesList.Add(new DrawFigureLine(points[0], points[1], lineProperty)); break;
                case "Создать полилинию": _drawablesList.Add(new DrawFigurePolyline(points, lineProperty)); break;
                case "Создать прямоугольник": _drawablesList.Add(new DrawFigureRectangle(points[0], points[1], lineProperty, fillProperty)); break;
                case "Создать окружность": _drawablesList.Add(new DrawFigureCircle(points[0], points[1], lineProperty, fillProperty)); break;
                case "Создать эллипс": _drawablesList.Add(new DrawFigureEllipse(points[0], points[1], lineProperty, fillProperty)); break;
                case "Удалить фигуру": _drawablesList.Remove(_drawablesList[_drawablesList.Count - 1]); break;
            }
            Console.WriteLine(keywords);
        }
    }
}
