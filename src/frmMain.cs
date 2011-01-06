using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;
using DrawEA.Model;

namespace DrawEA
{
    public partial class frmMain : Form
    {
        #region Fields
        volatile bool cancelTest, interruptTest;
        bool nondeterministic;
        int upperPanelSplitter;

        readonly ResourceManager resMan = new ResourceManager("DrawEA.Resources.lang", System.Reflection.Assembly.GetExecutingAssembly());

        readonly Regex dfaParse = new Regex(@"^(?<switch>(?<dest>[^/\s]+)\s*(?<output>(/\s*[^\s]+){0,1}))$", RegexOptions.Compiled | RegexOptions.ExplicitCapture);
        readonly Regex nfaParse = new Regex(@"^{(\s*(?<switch>(?<dest>[^/\s,]+)\s*(?<output>(/\s*[^\s,]+){0,1}))(?:\s*,?))+}$", RegexOptions.Compiled | RegexOptions.ExplicitCapture);

        Thread testWorker;

        readonly CultureInfo cultEn = new CultureInfo("en-US");
        readonly CultureInfo cultDe = new CultureInfo("de-DE");
        CultureInfo currCult;
        #endregion

        #region Constructor
        public frmMain()
        {
            InitializeComponent();

            currCult = cultEn;

            upperPanelSplitter = scMain.SplitterDistance;

            tsbChangeLanguage.DropDownDirection = ToolStripDropDownDirection.BelowRight;
            tsbFAMode.DropDownDirection = ToolStripDropDownDirection.BelowRight;
            tsbInfo.DropDownDirection = ToolStripDropDownDirection.BelowRight;

            if (resMan.GetResourceSet(cultEn, true, false) == null)
                tsmiEnglish.Enabled = false;
            if (resMan.GetResourceSet(cultDe, true, false) == null)
                tsmiGerman.Enabled = false;

            tsbFAMode.Image = tsmiDFA.Image;
            tsbFAMode.Text = tsmiDFA.Text;
            tsbFAMode.ToolTipText = tsmiDFA.ToolTipText;

            State.SigmaStep += State_SigmaStep;
        }

        #endregion

        #region Methods
        private void AddInput()
        {
            lstInput.Items.Add(tstInput.Text);
            tstInput.Text = string.Empty;
            tstInput.Focus();
            UpdateTable();
        }

        private void Build()
        {
            if (dgvTable.Rows.Count == 0)
            {
                suMain.States = null;
                return;
            }

            SortedDictionary<string, State> newStates = new SortedDictionary<string, State>(), oldStates = suMain.States;
            Dictionary<int, string> rowToName = new Dictionary<int, string>();

            for (int i = 0; i < dgvTable.Rows.Count - 1; i++)
            {
                string name = dgvTable.Rows[i].Cells[0].Value as string;

                if (string.IsNullOrEmpty(name) || newStates.ContainsKey(name))
                    continue;

                object isStart = dgvTable.Rows[i].Cells[1].Value;
                object isAccepted = dgvTable.Rows[i].Cells[2].Value;
                State state = new State(name, isAccepted == null ? false : (bool)isAccepted, isStart == null ? false : (bool)isStart);

                newStates.Add(state.Name, state);
                rowToName.Add(i, name);

                if (oldStates.ContainsKey(state.Name))
                    newStates[state.Name].Location = oldStates[state.Name].Location;
                else
                    newStates[state.Name].Location = new Point(State.STATE_SIZE + i * State.STATE_SIZE * 4, State.STATE_SIZE * 3);
            }

            for (int i = 0; i < dgvTable.Rows.Count - 1; i++)
            {
                using (DataGridViewRow row = dgvTable.Rows[i])
                {
                    if (!rowToName.ContainsKey(i))
                        continue;

                    string name = rowToName[i];

                    for (int col = 3; col < dgvTable.Columns.Count; col++)
                    {
                        string value = row.Cells[col].Value as string;

                        if (string.IsNullOrEmpty(value))
                            continue;

                        Match m = nondeterministic ? nfaParse.Match(value) : dfaParse.Match(value);

                        if (m!=null)
                        {
                            for (int capture = 0; capture < m.Groups["switch"].Captures.Count; capture++)
                            {
                                string dest = m.Groups["dest"].Captures[capture].Value;

                                if (!newStates.ContainsKey(dest))
                                    continue;

                                string input = dgvTable.Columns[col].HeaderText;
                                string output = m.Groups["output"].Captures[capture].Value;

                                if (!string.IsNullOrEmpty(output))
                                    output = output.TrimStart(new char[] { '/', ' ' });

                                Switch sw = new Switch(newStates[name], newStates[dest], input, output);

                                sw.From.Destinations.Add(sw);
                                sw.To.Sources.Add(sw);  
                            }
                        }
                    }
                }
            }

            foreach (State s in newStates.Values)
            {
                if (oldStates.ContainsKey(s.Name))
                {
                    State oldState = oldStates[s.Name];
                    foreach (Switch swOld in oldState.Destinations)
                    {
                        foreach(Switch swNew in s.Destinations)
                            if (swOld.ToName == swNew.ToName)
                            {
                                swNew.C1Offset = swOld.C1Offset;
                                swNew.C1ROffset = swOld.C1ROffset;
                                swNew.C2Offset = swOld.C2Offset;
                                swNew.C2ROffset = swOld.C2ROffset;
                            }
                    }
                }
            }

            suMain.States = newStates;
        }

        private bool IsValidEntry(string value)
        {
            if (nondeterministic)
                return nfaParse.IsMatch(value);
            
            return dfaParse.IsMatch(value);
        }

        private void LoadFrom(Stream stream, bool nfa_mode)
        {
            dgvTable.SuspendLayout();

            XmlSerializer xSer = new XmlSerializer(typeof(List<State>));

            List<State> lstStates = (List<State>)xSer.Deserialize(stream);

            lstInput.Items.Clear();
            suMain.States.Clear();
            dgvTable.Rows.Clear();

            ShowEpsilon(false);

            for (int i = dgvTable.Columns.Count - 1; i >= (tsbEpsilon.Checked ? 4 : 3); i--)
                dgvTable.Columns.RemoveAt(i);

            SetFAMode(nfa_mode);

            foreach (State s in lstStates)
            {
                suMain.States.Add(s.Name, s);

                foreach (Switch sw in s.Destinations)
                {
                    if (sw.Input == Switch.EPSILON)
                        ShowEpsilon(true);
                    else if (!lstInput.Items.Contains(sw.Input))
                        lstInput.Items.Add(sw.Input);
                }

                UpdateTable();

                DataGridViewRow row = dgvTable.Rows[dgvTable.Rows.Add()];

                foreach (Switch sw in s.Destinations)
                {
                    DataGridViewCell cell = row.Cells[sw.Input == Switch.EPSILON ? "epsilon" : sw.Input];
                    cell.Value =
                        (cell.Value == null ? string.Empty : (cell.Value as string) + ", ") +
                        (string.IsNullOrEmpty(sw.Output) ?
                                                             sw.ToName :
                                                                           sw.ToName + "/" + sw.Output);
                }

                if (nondeterministic)
                {
                    for (int col = 3; col < dgvTable.ColumnCount; col++)
                    {
                        DataGridViewCell cell = row.Cells[col];

                        string cellVal = cell.Value as string;

                        if (!string.IsNullOrEmpty(cellVal))
                        {
                            if (!cellVal.StartsWith("{"))
                                cellVal = "{" + cellVal;

                            if (!cellVal.EndsWith("}"))
                                cellVal += "}";

                            cell.Value = cellVal;
                        }
                    }
                }

                row.Cells[0].Value = s.Name;
                row.Cells[1].Value = s.IsStart;
                row.Cells[2].Value = s.IsAccepted;
            }

            for (int y = 0; y < dgvTable.RowCount - 1; y++)
                for (int x = 0; x < dgvTable.ColumnCount; x++)
                    ValidateCell(x, y);

            dgvTable.ResumeLayout();

            Build();
        }

        private void RemoveSelectedInput()
        {
            string strInput = lstInput.SelectedItem as string;
            if (strInput != null && dgvTable.Columns.Contains(strInput))
                dgvTable.Columns.Remove(strInput);
            lstInput.Items.RemoveAt(lstInput.SelectedIndex);
        }

        private void RunTest(object param)
        {
            List<State> starts = param as List<State>;

            if(starts==null)
            {
                UpdateButtonStatesTest(false, false);
                return;
            }
                 
            bool accepted = false;

            foreach (State s in starts)
            {
                Stack<State> way = new Stack<State>();
                Stack<string> output = new Stack<string>();

                if (s.Sigma(txtInput.Text, ref way, ref output))
                {
                    accepted = true;
                    break;
                }
            }

            ThreadingControlsHelper.SyncInvoke(this, delegate
                 {
                     txtOutput.AppendText(Environment.NewLine);
                     txtOutput.AppendText((accepted
                                               ? resMan.GetString(
                                                     "Testresult_Accepted",
                                                     currCult)
                                               : resMan.GetString(
                                                     "Testresult_NotAccepted",
                                                     currCult)));
                     suMain.TestState = null;
                 });

            UpdateButtonStatesTest(false, false);
        }

        private void SetFAMode(bool newNondeterministic)
        {
            if (newNondeterministic != nondeterministic)
            {
                nondeterministic = newNondeterministic;

                if (newNondeterministic)
                {
                    tsbFAMode.Image = tsmiNFA.Image;
                    tsbFAMode.Text = tsmiNFA.Text;
                    tsbFAMode.ToolTipText = tsmiNFA.ToolTipText;
                }
                else
                {
                    tsbFAMode.Image = tsmiDFA.Image;
                    tsbFAMode.Text = tsmiDFA.Text;
                    tsbFAMode.ToolTipText = tsmiDFA.ToolTipText;
                }

                for (int y = 0; y < dgvTable.RowCount - 1; y++)
                    for (int x = 3; x < dgvTable.ColumnCount; x++)
                        ValidateCell(x, y);
            }
        }
        
        public void ShowEpsilon(bool enabled)
        {
            if (dgvTable.Columns.Contains("epsilon") != enabled)
            {
                tsbEpsilon.Checked = enabled;

                if (tsbEpsilon.Checked)
                {
                    DataGridViewColumn col = new DataGridViewTextBoxColumn
                                                 {
                                                     HeaderText = Switch.EPSILON,
                                                     Name = "epsilon",
                                                     Width = 40
                                                 };

                    dgvTable.Columns.Insert(3, col);
                }
                else
                {
                    dgvTable.Columns.Remove("epsilon");
                }
            }
        }

        private static string StateStackToString(Stack<State> way)
        {
            StringBuilder retVal = new StringBuilder();

            List<State> wayLst = new List<State>(way.ToArray());
            wayLst.Reverse();

            wayLst.ForEach(s =>
                               {
                                   retVal.Append(" --> ");
                                   retVal.Append(s.Name);
                               });
            
            return retVal.ToString();
        }

        private void TestInterrupt()
        {
            interruptTest = true;
        }

        private void TestStart(bool steps)
        {
            cancelTest = false;
            interruptTest = steps;

            if (testWorker == null || testWorker.ThreadState == ThreadState.Stopped || testWorker.ThreadState == ThreadState.Aborted)
            {
                Build();

                bool anyAccepted = false;
                List<bool> startsCanAccept = new List<bool>();
                List<State> starts = new List<State>();
                foreach (State s in suMain.States.Values)
                {
                    if (s.IsStart)
                        starts.Add(s);
                    if (s.IsAccepted)
                        anyAccepted = true;
                }

                if (!anyAccepted)
                {
                    MessageBox.Show("No Acceptstate defined", "Unable to test", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                foreach (State s in starts)
                    startsCanAccept.Add(s.CanAccept());

                if (starts.Count == 0)
                {
                    MessageBox.Show("No Startstate defined", "Unable to test", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!startsCanAccept.Contains(true))
                {
                    MessageBox.Show("No Acceptstate can be reached from any Startstate", "Unable to test", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                UpdateButtonStatesTest(true, false);
                //dgvTable.Enabled = false;

                //tsEditInput.Enabled = false;
                //tsMain.Enabled = false;

                //txtInput.Enabled = false;
                //txtInput.HideSelection = false;

                ParameterizedThreadStart testStarter = RunTest;
                testWorker = new Thread(testStarter) {Name = "TestWorker", Priority = ThreadPriority.BelowNormal};
                testWorker.Start(starts);
            }
            else if (testWorker.ThreadState == ThreadState.Suspended)
            {
                interruptTest = steps;
                UpdateButtonStatesTest(true, false);
                testWorker.Resume();
            }

        }

        private void TestStop()
        {
            cancelTest = true;
            if (testWorker.ThreadState == ThreadState.Suspended)
            {
                try
                {
                    testWorker.Resume();
                }
                catch (Exception ex)
                { Console.WriteLine(ex); }
            }
        }

        private void ToggleCollapse()
        {
            if (scMain.SplitterDistance == tsMain.Height)
            {
                tsbToggleCollapse.Image = Properties.Resources.bullet_arrow_up;
                scMain.IsSplitterFixed = false;
                scMain.SplitterDistance = upperPanelSplitter;
            }
            else
            {
                tsbToggleCollapse.Image = Properties.Resources.bullet_arrow_down;
                scMain.IsSplitterFixed = true;
                upperPanelSplitter = scMain.SplitterDistance;
                scMain.SplitterDistance = tsMain.Height;
            }
        }

        private void UpdateButtonStatesTest(bool running, bool paused)
        {
            ThreadingControlsHelper.SyncInvoke(
                this, () =>
                         {
                             dgvTable.Enabled = !running;

                             tsEditInput.Enabled = !running;
                             tsMain.Enabled = !running;

                             txtInput.Enabled = !running;
                             txtInput.HideSelection = !running;

                             tsbInterrupt.Enabled = running && !paused;
                             tsbRun.Enabled = !running || paused;
                             tsbStep.Enabled = tsbRun.Enabled;
                             tsbStop.Enabled = running;
                         });
        }

        private void UpdateTable()
        {
            foreach (string s in lstInput.Items)
            {
                if (!dgvTable.Columns.Contains(s))
                {
                    dgvTable.Columns.Add(s, s);
                }
            }

            for (int i = 0; i < lstInput.Items.Count; i++)
            {
                dgvTable.Columns[(string)lstInput.Items[i]].DisplayIndex = i + (tsbEpsilon.Checked ? 4 : 3);
                dgvTable.Columns[(string)lstInput.Items[i]].Width = 40;
            }
        }

        private void UpdateUILang()
        {
            dgvTable.Columns[0].HeaderText = resMan.GetString("State", currCult);
            dgvTable.Columns[1].HeaderText = resMan.GetString("Start", currCult);
            dgvTable.Columns[2].HeaderText = resMan.GetString("Accepted", currCult);

            gbInputs.Text = resMan.GetString("Inputs", currCult);

            tsbAddInput.Text = resMan.GetString("Add_Input", currCult);
            tsbDeleteInput.Text = resMan.GetString("Del_Input", currCult);

            tsbToggleCollapse.Text = resMan.GetString("Toggle_Collapse", currCult);
            tsbChangeLanguage.Text = resMan.GetString("Change_Lang", currCult);

            tsbSave.Text = resMan.GetString("Save", currCult);
            tsbLoad.Text = resMan.GetString("Load", currCult);
            tsbClear.Text = resMan.GetString("Clear", currCult);

            tsbBuildGraph.Text = resMan.GetString("Build", currCult);
            tsbExport.Text = resMan.GetString("Export", currCult);

            tsmiDFA.Text = resMan.GetString("DFA", currCult);
            tsmiDFA.ToolTipText = resMan.GetString("DFA_tt", currCult);
            tsmiNFA.Text = resMan.GetString("NFA", currCult);
            tsmiNFA.ToolTipText = resMan.GetString("NFA_tt", currCult);

            if (nondeterministic)
            {
                tsbFAMode.Text = tsmiNFA.Text;
                tsbFAMode.ToolTipText = tsmiNFA.ToolTipText;
            }
            else
            {
                tsbFAMode.Text = tsmiDFA.Text;
                tsbFAMode.ToolTipText = tsmiDFA.ToolTipText;
            }

            tsbFont.Text = resMan.GetString("Font", currCult);
            tsbSize.Text = resMan.GetString("Size", currCult);

            tpBuild.Text = resMan.GetString("Build_Tab", currCult);
            tpTest.Text = resMan.GetString("Test_Tab", currCult);

            tsbRun.Text = resMan.GetString("runTest", currCult);

            tsmiDFAExample.Text = resMan.GetString("DFAExample", currCult);
            tsmiNFAExample.Text = resMan.GetString("NFAExample", currCult);
            tsmiAbout.Text = resMan.GetString("About", currCult);
        }

		private void DumpRow(int y){
			for(int x = 0; x < dgvTable.ColumnCount; x++){
				try{
					DataGridViewCell tmpCell = dgvTable[x, y];
					Console.Write(" > " +(tmpCell==null?"no cell":(tmpCell.Value==null?"no value":tmpCell.Value.ToString())));
				}
				catch(Exception ex) {
					Console.WriteLine(ex);
				}
			}
			Console.WriteLine();
		}
		
        private void ValidateCell(int x, int y)
        {
			DumpRow(y);
			
            if (x > 2)
            {
                DataGridViewCell cell = dgvTable[x, y];
                string value = cell.Value as string;

                if (string.IsNullOrEmpty(value))
                    cell.Style.BackColor = SystemColors.Window;
                else
                    cell.Style.BackColor = IsValidEntry(value) ? Color.LightGreen : Color.LightSalmon;
            }
            else if (x == 0)
            {
                DataGridViewCell cell = dgvTable[x, y];
                string value = cell.Value as string;

                bool doubleOrEmpty = string.IsNullOrEmpty(value);

                if (!doubleOrEmpty)
                    for (int i = 0; i < dgvTable.Rows.Count - 1; i++)
                    {
                        if (i == y)
                            continue;

                        DataGridViewCell c = dgvTable[0, i];
                        string cVal = c.Value as string;
                        if (cVal == value)
                        {
                            c.Style.BackColor = Color.LightSalmon;
                            doubleOrEmpty = true;
                        }
                        else 
                            c.Style.BackColor = string.IsNullOrEmpty(cVal) ? Color.LightSalmon : Color.LightGreen;
                    }

                cell.Style.BackColor = doubleOrEmpty ? Color.LightSalmon : Color.LightGreen;
            }
        }
        #endregion

        #region EventHandler
        #region dgvTable
        private void dgvTable_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            ValidateCell(e.ColumnIndex, e.RowIndex);
        }

        private void dgvTable_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
			if (e.Row.Index > 0)
                for (int x = 0; x < dgvTable.ColumnCount; x++)
                    ValidateCell(x, e.Row.Index - 1);
        }
        #endregion

        #region lstInput
        private void lstInput_SelectedIndexChanged(object sender, EventArgs e)
        {
            tsbDeleteInput.Enabled = lstInput.SelectedItem != null;
        }
        #endregion

        #region ToolStripButtons
        private void tsbAddInput_Click(object sender, EventArgs e)
        {
            AddInput();
        }

        private void tsbBuildGraph_Click(object sender, EventArgs e)
        {
            Build();
        }

        private void tsbChangeLanguage_ButtonClick(object sender, EventArgs e)
        {
            tsbChangeLanguage.ShowDropDown();
        }

        private void tsbClear_Click(object sender, EventArgs e)
        {
            dgvTable.Rows.Clear();
            for (int i = dgvTable.Columns.Count - 1; i >= (tsbEpsilon.Checked ? 4 : 3); i--)
                dgvTable.Columns.RemoveAt(i);

            lstInput.Items.Clear();

            suMain.States = null;
        }

        private void tsbDeleteInput_Click(object sender, EventArgs e)
        {
            RemoveSelectedInput();
        }
       
        private void tsbEpsilon_Click(object sender, EventArgs e)
        {
            ShowEpsilon(!tsbEpsilon.Checked);
        }

        private void tsbExport_Click(object sender, EventArgs e)
        {
            if (suMain.States.Count == 0)
                Build();

            if (suMain.CanExport())
            {
                SaveFileDialog sfd = new SaveFileDialog
                                         {
                                             CheckPathExists = true,
                                             Filter = "Bitmap (*.bmp)|*.bmp|Portable Network Graphics (*.png)|*.png",
                                             OverwritePrompt = true
                                         };

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    FileStream fs = new FileStream(sfd.FileName, FileMode.Create, FileAccess.ReadWrite);

                    suMain.Export(fs, sfd.FileName.EndsWith("png", true, CultureInfo.CurrentCulture) ? ImageFormat.Png : ImageFormat.Bmp);
                    
                    fs.Flush();
                    fs.Close();
                }
            }
        }
        
        private void tsbFAMode_ButtonClick(object sender, EventArgs e)
        {
            tsbFAMode.ShowDropDown();
        }

        private void tsbFAMode_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (nondeterministic && e.ClickedItem == tsmiDFA)
                SetFAMode(!nondeterministic);

            if (!nondeterministic && e.ClickedItem == tsmiNFA)
                SetFAMode(!nondeterministic);
        }
        
        private void tsbFont_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog
                                {
                                    AllowSimulations = false,
                                    AllowVectorFonts = true,
                                    AllowVerticalFonts = false,
                                    Font = suMain.Font,
                                    FontMustExist = true,
                                    ShowColor = false,
                                    ShowEffects = false,
                                    ShowHelp = false
                                };

            if (fd.ShowDialog() == DialogResult.OK)
            {
                suMain.Font = fd.Font;
                suMain.Recalculate();
                suMain.Redraw(true);
            }
        }

        private void tsbInfo_ButtonClick(object sender, EventArgs e)
        {
            tsbInfo.ShowDropDown();
        }

        private void tsbInterrupt_Click(object sender, EventArgs e)
        {
            TestInterrupt();
        }

        private void tsbLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
                                     {
                                         CheckFileExists = true,
                                         CheckPathExists = true,
                                         Filter = "DrawEA-File (*.dfa, *.nfa)|*.dfa;*.nfa",
                                         Multiselect = false
                                     };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read);
                bool nfa_mode = ofd.FileName.EndsWith("nfa", true, currCult);

                LoadFrom(fs, nfa_mode);

                fs.Close();
            }
        }

        private void tsbRun_Click(object sender, EventArgs e)
        {
            TestStart(false);
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            Build();

            if (suMain.States.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog {CheckPathExists = true};
                if (nondeterministic)
                    sfd.Filter = "DrawEA-File (*.nfa)|*.nfa";
                else
                    sfd.Filter = "DrawEA-File (*.dfa)|*.dfa";
                sfd.OverwritePrompt = true;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    FileStream fs = new FileStream(sfd.FileName, FileMode.Create, FileAccess.ReadWrite);
                    XmlSerializer xSer = new XmlSerializer(typeof(List<State>));

                    xSer.Serialize(fs, new List<State>(suMain.States.Values));

                    fs.Flush();
                    fs.Close();
                }
            }
        }
        
        private void tsbSize_Click(object sender, EventArgs e)
        {
            frmSize fSz = new frmSize {Value = State.STATE_SIZE, Text = resMan.GetString("Size", currCult)};

            if (fSz.ShowDialog() == DialogResult.OK)
            {
                State.STATE_SIZE = fSz.Value;

                suMain.Recalculate();
                suMain.Redraw(true);
            }
        }
        
        private void tsbStep_Click(object sender, EventArgs e)
        {
            TestStart(true);
        }

        private void tsbStop_Click(object sender, EventArgs e)
        {
            TestStop();
        }

        private void tsbToggleCollapse_Click(object sender, EventArgs e)
        {
            ToggleCollapse();
        }
        #endregion

        #region ToolStripMenuItems
        private void tsmiAbout_Click(object sender, EventArgs e)
        {
            new frmAbout().ShowDialog(this);
        }

        private void tsmiDFAExample_Click(object sender, EventArgs e)
        {
            MemoryStream ms = new MemoryStream(Properties.Resources.DFA_Example_Output);
            LoadFrom(ms, false);
            ms.Close();
        }

        private void tsmiNFAExample_Click(object sender, EventArgs e)
        {
            MemoryStream ms = new MemoryStream(Properties.Resources.NFA_Example);
            LoadFrom(ms, true);
            ms.Close();
        }

        private void tsmiEnglish_Click(object sender, EventArgs e)
        {
            currCult = cultEn;
            UpdateUILang();
        }

        private void tsmiGerman_Click(object sender, EventArgs e)
        {
            currCult = cultDe;
            UpdateUILang();
        }
        #endregion

        #region tstInput
        private void tstInput_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (tsbAddInput.Enabled)
                    {
                        AddInput();
                        e.Handled = true;
                    }
                    break;
            }
        }

        private void tstInput_TextChanged(object sender, EventArgs e)
        {
            tsbAddInput.Enabled = tstInput.TextLength > 0 && !lstInput.Items.Contains(tstInput.Text);
        }
        #endregion

        private void State_SigmaStep(State sender, State.SigmaInterruptArgs e)
        {
            ThreadingControlsHelper.SyncInvoke(
                this, () =>
                         {
                             txtInput.SelectionStart = txtInput.TextLength -
                                                       e.RemainingWord.Length;
                             txtInput.SelectionLength = e.RemainingWord.Length;

                             txtOutput.Text = StateStackToString(e.StateStack);
                             txtOutput.AppendText(Environment.NewLine);
                             txtOutput.AppendText(
                                 resMan.GetString("Testresult_Output", currCult) + ": ");
                             txtOutput.AppendText(e.CurrentOutput);

                             suMain.TestState = sender;
                             Application.DoEvents();
                             Thread.Sleep(50);
                         });

            if (cancelTest)
            {
                suMain.TestState = null;
                UpdateButtonStatesTest(false, false);
                Thread.CurrentThread.Abort();
            }
            if (interruptTest)
            {
                UpdateButtonStatesTest(true, true);
                Thread.CurrentThread.Suspend();
            }
        }
        
        #endregion
    }
}