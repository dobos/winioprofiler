namespace IOProfilerUI
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.listVolumes = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStripVolumes = new System.Windows.Forms.ToolStrip();
            this.toolButtonRefreshVolumes = new System.Windows.Forms.ToolStripButton();
            this.label1 = new System.Windows.Forms.Label();
            this.listTests = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStripTests = new System.Windows.Forms.ToolStrip();
            this.toolButtonAddTest = new System.Windows.Forms.ToolStripDropDownButton();
            this.sequentiaReadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolButtonSequentialReadBlockSizeTest = new System.Windows.Forms.ToolStripMenuItem();
            this.sequentialWriteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolButtonSequentialWriteBlockSizeTest = new System.Windows.Forms.ToolStripMenuItem();
            this.toolButtonAddOutstandingIOTest = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.label2 = new System.Windows.Forms.Label();
            this.mainGraph = new System.Windows.Forms.PictureBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.toolButtonExecute = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolButtonPlot = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.toolStripVolumes.SuspendLayout();
            this.toolStripTests.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainGraph)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 49);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.mainGraph);
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(852, 487);
            this.splitContainer1.SplitterDistance = 284;
            this.splitContainer1.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.listVolumes);
            this.splitContainer2.Panel1.Controls.Add(this.toolStripVolumes);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.listTests);
            this.splitContainer2.Panel2.Controls.Add(this.toolStripTests);
            this.splitContainer2.Panel2.Controls.Add(this.label2);
            this.splitContainer2.Size = new System.Drawing.Size(284, 487);
            this.splitContainer2.SplitterDistance = 230;
            this.splitContainer2.TabIndex = 0;
            // 
            // listVolumes
            // 
            this.listVolumes.CheckBoxes = true;
            this.listVolumes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listVolumes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listVolumes.Location = new System.Drawing.Point(0, 43);
            this.listVolumes.Name = "listVolumes";
            this.listVolumes.Size = new System.Drawing.Size(284, 187);
            this.listVolumes.TabIndex = 1;
            this.listVolumes.UseCompatibleStateImageBehavior = false;
            this.listVolumes.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Volume";
            this.columnHeader1.Width = 125;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "File system";
            this.columnHeader2.Width = 65;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Size";
            this.columnHeader3.Width = 67;
            // 
            // toolStripVolumes
            // 
            this.toolStripVolumes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolButtonRefreshVolumes});
            this.toolStripVolumes.Location = new System.Drawing.Point(0, 18);
            this.toolStripVolumes.Name = "toolStripVolumes";
            this.toolStripVolumes.Size = new System.Drawing.Size(284, 25);
            this.toolStripVolumes.TabIndex = 0;
            this.toolStripVolumes.Text = "toolStrip1";
            this.toolStripVolumes.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStripVolumes_ItemClicked);
            // 
            // toolButtonRefreshVolumes
            // 
            this.toolButtonRefreshVolumes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolButtonRefreshVolumes.Image = ((System.Drawing.Image)(resources.GetObject("toolButtonRefreshVolumes.Image")));
            this.toolButtonRefreshVolumes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolButtonRefreshVolumes.Name = "toolButtonRefreshVolumes";
            this.toolButtonRefreshVolumes.Size = new System.Drawing.Size(23, 22);
            this.toolButtonRefreshVolumes.Text = "toolStripButton2";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(2);
            this.label1.Size = new System.Drawing.Size(284, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "Logical Volumes";
            // 
            // listTests
            // 
            this.listTests.CheckBoxes = true;
            this.listTests.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4});
            this.listTests.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listTests.Location = new System.Drawing.Point(0, 43);
            this.listTests.Name = "listTests";
            this.listTests.Size = new System.Drawing.Size(284, 210);
            this.listTests.TabIndex = 1;
            this.listTests.UseCompatibleStateImageBehavior = false;
            this.listTests.View = System.Windows.Forms.View.Details;
            this.listTests.SelectedIndexChanged += new System.EventHandler(this.listTests_SelectedIndexChanged);
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Test";
            this.columnHeader4.Width = 255;
            // 
            // toolStripTests
            // 
            this.toolStripTests.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolButtonAddTest,
            this.toolButtonAddOutstandingIOTest,
            this.toolStripSeparator1,
            this.toolStripButton2});
            this.toolStripTests.Location = new System.Drawing.Point(0, 18);
            this.toolStripTests.Name = "toolStripTests";
            this.toolStripTests.Size = new System.Drawing.Size(284, 25);
            this.toolStripTests.TabIndex = 0;
            this.toolStripTests.Text = "toolStrip2";
            this.toolStripTests.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStripTests_ItemClicked);
            // 
            // toolButtonAddTest
            // 
            this.toolButtonAddTest.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolButtonAddTest.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sequentiaReadToolStripMenuItem,
            this.sequentialWriteToolStripMenuItem});
            this.toolButtonAddTest.Image = ((System.Drawing.Image)(resources.GetObject("toolButtonAddTest.Image")));
            this.toolButtonAddTest.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolButtonAddTest.Name = "toolButtonAddTest";
            this.toolButtonAddTest.Size = new System.Drawing.Size(29, 22);
            this.toolButtonAddTest.Text = "toolStripButton5";
            this.toolButtonAddTest.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStripTests_ItemClicked);
            // 
            // sequentiaReadToolStripMenuItem
            // 
            this.sequentiaReadToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolButtonSequentialReadBlockSizeTest});
            this.sequentiaReadToolStripMenuItem.Name = "sequentiaReadToolStripMenuItem";
            this.sequentiaReadToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.sequentiaReadToolStripMenuItem.Text = "Sequential read";
            // 
            // toolButtonSequentialReadBlockSizeTest
            // 
            this.toolButtonSequentialReadBlockSizeTest.Name = "toolButtonSequentialReadBlockSizeTest";
            this.toolButtonSequentialReadBlockSizeTest.Size = new System.Drawing.Size(193, 22);
            this.toolButtonSequentialReadBlockSizeTest.Text = "Optimal block size test";
            this.toolButtonSequentialReadBlockSizeTest.Click += new System.EventHandler(this.toolButtonSequentialReadBlockSizeTest_Click);
            // 
            // sequentialWriteToolStripMenuItem
            // 
            this.sequentialWriteToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolButtonSequentialWriteBlockSizeTest});
            this.sequentialWriteToolStripMenuItem.Name = "sequentialWriteToolStripMenuItem";
            this.sequentialWriteToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.sequentialWriteToolStripMenuItem.Text = "Sequential write";
            // 
            // toolButtonSequentialWriteBlockSizeTest
            // 
            this.toolButtonSequentialWriteBlockSizeTest.Name = "toolButtonSequentialWriteBlockSizeTest";
            this.toolButtonSequentialWriteBlockSizeTest.Size = new System.Drawing.Size(193, 22);
            this.toolButtonSequentialWriteBlockSizeTest.Text = "Optimal block size test";
            this.toolButtonSequentialWriteBlockSizeTest.Click += new System.EventHandler(this.toolButtonSequentialWriteBlockSizeTest_Click);
            // 
            // toolButtonAddOutstandingIOTest
            // 
            this.toolButtonAddOutstandingIOTest.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolButtonAddOutstandingIOTest.Image = ((System.Drawing.Image)(resources.GetObject("toolButtonAddOutstandingIOTest.Image")));
            this.toolButtonAddOutstandingIOTest.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolButtonAddOutstandingIOTest.Name = "toolButtonAddOutstandingIOTest";
            this.toolButtonAddOutstandingIOTest.Size = new System.Drawing.Size(23, 22);
            this.toolButtonAddOutstandingIOTest.Text = "toolStripButton1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "toolStripButton2";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(2);
            this.label2.Size = new System.Drawing.Size(284, 18);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tests";
            // 
            // mainGraph
            // 
            this.mainGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainGraph.Location = new System.Drawing.Point(0, 22);
            this.mainGraph.Name = "mainGraph";
            this.mainGraph.Size = new System.Drawing.Size(564, 465);
            this.mainGraph.TabIndex = 1;
            this.mainGraph.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(564, 22);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(556, 0);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Latency";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(556, 0);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Throughput";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(852, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // toolStripMain
            // 
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolButtonExecute,
            this.toolButtonPlot});
            this.toolStripMain.Location = new System.Drawing.Point(0, 24);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(852, 25);
            this.toolStripMain.TabIndex = 3;
            this.toolStripMain.Text = "toolStrip3";
            this.toolStripMain.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStripMain_ItemClicked);
            // 
            // toolButtonExecute
            // 
            this.toolButtonExecute.Image = ((System.Drawing.Image)(resources.GetObject("toolButtonExecute.Image")));
            this.toolButtonExecute.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolButtonExecute.Name = "toolButtonExecute";
            this.toolButtonExecute.Size = new System.Drawing.Size(67, 22);
            this.toolButtonExecute.Text = "Execute";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 536);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(852, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolButtonPlot
            // 
            this.toolButtonPlot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolButtonPlot.Image = ((System.Drawing.Image)(resources.GetObject("toolButtonPlot.Image")));
            this.toolButtonPlot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolButtonPlot.Name = "toolButtonPlot";
            this.toolButtonPlot.Size = new System.Drawing.Size(23, 22);
            this.toolButtonPlot.Text = "Plot";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 558);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStripMain);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Main";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.toolStripVolumes.ResumeLayout(false);
            this.toolStripVolumes.PerformLayout();
            this.toolStripTests.ResumeLayout(false);
            this.toolStripTests.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainGraph)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ListView listVolumes;
        private System.Windows.Forms.ToolStrip toolStripVolumes;
        private System.Windows.Forms.ListView listTests;
        private System.Windows.Forms.ToolStrip toolStripTests;
        private System.Windows.Forms.ToolStripButton toolButtonRefreshVolumes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripButton toolButtonExecute;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ToolStripButton toolButtonAddOutstandingIOTest;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.PictureBox mainGraph;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStripDropDownButton toolButtonAddTest;
        private System.Windows.Forms.ToolStripMenuItem sequentiaReadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolButtonSequentialReadBlockSizeTest;
        private System.Windows.Forms.ToolStripMenuItem sequentialWriteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolButtonSequentialWriteBlockSizeTest;
        private System.Windows.Forms.ToolStripButton toolButtonPlot;
    }
}