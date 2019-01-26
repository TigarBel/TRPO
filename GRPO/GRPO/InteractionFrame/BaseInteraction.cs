using GRPO.Drawing;
using GRPO.Drawing.Interface;
using GRPO.Drawing.Property;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace GRPO.InteractionFrame
{
    /// <summary>
    /// Класс отрисовки рамок для класса интерактив
    /// </summary>
    [Serializable]
    public abstract class BaseInteraction
    {
        /// <summary>
        /// Рисуемые объекты
        /// </summary>
        private List<IDrawable> _drawables = new List<IDrawable>();

        /// <summary>
        /// Радиус точек отображения габаритов
        /// </summary>
        protected int _radiusDrawPoint = 4;

        /// <summary>
        /// Разрашение изменять опорные точки
        /// </summary>
        protected bool EnablePointsForDraw { get; set; }

        /// <summary>
        /// Рисуемые объекты
        /// </summary>
        protected List<IDrawable> Drawables
        {
            get { return _drawables; }
            set { _drawables = value; }
        }

        //public InteractionPoints InteractionPoints { get; set; }

        /// <summary>
        /// Отрисовка выделения
        /// </summary>
        /// <param name="pictureBox">Холст на котором рисуют</param>
        public void DrawSelcet(PictureBox pictureBox)
        {
            if (EnablePointsForDraw)
            {
                if (Drawables.Count == 1)
                {
                    DrawPoints(pictureBox, Drawables);
                }
                else
                {
                    throw new ArgumentException(
                        "Режим изменения опорных точек разрешен только при выделении одной фигуры!");
                }
            }
            else
            {
                if (Drawables.Count > 0)
                {
                    DrawInteraction(pictureBox, Drawables);
                }
            }
        }

        /// <summary>
        /// Отрисовка интерактивного квадрата без опорных точек
        /// </summary>
        private void DrawInteraction(PictureBox pictureBox, List<IDrawable> drawableFigures)
        {
            List<Point> points = new List<Point>();
            foreach (IDrawable drawable in drawableFigures)
            {
                foreach (Point point in drawable.Points)
                {
                    points.Add(point);
                }
            }

            int minX = points.Min(point => point.X);
            int maxX = points.Max(point => point.X);
            int minY = points.Min(point => point.Y);
            int maxY = points.Max(point => point.Y);

            points.Clear();
            Point pointA = new Point(minX, minY);
            Point pointB = new Point(maxX, maxY);

            DrawSquare(pictureBox, pointA, pointB);
            DrawPointsSize(pictureBox, pointA, pointB);
        }

        /// <summary>
        /// Отрисовка квадрата границ объекта
        /// </summary>
        /// <param name="index">Номер фигуры из списка фигур</param>
        private void DrawSquare(PictureBox pictureBox, Point pointA, Point pointB)
        {
            DrawFigureRectangle drawFigure = new DrawFigureRectangle(pointA, pointB,
                new LineProperty(1, Color.Black, DashStyle.Dash),
                new FillProperty(Color.Transparent));
            drawFigure.Draw(pictureBox);
        }

        /// <summary>
        /// Точки размера объекта
        /// </summary>
        /// <param name="index">Номер фигуры из списка фигур</param>
        private void DrawPointsSize(PictureBox pictureBox, Point pointA, Point pointB)
        {
            /*InteractionPoints.UpPointInteraction.PointInteraction.Draw(pictureBox);
            InteractionPoints.RightPointInteraction.PointInteraction.Draw(pictureBox);
            InteractionPoints.DownPointInteraction.PointInteraction.Draw(pictureBox);
            InteractionPoints.LeftPointInteraction.PointInteraction.Draw(pictureBox);*/
            DrawFigureCircle drawFigure = new DrawFigureCircle(
                new Point(pointA.X + (pointB.X - pointA.X) / 2, pointA.Y),
                new Point(pointA.X + (pointB.X - pointA.X) / 2 + _radiusDrawPoint,
                    pointA.Y),
                new LineProperty(), new FillProperty());
            drawFigure.Draw(pictureBox);
            drawFigure = new DrawFigureCircle(new Point(pointB.X, pointA.Y + (pointB.Y - pointA.Y) / 2),
                new Point(pointB.X + _radiusDrawPoint, pointA.Y + (pointB.Y - pointA.Y) / 2),
                new LineProperty(), new FillProperty());
            drawFigure.Draw(pictureBox);
            drawFigure = new DrawFigureCircle(new Point(pointA.X + (pointB.X - pointA.X) / 2, pointB.Y),
                new Point(pointA.X + (pointB.X - pointA.X) / 2 + _radiusDrawPoint, pointB.Y),
                new LineProperty(), new FillProperty());
            drawFigure.Draw(pictureBox);
            drawFigure = new DrawFigureCircle(new Point(pointA.X, pointA.Y + (pointB.Y - pointA.Y) / 2),
                new Point(pointA.X + _radiusDrawPoint, pointA.Y + (pointB.Y - pointA.Y) / 2),
                new LineProperty(), new FillProperty());
            drawFigure.Draw(pictureBox);
        }

        /// <summary>
        /// Отрисовка угловых точек
        /// </summary>
        /// <param name="index">Номер фигуры из списка фигур</param>
        public void DrawPointsSquare(int index, PictureBox pictureBox, List<IDrawable> drawableFigures)
        {
            List<Point> points = GetBorderPoints(index, drawableFigures);
            for (int i = 0; i < 4; i++)
            {
                DrawFigureCircle drawFigure = new DrawFigureCircle(points[i],
                    new Point(points[i].X + _radiusDrawPoint, points[i].Y + _radiusDrawPoint),
                    new LineProperty(), new FillProperty());
                drawFigure.Draw(pictureBox);
            }
        }

        /// <summary>
        /// Функция возвращающая список угловыч точек
        /// </summary>
        /// <param name="index">Номер фигуры из списка фигур</param>
        /// <returns>Угловые точки</returns>
        private List<Point> GetBorderPoints(int index, List<IDrawable> drawableFigures)
        {
            int minX = drawableFigures[index].Points.Min(point => point.X);
            int maxX = drawableFigures[index].Points.Max(point => point.X);
            int minY = drawableFigures[index].Points.Min(point => point.Y);
            int maxY = drawableFigures[index].Points.Max(point => point.Y);
            return new List<Point>()
                {new Point(minX, minY), new Point(maxX, minY), new Point(maxX, maxY), new Point(minX, maxY)};
        }

        /// <summary>
        /// Отрисовка опорных точек
        /// </summary>
        private void DrawPoints(PictureBox pictureBox, List<IDrawable> drawableFigures)
        {
            List<Point> points = drawableFigures[0].Points;
            for (int i = 0; i < points.Count; i++)
            {
                DrawFigureCircle drawFigure = new DrawFigureCircle(points[i],
                    new Point(points[i].X + _radiusDrawPoint, points[i].Y + _radiusDrawPoint),
                    new LineProperty(), new FillProperty(Color.Transparent));
                drawFigure.Draw(pictureBox);
            }
        }
    }
}