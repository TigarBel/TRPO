using System.Collections.Generic;
using GRPO.Drawing.Interface;

namespace GRPO.InteractionFrame.PointInteractions
{
    /// <summary>
    /// Класс интерактивных точек
    /// </summary>
    public class InteractionPoints
    {
        /// <summary>
        /// Конструктор класса интерактивных точек
        /// </summary>
        /// <param name="drawables">Список фигур</param>
        /// <param name="radiusPoints">Радиус точек</param>
        public InteractionPoints(List<IDrawable> drawables, int radiusPoints)
        {
            UpPointInteraction = new UpPointInteraction(drawables, radiusPoints);
            RightPointInteraction = new RightPointInteraction(drawables, radiusPoints);
            DownPointInteraction = new DownPointInteraction(drawables, radiusPoints);
            LeftPointInteraction = new LeftPointInteraction(drawables, radiusPoints);
        }

        /// <summary>
        /// Верхняя интерактивная точка
        /// </summary>
        public UpPointInteraction UpPointInteraction { get; set; }

        /// <summary>
        /// Правая интерактивная точка
        /// </summary>
        public RightPointInteraction RightPointInteraction { get; set; }

        /// <summary>
        /// Нижняя интерактивная точка
        /// </summary>
        public DownPointInteraction DownPointInteraction { get; set; }

        /// <summary>
        /// Левая интерактивная точка
        /// </summary>
        public LeftPointInteraction LeftPointInteraction { get; set; }
    }
}