using System;
using System.Text;
using System.Windows.Forms;

namespace CardRotager {
    public class Logger {
        private StringBuilder sb;
        private Localize localize;

        public Logger(Localize localize, StringBuilder sb) {
            this.localize = localize;
            this.sb = sb;
        }

        public void AppendFormat(string format, params object[] args) {
            sb.AppendFormat(format, args);
        }

        public string l(string text) {
            return localize.localize(text);
        }
        
        public string l(string text, params object[] args) {
            return string.Format(localize.localize(text), args);
        }

        public void AppendLine(string text) {
            sb.AppendLine(text);
        }

        public string ConvertToString() {
            return sb.ToString();
        }

        public void resetStringBuilder() {
            sb.Clear();
        }

        public static void error(string msg, string arg1, Exception ex) {
            MessageBox.Show(string.Format(msg, arg1, ex.Message), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}