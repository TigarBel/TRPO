using GRPO.Drawing.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPO.Drawing.Interface
{
    /// <summary>
    /// Интерфейс для хранения свойство заливки фигуры
    /// </summary>
    interface IFillPropertyble
    {
        /// <summary>
        /// Свойство заливки
        /// </summary>
        FillProperty FillProperty { get; set; }
    }
}
