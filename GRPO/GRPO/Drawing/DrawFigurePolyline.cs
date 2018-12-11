using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace GRPO
{
    class DrawFigurePolyline : FigurePolyline, IDraw
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
        /// Псутой класс Отрисовки полилинии
        /// </summary>
        public DrawFigurePolyline() : base()
        {
            Canvas = new PictureBox();
            Extended = new ExtendedForLine(1, Color.Black, EnumLineType.Solid);
        }
        /// <summary>
        /// Класс Отрисовки полилинии
        /// </summary>
        /// <param name="a">Начальная точка</param>
        /// <param name="b">Точка продолжения</param>
        /// <param name="circular">Тип полилинии</param>
        /// <param name="pictureBox">Полотно на котором рисуем</param>
        public DrawFigurePolyline(List<Point> points, bool circular, PictureBox pictureBox) : base(points, circular)
        {
            Canvas = pictureBox;
            Extended = new ExtendedForLine(1, Color.Black, EnumLineType.Solid);
        }
        /// <summary>
        /// Класс Отрисовки полилинии
        /// </summary>
        /// <param name="a">Начальная точка</param>
        /// <param name="b">Точка продолжения</param>
        /// <param name="circular">Тип полилинии</param>
        /// <param name="pictureBox">Полотно на котором рисуем</param>
        /// <param name="extended">Объект расширения для отрисовки</param>
        public DrawFigurePolyline(List<Point> points, bool circular, PictureBox pictureBox, ExtendedForLine extended) : base(points, circular)
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
        /// Отрисовка последнюю часть полилинии
        /// </summary>
        public void Draw()
        {
            if (Points.Count > 1)
            {
                for (int i = 0; i < Points.Count - 1; i++)
                {
                    Graphics g = _pictureBox.CreateGraphics();
                    g.DrawLine(new Pen(Extended.LineColor), Points[i].X, Points[i].Y, Points[i + 1].X, Points[i + 1].Y);
                }
            }
            else
            {
                throw new Exception("Полилиниия пустая, либо не имеет 2 точек отрисовки!");
            }
        }
        /// <summary>
        /// Очистить полилинию
        /// </summary>
        public void Clear()
        {
            if (Points.Count > 1)
            {
                for (int i = 0; i < Points.Count - 1; i++)
                {
                    Graphics g = _pictureBox.CreateGraphics();
                    g.DrawLine(new Pen(Brushes.White), Points[i].X, Points[i].Y, Points[i + 1].X, Points[i + 1].Y);
                }
            }
            else
            {
                throw new Exception("Полилиниия пустая, либо не имеет 2 точек отрисовки!");
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
