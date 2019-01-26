using System;
using System.Collections.Generic;
using System.Drawing;
using GRPO.Drawing.Interface;

namespace GRPO.Commands
{
    /// <summary>
    /// Класс команды реконструирования фигуры
    /// </summary>
    [Serializable]
    internal class Reconstruction : BaseCommand
    {
        /// <summary>
        /// Иницализация класса
        /// </summary>
        /// <param name="graphicsEditor">Объект хранения реализации команд</param>
        /// <param name="keywords">Команда</param>
        /// <param name="drawables">Список реконструируемых фигур</param>
        /// <param name="indexes">Список индексов фигур</param>
        /// <param name="pointIndex">Индекс изменяемой опорной точки фигуры</param>
        /// <param name="selectPoint">Выбранная точка</param>
        /// <param name="newPoint">Новая точка</param>
        public Reconstruction(GraphicsEditor graphicsEditor,
            string keywords, List<IDrawable> drawables, List<int> indexes, int pointIndex, Point selectPoint,
            Point newPoint)
        {
            GraphicsEditor = graphicsEditor;
            Keywords = keywords;
            Drawables = new List<IDrawable>();
            foreach (var drawable in drawables)
            {
                Drawables.Add(drawable.Clone());
            }

            PointIndex = pointIndex;
            Indexes = indexes;
            SelectPoint = new Point(selectPoint.X, selectPoint.Y);
            NewPoint = new Point(newPoint.X, newPoint.Y);
        }

        /// <summary>
        /// Объект хранения реализации команд
        /// </summary>
        private GraphicsEditor GraphicsEditor { get; set; }

        /// <summary>
        /// Команда
        /// </summary>
        private string Keywords { get; set; }

        /// <summary>
        /// Список реконструируемых фигур
        /// </summary>
        private List<IDrawable> Drawables { get; set; }

        /// <summary>
        /// Список индексов фигур
        /// </summary>
        private List<int> Indexes { get; set; }

        /// <summary>
        /// Индекс изменяемой опорной точки фигуры
        /// </summary>
        private int PointIndex { get; set; }

        /// <summary>
        /// Выбранная точка
        /// </summary>
        private Point SelectPoint { get; set; }

        /// <summary>
        /// Новая точка
        /// </summary>
        private Point NewPoint { get; set; }

        /// <summary>
        /// Исполнить комнаду
        /// </summary>
        public override void Execute()
        {
            GraphicsEditor.Reconstruct(Keywords, Drawables, Indexes, PointIndex, SelectPoint, NewPoint);
        }

        /// <summary>
        /// Исполнить команду наоборот
        /// </summary>
        public override void UnExecute()
        {
            GraphicsEditor.Reconstruct(Undo(), Drawables, Indexes, PointIndex, SelectPoint, NewPoint);
        }

        /// <summary>
        /// Выдать обратную команду
        /// </summary>
        /// <returns>Обратная команда</returns>
        private string Undo()
        {
            switch (Keywords)
            {
                case "Изменить положение фигур(ы)":
                    return "Вернуть положение фигур(ы)";
                case "Изменить опорную точку":
                    return "Вернуть назад опорную точку";
                case "Добавить фигуру(ы)":
                    return "Убрать фигуру(ы)";
            }

            return null;
        }
    }
}