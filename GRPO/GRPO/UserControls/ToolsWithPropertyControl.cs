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
using GRPO.Drawing.Property;

namespace GRPO
{
    public partial class ToolsWithPropertyControl : UserControl
    {
        public delegate void FigurePropertyEventHandler();
        public event FigurePropertyEventHandler FigurePropertyChanged;
        public event FigurePropertyEventHandler ToolsChanged;
        public event FigurePropertyEventHandler LinePropertyChanged;
        public event FigurePropertyEventHandler FillPropertyChanged;

        public ToolsWithPropertyControl()
        {
            InitializeComponent();

            _toolsControl.ButtonClick += ChangeTools;
            _propertyLineControl.LinePropertyChanged += ChangeLineProperty;
            _fillFigureControl.FillPropertyChanged += ChangeFillProperty;

            _toolsControl.ButtonClick += ChangeProperty;
            _propertyLineControl.LinePropertyChanged += ChangeProperty;
            _fillFigureControl.FillPropertyChanged += ChangeProperty;

            _toolsControl.ButtonClick += HidingUserControl;
            HidingUserControl();
        }

        public void HidingUserControl()
        {
            switch (_toolsControl.SelectTool.DrawingTools)
            {
                case DrawingTools.CursorSelect:
                    {
                        _propertyLineControl.Visible = false;
                        _fillFigureControl.Visible = false;
                        break;
                    }
                case DrawingTools.MassSelect:
                    {
                        _propertyLineControl.Visible = false;
                        _fillFigureControl.Visible = false;
                        break;
                    }
                case DrawingTools.DrawFigureLine:
                    {
                        _propertyLineControl.Visible = true;
                        _fillFigureControl.Visible = false;
                        break;
                    }
                case DrawingTools.DrawFigurePolyline:
                    {
                        _propertyLineControl.Visible = true;
                        _fillFigureControl.Visible = false;
                        break;
                    }
                case DrawingTools.DrawFigureRectangle:
                    {
                        _propertyLineControl.Visible = true;
                        _fillFigureControl.Visible = true;
                        break;
                    }
                case DrawingTools.DrawFigureCircle:
                    {
                        _propertyLineControl.Visible = true;
                        _fillFigureControl.Visible = true;
                        break;
                    }
                case DrawingTools.DrawFigureEllipse:
                    {
                        _propertyLineControl.Visible = true;
                        _fillFigureControl.Visible = true;
                        break;
                    }
                default:
                    {
                        throw new ArgumentException("Данный инструмент отсутсвует!");
                    }
            }
        }
        /// <summary>
        /// Инструмент для рисования
        /// </summary>
        public Tools SelectTool
        {
            get
            {
                return _toolsControl.SelectTool;
            }
            set
            {
                _toolsControl.SelectTool = value;
            }
        }
        /// <summary>
        /// Свойство линии
        /// </summary>
        public LineProperty LineProperty
        {
            get
            {
                return _propertyLineControl.LineProperty;
            }
            set
            {
                if (value == null)
                {
                    _propertyLineControl.Visible = false;
                }
                else
                {
                    _propertyLineControl.LineProperty = value;
                    _propertyLineControl.Visible = true;
                }
            }
        }
        /// <summary>
        /// Свойство заливки
        /// </summary>
        public FillProperty FillProperty
        {
            get
            {
                return _fillFigureControl.FillProperty;
            }
            set
            {
                if (value == null)
                {
                    _fillFigureControl.Visible = false;
                }
                else
                {
                    _fillFigureControl.FillProperty = value;
                    _fillFigureControl.Visible = true;
                }
            }
        }
        /// <summary>
        /// Событие при изменении любого свойства
        /// </summary>
        private void ChangeProperty()
        {
            if (FigurePropertyChanged != null) FigurePropertyChanged();
        }
        /// <summary>
        /// Событие при смене инструмента
        /// </summary>
        private void ChangeTools()
        {
            if (ToolsChanged != null) ToolsChanged();
        }
        /// <summary>
        /// Событие при изменении свойства линии
        /// </summary>
        private void ChangeLineProperty()
        {
            if (LinePropertyChanged != null) LinePropertyChanged();
        }
        /// <summary>
        /// Событие при изменении свойства заливки
        /// </summary>
        private void ChangeFillProperty()
        {
            if (FillPropertyChanged != null) FillPropertyChanged();
        }
    }
}
