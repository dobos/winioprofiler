using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;

namespace Elte.WinIOProfiler.WMI
{
    public class Win32_DiskDrive : CIM_DiskDrive
    {
        public UInt32 BytesPerSector { get; protected set; }
        public string FirmwareRevision { get; protected set; }
        public UInt32 Index { get; protected set; }
        public string InterfaceType { get; protected set; }
        public string Manufacturer { get; protected set; }
        public bool MediaLoaded { get; protected set; }
        public string MediaType { get; protected set; }
        public string Model { get; protected set; }
        public UInt32 Partitions { get; protected set; }
        public UInt32 SCSIBus { get; protected set; }
        public UInt16 SCSILogicalUnit { get; protected set; }
        public UInt16 SCSIPort { get; protected set; }
        public UInt16 SCSITargetId { get; protected set; }
        public UInt32 SectorsPerTrack { get; protected set; }
        public string SerialNumber { get; protected set; }
        public UInt32 Signature { get; protected set; }
        public UInt64 Size { get; protected set; }
        public UInt64 TotalCylinders { get; protected set; }
        public UInt32 TotalHeads { get; protected set; }
        public UInt64 TotalSectors { get; protected set; }
        public UInt64 TotalTracks { get; protected set; }
        public UInt32 TracksPerCylinder { get; protected set; }

        public Win32_DiskDrive(ManagementObject mo)
            : base(mo)
        {
        }

        public static IEnumerable<Win32_DiskDrive> Get_Win32_DiskDrives()
        {
            ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");

            foreach (ManagementObject mo in mos.Get())
            {
                yield return new WMI.Win32_DiskDrive(mo);
            }
        }
    }
}
