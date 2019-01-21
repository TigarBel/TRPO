using GRPO.Drawing.Interface;
using GRPO.Drawing.Property;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPO.Commands
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ControlUnit
    {
        // Initializers
        public GraphicsEditor _graphicsEditor = new GraphicsEditor();
        private List<Command> _commands = new List<Command>();

        private int _current = 0;

        public string FileName { get; set; }

        public Image Image { get; set; }

        public void Redo(int levels)
        {
            Console.WriteLine("levels " + levels);

            // Делаем возврат операций
            for (int i = 0; i < levels; i++)
                if (_current < _commands.Count)
                    _commands[_current++].Execute();
        }

        public void Undo(int levels)
        {
            Console.WriteLine("levels " + levels);

            // Делаем отмену операций
            for (int i = 0; i < levels; i++)
                if (_current > 0)
                    _commands[--_current].UnExecute();
        }

        public void Drawing(string keywords, List<Point> points, LineProperty lineProperty, FillProperty fillProperty)
        {
            Command command = new DrawingCommand(_graphicsEditor, keywords, points, lineProperty, fillProperty);
            command.Execute();
            if (_current < _commands.Count)
            {
                _commands.RemoveRange(_current, _commands.Count - _current);
            }
            _commands.Add(command);
            _current++;
        }

        public void ChangeProperty(string keywords, int index, LineProperty lineProperty, FillProperty fillProperty)
        {
            Command command = new PropertyCommand(_graphicsEditor, keywords, index, lineProperty, fillProperty);
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