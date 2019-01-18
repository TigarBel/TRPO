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
        ///// <summary>
        ///// Рисуемый объект
        ///// </summary>
        //private IDrawable _drawable;
        /// <summary>
        /// Рисуемые объекты
        /// </summary>
        private List<IDrawable> _drawables = new List<IDrawable>();
        /// <summary>
        /// Холст на котором рисуется объект
        /// </summary>
        private PictureBox _canvas;
        /// <summary>
        /// Разрашение измять опортные точки
        /// </summary>
        private bool _enablePoints;
        /// <summary>
        /// Индекс выбранной габоритной точки
        /// </summary>
        private int _indexSelectPoint;
        /// <summary>
        /// Радиус точек отображения габаритов
        /// </summary>
        private int _radiusDrawPoint = 5;
        /// <summary>
        /// Пустой класс взаимодействия
        /// </summary>
        public Interaction()
        {
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
            DrawableFigures.Add(drawableFigure);
            Canvas = canvas;
            EnablePoints = enablePoints;
        }
        /// <summary>
        /// Рисуемые объекты
        /// </summary>
        public List<IDrawable> DrawableFigures
        {
            get
            {
                return _drawables;
            }
            set
            {
                _drawables = value;
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
                if (value && DrawableFigures.Count == 1)
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
            DrawPointsSize();
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
        /// Точки размера объекта
        /// </summary>
        private void DrawPointsSize()
        {
            List<Point> points = GetBorderPoints();
            DrawFigureCircle drawFigure = new DrawFigureCircle(new Point(points[0].X + (points[1].X - points[0].X) / 2, points[0].Y),
                _radiusDrawPoint, _canvas, new LineProperty(), new FillProperty());
            drawFigure.Draw();
            drawFigure = new DrawFigureCircle(new Point(points[1].X, points[1].Y + (points[2].Y - points[1].Y) / 2),
                _radiusDrawPoint, _canvas, new LineProperty(), new FillProperty());
            drawFigure.Draw();
            drawFigure = new DrawFigureCircle(new Point(points[0].X + (points[1].X - points[0].X) / 2, points[2].Y),
                _radiusDrawPoint, _canvas, new LineProperty(), new FillProperty());
            drawFigure.Draw();
            drawFigure = new DrawFigureCircle(new Point(points[0].X, points[1].Y + (points[2].Y - points[1].Y) / 2),
                _radiusDrawPoint, _canvas, new LineProperty(), new FillProperty());
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
                DrawFigureCircle drawFigure = new DrawFigureCircle(points[i], _radiusDrawPoint, _canvas, new LineProperty(), new FillProperty());
                drawFigure.Draw();
            }
        }
        /// <summary>
        /// Функция возвращающая список угловыч точек
        /// </summary>
        /// <returns>Угловые точки</returns>
        private List<Point> GetBorderPoints()
        {
            int minX = DrawableFigures[0].GetPoints().Min(point => point.X);
            int maxX = DrawableFigures[0].GetPoints().Max(point => point.X);
            int minY = DrawableFigures[0].GetPoints().Min(point => point.Y);
            int maxY = DrawableFigures[0].GetPoints().Max(point => point.Y);
            return new List<Point>() { new Point(minX, minY), new Point(maxX, minY), new Point(maxX, maxY), new Point(minX, maxY) };
        }
        /// <summary>
        /// Отрисовка опорных точек
        /// </summary>
        private void DrawPoints()
        {
            List<Point> points = DrawableFigures[0].GetPoints();
            for (int i = 0; i < points.Count; i++) 
            {
                DrawFigureCircle drawFigure = new DrawFigureCircle(points[i], _radiusDrawPoint, _canvas, new LineProperty(), new FillProperty(Color.Transparent));
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
            foreach (Point sizePoint in DrawableFigures[0].GetPoints())
            {
                if (point.X >= sizePoint.X - _radiusDrawPoint && point.X <= sizePoint.X + _radiusDrawPoint && 
                    point.Y >= sizePoint.Y - _radiusDrawPoint && point.Y <= sizePoint.Y + _radiusDrawPoint)
                {
                    return number;
                }
                number++;
            }
            return -1;
        }
        /// <summary>
        /// Выбранная габаритная точка
        /// </summary>
        public Point SelectPoint
        {
            set
            {
                _indexSelectPoint = GetNumberPoint(new Point(value.X, value.Y));
            }
        }
        /// <summary>
        /// Изменить габаритную точку
        /// </summary>
        /// <param name="pointDeviation">Подредактированная точка</param>
        public void ChangePoint(Point pointDeviation)
        {
            switch (DrawableFigures[0].GetType().Name)
            {
                case "DrawFigureLine":
                    {
                        if (_indexSelectPoint == 0)
                        {
                            ((DrawFigureLine)DrawableFigures[0]).Line.PointA = new Point(pointDeviation.X, pointDeviation.Y);
                        }
                        else if (_indexSelectPoint == 1)
                        {
                            ((DrawFigureLine)DrawableFigures[0]).Line.PointB = new Point(pointDeviation.X, pointDeviation.Y);
                        }
                        break;
                    }
                case "DrawFigurePolyline":
                    {
                        if (_indexSelectPoint != -1)
                        {
                            ((DrawFigurePolyline)DrawableFigures[0]).Polyline.Points[_indexSelectPoint] =
                                new Point(pointDeviation.X, pointDeviation.Y);
                        }
                        break;
                    }
                case "DrawFigureRectangle":
                    {
                        switch (_indexSelectPoint)
                        {
                            case 0:
                                {
                                    ((DrawFigureRectangle)DrawableFigures[0]).Rectangle.PointLeftUp = new Point(pointDeviation.X, pointDeviation.Y);
                                    break;
                                }
                            case 1:
                                {
                                    ((DrawFigureRectangle)DrawableFigures[0]).Rectangle.PointRightUp = new Point(pointDeviation.X, pointDeviation.Y);
                                    break;
                                }
                            case 2:
                                {
                                    ((DrawFigureRectangle)DrawableFigures[0]).Rectangle.PointRightDown = new Point(pointDeviation.X, pointDeviation.Y);
                                    break;
                                }
                            case 3:
                                {
                                    ((DrawFigureRectangle)DrawableFigures[0]).Rectangle.PointLeftDown = new Point(pointDeviation.X, pointDeviation.Y);
                                    break;
                                }
                        }
                        break;
                    }
                case "DrawFigureCircle":
                    {
                        ((DrawFigureCircle)DrawableFigures[0]).Circle.Radius = (pointDeviation.X - ((DrawFigureCircle)DrawableFigures[0]).Position.X) / 2;
                        break;
                    }
                case "DrawFigureEllipse":
                    {
                        switch(_indexSelectPoint)
                        {
                            case 0:
                                {
                                    ((DrawFigureEllipse)DrawableFigures[0]).Width = ((DrawFigureEllipse)DrawableFigures[0]).Width -
                                        (pointDeviation.X - ((DrawFigureEllipse)DrawableFigures[0]).Position.X);
                                    ((DrawFigureEllipse)DrawableFigures[0]).Height = ((DrawFigureEllipse)DrawableFigures[0]).Height -
                                        (pointDeviation.Y - ((DrawFigureEllipse)DrawableFigures[0]).Position.Y);
                                    ((DrawFigureEllipse)DrawableFigures[0]).Position = new Point(pointDeviation.X,pointDeviation.Y);
                                    break;
                                }
                            case 1:
                                {
                                    ((DrawFigureEllipse)DrawableFigures[0]).Width = pointDeviation.X - ((DrawFigureEllipse)DrawableFigures[0]).Position.X;
                                    ((DrawFigureEllipse)DrawableFigures[0]).Height = ((DrawFigureEllipse)DrawableFigures[0]).Height -
                                        (pointDeviation.Y - ((DrawFigureEllipse)DrawableFigures[0]).Position.Y);
                                    ((DrawFigureEllipse)DrawableFigures[0]).Position = 
                                        new Point(((DrawFigureEllipse)DrawableFigures[0]).Position.X, pointDeviation.Y);
                                    break;
                                }
                            case 2:
                                {
                                    ((DrawFigureEllipse)DrawableFigures[0]).Width = pointDeviation.X - ((DrawFigureEllipse)DrawableFigures[0]).Position.X;
                                    ((DrawFigureEllipse)DrawableFigures[0]).Height = pointDeviation.Y - ((DrawFigureEllipse)DrawableFigures[0]).Position.Y;
                                    break;
                                }
                            case 3:
                                {
                                    ((DrawFigureEllipse)DrawableFigures[0]).Width = ((DrawFigureEllipse)DrawableFigures[0]).Width -
                                        (pointDeviation.X - ((DrawFigureEllipse)DrawableFigures[0]).Position.X);
                                    ((DrawFigureEllipse)DrawableFigures[0]).Height = pointDeviation.Y - ((DrawFigureEllipse)DrawableFigures[0]).Position.Y;
                                    ((DrawFigureEllipse)DrawableFigures[0]).Position =
                                        new Point(pointDeviation.X, ((DrawFigureEllipse)DrawableFigures[0]).Position.Y);
                                    break;
                                }
                        }
                        break;
                    }
            }
        }
    }
}