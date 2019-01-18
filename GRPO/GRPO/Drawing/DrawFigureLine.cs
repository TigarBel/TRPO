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
    /// <summary>
    /// Класс отрисовки фигуры - линия
    /// </summary>
    [Serializable]
    class DrawFigureLine : IDrawable, ILinePropertyble
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
        private LineProperty _lineProperty;
        /// <summary>
        /// Псутой класс Отрисовки линии
        /// </summary>
        public DrawFigureLine()
        {
            Line = new FigureLine();
            Canvas = new PictureBox();
            LineProperty = new LineProperty();
        }
        /// <summary>
        /// Класс Отрисовки линии
        /// </summary>
        /// <param name="a">Начальная точка</param>
        /// <param name="b">Конечная точка</param>
        /// <param name="pictureBox">Холст на котором рисуют линию</param>
        /// <param name="extended">Объект расширения для отрисовки</param>
        public DrawFigureLine(Point a, Point b, PictureBox pictureBox, LineProperty extended)
        {
            Line = new FigureLine(a, b);
            Canvas = pictureBox;
            LineProperty = extended;
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
        public LineProperty LineProperty
        {
            get
            {
                return _lineProperty;
            }
            set
            {
                _lineProperty = value;
            }
        }
        /// <summary>
        /// Нарисовать линию
        /// </summary>
        public void Draw()
        {
            if (_pictureBox.Image != null)
            {
                Graphics graphics = Graphics.FromImage(_pictureBox.Image);
                Pen pen = new Pen(LineProperty.LineColor, LineProperty.LineThickness);
                pen.DashStyle = LineProperty.LineType;
                graphics.DrawLine(pen, Line.PointA.X, Line.PointA.Y, Line.PointB.X, Line.PointB.Y);
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
            points.Add(Line.PointA);
            points.Add(Line.PointB);
            return points;
        }
        /// <summary>
        /// Позиция фигуры
        /// </summary>
        public Point Position
        {
            get
            {
                return Line.Position;
            }
            set
            {
                Line.Position = value;
            }
        }
        /// <summary>
        /// Ширина фигуры
        /// </summary>
        public int Width
        {
            get
            {
                return Line.Width;
            }
            set
            {
                Line.Width = value;
            }
        }
        /// <summary>
        /// Высота фигуры
        /// </summary>
        public int Height
        {
            get
            {
                return Line.Height;
            }
            set
            {
                Line.Height = value;
            }
        }
        /// <summary>
        /// Клонировать объект
        /// </summary>
        /// <returns>Новая копия объекта</returns>
        public IDrawable Clone()
        {
            return new DrawFigureLine(Line.PointA, Line.PointB, _pictureBox, LineProperty);
        }
    }
}