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
    /// <summary>
    /// Класс отвечающий за историю команд
    /// </summary>
    class HistoryManager
    {
        /// <summary>
        /// Количество шагов
        /// </summary>
        private int _stepsCount;
        /// <summary>
        /// Выбранный шаг
        /// </summary>
        private int _stepsSelect;
        /// <summary>
        /// Список истории ToolsControls
        /// </summary>
        private List<HistoryManagerToolsControl> _managerToolsControls = new List<HistoryManagerToolsControl>();
        /// <summary>
        /// Список истории CanvasControls
        /// </summary>
        private List<HistoryManagerCanvasControl> _managerCanvasControls = new List<HistoryManagerCanvasControl>();
        /// <summary>
        /// Пустой класс объект менеджер
        /// </summary>
        public HistoryManager(HistoryManagerToolsControl managerToolsControl, HistoryManagerCanvasControl managerCanvasControl)
        {
            _managerToolsControls.Add(managerToolsControl);
            _managerCanvasControls.Add(managerCanvasControl);
            _stepsCount = 0;
            _stepsSelect = 0;
        }
        /// <summary>
        /// Количество шагов
        /// </summary>
        public int StepsCount
        {
            get
            {
                return _stepsCount;
            }
        }
        /// <summary>
        /// Выбранный шаг
        /// </summary>
        public int StepsSelect
        {
            get
            {
                return _stepsSelect;
            }
        }
        /// <summary>
        /// Добавить менеджеров истории
        /// </summary>
        /// <param name="managerToolsControl">Менеджер истории ToolsControl</param>
        /// <param name="managerCanvasControl">Менеджер истории CanvasControl</param>
        public void SaveStep(HistoryManagerToolsControl managerToolsControl, HistoryManagerCanvasControl managerCanvasControl)
        {
            _managerToolsControls.Add(managerToolsControl);
            _managerCanvasControls.Add(managerCanvasControl);
            if (_stepsCount > _stepsSelect)
            {
                _managerToolsControls.RemoveRange(_stepsSelect + 1, _stepsCount - _stepsSelect);
                _managerCanvasControls.RemoveRange(_stepsSelect + 1, _stepsCount - _stepsSelect);
                _stepsCount = _stepsSelect;
            }
            _stepsCount++;
            _stepsSelect++;
        }
        /// <summary>
        /// Вернутся на один шаг вперед
        /// </summary>
        public void StepForward()
        {
            if (StepsSelect < StepsCount)
            {
                _stepsSelect++;
            }
        }
        /// <summary>
        /// Вернутся на один шаг назад
        /// </summary>
        public void StepBack()
        {
            if (StepsSelect > 0)
            {
                _stepsSelect--;
            }
        }
        /// <summary>
        /// История ToolsControl на данный шаг
        /// </summary>
        public HistoryManagerToolsControl ManagerToolsControl
        {
            get
            {
                return new HistoryManagerToolsControl(_managerToolsControls[StepsSelect].SelectTool, 
                    _managerToolsControls[StepsSelect].LineProperty,
                    _managerToolsControls[StepsSelect].FillProperty);
            }
        }
        /// <summary>
        /// История CanvasControl на данный шаг
        /// </summary>
        public HistoryManagerCanvasControl ManagerCanvasControl
        {
            get
            {
                return new HistoryManagerCanvasControl(_managerCanvasControls[StepsSelect].BuferDraw, 
                    _managerCanvasControls[StepsSelect].Drawables,
                    _managerCanvasControls[StepsSelect].Image, 
                    _managerCanvasControls[StepsSelect].Interaction,
                    _managerCanvasControls[StepsSelect].GetWidthCanvas(), 
                    _managerCanvasControls[StepsSelect].GetHeightCanvas());
            }
        }
    }
}
