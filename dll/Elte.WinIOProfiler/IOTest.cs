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
    public abstract class IOTest
    {
        protected BasicIOSettings ioSettings;

        protected List<LogicalDisk> logicalDisks;
        protected List<PerformanceCounter> counters;

        private Task<float[][]> counterWorker;
        private CancellationTokenSource counterStop;

        protected List<float[][]> counterReadouts;
        protected List<IOWorkerResults> workerResults;

        protected bool hasResults;

        public BasicIOSettings IOSettings
        {
            get { return ioSettings; }
            set { ioSettings = value; }
        }

        public List<LogicalDisk> LogicalDisks
        {
            get { return logicalDisks; }
        }

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
            this.ioSettings = new BasicIOSettings();

            this.logicalDisks = new List<LogicalDisk>();
            this.counters = new List<PerformanceCounter>();

            this.hasResults = false;
        }

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

        protected abstract PerformanceCounter[] InitializeCounters();

        protected void StartCounters()
        {
            counterStop = new CancellationTokenSource();
            CancellationToken ct = counterStop.Token;

            counterWorker = Task<float[][]>.Factory.StartNew(() => CounterWorker(ct), ct);
        }

        protected void StopCounters()
        {
            counterStop.Cancel();
            counterWorker.Wait();

            counterReadouts.Add(counterWorker.Result);
        }

        protected float[][] CounterWorker(CancellationToken ct)
        {
            List<float[]> readouts = new List<float[]>();

            while (!ct.IsCancellationRequested)
            {
                float[] ro = new float[counters.Count];
                for (int i = 0; i < counters.Count; i++)
                {
                    ro[i] = counters[i].NextValue();
                }

                readouts.Add(ro);

                Thread.Sleep(1000); //***
            }

            return readouts.ToArray();
        }

        protected void DisposeCounters()
        {
            foreach (PerformanceCounter pc in counters)
            {
                pc.Dispose();
            }

            counters.Clear();
        }

        protected void RecordResults(IOWorkerResults results, string text)
        {
            results.Text = text;
            workerResults.Add(results);
        }

        protected abstract void InitializeTest();

        protected abstract void ExecuteTest();

        protected abstract void FinalizeTest();

        public abstract IOTestPlot[] GetPlots();

    }
}
