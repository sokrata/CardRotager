using AForge.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

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
        private const string xmlLastFileName = "lastFileName";
        private const string xmlConvertOpenImage = "convertOpenImage";
        private const string xmlRotateSubImages = "rotateSubImages";
        private const string xmlProcessCycleF4 = "processCycleF4";
        private const string xmlShowFoundContour = "showFoundContour";
        private const string xmlShowLastDot = "showLastDot";
        private const string xmlShowRuler = "showRuler";
        private const string xmlShowHelpLines = "showHelpLines";
        private const string xmlShowTargetFrame = "showTargetFrame";
        private const string xmlProcessWhenOpen = "processWhenOpen";
        
        private const int CHECK_ACCURACY = 300;

        /// <summary>
        /// Статус обработки изображения: Null - не открыт, false - открыт но не обработан, true - открыт и обработан
        /// </summary>
        private bool? imageProcessState;

        private string fileName;
        public Drawler drawler;
        public static Localize localize;
        private ImageProcessor ip;

        //false-режим не работает, т.к. масштаб для pictureBox не учитывается
        bool drawHelpLineOnPaint = false;
        private Pen redDashPen;
        public string AppSettingFileName { get; set; } = Path.Combine(Application.UserAppDataPath, "setting.xml");

        public MainForm() {
            InitializeComponent();
            imageProcessState = null;
            localize = new Localize();

            pbDraft.MouseWheel += pictureBox_MouseWheel;
            panelDraft.MouseWheel += pictureBox_MouseWheel;
            pbSource.MouseWheel += pictureBox_MouseWheel;
            panelOriginal.MouseWheel += pictureBox_MouseWheel;
            pbTarget.MouseWheel += pictureBox_MouseWheel;
            panelTarget.MouseWheel += pictureBox_MouseWheel;

            lbHintImageOpen.Visible = true;
            lbHintDraft.Visible = true;
            pbSource.Visible = false;

            lbHintTarget.Visible = true;
            pbTarget.Visible = false;

            pbDraft.Visible = false;
            pbDraft.Paint += onPbDraftOnPaint;
            pbSource.Paint += onPbSourceOnPaint;
            pbTarget.Paint += onPbTargetOnPaint;

            float[] dashValues = {5, 2, 15, 4};
            redDashPen = new Pen(Color.Red, 5) {
                DashPattern = dashValues
            };

            drawler = new Drawler();
        }

        private void processImage() {
            StringBuilder sb = new StringBuilder();
            ip = new ImageProcessor(localize, sb);
            Bitmap bmpDraft = ((Bitmap) pbDraft.Image);
            pbTarget.Image = null;

            ip.fillHVLinesAll(bmpDraft);

            int width = bmpDraft.Width;
            int height = bmpDraft.Height;
            using (Graphics graphics = Graphics.FromImage(bmpDraft)) {
                if (!drawHelpLineOnPaint) {
                    drawVerticalDots(graphics, sb);
                }

                sb.AppendLine(l("Линии-границы поиска горизонтальных линий ниже верхних:"));
                if (!drawHelpLineOnPaint) {
                    drawDashLine(graphics, sb);
                }

                sb.AppendFormat(l(l("\r\nИтоговые горизонтальные линии: \r\n")));
                
                if (!drawHelpLineOnPaint) {
                    drawHLines(graphics, sb);
                }

                sb.AppendFormat(l("\r\nИтоговые вертикальные линии: \r\n"));
                if (!drawHelpLineOnPaint) {
                    drawVLines(graphics, sb);
                }

                sb.AppendLine(l("\r\nЛинии, по которым рассчитывается угол наклона карт\r\n"));
                if (!drawHelpLineOnPaint) {
                    drawAngleLines(graphics, sb);
                }

                List<Rectangle> rectangles = makeRect(sb, ip.LinesAll, out List<float> angles);

                if (!drawHelpLineOnPaint) {
                    drawRuler(graphics, width, height);
                }

                const int EXTEND_SIDE = 50;
                Bitmap originalImage = (Bitmap) pbSource.Image;
                Bitmap targetImage = createEmptyBitmapSource(width, height, originalImage);
                using (Graphics targetGraphics = Graphics.FromImage(targetImage)) {
                    List<Rectangle> rectDest = drawler.makeFrame();
                    targetGraphics.Clear(Color.White);
                    for (int i = 0; i < rectDest.Count; i++) {
                        if (i >= rectangles.Count) {
                            break;
                        }

                        float angle = 0;
                        if (cbRotateFoundSubImages.Checked) {
                            angle = -angles[i];
                        }
                        // Debug.WriteLine("{0}: to: {1}, from: {2}, an = {3}", i, rectDest[i], rectangles[i], angle);
                        copyRegionIntoImage(targetGraphics, rectDest[i], originalImage, rectangles[i], angle, EXTEND_SIDE, i);
                    }

                    if (!drawHelpLineOnPaint) {
                        drawTargetFrame(targetGraphics, width, height);
                    }
                }

                pbTarget.Image = targetImage;

                tbLog.Text = sb.ToString();
                if (!drawHelpLineOnPaint) {
                    using (Graphics originalGraphic = Graphics.FromImage(originalImage)) {
                        drawFoundContours(originalGraphic, rectangles);
                    }
                }
                pbSource.Invalidate();
                pbTarget.Invalidate();
                pbDraft.Invalidate();
            }

            WinSpecific.clearMemory();
        }

        private void onPbDraftOnPaint(object sender, PaintEventArgs args) {
            if (ip == null || !drawHelpLineOnPaint) {
                return;
            }
            Graphics graphics = args.Graphics;
            drawVerticalDots(graphics, null);
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
            drawTargetFrame(graphics, image.Width, image.Height);
            drawRuler(graphics, image.Width, image.Height);
        }

        private void drawHLines(Graphics graphics, StringBuilder sb) {
            if (cbShowHelpLines.Checked || cbShowHorVertLines.Checked) {
                for (int colIndex = 0; colIndex < ip.LinesAll.Count; colIndex++) {
                    for (int rowIndex = 0; rowIndex < ip.LinesAll[colIndex].Count; rowIndex++) {
                    drawler.drawLine(graphics, ip.LinesAll[colIndex][rowIndex].HLine, Pens.Cyan, rowIndex, sb, true, ImageProcessor.THICK);
                    }
                }
            }
        }

        private void drawVLines(Graphics graphics, StringBuilder sb) {
            if (cbShowHelpLines.Checked || cbShowHorVertLines.Checked) {
                for (int colIndex = 0; colIndex < ip.LinesAll.Count; colIndex++) {
                    for (int rowIndex = 0; rowIndex < ip.LinesAll[colIndex].Count; rowIndex++) {
                        drawler.drawLine(graphics, ip.LinesAll[colIndex][rowIndex].VLine, Pens.Blue, rowIndex, sb, true, ImageProcessor.THICK);
                    }
                }
            }
        }

        private void drawFoundContours(Graphics graphics, List<Rectangle> rectangles) {
            if (cbShowHelpLines.Checked || cbShowFoundContour.Checked) {
                drawler.drawRect(graphics, rectangles);
            }
        }

        private void drawTargetFrame(Graphics graphics, int width, int height) {
            const int penWidth = 7;
            Pen penFrame = new Pen(Color.LimeGreen, penWidth);
            if (cbShowHelpLines.Checked || cbShowTargetFrame.Checked) {
                drawler.drawTargetFrame(graphics, penFrame, width, height);
            }
        }

        private void drawRuler(Graphics graphics, int width, int height) {
            if (cbShowHelpLines.Checked || cbShowRuler.Checked) {
                drawler.drawRuler(graphics, width, height);
            }
        }

        private void drawAngleLines(Graphics graphics, StringBuilder sb) {
            Pen anglePen = new Pen(Color.Lime, 12);
            foreach (var angle in ip.AngleEdges) {
                sb?.AppendFormat("{0}\r\n", angle);
                if (cbShowHelpLines.Checked) {
                    graphics.DrawLine(anglePen, angle.X, angle.Y, angle.X2, angle.Y2);
                }
            }
        }

        private void drawVerticalDots(Graphics graphics, StringBuilder sb) {
            if (cbShowHelpLines.Checked || cbLastDot.Checked) {
                sb?.AppendLine("Последние список точек для создания линий:");
                //drawler.drawLine(graphics, ip.ShowLines, null, sb);
                //drawler.drawHorizontalDots(graphics, Pens.MintCream, 2, ip.HDots);
                Drawler.drawVerticalDots(graphics, Pens.Red, 0, ip.DotsVertical);
            }
        }

        private void drawDashLine(Graphics graphics, StringBuilder sb) {
            bool showHelpLines = cbShowHelpLines.Checked;
            foreach (var item in ip.DashLines) {
                sb?.AppendFormat("{0}\r\n", item);
                if (showHelpLines) {
                    graphics.DrawLine(redDashPen, item.X, item.Y1, item.X2, item.Y2);
                }
            }
        }

        private string l(string text) {
            return localize.localize(text);
        }

        public static Bitmap createEmptyBitmapSource(int width, int height, Bitmap bitmapOriginal) {
            PixelFormat pf = bitmapOriginal.PixelFormat;
            var bitmap = new Bitmap(width, height, pf);
            bitmap.SetResolution(bitmapOriginal.HorizontalResolution, bitmapOriginal.VerticalResolution);

            return bitmap;
        }

        public static void copyRegionIntoImage(Graphics toGraphic, Rectangle toRegion, Bitmap fromBitmap, Rectangle fromRegion, float angle, int extend, int imageIndex) {
            const int CROP_PADDING = 80;
            //увеличим на запас (защита от скоса сторон картинки)
            double fromRegionWidth = fromRegion.Width;
            fromRegion.Inflate(new Size(extend, extend));
            int addX = (toRegion.Width - fromRegion.Width) / 2;
            int addY = (toRegion.Height - fromRegion.Height) / 2;
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
                    // rotatedGraphic.ScaleTransform(scale, scale);

                    rotatedGraphic.DrawImage(fromBitmap, new Rectangle(0, 0, rotatedBitmap.Width, rotatedBitmap.Height), fromRegion, GraphicsUnit.Pixel);
                    rotatedGraphic.RotateTransform(-angle);
                    rotatedGraphic.FillRectangle(Brushes.White, new Rectangle(0, -extend / 2, rotatedBitmap.Width, shiftY + extend / 2 + CROP_PADDING));

                    // rotatedBitmap.Save(string.Format("d:\\temp\\tmpimg\\save{0}.bmp", imageIndex + 1));
                }

                Rectangle srcRect = new Rectangle(0, 0, rotatedBitmap.Width, rotatedBitmap.Height);
                toGraphic.DrawImage(rotatedBitmap, toRegion.X + addX, toRegion.Y + addY, srcRect, GraphicsUnit.Pixel);
            }
        }

        /// <summary>
        /// Сформировать линии для горизонтальных hLinesAll и вертикальных vLinesAll и высчитать углы в градусах списка angles
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="hLinesAll"></param>
        /// <param name="vLinesAll"></param>
        /// <param name="angles"></param>
        /// <returns></returns>
        private List<Rectangle> makeRect(StringBuilder sb, List<List<HVLine>> LinesAll, out List<float> angles) {
            sb?.AppendFormat(l("\r\nГабариты карт:\r\n"));
            List<Rectangle> rectangles = new List<Rectangle>();
            angles = new List<float>();
            int i = 0;
            for (int hIndex = 0; hIndex < LinesAll.Count; hIndex++) {
                for (int vIndex = 0; vIndex < LinesAll[hIndex].Count; vIndex++) {
                    Edge hLine = LinesAll[hIndex][vIndex].HLine;
                    Edge vLine = LinesAll[hIndex][vIndex].VLine;
                    int minY = min(vLine.Y, hLine.Y, hLine.Y2);
                    int maxY = vLine.Y2;
                    int minX = hLine.X;
                    int maxX = max(hLine.X2, vLine.X, vLine.X2);
                    float angle = (float) hLine.Angle;
                    angles.Add(angle);
                    Rectangle item = new Rectangle(minX, minY, maxX - minX, maxY - minY);
                    sb?.AppendFormat(l("{0}: {1}, angle = {2}\r\n"), i + 1, item, angle);
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
                this.fileName = fileName;
                Image initImage = Image.FromFile(fileName);
                readImage(initImage);
                //tbLog.Text = string.Format("w = {0}, h = {1}, w,h = {2}, {3}; res X,Y = {4}, {5}", bitmap.Width, bitmap.Height, imageBlackWhite.Width, imageBlackWhite.Height, this.bitmap.HorizontalResolution, this.bitmap.VerticalResolution);
                Text = fileName;
                return true;
            } catch (Exception ex) {
                MessageBox.Show(l("Ошибка: ") + ex.Message, l("Невозможно открыть файл"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            } finally {
                WinSpecific.clearMemory();
            }
        }

        private void readImage(System.Drawing.Image initImage) {
            Bitmap bitmap;
            pbSource.Image = initImage;
            if (cbConvertOpenImage.Checked) {
                using (Bitmap image = new Bitmap(initImage)) {
                    int thresholdValue = 227;
                    IFilter threshold = new Threshold(thresholdValue);
                    using (Bitmap image2 = Grayscale.CommonAlgorithms.RMY.Apply(image)) {
                        using (Bitmap bitmap1 = threshold.Apply(image2)) {
                            //using(var bitmap1 = image3) {
                            MemoryStream ms = new MemoryStream();
                            bitmap1.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                            var streamBitmap = System.Drawing.Image.FromStream(ms);
                            bitmap = new Bitmap(streamBitmap);
                        }
                    }
                }
            } else {
                bitmap = (Bitmap) initImage;
            }

            pbDraft.Image = bitmap;
            pbDraft.Width = bitmap.Width;
            pbDraft.Height = bitmap.Height;
            pbTarget.Image = null;
            reloadZoom(pbDraft);
            reloadZoom(pbSource);
        }

        private static void reloadZoom(PictureBox pictureBox) {
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
        }

        private void buttonProcess_Click(object sender, EventArgs e) {
            workFlowImageProcessExecute();
        }

        private void workFlowImageProcessExecute() {
            prepareProcessImageState();
            switch (imageProcessState) {
                case null: {
                    if (openPlusProcess()) {
                        return;
                    }
                    break;
                }
                case false:
                    processImage();
                    break;
                default: {
                    if (cbProcessCycleF4.Checked) {
                        imageProcessState = null;
                        prepareProcessImageState();
                        if (openPlusProcess()) {
                            return;
                        }
                    }
                    break;
                }
            }

            postProcessImageState();
        }

        private bool openPlusProcess() {
            if (imageOpen()) {
                if (cbProcessWhenOpen.Checked) {
                    postProcessImageState();
                    prepareProcessImageState();
                    processImage();
                } else {
                    return true;
                }
            }
            return false;
        }

        private bool imageOpen() {
            if (fileName == null) {
                if (!openImageWithDialog()) {
                    resetImageState();
                }
                return true;
            }

            return openFile(fileName);
        }

        private void prepareProcessImageState() {
            if (imageProcessState == null) {
                lbHintImageOpen.Text = l("Открытие изображение.\r\nФормирование промежуточного ч/б изображения на закладке 'Обработка'...");
                lbHintDraft.Text = l("Открыто изображение.\r\nСформировать промежуточное ч/б изображение\r\n(щелкните сюда)");
                lbHintTarget.Text = l("Сформировать файл изображения с центрированными изображения картами\r\n(щелкните сюда)");
            } else if (imageProcessState == false) {
                lbHintTarget.Text = l("Обработка изображения.\r\nФормирование итогового изображения...");
                lbHintDraft.Text = l("Открыто оригинальное изображение.\r\nДля формирования промежуточного изображения\r\n(щелкните сюда)...");
            } else {
                lbHintImageOpen.Text = l("Открытие изображение.\r\nФормирование промежуточного ч/б изображения на закладке 'Обработка'...");
                lbHintDraft.Text = l("Открытие изображение.\r\nФормирование промежуточного ч/б изображения...");
            }

            Application.DoEvents();
            WinSpecific.UseWaitCursor = true;
        }

        private void postProcessImageState() {
            if (imageProcessState == null) {
                pbSource.Visible = true;
                lbHintImageOpen.Visible = false;

                pbTarget.Visible = false;
                lbHintTarget.Visible = true;

                lbHintDraft.Visible = true;
                pbDraft.Visible = false;

                imageProcessState = false;
            } else if (imageProcessState == false) {
                lbHintImageOpen.Visible = false;
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
            lbHintImageOpen.Visible = true;
            lbHintDraft.Visible = true;
            lbHintTarget.Visible = true;
            lbHintImageOpen.Text = l("Открыть файл изображения с картами...\r\n(щелкните сюда)");
            lbHintDraft.Text = l("Открыть файл изображения с картами...\r\n(щелкните сюда)");
            lbHintTarget.Text = l("Сформировать файл изображения с центированные изображения картами\r\n(щелкните сюда)");
            WinSpecific.UseWaitCursor = false;
            Application.DoEvents();
        }

        private void MenuViewFitItem_Click(object sender, EventArgs e) {
            switchInFitMode(pbDraft);
            switchInFitMode(pbSource);
            switchInFitMode(pbTarget);
            showZoomImage(pbSource);
        }

        /**
         * Режим авто подбора размера картинки
         */
        private static void switchInFitMode(PictureBox pictureBox) {
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.Dock = DockStyle.Fill;
        }

        /**
         * Режим масштабирования картинки
         */
        private static void switchInScaleMode(PictureBox pictureBox) {
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.Dock = DockStyle.None;
            scalePictureBox(pictureBox);
        }

        private void MenuViewScaleItem_Click(object sender, EventArgs e) {
            switchInScaleMode(pbDraft);
            switchInScaleMode(pbSource);
            switchInScaleMode(pbTarget);
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
            double zoomFactor = getZoomFactor(pb);
            if (pb.Image == null) {
                return;
            }
            pb.SizeMode = PictureBoxSizeMode.Zoom;
            pb.Dock = DockStyle.None;
            pb.Width = (int) ((double) pb.Image.Width * zoomFactor);
            pb.Height = (int) ((double) pb.Image.Height * zoomFactor);
            pb.Left = 0;
            pb.Top = 0;
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
            scalePictureBox(pbDraft, zoomIndex);
            scalePictureBox(pbSource, zoomIndex);
            scalePictureBox(pbTarget, zoomIndex);
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
            new Form2().ShowDialog();
        }

        private void form2ToolStripMenuItem_Click(object sender, EventArgs e) {
            Form1 form1 = new Form1();
            form1.ShowDialog();
        }

        private bool openImageWithDialog() {
            using (OpenFileDialog ofd = new OpenFileDialog()) {
                ofd.Filter = l("Графический файл (*.jpg, *.jpeg, *.png, *.bmp)|*.bmp;*.jpg;*.jpeg;*.png;*.bmp|Все файлы (*.*)|*.*");
                if (!string.IsNullOrEmpty(fileName)) {
                    ofd.FileName = Path.GetFileName(fileName);
                    ofd.InitialDirectory = Path.GetDirectoryName(fileName);
                }

                if (ofd.ShowDialog() == DialogResult.OK) {
                    imageProcessState = null;
                    prepareProcessImageState();
                    fileName = ofd.FileName;
                    bool result = openFile(fileName);
                    postProcessImageState();
                    if (cbProcessWhenOpen.Checked) {
                        postProcessImageState();
                        prepareProcessImageState();
                        processImage();
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
            openImageWithDialog();
        }

        private void menuImageProcessItem_Click(object sender, EventArgs e) {
            workFlowImageProcessExecute();
        }

        private void saveTextForTranslateToolStripMenuItem_Click(object sender, EventArgs e) {
            localize.saveCollectedLine();
        }

        private void englishToolStripMenuItem_Click(object sender, EventArgs e) {
            Localize.Language = "en-US";
            localize.localizeControl(this);
        }

        private void russianToolStripMenuItem_Click(object sender, EventArgs e) {
            Localize.Language = null;
            localize.revert(true);
            localize.localizeControl(this);
            localize.revert(false);
            localize.resetLoadTranslate();
        }

        private void menuImageSaveItem_Click(object sender, EventArgs e) {
            if (imageProcessState != true || pbTarget.Image == null) {
                MessageBox.Show(l("Невозможно скопировать обработанный файл. Необходимо открыть и обработать файл с изображением."),
                    l("Сохранение обработанного файла изображения"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = l("Графический файл (*.bmp)|*.bmp");
            if (fileName != null) {
                saveFileDialog.InitialDirectory = Path.GetDirectoryName(fileName);
                saveFileDialog.FileName = Path.ChangeExtension(Path.GetFileName(fileName), ".bmp");
            }

            if (saveFileDialog.ShowDialog(this) == DialogResult.OK) {
                pbTarget.Image.Save(saveFileDialog.FileName);
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
            if (fileName != null) {
                saveFileDialog.InitialDirectory = Path.GetDirectoryName(fileName);
                saveFileDialog.FileName = Path.ChangeExtension(Path.GetFileName(fileName), ".draft.bmp");
            }

            if (saveFileDialog.ShowDialog(this) == DialogResult.OK) {
                pbDraft.Image.Save(saveFileDialog.FileName);
            }
        }

        private void mainForm_Load(object sender, EventArgs e) {
            localize.localizeControlAll(this);

            if (File.Exists(AppSettingFileName)) {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(AppSettingFileName);

                XmlNode xmlRoot = xmlDoc.DocumentElement.SelectSingleNode("/settings");
                if (xmlRoot is XmlElement element) {
                    fileName = element.GetAttribute(xmlLastFileName);
                    if (bool.TryParse(element.GetAttribute(xmlConvertOpenImage), out bool cb)) {
                        cbConvertOpenImage.Checked = cb;
                    }
                    if (bool.TryParse(element.GetAttribute(xmlProcessCycleF4), out cb)) {
                        cbProcessCycleF4.Checked = cb;
                    }
                    if (bool.TryParse(element.GetAttribute(xmlShowFoundContour), out cb)) {
                        cbShowFoundContour.Checked = cb;
                    }
                    if (bool.TryParse(element.GetAttribute(xmlShowLastDot), out cb)) {
                        cbLastDot.Checked = cb;
                    }
                    if (bool.TryParse(element.GetAttribute(xmlShowRuler), out cb)) {
                        cbShowRuler.Checked = cb;
                    }
                    if (bool.TryParse(element.GetAttribute(xmlShowHelpLines), out cb)) {
                        cbShowHelpLines.Checked = cb;
                    }
                    if (bool.TryParse(element.GetAttribute(xmlShowHelpLines), out cb)) {
                        cbShowTargetFrame.Checked = cb;
                    }
                    if (bool.TryParse(element.GetAttribute(xmlRotateSubImages), out cb)) {
                        cbRotateFoundSubImages.Checked = cb;
                    }
                    if (bool.TryParse(element.GetAttribute(xmlProcessWhenOpen), out cb)) {
                        cbProcessWhenOpen.Checked = cb;
                    }
                }
            }
            //":\YandexDisk\Файлы\C# Sources\CardRotager\17+18. Оборот.jpg"
        }

        private void mainForm_FormClosed(object sender, FormClosedEventArgs e) {
            //if (File.Exists(AppSettingFileName)) {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<settings/>");
            XmlNode xmlRoot = xmlDoc.DocumentElement.SelectSingleNode("/settings");
            ((XmlElement) xmlRoot).SetAttribute(xmlLastFileName, fileName);
            ((XmlElement) xmlRoot).SetAttribute(xmlRotateSubImages, cbRotateFoundSubImages.Checked.ToString());
            ((XmlElement) xmlRoot).SetAttribute(xmlConvertOpenImage, cbConvertOpenImage.Checked.ToString());
            ((XmlElement) xmlRoot).SetAttribute(xmlProcessCycleF4, cbProcessCycleF4.Checked.ToString());
            ((XmlElement) xmlRoot).SetAttribute(xmlShowFoundContour, cbShowFoundContour.Checked.ToString());
            ((XmlElement) xmlRoot).SetAttribute(xmlShowLastDot, cbLastDot.Checked.ToString());
            ((XmlElement) xmlRoot).SetAttribute(xmlShowRuler, cbShowRuler.Checked.ToString());
            ((XmlElement) xmlRoot).SetAttribute(xmlShowHelpLines, cbShowHelpLines.Checked.ToString());
            ((XmlElement) xmlRoot).SetAttribute(xmlShowTargetFrame, cbShowTargetFrame.Checked.ToString());
            ((XmlElement) xmlRoot).SetAttribute(xmlProcessWhenOpen, cbProcessWhenOpen.Checked.ToString());
            xmlDoc.Save(AppSettingFileName);
            //}
        }

        private void pbTarget_Click(object sender, EventArgs e) {
            Graphics graphics = pbTarget.CreateGraphics();
            Point p = pbTarget.PointToClient(MousePosition);
            graphics.DrawLine(new Pen(Brushes.Crimson, 3), 0, p.Y, pbTarget.Width, p.Y);
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