namespace DrawEA
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.tcTop = new System.Windows.Forms.TabControl();
            this.tpBuild = new System.Windows.Forms.TabPage();
            this.tlpTop = new System.Windows.Forms.TableLayoutPanel();
            this.gbInputs = new System.Windows.Forms.GroupBox();
            this.lstInput = new System.Windows.Forms.ListBox();
            this.tsEditInput = new System.Windows.Forms.ToolStrip();
            this.tsbAddInput = new System.Windows.Forms.ToolStripButton();
            this.tstInput = new System.Windows.Forms.ToolStripTextBox();
            this.tsbDeleteInput = new System.Windows.Forms.ToolStripButton();
            this.dgvTable = new System.Windows.Forms.DataGridView();
            this.colState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStart = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colAccepted = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tpTest = new System.Windows.Forms.TabPage();
            this.tlpTest = new System.Windows.Forms.TableLayoutPanel();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.tsTest = new System.Windows.Forms.ToolStrip();
            this.tsbRun = new System.Windows.Forms.ToolStripButton();
            this.tsbStep = new System.Windows.Forms.ToolStripButton();
            this.tsbInterrupt = new System.Windows.Forms.ToolStripButton();
            this.tsbStop = new System.Windows.Forms.ToolStripButton();
            this.ilTabControl = new System.Windows.Forms.ImageList(this.components);
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tsbToggleCollapse = new System.Windows.Forms.ToolStripButton();
            this.tsbChangeLanguage = new System.Windows.Forms.ToolStripSplitButton();
            this.tsmiEnglish = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiGerman = new System.Windows.Forms.ToolStripMenuItem();
            this.sep00 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.tsbLoad = new System.Windows.Forms.ToolStripButton();
            this.tsbClear = new System.Windows.Forms.ToolStripButton();
            this.sep01 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbFAMode = new System.Windows.Forms.ToolStripSplitButton();
            this.tsmiNFA = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDFA = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbEpsilon = new System.Windows.Forms.ToolStripButton();
            this.tsbFont = new System.Windows.Forms.ToolStripButton();
            this.tsbSize = new System.Windows.Forms.ToolStripButton();
            this.sep02 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbBuildGraph = new System.Windows.Forms.ToolStripButton();
            this.tsbExport = new System.Windows.Forms.ToolStripButton();
            this.sep03 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbInfo = new System.Windows.Forms.ToolStripSplitButton();
            this.tsmiDFAExample = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNFAExample = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.suMain = new DrawEA.StateUI();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            this.tcTop.SuspendLayout();
            this.tpBuild.SuspendLayout();
            this.tlpTop.SuspendLayout();
            this.gbInputs.SuspendLayout();
            this.tsEditInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTable)).BeginInit();
            this.tpTest.SuspendLayout();
            this.tlpTest.SuspendLayout();
            this.tsTest.SuspendLayout();
            this.tsMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // scMain
            // 
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scMain.Location = new System.Drawing.Point(0, 0);
            this.scMain.Name = "scMain";
            this.scMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.Controls.Add(this.tcTop);
            this.scMain.Panel1.Controls.Add(this.tsMain);
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.AutoScroll = true;
            this.scMain.Panel2.Controls.Add(this.suMain);
            this.scMain.Size = new System.Drawing.Size(768, 452);
            this.scMain.SplitterDistance = 218;
            this.scMain.TabIndex = 0;
            // 
            // tcTop
            // 
            this.tcTop.Controls.Add(this.tpBuild);
            this.tcTop.Controls.Add(this.tpTest);
            this.tcTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcTop.HotTrack = true;
            this.tcTop.ImageList = this.ilTabControl;
            this.tcTop.ItemSize = new System.Drawing.Size(59, 17);
            this.tcTop.Location = new System.Drawing.Point(0, 0);
            this.tcTop.Multiline = true;
            this.tcTop.Name = "tcTop";
            this.tcTop.SelectedIndex = 0;
            this.tcTop.Size = new System.Drawing.Size(768, 182);
            this.tcTop.TabIndex = 2;
            // 
            // tpBuild
            // 
            this.tpBuild.Controls.Add(this.tlpTop);
            this.tpBuild.ImageIndex = 0;
            this.tpBuild.Location = new System.Drawing.Point(4, 21);
            this.tpBuild.Name = "tpBuild";
            this.tpBuild.Padding = new System.Windows.Forms.Padding(3);
            this.tpBuild.Size = new System.Drawing.Size(760, 157);
            this.tpBuild.TabIndex = 0;
            this.tpBuild.Text = "Build";
            this.tpBuild.UseVisualStyleBackColor = true;
            // 
            // tlpTop
            // 
            this.tlpTop.ColumnCount = 2;
            this.tlpTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tlpTop.Controls.Add(this.gbInputs, 1, 0);
            this.tlpTop.Controls.Add(this.dgvTable, 0, 0);
            this.tlpTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTop.Location = new System.Drawing.Point(3, 3);
            this.tlpTop.Name = "tlpTop";
            this.tlpTop.RowCount = 1;
            this.tlpTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 151F));
            this.tlpTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 151F));
            this.tlpTop.Size = new System.Drawing.Size(754, 151);
            this.tlpTop.TabIndex = 0;
            // 
            // gbInputs
            // 
            this.gbInputs.Controls.Add(this.lstInput);
            this.gbInputs.Controls.Add(this.tsEditInput);
            this.gbInputs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbInputs.Location = new System.Drawing.Point(557, 3);
            this.gbInputs.Name = "gbInputs";
            this.gbInputs.Size = new System.Drawing.Size(194, 145);
            this.gbInputs.TabIndex = 1;
            this.gbInputs.TabStop = false;
            this.gbInputs.Text = "Inputs";
            // 
            // lstInput
            // 
            this.lstInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstInput.FormattingEnabled = true;
            this.lstInput.IntegralHeight = false;
            this.lstInput.Location = new System.Drawing.Point(3, 16);
            this.lstInput.Name = "lstInput";
            this.lstInput.Size = new System.Drawing.Size(188, 101);
            this.lstInput.Sorted = true;
            this.lstInput.TabIndex = 0;
            this.lstInput.SelectedIndexChanged += new System.EventHandler(this.lstInput_SelectedIndexChanged);
            // 
            // tsEditInput
            // 
            this.tsEditInput.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tsEditInput.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsEditInput.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAddInput,
            this.tstInput,
            this.tsbDeleteInput});
            this.tsEditInput.Location = new System.Drawing.Point(3, 117);
            this.tsEditInput.Name = "tsEditInput";
            this.tsEditInput.Size = new System.Drawing.Size(188, 25);
            this.tsEditInput.TabIndex = 1;
            this.tsEditInput.TabStop = true;
            this.tsEditInput.Text = "toolStrip1";
            // 
            // tsbAddInput
            // 
            this.tsbAddInput.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAddInput.Enabled = false;
            this.tsbAddInput.Image = global::DrawEA.Properties.Resources.add;
            this.tsbAddInput.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddInput.Name = "tsbAddInput";
            this.tsbAddInput.Size = new System.Drawing.Size(23, 22);
            this.tsbAddInput.Text = "Add Input";
            this.tsbAddInput.Click += new System.EventHandler(this.tsbAddInput_Click);
            // 
            // tstInput
            // 
            this.tstInput.AcceptsReturn = true;
            this.tstInput.Name = "tstInput";
            this.tstInput.Size = new System.Drawing.Size(100, 25);
            this.tstInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tstInput_KeyDown);
            this.tstInput.TextChanged += new System.EventHandler(this.tstInput_TextChanged);
            // 
            // tsbDeleteInput
            // 
            this.tsbDeleteInput.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDeleteInput.Enabled = false;
            this.tsbDeleteInput.Image = global::DrawEA.Properties.Resources.cross;
            this.tsbDeleteInput.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDeleteInput.Name = "tsbDeleteInput";
            this.tsbDeleteInput.Size = new System.Drawing.Size(23, 22);
            this.tsbDeleteInput.Text = "Delete Input";
            this.tsbDeleteInput.Click += new System.EventHandler(this.tsbDeleteInput_Click);
            // 
            // dgvTable
            // 
            this.dgvTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colState,
            this.colStart,
            this.colAccepted});
            this.dgvTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTable.Location = new System.Drawing.Point(3, 3);
            this.dgvTable.Name = "dgvTable";
            this.dgvTable.Size = new System.Drawing.Size(548, 145);
            this.dgvTable.TabIndex = 0;
            this.dgvTable.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgvTable_UserAddedRow);
            this.dgvTable.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTable_CellEndEdit);
            // 
            // colState
            // 
            this.colState.HeaderText = "State";
            this.colState.Name = "colState";
            this.colState.Width = 80;
            // 
            // colStart
            // 
            this.colStart.HeaderText = "Start";
            this.colStart.Name = "colStart";
            this.colStart.Width = 65;
			this.colStart.ValueType = typeof(bool);
            // 
            // colAccepted
            // 
            this.colAccepted.HeaderText = "Accepted";
            this.colAccepted.Name = "colAccepted";
            this.colAccepted.Width = 65;
            this.colAccepted.ValueType = typeof(bool);
			//
            // tpTest
            // 
            this.tpTest.Controls.Add(this.tlpTest);
            this.tpTest.Controls.Add(this.tsTest);
            this.tpTest.ImageIndex = 1;
            this.tpTest.Location = new System.Drawing.Point(4, 21);
            this.tpTest.Name = "tpTest";
            this.tpTest.Padding = new System.Windows.Forms.Padding(3);
            this.tpTest.Size = new System.Drawing.Size(760, 157);
            this.tpTest.TabIndex = 1;
            this.tpTest.Text = "Test";
            this.tpTest.UseVisualStyleBackColor = true;
            // 
            // tlpTest
            // 
            this.tlpTest.ColumnCount = 1;
            this.tlpTest.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTest.Controls.Add(this.txtOutput, 0, 1);
            this.tlpTest.Controls.Add(this.txtInput, 0, 0);
            this.tlpTest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTest.Location = new System.Drawing.Point(3, 28);
            this.tlpTest.Name = "tlpTest";
            this.tlpTest.RowCount = 2;
            this.tlpTest.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tlpTest.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTest.Size = new System.Drawing.Size(754, 126);
            this.tlpTest.TabIndex = 0;
            // 
            // txtOutput
            // 
            this.txtOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOutput.Location = new System.Drawing.Point(3, 29);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOutput.Size = new System.Drawing.Size(748, 94);
            this.txtOutput.TabIndex = 2;
            // 
            // txtInput
            // 
            this.txtInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInput.Location = new System.Drawing.Point(3, 3);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(748, 20);
            this.txtInput.TabIndex = 1;
            // 
            // tsTest
            // 
            this.tsTest.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsTest.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbRun,
            this.tsbStep,
            this.tsbInterrupt,
            this.tsbStop});
            this.tsTest.Location = new System.Drawing.Point(3, 3);
            this.tsTest.Name = "tsTest";
            this.tsTest.Size = new System.Drawing.Size(754, 25);
            this.tsTest.TabIndex = 3;
            this.tsTest.Text = "toolStrip1";
            // 
            // tsbRun
            // 
            this.tsbRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRun.Image = global::DrawEA.Properties.Resources.control_play;
            this.tsbRun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRun.Name = "tsbRun";
            this.tsbRun.Size = new System.Drawing.Size(23, 22);
            this.tsbRun.Text = "Run";
            this.tsbRun.Click += new System.EventHandler(this.tsbRun_Click);
            // 
            // tsbStep
            // 
            this.tsbStep.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbStep.Image = global::DrawEA.Properties.Resources.control_fastforward;
            this.tsbStep.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbStep.Name = "tsbStep";
            this.tsbStep.Size = new System.Drawing.Size(23, 22);
            this.tsbStep.Text = "Step";
            this.tsbStep.Click += new System.EventHandler(this.tsbStep_Click);
            // 
            // tsbInterrupt
            // 
            this.tsbInterrupt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbInterrupt.Enabled = false;
            this.tsbInterrupt.Image = global::DrawEA.Properties.Resources.control_pause;
            this.tsbInterrupt.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbInterrupt.Name = "tsbInterrupt";
            this.tsbInterrupt.Size = new System.Drawing.Size(23, 22);
            this.tsbInterrupt.Text = "Pause";
            this.tsbInterrupt.Click += new System.EventHandler(this.tsbInterrupt_Click);
            // 
            // tsbStop
            // 
            this.tsbStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbStop.Enabled = false;
            this.tsbStop.Image = global::DrawEA.Properties.Resources.control_stop;
            this.tsbStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbStop.Name = "tsbStop";
            this.tsbStop.Size = new System.Drawing.Size(23, 22);
            this.tsbStop.Text = "Stop";
            this.tsbStop.Click += new System.EventHandler(this.tsbStop_Click);
            // 
            // ilTabControl
            // 
            this.ilTabControl.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilTabControl.ImageStream")));
            this.ilTabControl.TransparentColor = System.Drawing.Color.Transparent;
            this.ilTabControl.Images.SetKeyName(0, "bullet_wrench.png");
            this.ilTabControl.Images.SetKeyName(1, "bullet_go.png");
            // 
            // tsMain
            // 
            this.tsMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tsMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbToggleCollapse,
            this.tsbChangeLanguage,
            this.sep00,
            this.tsbSave,
            this.tsbLoad,
            this.tsbClear,
            this.sep01,
            this.tsbFAMode,
            this.tsbEpsilon,
            this.tsbFont,
            this.tsbSize,
            this.sep02,
            this.tsbBuildGraph,
            this.tsbExport,
            this.sep03,
            this.tsbInfo});
            this.tsMain.Location = new System.Drawing.Point(0, 182);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(768, 36);
            this.tsMain.TabIndex = 1;
            // 
            // tsbToggleCollapse
            // 
            this.tsbToggleCollapse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbToggleCollapse.Image = global::DrawEA.Properties.Resources.bullet_arrow_up;
            this.tsbToggleCollapse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbToggleCollapse.Name = "tsbToggleCollapse";
            this.tsbToggleCollapse.Size = new System.Drawing.Size(23, 33);
            this.tsbToggleCollapse.Text = "Toggle Collapse";
            this.tsbToggleCollapse.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbToggleCollapse.Click += new System.EventHandler(this.tsbToggleCollapse_Click);
            // 
            // tsbChangeLanguage
            // 
            this.tsbChangeLanguage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbChangeLanguage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiEnglish,
            this.tsmiGerman});
            this.tsbChangeLanguage.Image = global::DrawEA.Properties.Resources.flag_blue;
            this.tsbChangeLanguage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbChangeLanguage.Name = "tsbChangeLanguage";
            this.tsbChangeLanguage.Size = new System.Drawing.Size(32, 33);
            this.tsbChangeLanguage.Text = "Change Language";
            this.tsbChangeLanguage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbChangeLanguage.ButtonClick += new System.EventHandler(this.tsbChangeLanguage_ButtonClick);
            // 
            // tsmiEnglish
            // 
            this.tsmiEnglish.Image = global::DrawEA.Properties.Resources.gb;
            this.tsmiEnglish.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiEnglish.Name = "tsmiEnglish";
            this.tsmiEnglish.Size = new System.Drawing.Size(124, 22);
            this.tsmiEnglish.Text = "English";
            this.tsmiEnglish.Click += new System.EventHandler(this.tsmiEnglish_Click);
            // 
            // tsmiGerman
            // 
            this.tsmiGerman.Image = global::DrawEA.Properties.Resources.de;
            this.tsmiGerman.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiGerman.Name = "tsmiGerman";
            this.tsmiGerman.Size = new System.Drawing.Size(124, 22);
            this.tsmiGerman.Text = "Deutsch";
            this.tsmiGerman.Click += new System.EventHandler(this.tsmiGerman_Click);
            // 
            // sep00
            // 
            this.sep00.Name = "sep00";
            this.sep00.Size = new System.Drawing.Size(6, 36);
            // 
            // tsbSave
            // 
            this.tsbSave.Image = global::DrawEA.Properties.Resources.disk;
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(35, 33);
            this.tsbSave.Text = "Save";
            this.tsbSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // tsbLoad
            // 
            this.tsbLoad.Image = global::DrawEA.Properties.Resources.folder;
            this.tsbLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLoad.Name = "tsbLoad";
            this.tsbLoad.Size = new System.Drawing.Size(34, 33);
            this.tsbLoad.Text = "Load";
            this.tsbLoad.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbLoad.Click += new System.EventHandler(this.tsbLoad_Click);
            // 
            // tsbClear
            // 
            this.tsbClear.Image = global::DrawEA.Properties.Resources.bin;
            this.tsbClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClear.Name = "tsbClear";
            this.tsbClear.Size = new System.Drawing.Size(36, 33);
            this.tsbClear.Text = "Clear";
            this.tsbClear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbClear.Click += new System.EventHandler(this.tsbClear_Click);
            // 
            // sep01
            // 
            this.sep01.Name = "sep01";
            this.sep01.Size = new System.Drawing.Size(6, 36);
            // 
            // tsbFAMode
            // 
            this.tsbFAMode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiNFA,
            this.tsmiDFA});
            this.tsbFAMode.Image = ((System.Drawing.Image)(resources.GetObject("tsbFAMode.Image")));
            this.tsbFAMode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFAMode.Name = "tsbFAMode";
            this.tsbFAMode.Size = new System.Drawing.Size(32, 33);
            this.tsbFAMode.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbFAMode.ButtonClick += new System.EventHandler(this.tsbFAMode_ButtonClick);
            this.tsbFAMode.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tsbFAMode_DropDownItemClicked);
            // 
            // tsmiNFA
            // 
            this.tsmiNFA.Image = global::DrawEA.Properties.Resources.arrow_switch;
            this.tsmiNFA.Name = "tsmiNFA";
            this.tsmiNFA.Size = new System.Drawing.Size(105, 22);
            this.tsmiNFA.Text = "NFA";
            this.tsmiNFA.ToolTipText = "nondeterministic finite automaton";
            // 
            // tsmiDFA
            // 
            this.tsmiDFA.Image = global::DrawEA.Properties.Resources.arrow_right;
            this.tsmiDFA.Name = "tsmiDFA";
            this.tsmiDFA.Size = new System.Drawing.Size(105, 22);
            this.tsmiDFA.Text = "DFA";
            this.tsmiDFA.ToolTipText = "deterministic finite automaton";
            // 
            // tsbEpsilon
            // 
            this.tsbEpsilon.Image = global::DrawEA.Properties.Resources.link_go;
            this.tsbEpsilon.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEpsilon.Name = "tsbEpsilon";
            this.tsbEpsilon.Size = new System.Drawing.Size(23, 33);
            this.tsbEpsilon.Text = "ε";
            this.tsbEpsilon.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbEpsilon.Click += new System.EventHandler(this.tsbEpsilon_Click);
            // 
            // tsbFont
            // 
            this.tsbFont.Image = global::DrawEA.Properties.Resources.font;
            this.tsbFont.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFont.Name = "tsbFont";
            this.tsbFont.Size = new System.Drawing.Size(33, 33);
            this.tsbFont.Text = "Font";
            this.tsbFont.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbFont.Click += new System.EventHandler(this.tsbFont_Click);
            // 
            // tsbSize
            // 
            this.tsbSize.Image = global::DrawEA.Properties.Resources.arrow_inout;
            this.tsbSize.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSize.Name = "tsbSize";
            this.tsbSize.Size = new System.Drawing.Size(30, 33);
            this.tsbSize.Text = "Size";
            this.tsbSize.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbSize.Click += new System.EventHandler(this.tsbSize_Click);
            // 
            // sep02
            // 
            this.sep02.Name = "sep02";
            this.sep02.Size = new System.Drawing.Size(6, 36);
            // 
            // tsbBuildGraph
            // 
            this.tsbBuildGraph.Image = global::DrawEA.Properties.Resources.wand;
            this.tsbBuildGraph.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBuildGraph.Name = "tsbBuildGraph";
            this.tsbBuildGraph.Size = new System.Drawing.Size(65, 33);
            this.tsbBuildGraph.Text = "Build Graph";
            this.tsbBuildGraph.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbBuildGraph.Click += new System.EventHandler(this.tsbBuildGraph_Click);
            // 
            // tsbExport
            // 
            this.tsbExport.Image = global::DrawEA.Properties.Resources.images;
            this.tsbExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExport.Name = "tsbExport";
            this.tsbExport.Size = new System.Drawing.Size(43, 33);
            this.tsbExport.Text = "Export";
            this.tsbExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbExport.Click += new System.EventHandler(this.tsbExport_Click);
            // 
            // sep03
            // 
            this.sep03.Name = "sep03";
            this.sep03.Size = new System.Drawing.Size(6, 36);
            // 
            // tsbInfo
            // 
            this.tsbInfo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiDFAExample,
            this.tsmiNFAExample,
            this.tsmiAbout});
            this.tsbInfo.Image = global::DrawEA.Properties.Resources.help;
            this.tsbInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbInfo.Name = "tsbInfo";
            this.tsbInfo.Size = new System.Drawing.Size(43, 33);
            this.tsbInfo.Text = "Info";
            this.tsbInfo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbInfo.ButtonClick += new System.EventHandler(this.tsbInfo_ButtonClick);
            // 
            // tsmiDFAExample
            // 
            this.tsmiDFAExample.Name = "tsmiDFAExample";
            this.tsmiDFAExample.Size = new System.Drawing.Size(152, 22);
            this.tsmiDFAExample.Text = "DFA Example";
            this.tsmiDFAExample.Click += new System.EventHandler(this.tsmiDFAExample_Click);
            // 
            // tsmiNFAExample
            // 
            this.tsmiNFAExample.Name = "tsmiNFAExample";
            this.tsmiNFAExample.Size = new System.Drawing.Size(152, 22);
            this.tsmiNFAExample.Text = "NFA Example";
            this.tsmiNFAExample.Click += new System.EventHandler(this.tsmiNFAExample_Click);
            // 
            // tsmiAbout
            // 
            this.tsmiAbout.Name = "tsmiAbout";
            this.tsmiAbout.Size = new System.Drawing.Size(152, 22);
            this.tsmiAbout.Text = "About";
            this.tsmiAbout.Click += new System.EventHandler(this.tsmiAbout_Click);
            // 
            // suMain
            // 
            this.suMain.AutoScroll = true;
            this.suMain.AutoScrollMinSize = new System.Drawing.Size(10, 10);
            this.suMain.BackColor = System.Drawing.Color.White;
            this.suMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.suMain.Location = new System.Drawing.Point(0, 0);
            this.suMain.Name = "suMain";
            this.suMain.Size = new System.Drawing.Size(768, 230);
            this.suMain.TabIndex = 0;
            this.suMain.Text = "stateUI1";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(768, 452);
            this.Controls.Add(this.scMain);
            this.Name = "frmMain";
            this.Text = "DrawEA";
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel1.PerformLayout();
            this.scMain.Panel2.ResumeLayout(false);
            this.scMain.ResumeLayout(false);
            this.tcTop.ResumeLayout(false);
            this.tpBuild.ResumeLayout(false);
            this.tlpTop.ResumeLayout(false);
            this.gbInputs.ResumeLayout(false);
            this.gbInputs.PerformLayout();
            this.tsEditInput.ResumeLayout(false);
            this.tsEditInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTable)).EndInit();
            this.tpTest.ResumeLayout(false);
            this.tpTest.PerformLayout();
            this.tlpTest.ResumeLayout(false);
            this.tlpTest.PerformLayout();
            this.tsTest.ResumeLayout(false);
            this.tsTest.PerformLayout();
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer scMain;
        private System.Windows.Forms.TableLayoutPanel tlpTop;
        private System.Windows.Forms.ToolStrip tsEditInput;
        private System.Windows.Forms.ToolStripButton tsbAddInput;
        private System.Windows.Forms.ToolStripTextBox tstInput;
        private System.Windows.Forms.ToolStripButton tsbDeleteInput;
        private System.Windows.Forms.ListBox lstInput;
        private System.Windows.Forms.GroupBox gbInputs;
        private System.Windows.Forms.DataGridView dgvTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn colState;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colStart;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colAccepted;
        private System.Windows.Forms.ToolStrip tsMain;
        private System.Windows.Forms.ToolStripButton tsbToggleCollapse;
        private System.Windows.Forms.ToolStripButton tsbBuildGraph;
        private System.Windows.Forms.ToolStripButton tsbExport;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripButton tsbLoad;
        private System.Windows.Forms.ToolStripSeparator sep00;
        private System.Windows.Forms.ToolStripSeparator sep01;
        private System.Windows.Forms.ToolStripButton tsbClear;
        private System.Windows.Forms.ToolStripSplitButton tsbChangeLanguage;
        private System.Windows.Forms.ToolStripMenuItem tsmiEnglish;
        private System.Windows.Forms.ToolStripMenuItem tsmiGerman;
        private System.Windows.Forms.ToolStripSplitButton tsbFAMode;
        private System.Windows.Forms.ToolStripMenuItem tsmiNFA;
        private System.Windows.Forms.ToolStripMenuItem tsmiDFA;
        private System.Windows.Forms.ToolStripSeparator sep02;
        private System.Windows.Forms.ToolStripButton tsbFont;
        private System.Windows.Forms.ToolStripButton tsbSize;
        private System.Windows.Forms.ToolStripButton tsbEpsilon;
        private System.Windows.Forms.TabControl tcTop;
        private System.Windows.Forms.TabPage tpBuild;
        private System.Windows.Forms.TabPage tpTest;
        private System.Windows.Forms.ImageList ilTabControl;
        private System.Windows.Forms.TableLayoutPanel tlpTest;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.ToolStrip tsTest;
        private System.Windows.Forms.ToolStripButton tsbRun;
        private System.Windows.Forms.ToolStripButton tsbStep;
        private System.Windows.Forms.ToolStripButton tsbStop;
        private System.Windows.Forms.ToolStripButton tsbInterrupt;
        private StateUI suMain;
        private System.Windows.Forms.ToolStripSeparator sep03;
        private System.Windows.Forms.ToolStripSplitButton tsbInfo;
        private System.Windows.Forms.ToolStripMenuItem tsmiDFAExample;
        private System.Windows.Forms.ToolStripMenuItem tsmiNFAExample;
        private System.Windows.Forms.ToolStripMenuItem tsmiAbout;
    }
}

