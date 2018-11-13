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

        public void Draw()
        {

        }

        public void Clear()
        {

        }
    }
}
