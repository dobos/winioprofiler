using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace Elte.WinIOProfiler
{
    static class Native
    {
        public struct GroupAffinity
        {
            UIntPtr Mask;
            UInt16 Group;
            UInt16 Reserved0;
            UInt16 Reserved1;
            UInt16 Reserved2;
        }

        [DllImport(Constants.DllKernel32, SetLastError = true)]
        public static extern Boolean GetNumaHighestNodeNumber(out UInt32 highestNodeNumber);

        [DllImport(Constants.DllKernel32, SetLastError = true)]
        public static extern Boolean GetNumaNodeProcessorMask(byte node, out UInt64 processorMask);

        [DllImport(Constants.DllKernel32, SetLastError = true)]
        public static extern IntPtr GetCurrentThread();

        [DllImport(Constants.DllKernel32, SetLastError = true)]
        public static extern Boolean GetThreadGroupAffinity(IntPtr threadHandle, out GroupAffinity groupAffinity);

        [DllImport(Constants.DllKernel32, SetLastError = true)]
        public static extern bool SetThreadGroupAffinity(IntPtr threadHandle, GroupAffinity groupAffinity, out GroupAffinity previousGroupAffinity);

        [HostProtectionAttribute(SelfAffectingThreading = true)]
        [DllImport(Constants.DllKernel32, SetLastError = true)]
        public static extern UIntPtr SetThreadAffinityMask(IntPtr threadHandle, UIntPtr threadAffinityMask);
    }
}
