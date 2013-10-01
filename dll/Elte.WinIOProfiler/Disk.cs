using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Elte.WinIOProfiler
{
    public abstract class Disk
    {
        protected static Dictionary<uint, string> counterInstances = null;

        public string DeviceID { get; protected set; }
        public string Name { get; protected set; }
        public string Caption { get; protected set; }
        public ulong Size { get; protected set; }

        public abstract PerformanceCounter GetPerformanceCounter(DiskMetricType metric);
    }
}
