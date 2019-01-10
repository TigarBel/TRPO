using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GRPO
{
    /// <summary>
    /// Класс взаимодействия с нарисованными фигурами
    /// </summary>
    public class Interaction
    {
        /// <summary>
        /// Рисуемый объект
        /// </summary>
        private IDrawable _drawable;
        /// <summary>
        /// Холст на котором рисуется объект
        /// </summary>
        private PictureBox _canvas;
        /// <summary>
        /// Разрашение измять опортные точки
        /// </summary>
        private bool _enablePoints;
        /// <summary>
        /// Пустой класс взаимодействия
        /// </summary>
        public Interaction()
        {
            DrawableFigure = new DrawFigureLine();
            Canvas = new PictureBox();
            EnablePoints = false;
        }
        /// <summary>
        /// Класс взаимодействия
        /// </summary>
        /// <param name="drawableFigure">Рисуемый объект</param>
        /// <param name="enablePoints">Разрашение измять опортные точки</param>
        public Interaction(IDrawable drawableFigure, PictureBox canvas, bool enablePoints)
        {
            DrawableFigure = drawableFigure;
            Canvas = canvas;
            EnablePoints = enablePoints;
        }

        /// <summary>
        /// Рисуемый объект
        /// </summary>
        public IDrawable DrawableFigure
        {
            get
            {
                return _drawable;
            }
            set
            {
                _drawable = value;
            }
        }
        /// <summary>
        /// Холст на котором рисуют объект
        /// </summary>
        public PictureBox Canvas
        {
            set
            {
                _canvas = value;
            }
        }
        /// <summary>
        /// Разрашение измять опортные точки
        /// </summary>
        public bool EnablePoints
        {
            get
            {
                return _enablePoints;
            }
            set
            {
                if (value)
                {
                    DrawPoints();
                }
                else
                {
                    DrawInteraction();
                }
                _enablePoints = value;
            }
        }
        /// <summary>
        /// Отрисовка интерактивного квадрата без опорных точек
        /// </summary>
        private void DrawInteraction()
        {
            DrawSquare();
            DrawPointsSquare();
        }
        /// <summary>
        /// Отрисовка квадрата границ объекта
        /// </summary>
        private void DrawSquare()
        {
            List<Point> points = GetBorderPoints();
            DrawFigurePolygon drawFigure = new DrawFigurePolygon(points, _canvas,
                new LineProperty(1, Color.Black, DashStyle.Dash), new FillProperty(Color.Transparent));
            drawFigure.Draw();
        }
        /// <summary>
        /// Отрисовка угловых точек
        /// </summary>
        private void DrawPointsSquare()
        {
            List<Point> points = GetBorderPoints();
            for (int i = 0; i < 4; i++)
            {
                DrawFigureCircle drawFigure = new DrawFigureCircle(points[i], 3, _canvas, new LineProperty(), new FillProperty());
                drawFigure.Draw();
            }
        }
        /// <summary>
        /// Функция возвращающая список угловыч точек
        /// </summary>
        /// <returns>Угловые точки</returns>
        private List<Point> GetBorderPoints()
        {
            int minX = DrawableFigure.GetPoints().Min(point => point.X);
            int maxX = DrawableFigure.GetPoints().Max(point => point.X);
            int minY = DrawableFigure.GetPoints().Min(point => point.Y);
            int maxY = DrawableFigure.GetPoints().Max(point => point.Y);
            return new List<Point>() { new Point(minX, minY), new Point(maxX, minY), new Point(maxX, maxY), new Point(minX, maxY) };
        }
        /// <summary>
        /// Отрисовка опорных точек
        /// </summary>
        private void DrawPoints()
        {
            List<Point> points = DrawableFigure.GetPoints();
            for (int i = 0; i < points.Count; i++) 
            {
                DrawFigureCircle drawFigure = new DrawFigureCircle(points[i], 3, _canvas, new LineProperty(), new FillProperty(Color.Transparent));
                drawFigure.Draw();
            }
        }
    }
}