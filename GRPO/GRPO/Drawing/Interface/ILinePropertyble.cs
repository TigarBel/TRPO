using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPO
{
    interface ILinePropertyble
    {
        /// <summary>
        /// Свойство линии
        /// </summary>
        LineProperty LineProperty { get; set; }
    }
}
