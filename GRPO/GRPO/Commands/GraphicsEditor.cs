using GRPO.Drawing;
using GRPO.Drawing.Interface;
using GRPO.Drawing.Property;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRPO.InteractionFrame;

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
            "Создать фигуру" /*0*/, "Удалить фигуру" /*1*/, "Изменить свойство линии" /*2*/,
            "Изменить свойство заливки" /*3*/,
            "Удалить весь список" /*4*/, "Удалить элемент(ы)" /*5*/, "Изменить положение фигур(ы)" /*6*/,
            "Изменить опорную точку" /*7*/,
            "Добавить фигуру(ы)" /*8*/
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
                case "Изменить свойство заливки": ((IFillPropertyble)_drawablesList[index]).FillProperty = newFillProperty; break;
                case "Вернуть свойство линии": ((ILinePropertyble)_drawablesList[index]).LineProperty = oldLineProperty; break;
                case "Вернуть свойство заливки": ((IFillPropertyble)_drawablesList[index]).FillProperty = oldFillProperty; break;
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
                case "Удалить элемент(ы)":
                    {
                        indexes.Reverse();
                        foreach (int index in indexes)
                        {
                            _drawablesList.RemoveAt(index);
                        }

                        indexes.Reverse();
                        break;
                    }
                case "Восстановить весь список":
                    _drawablesList = drawables;
                    break;
                case "Восстановить элемент(ы) списка":
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
            for (int i = 0; i < _drawablesList.Count + drawables.Count; i++)
            {
                if (indexes.Contains(i))
                {
                    localDrawables.Add(drawables[count]);
                    count++;
                }
                else
                {
                    localDrawables.Add(_drawablesList[i - count]);
                }
            }
            return localDrawables;
        }
        /// <summary>
        /// Реконструировать фигуру
        /// </summary>
        /// <param name="keywords">Команда</param>
        /// <param name="indexes">Список индексов фигур(ы)</param>
        /// <param name="selectPoint">Выбранная точка</param>
        /// <param name="newPoint">Новая точка</param>
        public void Reconstruct(string keywords, List<IDrawable> drawables, Point selectPoint, Point newPoint)
        {
            switch (keywords)
            {
                case "Изменить положение фигур(ы)":
                    ChangePosition(drawables,selectPoint,newPoint);
                    break;
                case "Изменить опорную точку":
                    ChangePoint(drawables, selectPoint, newPoint);
                    break;
                case "Добавить фигуру(ы)":
                    foreach (IDrawable drawable in drawables)
                    {
                        _drawablesList.Add(drawable);
                    }
                    break;
                case "Вернуть положение фигур(ы)":
                    ChangePosition(drawables, newPoint, selectPoint);
                    break;
                case "Вернуть назад опорную точку":
                    ChangePoint(drawables, newPoint, selectPoint);
                    break;
                case "Убрать фигуру(ы)":
                    foreach (IDrawable drawable in drawables)
                    {
                        _drawablesList.Remove(drawable);
                    }
                    break;
            }

            Console.WriteLine(keywords);
        }

        private void ChangePosition(List<IDrawable> drawables, Point selectPoint, Point newPoint)
        {
            foreach (IDrawable drawable in drawables)
            {
                drawable.Position = new Point(
                    drawable.Position.X - selectPoint.X + newPoint.X,
                    drawable.Position.Y - selectPoint.Y + newPoint.Y);
            }
        }
        /// <summary>
        /// Изменить опорную точку
        /// </summary>
        /// <param name="drawables">Список с одной фигурой, которой меняем опорную точку</param>
        /// <param name="selectPoint">Выбранная точка</param>
        /// <param name="newPoint">Новая точка</param>
        private void ChangePoint(List<IDrawable> drawables, Point selectPoint, Point newPoint)
        {
            Checking checking = new Checking();
            int indexSelectPoint = checking.GetNumberPoint(selectPoint, drawables[0], 4);
            if (indexSelectPoint != -1)
            {
                List<Point> points = drawables[0].Points;
                points[indexSelectPoint] = newPoint;
                drawables[0].Points = points;
            }
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
        public List<string> Keywords
        {
            get { return _keywords; }
        }
    }
}
