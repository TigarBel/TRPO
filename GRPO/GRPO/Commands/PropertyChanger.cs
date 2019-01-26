using GRPO.Drawing.Property;
using System;

namespace GRPO.Commands
{
    /// <summary>
    /// Класс команды измения свойства фигуры
    /// </summary>
    [Serializable]
    internal class PropertyChanger : BaseCommand
    {
        /// <summary>
        /// Инициализация класса
        /// </summary>
        /// <param name="graphicsEditor">Объект хранения реализации команд</param>
        /// <param name="keywords">Команда</param>
        /// <param name="index">Индекс фигуры</param>
        /// <param name="oldLineProperty">Старое свойство линии фигуры</param>
        /// <param name="newLineProperty">Новое свойство линии фигуры</param>
        /// <param name="oldFillProperty">Старое свойство заливки фигуры</param>
        /// <param name="newFillProperty">Новое свойство заливки фигуры</param>
        public PropertyChanger(GraphicsEditor graphicsEditor, string keywords, int index, LineProperty oldLineProperty,
            LineProperty newLineProperty, FillProperty oldFillProperty, FillProperty newFillProperty)
        {
            GraphicsEditor = graphicsEditor;
            Keywords = keywords;
            Index = index;
            if (oldLineProperty != null && newLineProperty != null)
            {
                OldLineProperty = new LineProperty(oldLineProperty.LineThickness, oldLineProperty.LineColor,
                    oldLineProperty.LineType);
                NewLineProperty = new LineProperty(newLineProperty.LineThickness, newLineProperty.LineColor,
                    newLineProperty.LineType);
            }

            if (oldFillProperty != null && newFillProperty != null)
            {
                OldFillProperty = new FillProperty(oldFillProperty.FillColor);
                NewFillProperty = new FillProperty(newFillProperty.FillColor);
            }
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
        /// Индекс фигуры
        /// </summary>
        private int Index { get; set; }

        /// <summary>
        /// Старое свойство линии фигуры
        /// </summary>
        private LineProperty OldLineProperty { get; set; }

        /// <summary>
        /// Новое свойство линии фигуры
        /// </summary>
        private LineProperty NewLineProperty { get; set; }

        /// <summary>
        /// Старое свойство заливки фигуры
        /// </summary>
        private FillProperty OldFillProperty { get; set; }

        /// <summary>
        /// Новое свойство заливки фигуры
        /// </summary>
        private FillProperty NewFillProperty { get; set; }

        /// <summary>
        /// Исполнить комнаду
        /// </summary>
        public override void Execute()
        {
            GraphicsEditor.ChangeProperty(Keywords, Index, OldLineProperty, NewLineProperty, OldFillProperty,
                NewFillProperty);
        }

        /// <summary>
        /// Исполнить команду наоборот
        /// </summary>
        public override void UnExecute()
        {
            GraphicsEditor.ChangeProperty(Undo(), Index, OldLineProperty, NewLineProperty, OldFillProperty,
                NewFillProperty);
        }

        /// <summary>
        /// Выдать обратную команду
        /// </summary>
        /// <returns>Обратная команда</returns>
        private string Undo()
        {
            switch (Keywords)
            {
                case "Изменить свойство линии":
                    return "Вернуть свойство линии";
                case "Изменить свойство заливки":
                    return "Вернуть свойство заливки";
            }

            return null;
        }
    }
}