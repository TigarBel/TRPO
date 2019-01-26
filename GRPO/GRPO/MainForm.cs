using GRPO.Commands;
using GRPO.Drawing;
using GRPO.Drawing.Interface;
using System;
using System.Drawing;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace GRPO
{
    /// <summary>
    /// Основная форма программы
    /// </summary>
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

        /// <summary>
        /// Инициализация формы
        /// </summary>
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
            this.Size = new Size(1000, 600);
            this.FormClosing += MainForm_FormClosing;
            textBox1.TextChanged += textBox_TextChanged;
            textBox2.TextChanged += textBox_TextChanged;
        }

        /// <summary>
        /// Событие вызываемое при закрытии формы
        /// </summary>
        /// <param name="sender">объект(форма)</param>
        /// <param name="e">событие(закрытие)</param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_canvasControl.ControlUnit.Current != _currentBeginControlUnit)
            {
                var dialogResult = MessageBox.Show("Есть несохраненные изменения. Сохранить перед выходом!?",
                    "Внимание", MessageBoxButtons.YesNoCancel);
                bool checkedSave = false;
                switch (dialogResult)
                {
                    case DialogResult.Yes:
                        SaveProject();
                        checkedSave = false;
                        // еще раз проверка, а то вдруг юзер закрыл окно сохранения
                        if (_currentBeginControlUnit != _canvasControl.ControlUnit.Current) checkedSave = true;
                        break;
                    case DialogResult.Cancel:
                        checkedSave = true;
                        break;
                    default:
                        checkedSave = false;
                        break;
                }

                e.Cancel = checkedSave;
            }
        }

        /// <summary>
        /// Событие при очистке холста
        /// </summary>
        /// <param name="sender">объект(кнопка)</param>
        /// <param name="e">событие(нажатие)</param>
        private void button1_Click(object sender, EventArgs e)
        {
            _canvasControl.ClearCanvas();
        }

        /// <summary>
        /// Событие при сохрании проекта/картинки
        /// </summary>
        /// <param name="sender">объект(кнопка)</param>
        /// <param name="e">событие(нажатие)</param>
        private void button2_Click(object sender, EventArgs e)
        {
            ShowSaveFileDialog();
        }

        /// <summary>
        /// Функция автоматического сохранения проекта
        /// </summary>
        private void SaveProject()
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

        /// <summary>
        /// Функция открытия окна сохранения проекта/картинки
        /// </summary>
        private void ShowSaveFileDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "GraphicsPO Project|*.grpo|JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            saveFileDialog.Title = "Сохранение";
            saveFileDialog.FileName = _canvasControl.ControlUnit.FileName;
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

        /// <summary>
        /// Событие при открытии проекта
        /// </summary>
        /// <param name="sender">объект(кнопка)</param>
        /// <param name="e">событие(нажатие)</param>
        private void button3_Click(object sender, EventArgs e)
        {
            //mainPictureBox.Image.Save("111lol.jpg");
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "GraphicsPO Project|*.grpo";
            openFileDialog.Title = "Открытие проекта";
            if (openFileDialog.ShowDialog() != DialogResult.Cancel && openFileDialog.FileName != "")
            {
                _fileNameOpenProject = openFileDialog.FileName;
                using (var stream = openFileDialog.OpenFile())
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    _canvasControl.ControlUnit = (ControlUnit) binaryFormatter.Deserialize(stream);
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
            if (!_canvasControl.FlagMouseDown && !_canvasControl.FlagPolyFigure)
            {
                if (Convert.ToInt32(textBox1.Text) >= 0 && Convert.ToInt32(textBox1.Text) <= 1920 &&
                    Convert.ToInt32(textBox2.Text) >= 0 && Convert.ToInt32(textBox1.Text) <= 1000)
                {
                    _canvasControl.SetSizeCanvas(Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text));
                }
            }
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {

            if (System.Text.RegularExpressions.Regex.IsMatch(((TextBox) sender).Text, "[^0-9]"))
            {
                MessageBox.Show("Ввод только числа!");
                ((TextBox) sender).Text = ((TextBox) sender).Text.Remove(((TextBox) sender).Text.Length - 1);
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
                //_canvasControl.FlagMouseDown = false;
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
                    SaveProject();
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
            if (_canvasControl.Interaction != null)
            {
                if (_canvasControl.ControlUnit.GraphicsEditor.Drawables[_canvasControl.Interaction.Indexes[0]] is
                    ILineProperty figureWithLineProperty)
                {
                    _canvasControl.ControlUnit.ChangeProperty(_canvasControl.ControlUnit.GraphicsEditor.Keywords[2],
                        _canvasControl.Interaction.Indexes[0], figureWithLineProperty.LineProperty,
                        _toolsWithPropertyControl.LineProperty, null, null);
                }

                _canvasControl.RefreshCanvas();
            }
        }

        /// <summary>
        /// Функция по отлову изменения свойства заливки фигуры
        /// </summary>
        private void _toolsWithPropertyControl_FillPropertyChanged()
        {
            _canvasControl.FillProperty = _toolsWithPropertyControl.FillProperty;
            if (_canvasControl.Interaction != null)
            {
                if (_canvasControl.ControlUnit.GraphicsEditor.Drawables[_canvasControl.Interaction.Indexes[0]] is
                    IFillProperty figureWithFillProperty)
                {
                    _canvasControl.ControlUnit.ChangeProperty(_canvasControl.ControlUnit.GraphicsEditor.Keywords[3],
                        _canvasControl.Interaction.Indexes[0], null,
                        null, figureWithFillProperty.FillProperty, _toolsWithPropertyControl.FillProperty);
                }

                _canvasControl.RefreshCanvas();
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

            if (drawable is ILineProperty figureWithLineProperty)
            {
                _toolsWithPropertyControl.LineProperty = figureWithLineProperty.LineProperty;
            }

            if (drawable is IFillProperty figureWithFillProperty)
            {
                _toolsWithPropertyControl.FillProperty = figureWithFillProperty.FillProperty;
            }
        }
    }
}