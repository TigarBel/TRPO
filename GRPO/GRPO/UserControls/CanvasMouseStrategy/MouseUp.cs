using GRPO.Drawing;
using GRPO.Drawing.Property;
using System.Collections.Generic;
using System.Drawing;
using GRPO.Drawing.Interface;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using GRPO.Commands;
using static GRPO.CanvasControl;

namespace GRPO.UserControls.CanvasMouseStrategy
{
    /// <summary>
    /// Класс стратегии мыши при отжатии
    /// </summary>
    public class MouseUp : BaseMouseStrategy
    {

        /// <summary>
        /// Событие при выборе рисуемой фигуры
        /// </summary>
        public event CanvasControl.DragEventHandler DragProperty;

        /// <summary>
        /// Конструктор класса стратегии мыши при отжатии
        /// </summary>
        /// <param name="dragProperty"></param>
        public MouseUp(CanvasControl.DragEventHandler dragProperty)
        {
            DragProperty += dragProperty;
        }

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
                    List<Point> points = new List<Point>() {pointA, pointB};
                    controlUnit.Drawing(controlUnit.GraphicsEditor.Keywords[0], new Tools(selectTool.DrawingTools),
                        points, lineProperty, fillProperty);
                    drawables.Clear();
                }

                if (selectTool.TypeTools == TypeTools.PolyFigure)
                {
                    if (e.Button == MouseButtons.Left && drawables.Count != 0)
                    {
                        List<Point> points = drawables[drawables.Count - 1].Points;
                        points.Add(pointB);
                        drawables.RemoveAt(drawables.Count - 1);
                        drawables.Add(new FactoryDrawFigure().DrawFigure(points, lineProperty, fillProperty,
                            selectTool.DrawingTools));
                    }
                }

                if (selectTool.TypeTools == TypeTools.SelectFigure)
                {
                    if (selectTool.DrawingTools == DrawingTools.MassSelect)
                    {
                        if (interaction == null)
                        {
                            interaction = new Interaction(controlUnit.GraphicsEditor.Drawables, pointA, pointB);
                            if (interaction.DrawableFigures.Count == 1)
                            {
                                if (DragProperty != null) DragProperty(interaction.DrawableFigures[0]);
                            }

                            if (interaction.DrawableFigures.Count == 0)
                            {
                                interaction = null;
                                if (DragProperty != null) DragProperty(null);
                            }
                        }
                        else
                        {
                            if (pointA.X != pointB.X && pointA.Y != pointB.Y)
                            {
                                if (!interaction.EnablePoints)
                                {
                                    if (interaction.IndexSelectPoint >= 0)
                                    {
                                        interaction.ChangeSize(pointB);
                                        controlUnit.Reconstruction(controlUnit.GraphicsEditor.Keywords[9],
                                            controlUnit.GraphicsEditor.Drawables, interaction.Indexes, 0, pointA,
                                            pointB);
                                        interaction.GetMaxMinXY();
                                    }
                                    else
                                    {
                                        controlUnit.Reconstruction(controlUnit.GraphicsEditor.Keywords[6],
                                            controlUnit.GraphicsEditor.Drawables, interaction.Indexes, 0, pointA,
                                            pointB);
                                        interaction.GetMaxMinXY();
                                    }
                                }
                            }
                        }
                    }

                    if (selectTool.DrawingTools == DrawingTools.CursorSelect)
                    {
                        if (interaction == null)
                        {
                            if (e.Button == MouseButtons.Left)
                            {
                                interaction = new Interaction(controlUnit.GraphicsEditor.Drawables, pointB, false);
                                if (interaction.DrawableFigures.Count == 0)
                                {
                                    interaction = null;
                                    if (DragProperty != null) DragProperty(null);
                                }
                                else if (DragProperty != null) DragProperty(interaction.DrawableFigures[0]);
                            }

                            if (e.Button == MouseButtons.Right)
                            {
                                interaction = new Interaction(controlUnit.GraphicsEditor.Drawables, pointB, true);
                                if (interaction.DrawableFigures.Count == 0)
                                {
                                    interaction = null;
                                    if (DragProperty != null) DragProperty(null);
                                }
                                else if (DragProperty != null) DragProperty(interaction.DrawableFigures[0]);
                            }
                        }
                        else
                        {
                            if (pointA.X != pointB.X && pointA.Y != pointB.Y)
                            {
                                if (!interaction.EnablePoints)
                                {
                                    if (interaction.IndexSelectPoint >= 0)
                                    {
                                        interaction.ChangeSize(pointB);
                                        controlUnit.Reconstruction(controlUnit.GraphicsEditor.Keywords[9],
                                            controlUnit.GraphicsEditor.Drawables, interaction.Indexes, 0, pointA,
                                            pointB);
                                        interaction.GetMaxMinXY();
                                    }
                                    else
                                    {
                                        controlUnit.Reconstruction(controlUnit.GraphicsEditor.Keywords[6],
                                            controlUnit.GraphicsEditor.Drawables, interaction.Indexes, 0, pointA,
                                            pointB);
                                        interaction.GetMaxMinXY();
                                    }
                                }
                                else if (interaction.IndexSelectPoint != -1)
                                {
                                    controlUnit.Reconstruction(controlUnit.GraphicsEditor.Keywords[7],
                                        controlUnit.GraphicsEditor.Drawables, interaction.Indexes,
                                        interaction.IndexSelectPoint, pointA, pointB);
                                    interaction.GetMaxMinXY();

                                }
                            }
                        }
                    }
                }

                flagMouseDown = false;
            }
        }
    }
}
