using System.Text;

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

        public void AppendLine(string text) {
            sb.AppendLine(text);
        }

        public string ConvertToString() {
            return sb.ToString();
        }

        public void resetStringBuilder() {
            sb.Clear();
        }
    }
}