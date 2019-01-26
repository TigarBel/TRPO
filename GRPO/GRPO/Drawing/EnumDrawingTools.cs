namespace GRPO.Drawing
{
    /// <summary>
    /// Набор инструментов для рисования
    /// </summary>
    public enum DrawingTools
    {
        //Инструменты для выделения фигур
        CursorSelect = 0,
        MassSelect,

        //Инструменты для отрисовки простых фигур
        DrawFigureLine,
        DrawFigureRectangle,
        DrawFigureCircle,
        DrawFigureEllipse,

        //Инструменты для полифигур
        DrawFigurePolyline,
        DrawFigurePolygon,
    }
}
