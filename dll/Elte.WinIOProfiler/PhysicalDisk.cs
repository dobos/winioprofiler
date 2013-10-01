using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Elte.WinIOProfiler
{
    public class PhysicalDisk : Disk
    {
        public uint Index { get; protected set; }
        public string SerialNumber { get; protected set; }
        public string FirmwareRevision { get; protected set; }
        public DiskInterfaceType InterfaceType { get; protected set; }
        public uint BytesPerSector { get; protected set; }
        public DiskMediaType MediaType { get; protected set; }

        public PhysicalDisk()
        {
        }

        public PhysicalDisk(WMI.Win32_DiskDrive dd)
        {
            Index = dd.Index;
            DeviceID = dd.DeviceID;
            Caption = dd.Caption;
            SerialNumber = Util.HexToChar(dd.SerialNumber);
            FirmwareRevision = dd.FirmwareRevision; //Util.HexToChar(dd.FirmwareRevision);
            InterfaceType = Constants.DiskInterfaceTypes[dd.InterfaceType];
            Size = dd.Size;
            BytesPerSector = dd.BytesPerSector;
            MediaType = dd.MediaType == null ? DiskMediaType.Unknown : Constants.DiskMediaTypes[dd.MediaType];
        }

        public override PerformanceCounter GetPerformanceCounter(DiskMetricType metric)
        {
            if (counterInstances == null)
            {
                counterInstances = new Dictionary<uint, string>();

                PerformanceCounterCategory pcc = new PerformanceCounterCategory(Constants.PhysicalDiskCounter);
                foreach (string i in pcc.GetInstanceNames())
                {
                    string[] parts = i.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    uint index;
                    if (uint.TryParse(parts[0], out index))
                    {
                        counterInstances.Add(index, i);
                    }
                }
            }

            return new PerformanceCounter(Constants.PhysicalDiskCounter, Constants.DiskMetricTypes[metric], counterInstances[Index]);
        }

        public static IEnumerable<PhysicalDisk> GetPhysicalDisks()
        {
            foreach (WMI.Win32_DiskDrive dd in WMI.Win32_DiskDrive.Get_Win32_DiskDrives())
            {
                yield return new PhysicalDisk(dd);
            }
        }

        public static PerformanceCounter GetTotalPerformanceCounter(DiskMetricType metric)
        {
            return new PerformanceCounter(Constants.PhysicalDiskCounter, Constants.DiskMetricTypes[metric], Constants.TotalCounter);
        }
    }
}
