using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Management;
using System.Diagnostics;

namespace IOProfiler
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.InstalledUICulture;

            System.Windows.Forms.Application.Run(new Main());

            /*SystemProfiler prof = new SystemProfiler();

            List<LogicalDisk> dd = new List<LogicalDisk>(LogicalDisk.GetLogicalDisks());
            /*foreach (LogicalDisk d in dd)
            {
                Console.WriteLine(d.Path);
            }*/
            
            /*prof.LogicalDisks.Add(dd[5]);

            prof.Tests.Add(new BlockSizeTest());

            prof.Run();

            Console.WriteLine("Done.");*/
            //Console.ReadLine();
        }

        static void VolumeEnumerateTest()
        {
            /*foreach (PhysicalDisk dd in PhysicalDisk.GetPhysicalDisks())
            {
                Console.WriteLine(dd.Caption);
                Console.WriteLine(dd.MediaType);
                Console.WriteLine(dd.InterfaceType);

                using (PerformanceCounter pc = dd.GetPerformanceCounter(DiskMetricType.ReadBytesPerSec))
                {
                    Console.WriteLine("Izé: {0}", pc.RawValue);
                }
            }*/

            foreach (LogicalDisk dd in LogicalDisk.GetLogicalDisks())
            {
                Console.WriteLine(dd.Name);
                Console.WriteLine(dd.FileSystem);
                Console.WriteLine(dd.DriveType);

                using (PerformanceCounter pc = dd.GetPerformanceCounter(DiskMetricType.ReadBytesPerSec))
                {
                    if (pc != null)
                    {
                        Console.WriteLine("Izé: {0}", pc.RawValue);
                    }
                }
            }
        }

        static void PerformanceCounterTest()
        {
            /*foreach (PerformanceCounterCategory pcc in PerformanceCounterCategory.GetCategories().OrderBy(i => i.CategoryName))
            {
                Console.WriteLine(pcc.CategoryName);
            }*/

            /*PerformanceCounterCategory pcc = new PerformanceCounterCategory("Processor");

            Console.WriteLine(pcc.CategoryName);
            Console.WriteLine(pcc.CategoryType);

            foreach (string i in pcc.GetInstanceNames())
            {
                Console.WriteLine(i);
            }

            foreach (PerformanceCounter pc in pcc.GetCounters("_Total"))
            {
                Console.WriteLine(pc.CounterName);
            }*/

            //PerformanceCounter pc = new PerformanceCounter("LogicalDisk", "Disk Bytes/sec", "_Total");
            /*PerformanceCounter pc = new PerformanceCounter("Processor", "% Processor Time", "_Total");

            Console.WriteLine(pc.InstanceName);

            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine(pc.NextValue());
                System.Threading.Thread.Sleep(1000);
            }*/
        }

        static void SingleVolumeTest()
        {
            LogicalDiskProfiler pr = new LogicalDiskProfiler();

            pr.Filename = @"F:\test.dat";
            //pr.FileSize = 20 * Constants.MegaByte;

            pr.AllocateFile();

            pr.Run();
        }

        static void WMITests()
        {
            Console.WriteLine("Drives:");
            foreach (WMI.Win32_DiskDrive dd in EnumerateDiskDrives())
            {
                Console.WriteLine(dd.Caption);
                Console.WriteLine(dd.Index);
                //Console.WriteLine(dd.DeviceID);
                //Console.WriteLine(dd.InterfaceType);
                //Console.WriteLine(dd.Partitions);

            }


            Console.WriteLine("Volumes:");
            foreach (WMI.Win32_Volume vv in EnumerateVolumes())
            {
                Console.WriteLine(vv.Caption);

            }

            Console.WriteLine("Logical disks:");
            foreach (WMI.Win32_LogicalDisk ld in EnumerateLogicalDisks())
            {
                Console.WriteLine(ld.Caption);

            }

            Console.WriteLine("Partitions:");
            foreach (WMI.Win32_DiskPartition pp in EnumeratePartitions())
            {
                Console.WriteLine(pp.Caption);
                Console.WriteLine(pp.DiskIndex);

            }

            Console.WriteLine("MountPoints:");
            foreach (WMI.Win32_MountPoint mp in EnumerateMountPoints())
            {
                Console.WriteLine(mp.Volume);
                Console.WriteLine(mp.Directory);

            }
        }

        static IEnumerable<WMI.CIM_DiskDrive> EnumerateDiskDrives()
        {
            ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT * FROM WIN32_DiskDrive");

            foreach (ManagementObject mo in mos.Get())
            {
                yield return new WMI.Win32_DiskDrive(mo);
            }
        }

        static IEnumerable<WMI.Win32_Volume> EnumerateVolumes()
        {
            ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT * FROM WIN32_Volume");

            foreach (ManagementObject mo in mos.Get())
            {
                yield return new WMI.Win32_Volume(mo);
            }
        }

        static IEnumerable<WMI.Win32_LogicalDisk> EnumerateLogicalDisks()
        {
            ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT * FROM WIN32_LogicalDisk");

            foreach (ManagementObject mo in mos.Get())
            {
                yield return new WMI.Win32_LogicalDisk(mo);
            }
        }

        static IEnumerable<WMI.Win32_DiskPartition> EnumeratePartitions()
        {
            ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT * FROM WIN32_DiskPartition");

            foreach (ManagementObject mo in mos.Get())
            {
                yield return new WMI.Win32_DiskPartition(mo);
            }
        }

        static IEnumerable<WMI.Win32_MountPoint> EnumerateMountPoints()
        {
            ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT * FROM WIN32_MountPoint");

            foreach (ManagementObject mo in mos.Get())
            {
                yield return new WMI.Win32_MountPoint(mo);
            }
        }
    }
}
