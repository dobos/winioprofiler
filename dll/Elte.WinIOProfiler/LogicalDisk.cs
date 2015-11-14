using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Elte.WinIOProfiler
{
    public class LogicalDisk : Disk
    {
        public string Label { get; protected set; }
        public DriveType DriveType { get; protected set; }
        public string FileSystem { get; protected set; }
        public ulong BlockSize { get; protected set; }
        public ulong FreeSpace { get; protected set; }

        public string Path
        {
            get { return Name; }
        }

        public LogicalDisk()
        {
        }

        public LogicalDisk(WMI.Win32_Volume vol)
        {
            DeviceID = vol.DeviceID;
            Caption = vol.Caption;
            Name = vol.Name;
            Label = vol.Label;
            DriveType = (DriveType)vol.DriveType;
            FileSystem = vol.FileSystem;
            Size = vol.Capacity;
            BlockSize = vol.BlockSize;
            FreeSpace = vol.FreeSpace;
        }

        public override PerformanceCounter GetPerformanceCounter(DiskMetricType metric)
        {
            if (DriveType == DriveType.LocalDisk &&
                DeviceID != Name                       // Return mounted volumes only
                )
            {
                // remove trailing backslash
                string iname = Name.TrimEnd('\\');
                return new PerformanceCounter(Constants.LogicalDiskCounter, Constants.DiskMetricTypes[metric], iname);
            }
            else
            {
                return null;
            }
        }

        public static IEnumerable<LogicalDisk> GetLogicalDisks()
        {
            foreach (WMI.Win32_Volume ld in WMI.Win32_Volume.Get_Win32_Volumes())
            {
                yield return new LogicalDisk(ld);
            }
        }

        public static PerformanceCounter GetTotalPerformanceCounter(DiskMetricType metric)
        {
            return new PerformanceCounter(Constants.LogicalDiskCounter, Constants.DiskMetricTypes[metric], Constants.TotalCounter);
        }
    }
}
