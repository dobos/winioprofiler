using System;
using System.Threading;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Elte.WinIOProfiler
{
    [TestClass]
    public class ThreadSchedulerTest
    {
        [TestMethod]
        public void ParallelExecuteTest()
        {
            var sch = new AffineThreadScheduler<int>()
            {
                ThreadCount = 2
            };

            var res = sch.Execute(() => 
            { 
                Thread.SpinWait(1000);
                return 1;
            });

            Assert.AreEqual(2, res.Length);
        }

    }
}
