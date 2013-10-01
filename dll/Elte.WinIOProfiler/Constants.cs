using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Elte.WinIOProfiler
{
    public class Constants
    {
        public const long KiloByte = 0x400;
        public const long MegaByte = 0x100000;
        public const long GigaByte = 0x40000000;

        public const long BufferSize = 0x10000;     // 64k

        public const string LogicalDiskCounter = "LogicalDisk";
        public const string PhysicalDiskCounter = "PhysicalDisk";
        public const string TotalCounter = "_Total";

        public const string TestFileName = "ioperftest.dat";

        public const int WaitTimeBins = 100;

        public static readonly string[] FileSizeUnits = { "B", "kB", "MB", "GB", "TB", "PB" };
        public static readonly Color[] Colors = { Color.Red, Color.Green, Color.Blue, Color.Purple, Color.DarkBlue, Color.Brown, Color.Black };

        public static readonly Dictionary<string, DiskInterfaceType> DiskInterfaceTypes = new Dictionary<string, DiskInterfaceType>()
        {
            { "HDC", DiskInterfaceType.HDC },
            { "IDE", DiskInterfaceType.IDE },
            { "1394", DiskInterfaceType.IEEE1394 },
            { "SCSI", DiskInterfaceType.SCSI },
            { "USB", DiskInterfaceType.USB },
        };

        public static readonly Dictionary<string, DiskMediaType> DiskMediaTypes = new Dictionary<string, DiskMediaType>()
        {
            { "External hard disk media", DiskMediaType.ExternalHardDisk },
            { "Removable media other than floppy", DiskMediaType.Removable },
            { "Fixed hard disk media", DiskMediaType.FixedHardDisk },
            { "Format is unknown", DiskMediaType.Unknown },

            { "Removable media", DiskMediaType.Removable },
            { "Fixed hard disk", DiskMediaType.FixedHardDisk },
            { "Unknown", DiskMediaType.Unknown },
        };

        public static readonly Dictionary<DiskMetricType, string> DiskMetricTypes = new Dictionary<DiskMetricType, string>()
        {
            { DiskMetricType.ReadBytesPerSec, "Disk Read Bytes/sec" },
            { DiskMetricType.WriteBytesPerSec, "Disk Write Bytes/sec" }
        };
    }
}
