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
    public partial class FillFigureControl : UserControl
    {
        public FillFigureControl()
        {
            InitializeComponent();
        }

        public ExtendedForFigure Extended
        {
            get
            {
                return new ExtendedForFigure(buttonSelectColorFill.BackColor);
            }
            set
            {
                buttonSelectColorFill.BackColor = value.FillColor;
            }
        }

        private void buttonWhiteColorFill_Click(object sender, EventArgs e)
        {
            buttonSelectColorFill.BackColor = buttonWhiteColorFill.BackColor;
        }

        private void buttonBlackColorFill_Click(object sender, EventArgs e)
        {
            buttonSelectColorFill.BackColor = buttonBlackColorFill.BackColor;
        }

        private void buttonRedColorFill_Click(object sender, EventArgs e)
        {
            buttonSelectColorFill.BackColor = buttonRedColorFill.BackColor;
        }

        private void buttonColorFill_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                buttonSelectColorFill.BackColor = MyDialog.Color;
            }
        }
    }
}
