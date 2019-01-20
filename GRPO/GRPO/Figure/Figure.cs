using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPO.Figure
{
    /// <summary>
    /// Класс фигуры
    /// </summary>
    [Serializable]
    abstract class Figure
    {
        /// <summary>
        /// Пустой класс фигуры
        /// </summary>
        protected Figure()
        {
            X = 0;
            Y = 0;
            Width = 0;
            Height = 0;
        }
        /// <summary>
        /// Класс фигуры
        /// </summary>
        /// <param name="x">Позиция фигуры по координате X</param>
        /// <param name="y">Позиция фигуры по координате Y</param>
        /// <param name="width">Ширина фигуры</param>
        /// <param name="height">Высота фигуры</param>
        protected Figure(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
        /// <summary>
        /// Позиция фигуры по координате X
        /// </summary>
        protected int X { get; set; }
        /// <summary>
        /// Позиция фигуры по координате Y
        /// </summary>
        protected int Y { get; set; }
        /// <summary>
        /// Ширина фигуры
        /// </summary>
        protected int Width { get; set; }
        /// <summary>
        /// Высота фигуры
        /// </summary>
        protected int Height { get; set; }
    }
}
