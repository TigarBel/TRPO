using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GRPO
{
    public enum DrawingTools
    {
        DrawFigureLine = 0,
        DrawFigurePolyline,
        CursorSelect,
        DrawFigureRectangle,
        DrawFigureCircle,
        DrawFigureEllipse,
        DrawFigurePolygon
    }

    public partial class ToolsControl : UserControl
    {
        private DrawingTools _selectTool;
        private List<Button> _buttons = new List<Button>();

        public delegate void ButtonStateHandler();
        public event ButtonStateHandler ButtonClick;

        public ToolsControl()
        {
            InitializeComponent();
            SelectTool = DrawingTools.DrawFigureLine;
            _buttons.Add(buttonFigureLine);
            _buttons.Add(buttonFigurePolyline);
            _buttons.Add(buttonCursorSelect);
            _buttons.Add(buttonFigureRectangle);
            _buttons.Add(buttonFigureCircle);
            _buttons.Add(buttonFigureEllips);
        }

        private void AllButtonBackColorWhite()
        {
            foreach(Button button in _buttons)
            {
                button.BackColor = Color.White;
            }
        }

        private void buttonFigureLine_Click(object sender, EventArgs e)
        {
            AllButtonBackColorWhite();
            SelectTool = DrawingTools.DrawFigureLine;
            ((Button)sender).BackColor = Color.Black;
        }

        private void buttonFigurePolyline_Click(object sender, EventArgs e)
        {
            AllButtonBackColorWhite();
            SelectTool = DrawingTools.DrawFigurePolyline;
            ((Button)sender).BackColor = Color.Black;
        }

        private void buttonCursorSelect_Click(object sender, EventArgs e)
        {
            AllButtonBackColorWhite();
            SelectTool = DrawingTools.CursorSelect;
            ((Button)sender).BackColor = Color.Black;
        }

        private void buttonFigureRectangle_Click(object sender, EventArgs e)
        {
            AllButtonBackColorWhite();
            SelectTool = DrawingTools.DrawFigureRectangle;
            ((Button)sender).BackColor = Color.Black;
        }

        private void buttonFigureCircle_Click(object sender, EventArgs e)
        {
            AllButtonBackColorWhite();
            SelectTool = DrawingTools.DrawFigureCircle;
            ((Button)sender).BackColor = Color.Black;
        }

        private void buttonFigureEllips_Click(object sender, EventArgs e)
        {
            AllButtonBackColorWhite();
            SelectTool = DrawingTools.DrawFigureEllipse;
            ((Button)sender).BackColor = Color.Black;
        }
        /// <summary>
        /// Выбранный тип класса IDrawable
        /// </summary>
        public DrawingTools SelectTool
        {
            get
            {
                return _selectTool;
            }
            set
            {
                _selectTool = value;
                if (ButtonClick != null) ButtonClick();
            }
        }
    }
}
