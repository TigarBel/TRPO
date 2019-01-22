using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRPO.Drawing.Interface;

namespace GRPO.Commands
{
    /// <summary>
    /// Класс команды реконструирования фигуры
    /// </summary>
    [Serializable]
    class CommandReconstruction : Command
    {
        public CommandReconstruction(GraphicsEditor graphicsEditor,
            string keywords, List<IDrawable> drawables,List<int>indexes, Point selectPoint,Point newPoint)
        {
            GraphicsEditor = graphicsEditor;
            Keywords = keywords;
            Drawables = new List<IDrawable>();
            foreach (var drawable in drawables)
            {
                Drawables.Add(drawable.Clone());
            }

            Indexes = indexes;
            SelectPoint = selectPoint;
            NewPoint = newPoint;
        }

        private GraphicsEditor GraphicsEditor { get; set; }

        private string Keywords { get; set; }

        private List<IDrawable> Drawables { get; set; }

        private List<int> Indexes { get; set; }

        private Point SelectPoint { get; set; }

        private Point NewPoint { get; set; }

        public override void Execute()
        {
            GraphicsEditor.Reconstruct(Keywords, Drawables, Indexes, SelectPoint, NewPoint);
        }

        public override void UnExecute()
        {
            GraphicsEditor.Reconstruct(Undo(), Drawables, Indexes, SelectPoint, NewPoint);
        }

        // Private helper function : приватные вспомогательные функции
        private string Undo()
        {
            switch (Keywords)
            {
                case "Изменить положение фигур(ы)": return "Вернуть положение фигур(ы)";
                case "Изменить опорную точку": return "Вернуть назад опорную точку";
                case "Добавить фигуру(ы)": return "Убрать фигуру(ы)";
            }
            return null;
        }
    }
}
