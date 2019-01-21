using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPO.Commands
{
    /// <summary>
    /// Класс команды реконструирования фигуры
    /// </summary>
    [Serializable]
    class CommandReconstruction : Command
    {
        public CommandReconstruction(GraphicsEditor graphicsEditor,
            string keywords, List<int> indexes, Point selectPoint,Point newPoint)
        {
            GraphicsEditor = graphicsEditor;
            Keywords = keywords;
            Indexes = indexes;
            SelectPoint = selectPoint;
            NewPoint = newPoint;
        }

        private GraphicsEditor GraphicsEditor { get; set; }

        private string Keywords { get; set; }

        private List<int> Indexes { get; set; }

        private Point SelectPoint { get; set; }

        private Point NewPoint { get; set; }

        public override void Execute()
        {
            GraphicsEditor.Reconstruct(Keywords, Indexes, SelectPoint, NewPoint);
        }

        public override void UnExecute()
        {
            GraphicsEditor.Reconstruct(Undo(), Indexes, SelectPoint, NewPoint);
        }

        // Private helper function : приватные вспомогательные функции
        private string Undo()
        {
            switch (Keywords)
            {
                case "Изменить положение фигур(ы)": return "Вернуть положение фигур(ы)";
                case "Изменить опорную точку": return "Вернуть назад опорную точку";
            }
            return null;
        }
    }
}
