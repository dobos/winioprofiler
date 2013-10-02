using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Drawing;
using ZedGraph;

namespace Elte.WinIOProfiler
{
    /// <summary>
    /// Implements basic functionality for IO tests.
    /// </summary>
    public abstract class IOTest
    {
        protected string text;

        protected BasicIOSettings ioSettings;

        protected List<LogicalDisk> logicalDisks;
        protected List<PerformanceCounter> counters;

        private Task<float[][]> counterWorker;
        private CancellationTokenSource counterStop;

        protected List<float[][]> counterReadouts;
        protected List<IOWorkerResults> workerResults;

        protected bool hasResults;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public BasicIOSettings IOSettings
        {
            get { return ioSettings; }
            set { ioSettings = value; }
        }

        /// <summary>
        /// Gets the list of logical disk volumes to be included in the test.
        /// </summary>
        public List<LogicalDisk> LogicalDisks
        {
            get { return logicalDisks; }
        }

        /// <summary>
        /// Gets the value indicating whether this test has results.
        /// </summary>
        public bool HasResults
        {
            get { return hasResults; }
        }

        public IOTest()
        {
            InitializeMembers();
        }

        private void InitializeMembers()
        {
            this.text = "undefined test";
            this.ioSettings = new BasicIOSettings();

            this.logicalDisks = new List<LogicalDisk>();
            this.counters = new List<PerformanceCounter>();

            this.hasResults = false;
        }

        /// <summary>
        /// Runs the test.
        /// </summary>
        public void Run()
        {
            counters = new List<PerformanceCounter>(InitializeCounters());
            counterReadouts = new List<float[][]>();
            workerResults = new List<IOWorkerResults>();

            InitializeTest();

            ExecuteTest();

            FinalizeTest();

            DisposeCounters();

            hasResults = true;
        }

        /// <summary>
        /// When inherited in derived classes, initializes performance counters
        /// necessary to evaluate the test.
        /// </summary>
        /// <returns></returns>
        protected abstract PerformanceCounter[] InitializeCounters();

        /// <summary>
        /// Starts all counters.
        /// </summary>
        protected void StartCounters()
        {
            counterStop = new CancellationTokenSource();
            var ct = counterStop.Token;

            counterWorker = Task<float[][]>.Factory.StartNew(() => CounterWorker(ct), ct);
        }

        /// <summary>
        /// Stops all counters.
        /// </summary>
        protected void StopCounters()
        {
            counterStop.Cancel();
            counterWorker.Wait();

            counterReadouts.Add(counterWorker.Result);
        }

        /// <summary>
        /// Periodically reads and stores counter values.
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        protected float[][] CounterWorker(CancellationToken ct)
        {
            var readouts = new List<float[]>();

            while (!ct.IsCancellationRequested)
            {
                var ro = new float[counters.Count];
                for (int i = 0; i < counters.Count; i++)
                {
                    ro[i] = counters[i].NextValue();
                }

                readouts.Add(ro);

                Thread.Sleep(1000); //*** TODO: implement read-out period as parameter
            }

            return readouts.ToArray();
        }

        /// <summary>
        /// Dispose all performance counters.
        /// </summary>
        private void DisposeCounters()
        {
            foreach (var pc in counters)
            {
                pc.Dispose();
            }

            counters.Clear();
        }

        /// <summary>
        /// Adds the results of the current test to the collection of
        /// all results.
        /// </summary>
        /// <param name="results"></param>
        /// <param name="text"></param>
        protected void RecordResults(IOWorkerResults results, string text)
        {
            results.Text = text;
            workerResults.Add(results);
        }

        /// <summary>
        /// When overriden in an inherited class, initializes the IO test.
        /// </summary>
        protected abstract void InitializeTest();

        /// <summary>
        /// When overriden in an inherited class, executes the IO test.
        /// </summary>
        protected abstract void ExecuteTest();

        /// <summary>
        /// When overriden in an inherited class, finalizes the IO test.
        /// </summary>
        protected abstract void FinalizeTest();

        /// <summary>
        /// When overriden in an inherited class, returns the generated plots.
        /// </summary>
        /// <returns></returns>
        public abstract IOTestPlot[] GetPlots();

    }
}
