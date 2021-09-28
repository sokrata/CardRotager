using AForge.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CardRotager {
    public partial class MainForm : Form {
        private static readonly double[] zoomFactor = {0.01, 0.05, 0.1, .15, .25, .50, .75, 1, 1.25, 1.5, 1.75, 2.0, 2.5, 3.0};
        private const int zoomIndexDefault = 7;
        private const int zoomIndex1percent = 0;
        private const int zoomIndex5percent = 1;
        private const int zoomIndex10percent = 2;
        private const int zoomIndex15percent = 3;
        private const int zoomIndex25percent = 4;
        private const int zoomIndex50percent = 5;
        private const int zoomIndex100percent = 7;
        private const int zoomIndex150percent = 9;
        private const int zoomIndex200percent = 11;
        private const int zoomIndex300percent = 13;
        private const double TOLERANCE = 0.01;
        const string openImageFileWithCardsClickHere = "Открыть файл изображения с картами (сейчас только 4 строки и 2 колонки)...\r\n(щелкните сюда)";
        private Brush spotBrush;
        private bool reloadInsteadReOpen = false;

        /// <summary>
        /// Статус обработки изображения: Null - не открыт, false - открыт но не обработан, true - открыт и обработан
        /// </summary>
        private bool? imageProcessState;

        private string fileName {
            get => settings.LastOpenFileName;
            set => settings.LastOpenFileName = value;
        }

        public Drawler drawler;
        private ImageProcessor ip;

        //false-режим не работает, т.к. масштаб для pictureBox не учитывается
        bool drawHelpLineOnPaint = false;
        private Pen redDashPen;
        public string AppSettingFileName { get; } = Path.Combine(Application.UserAppDataPath, "setting.xml");
        public Settings settings;

        public MainForm(Logger log) {
            InitializeComponent();
            imageProcessState = null;

            pbDraft.MouseWheel += pictureBox_MouseWheel;
            panelDraft.MouseWheel += pictureBox_MouseWheel;
            pbSource.MouseWheel += pictureBox_MouseWheel;
            panelOriginal.MouseWheel += pictureBox_MouseWheel;
            pbTarget.MouseWheel += pictureBox_MouseWheel;
            panelTarget.MouseWheel += pictureBox_MouseWheel;

            lbHintSource.Visible = true;
            lbHintDraft.Visible = true;
            pbSource.Visible = false;

            lbHintTarget.Visible = true;
            pbTarget.Visible = false;

            pbDraft.Visible = false;
            pbDraft.Paint += onPbDraftOnPaint;
            pbSource.Paint += onPbSourceOnPaint;
            pbTarget.Paint += onPbTargetOnPaint;


            settings = new Settings(log);

            float[] dashValues = {5, 2, 15, 4};
            redDashPen = new Pen(Color.Red, 5) {
                DashPattern = dashValues
            };
            spotBrush = new SolidBrush(Color.White);

            drawler = new Drawler();
        }
        private bool processImage() {
            Program.log.resetStringBuilder();
            ip = new ImageProcessor(Program.log, settings, statusBarProgressBar);
            Bitmap bmpDraft = ((Bitmap) pbDraft.Image);
            pbTarget.Image = null;
            bool result = ip.fillHVLinesAll(bmpDraft);
            int width = bmpDraft.Width;
            int height = bmpDraft.Height;
            using (Graphics graphics = Graphics.FromImage(bmpDraft)) {
                if (!drawHelpLineOnPaint) {
                    drawVerticalDots(graphics);
                }

                Program.log.AppendLine(l("Линии-границы поиска горизонтальных линий ниже верхних:"));
                if (!drawHelpLineOnPaint) {
                    drawDashLine(graphics, Program.log);
                }

                Program.log.AppendFormat(l(l("\r\nИтоговые горизонтальные линии: \r\n")));

                if (!drawHelpLineOnPaint) {
                    drawHLines(graphics, Program.log);
                }

                Program.log.AppendFormat(l("\r\nИтоговые вертикальные линии: \r\n"));
                if (!drawHelpLineOnPaint) {
                    drawVLines(graphics, Program.log);
                }

                Program.log.AppendLine(l("\r\nЛинии, по которым рассчитывается угол наклона карт\r\n"));
                if (!drawHelpLineOnPaint) {
                    drawAngleLines(graphics, Program.log);
                }

                List<Rectangle> rectangles = makeRect(Program.log, ip.LinesAll, out List<float> angles);

                if (!drawHelpLineOnPaint) {
                    drawRuler(graphics, width, height);
                }

                Bitmap originalImage = (Bitmap) pbSource.Image;
                pbTarget.Image = makeTargetImage(originalImage, rectangles, width, height, angles);

                if (!drawHelpLineOnPaint) {
                    using (Graphics originalGraphic = Graphics.FromImage(originalImage)) {
                        drawFoundContours(originalGraphic, rectangles);
                    }
                }
                pbSource.Invalidate();
                pbTarget.Invalidate();
                pbDraft.Invalidate();
            }

            tbLog.Text = Program.log.ConvertToString();
            WinSpecific.clearMemory();
            return result;
        }

        private void setProgress(bool value, string msgResult = "Обработка завершена") {
            // statusBarProgressBar.Style = value ? ProgressBarStyle.Marquee : ProgressBarStyle.Continuous;
            // statusBarProgressBar.MarqueeAnimationSpeed = value ? 100 : 0;
            if (value) {
                statusBarProgressBar.Value = statusBarProgressBar.Minimum;
            } else {
                statusBarProgressBar.Value = statusBarProgressBar.Maximum;
                statusBarInfo.Text = msgResult;
            }

            Application.DoEvents();
        }

        private void resetProgress() {
            // statusBarProgressBar.MarqueeAnimationSpeed = 0;
            statusBarProgressBar.Style = ProgressBarStyle.Continuous;
            statusBarProgressBar.Value = 0;
            WinSpecific.SetState(statusBarProgressBar.ProgressBar, (int) WinSpecific.ProgressBarState.NORMAL);
            statusBarInfo.BackColor = SystemColors.Control;
            statusBarInfo.Text = "Начата обработка";
            Application.DoEvents();
        }

        private Image makeTargetImage(Bitmap fromImage, List<Rectangle> fromRectList, int width, int height, List<float> angles) {
            const int EXTEND_SIDE = 50;
            const int EXT_WIDTH = 150;
            const int EXT_HEIGHT = 150;
            const int WIDTH_8_8_CM = 4160 + EXT_WIDTH; //px
            const int HEIGHT_6_3_CM = 2965 + EXT_HEIGHT; //px
            Bitmap targetImage = createEmptyBitmapSource(width, height, fromImage);
            using (Graphics toGraphics = Graphics.FromImage(targetImage)) {
                int cardWidth = Math.Max(fromRectList.Count == 0 ? 0 : fromRectList.Max(a => a.Width), WIDTH_8_8_CM);
                int cardHeight = Math.Max(fromRectList.Count == 0 ? 0 : fromRectList.Max(a => a.Height), HEIGHT_6_3_CM);
                drawler.calcFrame(width, height, cardWidth, cardHeight);
                List<Rectangle> toRectList = drawler.makeFrame();
                toGraphics.Clear(Color.White);
                bool needFlipRect = isNeedFlipRect();
                Brush cropFillBrush = new SolidBrush(settings.CropPaddingColor);
                // for (int imageIndex = 0; imageIndex < fromRectList.Count; imageIndex++) {
                //     fromRectList[imageIndex].Inflate(new Size(EXTEND_SIDE, EXTEND_SIDE));
                // }
                for (int imageIndex = 0; imageIndex < toRectList.Count; imageIndex++) {
                    if (imageIndex >= fromRectList.Count) {
                        break;
                    }

                    float angle = 0;
                    if (settings.RotateFoundSubImages) {
                        angle = -angles[imageIndex];
                    }
                    copyRegionIntoImage(toGraphics, fromImage, imageIndex, toRectList, fromRectList, angle, EXTEND_SIDE, Program.log, needFlipRect, cropFillBrush);
                    progressIncrement();
                }

                if (!drawHelpLineOnPaint) {
                    drawOnTargetImage(toGraphics);
                }
            }
            if (settings.NewDpiX > 0 || settings.NewDpiY > 0) {
                return CardRotager.ProcessImageForm.processBatchFile(targetImage, settings.NewDpiX, settings.NewDpiY);
            }
            return targetImage;
        }

        private void progressIncrement() {
            statusBarProgressBar.Increment(1);
            Application.DoEvents();
        }

        private void onPbDraftOnPaint(object sender, PaintEventArgs args) {
            if (ip == null || !drawHelpLineOnPaint) {
                return;
            }
            Graphics graphics = args.Graphics;
            drawVerticalDots(graphics);
            drawDashLine(graphics, null);
            drawHLines(graphics, null);
            drawVLines(graphics, null);
            drawAngleLines(graphics, null);
            Image image = (sender as PictureBox).Image;
            drawRuler(graphics, image.Width, image.Height);
        }

        private void onPbSourceOnPaint(object sender, PaintEventArgs args) {
            if (ip == null || !drawHelpLineOnPaint) {
                return;
            }
            Graphics graphics = args.Graphics;
            Image image = (sender as PictureBox).Image;
            drawFoundContours(graphics, null);
            drawRuler(graphics, image.Width, image.Height);
        }

        private void onPbTargetOnPaint(object sender, PaintEventArgs args) {
            if (ip == null || !drawHelpLineOnPaint) {
                return;
            }
            Graphics graphics = args.Graphics;
            Image image = (sender as PictureBox).Image;
            // drawTargetFrame(graphics, image.Width, image.Height);
            drawRuler(graphics, image.Width, image.Height);
        }

        private void drawHLines(Graphics graphics, Logger log) {
            if (settings.ShowHorVertLines && ip.LinesAll != null) {
                for (int colIndex = 0; colIndex < ip.LinesAll.Count; colIndex++) {
                    for (int rowIndex = 0; rowIndex < ip.LinesAll[colIndex].Count; rowIndex++) {
                        drawler.drawLine(graphics, ip.LinesAll[colIndex][rowIndex].HLine, Pens.Maroon, rowIndex, log, true, settings.HVThickLine);
                    }
                }
            }
        }

        private void drawVLines(Graphics graphics, Logger log) {
            if (settings.ShowHorVertLines && ip.LinesAll != null) {
                for (int colIndex = 0; colIndex < ip.LinesAll.Count; colIndex++) {
                    for (int rowIndex = 0; rowIndex < ip.LinesAll[colIndex].Count; rowIndex++) {
                        drawler.drawLine(graphics, ip.LinesAll[colIndex][rowIndex].VLine, Pens.Blue, rowIndex, log, true, settings.HVThickLine);
                    }
                }
            }
        }

        private void drawFoundContours(Graphics graphics, List<Rectangle> rectangles) {
            if (settings.ShowImageFoundContour) {
                drawler.drawRect(graphics, rectangles);
            }
        }

        private void drawOnTargetImage(Graphics graphics) {
            const int penWidth = 7;
            Pen penFrame = new Pen(Color.LimeGreen, penWidth);
            if (settings.ShowImageTargetFrame) {
                drawler.drawTargetFrame(graphics, penFrame);
            }
            if (string.IsNullOrEmpty(settings.CutMarkShowOnTargetImageMask)) {
                return;
            }
            if (isFileMaskMatch(fileName, settings.CutMarkShowOnTargetImageMask)) {
                Pen penCutMark = new Pen(settings.CutMarkColor, penWidth);
                drawler.drawTargetCutMark(graphics, penCutMark, settings.CutMarkRadius);
            }
            if (!string.IsNullOrWhiteSpace(settings.DrawTextOnTargetImage)) {
                string fn = Path.GetFileName(fileName);
                string fnOnly = Path.GetFileNameWithoutExtension(fileName);
                string extOnly = Path.GetExtension(fileName);
                string text = settings.DrawTextOnTargetImage.Replace("{fn}", fn)
                    .Replace("{fno}", fnOnly).Replace("{ext}", extOnly);
                Font headerFont = UIFontChooser.createFont(settings.DrawTextTargetFont, SystemFonts.CaptionFont, Program.log);
                drawler.drawText(graphics, headerFont, text);
            }
        }

        private void drawRuler(Graphics graphics, int width, int height) {
            if (settings.ShowRuler) {
                drawler.drawRuler(graphics, width, height);
            }
        }

        private void drawAngleLines(Graphics graphics, Logger log) {
            Pen anglePen = new Pen(Color.Lime, 12);
            foreach (var angle in ip.AngleEdges) {
                log.AppendFormat("{0}\r\n", angle);
                if (settings.ShowAngleLines) {
                    graphics.DrawLine(anglePen, angle.X, angle.Y, angle.X2, angle.Y2);
                }
            }
        }

        private void drawVerticalDots(Graphics graphics) {
            if (settings.ShowDetailDotsOfImageNumber > 0) {
                drawler.drawPolyLine(graphics, ip.getLineByImageIndex(settings.ShowDetailDotsOfImageNumber - 1));
                Drawler.drawHorizontalDots(graphics, Pens.DeepSkyBlue, 0, ip.DotsHorizontal);
                Drawler.drawVerticalDots(graphics, Pens.Red, 0, ip.DotsVertical);
            }
        }

        private void drawDashLine(Graphics graphics, Logger log) {
            bool showHelpLines = settings.ShowAngleLines;
            foreach (var item in ip.DashLines) {
                log.AppendFormat("{0}\r\n", item);
                if (showHelpLines) {
                    graphics.DrawLine(redDashPen, item.X, item.Y1, item.X2, item.Y2);
                }
            }
        }

        private string l(string text) {
            return Program.localize.localize(text);
        }

        public static Bitmap createEmptyBitmapSource(int width, int height, Bitmap bitmapOriginal) {
            PixelFormat pf = bitmapOriginal.PixelFormat;
            var bitmap = new Bitmap(width, height, pf);
            bitmap.SetResolution(bitmapOriginal.HorizontalResolution, bitmapOriginal.VerticalResolution);

            return bitmap;
        }

        public void copyRegionIntoImage(Graphics toGraphic, Bitmap fromBitmap, int imageIndex, List<Rectangle> toRegionList, List<Rectangle> fromRegionList,
            float angle, int extend, Logger log, bool needFlipRect, Brush cropFillBrush) {
            Rectangle fromRegion = fromRegionList[imageIndex];
            Rectangle toRegion = toRegionList[imageIndex];
            //увеличим на запас (защита от скоса сторон картинки)
            double fromRegionWidth = fromRegion.Width;
            fromRegion.Inflate(new Size(extend, extend));
            //угол от скоса - вычтем высоту и добавим для половинения
            int shiftY = (int) (Math.Tan(angle * Math.PI / 180) * fromRegionWidth);
            using (Bitmap rotatedBitmap = createEmptyBitmapSource(fromRegion.Width, fromRegion.Height, fromBitmap)) {
                using (Graphics rotatedGraphic = Graphics.FromImage(rotatedBitmap)) {
                    rotatedGraphic.Clear(Color.White);
                    float centerX = (fromRegion.Width) * 0.5f;
                    float centerY = (fromRegion.Height + shiftY) * 0.5f;
                    rotatedGraphic.TranslateTransform(centerX, centerY);
                    rotatedGraphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    //rotatedGraphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    rotatedGraphic.RotateTransform(angle);
                    rotatedGraphic.TranslateTransform(-centerX, -centerY);
                    // float scale = (float) rotatedBitmap.Width / rotatedBitmap.Width;
                    rotatedGraphic.DrawImage(fromBitmap, new Rectangle(0, 0, rotatedBitmap.Width, rotatedBitmap.Height), fromRegion, GraphicsUnit.Pixel);
                    rotatedGraphic.RotateTransform(-angle);
                    Rectangle cropTop = new Rectangle(0, -extend / 2, rotatedBitmap.Width, shiftY + extend / 2 + settings.CropPaddingTop);
                    Rectangle cropRight = new Rectangle(rotatedBitmap.Width - settings.CropPaddingRight, 0, settings.CropPaddingRight, rotatedBitmap.Height);
                    // fromRegion.Width -= settings.CropPaddingRight;
                    // fromRegion.Height -= settings.CropPaddingTop + shiftY;
                    rotatedGraphic.FillRectangle(cropFillBrush, cropTop);
                    rotatedGraphic.FillRectangle(cropFillBrush, cropRight);
                    string saveSubImageFileName = null;
                    try {
                        if (!string.IsNullOrWhiteSpace(settings.SaveEachRectangleFileName)) {
                            string fn = Path.GetFileName(fileName);
                            string fnOnly = Path.GetFileNameWithoutExtension(fileName);
                            saveSubImageFileName = Path.Combine(settings.SaveEachRectangleFileName).Replace("{#}", (imageIndex + 1).ToString()).Replace("{fn}", fn)
                                .Replace("{fno}", fnOnly);
                            string folderPath = Path.GetDirectoryName(saveSubImageFileName);
                            string extension = Path.GetExtension(saveSubImageFileName).ToLower();
                            int filterIndex = getFilterByExt(extension);
                            ImageFormat imageFormat = getFilterByIndex(filterIndex);
                            if (!Directory.Exists(folderPath)) {
                                Directory.CreateDirectory(folderPath);
                            }
                            rotatedBitmap.Save(saveSubImageFileName, imageFormat);
                        }
                    } catch (Exception ex) {
                        string stError = string.Format("Не удалось сохранить файл: {0}\r\n{1}", saveSubImageFileName, ex.Message);
                        if (log != null) {
                            log.AppendLine(stError);
                        } else {
                            MessageBox.Show(stError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

                Rectangle srcRect = new Rectangle(0, 0, rotatedBitmap.Width, rotatedBitmap.Height);
                if (needFlipRect) {
                    rotatedBitmap.RotateFlip(RotateFlipType.RotateNoneFlipXY);
                    if (imageIndex < 4) {
                        toRegion = toRegionList[imageIndex + 4];
                    } else {
                        toRegion = toRegionList[imageIndex - 4];
                    }
                }
                int addX = (toRegion.Width - fromRegion.Width) / 2;
                int addY = (toRegion.Height - fromRegion.Height) / 2;
                int destX = toRegion.X;
                int destY = toRegion.Y;
                toGraphic.DrawImage(rotatedBitmap, destX + addX, destY + addY, srcRect, GraphicsUnit.Pixel);
            }
        }

        /// <summary>
        /// Нужна ли переворот карт и колонок
        /// </summary>
        /// <returns>true, нужна</returns>
        private bool isNeedFlipRect() {
            if (settings.FlipHorizontalEachRect) {
                return true;
            }
            if (string.IsNullOrEmpty(settings.FlipHorizontalEachRectFileMask)) {
                return false;
            }
            return isFileMaskMatch(fileName, settings.FlipHorizontalEachRectFileMask);
        }

        //     
        /// <summary>
        /// Проверка соответствия имени файла маске
        /// Пример использования: bool fl = isFileMaskMatch(@"C:\temp\G-00.jpg", "G-*.jpg|*.bmp|*.jpg|???.???");
        /// код на базе примера: https://stud-work.ru/maska-fajla-s-pomoshchyu-regulyarnykh-vyrazhenij
        /// </summary>
        /// <param name="fileName">Имя проверяемого файла</param>
        /// <param name="mask">Маска файла</param>
        /// <returns>true - файл удовлетворяет маске, иначе false</returns>
        private static bool isFileMaskMatch(string fileName, string mask) {
            string[] exts = mask.Split('|', ',', ';');
            string pattern = string.Empty;
            foreach (string ext in exts) {
                pattern += @"^"; //признак начала строки
                foreach (char symbol in ext)
                    switch (symbol) {
                        case '.':
                            pattern += @"\.";
                            break;
                        case '?':
                            pattern += @".";
                            break;
                        case '*':
                            pattern += @".*";
                            break;
                        default:
                            pattern += symbol;
                            break;
                    }
                pattern += @"$|"; //признак окончания строки
            }
            if (pattern.Length == 0) return false;
            pattern = pattern.Remove(pattern.Length - 1);
            Regex msk = new Regex(pattern, RegexOptions.IgnoreCase);
            return msk.IsMatch(Path.GetFileName(fileName));
        }

        /// <summary>
        /// Сформировать линии для горизонтальных hLinesAll и вертикальных vLinesAll и высчитать углы в градусах списка angles
        /// </summary>
        /// <param name="log"></param>
        /// <param name="hLinesAll"></param>
        /// <param name="vLinesAll"></param>
        /// <param name="angles"></param>
        /// <returns></returns>
        private List<Rectangle> makeRect(Logger log, List<List<HVLine>> LinesAll, out List<float> angles) {
            log?.AppendFormat(l("\r\nГабариты карт:\r\n"));
            List<Rectangle> rectangles = new List<Rectangle>();
            angles = new List<float>();
            int i = 0;
            if (LinesAll == null) {
                log?.AppendFormat(l("Не удалось сформировать карт, так как нет линий.\r\n"));
                return rectangles;
            }
            for (int hIndex = 0; hIndex < LinesAll.Count; hIndex++) {
                for (int vIndex = 0; vIndex < LinesAll[hIndex].Count; vIndex++) {
                    Edge hLine = LinesAll[hIndex][vIndex].HLine;
                    Edge vLine = LinesAll[hIndex][vIndex].VLine;
                    if (hLine == null || vLine == null) {
                        log?.AppendFormat(l("Не хватает линий для формирования {0} рамки: горизонтальная = {1}, вертикальная = {2}\r\n"), i + 1, hLine, vLine);
                        i++;
                        continue;
                    }
                    int minY = min(vLine.Y, hLine.Y, hLine.Y2);
                    int maxY = vLine.Y2;
                    int minX = hLine.X;
                    int maxX = max(hLine.X2, vLine.X, vLine.X2);
                    float angle = (float) hLine.Angle;
                    angles.Add(angle);
                    Rectangle item = new Rectangle(minX, minY, maxX - minX, maxY - minY);
                    log?.AppendFormat(l("{0}: {1}, angle = {2}\r\n"), i + 1, item, angle);
                    i++;
                    rectangles.Add(item);
                }
            }

            return rectangles;
        }

        private static int min(int v1, int v2, int v3) {
            return Math.Min(Math.Min(v1, v2), v3);
        }

        private static int max(int v1, int v2, int v3) {
            return Math.Max(Math.Max(v1, v2), v3);
        }

        private bool openFile(string fileName) {
            try {
                Image initImage;
                if (reloadInsteadReOpen && !string.IsNullOrEmpty(fileName) && pbSource.Image != null) {
                    initImage = (Bitmap) pbSource.Image.Clone();
                } else {
                    initImage = Image.FromFile(fileName);
                }
                readImage(initImage);
                this.fileName = fileName;
                Text = fileName;
                return true;
            } catch (Exception ex) {
                MessageBox.Show(l("Ошибка: ") + ex.Message, l("Невозможно открыть файл"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            } finally {
                WinSpecific.clearMemory();
            }
        }

        private void readImage(Image initImage) {
            Bitmap bitmap;
            pbSource.Image = initImage;
            if (settings.ConvertOpenImage) {
                using (Bitmap image = new Bitmap(initImage)) {
                    int thresholdValue = 227;
                    IFilter threshold = new Threshold(thresholdValue);
                    using (Bitmap image2 = Grayscale.CommonAlgorithms.RMY.Apply(image)) {
                        using (Bitmap bitmap1 = threshold.Apply(image2)) {
                            MemoryStream ms = new MemoryStream();
                            bitmap1.Save(ms, ImageFormat.Jpeg);
                            var streamBitmap = Image.FromStream(ms);
                            bitmap = new Bitmap(streamBitmap);
                        }
                    }
                }
            } else {
                bitmap = (Bitmap) initImage;
            }

            loadImage(bitmap);
        }

        private void loadImage(Image bitmap) {
            pbDraft.Image = bitmap;
            pbDraft.Width = bitmap.Width;
            pbDraft.Height = bitmap.Height;
            pbTarget.Image = null;
            reloadZoom(pbSource);
            reloadZoom(pbTarget);
            reloadZoom(pbDraft);
        }

        private void reloadZoom(PictureBox pictureBox) {
            if (pictureBox.Tag == null) {
                pictureBox.Tag = zoomIndexDefault;
            }
            if (isFitMode(pictureBox)) {
                //zoom -> fit
                switchInFitMode(pictureBox);
            } else {
                //autosize -> scale
                switchInScaleMode(pictureBox);
            }
            showZoomImage(pictureBox);
        }

        private void buttonProcess_Click(object sender, EventArgs e) {
            reloadInsteadReOpen = true;
            workFlowImageProcessExecute();
            reloadInsteadReOpen = false;
        }

        bool processImageStep() {
            switch (imageProcessState) {
                case null: {
                    return openPlusProcess();
                }
                case false:
                    return processImage();
                default: {
                    if (settings.ProcessCycleF4) {
                        imageProcessState = null;
                        prepareProcessImageState();
                        return openPlusProcess();
                    }
                    break;
                }
            }
            return true;
        }

        private void workFlowImageProcessExecute() {
            resetProgress();
            prepareProcessImageState();
            // Task task = new Task<bool>(processImageStep);
            // task.Start();
            setProgress(true);
            string msg = "Обработка завершена";
            if (!processImageStep()) {
                msg = "Возникла ошибка при обработке";
                WinSpecific.SetState(statusBarProgressBar.ProgressBar, (int) WinSpecific.ProgressBarState.ERROR);
                statusBarInfo.BackColor = Color.Red;
            }
            // while (!task.IsCompleted) {
            //     Application.DoEvents();
            //     Thread.Sleep(1);
            // }
            setProgress(false, msg);
            postProcessImageState();
        }

        private bool openPlusProcess() {
            if (!imageOpen()) {
                return false;
            }
            if (settings.ProcessWhenOpen) {
                postProcessImageState();
                prepareProcessImageState();
                return processImage();
            }
            return true;
        }

        private bool imageOpen() {
            if (string.IsNullOrEmpty(fileName)) {
                if (!openImageWithDialog(false)) {
                    resetImageState();
                }
                return true;
            }

            return openFile(fileName);
        }

        private void prepareProcessImageState() {
            if (imageProcessState == null) {
                lbHintSource.Text = l("Открытие изображение.\r\nФормирование промежуточного ч/б изображения на закладке 'Обработка'...");
                lbHintDraft.Text = l("Открыто изображение.\r\nСформировать промежуточное ч/б изображение\r\n(щелкните сюда)");
                lbHintTarget.Text = l("Сформировать файл изображения с центрированными изображения картами\r\n(щелкните сюда)");
            } else if (imageProcessState == false) {
                lbHintTarget.Text = l("Обработка изображения.\r\nФормирование итогового изображения...");
                lbHintDraft.Text = l("Открыто оригинальное изображение.\r\nДля формирования промежуточного изображения\r\n(щелкните сюда)...");
            } else {
                lbHintSource.Text = l("Открытие изображение.\r\nФормирование промежуточного ч/б изображения на закладке 'Обработка'...");
                lbHintDraft.Text = l("Открытие изображение.\r\nФормирование промежуточного ч/б изображения...");
            }

            Application.DoEvents();
            WinSpecific.UseWaitCursor = true;
        }

        private void postProcessImageState() {
            if (imageProcessState == null) {
                pbSource.Visible = true;
                lbHintSource.Visible = false;

                pbTarget.Visible = false;
                lbHintTarget.Visible = true;

                lbHintDraft.Visible = true;
                pbDraft.Visible = false;

                imageProcessState = false;
            } else if (imageProcessState == false) {
                lbHintSource.Visible = false;
                pbSource.Visible = true;

                pbTarget.Visible = true;
                lbHintTarget.Visible = false;

                lbHintDraft.Visible = false;
                pbDraft.Visible = true;

                imageProcessState = true;
            }

            WinSpecific.UseWaitCursor = false;
            Application.DoEvents();
        }

        private void resetImageState() {
            lbHintSource.Visible = true;
            lbHintDraft.Visible = true;
            lbHintTarget.Visible = true;
            pbSource.Visible = false;
            pbDraft.Visible = false;
            pbTarget.Visible = false;
            lbHintSource.Text = l(openImageFileWithCardsClickHere);
            lbHintDraft.Text = l(openImageFileWithCardsClickHere);
            lbHintTarget.Text = l("Сформировать файл изображения с центированные изображения картами\r\n(щелкните сюда)");
            WinSpecific.UseWaitCursor = false;
            Application.DoEvents();
        }

        private void menuViewFitItem_Click(object sender, EventArgs e) {
            switchInFitMode(pbSource);
            switchInFitMode(pbTarget);
            switchInFitMode(pbDraft);
            showZoomImage(pbSource);
        }

        /**
         * Режим авто подбора размера картинки
         */
        private static void switchInFitMode(PictureBox pictureBox) {
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.Parent.AutoScrollOffset = new Point(0, 0);
        }

        /**
         * Режим масштабирования картинки
         */
        private static void switchInScaleMode(PictureBox pictureBox) {
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.Dock = DockStyle.None;
            scalePictureBox(pictureBox);
        }

        private void menuViewScaleItem_Click(object sender, EventArgs e) {
            switchInScaleMode(pbSource);
            switchInScaleMode(pbTarget);
            switchInScaleMode(pbDraft);
            showZoomImage(pbSource);
        }

        private void scalePictureBox(PictureBox pb, int zoomIndex) {
            pb.Tag = zoomIndex;
            scalePictureBox(pb);
            showZoomImage(pb);
        }

        private void showZoomImage(PictureBox pb) {
            if (pb.Dock == DockStyle.Fill) {
                menuZoomButton.Text = l("Заполнить");
            } else {
                menuZoomButton.Text = string.Format(l("Масштаб: {0}%"), getZoomFactor(pb) * 100.0);
            }
        }

        private static void scalePictureBox(PictureBox pb) {
            double zoomFact = getZoomFactor(pb);
            if (pb.Image == null) {
                return;
            }
            pb.Parent.AutoScrollOffset = new Point(0, 0);
            pb.SizeMode = PictureBoxSizeMode.Zoom;
            pb.Dock = DockStyle.None;
            pb.Width = (int) (pb.Image.Width * zoomFact);
            pb.Height = (int) (pb.Image.Height * zoomFact);
        }

        private static double getZoomFactor(Control ctrl) {
            if (ctrl.Tag is double tag)
                return tag;
            else
                return ctrl.Tag is int ctrlTag ? zoomFactor[ctrlTag] : 1;
        }

        private void menuItem10Percent_Click(object sender, EventArgs e) {
            zoomAll(zoomIndex10percent);
        }

        private void zoomAll(int zoomIndex) {
            scalePictureBox(pbSource, zoomIndex);
            scalePictureBox(pbTarget, zoomIndex);
            scalePictureBox(pbDraft, zoomIndex);
        }

        private void pictureBox_MouseWheel(object sender, MouseEventArgs e) {
            if (Control.ModifierKeys != Keys.Control) {
                return;
            }
            PictureBox pictureBox = null;
            if (sender is Panel) {
                foreach (var ctrl in ((Panel) sender).Controls) {
                    if (ctrl is PictureBox) {
                        pictureBox = (PictureBox) ctrl;
                        if (pictureBox.Image == null) {
                            return;
                        }
                        break;
                    }
                }
                if (pictureBox == null) {
                    return;
                }
            } else {
                pictureBox = sender as PictureBox;
            }
            if (pictureBox.Tag == null) {
                pictureBox.Tag = zoomIndexDefault;
            }

            bool zoomBy10Percent = false;
            if (Control.ModifierKeys == Keys.Shift) {
                zoomBy10Percent = true;
            }
            ((HandledMouseEventArgs) e).Handled = true;
            if (zoomBy10Percent) {
                //todo реализовать
            }
            pictureBox.Parent.AutoScrollOffset = new Point(0, 0);
            if (pictureBox.Tag is int) {
                int zoomIndex = (int) pictureBox.Tag;
                int newZoomIndex = e.Delta > 0 ? Math.Min(zoomFactor.Length - 1, zoomIndex + 1) : Math.Max(0, zoomIndex - 1);
                if (zoomIndex == newZoomIndex) {
                    return;
                }
                pictureBox.Tag = newZoomIndex;
            } else {
                double zoomFactor = (double) pictureBox.Tag;
                double newZoomFactor = e.Delta > 0 ? Math.Min(1, zoomFactor + 0.1) : Math.Max(0, zoomFactor - 0.1);
                if (Math.Abs(zoomFactor - newZoomFactor) < TOLERANCE) {
                    return;
                }
                pictureBox.Tag = newZoomFactor;
            }
            if (isFitMode(pictureBox)) {
                switchInScaleMode(pictureBox);
            }
            scalePictureBox(pictureBox);
            showZoomImage(pictureBox);
        }

        private static bool isFitMode(PictureBox pictureBox) {
            return pictureBox.Dock == DockStyle.Fill; //pictureBox.SizeMode != PictureBoxSizeMode.Zoom;
        }

        private void form2ToolStripMenuItem1_Click(object sender, EventArgs e) {
            new ProcessImageForm(Program.log).ShowDialog();
        }

        private void form2ToolStripMenuItem_Click(object sender, EventArgs e) {
            Form1 form1 = new Form1();
            form1.ShowDialog();
        }

        private bool openImageWithDialog(bool settingsProcessWhenOpen) {
            using (OpenFileDialog ofd = new OpenFileDialog()) {
                ofd.Filter = l("Графический файл (*.jpg, *.jpeg, *.jpe, *.jfif, *.tif, *.tiff, *.png, *.bmp)|*.bmp;*.jpg;*.jpeg;*.jpe;*.jfif;*.tif;*.tiff;*.png;*.bmp|Все файлы (*.*)|*.*");
                if (!string.IsNullOrEmpty(fileName)) {
                    ofd.FileName = Path.GetFileName(fileName);
                    ofd.InitialDirectory = Path.GetDirectoryName(fileName);
                }

                if (ofd.ShowDialog() == DialogResult.OK) {
                    imageProcessState = null;
                    prepareProcessImageState();
                    fileName = ofd.FileName;
                    bool result = openFile(fileName);
                    if (result) {
                        postProcessImageState();
                        if (settingsProcessWhenOpen) {
                            postProcessImageState();
                            prepareProcessImageState();
                            return processImage();
                        }
                    }
                    return result;
                }
            }

            return false;
        }

        private void cbSelectIgnoreColor_Click(object sender, EventArgs e) {
            pbDraft.Cursor = Cursors.Cross;
        }

        private void lbHintFileOpen_Click(object sender, EventArgs e) {
            menuImageOpenButton.PerformClick();
        }

        private void lbHintProcess_Click(object sender, EventArgs e) {
            menuImageProcessItem.PerformClick();
        }

        private void menuImageCloseItem_Click(object sender, EventArgs e) {
            pbDraft.Image = null;
            pbSource.Image = null;
            pbTarget.Image = null;
            imageProcessState = true;
            postProcessImageState();
            imageProcessState = null;
            WinSpecific.clearMemory();
        }

        private void menuImageOpenItem_Click(object sender, EventArgs e) {
            resetProgress();
            setProgress(true);
            string msg = "Обработка завершена";
            if (!openImageWithDialog(true)) {
                msg = "Возникла ошибка при обработке";
                WinSpecific.SetState(statusBarProgressBar.ProgressBar, (int) WinSpecific.ProgressBarState.ERROR);
                statusBarInfo.BackColor = Color.Red;
            }
            setProgress(false, msg);
        }

        private void saveTextForTranslateToolStripMenuItem_Click(object sender, EventArgs e) {
            Program.localize.saveCollectedLine();
        }

        private void englishToolStripMenuItem_Click(object sender, EventArgs e) {
            Localize.Language = "en-US";
            Program.localize.localizeControl(this);
        }

        private void russianToolStripMenuItem_Click(object sender, EventArgs e) {
            Localize.Language = null;
            Program.localize.revert(true);
            Program.localize.localizeControl(this);
            Program.localize.revert(false);
            Program.localize.resetLoadTranslate();
        }

        private void menuImageSaveItem_Click(object sender, EventArgs e) {
            saveImageToFile(false);
        }

        private void saveImageToFile(bool directSave) {
            if (imageProcessState != true || pbTarget.Image == null || directSave && string.IsNullOrEmpty(fileName)) {
                MessageBox.Show(l("Невозможно скопировать обработанный файл. Необходимо открыть и обработать файл с изображением."),
                    l("Сохранение обработанного файла изображения"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = l("Графический файл (*.jpg)|*.jpg|Графический файл (*.png)|*.png|Графический файл (*.tiff)|*.tiff|Графический файл (*.bmp)|*.bmp");
            if (!string.IsNullOrEmpty(fileName)) {
                string extension = Path.GetExtension(fileName).ToLower();
                saveFileDialog.FilterIndex = getFilterByExt(extension);
                saveFileDialog.InitialDirectory = Path.GetDirectoryName(fileName);
                saveFileDialog.FileName = Path.GetFileNameWithoutExtension(fileName);
            }
            if (!string.IsNullOrWhiteSpace(settings.CustomSavePath)) {
                saveFileDialog.InitialDirectory = settings.CustomSavePath;
            }
            if (directSave) {
                string outFileName = Path.Combine(saveFileDialog.InitialDirectory,  Path.GetFileName(fileName));
                pbTarget.Image.Save(outFileName, getFilterByIndex(saveFileDialog.FilterIndex));
                System.Media.SystemSounds.Hand.Play();
            } else if (saveFileDialog.ShowDialog(this) == DialogResult.OK) {
                pbTarget.Image.Save(saveFileDialog.FileName, getFilterByIndex(saveFileDialog.FilterIndex));
            }
            statusBarInfo.Text = "Файл успешно сохранён";
        }

        public static int getFilterByExt(string extension) {
            switch (extension) {
                case ".jpg":
                case ".jpeg":
                case ".jfif":
                case ".jpe":
                    return 1;
                
                case ".png":
                    return 2;

                case ".tiff":
                case ".tif":
                    return 3;
                
                default:
                case ".bmp":
                    return 4;
            }
        }

        public static ImageFormat getFilterByIndex(int filterIndex) {
            switch (filterIndex) {
                case 1:
                    return ImageFormat.Jpeg;
                
                case 2:
                    return ImageFormat.Png;
                
                case 3:
                    return ImageFormat.Tiff;
                
                default:
                case 4:
                    return ImageFormat.Bmp;
            }
        }

        private void copyToolStripButton_Click(object sender, EventArgs e) {
            if (imageProcessState != true || pbTarget.Image == null) {
                MessageBox.Show(l("Скопировать можно только обработанный файл."), l("Копирование в буфер обмена"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Clipboard.SetImage(pbTarget.Image);
        }

        private void menuPasteButton_Click(object sender, EventArgs e) {
            if (!Clipboard.ContainsImage()) {
                return;
            }

            imageProcessState = null;
            prepareProcessImageState();
            readImage(Clipboard.GetImage());
            fileName = null;
            postProcessImageState();
        }

        private void menuImageSaveDraftItem_Click(object sender, EventArgs e) {
            if (imageProcessState == null || pbDraft.Image == null) {
                MessageBox.Show(l("Невозможно скопировать черновой файл. Необходимо открыть и обработать файл с изображением."), l("Сохранение чернового файла изображения"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = l("Графический файл (*.bmp)|*.bmp");
            if (!string.IsNullOrEmpty(fileName)) {
                saveFileDialog.InitialDirectory = Path.GetDirectoryName(fileName);
                saveFileDialog.FileName = Path.ChangeExtension(Path.GetFileName(fileName), ".draft.bmp");
            }

            if (saveFileDialog.ShowDialog(this) == DialogResult.OK) {
                pbDraft.Image.Save(saveFileDialog.FileName);
            }
        }

        private void mainForm_Load(object sender, EventArgs e) {
            Program.localize.localizeControlAll(this);

            settings.loadFromXml(AppSettingFileName);
            propertyGrid1.SelectedObject = settings.PropertyObject;
            propertyGrid1.Refresh();
            statusBarInfo.Text = l("(Слева отображается шкала процесса обработки изображения)");
        }

        private void mainForm_FormClosed(object sender, FormClosedEventArgs e) {
            settings.saveToXml(AppSettingFileName);
        }

        private void pbTarget_Click(object sender, EventArgs e) {
            // Graphics graphics = pbTarget.CreateGraphics();
            // Point p = pbTarget.PointToClient(MousePosition);
            // graphics.DrawLine(new Pen(Brushes.Crimson, 3), 0, p.Y, pbTarget.Width, p.Y);
        }

        private void menuZoom50Button_Click(object sender, EventArgs e) {
            zoomAll(zoomIndex50percent);
        }

        private void menuZoom100Button_Click(object sender, EventArgs e) {
            zoomAll(zoomIndex100percent);
        }

        private void menuZoom1Button_Click(object sender, EventArgs e) {
            zoomAll(zoomIndex1percent);
        }

        private void menuZoom150Button_Click(object sender, EventArgs e) {
            zoomAll(zoomIndex150percent);
        }

        private void menuZoom200Button_Click(object sender, EventArgs e) {
            zoomAll(zoomIndex200percent);
        }

        private void menuZoom300Button_Click(object sender, EventArgs e) {
            zoomAll(zoomIndex300percent);
        }

        private void menuZoom5Button_Click(object sender, EventArgs e) {
            zoomAll(zoomIndex5percent);
        }

        private void menuZoomFitButton_Click(object sender, EventArgs e) {
            MenuViewFitItem.PerformClick();
        }

        private void menuZoom25Button_Click(object sender, EventArgs e) {
            zoomAll(zoomIndex25percent);
        }

        private void lbHintImageOpen2_Click(object sender, EventArgs e) {
            workFlowImageProcessExecute();
        }

        private void menuZoom15Button_Click(object sender, EventArgs e) {
            zoomAll(zoomIndex15percent);
        }

        private void menuContextResetItem_Click(object sender, EventArgs e) {
            propertyGrid1.ResetSelectedProperty();
        }

        private void pbSource_Click(object sender, MouseEventArgs e) {
            using (Graphics g = Graphics.FromImage(pbSource.Image)) {
                Point pt = e.Location;
                drawler.drawRect(g, spotBrush, pt.X, pt.Y, 20);
            }
            pbSource.Invalidate();
        }

        private void menuImageQuickSaveAsItem_Click(object sender, EventArgs e) {
            saveImageToFile(true);
        }

        private void MainForm_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e) {
            if (e.Alt && e.KeyCode == Keys.D1) {
                menuImageOpenItem.PerformClick();
            }
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e) {
            if (e.Alt && e.KeyCode == Keys.D1) {
                menuImageOpenItem.PerformClick();
            }
        }
    }

    public class Line {
        public Line(int x, int y1, int x2, int y2) {
            X = x;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
        }

        public int X { get; }
        public int Y1 { get; }
        public int X2 { get; }
        public int Y2 { get; }

        public override string ToString() {
            return string.Format("x = {0}, y1 = {1}, x2 = {2}, y2 = {3}", X, Y1, X2, Y2);
        }
    }
}