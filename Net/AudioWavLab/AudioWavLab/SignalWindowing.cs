using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace AudioWavLab
{
    class SignalWindowing
    {
        //int windowType;
        int Npoints;

        public float[] GenerateRampWindowed(int Npoints, ref float[] values, ref int newNpoints)
        {
            int startsign = 0;
            float[] wvalues = new float[Npoints];

            // First cut: just zero out the array.
            newNpoints = Npoints;
            bool done = false;
            int inext = 0;
            if (Math.Abs(values[0]) > .00001)
            {
                if (values[0] > 0.0)
                    startsign = 1;
                else
                    startsign = -1;

                for (int i = 0; i < Npoints; i++)
                {
                    if (startsign == 1)
                    {
                        if (values[i] > 0.0)
                        {
                            wvalues[newNpoints] = 0.0f;
                        }
                        else
                        {
                            wvalues[newNpoints] = values[i];
                            done = true;
                        }
                    }
                    else
                    {
                        if (values[i] < 0.0)
                        {
                            wvalues[newNpoints] = 0.0f;
                        }
                        else
                        {
                            wvalues[newNpoints] = values[i];
                            done = true;
                        }
                    }
                    inext = i + 1;
                    if (done)
                        break;
                }

            }
            // end of first point not zero
            // copy the rest of the points
            for (int i = inext; i < Npoints; i++)
            {
                wvalues[i] = values[i];
            }

            int last = Npoints - 1;
            if (Math.Abs(values[last]) < .00001)
            {
                return wvalues;
            }
            if (values[last] > 0.0)
                startsign = 1;
            else
                startsign = -1;
            int iend = last;
            for (int i = last; i > inext; i--)
            {
                if (startsign == 1)
                {
                    if (values[i] > 0.0)
                    {
                        wvalues[i] = 0.0f;
                    }
                    else
                    {
                        done = true;
                    }
                }
                else
                {
                    if (values[i] < 0.0)
                    {
                        wvalues[i] = 0.0f;
                    }
                    else
                    {
                        done = true;
                    }
                }
                if (done)
                    break;
            }

            return wvalues;
        }
        public float[] GeneratePlanckTukeyWindowed(int Npoints, ref float[] values, ref int newNpoints)
        {
            double alpha = 0.25;
            float[] wvalues = new float[Npoints];
            double[] windowf = new double[Npoints];

            double N2 = (double)Npoints / 2.0;
            //double term = (1.0 + alpha) * N2;
            double term =  alpha * N2;
            double denom = (1.0 - alpha) * (double)(Npoints);
            int aN = (int)(alpha * (double)Npoints);
            double dan = (alpha * (double)Npoints);

            newNpoints = Npoints;
            int k;
            int iend = aN / 2;
            for (int i = 0; i < iend; i++)
            {
                //k = i - Npoints / 2;
                //double Z = ((double)k - term) / denom;
                //windowf[i] = 0.5 * (1.0 + Math.Cos(Math.PI * Z));
                double Z = (2.0 * (double)i) / dan - 1.0;
                windowf[i] = .5 * (1.0 + Math.Cos(Math.PI * Z));
                wvalues[i] = (float)windowf[i] * values[i];
            }
            int istart = iend;
            iend = Npoints - iend;
            for (int i = istart; i< iend; i++)
            {
                windowf[i] = 1.0;
            }
            istart = iend;
            double x = 2.0 / alpha + 1.0;
            for (int i = istart; i < Npoints; i++)
            {
                double Z = (2.0 * (double)i) / dan - x;
                windowf[i] = .5 * (1.0 + Math.Cos(Math.PI * Z));
            }

            //SaveWindowToFile(Npoints, windowf);
            return wvalues;
        }
        public float[] GenerateFlattopWindowed(int Npoints, ref float[] values, ref int newNpoints)
        {
            float[] wvalues = new float[Npoints];
            double[] windowf = new double[Npoints];
            double term, kterm;
            term = Math.PI / (double)(Npoints - 1);

            newNpoints = Npoints;

            for(int i = 0;i<Npoints;i++)
            {
                kterm = term * (double)i;
                //windowf[i] = 1.0 - 1.24 * Math.Cos(2.0 * kterm) + 0.244 * Math.Cos(4.0 * kterm)
                //           + 0.00305 * Math.Cos(6 * kterm);
                windowf[i] = .21557895 - 0.41663158 * Math.Cos(2.0 * kterm) + 0.277263158 * Math.Cos(4.0 * kterm)
                           - 0.083578947 * Math.Cos(6 * kterm) + 0.006947368 * Math.Cos(8 * kterm);
                wvalues[i] = values[i] * (float)windowf[i];
            }

            SaveWindowToFile(Npoints, windowf);
            return wvalues;
        }

        public void SaveWindowToFile(int Npoints, double[] values)
        {

            DialogResult result;
            StreamWriter sw;
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.DefaultExt = ".csv"; // Default file extension 
            dlg.Filter = "csv files (.csv)|*.csv"; // Filter files by extension 
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


            sw.WriteLine("DSN,Filter,DST,Standard");
            sw.WriteLine("K,Value");
            for (int i = 0; i < Npoints; i++)
            {
                sw.WriteLine("{0},{1}", i, values[i]);
            }


            sw.Close();

        }
    }
}
