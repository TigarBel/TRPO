using GRPO.Drawing.Property;
using System;
using System.Collections.Generic;
using System.Drawing;
using GRPO.Drawing;

namespace GRPO.Commands
{
    /// <summary>
    /// Класс команды создания фигуры
    /// </summary>
    [Serializable]
    internal class Drawing : BaseCommand
    {
        /// <summary>
        /// Инициализация класса
        /// </summary>
        /// <param name="graphicsEditor">Объект хранения реализации команд</param>
        /// <param name="keywords">Команда</param>
        /// <param name="tools">Инструмент рисования</param>
        /// <param name="points">Список точек образующих фигуру</param>
        /// <param name="lineProperty">Свойство линии фигуры</param>
        /// <param name="fillProperty">Свойство заливки фигуры</param>
        public Drawing(GraphicsEditor graphicsEditor,
            string keywords, Tools tools, List<Point> points, LineProperty lineProperty, FillProperty fillProperty)
        {
            GraphicsEditor = graphicsEditor;
            Keywords = keywords;
            Tools = tools;
            Points = points;
            LineProperty = lineProperty;
            FillProperty = fillProperty;
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
        /// Инструмент рисования
        /// </summary>
        private Tools Tools { get; set; }

        /// <summary>
        /// Список точек образующих фигуру
        /// </summary>
        private List<Point> Points { get; set; }

        /// <summary>
        /// Свойство линии фигуры
        /// </summary>
        private LineProperty LineProperty { get; set; }

        /// <summary>
        /// Свойство заливки фигуры
        /// </summary>
        private FillProperty FillProperty { get; set; }

        /// <summary>
        /// Исполнить комнаду
        /// </summary>
        public override void Execute()
        {
            GraphicsEditor.CreateFigure(Keywords, Tools, Points, LineProperty, FillProperty);
        }

        /// <summary>
        /// Исполнить команду наоборот
        /// </summary>
        public override void UnExecute()
        {
            GraphicsEditor.CreateFigure(Undo(), Tools, Points, LineProperty, FillProperty);
        }

        /// <summary>
        /// Выдать обратную команду
        /// </summary>
        /// <returns>Обратная команда</returns>
        private string Undo()
        {
            return "Удалить фигуру";
        }
    }
}
