using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;

namespace Elte.WinIOProfiler.WMI
{
    public abstract class CIM_ManagedSystemElement : WMIWrapperClass
    {
        public string Caption { get; protected set; }
        public string Description { get; protected set; }
        public UInt16 HealthState { get; protected set; }
        public DateTime InstallDate { get; protected set; }
        public string Name { get; protected set; }
        public string Status { get; protected set; }

        public CIM_ManagedSystemElement(ManagementObject mo)
            :base(mo)
        {
        }
    }
}
