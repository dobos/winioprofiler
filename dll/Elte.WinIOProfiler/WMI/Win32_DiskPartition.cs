using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;

namespace Elte.WinIOProfiler.WMI
{
    public class Win32_DiskPartition : WMIWrapperClass
    {
        public UInt16 Access { get; protected set; }
        public UInt16 Availability { get; protected set; }
        public UInt64 BlockSize { get; protected set; }
        public bool Bootable { get; protected set; }
        public bool BootPartition { get; protected set; }
        public string Caption { get; protected set; }
        public UInt32 ConfigManagerErrorCode { get; protected set; }
        public bool ConfigManagerUserConfig { get; protected set; }
        public string CreationClassName { get; protected set; }
        public string Description { get; protected set; }
        public string DeviceID { get; protected set; }
        public UInt32 DiskIndex { get; protected set; }
        public bool ErrorCleared { get; protected set; }
        public string ErrorDescription { get; protected set; }
        public string ErrorMethodology { get; protected set; }
        public UInt32 HiddenSectors { get; protected set; }
        public UInt32 Index { get; protected set; }
        public DateTime InstallDate { get; protected set; }
        public UInt32 LastErrorCode { get; protected set; }
        public string Name { get; protected set; }
        public UInt64 NumberOfBlocks { get; protected set; }
        public string PNPDeviceID { get; protected set; }
        public UInt16[] PowerManagementCapabilities { get; protected set; }
        public bool PowerManagementSupported { get; protected set; }
        public bool PrimaryPartition { get; protected set; }
        public string Purpose { get; protected set; }
        public bool RewritePartition { get; protected set; }
        public UInt64 Size { get; protected set; }
        public UInt64 StartingOffset { get; protected set; }
        public string Status { get; protected set; }
        public UInt16 StatusInfo { get; protected set; }
        public string SystemCreationClassName { get; protected set; }
        public string SystemName { get; protected set; }
        public string Type { get; protected set; }

        public Win32_DiskPartition(ManagementObject mo)
            : base(mo)
        {
        }
    }
}
