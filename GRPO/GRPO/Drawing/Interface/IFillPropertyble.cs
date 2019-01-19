using GRPO.Drawing.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPO
{
    interface IFillPropertyble
    {
        /// <summary>
        /// Свойство заливки
        /// </summary>
        FillProperty FillProperty { get; set; }
    }
}
