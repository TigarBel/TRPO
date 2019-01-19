using GRPO.Drawing.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPO.Drawing.Interface
{
    /// <summary>
    /// Интерфей для хранения свойства линии фигуры
    /// </summary>
    interface ILinePropertyble
    {
        /// <summary>
        /// Свойство линии
        /// </summary>
        LineProperty LineProperty { get; set; }
    }
}
