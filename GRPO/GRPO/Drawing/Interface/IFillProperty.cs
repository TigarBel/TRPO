using GRPO.Drawing.Property;

namespace GRPO.Drawing.Interface
{
    /// <summary>
    /// Интерфейс для хранения свойство заливки фигуры
    /// </summary>
    public interface IFillProperty
    {
        /// <summary>
        /// Свойство заливки
        /// </summary>
        FillProperty FillProperty { get; set; }
    }
}
