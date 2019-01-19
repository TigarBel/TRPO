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
            GRPO.Drawing.Property.FillProperty fillProperty1 = new GRPO.Drawing.Property.FillProperty();
            GRPO.Drawing.Property.LineProperty lineProperty1 = new GRPO.Drawing.Property.LineProperty();
            GRPO.Drawing.Property.FillProperty fillProperty2 = new GRPO.Drawing.Property.FillProperty();
            GRPO.Drawing.Property.LineProperty lineProperty2 = new GRPO.Drawing.Property.LineProperty();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBoxSelect = new System.Windows.Forms.GroupBox();
            this._toolsWithPropertyControl = new GRPO.ToolsWithPropertyControl();
            this.buttonAcceptSizePictureBox = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this._canvasControl = new GRPO.CanvasControl();
            this.groupBoxSelect.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 386);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(109, 88);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(882, 65);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(109, 90);
            this.button2.TabIndex = 2;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(882, 161);
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
            this.groupBoxSelect.Controls.Add(this.button1);
            this.groupBoxSelect.Location = new System.Drawing.Point(12, 65);
            this.groupBoxSelect.Name = "groupBoxSelect";
            this.groupBoxSelect.Size = new System.Drawing.Size(218, 480);
            this.groupBoxSelect.TabIndex = 4;
            this.groupBoxSelect.TabStop = false;
            this.groupBoxSelect.Text = "Уструменты";
            // 
            // _toolsWithPropertyControl
            // 
            fillProperty1.FillColor = System.Drawing.Color.White;
            this._toolsWithPropertyControl.FillProperty = fillProperty1;
            lineProperty1.LineColor = System.Drawing.Color.Black;
            lineProperty1.LineThickness = 1F;
            lineProperty1.LineType = System.Drawing.Drawing2D.DashStyle.Solid;
            this._toolsWithPropertyControl.LineProperty = lineProperty1;
            this._toolsWithPropertyControl.Location = new System.Drawing.Point(7, 20);
            this._toolsWithPropertyControl.Name = "_toolsWithPropertyControl";
            this._toolsWithPropertyControl.Size = new System.Drawing.Size(207, 325);
            this._toolsWithPropertyControl.TabIndex = 19;
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
            // _canvasControl
            // 
            this._canvasControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            fillProperty2.FillColor = System.Drawing.Color.White;
            this._canvasControl.FillProperty = fillProperty2;
            lineProperty2.LineColor = System.Drawing.Color.Black;
            lineProperty2.LineThickness = 1F;
            lineProperty2.LineType = System.Drawing.Drawing2D.DashStyle.Solid;
            this._canvasControl.LineProperty = lineProperty2;
            this._canvasControl.Location = new System.Drawing.Point(236, 65);
            this._canvasControl.Name = "_canvasControl";
            this._canvasControl.Size = new System.Drawing.Size(640, 480);
            this._canvasControl.TabIndex = 9;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 605);
            MainForm mainForm = this;
            mainForm.Controls.Add(this._canvasControl);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonAcceptSizePictureBox);
            this.Controls.Add(this.groupBoxSelect);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.Text = "GRPO";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.groupBoxSelect.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox groupBoxSelect;
        private System.Windows.Forms.Button buttonAcceptSizePictureBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private ToolsWithPropertyControl _toolsWithPropertyControl;
        private CanvasControl _canvasControl;
    }
}

