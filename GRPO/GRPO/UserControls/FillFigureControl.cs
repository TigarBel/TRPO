using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GRPO.Drawing.Property;

namespace GRPO
{
    public partial class FillFigureControl : UserControl
    {
        public delegate void FillPropertyEventHandler();
        public event FillPropertyEventHandler FillPropertyChanged;

        public FillFigureControl()
        {
            InitializeComponent();

            buttonSelectColorFill.BackColorChanged += buttonSelectColorFill_BackColor;
        }
        /// <summary>
        /// Свойство заливки
        /// </summary>
        public FillProperty FillProperty
        {
            get
            {
                return new FillProperty(buttonSelectColorFill.BackColor);
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

        private void buttonSelectColorFill_BackColor(object sender, EventArgs e)
        {
            FillPropertyChanged();
        }
    }
}
