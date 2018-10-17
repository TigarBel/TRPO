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
        private Point _pointA, _pointB;
        private bool _flagMouseDown;
        private List<Pixel> _pixels = new List<Pixel>();
        private List<Bitmap> _bitmaps = new List<Bitmap>();
        private int _index;
        private int _count;
        public MainForm()
        {
            InitializeComponent();
            FigureLine line1 = new FigureLine(new Point(100, 100), new Point(200, 200));
            FigureLine line2 = new FigureLine(new Point(100, 100), new Point(200, 0));
            FigureLine line3 = new FigureLine(new Point(100, 100), new Point(0, 200));
            FigureLine line4 = new FigureLine(new Point(100, 100), new Point(0, 0));

            FigurePolyline poly1 = new FigurePolyline(new Point(100, 100), new Point(200, 200), false);
            FigurePolyline poly2 = new FigurePolyline(new Point(100, 100), new Point(200, 0), false);
            FigurePolyline poly3 = new FigurePolyline(new Point(100, 100), new Point(0, 200), false);
            FigurePolyline poly4 = new FigurePolyline(new Point(100, 100), new Point(0, 0), false);

            FigurePolygon polygon1 = new FigurePolygon(new Point(100, 100), 200, 200, 90);

            Bitmap btm = new Bitmap(mainPictureBox.Width, mainPictureBox.Height);
            Graphics g = Graphics.FromImage(btm);
            Pen pen = new Pen(Color.Red);
            for (int i = 1; i < polygon1.Points.Count; i++)
            {
                g.DrawLine(pen, polygon1.Points[i-1].X, polygon1.Points[i-1].Y, polygon1.Points[i].X, polygon1.Points[i].Y);
            }
            g.DrawLine(pen, polygon1.Points[polygon1.Points.Count - 1].X, polygon1.Points[polygon1.Points.Count - 1].Y, 
                polygon1.Points[0].X, polygon1.Points[0].Y);
            mainPictureBox.Image = btm;
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
            _index = _pixels.Count;
        }

        private void mainPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (_flagMouseDown)
            {
                _count = _pixels.Count;
                _pointB = new Point(e.X, e.Y);
                DrawLine(_pointA, _pointB, Color.FromArgb(255, 0, 0));
                _count = _pixels.Count - _count;
                _pixels.RemoveRange(_index, _count);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _pixels.Clear();
            mainPictureBox.Image = new Bitmap(mainPictureBox.Width, mainPictureBox.Height);
        }

        private void mainPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            _flagMouseDown = false;
            _pointB = new Point(e.X, e.Y);
            DrawLine(_pointA, _pointB, Color.FromArgb(255, 0, 0));

            _index = _count = 0;
        }
    }
}
