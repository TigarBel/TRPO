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

        /// <summary>
        /// Список ключевых слов
        /// </summary>
        private List<string> _keywords = new List<string>()
            {"Создать фигуру", "Удалить фигуру", "Изменить свойство линии", "Изменить свойство заливки"};
        /// <summary>
        /// Список фигур
        /// </summary>
        private List<IDrawable> _drawablesList = new List<IDrawable>();

        public void CreateFigure(string keywords, Tools tools, List<Point> points, LineProperty lineProperty, FillProperty fillProperty)
        {
            switch (keywords)
            {
                case "Создать фигуру":
                {
                    switch (tools.DrawingTools)
                    {
                        case DrawingTools.DrawFigureLine:
                        {
                            _drawablesList.Add(new DrawFigureLine(points[0], points[1], lineProperty));
                            break;
                        }
                        case DrawingTools.DrawFigurePolyline:
                        {
                            _drawablesList.Add(new DrawFigurePolyline(points, lineProperty));
                            break;
                        }
                        case DrawingTools.DrawFigureRectangle:
                        {
                            _drawablesList.Add(
                                new DrawFigureRectangle(points[0], points[1], lineProperty, fillProperty));
                            break;
                        }
                        case DrawingTools.DrawFigureCircle:
                        {
                            _drawablesList.Add(new DrawFigureCircle(points[0], points[1], lineProperty, fillProperty));
                            break;
                        }
                        case DrawingTools.DrawFigureEllipse:
                        {
                            _drawablesList.Add(new DrawFigureEllipse(points[0], points[1], lineProperty, fillProperty));
                            break;
                        }
                    }

                    break;
                }
                case "Удалить фигуру":
                {
                    _drawablesList.Remove(_drawablesList[_drawablesList.Count - 1]);
                    break;
                }
            }

            Console.WriteLine(keywords);
        }

        public void ChangeProperty(string keywords, int index, LineProperty _oldLineProperty,
            LineProperty _newLineProperty, FillProperty _oldFillProperty, FillProperty _newFillProperty)
        {
            switch (keywords)
            {
                case "Изменить свойство линии": ((ILinePropertyble)_drawablesList[index]).LineProperty = _newLineProperty; break;
                case "Изменить свойство заливки":((IFillPropertyble)_drawablesList[index]).FillProperty = _newFillProperty; break;
                case "Вернуть свойство линии": ((ILinePropertyble)_drawablesList[index]).LineProperty = _oldLineProperty;break;
                case "Вернуть свойство заливки": ((IFillPropertyble)_drawablesList[index]).FillProperty = _oldFillProperty;break;
            }
            Console.WriteLine(keywords);
        }
        /// <summary>
        /// Список фигур
        /// </summary>
        public List<IDrawable> Drawables
        {
            get { return _drawablesList; }
        }
        /// <summary>
        /// Список ключевых слов
        /// </summary>
        public  List<string> Keywords
        {
            get { return _keywords; }
        }
    }
}
