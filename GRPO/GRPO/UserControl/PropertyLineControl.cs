using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace GRPO
{
    public partial class PropertyLineControl : UserControl
    {
        public PropertyLineControl()
        {
            InitializeComponent();
            comboBoxLineType.Items.Add(DashStyle.Solid);
            comboBoxLineType.Items.Add(DashStyle.Dash);
            comboBoxLineType.Items.Add(DashStyle.Dot);
            comboBoxLineType.Items.Add(DashStyle.DashDot);
            comboBoxLineType.Items.Add(DashStyle.DashDotDot);
            comboBoxLineType.SelectedIndex = 0;
        }
        /// <summary>
        /// Свойство линии
        /// </summary>
        public ExtendedForLine Extended
        {
            get
            {
                return new ExtendedForLine(
                    (float)numericUpDownLineThickness.Value,
                    buttonSelectColorLine.BackColor,
                    (DashStyle)comboBoxLineType.Items[comboBoxLineType.SelectedIndex]);
            }
            set
            {
                numericUpDownLineThickness.Value = (decimal)value.LineThickness;
                buttonSelectColorLine.BackColor = value.LineColor;
                comboBoxLineType.SelectedIndex = comboBoxLineType.FindStringExact(value.LineType.ToString());
            }
        }

        private void buttonBlackColorLine_Click(object sender, EventArgs e)
        {
            buttonSelectColorLine.BackColor = buttonBlackColorLine.BackColor;
        }

        private void buttonWhiteColorLine_Click(object sender, EventArgs e)
        {
            buttonSelectColorLine.BackColor = buttonWhiteColorLine.BackColor;
        }

        private void buttonRedColorLine_Click(object sender, EventArgs e)
        {
            buttonSelectColorLine.BackColor = buttonRedColorLine.BackColor;
        }

        private void buttonColorLine_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                buttonSelectColorLine.BackColor = MyDialog.Color;
            }
        }
    }
}
