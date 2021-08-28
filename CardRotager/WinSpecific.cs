using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CardRotager {
    public static class WinSpecific {

        [DllImport("Psapi.dll")]
        static extern bool EmptyWorkingSet(IntPtr hProcess);

        #region Ожидание (часики)
        [DllImport("User32.dll")]
        private static extern int SetCursor(int hCursor);
        [DllImport("User32.dll")]
        private static extern int LoadCursorA(int hInstance, int lpCursorName);


        private const int IDC_WAIT = 0x7f02;
        private const int IDC_ARROW = 0x7f00;

        private static bool _UseWaitCursor = false;

        /// <summary>
        /// Установка курсора ожидания 
        /// <remarks>(сразу же после вызова, а не как у стандартный Application.UseWaitCursor)</remarks>
        /// </summary>
        public static bool UseWaitCursor {
            get { return _UseWaitCursor; }
            set {
                _UseWaitCursor = value;
                if (_UseWaitCursor)
                    SetCursor(LoadCursorA(0, IDC_WAIT));
                else
                    SetCursor(LoadCursorA(0, IDC_ARROW));
            }
        }
        #endregion

        /// <summary>
        /// принудительная очистка памяти
        /// </summary>
        public static void clearMemory() {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            EmptyWorkingSet(Process.GetCurrentProcess().Handle);
        }

    }

}