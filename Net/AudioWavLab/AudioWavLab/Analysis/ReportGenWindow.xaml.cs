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
using System.Windows.Shapes;

namespace AudioWavLab.Analysis
{
    /// <summary>
    /// Interaction logic for ReportGenWindow.xaml
    /// </summary>
    public partial class ReportGenWindow : Window
    {
        public List<Tuple<float, float>> sortedspectra;
        float rf01;
        float rf02;
        //float rf12;
        float af01;
        float af02;
        //float af12;

        public ReportGenWindow()
        {
            InitializeComponent();
        }
        public void Set(List<Tuple<float, float>> sp, float [] resultvector)
        {
            sortedspectra = sp;
            float rf01 = resultvector[0];
            float rf02 = resultvector[1];
            float rf12 = resultvector[2];
            float af01 = resultvector[3];
            float af02 = resultvector[4];
            float af12 = resultvector[5];
        }

        private void GenTextAfterLoad(object sender, RoutedEventArgs e)
        {
            reportTextBox.Text = "\n\n\t\t Ratios of frequencies and amplitudes of harmonics to fundamental";
            reportTextBox.Text += "\n\t\t\t\t f0/f1\t f0/f2\t f1/f2\t A0/A1\t A0/A2\t A1/A2";
            reportTextBox.Text += "\n\t\t\t\t -----\t -----\t -----\t -----\t -----\t -----";

            float freqofMaxValue = sortedspectra[0].Item1;
            float maxamplitude = sortedspectra[0].Item2;

            float rf01 = freqofMaxValue / sortedspectra[1].Item1;
            float rf02 = freqofMaxValue / sortedspectra[2].Item1;
            float rf12 = sortedspectra[1].Item1 / sortedspectra[2].Item1;
            float af01 = maxamplitude / sortedspectra[1].Item2;
            float af02 = maxamplitude / sortedspectra[2].Item2;
            float af12 = sortedspectra[1].Item2 / sortedspectra[2].Item2;
            string freqratios = String.Format("\n\t\t\t\t{0:n3}\t{1:n3}\t{2:n3}\t{3:n3}\t{4:n3}\t{5:n3}", 
                      rf01, rf02, rf12, af01, af02, af12);
            
            reportTextBox.Text += freqratios;
            
        }
    }
}
