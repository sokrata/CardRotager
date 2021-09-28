using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace CardRotager {
    public partial class ProcessImageForm : Form {
        public const string main = "main";
        private const string DpiY = "dpiY";
        private const string DpiX = "dpiX";
        private const string newDpiY = "newDpiY";
        private const string newDpiX = "newDpiX";
        private const string imageWidth = "imageWidth";
        private const string imageHeight = "imageHeight";
        private const string xmlFullFileName = "fullFileName";
        private const string XPath = "/settings";
        public string AppSettingFileName { get; } = Path.Combine(Application.UserAppDataPath, "setting2.xml");

        private Logger logger;
        private PropertyObject prop;
        private List<string> openFileNames;
        public ProcessImageForm(Logger logger) {
            InitializeComponent();
            this.logger = logger;

            prop = new PropertyObject();
            prop.addParam(main, DpiX, 0, false);
            prop.addParam(main, DpiY, 0, false);
            prop.addParam(main, newDpiX, 300, true);
            prop.addParam(main, newDpiY, 300, true);
            prop.addParam(main, imageWidth, 0, false);
            prop.addParam(main, imageHeight, 0, false);
            prop.addParam(main, xmlFullFileName, "{fno}_out{ext}", true, logger.l("Путь и имя сохраняемого файла (пример: {fno} сохраняет как имя файла без расширения). Доступны автозамены: {fn} - имя главного файла, {fno} - имя глав.файла без расширения и точки, {ext} - точка и расширение файла"), browsable: true, readOnly: false, editor: new UIFolderNameEditor());
            propertyGrid1.SelectedObject = prop;
            openFileNames = new List<string>();
        }

        private void Form2_Load(object sender, EventArgs e) {
            pbSource.Image = null;
            prop.loadFromXml(AppSettingFileName,XPath);
        }

        private void pictureBox1_Click(object sender, EventArgs e) {
            if (pbSource.Image == null) {
                openFile();
            }
        }

        private void menuOpenResolutionItem_Click(object sender, EventArgs e) {
            openFile();
        }

        private void openFile() {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                if (openFileDialog.FileNames.Length > 1) {
                }
                openFileNames.Clear();
                openFileNames.AddRange(openFileDialog.FileNames);
                Bitmap originalBitmap = new Bitmap(openFileNames[0]);
                using (Graphics gr = Graphics.FromImage(originalBitmap)) {
                    prop[DpiX].Value = (int) gr.DpiX;
                    prop[DpiY].Value = (int) gr.DpiY;
                }
                propertyGrid1.SelectedObject = prop;
                prop[imageWidth].Value = originalBitmap.Width;
                prop[imageHeight].Value = originalBitmap.Height;
                pbSource.Image = originalBitmap;
                lbSource.Visible = false;
                pbSource.Visible = true;
            }
        }

        private void processFile() {
            if (pbSource.Image == null) {
                return;
            }
            int xDpi = prop.i(newDpiX);
            int yDpi = prop.i(newDpiY);
            pbTarget.Image = processBatchFile(pbSource.Image, xDpi, yDpi);
            lbTarget.Visible = false;
            pbTarget.Visible = true;
        }

        public static Bitmap processBatchFile(Image img, int xDpi, int yDpi) {
            double dpiX = xDpi > 0 ? xDpi : img.HorizontalResolution;
            double dpiY = yDpi > 0 ? xDpi : img.VerticalResolution;
            double ratioH = img.HorizontalResolution / dpiX;
            double ratioV = img.VerticalResolution / dpiY;
            int newWidth = (int) Math.Round(img.Width / ratioH);
            int newHeight = (int) Math.Round(img.Height / ratioV);
            Bitmap bm = new Bitmap(newWidth, newHeight);
            bm.SetResolution((float)dpiX, (float)dpiY);
            using (Graphics gr = Graphics.FromImage(bm)) {
                gr.DrawImage(img, new Rectangle(0, 0, newWidth, newHeight));
            }
            return bm;
        }

        private void lbSource_Click(object sender, EventArgs e) {
            openFile();
        }

        private void lbResult_Click(object sender, EventArgs e) {
            processFile();
        }

        private void menuConvertSaveItem_Click(object sender, EventArgs e) {
            string openFileName = null;

            for (int i = 0; i < openFileNames.Count; i++) {
                try {
                    openFileName = openFileNames[i];
                    ImageFormat imageFormat = prepareTargetFileName(openFileName, out string fullFileName);

                    if (i == 0) {
                        processFile();
                        pbTarget.Image.Save(fullFileName, imageFormat);
                    } else {
                        int xDpi = prop.i(newDpiX);
                        int yDpi = prop.i(newDpiY);
                        Bitmap batchFile = processBatchFile(Image.FromFile(openFileName), xDpi, yDpi);
                        batchFile.Save(fullFileName, imageFormat);
                    }
                } catch (Exception ex) {
                    Logger.error(logger.l("Не удалось обработать файл {0}. \r\nПричина: {1}"), openFileName, ex);
                }
            }
        }

        private ImageFormat prepareTargetFileName(string openFileName, out string fullFileName) {
            int filterIndex = MainForm.getFilterByExt(Path.GetExtension(openFileName));
            ImageFormat imageFormat = MainForm.getFilterByIndex(filterIndex);
            string fn = Path.GetFileName(openFileName);
            string fnOnly = Path.GetFileNameWithoutExtension(openFileName);
            string extOnly = Path.GetExtension(openFileName);
            fullFileName = prop.s(xmlFullFileName).Replace("{fn}", fn).Replace("{fno}", fnOnly).Replace("{ext}", extOnly);
            string folderPath = Path.GetDirectoryName(fullFileName);
            if (!string.IsNullOrEmpty(folderPath)) {
                if (!Directory.Exists(folderPath)) {
                    Directory.CreateDirectory(folderPath);
                }
            } else {
                folderPath = Path.GetDirectoryName(openFileName);
                fullFileName = Path.Combine(folderPath, fullFileName);
            }
            return imageFormat;
        }

        private void menuConvertNewItem_Click(object sender, EventArgs e) {
            lbSource.Visible = true;
            pbSource.Visible = false;
            lbTarget.Visible = true;
            pbTarget.Visible = false;
            pbSource.Image = null;
            pbTarget.Image = null;
        }

        private void ProcessImageForm_FormClosing(object sender, FormClosingEventArgs e) {
            prop.saveToXml(AppSettingFileName, "<settings/>", XPath);
        }

        private void menuContextResetItem_Click(object sender, EventArgs e) {
            propertyGrid1.ResetSelectedProperty();
        }
    }
}