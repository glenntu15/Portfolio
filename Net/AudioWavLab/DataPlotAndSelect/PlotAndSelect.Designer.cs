namespace DataAndResultsPlot
{
    partial class PlotAndSelect
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
            this.selectButton = new System.Windows.Forms.Button();
            this.replotSelectedButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.sePlotScaleButton = new System.Windows.Forms.Button();
            this.playButton = new System.Windows.Forms.Button();
            this.fileWriteButton = new System.Windows.Forms.Button();
            this.segmentSelectComboBox = new System.Windows.Forms.ComboBox();
            this.numSegmentsLabel = new System.Windows.Forms.Label();
            this.displayIntegerLlabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.plotPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // plotPictureBox
            // 
            this.plotPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plotPictureBox.Location = new System.Drawing.Point(221, 12);
            this.plotPictureBox.Name = "plotPictureBox";
            this.plotPictureBox.Size = new System.Drawing.Size(1094, 532);
            this.plotPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.plotPictureBox.TabIndex = 0;
            this.plotPictureBox.TabStop = false;
            this.plotPictureBox.LoadCompleted += new System.ComponentModel.AsyncCompletedEventHandler(this.plotPictureBox_LoadCompleted);
            this.plotPictureBox.SizeChanged += new System.EventHandler(this.plotPictureBox_SizeChanged);
            this.plotPictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.plotPictureBox_MouseClick);
            // 
            // selectButton
            // 
            this.selectButton.Location = new System.Drawing.Point(76, 131);
            this.selectButton.Name = "selectButton";
            this.selectButton.Size = new System.Drawing.Size(96, 23);
            this.selectButton.TabIndex = 1;
            this.selectButton.Text = "Select";
            this.selectButton.UseVisualStyleBackColor = true;
            this.selectButton.Click += new System.EventHandler(this.selectButton_Click);
            // 
            // replotSelectedButton
            // 
            this.replotSelectedButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.replotSelectedButton.Location = new System.Drawing.Point(76, 218);
            this.replotSelectedButton.Name = "replotSelectedButton";
            this.replotSelectedButton.Size = new System.Drawing.Size(96, 23);
            this.replotSelectedButton.TabIndex = 2;
            this.replotSelectedButton.Text = "Replot Selection";
            this.replotSelectedButton.UseVisualStyleBackColor = false;
            this.replotSelectedButton.Click += new System.EventHandler(this.replotSelectedButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.closeButton.Location = new System.Drawing.Point(12, 466);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(85, 23);
            this.closeButton.TabIndex = 3;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cancelButton.Location = new System.Drawing.Point(103, 466);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(85, 23);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // sePlotScaleButton
            // 
            this.sePlotScaleButton.Location = new System.Drawing.Point(76, 178);
            this.sePlotScaleButton.Name = "sePlotScaleButton";
            this.sePlotScaleButton.Size = new System.Drawing.Size(96, 23);
            this.sePlotScaleButton.TabIndex = 5;
            this.sePlotScaleButton.Text = "Set Plot Scale";
            this.sePlotScaleButton.UseVisualStyleBackColor = true;
            this.sePlotScaleButton.Click += new System.EventHandler(this.sePlotScaleButton_Click);
            // 
            // playButton
            // 
            this.playButton.Enabled = false;
            this.playButton.Location = new System.Drawing.Point(76, 495);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(96, 23);
            this.playButton.TabIndex = 6;
            this.playButton.Text = "Play / Stop";
            this.playButton.UseVisualStyleBackColor = true;
            this.playButton.Visible = false;
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            // 
            // fileWriteButton
            // 
            this.fileWriteButton.Location = new System.Drawing.Point(76, 386);
            this.fileWriteButton.Name = "fileWriteButton";
            this.fileWriteButton.Size = new System.Drawing.Size(96, 23);
            this.fileWriteButton.TabIndex = 7;
            this.fileWriteButton.Text = "Write to File";
            this.fileWriteButton.UseVisualStyleBackColor = true;
            this.fileWriteButton.Click += new System.EventHandler(this.fileWriteButton_Click);
            // 
            // segmentSelectComboBox
            // 
            this.segmentSelectComboBox.FormattingEnabled = true;
            this.segmentSelectComboBox.Location = new System.Drawing.Point(59, 66);
            this.segmentSelectComboBox.Name = "segmentSelectComboBox";
            this.segmentSelectComboBox.Size = new System.Drawing.Size(112, 21);
            this.segmentSelectComboBox.TabIndex = 8;
            // 
            // numSegmentsLabel
            // 
            this.numSegmentsLabel.AutoSize = true;
            this.numSegmentsLabel.Location = new System.Drawing.Point(32, 36);
            this.numSegmentsLabel.Name = "numSegmentsLabel";
            this.numSegmentsLabel.Size = new System.Drawing.Size(79, 13);
            this.numSegmentsLabel.TabIndex = 9;
            this.numSegmentsLabel.Text = "Num Segments";
            // 
            // displayIntegerLlabel
            // 
            this.displayIntegerLlabel.AutoSize = true;
            this.displayIntegerLlabel.ForeColor = System.Drawing.Color.Red;
            this.displayIntegerLlabel.Location = new System.Drawing.Point(118, 36);
            this.displayIntegerLlabel.Name = "displayIntegerLlabel";
            this.displayIntegerLlabel.Size = new System.Drawing.Size(13, 13);
            this.displayIntegerLlabel.TabIndex = 10;
            this.displayIntegerLlabel.Text = "1";
            // 
            // PlotAndSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1356, 556);
            this.Controls.Add(this.displayIntegerLlabel);
            this.Controls.Add(this.numSegmentsLabel);
            this.Controls.Add(this.segmentSelectComboBox);
            this.Controls.Add(this.fileWriteButton);
            this.Controls.Add(this.playButton);
            this.Controls.Add(this.sePlotScaleButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.replotSelectedButton);
            this.Controls.Add(this.selectButton);
            this.Controls.Add(this.plotPictureBox);
            this.Name = "PlotAndSelect";
            this.Text = "Plot and Select";
            this.Load += new System.EventHandler(this.PlotAndSelect_Load);
            ((System.ComponentModel.ISupportInitialize)(this.plotPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox plotPictureBox;
        private System.Windows.Forms.Button selectButton;
        private System.Windows.Forms.Button replotSelectedButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button sePlotScaleButton;
        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.Button fileWriteButton;
        private System.Windows.Forms.ComboBox segmentSelectComboBox;
        private System.Windows.Forms.Label numSegmentsLabel;
        private System.Windows.Forms.Label displayIntegerLlabel;
    }
}