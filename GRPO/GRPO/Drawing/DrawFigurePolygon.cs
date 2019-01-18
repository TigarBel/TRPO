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
    /// Класс отрисовки фигуры - многоугольник
    /// </summary>
    [Serializable]
    class DrawFigurePolygon : IDrawable, ILinePropertyble, IFillPropertyble
    {
        /// <summary>
        /// Объект многоугольника
        /// </summary>
        private FigurePolygon _figurePolygon;
        /// <summary>
        /// Расширение для отрисовки линии
        /// </summary>
        private LineProperty _lineProperty;
        /// <summary>
        /// Расширение для отрисовки фигуры
        /// </summary>
        private FillProperty _fillProperty;
        /// <summary>
        /// Пустой класс Отрисовки многоугольника
        /// </summary>
        public DrawFigurePolygon()
        {
            Polygon = new FigurePolygon();
            LineProperty = new LineProperty();
            FillProperty = new FillProperty();
        }
        /// <summary>
        /// Класс Отрисовки многоугольника
        /// </summary>
        /// <param name="points">Список существующих точек для многоугольника</param>
        /// <param name="extendedForLine">Дополнительные свойства отрисовки линии</param>
        /// <param name="extendedForFigure">Дополнительные свойства отрисовки фигуры</param>
        public DrawFigurePolygon(List<Point> points, LineProperty extendedForLine, FillProperty extendedForFigure)
        {
            Polygon = new FigurePolygon(points);
            LineProperty = extendedForLine;
            FillProperty = extendedForFigure;
        }
        /// <summary>
        /// Класс Отрисовки многоугольника
        /// </summary>
        /// <param name="position">Позиция многоугольника</param>
        /// <param name="width">Ширина многоугольника</param>
        /// <param name="height">Высота многоугольника</param>
        /// <param name="countAngle">Количество углов многоугольника</param>
        /// <param name="phase">Угол поворота многоугольника</param>
        /// <param name="extendedForLine">Дополнительные свойства отрисовки линии</param>
        /// <param name="extendedForFigure">Дополнительные свойства отрисовки фигуры</param>
        public DrawFigurePolygon(Point position, int width, int height, int countAngle, int phase, 
            LineProperty extendedForLine, FillProperty extendedForFigure)
        {
            Polygon = new FigurePolygon(position, width, height, countAngle, phase);
            LineProperty = extendedForLine;
            FillProperty = extendedForFigure;
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
        /// Расширение для отрисовки фигуры
        /// </summary>
        public FillProperty FillProperty
        {
            get
            {
                return _fillProperty;
            }
            set
            {
                _fillProperty = value;
            }
        }
        /// <summary>
        /// Отрисовка последнюю часть многоугольника
        /// </summary>
        /// <param name="pictureBox">Холст на котором рисуют</param>
        public void Draw(PictureBox pictureBox)
        {
            if (pictureBox.Image != null)
            {
                if (Polygon.Points.Count > 2)
                {
                    Graphics graphics = Graphics.FromImage(pictureBox.Image);
                    Pen pen = new Pen(LineProperty.LineColor, LineProperty.LineThickness);
                    pen.DashStyle = LineProperty.LineType;
                    graphics.FillPolygon(new SolidBrush(FillProperty.FillColor), Polygon.Points.ToArray());
                    for (int i = 1; i < Polygon.Points.Count; i++)
                    {
                        graphics.DrawLine(pen, Polygon.Points[i - 1], Polygon.Points[i]);
                    }
                    graphics.DrawLine(pen, Polygon.Points[Polygon.Points.Count - 1], Polygon.Points[0]);
                    graphics.Dispose();
                    pictureBox.Invalidate();
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
            List<Point> points = new List<Point>();
            for (int i = 0; i < Polygon.Points.Count; i++)
            {
                points.Add(Polygon.Points[i]);
            }
            return points;
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
        /// <summary>
        /// Клонировать объект
        /// </summary>
        /// <returns>Новая копия объекта</returns>
        public IDrawable Clone()
        {
            return new DrawFigurePolygon(GetPoints(), LineProperty, FillProperty);
        }
    }
}
