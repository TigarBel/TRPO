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
    /// Класс команды измения свойства фигуры
    /// </summary>
    [Serializable]
    class CommandPropertyChanger : Command
    {

        public CommandPropertyChanger(GraphicsEditor graphicsEditor, string keywords, int index, LineProperty oldLineProperty,
            LineProperty newLineProperty, FillProperty oldFillProperty, FillProperty newFillProperty)
        {
            GraphicsEditor = graphicsEditor;
            Keywords = keywords;
            Index = index;
            if(oldLineProperty!=null && newLineProperty != null)
            {
                OldLineProperty = new LineProperty(oldLineProperty.LineThickness, oldLineProperty.LineColor, oldLineProperty.LineType);
                NewLineProperty = new LineProperty(newLineProperty.LineThickness, newLineProperty.LineColor, newLineProperty.LineType);
            }

            if (oldFillProperty != null && newFillProperty != null)
            {
                OldFillProperty = new FillProperty(oldFillProperty.FillColor);
                NewFillProperty = new FillProperty(newFillProperty.FillColor);
            }
        }

        private GraphicsEditor GraphicsEditor { get; set; }

        private string Keywords { get; set; }

        private int Index { get; set; }

        private LineProperty OldLineProperty { get; set; }

        private FillProperty OldFillProperty { get; set; }

        private LineProperty NewLineProperty { get; set; }

        private FillProperty NewFillProperty { get; set; }

        public override void Execute()
        {
            GraphicsEditor.ChangeProperty(Keywords, Index, OldLineProperty, NewLineProperty, OldFillProperty,
                NewFillProperty);
        }

        public override void UnExecute()
        {
            GraphicsEditor.ChangeProperty(Undo(), Index, OldLineProperty, NewLineProperty, OldFillProperty,
                NewFillProperty);
        }

        // Private helper function : приватные вспомогательные функции
        private string Undo()
        {
            switch (Keywords)
            {
                case "Изменить свойство линии": return "Вернуть свойство линии";
                case "Изменить свойство заливки": return "Вернуть свойство заливки";
            }

            return null;
        }
    }
}
