using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using XYPLotter;
using System.Media;

namespace DataAndResultsPlot
{
    public partial class PlotAndSelect : Form
    {
        public int Xaxlen { get; set; }
        public int Yaxlen { get; set; }
        float[] x;
        float[] y;
        double signalLength;
        double deltaT;
        int npts;
        //int indexStart;
        //int indexEnd;

        // plotting
        double firstx, deltax;
        int xaxlen;
        double firsty, deltay;
        int yaxlen;
        Plotter plotter;
        bool isSelectionMode;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xarray">array of dependant variable (signal)</param>
        /// <param name="yarray">array of independant variable (time)</param>
        /// <param name="n">number of points in xarray, yarray</param>
        /// <param name="slength"></param>
        /// <param name="dt"></param>
        public PlotAndSelect(ref float[] xarray, ref float[] yarray, int n, double slength)
        {
            InitializeComponent();
            plotter = new Plotter();

            x = xarray;
            y = yarray;
            npts = n;
            signalLength = slength;
            //indexStart = 0;
            //indexEnd = 0;
            deltaT = signalLength / (double)n;

            xaxlen = 5;
            firstx = 0.0;
            //deltax = signalLength / (double)xaxlen;
            deltax = 100;
            firsty = -1.0;
            deltay = 0.5;
            yaxlen = 4;
            isSelectionMode = false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xarray"></param>
        /// <param name="yarray"></param>
        /// <param name="npts"></param>
        /// <param name="length"></param>
        public void ShowAndSelect()
        {
            
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 
        /// </summary>
        private void DrawPLot()
        {
            Size windowSize = plotPictureBox.Size;
            Graphics gx = plotPictureBox.CreateGraphics();
            plotter.Reset(gx);
           
            plotter.Axis(0, firstx, deltax, xaxlen, "Time (ms)");
            plotter.Axis(1, firsty, deltay, yaxlen, "Value (scaled)");

            plotter.Line(ref x, ref y, npts, 0, Color.MediumSlateBlue, 0, 0, "f", 0, 1);
            plotter.Draw(windowSize, gx);
        }

        
        private void replotSelectedButton_Click(object sender, EventArgs e)
        {
            DrawPLot();
        }

        private void fileWriteButton_Click(object sender, EventArgs e)
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
            sw.WriteLine("DSN waveform");
            sw.WriteLine("Time,Amplitude");
            for (int i = 0;i< npts;i++) {
                sw.WriteLine("{0},{1}", x[i],y[i]);
            }
            sw.Close();
        }

        private void plotPictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (isSelectionMode)
            {
                int X = e.X;
                int Y = e.Y;
                double wx = plotter.GetWorldXFromPixel(X);
                MessageBox.Show(string.Format("X: {0} Y: {1} world X: {2}", X, Y, wx));

                plotPictureBox.Update();
            }


        }
        private void plotPictureBox_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            DrawPLot();
        }
        private void plotPictureBox_SizeChanged(object sender, EventArgs e)
        {
            DrawPLot();
            plotPictureBox.Refresh();
        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            if (isSelectionMode)
            {
                isSelectionMode = false;
                selectButton.BackColor = SystemColors.Control;
            }
            else
            {
                isSelectionMode = true;
                selectButton.BackColor = Color.Red;
            }
                
        }

        private void PlotAndSelect_Load(object sender, EventArgs e)
        {
            DrawPLot();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {

        }

        private void sePlotScaleButton_Click(object sender, EventArgs e)
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
            }

            DrawPLot();
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            SoundPlayer player = new SoundPlayer();
            player.Play();
        }
        
    }
}
