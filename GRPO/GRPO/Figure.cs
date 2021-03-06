﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace GRPO
{
    class Figure
    {
        /// <summary>
        /// Ширина
        /// </summary>
        private int _width;
        /// <summary>
        /// Высота
        /// </summary>
        private int _height;
        /// <summary>
        /// Расположение по оси X
        /// </summary>
        private int _x;
        /// <summary>
        /// Расположение по оси Y
        /// </summary>
        private int _y;
        /// <summary>
        /// Цвет фигуры
        /// </summary>
        private Color _color;
        /// <summary>
        /// Список пикселей для отрисовки фигуры
        /// </summary>
        private List<Pixel> _pixels = new List<Pixel>();

        private PictureBox _canvas;

        /// <summary>
        /// Пустой объект фигуры
        /// </summary>
        public Figure()
        {
            X = 0;
            Y = 0;
            Width = 0;
            Height = 0;
            Color = new Color();
        }

        public Figure(int x, int y, int width, int height, Color color, PictureBox canvas)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Color = color;
            Canvas = canvas;
        }
        /// <summary>
        /// Расположение фигуры по оси X
        /// </summary>
        public int X
        {
            get
            {
                return _x;
            }
            set
            {
                if (value >= 0 && value < _canvas.Width)
                    _x = value;
                else
                {
                    if (value < 0)
                        _x = 0;
                    if (value >= _canvas.Width)
                        X = _canvas.Width - 1;
                }
            }
        }
        /// <summary>
        /// Расположение фугуры по оси Y
        /// </summary>
        public int Y
        {
            get
            {
                return _y;
            }
            set
            {
                if (value >= 0 && value < _canvas.Height)
                    _y = value;
                else
                {
                    if (value < 0)
                        _y = 0;
                    if (value >= _canvas.Height)
                        _y = _canvas.Height - 1;
                }
            }
        }
        /// <summary>
        /// Ширина фигуры
        /// </summary>
        public int Width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
            }
        }
        /// <summary>
        /// Высота фигуры
        /// </summary>
        public int Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
            }
        }

        public Color Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
            }
        }

        public PictureBox Canvas
        {
            set
            {
                _canvas = value;
            }
        }

        public void Draw(Point pointDown, Point pointUp, Color color)
        {
            if (_canvas == null)
                return;

            if (pointUp.X >= _canvas.Width) pointUp.X = _canvas.Width - 1;
            if (pointUp.Y >= _canvas.Height) pointUp.Y = _canvas.Height - 1;
            if (pointUp.X <= 0) pointUp.X = 0;
            if (pointUp.Y <= 0) pointUp.Y = 0;

            double x = pointDown.X;
            double y = pointDown.Y;
            int sq = System.Convert.ToInt32(Math.Sqrt((pointDown.X - pointUp.X) * (pointDown.X - pointUp.X) +
                (pointDown.Y - pointUp.Y) * (pointDown.Y - pointUp.Y)));
            double sin = (double)(pointDown.X - pointUp.X) / (double)sq;
            double cos = (double)(pointDown.Y - pointUp.Y) / (double)sq;

            _pixels.Add(new Pixel(pointDown, color));
            for (int i = 0; i < sq; i++)
            {
                x = x - sin;
                y = y - cos;
                _pixels.Add(new Pixel(new Point(System.Convert.ToInt32(x), System.Convert.ToInt32(y)), color));
            }

            _canvas.Image = new Bitmap(_canvas.Width, _canvas.Height);
            foreach (Pixel pix in _pixels)
            {
                ((Bitmap)_canvas.Image).SetPixel(pix.Point.X, pix.Point.Y, pix.Color);
            }
        }
    }
}
