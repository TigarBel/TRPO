using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace GRPO
{
    class DrawFigurePolygon : FigurePolygon, IDraw
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
        /// Пустой класс Отрисовки многоугольника
        /// </summary>
        public DrawFigurePolygon() : base()
        {
            Canvas = new PictureBox();
            ExtendedLine = new ExtendedForLine(1, Color.Black, EnumLineType.Solid);
            ExtendedFigure = new ExtendedForFigure(Color.White);
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
        public DrawFigurePolygon(Point position, int width, int height, int countAngle, int phase, PictureBox canvas) : 
            base(position, width, height, countAngle, phase)
        {
            Canvas = canvas;
            ExtendedLine = new ExtendedForLine(1, Color.Black, EnumLineType.Solid);
            ExtendedFigure = new ExtendedForFigure(Color.White);
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
            ExtendedForLine extendedForLine, ExtendedForFigure extendedForFigure) : base(position, width, height, countAngle, phase)
        {
            Canvas = canvas;
            ExtendedLine = extendedForLine;
            ExtendedFigure = extendedForFigure;
        }
        /// <summary>
        /// Класс Отрисовки многоугольника
        /// </summary>
        /// <param name="a">Начальная точка</param>
        /// <param name="b">Конечная точка</param>
        /// <param name="countAngle">Количество углов многоугольника</param>
        /// <param name="phase">Угол поворота многоугольника, в градусах</param>
        /// <param name="canvas">Полотно на котором отрисовывается многоугольник</param>
        public DrawFigurePolygon(Point a, Point b, int countAngle, int phase, PictureBox canvas) : base(a, b, countAngle, phase)
        {
            Canvas = canvas;
            ExtendedLine = new ExtendedForLine(1, Color.Black, EnumLineType.Solid);
            ExtendedFigure = new ExtendedForFigure(Color.White);
        }
        /// <summary>
        /// Класс Отрисовки многоугольника
        /// </summary>
        /// <param name="a">Начальная точка</param>
        /// <param name="b">Конечная точка</param>
        /// <param name="countAngle">Количество углов многоугольника</param>
        /// <param name="phase">Угол поворота многоугольника, в градусах</param>
        /// <param name="canvas">Полотно на котором отрисовывается многоугольник</param>
        /// <param name="extendedForLine">Дополнительные свойства отрисовки линии</param>
        /// <param name="extendedForFigure">Дополнительные свойства отрисовки фигуры</param>
        public DrawFigurePolygon(Point a, Point b, int countAngle, int phase, PictureBox canvas,
            ExtendedForLine extendedForLine, ExtendedForFigure extendedForFigure) : base(a, b, countAngle, phase)
        {
            Canvas = canvas;
            ExtendedLine = extendedForLine;
            ExtendedFigure = extendedForFigure;
        }
        /// <summary>
        /// Класс Отрисовки многоугольника
        /// </summary>
        /// <param name="points">Список существующих точек для многоугольника</param>
        /// <param name="canvas">Полотно на котором отрисовывается многоугольник</param>
        public DrawFigurePolygon(List<Point> points, PictureBox canvas) : base(points)
        {
            Canvas = canvas;
            ExtendedLine = new ExtendedForLine(1, Color.Black, EnumLineType.Solid);
            ExtendedFigure = new ExtendedForFigure(Color.White);
        }
        /// <summary>
        /// Класс Отрисовки многоугольника
        /// </summary>
        /// <param name="points">Список существующих точек для многоугольника</param>
        /// <param name="canvas">Полотно на котором отрисовывается многоугольник</param>
        /// <param name="extendedForLine">Дополнительные свойства отрисовки линии</param>
        /// <param name="extendedForFigure">Дополнительные свойства отрисовки фигуры</param>
        public DrawFigurePolygon(List<Point> points, PictureBox canvas,
            ExtendedForLine extendedForLine, ExtendedForFigure extendedForFigure) : base(points)
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
            if (Points.Count > 2)
            {
                Graphics g = _pictureBox.CreateGraphics();
                for (int i = 1; i < Points.Count; i++)
                {
                    g.DrawLine(new Pen(ExtendedLine.LineColor), Points[i - 1].X, Points[i - 1].Y, Points[i].X, Points[i].Y);
                }
                g.DrawLine(new Pen(ExtendedLine.LineColor), Points[Points.Count - 1].X, Points[Points.Count - 1].Y, Points[0].X, Points[0].Y);
            }
            else
            {
                throw new Exception("Многоугольник пустой, либо не имеет минимум 3 точек отрисовки!");
            }
        }
        /// <summary>
        /// Очистить многоугольник
        /// </summary>
        public void Clear()
        {
            if (Points.Count > 2)
            {
                Graphics g = _pictureBox.CreateGraphics();
                for (int i = 1; i < Points.Count; i++)
                {
                    g.DrawLine(new Pen(Brushes.White), Points[i - 1].X, Points[i - 1].Y, Points[i].X, Points[i].Y);
                }
                g.DrawLine(new Pen(Brushes.White), Points[Points.Count - 1].X, Points[Points.Count - 1].Y, Points[0].X, Points[0].Y);
            }
            else
            {
                throw new Exception("Многоугольник пустой, либо не имеет минимум 3 точек отрисовки!");
            }
        }
        /// <summary>
        /// Взять список точек
        /// </summary>
        /// <returns>Списко точек формирующих фигуру</returns>
        public List<Point> GetPoints()
        {
            return Points;
        }
    }
}
