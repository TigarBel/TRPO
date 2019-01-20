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
            this.buttonColorFill = new System.Windows.Forms.Button();
            this.buttonRedColorFill = new System.Windows.Forms.Button();
            this.buttonBlackColorFill = new System.Windows.Forms.Button();
            this.labelFillExtended = new System.Windows.Forms.Label();
            this.buttonWhiteColorFill = new System.Windows.Forms.Button();
            this.buttonSelectColorFill = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonColorFill
            // 
            this.buttonColorFill.Location = new System.Drawing.Point(3, 47);
            this.buttonColorFill.Name = "buttonColorFill";
            this.buttonColorFill.Size = new System.Drawing.Size(118, 25);
            this.buttonColorFill.TabIndex = 4;
            this.buttonColorFill.Text = "Select color";
            this.buttonColorFill.UseVisualStyleBackColor = true;
            this.buttonColorFill.Click += new System.EventHandler(this.buttonColorFill_Click);
            // 
            // buttonRedColorFill
            // 
            this.buttonRedColorFill.BackColor = System.Drawing.Color.Red;
            this.buttonRedColorFill.Location = new System.Drawing.Point(65, 16);
            this.buttonRedColorFill.Name = "buttonRedColorFill";
            this.buttonRedColorFill.Size = new System.Drawing.Size(25, 25);
            this.buttonRedColorFill.TabIndex = 3;
            this.buttonRedColorFill.UseVisualStyleBackColor = false;
            this.buttonRedColorFill.Click += new System.EventHandler(this.buttonRedColorFill_Click);
            // 
            // buttonBlackColorFill
            // 
            this.buttonBlackColorFill.BackColor = System.Drawing.Color.Black;
            this.buttonBlackColorFill.Location = new System.Drawing.Point(34, 16);
            this.buttonBlackColorFill.Name = "buttonBlackColorFill";
            this.buttonBlackColorFill.Size = new System.Drawing.Size(25, 25);
            this.buttonBlackColorFill.TabIndex = 2;
            this.buttonBlackColorFill.UseVisualStyleBackColor = false;
            this.buttonBlackColorFill.Click += new System.EventHandler(this.buttonBlackColorFill_Click);
            // 
            // labelFillExtended
            // 
            this.labelFillExtended.AutoSize = true;
            this.labelFillExtended.Location = new System.Drawing.Point(3, 0);
            this.labelFillExtended.Name = "labelFillExtended";
            this.labelFillExtended.Size = new System.Drawing.Size(22, 13);
            this.labelFillExtended.TabIndex = 13;
            this.labelFillExtended.Text = "Fill:";
            // 
            // buttonWhiteColorFill
            // 
            this.buttonWhiteColorFill.BackColor = System.Drawing.Color.White;
            this.buttonWhiteColorFill.Location = new System.Drawing.Point(3, 16);
            this.buttonWhiteColorFill.Name = "buttonWhiteColorFill";
            this.buttonWhiteColorFill.Size = new System.Drawing.Size(25, 25);
            this.buttonWhiteColorFill.TabIndex = 1;
            this.buttonWhiteColorFill.UseVisualStyleBackColor = false;
            this.buttonWhiteColorFill.Click += new System.EventHandler(this.buttonWhiteColorFill_Click);
            // 
            // buttonSelectColorFill
            // 
            this.buttonSelectColorFill.BackColor = System.Drawing.Color.White;
            this.buttonSelectColorFill.Enabled = false;
            this.buttonSelectColorFill.Location = new System.Drawing.Point(127, 47);
            this.buttonSelectColorFill.Name = "buttonSelectColorFill";
            this.buttonSelectColorFill.Size = new System.Drawing.Size(50, 25);
            this.buttonSelectColorFill.TabIndex = 18;
            this.buttonSelectColorFill.UseVisualStyleBackColor = false;
            // 
            // FillFigureControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonSelectColorFill);
            this.Controls.Add(this.buttonWhiteColorFill);
            this.Controls.Add(this.buttonColorFill);
            this.Controls.Add(this.buttonRedColorFill);
            this.Controls.Add(this.buttonBlackColorFill);
            this.Controls.Add(this.labelFillExtended);
            this.Name = "FillFigureControl";
            this.Size = new System.Drawing.Size(182, 81);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonColorFill;
        private System.Windows.Forms.Button buttonRedColorFill;
        private System.Windows.Forms.Button buttonBlackColorFill;
        private System.Windows.Forms.Label labelFillExtended;
        private System.Windows.Forms.Button buttonWhiteColorFill;
        private System.Windows.Forms.Button buttonSelectColorFill;
    }
}
