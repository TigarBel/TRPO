using GRPO.Drawing.Interface;
using GRPO.Drawing.Property;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRPO.Drawing;

namespace GRPO.Commands
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ControlUnit
    {

        private GraphicsEditor _graphicsEditor = new GraphicsEditor();

        private List<Command> _commands = new List<Command>();

        private string _fileName = "Безымянный";

        private int _current = 0;

        public GraphicsEditor GraphicsEditor
        {
            get { return _graphicsEditor; }
        }

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        public Image Image { get; set; }

        public void Redo(int levels)
        {
            Console.WriteLine("levels " + levels);

            // Делаем возврат операций
            for (int i = 0; i < levels; i++)
            {
                if (_current < _commands.Count)
                {
                    _commands[_current++].Execute();
                }
            }
        }

        public void Undo(int levels)
        {
            Console.WriteLine("levels " + levels);

            // Делаем отмену операций
            for (int i = 0; i < levels; i++)
            {
                if (_current > 0)
                {
                    _commands[--_current].UnExecute();
                }
            }

        }

        public void Drawing(string keywords, Tools tools, List<Point> points, LineProperty lineProperty,
            FillProperty fillProperty)
        {
            Command command = new CommandDrawing(_graphicsEditor, keywords, tools, points, lineProperty, fillProperty);
            command.Execute();
            if (_current < _commands.Count)
            {
                _commands.RemoveRange(_current, _commands.Count - _current);
            }

            _commands.Add(command);
            _current++;
        }

        public void ChangeProperty(string keywords, int index, LineProperty _oldLineProperty,
            LineProperty _newLineProperty, FillProperty _oldFillProperty, FillProperty _newFillProperty)
        {
            Command command = new CommandPropertyChanger(_graphicsEditor, keywords, index, _oldLineProperty, _newLineProperty,
                _oldFillProperty, _newFillProperty);
            command.Execute();
            if (_current < _commands.Count)
            {
                _commands.RemoveRange(_current, _commands.Count - _current);
            }

            _commands.Add(command);
            _current++;
        }
    }
}