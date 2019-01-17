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
    public class MassSelect
    {
        /// <summary>
        /// Список фигур
        /// </summary>
        private List<IDrawable> _drawables;
        /// <summary>
        /// Холст на котором рисуется объект
        /// </summary>
        private PictureBox _canvas;
        /// <summary>
        /// Габаритные точки
        /// </summary>
        private List<Point> _pointsSize;
        /// <summary>
        /// Пустой класс выделения нескольких фигурc
        /// </summary>
        public MassSelect()
        {
            Drawables = new List<IDrawable>();
            Canvas = new PictureBox();
        }
        /// <summary>
        /// Класс выделения нескольких фигурc
        /// </summary>
        /// <param name="pictureBox">Полотно на котором рисуются</param>
        public MassSelect(PictureBox pictureBox)
        {
            Drawables = new List<IDrawable>();
            Canvas = pictureBox;
        }
        /// <summary>
        /// Класс выделения нескольких фигурc
        /// </summary>
        /// <param name="pointA">Начальная точка</param>
        /// <param name="pointB">Конечная точка</param>
        /// <param name="drawables">Список нарисованных фигур</param>
        /// <param name="pictureBox">Полотно на котором рисуются</param>
        public MassSelect(Point pointA, Point pointB, List<IDrawable> drawables, PictureBox pictureBox)
        {
            Drawables = SortingDrawables(pointA, pointB, drawables);
            Canvas = pictureBox;
        }
        /// <summary>
        /// Список фигур
        /// </summary>
        public List<IDrawable> Drawables
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
        public List<Point> PointsSize
        {
            get
            {
                return _pointsSize;
            }
            set
            {
                _pointsSize = value;
            }
        }
        /// <summary>
        /// Сортировка списка на отсеивания неподходящих фигур
        /// </summary>
        /// <param name="pointA">Начальная точка</param>
        /// <param name="pointB">Конечная точка</param>
        /// <param name="drawables">Список фигур</param>
        /// <returns></returns>
        private List<IDrawable> SortingDrawables(Point pointA, Point pointB, List<IDrawable> drawables)
        {
            List<IDrawable> localDrawables = new List<IDrawable>();
            List<Point> points = new List<Point>();
            points.Add(pointA);
            points.Add(pointB);

            foreach (IDrawable drawable in drawables)
            {
                int X = drawable.GetPoints().Max(point => point.X) -
                    (drawable.GetPoints().Max(point => point.X) - drawable.GetPoints().Min(point => point.X)) / 2;
                int Y = drawable.GetPoints().Max(point => point.Y) -
                    (drawable.GetPoints().Max(point => point.Y) - drawable.GetPoints().Min(point => point.Y)) / 2;

                if (X >= points.Min(point => point.X) && 
                    X <= points.Max(point => point.X) && 
                    Y >= points.Min(point => point.Y) && 
                    Y <= points.Max(point => point.Y))
                {
                    localDrawables.Add(drawable);
                }
            }
            return localDrawables;
        }
        /// <summary>
        /// Отрисовка интерактивного квадрата без опорных точек
        /// </summary>
        public void DrawInteraction()
        {
            if (Drawables.Count > 0)
            {
                DrawSquare();
                DrawPointsSize();
            }
            else
            {
                throw new ArgumentException("В списке нет элементов!");
            }
        }
        /// <summary>
        /// Отрисовка квадрата границ объекта
        /// </summary>
        private void DrawSquare()
        {
            DrawFigurePolygon drawFigure = new DrawFigurePolygon(PointsSize, _canvas,
                new LineProperty(1, Color.Black, DashStyle.Dash), new FillProperty(Color.Transparent));
            drawFigure.Draw();
        }
        /// <summary>
        /// Точки размера объекта
        /// </summary>
        private void DrawPointsSize()
        {
            DrawFigureCircle drawFigure = new DrawFigureCircle(new Point(PointsSize[0].X + (PointsSize[1].X - PointsSize[0].X) / 2, PointsSize[0].Y),
                3, _canvas, new LineProperty(), new FillProperty());
            drawFigure.Draw();
            drawFigure = new DrawFigureCircle(new Point(PointsSize[1].X, PointsSize[1].Y + (PointsSize[2].Y - PointsSize[1].Y) / 2),
                3, _canvas, new LineProperty(), new FillProperty());
            drawFigure.Draw();
            drawFigure = new DrawFigureCircle(new Point(PointsSize[0].X + (PointsSize[1].X - PointsSize[0].X) / 2, PointsSize[2].Y),
                3, _canvas, new LineProperty(), new FillProperty());
            drawFigure.Draw();
            drawFigure = new DrawFigureCircle(new Point(PointsSize[0].X, PointsSize[1].Y + (PointsSize[2].Y - PointsSize[1].Y) / 2),
                3, _canvas, new LineProperty(), new FillProperty());
            drawFigure.Draw();
        }
        /// <summary>
        /// Функция возвращающая список угловыч точек
        /// </summary>
        /// <returns>Угловые точки</returns>
        private void GetBorderPoints()
        {
            List<Point> points = new List<Point>();
            foreach (IDrawable drawable in Drawables)
            {
                foreach(Point point in drawable.GetPoints())
                {
                    points.Add(point);
                }
            }
            
            int minX = points.Min(point => point.X);
            int maxX = points.Max(point => point.X);
            int minY = points.Min(point => point.Y);
            int maxY = points.Max(point => point.Y);
            PointsSize = new List<Point>() { new Point(minX, minY), new Point(maxX, minY), new Point(maxX, maxY), new Point(minX, maxY) };
        }
    }
}
