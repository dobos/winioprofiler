using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;

namespace Elte.WinIOProfiler.WMI
{
    public class Win32_LogicalDisk : WMIWrapperClass
    {
        public UInt16 Access { get; protected set; }
        public UInt16 Availability { get; protected set; }
        public UInt64 BlockSize { get; protected set; }
        public string Caption { get; protected set; }
        public bool Compressed { get; protected set; }
        public UInt32 ConfigManagerErrorCode { get; protected set; }
        public bool ConfigManagerUserConfig { get; protected set; }
        public string CreationClassName { get; protected set; }
        public string Description { get; protected set; }
        public string DeviceID { get; protected set; }
        public UInt32 DriveType { get; protected set; }
        public bool ErrorCleared { get; protected set; }
        public string ErrorDescription { get; protected set; }
        public string ErrorMethodology { get; protected set; }
        public string FileSystem { get; protected set; }
        public UInt64 FreeSpace { get; protected set; }
        public DateTime InstallDate { get; protected set; }
        public UInt32 LastErrorCode { get; protected set; }
        public UInt32 MaximumComponentLength { get; protected set; }
        public UInt32 MediaType { get; protected set; }
        public string Name { get; protected set; }
        public UInt64 NumberOfBlocks { get; protected set; }
        public string PNPDeviceID { get; protected set; }
        public UInt16[] PowerManagementCapabilities { get; protected set; }
        public bool PowerManagementSupported { get; protected set; }
        public string ProviderName { get; protected set; }
        public string Purpose { get; protected set; }
        public bool QuotasDisabled { get; protected set; }
        public bool QuotasIncomplete { get; protected set; }
        public bool QuotasRebuilding { get; protected set; }
        public UInt64 Size { get; protected set; }
        public string Status { get; protected set; }
        public UInt16 StatusInfo { get; protected set; }
        public bool SupportsDiskQuotas { get; protected set; }
        public bool SupportsFileBasedCompression { get; protected set; }
        public string SystemCreationClassName { get; protected set; }
        public string SystemName { get; protected set; }
        public bool VolumeDirty { get; protected set; }
        public string VolumeName { get; protected set; }
        public string VolumeSerialNumber { get; protected set; }

        public Win32_LogicalDisk(ManagementObject mo)
            : base(mo)
        {
        }

        public static IEnumerable<Win32_LogicalDisk> Get_Win32_LogicalDisks()
        {
            ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT * FROM WIN32_LogicalDisk");

            foreach (ManagementObject mo in mos.Get())
            {
                yield return new WMI.Win32_LogicalDisk(mo);
            }
        }
    }
}
