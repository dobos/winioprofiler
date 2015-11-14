using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Reflection;

namespace Elte.WinIOProfiler.WMI
{
    public abstract class WMIWrapperClass
    {
        public WMIWrapperClass(ManagementObject mo)
        {
            Type t = this.GetType();

            foreach (PropertyData p in mo.Properties)
            {
                PropertyInfo pi = t.GetProperty(p.Name, BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.NonPublic );

                if (!pi.PropertyType.IsValueType || p.Value != null)
                {
                    pi.SetValue(this, p.Value, null);
                }
            }

        }
    }
}
