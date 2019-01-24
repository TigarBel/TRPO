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
    /// Класс элемента управления
    /// </summary>
    [Serializable]
    public class ControlUnit
    {
        /// <summary>
        /// Объект хранения реализации команд
        /// </summary>
        private GraphicsEditor _graphicsEditor = new GraphicsEditor();
        /// <summary>
        /// Список объектов команд
        /// </summary>
        private List<Command> _commands = new List<Command>();
        /// <summary>
        /// Имя пользователя
        /// </summary>
        private string _fileName = "Безымянный";
        /// <summary>
        /// Номер операции
        /// </summary>
        private int _current = 0;
        /// <summary>
        /// Объект хранения реализации команд
        /// </summary>
        public GraphicsEditor GraphicsEditor
        {
            get { return _graphicsEditor; }
        }
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }
        /// <summary>
        /// Номер операции
        /// </summary>
        public int Current
        {
            get { return _current; }
        }
        /// <summary>
        /// Уменьшить общее количество команд на еденицу(опасная ф-ция)
        /// </summary>
        public void CommandsCountDecrement()
        {//Опасная функция
            _commands.RemoveAt(_commands.Count - 1);
        }
        /// <summary>
        /// Вернуть команду
        /// </summary>
        /// <param name="levels">На сколько</param>
        public void Redo(int levels)
        {
            Console.WriteLine("levels " + levels + " current " + _current);

            // Делаем возврат операций
            for (int i = 0; i < levels; i++)
            {
                if (_current < _commands.Count)
                {
                    _commands[_current++].Execute();
                }
            }
        }
        /// <summary>
        /// Убрать команду
        /// </summary>
        /// <param name="levels">На сколько</param>
        public void Undo(int levels)
        {
            Console.WriteLine("levels " + levels + " current " + _current);

            // Делаем отмену операций
            for (int i = 0; i < levels; i++)
            {
                if (_current > 0)
                {
                    _commands[--_current].UnExecute();
                }
            }

        }
        /// <summary>
        /// Команды создания фигуры
        /// </summary>
        /// <param name="keywords">Команда</param>
        /// <param name="tools">Инструмент рисования</param>
        /// <param name="points">Список точек образующих фигуру</param>
        /// <param name="lineProperty">Свойство линии фигуры</param>
        /// <param name="fillProperty">Свойство заливки фигуры</param>
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
        /// <summary>
        /// Команда измения свойства фигуры
        /// </summary>
        /// <param name="keywords">Команда</param>
        /// <param name="index">Индекс фигуры</param>
        /// <param name="oldLineProperty">Старое свойство линии фигуры</param>
        /// <param name="newLineProperty">Новое свойство линии фигуры</param>
        /// <param name="oldFillProperty">Старое свойство заливки фигуры</param>
        /// <param name="newFillProperty">Новое свойство заливки фигуры</param>
        public void ChangeProperty(string keywords, int index, LineProperty oldLineProperty,
            LineProperty newLineProperty, FillProperty oldFillProperty, FillProperty newFillProperty)
        {
            Command command = new CommandPropertyChanger(_graphicsEditor, keywords, index, oldLineProperty,
                newLineProperty, oldFillProperty, newFillProperty);
            command.Execute();
            if (_current < _commands.Count)
            {
                _commands.RemoveRange(_current, _commands.Count - _current);
            }

            _commands.Add(command);
            _current++;
        }
        /// <summary>
        /// Команд очистка холста
        /// </summary>
        /// <param name="keywords">Команда</param>
        /// <param name="drawables">Список удаляемых фигур</param>
        /// <param name="indexes">Список индексов удаляемых фигур</param>
        public void Clear(string keywords, List<IDrawable> drawables, List<int> indexes)
        {
            Command command = new CommandClear(_graphicsEditor, keywords, drawables, indexes);
            command.Execute();
            if (_current < _commands.Count)
            {
                _commands.RemoveRange(_current, _commands.Count - _current);
            }

            _commands.Add(command);
            _current++;
        }
        /// <summary>
        /// Команда реконструирования фигуры
        /// </summary>
        /// <param name="keywords">Команда</param>
        /// <param name="drawables">Список реконструируемых фигур</param>
        /// <param name="indexes">Список индексов фигур</param>
        /// <param name="pointIndex">Индекс изменяемой опорной точки фигуры</param>
        /// <param name="selectPoint">Выбранная точка</param>
        /// <param name="newPoint">Новая точка</param>
        public void Reconstruction(string keywords, List<IDrawable> drawables, List<int> indexes, int pointIndex,
            Point selectPoint, Point newPoint)
        {
            Command command =
                new CommandReconstruction(_graphicsEditor, keywords, drawables, indexes, pointIndex, selectPoint,
                    newPoint);
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