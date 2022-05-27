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

namespace AudioWavLab
{
    /// <summary>
    /// Interaction logic for FunctionParams.xaml
    /// </summary>
    public partial class FunctionParams : Window
    {
        FunctParmsData fparms;

        public FunctionParams(ref FunctParmsData fp)
        {
            fparms = fp;

            InitializeComponent();
            List<string> MainFreqLIst = new List<string>();
            MainFreqLIst.Add("110");
            MainFreqLIst.Add("116");
            MainFreqLIst.Add("220");
            MainFreqLIst.Add("440");
            MainFreqLIst.Add("880");
            MainFreqLIst.Add("111.5");

            mainFrequencyListBox.ItemsSource = MainFreqLIst;

            List<string> thirdsFrequencyList = new List<string>();
            thirdsFrequencyList.Add("138.59");
            thirdsFrequencyList.Add("261.63");
            thirdsFrequencyList.Add("554.37");
            thirdsFrequencyList.Add("1108.73");
            musicalThirdFrequencyListBox.ItemsSource = thirdsFrequencyList;

            List<string> AmplitudeList = new List<string>();
            AmplitudeList.Add("1.0");
            AmplitudeList.Add("0.75");
            AmplitudeList.Add("0.5");
            AmplitudeList.Add("0.25");
            AmplitudeList.Add("0.0");
            musicalThirdApmlitudeListBox.ItemsSource = AmplitudeList;

            List<string> fifthsFrequencyList = new List<string>();
            fifthsFrequencyList.Add("164.81");
            fifthsFrequencyList.Add("329.63");
            fifthsFrequencyList.Add("659.26");
            fifthsFrequencyList.Add("1318.51");
            musicalFifthFrequencyListBox.ItemsSource = fifthsFrequencyList;

            musicalFifthApmlitudeListBox.ItemsSource = AmplitudeList;

            octaveApmlitudeListBox.ItemsSource = AmplitudeList;
            /// signal charictaristics
            List<string> SampleFreqList = new List<string>();
            SampleFreqList.Add("512");
            SampleFreqList.Add("1024");
            SampleFreqList.Add("4096");
            SampleFreqList.Add("44100");
            SampleFreqList.Add("48000");
            SampleFreqList.Add("96000");

            SamplesPerSecondListBox.ItemsSource = SampleFreqList;

            List<string> SampleLengthList = new List<string>();
            SampleLengthList.Add(".01");
            SampleLengthList.Add(".1");
            SampleLengthList.Add("1.0");
            SampleLengthList.Add("1.1");
            SampleLengthList.Add("1.25");
            SampleLengthList.Add("2.0");

            SampleLengthListBox.ItemsSource = SampleLengthList;
        }

        #region initialize list boxes
        private void mainFrequencyListBox_Loaded(object sender, RoutedEventArgs e)
        {
            mainFrequencyListBox.SelectedItem = fparms.SelectedFrequency1;
            mainFrequencyListBox.ScrollIntoView(fparms.SelectedFrequency1);
        }

        private void musicalThirdFrequencyListBox_Loaded(object sender, RoutedEventArgs e)
        {
            musicalThirdFrequencyListBox.SelectedItem = fparms.SelectedFrequency2;
            musicalThirdFrequencyListBox.ScrollIntoView(fparms.SelectedFrequency2);
        }
        private void MusicalThirdApmlitudeListBox_Loaded(object sender, RoutedEventArgs e)
        {
            musicalThirdApmlitudeListBox.SelectedItem = fparms.SelectedAmplitude2;
            musicalThirdApmlitudeListBox.ScrollIntoView(fparms.SelectedAmplitude2);

        }

        private void musicalFifthFrequencyListBox_Loaded(object sender, RoutedEventArgs e)
        {
            mainFrequencyListBox.SelectedItem = fparms.SelectedFrequency3;
            mainFrequencyListBox.ScrollIntoView(fparms.SelectedFrequency3);
        }

        private void musicalFifthApmlitudeListBox_Loaded(object sender, RoutedEventArgs e)
        {
            musicalFifthApmlitudeListBox.SelectedItem = fparms.SelectedAmplitude3;
            musicalFifthApmlitudeListBox.ScrollIntoView(fparms.SelectedAmplitude3);
        }

        private void octaveCheckBox_Loaded(object sender, RoutedEventArgs e)
        {
            octaveCheckBox.IsChecked = fparms.SelectedOctave;
        }
        
        private void OctaveApmlitudeListBox_Loaded(object sender, RoutedEventArgs e)
        {
            octaveApmlitudeListBox.SelectedItem = fparms.SelectedAmplitude4;
            octaveApmlitudeListBox.ScrollIntoView(fparms.SelectedAmplitude4);
        }

        private void SamplesPerSecondListBox_Loaded(object sender, RoutedEventArgs e)
        {
            SamplesPerSecondListBox.SelectedItem = fparms.SelectedSamplesPerSecond;
            SamplesPerSecondListBox.ScrollIntoView(fparms.SelectedSamplesPerSecond);
        }
        private void SampleLengthListBox_Loaded(object sender, RoutedEventArgs e)
        {
            SampleLengthListBox.SelectedItem = fparms.SelectedSignalLength;
            SampleLengthListBox.ScrollIntoView(fparms.SelectedSignalLength);
        }
        #endregion

        private void oKButton_Click(object sender, RoutedEventArgs e)
        {
            string txt;
            txt = SamplesPerSecondListBox.SelectedItem.ToString();
            fparms.SamplesPerSecond = Convert.ToDouble(txt);
            fparms.SelectedSamplesPerSecond = txt;

            txt = SampleLengthListBox.SelectedItem.ToString();
            fparms.SignalLength = Convert.ToDouble(txt);
            fparms.npts = (int)(fparms.SamplesPerSecond * fparms.SignalLength);
            fparms.SelectedSignalLength = txt;

            txt = mainFrequencyListBox.SelectedItem.ToString();
            fparms.Frequency1 = Convert.ToDouble(txt);
            fparms.SelectedFrequency1 = txt;

            txt =  musicalThirdFrequencyListBox.SelectedItem.ToString();
            fparms.Frequency2 = Convert.ToDouble(txt);
            fparms.SelectedFrequency2 = txt;
            txt =  musicalThirdApmlitudeListBox.SelectedItem.ToString();
            fparms.Amplitude2 = Convert.ToDouble(txt);
            fparms.SelectedAmplitude2 = txt;

            txt = musicalFifthFrequencyListBox.SelectedItem.ToString();
            fparms.Frequency3 = Convert.ToDouble(txt);
            fparms.SelectedFrequency3 = txt;
            txt = musicalFifthApmlitudeListBox.SelectedItem.ToString();
            fparms.Amplitude3 = Convert.ToDouble(txt);
            fparms.SelectedAmplitude3 = txt;

            fparms.SelectedOctave = (bool)octaveCheckBox.IsChecked;
            if ((bool)octaveCheckBox.IsChecked)
            {
                fparms.Frequency4 = fparms.Frequency1 * 2.0;
                txt = octaveApmlitudeListBox.SelectedItem.ToString();
                fparms.Amplitude4 = Convert.ToDouble(txt);
            }
            this.DialogResult = true;
            this.Close();
        }

        private void MusicalThirdFrequencyListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
