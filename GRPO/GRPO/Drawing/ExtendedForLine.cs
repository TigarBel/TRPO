using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace GRPO
{

    class ExtendedForLine
    {
        /// <summary>
        /// Толщина линии
        /// </summary>
        private float _lineThickness;
        /// <summary>
        /// Цвет линии
        /// </summary>
        private Color _lineColor;
        /// <summary>
        /// Тип линии
        /// </summary>
        private DashStyle _lineType;
        /// <summary>
        /// Пустой класс Расширения
        /// </summary>
        public ExtendedForLine()
        {
            LineThickness = 0;
            LineColor = new Color();
            LineType = DashStyle.Solid;
        }
        /// <summary>
        /// Класс Расширения
        /// </summary>
        /// <param name="lineThickness">Толщина линии</param>
        /// <param name="lineColor">Цвет линии</param>
        /// <param name="fillColor">Цвет заливки</param>
        /// <param name="lineType">Тип линии</param>
        public ExtendedForLine(int lineThickness, Color lineColor, DashStyle lineType)
        {
            LineThickness = lineThickness;
            LineColor = lineColor;
            LineType = lineType;
        }
        /// <summary>
        /// Толщина линии
        /// </summary>
        public float LineThickness
        {
            get
            {
                return _lineThickness;
            }
            set
            {
                if (value > 0)
                {
                    _lineThickness = value;
                }
                else
                {
                    throw new ArgumentException("Толщина линии не может быть меньше 1!");
                }
            }
        }
        /// <summary>
        /// Цвет линии
        /// </summary>
        public Color LineColor
        {
            get
            {
                return _lineColor;
            }
            set
            {
                _lineColor = value;
            }
        }
        /// <summary>
        /// Тип линии
        /// </summary>
        public DashStyle LineType
        {
            get
            {
                return _lineType;
            }
            set
            {
                _lineType = value;
            }
        }
    }
}
