using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CardRotager {
    public class Localize {

        private readonly Dictionary<string, string> dictTranslate;

        private bool reverseTranslate;

        private static string lng = null;
        public Localize() {
            dictTranslate = new Dictionary<string, string>();
        }

        public String localize(String text) {
            return getTranslate(text);
        }

        private String getTranslate(string text) {
            if (lng == "ru-RU") {
                if (!dictTranslate.ContainsKey(text)) {
                    dictTranslate.Add(text, null);
                }
                return text;
            }
            if (reverseTranslate) {
                if (dictTranslate.ContainsValue(text)) {
                    string key = dictTranslate.FirstOrDefault(x => x.Value == text).Key;
                    if (key != null) {
                        return key;
                    }
                }
                return text;
            }
            if (dictTranslate.ContainsKey(text)) {
                if (dictTranslate[text] != null) {
                    return dictTranslate[text];
                }
            } else {
                dictTranslate.Add(text, null);
            }
            return text;
        }

        public void loadTranslatedText() {
            string path = getFullCurrentTranslateFileName();

            if (File.Exists(path)) {
                dictTranslate.Clear();
                string[] vs = File.ReadAllLines(getFullCurrentTranslateFileName());
                foreach (var item in vs) {
                    String escItem = fromTxt(item);
                    string[] vs1 = escItem.Split("=".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (vs1 != null && vs1.Length == 2) {
                        dictTranslate.Add(fromTextToDict(vs1[0]), fromTextToDict(vs1[1]));
                    }
                }
            }
        }

        public void saveCollectedLine() {
            StringBuilder sb = new StringBuilder();
            foreach (var item in dictTranslate) {
                sb.Append(toTxt(item.Key));
                if (!String.IsNullOrEmpty(item.Value)) {
                    sb.Append("=");
                    sb.Append(toTxt(item.Value));
                }
                sb.AppendLine();
            }
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Text file with text for translate (*.lng)|*.lng";
            dialog.InitialDirectory = getCurrentTranslateFilePath();
            dialog.FileName = getCurrentTranslateFileName();
            if (dialog.ShowDialog() == DialogResult.OK) {
                File.WriteAllText(dialog.FileName, sb.ToString());
            }
        }

        public void localizeControl(Control ctrl) {
            String translate = getTranslate(ctrl.Text);
            if (translate != null) {
                ctrl.Text = translate;
            } else {
                dictTranslate.Add(ctrl.Text, null);
            }
        }

        public void localizeControl(ToolStripMenuItem ctrl) {
            String translate = getTranslate(ctrl.Text);
            if (translate != null) {
                ctrl.Text = translate;
            } else {
                dictTranslate.Add(ctrl.Text, null);
            }
        }

        public void localizeControl(ToolStripButton ctrl) {
            String translate = getTranslate(ctrl.Text);
            if (translate != null) {
                ctrl.Text = translate;
            } else {
                dictTranslate.Add(ctrl.Text, null);
            }
        }

        public void localizeControl(Label ctrl) {
            String translate = getTranslate(ctrl.Text);
            if (translate != null) {
                ctrl.Text = translate;
            } else {
                dictTranslate.Add(ctrl.Text, null);
            }
        }

        private static string getEscString(string escItem, int startIndex, out int endIndex) {
            String key = null;
            endIndex = -1;
            if (startIndex != -1 && escItem.Substring(0, startIndex).Trim().Length == 0) {
                endIndex = escItem.IndexOf('"', startIndex + 1);
                if (endIndex != -1) {
                    key = escItem.Substring(startIndex + 1, endIndex - startIndex - 1);
                }
            }
            return key;
        }

        private static string getFullCurrentTranslateFileName() {
            string fileName = Path.Combine(getCurrentTranslateFilePath(), getCurrentTranslateFileName());
            if (File.Exists(fileName)) {
                return fileName;
            }
            string st = Path.Combine(getCurrentTranslateFilePath()+"\\..\\..\\..", getCurrentTranslateFileName());
            if (File.Exists(st)) {
                return st;
            }
            return fileName;
        }


        private string fromTxt(string text) {
            return text.Replace("\\r", "\r").Replace("\\n", "\n").Replace("\\=", "&equal;");
        }
        private string toTxt(string text) {
            return text.Replace("\r", "\\r").Replace("\n", "\\n").Replace("=", "\\=");
        }

        private string fromTextToDict(string text) {
            return text.Replace("&equal;", "=");
        }

        private static string getCurrentTranslateFilePath() {
            return Application.StartupPath;
        }

        public static string Language {
            get {
                if (lng == null) {
                    return Thread.CurrentThread.CurrentCulture.Name;
                }
                return lng;
            }
            set {
                lng = value;
            }
        }
        private static string getCurrentTranslateFileName() {
            return Language + ".lng";
        }
        public void revert(bool reverse) {
            reverseTranslate = reverse;
        }

        public void resetLoadTranslate() {
            dictTranslate.Clear();
        }
    }
}
