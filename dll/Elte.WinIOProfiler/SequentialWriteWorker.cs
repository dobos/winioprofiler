using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace Elte.WinIOProfiler
{
    public class SequentialWriteWorker : IOWorker
    {
        public SequentialWriteWorker(FileStream stream, int outstanding, long blockSize)
            :base(stream, outstanding, blockSize)
        {

        }

        protected override IAsyncResult BeginIOOperation(FileStream stream, byte[] buffer, AsyncCallback callback, object asyncState)
        {
            return stream.BeginWrite(buffer, 0, buffer.Length, callback, asyncState);
        }

        protected override int EndIOOperation(FileStream stream, IAsyncResult ar)
        {
            // EndWrite doesn't return number of bytes written so we
            // assume the entire buffer has been written if no error occured.
            stream.EndWrite(ar);
            return buffers.Length;
        }
    }
}
