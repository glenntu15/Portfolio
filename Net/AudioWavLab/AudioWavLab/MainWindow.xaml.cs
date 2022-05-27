using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AudioWavLab.Events;

namespace AudioWavLab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Create this class to hold parms for function generation independant of dialog
        /// </summary>
        FunctParmsData parmsForGenerator;
        WavLabModel businessModel = new WavLabModel();
        /// <summary>
        /// Main Window constructor -- create viewmodel for function parms, Model for app
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            parmsForGenerator = new FunctParmsData();
        }
        
        private void PlotButton_Click(object sender, RoutedEventArgs e)
        {
            businessModel.PlotSignal();
        }

        private void GenerateFunction_Click(object sender, RoutedEventArgs e)
        {
            FunctionParams parmWindow = new FunctionParams(ref parmsForGenerator);
            Nullable<bool> result = parmWindow.ShowDialog();
            if (result == true)
            {
                WaveFormGenerator generator = new WaveFormGenerator();
                generator.Calculated += new FunctionGeneratedEventHandler(OnGenerated);

                businessModel.CreateSignalArrays(parmsForGenerator.npts);
                generator.Generate(parmsForGenerator, ref businessModel);
            }
        }

        private void analyzeButton_Click(object sender, RoutedEventArgs e)
        {
            // windowing type 0 - none, 1 - ramp, 2 - hanning
            // 
            businessModel.Analyze();
        }
        /// <summary>
        /// Handeles event from waveform generator
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnGenerated(object sender, GeneratorStatusEventArgs e)
        {
            string s = string.Format("Generated {0} points", e.npts);
            generatedStatusLabel.Content = s;
            generatedStatusLabel.Foreground = new SolidColorBrush(Colors.Green);
            SetInfoLabels();
        }

        private void ReadFile_Click(object sender, RoutedEventArgs e)
        {
            businessModel.ReadSignal(2000);     //wavlabmodel

            readFileStatusLabel.Content = businessModel.FileName;
            SetInfoLabels();
        }
        private void SetInfoLabels()
        {
            string stext = string.Format("Sample Rate = {0}", businessModel.SampleRate);
            samplesPerSecond.Content = stext;
            stext = string.Format(" Bits per Sample = {0}", businessModel.BitsPerSample);

        }
        private void savetoFileButton_Click(object sender, RoutedEventArgs e)
        {
            businessModel.SaveSignalToFile(0);
        }

    }
}
