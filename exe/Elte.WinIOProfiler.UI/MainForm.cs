using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Elte.WinIOProfiler;
using ZedGraph;

namespace IOProfilerUI
{
    public partial class MainForm : Form
    {
        private IOTest selectedTest;

        public MainForm()
        {
            InitializeComponent();
            InitializeMembers();
        }

        private void InitializeMembers()
        {
            this.selectedTest = null;
        }

        private void RefreshVolumesList()
        {
            listVolumes.Items.Clear();

            foreach (var d in LogicalDisk.GetLogicalDisks())
            {
                var li = new ListViewItem(d.Caption);
                li.SubItems.Add(d.FileSystem);
                li.SubItems.Add(Util.FormatFileSize(d.Size));
                li.Tag = d;

                listVolumes.Items.Add(li);
            }
        }

        private void AddTest(IOTest test)
        {
            var li = new ListViewItem(test.Text);
            li.Tag = test;
            listTests.Items.Add(li);
        }

        private void ExecuteTests()
        {
            this.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;

            SystemProfiler profiler = new SystemProfiler();

            foreach (ListViewItem li in listVolumes.CheckedItems)
            {
                profiler.LogicalDisks.Add((LogicalDisk)li.Tag);
            }

            foreach (ListViewItem li in listTests.CheckedItems)
            {
                profiler.Tests.Add((IOTest)li.Tag);
            }

            profiler.Run();


            Cursor.Current = Cursors.Default;
            this.Enabled = true;
        }

        private void toolStripVolumes_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == toolButtonRefreshVolumes)
            {
                RefreshVolumesList();
            }
        }

        private void toolStripTests_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == toolButtonSequentialReadBlockSizeTest)
            {
                var test = new BlockSizeTest();
                test.Text = "SequentialReadBlockSizeTest";
                test.IOSettings.IOType = IOType.Read;
                AddTest(test);
            }
            else if (e.ClickedItem == toolButtonSequentialWriteBlockSizeTest)
            {
                var test = new BlockSizeTest();
                test.Text = "SequentialWriteBlockSizeTest";
                test.IOSettings.IOType = IOType.Write;
                AddTest(test);
            }
        }

        private void toolStripMain_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == toolButtonExecute)
            {
                ExecuteTests();
            }
        }

        private void listTests_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listTests.SelectedItems.Count > 0)
            {
                IOTest test = (IOTest)listTests.SelectedItems[0].Tag;

                if (test.HasResults)
                {
                    selectedTest = test;
                }
                else
                {
                    selectedTest = null;
                }
            }
        }

        private void mainGraph_Paint(object sender, PaintEventArgs e)
        {
            if (selectedTest != null)
            {
                selectedTest.GetPlots()[0].DrawingMethod(e.Graphics, mainGraph.ClientRectangle);
            }
        }

    }
}
