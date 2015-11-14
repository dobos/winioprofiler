using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;

namespace Elte.WinIOProfiler.WMI
{
    public class Win32_MountPoint : WMIWrapperClass
    {
        public string Directory { get; protected set; }
        public string Volume { get; protected set; }

        public Win32_MountPoint(ManagementObject mo)
            : base(mo)
        {
        }
    }
}
