using System;

namespace GRPO.Drawing
{
    /// <summary>
    /// Класс инструмента(выбор инструмента и его тип)
    /// </summary>
    [Serializable]
    public class Tools
    {
        /// <summary>
        /// Инструмент для рисования
        /// </summary>
        DrawingTools _drawingTools;

        /// <summary>
        /// Тип инструмента
        /// </summary>
        TypeTools _typeTools;

        /// <summary>
        /// Пустой класс инструмента
        /// </summary>
        public Tools()
        {
            DrawingTools = DrawingTools.DrawFigureLine;
        }

        /// <summary>
        /// Класс инструмента для рисования
        /// </summary>
        /// <param name="drawingTools">Инструмент</param>
        public Tools(DrawingTools drawingTools)
        {
            DrawingTools = drawingTools;
        }

        /// <summary>
        /// Инструмент для рисования
        /// </summary>
        public DrawingTools DrawingTools
        {
            get { return _drawingTools; }
            set
            {
                _drawingTools = value;
                switch (value)
                {
                    case DrawingTools.CursorSelect:
                    {
                        _typeTools = TypeTools.SelectFigure;
                        break;
                    }
                    case DrawingTools.MassSelect:
                    {
                        _typeTools = TypeTools.SelectFigure;
                        break;
                    }
                    case DrawingTools.DrawFigureLine:
                    {
                        _typeTools = TypeTools.SimpleFigure;
                        break;
                    }
                    case DrawingTools.DrawFigureRectangle:
                    {
                        _typeTools = TypeTools.SimpleFigure;
                        break;
                    }
                    case DrawingTools.DrawFigureCircle:
                    {
                        _typeTools = TypeTools.SimpleFigure;
                        break;
                    }
                    case DrawingTools.DrawFigureEllipse:
                    {
                        _typeTools = TypeTools.SimpleFigure;
                        break;
                    }
                    case DrawingTools.DrawFigurePolyline:
                    {
                        _typeTools = TypeTools.PolyFigure;
                        break;
                    }
                    case DrawingTools.DrawFigurePolygon:
                    {
                        _typeTools = TypeTools.PolyFigure;
                        break;
                    }
                    default:
                    {
                        throw new ArgumentException("Данный инструмент отсутсвует!");
                    }
                }
            }
        }

        /// <summary>
        /// Тип инструмента
        /// </summary>
        public TypeTools TypeTools
        {
            get { return _typeTools; }
        }
    }
}
