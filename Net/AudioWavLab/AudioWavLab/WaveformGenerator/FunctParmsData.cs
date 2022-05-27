using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioWavLab
{
    public class FunctParmsData
    {
        public double SamplesPerSecond;
        public double SignalLength;
        public double Frequency1;
        public double Frequency2;
        public double Amplitude2;
        public double Frequency3;
        public double Amplitude3;
        public double Frequency4;
        public double Amplitude4;
        public string SelectedSamplesPerSecond { get; set; }
        public string SelectedSignalLength { get; set; }
        public string SelectedFrequency1 { get; set; }  // fundamental
        public string SelectedFrequency2 { get; set; }  // third
        public string SelectedAmplitude2 { get; set; }  
        public string SelectedFrequency3 { get; set; }  // fitfh
        public string SelectedAmplitude3 { get; set; }
        public string SelectedAmplitude4 { get; set; }  // octive
        public bool SelectedOctave { get; set; }
        public bool SaveDataToFile { get; set; }
        public int npts;

        public FunctParmsData()
        {
            SamplesPerSecond = 48000.0;
            SignalLength = 0.1;
            Frequency1 = 110.00;
            Frequency2 = 110.00;
            Amplitude2 = 0.0;
            Frequency3 = 110.0;
            Amplitude3 = 0.0;
            Frequency4 = 0.0;
            Amplitude4 = 0.0;
            SelectedOctave = false;
            SaveDataToFile = false;

            SelectedSamplesPerSecond = "1024";
            SelectedSignalLength = "1.0";
            SelectedFrequency1 = "110";
            SelectedFrequency2 = "138.59";
            SelectedAmplitude2 = "0.0";
            SelectedFrequency3 = "164.81";
            SelectedAmplitude3 = "0.0";
            SelectedAmplitude4 = "0.0";
        }
    }
}
