using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Elte.WinIOProfiler
{
    public class IOTestPlot
    {
        public PlotType Type { get; set; }
        public string Text { get; set; }
        public Color Color { get; set; }
        public double[] X { get; set; }
        public double[] Y { get; set; }
        public double[] YErrMin { get; set; }
        public double[] YErrMax { get; set; }
    }
}
