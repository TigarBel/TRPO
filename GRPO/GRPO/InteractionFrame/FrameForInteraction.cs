using GRPO.Drawing;
using GRPO.Drawing.Interface;
using GRPO.Drawing.Property;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GRPO.InteractionFrame
{
    public abstract class FrameForInteraction
    {
        /// <summary>
        /// Радиус точек отображения габаритов
        /// </summary>
        public int _radiusDrawPoint = 4;
        /// <summary>
        /// Отрисовка выделения
        /// </summary>
        /// <param name="pictureBox">Холст на котором рисуют</param>
        public void DrawSelcet(PictureBox pictureBox, bool enablePoints, List<IDrawable> drawableFigures)
        {
            if (enablePoints)
            {
                if (drawableFigures.Count == 1)
                {
                    DrawPoints(pictureBox, drawableFigures);
                }
                else
                {
                    throw new ArgumentException("Режим изменения опорных точек разрешен только при выделении одной фигуры!");
                }
            }
            else
            {
                if (drawableFigures.Count == 1)
                {
                    DrawInteraction(pictureBox, drawableFigures);
                }
                else
                {
                    foreach (IDrawable drawable in drawableFigures)
                    {
                        DrawSquare(drawableFigures.IndexOf(drawable), pictureBox, drawableFigures);
                    }
                }
            }
        }
        /// <summary>
        /// Отрисовка интерактивного квадрата без опорных точек
        /// </summary>
        public void DrawInteraction(PictureBox pictureBox, List<IDrawable> drawableFigures)
        {
            DrawSquare(0, pictureBox, drawableFigures);
            DrawPointsSize(0, pictureBox, drawableFigures);
        }
        /// <summary>
        /// Отрисовка квадрата границ объекта
        /// </summary>
        /// <param name="index">Номер фигуры из списка фигур</param>
        public void DrawSquare(int index, PictureBox pictureBox, List<IDrawable> drawableFigures)
        {
            List<Point> points = GetBorderPoints(index, drawableFigures);
            DrawFigurePolygon drawFigure = new DrawFigurePolygon(points, new LineProperty(1, Color.Black, DashStyle.Dash),
                new FillProperty(Color.Transparent));
            drawFigure.Draw(pictureBox);
        }
        /// <summary>
        /// Точки размера объекта
        /// </summary>
        /// <param name="index">Номер фигуры из списка фигур</param>
        public void DrawPointsSize(int index, PictureBox pictureBox, List<IDrawable> drawableFigures)
        {
            List<Point> points = GetBorderPoints(index, drawableFigures);
            DrawFigureCircle drawFigure = new DrawFigureCircle(new Point(points[0].X + (points[1].X - points[0].X) / 2, points[0].Y),
                new Point(points[0].X + (points[1].X - points[0].X) / 2 + _radiusDrawPoint, points[0].Y + _radiusDrawPoint),
                new LineProperty(), new FillProperty());
            drawFigure.Draw(pictureBox);
            drawFigure = new DrawFigureCircle(new Point(points[1].X, points[1].Y + (points[2].Y - points[1].Y) / 2),
                new Point(points[1].X + _radiusDrawPoint, points[1].Y + (points[2].Y - points[1].Y) / 2 + _radiusDrawPoint),
                new LineProperty(), new FillProperty());
            drawFigure.Draw(pictureBox);
            drawFigure = new DrawFigureCircle(new Point(points[0].X + (points[1].X - points[0].X) / 2, points[2].Y),
                new Point(points[0].X + (points[1].X - points[0].X) / 2 + _radiusDrawPoint, points[2].Y + _radiusDrawPoint),
                new LineProperty(), new FillProperty());
            drawFigure.Draw(pictureBox);
            drawFigure = new DrawFigureCircle(new Point(points[0].X, points[1].Y + (points[2].Y - points[1].Y) / 2),
                new Point(points[0].X + _radiusDrawPoint, points[1].Y + (points[2].Y - points[1].Y) / 2 + _radiusDrawPoint),
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
                DrawFigureCircle drawFigure = new DrawFigureCircle(points[i], new Point(points[i].X + _radiusDrawPoint, points[i].Y + _radiusDrawPoint),
                    new LineProperty(), new FillProperty());
                drawFigure.Draw(pictureBox);
            }
        }
        /// <summary>
        /// Функция возвращающая список угловыч точек
        /// </summary>
        /// <param name="index">Номер фигуры из списка фигур</param>
        /// <returns>Угловые точки</returns>
        public List<Point> GetBorderPoints(int index, List<IDrawable> drawableFigures)
        {
            int minX = drawableFigures[index].Points.Min(point => point.X);
            int maxX = drawableFigures[index].Points.Max(point => point.X);
            int minY = drawableFigures[index].Points.Min(point => point.Y);
            int maxY = drawableFigures[index].Points.Max(point => point.Y);
            return new List<Point>() { new Point(minX, minY), new Point(maxX, minY), new Point(maxX, maxY), new Point(minX, maxY) };
        }
        /// <summary>
        /// Отрисовка опорных точек
        /// </summary>
        public void DrawPoints(PictureBox pictureBox, List<IDrawable> drawableFigures)
        {
            List<Point> points = drawableFigures[0].Points;
            for (int i = 0; i < points.Count; i++)
            {
                DrawFigureCircle drawFigure = new DrawFigureCircle(points[i], new Point(points[i].X + _radiusDrawPoint, points[i].Y + _radiusDrawPoint),
                    new LineProperty(), new FillProperty(Color.Transparent));
                drawFigure.Draw(pictureBox);
            }
        }
    }
}
