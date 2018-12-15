namespace GRPO
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            GRPO.ExtendedForLine extendedForLine1 = new GRPO.ExtendedForLine();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBoxSelect = new System.Windows.Forms.GroupBox();
            this.labelLineType = new System.Windows.Forms.Label();
            this.comboBoxLineType = new System.Windows.Forms.ComboBox();
            this.numericUpDownLineThickness = new System.Windows.Forms.NumericUpDown();
            this.labelLineThickness = new System.Windows.Forms.Label();
            this.buttonColorFill = new System.Windows.Forms.Button();
            this.buttonRedColorFill = new System.Windows.Forms.Button();
            this.buttonBlackColorFill = new System.Windows.Forms.Button();
            this.labelFillExtended = new System.Windows.Forms.Label();
            this.labelLineExtended = new System.Windows.Forms.Label();
            this.buttonColorLine = new System.Windows.Forms.Button();
            this.buttonRedColorLine = new System.Windows.Forms.Button();
            this.buttonBlackColorLine = new System.Windows.Forms.Button();
            this.radioButtonEllipse = new System.Windows.Forms.RadioButton();
            this.radioButtonCircle = new System.Windows.Forms.RadioButton();
            this.radioButtonPolygon = new System.Windows.Forms.RadioButton();
            this.radioButtonPolyline = new System.Windows.Forms.RadioButton();
            this.radioButtonLine = new System.Windows.Forms.RadioButton();
            this.mainPictureBox = new System.Windows.Forms.PictureBox();
            this.buttonAcceptSizePictureBox = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this._propertyLineControl = new GRPO.PropertyLineControl();
            this.groupBoxSelect.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLineThickness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(963, 457);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(109, 88);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(963, 65);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(109, 90);
            this.button2.TabIndex = 2;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1078, 65);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(109, 90);
            this.button3.TabIndex = 3;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // groupBoxSelect
            // 
            this.groupBoxSelect.Controls.Add(this.labelLineType);
            this.groupBoxSelect.Controls.Add(this.comboBoxLineType);
            this.groupBoxSelect.Controls.Add(this.numericUpDownLineThickness);
            this.groupBoxSelect.Controls.Add(this.labelLineThickness);
            this.groupBoxSelect.Controls.Add(this.buttonColorFill);
            this.groupBoxSelect.Controls.Add(this.buttonRedColorFill);
            this.groupBoxSelect.Controls.Add(this.buttonBlackColorFill);
            this.groupBoxSelect.Controls.Add(this.labelFillExtended);
            this.groupBoxSelect.Controls.Add(this.labelLineExtended);
            this.groupBoxSelect.Controls.Add(this.buttonColorLine);
            this.groupBoxSelect.Controls.Add(this.buttonRedColorLine);
            this.groupBoxSelect.Controls.Add(this.buttonBlackColorLine);
            this.groupBoxSelect.Controls.Add(this.radioButtonEllipse);
            this.groupBoxSelect.Controls.Add(this.radioButtonCircle);
            this.groupBoxSelect.Controls.Add(this.radioButtonPolygon);
            this.groupBoxSelect.Controls.Add(this.radioButtonPolyline);
            this.groupBoxSelect.Controls.Add(this.radioButtonLine);
            this.groupBoxSelect.Location = new System.Drawing.Point(12, 65);
            this.groupBoxSelect.Name = "groupBoxSelect";
            this.groupBoxSelect.Size = new System.Drawing.Size(299, 307);
            this.groupBoxSelect.TabIndex = 4;
            this.groupBoxSelect.TabStop = false;
            this.groupBoxSelect.Text = "Уструменты";
            // 
            // labelLineType
            // 
            this.labelLineType.AutoSize = true;
            this.labelLineType.Location = new System.Drawing.Point(92, 196);
            this.labelLineType.Name = "labelLineType";
            this.labelLineType.Size = new System.Drawing.Size(54, 13);
            this.labelLineType.TabIndex = 16;
            this.labelLineType.Text = "Line Type";
            // 
            // comboBoxLineType
            // 
            this.comboBoxLineType.DisplayMember = "1";
            this.comboBoxLineType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLineType.FormattingEnabled = true;
            this.comboBoxLineType.Items.AddRange(new object[] {
            "Solid",
            "Dash",
            "DashDot",
            "DashDotDot",
            "Dot"});
            this.comboBoxLineType.Location = new System.Drawing.Point(152, 193);
            this.comboBoxLineType.Name = "comboBoxLineType";
            this.comboBoxLineType.Size = new System.Drawing.Size(121, 21);
            this.comboBoxLineType.TabIndex = 15;
            this.comboBoxLineType.SelectedIndexChanged += new System.EventHandler(this.comboBoxLineType_SelectedIndexChanged);
            // 
            // numericUpDownLineThickness
            // 
            this.numericUpDownLineThickness.Location = new System.Drawing.Point(152, 141);
            this.numericUpDownLineThickness.Maximum = new decimal(new int[] {
            30,
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
            this.numericUpDownLineThickness.TabIndex = 14;
            this.numericUpDownLineThickness.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownLineThickness.ValueChanged += new System.EventHandler(this.numericUpDownLineThickness_ValueChanged);
            // 
            // labelLineThickness
            // 
            this.labelLineThickness.AutoSize = true;
            this.labelLineThickness.Location = new System.Drawing.Point(68, 143);
            this.labelLineThickness.Name = "labelLineThickness";
            this.labelLineThickness.Size = new System.Drawing.Size(78, 13);
            this.labelLineThickness.TabIndex = 13;
            this.labelLineThickness.Text = "Line thickness:";
            // 
            // buttonColorFill
            // 
            this.buttonColorFill.Location = new System.Drawing.Point(6, 278);
            this.buttonColorFill.Name = "buttonColorFill";
            this.buttonColorFill.Size = new System.Drawing.Size(56, 25);
            this.buttonColorFill.TabIndex = 12;
            this.buttonColorFill.Text = "Color";
            this.buttonColorFill.UseVisualStyleBackColor = true;
            this.buttonColorFill.Click += new System.EventHandler(this.buttonColorFill_Click);
            // 
            // buttonRedColorFill
            // 
            this.buttonRedColorFill.BackColor = System.Drawing.Color.Red;
            this.buttonRedColorFill.Location = new System.Drawing.Point(37, 247);
            this.buttonRedColorFill.Name = "buttonRedColorFill";
            this.buttonRedColorFill.Size = new System.Drawing.Size(25, 25);
            this.buttonRedColorFill.TabIndex = 11;
            this.buttonRedColorFill.UseVisualStyleBackColor = false;
            this.buttonRedColorFill.Click += new System.EventHandler(this.buttonRedColorFill_Click);
            // 
            // buttonBlackColorFill
            // 
            this.buttonBlackColorFill.BackColor = System.Drawing.Color.Black;
            this.buttonBlackColorFill.Location = new System.Drawing.Point(6, 247);
            this.buttonBlackColorFill.Name = "buttonBlackColorFill";
            this.buttonBlackColorFill.Size = new System.Drawing.Size(25, 25);
            this.buttonBlackColorFill.TabIndex = 10;
            this.buttonBlackColorFill.UseVisualStyleBackColor = false;
            this.buttonBlackColorFill.Click += new System.EventHandler(this.buttonBlackColorFill_Click);
            // 
            // labelFillExtended
            // 
            this.labelFillExtended.AutoSize = true;
            this.labelFillExtended.Location = new System.Drawing.Point(6, 231);
            this.labelFillExtended.Name = "labelFillExtended";
            this.labelFillExtended.Size = new System.Drawing.Size(22, 13);
            this.labelFillExtended.TabIndex = 9;
            this.labelFillExtended.Text = "Fill:";
            // 
            // labelLineExtended
            // 
            this.labelLineExtended.AutoSize = true;
            this.labelLineExtended.Location = new System.Drawing.Point(6, 143);
            this.labelLineExtended.Name = "labelLineExtended";
            this.labelLineExtended.Size = new System.Drawing.Size(30, 13);
            this.labelLineExtended.TabIndex = 8;
            this.labelLineExtended.Text = "Line:";
            // 
            // buttonColorLine
            // 
            this.buttonColorLine.Location = new System.Drawing.Point(6, 190);
            this.buttonColorLine.Name = "buttonColorLine";
            this.buttonColorLine.Size = new System.Drawing.Size(56, 25);
            this.buttonColorLine.TabIndex = 7;
            this.buttonColorLine.Text = "Color";
            this.buttonColorLine.UseVisualStyleBackColor = true;
            this.buttonColorLine.Click += new System.EventHandler(this.buttonColorLine_Click);
            // 
            // buttonRedColorLine
            // 
            this.buttonRedColorLine.BackColor = System.Drawing.Color.Red;
            this.buttonRedColorLine.Location = new System.Drawing.Point(37, 159);
            this.buttonRedColorLine.Name = "buttonRedColorLine";
            this.buttonRedColorLine.Size = new System.Drawing.Size(25, 25);
            this.buttonRedColorLine.TabIndex = 6;
            this.buttonRedColorLine.UseVisualStyleBackColor = false;
            this.buttonRedColorLine.Click += new System.EventHandler(this.buttonRedColorLine_Click);
            // 
            // buttonBlackColorLine
            // 
            this.buttonBlackColorLine.BackColor = System.Drawing.Color.Black;
            this.buttonBlackColorLine.Location = new System.Drawing.Point(6, 159);
            this.buttonBlackColorLine.Name = "buttonBlackColorLine";
            this.buttonBlackColorLine.Size = new System.Drawing.Size(25, 25);
            this.buttonBlackColorLine.TabIndex = 5;
            this.buttonBlackColorLine.UseVisualStyleBackColor = false;
            this.buttonBlackColorLine.Click += new System.EventHandler(this.buttonBlackColorLine_Click);
            // 
            // radioButtonEllipse
            // 
            this.radioButtonEllipse.AutoSize = true;
            this.radioButtonEllipse.Location = new System.Drawing.Point(6, 111);
            this.radioButtonEllipse.Name = "radioButtonEllipse";
            this.radioButtonEllipse.Size = new System.Drawing.Size(109, 17);
            this.radioButtonEllipse.TabIndex = 4;
            this.radioButtonEllipse.Text = "radioButtonEllipse";
            this.radioButtonEllipse.UseVisualStyleBackColor = true;
            this.radioButtonEllipse.Click += new System.EventHandler(this.radioButtonEllipse_Click);
            // 
            // radioButtonCircle
            // 
            this.radioButtonCircle.AutoSize = true;
            this.radioButtonCircle.Location = new System.Drawing.Point(6, 88);
            this.radioButtonCircle.Name = "radioButtonCircle";
            this.radioButtonCircle.Size = new System.Drawing.Size(105, 17);
            this.radioButtonCircle.TabIndex = 3;
            this.radioButtonCircle.Text = "radioButtonCircle";
            this.radioButtonCircle.UseVisualStyleBackColor = true;
            this.radioButtonCircle.Click += new System.EventHandler(this.radioButtonCircle_Click);
            // 
            // radioButtonPolygon
            // 
            this.radioButtonPolygon.AutoSize = true;
            this.radioButtonPolygon.Location = new System.Drawing.Point(6, 65);
            this.radioButtonPolygon.Name = "radioButtonPolygon";
            this.radioButtonPolygon.Size = new System.Drawing.Size(117, 17);
            this.radioButtonPolygon.TabIndex = 2;
            this.radioButtonPolygon.Text = "radioButtonPolygon";
            this.radioButtonPolygon.UseVisualStyleBackColor = true;
            this.radioButtonPolygon.Click += new System.EventHandler(this.radioButtonPolygon_Click);
            // 
            // radioButtonPolyline
            // 
            this.radioButtonPolyline.AutoSize = true;
            this.radioButtonPolyline.Location = new System.Drawing.Point(6, 42);
            this.radioButtonPolyline.Name = "radioButtonPolyline";
            this.radioButtonPolyline.Size = new System.Drawing.Size(115, 17);
            this.radioButtonPolyline.TabIndex = 1;
            this.radioButtonPolyline.Text = "radioButtonPolyline";
            this.radioButtonPolyline.UseVisualStyleBackColor = true;
            this.radioButtonPolyline.Click += new System.EventHandler(this.radioButtonPolyline_Click);
            // 
            // radioButtonLine
            // 
            this.radioButtonLine.AutoSize = true;
            this.radioButtonLine.Checked = true;
            this.radioButtonLine.Location = new System.Drawing.Point(6, 19);
            this.radioButtonLine.Name = "radioButtonLine";
            this.radioButtonLine.Size = new System.Drawing.Size(99, 17);
            this.radioButtonLine.TabIndex = 0;
            this.radioButtonLine.TabStop = true;
            this.radioButtonLine.Text = "radioButtonLine";
            this.radioButtonLine.UseVisualStyleBackColor = true;
            this.radioButtonLine.Click += new System.EventHandler(this.radioButtonLine_Click);
            // 
            // mainPictureBox
            // 
            this.mainPictureBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.mainPictureBox.Location = new System.Drawing.Point(317, 65);
            this.mainPictureBox.Name = "mainPictureBox";
            this.mainPictureBox.Size = new System.Drawing.Size(640, 480);
            this.mainPictureBox.TabIndex = 0;
            this.mainPictureBox.TabStop = false;
            this.mainPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mainPictureBox_MouseDown);
            this.mainPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mainPictureBox_MouseMove);
            this.mainPictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mainPictureBox_MouseUp);
            // 
            // buttonAcceptSizePictureBox
            // 
            this.buttonAcceptSizePictureBox.Location = new System.Drawing.Point(236, 12);
            this.buttonAcceptSizePictureBox.Name = "buttonAcceptSizePictureBox";
            this.buttonAcceptSizePictureBox.Size = new System.Drawing.Size(75, 23);
            this.buttonAcceptSizePictureBox.TabIndex = 5;
            this.buttonAcceptSizePictureBox.Text = "Accept";
            this.buttonAcceptSizePictureBox.UseVisualStyleBackColor = true;
            this.buttonAcceptSizePictureBox.Click += new System.EventHandler(this.buttonAcceptSizePictureBox_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Size picture box:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(103, 14);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(55, 20);
            this.textBox1.TabIndex = 7;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(175, 14);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(55, 20);
            this.textBox2.TabIndex = 8;
            // 
            // _propertyLineControl
            // 
            extendedForLine1.LineColor = System.Drawing.Color.Black;
            extendedForLine1.LineThickness = 1F;
            extendedForLine1.LineType = System.Drawing.Drawing2D.DashStyle.Solid;
            this._propertyLineControl.Extended = extendedForLine1;
            this._propertyLineControl.Location = new System.Drawing.Point(12, 378);
            this._propertyLineControl.Name = "_propertyLineControl";
            this._propertyLineControl.Size = new System.Drawing.Size(190, 146);
            this._propertyLineControl.TabIndex = 9;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1193, 604);
            this.Controls.Add(this._propertyLineControl);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonAcceptSizePictureBox);
            this.Controls.Add(this.groupBoxSelect);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.mainPictureBox);
            this.Name = "MainForm";
            this.Text = "GRPO";
            this.groupBoxSelect.ResumeLayout(false);
            this.groupBoxSelect.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLineThickness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox mainPictureBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox groupBoxSelect;
        private System.Windows.Forms.RadioButton radioButtonEllipse;
        private System.Windows.Forms.RadioButton radioButtonCircle;
        private System.Windows.Forms.RadioButton radioButtonPolygon;
        private System.Windows.Forms.RadioButton radioButtonPolyline;
        private System.Windows.Forms.RadioButton radioButtonLine;
        private System.Windows.Forms.Button buttonColorLine;
        private System.Windows.Forms.Button buttonRedColorLine;
        private System.Windows.Forms.Button buttonBlackColorLine;
        private System.Windows.Forms.Label labelFillExtended;
        private System.Windows.Forms.Label labelLineExtended;
        private System.Windows.Forms.Button buttonColorFill;
        private System.Windows.Forms.Button buttonRedColorFill;
        private System.Windows.Forms.Button buttonBlackColorFill;
        private System.Windows.Forms.Label labelLineThickness;
        private System.Windows.Forms.NumericUpDown numericUpDownLineThickness;
        private System.Windows.Forms.Label labelLineType;
        private System.Windows.Forms.ComboBox comboBoxLineType;
        private System.Windows.Forms.Button buttonAcceptSizePictureBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private PropertyLineControl _propertyLineControl;
    }
}

