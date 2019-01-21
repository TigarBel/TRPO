﻿using GRPO.Drawing.Property;
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
    /// Класс команды создания фигуры
    /// </summary>
    [Serializable]
    class CommandDrawing : Command
    {

        public CommandDrawing(GraphicsEditor graphicsEditor,
            string keywords, Tools tools, List<Point> points, LineProperty lineProperty, FillProperty fillProperty)
        {
            GraphicsEditor = graphicsEditor;
            Keywords = keywords;
            Tools = tools;
            Points = points;
            LineProperty = lineProperty;
            FillProperty = fillProperty;
        }

        private GraphicsEditor GraphicsEditor { get; set; }

        private string Keywords { get; set; }

        private Tools Tools { get; set; }

        private List<Point> Points { get; set; }

        private LineProperty LineProperty { get; set; }

        private FillProperty FillProperty { get; set; }

        public override void Execute()
        {
            GraphicsEditor.CreateFigure(Keywords, Tools, Points, LineProperty, FillProperty);
        }

        public override void UnExecute()
        {
            GraphicsEditor.CreateFigure(Undo(), Tools, Points, LineProperty, FillProperty);
        }

        // Private helper function : приватные вспомогательные функции
        private string Undo()
        {
            return "Удалить фигуру";
        }
    }
}
