namespace DataAndResultsPlot
{
    partial class Selection
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.startTimeTextBox = new System.Windows.Forms.TextBox();
            this.segmentLengthTextBox = new System.Windows.Forms.TextBox();
            this.numberSegmentsTextBox = new System.Windows.Forms.TextBox();
            this.oKButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Start Time (x-axis)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Segment Length";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Number of Segments";
            // 
            // startTimeTextBox
            // 
            this.startTimeTextBox.Location = new System.Drawing.Point(145, 44);
            this.startTimeTextBox.Name = "startTimeTextBox";
            this.startTimeTextBox.Size = new System.Drawing.Size(81, 20);
            this.startTimeTextBox.TabIndex = 3;
            // 
            // segmentLengthTextBox
            // 
            this.segmentLengthTextBox.Location = new System.Drawing.Point(145, 78);
            this.segmentLengthTextBox.Name = "segmentLengthTextBox";
            this.segmentLengthTextBox.Size = new System.Drawing.Size(81, 20);
            this.segmentLengthTextBox.TabIndex = 4;
            // 
            // numberSegmentsTextBox
            // 
            this.numberSegmentsTextBox.Location = new System.Drawing.Point(145, 116);
            this.numberSegmentsTextBox.Name = "numberSegmentsTextBox";
            this.numberSegmentsTextBox.Size = new System.Drawing.Size(81, 20);
            this.numberSegmentsTextBox.TabIndex = 5;
            // 
            // oKButton
            // 
            this.oKButton.Location = new System.Drawing.Point(43, 200);
            this.oKButton.Name = "oKButton";
            this.oKButton.Size = new System.Drawing.Size(75, 23);
            this.oKButton.TabIndex = 6;
            this.oKButton.Text = "OK";
            this.oKButton.UseVisualStyleBackColor = true;
            this.oKButton.Click += new System.EventHandler(this.oKButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(151, 200);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // Selection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 266);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.oKButton);
            this.Controls.Add(this.numberSegmentsTextBox);
            this.Controls.Add(this.segmentLengthTextBox);
            this.Controls.Add(this.startTimeTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Selection";
            this.Text = "Selection Parameters";
            this.Load += new System.EventHandler(this.Selection_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox startTimeTextBox;
        private System.Windows.Forms.TextBox segmentLengthTextBox;
        private System.Windows.Forms.TextBox numberSegmentsTextBox;
        private System.Windows.Forms.Button oKButton;
        private System.Windows.Forms.Button cancelButton;
    }
}