using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPO
{
    class Figure
    {
        /// <summary>
        /// Позиция фигуры по координате X
        /// </summary>
        private int _xPosition;
        /// <summary>
        /// Позиция фигуры по координате Y
        /// </summary>
        private int _yPosition;
        /// <summary>
        /// Ширина фигуры
        /// </summary>
        private int _width;
        /// <summary>
        /// Высота фигуры
        /// </summary>
        private int _height;
        /// <summary>
        /// Пустой класс фигуры
        /// </summary>
        public Figure()
        {
            X = 0;
            Y = 0;
            Width = 0;
            Height = 0;
        }
        /// <summary>
        /// Позиция фигуры по координате X
        /// </summary>
        public int X
        {
            get
            {
                return _xPosition;
            }
            set
            {
                _xPosition = value;
            }
        }
        /// <summary>
        /// Позиция фигуры по координате Y
        /// </summary>
        public int Y
        {
            get
            {
                return _yPosition;
            }
            set
            {
                _yPosition = value;
            }
        }
        /// <summary>
        /// Ширина фигуры
        /// </summary>
        public int Width
        {
            get
            {
                return _width;
            }
            set
            {
                if (value >= 0)
                {
                    _width = value;
                }
                else
                {
                    throw new ArgumentException("Ширина фигуры не может быть меньше нуля");
                }
            }
        }
        /// <summary>
        /// Высота фигуры
        /// </summary>
        public int Height
        {
            get
            {
                return _height;
            }
            set
            {
                if (value >= 0)
                {
                    _height = value;
                }
                else
                {
                    throw new ArgumentException("Высота фигуры не может быть меньше нуля");
                }
            }
        }
    }
}
