using GRPO.Drawing.Property;

namespace GRPO.Drawing.Interface
{
    /// <summary>
    /// Интерфей для хранения свойства линии фигуры
    /// </summary>
    public interface ILineProperty
    {
        /// <summary>
        /// Свойство линии
        /// </summary>
        LineProperty LineProperty { get; set; }
    }
}
