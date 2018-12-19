﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace GRPO
{
    class DrawFigurePolyline : IDrawable
    {
        /// <summary>
        /// Объект полилинии
        /// </summary>
        private FigurePolyline _figurePolyline;
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
        public DrawFigurePolyline()
        {
            Polyline = new FigurePolyline();
            Canvas = new PictureBox();
            Extended = new ExtendedForLine();
        }
        /// <summary>
        /// Класс Отрисовки полилинии
        /// </summary>
        /// <param name="points">Список точек полилиинии</param>
        /// <param name="circular">Тип полилинии</param>
        /// <param name="pictureBox">Полотно на котором рисуем</param>
        /// <param name="extended">Объект расширения для отрисовки</param>
        public DrawFigurePolyline(List<Point> points, bool circular, PictureBox pictureBox, ExtendedForLine extended)
        {
            Polyline = new FigurePolyline(points, circular);
            Canvas = pictureBox;
            Extended = extended;
        }
        /// <summary>
        /// Векторный объект полилинии
        /// </summary>
        public FigurePolyline Polyline
        {
            get
            {
                return _figurePolyline;
            }
            set
            {
                _figurePolyline = value;
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
            if (_pictureBox.Image != null)
            {
                if (Polyline.Points.Count > 1)
                {
                    for (int i = 0; i < Polyline.Points.Count - 1; i++)
                    {
                        Graphics graphics = Graphics.FromImage(_pictureBox.Image);
                        Pen pen = new Pen(Extended.LineColor, Extended.LineThickness);
                        pen.DashStyle = Extended.LineType;
                        graphics.DrawLine(pen, Polyline.Points[i].X, Polyline.Points[i].Y, Polyline.Points[i + 1].X, Polyline.Points[i + 1].Y);
                        graphics.Dispose();
                        _pictureBox.Invalidate();
                    }
                }
                else
                {
                    throw new Exception("Полилиниия пустая, либо не имеет 2 точек отрисовки!");
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
            return Polyline.Points;
        }
        /// <summary>
        /// Позиция фигуры
        /// </summary>
        public Point Position
        {
            get
            {
                return Polyline.Position;
            }
            set
            {
                Polyline.Position = value;
            }
        }
        /// <summary>
        /// Ширина фигуры
        /// </summary>
        public int Width
        {
            get
            {
                return Polyline.WidthPolyline;
            }
            set
            {
                Polyline.WidthPolyline = value;
            }
        }
        /// <summary>
        /// Высота фигуры
        /// </summary>
        public int Height
        {
            get
            {
                return Polyline.HeightPolyline;
            }
            set
            {
                Polyline.HeightPolyline = value;
            }
        }
    }
}
