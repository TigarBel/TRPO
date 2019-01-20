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
    class DrawingCommand : Command
    {
        GraphicsEditor _graphicsEditor;
        string _keywords;
        List<Point> _points;
        LineProperty _lineProperty;
        FillProperty _fillProperty;

        // Constructor
        public DrawingCommand(GraphicsEditor graphicsEditor,
            string keywords, List<Point> points, LineProperty lineProperty, FillProperty fillProperty)
        {
            _graphicsEditor = graphicsEditor;
            _keywords = keywords;
            _points = points;
            _lineProperty = lineProperty;
            _fillProperty = fillProperty;
        }

        public string Keywords
        {
            set { _keywords = value; }
        }

        public List<Point> Points
        {
            set { _points = value; }
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
            _graphicsEditor.Operation(_keywords, _points, _lineProperty, _fillProperty);
        }

        public override void UnExecute()
        {
            _graphicsEditor.Operation(Undo(), _points, _lineProperty, _fillProperty);
        }

        // Private helper function : приватные вспомогательные функции
        private string Undo()
        {
            return "Удалить фигуру";
        }
    }
}
