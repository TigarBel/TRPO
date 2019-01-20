using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GRPO.Drawing;
using GRPO.UserControls.ToolsIncludeButtons;

namespace GRPO
{
    /// <summary>
    /// Пользовательский интерфейс для выбора инструмента
    /// </summary>
    public partial class ToolsControl : UserControl
    {
        /// <summary>
        /// Объект инструмента
        /// </summary>
        private Tools _tools = new Tools();
        /// <summary>
        /// Списко объектов инструментов в кнопках
        /// </summary>
        private List<ToolIncludeButton> _toolIncludeButtons = new List<ToolIncludeButton>();
        /// <summary>
        /// Список кнопок
        /// </summary>
        private List<Button> _buttons = new List<Button>();
        /// <summary>
        /// Делегат для события изменения состояния кнопка
        /// </summary>
        public delegate void ButtonStateHandler();
        /// <summary>
        /// Событие при изменении состояния кнопки
        /// </summary>
        public event ButtonStateHandler ButtonClick;
        /// <summary>
        /// Конструктор класса пользовательский интерфейс для выбора инструмента
        /// </summary>
        public ToolsControl()
        {
            InitializeComponent();

            SelectTool = new Tools(DrawingTools.DrawFigureLine);

            _toolIncludeButtons.Add(new ToolIncludeButton(buttonCursorSelect, DrawingTools.CursorSelect, ref _tools, _buttons, RefreshSelectTool));
            _toolIncludeButtons.Add(new ToolIncludeButton(buttonMassSelect, DrawingTools.MassSelect, ref _tools, _buttons, RefreshSelectTool));
            _toolIncludeButtons.Add(new ToolIncludeButton(buttonFigureLine, DrawingTools.DrawFigureLine, ref _tools, _buttons, RefreshSelectTool));
            _toolIncludeButtons.Add(new ToolIncludeButton(buttonFigurePolyline, DrawingTools.DrawFigurePolyline, ref _tools, _buttons, RefreshSelectTool));
            _toolIncludeButtons.Add(new ToolIncludeButton(buttonFigureRectangle, DrawingTools.DrawFigureRectangle, ref _tools, _buttons, RefreshSelectTool));
            _toolIncludeButtons.Add(new ToolIncludeButton(buttonFigureCircle, DrawingTools.DrawFigureCircle, ref _tools, _buttons, RefreshSelectTool));
            _toolIncludeButtons.Add(new ToolIncludeButton(buttonFigureEllips, DrawingTools.DrawFigureEllipse, ref _tools, _buttons, RefreshSelectTool));
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
                foreach(ToolIncludeButton toolIncludeButton in _toolIncludeButtons)
                {
                    toolIncludeButton.PaintOver();
                }
            }
        }
        /// <summary>
        /// Процедура для события, при изменении ссылки
        /// </summary>
        private void RefreshSelectTool() { SelectTool = SelectTool; }
    }
}
