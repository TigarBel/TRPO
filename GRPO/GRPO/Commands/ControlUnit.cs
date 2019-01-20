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
    public class ControlUnit
    {
        // Initializers
        public GraphicsEditor _graphicsEditor = new GraphicsEditor();
        private List<Command> _commands = new List<Command>();

        private int _current = 0;

        public void Redo(int levels)
        {
            Console.WriteLine("\n---- Redo {0} levels ", levels);

            // Делаем возврат операций
            for (int i = 0; i < levels; i++)
                if (_current < _commands.Count)
                    _commands[_current++].Execute();
        }

        public void Undo(int levels)
        {
            Console.WriteLine("\n---- Undo {0} levels ", levels);

            // Делаем отмену операций
            for (int i = 0; i < levels; i++)
                if (_current > 0)
                    _commands[--_current].UnExecute();
        }

        public void Drawing(string keywords, List<Point> points, LineProperty lineProperty, FillProperty fillProperty)
        {

            // Создаем команду операции и выполняем её
            Command command = new DrawingCommand(_graphicsEditor, keywords, points, lineProperty, fillProperty);
            command.Execute();

            if (_current < _commands.Count)
            {
                // если "внутри undo" мы запускаем новую операцию, 
                // надо обрубать список команд, следующих после текущей, 
                // иначе undo/redo будут некорректны
                _commands.RemoveRange(_current, _commands.Count - _current);
            }

            // Добавляем операцию к списку отмены
            _commands.Add(command);
            _current++;
        }
    }
}