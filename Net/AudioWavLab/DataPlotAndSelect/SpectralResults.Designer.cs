namespace DataAndResultsPlot
{
    partial class SpectralResults
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
            this.plotPictureBox = new System.Windows.Forms.PictureBox();
            this.plotButton = new System.Windows.Forms.Button();
            this.setScaleButton = new System.Windows.Forms.Button();
            this.saveDataButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.plotPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // plotPictureBox
            // 
            this.plotPictureBox.BackColor = System.Drawing.SystemColors.Control;
            this.plotPictureBox.Location = new System.Drawing.Point(239, 12);
            this.plotPictureBox.Name = "plotPictureBox";
            this.plotPictureBox.Size = new System.Drawing.Size(961, 616);
            this.plotPictureBox.TabIndex = 0;
            this.plotPictureBox.TabStop = false;
            this.plotPictureBox.WaitOnLoad = true;
            // 
            // plotButton
            // 
            this.plotButton.Location = new System.Drawing.Point(76, 204);
            this.plotButton.Name = "plotButton";
            this.plotButton.Size = new System.Drawing.Size(75, 23);
            this.plotButton.TabIndex = 1;
            this.plotButton.Text = "Replot";
            this.plotButton.UseVisualStyleBackColor = true;
            this.plotButton.Click += new System.EventHandler(this.plotbutton_Click);
            // 
            // setScaleButton
            // 
            this.setScaleButton.Location = new System.Drawing.Point(76, 248);
            this.setScaleButton.Name = "setScaleButton";
            this.setScaleButton.Size = new System.Drawing.Size(75, 23);
            this.setScaleButton.TabIndex = 2;
            this.setScaleButton.Text = "Set Scale";
            this.setScaleButton.UseVisualStyleBackColor = true;
            this.setScaleButton.Click += new System.EventHandler(this.setScaleButton_Click);
            // 
            // saveDataButton
            // 
            this.saveDataButton.Location = new System.Drawing.Point(76, 296);
            this.saveDataButton.Name = "saveDataButton";
            this.saveDataButton.Size = new System.Drawing.Size(75, 23);
            this.saveDataButton.TabIndex = 3;
            this.saveDataButton.Text = "Save ";
            this.saveDataButton.UseVisualStyleBackColor = true;
            this.saveDataButton.Click += new System.EventHandler(this.saveDataButton_Click);
            // 
            // SpectralResults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1227, 640);
            this.Controls.Add(this.saveDataButton);
            this.Controls.Add(this.setScaleButton);
            this.Controls.Add(this.plotButton);
            this.Controls.Add(this.plotPictureBox);
            this.Name = "SpectralResults";
            this.Text = "Spectra";
            this.Shown += new System.EventHandler(this.SpectralResults_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.plotPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox plotPictureBox;
        private System.Windows.Forms.Button plotButton;
        private System.Windows.Forms.Button setScaleButton;
        private System.Windows.Forms.Button saveDataButton;
    }
}