using GRPO.Drawing;
using GRPO.Drawing.Property;
using System.Collections.Generic;
using System.Drawing;
using GRPO.Drawing.Interface;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using GRPO.Commands;

namespace GRPO.UserControls.CanvasMouseStrategy
{
    /// <summary>
    /// Класс стратегии при нажатии кнопки мыши
    /// </summary>
    public class MouseDown : BaseMouseStrategy
    {
        /// <summary>
        /// Базовый метод выполнения стратегии мыши
        /// </summary>
        /// <param name="flagMouseDown">Флаг нажата ли кнопка</param>
        /// <param name="flagPolyFigure">Флаг завершена ли полифигура</param>
        /// <param name="selectTool">Выбран инструмент</param>
        /// <param name="lineProperty">Свойство линии</param>
        /// <param name="fillProperty">Свойство заливки</param>
        /// <param name="canvas">Холст на котором рисуют</param>
        /// <param name="drawables">Локальный список фигур</param>
        /// <param name="controlUnit">Элемент управления</param>
        /// <param name="pointA">Начальная точка</param>
        /// <param name="pointB">Конечная точка</param>
        /// <param name="pointC">Результирующая точка</param>
        /// <param name="interaction">Интерактив</param>
        /// <param name="e">Состояние мыши</param>
        public override void MouseAction(ref bool flagMouseDown, ref bool flagPolyFigure, Tools selectTool,
            LineProperty lineProperty, FillProperty fillProperty, PictureBox canvas, ref List<IDrawable> drawables,
            ref ControlUnit controlUnit, Point pointA, Point pointB, ref Point pointC, ref Interaction interaction,
            MouseEventArgs e)
        {
            if (selectTool.TypeTools == TypeTools.SimpleFigure)
            {
                List<Point> localPoints = new List<Point>() {pointA, pointB};
                drawables.Add(new FactoryDrawFigure().DrawFigure(localPoints, lineProperty, fillProperty,
                    selectTool.DrawingTools));
            }

            if (selectTool.TypeTools == TypeTools.PolyFigure)
            {
                if (flagPolyFigure)
                {
                    List<Point> points = drawables[drawables.Count - 1].Points;
                    points.RemoveAt(points.Count - 1);
                    points.Add(pointA);

                    drawables.RemoveAt(drawables.Count - 1);
                    if (e.Button == MouseButtons.Right)
                    {
                        controlUnit.Drawing(controlUnit.GraphicsEditor.Keywords[0], new Tools(selectTool.DrawingTools),
                            points, lineProperty, fillProperty);
                        drawables.Clear();

                        flagPolyFigure = false;
                    }
                    else
                    {
                        drawables.Add(new FactoryDrawFigure().DrawFigure(points, lineProperty, fillProperty,
                            selectTool.DrawingTools));
                    }
                }

                if (!flagPolyFigure && e.Button == MouseButtons.Left)
                {
                    List<Point> points = new List<Point>()
                        {new Point(pointA.X, pointA.Y), new Point(pointA.X, pointA.Y)};
                    drawables.Add(new FactoryDrawFigure().DrawFigure(points, lineProperty, fillProperty,
                        selectTool.DrawingTools));

                    flagPolyFigure = true;
                }
            }

            if (selectTool.TypeTools == TypeTools.SelectFigure)
            {
                if (selectTool.DrawingTools == DrawingTools.MassSelect)
                {
                    if (interaction != null)
                    {
                        if (interaction.MinX > pointA.X || interaction.MaxX < pointA.X ||
                            interaction.MinY > pointA.Y ||
                            interaction.MaxY < pointA.Y)
                        {
                            interaction = null;
                        }
                        else
                        {
                            interaction.SelectPoint = pointA;
                            pointC = new Point(e.X, e.Y);
                        }
                    }
                }

                if (selectTool.DrawingTools == DrawingTools.CursorSelect)
                {
                    if (interaction != null)
                    {
                        if (interaction.MinX > pointA.X || interaction.MaxX < pointA.X ||
                            interaction.MinY > pointA.Y ||
                            interaction.MaxY < pointA.Y)
                        {
                            interaction = null;
                        }
                        else
                        {
                            interaction.SelectPoint = pointA;
                            pointC = new Point(e.X, e.Y);
                        }
                    }
                }
            }
        }
    }
}
