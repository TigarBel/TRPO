using GRPO.Commands;
using GRPO.Drawing;
using GRPO.Drawing.Interface;
using GRPO.Drawing.Property;
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
        /// <summary>
        /// Количество команд при загрузки проекта
        /// </summary>
        private int _currentBeginControlUnit = 0;
        /// <summary>
        /// Имя проекта
        /// </summary>
        private string _fileNameOpenProject;

        public MainForm()
        {
            InitializeComponent();
            Init();
        }

        /// <summary>
        /// Ручная инициализация
        /// </summary>
        private void Init()
        {

            foreach (Control control in Controls)
            {
                typeof(Control).InvokeMember("DoubleBuffered",
                    BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                    null, control, new object[] {true});
            }

            _canvasControl.SetSizeCanvas(640, 480);

            _canvasControl.ControlUnit = new ControlUnit();
            
            _toolsWithPropertyControl.ToolsChanged += _toolsWithPropertyControl_ToolsChanged;
            _toolsWithPropertyControl.LinePropertyChanged += _toolsWithPropertyControl_LinePropertyChanged;
            _toolsWithPropertyControl.FillPropertyChanged += _toolsWithPropertyControl_FillPropertyChanged;
            _canvasControl.DragProperty += _canvasControl_SetProperty;
            this.Size = new Size(1000,600);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _canvasControl.ClearCanvas();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ShowSaveFileDialog();
        }

        private void ShowSaveFileDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "GraphicsPO Project|*.grpo|JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            saveFileDialog.Title = "Save an Image File";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                _fileNameOpenProject = saveFileDialog.FileName;
                switch (saveFileDialog.FilterIndex)
                {
                    case 1:
                    {
                        using (var stream = saveFileDialog.OpenFile())
                        {
                            BinaryFormatter binaryFormatter = new BinaryFormatter();
                            _canvasControl.ControlUnit.FileName = saveFileDialog.FileName;
                            binaryFormatter.Serialize(stream, _canvasControl.ControlUnit);
                        }

                        break;
                    }
                    case 2:
                    {
                        _canvasControl.Image.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    }
                    case 3:
                    {
                        _canvasControl.Image.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Bmp);

                        break;
                    }
                    case 4:
                    {
                        _canvasControl.Image.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //mainPictureBox.Image.Save("111lol.jpg");
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "GraphicsPO Project|*.grpo";
            if (openFileDialog.ShowDialog() != DialogResult.Cancel && openFileDialog.FileName != "")
            {
                _fileNameOpenProject = openFileDialog.FileName;
                using (var stream = openFileDialog.OpenFile())
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    _canvasControl.ControlUnit = (ControlUnit)binaryFormatter.Deserialize(stream);
                    _currentBeginControlUnit = _canvasControl.ControlUnit.Current;
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
            if (Convert.ToInt32(textBox1.Text) >= 0 && Convert.ToInt32(textBox1.Text) <= 1920 &&
                Convert.ToInt32(textBox2.Text) >= 0 && Convert.ToInt32(textBox1.Text) <= 1000)
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
            if (e.KeyCode == Keys.Escape)
            {
                _canvasControl.FlagPolyFigure = false;
                _canvasControl.Interaction = null;
                _canvasControl.RefreshCanvas();
            }

            if (!_canvasControl.FlagMouseDown && !_canvasControl.FlagPolyFigure)
            {
                //MessageBox.Show(e.KeyCode.ToString());
                if (e.Control && e.KeyCode == Keys.Z)
                {
                    _canvasControl.ControlUnit.Undo(1);
                    _canvasControl.Interaction = null;
                    _canvasControl.RefreshCanvas();
                }
                else if (e.Control && e.KeyCode == Keys.Y)
                {
                    _canvasControl.ControlUnit.Redo(1);
                    _canvasControl.Interaction = null;
                    _canvasControl.RefreshCanvas();
                }
                else if (e.Control && e.KeyCode == Keys.S)
                {
                    if (_fileNameOpenProject != null)
                    {
                        SaveFileDialog saveFileDialog = new SaveFileDialog();
                        saveFileDialog.FileName = _fileNameOpenProject;
                        using (var stream = saveFileDialog.OpenFile())
                        {
                            BinaryFormatter binaryFormatter = new BinaryFormatter();
                            _canvasControl.ControlUnit.FileName = saveFileDialog.FileName;
                            binaryFormatter.Serialize(stream, _canvasControl.ControlUnit);
                        }
                    }
                    else
                    {
                        ShowSaveFileDialog();
                    }
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
        }

        /// <summary>
        /// Функция по отлову изменения инструмента
        /// </summary>
        private void _toolsWithPropertyControl_ToolsChanged()
        {
            if (!_canvasControl.FlagMouseDown && !_canvasControl.FlagPolyFigure)
            {
                _canvasControl.SelectTool = new Tools(_toolsWithPropertyControl.SelectTool.DrawingTools);
                if (_toolsWithPropertyControl.SelectTool.DrawingTools == DrawingTools.CursorSelect)
                {
                    if (_canvasControl.Interaction != null)
                    {
                        _canvasControl.RefreshCanvas();
                    }
                }
            }
            else
            {
                _toolsWithPropertyControl.SelectTool.DrawingTools = _canvasControl.SelectTool.DrawingTools;
            }
        }
        /// <summary>
        /// Функция по отлову изменения свойства линии фигуры
        /// </summary>
        private void _toolsWithPropertyControl_LinePropertyChanged()
        {
            _canvasControl.LineProperty = _toolsWithPropertyControl.LineProperty;
            if (_toolsWithPropertyControl.SelectTool.DrawingTools == DrawingTools.CursorSelect)
            {
                if (_canvasControl.Interaction != null)
                {
                    if (_canvasControl.Interaction.DrawableFigures[0] is ILinePropertyble figureWithLineProperty)
                    {
                        _canvasControl.ControlUnit.ChangeProperty(_canvasControl.ControlUnit.GraphicsEditor.Keywords[2],
                            _canvasControl.Interaction.Indexes[0], figureWithLineProperty.LineProperty,
                            _toolsWithPropertyControl.LineProperty, null, null);
                    }

                    _canvasControl.RefreshCanvas();
                }
            }
        }
        /// <summary>
        /// Функция по отлову изменения свойства заливки фигуры
        /// </summary>
        private void _toolsWithPropertyControl_FillPropertyChanged()
        {
            _canvasControl.FillProperty = _toolsWithPropertyControl.FillProperty;
            if (_toolsWithPropertyControl.SelectTool.DrawingTools == DrawingTools.CursorSelect)
            {
                if (_canvasControl.Interaction != null)
                {
                    if (_canvasControl.Interaction.DrawableFigures[0] is IFillPropertyble figureWithFillProperty)
                    {
                        _canvasControl.ControlUnit.ChangeProperty(_canvasControl.ControlUnit.GraphicsEditor.Keywords[3],
                            _canvasControl.Interaction.Indexes[0], null,
                            null, figureWithFillProperty.FillProperty, _toolsWithPropertyControl.FillProperty);
                    }

                    _canvasControl.RefreshCanvas();
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
                _toolsWithPropertyControl.SelectTool.DrawingTools = DrawingTools.CursorSelect;
                _toolsWithPropertyControl.LineProperty = null;
                _toolsWithPropertyControl.FillProperty = null;
                return;
            }

            if (drawable is ILinePropertyble figureWithLineProperty)
            {
                _toolsWithPropertyControl.LineProperty = figureWithLineProperty.LineProperty;
            }

            if (drawable is IFillPropertyble figureWithFillProperty)
            {
                _toolsWithPropertyControl.FillProperty = figureWithFillProperty.FillProperty;
            }
        }
    }
}