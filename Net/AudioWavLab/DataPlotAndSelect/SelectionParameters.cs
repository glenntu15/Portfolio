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
    public partial class Selection : Form
    {
        public double StartTime { get; set; }
        public double DeltaTime { get; set; }
        public int NumSegments { get; set; }

        public Selection()
        {
            InitializeComponent();
        }

        private void Selection_Load(object sender, EventArgs e)
        {
            startTimeTextBox.Text = string.Format("{0:N}", StartTime);
            segmentLengthTextBox.Text = string.Format("{0:N}", DeltaTime);
            numberSegmentsTextBox.Text = string.Format("{0}", NumSegments);
        }

        private void oKButton_Click(object sender, EventArgs e)
        {
            bool success;
            double val;
            int ival;

            success = double.TryParse(startTimeTextBox.Text, out val);
            if (success)
                StartTime = val;
            else
            {
                MessageBox.Show(" Error: " + startTimeTextBox.Text, "Invalid number format");
                return;
            }
            success = double.TryParse(segmentLengthTextBox.Text, out val);
            if (success)
                DeltaTime = val;
            else
            {
                MessageBox.Show(" Error: " + segmentLengthTextBox.Text, "Invalid number format");
                return;
            }
            success = Int32.TryParse(numberSegmentsTextBox.Text, out ival);
            if (success)
                NumSegments = ival;
            else
            {
                MessageBox.Show(" Error: " + numberSegmentsTextBox.Text, "Invalid number format");
                return;
            }
        }
    }
}
