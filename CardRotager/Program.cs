using System;
using System.Text;
using System.Windows.Forms;

namespace CardRotager {
    static class Program {
        
        public static Localize localize;
        public static Logger log;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args) {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            localize = new Localize();
            localize.loadTranslatedText();
            log = new Logger(localize, new StringBuilder());
            
            if (args.Length > 0 && args[0] == "/changeImage") {
                Application.Run(new ProcessImageForm(log));
            } else {
                Application.Run(new MainForm(log));
            }
        }
    }
}