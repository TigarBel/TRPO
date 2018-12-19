﻿using System;
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
        

        public ToolsWithPropertyControl()
        {
            InitializeComponent();
            _toolsControl.ButtonClick += HidingUserControl;
            HidingUserControl();
        }

        public void HidingUserControl()
        {
            switch (_toolsControl.SelectTool)
            {
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
                case DrawingTools.CursorSelect:
                    {
                        _propertyLineControl.Visible = false;
                        _fillFigureControl.Visible = false;
                        break;
                    }
                case DrawingTools.DrawFigurePolygon:
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
        public ExtendedForLine ExtendedForLine
        {
            get
            {
                return _propertyLineControl.Extended;
            }
            set
            {
                _propertyLineControl.Visible = true;
                _propertyLineControl.Extended = value;
            }
        }
        /// <summary>
        /// Свойство заливки
        /// </summary>
        public ExtendedForFigure ExtendedForFigure
        {
            get
            {
                return _fillFigureControl.Extended;
            }
            set
            {
                _fillFigureControl.Visible = true;
                _fillFigureControl.Extended = value;
            }
        }
    }
}