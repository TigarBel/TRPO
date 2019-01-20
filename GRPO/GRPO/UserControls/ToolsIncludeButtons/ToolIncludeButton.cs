using GRPO.Drawing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GRPO.UserControls.ToolsIncludeButtons
{
    /// <summary>
    /// Кнопка определяющая для какой кнопки выбран инструмент для рисования
    /// </summary>
    class ToolIncludeButton
    {
        /// <summary>
        /// Делегат для события вызывающегося при изменении ссылки
        /// </summary>
        public delegate void ToolsHandler();
        /// <summary>
        /// События при изменении ссылки на объект инструмента
        /// </summary>
        public event ToolsHandler ToolsChanged;
        /// <summary>
        /// Ссылка на изменяемы инструмент
        /// </summary>
        Tools _tools;
        /// <summary>
        /// Конструктор кнопки со встроенным инструментом
        /// </summary>
        /// <param name="button">Выбранная кнопка</param>
        /// <param name="drawingTools">Инструмент</param>
        /// <param name="tools">Ссылка на изменяемы инструмент</param>
        /// <param name="buttons">Список кнопок</param>
        /// <param name="toolsHandler">Событие при изменении содержания в ссылке</param>
        public ToolIncludeButton(Button button, DrawingTools drawingTools, ref Tools tools, List<Button> buttons, ToolsHandler toolsHandler)
        {
            Button = button;
            Button.Click += buttonIncludeTool;
            DrawingTools = drawingTools;
            _tools = tools;
            Buttons = buttons;
            Buttons.Add(Button);
            ToolsChanged += toolsHandler;
        }
        /// <summary>
        /// Заданная кнопка
        /// </summary>
        private Button Button { get; set; }
        /// <summary>
        /// Заданный инструмент
        /// </summary>
        private DrawingTools DrawingTools { get; set; }
        /// <summary>
        /// Список закрашиваемых кнопок
        /// </summary>
        private List<Button> Buttons { get; set; }
        /// <summary>
        /// Событие при нажатии выбранной кнопки
        /// </summary>
        /// <param name="sender">Объект кнопки</param>
        /// <param name="e">Объект события</param>
        private void buttonIncludeTool(object sender, EventArgs e)
        {
            AllButtonBackColorWhite();
            _tools.DrawingTools = DrawingTools;
            ((Button)sender).BackColor = Color.Black;
            if (ToolsChanged != null) ToolsChanged();
        }
        /// <summary>
        /// Метод по закрашиванию кнопок в белый цвет
        /// </summary>
        private void AllButtonBackColorWhite()
        {
            if (Buttons != null)
            {
                foreach (Button button in Buttons)
                {
                    button.BackColor = Color.White;
                }
            }
        }
    }
}
