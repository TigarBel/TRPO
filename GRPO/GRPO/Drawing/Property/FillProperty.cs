using System;
using System.Drawing;

namespace GRPO.Drawing.Property
{
    /// <summary>
    /// Класс свойства заливки фигуры
    /// </summary>
    [Serializable]
    public class FillProperty
    {
        /// <summary>
        /// Пустой класс свойства заливки фигуры
        /// </summary>
        public FillProperty()
        {
            FillColor = Color.White;
        }

        /// <summary>
        /// Конструктор класс свойства заливки фигуры
        /// </summary>
        /// <param name="fillColor">Цвет заливки</param>
        public FillProperty(Color fillColor)
        {
            FillColor = fillColor;
        }

        /// <summary>
        /// Цвет заливки
        /// </summary>
        public Color FillColor { get; set; }
    }
}
