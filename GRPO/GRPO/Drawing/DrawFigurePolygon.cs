using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace GRPO
{
    class DrawFigurePolygon : IDrawable
    {
        /// <summary>
        /// Объект многоугольника
        /// </summary>
        private FigurePolygon _figurePolygon;
        /// <summary>
        /// Холст на котором рисуют
        /// </summary>
        private PictureBox _pictureBox;
        /// <summary>
        /// Расширение для отрисовки линии
        /// </summary>
        private ExtendedForLine _extendedForLine;
        /// <summary>
        /// Расширение для отрисовки фигуры
        /// </summary>
        private ExtendedForFigure _extendedForFigure;
        /// <summary>
        /// Пустой класс Отрисовки многоугольника
        /// </summary>
        public DrawFigurePolygon()
        {
            Polygon = new FigurePolygon();
            Canvas = new PictureBox();
            ExtendedLine = new ExtendedForLine();
            ExtendedFigure = new ExtendedForFigure();
        }
        /// <summary>
        /// Класс Отрисовки многоугольника
        /// </summary>
        /// <param name="points">Список существующих точек для многоугольника</param>
        /// <param name="canvas">Полотно на котором отрисовывается многоугольник</param>
        /// <param name="extendedForLine">Дополнительные свойства отрисовки линии</param>
        /// <param name="extendedForFigure">Дополнительные свойства отрисовки фигуры</param>
        public DrawFigurePolygon(List<Point> points, PictureBox canvas,
            ExtendedForLine extendedForLine, ExtendedForFigure extendedForFigure)
        {
            Polygon = new FigurePolygon(points);
            Canvas = canvas;
            ExtendedLine = extendedForLine;
            ExtendedFigure = extendedForFigure;
        }
        /// <summary>
        /// Класс Отрисовки многоугольника
        /// </summary>
        /// <param name="position">Позиция многоугольника</param>
        /// <param name="width">Ширина многоугольника</param>
        /// <param name="height">Высота многоугольника</param>
        /// <param name="countAngle">Количество углов многоугольника</param>
        /// <param name="phase">Угол поворота многоугольника</param>
        /// <param name="canvas">Полотно на котором отрисовывается многоугольник</param>
        /// <param name="extendedForLine">Дополнительные свойства отрисовки линии</param>
        /// <param name="extendedForFigure">Дополнительные свойства отрисовки фигуры</param>
        public DrawFigurePolygon(Point position, int width, int height, int countAngle, int phase, PictureBox canvas, 
            ExtendedForLine extendedForLine, ExtendedForFigure extendedForFigure)
        {
            Polygon = new FigurePolygon(position, width, height, countAngle, phase);
            Canvas = canvas;
            ExtendedLine = extendedForLine;
            ExtendedFigure = extendedForFigure;
        }
        /// <summary>
        /// Векторный объект многоугольника
        /// </summary>
        public FigurePolygon Polygon
        {
            get
            {
                return _figurePolygon;
            }
            set
            {
                _figurePolygon = value;
            }
        }
        /// <summary>
        /// Холст на котором рисуют
        /// </summary>
        public PictureBox Canvas
        {
            set
            {
                _pictureBox = value;
            }
        }
        /// <summary>
        /// Расширение для отрисовки линии
        /// </summary>
        public ExtendedForLine ExtendedLine
        {
            get
            {
                return _extendedForLine;
            }
            set
            {
                _extendedForLine = value;
            }
        }
        /// <summary>
        /// Расширение для отрисовки фигуры
        /// </summary>
        public ExtendedForFigure ExtendedFigure
        {
            get
            {
                return _extendedForFigure;
            }
            set
            {
                _extendedForFigure = value;
            }
        }
        /// <summary>
        /// Отрисовка последнюю часть многоугольника
        /// </summary>
        public void Draw()
        {
            if (_pictureBox.Image != null)
            {
                if (Polygon.Points.Count > 2)
                {
                    Graphics graphics = Graphics.FromImage(_pictureBox.Image);
                    Pen pen = new Pen(ExtendedLine.LineColor, ExtendedLine.LineThickness);
                    pen.DashStyle = ExtendedLine.LineType;
                    graphics.FillPolygon(new SolidBrush(ExtendedFigure.FillColor), Polygon.Points.ToArray());
                    for (int i = 1; i < Polygon.Points.Count; i++)
                    {
                        graphics.DrawLine(pen, Polygon.Points[i - 1].X, Polygon.Points[i - 1].Y, Polygon.Points[i].X, Polygon.Points[i].Y);
                    }
                    graphics.DrawLine(pen, Polygon.Points[Polygon.Points.Count - 1].X, Polygon.Points[Polygon.Points.Count - 1].Y,
                        Polygon.Points[0].X, Polygon.Points[0].Y);
                    graphics.Dispose();
                    _pictureBox.Invalidate();
                }
                else
                {
                    throw new Exception("Многоугольник пустой, либо не имеет минимум 3 точек отрисовки!");
                }
            }
            else
            {
                throw new Exception("Не выбран холст!");
            }
        }
        /// <summary>
        /// Взять список точек
        /// </summary>
        /// <returns>Списко точек формирующих фигуру</returns>
        public List<Point> GetPoints()
        {
            return Polygon.Points;
        }
        /// <summary>
        /// Позиция фигуры
        /// </summary>
        public Point Position
        {
            get
            {
                return Polygon.Position;
            }
            set
            {
                Polygon.Position = value;
            }
        }
        /// <summary>
        /// Ширина фигуры
        /// </summary>
        public int Width
        {
            get
            {
                return Polygon.WidthPolygon;
            }
            set
            {
                Polygon.WidthPolygon = value;
            }
        }
        /// <summary>
        /// Высота фигуры
        /// </summary>
        public int Height
        {
            get
            {
                return Polygon.HeightPolygon;
            }
            set
            {
                Polygon.HeightPolygon = value;
            }
        }
    }
}
