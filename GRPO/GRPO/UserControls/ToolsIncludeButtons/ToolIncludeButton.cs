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
        Tools _tools;

        public ToolIncludeButton(Button button, DrawingTools drawingTools, ref Tools tools, List<Button> buttons)
        {
            Button = button;
            Button.Click += buttonIncludeTool;
            DrawingTools = drawingTools;
            _tools = tools;
            Buttons = buttons;
            Buttons.Add(Button);
        }
        /// <summary>
        /// Заданная кнопка
        /// </summary>
        private Button Button { get; set; }
        /// <summary>
        /// Заданный инструмент
        /// </summary>
        private DrawingTools DrawingTools { get; set; }

        private Tools SelectTool { get; set; }

        private List<Button> Buttons { get; set; }

        private void buttonIncludeTool(object sender, EventArgs e)
        {
            AllButtonBackColorWhite();
            _tools = new Tools(DrawingTools);
            ((Button)sender).BackColor = Color.Black;
        }

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
