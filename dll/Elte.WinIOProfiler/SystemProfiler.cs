using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Elte.WinIOProfiler
{
    public class SystemProfiler
    {
        private BasicIOSettings ioSettings;

        private List<LogicalDisk> logicalDisks;
        private List<IOTest> tests;

        public List<LogicalDisk> LogicalDisks
        {
            get { return logicalDisks; }
        }

        public List<IOTest> Tests
        {
            get { return tests; }
        }

        public SystemProfiler()
        {
            InitializeMembers();
        }

        private void InitializeMembers()
        {
            this.ioSettings = new BasicIOSettings();
            this.logicalDisks = new List<LogicalDisk>();
            this.tests = new List<IOTest>();
        }

        public void Run()
        {
            foreach (var test in tests)
            {
                test.LogicalDisks.Clear();
                test.LogicalDisks.AddRange(logicalDisks);
            }

            foreach (var test in tests)
            {
                test.Run();
            }
        }
    }
}
