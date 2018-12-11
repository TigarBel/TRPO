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
            this.mainPictureBox = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBoxSelect = new System.Windows.Forms.GroupBox();
            this.radioButtonEllipse = new System.Windows.Forms.RadioButton();
            this.radioButtonCircle = new System.Windows.Forms.RadioButton();
            this.radioButtonPolygon = new System.Windows.Forms.RadioButton();
            this.radioButtonPolyline = new System.Windows.Forms.RadioButton();
            this.radioButtonLine = new System.Windows.Forms.RadioButton();
            this.buttonBlackColor = new System.Windows.Forms.Button();
            this.buttonRedColor = new System.Windows.Forms.Button();
            this.buttonColor = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.mainPictureBox)).BeginInit();
            this.groupBoxSelect.SuspendLayout();
            this.SuspendLayout();
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
            this.groupBoxSelect.Controls.Add(this.buttonColor);
            this.groupBoxSelect.Controls.Add(this.buttonRedColor);
            this.groupBoxSelect.Controls.Add(this.buttonBlackColor);
            this.groupBoxSelect.Controls.Add(this.radioButtonEllipse);
            this.groupBoxSelect.Controls.Add(this.radioButtonCircle);
            this.groupBoxSelect.Controls.Add(this.radioButtonPolygon);
            this.groupBoxSelect.Controls.Add(this.radioButtonPolyline);
            this.groupBoxSelect.Controls.Add(this.radioButtonLine);
            this.groupBoxSelect.Location = new System.Drawing.Point(12, 65);
            this.groupBoxSelect.Name = "groupBoxSelect";
            this.groupBoxSelect.Size = new System.Drawing.Size(299, 480);
            this.groupBoxSelect.TabIndex = 4;
            this.groupBoxSelect.TabStop = false;
            this.groupBoxSelect.Text = "Уструменты";
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
            // buttonBlackColor
            // 
            this.buttonBlackColor.BackColor = System.Drawing.Color.Black;
            this.buttonBlackColor.Location = new System.Drawing.Point(6, 134);
            this.buttonBlackColor.Name = "buttonBlackColor";
            this.buttonBlackColor.Size = new System.Drawing.Size(25, 25);
            this.buttonBlackColor.TabIndex = 5;
            this.buttonBlackColor.UseVisualStyleBackColor = false;
            this.buttonBlackColor.Click += new System.EventHandler(this.buttonBlackColor_Click);
            // 
            // buttonRedColor
            // 
            this.buttonRedColor.BackColor = System.Drawing.Color.Red;
            this.buttonRedColor.Location = new System.Drawing.Point(37, 134);
            this.buttonRedColor.Name = "buttonRedColor";
            this.buttonRedColor.Size = new System.Drawing.Size(25, 25);
            this.buttonRedColor.TabIndex = 6;
            this.buttonRedColor.UseVisualStyleBackColor = false;
            this.buttonRedColor.Click += new System.EventHandler(this.buttonRedColor_Click);
            // 
            // buttonColor
            // 
            this.buttonColor.Location = new System.Drawing.Point(6, 165);
            this.buttonColor.Name = "buttonColor";
            this.buttonColor.Size = new System.Drawing.Size(56, 25);
            this.buttonColor.TabIndex = 7;
            this.buttonColor.Text = "Color";
            this.buttonColor.UseVisualStyleBackColor = true;
            this.buttonColor.Click += new System.EventHandler(this.buttonColor_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1193, 557);
            this.Controls.Add(this.groupBoxSelect);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.mainPictureBox);
            this.Name = "MainForm";
            this.Text = "GRPO";
            ((System.ComponentModel.ISupportInitialize)(this.mainPictureBox)).EndInit();
            this.groupBoxSelect.ResumeLayout(false);
            this.groupBoxSelect.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.Button buttonColor;
        private System.Windows.Forms.Button buttonRedColor;
        private System.Windows.Forms.Button buttonBlackColor;
    }
}

