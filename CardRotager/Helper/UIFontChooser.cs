using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace CardRotager {
    public class UIFontChooser : UITypeEditor {

        private Logger log;
        public UIFontChooser(Logger log) {
            this.log = log;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value) {
            FontDialog dlg = new FontDialog();
            dlg = new FontDialog();

            Font font = value as Font;
            if (font == null) {
                string st = value as String;
                font = createFont(st, null, log);
            }
            if (font != null) {
                dlg.Font = font;
            }

            if (dlg.ShowDialog() == DialogResult.OK) {
                return new FontConverter().ConvertToString(dlg.Font);
            }

            return base.EditValue(context, provider, value);
        }
        
        public static Font createFont(string settingsDrawTextTargetFont, Font captionFont, Logger log) {
            if (!string.IsNullOrEmpty(settingsDrawTextTargetFont)) {
                try {
                    return getFont(settingsDrawTextTargetFont);
                } catch (Exception ex) {
                    log?.AppendFormat(log?.l("Не удалось создать шрифт из строки '{0}'. Будет использован шрифт заголовка из темы Windows"), settingsDrawTextTargetFont, ex.Message);
                }
            }
            return captionFont;
        }

        private static Font getFont(string fontDesc) {
            return (Font) new FontConverter().ConvertFromString(fontDesc);
        }

    }
}