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
    /// Класс стратегии мыши при перемещении
    /// </summary>
    public class MouseMove : BaseMouseStrategy
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
            if (flagMouseDown)
            {
                if (selectTool.TypeTools == TypeTools.SimpleFigure)
                {
                    drawables.RemoveAt(drawables.Count - 1);
                    List<Point> localPoints = new List<Point>() {pointA, pointB};
                    drawables.Add(new FactoryDrawFigure().DrawFigure(localPoints, lineProperty, fillProperty,
                        selectTool.DrawingTools));
                }

                if (selectTool.TypeTools == TypeTools.SelectFigure)
                {
                    if (selectTool.DrawingTools == DrawingTools.MassSelect)
                    {
                        if (interaction != null)
                        {
                            if (interaction.IndexSelectPoint >= 0)
                            {
                                interaction.ChangeSize(pointA);
                                interaction.ChangeSize(pointB);
                                interaction.Draw(canvas);
                            }
                            else
                            {
                                interaction.Position = new Point(pointB.X - pointC.X, pointB.Y - pointC.Y);
                                interaction.Draw(canvas);
                                pointC = new Point(e.X, e.Y);
                            }
                        }
                        else
                        {
                            DrawFigureRectangle rectangle = new DrawFigureRectangle(pointA, pointB,
                                new LineProperty(1, Color.Gray, DashStyle.Dash),
                                new FillProperty(Color.Transparent));
                            rectangle.Draw(canvas);
                        }
                    }

                    if (selectTool.DrawingTools == DrawingTools.CursorSelect)
                    {
                        if (interaction != null)
                        {
                            if (!interaction.EnablePoints)
                            {
                                if (interaction.IndexSelectPoint >= 0)
                                {
                                    interaction.ChangeSize(pointA);
                                    interaction.ChangeSize(pointB);
                                    interaction.Draw(canvas);
                                }
                                else
                                {
                                    interaction.Position = new Point(pointB.X - pointC.X, pointB.Y - pointC.Y);
                                    interaction.Draw(canvas);
                                    pointC = new Point(e.X, e.Y);
                                }
                            }
                            else if (interaction.IndexSelectPoint != -1)
                            {
                                interaction.ChangePoint(pointB);
                                interaction.Draw(canvas);
                            }
                        }
                    }
                }
            }

            if (selectTool.TypeTools == TypeTools.PolyFigure)
            {
                if (flagPolyFigure)
                {
                    List<Point> points = drawables[drawables.Count - 1].Points;
                    points.RemoveAt(points.Count - 1);
                    points.Add(pointB);

                    drawables.RemoveAt(drawables.Count - 1);
                    drawables.Add(new FactoryDrawFigure().DrawFigure(points, lineProperty, fillProperty,
                        selectTool.DrawingTools));

                }
            }
        }
    }
}
