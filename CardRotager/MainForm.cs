using AForge.Imaging;
using AForge.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;


namespace CardRotager {
    public partial class MainForm : Form {
        private static readonly double[] zoomFactor = { 0.1, .25, .50, .75, 1, 1.25, 1.5, 1.75, 2.0, 2.5, 3.0 };
        private const int zoomIndexDefault = 4;

        /// <summary>
        /// Статус обработки изображения: Null - не открыт, false - открыт но не обработан, true - открыт и обработан
        /// </summary>
        private bool? imageProcess;

        private string fileName;
        public Drawler drawler;
        private List<Rectangle> rectangles;
        public static Localize localize;

        public MainForm() {
            InitializeComponent();
            imageProcess = null;
            localize = new Localize();

            pbDraft.MouseWheel += pictureBox_MouseWheel;
            pbOriginal.MouseWheel += pictureBox_MouseWheel;
            pbTarget.MouseWheel += pictureBox_MouseWheel;
        }

        private void form1_Load(object sender, EventArgs e) {
            localizeControl();
            //fileName = @"C:\Users\sokra\OneDrive\Рабочий стол\www\17 + 18.Оборот.jpg";
            if (Directory.Exists("S:\\2021-08-21\\Дилеммы короля\\конверты")) {
                fileName = @"S:\2021-08-21\Дилеммы короля\конверты\17+18. Оборот.jpg";
            }
            if (Directory.Exists(@"E:\YandexDisk\Файлы\C# Sources\CardRotager")) {
                fileName = @"E:\YandexDisk\Файлы\C# Sources\CardRotager\17+18. Оборот.jpg";
            }
            //fileName = @"C:\Users\sokra\OneDrive\Рабочий стол\www\17+18. Оборот.jpg";
        }

        private void localizeControl() {
            localize.loadTranslatedText();
            localize.localizeControl(lbHintImageOpen);
            localize.localizeControl(lbHintImageProcess);
            localize.localizeControl(this);
            localize.localizeControl(tabPageBW);
            localize.localizeControl(tabPageImage);
            
            localize.localizeControl(menuImage);
            localize.localizeControl(menuImageOpenItem);
            localize.localizeControl(menuImageSaveItem);
            localize.localizeControl(menuImageSaveDraftItem);
            localize.localizeControl(menuImageCloseItem);
            localize.localizeControl(menuImageProcessItem);
            localize.localizeControl(menuImageExitItem);

            localize.localizeControl(MenuView);
            localize.localizeControl(MenuView10PercentItem);
            localize.localizeControl(MenuViewFitItem);
            localize.localizeControl(MenuViewScrollItem);
            
            localize.localizeControl(menuImageOpenButton);
            localize.localizeControl(menuImageSaveButton);
            localize.localizeControl(menuProcessButton);

            localize.localizeControl(label1);
        }

        private void processImage() {
            StringBuilder sb = new StringBuilder();
            ImageProcessor ip = new ImageProcessor(localize);
            Bitmap bitmap = ((Bitmap)pbDraft.Image);
            pbTarget.Image = null;

            ip.prepareData(sb, bitmap, out List<Line> dashLines,out List<Edge> angleEdge, out int[] showDots);

            int width = bitmap.Width;
            int height = bitmap.Height;
            using (Graphics graphics = Graphics.FromImage(bitmap)) {
                drawler.DrawDots(graphics, Pens.Red, 2, showDots, true);

                float[] dashValues = { 5, 2, 15, 4 };
                Pen redDashPen = new Pen(Color.Red, 5) {
                    DashPattern = dashValues
                };

                sb.AppendLine(l("Рисование линий-границ поиска горизонтальных линий ниже верхних:"));
                foreach (var item in dashLines) {
                    sb.AppendFormat("{0}\r\n", item);
                    graphics.DrawLine(redDashPen, item.X, item.Y1, item.X2, item.Y2);
                }

                sb.AppendFormat(l(l("\r\nИтоговые горизонтальные линии: \r\n")));
                ip.HLinesAll.Sort((x, y) => x.Y.CompareTo(y.Y));
                drawler.DrawLines(graphics, ip.HLinesAll, Pens.Cyan, sb, true, ImageProcessor.THICK);

                sb.AppendFormat(l("\r\nИтоговые вертикальные линии: \r\n"));
                ip.VLinesAll.Sort((line1, line2) => {
                    if (LinesDetectorBase.inRange(line1.Y, line2.Y, 100)) {
                        return line1.X.CompareTo(line2.X);
                    }
                    return line1.Y.CompareTo(line2.Y);
                });
                drawler.DrawLines(graphics, ip.VLinesAll, Pens.Blue, sb, true, ImageProcessor.THICK);

                sb.AppendLine(l("Рисование линий, по которым расчитывается угол наклона карт"));
                Pen anglePen = new Pen(Color.Lime, 12);
                foreach (var item in angleEdge) {
                    sb.AppendFormat("{0}\r\n", item);
                    graphics.DrawLine(anglePen, item.X, item.Y, item.X2, item.Y2);
                }

                rectangles = makeRect(sb, ip.HLinesAll, ip.VLinesAll, out List<float> angles);

                //drawler.DrawRuler(graphics);
                const int penWidth = 7;
                Pen penFrame = new Pen(Color.LimeGreen, penWidth);
                const int EXTEND_SIDE = 50;
                Bitmap originalImage = (Bitmap)pbOriginal.Image;
                Bitmap targetImage = createEmtpyBitmapSource(width, height, originalImage);
                using (Graphics targetGraphics = Graphics.FromImage(targetImage)) {
                    List<Rectangle> rectDest = drawler.MakeFrame();
                    targetGraphics.Clear(Color.White);
                    for (int i = 0; i < rectDest.Count; i++) {
                        if (i >= rectangles.Count) {
                            break;
                        }
                        copyRegionIntoImage(targetGraphics, rectDest[i], originalImage, rectangles[i], angles[i], EXTEND_SIDE);
                    }
                    //drawler.DrawFrame(targetGraphics, penFrame, rectDest);
                    drawler.DrawTargetFrame(targetGraphics, penFrame, width, height);
                }
                pbTarget.Image = targetImage;
                tbLog.Text = sb.ToString();
                using (Graphics originalGraphic = Graphics.FromImage(originalImage)) {
                    drawler.DrawRect(originalGraphic, rectangles);
                }
            }

            WinSpecific.clearMemory();
        }

        private string l(string text) {
            return localize.localize(text);
        }

        public static Bitmap createEmtpyBitmapSource(int width, int height, Bitmap bitmapOriginal) {
            PixelFormat pf = bitmapOriginal.PixelFormat;
            var bitmap = new Bitmap(width, height, pf);
            bitmap.SetResolution(bitmapOriginal.HorizontalResolution, bitmapOriginal.VerticalResolution);

            return bitmap;
        }

        public static void copyRegionIntoImage(Graphics toGraphic, Rectangle toRegion, Bitmap fromBitmap, Rectangle fromRegion, float angle, int extend) {
            //увеличим на запас (защита от скоса сторон картинки)
            fromRegion.Inflate(new Size(extend, extend));
            int addX = (toRegion.Width - fromRegion.Width) / 2;
            int addY = (toRegion.Height - fromRegion.Height) / 2;
            if (angle != 0) {
                using (Bitmap rotatedBitmap = createEmtpyBitmapSource(fromRegion.Width, fromRegion.Height, fromBitmap)) {
                    using (Graphics rotatedGraphic = Graphics.FromImage(rotatedBitmap)) {
                        rotatedGraphic.Clear(Color.White);
                        float centerX = (fromRegion.Left + fromRegion.Right) * 0.5f;
                        float centerY = (fromRegion.Top + fromRegion.Bottom) * 0.5f;
                        rotatedGraphic.TranslateTransform(centerX, centerY);
                        rotatedGraphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        rotatedGraphic.RotateTransform(angle);
                        rotatedGraphic.TranslateTransform(-centerX, -centerY);
                        float scale = (float)rotatedBitmap.Width / rotatedBitmap.Width;
                        rotatedGraphic.ScaleTransform(scale, scale);

                        rotatedGraphic.DrawImage(fromBitmap, new Rectangle(0, 0, rotatedBitmap.Width, rotatedBitmap.Height), fromRegion, GraphicsUnit.Pixel);
                    }
                    Rectangle srcRect = new Rectangle(0, 0, rotatedBitmap.Width, rotatedBitmap.Height);
                    toGraphic.DrawImage(rotatedBitmap, toRegion.X + addX, toRegion.Y + addY, srcRect, GraphicsUnit.Pixel);
                }
                return;
            }

            toGraphic.DrawImage(fromBitmap, toRegion.X + addX, toRegion.Y + addY, fromRegion, GraphicsUnit.Pixel);
        }

        private List<Rectangle> makeRect(StringBuilder sb, List<Edge> hLinesAll, List<Edge> vLinesAll, out List<float> angles) {
            sb?.AppendFormat(l("\r\nГабариты карт:\r\n"));
            List<Rectangle> rectangels = new List<Rectangle>();
            angles = new List<float>();
            int i = 0;
            for (int hIndex = 0; hIndex < hLinesAll.Count; hIndex++) {
                Edge hLine = hLinesAll[hIndex];
                if (hIndex < vLinesAll.Count) {
                    Edge vLine = vLinesAll[hIndex];
                    int minY = Math.Min(vLine.Y, hLine.Y);
                    int maxX = Math.Max(hLine.X2, vLine.X2);
                    float angle = (float)vLinesAll[hIndex].Angle;
                    angles.Add(angle);
                    Rectangle item = new Rectangle(hLine.X, minY, maxX - hLine.X, vLine.Y2 - minY);
                    sb?.AppendFormat("{0}: {1}, {2}\r\n", i++, item, angle);
                    rectangels.Add(item);
                }
            }
            return rectangels;
        }

        private void openFile(string fileName) {
            try {
                this.fileName = fileName;

                Bitmap bitmap;

                System.Drawing.Image initImage = System.Drawing.Image.FromFile(fileName);
                pbOriginal.Image = initImage;
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

                //bitmap = ToGray(bitmap);
                pbDraft.Image = bitmap;
                pbDraft.Width = bitmap.Width;
                pbDraft.Height = bitmap.Height;
                bitmap = null;

                pbOriginal.SizeMode = PictureBoxSizeMode.AutoSize;
                pbTarget.SizeMode = PictureBoxSizeMode.AutoSize;
                pbDraft.SizeMode = PictureBoxSizeMode.AutoSize;


                //graphics = Graphics.FromImage(bitmap);
                drawler = new Drawler(this);
                //graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                //tbLog.Text = string.Format("w = {0}, h = {1}, w,h = {2}, {3}; res X,Y = {4}, {5}", bitmap.Width, bitmap.Height, imageBlackWhite.Width, imageBlackWhite.Height, this.bitmap.HorizontalResolution, this.bitmap.VerticalResolution);

                Text = fileName;
            } catch (Exception ex) {
                MessageBox.Show(l("Ошибка: ") + ex.Message, l("Невозможно открыть файл"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            WinSpecific.clearMemory();
        }

        private void buttonProcess_Click(object sender, EventArgs e) {
            workFlowImageProcessExecute();
        }

        private void workFlowImageProcessExecute() {
            prepareProcessImageState();
            if (imageProcess == null) {
                openFile(fileName);
            } else if (imageProcess == false) {
                processImage();
            }
            postProcessImageState();
        }

        private void prepareProcessImageState() {
            if (imageProcess == null) {
                lbHintImageOpen.Text = l("Открытие изображение.\r\nФормирование промежуточного ч/б изображения на закладке 'Обработка'...");
                lbHintImageProcess.Text = l("Сформировать файл изображения с центированные изображения картами\r\n(щелкните сюда)");
            } else if (imageProcess == false) {
                lbHintImageProcess.Text = l("Обработка изображения.\r\nФормирование итогового изображения...");
            } else {
                lbHintImageOpen.Text = l("Открытие изображение.\r\nФормирование промежуточного ч/б изображения на закладке 'Обработка'...");
            }
            Application.DoEvents();
            WinSpecific.UseWaitCursor = true;
        }

        private void postProcessImageState() {
            if (imageProcess == null) {
                lbHintImageOpen.Visible = false;
                lbHintImageProcess.Visible = true;
                imageProcess = false;
            } else if (imageProcess == false) {
                lbHintImageOpen.Visible = false;
                lbHintImageProcess.Visible = false;
                imageProcess = true;
            } else {
                Debug.WriteLine(l("Изображение обработано"));
                lbHintImageOpen.Visible = true;
                lbHintImageProcess.Visible = true;
                lbHintImageOpen.Text = l("Открыть файл изображения с картами...\r\n(щелкните сюда)");
                lbHintImageProcess.Text = l("Сформировать файл изображения с центированные изображения картами\r\n(щелкните сюда)");
            }
            WinSpecific.UseWaitCursor = false;
            Application.DoEvents();
        }

        private void fitToolStripMenuItem_Click(object sender, EventArgs e) {
            switchInZoomMode(pbDraft);
            switchInZoomMode(pbOriginal);
            switchInZoomMode(pbTarget);
        }

        private void switchInZoomMode(PictureBox pbBlackWhite) {
            pbBlackWhite.SizeMode = PictureBoxSizeMode.Zoom;
            pbBlackWhite.Dock = DockStyle.Fill;
        }

        private void scrollToolStripMenuItem_Click(object sender, EventArgs e) {
            switchInScrollMode(pbDraft);
            switchInScrollMode(pbOriginal);
            switchInScrollMode(pbTarget);
        }

        private void switchInScrollMode(PictureBox pbBlackWhite) {
            pbBlackWhite.Dock = DockStyle.None;
            pbBlackWhite.SizeMode = PictureBoxSizeMode.AutoSize;
        }

        private void menuItem10Percent_Click(object sender, EventArgs e) {
            if (pbDraft == null || pbDraft.Image == null) {
                return;
            }
            zoomPictureBox(panelImage, pbDraft, label1, 0, 0, 0, true, 0, false);
            zoomPictureBox(panelOriginal, pbOriginal, label1, 0, 0, 0, true, 0, false);
            zoomPictureBox(panelOriginal, pbTarget, label1, 0, 0, 0, true, 0, false);
        }

        private void pictureBox_MouseWheel(object sender, MouseEventArgs e) {
            if (Control.ModifierKeys != Keys.Control) {
                return;
            }

            PictureBox pictureBox = sender as PictureBox;
            if (pictureBox.Tag == null) {
                pictureBox.Tag = zoomIndexDefault;
            }
            int zoomIndex = (int)pictureBox.Tag;

            zoomPictureBox(pictureBox.Parent as Panel, pictureBox, label1, zoomIndex, e.X, e.Y, e.Delta > 0, e.Delta, true);
        }

        private static void zoomPictureBox(Panel panel1, PictureBox pictureBox, Label label1, int zoomIndex, int x, int y, bool zoomIn, int step, bool changeZoom) {
            if (pictureBox.Image == null) {
                return;
            }
            int orgX = 0; //Transforms the location of mouse to the point at 100% resolution 
            int orgY = 0;
            if (zoomFactor[zoomIndex] != 1) {
                orgX = Convert.ToInt32(x / zoomFactor[zoomIndex]);
                orgY = Convert.ToInt32(y / zoomFactor[zoomIndex]);
            }
            if (zoomFactor[zoomIndex] == 1) {
                orgX = x;
                orgY = y;
            }
            if (zoomIn) {
                if (changeZoom && zoomIndex < zoomFactor.Length)
                    zoomIndex++;
                int zoom1 = Convert.ToInt32(zoomFactor[zoomIndex] * 100);
                label1.Text = String.Format("{0}%", zoom1);


                pictureBox.Image = zoomingControl(pictureBox.Image, zoomIndex);
                // using centre of picturebox to bring the the centre of the image in focus(centre of) of the client area(panel)
                //int X = (int)((this.pictureBox1.Width / 2) - panel1.Width/ 2);
                //int Y = (int)((this.pictureBox1.Height / 2) - panel1.Height / 2 + e.Delta);

                //using the mouse location to zoom
                int X = (int)((orgX * zoomFactor[zoomIndex]) - panel1.Width / 2);
                int Y = (int)((orgY * zoomFactor[zoomIndex]) - panel1.Height / 2 + step);
                panel1.AutoScrollPosition = new Point(X, Y);

            } else if (!zoomIn) {
                if (changeZoom && zoomIndex > 0)
                    zoomIndex--;
                int zoom1 = Convert.ToInt32(zoomFactor[zoomIndex] * 100);
                label1.Text = String.Format("{0}%", zoom1);
                pictureBox.Image = zoomingControl(pictureBox.Image, zoomIndex);
                // using centre of picturebox to bring the the centre of the image in focus(centre of) of the client area(panel)
                //int X = (int)((this.pictureBox1.Width / 2) - panel1.Width/ 2);
                //int Y = (int)((this.pictureBox1.Height / 2) - panel1.Height / 2 + e.Delta);

                //using the mouse location to zoom
                int X = (int)((orgX * zoomFactor[zoomIndex]) - panel1.Width / 2);
                int Y = (int)((orgY * zoomFactor[zoomIndex]) - panel1.Height / 2 + step);
                panel1.AutoScrollPosition = new Point(X, Y);
            }
            pictureBox.Invalidate();
        }

        private static Bitmap zoomingControl(System.Drawing.Image imgtmp, int zoomIndex) {
            int width = Convert.ToInt32(imgtmp.Width * zoomFactor[zoomIndex]);
            int height = Convert.ToInt32(imgtmp.Height * zoomFactor[zoomIndex]);
            Bitmap bm = new Bitmap(imgtmp, width, height);
            return bm;
        }

        private void form2ToolStripMenuItem1_Click(object sender, EventArgs e) {
            new Form2().ShowDialog();
        }

        private void form2ToolStripMenuItem_Click(object sender, EventArgs e) {
            Form1 form1 = new Form1();
            form1.ShowDialog();
        }

        private void menuImageOpen_Click(object sender, EventArgs e) {
            using (OpenFileDialog ofd = new OpenFileDialog()) {
                if (!string.IsNullOrEmpty(fileName)) {
                    ofd.FileName = fileName;
                    ofd.InitialDirectory = Path.GetDirectoryName(fileName);
                }
                if (ofd.ShowDialog() == DialogResult.OK) {
                    imageProcess = null;
                    prepareProcessImageState();
                    openFile(ofd.FileName);
                    fileName = ofd.FileName;
                    postProcessImageState();
                }
            }

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
            pbOriginal.Image = null;
            pbTarget.Image = null;
            imageProcess = true;
            postProcessImageState();
            imageProcess = null;
            WinSpecific.clearMemory();
        }

        private void menuImageOpenItem_Click(object sender, EventArgs e) {
            lbHintImageOpen.Visible = false;
            pbTarget.Image = null;
            pbDraft.Image = null;
            lbHintImageProcess.Visible = false;
        }

        private void menuImageProcessItem_Click(object sender, EventArgs e) {
            workFlowImageProcessExecute();
        }

        private void saveTextForTranslateToolStripMenuItem_Click(object sender, EventArgs e) {
            localize.saveCollectedLine();
        }

        private void englishToolStripMenuItem_Click(object sender, EventArgs e) {
            Localize.Language = "en-US";
            localizeControl();
        }

        private void russianToolStripMenuItem_Click(object sender, EventArgs e) {
            Localize.Language = null;
            localize.revert(true);
            localizeControl();
            localize.revert(false);
            localize.resetLoadTranslate();
        }

        private void menuImageSaveItem_Click(object sender, EventArgs e) {
            if (imageProcess != true || pbTarget.Image == null) {
                MessageBox.Show(l("Невозможно скопировать обработанный файл. Необходимо открыть и обработать файл с изображением."),l("Сохранение обработанного файла изображения"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (imageProcess != true || pbTarget.Image == null) {
                return;
            }
            Clipboard.SetImage(pbTarget.Image);
        }

        private void menuPasteButton_Click(object sender, EventArgs e) {
            if (imageProcess != true || !Clipboard.ContainsImage()) {
                return;
            }
            pbOriginal.Image = Clipboard.GetImage();
            imageProcess = false;
            fileName = null;
            pbDraft.Image = null;
            pbTarget.Image = null;
        }

        private void menuImageSaveDraftItem_Click(object sender, EventArgs e) {
            if (imageProcess != true || pbDraft.Image == null) {
                MessageBox.Show(l("Невозможно скопировать черновой файл. Необходимо открыть и обработать файл с изображением."), l("Сохранение чернового файла изображения"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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
