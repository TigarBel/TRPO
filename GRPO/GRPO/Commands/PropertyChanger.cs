using GRPO.Drawing.Property;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPO.Commands
{
    class PropertyCommand : Command
    {
        GraphicsEditor _graphicsEditor;
        string _keywords;
        int _index;
        LineProperty _lineProperty;
        FillProperty _fillProperty;

        // Constructor
        public PropertyCommand(GraphicsEditor graphicsEditor,
            string keywords, int index, LineProperty lineProperty, FillProperty fillProperty)
        {
            _graphicsEditor = graphicsEditor;
            _keywords = keywords;
            _index = index;
            _lineProperty = lineProperty;
            _fillProperty = fillProperty;
        }

        public string Keywords
        {
            set { _keywords = value; }
        }

        public int Index
        {
            set { _index = value; }
        }

        public LineProperty LineProperty
        {
            set { _lineProperty = value; }
        }

        public FillProperty FillProperty
        {
            set { _fillProperty = value; }
        }

        public override void Execute()
        {
            _graphicsEditor.ChangeProperty(_keywords, _index, _lineProperty, _fillProperty);
        }

        public override void UnExecute()
        {
            _graphicsEditor.ChangeProperty(Undo(), _index, _lineProperty, _fillProperty);
        }

        // Private helper function : приватные вспомогательные функции
        private string Undo()
        {
            return "Вернуть свойство";
        }
    }
}
