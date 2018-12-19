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
            GRPO.ExtendedForFigure extendedForFigure1 = new GRPO.ExtendedForFigure();
            GRPO.ExtendedForLine extendedForLine1 = new GRPO.ExtendedForLine();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBoxSelect = new System.Windows.Forms.GroupBox();
            this.mainPictureBox = new System.Windows.Forms.PictureBox();
            this.buttonAcceptSizePictureBox = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this._toolsWithPropertyControl = new GRPO.ToolsWithPropertyControl();
            this.groupBoxSelect.SuspendLayout();
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
            this.groupBoxSelect.Controls.Add(this._toolsWithPropertyControl);
            this.groupBoxSelect.Location = new System.Drawing.Point(12, 65);
            this.groupBoxSelect.Name = "groupBoxSelect";
            this.groupBoxSelect.Size = new System.Drawing.Size(299, 480);
            this.groupBoxSelect.TabIndex = 4;
            this.groupBoxSelect.TabStop = false;
            this.groupBoxSelect.Text = "Уструменты";
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
            // _toolsWithPropertyControl
            // 
            extendedForFigure1.FillColor = System.Drawing.Color.White;
            this._toolsWithPropertyControl.ExtendedForFigure = extendedForFigure1;
            extendedForLine1.LineColor = System.Drawing.Color.Black;
            extendedForLine1.LineThickness = 1F;
            extendedForLine1.LineType = System.Drawing.Drawing2D.DashStyle.Solid;
            this._toolsWithPropertyControl.ExtendedForLine = extendedForLine1;
            this._toolsWithPropertyControl.Location = new System.Drawing.Point(7, 20);
            this._toolsWithPropertyControl.Name = "_toolsWithPropertyControl";
            this._toolsWithPropertyControl.SelectTool = GRPO.DrawingTools.DrawFigureLine;
            this._toolsWithPropertyControl.Size = new System.Drawing.Size(207, 325);
            this._toolsWithPropertyControl.TabIndex = 19;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1193, 563);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonAcceptSizePictureBox);
            this.Controls.Add(this.groupBoxSelect);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.mainPictureBox);
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.Text = "GRPO";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.groupBoxSelect.ResumeLayout(false);
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
        private System.Windows.Forms.Button buttonAcceptSizePictureBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private ToolsWithPropertyControl _toolsWithPropertyControl;
    }
}

