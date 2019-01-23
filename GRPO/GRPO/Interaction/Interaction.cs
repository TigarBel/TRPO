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
        /// Класс взаимодействия
        /// </summary>
        /// <param name="drawables">Весь список фигур</param>
        /// <param name="point">Точка нахождения фигуры</param>
        /// <param name="enablePoints">Разрашение изменять опорные точки(только для одной фигуры)</param>
        public Interaction(List<IDrawable> drawables, Point point, bool enablePoints)
        {
            for (int i = drawables.Count; i > 0; i--)
            {
                int minX = drawables[i].Points.Min(localPoint => point.X);
                int maxX = drawables[i].Points.Max(localPoint => point.X);
                int minY = drawables[i].Points.Min(localPoint => point.Y);
                int maxY = drawables[i].Points.Max(localPoint => point.Y);
                if (point.X >= minX && point.X <= maxX && point.Y >= minY && point.Y <= maxY)
                {
                    DrawableFigures.Add(drawables[i]);
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
            DrawableFigures = drawables;
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