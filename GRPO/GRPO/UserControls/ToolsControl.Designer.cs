namespace GRPO
{
    partial class ToolsControl
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
            this.buttonMassSelect = new System.Windows.Forms.Button();
            this.buttonCursorSelect = new System.Windows.Forms.Button();
            this.buttonFigureEllips = new System.Windows.Forms.Button();
            this.buttonFigureCircle = new System.Windows.Forms.Button();
            this.buttonFigureRectangle = new System.Windows.Forms.Button();
            this.buttonFigurePolyline = new System.Windows.Forms.Button();
            this.buttonFigureLine = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonMassSelect
            // 
            this.buttonMassSelect.BackColor = System.Drawing.Color.White;
            this.buttonMassSelect.Image = global::GRPO.Properties.Resources.mass_select;
            this.buttonMassSelect.Location = new System.Drawing.Point(44, 3);
            this.buttonMassSelect.Name = "buttonMassSelect";
            this.buttonMassSelect.Size = new System.Drawing.Size(35, 35);
            this.buttonMassSelect.TabIndex = 2;
            this.buttonMassSelect.UseVisualStyleBackColor = false;
            // 
            // buttonCursorSelect
            // 
            this.buttonCursorSelect.BackColor = System.Drawing.Color.White;
            this.buttonCursorSelect.Image = global::GRPO.Properties.Resources.cursor;
            this.buttonCursorSelect.Location = new System.Drawing.Point(3, 3);
            this.buttonCursorSelect.Name = "buttonCursorSelect";
            this.buttonCursorSelect.Size = new System.Drawing.Size(35, 35);
            this.buttonCursorSelect.TabIndex = 1;
            this.buttonCursorSelect.UseVisualStyleBackColor = false;
            // 
            // buttonFigureEllips
            // 
            this.buttonFigureEllips.BackColor = System.Drawing.Color.White;
            this.buttonFigureEllips.Image = global::GRPO.Properties.Resources.ellips;
            this.buttonFigureEllips.Location = new System.Drawing.Point(85, 44);
            this.buttonFigureEllips.Name = "buttonFigureEllips";
            this.buttonFigureEllips.Size = new System.Drawing.Size(35, 35);
            this.buttonFigureEllips.TabIndex = 7;
            this.buttonFigureEllips.UseVisualStyleBackColor = false;
            // 
            // buttonFigureCircle
            // 
            this.buttonFigureCircle.BackColor = System.Drawing.Color.White;
            this.buttonFigureCircle.Image = global::GRPO.Properties.Resources.circle;
            this.buttonFigureCircle.Location = new System.Drawing.Point(44, 44);
            this.buttonFigureCircle.Name = "buttonFigureCircle";
            this.buttonFigureCircle.Size = new System.Drawing.Size(35, 35);
            this.buttonFigureCircle.TabIndex = 6;
            this.buttonFigureCircle.UseVisualStyleBackColor = false;
            // 
            // buttonFigureRectangle
            // 
            this.buttonFigureRectangle.BackColor = System.Drawing.Color.White;
            this.buttonFigureRectangle.Image = global::GRPO.Properties.Resources.squer;
            this.buttonFigureRectangle.Location = new System.Drawing.Point(3, 44);
            this.buttonFigureRectangle.Name = "buttonFigureRectangle";
            this.buttonFigureRectangle.Size = new System.Drawing.Size(35, 35);
            this.buttonFigureRectangle.TabIndex = 5;
            this.buttonFigureRectangle.UseVisualStyleBackColor = false;
            // 
            // buttonFigurePolyline
            // 
            this.buttonFigurePolyline.BackColor = System.Drawing.Color.White;
            this.buttonFigurePolyline.Image = global::GRPO.Properties.Resources.polyline;
            this.buttonFigurePolyline.Location = new System.Drawing.Point(126, 3);
            this.buttonFigurePolyline.Name = "buttonFigurePolyline";
            this.buttonFigurePolyline.Size = new System.Drawing.Size(35, 35);
            this.buttonFigurePolyline.TabIndex = 4;
            this.buttonFigurePolyline.UseVisualStyleBackColor = false;
            // 
            // buttonFigureLine
            // 
            this.buttonFigureLine.BackColor = System.Drawing.Color.Black;
            this.buttonFigureLine.Image = global::GRPO.Properties.Resources.line;
            this.buttonFigureLine.Location = new System.Drawing.Point(85, 3);
            this.buttonFigureLine.Name = "buttonFigureLine";
            this.buttonFigureLine.Size = new System.Drawing.Size(35, 35);
            this.buttonFigureLine.TabIndex = 3;
            this.buttonFigureLine.UseVisualStyleBackColor = false;
            // 
            // ToolsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonMassSelect);
            this.Controls.Add(this.buttonCursorSelect);
            this.Controls.Add(this.buttonFigureEllips);
            this.Controls.Add(this.buttonFigureCircle);
            this.Controls.Add(this.buttonFigureRectangle);
            this.Controls.Add(this.buttonFigurePolyline);
            this.Controls.Add(this.buttonFigureLine);
            this.Name = "ToolsControl";
            this.Size = new System.Drawing.Size(168, 84);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonFigureLine;
        private System.Windows.Forms.Button buttonFigurePolyline;
        private System.Windows.Forms.Button buttonFigureRectangle;
        private System.Windows.Forms.Button buttonFigureCircle;
        private System.Windows.Forms.Button buttonFigureEllips;
        private System.Windows.Forms.Button buttonCursorSelect;
        private System.Windows.Forms.Button buttonMassSelect;
    }
}
