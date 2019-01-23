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

        private List<int> _indexes = new List<int>();

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

        public List<int> Indexes
        {
            get { return _indexes; }
            set { _indexes = value; }
        }

        /// <summary>
        /// Класс взаимодействия
        /// </summary>
        /// <param name="drawables">Весь список фигур</param>
        /// <param name="point">Точка нахождения фигуры</param>
        /// <param name="enablePoints">Разрашение изменять опорные точки(только для одной фигуры)</param>
        public Interaction(List<IDrawable> drawables, Point pointMouse, bool enablePoints)
        {
            for (int i = drawables.Count - 1; i >= 0; i--)
            {
                MinX = drawables[i].Points.Min(point => point.X) - 5;
                MaxX = drawables[i].Points.Max(point => point.X) + 5;
                MinY = drawables[i].Points.Min(point => point.Y) - 5;
                MaxY = drawables[i].Points.Max(point => point.Y) + 5;
                if (pointMouse.X >= MinX && pointMouse.X <= MaxX && pointMouse.Y >= MinY && pointMouse.Y <= MaxY)
                {
                    Indexes.Add(drawables.IndexOf(drawables[i]));
                    DrawableFigures.Add(drawables[i].Clone());
                    EnablePoints = enablePoints;
                    break;
                }
                else
                {
                    MinX = MaxX = MinY = MaxY = 0;
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
                    Indexes.Add(drawables.IndexOf(drawable));
                    localDrawables.Add(drawable.Clone());
                }
            }

            if (localDrawables.Count == 0)
            {
                foreach (IDrawable drawable in drawables)
                {
                    if (points.Min(point => point.X) >= drawable.Points.Min(point => point.X) &&
                        points.Max(point => point.X) <= drawable.Points.Max(point => point.X) &&
                        points.Min(point => point.Y) >= drawable.Points.Min(point => point.Y) &&
                        points.Max(point => point.Y) <= drawable.Points.Max(point => point.Y))
                    {
                        Indexes.Add(drawables.IndexOf(drawable));
                        localDrawables.Add(drawable.Clone());
                    }
                }
            }

            if (localDrawables.Count != 0)
            {
                DrawableFigures = localDrawables;
                foreach (IDrawable drawable in DrawableFigures)
                {
                    MinX = drawable.Points.Min(point => point.X) - 5;
                    MaxX = drawable.Points.Max(point => point.X) + 5;
                    MinY = drawable.Points.Min(point => point.Y) - 5;
                    MaxY = drawable.Points.Max(point => point.Y) + 5;
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