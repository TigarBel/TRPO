namespace GRPO
{
    partial class PropertyLineControl
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
            this.buttonSelectColorLine = new System.Windows.Forms.Button();
            this.buttonWhiteColorLine = new System.Windows.Forms.Button();
            this.labelLineType = new System.Windows.Forms.Label();
            this.comboBoxLineType = new System.Windows.Forms.ComboBox();
            this.numericUpDownLineThickness = new System.Windows.Forms.NumericUpDown();
            this.labelLineThickness = new System.Windows.Forms.Label();
            this.labelLineExtended = new System.Windows.Forms.Label();
            this.buttonColorLine = new System.Windows.Forms.Button();
            this.buttonRedColorLine = new System.Windows.Forms.Button();
            this.buttonBlackColorLine = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLineThickness)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonSelectColorLine
            // 
            this.buttonSelectColorLine.BackColor = System.Drawing.Color.Black;
            this.buttonSelectColorLine.Enabled = false;
            this.buttonSelectColorLine.Location = new System.Drawing.Point(127, 47);
            this.buttonSelectColorLine.Name = "buttonSelectColorLine";
            this.buttonSelectColorLine.Size = new System.Drawing.Size(50, 25);
            this.buttonSelectColorLine.TabIndex = 36;
            this.buttonSelectColorLine.UseVisualStyleBackColor = false;
            // 
            // buttonWhiteColorLine
            // 
            this.buttonWhiteColorLine.BackColor = System.Drawing.Color.White;
            this.buttonWhiteColorLine.Location = new System.Drawing.Point(34, 16);
            this.buttonWhiteColorLine.Name = "buttonWhiteColorLine";
            this.buttonWhiteColorLine.Size = new System.Drawing.Size(25, 25);
            this.buttonWhiteColorLine.TabIndex = 2;
            this.buttonWhiteColorLine.UseVisualStyleBackColor = false;
            this.buttonWhiteColorLine.Click += new System.EventHandler(this.buttonWhiteColorLine_Click);
            // 
            // labelLineType
            // 
            this.labelLineType.AutoSize = true;
            this.labelLineType.Location = new System.Drawing.Point(3, 121);
            this.labelLineType.Name = "labelLineType";
            this.labelLineType.Size = new System.Drawing.Size(54, 13);
            this.labelLineType.TabIndex = 34;
            this.labelLineType.Text = "Line Type";
            // 
            // comboBoxLineType
            // 
            this.comboBoxLineType.DisplayMember = "1";
            this.comboBoxLineType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLineType.FormattingEnabled = true;
            this.comboBoxLineType.Location = new System.Drawing.Point(63, 118);
            this.comboBoxLineType.Name = "comboBoxLineType";
            this.comboBoxLineType.Size = new System.Drawing.Size(121, 21);
            this.comboBoxLineType.TabIndex = 6;
            // 
            // numericUpDownLineThickness
            // 
            this.numericUpDownLineThickness.Location = new System.Drawing.Point(87, 84);
            this.numericUpDownLineThickness.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.numericUpDownLineThickness.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownLineThickness.Name = "numericUpDownLineThickness";
            this.numericUpDownLineThickness.Size = new System.Drawing.Size(39, 20);
            this.numericUpDownLineThickness.TabIndex = 5;
            this.numericUpDownLineThickness.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // labelLineThickness
            // 
            this.labelLineThickness.AutoSize = true;
            this.labelLineThickness.Location = new System.Drawing.Point(3, 86);
            this.labelLineThickness.Name = "labelLineThickness";
            this.labelLineThickness.Size = new System.Drawing.Size(78, 13);
            this.labelLineThickness.TabIndex = 31;
            this.labelLineThickness.Text = "Line thickness:";
            // 
            // labelLineExtended
            // 
            this.labelLineExtended.AutoSize = true;
            this.labelLineExtended.Location = new System.Drawing.Point(3, 0);
            this.labelLineExtended.Name = "labelLineExtended";
            this.labelLineExtended.Size = new System.Drawing.Size(30, 13);
            this.labelLineExtended.TabIndex = 30;
            this.labelLineExtended.Text = "Line:";
            // 
            // buttonColorLine
            // 
            this.buttonColorLine.Location = new System.Drawing.Point(3, 47);
            this.buttonColorLine.Name = "buttonColorLine";
            this.buttonColorLine.Size = new System.Drawing.Size(118, 25);
            this.buttonColorLine.TabIndex = 4;
            this.buttonColorLine.Text = "Select color";
            this.buttonColorLine.UseVisualStyleBackColor = true;
            this.buttonColorLine.Click += new System.EventHandler(this.buttonColorLine_Click);
            // 
            // buttonRedColorLine
            // 
            this.buttonRedColorLine.BackColor = System.Drawing.Color.Red;
            this.buttonRedColorLine.Location = new System.Drawing.Point(65, 16);
            this.buttonRedColorLine.Name = "buttonRedColorLine";
            this.buttonRedColorLine.Size = new System.Drawing.Size(25, 25);
            this.buttonRedColorLine.TabIndex = 3;
            this.buttonRedColorLine.UseVisualStyleBackColor = false;
            this.buttonRedColorLine.Click += new System.EventHandler(this.buttonRedColorLine_Click);
            // 
            // buttonBlackColorLine
            // 
            this.buttonBlackColorLine.BackColor = System.Drawing.Color.Black;
            this.buttonBlackColorLine.Location = new System.Drawing.Point(3, 16);
            this.buttonBlackColorLine.Name = "buttonBlackColorLine";
            this.buttonBlackColorLine.Size = new System.Drawing.Size(25, 25);
            this.buttonBlackColorLine.TabIndex = 1;
            this.buttonBlackColorLine.UseVisualStyleBackColor = false;
            this.buttonBlackColorLine.Click += new System.EventHandler(this.buttonBlackColorLine_Click);
            // 
            // PropertyLineControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonSelectColorLine);
            this.Controls.Add(this.buttonWhiteColorLine);
            this.Controls.Add(this.labelLineType);
            this.Controls.Add(this.comboBoxLineType);
            this.Controls.Add(this.numericUpDownLineThickness);
            this.Controls.Add(this.labelLineThickness);
            this.Controls.Add(this.labelLineExtended);
            this.Controls.Add(this.buttonColorLine);
            this.Controls.Add(this.buttonRedColorLine);
            this.Controls.Add(this.buttonBlackColorLine);
            this.Name = "PropertyLineControl";
            this.Size = new System.Drawing.Size(190, 146);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLineThickness)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSelectColorLine;
        private System.Windows.Forms.Button buttonWhiteColorLine;
        private System.Windows.Forms.Label labelLineType;
        private System.Windows.Forms.ComboBox comboBoxLineType;
        private System.Windows.Forms.NumericUpDown numericUpDownLineThickness;
        private System.Windows.Forms.Label labelLineThickness;
        private System.Windows.Forms.Label labelLineExtended;
        private System.Windows.Forms.Button buttonColorLine;
        private System.Windows.Forms.Button buttonRedColorLine;
        private System.Windows.Forms.Button buttonBlackColorLine;
    }
}
