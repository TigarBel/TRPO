using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//throw new ArgumentException("Ширина фигуры не может быть меньше нуля");
namespace GRPO
{
    public partial class MainForm : System.Windows.Forms.Form
    {
        private Point _pointA;
        private Point _pointB;
        private bool _flagMouseDown;
        private List<Pixel> _pixels = new List<Pixel>();
        private List<Bitmap> _bitmaps = new List<Bitmap>();
        private int _index;
        private int _count;

        private List<IDraw> _draws = new List<IDraw>();

        public MainForm()
        {
            InitializeComponent();

            DrawFigureLine drawFigure = new DrawFigureLine();
            _draws.Add(drawFigure);
            _index = _draws.Count - 1;
        }

        private void Draw(MouseEventArgs e)
        {
            _pixels.Add(new Pixel(new Point(e.X, e.Y), Color.FromArgb(255, 0, 0)));
            mainPictureBox.Image = new Bitmap(mainPictureBox.Width, mainPictureBox.Height);
            foreach (Pixel pix in _pixels)
            {
                ((Bitmap)mainPictureBox.Image).SetPixel(pix.Point.X, pix.Point.Y, pix.Color);
            }
        }

        private void DrawLine(Point pointDown, Point pointUp, Color color)
        {
            if (pointUp.X >= mainPictureBox.Width) pointUp.X = mainPictureBox.Width - 1;
            if (pointUp.Y >= mainPictureBox.Height) pointUp.Y = mainPictureBox.Height - 1;
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

            mainPictureBox.Image = new Bitmap(mainPictureBox.Width, mainPictureBox.Height);
            foreach (Pixel pix in _pixels)
            {
                ((Bitmap)mainPictureBox.Image).SetPixel(pix.Point.X, pix.Point.Y, pix.Color);
            }
        }

        private void mainPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            _flagMouseDown = true;
            _pointA = new Point(e.X, e.Y);
            
            if (_draws[_index].GetType() == typeof(DrawFigurePolyline) && radioButtonPolyline.Checked)
            {
                List<Point> points = new List<Point>();
                points = _draws[_index].GetPoints();
                points.Add(_pointA);
                DrawFigurePolyline drawFigure = new DrawFigurePolyline(points, false, mainPictureBox);
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

            
        }

        private void mainPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (_flagMouseDown)
            {
                /*_draws[_index].Clear();
                _draws.Remove(_draws[_index]);*/
                _pointB = new Point(e.X, e.Y);
                
                if (radioButtonLine.Checked)
                {
                    _draws[_index].Clear();
                    _draws.Remove(_draws[_index]);
                    DrawFigureLine drawFigure = new DrawFigureLine(_pointA, _pointB, mainPictureBox);
                    drawFigure.Draw();
                    _draws.Add(drawFigure);
                    _index = _draws.Count - 1;
                }
                if (radioButtonPolyline.Checked)
                {
                    if (_draws[_index].GetType() != typeof(DrawFigurePolyline))
                    {
                        _draws[_index].Clear();
                        _draws.Remove(_draws[_index]);

                        List<Point> points = new List<Point>();
                        points.Add(_pointA);
                        points.Add(_pointB);

                        DrawFigurePolyline drawFigure = new DrawFigurePolyline(points, false, mainPictureBox);

                        drawFigure.Draw();
                        _draws.Add(drawFigure);
                        _index = _draws.Count - 1;
                    }
                }
                if (radioButtonPolygon.Checked)
                {
                    _draws[_index].Clear();
                    _draws.Remove(_draws[_index]);

                    List<Point> squer = new List<Point>();
                    squer.Add(_pointA);
                    squer.Add(new Point(_pointA.X, _pointB.Y));
                    squer.Add(_pointB);
                    squer.Add(new Point(_pointB.X, _pointA.Y));

                    DrawFigurePolygon drawFigure = new DrawFigurePolygon(squer, mainPictureBox);
                    drawFigure.Draw();
                    _draws.Add(drawFigure);
                    _index = _draws.Count - 1;
                }
                if (radioButtonCircle.Checked)
                {
                    _draws[_index].Clear();
                    _draws.Remove(_draws[_index]);
                    DrawFigureCircle drawFigure = new DrawFigureCircle(_pointA,
                        /*(_pointB.X - _pointA.X) / 2, mainPictureBox);*/
                        Convert.ToInt32(Math.Sqrt(Convert.ToDouble(Math.Pow((_pointB.X - _pointA.X), 2) + Math.Pow((_pointB.Y - _pointA.Y), 2)))), mainPictureBox);
                    drawFigure.Draw();
                    _draws.Add(drawFigure);
                    _index = _draws.Count - 1;
                }
                if (radioButtonEllipse.Checked)
                {
                    _draws[_index].Clear();
                    _draws.Remove(_draws[_index]);
                    DrawFigureEllipse drawFigure = new DrawFigureEllipse(_pointA, _pointB.X - _pointA.X, _pointB.Y - _pointA.Y, mainPictureBox);
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
            /*_draws[_index].Clear();
            _draws.Remove(_draws[_index]);*/
            _flagMouseDown = false;
            _pointB = new Point(e.X, e.Y);


            /*_draws.Add(drawFigureLine);
            _index = _draws.Count - 1;
            _draws[_index].Draw();*/

            //foreach (IDraw drawFigure in _draws)
            //{
            //    drawFigure.Draw();
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _draws.Clear();
            mainPictureBox.Image = new Bitmap(mainPictureBox.Width, mainPictureBox.Height);
            DrawFigureLine drawFigure = new DrawFigureLine();
            _draws.Add(drawFigure);
            _index = _draws.Count - 1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //DrawFigureLine drawFigureLine1 = new DrawFigureLine(new Point(100, 20), new Point(200, 20), mainPictureBox);
            //drawFigureLine1.Draw();
            //DrawFigureLine drawFigureLine2 = new DrawFigureLine(new Point(100, 40), new Point(200, 40), mainPictureBox);
            //drawFigureLine2.Draw();
            //DrawFigureLine drawFigureLine3 = new DrawFigureLine(new Point(100, 60), new Point(200, 60), mainPictureBox);
            //drawFigureLine3.Draw();
            for (int i = 0; i < 100; i++)
            {
                DrawFigureLine drawFigureLine = new DrawFigureLine(new Point(10, i + 1), new Point(100, i + 100), mainPictureBox);
                _draws.Add(drawFigureLine);
                _draws[i].Draw();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _pointA = new Point(100, 100);
            _pointB = new Point(200, 200);
            List<Point> squer = new List<Point>();
            squer.Add(_pointA);
            squer.Add(new Point(_pointA.X, _pointB.Y));
            squer.Add(_pointB);
            squer.Add(new Point(_pointB.X, _pointA.Y));

            DrawFigurePolyline polyline = new DrawFigurePolyline(squer, false, mainPictureBox);
            polyline.Draw();
        }

        private void radioButtonLine_Click(object sender, EventArgs e)
        {
            radioButtonLine.Checked = true;
            radioButtonPolyline.Checked = false;
            radioButtonPolygon.Checked = false;
            radioButtonCircle.Checked = false;
            radioButtonEllipse.Checked = false;
        }

        private void radioButtonPolyline_Click(object sender, EventArgs e)
        {
            radioButtonPolyline.Checked = true;
            radioButtonLine.Checked = false;
            radioButtonPolygon.Checked = false;
            radioButtonCircle.Checked = false;
            radioButtonEllipse.Checked = false;
        }

        private void radioButtonPolygon_Click(object sender, EventArgs e)
        {
            radioButtonPolygon.Checked = true;
            radioButtonLine.Checked = false;
            radioButtonPolyline.Checked = false;
            radioButtonCircle.Checked = false;
            radioButtonEllipse.Checked = false;
        }

        private void radioButtonCircle_Click(object sender, EventArgs e)
        {
            radioButtonCircle.Checked = true;
            radioButtonLine.Checked = false;
            radioButtonPolyline.Checked = false;
            radioButtonPolygon.Checked = false;
            radioButtonEllipse.Checked = false;
        }

        private void radioButtonEllipse_Click(object sender, EventArgs e)
        {
            radioButtonEllipse.Checked = true;
            radioButtonLine.Checked = false;
            radioButtonPolyline.Checked = false;
            radioButtonPolygon.Checked = false;
            radioButtonCircle.Checked = false;
        }
    }
}
