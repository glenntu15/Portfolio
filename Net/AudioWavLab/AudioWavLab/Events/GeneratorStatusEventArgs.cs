using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioWavLab.Events
{
    public class GeneratorStatusEventArgs : EventArgs
    {
        public int npts;
        public GeneratorStatusEventArgs(int n)
        {
            npts = n;
        }
    }
}
