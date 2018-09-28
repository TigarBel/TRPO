using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GRPO
{
    public partial class MainForm : System.Windows.Forms.Form
    {
        private Point _pointA, _pointB;
        private bool _flagMouseDown;
        private List<Pixel> _pixels = new List<Pixel>();
        private List<Bitmap> _bitmaps = new List<Bitmap>();
        public MainForm()
        {
            InitializeComponent();
            
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

        private void DrawLine(Point pointDown, Point pointUp)
        {
            if (pointUp.X > mainPictureBox.Width) pointUp.X = mainPictureBox.Width - 1;
            if (pointUp.Y > mainPictureBox.Height) pointUp.Y = mainPictureBox.Height - 1;
            if (pointUp.X < 0) pointUp.X = 0;
            if (pointUp.Y < 0) pointUp.Y = 0;

            double x = pointDown.X;
            double y = pointDown.Y;
            int sq = System.Convert.ToInt32(Math.Sqrt((pointDown.X - pointUp.X) * (pointDown.X - pointUp.X) +
                (pointDown.Y - pointUp.Y) * (pointDown.Y - pointUp.Y)));
            double sin = (double)(pointDown.X - pointUp.X) / (double)sq;
            double cos = (double)(pointDown.Y - pointUp.Y) / (double)sq;

            _pixels.Add(new Pixel(pointDown, Color.FromArgb(255, 0, 0)));
            for (int i = 0; i < sq; i++)
            {
                x = x - sin;
                y = y - cos;
                _pixels.Add(new Pixel(new Point(System.Convert.ToInt32(x), System.Convert.ToInt32(y)), Color.FromArgb(255, 0, 0)));
            }
            //for (int x = pointDown.X; x < pointUp.X; x++)
            //{
            //    for (int y = pointDown.Y; y < pointUp.Y; y++)
            //    {
            //        _pixels.Add(new Pixel(new Point(x, y), Color.FromArgb(255, 0, 0)));
            //    }
            //}
            mainPictureBox.Image = new Bitmap(mainPictureBox.Width, mainPictureBox.Height);
            foreach (Pixel pix in _pixels)
            {
                ((Bitmap)mainPictureBox.Image).SetPixel(pix.Point.X, pix.Point.Y, pix.Color);
            }
        }

        private void mainPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            //_flagMouseDown = true;
            _pointA = new Point(e.X, e.Y);
        }

        private void mainPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            //if (_flagMouseDown)
            //{
            //    mainPictureBox.CreateGraphics().DrawLine(new Pen(Brushes.White, 4), _pointA, _pointB);
            //    _pointB = new Point(e.X, e.Y);
            //    mainPictureBox.CreateGraphics().DrawLine(new Pen(Brushes.Red, 4), _pointA, _pointB);
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _pixels.Clear();
            mainPictureBox.Image = new Bitmap(mainPictureBox.Width, mainPictureBox.Height);
        }

        private void mainPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            //Draw(e);

            //mainPictureBox.Image = new Bitmap(mainPictureBox.Width, mainPictureBox.Height);
            //((Bitmap)mainPictureBox.Image).SetPixel(e.X, e.Y, System.Drawing.Color.Red);
            //Color bit = ((Bitmap)mainPictureBox.Image).GetPixel(e.X, e.Y);

            //_flagMouseDown = false;
            _pointB = new Point(e.X, e.Y);
            DrawLine(_pointA, _pointB);
            //mainPictureBox.CreateGraphics().DrawLine(new Pen(Brushes.Red, 4), _pointA, _pointB);
        }
    }
}
