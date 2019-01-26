using GRPO.Drawing.Interface;
using GRPO.InteractionFrame;
using GRPO.InteractionFrame.PointInteractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GRPO
{
    /// <summary>
    /// Класс взаимодействия с нарисованными фигурами
    /// </summary>
    [Serializable]
    public class Interaction : BaseInteraction
    {

        /// <summary>
        /// Индекс выбранной габоритной точки
        /// </summary>
        private int _indexSelectPoint;

        private bool _upPoint = false;

        private bool _rightPoint = false;

        private bool _downPoint = false;

        private bool _leftPoint = false;

        /// <summary>
        /// Список индексов взятых фигур
        /// </summary>
        private List<int> _indexes = new List<int>();

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
        /// <param name="drawables">Весь список фигур</param>
        /// <param name="pointMouse">Точка нахождения фигуры</param>
        /// <param name="enablePoints">Разрашение изменять опорные точки(только для одной фигуры)</param>
        public Interaction(List<IDrawable> drawables, Point pointMouse, bool enablePoints)
        {
            for (int i = drawables.Count - 1; i >= 0; i--)
            {
                if (pointMouse.X >= drawables[i].Points.Min(point => point.X) - 5 &&
                    pointMouse.X <= drawables[i].Points.Max(point => point.X) + 5 &&
                    pointMouse.Y >= drawables[i].Points.Min(point => point.Y) - 5 &&
                    pointMouse.Y <= drawables[i].Points.Max(point => point.Y) + 5)
                {
                    Indexes.Add(drawables.IndexOf(drawables[i]));
                    DrawableFigures.Add(drawables[i].Clone());
                    InteractionPoints = new InteractionPoints(DrawableFigures, _radiusDrawPoint);
                    GetMaxMinXY();
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

                int x = drawable.Points.Max(point => point.X) -
                        (drawable.Points.Max(point => point.X) -
                         drawable.Points.Min(point => point.X)) / 2;
                int y = drawable.Points.Max(point => point.Y) -
                        (drawable.Points.Max(point => point.Y) -
                         drawable.Points.Min(point => point.Y)) / 2;

                if (x >= points.Min(point => point.X) &&
                    x <= points.Max(point => point.X) &&
                    y >= points.Min(point => point.Y) &&
                    y <= points.Max(point => point.Y))
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
                InteractionPoints = new InteractionPoints(DrawableFigures, _radiusDrawPoint);
                GetMaxMinXY();
            }

            EnablePoints = false;
        }

        public Point Position
        {
            set
            {
                foreach (IDrawable drawable in DrawableFigures)
                {
                    drawable.Position = new Point(drawable.Position.X + value.X, drawable.Position.Y + value.Y);
                }

                InteractionPoints = new InteractionPoints(DrawableFigures, _radiusDrawPoint);
            }
        }

        /// <summary>
        /// Рисуемые объекты
        /// </summary>
        public List<IDrawable> DrawableFigures
        {
            get { return Drawables; }
            set { Drawables = value; }
        }

        /// <summary>
        /// Минимальное значение по X
        /// </summary>
        public int MinX { get; set; }

        /// <summary>
        /// Максимальное значение по X
        /// </summary>
        public int MaxX { get; set; }

        /// <summary>
        /// Минимальное значение по Y
        /// </summary>
        public int MinY { get; set; }

        /// <summary>
        /// Максимальное значение по Y
        /// </summary>
        public int MaxY { get; set; }

        /// <summary>
        /// Обновить габариты интерактива
        /// </summary>
        public void GetMaxMinXY()
        {
            List<Point> points = new List<Point>();
            foreach (IDrawable drawable in DrawableFigures)
            {
                foreach (Point point in drawable.Points)
                {
                    points.Add(point);
                }
            }

            MinX = points.Min(point => point.X) - 5;
            MaxX = points.Max(point => point.X) + 5;
            MinY = points.Min(point => point.Y) - 5;
            MaxY = points.Max(point => point.Y) + 5;
        }

        /// <summary>
        /// Список индексов взятых фигур
        /// </summary>
        public List<int> Indexes
        {
            get { return _indexes; }
            set { _indexes = value; }
        }

        /// <summary>
        /// Индекс выбранной опорной точки
        /// </summary>
        public int IndexSelectPoint
        {
            get { return _indexSelectPoint; }
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
                else
                {
                    _upPoint = InteractionPoints.UpPointInteraction.PointInteraction.GetInto(value);
                    _rightPoint = InteractionPoints.RightPointInteraction.PointInteraction.GetInto(value);
                    _downPoint = InteractionPoints.DownPointInteraction.PointInteraction.GetInto(value);
                    _leftPoint = InteractionPoints.LeftPointInteraction.PointInteraction.GetInto(value);
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

        public bool CheckedSelectSizePoint()
        {
            if (_upPoint || _rightPoint || _downPoint || _leftPoint)
            {
                return true;
            }

            return false;
        }

        public void ChangeSize(Point resolutionPoint)
        {
            if (!EnablePoints)
            {
                if (_upPoint)
                {
                    InteractionPoints.UpPointInteraction.ChangeUpSize(InteractionPoints.UpPointInteraction.MinY, resolutionPoint.Y);
                }
                else if (_rightPoint)
                {
                    InteractionPoints.RightPointInteraction.ChangeRightSize(MaxX, resolutionPoint.X);
                }
                else if (_downPoint)
                {
                    InteractionPoints.DownPointInteraction.ChangeDownSize(MaxY, resolutionPoint.Y);
                }
                else if (_leftPoint)
                {
                    InteractionPoints.LeftPointInteraction.ChangeLeftSize(MinX, resolutionPoint.X);
                }
            }
        }

        /// <summary>
        /// Добавление фигур в список выделяемых фигур
        /// </summary>
        /// <param name="drawable">Фигура</param>
        public void AddDrawableFigure(IDrawable drawable)
        {
            DrawableFigures.Add(drawable.Clone());
            EnablePoints = false;
        }
    }
}