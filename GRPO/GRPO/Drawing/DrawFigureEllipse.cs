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
    class DrawFigureEllipse : FigureEllipse, IDrawable
    {
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
        /// Пустой класс Отрисовки эллипса
        /// </summary>
        public DrawFigureEllipse() : base()
        {
            Canvas = new PictureBox();
            ExtendedLine = new ExtendedForLine(1, Color.Black, DashStyle.Solid);
            ExtendedFigure = new ExtendedForFigure(Color.White);
        }
        /// <summary>
        /// Класс Отрисовки эллипса
        /// </summary>
        /// <param name="position">Расположения эллипса</param>
        /// <param name="width">Ширина эллипса</param>
        /// <param name="height">Высота эллипса</param>
        /// <param name="canvas">Полотно на котором отрисовывается эллипс</param>
        public DrawFigureEllipse(Point position, int width, int height, PictureBox canvas) : base(position, width, height)
        {
            Canvas = canvas;
            ExtendedLine = new ExtendedForLine(1, Color.Black, DashStyle.Solid);
            ExtendedFigure = new ExtendedForFigure(Color.White);
        }
        /// <summary>
        /// Класс Отрисовки эллипса
        /// </summary>
        /// <param name="position">Расположения эллипса</param>
        /// <param name="width">Ширина эллипса</param>
        /// <param name="height">Высота эллипса</param>
        /// <param name="canvas">Полотно на котором отрисовывается эллипс</param>
        /// <param name="extendedForLine">Дополнительные свойства отрисовки линии</param>
        /// <param name="extendedForFigure">Дополнительные свойства отрисовки фигуры</param>
        public DrawFigureEllipse(Point position, int width, int height, PictureBox canvas,
            ExtendedForLine extendedForLine, ExtendedForFigure extendedForFigure) : base(position, width, height)
        {
            Canvas = canvas;
            ExtendedLine = extendedForLine;
            ExtendedFigure = extendedForFigure;
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
            Graphics g = Graphics.FromImage(_pictureBox.Image);
            //Graphics g = _pictureBox.CreateGraphics();
            Pen pen = new Pen(ExtendedLine.LineColor, ExtendedLine.LineThickness);
            pen.DashStyle = ExtendedLine.LineType;
            g.FillEllipse(new SolidBrush(ExtendedFigure.FillColor), X, Y, Width, Height);
            g.DrawEllipse(pen, X, Y, Width, Height);
            g.Dispose();
            _pictureBox.Invalidate();
        }
        /// <summary>
        /// Очистить многоугольник
        /// </summary>
        public void Clear()
        {
            Graphics g = Graphics.FromImage(_pictureBox.Image);
            //Graphics g = _pictureBox.CreateGraphics();
            Pen pen = new Pen(ExtendedLine.LineColor, ExtendedLine.LineThickness);
            pen.DashStyle = ExtendedLine.LineType;
            g.FillEllipse(Brushes.White, X, Y, Width, Height);
            g.DrawEllipse(pen, X, Y, Width, Height);
            g.Dispose();
            _pictureBox.Invalidate();
        }
        /// <summary>
        /// Взять список точек
        /// </summary>
        /// <returns>Списко точек формирующих фигуру</returns>
        public List<Point> GetPoints()
        {
            List<Point> points = new List<Point>();
            points.Add(new Point(X, Y));
            points.Add(new Point(X + Width, Y));
            points.Add(new Point(X + Width, Y + Height));
            points.Add(new Point(X, Y + Height));
            return points;
        }
    }
}
