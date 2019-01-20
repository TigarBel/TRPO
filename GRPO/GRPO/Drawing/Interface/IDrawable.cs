using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace GRPO.Drawing.Interface
{
    /// <summary>
    /// Интерфейс для отрисовки фигур
    /// </summary>
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
        void Draw(PictureBox pictureBox);
        /// <summary>
        /// Взять список точек
        /// </summary>
        /// <returns>Списко точек формирующих фигуру</returns>
        List<Point> Points { get; set; }
        /// <summary>
        /// Клонировать объект
        /// </summary>
        /// <returns>Новая копия объекта</returns>
        IDrawable Clone();
    }
}
