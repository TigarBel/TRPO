using GRPO.Drawing;
using GRPO.Drawing.Property;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GRPO.InteractionFrame.PointInteractions
{
    public class PointInteraction
    {
        private DrawFigureCircle _circle;

        public PointInteraction(Point position, int radius)
        {
            DrawCircle = new DrawFigureCircle(new Point(position.X, position.Y),
                new Point(position.X + radius, position.Y),
                new LineProperty(), new FillProperty());
        }

        public DrawFigureCircle DrawCircle
        {
            get { return _circle; }
            set { _circle = value; }
        }

        public Point Position
        {
            get { return DrawCircle.Position; }
            set { DrawCircle.Position = value; }
        }

        public int Radius
        {
            get { return DrawCircle.Circle.Radius; }
            set { DrawCircle.Circle.Radius = value; }
        }

        public void Draw(PictureBox canvas)
        {
            DrawCircle.Draw(canvas);
        }

        public bool GetInto(Point point)
        {
            if (DrawCircle.Position.X <= point.X && DrawCircle.Position.X + DrawCircle.Width >= point.X &&
                DrawCircle.Position.Y <= point.Y && DrawCircle.Position.Y + DrawCircle.Height >= point.Y)
                return true;
            return false;
        }
    }
}
