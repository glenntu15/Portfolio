using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;
using AudioWavLab.Events;


namespace AudioWavLab
{
    public delegate void FunctionGeneratedEventHandler(object sender, GeneratorStatusEventArgs e);

    public class WaveFormGenerator
    {
        public event FunctionGeneratedEventHandler Calculated;

        public int SamplesPerSec { get; set; }
        public void WaveformGenerator()
        {
        }

    /// <summary>
    /// Generates a time series
    /// </summary>
    /// <param name="p"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void Generate(FunctParmsData p, ref WavLabModel data)
    {
        double deltaT = 1.0 / p.SamplesPerSecond;  // seconds
        double f1 = Math.PI * p.Frequency1 * 2.0;  // in radians
        double f2 = Math.PI * p.Frequency2 * 2.0;
        double f3 = Math.PI * p.Frequency3 * 2.0;
        double f4 = Math.PI * p.Frequency4 * 2.0;
        int nSamples = p.npts;
        double scale = 1.0 / (1.0 + p.Amplitude2 + p.Amplitude3 + p.Amplitude4);
       
       // Times = new double[nSamples];
        //Values = new double[nSamples];
        double time = 0.0;
        double value;
        //int len = x.Length;
        //Console.WriteLine(" length {0}", len);
        data.time[0] = 0.0f;
        data.values[0] = 0.0f;
        for (int i = 1; i < nSamples; i++)
        {
            time += deltaT;
            value = Math.Sin(f1 * time);
            if (p.Amplitude2 > 0.001)
                value += Math.Sin(f2 * time) * p.Amplitude2;
            if (p.Amplitude3 > 0.001)
                value += Math.Sin(f3 * time) * p.Amplitude3;
            if (p.Amplitude4 > 0.001)
                value += Math.Sin(f4 * time) * p.Amplitude4;

            data.time[i] = (float)time * 1000.0f;
            data.values[i] = (float)(value * scale);
        }
        data.SamplePoints = nSamples;
        data.SignalLength = p.SignalLength;
        data.SampleRate = p.SamplesPerSecond;
        if (nSamples > 1 )
        {
            GeneratorStatusEventArgs e = new GeneratorStatusEventArgs(nSamples);
            if (Calculated != null)
                Calculated(this, e);
        }

    }

    }
    public static class Trace
    {    
        public static void WriteLine(string message,        
            [CallerFilePath] string file = "",        
            [CallerLineNumber] int line = 0,        
            [CallerMemberName] string member = "")    
        {        
            var s = string.Format("{0}:{1} - {2}: {3}", file, line, member, message);        
            Console.WriteLine(s);    
        }
    }
}
