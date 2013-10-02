﻿using System;
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
        #region DLLimport
        const int FILE_FLAG_NO_BUFFERING = unchecked((int)0x20000000);
        const int FILE_FLAG_OVERLAPPED = unchecked((int)0x40000000);
        const int FILE_FLAG_SEQUENTIAL_SCAN = unchecked((int)0x08000000);

        [DllImport("KERNEL32", SetLastError = true, CharSet = CharSet.Auto, BestFitMapping = false)]
        static extern SafeFileHandle CreateFile(String fileName,
                                                   int desiredAccess,
                                                   System.IO.FileShare shareMode,
                                                   IntPtr securityAttrs,
                                                   System.IO.FileMode creationDisposition,
                                                   int flagsAndAttributes,
                                                   IntPtr templateFile);
        #endregion DLLimport

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
            int flags = FILE_FLAG_NO_BUFFERING;     // default to simmple no buffering
            if (sequential) flags |= FILE_FLAG_SEQUENTIAL_SCAN;
            if (async) flags |= FILE_FLAG_OVERLAPPED;

            SafeFileHandle handle = CreateFile(path, (int)acc, share, IntPtr.Zero, mode, flags, IntPtr.Zero);

            FileStream stream = null;

            if (!handle.IsInvalid)
            {   /* Wrap the handle in a stream and return it to the caller */
                stream = new FileStream(handle, acc, blockSize, async);
            }
            else                    // if create call failed to get a handle
            {                       // return a null pointer. 
                stream = null;
                handle = null;
            }

            return stream;
        }

        public void Run()
        {
            using (FileStream stream = OpenStream(filename, FileMode.Open, FileAccess.Read, FileShare.None, true, true, (int)IOSettings.BlockSize))
            {
                var q = Enumerable.Range(0, IOSettings.Threads).AsParallel().WithDegreeOfParallelism(IOSettings.Threads).Select(i =>
                    {

                        Stopwatch sw = new Stopwatch();
                        sw.Start();

                        IOWorker worker = new SequentialReadWorker(stream, IOSettings.Outstanding, IOSettings.BlockSize);

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

                        return worker.GetResults();

                    });

                results = IOWorkerResults.Merge(q);
            }
        }

        public IOWorkerResults GetResults()
        {
            return results;
        }
    }
}
