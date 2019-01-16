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
            
            _toolsWithPropertyControl.FigurePropertyChanged += _toolsWithPropertyControl_FigurePropertyChanged;
            _canvasControl.DragProperty += SetProperty;
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
            if (_toolsWithPropertyControl.SelectTool == DrawingTools.CursorSelect && e.Control && e.KeyCode == Keys.C)
            {
                _canvasControl.Copy();
            }
            else if (e.Control && e.KeyCode == Keys.V)
            {
                _canvasControl.Paste();
            }
            else if (_toolsWithPropertyControl.SelectTool == DrawingTools.CursorSelect && e.KeyCode == Keys.Delete)
            {
                _canvasControl.Delete();
            }
            else if (_toolsWithPropertyControl.SelectTool == DrawingTools.CursorSelect && e.Control && e.KeyCode == Keys.X)
            {
                _canvasControl.Cut();
            }
        }
        
        private void _toolsWithPropertyControl_FigurePropertyChanged()
        {
            _canvasControl.SelectTool = _toolsWithPropertyControl.SelectTool;
            _canvasControl.LineProperty = _toolsWithPropertyControl.LineProperty;
            _canvasControl.FillProperty = _toolsWithPropertyControl.FillProperty;
            if(_toolsWithPropertyControl.SelectTool == DrawingTools.CursorSelect)
            {
                if (_canvasControl._interaction != null)
                {
                    if(_canvasControl._interaction.DrawableFigure is ILinePropertyble figureWithLineProperty)
                    {
                        figureWithLineProperty.LineProperty = _toolsWithPropertyControl.LineProperty;
                    }
                    if(_canvasControl._interaction.DrawableFigure is IFillPropertyble figureWithFillProperty)
                    {
                        figureWithFillProperty.FillProperty = _toolsWithPropertyControl.FillProperty;
                    }
                    _canvasControl.RefreshCanvas();
                    _canvasControl._interaction.EnablePoints = false;
                }
            }
        }

        public void SetProperty(IDrawable drawable)
        {
            if (drawable == null)
            {
                _toolsWithPropertyControl.SelectTool = DrawingTools.CursorSelect;
                return;
            }
            if(drawable is ILinePropertyble figureWithLineProperty)
            {
                _toolsWithPropertyControl.LineProperty = figureWithLineProperty.LineProperty;
            }
            if (drawable is IFillPropertyble figureWithFillProperty)
            {
                _toolsWithPropertyControl.FillProperty = figureWithFillProperty.FillProperty;
            }
        }
    }
}