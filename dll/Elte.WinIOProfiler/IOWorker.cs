using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace Elte.WinIOProfiler
{
    public abstract class IOWorker : IDisposable
    {
        private class Slot
        {
            public bool Empty;
            public byte[] Buffer;
            public Stopwatch Watch;
            public IOWorkerResults Results;

            public Slot(long blockSize)
            {
                Empty = true;
                Buffer = new byte[blockSize];
                Watch = new Stopwatch();
                Results = new IOWorkerResults();
            }
        }


        private FileStream stream;

        protected int iosPerRun;
        protected int outstanding;
        protected long blockSize;

        protected SemaphoreSlim semaphore;

        private Slot[] slots;

        public IOWorker(FileStream stream, int iosPerRun, int outstanding, long blockSize)
        {
            this.stream = stream;

            this.iosPerRun = iosPerRun;
            this.outstanding = outstanding;
            this.blockSize = blockSize;

            InitializeSlots();
        }

        public void Dispose()
        {
            if (semaphore != null)
            {
                semaphore.Dispose();
                semaphore = null;
            }
        }

        private void InitializeSlots()
        {
            this.semaphore = new SemaphoreSlim(outstanding);

            // Initialize buffers for the outstanding IO ops
            try
            {
                slots = new Slot[outstanding];

                for (int i = 0; i < outstanding; i++)
                {
                    slots[i] = new Slot(blockSize);
                }
            }
            catch (OutOfMemoryException)
            {
                throw new Exception("Cannot allocate more than " + Environment.WorkingSet);
            }
        }

        public void Run(int run)
        {
            int rr = run;

            while (rr > 0)
            {
                semaphore.Wait();

                // find a slot that's not running
                int slot = -1;
                for (int i = 0; i < slots.Length; i++)
                {
                    if (slots[i].Empty)
                    {
                        slot = i;
                    }
                }

                slots[slot].Empty = false;
                slots[slot].Watch.Reset();
                slots[slot].Watch.Start();

                rr--;

                OnBeginIOOperation(stream, slots[slot].Buffer, IOOperationCallback, slot);
            }

            // wait for all other threads
            semaphore.Wait(outstanding);
            semaphore.Release(outstanding);
        }

        protected abstract IAsyncResult OnBeginIOOperation(FileStream stream, byte[] buffer, AsyncCallback callback, object asyncState);

        protected abstract long OnEndIOOperation(FileStream stream, IAsyncResult ar);

        private void IOOperationCallback(IAsyncResult ar)
        {
            long res = OnEndIOOperation(stream, ar);
            int slot = (int)ar.AsyncState;

            // Update counters
            slots[slot].Watch.Stop();
            slots[slot].Results.Append(slots[slot].Watch.Elapsed, res);

            // TODO: check number of bytes read

            slots[slot].Empty = true;
            semaphore.Release();
        }

        public IOWorkerResults GetResults()
        {
            return IOWorkerResults.Merge(slots.Select(s => s.Results));
        }
    }
}
