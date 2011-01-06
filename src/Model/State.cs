using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DrawEA.Model
{
    [Serializable]
    public class State : IComparable
    {
        #region Fields

        public static int STATE_SIZE = 50;
        Switch cMovingSwitch;
        bool cMovingFirst;

        public static event SigmaInterruptHandler SigmaStep;

        #region SigmaInterruptEvent
        public delegate void SigmaInterruptHandler(State sender, SigmaInterruptArgs e);

        public class SigmaInterruptArgs : CancelEventArgs
        {
            readonly string word;
            readonly Stack<State> way;
            readonly Stack<string> output;

            public SigmaInterruptArgs(string word, Stack<State> way, Stack<string> output)
            {
                this.word = word;
                this.way = way;
                this.output = output;
            }

            public string RemainingWord
            {
                get { return word; }
            }

            public string CurrentOutput
            {
                get
                {
                    List<string> outputReversed = new List<string>(output.ToArray());
                    outputReversed.Reverse();
                    return string.Join(string.Empty, outputReversed.ToArray());
                }
            }

            public Stack<State> StateStack
            {
                get { return way; }
            }
        }
        #endregion
        #endregion

        #region Constructor
        public State()
        {
            Destinations = new List<Switch>();
            Sources = new List<Switch>();
        }

        public State(string name, bool isAccepted, bool isStart)
            : this()
        {
            Name = name;
            IsAccepted = isAccepted;
            IsStart = isStart;
        }
        #endregion

        #region Properties

        public List<Switch> Destinations { get; set; }

        public List<Switch> Sources { get; set; }

        public string Name { get; set; }

        public bool IsAccepted { get; set; }

        public bool IsStart { get; set; }

        private Point location;
        public Point Location
        {
            get { return location; }
            set
            {
                location.X = Math.Max(0, value.X);
                location.Y = Math.Max(0, value.Y);
            }
        }

        #endregion

        #region Methods
        public PointF CalculateCenterBezierPoint(PointF start, PointF control1, PointF control2, PointF end)
        {
            // cubic bezier:
            // B(t) = (1-t)^3*P_0 + 3t(1-t)^2*P_1 + 3t^2(1-t)*P_2 + t^3*P_3; 0 <= t <= 1

            // label should reside in the middle --> t = (1-t) = 0.5
            const float tCubed = 0.125f; // (1-0.5)^3 = 0.5(1-0.5)^2 = 0.5^2(1-0.5) = 0.5^3 = 0.125

            float cx = tCubed*start.X + 3.0f*tCubed*control1.X + 3.0f*tCubed*control2.X + tCubed*end.X;
            float cy = tCubed*start.Y + 3.0f*tCubed*control1.Y + 3.0f*tCubed*control2.Y + tCubed*end.Y;

            return new PointF(cx, cy);

            //float ax, bx, cx;
            //float ay, by, cy;

            //cx = 3.0f * (control1.X - start.X);
            //bx = 3.0f * (control2.X - control1.X) - cx;
            //ax = end.X - start.X - cx - bx;

            //cy = 3.0f * (control1.Y - start.Y);
            //by = 3.0f * (control2.Y - control1.Y) - cy;
            //ay = end.Y - start.Y - cy - by;

            //return new PointF(
            //    (ax * tCubed) + (bx * tSquared) + (cx * 0.5f) + start.X,
            //    (ay * tCubed) + (by * tSquared) + (cy * 0.5f) + start.Y);
        }

        public void CalculateSwitchPoints()
        {
            List<Switch> processedSwitches = new List<Switch>();

            for (int i = 0; i < Destinations.Count; i++)
            {
                if (processedSwitches.Contains(Destinations[i]))
                    continue;

                List<Switch> list = Destinations[i].GetEqualSwitches();

                bool anyWayBack = false;

                foreach (Switch sw in list)
                {
                    if (sw.To != this)
                        foreach (Switch wb in sw.To.Destinations)
                            if (wb.To == this)
                            {
                                anyWayBack = true;
                                break;
                            }

                    processedSwitches.Add(sw);
                }

                Point center = CenterPoint();
                PointF start, c1, c2, end, labelLoc;
                
                CalculateSwitchPoint(out start, out c1, out c2, out end, out labelLoc,
                    center, Destinations[i].To == this ? center : Destinations[i].To.CenterPoint(), anyWayBack,
                    Destinations[i].C1Offset, Destinations[i].C2Offset, Destinations[i].C1ROffset, Destinations[i].C2ROffset);

                foreach (Switch sw in list)
                {
                    sw.Start = start;
                    sw.Control1 = c1;
                    sw.Control2 = c2;
                    sw.End = end;
                    sw.LabelLoc = labelLoc;
                }
            }
        }

        private void CalculateSwitchPoint(out PointF start, out PointF control1, out PointF control2, out PointF end, out PointF labelLoc,
            Point center1, Point center2, bool anyWayBack, double c1Offset, double c2Offset, float c1ROffset, float c2ROffset)
        {
            bool noStateChange = center1 == center2;
            double alpha, offset;
            float r = STATE_SIZE / 2 + 4;
            int distX = center2.X - center1.X;
            int distY = center2.Y - center1.Y;

            if (noStateChange) {
                alpha = Math.PI * (270.0 / 180.0);
                offset = Math.PI * (20.0 / 180.0);
            }
            else {
                alpha = Math.Atan2(distY, distX);
                offset = anyWayBack ? Math.PI * (15.0 / 180.0) : 0.0;
            }

            start = new PointF(
                (float)(center1.X + r * Math.Cos(alpha - offset + c1Offset)),
                (float)(center1.Y + r * Math.Sin(alpha - offset + c1Offset)));
            control1 = new PointF(
                (float)(center1.X + (3 * r + c1ROffset) * Math.Cos(alpha - offset + c1Offset)),
                (float)(center1.Y + (3 * r + c1ROffset) * Math.Sin(alpha - offset + c1Offset)));
            control2 = new PointF(
                (float)(center2.X + (3 * r + c2ROffset) * Math.Cos(alpha + offset + c2Offset + (noStateChange ? 0.0 : Math.PI))),
                (float)(center2.Y + (3 * r + c2ROffset) * Math.Sin(alpha + offset + c2Offset + (noStateChange ? 0.0 : Math.PI))));
            end = new PointF(
                (float)(center2.X + r * Math.Cos(alpha + offset + c2Offset + (noStateChange ? 0.0 : Math.PI))),
                (float)(center2.Y + r * Math.Sin(alpha + offset + c2Offset + (noStateChange ? 0.0 : Math.PI))));

            labelLoc = CalculateCenterBezierPoint(start, control1, control2, end);
        }

        public Point CenterPoint()
        {
            return new Point(Location.X + STATE_SIZE / 2, Location.Y + STATE_SIZE / 2);
        }

        public bool ContainsPoint(Point p)
        {
            return ContainsPoint(p.X, p.Y);
        }

        public bool ContainsPoint(int x, int y)
        {
            return new Rectangle(Location, new Size(STATE_SIZE, STATE_SIZE)).Contains(x, y);
        }

        public bool StartMovingControlPoint(Point p)
        { return StartMovingControlPoint(p.X, p.Y); }

        public bool StartMovingControlPoint(int x, int y)
        {
            foreach (Switch sw in Destinations)
            {
                if (new RectangleF(sw.Control1.X - 6, sw.Control1.Y - 6, 12, 12).Contains(x, y))
                {
                    cMovingFirst = true;
                    cMovingSwitch = sw;
                    return true;
                }
                if (new RectangleF(sw.Control2.X - 6, sw.Control2.Y - 6, 12, 12).Contains(x, y))
                {
                    cMovingFirst = false;
                    cMovingSwitch = sw;
                    return true;
                }
            }

            foreach (Switch sw in Sources)
            {
                if (new RectangleF(sw.Control1.X - 6, sw.Control1.Y - 6, 12, 12).Contains(x, y))
                {
                    cMovingFirst = true;
                    cMovingSwitch = sw;
                    return true;
                }
                if (new RectangleF(sw.Control2.X - 6, sw.Control2.Y - 6, 12, 12).Contains(x, y))
                {
                    cMovingFirst = false;
                    cMovingSwitch = sw;
                    return true;
                }
            }
            cMovingSwitch = null;
            return false;
        }

        public bool ContinueMovingControlPoint(Point p)
        { return ContinueMovingControlPoint(p.X, p.Y); }

        public bool ContinueMovingControlPoint(int x, int y)
        {
            if (cMovingSwitch == null)
                return false;

            List<Switch> list = cMovingSwitch.GetEqualSwitches();

            double alphaOffset;
            float rOffset;

            if (cMovingFirst)
            {
                CalculateSwitchMovement(out alphaOffset, out rOffset, cMovingSwitch.Start, cMovingSwitch.Control1, x, y);

                foreach (Switch sw in list)
                {
                    sw.C1Offset += alphaOffset;
                    sw.C1ROffset += rOffset;
                }
            }
            else
            {
                CalculateSwitchMovement(out alphaOffset, out rOffset, cMovingSwitch.End, cMovingSwitch.Control2, x, y);

                foreach (Switch sw in list)
                {
                    sw.C2Offset += alphaOffset;
                    sw.C2ROffset += rOffset;
                }
            }

            return true;
        }

        public bool EndMovingControlPoint(Point p)
        { return EndMovingControlPoint(p.X, p.Y); }

        public bool EndMovingControlPoint(int x, int y)
        {
            //if (cMovingSwitch == null)
            //    return false;

            //List<Switch> list = cMovingSwitch.GetEqualSwitches();

            //double alphaOffset;
            //float rOffset;

            //if (cMovingFirst)
            //{
            //    CalculateSwitchMovement(out alphaOffset, out rOffset, cMovingSwitch.Start, cMovingSwitch.Control1, x, y);

            //    foreach (Switch sw in list)
            //    {
            //        sw.C1Offset += alphaOffset;
            //        sw.C1ROffset += rOffset;
            //    }
            //}
            //else
            //{
            //    CalculateSwitchMovement(out alphaOffset, out rOffset, cMovingSwitch.End, cMovingSwitch.Control2, x, y);

            //    foreach (Switch sw in list)
            //    {
            //        sw.C2Offset += alphaOffset;
            //        sw.C2ROffset += rOffset;
            //    }
            //}

            if (!ContinueMovingControlPoint(x, y))
                return false;

            cMovingSwitch = null;
            return true;
        }

        private static void CalculateSwitchMovement(out double alphaOffset, out float rOffset, PointF start, PointF controlOld, int newX, int newY)
        {
            double distX1, distY1, distX2, distY2;
            distX1 = controlOld.X - start.X;
            distY1 = controlOld.Y - start.Y;
            distX2 = newX - start.X;
            distY2 = newY - start.Y;

            double alpha1 = Math.Atan2(distY1, distX1),
                alpha2 = Math.Atan2(distY2, distX2);
            double r1 = Math.Sqrt(distX1*distX1 + distY1*distY1),
                r2 = Math.Sqrt(distX2*distX2 + distY2*distY2);

            alphaOffset = alpha2 - alpha1;
            rOffset = (float)(r2 - r1);
        }

        public void Draw(Graphics g, Brush backBrush, Pen borderPen, Pen switchPen, Font f, StringFormat sf)
        {
            Rectangle rect = new Rectangle(Location, new Size(STATE_SIZE, STATE_SIZE));
            Point center = CenterPoint();

            g.FillEllipse(backBrush, rect);

            g.DrawEllipse(borderPen, rect);

            if (IsAccepted)
                using (Pen thinBorderPen = new Pen(borderPen.Color))
                    g.DrawEllipse(thinBorderPen, rect.X + 4, rect.Y + 4, rect.Width - 8, rect.Height - 8);

            if (IsStart)
                g.DrawLine(switchPen, Location.X - STATE_SIZE*0.75f, center.Y, Location.X - 4, center.Y);

            g.DrawString(Name, f, Brushes.Black, center.X, center.Y, sf);
        }

        public void DrawAllSwitches(Graphics g,Point center, Pen switchPen, Pen switchOutline, Font f, StringFormat sf, bool drawHandles)
        {
            List<Switch> processedSwitches = new List<Switch>();

            for (int i = 0; i < Destinations.Count; i++)
            {
                if (processedSwitches.Contains(Destinations[i]))
                    continue;

                List<Switch> list = Destinations[i].GetEqualSwitches();

                string label = string.Empty;

                foreach (Switch sw in list)
                {
                    processedSwitches.Add(sw);
                    if (label.Length > 0)
                        label += ", ";

                    label += string.IsNullOrEmpty(sw.Output) ? sw.Input : sw.Input + "/" + sw.Output;
                }

                DrawSwitches(g, switchPen, switchOutline, f, sf, label, drawHandles, 
                    Destinations[i].Start, Destinations[i].Control1, Destinations[i].Control2, Destinations[i].End, Destinations[i].LabelLoc);
            }

            if (drawHandles)
            {
                processedSwitches.Clear();
                
                for (int i = 0; i < Sources.Count; i++)
                {
                    if (processedSwitches.Contains(Sources[i]))
                        continue;

                    List<Switch> list = Sources[i].GetEqualSwitches();

                    string label = string.Empty;

                    foreach (Switch sw in list)
                    {
                        processedSwitches.Add(sw);
                        if (label.Length > 0)
                            label += ", ";

                        label += string.IsNullOrEmpty(sw.Output) ? sw.Input : sw.Input + "/" + sw.Output;
                    }

                    DrawSwitches(g, switchPen, switchOutline, f, sf, label, drawHandles,
                                 Sources[i].Start, Sources[i].Control1, Sources[i].Control2,
                                 Sources[i].End, Sources[i].LabelLoc);
                }
            }
        }

        public void DrawSwitches(Graphics g, Pen switchPen, Pen switchOutline, Font f, StringFormat sf, string label, bool drawHandles,
            PointF start, PointF control1, PointF control2, PointF end, PointF labelLoc)
        {
            PointF[] bezier = new [] {start, control1, control2, end};

            g.DrawBeziers(switchOutline, bezier);
            g.DrawBeziers(switchPen, bezier);

            using (GraphicsPath gp = new GraphicsPath())
            {
                gp.AddString(label, f.FontFamily, (int) f.Style, f.GetHeight(g), labelLoc, sf);

                g.DrawPath(switchOutline, gp); 
                g.FillPath(Brushes.Black, gp);
            }

            if (!drawHandles) return;

            DrawBezierHandle(g, start, control1);
            DrawBezierHandle(g, end, control2);
        }

        private static void DrawBezierHandle(Graphics g, PointF p, PointF c)
        {
            g.DrawLine(Pens.Blue, p, c);
            g.FillEllipse(Brushes.White, c.X - 5, c.Y - 5, 10, 10);
            g.DrawEllipse(Pens.Black, c.X - 5, c.Y - 5, 10, 10);
        }

        public bool CanAccept()
        {
            if (IsAccepted)
                return true;

            StackTrace st = new StackTrace();
            if (st.FrameCount > 500)
                return false;

            foreach (Switch sw in Destinations)
                if (sw.To != this && !sw.To.IsStart && sw.To.CanAccept())
                    return true;

            return false;
        }

        public bool Sigma(string word, ref Stack<State> way, ref Stack<string> output)
        {
            way.Push(this);

            OnSigmaStep(this, new SigmaInterruptArgs(word, way, output));

            if (word.Length == 0 && IsAccepted)
                return true;

            foreach (Switch sw in Destinations)
            {
                if(!string.IsNullOrEmpty(sw.Output))
                    output.Push(sw.Output);

                if (word.StartsWith(sw.Input) && sw.To.Sigma(word.Substring(sw.Input.Length), ref way, ref output))
                    return true;
                if (sw.Input == Switch.EPSILON && sw.To.Sigma(word, ref way, ref output))
                    return true;

                if (!string.IsNullOrEmpty(sw.Output))
                    output.Pop();
            }

            way.Pop();
            return false;
        }

        private static void OnSigmaStep(State sender, SigmaInterruptArgs e)
        {
            if (SigmaStep != null)
                SigmaStep(sender, e);
        }

        #endregion

        #region IComparable Members

        public int CompareTo(object value)
        {
            if (value == null)
                return 1;
            if (!(value is State))
                throw new ArgumentException("Must be a state");

            State state = (State) value;

            if (IsStart)
            {
                if (!state.IsStart)
                    return 1;

                return Name.CompareTo(state.Name);
            }

            if (state.IsStart)
                return -1;

            return Name.CompareTo(state.Name);
        }

        #endregion
    }
}