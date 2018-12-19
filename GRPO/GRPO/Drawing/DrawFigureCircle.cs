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
    class DrawFigureCircle : IDrawable
    {
        /// <summary>
        /// Объект круга
        /// </summary>
        private FigureCircle _figureCircle;
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
        public DrawFigureCircle()
        {
            Circle = new FigureCircle();
            Canvas = new PictureBox();
            ExtendedLine = new ExtendedForLine();
            ExtendedFigure = new ExtendedForFigure();
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
            ExtendedForLine extendedForLine, ExtendedForFigure extendedForFigure)
        {
            Circle = new FigureCircle(position, radius);
            Canvas = canvas;
            ExtendedLine = extendedForLine;
            ExtendedFigure = extendedForFigure;
        }
        /// <summary>
        /// Векторный объект круга
        /// </summary>
        public FigureCircle Circle
        {
            get
            {
                return _figureCircle;
            }
            set
            {
                _figureCircle = value;
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
                Graphics graphics = Graphics.FromImage(_pictureBox.Image);
                Pen pen = new Pen(ExtendedLine.LineColor, ExtendedLine.LineThickness);
                pen.DashStyle = ExtendedLine.LineType;
                graphics.FillEllipse(new SolidBrush(ExtendedFigure.FillColor), 
                    Circle.Position.X, Circle.Position.Y, Circle.WidthCircle, Circle.HeightCircle);
                graphics.DrawEllipse(pen, Circle.Position.X, Circle.Position.Y, Circle.WidthCircle, Circle.HeightCircle);
                graphics.Dispose();
                _pictureBox.Invalidate();
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
            List<Point> points = new List<Point>();
            points.Add(new Point(Circle.Position.X, Circle.Position.Y));
            points.Add(new Point(Circle.Position.X + Circle.WidthCircle, Circle.Position.Y));
            points.Add(new Point(Circle.Position.X + Circle.WidthCircle, Circle.Position.Y + Circle.HeightCircle));
            points.Add(new Point(Circle.Position.X, Circle.Position.Y + Circle.HeightCircle));
            return points;
        }
        /// <summary>
        /// Позиция фигуры
        /// </summary>
        public Point Position
        {
            get
            {
                return Circle.Position;
            }
            set
            {
                Circle.Position = value;
            }
        }
        /// <summary>
        /// Ширина фигуры
        /// </summary>
        public int Width
        {
            get
            {
                return Circle.WidthCircle;
            }
            set
            {
                Circle.WidthCircle = value;
            }
        }
        /// <summary>
        /// Высота фигуры
        /// </summary>
        public int Height
        {
            get
            {
                return Circle.HeightCircle;
            }
            set
            {
                Circle.HeightCircle = value;
            }
        }
    }
}
