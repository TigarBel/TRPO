using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GRPO
{
    public class FillProperty
    {
        /// <summary>
        /// Цвет заливки
        /// </summary>
        private Color _fillColor;
        /// <summary>
        /// Пустой класс Расширения
        /// </summary>
        public FillProperty()
        {
            FillColor = Color.White;
        }
        /// <summary>
        /// Класс Расширения
        /// </summary>
        /// <param name="lineThickness">Толщина линии</param>
        /// <param name="lineColor">Цвет линии</param>
        /// <param name="fillColor">Цвет заливки</param>
        /// <param name="lineType">Тип линии</param>
        public FillProperty(Color fillColor)
        {
            FillColor = fillColor;
        }
        /// <summary>
        /// Цвет заливки
        /// </summary>
        public Color FillColor
        {
            get
            {
                return _fillColor;
            }
            set
            {
                _fillColor = value;
            }
        }
    }
}
