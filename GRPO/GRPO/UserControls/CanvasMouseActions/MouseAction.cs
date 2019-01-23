using GRPO.Drawing;
using GRPO.Drawing.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GRPO.Drawing.Property;
using GRPO.Commands;
using GRPO.InteractionFrame;
using System.Drawing.Drawing2D;

namespace GRPO.UserControls.CanvasMouseActions
{
    public abstract class MouseAction : UserControl
    {
        public delegate void MouseActionDragProperty(IDrawable drawable);

        public event MouseActionDragProperty DragPropertyEvent;

        public delegate void Refresh();

        public event Refresh RefreshCanvasEvent;

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
        /// <summary>
        /// Флаг для определения зажатия точки
        /// </summary>
        private bool _flagSelectPoint;

        private ControlUnit _controlUnit = new ControlUnit();
        /// <summary>
        /// Буфер для цвета линии фигуры
        /// </summary>
        private List<Color> _lineColors = new List<Color>();
        /// <summary>
        /// Буфер для цвета заливки фигуры
        /// </summary>
        private List<Color> _fillColors = new List<Color>();

        protected bool FlagMouseDown
        {
            get { return _flagMouseDown; }
        }

        protected Point PointB
        {
            get { return _pointB; }
        }
        /// <summary>
        /// Инструмент для рисования
        /// </summary>
        private Tools _selectTool;

        protected Tools SelectToolMouseAction
        {
            get { return _selectTool; }
            set
            {
                _flagMouseDown = false;
                if (_flagPolyFigure)
                {
                    List<Point> points = DrawablesMouseAction[DrawablesMouseAction.Count - 1].Points;
                    points.RemoveAt(points.Count - 1);
                    points.Add(_pointB);

                    DrawablesMouseAction.RemoveAt(DrawablesMouseAction.Count - 1);
                    ControlUnitMouseAction.Drawing(ControlUnitMouseAction.GraphicsEditor.Keywords[0], new Tools(SelectToolMouseAction.DrawingTools),
                        points, LinePropertyMouseAction, FillPropertyMouseAction);
                    _flagPolyFigure = false;
                }

                InteractionMouseAction = null;
                _selectTool = value;
                if (RefreshCanvasEvent != null) RefreshCanvasEvent();
            }
        }

        protected List<IDrawable> DrawablesMouseAction { get; set; }

        protected LineProperty LinePropertyMouseAction { get; set; }

        protected FillProperty FillPropertyMouseAction { get; set; }

        protected ControlUnit ControlUnitMouseAction
        {
            get { return _controlUnit; }
            set { _controlUnit = value; }
        }
        /// <summary>
        /// Объект взаимодействия с нарисованными фигурами
        /// </summary>
        protected Interaction InteractionMouseAction { get; set; }

        protected PictureBox CnavasMouseAction { get; set; }

        protected void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            _flagMouseDown = true;
            _pointA = new Point(e.X, e.Y);

            if (SelectToolMouseAction.TypeTools == TypeTools.SimpleFigure)
            {
                DrawablesMouseAction.Add(_factoryDrawFigure.SimpleFigure(_pointA, _pointA, LinePropertyMouseAction,
                    FillPropertyMouseAction,
                    SelectToolMouseAction.DrawingTools));
            }

            if (SelectToolMouseAction.TypeTools == TypeTools.PolyFigure)
            {/*
                if (_flagPolyFigure)
                {
                    List<Point> points = DrawablesMouseAction[DrawablesMouseAction.Count - 1].Points;
                    points.RemoveAt(points.Count - 1);
                    points.Add(_pointA);

                    DrawablesMouseAction.RemoveAt(DrawablesMouseAction.Count - 1);
                    DrawablesMouseAction.Add(_factoryDrawFigure.PolyFigure(points, LinePropertyMouseAction,
                        FillPropertyMouseAction,
                        SelectToolMouseAction.DrawingTools));
                }

                if (!_flagPolyFigure)
                {
                    List<Point> points = new List<Point>()
                        {new Point(_pointA.X, _pointA.Y), new Point(_pointA.X, _pointA.Y)};
                    DrawablesMouseAction.Add(_factoryDrawFigure.PolyFigure(points, LinePropertyMouseAction,
                        FillPropertyMouseAction,
                        SelectToolMouseAction.DrawingTools));

                    _flagPolyFigure = true;
                }

                if (e.Button == MouseButtons.Right && _flagPolyFigure)
                {
                    List<Point> points = DrawablesMouseAction[DrawablesMouseAction.Count - 1].Points;
                    points.RemoveAt(points.Count - 1);
                    points.Add(_pointA);

                    DrawablesMouseAction.RemoveAt(DrawablesMouseAction.Count - 1);
                    ControlUnitMouseAction.Drawing(ControlUnitMouseAction.GraphicsEditor.Keywords[0],
                        new Tools(SelectToolMouseAction.DrawingTools),
                        points, LinePropertyMouseAction, FillPropertyMouseAction);

                    _flagPolyFigure = false;
                }
            */}

            if (SelectToolMouseAction.TypeTools == TypeTools.SelectFigure)
            {/*
                if (InteractionMouseAction != null)
                {

                    if (SelectToolMouseAction.DrawingTools == DrawingTools.CursorSelect)
                    {
                        if (InteractionMouseAction.EnablePoints)
                        {
                            Checking checking = new Checking();
                            if (checking.GetNumberPoint(_pointA, InteractionMouseAction.DrawableFigures[0],
                                    4) != -1 || _pointA.X == _pointB.X ||
                                _pointA.Y == _pointB.Y)
                            {
                                InteractionMouseAction.SelectPoint = _pointA;
                                _flagSelectPoint = true;
                            }
                            else
                            {
                                InteractionMouseAction = null;
                            }
                        }
                        else if (InteractionMouseAction.DrawableFigures[0].Position.X > _pointA.X &&
                                 InteractionMouseAction.DrawableFigures[0].Position.Y > _pointA.Y &&
                                 InteractionMouseAction.DrawableFigures[0].Position.X +
                                 InteractionMouseAction.DrawableFigures[0].Width <
                                 _pointA.X &&
                                 InteractionMouseAction.DrawableFigures[0].Position.Y +
                                 InteractionMouseAction.DrawableFigures[0].Height <
                                 _pointA.Y)
                        {
                            InteractionMouseAction = null;
                            if (RefreshCanvasEvent != null) RefreshCanvasEvent();
                        }

                    }

                    if (SelectToolMouseAction.DrawingTools == DrawingTools.MassSelect)
                    {
                        List<Point> localPoints = new List<Point>();
                        foreach (IDrawable drawable in InteractionMouseAction.DrawableFigures)
                        {
                            foreach (Point point in drawable.Points)
                            {
                                localPoints.Add(point);
                            }
                        }

                        if (localPoints.Min(point => point.X) > _pointA.X ||
                            localPoints.Min(point => point.Y) > _pointA.Y ||
                            localPoints.Max(point => point.X) < _pointA.X ||
                            localPoints.Max(point => point.Y) < _pointA.Y)
                        {
                            InteractionMouseAction = null;
                            if (RefreshCanvasEvent != null) RefreshCanvasEvent();
                        }
                    }
                }
            */}
        }

        protected void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            _pointB = new Point(e.X, e.Y);

            if (_flagMouseDown)
            {
                if (SelectToolMouseAction.TypeTools == TypeTools.SimpleFigure)
                {
                    DrawablesMouseAction.RemoveAt(DrawablesMouseAction.Count - 1);
                    DrawablesMouseAction.Add(_factoryDrawFigure.SimpleFigure(_pointA, _pointB, LinePropertyMouseAction,
                        FillPropertyMouseAction,
                        SelectToolMouseAction.DrawingTools));
                    if (RefreshCanvasEvent != null) RefreshCanvasEvent();
                }

                if (SelectToolMouseAction.TypeTools == TypeTools.SelectFigure)
                {/*
                    if (SelectToolMouseAction.DrawingTools == DrawingTools.CursorSelect)
                    {
                        if (InteractionMouseAction != null)
                        {
                            if (InteractionMouseAction.EnablePoints)
                            {
                                InteractionMouseAction.ChangePoint(_pointB);
                                if (RefreshCanvasEvent != null)
                                    if (RefreshCanvasEvent != null)
                                        RefreshCanvasEvent();
                            }
                            else if (_pointA.X != _pointB.X || _pointA.Y != _pointB.Y)
                            {
                                DrawEmpty();
                            }
                        }
                    }

                    if (SelectToolMouseAction.DrawingTools == DrawingTools.MassSelect)
                    {
                        if (InteractionMouseAction == null)
                        {
                            if (RefreshCanvasEvent != null) RefreshCanvasEvent();
                            DrawFigureRectangle drawFigureRectangle = new DrawFigureRectangle(_pointA, _pointB,
                                new LineProperty(1, Color.Gray, DashStyle.Dash), new FillProperty(Color.Transparent));
                            drawFigureRectangle.Draw(CnavasMouseAction);
                        }
                        else if (_pointA.X != _pointB.X || _pointA.Y != _pointB.Y)
                        {
                            DrawEmpty();
                        }
                    }
                */}
            }

            if (_flagPolyFigure)
            {
                if (SelectToolMouseAction.TypeTools == TypeTools.PolyFigure)
                {/*
                    List<Point> points = DrawablesMouseAction[DrawablesMouseAction.Count - 1].Points;
                    points.RemoveAt(points.Count - 1);
                    points.Add(_pointB);

                    DrawablesMouseAction.RemoveAt(DrawablesMouseAction.Count - 1);
                    DrawablesMouseAction.Add(_factoryDrawFigure.PolyFigure(points, LinePropertyMouseAction,
                        FillPropertyMouseAction,
                        SelectToolMouseAction.DrawingTools));
                    if (RefreshCanvasEvent != null) RefreshCanvasEvent();
                */}
            }
        }

        protected void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            _pointB = new Point(e.X, e.Y);
            if (_flagMouseDown)
            {
                if (SelectToolMouseAction.TypeTools == TypeTools.SimpleFigure)
                {
                    DrawablesMouseAction.RemoveAt(DrawablesMouseAction.Count - 1);
                    List<Point> points = new List<Point>() {_pointA, _pointB};
                    ControlUnitMouseAction.Drawing(ControlUnitMouseAction.GraphicsEditor.Keywords[0],
                        new Tools(SelectToolMouseAction.DrawingTools),
                        points, LinePropertyMouseAction, FillPropertyMouseAction);
                    if (RefreshCanvasEvent != null) RefreshCanvasEvent();

                }

                if (SelectToolMouseAction.TypeTools == TypeTools.PolyFigure)
                {
                    /*if (e.Button == MouseButtons.Left)
                    {
                        List<Point> points = DrawablesMouseAction[DrawablesMouseAction.Count - 1].Points;
                        points.Add(_pointB);
                        DrawablesMouseAction.RemoveAt(DrawablesMouseAction.Count - 1);
                        DrawablesMouseAction.Add(_factoryDrawFigure.PolyFigure(points, LinePropertyMouseAction,
                            FillPropertyMouseAction,
                            SelectToolMouseAction.DrawingTools));
                        if (RefreshCanvasEvent != null) RefreshCanvasEvent();

                    }*/
                }

                if (SelectToolMouseAction.TypeTools == TypeTools.SelectFigure)
                {/*
                    if (InteractionMouseAction == null)
                    {
                        if (SelectToolMouseAction.DrawingTools == DrawingTools.CursorSelect)
                        {
                            InteractionMouseAction = null;
                            _pointA = new Point(e.X, e.Y);
                            if (DragPropertyEvent != null) DragPropertyEvent(null);
                            for (int i = DrawablesMouseAction.Count - 1; i >= 0; i--)
                            {
                                int minX = DrawablesMouseAction[i].Points.Min(point => point.X);
                                int maxX = DrawablesMouseAction[i].Points.Max(point => point.X);
                                int minY = DrawablesMouseAction[i].Points.Min(point => point.Y);
                                int maxY = DrawablesMouseAction[i].Points.Max(point => point.Y);
                                if (_pointA.X >= minX && _pointA.X <= maxX && _pointA.Y >= minY && _pointA.Y <= maxY)
                                {
                                    if (DragPropertyEvent != null) DragPropertyEvent(DrawablesMouseAction[i]);
                                    if (e.Button == MouseButtons.Left)
                                    {
                                        InteractionMouseAction = new Interaction(DrawablesMouseAction[i], false);
                                        InteractionMouseAction.DrawSelcet(CnavasMouseAction,
                                            InteractionMouseAction.EnablePoints,
                                            InteractionMouseAction.DrawableFigures);
                                    }
                                    else if (e.Button == MouseButtons.Right)
                                    {
                                        InteractionMouseAction = new Interaction(DrawablesMouseAction[i], true);
                                        InteractionMouseAction.DrawPoints(CnavasMouseAction,
                                            InteractionMouseAction.EnablePoints,
                                            InteractionMouseAction.DrawableFigures);
                                    }

                                    break;
                                }
                            }
                        }

                        if (SelectToolMouseAction.DrawingTools == DrawingTools.MassSelect)
                        {

                            List<Point> points = new List<Point>();
                            points.Add(_pointA);
                            points.Add(_pointB);
                            List<IDrawable> localDrawables = new List<IDrawable>();

                            foreach (IDrawable drawable in DrawablesMouseAction)
                            {
                                int X = drawable.Points.Max(point => point.X) -
                                        (drawable.Points.Max(point => point.X) -
                                         drawable.Points.Min(point => point.X)) / 2;
                                int Y = drawable.Points.Max(point => point.Y) -
                                        (drawable.Points.Max(point => point.Y) -
                                         drawable.Points.Min(point => point.Y)) / 2;

                                if (X >= points.Min(point => point.X) &&
                                    X <= points.Max(point => point.X) &&
                                    Y >= points.Min(point => point.Y) &&
                                    Y <= points.Max(point => point.Y))
                                {
                                    localDrawables.Add(drawable);
                                }
                            }

                            if (localDrawables.Count > 0)
                            {
                                InteractionMouseAction = new Interaction(localDrawables, false);
                                InteractionMouseAction.DrawSelcet(CnavasMouseAction,
                                    InteractionMouseAction.EnablePoints, InteractionMouseAction.DrawableFigures);
                                //
                            }

                            if (InteractionMouseAction == null)
                            {
                                InteractionMouseAction = new Interaction();
                                int Xmin = points.Min(point => point.X);
                                int Ymin = points.Min(point => point.Y);
                                int Xmax = points.Max(point => point.X);
                                int Ymax = points.Max(point => point.Y);
                                foreach (IDrawable drawable in DrawablesMouseAction)
                                {
                                    if (Xmin >= drawable.Points.Min(point => point.X) &&
                                        Xmax <= drawable.Points.Max(point => point.X) &&
                                        Ymin >= drawable.Points.Min(point => point.Y) &&
                                        Ymax <= drawable.Points.Max(point => point.Y))
                                    {
                                        InteractionMouseAction.AddDrawableFigure(drawable);
                                    }
                                }

                                if (InteractionMouseAction.DrawableFigures.Count == 0)
                                {
                                    InteractionMouseAction = null;
                                }
                                else
                                {
                                    InteractionMouseAction.DrawSelcet(CnavasMouseAction,
                                        InteractionMouseAction.EnablePoints,
                                        InteractionMouseAction.DrawableFigures);
                                }
                            }
                        }

                    }
                    else
                    {
                        if (_lineColors.Count != 0)
                        {
                            int idLineColor = 0;
                            int idFillColor = 0;
                            foreach (IDrawable drawable in InteractionMouseAction.DrawableFigures)
                            {
                                if (drawable is ILinePropertyble drawableWithLinePropertyble)
                                {
                                    drawableWithLinePropertyble.LineProperty.LineColor = _lineColors[idLineColor];
                                    idLineColor++;
                                }

                                if (drawable is IFillPropertyble drawableWithFillPropertyble)
                                {
                                    drawableWithFillPropertyble.FillProperty.FillColor = _fillColors[idFillColor];
                                    idFillColor++;
                                }
                            }

                            ControlUnitMouseAction.Reconstruction(ControlUnitMouseAction.GraphicsEditor.Keywords[6],
                                InteractionMouseAction.DrawableFigures,
                                GetIndexes(InteractionMouseAction.DrawableFigures, DrawablesMouseAction), _pointA,
                                _pointB);
                            InteractionMouseAction.EnablePoints = false;
                            _lineColors = new List<Color>();
                            _fillColors = new List<Color>();
                        }

                        else if (_flagSelectPoint)
                        {
                            InteractionMouseAction.SelectPoint = _pointB;
                            InteractionMouseAction.ChangePoint(_pointA);

                            ControlUnitMouseAction.Reconstruction(ControlUnitMouseAction.GraphicsEditor.Keywords[7],
                                InteractionMouseAction.DrawableFigures,
                                GetIndexes(InteractionMouseAction.DrawableFigures, DrawablesMouseAction),
                                _pointA, _pointB
                            );
                            if (RefreshCanvasEvent != null) RefreshCanvasEvent();
                            _flagSelectPoint = false;
                        }
                    }

                    if (RefreshCanvasEvent != null) RefreshCanvasEvent();

                */}

                _flagMouseDown = false;
                if (RefreshCanvasEvent != null) RefreshCanvasEvent();
            }
        }

        private void DrawEmpty()
        {
            if (RefreshCanvasEvent != null) RefreshCanvasEvent();
            List<IDrawable> drawables = new List<IDrawable>();
            foreach (IDrawable drawable in InteractionMouseAction.DrawableFigures)
            {
                drawables.Add(drawable.Clone());
                if (drawable is ILinePropertyble drawableWithLinePropertyble)
                {
                    _lineColors.Add(drawableWithLinePropertyble.LineProperty.LineColor);
                    drawableWithLinePropertyble.LineProperty.LineColor = Color.Transparent;
                }

                if (drawable is IFillPropertyble drawableWithFillPropertyble)
                {
                    _fillColors.Add(drawableWithFillPropertyble.FillProperty.FillColor);
                    drawableWithFillPropertyble.FillProperty.FillColor = Color.Transparent;
                }
            }

            foreach (IDrawable drawable in drawables)
            {
                int idLineProperty = 0;
                int idFillProperty = 0;

                if (drawable is ILinePropertyble drawableWithLinePropertyble)
                {
                    drawableWithLinePropertyble.LineProperty.LineColor = _lineColors[idLineProperty];
                    idLineProperty++;
                }

                if (drawable is IFillPropertyble drawableWithFillPropertyble)
                {
                    drawableWithFillPropertyble.FillProperty.FillColor = _fillColors[idFillProperty];
                    idFillProperty++;
                }

                int x = drawable.Position.X;
                int y = drawable.Position.Y;
                drawable.Position = new Point(x + (_pointB.X - _pointA.X), y + (_pointB.Y - _pointA.Y));
                drawable.Draw(CnavasMouseAction);
            }
        }

        protected List<int> GetIndexes(List<IDrawable> localDrawables, List<IDrawable> globalDrawables)
        {
            List<int> indexes = new List<int>();
            int count = 0;
            foreach (IDrawable drawable in localDrawables)
            {
                if (globalDrawables.IndexOf(drawable) != -1)
                    indexes.Add(globalDrawables.IndexOf(drawable));
                else
                {
                    indexes.Add(count + DrawablesMouseAction.Count);
                    count++;
                }

            }

            return indexes;
        }
    }
}
