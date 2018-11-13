using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace GRPO
{
    class DrawFigureLine : FigureLine, IDraw
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
        /// Псутой класс Отрисовки линии
        /// </summary>
        public DrawFigureLine() : base()
        {
            Canvas = new PictureBox();
            Extended = new ExtendedForLine(1, Color.Black, EnumLineType.Solid);
        }
        /// <summary>
        /// Класс Отрисовки линии
        /// </summary>
        /// <param name="a">Начальная точка</param>
        /// <param name="b">Конечная точка</param>
        /// <param name="pictureBox">Холст на котором рисуют линию</param>
        public DrawFigureLine(Point a, Point b, PictureBox pictureBox) : base(a, b)
        {
            Canvas = pictureBox;
            Extended = new ExtendedForLine(1, Color.Black, EnumLineType.Solid);
        }
        /// <summary>
        /// Класс Отрисовки линии
        /// </summary>
        /// <param name="a">Начальная точка</param>
        /// <param name="b">Конечная точка</param>
        /// <param name="pictureBox">Холст на котором рисуют линию</param>
        /// <param name="extended">Объект расширения для отрисовки</param>
        public DrawFigureLine(Point a, Point b, PictureBox pictureBox, ExtendedForLine extended) : base(a, b)
        {
            Canvas = pictureBox;
            Extended = extended;
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
            Graphics g = _pictureBox.CreateGraphics();
            g.DrawLine(new Pen(Extended.LineColor), A.X, A.Y, B.X, B.Y);
        }
        /// <summary>
        /// Очистить место
        /// </summary>
        public void Clear()
        {
            Graphics g = _pictureBox.CreateGraphics();
            g.DrawLine(new Pen(Brushes.White), A.X, A.Y, B.X, B.Y);
        }
    }
}