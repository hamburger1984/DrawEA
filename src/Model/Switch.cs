using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;

namespace DrawEA.Model
{
    [Serializable]
    public class Switch : IComparable
    {
        public const string EPSILON = "ε";
        string fromName, toName;

        public Switch() { }

        public Switch(State from, State to, string input, string output)
        {
            From = from;
            To = to;

            Input = input;
            Output = output;
        }

        [XmlIgnore]
        public State From { get; set; }

        public string FromName
        {
            get { return From == null ? fromName : From.Name; }
            set { fromName = value; }
        }

        public bool IsEpsilon()
        {
            return Input == EPSILON;
        }

        [XmlIgnore]
        public State To { get; set; }

        public string ToName
        {
            get { return To == null ? toName : To.Name; }
            set { toName = value; }
        }

        public string Input { get; set; }

        public string Output { get; set; }

        public List<Switch> GetEqualSwitches()
        {
            List<Switch> retVal = new List<Switch>();

            foreach (Switch s in From.Destinations)
                if (s == this || s.From == From && s.To == To)
                    retVal.Add(s);

            return retVal;
        }

        public double C1Offset { get; set; }

        public double C2Offset { get; set; }

        public float C1ROffset { get; set; }

        public float C2ROffset { get; set; }

        [XmlIgnore]
        public PointF Start { get; set; }

        [XmlIgnore]
        public PointF Control1 { get; set; }

        [XmlIgnore]
        public PointF Control2 { get; set; }

        [XmlIgnore]
        public PointF End { get; set; }

        [XmlIgnore]
        public PointF LabelLoc { get; set; }

        #region IComparable Members

        public int CompareTo(object value)
        {
            if (value == null)
                return 1;
            if (!(value is Switch))
                throw new ArgumentException("Must be a switch");

            Switch sw = (Switch)value;

            if (IsEpsilon())
            {
                if (!sw.IsEpsilon())
                    return 1;

                return Input.CompareTo(sw.Input);
            }
            if (sw.IsEpsilon())
                return -1;

            return Input.CompareTo(sw.Input);
        }

        #endregion
    }
}