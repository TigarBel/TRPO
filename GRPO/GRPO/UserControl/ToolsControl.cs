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
    /// <summary>
    /// Пользовательский интерфейс для выбора инструмента
    /// </summary>
    public partial class ToolsControl : UserControl
    {
        private Tools _tools;
        private List<Button> _buttons;

        public delegate void ButtonStateHandler();
        public event ButtonStateHandler ButtonClick;

        public ToolsControl()
        {
            InitializeComponent();

            SelectTool = new Tools(DrawingTools.DrawFigureLine);

            _buttons = new List<Button>();
            _buttons.Add(buttonCursorSelect);
            _buttons.Add(buttonMassSelect);
            _buttons.Add(buttonFigureLine);
            _buttons.Add(buttonFigurePolyline);
            _buttons.Add(buttonFigureRectangle);
            _buttons.Add(buttonFigureCircle);
            _buttons.Add(buttonFigureEllips);
        }

        private void AllButtonBackColorWhite()
        {
            if (_buttons != null)
            {
                foreach (Button button in _buttons)
                {
                    button.BackColor = Color.White;
                }
            }
        }

        private void buttonCursorSelect_Click(object sender, EventArgs e)
        {
            AllButtonBackColorWhite();
            SelectTool = new Tools(DrawingTools.CursorSelect);
            ((Button)sender).BackColor = Color.Black;
        }

        private void buttonMassSelect_Click(object sender, EventArgs e)
        {
            AllButtonBackColorWhite();
            SelectTool = new Tools(DrawingTools.MassSelect);
            ((Button)sender).BackColor = Color.Black;
        }

        private void buttonFigureLine_Click(object sender, EventArgs e)
        {
            AllButtonBackColorWhite();
            SelectTool = new Tools(DrawingTools.DrawFigureLine);
            ((Button)sender).BackColor = Color.Black;
        }

        private void buttonFigurePolyline_Click(object sender, EventArgs e)
        {
            AllButtonBackColorWhite();
            SelectTool = new Tools(DrawingTools.DrawFigurePolyline);
            ((Button)sender).BackColor = Color.Black;
        }

        private void buttonFigureRectangle_Click(object sender, EventArgs e)
        {
            AllButtonBackColorWhite();
            SelectTool = new Tools(DrawingTools.DrawFigureRectangle);
            ((Button)sender).BackColor = Color.Black;
        }

        private void buttonFigureCircle_Click(object sender, EventArgs e)
        {
            AllButtonBackColorWhite();
            SelectTool = new Tools(DrawingTools.DrawFigureCircle);
            ((Button)sender).BackColor = Color.Black;
        }

        private void buttonFigureEllips_Click(object sender, EventArgs e)
        {
            AllButtonBackColorWhite();
            SelectTool = new Tools(DrawingTools.DrawFigureEllipse);
            ((Button)sender).BackColor = Color.Black;
        }
        /// <summary>
        /// Выбранный тип класса IDrawable
        /// </summary>
        public Tools SelectTool
        {
            get
            {
                return _tools;
            }
            set
            {
                _tools = value;
                if (ButtonClick != null) ButtonClick();
                /*!!!*/ChoiceButton();/*!!!*/
            }
        }
        /*!!!*/
        private void ChoiceButton()
        {
            AllButtonBackColorWhite();
            switch (SelectTool.DrawingTools)
            {
                case DrawingTools.CursorSelect:
                    {
                        buttonCursorSelect.BackColor = Color.Black;
                        break;
                    }
                case DrawingTools.MassSelect:
                    {
                        buttonMassSelect.BackColor = Color.Black;
                        break;
                    }
                case DrawingTools.DrawFigureLine:
                    {
                        buttonFigureLine.BackColor = Color.Black;
                        break;
                    }
                case DrawingTools.DrawFigurePolyline:
                    {
                        buttonFigurePolyline.BackColor = Color.Black;
                        break;
                    }
                case DrawingTools.DrawFigureRectangle:
                    {
                        buttonFigureRectangle.BackColor = Color.Black;
                        break;
                    }
                case DrawingTools.DrawFigureCircle:
                    {
                        buttonFigureCircle.BackColor = Color.Black;
                        break;
                    }
                case DrawingTools.DrawFigureEllipse:
                    {
                        buttonFigureEllips.BackColor = Color.Black;
                        break;
                    }
            }
        }
        /*!!!*/
    }
}
