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
        /// <summary>
        /// Получить номер выбранной габаритной точки фигуры
        /// </summary>
        /// <param name="point">Локальная точка</param>
        /// <returns>Номер габаритной точки фигуры</returns>
        private int GetNumberPoint(Point point)
        {
            int number = 0;
            foreach (Point sizePoint in DrawableFigure.GetPoints())
            {
                if (point.X >= sizePoint.X - 3 && point.X <= sizePoint.X + 3 && point.Y >= sizePoint.Y - 3 && point.Y <= sizePoint.Y + 3)
                {
                    return number;
                }
                number++;
            }
            return 0;
        }
        /// <summary>
        /// Изменить габаритную точку
        /// </summary>
        /// <param name="point">Выбранная точка</param>
        /// <param name="pointDeviation">Подредактированная точка</param>
        public void ChangeSizePoint(Point point, Point pointDeviation)
        {
            switch (DrawableFigure.GetType().Name)
            {
                case "DrawFigureLine":
                    {
                        if (GetNumberPoint(point) == 0)
                        {
                            ((DrawFigureLine)DrawableFigure).Line.A = new Point(pointDeviation.X, pointDeviation.Y);
                        }
                        else
                        {
                            ((DrawFigureLine)DrawableFigure).Line.B = new Point(pointDeviation.X, pointDeviation.Y);
                        }
                        break;
                    }
                case "DrawFigurePolyline":
                    {
                        ((DrawFigurePolyline)DrawableFigure).Polyline.Points[GetNumberPoint(point)] = new Point(pointDeviation.X, pointDeviation.Y);
                        break;
                    }
                case "DrawFigureRectangle":
                    {
                        ((DrawFigureRectangle)DrawableFigure).Polygon.Points[GetNumberPoint(point)] = new Point(pointDeviation.X, pointDeviation.Y);
                        break;
                    }
                case "DrawFigureCircle":
                    {
                        if (GetNumberPoint(point) == 0)
                        {
                            /*Point localPoint = new Point(((DrawFigureCircle)DrawableFigure).GetPoints()[2].X,
                                ((DrawFigureCircle)DrawableFigure).GetPoints()[2].Y);
                            int result = (((DrawFigureCircle)DrawableFigure).GetPoints()[2].X - pointDeviation.X) / 2;
                            ((DrawFigureCircle)DrawableFigure).Position = new Point(pointDeviation.X, pointDeviation.Y);
                            ((DrawFigureCircle)DrawableFigure).Circle.Radius = result;*/
                        }
                        /*if (GetNumberPoint(point) == 1)
                        {

                        }
                        if (GetNumberPoint(point) == 2)
                        {

                        }
                        if (GetNumberPoint(point) == 3)
                        {

                        }*/
                        break;
                    }
                /*case "DrawFigureEllipse":
                    {
                        ((DrawFigureEllipse)DrawableFigure).Ellipse.Points[GetNumberPoint(point)] = new Point(pointDeviation.X, pointDeviation.Y);
                        break;
                    }*/
            }
        }
    }
}