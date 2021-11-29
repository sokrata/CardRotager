using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace CardRotager {
    public partial class ProcessImageForm : Form {
        private const string main = "main";
        private const string split = "split";
        private const string imageDpiW = "imageDpiW";
        private const string imageDpiH = "imageDpiH";
        private const string imageWidth = "imageWidth";
        private const string imageHeight = "imageHeight";
        private const string newDpiW = "newDpiW";
        private const string newDpiH = "newDpiH";
        private const string xmlSplitPaddingCm = "splitPaddingCm";
        private const string xmlSplitZoom = "splitZoom";
        private const string xmlSplitPaddingHCenter = "splitPaddingHCenter";
        private const string xmlSplitShowCutMark = "splitShowCutMark";
        private const string xmlSplitCutMarkColor = "splitCutMarkColor";
        private const string xmlSplitCutMarkRadius = "splitCutMarkRadius";
        private const string xmlSplitCutMarkThick = "splitCutMarkThick";
        private const string xmlUseFixedResolution = "useFixedResolution";
        private static readonly Color CUT_MARK_COLOR = Color.Gray;
        private const int CUT_MARK_RADIUS = 20;
        private const int CUT_MARK_THICK = 7;
        private const string xmlFullFileName = "fullFileName";
        private const string XPath = "/settings";
        public string AppSettingFileName { get; } = Path.Combine(Application.UserAppDataPath, "setting2.xml");
        private const double NONDPI_1_MM_TO_PX = 3.794;

        private const double DPI300_1_CM_TO_PX = 118;

        private const int DPI300_PAGE_A4_WIDTH = 2480;
        private const int DPI300_PAGE_A4_HEIGHT = 3508;
        private const double INCH = 2.54;
        //2480 (300dpi) 2480 px/21.0 cm
//3508 (300dpi) 3508 px/29.7 cm = 118.(...)
//300dpi: 1cm = 118 px; 21 cm = 2478 px

        private Logger logger;
        private PropertyObject prop;
        private List<string> openFileNames;

        public ProcessImageForm(Logger logger) {
            InitializeComponent();
            this.logger = logger;

            prop = new PropertyObject();
            prop.addParam(main, imageDpiW, 0, false, "горизонтальное DPI открытой картинки. Только для чтения", true, true);
            prop.addParam(main, imageDpiH, 0, false, "вертикальное DPI открытой картинки. Только для чтения", true, true);
            prop.addParam(main, imageWidth, 0, false, "ширина открытой картинки. Только для чтения", true, true);
            prop.addParam(main, imageHeight, 0, false, "высота открытой картинки. Только для чтения", true, true);
            prop.addParam(main, newDpiW, 300, true);
            prop.addParam(main, newDpiH, 300, true);
            prop.addParam(main, xmlFullFileName, "{fno}_out{ext}", true, l("Путь и имя сохраняемого файла (пример: {{fno}} сохраняет как имя файла без расширения). Доступны автозамены: {{fn}} - имя главного файла, {{fno}} - имя глав.файла без расширения и точки, {{ext}} - точка и расширение файла"), browsable: true, readOnly: false, editor: new UIFolderNameEditor());
            prop.addParam(split, xmlSplitPaddingCm, 1D, false, l("Отступ от краев на А4 от каждого края, в см. Если значение не 0 то появляются поля, на которых могут показываться метки (будет учитываться splitShowCutMark)"));
            prop.addParam(split, xmlSplitZoom, 100, true, l("Масштаб оригинальной картинки перед резом"));
            prop.addParam(split, xmlSplitPaddingHCenter, false, false, l("Центрирование на листе А4, на странице, если указан ненулевой отступ"));
            prop.addParam(split, xmlSplitCutMarkThick, CUT_MARK_THICK, true, l("Толщина линий для точек реза (если задан параметр splitShowCutMark)"));
            prop.addParam(split, xmlSplitShowCutMark, false, false, l("Показывать метки"));
            prop.addParam(split, xmlSplitCutMarkColor, CUT_MARK_COLOR, true, l("Цвет точки реза (если задан параметр splitShowCutMark)"));
            prop.addParam(split, xmlSplitCutMarkRadius, CUT_MARK_RADIUS, true, l("Радиус луча для точки реза (если задан параметр splitShowCutMark)"));
            prop.addParam(split, xmlUseFixedResolution, 0, true, l("Использовать фиксированный DPI вместо встроенного (0 - не использовать)"));
            propertyGrid1.SelectedObject = prop;
            openFileNames = new List<string>();
        }

        private string l(string format, params object[] args) {
            return logger.l(format, args);
        }

        private void Form2_Load(object sender, EventArgs e) {
            pbSource.Image = null;
            prop.loadFromXml(AppSettingFileName, XPath);
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
                    prop[imageDpiW].Value = (int) gr.DpiX;
                    prop[imageDpiH].Value = (int) gr.DpiY;
                }
                propertyGrid1.SelectedObject = prop;
                prop[imageWidth].Value = originalBitmap.Width;
                prop[imageHeight].Value = originalBitmap.Height;
                pbSource.Image = originalBitmap;
                lbSource.Visible = false;
                pbSource.Visible = true;
            }
        }

        private void processFile(int functionType) {
            if (pbSource.Image == null) {
                return;
            }
            int cnt = -1;
            pbTarget.Image = processBatchFile(pbSource.Image, prop, functionType, ref cnt);
            lbTarget.Visible = false;
            pbTarget.Visible = true;
        }

        public static Bitmap processBatchFile(Image srcImg, PropertyObject prop, int functionType, ref int imgCnt) {
            int xDpi = prop.i(newDpiW);
            int yDpi = prop.i(newDpiH);
            double srcDpiW = prop.i(xmlUseFixedResolution) > 0 ? prop.i(xmlUseFixedResolution): srcImg.HorizontalResolution;
            double srcDpiH = prop.i(xmlUseFixedResolution) > 0 ? prop.i(xmlUseFixedResolution) : srcImg.VerticalResolution;
            double dstDpiW = xDpi > 0 ? xDpi : srcDpiW;
            double dstDpiH = yDpi > 0 ? xDpi : srcDpiH;
            double ratioH = srcDpiW / dstDpiW;
            double ratioV = srcDpiH / dstDpiH;
            // double srcPageWidthCm = px2cm(srcImg.Width, srcDpiW);
            // double srcPageHeightCm = px2cm(srcImg.Height, srcDpiH);
            int dstPageWidthPx = a4(dstDpiW, true);
            int dstPageHeightPx = a4(dstDpiH, false);
            int srcPageWidthPx = a4(srcDpiW, true);
            int srcPageHeightPx = a4(srcDpiH, false);
            /*
            int dstPageWidth = a4(dstDpiW, true);
            int dstPageHeight = a4(dstDpiH, false);
             */
            Bitmap bm;
            if (functionType == 2) {
                double paddingCm = prop.d(xmlSplitPaddingCm);
                double zoom = prop.i(xmlSplitZoom);
                int srcPaddingPx = cm2px(paddingCm, srcDpiW);// * DPI300_1_CM_TO_PX);
                int dstPaddingPx = cm2px(paddingCm, dstDpiW);// * DPI300_1_CM_TO_PX);
                int dstSplitWidthPx = dstPageWidthPx - dstPaddingPx * 2;
                int dstSplitHeightPx = dstPageHeightPx - dstPaddingPx * 2;
                // int srcSplitWidthPx = (int)Math.Ceiling((21D - srcPaddingPx * 2) * srcDpiH);
                // int srcSplitHeightPx = (int)Math.Ceiling((29.7D - srcPaddingPx * 2) * srcDpiW);
                double zoomFactor = zoom / 100D;
                int srcSplitWidthPx = roundUp((srcPageWidthPx - srcPaddingPx * 2) / zoomFactor);
                int srcSplitHeightPx = roundUp((srcPageHeightPx - srcPaddingPx * 2) / zoomFactor);
                
                if (!calcPart(srcImg, ref imgCnt, srcSplitWidthPx, srcSplitHeightPx, out int srcXpx, out int srcYpx)) {
                    return null;
                }
                //1000 - 200 = 800
                int srcImgWidth;
                int dstImgWidth;
                if (srcImg.Width - srcXpx > srcSplitWidthPx) {
                    srcImgWidth = srcSplitWidthPx;
                    dstImgWidth = dstSplitWidthPx;
                } else {
                    srcImgWidth = srcImg.Width - srcXpx;
                    dstImgWidth = roundUp(cm2px(px2cm(srcImgWidth, srcDpiW), dstDpiW) * zoomFactor);
                }
                int srcImgHeight;
                int dstImgHeight;
                if (srcImg.Height - srcYpx > srcSplitHeightPx) {
                    srcImgHeight = srcSplitHeightPx;
                    dstImgHeight = dstSplitHeightPx;
                } else {
                    srcImgHeight = srcImg.Height - srcYpx;
                    dstImgHeight = roundUp(cm2px(px2cm(srcImgHeight, srcDpiH), dstDpiH) * zoomFactor);
                }
                int dstX;
                int dstY;
                bool withDestFrame = paddingCm > 0;
                if (withDestFrame) {
                    bm = new Bitmap(dstPageWidthPx, dstPageHeightPx);
                    dstX = dstPaddingPx;
                    dstY = dstPaddingPx;
                    if (prop.b(xmlSplitPaddingHCenter)) {
                        dstX = dstPageWidthPx / 2 - dstImgWidth / 2 - dstPaddingPx;
                    }
                } else {
                    bm = new Bitmap(dstImgWidth, dstImgHeight);
                    dstX = 0;
                    dstY = 0;
                }
                bm.SetResolution((float) dstDpiW, (float) dstDpiH);
                using (Graphics gr = Graphics.FromImage(bm)) {
                    gr.Clear(Color.White);
                  
                    Rectangle dstRect = new Rectangle(dstX, dstY, dstImgWidth, dstImgHeight);
                    Rectangle srcRect = new Rectangle(srcXpx, srcYpx, srcImgWidth, srcImgHeight);
                    gr.DrawImage(srcImg, dstRect, srcRect, GraphicsUnit.Pixel);
                    if (prop.b(xmlSplitShowCutMark)) {
                        Pen penCutMark = new Pen(prop.clr(xmlSplitCutMarkColor), prop.i(xmlSplitCutMarkThick));
                        int crossRadius = prop.i(xmlSplitCutMarkRadius);
                        Drawler.drawCross(gr, penCutMark, srcPaddingPx, srcPaddingPx, crossRadius);
                        Drawler.drawCross(gr, penCutMark, srcPaddingPx + dstImgWidth, srcPaddingPx, crossRadius);
                        Drawler.drawCross(gr, penCutMark, srcPaddingPx, srcPaddingPx + dstImgHeight, crossRadius);
                        Drawler.drawCross(gr, penCutMark, srcPaddingPx + dstImgWidth, srcPaddingPx + dstImgHeight, crossRadius);
                    }
                }
            } else {
                int newWidth = (int) Math.Round(srcImg.Width / ratioH);
                int newHeight = (int) Math.Round(srcImg.Height / ratioV);
                bm = new Bitmap(newWidth, newHeight);
                bm.SetResolution((float) dstDpiW, (float) dstDpiH);
                using (Graphics gr = Graphics.FromImage(bm)) {
                    gr.DrawImage(srcImg, new Rectangle(0, 0, newWidth, newHeight));
                }
            }
            return bm;
        }
        private static int roundUp(double val) {
            return (int)Math.Ceiling(val);
        }

        private static int a4(double dpi, bool width) {
            if (Math.Abs(dpi - 300) < 0.1) {
                return width ? DPI300_PAGE_A4_WIDTH : DPI300_PAGE_A4_HEIGHT;
            } else {
                return cm2px(width ? 21.0 : 29.7, dpi);
            }
        }
        
        private static double px2cm(int px, double dpi) {
            return px * INCH / dpi;
        }
        private static int cm2px(double cm, double dpi) {
            return (int) (cm / INCH * dpi);
        }

        private static bool calcPart(Image image, ref int imgCnt, int cutWidth, int cutHeight, out int x, out int y) {
            double imgWidth = image.Width;
            double imgHeight = image.Height;
            int colCount = (int) Math.Ceiling(imgWidth / cutWidth);
            int rowCount = (int) Math.Ceiling(imgHeight / cutHeight);
            int colIndex = (int) (1D * imgCnt % colCount);
            int rowIndex = (int) Math.Floor(1D * imgCnt / colCount);
            //imgCnt - colIndex * colCount;
            if (imgCnt >= rowCount * colCount) {
                x = 0;
                y = 0;
                return false;
            }
            x = colIndex * cutWidth;
            y = rowIndex * cutHeight;
            Debug.WriteLine("c={0}, r={1}, ({2},{3}), x = {4}, y = {5}", colIndex, rowIndex, colCount, rowCount, x, y);
            if (colCount == 1 && rowCount == 1) {
                imgCnt = 0;
            } else {
                imgCnt++;
            }
            return true;
        }

        private void lbSource_Click(object sender, EventArgs e) {
            openFile();
        }

        private void lbResult_Click(object sender, EventArgs e) {
            processFile(1);
        }

        private void menuConvertSaveItem_Click(object sender, EventArgs e) {
            string openFileName = null;

            for (int i = 0; i < openFileNames.Count; i++) {
                try {
                    openFileName = openFileNames[i];
                    ImageFormat imageFormat = prepareTargetFileName(openFileName, out string fullFileName);

                    if (i == 0) {
                        processFile(1);
                        pbTarget.Image.Save(fullFileName, imageFormat);
                    } else {
                        int cnt = 0;
                        Bitmap batchFile = processBatchFile(Image.FromFile(openFileName), prop, 1, ref cnt);
                        batchFile.Save(fullFileName, imageFormat);
                    }
                } catch (Exception ex) {
                    Logger.error(logger.l("Не удалось обработать файл {0}. \r\nПричина: {1}"), openFileName, ex);
                }
            }
            WinSpecific.clearMemory();
        }

        private ImageFormat prepareTargetFileName(string openFileName, out string fullFileName) {
            int filterIndex = MainForm.getFilterByExt(Path.GetExtension(openFileName));
            ImageFormat imageFormat = MainForm.getFilterByIndex(filterIndex);
            string fn = Path.GetFileName(openFileName);
            string fnOnly = Path.GetFileNameWithoutExtension(openFileName);
            string extOnly = Path.GetExtension(openFileName);
            fullFileName = Path.Combine(Path.GetDirectoryName(openFileName), prop.s(xmlFullFileName).Replace("{fn}", fn).Replace("{fno}", fnOnly).Replace("{ext}", extOnly));
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

        private void menuSaveSplitItem_Click(object sender, EventArgs e) {
            string openFileName = null;

            for (int i = 0; i < openFileNames.Count; i++) {
                try {
                    openFileName = openFileNames[i];
                    ImageFormat imageFormat = prepareTargetFileName(openFileName, out string fullFileName);

                    int cnt = 0;
                    do {
                        Bitmap batchFile = processBatchFile(Image.FromFile(openFileName), prop, 2, ref cnt);
                        if (batchFile == null) {
                            break;
                        }
                        if (cnt > 0) {
                            string path = Path.GetDirectoryName(fullFileName) + "\\" + Path.GetFileNameWithoutExtension(fullFileName) + string.Format("[{0}]{1}", cnt, Path.GetExtension(fullFileName));
                            batchFile.Save(path, imageFormat);
                            continue;
                        }
                        batchFile.Save(fullFileName, imageFormat);
                        break;
                    } while (true);
                } catch (Exception ex) {
                    Logger.error(logger.l("Не удалось обработать файл {0}. \r\nПричина: {1}"), openFileName, ex);
                }
            }
            WinSpecific.clearMemory();
        }
    }
}