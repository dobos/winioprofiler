﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Elte.WinIOProfiler
{
    public class AffineThreadScheduler<TResult>
    {
        private int threadCount;
        private int[][] cpuMask;

        private CountdownEvent countdownEvent;
        private Func<TResult> worker;
        private TResult[] results;
        private Thread[] threads;

        public int ThreadCount
        {
            get { return threadCount; }
            set { threadCount = value; }
        }

        public int[][] CpuMask
        {
            get { return cpuMask; }
            set { cpuMask = value; }
        }

        public AffineThreadScheduler()
        {
            InitializeMembers();
        }

        private void InitializeMembers()
        {
            this.threadCount = 1;
            this.cpuMask = null;

            this.countdownEvent = null;
            this.worker = null;
            this.results = null;
            this.threads = null;
        }

        public TResult[] Execute(Func<TResult> worker)
        {
            this.countdownEvent = new CountdownEvent(threadCount);
            this.worker = worker;
            this.results = new TResult[threadCount];
            this.threads = new Thread[threadCount];

            for (int i = 0; i < threadCount; i++)
            {
                threads[i] = CreateThread(i);
            }

            countdownEvent.Wait();
            countdownEvent.Dispose();

            return results;
        }

        private Thread CreateThread(int i)
        {
            var start = new ParameterizedThreadStart(ThreadWorker);
            var thread = new Thread(start);
            thread.Start(i);
            return thread;
        }

        private void ThreadWorker(object state)
        {
            Thread.BeginThreadAffinity();

            int i = (int)state;
            UIntPtr mask;
            UIntPtr oldMask = UIntPtr.Zero;

            if (cpuMask != null && cpuMask[i] != null && cpuMask[i].Length != 0)
            {
                mask = GetCpuMask(cpuMask[i]);
                oldMask = SetThreadCpuAffinity(mask);
            }

            // Do the actual work
            this.results[i] = this.worker();

            if (oldMask != UIntPtr.Zero)
            {
                SetThreadCpuAffinity(oldMask);
            }

            countdownEvent.Signal();

            Thread.EndThreadAffinity();
        }

        private UIntPtr GetCpuMask(params int[] cpus)
        {
            UInt64 cpuMask = 0;

            for (int i = 0; i < cpus.Length; i ++)
            {
                cpuMask |= 1UL << cpus[i];
            }

            return new UIntPtr(cpuMask);
        }

        private UIntPtr SetThreadCpuAffinity(UIntPtr mask)
        {

            IntPtr threadID = Native.GetCurrentThread();
            UIntPtr lastMask = Native.SetThreadAffinityMask(threadID, mask);

            return lastMask;
        }
    }
}
