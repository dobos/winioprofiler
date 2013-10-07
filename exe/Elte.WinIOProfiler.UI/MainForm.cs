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
        }

        private void toolStripMain_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == toolButtonExecute)
            {
                ExecuteTests();
            }
            else if (e.ClickedItem == toolButtonPlot)
            {
                DrawThroughputPlots();
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

        private void DrawThroughputPlots()
        {
            var pane = new GraphPane(mainGraph.ClientRectangle, "IO System Performance", "Block size", "");


            var blockSizes = new SortedSet<uint>();

            foreach (ListViewItem li in listTests.Items)
            {
                var test = (IOTest)li.Tag;
                var plot = test.GetPlots()[0];

                pane.AddCurve(plot.Text, plot.X, plot.Y, plot.Color);
                pane.AddErrorBar("", plot.X, plot.YErrMin, plot.YErrMax, plot.Color);

                for (int i = 0; i < plot.X.Length; i++)
                {
                    if (!blockSizes.Contains((uint)plot.X[i]))
                    {
                        blockSizes.Add((uint)plot.X[i]);
                    }
                }
            }

            var bs = blockSizes.ToArray();
            var labels = new string[bs.Length];
            for (int i = 0; i < labels.Length; i++)
            {
                labels[i] = Util.FormatFileSize(bs[i]);
            }
            pane.XAxis.Type = AxisType.Text;
            pane.XAxis.Scale.TextLabels = labels;

            pane.AxisChange();
            pane.Draw(mainGraph.CreateGraphics());
        }

        private void toolButtonSequentialWriteBlockSizeTest_Click(object sender, EventArgs e)
        {
            var test = new BlockSizeTest();
            test.Text = "SequentialWriteBlockSizeTest";
            test.IOSettings.IOType = IOType.Write;
            AddTest(test);
        }

        private void toolButtonSequentialReadBlockSizeTest_Click(object sender, EventArgs e)
        {
            var test = new BlockSizeTest();
            test.Text = "SequentialReadBlockSizeTest";
            test.IOSettings.IOType = IOType.Read;
            AddTest(test);
        }

    }
}
