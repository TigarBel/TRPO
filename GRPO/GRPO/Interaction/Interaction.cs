using GRPO.Drawing;
using GRPO.Drawing.Interface;
using GRPO.Drawing.Property;
using GRPO.InteractionFrame;
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
    public class Interaction : FrameForInteraction
    {

        /// <summary>
        /// Индекс выбранной габоритной точки
        /// </summary>
        private int _indexSelectPoint;

        /// <summary>
        /// Пустой класс взаимодействия
        /// </summary>
        public Interaction()
        {
            EnablePoints = false;
        }

        /// <summary>
        /// Рисуемые объекты
        /// </summary>
        public List<IDrawable> DrawableFigures
        {
            get { return Drawables; }
            set { Drawables = value; }
        }

        public int MinX { get; set; }
        public int MaxX { get; set; }
        public int MinY { get; set; }
        public int MaxY { get; set; }

        /// <summary>
        /// Класс взаимодействия
        /// </summary>
        /// <param name="drawables">Весь список фигур</param>
        /// <param name="point">Точка нахождения фигуры</param>
        /// <param name="enablePoints">Разрашение изменять опорные точки(только для одной фигуры)</param>
        public Interaction(List<IDrawable> drawables, Point point, bool enablePoints)
        {
            for (int i = drawables.Count; i > 0; i--)
            {
                MinX = drawables[i].Points.Min(localPoint => point.X);
                MaxX = drawables[i].Points.Max(localPoint => point.X);
                MinY = drawables[i].Points.Min(localPoint => point.Y);
                MaxY = drawables[i].Points.Max(localPoint => point.Y);
                if (point.X >= MinX && point.X <= MaxX && point.Y >= MinY && point.Y <= MaxY)
                {
                    DrawableFigures.Add(drawables[i].Clone());
                    EnablePoints = enablePoints;
                    break;
                }
            }
        }

        /// <summary>
        /// Класс взаимодействия
        /// </summary>
        /// <param name="drawables">Весь список фигур</param>
        /// <param name="pointA">Первая крайняя точка диапозона по нахождению фигур</param>
        /// <param name="pointB">Вторая крайняя точка диапозона по нахождению фигур</param>
        public Interaction(List<IDrawable> drawables, Point pointA, Point pointB)
        {
            List<Point> points = new List<Point>();
            points.Add(pointA);
            points.Add(pointB);
            List<IDrawable> localDrawables = new List<IDrawable>();

            foreach (IDrawable drawable in drawables)
            {

                int X = drawable.Points.Max(point => point.X) -
                        (drawable.Points.Max(point => point.X) -
                         drawable.Points.Min(point => point.X)) / 2;
                int Y = drawable.Points.Max(point => point.Y) -
                        (drawable.Points.Max(point => point.Y) -
                         drawable.Points.Min(point => point.Y)) / 2;

                if (X >= points.Min(point => point.X) &&
                    X <= points.Max(point => point.X) &&
                    Y >= points.Min(point => point.Y) &&
                    Y <= points.Max(point => point.Y))
                {
                    localDrawables.Add(drawable.Clone());
                }
            }

            if (localDrawables.Count == 0)
            {
                int Xmin = points.Min(point => point.X);
                int Ymin = points.Min(point => point.Y);
                int Xmax = points.Max(point => point.X);
                int Ymax = points.Max(point => point.Y);
                foreach (IDrawable drawable in drawables)
                {
                    if (Xmin >= drawable.Points.Min(point => point.X) &&
                        Xmax <= drawable.Points.Max(point => point.X) &&
                        Ymin >= drawable.Points.Min(point => point.Y) &&
                        Ymax <= drawable.Points.Max(point => point.Y))
                    {
                        localDrawables.Add(drawable.Clone());
                    }
                }
            }

            if (localDrawables.Count != 0)
            {
                DrawableFigures = localDrawables;
                foreach (IDrawable drawable in DrawableFigures)
                {
                    MinX = drawable.Points.Min(point => point.X);
                    MaxX = drawable.Points.Max(point => point.X);
                    MinY = drawable.Points.Min(point => point.Y);
                    MaxY = drawable.Points.Max(point => point.Y);
                }
            }

            EnablePoints = false;
        }

        /// <summary>
        /// Разрашение изменять опорные точки
        /// </summary>
        public bool EnablePoints
        {
            get { return EnablePointsForDraw; }
            private set { EnablePointsForDraw = value; }
        }

        /// <summary>
        /// Выбранная опорная точка
        /// </summary>
        public Point SelectPoint
        {
            set
            {
                if (EnablePoints)
                {
                    Checking _checking = new Checking();
                    _indexSelectPoint =
                        _checking.GetNumberPoint(new Point(value.X, value.Y), DrawableFigures[0], _radiusDrawPoint);
                }
            }
        }

        /// <summary>
        /// Изменить опорную точку
        /// </summary>
        /// <param name="pointDeviation">Подредактированная точка</param>
        public void ChangePoint(Point pointDeviation)
        {
            if (EnablePoints)
            {
                if (_indexSelectPoint != -1)
                {
                    List<Point> points = DrawableFigures[0].Points;
                    points[_indexSelectPoint] = pointDeviation;
                    DrawableFigures[0].Points = points;
                }
            }
        }

        /// <summary>
        /// Добавление фигур в список выделяемых фигур
        /// </summary>
        /// <param name="drawable">Фигура</param>
        public void AddDrawableFigure(IDrawable drawable)
        {
            if (!DrawableFigures.Contains(drawable))
            {
                DrawableFigures.Add(drawable);
                EnablePoints = false;
            }
        }
    }
}