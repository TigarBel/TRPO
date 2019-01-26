using System;

namespace GRPO.Commands
{
    /// <summary>
    /// Класс команды
    /// </summary>
    [Serializable]
    abstract internal class BaseCommand
    {
        /// <summary>
        /// Использовать команду
        /// </summary>
        public abstract void Execute();

        /// <summary>
        /// Отменить команду
        /// </summary>
        public abstract void UnExecute();
    }
}
