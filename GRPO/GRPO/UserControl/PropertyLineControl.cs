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
        public delegate void LinePropertyEventHandler();
        public event LinePropertyEventHandler LinePropertyChanged;

        public PropertyLineControl()
        {
            InitializeComponent();
            comboBoxLineType.Items.Add(DashStyle.Solid);
            comboBoxLineType.Items.Add(DashStyle.Dash);
            comboBoxLineType.Items.Add(DashStyle.Dot);
            comboBoxLineType.Items.Add(DashStyle.DashDot);
            comboBoxLineType.Items.Add(DashStyle.DashDotDot);
            comboBoxLineType.SelectedIndex = 0;

            buttonSelectColorLine.BackColorChanged += buttonSelectColorLine_BackColorChanged;
            numericUpDownLineThickness.ValueChanged += numericUpDownLineThickness_ValueChanged;
            comboBoxLineType.SelectedValueChanged += comboBoxLineType_SelectedValueChanged;
        }
        /// <summary>
        /// Свойство линии
        /// </summary>
        public LineProperty LineProperty
        {
            get
            {
                return new LineProperty(
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

        private void buttonSelectColorLine_BackColorChanged(object sender, EventArgs e)
        {
            if (LinePropertyChanged != null) LinePropertyChanged();
        }

        private void numericUpDownLineThickness_ValueChanged(object sender, EventArgs e)
        {
            if (LinePropertyChanged != null) LinePropertyChanged();
        }

        private void comboBoxLineType_SelectedValueChanged(object sender, EventArgs e)
        {
            if (LinePropertyChanged != null) LinePropertyChanged();
        }
    }
}
