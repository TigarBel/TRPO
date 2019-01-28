using System;
using System.Windows.Forms;
using GRPO.Drawing;
using GRPO.Drawing.Property;

namespace GRPO
{
    /// <summary>
    /// Пользовательский элемент инструментов со свойствами отрисовки
    /// </summary>
    public partial class ToolsWithPropertyControl : UserControl
    {
        /// <summary>
        /// Делегат для события изменения элемента
        /// </summary>
        public delegate void FigurePropertyEventHandler();

        /// <summary>
        /// Событие при изменении инструмента
        /// </summary>
        public event FigurePropertyEventHandler ToolsChanged;

        /// <summary>
        /// Событие при изменении свойства линии
        /// </summary>
        public event FigurePropertyEventHandler LinePropertyChanged;

        /// <summary>
        /// Событие при изменении заливки
        /// </summary>
        public event FigurePropertyEventHandler FillPropertyChanged;

        /// <summary>
        /// Инициализация класса
        /// </summary>
        public ToolsWithPropertyControl()
        {
            InitializeComponent();

            _toolsControl.ButtonClick += ChangeTools;
            //_toolsControl.ButtonClick += ToolsChanged;
            _propertyLineControl.LinePropertyChanged += ChangeLineProperty;
            _fillFigureControl.FillPropertyChanged += ChangeFillProperty;

            _toolsControl.ButtonClick += HidingUserControl;
            HidingUserControl();
        }

        /// <summary>
        /// Процедура исчезновения элемента при изменении инструмента
        /// </summary>
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
                case DrawingTools.DrawFigurePolygon:
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
            get { return _toolsControl.SelectTool; }
            set { _toolsControl.SelectTool = value; }
        }

        /// <summary>
        /// Свойство линии
        /// </summary>
        public LineProperty LineProperty
        {
            get { return _propertyLineControl.LineProperty; }
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
            get { return _fillFigureControl.FillProperty; }
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
