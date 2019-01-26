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
            this.labelLineType = new System.Windows.Forms.Label();
            this.comboBoxLineType = new System.Windows.Forms.ComboBox();
            this.numericUpDownLineThickness = new System.Windows.Forms.NumericUpDown();
            this.labelLineThickness = new System.Windows.Forms.Label();
            this.labelLineExtended = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLineThickness)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonSelectColorLine
            // 
            this.buttonSelectColorLine.BackColor = System.Drawing.Color.Black;
            this.buttonSelectColorLine.Location = new System.Drawing.Point(77, 3);
            this.buttonSelectColorLine.Name = "buttonSelectColorLine";
            this.buttonSelectColorLine.Size = new System.Drawing.Size(110, 25);
            this.buttonSelectColorLine.TabIndex = 36;
            this.buttonSelectColorLine.UseVisualStyleBackColor = false;
            this.buttonSelectColorLine.Click += new System.EventHandler(this.buttonSelectColorLine_Click);
            // 
            // labelLineType
            // 
            this.labelLineType.AutoSize = true;
            this.labelLineType.Location = new System.Drawing.Point(3, 63);
            this.labelLineType.Name = "labelLineType";
            this.labelLineType.Size = new System.Drawing.Size(62, 13);
            this.labelLineType.TabIndex = 34;
            this.labelLineType.Text = "Тип линии:";
            // 
            // comboBoxLineType
            // 
            this.comboBoxLineType.DisplayMember = "1";
            this.comboBoxLineType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLineType.FormattingEnabled = true;
            this.comboBoxLineType.Location = new System.Drawing.Point(66, 60);
            this.comboBoxLineType.Name = "comboBoxLineType";
            this.comboBoxLineType.Size = new System.Drawing.Size(121, 21);
            this.comboBoxLineType.TabIndex = 6;
            // 
            // numericUpDownLineThickness
            // 
            this.numericUpDownLineThickness.Location = new System.Drawing.Point(98, 34);
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
            this.labelLineThickness.Location = new System.Drawing.Point(3, 36);
            this.labelLineThickness.Name = "labelLineThickness";
            this.labelLineThickness.Size = new System.Drawing.Size(89, 13);
            this.labelLineThickness.TabIndex = 31;
            this.labelLineThickness.Text = "Толщина линии:";
            // 
            // labelLineExtended
            // 
            this.labelLineExtended.AutoSize = true;
            this.labelLineExtended.Location = new System.Drawing.Point(3, 9);
            this.labelLineExtended.Name = "labelLineExtended";
            this.labelLineExtended.Size = new System.Drawing.Size(68, 13);
            this.labelLineExtended.TabIndex = 30;
            this.labelLineExtended.Text = "Цвет линии:";
            // 
            // PropertyLineControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonSelectColorLine);
            this.Controls.Add(this.labelLineType);
            this.Controls.Add(this.comboBoxLineType);
            this.Controls.Add(this.numericUpDownLineThickness);
            this.Controls.Add(this.labelLineThickness);
            this.Controls.Add(this.labelLineExtended);
            this.Name = "PropertyLineControl";
            this.Size = new System.Drawing.Size(190, 88);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLineThickness)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSelectColorLine;
        private System.Windows.Forms.Label labelLineType;
        private System.Windows.Forms.ComboBox comboBoxLineType;
        private System.Windows.Forms.NumericUpDown numericUpDownLineThickness;
        private System.Windows.Forms.Label labelLineThickness;
        private System.Windows.Forms.Label labelLineExtended;
    }
}
