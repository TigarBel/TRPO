using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace GRPO
{
    public interface IDrawable
    {
        /// <summary>
        /// Позиция фигуры
        /// </summary>
        Point Position { get; set; }
        /// <summary>
        /// Ширина фигуры
        /// </summary>
        int Width { get; set; }
        /// <summary>
        /// Высота фигуры
        /// </summary>
        int Height { get; set; }
        /// <summary>
        /// Нарисовать объект
        /// </summary>
        void Draw();
        /// <summary>
        /// Взять список точек
        /// </summary>
        /// <returns>Списко точек формирующих фигуру</returns>
        List<Point> GetPoints();
        /// <summary>
        /// Клонировать объект
        /// </summary>
        /// <returns>Новая копия объекта</returns>
        IDrawable Clone();
    }
}
