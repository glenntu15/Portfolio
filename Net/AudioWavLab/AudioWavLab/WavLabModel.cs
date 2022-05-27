using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAndResultsPlot;
using EricOulashin;
using System.IO;

using AudioWavLab.Analysis;

namespace AudioWavLab
{
    /// <summary>
    /// The class WavLabModel just hold the data communicates with the other objects that do the work
    /// </summary>
    public class WavLabModel 
    {
        public float[] time;
        public float[] values;
        public float[] wvalues;
        public float[] freq;
        public float[] power;
        public float[] shiftedfreq;
        public float[] normalizedpower;
        public List<Tuple<float, float>> sortedspectra;
        public List<Tuple<float, float>> filteredsortedspectra;
        public double FreqofMaxValue;
        private string fileName;
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        private int samplePoints;
        public int SamplePoints
        {
            get { return samplePoints; }
            set { samplePoints = value; }
        }
        private int spectralPoints;
        public int SpectralPoints
        {
            get { return spectralPoints; }
            set { spectralPoints = value; }
        }

        // public int SpectralPoints { get; set; }
        public double SampleRate { get; set; }
        public double SignalLength { get; set; }
        public double MaxValue { get; set; }
        public double DeltaT { get; set; }
        public int BitsPerSample { get; set; }

        /// <summary>
        ///  *** constructor ***
        /// </summary>
        public WavLabModel()
        {
            samplePoints = 0;
            SignalLength = 0.0;
            spectralPoints = 0;
            freq = null;

        }
        /// <summary>
        /// Call this before using ;)
        /// </summary>
        /// <param name="npts"></param>
        public void CreateSignalArrays(int npts)
        {
           
            time = new float[npts];
                
            values = new float[npts];
        }
        //  ---------------------------------------------------------------------------
        public void PlotSignal()
        {
            PlotAndSelect plt = new PlotAndSelect(ref time, ref values, SamplePoints, SignalLength);
            plt.Show();
        }
        //  ---------------------------------------------------------------------------
        public void CreateNormalizedandShifted(int WindowingFlag)
        {

            // create specra if needed
            if (spectralPoints <= 0)
                Transform(WindowingFlag);

            float fr, am;  // 
            sortedspectra = new List<Tuple<float, float>>();
            filteredsortedspectra = new List<Tuple<float, float>>();
            for (int i = 0;i<spectralPoints;i++)
            {
                fr = freq[i];
                if (fr > 10000.0f)
                    break;
                am = power[i];
                var entry = Tuple.Create(fr, am);
                sortedspectra.Add(entry);
                if (fr > 99.999)
                    filteredsortedspectra.Add(entry);
            }

            //sortedspectra.Sort((x, y) => y.Item1.CompareTo(x.Item1));
            sortedspectra.Sort((x, y) => y.Item2.CompareTo(x.Item2));
            //var result = sortedspectra.OrderByDecending(x => x.Item2).ToList();

            FreqofMaxValue = sortedspectra[0].Item1;
            double maxamplitude = sortedspectra[0].Item2;
            double ratio = 220.0 / FreqofMaxValue;

            filteredsortedspectra.Sort((x, y) => x.Item1.CompareTo(y.Item1));

           

            shiftedfreq = new float[spectralPoints];
            normalizedpower = new float[spectralPoints];
            for (int i = 0;i<spectralPoints;i++)
            {
                shiftedfreq[i] = (float)((double)freq[i] * ratio);
                normalizedpower[i] = (float)((double)power[i] / maxamplitude);
            }

        }
        //  ---------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------
        public void Analyze()
        {
            //Transform();
            //Fourier dft = new Fourier(SamplePoints, SampleRate);
            //freq = new float[SamplePoints];
            //power = new float[SamplePoints];
            //dft.Discrete(ref values, ref freq, ref power);
            
            AnalyzeForm dlg = new AnalyzeForm(this);
            dlg.Show();
            // end temp
           // SpectralResults dlg = new SpectralResults();
           // dlg.Set(ref freq, ref power, spectralPoints);
            //dlg.Show();

        }
        public void Transform(int WindowFunctionFlag)
        {
            // full transform

            Fourier dft = new Fourier(SamplePoints, SampleRate);
            freq = new float[SamplePoints];
            power = new float[SamplePoints];
            int newNpoints = 0;
            if (WindowFunctionFlag == 0)
            {
                spectralPoints = SamplePoints / 2;
                dft.Discrete(ref values, ref freq, ref power);
            }
            else
            {
                SignalWindowing sw = new SignalWindowing();
                if (WindowFunctionFlag == 1)
                    wvalues = sw.GenerateRampWindowed(SamplePoints, ref values, ref newNpoints);
                if (WindowFunctionFlag == 2)
                    wvalues = sw.GeneratePlanckTukeyWindowed(SamplePoints, ref values, ref newNpoints);
                if (WindowFunctionFlag == 3)
                    wvalues = sw.GenerateFlattopWindowed(SamplePoints, ref values, ref newNpoints);
                spectralPoints = newNpoints / 2;
                dft.Discrete(ref wvalues, ref freq, ref power);
                SaveSignalToFile(1);
            }
        }
        public void SetWindowingfunction(int functionFlag)
        {


        }
        public void PlotSpectra(int which)
        {
            SpectralResults dlg = new SpectralResults();
            if (which == 0)
                dlg.Set(ref freq, ref power, spectralPoints,"Raw Spectra");
            else if (which == 1)
                dlg.Set(ref shiftedfreq, ref power, spectralPoints, "Shifted Spectra");
            else if (which == 2)
                dlg.Set(ref freq, ref normalizedpower, spectralPoints, "Normalized Spectra");
            else if (which == 3)
                dlg.Set(ref shiftedfreq, ref normalizedpower, spectralPoints, "Normalized Spectra");
            dlg.Show();
            //dlg.Plot();
        }
        public void ShowReport(int which)
        {
            float [] resultvector = new float[6];

            ReportGenWindow wind = new ReportGenWindow();
           
            float freqofMaxValue = sortedspectra[0].Item1;
            float maxamplitude = sortedspectra[0].Item2;

            //Tuple<float, float> pnt;
            List<Tuple<float, float>> spectrapeaks = new List<Tuple<float, float>>();
            float cutoff = maxamplitude * 0.33f;
            foreach(Tuple<float, float> pnt in filteredsortedspectra)
            {
                float am = pnt.Item2;
                if (am > cutoff)
                    spectrapeaks.Add(pnt);
            }

            resultvector[0] = freqofMaxValue / sortedspectra[1].Item1;                  //rf01
            resultvector[1] = freqofMaxValue / sortedspectra[2].Item1;                  //rf02
            resultvector[2] = sortedspectra[1].Item1 / sortedspectra[2].Item1;          //rf12
            resultvector[3] = maxamplitude / sortedspectra[1].Item2;                    //af01
            resultvector[4] = maxamplitude / sortedspectra[2].Item2;                    //af02
            resultvector[5] = sortedspectra[1].Item2 / sortedspectra[2].Item2;          //af12

            wind.Set(sortedspectra, resultvector);
            wind.Show();
        }
//  ---------------------------------------------------------------------------
        public void ReadSignal(int mseconds)
        {
            WAVFile wf = new WAVFile();
            bool success = false;
            // Create OpenFileDialog 

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".wav";
            dlg.Filter = "WAV File (.txt)|*.wav";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document
                FileName = dlg.FileName;
                
                string rslt = wf.Open(FileName, WAVFile.WAVFileMode.READ);
                if (rslt == "")
                    success = true;
            }
            if (!success)
                return;

            SampleRate = wf.SampleRate;
            int MaxPoints = wf.SampleRate * mseconds / 1000;
            values = new float[MaxPoints];
            time = new float[MaxPoints];

            byte[] sample;
            float[] fsample;
            float deltaT = 1000.0f / (float)wf.SampleRate;
            float t = 0.0f;
            // Stereo 16 bit per sample
            float maxValue = 0.0f;
            SamplePoints = 0;
            if ((wf.BytesPerSample == 4 && wf.NumChanels == 2) || (wf.BytesPerSample ==2))
            { 
                for (int i = 0; i< MaxPoints; i++)
                {
                    sample = wf.GetNextSample_ByteArray();
                    if (sample.Length < 2)
                        break;
                    SamplePoints++;
                    
                    //short iiix = BitConverter.ToInt16(sample,0);
                    values[i] = (float)BitConverter.ToInt16(sample,0);
                    time[i] = t;
                    if (values[i] > maxValue)
                        maxValue = values[i];
                    t += deltaT;
                    if (SamplePoints >= MaxPoints)  // done :)
                        break;
                }
                for (int j = 0; j < SamplePoints; j++)
                    values[j] = values[j] / maxValue;
            }
            else if (wf.BytesPerSample == 3)
            {
                // future reading of mono 24 bit samples
                sample = wf.GetNextSample_ByteArray();
            }
            // stereo or mono float samples
            else if (wf.BytesPerSample % 4 == 0)
            {
                for (int i = 0; i < SamplePoints; i++)
                {
                    fsample = wf.GetNextSample_FloatArray();
                    values[i] = fsample[0];
                    if (values[i] > maxValue)
                        maxValue = values[i];
                    time[i] = t;
                    t += deltaT;
                }
            }
            else
            {
                MessageBox.Show("Error: unable to read this file");
            }
            DeltaT = deltaT;
            MaxValue = maxValue;
            BitsPerSample = wf.BitsPerSample;
            SignalLength = (double)t / 1000.0;  // signal length in seconds
            Console.WriteLine("done - Maxvalue = {0}", MaxValue);
            spectralPoints = 0; // flag to clear spectra
            wf.Close();
        }
//  ---------------------------------------------------------------------------
        public void SaveSignalToFile(int flag)
        {
            if (samplePoints < 1)
            {
                MessageBox.Show(" You have no data");
                return;
            }

            DialogResult result;
            StreamWriter sw;
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.DefaultExt = ".txt"; // Default file extension 
            dlg.Filter = "text files (.txt)|*.txt"; // Filter files by extension 
            result = dlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                //string filename = dlg.FileName;
                FileInfo fi = new FileInfo(dlg.FileName);
                if (fi.Exists)
                {
                    sw = new StreamWriter(fi.Open(FileMode.Truncate)); // remove prev contents
                }
                else
                {
                    sw = new StreamWriter(fi.Open(FileMode.CreateNew));
                }
            } 
            else
            {
                return;
            }

            if (flag == 0)
            {
                sw.WriteLine("{0},{1}", SamplePoints, SampleRate);
                for (int i = 0; i < samplePoints; i++)
                {
                    sw.WriteLine("{0},{1}", time[i], values[i]);
                }
               
            } else
            {
                sw.WriteLine("{0},{1}", SamplePoints, SampleRate);
                for (int i = 0; i < samplePoints; i++)
                {
                    sw.WriteLine("{0},{1},{2}", time[i], values[i],wvalues[i]);
                }
            }
            sw.Close();

        }
    }
}
