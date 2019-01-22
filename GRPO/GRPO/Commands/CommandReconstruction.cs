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
            string keywords, List<IDrawable> drawables, Point selectPoint,Point newPoint)
        {
            GraphicsEditor = graphicsEditor;
            Keywords = keywords;
            Drawables = drawables;
            SelectPoint = selectPoint;
            NewPoint = newPoint;
        }

        private GraphicsEditor GraphicsEditor { get; set; }

        private string Keywords { get; set; }

        private List<IDrawable> Drawables { get; set; }

        private Point SelectPoint { get; set; }

        private Point NewPoint { get; set; }

        public override void Execute()
        {
            GraphicsEditor.Reconstruct(Keywords, Drawables, SelectPoint, NewPoint);
        }

        public override void UnExecute()
        {
            GraphicsEditor.Reconstruct(Undo(), Drawables, SelectPoint, NewPoint);
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
