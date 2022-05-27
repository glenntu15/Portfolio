using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AudioWavLab;
using DataAndResultsPlot;

namespace AudioWavLab.Analysis
{
    public partial class AnalyzeForm : Form
    {
        WavLabModel _model;
        bool hasSpectrum;
        int functionFlag;
        public AnalyzeForm(WavLabModel model)
        {
            _model = model;
            InitializeComponent();
            rawRadioButton.Checked = true;
            reportButton.Enabled = false;
            noneRadioButton.Checked = true;
            hasSpectrum = false;
            statusLabel.Text = "No Spectrum";
            functionFlag = 0;
        }

        private void AnalyzeForm_Load(object sender, EventArgs e)
        {

        }

        private void normalizeButton_Click(object sender, EventArgs e)
        {
            _model.CreateNormalizedandShifted(functionFlag);
            reportButton.Enabled = true;
        }

        private void fullFFTButton_Click(object sender, EventArgs e)
        {
            _model.Transform(functionFlag);
            hasSpectrum = true;
            statusLabel.Text = "Raw Spectrum";
        }

        private void Plot_Click(object sender, EventArgs e)
        {
            int which = SetWhitch();

            //if (reportButton.Enabled != true)
            //{
            //    _model.CreateNormalizedandShifted(functionFlag);
            //    reportButton.Enabled = true;
            //}
                
            
            Console.WriteLine(" selected {0}", which);
            _model.PlotSpectra(which);
        }
        private int SetWhitch()
        {
            int which = 0;
            foreach (var control in whichGroupBox.Controls)
            {
                RadioButton radio = control as RadioButton;

                if (radio != null && radio.Checked)
                {
                    if (radio.Text == "Raw")
                        break;
                    if (radio.Text == "Shifted")
                    {
                        which = 1;
                        break;
                    }
                    if (radio.Text == "Normalized")
                    {
                        which = 2;
                        break;
                    }
                    if (radio.Text == "Shifted+Normalized")
                    {
                        which = 3;
                        break;
                    }
                }
            }
            return which;
    }

        private void reportButton_Click(object sender, EventArgs e)
        {
            int which = SetWhitch();

            _model.ShowReport(which);
            
        }

        private void saveSpectraButton_Click(object sender, EventArgs e)
        {

        }

        private void shiftNormRadioButton_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void endRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            functionFlag = 1;
            hasSpectrum = false;
        }

        private void PlanckTukeyRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            functionFlag = 2;
            hasSpectrum = false;
        }

        private void flattopRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            functionFlag = 3;
            hasSpectrum = false;
        }

        private void noneRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            functionFlag = 0;
            hasSpectrum = false;
        }
    }
    
}
