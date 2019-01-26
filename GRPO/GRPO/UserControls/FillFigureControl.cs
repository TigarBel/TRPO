using System;
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
        /// Список выбранных цветов
        /// </summary>
        private int[] colorInts = new int[16];

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
            get { return new FillProperty(buttonSelectColorFill.BackColor); }
            set { buttonSelectColorFill.BackColor = value.FillColor; }
        }

        /// <summary>
        /// Событие при нажатии кнопки выбора цвета
        /// </summary>
        /// <param name="sender">объект(кнопка)</param>
        /// <param name="e">событие(нажатие)</param>
        private void buttonSelectColorFill_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            MyDialog.CustomColors = colorInts;
            MyDialog.Color = buttonSelectColorFill.BackColor;
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                buttonSelectColorFill.BackColor = MyDialog.Color;
                colorInts = MyDialog.CustomColors;
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
