﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace Elte.WinIOProfiler
{
    public class SequentialReadWorker : IOWorker
    {
        public SequentialReadWorker(FileStream stream, int iosPerRun, int outstanding, long blockSize)
            :base(stream, iosPerRun, outstanding, blockSize)
        {

        }

        protected override IAsyncResult OnBeginIOOperation(FileStream stream, byte[] buffer, AsyncCallback callback, object asyncState)
        {
            return stream.BeginRead(buffer, 0, buffer.Length, callback, asyncState);
        }

        protected override long OnEndIOOperation(FileStream stream, IAsyncResult ar)
        {
            return stream.EndRead(ar);
        }
    }
}
