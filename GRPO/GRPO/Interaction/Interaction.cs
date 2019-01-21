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
        /// Разрашение измять опортные точки
        /// </summary>
        private bool _enablePoints;

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
        /// Разрашение измять опортные точки
        /// </summary>
        public bool EnablePoints { get; set; }

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
            set { _indexSelectPoint = GetNumberPoint(new Point(value.X, value.Y)); }
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