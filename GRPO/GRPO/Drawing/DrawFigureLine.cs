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
    class DrawFigureLine : IDrawable
    {
        /// <summary>
        /// Объект класса линии
        /// </summary>
        private FigureLine _figureLine;
        /// <summary>
        /// Холст на котором рисуют
        /// </summary>
        private PictureBox _pictureBox;
        /// <summary>
        /// Расширение для отрисовки линии
        /// </summary>
        private ExtendedForLine _extendedForLine;
        /// <summary>
        /// Псутой класс Отрисовки линии
        /// </summary>
        public DrawFigureLine()
        {
            Line = new FigureLine();
            Canvas = new PictureBox();
            Extended = new ExtendedForLine();
        }
        /// <summary>
        /// Класс Отрисовки линии
        /// </summary>
        /// <param name="a">Начальная точка</param>
        /// <param name="b">Конечная точка</param>
        /// <param name="pictureBox">Холст на котором рисуют линию</param>
        /// <param name="extended">Объект расширения для отрисовки</param>
        public DrawFigureLine(Point a, Point b, PictureBox pictureBox, ExtendedForLine extended)
        {
            Line = new FigureLine(a, b);
            Canvas = pictureBox;
            Extended = extended;
        }
        /// <summary>
        /// Векторный объект линии
        /// </summary>
        public FigureLine Line
        {
            get
            {
                return _figureLine;
            }
            set
            {
                _figureLine = value;
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
        public ExtendedForLine Extended
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
        /// Нарисовать линию
        /// </summary>
        public void Draw()
        {
            Graphics graphics = Graphics.FromImage(_pictureBox.Image);
            Pen pen = new Pen(Extended.LineColor, Extended.LineThickness);
            pen.DashStyle = Extended.LineType;
            graphics.DrawLine(pen, Line.A.X, Line.A.Y, Line.B.X, Line.B.Y);
            graphics.Dispose();
            _pictureBox.Invalidate();
        }
        /// <summary>
        /// Взять список точек
        /// </summary>
        /// <returns>Списко точек формирующих фигуру</returns>
        public List<Point> GetPoints()
        {
            List<Point> points = new List<Point>();
            points.Add(Line.A);
            points.Add(Line.B);
            return points;
        }
    }
}