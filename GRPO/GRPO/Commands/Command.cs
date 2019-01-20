using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPO.Commands
{
    /// <summary>
    /// Класс команды
    /// </summary>
    [Serializable]
    abstract class Command
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
