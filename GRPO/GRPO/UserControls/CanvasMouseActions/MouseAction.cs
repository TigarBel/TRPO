using GRPO.Drawing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GRPO.UserControls.CanvasMouseActions
{
    class MouseAction
    {
        /// <summary>
        /// Фабрика фигур
        /// </summary>
        private FactoryDrawFigure _factoryDrawFigure = new FactoryDrawFigure();
        /// <summary>
        /// Начальная точка / Точка в момент нажатия кнопки мыши
        /// </summary>
        private Point _pointA;
        /// <summary>
        /// Конечная точка / Точка в момент отжатия кнопки мыши
        /// </summary>
        private Point _pointB;
        /// <summary>
        /// Флаг устанавливаемый при зажатии кнопки мыши
        /// </summary>
        private bool _flagMouseDown;
        /// <summary>
        /// Флаг при создании полифигур
        /// </summary>
        private bool _flagPolyFigure;

        public MouseAction(PictureBox pictureBox)
        {
            Canvas = pictureBox;
        }

        private PictureBox Canvas { get; set; }

        /*private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            _flagMouseDown = true;
            _pointA = new Point(e.X, e.Y);

            if (SelectTool.TypeTools == TypeTools.SimpleFigure)
            {
                Drawables.Add(_factoryDrawFigure.SimpleFigure(_pointA, _pointA, LineProperty, FillProperty, SelectTool.DrawingTools));
            }

            if (SelectTool.TypeTools == TypeTools.PolyFigure)
            {
                if (_flagPolyFigure)
                {
                    List<Point> points = Drawables[Drawables.Count - 1].GetPoints();
                    points.RemoveAt(points.Count - 1);
                    points.Add(_pointA);

                    Drawables.RemoveAt(Drawables.Count - 1);
                    Drawables.Add(_factoryDrawFigure.PolyFigure(points, LineProperty, FillProperty, SelectTool.DrawingTools));
                }
                if (!_flagPolyFigure)
                {
                    List<Point> points = new List<Point>() { new Point(_pointA.X, _pointA.Y), new Point(_pointA.X, _pointA.Y) };
                    Drawables.Add(_factoryDrawFigure.PolyFigure(points, LineProperty, FillProperty, SelectTool.DrawingTools));

                    _flagPolyFigure = true;
                }
                if (e.Button == MouseButtons.Right && _flagPolyFigure)
                {
                    List<Point> points = Drawables[Drawables.Count - 1].GetPoints();
                    points.RemoveAt(points.Count - 1);
                    points.Add(_pointA);

                    Drawables.RemoveAt(Drawables.Count - 1);
                    Drawables.Add(_factoryDrawFigure.PolyFigure(points, LineProperty, FillProperty, SelectTool.DrawingTools));

                    _flagPolyFigure = false;
                }
            }

            if (SelectTool.TypeTools == TypeTools.SelectFigure)
            {
                if (Interaction != null)
                {
                    if (SelectTool.DrawingTools == DrawingTools.CursorSelect)
                    {
                        if (Interaction.EnablePoints)
                        {
                            Interaction.SelectPoint = _pointA;
                        }
                        else if (Interaction.DrawableFigures[0].Position.X > _pointA.X ||
                            Interaction.DrawableFigures[0].Position.Y > _pointA.Y ||
                            Interaction.DrawableFigures[0].Position.X + Interaction.DrawableFigures[0].Width < _pointA.X ||
                            Interaction.DrawableFigures[0].Position.Y + Interaction.DrawableFigures[0].Height < _pointA.Y)
                        {
                            Interaction = null;
                            RefreshCanvas();
                        }

                    }
                    if (SelectTool.DrawingTools == DrawingTools.MassSelect)
                    {
                        List<Point> localPoints = new List<Point>();
                        foreach (IDrawable drawable in Interaction.DrawableFigures)
                        {
                            foreach (Point point in drawable.GetPoints())
                            {
                                localPoints.Add(point);
                            }
                        }

                        if (localPoints.Min(point => point.X) > _pointA.X ||
                            localPoints.Min(point => point.Y) > _pointA.Y ||
                            localPoints.Max(point => point.X) < _pointA.X ||
                            localPoints.Max(point => point.Y) < _pointA.Y)
                        {
                            Interaction = null;
                            RefreshCanvas();
                        }
                    }
                }
            }
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            _pointB = new Point(e.X, e.Y);

            if (_flagMouseDown)
            {
                if (SelectTool.TypeTools == TypeTools.SimpleFigure)
                {
                    Drawables.RemoveAt(Drawables.Count - 1);
                    Drawables.Add(_factoryDrawFigure.SimpleFigure(_pointA, _pointB, LineProperty, FillProperty, SelectTool.DrawingTools));
                    RefreshCanvas();
                }

                if (SelectTool.TypeTools == TypeTools.SelectFigure)
                {
                    if (SelectTool.DrawingTools == DrawingTools.CursorSelect)
                    {
                        if (Interaction != null)
                        {
                            if (Interaction.EnablePoints)
                            {
                                Interaction.ChangePoint(_pointB);
                                RefreshCanvas();
                                _pointA = new Point(e.X, e.Y);
                            }
                            else
                            {
                                int x = Interaction.DrawableFigures[0].Position.X;
                                int y = Interaction.DrawableFigures[0].Position.Y;
                                Interaction.DrawableFigures[0].Position = new Point(x + (_pointB.X - _pointA.X), y + (_pointB.Y - _pointA.Y));
                                RefreshCanvas();
                                _pointA = new Point(e.X, e.Y);
                            }
                        }
                    }
                    if (SelectTool.DrawingTools == DrawingTools.MassSelect)
                    {
                        if (Interaction == null)
                        {
                            RefreshCanvas();
                            DrawFigureRectangle drawFigureRectangle = new DrawFigureRectangle(_pointA, _pointB,
                                new LineProperty(1, Color.Gray, DashStyle.Dash), new FillProperty(Color.Transparent));
                            drawFigureRectangle.Draw(canvas);
                        }
                        else
                        {
                            foreach (IDrawable drawable in Interaction.DrawableFigures)
                            {
                                int x = drawable.Position.X;
                                int y = drawable.Position.Y;
                                drawable.Position = new Point(x + (_pointB.X - _pointA.X), y + (_pointB.Y - _pointA.Y));
                            }
                            RefreshCanvas();
                            _pointA = new Point(e.X, e.Y);
                        }
                    }
                }
            }

            if (_flagPolyFigure)
            {
                if (SelectTool.TypeTools == TypeTools.PolyFigure)
                {
                    List<Point> points = Drawables[Drawables.Count - 1].GetPoints();
                    points.RemoveAt(points.Count - 1);
                    points.Add(_pointB);

                    Drawables.RemoveAt(Drawables.Count - 1);
                    Drawables.Add(_factoryDrawFigure.PolyFigure(points, LineProperty, FillProperty, SelectTool.DrawingTools));
                    RefreshCanvas();
                }
            }
        }

        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            _pointB = new Point(e.X, e.Y);

            if (_flagMouseDown)
            {
                if (SelectTool.TypeTools == TypeTools.SimpleFigure)
                {
                    Drawables.RemoveAt(Drawables.Count - 1);
                    Drawables.Add(_factoryDrawFigure.SimpleFigure(_pointA, _pointB, LineProperty, FillProperty, SelectTool.DrawingTools));
                    RefreshCanvas();
                    if (SaveStep != null) SaveStep();
                }

                if (SelectTool.TypeTools == TypeTools.PolyFigure)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        List<Point> points = Drawables[Drawables.Count - 1].GetPoints();
                        points.Add(_pointB);
                        Drawables.RemoveAt(Drawables.Count - 1);
                        Drawables.Add(_factoryDrawFigure.PolyFigure(points, LineProperty, FillProperty, SelectTool.DrawingTools));
                        RefreshCanvas();
                        if (SaveStep != null) SaveStep();
                    }
                }

                if (SelectTool.TypeTools == TypeTools.SelectFigure)
                {
                    if (SelectTool.DrawingTools == DrawingTools.CursorSelect)
                    {
                        Interaction = null;
                        _pointA = new Point(e.X, e.Y);
                        if (DragProperty != null) DragProperty(null);
                        for (int i = Drawables.Count - 1; i >= 0; i--)
                        {
                            int minX = Drawables[i].GetPoints().Min(point => point.X);
                            int maxX = Drawables[i].GetPoints().Max(point => point.X);
                            int minY = Drawables[i].GetPoints().Min(point => point.Y);
                            int maxY = Drawables[i].GetPoints().Max(point => point.Y);
                            if (_pointA.X >= minX && _pointA.X <= maxX && _pointA.Y >= minY && _pointA.Y <= maxY)
                            {
                                if (DragProperty != null) DragProperty(Drawables[i]);
                                if (e.Button == MouseButtons.Left)
                                {
                                    Interaction = new Interaction(Drawables[i], false);
                                    Interaction.DrawSelcet(canvas);
                                    //if (SaveStep != null) SaveStep();
                                }
                                else if (e.Button == MouseButtons.Right)
                                {
                                    Interaction = new Interaction(Drawables[i], true);
                                    Interaction.DrawSelcet(canvas);
                                    //if (SaveStep != null) SaveStep();
                                }
                                break;
                            }
                        }
                    }
                    if (SelectTool.DrawingTools == DrawingTools.MassSelect)
                    {
                        if (Interaction == null)
                        {
                            List<Point> points = new List<Point>();
                            points.Add(_pointA);
                            points.Add(_pointB);
                            List<IDrawable> localDrawables = new List<IDrawable>();

                            foreach (IDrawable drawable in Drawables)
                            {
                                int X = drawable.GetPoints().Max(point => point.X) -
                                    (drawable.GetPoints().Max(point => point.X) - drawable.GetPoints().Min(point => point.X)) / 2;
                                int Y = drawable.GetPoints().Max(point => point.Y) -
                                    (drawable.GetPoints().Max(point => point.Y) - drawable.GetPoints().Min(point => point.Y)) / 2;

                                if (X >= points.Min(point => point.X) &&
                                    X <= points.Max(point => point.X) &&
                                    Y >= points.Min(point => point.Y) &&
                                    Y <= points.Max(point => point.Y))
                                {
                                    localDrawables.Add(drawable);
                                }
                            }

                            RefreshCanvas();
                            if (localDrawables.Count > 0)
                            {
                                Interaction = new Interaction(localDrawables, false);
                                Interaction.DrawSelcet(canvas);
                                //if (SaveStep != null) SaveStep();
                            }

                            if (Interaction == null)
                            {
                                Interaction = new Interaction();
                                int Xmin = points.Min(point => point.X);
                                int Ymin = points.Min(point => point.Y);
                                int Xmax = points.Max(point => point.X);
                                int Ymax = points.Max(point => point.Y);
                                foreach (IDrawable drawable in Drawables)
                                {
                                    if (Xmin >= drawable.GetPoints().Min(point => point.X) &&
                                        Xmax <= drawable.GetPoints().Max(point => point.X) &&
                                        Ymin >= drawable.GetPoints().Min(point => point.Y) &&
                                        Ymax <= drawable.GetPoints().Max(point => point.Y))
                                    {
                                        Interaction.AddDrawableFigure(drawable);
                                    }
                                }
                                if (Interaction.DrawableFigures.Count == 0)
                                {
                                    Interaction = null;
                                }
                                else
                                {
                                    Interaction.DrawSelcet(canvas);
                                }
                            }
                        }
                        else
                        {
                            Interaction.EnablePoints = false;
                        }
                    }
                    if (SaveStep != null) SaveStep();
                }
                _flagMouseDown = false;
            }
        }
        /// <summary>
        /// Перерисовать фигуры из списка
        /// </summary>
        public void RefreshCanvas()
        {
            canvas.Image = new Bitmap(canvas.Width, canvas.Height);
            foreach (IDrawable drawable in Drawables)
            {
                drawable.Draw(canvas);
            }

            if (Interaction != null)
            {
                Interaction.DrawSelcet(canvas);
            }
        }*/
    }
}
