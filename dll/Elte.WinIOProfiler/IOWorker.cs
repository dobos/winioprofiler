using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace Elte.WinIOProfiler
{
    public abstract class IOWorker
    {
        private FileStream stream;

        protected int outstanding;
        protected long blockSize;

        protected int[] runs;
        protected byte[][] buffers;
        protected AutoResetEvent[] waithandles;
        protected Stopwatch[] watches;

        protected IOWorkerResults[] results;

        public IOWorker(FileStream stream, int outstanding, long blockSize)
        {
            this.stream = stream;
            this.outstanding = outstanding;
            this.blockSize = blockSize;

            InitializeMembers();
        }

        private void InitializeMembers()
        {
            runs = new int[outstanding];

            // Initialize buffers for the outstanding IO ops
            buffers = new byte[outstanding][];
            for (int i = 0; i < buffers.Length; i++)
            {
                try
                {
                    buffers[i] = new byte[blockSize];
                }
                catch (OutOfMemoryException)
                {
                    throw new Exception("Cannot allocate more than " + Environment.WorkingSet);
                }
            }

            waithandles = new AutoResetEvent[outstanding];
            for (int i = 0; i < waithandles.Length; i++)
            {
                waithandles[i] = new AutoResetEvent(true);
            }

            watches = new Stopwatch[outstanding];
            for (int i = 0; i < watches.Length; i++)
            {
                watches[i] = new Stopwatch();
            }

            // Initialize counters
            results = new IOWorkerResults[outstanding];
            for (int i = 0; i < results.Length; i++)
            {
                results[i] = new IOWorkerResults();
            }
        }

        public void Run(int run)
        {
            int rr = run;
            while (rr > 0)
            {
                int slot = WaitHandle.WaitAny(waithandles);

                watches[slot].Reset();
                watches[slot].Start();
                BeginIOOperation(stream, buffers[slot], IOOperationCallback, slot);

                runs[slot] = rr;

                rr--;
            }

            WaitHandle.WaitAll(waithandles);
            for (int i = 0; i < waithandles.Length; i++)
            {
                waithandles[i].Set();
            }
        }

        protected abstract IAsyncResult BeginIOOperation(FileStream stream, byte[] buffer, AsyncCallback callback, object asyncState);
        
        protected abstract int EndIOOperation(FileStream stream, IAsyncResult ar);

        private void IOOperationCallback(IAsyncResult ar)
        {
            int res = EndIOOperation(stream, ar);

            // TODO: check number of bytes read

            int slot = (int)ar.AsyncState;

            watches[slot].Stop();

            // Update counters
            results[slot].Append(watches[slot].Elapsed, res);            

            runs[slot] = 0;
            waithandles[slot].Set();
        }

        public IOWorkerResults GetResults()
        {
            return IOWorkerResults.Merge(results);
        }
    }
}
