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

        public MainForm()
        {
            InitializeComponent();
            
            _canvasControl.SetSizeCanvas(640, 480);

            _canvasControl.DragExtended += SetExtended;
        } 

        private void button1_Click(object sender, EventArgs e)
        {
            _canvasControl.ClearCanvas();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //mainPictureBox.Image.Save("111lol.jpg");
        }

        private void buttonAcceptSizePictureBox_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBox1.Text) >= 0 && Convert.ToInt32(textBox1.Text) <= 640 &&
                Convert.ToInt32(textBox2.Text) >= 0 && Convert.ToInt32(textBox1.Text) <= 480)
            {
                _canvasControl.SetSizeCanvas(Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text));
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            //MessageBox.Show(e.KeyCode.ToString());
            if (/*_toolsWithPropertyControl.SelectTool == DrawingTools.CursorSelect && */e.Control && e.KeyCode == Keys.C)
            {
                _canvasControl.Copy();
            }
            else if (/*_toolsWithPropertyControl.SelectTool == DrawingTools.CursorSelect && */e.Control && e.KeyCode == Keys.V)
            {
                _canvasControl.Paste();
            }
            else if (/*_toolsWithPropertyControl.SelectTool == DrawingTools.CursorSelect && */e.KeyCode == Keys.Delete)
            {
                _canvasControl.Delete();
            }
            else if (/*_toolsWithPropertyControl.SelectTool == DrawingTools.CursorSelect && */e.Control && e.KeyCode == Keys.X)
            {
                _canvasControl.Cut();
            }
        }

        public void GetPropertyDrawable()
        {
            _canvasControl.SelectTool = _toolsWithPropertyControl.SelectTool;
            _canvasControl.ExtendedForLine = _toolsWithPropertyControl.ExtendedForLine;
            _canvasControl.ExtendedForFigure = _toolsWithPropertyControl.ExtendedForFigure;
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            _canvasControl.SelectTool = _toolsWithPropertyControl.SelectTool;
            _canvasControl.ExtendedForLine = _toolsWithPropertyControl.ExtendedForLine;
            _canvasControl.ExtendedForFigure = _toolsWithPropertyControl.ExtendedForFigure;
        }

        public void SetExtended(IDrawable drawable)
        {
            if (drawable == null)
            {
                _toolsWithPropertyControl.SelectTool = DrawingTools.CursorSelect;
                return;
            }
            switch (drawable.GetType().Name)
            {
                case "DrawFigureLine":
                    {
                        _toolsWithPropertyControl.SelectTool = DrawingTools.CursorSelect;
                        _toolsWithPropertyControl.ExtendedForLine = ((DrawFigureLine)drawable).Extended;
                        break;
                    }
                case "DrawFigurePolyline":
                    {
                        _toolsWithPropertyControl.SelectTool = DrawingTools.CursorSelect;
                        _toolsWithPropertyControl.ExtendedForLine = ((DrawFigurePolyline)drawable).Extended;
                        break;
                    }
                case "DrawFigurePolygon":
                    {
                        _toolsWithPropertyControl.SelectTool = DrawingTools.CursorSelect;
                        _toolsWithPropertyControl.ExtendedForLine = ((DrawFigurePolygon)drawable).ExtendedLine;
                        _toolsWithPropertyControl.ExtendedForFigure = ((DrawFigurePolygon)drawable).ExtendedFigure;
                        break;
                    }
                case "DrawFigureCircle":
                    {
                        _toolsWithPropertyControl.SelectTool = DrawingTools.CursorSelect;
                        _toolsWithPropertyControl.ExtendedForLine = ((DrawFigureCircle)drawable).ExtendedLine;
                        _toolsWithPropertyControl.ExtendedForFigure = ((DrawFigureCircle)drawable).ExtendedFigure;
                        break;
                    }
                case "DrawFigureEllipse":
                    {
                        _toolsWithPropertyControl.SelectTool = DrawingTools.CursorSelect;
                        _toolsWithPropertyControl.ExtendedForLine = ((DrawFigureEllipse)drawable).ExtendedLine;
                        _toolsWithPropertyControl.ExtendedForFigure = ((DrawFigureEllipse)drawable).ExtendedFigure;
                        break;
                    }
            }
        }
    }
}