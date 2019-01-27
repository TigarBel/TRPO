﻿using GRPO.Drawing;
using GRPO.Drawing.Interface;
using GRPO.Drawing.Property;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GRPO.Commands;

namespace GRPO.UserControls.CanvasMouseStrategy
{
    /// <summary>
    /// Базовый класс стратегии использивония мыши
    /// </summary>
    public abstract class BaseMouseStrategy
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
        public abstract void MouseAction(ref bool flagMouseDown, ref bool flagPolyFigure, Tools selectTool,
            LineProperty lineProperty, FillProperty fillProperty, PictureBox canvas, ref List<IDrawable> drawables,
            ref ControlUnit controlUnit,
            Point pointA, Point pointB, ref Point pointC, ref Interaction interaction, MouseEventArgs e);
    }
}
