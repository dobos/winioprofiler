using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;

namespace Elte.WinIOProfiler
{
    public class LogicalDiskProfiler
    {
        protected BasicIOSettings ioSettings;

        protected string filename;
        protected long fileSize;

        protected IOWorkerResults results;

        public BasicIOSettings IOSettings
        {
            get { return ioSettings; }
            set { ioSettings = value; }
        }

        public string Filename
        {
            get { return filename; }
            set { filename = value; }
        }

        public long FileSize
        {
            get { return fileSize; }
            set { fileSize = value; }
        }

        public LogicalDiskProfiler()
        {
            InitializeMembers();
        }

        private void InitializeMembers()
        {
            this.ioSettings = new BasicIOSettings();

            this.filename = String.Empty;
            this.fileSize = GetMinimumFileSize();
        }

        protected long GetFileSizePerThread()
        {
            return ioSettings.Outstanding * ioSettings.IOsPerRun * ioSettings.BlockSize * ioSettings.FileSizeMultiplier;
        }

        protected long GetMinimumFileSize()
        {
            return ioSettings.Threads * GetFileSizePerThread();
        }

        public void AllocateFile()
        {
            long fs = GetMinimumFileSize();

            if (fs > this.fileSize)
            {
                this.fileSize = fs;
            }
            else
            {
                fs = this.fileSize;
            }

            using (FileStream outfile = File.Open(filename, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
            {
                outfile.Seek(0, SeekOrigin.End);

                byte[] buffer = new byte[Constants.BufferSize];

                long btw = fs - outfile.Length;

                if (btw > 0)
                {
                    Console.WriteLine("Expanding file to {0} bytes.", outfile.Length);
                }
                else
                {
                    Console.WriteLine("Using already allocated file of {0} bytes.", outfile.Length);
                }

                while (btw > 0)
                {
                    int bb = (int)Math.Min(btw, buffer.Length);
                    outfile.Write(buffer, 0, bb);

                    btw -= bb;
                }

            }
        }

        public void DeleteFile()
        {
            File.Delete(filename);
        }

        protected FileStream OpenStream(string path, FileMode mode, FileAccess acc, FileShare share, bool sequential, bool async, int blockSize)
        {
            int flags = Native.FILE_FLAG_NO_BUFFERING;     // default to simmple no buffering
            if (sequential) flags |= Native.FILE_FLAG_SEQUENTIAL_SCAN;
            if (async) flags |= Native.FILE_FLAG_OVERLAPPED;

            SafeFileHandle handle = Native.CreateFile(path, (int)acc, share, IntPtr.Zero, mode, flags, IntPtr.Zero);

            FileStream stream = null;

            if (!handle.IsInvalid)
            {   
                stream = new FileStream(handle, acc, blockSize, async);
            }
            else                    
            { 
                stream = null;
                handle = null;
            }

            return stream;
        }

        public void Run()
        {
            FileAccess access;

            switch (ioSettings.IOType)
            {
                case IOType.Read:
                    access = FileAccess.Read;
                    break;
                case IOType.Write:
                    access = FileAccess.Write;
                    break;
                default:
                    throw new NotImplementedException();
            }

            using (FileStream stream = OpenStream(filename, FileMode.Open, access, FileShare.None, true, true, (int)IOSettings.BlockSize))
            {
                var sch = new AffineThreadScheduler<IOWorkerResults>()
                {
                    ThreadCount = ioSettings.Threads,
                    CpuMask = ioSettings.CpuMask,
                };
                var res = sch.Execute(WorkerThread, stream);
                this.results = IOWorkerResults.Merge(res);
            }
        }

        private IOWorkerResults WorkerThread(object state)
        {
            var stream = (FileStream)state;
            var sw = new Stopwatch();
            sw.Start();

            IOWorker worker;
            switch (ioSettings.IOType)
            {
                case IOType.Read:
                    worker = new SequentialReadWorker(stream, IOSettings.IOsPerRun, IOSettings.Outstanding, IOSettings.BlockSize);
                    break;
                case IOType.Write:
                    worker = new SequentialWriteWorker(stream, IOSettings.IOsPerRun, IOSettings.Outstanding, IOSettings.BlockSize);
                    break;
                default:
                    throw new NotImplementedException();
            }


            while (sw.ElapsedMilliseconds < ioSettings.TimePerRun.TotalMilliseconds)
            {
                // reposition stream to the beginning if rest of file is less than the minimum required
                if (stream.Length - stream.Position < GetMinimumFileSize())
                {
                    stream.Seek(0, SeekOrigin.Begin);
                }

                // Run on a single thread
                worker.Run(ioSettings.IOsPerRun / IOSettings.Threads);
            }

            sw.Stop();

            var res = worker.GetResults();
            worker.Dispose();

            return res;
        }

        public IOWorkerResults GetResults()
        {
            return results;
        }
    }
}
