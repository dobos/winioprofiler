using System;
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
        public SequentialReadWorker(FileStream stream, int outstanding, long blockSize)
            :base(stream, outstanding, blockSize)
        {

        }

        protected override IAsyncResult BeginIOOperation(FileStream stream, byte[] buffer, AsyncCallback callback, object asyncState)
        {
            return stream.BeginRead(buffer, 0, buffer.Length, callback, asyncState);
        }

        protected override int EndIOOperation(FileStream stream, IAsyncResult ar)
        {
            return stream.EndRead(ar);
        }
    }
}
