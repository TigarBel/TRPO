namespace GRPO
{
    partial class ToolsWithPropertyControl
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            GRPO.FillProperty fillProperty1 = new GRPO.FillProperty();
            GRPO.LineProperty lineProperty1 = new GRPO.LineProperty();
            this._toolsControl = new GRPO.ToolsControl();
            this._fillFigureControl = new GRPO.FillFigureControl();
            this._propertyLineControl = new GRPO.PropertyLineControl();
            this.SuspendLayout();
            // 
            // _toolsControl
            // 
            this._toolsControl.Location = new System.Drawing.Point(3, 3);
            this._toolsControl.Name = "_toolsControl";
            this._toolsControl.Size = new System.Drawing.Size(201, 84);
            this._toolsControl.TabIndex = 3;
            // 
            // _fillFigureControl
            // 
            fillProperty1.FillColor = System.Drawing.Color.White;
            this._fillFigureControl.FillProperty = fillProperty1;
            this._fillFigureControl.Location = new System.Drawing.Point(3, 245);
            this._fillFigureControl.Name = "_fillFigureControl";
            this._fillFigureControl.Size = new System.Drawing.Size(182, 81);
            this._fillFigureControl.TabIndex = 5;
            this._fillFigureControl.Visible = false;
            // 
            // _propertyLineControl
            // 
            lineProperty1.LineColor = System.Drawing.Color.Black;
            lineProperty1.LineThickness = 1F;
            lineProperty1.LineType = System.Drawing.Drawing2D.DashStyle.Solid;
            this._propertyLineControl.LineProperty = lineProperty1;
            this._propertyLineControl.Location = new System.Drawing.Point(3, 93);
            this._propertyLineControl.Name = "_propertyLineControl";
            this._propertyLineControl.Size = new System.Drawing.Size(190, 146);
            this._propertyLineControl.TabIndex = 4;
            // 
            // ToolsWithPropertyControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._toolsControl);
            this.Controls.Add(this._fillFigureControl);
            this.Controls.Add(this._propertyLineControl);
            this.Name = "ToolsWithPropertyControl";
            this.Size = new System.Drawing.Size(207, 325);
            this.ResumeLayout(false);

        }

        #endregion

        private ToolsControl _toolsControl;
        private FillFigureControl _fillFigureControl;
        private PropertyLineControl _propertyLineControl;
    }
}
