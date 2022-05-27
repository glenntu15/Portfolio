using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataAndResultsPlot
{
    public partial class PlotScaleForm : Form
    {
        public double Firstx { get; set; }
        public double Deltax { get; set; }
        public int Xaxlen { get; set; }
        public double Firsty { get; set; }
        public double Deltay { get; set; }
        public int Yaxlen { get; set; }
        public string Ptitle { get; set; }

        public PlotScaleForm()
        {
            InitializeComponent();
        }

        private void PlotScaleForm_Load(object sender, EventArgs e)
        {
            firstXTextBox.Text = string.Format("{0:N}", Firstx);
            deltaXTextBox.Text = string.Format("{0:N}", Deltax);
            xAxisLenTextBox.Text = string.Format("{0}", Xaxlen);
            firstYTextBox.Text = string.Format("{0:N}", Firsty);
            deltaYTextBox.Text = string.Format("{0:N}", Deltay);
            yAxisLenTextBox.Text = string.Format("{0}", Yaxlen);
        }

        private void oKButton_Click(object sender, EventArgs e)
        {
            bool success;
            double val;

            success = double.TryParse(firstXTextBox.Text, out val);
            if (success)
                Firstx = val;
            else
            { 
                MessageBox.Show(" Error: "+firstXTextBox.Text,"Invalid number format");
                return;
            }
            success = double.TryParse(deltaXTextBox.Text, out val);
            if (success)
                Deltax = val;
            else
            { 
                MessageBox.Show(" Error: "+deltaXTextBox.Text,"Invalid number format");
                return;
            }
            success = double.TryParse(firstYTextBox.Text, out val);
            if (success)
                Firsty = val;
            else
            {
                MessageBox.Show(" Error: " + firstYTextBox.Text, "Invalid number format");
                return;
            }
            success = double.TryParse(deltaYTextBox.Text, out val);
            if (success)
                Deltay = val;
            else
            {
                MessageBox.Show(" Error: " + deltaYTextBox.Text, "Invalid number format");
                return;
            }
            int ival;
            success = Int32.TryParse(xAxisLenTextBox.Text, out ival);
            if (success)
                Xaxlen = ival;
            else
            {
                MessageBox.Show(" Error: " + xAxisLenTextBox.Text, "Invalid number format");
                return;
            }
            success = Int32.TryParse(yAxisLenTextBox.Text, out ival);
            if (success)
                Yaxlen = ival;
            else
            {
                MessageBox.Show(" Error: " + yAxisLenTextBox.Text, "Invalid number format");
                return;
            }
            Ptitle = pLotTitleTextBox.Text;
           
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
            
        }

        private void xAxisLenTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
