﻿using System;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using GRPO.Drawing.Property;

namespace GRPO
{
    /// <summary>
    /// Пользовательский элемент свойства линии
    /// </summary>
    public partial class PropertyLineControl : UserControl
    {
        /// <summary>
        /// Делегат для события по изменению свойств
        /// </summary>
        public delegate void LinePropertyEventHandler();

        /// <summary>
        /// Событие при изменении свойств
        /// </summary>
        public event LinePropertyEventHandler LinePropertyChanged;

        /// <summary>
        /// Список выбранных цветов
        /// </summary>
        private int[] colorInts = new int[16];

        /// <summary>
        /// Инциализация класса
        /// </summary>
        public PropertyLineControl()
        {
            InitializeComponent();
            LineProperty = new LineProperty(); /*добавлено*/
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
                    (float) numericUpDownLineThickness.Value,
                    buttonSelectColorLine.BackColor,
                    (DashStyle) comboBoxLineType.Items[comboBoxLineType.SelectedIndex]);
            }
            set
            {
                numericUpDownLineThickness.Value = (decimal) value.LineThickness;
                buttonSelectColorLine.BackColor = value.LineColor;
                comboBoxLineType.SelectedIndex = comboBoxLineType.FindStringExact(value.LineType.ToString());
            }
        }
        /// <summary>
        /// Событие при нажатии кнопки выбора цвета
        /// </summary>
        /// <param name="sender">объект(кнопка)</param>
        /// <param name="e">событие(нажатие)</param>
        private void buttonSelectColorLine_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            MyDialog.CustomColors = colorInts;
            MyDialog.Color = buttonSelectColorLine.BackColor;
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                buttonSelectColorLine.BackColor = MyDialog.Color;
                colorInts = MyDialog.CustomColors;
            }
        }

        /// <summary>
        /// Событие при изменении заднего фона
        /// </summary>
        /// <param name="sender">объект(кнопка)</param>
        /// <param name="e">событие(изменение заднего фона)</param>
        private void buttonSelectColorLine_BackColorChanged(object sender, EventArgs e)
        {
            if (LinePropertyChanged != null) LinePropertyChanged();
        }

        /// <summary>
        /// Событие при изменении значения толщины линии
        /// </summary>
        /// <param name="sender">объект(числовой вверх вниз)</param>
        /// <param name="e">событие(изменение значения)</param>
        private void numericUpDownLineThickness_ValueChanged(object sender, EventArgs e)
        {
            if (LinePropertyChanged != null) LinePropertyChanged();
        }

        /// <summary>
        /// Событие при изменении значения типа линии
        /// </summary>
        /// <param name="sender">объект(коомбинировый элемент)</param>
        /// <param name="e">событие(изменение значения)</param>
        private void comboBoxLineType_SelectedValueChanged(object sender, EventArgs e)
        {
            if (LinePropertyChanged != null) LinePropertyChanged();
        }
    }
}
