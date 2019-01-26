using System.Collections.Generic;
using GRPO.Drawing.Interface;

namespace GRPO.InteractionFrame.PointInteractions
{
    public class InteractionPoints
    {
        public InteractionPoints(List<IDrawable> drawables, int radiusPoints)
        {
            UpPointInteraction = new UpPointInteraction(drawables, radiusPoints);
            RightPointInteraction = new RightPointInteraction(drawables, radiusPoints);
            DownPointInteraction = new DownPointInteraction(drawables, radiusPoints);
            LeftPointInteraction = new LeftPointInteraction(drawables, radiusPoints);
        }

        public UpPointInteraction UpPointInteraction { get; set; }
        public RightPointInteraction RightPointInteraction { get; set; }
        public DownPointInteraction DownPointInteraction { get; set; }
        public LeftPointInteraction LeftPointInteraction { get; set; }
    }
}
