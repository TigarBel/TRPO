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

        public CommandClear(GraphicsEditor graphicsEditor, string keywords, List<IDrawable> drawables, List<int> indexes)
        {
            GraphicsEditor = graphicsEditor;
            Keywords = keywords;
            Drawables = new List<IDrawable>();
            foreach (var drawable in drawables)
            {
                Drawables.Add(drawable.Clone());
            }
            Indexes = indexes;
        }

        private GraphicsEditor GraphicsEditor { get; set; }

        private string Keywords { get; set; }

        private List<IDrawable> Drawables  { get; set; }

        private List<int> Indexes { get; set; }

        public override void Execute()
        {
            GraphicsEditor.DrawablesCleaer(Keywords, Drawables, Indexes);
        }

        public override void UnExecute()
        {
            GraphicsEditor.DrawablesCleaer(Undo(), Drawables, Indexes);
        }

        // Private helper function : приватные вспомогательные функции
        private string Undo()
        {
            switch (Keywords)
            {
                case "Удалить весь список": return "Восстановить весь список";
                case "Удалить элемент(ы)": return "Восстановить элемент(ы) списка";
            }
            return null;
        }
    }
}
