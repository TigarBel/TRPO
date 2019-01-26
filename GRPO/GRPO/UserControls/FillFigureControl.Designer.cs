namespace GRPO
{
    partial class FillFigureControl
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
            this.labelFillExtended = new System.Windows.Forms.Label();
            this.buttonSelectColorFill = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelFillExtended
            // 
            this.labelFillExtended.AutoSize = true;
            this.labelFillExtended.Location = new System.Drawing.Point(3, 9);
            this.labelFillExtended.Name = "labelFillExtended";
            this.labelFillExtended.Size = new System.Drawing.Size(80, 13);
            this.labelFillExtended.TabIndex = 13;
            this.labelFillExtended.Text = "Цвет заливки:";
            // 
            // buttonSelectColorFill
            // 
            this.buttonSelectColorFill.BackColor = System.Drawing.Color.White;
            this.buttonSelectColorFill.Location = new System.Drawing.Point(89, 3);
            this.buttonSelectColorFill.Name = "buttonSelectColorFill";
            this.buttonSelectColorFill.Size = new System.Drawing.Size(90, 25);
            this.buttonSelectColorFill.TabIndex = 18;
            this.buttonSelectColorFill.UseVisualStyleBackColor = false;
            this.buttonSelectColorFill.Click += new System.EventHandler(this.buttonSelectColorFill_Click);
            // 
            // FillFigureControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonSelectColorFill);
            this.Controls.Add(this.labelFillExtended);
            this.Name = "FillFigureControl";
            this.Size = new System.Drawing.Size(182, 33);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelFillExtended;
        private System.Windows.Forms.Button buttonSelectColorFill;
    }
}
