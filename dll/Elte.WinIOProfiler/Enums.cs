using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elte.WinIOProfiler
{
    public enum IOType
    {
        Read,
        Write
    }

    public enum IOAccessPattern
    {
        Random,
        Sequencial,
        Stripes
    }

    public enum IOBuffering
    {
        Buffered,
        Unbuffered
    }

    public enum DiskInterfaceType
    {
        SCSI,
        HDC,
        IDE,
        USB,
        IEEE1394
    }

    public enum DiskMediaType
    {
        ExternalHardDisk,
        Removable,
        FixedHardDisk,
        Unknown
    }

    public enum DiskMetricType
    {
        ReadBytesPerSec,
        WriteBytesPerSec
    }

    public enum DriveType
    {
        Unknown = 0,
        NoRootDirectory = 1,
        RemovableDisk = 2,
        LocalDisk = 3,
        NetworkDrive = 4,
        CompactDisc = 5,
        RamDisk = 6
    }
}
