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
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class GraphicsEditor
    {

        public List<IDrawable> _drawablesList = new List<IDrawable>();

        public void CreateFigure(string keywords, List<Point> points, LineProperty lineProperty, FillProperty fillProperty)
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

        public void ChangeProperty(string keywords, int index, LineProperty lineProperty, FillProperty fillProperty)
        {
            switch (keywords)
            {
                case "Изменить свойство линии": ((ILinePropertyble)_drawablesList[index]).LineProperty = lineProperty; break;
                case "Изменить свойство заливки":((IFillPropertyble)_drawablesList[index]).FillProperty = fillProperty; break;
                case "Вернуть свойство":
                    {
                        if (_drawablesList is ILinePropertyble)
                        {
                            ((ILinePropertyble)_drawablesList[index]).LineProperty = lineProperty;
                        }
                        if (_drawablesList is IFillPropertyble)
                        {
                            ((IFillPropertyble)_drawablesList[index]).FillProperty = fillProperty;
                        }
                        break;
                    }
            }
            Console.WriteLine(keywords);
        }
    }
}
