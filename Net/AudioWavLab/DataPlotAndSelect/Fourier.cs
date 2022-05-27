using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DataAndResultsPlot
{
    public class Fourier
    {
        public float[] freq { get; set; }
        public float[] power { get; set; }

        int N;
        // public double[] y;
        // public double[] xw;
        double aa, bb;
       // public double[] a;
        //public double[] b;
        public double[] sine;
        public double[] cosine;
        double dN;
        double dN2;
        double freqConvert;
 
        public Fourier(int npts, double sampleRate)
        {
            N =  npts;
            dN = (double)N;
            dN2 = N * 2.0;
            // df = fs / N; dt = 1./ fs => fs = 1 / dt;
            freqConvert = sampleRate / dN;


            //Stopwatch sw = new System.Diagnostics.Stopwatch();
            //sw.Start();

            //xw = new double[N + 1];
            sine = new double[N + 1];
            cosine = new double[N + 1];

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="values">intput voltage value</param>
        /// <param name="freq">output spectral frequency</param>
        /// <param name="spectra">output spectral amplitude</param>
        /// <param name="npts">number of points to process</param>
        public void Discrete(ref float[] values, ref float[] freq, ref float[] spectra)
        {
            int k, n;
            int index;
            Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();


            cosine[0] = 1.0;    // we don't have to calculate cos(0) = 1
            sine[0] = 0.0;      //                        and sin(0) = 0
            for (k = 1; k < N; k++) //  init vectors of unit circle
            {
                cosine[k] = Math.Cos((2.0 * Math.PI * (double)(k) / dN));
                sine[k] = Math.Sin((2.0 * Math.PI * (double)(k) / dN));
            }

            float debuglastf;
            double dN2 = 2.0 / dN;
            int hN = N / 2;
            if (N > 0)
            {
                for (k = 0; k < N; k++)
                {
                    aa = 0.0;
                    bb = 0.0;
                    index = 0;
                    for (n = 0; n < (N - 1); n++)
                    {
                        //index = (k * n) % N;
                        
                        aa = aa + ((cosine[index] * values[n]));
                        bb = bb + ((sine[index] * values[n]));

                        index = index + k;
                        if (index >= N)
                            index = index - N;
                    }
                    aa = aa * dN2;
                    bb = bb * dN2;
                    freq[k] = (float)((double)k * freqConvert);
                    debuglastf = freq[k];
                    spectra[k] = (float)Math.Sqrt(aa * aa + bb * bb);
                }
            }
            sw.Stop();
            
            Console.WriteLine(" Total time = {0} ms", sw.ElapsedMilliseconds);
            //Console.Write("done");
        }
    }
    
}
