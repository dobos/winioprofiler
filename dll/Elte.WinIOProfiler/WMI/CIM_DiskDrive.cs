﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;

namespace Elte.WinIOProfiler.WMI
{
    public abstract class CIM_DiskDrive : CIM_ManagedSystemElement
    {
        public UInt16 Availability { get; protected set; }
        public UInt16[] Capabilities { get; protected set; }
        public string[] CapabilityDescriptions { get; protected set; }
        public string CompressionMethod { get; protected set; }
        public UInt32 ConfigManagerErrorCode { get; protected set; }
        public Boolean ConfigManagerUserConfig { get; protected set; }
        public string CreationClassName { get; protected set; }
        public UInt64 DefaultBlockSize { get; protected set; }
        public string DeviceID { get; protected set; }
        public bool ErrorCleared { get; protected set; }
        public string ErrorDescription { get; protected set; }
        public string ErrorMethodology { get; protected set; }
        public UInt32 LastErrorCode { get; protected set; }
        public UInt64 MaxBlockSize { get; protected set; }
        public UInt64 MaxMediaSize { get; protected set; }
        public UInt64 MinBlockSize { get; protected set; }
        public bool NeedsCleaning { get; protected set; }
        public UInt32 NumberOfMediaSupported { get; protected set; }
        public string PNPDeviceID { get; protected set; }
        public UInt16 PowerManagementCapabilities { get; protected set; }
        public bool PowerManagementSupported { get; protected set; }
        public UInt16 StatusInfo { get; protected set; }
        public string SystemCreationClassName { get; protected set; }
        public string SystemName { get; protected set; }

        public CIM_DiskDrive(ManagementObject mo)
            : base(mo)
        {
        }
    }
}
