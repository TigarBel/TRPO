using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace GRPO
{
    class DrawFigureCircle : FigureCircle, IDraw
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
        /// Пустой класс Отрисовки круга
        /// </summary>
        public DrawFigureCircle() : base()
        {
            Canvas = new PictureBox();
            ExtendedLine = new ExtendedForLine(1, Color.Black, EnumLineType.Solid);
            ExtendedFigure = new ExtendedForFigure(Color.White);
        }
        /// <summary>
        /// Класс Отрисовки круга
        /// </summary>
        /// <param name="position">Расположение окружности</param>
        /// <param name="radius"> Радиус окружности</param>
        /// <param name="canvas">Полотно на котором отрисовывается круг</param>
        public DrawFigureCircle(Point position, int radius, PictureBox canvas) : base(position, radius)
        {
            Canvas = canvas;
            ExtendedLine = new ExtendedForLine(1, Color.Black, EnumLineType.Solid);
            ExtendedFigure = new ExtendedForFigure(Color.White);
        }
        /// <summary>
        /// Класс Отрисовки круга
        /// </summary>
        /// <param name="position">Расположение окружности</param>
        /// <param name="radius"> Радиус окружности</param>
        /// <param name="canvas">Полотно на котором отрисовывается круг</param>
        /// <param name="extendedForLine">Дополнительные свойства отрисовки линии</param>
        /// <param name="extendedForFigure">Дополнительные свойства отрисовки фигуры</param>
        public DrawFigureCircle(Point position, int radius, PictureBox canvas,
            ExtendedForLine extendedForLine, ExtendedForFigure extendedForFigure) : base(position, radius)
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
            Graphics g = _pictureBox.CreateGraphics();
            g.DrawEllipse(new Pen(ExtendedLine.LineColor), X, Y, Width, Height);
        }
        /// <summary>
        /// Очистить многоугольник
        /// </summary>
        public void Clear()
        {
            Graphics g = _pictureBox.CreateGraphics();
            g.DrawEllipse(new Pen(Brushes.White), X, Y, Width, Height);
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
