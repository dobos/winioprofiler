using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elte.WinIOProfiler
{
    public class BasicIOSettings
    {
        private IOType ioType;
        private IOAccessPattern ioAccessPattern;
        private int iosPerRun;
        private IOBuffering ioBuffering;
        private int stripeSize;

        private int threads;
        private int outstanding;
        private uint blockSize;

        private TimeSpan timePerRun;

        public IOType IOType
        {
            get { return ioType; }
            set { ioType = value; }
        }

        public IOAccessPattern IOAccessPattern
        {
            get { return ioAccessPattern; }
            set { ioAccessPattern = value; }
        }

        public int IOsPerRun
        {
            get { return iosPerRun; }
            set { iosPerRun = value; }
        }

        public IOBuffering IOBuffering
        {
            get { return ioBuffering; }
            set { ioBuffering = value; }
        }

        public int StripeSize
        {
            get { return stripeSize; }
            set { stripeSize = value; }
        }

        public int Threads
        {
            get { return threads; }
            set { threads = value; }
        }

        public int Outstanding
        {
            get { return outstanding; }
            set { outstanding = value; }
        }

        public uint BlockSize
        {
            get { return blockSize; }
            set { blockSize = value; }
        }

        public TimeSpan TimePerRun
        {
            get { return timePerRun; }
            set { timePerRun = value; }
        }


        public BasicIOSettings()
        {
            InitializeMembers();
        }

        public BasicIOSettings(BasicIOSettings old)
        {
            CopyMembers(old);
        }

        private void InitializeMembers()
        {
            this.ioType = IOType.Read;
            this.ioAccessPattern = IOAccessPattern.Sequencial;
            this.iosPerRun = 64;
            this.ioBuffering = IOBuffering.Unbuffered;
            this.stripeSize = 64;

            this.threads = 1;
            this.outstanding = 8;
            this.blockSize = 64;

            this.timePerRun = new TimeSpan(0, 0, 20);
        }

        private void CopyMembers(BasicIOSettings old)
        {
            this.ioType = old.ioType;
            this.ioAccessPattern = old.ioAccessPattern;
            this.iosPerRun = old.iosPerRun;
            this.ioBuffering = old.ioBuffering;
            this.stripeSize = old.stripeSize;

            this.threads = old.threads;
            this.outstanding = old.outstanding;
            this.blockSize = old.blockSize;

            this.timePerRun = old.timePerRun;
        }
    }
}
