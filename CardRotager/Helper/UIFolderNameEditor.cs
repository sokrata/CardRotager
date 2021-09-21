using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CardRotager {
    public class UIFolderNameEditor : UITypeEditor {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value) {
            UIFolderBrowser browser = new UIFolderBrowser();
            if (value != null) {
                browser.DirectoryPath = string.Format("{0}", value);
            }
            if (browser.ShowDialog(null) == DialogResult.OK) {
                return browser.DirectoryPath;
            }

            return value;
        }
    }

    public class UIFolderBrowser {
        public string DirectoryPath { get; set; }

        public DialogResult ShowDialog(IWin32Window owner) {
            IntPtr hwndOwner = owner != null ? owner.Handle : WinSpecific.GetActiveWindow();

            WinSpecific.IFileOpenDialog dialog = (WinSpecific.IFileOpenDialog) new WinSpecific.FileOpenDialog();
            try {
                WinSpecific.IShellItem item;
                if (!string.IsNullOrEmpty(DirectoryPath)) {
                    IntPtr idl;
                    uint atts = 0;
                    if (WinSpecific.SHILCreateFromPath(DirectoryPath, out idl, ref atts) == 0) {
                        if (WinSpecific.SHCreateShellItem(IntPtr.Zero, IntPtr.Zero, idl, out item) == 0) {
                            dialog.SetFolder(item);
                        }
                        Marshal.FreeCoTaskMem(idl);
                    }
                }
                dialog.SetOptions(WinSpecific.FOS.FOS_PICKFOLDERS | WinSpecific.FOS.FOS_FORCEFILESYSTEM);
                uint hr = dialog.Show(hwndOwner);
                if (hr == WinSpecific.ERROR_CANCELLED)
                    return DialogResult.Cancel;

                if (hr != 0)
                    return DialogResult.Abort;

                dialog.GetResult(out item);
                string path;
                item.GetDisplayName(WinSpecific.SIGDN.SIGDN_FILESYSPATH, out path);
                DirectoryPath = path;
                return DialogResult.OK;
            } finally {
                Marshal.ReleaseComObject(dialog);
            }
        }
    }
}