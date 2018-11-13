using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPO
{
    interface IDraw
    {
        /// <summary>
        /// Нарисовать объект
        /// </summary>
        void Draw();
        /// <summary>
        /// Очистить место
        /// </summary>
        void Clear();
    }
}
