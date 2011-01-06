using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using DrawEA.Model;

namespace DrawEA
{
    public class StateUI : ScrollableControl
    {
        Bitmap bmpBuffer;

        Point lastMouse, mouseDownPoint, mouseDownOffset;

        SortedDictionary<string, State> states = new SortedDictionary<string, State>();

        State testState;
        State selectedState;

        static readonly StringFormat sfState;
        static readonly StringFormat sfSwitch;

        static StateUI()
        {
            sfState = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            sfSwitch = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            //sfSwitch = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Far };
        }

        public StateUI()
        {
            AutoScroll = true;
            BackColor = Color.White;
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer|
                ControlStyles.ResizeRedraw |
                ControlStyles.StandardClick |
                ControlStyles.UserPaint,
                true);
        }

        public bool Recalculate()
        {
            if (states.Count == 0)
            {
                AutoScrollMinSize = new Size(10, 10);
                bmpBuffer = null;
                Invalidate();
                return false;
            }

            Size spaceNeeded = Size.Empty;

            foreach (State state in states.Values)
            {
                spaceNeeded.Width = Math.Max(state.Location.X + State.STATE_SIZE * 2, spaceNeeded.Width);
                spaceNeeded.Height = Math.Max(state.Location.Y + State.STATE_SIZE * 2, spaceNeeded.Height);

                state.CalculateSwitchPoints();
            }

            if (bmpBuffer == null || bmpBuffer.Size != spaceNeeded)
            {
                AutoScrollMinSize = spaceNeeded;
                bmpBuffer = new Bitmap(spaceNeeded.Width, spaceNeeded.Height);
                return true;
            }
            return false;
        }

        public void Redraw(bool highQuality)
        {
            if (bmpBuffer == null)
            {
                if(!Recalculate())
                    return;
            }

            using (Graphics g = Graphics.FromImage(bmpBuffer))
            {
                g.FillRectangle(Brushes.White, 0, 0, bmpBuffer.Width, bmpBuffer.Height);

                if (highQuality)
                {
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.InterpolationMode = InterpolationMode.Bicubic;
                    g.SmoothingMode = SmoothingMode.HighQuality;
                }
                else
                {
                    g.CompositingQuality = CompositingQuality.Default;
                    g.InterpolationMode = InterpolationMode.Default;
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                }

                using (Pen borderPen = new Pen(ForeColor, 4f))
                using (Pen switchOutline = new Pen(highQuality?Color.FromArgb(220,BackColor):BackColor, 5f))
                using (Pen switchPen = new Pen(ForeColor, 4f)
                                           {
                                               EndCap = LineCap.Custom,
                                               CustomEndCap = new AdjustableArrowCap(3, 3, true)
                                           })
                using (SolidBrush fill = new SolidBrush(BackColor))
                {
                    foreach (State state in states.Values)
                    {
                        state.Draw(g, fill, borderPen, switchPen, Font, sfState);
                        state.DrawAllSwitches(g, state.CenterPoint(), switchPen, switchOutline, Font, sfSwitch, false);
                    }
                }
            }

            Invalidate();
        }

        private Point CorrectedMousePosition(int x, int y)
        {
            return new Point(x - AutoScrollPosition.X, y - AutoScrollPosition.Y);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if ((e.Button & MouseButtons.Left) != MouseButtons.Left)
                return;

            Point mousePos = CorrectedMousePosition(e.X, e.Y);

            lastMouse = mousePos;

            if (SelectedState != null && SelectedState.StartMovingControlPoint(mousePos))
            {
                Cursor = Cursors.SizeAll;
                return;
            }

            foreach (State s in states.Values)
            {
                if (s.ContainsPoint(mousePos))
                {
                    mouseDownOffset = new Point(mousePos.X - s.Location.X, mousePos.Y - s.Location.Y);
                    mouseDownPoint = mousePos;
                    SelectedState = s;
                    Cursor = Cursors.SizeAll;
                    return;
                }
            }

            SelectedState = null;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if ((e.Button & MouseButtons.Left) != MouseButtons.Left)
                return;

            Point mousePos = CorrectedMousePosition(e.X, e.Y);

            if (Math.Abs(lastMouse.X - mousePos.X) < 5 && Math.Abs(lastMouse.Y - mousePos.Y) < 5)
                return;

            lastMouse = mousePos;

            if (SelectedState == null) 
                return;

            if (mouseDownPoint != Point.Empty)
            {
                SelectedState.Location = new Point(mousePos.X - mouseDownOffset.X, mousePos.Y - mouseDownOffset.Y);
                Recalculate();
                Redraw(false);
            }
            else if (SelectedState.ContinueMovingControlPoint(
                Math.Max(5, Math.Min(AutoScrollMinSize.Width, mousePos.X)),
                Math.Max(5, Math.Min(AutoScrollMinSize.Height, mousePos.Y))))
            {
                Recalculate();
                Redraw(false);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if ((e.Button & MouseButtons.Left) != MouseButtons.Left)
                return;

            Point mousePos = CorrectedMousePosition(e.X, e.Y);

            if (SelectedState == null)
                return;

            if (mouseDownPoint != Point.Empty)
            {
                Cursor = Cursors.Default;
                SelectedState.Location = new Point(mousePos.X - mouseDownOffset.X, mousePos.Y - mouseDownOffset.Y);
                Recalculate();
                Redraw(true);
                mouseDownPoint = Point.Empty;
            }
            else if (SelectedState.EndMovingControlPoint(
                Math.Max(5, Math.Min(AutoScrollMinSize.Width, mousePos.X)),
                Math.Max(5, Math.Min(AutoScrollMinSize.Height, mousePos.Y))))
            {
                Cursor = Cursors.Default;
                Recalculate();
                Redraw(true);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (bmpBuffer != null)
            {
                Graphics g = e.Graphics;
                g.TranslateTransform(AutoScrollPosition.X, AutoScrollPosition.Y);
                g.DrawImageUnscaled(bmpBuffer, 0,0);

                if (testState == null && selectedState == null) 
                    return;

                if (selectedState != null)
                    using (SolidBrush shadow = new SolidBrush(Color.FromArgb(120, Color.White)))
                        g.FillRectangle(shadow, 0, 0, bmpBuffer.Width, bmpBuffer.Height);

                g.CompositingQuality = CompositingQuality.Default;
                g.InterpolationMode = InterpolationMode.Bilinear;
                g.SmoothingMode = SmoothingMode.HighQuality;

                using (Pen borderPen = new Pen(ForeColor, 4f))
                //using (Pen switchOutline = new Pen(Color.FromArgb(220,BackColor), 5f))
                using (Pen switchOutline = new Pen(BackColor, 5f))
                using (Pen switchPen = new Pen(ForeColor, 4f)
                                           {
                                               EndCap = LineCap.Custom,
                                               CustomEndCap = new AdjustableArrowCap(3, 3, true)
                                           })
                using (SolidBrush fillSelected = new SolidBrush(Color.LightSalmon))
                {
                    switchPen.DashStyle = DashStyle.Dash;

                    if (testState != null)
                        testState.Draw(g, fillSelected, borderPen, switchPen, Font, sfState);
                    else if (selectedState != null)
                    {
                        selectedState.Draw(g, fillSelected, borderPen, switchPen, Font, sfState);
                        selectedState.DrawAllSwitches(g, selectedState.CenterPoint(), switchPen, switchOutline, Font, sfSwitch, true);
                    }
                }
            }
        }

        [Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SortedDictionary<string, State> States
        {
            get { return states; }
            set {
                if (value == null)
                    states.Clear();
                else
                    states = value;
                selectedState = null;
                testState = null;
                Recalculate();
                Redraw(true);
            }
        }

        [Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public State TestState
        {
            get { return testState; }
            set {
                if (testState != value)
                {
                    testState = value;
                    Invalidate();
                }
            }
        }

        [Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public State SelectedState
        {
            get { return selectedState; }
            set {
                if (selectedState != value)
                {
                    selectedState = value;
                    Invalidate();
                }
            }
        }

        public bool CanExport()
        {
            return bmpBuffer != null && states.Count > 0;
        }

        public void Export(FileStream fs, ImageFormat format)
        {
            if (CanExport())
                bmpBuffer.Save(fs, format);
        }
    }
}