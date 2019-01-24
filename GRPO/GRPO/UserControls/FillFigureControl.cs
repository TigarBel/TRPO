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
    /// <summary>
    /// Пользовательский элемент заливки фигуры
    /// </summary>
    public partial class FillFigureControl : UserControl
    {
        /// <summary>
        /// Делегат для события изменения свойства
        /// </summary>
        public delegate void FillPropertyEventHandler();
        /// <summary>
        /// События для изменения свойства заливки
        /// </summary>
        public event FillPropertyEventHandler FillPropertyChanged;
        /// <summary>
        /// Инициализация класса
        /// </summary>
        public FillFigureControl()
        {
            InitializeComponent();
            FillProperty = new FillProperty();

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
        /// <summary>
        /// Событие при нажатии и отжатии кнопки с белым фоном
        /// </summary>
        /// <param name="sender">объект(кнопка)</param>
        /// <param name="e">событие(нажатие и отжатие)</param>
        private void buttonWhiteColorFill_Click(object sender, EventArgs e)
        {
            buttonSelectColorFill.BackColor = buttonWhiteColorFill.BackColor;
        }
        /// <summary>
        /// Событие при нажатии и отжатии кнопки с черным фоном
        /// </summary>
        /// <param name="sender">объект(кнопка)</param>
        /// <param name="e">событие(нажатие и отжатие)</param>
        private void buttonBlackColorFill_Click(object sender, EventArgs e)
        {
            buttonSelectColorFill.BackColor = buttonBlackColorFill.BackColor;
        }
        /// <summary>
        /// Событие при нажатии и отжатии кнопки с красным фоном
        /// </summary>
        /// <param name="sender">объект(кнопка)</param>
        /// <param name="e">событие(нажатие и отжатие)</param>
        private void buttonRedColorFill_Click(object sender, EventArgs e)
        {
            buttonSelectColorFill.BackColor = buttonRedColorFill.BackColor;
        }
        /// <summary>
        /// Событие при нажатии и отжатии кнопки выбора цвета
        /// </summary>
        /// <param name="sender">объект(кнопка)</param>
        /// <param name="e">событие(нажатие и отжатие)</param>
        private void buttonColorFill_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                buttonSelectColorFill.BackColor = MyDialog.Color;
            }
        }
        /// <summary>
        /// Событие при изменении заднего фона кнопки
        /// </summary>
        /// <param name="sender">объект(кнопка)</param>
        /// <param name="e">событие(изменение заднего фона)</param>
        private void buttonSelectColorFill_BackColor(object sender, EventArgs e)
        {
            FillPropertyChanged();
        }
    }
}
