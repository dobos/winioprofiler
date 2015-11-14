using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elte.WinIOProfiler
{
    public class IOWorkerResults
    {
        public string Text { get; set; }
        public TimeSpan Runtime { get; protected set; }
        public long TotalBytes { get; protected set; }
        public int[] WaitTimes { get; protected set; }

        public IOWorkerResults()
        {
            WaitTimes = new int[Constants.WaitTimeBins];
        }

        public void Append(TimeSpan runtime, long bytes)
        {
            Runtime.Add(runtime);
            TotalBytes += bytes;

            int wt = Math.Min((int)runtime.TotalMilliseconds, WaitTimes.Length - 1);
            WaitTimes[wt]++;
        }

        protected void Merge(IOWorkerResults other)
        {
            this.Runtime.Add(other.Runtime);
            this.TotalBytes += other.TotalBytes;

            for (int i = 0; i < WaitTimes.Length; i++)
            {
                this.WaitTimes[i] += other.WaitTimes[i];
            }
        }

        public static IOWorkerResults Merge(IEnumerable<IOWorkerResults> results)
        {
            IOWorkerResults res = new IOWorkerResults();
            foreach (IOWorkerResults r in results)
            {
                res.Merge(r);
            }

            return res;
        }
    }
}
