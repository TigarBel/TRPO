using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GRPO
{
    public partial class MainForm : System.Windows.Forms.Form
    {
        private Point _pointA;
        private Point _pointB;
        private bool _flagMouseDown;
        private List<Bitmap> _bitmaps = new List<Bitmap>();
        private int _index;
        private Image _backStep;
        private List<IDrawable> _draws = new List<IDrawable>();
        private int _indexSelectFigure;
        private IDrawable _drawableBufer;

        public MainForm()
        {
            InitializeComponent();

            mainPictureBox.Image = new Bitmap(mainPictureBox.Width, mainPictureBox.Height);
            _backStep = new Bitmap(mainPictureBox.Image);

            DrawFigureLine drawFigure = new DrawFigureLine();
            _draws.Add(drawFigure);
            _index = _draws.Count - 1;
        }

        private void mainPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (_toolsWithPropertyControl.SelectTool != DrawingTools.CursorSelect)
            {
                _flagMouseDown = true;
                _pointA = new Point(e.X, e.Y);

                if (_draws[_index].GetType() == typeof(DrawFigurePolyline) && _toolsWithPropertyControl.SelectTool == DrawingTools.DrawFigurePolyline)
                {
                    List<Point> points = new List<Point>();
                    points = _draws[_index].GetPoints();
                    points.Add(_pointA);
                    DrawFigurePolyline drawFigure = new DrawFigurePolyline(points, false, mainPictureBox, _toolsWithPropertyControl.ExtendedForLine);
                    drawFigure.Draw();
                    _draws.Add(drawFigure);
                    _index = _draws.Count - 1;
                }
                else
                {
                    DrawFigureLine drawFigure = new DrawFigureLine();
                    _draws.Add(drawFigure);
                    _index = _draws.Count - 1;
                }

                _backStep = new Bitmap(mainPictureBox.Image);
            }

            if (_toolsWithPropertyControl.SelectTool == DrawingTools.CursorSelect && _draws.Count > 1) 
            {
                mainPictureBox.Image = new Bitmap(_backStep);
                _pointA = new Point(e.X, e.Y);
                for(int i = _draws.Count - 1; i > 0; i--)
                {
                    List<Point> points = _draws[i].GetPoints();
                    int minX = points.Min(point => point.X);
                    int maxX = points.Max(point => point.X);
                    int minY = points.Min(point => point.Y);
                    int maxY = points.Max(point => point.Y);

                    if (_pointA.X >= minX && _pointA.X <= maxX && _pointA.Y >= minY && _pointA.Y <= maxY)
                    {
                        _indexSelectFigure = i;
                        List<Point> pointsForSquer = new List<Point>();
                        pointsForSquer.Add(new Point(minX, minY));
                        pointsForSquer.Add(new Point(maxX, minY));
                        pointsForSquer.Add(new Point(maxX, maxY));
                        pointsForSquer.Add(new Point(minX, maxY));
                        DrawFigurePolygon polygon = new DrawFigurePolygon(pointsForSquer, mainPictureBox,
                            new ExtendedForLine(1, Color.Black, DashStyle.Dash), new ExtendedForFigure(Color.Transparent));
                        polygon.Draw();
                        if (_draws[i] is DrawFigureLine) _toolsWithPropertyControl.ExtendedForLine = ((DrawFigureLine)_draws[i]).Extended;
                        if (_draws[i] is DrawFigurePolyline) _toolsWithPropertyControl.ExtendedForLine = ((DrawFigurePolyline)_draws[i]).Extended;
                        if (_draws[i] is DrawFigurePolygon)
                        {
                            _toolsWithPropertyControl.ExtendedForLine = ((DrawFigurePolygon)_draws[i]).ExtendedLine;
                            _toolsWithPropertyControl.ExtendedForFigure = ((DrawFigurePolygon)_draws[i]).ExtendedFigure;
                        }
                        if (_draws[i] is DrawFigureCircle)
                        {
                            _toolsWithPropertyControl.ExtendedForLine = ((DrawFigureCircle)_draws[i]).ExtendedLine;
                            _toolsWithPropertyControl.ExtendedForFigure = ((DrawFigureCircle)_draws[i]).ExtendedFigure;
                        }
                        if (_draws[i] is DrawFigureEllipse)
                        {
                            _toolsWithPropertyControl.ExtendedForLine = ((DrawFigureEllipse)_draws[i]).ExtendedLine;
                            _toolsWithPropertyControl.ExtendedForFigure = ((DrawFigureEllipse)_draws[i]).ExtendedFigure;
                        }
                        break;
                    }
                    else
                    {
                        _toolsWithPropertyControl.HidingUserControl();
                    }
                }
            }
        }

        private void mainPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (_flagMouseDown)
            {
                /*_draws[_index].Clear();
                _draws.Remove(_draws[_index]);*/
                _pointB = new Point(e.X, e.Y);
                mainPictureBox.Image = new Bitmap(_backStep);

                if (_toolsWithPropertyControl.SelectTool == DrawingTools.DrawFigureLine)
                {
                    _draws.Remove(_draws[_index]);
                    DrawFigureLine drawFigure = new DrawFigureLine(_pointA, _pointB, mainPictureBox, _toolsWithPropertyControl.ExtendedForLine);
                    drawFigure.Draw();
                    _draws.Add(drawFigure);
                    _index = _draws.Count - 1;
                }
                if (_toolsWithPropertyControl.SelectTool == DrawingTools.DrawFigurePolyline)
                {
                    if (_draws[_index].GetType() != typeof(DrawFigurePolyline))
                    {
                        _draws.Remove(_draws[_index]);

                        List<Point> points = new List<Point>();
                        points.Add(_pointA);
                        points.Add(_pointB);

                        DrawFigurePolyline drawFigure = new DrawFigurePolyline(points, false, mainPictureBox, _toolsWithPropertyControl.ExtendedForLine);

                        drawFigure.Draw();
                        _draws.Add(drawFigure);
                        _index = _draws.Count - 1;
                    }
                }
                if (_toolsWithPropertyControl.SelectTool == DrawingTools.DrawFigurePolygon)
                {
                    _draws.Remove(_draws[_index]);

                    List<Point> squer = new List<Point>();
                    squer.Add(_pointA);
                    squer.Add(new Point(_pointA.X, _pointB.Y));
                    squer.Add(_pointB);
                    squer.Add(new Point(_pointB.X, _pointA.Y));

                    DrawFigurePolygon drawFigure = new DrawFigurePolygon(squer, mainPictureBox, _toolsWithPropertyControl.ExtendedForLine,
                        _toolsWithPropertyControl.ExtendedForFigure);
                    drawFigure.Draw();
                    _draws.Add(drawFigure);
                    _index = _draws.Count - 1;
                }
                if (_toolsWithPropertyControl.SelectTool == DrawingTools.DrawFigureCircle)
                {
                    _draws.Remove(_draws[_index]);
                    DrawFigureCircle drawFigure = new DrawFigureCircle(_pointA,
                        Convert.ToInt32(Math.Sqrt(Convert.ToDouble(Math.Pow((_pointB.X - _pointA.X), 2) + Math.Pow((_pointB.Y - _pointA.Y), 2)))),
                        mainPictureBox, _toolsWithPropertyControl.ExtendedForLine, _toolsWithPropertyControl.ExtendedForFigure);
                    drawFigure.Draw();
                    _draws.Add(drawFigure);
                    _index = _draws.Count - 1;
                }
                if (_toolsWithPropertyControl.SelectTool == DrawingTools.DrawFigureEllipse)
                {
                    _draws.Remove(_draws[_index]);
                    DrawFigureEllipse drawFigure = new DrawFigureEllipse(_pointA, _pointB.X - _pointA.X, _pointB.Y - _pointA.Y, mainPictureBox,
                        _toolsWithPropertyControl.ExtendedForLine, _toolsWithPropertyControl.ExtendedForFigure);
                    drawFigure.Draw();
                    _draws.Add(drawFigure);
                    _index = _draws.Count - 1;
                }
                
                /*_draws.Add(drawFigureLine);
                _index = _draws.Count - 1;*/

                //foreach (IDraw drawFigure in _draws)
                //{
                //    drawFigure.Draw();
                //}
            }
        }

        private void mainPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (_flagMouseDown)
            {
                _flagMouseDown = false;
                _pointB = new Point(e.X, e.Y);
                mainPictureBox.Image = new Bitmap(_backStep);

                if (_toolsWithPropertyControl.SelectTool == DrawingTools.DrawFigureLine)
                {
                    _draws.Remove(_draws[_index]);
                    DrawFigureLine drawFigure = new DrawFigureLine(_pointA, _pointB, mainPictureBox, _toolsWithPropertyControl.ExtendedForLine);
                    drawFigure.Draw();
                    _draws.Add(drawFigure);
                    _index = _draws.Count - 1;
                }
                if (_toolsWithPropertyControl.SelectTool == DrawingTools.DrawFigurePolyline)
                {
                    if (_draws[_index].GetType() != typeof(DrawFigurePolyline))
                    {
                        _draws.Remove(_draws[_index]);

                        List<Point> points = new List<Point>();
                        points.Add(_pointA);
                        points.Add(_pointB);

                        DrawFigurePolyline drawFigure = new DrawFigurePolyline(points, false, mainPictureBox, _toolsWithPropertyControl.ExtendedForLine);

                        drawFigure.Draw();
                        _draws.Add(drawFigure);
                        _index = _draws.Count - 1;
                    }
                }
                if (_toolsWithPropertyControl.SelectTool == DrawingTools.DrawFigurePolygon)
                {
                    _draws.Remove(_draws[_index]);

                    List<Point> squer = new List<Point>();
                    squer.Add(_pointA);
                    squer.Add(new Point(_pointA.X, _pointB.Y));
                    squer.Add(_pointB);
                    squer.Add(new Point(_pointB.X, _pointA.Y));

                    DrawFigurePolygon drawFigure = new DrawFigurePolygon(squer, mainPictureBox, _toolsWithPropertyControl.ExtendedForLine,
                        _toolsWithPropertyControl.ExtendedForFigure);
                    drawFigure.Draw();
                    _draws.Add(drawFigure);
                    _index = _draws.Count - 1;
                }
                if (_toolsWithPropertyControl.SelectTool == DrawingTools.DrawFigureCircle)
                {
                    _draws.Remove(_draws[_index]);
                    DrawFigureCircle drawFigure = new DrawFigureCircle(_pointA,
                        Convert.ToInt32(Math.Sqrt(Convert.ToDouble(Math.Pow((_pointB.X - _pointA.X), 2) + Math.Pow((_pointB.Y - _pointA.Y), 2)))),
                        mainPictureBox, _toolsWithPropertyControl.ExtendedForLine, _toolsWithPropertyControl.ExtendedForFigure);
                    drawFigure.Draw();
                    _draws.Add(drawFigure);
                    _index = _draws.Count - 1;
                }
                if (_toolsWithPropertyControl.SelectTool == DrawingTools.DrawFigureEllipse)
                {
                    _draws.Remove(_draws[_index]);
                    DrawFigureEllipse drawFigure = new DrawFigureEllipse(_pointA, _pointB.X - _pointA.X, _pointB.Y - _pointA.Y, mainPictureBox,
                        _toolsWithPropertyControl.ExtendedForLine, _toolsWithPropertyControl.ExtendedForFigure);
                    drawFigure.Draw();
                    _draws.Add(drawFigure);
                    _index = _draws.Count - 1;
                }

                //foreach (IDraw drawFigure in _draws)
                //{
                //    drawFigure.Draw();
                //}
                _backStep = new Bitmap(mainPictureBox.Image);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _draws.Clear();
            mainPictureBox.Image = new Bitmap(mainPictureBox.Width, mainPictureBox.Height);
            _backStep = new Bitmap(mainPictureBox.Image);
            DrawFigureLine drawFigure = new DrawFigureLine();
            _draws.Add(drawFigure);
            _index = _draws.Count - 1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                DrawFigureLine drawFigureLine = new DrawFigureLine(new Point(10, i + 1), new Point(100, i + 100), mainPictureBox, new ExtendedForLine());
                _draws.Add(drawFigureLine);
                _draws[i + 1].Draw();
            }

            DrawFigureLine drawFigureLine1 = new DrawFigureLine(new Point(190, 190), new Point(200, 200), mainPictureBox, new ExtendedForLine());
            drawFigureLine1.Draw();
            DrawFigureLine drawFigureLine2 = new DrawFigureLine(new Point(510, 190), new Point(500, 200), mainPictureBox, new ExtendedForLine());
            drawFigureLine2.Draw();
            DrawFigureLine drawFigureLine3 = new DrawFigureLine(new Point(510, 310), new Point(500, 300), mainPictureBox, new ExtendedForLine());
            drawFigureLine3.Draw();
            DrawFigureLine drawFigureLine4 = new DrawFigureLine(new Point(190, 310), new Point(200, 300), mainPictureBox, new ExtendedForLine());
            drawFigureLine4.Draw();
            DrawFigurePolygon drawFigurePolygon = new DrawFigurePolygon(new Point(200, 200), 300, 100, 4, 45, mainPictureBox,
                new ExtendedForLine(), new ExtendedForFigure());
            drawFigurePolygon.Draw();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //mainPictureBox.Image.Save("111lol.jpg");
            var count = _draws.Count;
        }

        private void buttonAcceptSizePictureBox_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBox1.Text) >= 0 && Convert.ToInt32(textBox1.Text) <= 640 &&
                Convert.ToInt32(textBox2.Text) >= 0 && Convert.ToInt32(textBox1.Text) <= 480)
            {
                mainPictureBox.Location = new Point(317, 65);
                mainPictureBox.Size = new Size(Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text));
            }
        }

        private void buttonCopyFigure_Click(object sender, EventArgs e)
        {
            if (_toolsWithPropertyControl.SelectTool == DrawingTools.CursorSelect)
            {
                _drawableBufer = _draws[_indexSelectFigure];
            }
        }

        private void buttonPasteFigure_Click(object sender, EventArgs e)
        {
            if (_toolsWithPropertyControl.SelectTool == DrawingTools.CursorSelect && _drawableBufer != null)
            {
                mainPictureBox.Image = new Bitmap(_backStep);

                switch (_drawableBufer.GetType().Name)
                {
                    case "DrawFigureLine":
                        {
                            DrawFigureLine drawFigureLine = new DrawFigureLine(((DrawFigureLine)_drawableBufer).Line.A, 
                                ((DrawFigureLine)_drawableBufer).Line.B, mainPictureBox, ((DrawFigureLine)_drawableBufer).Extended);
                            drawFigureLine.Line.A = new Point(drawFigureLine.Line.A.X - 100, drawFigureLine.Line.A.Y);
                            drawFigureLine.Line.B = new Point(drawFigureLine.Line.B.X - 100, drawFigureLine.Line.B.Y);
                            drawFigureLine.Draw();
                            _draws.Add(drawFigureLine);
                            break;
                        }
                    case "DrawFigurePolyline":
                        {
                            //DrawFigurePolyline drawFigurePolyline = (DrawFigurePolyline)_drawableBufer;
                            DrawFigurePolyline drawFigurePolyline = new DrawFigurePolyline(((DrawFigurePolyline)_drawableBufer).Polyline.Points,
                                ((DrawFigurePolyline)_drawableBufer).Polyline.Circular, mainPictureBox, ((DrawFigurePolyline)_drawableBufer).Extended);
                            for (int i = 0; i < ((DrawFigurePolyline)drawFigurePolyline).Polyline.Points.Count - 1; i++)
                            {
                                drawFigurePolyline.Polyline.Points[i] = new Point(
                                    drawFigurePolyline.Polyline.Points[i].X - 100,
                                    drawFigurePolyline.Polyline.Points[i].Y);
                            }
                            drawFigurePolyline.Draw();
                            _draws.Add(drawFigurePolyline);
                            break;
                        }
                    case "DrawFigurePolygon":
                        {
                            DrawFigurePolygon drawFigurePolygon = (DrawFigurePolygon)_drawableBufer;
                            drawFigurePolygon.Polygon.Position = new Point(10, 10);
                            drawFigurePolygon.Draw();
                            _draws.Add(drawFigurePolygon);
                            break;
                        }
                    case "DrawFigureCircle":
                        {
                            DrawFigureCircle drawFigureCircle = new DrawFigureCircle(((DrawFigureCircle)_drawableBufer).Position,
                                ((DrawFigureCircle)_drawableBufer).Width / 2, mainPictureBox, ((DrawFigureCircle)_drawableBufer).ExtendedLine,
                                ((DrawFigureCircle)_drawableBufer).ExtendedFigure);
                            drawFigureCircle.Circle.Position = new Point(10, 10);
                            drawFigureCircle.Draw();
                            _draws.Add(drawFigureCircle);
                            break;
                        }
                    case "DrawFigureEllipse":
                        {

                            DrawFigureEllipse drawFigureEllipse = (DrawFigureEllipse)_drawableBufer;
                            drawFigureEllipse.Ellipse.Position = new Point(10, 10);
                            drawFigureEllipse.Draw();
                            _draws.Add(drawFigureEllipse);
                            break;
                        }
                }

                _backStep = new Bitmap(mainPictureBox.Image);
            }
        }

        private void buttonCutFigure_Click(object sender, EventArgs e)
        {
            if (_toolsWithPropertyControl.SelectTool == DrawingTools.CursorSelect && _drawableBufer != null)
            {
                _drawableBufer = _draws[_indexSelectFigure];

                _draws.Remove(_draws[_indexSelectFigure]);
                mainPictureBox.Image = new Bitmap(mainPictureBox.Width, mainPictureBox.Height);
                for (int i = 1; i <= _draws.Count - 1; i++)
                {
                    _draws[i].Draw();
                }
                _backStep = new Bitmap(mainPictureBox.Image);
                _index--;
            }
        }

        private void buttonDeleteFigure_Click(object sender, EventArgs e)
        {
            if (_toolsWithPropertyControl.SelectTool == DrawingTools.CursorSelect)
            {
                _draws.Remove(_draws[_indexSelectFigure]);
                mainPictureBox.Image = new Bitmap(mainPictureBox.Width, mainPictureBox.Height);
                for (int i = 1; i <= _draws.Count - 1; i++)
                {
                    _draws[i].Draw();
                }
                _backStep = new Bitmap(mainPictureBox.Image);
                _index--;
            }
        }

        private void MainForm_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            MessageBox.Show(e.KeyCode.ToString());
        }

        /*if (_flagSelectFigure && e.KeyCode == Keys.C && e.Control)
            {
                _draws.Add(_draws[_indexSelectFigure]);
            }
            else if (e.KeyCode == Keys.V && e.Control)
            {
                DrawFigureLine line = new DrawFigureLine(new Point(1, 1), new Point(100, 100), mainPictureBox,
                    new ExtendedForLine(1, Color.Blue, DashStyle.Dash));
                line.Draw();
            }*/
    }
}
