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
    /// <summary>
    /// Класс отрисовки фигуры - круг
    /// </summary>
    [Serializable]
    class DrawFigureCircle : IDrawable, ILinePropertyble, IFillPropertyble
    {
        /// <summary>
        /// Объект круга
        /// </summary>
        private FigureCircle _figureCircle;
        /// <summary>
        /// Расширение для отрисовки линии
        /// </summary>
        private LineProperty _lineProperty;
        /// <summary>
        /// Расширение для отрисовки фигуры
        /// </summary>
        private FillProperty _fillProperty;
        /// <summary>
        /// Пустой класс Отрисовки круга
        /// </summary>
        public DrawFigureCircle()
        {
            Circle = new FigureCircle();
            LineProperty = new LineProperty();
            FillProperty = new FillProperty();
        }
        /// <summary>
        /// Класс Отрисовки круга
        /// </summary>
        /// <param name="position">Расположение окружности</param>
        /// <param name="radius"> Радиус окружности</param>
        /// <param name="extendedForLine">Дополнительные свойства отрисовки линии</param>
        /// <param name="extendedForFigure">Дополнительные свойства отрисовки фигуры</param>
        public DrawFigureCircle(Point position, int radius, LineProperty extendedForLine, FillProperty extendedForFigure)
        {
            Circle = new FigureCircle(position, radius);
            LineProperty = extendedForLine;
            FillProperty = extendedForFigure;
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
                Graphics graphics = Graphics.FromImage(pictureBox.Image);
                Pen pen = new Pen(LineProperty.LineColor, LineProperty.LineThickness);
                pen.DashStyle = LineProperty.LineType;
                graphics.FillEllipse(new SolidBrush(FillProperty.FillColor), 
                    Circle.Position.X, Circle.Position.Y, Circle.WidthCircle, Circle.HeightCircle);
                graphics.DrawEllipse(pen, Circle.Position.X, Circle.Position.Y, Circle.WidthCircle, Circle.HeightCircle);
                graphics.Dispose();
                pictureBox.Invalidate();
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
        /// <summary>
        /// Клонировать объект
        /// </summary>
        /// <returns>Новая копия объекта</returns>
        public IDrawable Clone()
        {
            return new DrawFigureCircle(Circle.Position, Circle.Radius, LineProperty, FillProperty);
        }
    }
}
