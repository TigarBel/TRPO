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
        /// Рисуемые объекты
        /// </summary>
        private List<IDrawable> _drawables = new List<IDrawable>();

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
            get { return _drawables; }
            set { _drawables = value; }
        }

        /// <summary>
        /// Разрашение изменять опортные точки
        /// </summary>
        public bool EnablePoints { get; set; }

        /// <summary>
        /// Выбранная габаритная точка
        /// </summary>
        public Point SelectPoint
        {
            set
            {
                Checking _checking = new Checking();
                _indexSelectPoint =
                    _checking.GetNumberPoint(new Point(value.X, value.Y), DrawableFigures[0], _radiusDrawPoint);
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
            if (!DrawableFigures.Contains(drawable))
            {
                DrawableFigures.Add(drawable);
                EnablePoints = false;
            }
        }
    }
}