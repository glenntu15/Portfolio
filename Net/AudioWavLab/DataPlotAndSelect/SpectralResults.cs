using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using XYPLotter;

namespace DataAndResultsPlot
{
    public partial class SpectralResults : Form
    {
        Plotter plotter;
        float[] freq;
        float[] power;
        string title;
        int npts;

        // plotting
        double firstx, deltax;
        int xaxlen;
        double firsty, deltay;
        int yaxlen;

        public SpectralResults()
        {
            InitializeComponent();
            plotter = new Plotter();
        }

        public void Set(ref float[] f, ref float[] p, int n, string intitle)
        {
            freq = f;
            power = p;
            npts = n;
            title = intitle;

            firstx = 0.0;
            //deltat = freq[npts/2] / (double)xaxlen;
            deltax = 50.0;
            xaxlen = 20;
            firsty = 0.0;
            deltay = 0.05;
            yaxlen = 5;
        }

        private void Application_Idle(Object sender, EventArgs e)
        {

            MessageBox.Show("You are in the Application.Idle event.");

        }
        public void Plot()
        {
            Size windowSize = plotPictureBox.Size;
            Graphics gx = plotPictureBox.CreateGraphics();
            plotter.Reset(gx);

            plotter.Axis(0, firstx, deltax, xaxlen, "Frequency");
            plotter.Axis(1, firsty, deltay, yaxlen, "Power");

            plotter.Line(ref freq, ref power, npts, 2, Color.Crimson, 0, 0, "f", 0, 1);
            plotter.Title = title;

            plotter.Draw(windowSize, gx);
        }

        private void saveDataButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            StreamWriter sw;

            dlg.Filter = "text files (.csv)|*.csv"; // Filter files by extension 
            dlg.FileName = "waveform.csv";
            dlg.Title = "Data CSV file";
            DialogResult result = dlg.ShowDialog();
            if (result == DialogResult.OK)
            {
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
            StringBuilder sb = new StringBuilder();
            sw.WriteLine("DSN Spectra");
            sw.WriteLine("Frequency,Amplitude");
            for (int i = 0; i < npts; i++)
            {
                sw.WriteLine("{0},{1}", freq[i], power[i]);
            }
            sw.Close();
        }

        //private void SpectralResults_Load(object sender, EventArgs e)
        //{
        //    //MessageBox.Show("You are in the Form.SLoad event.");
        //    //Plot();

        //    plotbutton_Click(sender, e);
        //}
        
        private void SpectralResults_Shown(Object sender, EventArgs e)
        {
            //MessageBox.Show("You are in the Form.Shown event.");
            //plotbutton_Click(sender, e);
        }

        private void plotbutton_Click(object sender, EventArgs e)
        {
            Plot();
        }

        private void setScaleButton_Click(object sender, EventArgs e)
        {
            PlotScaleForm dlg = new PlotScaleForm();
            dlg.Firstx = firstx;
            dlg.Deltax = deltax;
            dlg.Xaxlen = xaxlen;
            dlg.Firsty = firsty;
            dlg.Deltay = deltay;
            dlg.Yaxlen = yaxlen;
            dlg.ShowDialog();
            if (dlg.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                firstx = dlg.Firstx;
                deltax = dlg.Deltax;
                xaxlen = dlg.Xaxlen;
                firsty = dlg.Firsty;
                deltay = dlg.Deltay;
                yaxlen = dlg.Yaxlen;
                title = dlg.Ptitle;
            }

        }
    }
}
