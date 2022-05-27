using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace XYPLotter
{
    /// <summary>
    /// Class Plotter draws an X-Y plot using a given graphics object
    /// </summary>
    /// <remarks>
    ///      Axis(double dFirstv, double dDeltav, int nTicks, int nAxisLocationCode, 
    ///					CString &csTitle, int nScaleType)
    /// 
    ///                 m_nAxisLocationCode         ____4________
    ///                                             |___3_______|	---Location Codes	
    ///		                                       1|___________| 2
    ///                                                  0				
    ///                 (only 0, 1, 2 supported)
    ///                 (Center line is drawn when scale spans zero)
    ///                
    ///      m_nScaleType;		   0 - linear, 1 - log
    ///      Line(double *pXarray, double *pYarray, int nPts, int nLineType,
    ///					  int nColor, CString csLegend, int nLinestyle, string label;
    ///					  int nXaxisScale, int nYaxisScale)
    ///             nLinestype 0, line only, >0 Line + symbols,  lt 0 symbols only 
    ///	            m_nLineStyle  0 solid, 1-dashed 2- dash dot, 3 dot dot
    /// 
    ///             nColor = { Color.Red, Color.Blue, Color.Green, Color.Violet, Color.Orange, 
    ///                         Color.Gray, Color.BlueViolet, Color.Chocolate };
    ///</remarks>
    /// Modifications
    ///  7/2014 Plot arrays of floats not doubles
	public class Plotter
    {
        //#pragma warning disable 1591

        private List<Axisdata> axisArray;
        private bool hasRightAxis;
        private bool hasTopAxis;
        private List<Linedata> linearray;
        public List<Linedata> Linearray
        {
            get { return linearray; }
        }
        private int nLines = 0;
        private int nAxis = 0;
        private int xLoc = 0;
        private int yLoc = 0;

        PlotArea plotarea;
        int heightflag = 3;
        Color BackGroundColor;
        bool setBackGround;
        int fontSize = 10;
        int fontSizeTitle;
        int fontSizeLabel;
        Font ticfont;
        Font labelfont;
        Font titlefont;
        Font tinyfont;

        #region plot parameters that are not reset
        private int plotType;
        public int PlotType
        {
            get { return plotType; }
            set { plotType = value; }
        }

        private bool useAlternateColors;
        public bool UseAlternateColors
        {
            get { return useAlternateColors; }
            set { useAlternateColors = value; }
        }
        #endregion

        private string title;
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private string subTitle;
        public string SubTitle
        {
            get { return subTitle; }
            set { subTitle = value; }
        }
        /// <summary>
        /// These are set when the plot is drawn -- they may be retrieved for use 
        /// by other drawings on the same canvas or to covert pixels to XY world coordinates
        /// </summary>
        private float xscale;
        public float XScale
        {
            get { return xscale; }
        }
        private float yscale;
        public float YScale
        {
            get { return yscale; }
        }

        #region PlotInteraction
        private bool hasSelection;
        public bool HasSelection
        {
            get { return hasSelection; }
            set { hasSelection = value; }
        }
        public double GetWorldXFromPixel(int pixelx)
        {
            double wx = 0.0;
            Axisdata axDataX = null;
           
            foreach (Axisdata ax in axisArray)
            {
                if (ax.Axistype == 0)
                    axDataX = ax;
            }
            double val = (double)(pixelx- plotarea.left) / (double)axDataX.seglen;
            wx = val * axDataX.deltav + (double)axDataX.firstv;

            return wx;
        }
        public double GetWorldYFromPixel(int pixely)
        {
            double wy = 0.0;
           
            Axisdata axDataY = null;
            foreach (Axisdata ax in axisArray)
            {
                if (ax.Axistype == 1)
                    axDataY = ax;
            }



            return wy;
        }
        //private double xvalSelected;
        #endregion

#pragma warning restore 1591

        /// <summary>
        /// constructor
        /// </summary>
		public Plotter()
        {
            axisArray = new List<Axisdata>();
            linearray = new List<Linedata>();
            plotarea = new PlotArea();
            //linearray = new Linedata[MAXLINES];
            PlotType = 0;
            useAlternateColors = false;
            title = "";
            subTitle = "";
            BackGroundColor = Color.White;
            setBackGround = false;
            hasTopAxis = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gx"></param>
        /// <remarks>Do not reset plottype</remarks>
        public void Reset(Graphics gx)
        {
            nLines = 0;
            title = "";
            subTitle = "";
            gx.Clear(Color.White);
            axisArray.Clear();
            linearray.Clear();
            hasRightAxis = false;
            xLoc = 0;
            yLoc = 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bkgrndColor"></param>
        public void SetPlotAreaColor(Color bkgrndColor)
        {
            BackGroundColor = bkgrndColor;
            setBackGround = true;
        }
        public int GetPlotTop()
        {
            return plotarea.top;
        }
        #region UserCallsSetUp
        /// <summary>
		/// Sets the parameters to draw an axis
		/// </summary>
		/// <param name="type"></param>
		/// <param name="firstv">first value</param>
		/// <param name="deltav">delta value per tic mark</param>
		/// <param name="axlen">number of tics</param>
		/// <param name="label">string to label axis</param>
		public void Axis(int type, double firstv, double deltav, int axlen,
                          string label)
        {
            Axisdata axis = new Axisdata(firstv, deltav, axlen, label, type);
            axisArray.Add(axis);
            if (type == 2)
                hasRightAxis = true;
            nAxis = axisArray.Count;
        }

        //Line(double *pXarray, double *pYarray, int nPts, int nLineType,
        //                     int nColor, CString csLegend, 
        //                     int legendType, int nLinestyle, 
        //                     int nXaxisScale, int nYaxisScale)

        /// <summary>
        /// 1st override -- most simple
        /// </summary>
        /// <param name="xarray">array of x values</param>
        /// <param name="yarray">array of y values</param>
        /// <param name="npts">number of points</param>
        /// <param name="symbol">symbol to use</param>
        /// <param name="linetype"> lt 0:symbols only,eq 0:line only, gt 0:line+symbols</param>
        public void line(ref double[] xarray, ref double[] yarray, int npts,
                       int symbol, int linetype)
        {
            Color[] cl = { Color.Blue, Color.Red, Color.Green, Color.Violet, Color.Orange,
                            Color.Orchid, Color.BlueViolet, Color.Chocolate };
            Color[] altcl = { Color.Violet, Color.BlueViolet, Color.Blue, Color.SteelBlue,
                             Color.LightBlue, Color.DarkGreen, Color.Green, Color.SlateBlue };

            int nclrs = cl.Length;


            int nLineStyle = 0;
            int nXaxisScale = 0;
            int nYaxisScale = 0;
            int legendType = 0;
            string legend = "";

            int n = nLines;
            if (n > nclrs)
                n = nLines % nclrs;
            Color useColor = cl[n];
            if (useAlternateColors)
                useColor = altcl[n];

            Line(ref xarray, ref yarray, npts, linetype,
                              useColor, nLineStyle, legendType, legend, nXaxisScale, nYaxisScale);
        }
        /// <summary>
        /// 2nd override -- has legend
        /// </summary>
        /// <param name="xarray">array of x values</param>
        /// <param name="yarray">array of y values</param>
        /// <param name="npts">number of points</param>
        /// <param name="symbol">symbol to use</param>
        /// <param name="linetype"> lt 0:symbols only,eq 0:line only, gt 0:line+symbols</param>
        /// <param name="lineStyle"> 0 solid, 1-dashed 2- dash dot, 3 dot dot </param>
        /// <param name="legendType">0 - no legend +int - line number at end -int line number at beginning</param>
        public void Line(ref double[] xarray, ref double[] yarray, int npts,
               int symbol, int linetype, int lineStyle, int legendType)
        {
            int nColor = nLines;
            int nLineStyle = lineStyle;
            int nXaxisScale = 0;
            int nYaxisScale = 0;
            string legend = "";

            Color[] cl = { Color.Blue, Color.Red, Color.Green, Color.Violet, Color.Orange,
                            Color.MediumTurquoise, Color.BlueViolet, Color.Chocolate };
            Color[] altcl = { Color.Violet, Color.BlueViolet, Color.Blue, Color.SteelBlue,
                             Color.LightBlue, Color.DarkGreen, Color.Green, Color.SlateBlue };

            int nclrs = cl.Length;
            int n = nLines;
            if (n > nclrs)
                n = nLines % nclrs;
            Color useColor = cl[n];
            if (useAlternateColors)
                useColor = altcl[n];

            Line(ref xarray, ref yarray, npts, linetype,
                              useColor, nLineStyle, legendType, legend, nXaxisScale, nYaxisScale);
        }

        /// <summary>
        /// This override adds color 
        /// </summary>
        /// <param name="xarray">array of x values</param>
        /// <param name="yarray">array of y values</param>
        /// <param name="npts">number of points</param>
        /// <param name="symbol">symbol to use</param>
        /// <param name="linetype"> lt 0:symbols only,eq 0:line only, gt 0:line+symbols</param>
        /// <param name="lineStyle"> 0 solid, 1-dashed 2- dash dot, 3 dot dot </param>
        /// <param name="legendType">0 - no legend +int - line number at end -int line number at beginning</param>
        /// <param name="colr">Line color of type Color</param>
        public void Line(ref double[] xarray, ref double[] yarray, int npts,
               int symbol, int linetype, int lineStyle, int legendType, Color colr)
        {
            int nLineStyle = lineStyle;
            int nXaxisScale = 0;
            int nYaxisScale = 0;
            string legend = "";

            Line(ref xarray, ref yarray, npts, linetype,
                              colr, nLineStyle, legendType, legend, nXaxisScale, nYaxisScale);
        }
        /// <summary>
        /// The full function Line -- with double arguments -- copy to floats
        /// </summary>
        /// <param name="xarray">array of x values</param>
        /// <param name="yarray">array of y values</param>
        /// <param name="npts">number of points</param>
        /// <param name="symbol">symbol to use</param>
        /// <param name="linetype"> lt 0:symbols only,eq 0:line only, gt 0:line+symbols</param>
        /// <param name="nColor">Line color of type Color</param>
        /// <param name="nLinestyle">(only when no symbols) 0 solid, 1: dash. 2: dash dot</param>
        /// <param name="legendType">0 - no legend +int - line number at end -int line number at beginning</param>
        /// <param name="label">label for legend</param>
        /// <param name="nXaxisScale">Not implemented -- which line to use for scaling</param>
        /// <param name="nYaxisScale"></param>
        public void Line(ref double[] xarray, ref double[] yarray, int npts, int linetype,
                              Color colr, int nLinestyle, int legendType, string label, int nXaxisScale, int nYaxisScale)
        {
            int symbol = Math.Abs(linetype);
            Linedata line = new Linedata(symbol, linetype, npts, colr, label, nLines, nLinestyle);
            //line.color = nColor;
            line.LegendType = legendType;

            for (int i = 0; i < npts; i++)
            {
                line.xarray[i] = (float)xarray[i];
                line.yarray[i] = (float)yarray[i];
            }
            linearray.Add(line);
            nLines++;
        }

        /// <summary>
        /// The full function Line except floats are used in the arrays
        /// No allocation of new storage uses provided storage
        /// </summary>
        /// <param name="xarray">array of x values</param>
        /// <param name="yarray">array of y values</param>
        /// <param name="npts">number of points</param>
        /// <param name="symbol">symbol to use</param>
        /// <param name="linetype"> lt 0:symbols only,eq 0:line only, gt 0:line+symbols</param>
        /// <param name="nColor">Line color of type Color</param>
        /// <param name="nLinestyle">(only when no symbols) 0 solid, 1: dash. 2: dash dot</param>
        /// <param name="legendType">0 - no legend +int - line number at end -int line number at beginning</param>
        /// <param name="label">label for legend</param>
        /// <param name="nXaxisScale"></param>
        /// <param name="nYaxisScale"></param>
        public void Line(ref float[] xarray, ref float[] yarray, int npts, int linetype,
                             Color colr, int nLinestyle, int legendType, string label, int nXaxisScale, int nYaxisScale)
        {
            int symbol = Math.Abs(linetype);
            Linedata line = new Linedata(symbol, linetype, npts, colr, label, nLines, nLinestyle, false);
            //line.color = nColor;
            line.LegendType = legendType;

            //for (int i = 0; i < npts; i++)
            //{
            //    line.xarray[i] = (double)xarray[i];
            //    line.yarray[i] = (double)yarray[i];
            //}
            line.xarray = xarray;
            line.yarray = yarray;
            linearray.Add(line);
            nLines++;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstv"></param>
        /// <param name="y"></param>
        /// <param name="npts"></param>
        /// <param name="num_v"></param>
        /// <param name="num_int"></param>
        /// <returns></returns>
        public double Scale(double firstv, double[] y, int npts,
                   int num_v, out int num_int)
        {
            num_int = num_v;
            int n = num_v;
            double dnumv = (double)num_v;
            double deltav = 1.0;
            double max = y[0];
            double min = y[0];
            double scale;

            double[] trial = { 2.0, 2.5, 4.0, 5.0, 7.5, 8.0, 10.0 };

            for (int i = 1; i < npts; i++)
            {
                if (!double.IsNaN(y[i]))
                {
                    if (y[i] > max)
                        max = y[i];
                    if (y[i] < min)
                        min = y[i];
                }
            }

            double range = (max - firstv);
            if (range == 0.0)
                return (1.0);
            deltav = range / (double)num_v;
            if ((deltav < 1.0) && (deltav > 0.1))
            {
                scale = 0.1;
            }
            else
            {
                scale = Math.Floor(Math.Log10(deltav));
                if (scale == 0.0)
                {
                    scale = 1.0;
                }
                else
                {
                    if (scale < 0.0)
                        scale = Math.Abs(Math.Pow(10.0, (scale)));
                    else if (scale > 0.0)
                        scale = Math.Pow(10.0, scale);
                }
            }
            deltav = deltav / scale;    //  scale is now in the range (0.,10)
            for (int i = 0; i < 7; i++)
            {
                if (trial[i] > deltav)
                {
                    deltav = trial[i] * scale;
                    //  if plotted array is less than 90 % of axis length reduce numv;
                    for (; ; )
                    {
                        if (max < (0.9 * deltav * dnumv))
                        {
                            num_int = --n;
                            dnumv = (double)(num_int);
                            continue;
                        }
                        if (max > (deltav * dnumv))
                        {
                            num_int = ++n;
                        }
                        return (deltav);
                    }
                }
            }
            return deltav;
        }


        public double SpecialScale(double[] y, int npts,
           int num_v, out int num_int)
        {
            num_int = num_v;
            int n = num_v;
            double dnumv = (double)num_v;
            double deltav = 1.0;
            double max = y[0];
            double scale;
            double firstv = 0.0;

            double[] trial = { 2.0, 2.5, 4.0, 5.0, 7.5, 8.0, 10.0 };

            for (int i = 1; i < npts; i++)
            {
                if (!double.IsNaN(y[i]))
                {
                    if (Math.Abs(y[i]) > max)
                        max = Math.Abs(y[i]);
                }
            }

            double range = (max - firstv);
            if (range == 0.0)
                return (1.0);
            deltav = range / (double)num_v;
            if ((deltav < 1.0) && (deltav > 0.1))
            {
                scale = 0.1;
            }
            else
            {
                scale = Math.Floor(Math.Log10(deltav));
                if (scale == 0.0)
                {
                    scale = 1.0;
                }
                else
                {
                    if (scale < 0.0)
                        scale = Math.Abs(Math.Pow(10.0, (scale)));
                    else if (scale > 0.0)
                        scale = Math.Pow(10.0, scale);
                }
            }
            deltav = deltav / scale;    //  scale is now in the range (0.,10)
            for (int i = 0; i < 7; i++)
            {
                if (trial[i] > deltav)
                {
                    deltav = trial[i] * scale;
                    //  if plotted array is less than 90 % of axis length reduce numv;
                    for (; ; )
                    {
                        if (max < (0.9 * deltav * dnumv))
                        {
                            num_int = --n;
                            dnumv = (double)(num_int);
                            continue;
                        }
                        if (max > (deltav * dnumv))
                        {
                            num_int = ++n;
                        }
                        return (deltav);
                    }
                }
            }
            return deltav;
        }
        #endregion

        #region Plotting code
        //--------------------------------------------------------------------
        //	This is the actual plotting code
        //--------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <param name="size">Size of he plot area</param>
        /// <param name="gx">Graphics object for that plotting area</param>
        /// <param name="xloc">x position of total plot area</param>
        /// <param name="yloc">y position of total plot area</param>
        public void Draw(Size size, Graphics gx, int xloc, int yloc)
        {
            xLoc = xloc;
            yLoc = yloc;
            Draw(size, gx);
        }
        /// <summary>
        /// Override using default location (0,0)
        /// </summary>
        /// <param name="size">Size of he plot area</param>
        /// <param name="gx">Graphics object for that plotting area</param>
        public void Draw(Size size, Graphics gx)
        {
            if (nLines < 1)
                return;
            if (nAxis < 1)
                return;
            int topmargin = 15 + yLoc;
            int rightmargin = 3;
            int leftmargin = 3 + xLoc;
            int bottommargin = 3;
            int ticlen = 6;

            if (size.Width > 500 || size.Height > 550)
                heightflag = 4;
            if (size.Width < 400 || size.Height < 350)
                heightflag = 2;
            if (size.Width < 300 || size.Height < 250)
                heightflag = 1;

            if (heightflag > 3)
                fontSize = 12;
            if (heightflag < 3)
                fontSize = 9;
            if (heightflag < 2)
                fontSize = 7;

            tinyfont = new Font("Arial", 8);
            //Console.WriteLine(" heightflag , fontSize {0}, {1)",heightflag);
            ticfont = new Font("Arial", fontSize);
            fontSizeLabel = fontSize + 1;
            labelfont = new Font("Arial", fontSizeLabel);
            fontSizeTitle = fontSize + 4;
            titlefont = new Font("Arial", (float)(fontSizeTitle), (FontStyle.Bold | FontStyle.Italic));
            if (title.Length > 0)
            {
                float titletextWidth = gx.MeasureString(title, titlefont).Width;
                float titletextHeigth = gx.MeasureString(title, titlefont).Height;
                PointF ptxt = new PointF(0, 0);
                ptxt.X = ((float)size.Width - titletextWidth) / 2.0f;
                ptxt.Y = (float)(topmargin + 2);
                SolidBrush brush1 = new SolidBrush(Color.DarkBlue);
                gx.DrawString(title, titlefont, brush1, ptxt);
            }
            // Compute the white space around the LHS Y-axis based on font size;
            //	ticlen + 1(space) + ticlabelWidth + 1(space) + labelheight + margin
            int labelLength = (int)GetMaxTicLabelLength(gx);

            //int offset = 2 + ticlen + labelLength + (int)labelfont.GetHeight();

            int offset = 6 + ticlen + labelLength + (int)labelfont.GetHeight();
            // Y-Axis
            plotarea.left = leftmargin + offset;
            // X-Axis 
            offset = 5 + ticlen + (int)(ticfont.GetHeight() + labelfont.GetHeight());

            plotarea.bottom = size.Height - offset - bottommargin;
            // if there is a rhs Y-Axis...
            if (hasRightAxis)
                rightmargin += (int)labelfont.GetHeight() + ticlen;
            //Console.WriteLine(" Right Margin of plot {0}", rightmargin);

            plotarea.top = topmargin;	// offset for title;

            offset = GetLastXTicLabelOffset(gx);

            plotarea.right = xLoc + size.Width - offset - rightmargin;
            //
            // plotarea is now set
            //
            if (setBackGround)
            {
                Point corner = new Point(plotarea.top, plotarea.bottom);
                //set size width, height
                Size sz = new Size((plotarea.right - plotarea.left), (plotarea.bottom - plotarea.top));
                Rectangle area = new Rectangle(corner, sz);
                SolidBrush brush1 = new SolidBrush(BackGroundColor);
                gx.FillRectangle(brush1, area);
            }
            Axisdata ax = axisArray[0];
            DrawAxis(ref gx, ax);
            ax = axisArray[1];
            DrawAxis(ref gx, ax);

            DrawBorders(gx);

            foreach (Linedata line in linearray)
            {
                DrawDataLine(ref gx, line);
            }
        }

        void DrawVerticalLineat(double Xworld)
        {
            Pen pen1 = new Pen(Color.Black, 1);
            Point p1 = new Point(10, plotarea.top);
            Point p2 = new Point(10, plotarea.bottom);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gx"></param>
        /// <returns></returns>
        private float GetMaxTicLabelLength(Graphics gx)
        {
            float len;

            Axisdata axData = axisArray[0];
            if (axData.Axistype != 1)
                axData = axisArray[1];

            double dvalue = axData.firstv;
            string ticvalue1 = dvalue.ToString("0.##");
            float ticvaluetextwidth1 = gx.MeasureString(ticvalue1, ticfont).Width;
            dvalue = axData.firstv + axData.axlen * axData.deltav;
            string ticvalue2 = dvalue.ToString("0.##");
            float ticvaluetextwidth2 = gx.MeasureString(ticvalue2, ticfont).Width;
            if (ticvaluetextwidth1 > ticvaluetextwidth2)
                len = ticvaluetextwidth1;
            else
                len = ticvaluetextwidth2;
            return len;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gx"></param>
        /// <returns></returns>
        private int GetLastXTicLabelOffset(Graphics gx)
        {
            Axisdata axData = axisArray[0];
            if (axData.Axistype != 0)
                axData = axisArray[1];

            double dvalue = axData.firstv;
            //string ticvalue1 = dvalue.ToString("0.##");
            //float ticvaluetextwidth1 = gx.MeasureString(ticvalue1, ticfont).Width;
            dvalue = axData.firstv + axData.axlen * axData.deltav;
            string ticvalue2 = dvalue.ToString("0.##");
            float ticvaluetextwidth2 = gx.MeasureString(ticvalue2, ticfont).Width;
            return (int)(ticvaluetextwidth2 / 2.0);
        }

        private int GetMaxXTicLabelHeight(Graphics gx)
        {
            int len;

            Axisdata axData = axisArray[0];
            double dvalue = axData.firstv;
            string ticvalue1 = dvalue.ToString("0.##");
            float ticvaluetextwidth1 = gx.MeasureString(ticvalue1, ticfont).Width;
            dvalue = axData.firstv + axData.axlen * axData.deltav;
            string ticvalue2 = dvalue.ToString("0.##");
            float ticvaluetextwidth2 = gx.MeasureString(ticvalue2, ticfont).Width;
            if (ticvaluetextwidth1 > ticvaluetextwidth2)
                len = (int)ticvaluetextwidth1;
            else
                len = (int)ticvaluetextwidth2;
            return len;

        }

        private void DrawBorders(Graphics gx)
        {
            Pen pen1 = new Pen(Color.Black, 1);
            Pen pen2 = new Pen(Color.Black, 1);
            Pen pen3 = new Pen(Color.Gray, 1);
            Pen pen;
            Point p1 = new Point(10, 10);
            Point p2 = new Point(10, 10);

            if (plotType == 1)
                pen = pen1;
            else
                pen = pen2;
            //top
            if (!hasTopAxis)
            {
                p1.X = plotarea.left;
                p1.Y = plotarea.top;
                p2.X = plotarea.right;
                p2.Y = plotarea.top;
                gx.DrawLine(pen, p1, p2);
            }
            p1.X = plotarea.right;
            p1.Y = plotarea.top;
            p2.X = plotarea.right;
            p2.Y = plotarea.bottom;
            gx.DrawLine(pen, p1, p2);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gx"></param>
        /// <param name="axData"></param>
        /// <remarks>For now only axis types 0 (x-axis at bottom and 1 Y-axis at left, are implemented</remarks>
        private void DrawAxis(ref Graphics gx, Axisdata axData)
        {
            Pen pen1 = new Pen(Color.Black, 2);
            Pen pen2 = new Pen(Color.Black, 1);
            Pen pen3 = new Pen(Color.LightGray, 1);
            Pen pendebug = new Pen(Color.Red, 2);
            Point p1 = new Point(10, 10);
            Point p2 = new Point(10, 10);
            float axislength = 0;
            int type = axData.Axistype;

            // Adjust bottom margin based on font size
            //	and tic length
            int ticlen = 4;
            if (type == 0)          // X-Axis - bottom
            {
                p1.X = plotarea.left;
                p1.Y = plotarea.bottom;
                p2.X = plotarea.right;
                p2.Y = plotarea.bottom;
                axislength = (float)p2.X - (float)p1.X;
            }
            if (type == 1)          // Y-axis LHS
            {
                p1.X = plotarea.left;
                p1.Y = plotarea.bottom;
                p2.X = plotarea.left;
                p2.Y = plotarea.top;
                axislength = (float)p1.Y - (float)p2.Y;
            }
            bool hasZeroLines = false;
            if (PlotType == 1)
            {
                double x;
                // does it span "0.0"
                if (axData.firstv < 0.0)
                {
                    x = axData.firstv + axData.axlen * axData.deltav;
                    if (x > 0.0)
                        hasZeroLines = true;
                }
                else
                {
                    x = axData.firstv + axData.axlen * axData.deltav;
                    if (x < 0.0)
                        hasZeroLines = true;
                }
            }
            /// draw the axis line (fine pen if not on plot borders)
            if (hasZeroLines)
                gx.DrawLine(pen2, p1, p2);
            else
                gx.DrawLine(pen1, p1, p2);
            String ticvalue;

            SolidBrush brush1 = new SolidBrush(Color.DarkBlue);
            int ntics = (int)axData.axlen;

            double dvalue = axData.firstv;

            // add tic marks and values
            PointF fp1 = new PointF(0, 0);
            PointF fp2 = new PointF(0, 0);
            PointF fp3 = new PointF(0, 0);
            //float axlen;
            if (type == 0)  // X-axis bottom
            {
                //axlen = (float)p2.X - (float)p1.X;
                axData.seglen = axislength / (float)ntics;
                fp1.Y = (float)(plotarea.bottom + ticlen);
                fp2.Y = (float)(plotarea.bottom - ticlen);
                fp1.X = (int)((float)plotarea.left + axData.seglen);
                fp2.X = fp1.X;
                gx.DrawLine(pen2, fp1, fp2);
                ticvalue = dvalue.ToString("0.###");
                float ticvaluetextheight = gx.MeasureString(ticvalue, ticfont).Height;
                float ticvaluetextwidth = gx.MeasureString(ticvalue, ticfont).Width;

                PointF ptxt = new PointF((float)plotarea.left - (ticvaluetextwidth / (float)2.0), (float)(p1.Y + ticlen));

                gx.DrawString(ticvalue, ticfont, brush1, ptxt);


                fp1.X = (float)p1.X;
                for (int i = 0; i < ntics; i++)
                {
                    fp1.X = fp1.X + axData.seglen;
                    fp2.X = fp1.X;
                    gx.DrawLine(pen2, fp1, fp2);
                    //extend the line to make hash
                    fp3.X = fp2.X;
                    fp3.Y = plotarea.top;
                    gx.DrawLine(pen3, fp2, fp3);
                    // add text showing value at each tic
                    dvalue = dvalue + axData.deltav;
                    ticvalue = dvalue.ToString("0.###");
                    ticvaluetextwidth = gx.MeasureString(ticvalue, ticfont).Width;
                    ptxt.X = fp1.X - (ticvaluetextwidth / (float)2.0);
                    gx.DrawString(ticvalue, ticfont, brush1, ptxt);
                    //fp1.X = fp1.X + seglen;
                    //Console.WriteLine(" ticvalue {0} written to x {1}",ticvalue,ptxt.X);
                }
                if (hasZeroLines)
                {
                    int nvals = (int)(-1.0 * axData.firstv / axData.deltav);
                    fp2.X = (int)(float)plotarea.left + axData.seglen * nvals;
                    fp3.X = fp2.X;
                    fp3.Y = plotarea.top;
                    gx.DrawLine(pen2, fp2, fp3);
                }

                // label the axis
                int len = axData.label.Length;
                if (len > 0)
                {
                    ticvaluetextheight = gx.MeasureString(ticvalue, ticfont).Height;
                    float labeltextwidth = gx.MeasureString(axData.label, labelfont).Width;
                    ptxt.X = plotarea.left + (axislength - labeltextwidth) / 2.0f;
                    ptxt.Y = (float)plotarea.bottom + ticlen + ticvaluetextheight + 1;
                    gx.DrawString(axData.label, labelfont, brush1, ptxt);
                }

            }
            else if (type == 1) // Y-axis LHS
            {
                float axlen = p2.Y - p1.Y;
                axData.seglen = axlen / (float)ntics;  // pixels per tic
                                                       //Console.WriteLine(" axlen, seglen {0} {1}",axlen,axisArray[type].seglen);
                fp1.Y = (float)plotarea.bottom + axData.seglen + (float)0.5;
                fp1.X = plotarea.left - ticlen;
                fp2.X = plotarea.left + ticlen;
                ticvalue = dvalue.ToString("0.##");
                float ticvaluetextheight = gx.MeasureString(ticvalue, ticfont).Height;
                float ticvaluetextwidth = gx.MeasureString(ticvalue, ticfont).Width;

                PointF ptxt = new PointF(fp1.X - ticvaluetextwidth, (float)(plotarea.bottom));
                ptxt.X = fp1.X - ticvaluetextwidth / (float)1.89;
                float minLeftTextVal = ptxt.X - ticvaluetextwidth;

                // label the y-axis
                int len = axData.label.Length;
                if (len > 0)
                {
                    float labeltextwidth = gx.MeasureString(axData.label, labelfont).Width;
                    float labeltextheight = gx.MeasureString(axData.label, labelfont).Height;
                    minLeftTextVal -= labeltextheight + 1;
                    PointF P = new Point(0, 0);
                    //P.X = (float)(plotarea.left - ticlen - ticvaluetextwidth - labeltextheight);
                    P.X = minLeftTextVal;
                    P.Y = (float)((float)plotarea.bottom - (axislength - labeltextwidth) / 2.0f);

                    SizeF S = new SizeF(labeltextwidth, labeltextwidth);
                    RectangleF R = new RectangleF(P, S);
                    DrawRotatedString(gx, axData.label, labelfont, brush1,
                        R, -90.0f);
                }
                StringFormat sf = new StringFormat
                    (StringFormatFlags.NoClip);
                sf.Alignment =
                    StringAlignment.Center;
                sf.LineAlignment =
                    StringAlignment.Center;
                gx.DrawString(ticvalue, ticfont, brush1, ptxt, sf);
                int ix = (int)ptxt.X;
                int iy = (int)ptxt.Y;
                //Console.WriteLine("Drawstring {0} ntics {1}", ticvalue, ntics);
                for (int i = 0; i < ntics; i++)
                {
                    fp2.Y = fp1.Y;
                    gx.DrawLine(pen2, fp1, fp2);
                    //extend the line to make hash
                    fp3.Y = fp2.Y;
                    fp3.X = plotarea.right;
                    gx.DrawLine(pen3, fp2, fp3);
                    // label the tic mark
                    dvalue = dvalue + axData.deltav;
                    ticvalue = dvalue.ToString("0.##");
                    ticvalue.Trim();
                    ticvaluetextwidth = gx.MeasureString(ticvalue, ticfont).Width;
                    //Console.WriteLine("text width {0}",textwidth);
                    ptxt.X = fp1.X - ticvaluetextwidth / (float)1.89;
                    ptxt.Y = fp1.Y;// + textwidth/(float)2.0;;
                    gx.DrawString(ticvalue, ticfont, brush1, ptxt, sf);
                    // Console.WriteLine("Drawstring {0} ", ticvalue);

                    fp1.Y = fp1.Y + axData.seglen;
                }
                if (hasZeroLines)
                {
                    int nvals = (int)(-1.0 * axData.firstv / axData.deltav);
                    fp2.Y = (int)(float)plotarea.bottom + axData.seglen * nvals;
                    fp3.Y = fp2.Y;
                    fp2.X = plotarea.left;
                    fp3.X = plotarea.right;
                    gx.DrawLine(pen2, fp2, fp3);
                }


            }
        }
        //
        //-----     -----     -----     -----     -----    -----    -----
        //
        private void DrawRotatedString(Graphics g, string text, Font font, Brush br,
            RectangleF rect, float angle)
        {
            PointF center = new PointF(rect.X + rect.Width / 2,
                rect.Y + rect.Height / 2);
            g.TranslateTransform(center.X, center.Y);
            g.RotateTransform(angle);
            rect.Offset(-center.X, -center.Y);
            //debug
            //Pen debugPen = new Pen(Color.Green,1);
            //g.DrawRectangle(debugPen, rect.Bottom, rect.Left, rect.Width, rect.Height);
            g.DrawString(text, font, br, rect.Bottom, rect.Left);
            g.ResetTransform();
        }

        /// <summary>
        /// Adds a legend to the current plot.
        /// </summary>
        /// <param name="size">The size of the current plot.</param>
        /// <param name="gx">The graphics object for the current plot.</param>
        /// <param name="legendData">
        /// A Linedata-String Dictionary that contains each line for the legend and
        /// the corresponding legend text.
        /// </param>
        public void addLegend(Size size, Graphics gx, int xloc, Dictionary<Linedata, string> legendData)
        {
            int height = legendData.Count * 14 + 4;
            //2pix padding around all sides
            Point start = new Point(xloc + 4, size.Height - (height + 2));
            Size borderSize = new Size(size.Width - 6, height);
            Rectangle legendBorder = new Rectangle(start, borderSize);
            gx.DrawRectangle(new Pen(Color.Black, 1), legendBorder);
            int pos = 4;
            foreach (Linedata data in legendData.Keys)
            {

                Pen linePen = new Pen(data.UseColor);

                if (data.LineStyle != 0)
                {
                    data.LineType = 0;
                    if (data.LineStyle == 1)
                        linePen.DashPattern = new float[] { 3f, 3f, 3f, 3f };
                    if (data.LineStyle == 2)
                        linePen.DashPattern = new float[] { 6f, 3f, 2f, 3f };
                    if (data.LineStyle == 3)
                        linePen.DashPattern = new float[] { 2f, 2f, 2f, 2f };
                }

                if (data.LineType != 0)
                {
                    Symbol(ref gx, ref linePen, data.Symbol, new Point(start.X + 4, start.Y + pos + 5));
                }

                gx.DrawLine(linePen, new Point(start.X + 4, start.Y + pos + 5), new Point(start.X + 14, start.Y + pos + 5));

                if (data.LineType != 0)
                {
                    Symbol(ref gx, ref linePen, data.Symbol, new Point(start.X + 14, start.Y + pos + 5));
                }
                gx.DrawLine(linePen, new Point(start.X + 14, start.Y + pos + 5), new Point(start.X + 24, start.Y + pos + 5));

                if (data.LineType != 0)
                {
                    Symbol(ref gx, ref linePen, data.Symbol, new Point(start.X + 24, start.Y + pos + 5));
                }
                gx.DrawLine(linePen, new Point(start.X + 24, start.Y + pos + 5), new Point(start.X + 24, start.Y + pos + 5));
                //Rectangle colorRectangle = new Rectangle(new Point(start.X+4, start.Y+pos), new Size(10,10));
                //gx.FillRectangle(new SolidBrush(data.color), colorRectangle);

                StringFormat format = new StringFormat();
                format.LineAlignment = StringAlignment.Center;

                gx.DrawString(legendData[data], new Font("Arial", 9), new SolidBrush(data.UseColor), new Point(start.X + 27, start.Y + pos + 5), format);
                pos += 14;
            }
        }

        /// <summary>
        /// Gets the height required for the legend. Useful for setting the height
        /// of a plot with a legend.
        /// </summary>
        /// <param name="items">The number of items in the legend</param>
        /// <returns>The height required by the legend.</returns>
        public int getRequiredLegendSize(int items)
        {
            //The number 6 comes from a padding of 2pix above and below the legend+2pix for the border+ a padding of 4pix befor e the first entry
            //Each entry is 10 pix tall +4 pix padding
            return 6 + (items * 14);
        }
        #region DrawLine
        /// <summary>
		/// 
		/// </summary>
		/// <param name="gx"></param>
		/// <param name="line"></param>
        private void DrawDataLine(ref Graphics gx, Linedata line)
        {
            Pen LinePen = new Pen(line.UseColor, 1);

            if (line.npts <= 0)
                return;
            Axisdata axDataX = null;
            Axisdata axDataY = null;
            foreach (Axisdata ax in axisArray)
            {
                if (ax.Axistype == 0)
                    axDataX = ax;
                if (ax.Axistype == 1)
                    axDataY = ax;
            }
            // set pen style
            //if linestyle is other than 0 (solid line) then linetype must be zero (no symbols)
            if (line.LineStyle != 0)
            {
                line.LineType = 0;
                if (line.LineStyle == 1)
                    LinePen.DashPattern = new float[] { 3f, 3f, 3f, 3f };
                if (line.LineStyle == 2)
                    LinePen.DashPattern = new float[] { 6f, 3f, 2f, 3f };
                if (line.LineStyle == 3)
                    LinePen.DashPattern = new float[] { 2f, 2f, 2f, 2f };
            }

            xscale = axDataX.seglen / axDataX.deltav;  // pixels per deltav
            yscale = axDataY.seglen / axDataY.deltav;  // pixels per deltav
            float firstx = axDataX.firstv;
            float firsty = axDataY.firstv;
            //Console.WriteLine(" xscale {0}, yscale {1}",xscale,yscale);
            PointF pnt1 = new PointF(0, 0);
            PointF pnt2 = new PointF(0, 0);
            PointF pntlast = new PointF(0, 0);
            pnt1.X = (float)plotarea.left + (line.xarray[0] - firstx) * xscale;
            pnt1.Y = (float)plotarea.bottom + (line.yarray[0] - firsty) * yscale;
            // draw symbol if required
            if (line.LineType != 0)
                Symbol(ref gx, ref LinePen, line.Symbol, pnt1);

            if (float.IsNaN(xscale))
                return;
            if (float.IsNaN(yscale))
                return;

            if (line.LegendType < 0)
            {
                // put it to the right if we can
                int n = Math.Abs(line.LegendType);
                string legend = " " + n.ToString();
                // int fh = (int)tinyfont.GetHeight();
                // int fw = (int)gx.MeasureString(legend, tinyfont).Width;
                int ix = (int)pnt1.X - 14;// +fw;
                PointF pntL = new PointF(0, 0);
                pntL.X = ix;
                pntL.Y = pnt1.Y - 10;
                if (ix > plotarea.left) // put the number
                {
                    SolidBrush brush1 = new SolidBrush(line.UseColor);
                    gx.DrawString(legend, tinyfont, brush1, pntL);
                }
            }
            double bottomBorder = plotarea.bottom + .001;
            double topBorder = plotarea.top - .001;
            
            for (int i = 1; i < line.npts; i++)
            {
                      
                if (Double.IsNaN(line.xarray[i]))
                    continue;
                if (Double.IsNaN(line.yarray[i]))
                    continue;
                pnt2.X = (float)plotarea.left + (line.xarray[i] - firstx) * xscale;
                pnt2.Y = (float)plotarea.bottom + (line.yarray[i] - firsty) * yscale;
                
                //Console.WriteLine(" x {0}, y {1} x {2}, y {3}",line.xarray[i],line.yarray[i],pnt2.X,pnt2.Y);
                // skip until we get on scale
                if ((pnt1.X < plotarea.left) || (pnt1.X > plotarea.right) ||
                     (pnt1.Y > bottomBorder) || (pnt1.Y < topBorder))
                {
                    pnt1 = pnt2;
                    continue;
                }
                if (pnt2.X > plotarea.right)
                    continue;
                if (pnt2.X < plotarea.left)
                    continue;
                if (pnt2.Y > plotarea.bottom)
                    continue;
                if (pnt2.Y < plotarea.top)
                    pnt2.Y = plotarea.top;
                    //continue;
                if (line.LineType >= 0) // 0,line >0,line+symbol, <0,symbol
                    gx.DrawLine(LinePen, pnt1, pnt2);
                if (line.LineType != 0)
                    Symbol(ref gx, ref LinePen, line.Symbol, pnt2);
                pnt1 = pnt2;
            }
            if (line.LegendType > 0)
            {
                // put it to the right if we can
                string legend = " " + line.LegendType.ToString();
                // int fh = (int)tinyfont.GetHeight();
                // int fw = (int)gx.MeasureString(legend, tinyfont).Width;
                int ix = (int)pnt1.X + 2;// +fw;
                if (ix < plotarea.right) // put the number
                {
                    SolidBrush brush1 = new SolidBrush(line.UseColor);
                    pnt1.X += 2;
                    gx.DrawString(legend, tinyfont, brush1, pnt1);
                }
            }
        }

        private void Symbol(ref Graphics gx, ref Pen pen1, int symno, PointF center)
        {
            PointF[] Points;

            Point[] sympts0 = { new Point(0, 2), new Point(2, 2), new Point(2, -2), new Point(-2, -2), new Point(-2, 2), new Point(0, 2) };
            Point[] sympts1 = { new Point(0, 2), new Point(1, 2), new Point(2, 1), new Point(2, -1), new Point(1, -2), new Point(-1, -2), new Point(-2, -1), new Point(-2, 1), new Point(-1, 2), new Point(0, 2) };
            Point[] sympts2 = { new Point(0, 2), new Point(3, -2), new Point(-3, -2), new Point(0, 2) };
            Point[] sympts3 = { new Point(0, -2), new Point(0, 2), new Point(0, 0), new Point(-2, 0), new Point(2, 0) };
            //Point [] sympts4 = {-2,-2, 2,2, 0,0, -2,2, 2,-2};
            //			sympoints [] sympts5 = {0,2, 2,0, 0,-2, -2,-2, 0,2};
            //			sympoints [] sympts6 = {0,2, -2,0, 2,0, 0,2, 0,-2};
            float fact = 1.0f;
            int npts = 0;
            int i;
            //PointF newPoint = new PointF(0,0);
            switch (symno)
            {
                case 0:
                    npts = 6;
                    Points = new PointF[npts];
                    for (i = 0; i < npts; i++)
                    {
                        Points[i].X = center.X + (float)sympts0[i].X * fact;
                        Points[i].Y = center.Y + (float)sympts0[i].Y * fact;
                    }
                    break;
                case 1:
                    npts = 10;
                    Points = new PointF[npts];
                    for (i = 0; i < npts; i++)
                    {
                        Points[i].X = center.X + (float)sympts1[i].X * fact;
                        Points[i].Y = center.Y + (float)sympts1[i].Y * fact;
                    }
                    break;
                case 2:
                    npts = 4;
                    Points = new PointF[npts];
                    for (i = 0; i < npts; i++)
                    {
                        Points[i].X = center.X + (float)sympts2[i].X * fact;
                        Points[i].Y = center.Y + (float)sympts2[i].Y * fact;
                    }
                    break;
                case 3:
                    npts = 5;
                    Points = new PointF[npts];
                    for (i = 0; i < npts; i++)
                    {
                        Points[i].X = center.X + (float)sympts3[i].X * fact;
                        Points[i].Y = center.Y + (float)sympts3[i].Y * fact;
                    }
                    break;
                default:
                    return;
            };
            gx.DrawLines(pen1, Points);
        }
        #endregion
        #endregion

        #region Supporting Classes
    }
    /// <summary>
    /// Summary description for Plotter.
    /// </summary>
    /// 
    struct PlotArea
    {
        public int left;
        public int bottom;
        public int right;
        public int top;
    }

    /// <summary>
    /// Class Axisdata is just a holder for the data to draw the axis
    /// </summary>
    public class Axisdata
    {
        public float firstv;
        public float deltav;
        public float axlen;
        private int axistype;	// 0, x- axis, 1: y-axis(left), 2: y-axis(right) 3: x-axis(top)
        public int Axistype
        {
            get { return axistype; }
            set { axistype = value; }
        }

        public string label;
        public float seglen;	// number of pixels per segment -- used to draw lines
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fv"></param>
        /// <param name="dv"></param>
        /// <param name="nv"></param>
        /// <param name="l"></param>
        /// <param name="type"></param>
        public Axisdata(double fv, double dv, int nv, string l, int type)
        {
            firstv = (float)fv;
            deltav = (float)dv;
            axlen = nv;
            label = l;
            axistype = type;
            seglen = 0.0f;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fv"></param>
        /// <param name="dv"></param>
        /// <param name="nv"></param>
        /// <param name="l"></param>
        public Axisdata(double fv, double dv, int nv, string l)
        {
            firstv = (float)fv;
            deltav = (float)dv;
            axlen = nv;
            label = l;
            axistype = 0;
            seglen = 0.0f;
        }
    }
    /// <summary>
    /// Class Linedata is just a holder for the data to draw the axis
    /// </summary>
    public class Linedata
    {
        private int symbol;
        public int Symbol
        {
            get { return symbol; }
            set { symbol = value; }
        }
        private int lineType;
        public int LineType
        {
            get { return lineType; }
            set { lineType = value; }
        }
        private int legendType;
        public int LegendType
        {
            get { return legendType; }
            set { legendType = value; }
        }
        // change to type Color
        public int color;
        private Color useColor;
        public Color UseColor
        {
            get { return useColor; }
            set { useColor = value; }
        }

        public String label;
        public int npts;

        private int lineStyle; // 0 solid, 1: dash. 2: dash dot (only when no symbols)
        public int LineStyle
        {
            get { return lineStyle; }
            set { lineStyle = value; }
        }

        private int lineIndex;
        public int LineIndex
        {
            get { return lineIndex; }
            set { lineIndex = value; }
        }

        bool isLocalData;
        public float[] xarray;
        public float[] yarray;
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="sym"></param>
        /// <param name="ltype"></param>
        /// <param name="n"></param>
        /// <param name="l"></param>
        //public Linedata(int sym, int ltype, int n, Color colr, string lab, int index)
        //{
        //    npts = n;
        //    xarray = null;
        //    yarray = null;
        //    lineType = ltype;	// // 0 line only <0 draw symbols only, 1 - line and symbols 
        //    symbol = sym;
        //    label = lab;
        //    color = 0;//deprecate
        //    useColor = colr;
        //    lineStyle = 0;
        //    legendType = 0;
        //    lineIndex = index;
        //    if (n > 0)
        //    {
        //        xarray = new double[n];
        //        yarray = new double[n];
        //    }
        //}

        public Linedata(int sym, int ltype, int n, Color colr, string lab, int index, int style)
        {
            npts = n;
            xarray = null;
            yarray = null;
            lineType = ltype;	// // 0 line only <0 draw symbols only, 1 - line and symbols 
            symbol = sym;
            label = lab;
            color = 0; //deprecated
            useColor = colr;
            lineStyle = style;
            legendType = 0;
            lineIndex = index;
            if (n > 0)
            {
                xarray = new float[n];
                yarray = new float[n];
                //isLocalData = true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sym"></param>
        /// <param name="ltype"></param>
        /// <param name="n"></param>
        /// <param name="colr"></param>
        /// <param name="lab"></param>
        /// <param name="index"></param>
        /// <param name="style"></param>
        public Linedata(int sym, int ltype, int n, Color colr, string lab, int index, int style, bool allocate)
        {
            npts = n;
            xarray = null;
            yarray = null;
            lineType = ltype;	// // 0 line only <0 draw symbols only, 1 - line and symbols 
            symbol = sym;
            label = lab;
            color = 0; //deprecated
            useColor = colr;
            lineStyle = style;
            legendType = 0;
            lineIndex = index;
            if (n > 0 & allocate == true)
            {
                xarray = new float[n];
                yarray = new float[n];
                isLocalData = true;
            }
            else
            {
                isLocalData = false;
            }
        }

    }
    #endregion
}

