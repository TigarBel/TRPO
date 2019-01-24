using GRPO.Drawing.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPO.Commands
{
    /// <summary>
    /// Класс команд отвечающих за очистку холста
    /// </summary>
    [Serializable]
    class CommandClear : Command
    {
        /// <summary>
        /// Инициализация класса
        /// </summary>
        /// <param name="graphicsEditor">Объект хранения реализации команд</param>
        /// <param name="keywords">Команда</param>
        /// <param name="drawables">Список удаляемых фигур</param>
        /// <param name="indexes">Список индексов удаляемых фигур</param>
        public CommandClear(GraphicsEditor graphicsEditor, string keywords, List<IDrawable> drawables, List<int> indexes)
        {
            GraphicsEditor = graphicsEditor;
            Keywords = keywords;
            Drawables = new List<IDrawable>();
            foreach (var drawable in drawables)
            {
                Drawables.Add(drawable.Clone());
            }
            Indexes = indexes;
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
        /// Список удаляемых фигур
        /// </summary>
        private List<IDrawable> Drawables  { get; set; }
        /// <summary>
        /// Список индексов удаляемых фигур
        /// </summary>
        private List<int> Indexes { get; set; }
        /// <summary>
        /// Исполнить комнаду
        /// </summary>
        public override void Execute()
        {
            GraphicsEditor.DrawablesCleaer(Keywords, Drawables, Indexes);
        }
        /// <summary>
        /// Исполнить команду наоборот
        /// </summary>
        public override void UnExecute()
        {
            GraphicsEditor.DrawablesCleaer(Undo(), Drawables, Indexes);
        }

        /// <summary>
        /// Выдать обратную команду
        /// </summary>
        /// <returns>Обратная команда</returns>
        private string Undo()
        {
            switch (Keywords)
            {
                case "Удалить весь список": return "Восстановить весь список";
                case "Удалить элемент(ы)": return "Восстановить элемент(ы) списка";
            }
            return null;
        }
    }
}
