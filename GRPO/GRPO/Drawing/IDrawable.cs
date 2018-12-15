using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace GRPO
{
    interface IDrawable
    {
        /// <summary>
        /// Нарисовать объект
        /// </summary>
        void Draw();
        /// <summary>
        /// Взять список точек
        /// </summary>
        /// <returns>Списко точек формирующих фигуру</returns>
        List<Point> GetPoints();
    }
}
