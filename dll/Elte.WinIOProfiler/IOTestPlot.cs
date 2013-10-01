using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Elte.WinIOProfiler
{
    public delegate void IOTestPlotDelegate(Graphics g, RectangleF rect);

    public class IOTestPlot
    {
        public string Text { get; protected set; }
        public IOTestPlotDelegate DrawingMethod { get; protected set; }

        public IOTestPlot(string text, IOTestPlotDelegate drawingMethod)
        {
            this.Text = text;
            this.DrawingMethod = drawingMethod;
        }
    }
}
