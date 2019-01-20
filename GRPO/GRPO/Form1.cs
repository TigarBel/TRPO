using GRPO.Drawing;
using GRPO.Drawing.Interface;
using GRPO.Drawing.Property;
using GRPO.HistoryManagers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GRPO
{
    public partial class MainForm : System.Windows.Forms.Form
    {
        private HistoryManager _historyManager;

        public MainForm()
        {
            InitializeComponent();

            foreach (Control control in Controls)
            {
                typeof(Control).InvokeMember("DoubleBuffered",
                    BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                    null, control, new object[] { true });
            }

            _canvasControl.SetSizeCanvas(640, 480);
            
            _toolsWithPropertyControl.FigurePropertyChanged += _toolsWithPropertyControl_FigurePropertyChanged;
            _canvasControl.DragProperty += _canvasControl_SetProperty;

            //
            _toolsWithPropertyControl.FigurePropertyChanged += _historyManager_SaveStep;
            _canvasControl.SaveStep += _historyManager_SaveStep;

            HistoryManagerToolsControl historyManagerToolsControl = new HistoryManagerToolsControl(_toolsWithPropertyControl.SelectTool,
                   _toolsWithPropertyControl.LineProperty, _toolsWithPropertyControl.FillProperty);

            HistoryManagerCanvasControl historyManagerCanvasControl = new HistoryManagerCanvasControl(_canvasControl.BuferDraw, _canvasControl.Drawables,
                _canvasControl.Image, _canvasControl.Interaction, _canvasControl.GetWidthCanvas(), _canvasControl.GetHeightCanvas());

            _historyManager = new HistoryManager(historyManagerToolsControl, historyManagerCanvasControl);
            //
        } 

        private void button1_Click(object sender, EventArgs e)
        {
            _canvasControl.ClearCanvas();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "GraphicsPO Project|*.grpo",
                FileName = _historyManager.FileName
            };
            if (saveFileDialog.ShowDialog() != DialogResult.Cancel)
            {
                using (var stream = saveFileDialog.OpenFile())
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(stream, _historyManager);
                }
            }*/
            _toolsWithPropertyControl.SelectTool.DrawingTools = DrawingTools.DrawFigureLine;
            _canvasControl.SelectTool.DrawingTools = DrawingTools.DrawFigureLine;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //mainPictureBox.Image.Save("111lol.jpg");
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "GraphicsPO Project|*.grpo";
            if (openFileDialog.ShowDialog() != DialogResult.Cancel)
            {
                using (var stream = openFileDialog.OpenFile())
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    _historyManager = (HistoryManager)binaryFormatter.Deserialize(stream);
                    _historyManager_Step();
                    _canvasControl.RefreshCanvas();
                }
            }
        }
        /// <summary>
        /// Кнопка для изменения размера холста
        /// </summary>
        /// <param name="sender">Объект нопки</param>
        /// <param name="e">Объект события</param>
        private void buttonAcceptSizePictureBox_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBox1.Text) >= 0 && Convert.ToInt32(textBox1.Text) <= 640 &&
                Convert.ToInt32(textBox2.Text) >= 0 && Convert.ToInt32(textBox1.Text) <= 480)
            {
                _canvasControl.SetSizeCanvas(Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text));
            }
        }
        /// <summary>
        /// Функция по отлову нажатия клавиш клавиатуры
        /// </summary>
        /// <param name="sender">Объект кнопки</param>
        /// <param name="e">Объект события</param>
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            //MessageBox.Show(e.KeyCode.ToString());
            if (e.Control && e.KeyCode == Keys.Z)
            {
                //Этот момент не подлежит сохранению
                _toolsWithPropertyControl.FigurePropertyChanged -= _historyManager_SaveStep;
                //
                _historyManager_StepBack();
                //Востанавливаем сохранение на изменение инструментов
                _toolsWithPropertyControl.FigurePropertyChanged += _historyManager_SaveStep;
                //
            }
            else if (e.Control && e.KeyCode == Keys.Y)
            {
                //Этот момент не подлежит сохранению
                _toolsWithPropertyControl.FigurePropertyChanged -= _historyManager_SaveStep;
                //
                _historyManager_StepForward();
                //Востанавливаем сохранение на изменение инструментов
                _toolsWithPropertyControl.FigurePropertyChanged += _historyManager_SaveStep;
                //
            }
            else if (_toolsWithPropertyControl.SelectTool.TypeTools == TypeTools.SelectFigure)
            {
                if (e.Control && e.KeyCode == Keys.C)
                {
                    _canvasControl.Copy();
                }
                else if (e.Control && e.KeyCode == Keys.V)
                {
                    _canvasControl.Paste();
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    _canvasControl.Delete();
                }
                else if (e.Control && e.KeyCode == Keys.X)
                {
                    _canvasControl.Cut();
                }
                else if (e.Control)
                {
                    //_canvasControl.Interaction.AddDrawableFigure();
                }
            }
        }
        /// <summary>
        /// Функция по отлову изменения свойства фигуры
        /// </summary>
        private void _toolsWithPropertyControl_FigurePropertyChanged()
        {
            _canvasControl.SelectTool = _toolsWithPropertyControl.SelectTool;
            _canvasControl.LineProperty = _toolsWithPropertyControl.LineProperty;
            _canvasControl.FillProperty = _toolsWithPropertyControl.FillProperty;
            if(_toolsWithPropertyControl.SelectTool.DrawingTools == DrawingTools.CursorSelect)
            {
                if (_canvasControl.Interaction != null)
                {
                    if(_canvasControl.Interaction.DrawableFigures[0] is ILinePropertyble figureWithLineProperty)
                    {
                        figureWithLineProperty.LineProperty = _toolsWithPropertyControl.LineProperty;
                    }
                    if(_canvasControl.Interaction.DrawableFigures[0] is IFillPropertyble figureWithFillProperty)
                    {
                        figureWithFillProperty.FillProperty = _toolsWithPropertyControl.FillProperty;
                    }
                    _canvasControl.RefreshCanvas();
                    _canvasControl.Interaction.EnablePoints = false;
                }
            }
        }
        /// <summary>
        /// Функция по отлову свойств фигуры
        /// </summary>
        /// <param name="drawable">Фигура</param>
        private void _canvasControl_SetProperty(IDrawable drawable)
        {
            if (drawable == null)
            {
                //Этот момент не подлежит сохранению
                _toolsWithPropertyControl.FigurePropertyChanged -= _historyManager_SaveStep;
                //
                _toolsWithPropertyControl.SelectTool.DrawingTools = DrawingTools.CursorSelect;
                //Востанавливаем сохранение на изменение инструментов
                _toolsWithPropertyControl.FigurePropertyChanged += _historyManager_SaveStep;
                //
                _toolsWithPropertyControl.LineProperty = null;
                _toolsWithPropertyControl.FillProperty = null;
                return;
            }
            if(drawable is ILinePropertyble figureWithLineProperty)
            {
                _toolsWithPropertyControl.LineProperty = figureWithLineProperty.LineProperty;
            }
            if (drawable is IFillPropertyble figureWithFillProperty)
            {
                _toolsWithPropertyControl.FillProperty = figureWithFillProperty.FillProperty;
            }
        }
        
        private void _historyManager_SaveStep()
        {

            HistoryManagerToolsControl historyManagerToolsControl = new HistoryManagerToolsControl(new Tools(_toolsWithPropertyControl.SelectTool.DrawingTools),
                   _toolsWithPropertyControl.LineProperty, _toolsWithPropertyControl.FillProperty);

            //Клонирование списка холста
            List<IDrawable> localDrawablesForCanvas = new List<IDrawable>();
            foreach (IDrawable drawable in _canvasControl.Drawables)
            {
                localDrawablesForCanvas.Add(drawable.Clone());
            }
            //Клонирование списка интерактива
            List<IDrawable> localDrawablesForInteraction = new List<IDrawable>();
            Interaction interaction = null;
            if (_canvasControl.Interaction != null)
            {
                foreach (IDrawable drawable in _canvasControl.Interaction.DrawableFigures)
                {
                    localDrawablesForInteraction.Add(drawable.Clone());
                }
                interaction = new Interaction(localDrawablesForInteraction, _canvasControl.Interaction.EnablePoints);
            }
            //
            HistoryManagerCanvasControl historyManagerCanvasControl = new HistoryManagerCanvasControl(_canvasControl.BuferDraw, localDrawablesForCanvas,
                _canvasControl.Image, interaction, _canvasControl.GetWidthCanvas(), _canvasControl.GetHeightCanvas());

            _historyManager.SaveStep(historyManagerToolsControl, historyManagerCanvasControl);
        }

        private void _historyManager_Step()
        {
            _canvasControl.BuferDraw = _historyManager.ManagerCanvasControl.BuferDraw;
            _canvasControl.Image = _historyManager.ManagerCanvasControl.Image;
            _canvasControl.Interaction = _historyManager.ManagerCanvasControl.Interaction;
            _canvasControl.Drawables = _historyManager.ManagerCanvasControl.Drawables;
            int X = _historyManager.ManagerCanvasControl.GetWidthCanvas();
            int Y = _historyManager.ManagerCanvasControl.GetHeightCanvas();
            _canvasControl.SetSizeCanvas(X, Y);

            //Этот момент не подлежит сохранению
            _toolsWithPropertyControl.FigurePropertyChanged -= _historyManager_SaveStep;
            //  
            _toolsWithPropertyControl.LineProperty = _historyManager.ManagerToolsControl.LineProperty;
            _toolsWithPropertyControl.FillProperty = _historyManager.ManagerToolsControl.FillProperty;
            _toolsWithPropertyControl.SelectTool.DrawingTools = _historyManager.ManagerToolsControl.SelectTool.DrawingTools;
            //Востанавливаем сохранение на изменение инструментов
            _toolsWithPropertyControl.FigurePropertyChanged += _historyManager_SaveStep;
            //
        }

        private void _historyManager_StepForward()
        {
            _historyManager.StepForward();
            _historyManager_Step();
        }

        private void _historyManager_StepBack()
        {
            _historyManager.StepBack();
            _historyManager_Step();
        }
    }
}