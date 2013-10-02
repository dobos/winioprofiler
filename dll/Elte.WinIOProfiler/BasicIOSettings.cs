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
        private int fileSizeMultiplier;

        private TimeSpan timePerRun;

        /// <summary>
        /// Gets or sets whether IO type is read or write.
        /// </summary>
        public IOType IOType
        {
            get { return ioType; }
            set { ioType = value; }
        }

        /// <summary>
        /// Gets or sets the IO access pattern.
        /// </summary>
        public IOAccessPattern IOAccessPattern
        {
            get { return ioAccessPattern; }
            set { ioAccessPattern = value; }
        }

        /// <summary>
        /// Gets or sets the number of IO operations per run.
        /// </summary>
        public int IOsPerRun
        {
            get { return iosPerRun; }
            set { iosPerRun = value; }
        }

        /// <summary>
        /// Gets or sets the IO buffering method.
        /// </summary>
        public IOBuffering IOBuffering
        {
            get { return ioBuffering; }
            set { ioBuffering = value; }
        }

        /// <summary>
        /// Gets or sets the stripe size for certain IO patterns.
        /// </summary>
        /// <remarks>
        /// Currently not used.
        /// </remarks>
        public int StripeSize
        {
            get { return stripeSize; }
            set { stripeSize = value; }
        }

        /// <summary>
        /// Gets or sets the number of threads to send IO jobs on.
        /// </summary>
        public int Threads
        {
            get { return threads; }
            set { threads = value; }
        }

        /// <summary>
        /// Gets or sets the number of outstanding IO jobs per thread.
        /// </summary>
        public int Outstanding
        {
            get { return outstanding; }
            set { outstanding = value; }
        }

        /// <summary>
        /// Gets or sets the block size of the IO operations.
        /// </summary>
        public uint BlockSize
        {
            get { return blockSize; }
            set { blockSize = value; }
        }

        /// <summary>
        /// Gets or sets the multiplier used to determine test file size.
        /// </summary>
        /// <remarks>
        /// Set this value higher for systems with large cache to avoid
        /// test uncached performance.
        /// </remarks>
        public int FileSizeMultiplier
        {
            get { return fileSizeMultiplier; }
            set { fileSizeMultiplier = value; }
        }

        /// <summary>
        /// Gets or sets the time spent in each IO run.
        /// </summary>
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
            this.fileSizeMultiplier = 4;

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
            this.fileSizeMultiplier = old.fileSizeMultiplier;

            this.timePerRun = old.timePerRun;
        }
    }
}
