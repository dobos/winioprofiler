using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using ZedGraph;

namespace Elte.WinIOProfiler
{
    /// <summary>
    /// 
    /// </summary>
    public class BlockSizeTest : IOTest
    {
        //private static readonly uint[] BlockSizes = { 8192, 16384, 32768, 65536, 131072, 262144, 524288, 1048576 };
        //private static readonly uint[] BlockSizes = { 512, 1024, 2048, 4096, 8192, 16384, 32768, 65536, 131072, 262144, 524288, 1048576 };
        //private static readonly uint[] BlockSizes = { 512, 4096, 65536, 131072, 262144, 1048576, 2097152, 4194304, 8388608 };
        private static readonly uint[] BlockSizes = { 4096, 8192, 16384, 32768, 65536 };
        //private static readonly uint[] BlockSizes = { 65536 };
        private LogicalDiskProfiler[] profilers;

        protected override PerformanceCounter[] InitializeCounters()
        {
            switch (ioSettings.IOType)
            {
                case IOType.Read:
                    return new PerformanceCounter[] { LogicalDisk.GetTotalPerformanceCounter(DiskMetricType.ReadBytesPerSec) };
                case IOType.Write:
                    return new PerformanceCounter[] { LogicalDisk.GetTotalPerformanceCounter(DiskMetricType.WriteBytesPerSec) };
                default:
                    throw new NotImplementedException();
            }
        }

        protected override void InitializeTest()
        {
            // Create a worker for each volume
            profilers = new LogicalDiskProfiler[logicalDisks.Count];

            for (int i = 0; i < logicalDisks.Count; i++)
            {
                profilers[i] = new LogicalDiskProfiler();
                profilers[i].IOSettings = new BasicIOSettings(ioSettings);
                profilers[i].IOSettings.BlockSize = BlockSizes[BlockSizes.Length - 1];

                // Initialize files
                profilers[i].Filename = Path.Combine(logicalDisks[i].Path, Constants.TestFileName);
            }

            // Allocate test files
            profilers.AsParallel().WithDegreeOfParallelism(profilers.Length).ForAll(p =>
            {
                p.AllocateFile();
            });

        }

        protected override void ExecuteTest()
        {
            // Run tests
            for (int i = 0; i < BlockSizes.Length; i++)
            {
                Console.WriteLine("Running test with block size {0}", BlockSizes[i]);

                for (int j = 0; j < profilers.Length; j++)
                {
                    profilers[j].IOSettings.BlockSize = BlockSizes[i];
                }

                // TODO: this here would be used to start parallel loads

                StartCounters();
                profilers.AsParallel().WithDegreeOfParallelism(profilers.Length).ForAll(p => { p.Run(); });
                StopCounters();

                RecordResults(IOWorkerResults.Merge(profilers.Select(p => p.GetResults())), String.Format("Block size {0}", Util.FormatFileSize(BlockSizes[i])));
            }
        }

        protected override void FinalizeTest()
        {

        }

        public override IOTestPlot[] GetPlots()
        {
            return new IOTestPlot[]
            {
                GetThroughputPlot()
            };
        }

        private IOTestPlot GetThroughputPlot()
        {
            var plot = new IOTestPlot()
            {
                Type = PlotType.Throughput,
                Text = this.text,
            };

            switch (ioSettings.IOType)
            {
                case IOType.Read:
                    plot.Color = Color.Blue;
                    break;
                case IOType.Write:
                    plot.Color = Color.Red;
                    break;
                default:
                    throw new NotImplementedException();
            }

            // x axis will show the block sizes
            plot.X = new double[BlockSizes.Length];
            for (int i = 0; i < plot.X.Length; i++)
            {
                plot.X[i] = BlockSizes[i];
            }

            // y axis
            plot.Y = new double[BlockSizes.Length];
            plot.YErrMin = new double[BlockSizes.Length];
            plot.YErrMax = new double[BlockSizes.Length];
            for (int i = 0; i < plot.Y.Length; i++)
            {
                float[][] read = counterReadouts[i];

                double avg = 0;
                double s2 = 0;
                for (int j = 0; j < read.Length; j++)
                {
                    avg += read[j][0];
                    s2 += read[j][0] * read[j][0];
                }
                avg /= read.Length;
                s2 = s2 / read.Length - avg * avg;

                plot.Y[i] = avg;
                plot.YErrMin[i] = avg - Math.Sqrt(s2) / Math.Sqrt(read.Length);
                plot.YErrMax[i] = avg + Math.Sqrt(s2) / Math.Sqrt(read.Length);
            }

            return plot;
        }

        /*
        public void PlotLatencyGraph(Graphics g, RectangleF rect)
        {
            ZedGraph.GraphPane pane = new GraphPane(rect, "IO System Performance", "Block size", "Latency [ms]");

            pane.XAxis.Type = AxisType.Text;

            // labels
            string[] labels = new string[BlockSizes.Length];
            for (int i = 0; i < labels.Length; i++)
            {
                labels[i] = Util.FormatFileSize(BlockSizes[i]);
            }
            pane.XAxis.Scale.TextLabels = labels;

            // x axis will show the block sizes
            double[] x = new double[BlockSizes.Length];
            for (int i = 0; i < x.Length; i++)
            {
                x[i] = BlockSizes[i];
            }

            // y axis
            double[] y = new double[BlockSizes.Length];
            double[] errmin = new double[BlockSizes.Length];
            double[] errmax = new double[BlockSizes.Length];
            for (int i = 0; i < y.Length; i++)
            {
                int[] wt = workerResults[i].WaitTimes;

                double sum = 0;
                double avg = 0;
                double stdev = 0;
                for (int j = 0; j < wt.Length; j++)
                {
                    sum += wt[j];
                    avg += j * wt[j];
                    stdev += j * j * wt[j];
                }
                avg /= sum;
                stdev /= sum;

                y[i] = avg;
                errmin[i] = avg - Math.Sqrt(stdev);
                errmin[i] = avg + Math.Sqrt(stdev);
            }

            pane.AddCurve("", x, y, Color.Red);
            pane.AddErrorBar("", x, errmin, errmax, Color.Red);

            pane.AxisChange();

            pane.Draw(g);
        }
         * */

        /*
        public void PlotThroughputGraph(Graphics g, RectangleF rect)
        {
            ZedGraph.GraphPane pane = new GraphPane(rect, "IO System Performance", "Block size", "Throughtput []");

            pane.XAxis.Type = AxisType.Text;

            // labels
            string[] labels = new string[BlockSizes.Length];
            for (int i = 0; i < labels.Length; i++)
            {
                labels[i] = Util.FormatFileSize(BlockSizes[i]);
            }
            pane.XAxis.Scale.TextLabels = labels;

            // x axis will show the block sizes
            double[] x = new double[BlockSizes.Length];
            for (int i = 0; i < x.Length; i++)
            {
                x[i] = BlockSizes[i];
            }

            // y axis
            double[] y = new double[BlockSizes.Length];
            double[] errmin = new double[BlockSizes.Length];
            double[] errmax = new double[BlockSizes.Length];
            for (int i = 0; i < y.Length; i++)
            {
                float[][] read = counterReadouts[i];

                double avg = 0;
                double s2 = 0;
                for (int j = 0; j < read.Length; j++)
                {
                    avg += read[j][0];
                    s2 += read[j][0] * read[j][0];
                }
                avg /= read.Length;
                s2 = s2 / read.Length - avg * avg;

                y[i] = avg;
                errmin[i] = avg - Math.Sqrt(s2) / Math.Sqrt(read.Length);
                errmax[i] = avg + Math.Sqrt(s2) / Math.Sqrt(read.Length);
            }

            pane.AddCurve("", x, y, Color.Red);
            pane.AddErrorBar("", x, errmin, errmax, Color.Red);

            pane.AxisChange();

            pane.Draw(g);
        }
        */
    }
}
