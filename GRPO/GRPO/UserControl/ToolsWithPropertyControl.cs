using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GRPO
{
    public partial class ToolsWithPropertyControl : UserControl
    {
        public delegate void FigurePropertyEventHandler();
        public event FigurePropertyEventHandler FigurePropertyChanged;

        public ToolsWithPropertyControl()
        {
            InitializeComponent();

            _toolsControl.ButtonClick += ChangeProperty;
            _propertyLineControl.LinePropertyChanged += ChangeProperty;
            _fillFigureControl.FillPropertyChanged += ChangeProperty;

            _toolsControl.ButtonClick += HidingUserControl;
            HidingUserControl();
        }

        public void HidingUserControl()
        {
            switch (_toolsControl.SelectTool)
            {
                case DrawingTools.CursorSelect:
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
            }
        }
        /// <summary>
        /// Выбранный тип класса IDrawable
        /// </summary>
        public DrawingTools SelectTool
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
                _propertyLineControl.LineProperty = value;
                _propertyLineControl.Visible = true;
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
                _fillFigureControl.FillProperty = value;
                _fillFigureControl.Visible = true;
            }
        }

        private void ChangeProperty()
        {
            if (FigurePropertyChanged != null) FigurePropertyChanged();
        }
    }
}
