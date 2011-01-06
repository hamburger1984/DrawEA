using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DrawEA.Model;
using System.Drawing.Drawing2D;

namespace DrawEA
{
    internal class frmAbout : Form
    {
        public frmAbout()
        {
            ControlBox = false;
            MinimizeBox = false;
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            StartPosition = FormStartPosition.CenterParent;
            ShowInTaskbar = false;
            Size = new Size(300,250);
            BackColor = Color.White;

            initControls();
        }

        private void initControls()
        {
            RichTextBox rtb = new RichTextBox();

            rtb.AppendText("DrawEA\n");
            rtb.SelectAll();
            rtb.SelectionFont = new Font(Font.FontFamily, Font.Size*2, FontStyle.Bold);
            
            rtb.SelectionLength = 0;
            rtb.SelectionStart = rtb.Text.Length;
            rtb.AppendText("Version " + Application.ProductVersion);
            rtb.SelectionLength = rtb.Text.Length - rtb.SelectionStart;
            rtb.SelectionFont = new Font(Font, FontStyle.Italic);

            rtb.ReadOnly = true;
            rtb.BackColor = Color.White;
            rtb.BorderStyle = BorderStyle.None;
            rtb.Location = new Point(50, 10);
            rtb.Size = new Size(200, 50);

            Controls.Add(rtb);

            Button close = new Button();

            close.Text = "x";
            close.Size = new Size(20, 20);
            close.Location = new Point(275, 5);

            close.Click += delegate { Close(); };

            Controls.Add(close);

            LinkLabel lblMail = new LinkLabel();

            lblMail.Text = "© 2006-2008 Andreas K.";
            lblMail.LinkArea = new LinkArea(12, 10);
            lblMail.LinkBehavior = LinkBehavior.HoverUnderline;
            lblMail.LinkClicked += delegate { System.Diagnostics.Process.Start("mailto:\"Andreas Krohn\"<Hamburger1984@gmail.com>?subject=DrawEA%20Version%20"+Application.ProductVersion); };

            lblMail.AutoSize = true;
            lblMail.Dock = DockStyle.Bottom;

            Controls.Add(lblMail);

            Bitmap bmp = new Bitmap(300, 190);
            Graphics g = Graphics.FromImage(bmp);

            int oldSize = State.STATE_SIZE;
            State.STATE_SIZE = 40;

            State A = new State("A", false, true);
            State B = new State("B", true, false);
            State C = new State("C", false, false);

            A.Destinations.Add(new Switch(A, B, "1", "done"));
            A.Destinations.Add(new Switch(A, C, "0", string.Empty));
            B.Destinations.Add(new Switch(B, B, "0", string.Empty));
            B.Destinations.Add(new Switch(B, B, "1", string.Empty));
            C.Destinations.Add(new Switch(C, A, "1", string.Empty));
            C.Destinations.Add(new Switch(C, C, "0", string.Empty));

            A.Location = new Point(40, 40);
            B.Location = new Point(230, 60);
            C.Location = new Point(160, 140);

            A.CalculateSwitchPoints();
            B.CalculateSwitchPoints();
            C.CalculateSwitchPoints();

            StringFormat sf = new StringFormat {Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center};

            using (Pen borderPen = new Pen(ForeColor, 4f))
            using (Pen switchOutline = new Pen(Color.FromArgb(220, BackColor), 5f))
            using (Pen switchPen = new Pen(ForeColor, 4f)
                                       {
                                           EndCap = LineCap.Custom,
                                           CustomEndCap = new AdjustableArrowCap(3, 3, true)
                                       })
            {
                g.FillRectangle(Brushes.White, 0, 0, bmp.Width, bmp.Height);

                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBilinear;
                g.SmoothingMode = SmoothingMode.HighQuality;
               
                A.Draw(g, Brushes.White, borderPen, switchPen, Font, sf);
                B.Draw(g, Brushes.White, borderPen, switchPen, Font, sf);
                C.Draw(g, Brushes.White, borderPen, switchPen, Font, sf);
                A.DrawAllSwitches(g, A.CenterPoint(), switchPen, switchOutline, Font, sf, false);
                B.DrawAllSwitches(g, B.CenterPoint(), switchPen, switchOutline, Font, sf, true);
                C.DrawAllSwitches(g, C.CenterPoint(), switchPen, switchOutline, Font, sf, false);
            }

            State.STATE_SIZE = oldSize;

            PictureBox pb = new PictureBox();
            pb.SizeMode = PictureBoxSizeMode.AutoSize;
            pb.Image = bmp;
            pb.Location = new Point(0,60);

            Controls.Add(pb);

            close.Select();
        }
    }
}
