using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;

namespace Elte.WinIOProfiler.WMI
{
    public class Win32_Volume : WMIWrapperClass
    {
        public UInt16 Access { get; protected set; }
        public bool Automount { get; protected set; }
        public UInt16 Availability { get; protected set; }
        public UInt64 BlockSize { get; protected set; }
        public bool BootVolume { get; protected set; }
        public UInt64 Capacity { get; protected set; }
        public string Caption { get; protected set; }
        public bool Compressed { get; protected set; }
        public UInt32 ConfigManagerErrorCode { get; protected set; }
        public bool ConfigManagerUserConfig { get; protected set; }
        public string CreationClassName { get; protected set; }
        public string Description { get; protected set; }
        public string DeviceID { get; protected set; }
        public bool DirtyBitSet { get; protected set; }
        public string DriveLetter { get; protected set; }
        public UInt32 DriveType { get; protected set; }
        public bool ErrorCleared { get; protected set; }
        public string ErrorDescription { get; protected set; }
        public string ErrorMethodology { get; protected set; }
        public string FileSystem { get; protected set; }
        public UInt64 FreeSpace { get; protected set; }
        public bool IndexingEnabled { get; protected set; }
        public DateTime InstallDate { get; protected set; }
        public string Label { get; protected set; }
        public UInt32 LastErrorCode { get; protected set; }
        public UInt32 MaximumFileNameLength { get; protected set; }
        public string Name { get; protected set; }
        public UInt64 NumberOfBlocks { get; protected set; }
        public bool PageFilePresent { get; protected set; }
        public string PNPDeviceID { get; protected set; }
        public UInt16[] PowerManagementCapabilities { get; protected set; }
        public bool PowerManagementSupported { get; protected set; }
        public string Purpose { get; protected set; }
        public bool QuotasEnabled { get; protected set; }
        public bool QuotasIncomplete { get; protected set; }
        public bool QuotasRebuilding { get; protected set; }
        public string Status { get; protected set; }
        public UInt16 StatusInfo { get; protected set; }
        public string SystemCreationClassName { get; protected set; }
        public string SystemName { get; protected set; }
        public bool SystemVolume { get; protected set; }
        public UInt32 SerialNumber { get; protected set; }
        public bool SupportsDiskQuotas { get; protected set; }
        public bool SupportsFileBasedCompression { get; protected set; }

        public Win32_Volume(ManagementObject mo)
            : base(mo)
        {
        }

        public static IEnumerable<Win32_Volume> Get_Win32_Volumes()
        {
            ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT * FROM WIN32_Volume");

            foreach (ManagementObject mo in mos.Get())
            {
                yield return new WMI.Win32_Volume(mo);
            }
        }
    }
}
