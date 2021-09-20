using System.Collections.Generic;

namespace CardRotager {
    public class HVLine {
        public Edge HLine { get; set; }
        public Edge VLine { get; set; }

        public HVLine(Edge hLine, Edge vLine) {
            VLine = vLine;
            HLine = hLine;
        }

        public override string ToString() {
            return string.Format("hLine = {0}, vLine = {1}", HLine, VLine);
        }
    }
}