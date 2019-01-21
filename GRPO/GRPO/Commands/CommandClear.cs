using GRPO.Drawing.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPO.Commands
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    class CommandClear : Command
    {

        public CommandClear(GraphicsEditor graphicsEditor, string keywords, List<IDrawable> drawables)
        {
            GraphicsEditor = graphicsEditor;
            Keywords = keywords;
            Drawables = drawables;
        }

        private GraphicsEditor GraphicsEditor { get; set; }

        private string Keywords { get; set; }

        private List<IDrawable> Drawables  { get; set; }

        public override void Execute()
        {
            GraphicsEditor.DrawablesCleaer(Keywords, Drawables);
        }

        public override void UnExecute()
        {
            GraphicsEditor.DrawablesCleaer(Undo(), Drawables);
        }

        // Private helper function : приватные вспомогательные функции
        private string Undo()
        {
            switch (Keywords)
            {
                case "Удалить весь список": return "Восстановить весь список";
                case "Удалить элементы": return "Восстановить элементы списка";
                case "Удалить элемент": return "Восстановить элемент списка";
            }
            return null;
        }
    }
}
