using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;

namespace Elte.WinIOProfiler.WMI
{
    public abstract class CIM_LogicalElement : CIM_ManagedSystemElement
    {
        public CIM_LogicalElement(ManagementObject mo)
            : base(mo)
        {
        }
    }
}
