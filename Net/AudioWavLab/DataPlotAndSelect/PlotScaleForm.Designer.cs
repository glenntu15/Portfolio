namespace DataAndResultsPlot
{
    partial class PlotScaleForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.firstXTextBox = new System.Windows.Forms.TextBox();
            this.deltaXTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.deltaYTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.xAxisLenTextBox = new System.Windows.Forms.TextBox();
            this.yAxisLenTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.firstYTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.oKButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.pLotTitleTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(75, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "First X";
            // 
            // firstXTextBox
            // 
            this.firstXTextBox.Location = new System.Drawing.Point(119, 23);
            this.firstXTextBox.Name = "firstXTextBox";
            this.firstXTextBox.Size = new System.Drawing.Size(53, 20);
            this.firstXTextBox.TabIndex = 1;
            // 
            // deltaXTextBox
            // 
            this.deltaXTextBox.Location = new System.Drawing.Point(119, 60);
            this.deltaXTextBox.Name = "deltaXTextBox";
            this.deltaXTextBox.Size = new System.Drawing.Size(53, 20);
            this.deltaXTextBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(66, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Delta X";
            // 
            // deltaYTextBox
            // 
            this.deltaYTextBox.Location = new System.Drawing.Point(304, 60);
            this.deltaYTextBox.Name = "deltaYTextBox";
            this.deltaYTextBox.Size = new System.Drawing.Size(53, 20);
            this.deltaYTextBox.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(252, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Delta Y";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.Location = new System.Drawing.Point(15, 97);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(98, 15);
            this.Label3.TabIndex = 4;
            this.Label3.Text = "Num Increments";
            // 
            // xAxisLenTextBox
            // 
            this.xAxisLenTextBox.Location = new System.Drawing.Point(119, 92);
            this.xAxisLenTextBox.Name = "xAxisLenTextBox";
            this.xAxisLenTextBox.Size = new System.Drawing.Size(53, 20);
            this.xAxisLenTextBox.TabIndex = 5;
            this.xAxisLenTextBox.TextChanged += new System.EventHandler(this.xAxisLenTextBox_TextChanged);
            // 
            // yAxisLenTextBox
            // 
            this.yAxisLenTextBox.Location = new System.Drawing.Point(304, 92);
            this.yAxisLenTextBox.Name = "yAxisLenTextBox";
            this.yAxisLenTextBox.Size = new System.Drawing.Size(53, 20);
            this.yAxisLenTextBox.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(200, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 15);
            this.label5.TabIndex = 8;
            this.label5.Text = "Num Increments";
            // 
            // firstYTextBox
            // 
            this.firstYTextBox.Location = new System.Drawing.Point(304, 23);
            this.firstYTextBox.Name = "firstYTextBox";
            this.firstYTextBox.Size = new System.Drawing.Size(53, 20);
            this.firstYTextBox.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(260, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 15);
            this.label6.TabIndex = 10;
            this.label6.Text = "First Y";
            // 
            // oKButton
            // 
            this.oKButton.Location = new System.Drawing.Point(119, 186);
            this.oKButton.Name = "oKButton";
            this.oKButton.Size = new System.Drawing.Size(75, 23);
            this.oKButton.TabIndex = 12;
            this.oKButton.Text = "OK";
            this.oKButton.UseVisualStyleBackColor = true;
            this.oKButton.Click += new System.EventHandler(this.oKButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(282, 186);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 13;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // pLotTitleTextBox
            // 
            this.pLotTitleTextBox.Location = new System.Drawing.Point(119, 138);
            this.pLotTitleTextBox.Name = "pLotTitleTextBox";
            this.pLotTitleTextBox.Size = new System.Drawing.Size(238, 20);
            this.pLotTitleTextBox.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(12, 143);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 15);
            this.label7.TabIndex = 15;
            this.label7.Text = "Plot Title";
            // 
            // PlotScaleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 267);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.pLotTitleTextBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.oKButton);
            this.Controls.Add(this.firstYTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.yAxisLenTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.deltaYTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.xAxisLenTextBox);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.deltaXTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.firstXTextBox);
            this.Controls.Add(this.label1);
            this.Name = "PlotScaleForm";
            this.Text = "Plot Axes values";
            this.Load += new System.EventHandler(this.PlotScaleForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox firstXTextBox;
        private System.Windows.Forms.TextBox deltaXTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox deltaYTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label Label3;
        private System.Windows.Forms.TextBox xAxisLenTextBox;
        private System.Windows.Forms.TextBox yAxisLenTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox firstYTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button oKButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TextBox pLotTitleTextBox;
        private System.Windows.Forms.Label label7;
    }
}