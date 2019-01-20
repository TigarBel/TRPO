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

namespace GRPO
{
    /// <summary>
    /// Класс взаимодействия с нарисованными фигурами
    /// </summary>
    [Serializable]
    public class Interaction
    {
        /// <summary>
        /// Рисуемые объекты
        /// </summary>
        private List<IDrawable> _drawables = new List<IDrawable>();
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
        private int _radiusDrawPoint = 4;
        /// <summary>
        /// Пустой класс взаимодействия
        /// </summary>
        public Interaction()
        {
            EnablePoints = false;
        }
        /// <summary>
        /// Класс взаимодействия
        /// </summary>
        /// <param name="drawableFigure">Рисуемый объект</param>
        /// <param name="enablePoints">Разрашение измять опортные точки</param>
        public Interaction(IDrawable drawableFigure, bool enablePoints)
        {
            DrawableFigures.Add(drawableFigure);
            EnablePoints = enablePoints;
        }
        /// <summary>
        /// Класс взаимодействия
        /// </summary>
        /// <param name="drawables">Список фигур</param>
        /// <param name="enablePoints">Разрашение измять опортные точки</param>
        public Interaction(List<IDrawable> drawables, bool enablePoints)
        {
            DrawableFigures = drawables;
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
                _enablePoints = value;
            }
        }
        /// <summary>
        /// Отрисовка выделения
        /// </summary>
        /// <param name="pictureBox">Холст на котором рисуют</param>
        public void DrawSelcet(PictureBox pictureBox)
        {
            if (this.EnablePoints)
            {
                if (DrawableFigures.Count == 1)
                {
                    DrawPoints(pictureBox);
                }
                else
                {
                    throw new ArgumentException("Режим изменения опорных точек разрешен только при выдилении одной фигуры!");
                }
            }
            else
            {
                if (DrawableFigures.Count == 1)
                {
                    DrawInteraction(pictureBox);
                }
                else
                {
                    foreach (IDrawable drawable in DrawableFigures)
                    {
                        DrawSquare(DrawableFigures.IndexOf(drawable), pictureBox);
                    }
                }
            }
        }
        /// <summary>
        /// Отрисовка интерактивного квадрата без опорных точек
        /// </summary>
        private void DrawInteraction(PictureBox pictureBox)
        {
            DrawSquare(0, pictureBox);
            DrawPointsSize(0, pictureBox);
        }
        /// <summary>
        /// Отрисовка квадрата границ объекта
        /// </summary>
        /// <param name="index">Номер фигуры из списка фигур</param>
        private void DrawSquare(int index , PictureBox pictureBox)
        {
            List<Point> points = GetBorderPoints(index);
            DrawFigurePolygon drawFigure = new DrawFigurePolygon(points, new LineProperty(1, Color.Black, DashStyle.Dash), 
                new FillProperty(Color.Transparent));
            drawFigure.Draw(pictureBox);
        }
        /// <summary>
        /// Точки размера объекта
        /// </summary>
        /// <param name="index">Номер фигуры из списка фигур</param>
        private void DrawPointsSize(int index, PictureBox pictureBox)
        {
            List<Point> points = GetBorderPoints(index);
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
        private void DrawPointsSquare(int index, PictureBox pictureBox)
        {
            List<Point> points = GetBorderPoints(index);
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
        private List<Point> GetBorderPoints(int index)
        {
            int minX = DrawableFigures[index].Points.Min(point => point.X);
            int maxX = DrawableFigures[index].Points.Max(point => point.X);
            int minY = DrawableFigures[index].Points.Min(point => point.Y);
            int maxY = DrawableFigures[index].Points.Max(point => point.Y);
            return new List<Point>() { new Point(minX, minY), new Point(maxX, minY), new Point(maxX, maxY), new Point(minX, maxY) };
        }
        /// <summary>
        /// Отрисовка опорных точек
        /// </summary>
        private void DrawPoints(PictureBox pictureBox)
        {
            List<Point> points = DrawableFigures[0].Points;
            for (int i = 0; i < points.Count; i++) 
            {
                DrawFigureCircle drawFigure = new DrawFigureCircle(points[i], new Point(points[i].X + _radiusDrawPoint, points[i].Y + _radiusDrawPoint),
                    new LineProperty(), new FillProperty(Color.Transparent));
                drawFigure.Draw(pictureBox);
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
            foreach (Point sizePoint in DrawableFigures[0].Points)
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
            if (_indexSelectPoint != -1)
            {
                List<Point> points = DrawableFigures[0].Points;
                points[_indexSelectPoint] = pointDeviation;
                DrawableFigures[0].Points = points;
            }
        }
        /// <summary>
        /// Добавление фигур в список выделяемых фигур
        /// </summary>
        /// <param name="drawable">Фигура</param>
        public void AddDrawableFigure(IDrawable drawable)
        {
            if(!DrawableFigures.Contains(drawable))
            {
                DrawableFigures.Add(drawable);
                EnablePoints = false;
            }
        }
    }
}