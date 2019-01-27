using GRPO.Drawing;
using GRPO.Drawing.Property;
using System.Drawing;
using System.Windows.Forms;

namespace GRPO.InteractionFrame.PointInteractions
{
    /// <summary>
    /// Класс интерактивной точки
    /// </summary>
    public class PointInteraction
    {
        /// <summary>
        /// Конструктор класса интерактивной точки
        /// </summary>
        /// <param name="position">Позиция</param>
        /// <param name="radius">Радиус</param>
        public PointInteraction(Point position, int radius)
        {
            DrawCircle = new DrawFigureCircle(new Point(position.X, position.Y),
                new Point(position.X + radius, position.Y),
                new LineProperty(), new FillProperty());
        }

        /// <summary>
        /// Рисуемый объект круга
        /// </summary>
        public DrawFigureCircle DrawCircle { get; set; }

        /// <summary>
        /// Позиция рисуемого круга
        /// </summary>
        public Point Position
        {
            get { return DrawCircle.Position; }
            set { DrawCircle.Position = value; }
        }

        /// <summary>
        /// Радиус рисуемого круга
        /// </summary>
        public int Radius
        {
            get { return DrawCircle.Circle.Radius; }
            set { DrawCircle.Circle.Radius = value; }
        }

        /// <summary>
        /// Нарисовать круг
        /// </summary>
        /// <param name="canvas">Холст на котором рисуют</param>
        public void Draw(PictureBox canvas)
        {
            DrawCircle.Draw(canvas);
        }
    }
}
