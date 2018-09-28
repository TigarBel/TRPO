using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GRPO
{
    public class Pixel
    {
        /// <summary>
        /// Точка расположения пикселя
        /// </summary>
        private Point _point;
        /// <summary>
        /// Цвет пикселя
        /// </summary>
        private Color _color;
        /// <summary>
        /// Точка на экране
        /// </summary>
        public Pixel()
        {
            _point = new Point();
            _color = new Color();
        }
        /// <summary>
        /// Точка на экране
        /// </summary>
        /// <param name="point">Точка расположения пикселя</param>
        /// <param name="color">Цвет пикселя</param>
        public Pixel(Point point, Color color)
        {
            _point = point;
            _color = color;
        }
        /// <summary>
        /// Точка на экране
        /// </summary>
        /// <param name="x">Расположение по X</param>
        /// <param name="y">Расположение по Y</param>
        /// <param name="color">Цвет пикселя</param>
        public Pixel(int x, int y, Color color)
        {
            _point = new Point(x,y);
            _color = color;
        }
        /// <summary>
        /// Точка расположения пикселя
        /// </summary>
        public Point Point
        {
            get
            {
                return _point;
            }
            set
            {
                _point = value;
            }
        }
        /// <summary>
        /// Цвет пикселя
        /// </summary>
        public Color Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
            }
        }
    }
}
