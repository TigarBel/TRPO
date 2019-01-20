using GRPO.Drawing;
using GRPO.Drawing.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPO.HistoryManagers
{
    /// <summary>
    /// Класс отвечающий за историю ToolsControl
    /// </summary>
    [Serializable]
    class HistoryManagerToolsControl
    {
        /// <summary>
        /// Инструмент для рисования
        /// </summary>
        private Tools _selectTool;
        /// <summary>
        /// Свойство линии
        /// </summary>
        private LineProperty _lineProperty;
        /// <summary>
        /// Свойство заливки
        /// </summary>
        private FillProperty _fillProperty;
        /// <summary>
        /// Заполненный объект класса история ToolsControl
        /// </summary>
        /// <param name="selectTool">Инструмент для рисования</param>
        /// <param name="lineProperty">Свойство линии</param>
        /// <param name="fillProperty">Свойство заливки</param>
        public HistoryManagerToolsControl(Tools selectTool, LineProperty lineProperty, FillProperty fillProperty)
        {
            _selectTool = selectTool;
            _lineProperty = lineProperty;
            _fillProperty = fillProperty;
        }
        /// <summary>
        /// Инструмент для рисования
        /// </summary>
        public Tools SelectTool
        {
            get
            {
                return _selectTool;
            }
        }
        /// <summary>
        /// Свойство линии
        /// </summary>
        public LineProperty LineProperty
        {
            get
            {
                return _lineProperty;
            }
        }
        /// <summary>
        /// Свойство заливки
        /// </summary>
        public FillProperty FillProperty
        {
            get
            {
                return _fillProperty;
            }
        }
    }
}
