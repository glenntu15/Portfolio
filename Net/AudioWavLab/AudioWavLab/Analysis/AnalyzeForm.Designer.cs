namespace AudioWavLab.Analysis
{
    partial class AnalyzeForm
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
            this.windowingGroupBox = new System.Windows.Forms.GroupBox();
            this.flattopRadioButton = new System.Windows.Forms.RadioButton();
            this.RampRadioButton = new System.Windows.Forms.RadioButton();
            this.PlanckTukeyRadioButton = new System.Windows.Forms.RadioButton();
            this.endRadioButton = new System.Windows.Forms.RadioButton();
            this.noneRadioButton = new System.Windows.Forms.RadioButton();
            this.NumberofSegments = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.timeSegmentGroupBox = new System.Windows.Forms.GroupBox();
            this.saveSFFTButton = new System.Windows.Forms.Button();
            this.calcShortFFTButton = new System.Windows.Forms.Button();
            this.fullFFTButton = new System.Windows.Forms.Button();
            this.spectraProcessGroupBox = new System.Windows.Forms.GroupBox();
            this.reportButton = new System.Windows.Forms.Button();
            this.whichGroupBox = new System.Windows.Forms.GroupBox();
            this.shiftNormRadioButton = new System.Windows.Forms.RadioButton();
            this.normalRadioButton = new System.Windows.Forms.RadioButton();
            this.shiftedRadioButton = new System.Windows.Forms.RadioButton();
            this.rawRadioButton = new System.Windows.Forms.RadioButton();
            this.rawOrShiftedLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.binUpButton = new System.Windows.Forms.Button();
            this.saveSpectraButton = new System.Windows.Forms.Button();
            this.Plot = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.freqShiftButton = new System.Windows.Forms.Button();
            this.normalizeButton = new System.Windows.Forms.Button();
            this.statusLabel = new System.Windows.Forms.Label();
            this.dFTGroupbox = new System.Windows.Forms.GroupBox();
            this.windowingGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumberofSegments)).BeginInit();
            this.timeSegmentGroupBox.SuspendLayout();
            this.spectraProcessGroupBox.SuspendLayout();
            this.whichGroupBox.SuspendLayout();
            this.dFTGroupbox.SuspendLayout();
            this.SuspendLayout();
            // 
            // windowingGroupBox
            // 
            this.windowingGroupBox.Controls.Add(this.flattopRadioButton);
            this.windowingGroupBox.Controls.Add(this.RampRadioButton);
            this.windowingGroupBox.Controls.Add(this.PlanckTukeyRadioButton);
            this.windowingGroupBox.Controls.Add(this.endRadioButton);
            this.windowingGroupBox.Controls.Add(this.noneRadioButton);
            this.windowingGroupBox.Location = new System.Drawing.Point(517, 19);
            this.windowingGroupBox.Name = "windowingGroupBox";
            this.windowingGroupBox.Size = new System.Drawing.Size(143, 134);
            this.windowingGroupBox.TabIndex = 0;
            this.windowingGroupBox.TabStop = false;
            this.windowingGroupBox.Text = "Windowing";
            // 
            // flattopRadioButton
            // 
            this.flattopRadioButton.AutoSize = true;
            this.flattopRadioButton.Location = new System.Drawing.Point(26, 88);
            this.flattopRadioButton.Name = "flattopRadioButton";
            this.flattopRadioButton.Size = new System.Drawing.Size(57, 17);
            this.flattopRadioButton.TabIndex = 4;
            this.flattopRadioButton.TabStop = true;
            this.flattopRadioButton.Text = "Flattop";
            this.flattopRadioButton.UseVisualStyleBackColor = true;
            this.flattopRadioButton.CheckedChanged += new System.EventHandler(this.flattopRadioButton_CheckedChanged);
            // 
            // RampRadioButton
            // 
            this.RampRadioButton.AutoSize = true;
            this.RampRadioButton.Enabled = false;
            this.RampRadioButton.Location = new System.Drawing.Point(26, 111);
            this.RampRadioButton.Name = "RampRadioButton";
            this.RampRadioButton.Size = new System.Drawing.Size(65, 17);
            this.RampRadioButton.TabIndex = 3;
            this.RampRadioButton.TabStop = true;
            this.RampRadioButton.Text = "Hanning";
            this.RampRadioButton.UseVisualStyleBackColor = true;
            // 
            // PlanckTukeyRadioButton
            // 
            this.PlanckTukeyRadioButton.AutoSize = true;
            this.PlanckTukeyRadioButton.Location = new System.Drawing.Point(26, 65);
            this.PlanckTukeyRadioButton.Name = "PlanckTukeyRadioButton";
            this.PlanckTukeyRadioButton.Size = new System.Drawing.Size(91, 17);
            this.PlanckTukeyRadioButton.TabIndex = 2;
            this.PlanckTukeyRadioButton.TabStop = true;
            this.PlanckTukeyRadioButton.Text = "Planck-Tukey";
            this.PlanckTukeyRadioButton.UseVisualStyleBackColor = true;
            this.PlanckTukeyRadioButton.CheckedChanged += new System.EventHandler(this.PlanckTukeyRadioButton_CheckedChanged);
            // 
            // endRadioButton
            // 
            this.endRadioButton.AutoSize = true;
            this.endRadioButton.Location = new System.Drawing.Point(26, 42);
            this.endRadioButton.Name = "endRadioButton";
            this.endRadioButton.Size = new System.Drawing.Size(75, 17);
            this.endRadioButton.TabIndex = 1;
            this.endRadioButton.TabStop = true;
            this.endRadioButton.Text = "End Ramp";
            this.endRadioButton.UseVisualStyleBackColor = true;
            this.endRadioButton.CheckedChanged += new System.EventHandler(this.endRadioButton_CheckedChanged);
            // 
            // noneRadioButton
            // 
            this.noneRadioButton.AutoSize = true;
            this.noneRadioButton.Location = new System.Drawing.Point(26, 19);
            this.noneRadioButton.Name = "noneRadioButton";
            this.noneRadioButton.Size = new System.Drawing.Size(51, 17);
            this.noneRadioButton.TabIndex = 0;
            this.noneRadioButton.TabStop = true;
            this.noneRadioButton.Text = "None";
            this.noneRadioButton.UseVisualStyleBackColor = true;
            this.noneRadioButton.CheckedChanged += new System.EventHandler(this.noneRadioButton_CheckedChanged);
            // 
            // NumberofSegments
            // 
            this.NumberofSegments.Location = new System.Drawing.Point(125, 74);
            this.NumberofSegments.Name = "NumberofSegments";
            this.NumberofSegments.Size = new System.Drawing.Size(120, 20);
            this.NumberofSegments.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(125, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Number of segments";
            // 
            // timeSegmentGroupBox
            // 
            this.timeSegmentGroupBox.Controls.Add(this.saveSFFTButton);
            this.timeSegmentGroupBox.Controls.Add(this.calcShortFFTButton);
            this.timeSegmentGroupBox.Controls.Add(this.label1);
            this.timeSegmentGroupBox.Controls.Add(this.NumberofSegments);
            this.timeSegmentGroupBox.Location = new System.Drawing.Point(32, 473);
            this.timeSegmentGroupBox.Name = "timeSegmentGroupBox";
            this.timeSegmentGroupBox.Size = new System.Drawing.Size(278, 106);
            this.timeSegmentGroupBox.TabIndex = 3;
            this.timeSegmentGroupBox.TabStop = false;
            this.timeSegmentGroupBox.Text = "Short FFT";
            // 
            // saveSFFTButton
            // 
            this.saveSFFTButton.Location = new System.Drawing.Point(17, 75);
            this.saveSFFTButton.Name = "saveSFFTButton";
            this.saveSFFTButton.Size = new System.Drawing.Size(75, 23);
            this.saveSFFTButton.TabIndex = 4;
            this.saveSFFTButton.Text = "Save To File";
            this.saveSFFTButton.UseVisualStyleBackColor = true;
            // 
            // calcShortFFTButton
            // 
            this.calcShortFFTButton.Location = new System.Drawing.Point(17, 35);
            this.calcShortFFTButton.Name = "calcShortFFTButton";
            this.calcShortFFTButton.Size = new System.Drawing.Size(75, 23);
            this.calcShortFFTButton.TabIndex = 3;
            this.calcShortFFTButton.Text = "Calculate";
            this.calcShortFFTButton.UseVisualStyleBackColor = true;
            // 
            // fullFFTButton
            // 
            this.fullFFTButton.Location = new System.Drawing.Point(28, 66);
            this.fullFFTButton.Name = "fullFFTButton";
            this.fullFFTButton.Size = new System.Drawing.Size(75, 23);
            this.fullFFTButton.TabIndex = 4;
            this.fullFFTButton.Text = "Full DFT";
            this.fullFFTButton.UseVisualStyleBackColor = true;
            this.fullFFTButton.Click += new System.EventHandler(this.fullFFTButton_Click);
            // 
            // spectraProcessGroupBox
            // 
            this.spectraProcessGroupBox.Controls.Add(this.reportButton);
            this.spectraProcessGroupBox.Controls.Add(this.whichGroupBox);
            this.spectraProcessGroupBox.Controls.Add(this.rawOrShiftedLabel);
            this.spectraProcessGroupBox.Controls.Add(this.label4);
            this.spectraProcessGroupBox.Controls.Add(this.binUpButton);
            this.spectraProcessGroupBox.Controls.Add(this.saveSpectraButton);
            this.spectraProcessGroupBox.Controls.Add(this.Plot);
            this.spectraProcessGroupBox.Controls.Add(this.label3);
            this.spectraProcessGroupBox.Controls.Add(this.label2);
            this.spectraProcessGroupBox.Controls.Add(this.freqShiftButton);
            this.spectraProcessGroupBox.Controls.Add(this.normalizeButton);
            this.spectraProcessGroupBox.Location = new System.Drawing.Point(26, 196);
            this.spectraProcessGroupBox.Name = "spectraProcessGroupBox";
            this.spectraProcessGroupBox.Size = new System.Drawing.Size(759, 271);
            this.spectraProcessGroupBox.TabIndex = 5;
            this.spectraProcessGroupBox.TabStop = false;
            this.spectraProcessGroupBox.Text = "Process Spectra";
            // 
            // reportButton
            // 
            this.reportButton.Location = new System.Drawing.Point(17, 158);
            this.reportButton.Name = "reportButton";
            this.reportButton.Size = new System.Drawing.Size(75, 23);
            this.reportButton.TabIndex = 10;
            this.reportButton.Text = "Report";
            this.reportButton.UseVisualStyleBackColor = true;
            this.reportButton.Click += new System.EventHandler(this.reportButton_Click);
            // 
            // whichGroupBox
            // 
            this.whichGroupBox.Controls.Add(this.shiftNormRadioButton);
            this.whichGroupBox.Controls.Add(this.normalRadioButton);
            this.whichGroupBox.Controls.Add(this.shiftedRadioButton);
            this.whichGroupBox.Controls.Add(this.rawRadioButton);
            this.whichGroupBox.Location = new System.Drawing.Point(134, 158);
            this.whichGroupBox.Name = "whichGroupBox";
            this.whichGroupBox.Size = new System.Drawing.Size(477, 53);
            this.whichGroupBox.TabIndex = 9;
            this.whichGroupBox.TabStop = false;
            this.whichGroupBox.Text = "Which (calculates if needed)";
            // 
            // shiftNormRadioButton
            // 
            this.shiftNormRadioButton.AutoSize = true;
            this.shiftNormRadioButton.Location = new System.Drawing.Point(237, 20);
            this.shiftNormRadioButton.Name = "shiftNormRadioButton";
            this.shiftNormRadioButton.Size = new System.Drawing.Size(116, 17);
            this.shiftNormRadioButton.TabIndex = 13;
            this.shiftNormRadioButton.TabStop = true;
            this.shiftNormRadioButton.Text = "Shifted+Normalized";
            this.shiftNormRadioButton.UseVisualStyleBackColor = true;
            this.shiftNormRadioButton.CheckedChanged += new System.EventHandler(this.shiftNormRadioButton_CheckedChanged);
            // 
            // normalRadioButton
            // 
            this.normalRadioButton.AutoSize = true;
            this.normalRadioButton.Location = new System.Drawing.Point(154, 20);
            this.normalRadioButton.Name = "normalRadioButton";
            this.normalRadioButton.Size = new System.Drawing.Size(77, 17);
            this.normalRadioButton.TabIndex = 12;
            this.normalRadioButton.TabStop = true;
            this.normalRadioButton.Text = "Normalized";
            this.normalRadioButton.UseVisualStyleBackColor = true;
            // 
            // shiftedRadioButton
            // 
            this.shiftedRadioButton.AutoSize = true;
            this.shiftedRadioButton.Location = new System.Drawing.Point(90, 20);
            this.shiftedRadioButton.Name = "shiftedRadioButton";
            this.shiftedRadioButton.Size = new System.Drawing.Size(58, 17);
            this.shiftedRadioButton.TabIndex = 11;
            this.shiftedRadioButton.TabStop = true;
            this.shiftedRadioButton.Text = "Shifted";
            this.shiftedRadioButton.UseVisualStyleBackColor = true;
            // 
            // rawRadioButton
            // 
            this.rawRadioButton.AutoSize = true;
            this.rawRadioButton.Location = new System.Drawing.Point(37, 20);
            this.rawRadioButton.Name = "rawRadioButton";
            this.rawRadioButton.Size = new System.Drawing.Size(47, 17);
            this.rawRadioButton.TabIndex = 10;
            this.rawRadioButton.TabStop = true;
            this.rawRadioButton.Text = "Raw";
            this.rawRadioButton.UseVisualStyleBackColor = true;
            // 
            // rawOrShiftedLabel
            // 
            this.rawOrShiftedLabel.AutoSize = true;
            this.rawOrShiftedLabel.Location = new System.Drawing.Point(257, 118);
            this.rawOrShiftedLabel.Name = "rawOrShiftedLabel";
            this.rawOrShiftedLabel.Size = new System.Drawing.Size(29, 13);
            this.rawOrShiftedLabel.TabIndex = 7;
            this.rawOrShiftedLabel.Text = "Raw";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(125, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Put Frequencies into bins";
            // 
            // binUpButton
            // 
            this.binUpButton.Location = new System.Drawing.Point(17, 113);
            this.binUpButton.Name = "binUpButton";
            this.binUpButton.Size = new System.Drawing.Size(75, 23);
            this.binUpButton.TabIndex = 4;
            this.binUpButton.Text = "Bin Up";
            this.binUpButton.UseVisualStyleBackColor = true;
            // 
            // saveSpectraButton
            // 
            this.saveSpectraButton.Location = new System.Drawing.Point(290, 217);
            this.saveSpectraButton.Name = "saveSpectraButton";
            this.saveSpectraButton.Size = new System.Drawing.Size(75, 23);
            this.saveSpectraButton.TabIndex = 9;
            this.saveSpectraButton.Text = "Save to File";
            this.saveSpectraButton.UseVisualStyleBackColor = true;
            this.saveSpectraButton.Click += new System.EventHandler(this.saveSpectraButton_Click);
            // 
            // Plot
            // 
            this.Plot.Location = new System.Drawing.Point(176, 217);
            this.Plot.Name = "Plot";
            this.Plot.Size = new System.Drawing.Size(75, 23);
            this.Plot.TabIndex = 8;
            this.Plot.Text = "Plot Spectra";
            this.Plot.UseVisualStyleBackColor = true;
            this.Plot.Click += new System.EventHandler(this.Plot_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(125, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Shift Max to 220 Hz";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(125, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Normalize Amplitude";
            // 
            // freqShiftButton
            // 
            this.freqShiftButton.Location = new System.Drawing.Point(17, 75);
            this.freqShiftButton.Name = "freqShiftButton";
            this.freqShiftButton.Size = new System.Drawing.Size(75, 23);
            this.freqShiftButton.TabIndex = 1;
            this.freqShiftButton.Text = "Freq Shift";
            this.freqShiftButton.UseVisualStyleBackColor = true;
            // 
            // normalizeButton
            // 
            this.normalizeButton.Location = new System.Drawing.Point(17, 37);
            this.normalizeButton.Name = "normalizeButton";
            this.normalizeButton.Size = new System.Drawing.Size(75, 23);
            this.normalizeButton.TabIndex = 0;
            this.normalizeButton.Text = "Normalize";
            this.normalizeButton.UseVisualStyleBackColor = true;
            this.normalizeButton.Click += new System.EventHandler(this.normalizeButton_Click);
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusLabel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.statusLabel.Location = new System.Drawing.Point(29, 12);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(84, 16);
            this.statusLabel.TabIndex = 6;
            this.statusLabel.Text = "statusLabel";
            // 
            // dFTGroupbox
            // 
            this.dFTGroupbox.Controls.Add(this.fullFFTButton);
            this.dFTGroupbox.Controls.Add(this.windowingGroupBox);
            this.dFTGroupbox.Location = new System.Drawing.Point(49, 31);
            this.dFTGroupbox.Name = "dFTGroupbox";
            this.dFTGroupbox.Size = new System.Drawing.Size(736, 159);
            this.dFTGroupbox.TabIndex = 7;
            this.dFTGroupbox.TabStop = false;
            this.dFTGroupbox.Text = "DFT";
            // 
            // AnalyzeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 591);
            this.Controls.Add(this.dFTGroupbox);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.spectraProcessGroupBox);
            this.Controls.Add(this.timeSegmentGroupBox);
            this.Name = "AnalyzeForm";
            this.Text = "Analyze";
            this.Load += new System.EventHandler(this.AnalyzeForm_Load);
            this.windowingGroupBox.ResumeLayout(false);
            this.windowingGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumberofSegments)).EndInit();
            this.timeSegmentGroupBox.ResumeLayout(false);
            this.timeSegmentGroupBox.PerformLayout();
            this.spectraProcessGroupBox.ResumeLayout(false);
            this.spectraProcessGroupBox.PerformLayout();
            this.whichGroupBox.ResumeLayout(false);
            this.whichGroupBox.PerformLayout();
            this.dFTGroupbox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox windowingGroupBox;
        private System.Windows.Forms.RadioButton PlanckTukeyRadioButton;
        private System.Windows.Forms.RadioButton RampRadioButton;
        private System.Windows.Forms.RadioButton endRadioButton;
        private System.Windows.Forms.RadioButton noneRadioButton;
        private System.Windows.Forms.NumericUpDown NumberofSegments;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox timeSegmentGroupBox;
        private System.Windows.Forms.Button saveSFFTButton;
        private System.Windows.Forms.Button calcShortFFTButton;
        private System.Windows.Forms.Button fullFFTButton;
        private System.Windows.Forms.GroupBox spectraProcessGroupBox;
        private System.Windows.Forms.Button binUpButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button freqShiftButton;
        private System.Windows.Forms.Button normalizeButton;
        private System.Windows.Forms.Label rawOrShiftedLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox whichGroupBox;
        private System.Windows.Forms.Button saveSpectraButton;
        private System.Windows.Forms.Button Plot;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.RadioButton shiftNormRadioButton;
        private System.Windows.Forms.RadioButton normalRadioButton;
        private System.Windows.Forms.RadioButton shiftedRadioButton;
        private System.Windows.Forms.RadioButton rawRadioButton;
        private System.Windows.Forms.Button reportButton;
        private System.Windows.Forms.GroupBox dFTGroupbox;
        private System.Windows.Forms.RadioButton flattopRadioButton;
    }
}