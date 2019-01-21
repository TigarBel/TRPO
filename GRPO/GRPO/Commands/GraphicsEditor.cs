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
    /// Класс хранения реализации команд
    /// </summary>
    [Serializable]
    public class GraphicsEditor
    {

        /// <summary>
        /// Список ключевых слов
        /// </summary>
        private List<string> _keywords = new List<string>()
        {
            "Создать фигуру", "Удалить фигуру", "Изменить свойство линии", "Изменить свойство заливки",
            "Удалить весь список", "Удалить элементы", "Удалить элемент"
        };
        /// <summary>
        /// Список фигур
        /// </summary>
        private List<IDrawable> _drawablesList = new List<IDrawable>();
        /// <summary>
        /// Команда по созданию фигур
        /// </summary>
        /// <param name="keywords">Команда</param>
        /// <param name="tools">Используемый инструмент</param>
        /// <param name="points">Точки образующие фигуру</param>
        /// <param name="lineProperty">Свойство линии</param>
        /// <param name="fillProperty">Свойство звлиыки</param>
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
        /// <summary>
        /// Командо для изменения свойств фигуры
        /// </summary>
        /// <param name="keywords">Команда</param>
        /// <param name="index">Индекс фигуры из списка</param>
        /// <param name="oldLineProperty">Старое свойство линии</param>
        /// <param name="newLineProperty">Новое свойство линии</param>
        /// <param name="oldFillProperty">Старое свойство заливки</param>
        /// <param name="newFillProperty">Новое свойство заливки</param>
        public void ChangeProperty(string keywords, int index, LineProperty oldLineProperty,
            LineProperty newLineProperty, FillProperty oldFillProperty, FillProperty newFillProperty)
        {
            switch (keywords)
            {
                case "Изменить свойство линии": ((ILinePropertyble)_drawablesList[index]).LineProperty = newLineProperty; break;
                case "Изменить свойство заливки":((IFillPropertyble)_drawablesList[index]).FillProperty = newFillProperty; break;
                case "Вернуть свойство линии": ((ILinePropertyble)_drawablesList[index]).LineProperty = oldLineProperty;break;
                case "Вернуть свойство заливки": ((IFillPropertyble)_drawablesList[index]).FillProperty = oldFillProperty;break;
            }
            Console.WriteLine(keywords);
        }
        /// <summary>
        /// Команда по очистке списка
        /// </summary>
        /// <param name="keywords">Команда</param>
        /// <param name="drawables">Список удаляемых фигур</param>
        /// <param name="indexes">Список индексов, свои места для фигур</param>
        public void DrawablesCleaer(string keywords, List<IDrawable> drawables, List<int> indexes)
        {
            switch (keywords)
            {
                case "Удалить весь список":
                    _drawablesList.Clear();
                    break;
                case "Удалить элементы":
                {
                    foreach (IDrawable drawable in drawables)
                    {
                        _drawablesList.Remove(drawable);
                    }

                    break;
                }
                case "Удалить элемент":
                    _drawablesList.Remove(drawables[0]);
                    break;
                case "Восстановить весь список":
                    _drawablesList = drawables;
                    break;
                case "Восстановить элементы списка":
                    _drawablesList = Reload(drawables, indexes);
                    break;
                case "Восстановить элемент списка":
                    _drawablesList = Reload(drawables, indexes);
                    break;
            }

            Console.WriteLine(keywords);
        }
        /// <summary>
        /// Перезарядить список фигур
        /// </summary>
        /// <param name="drawables">Список добавляемых фигур</param>
        /// <param name="indexes">Список индексов, свои места для фигур</param>
        /// <returns></returns>
        private List<IDrawable> Reload(List<IDrawable> drawables, List<int> indexes)
        {
            List<IDrawable> localDrawables = new List<IDrawable>();
            int count = 0;
            for (int i = 0; i < _drawablesList.Count - 1 + drawables.Count - 1; i++)
            {
                if (indexes.Contains(i))
                {
                    localDrawables.Add(drawables[count]);
                    count++;
                }
                else
                {
                    localDrawables.Add(_drawablesList[i]);
                }
            }
            return drawables;
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
