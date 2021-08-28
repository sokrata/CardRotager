using AForge.Imaging.Filters;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CardRotager {
    public partial class Form1 : Form {
        FileDialog openFileDialog;
        string fileName = null;

        public Form1() {
            InitializeComponent();
            label1.Text = "Threshold " + hScrollBar1.Value;
            stretchedToolStripMenuItem.PerformClick();

        }

        private void clearChecked() {
            normalToolStripMenuItem.Checked = false;
            fitToolStripMenuItem.Checked = false;
            stretchedToolStripMenuItem.Checked = false;
            centeredToolStripMenuItem.Checked = false;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e) {
            openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog(this);
            fileName = openFileDialog.FileName;
            if (fileName == string.Empty) return;
            initialPicture.Image = System.Drawing.Image.FromFile(fileName);
            hScrollBar1.Enabled = (initialPicture.Image != null);
            resetToolStripMenuItem.Enabled = (initialPicture.Image != null);
            hScrollBar1_Scroll(null, null);
        }

        private void normalToolStripMenuItem_Click(object sender, EventArgs e) {
            clearChecked();
            normalToolStripMenuItem.Checked = true;
            initialPicture.SizeMode = PictureBoxSizeMode.Normal;
            filteredPicture.SizeMode = PictureBoxSizeMode.Normal;
        }

        private void fitToolStripMenuItem_Click(object sender, EventArgs e) {
            clearChecked();
            fitToolStripMenuItem.Checked = true;
            initialPicture.SizeMode = PictureBoxSizeMode.Zoom;
            filteredPicture.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void stretchedToolStripMenuItem_Click(object sender, EventArgs e) {
            clearChecked();
            stretchedToolStripMenuItem.Checked = true;
            initialPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            filteredPicture.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void centeredToolStripMenuItem_Click(object sender, EventArgs e) {
            clearChecked();
            centeredToolStripMenuItem.Checked = true;
            initialPicture.SizeMode = PictureBoxSizeMode.CenterImage;
            filteredPicture.SizeMode = PictureBoxSizeMode.CenterImage;
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e) {
            label1.Text = "Threshold " + hScrollBar1.Value;
            Bitmap image = new Bitmap(initialPicture.Image);
            IFilter threshold = new Threshold(hScrollBar1.Value);
            image = Grayscale.CommonAlgorithms.RMY.Apply(image);
            image = threshold.Apply(image);
            filteredPicture.Image = image;
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e) {
            initialPicture.Image = null;
            filteredPicture.Image = null;
            label1.Text = "Threshold 0";
            hScrollBar1.Value = 0;
            hScrollBar1.Enabled = false;
            resetToolStripMenuItem.Enabled = false;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
            FileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = fileName == null ? null : Path.GetDirectoryName(fileName);
            saveFileDialog.FileName = Path.ChangeExtension(fileName, ".png");
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK) {
                fileName = saveFileDialog.FileName;
                saveToNonIndexedImageFormat(filteredPicture.Image, fileName);
            }
        }


        private void saveToNonIndexedImageFormat(System.Drawing.Image image, string fileName) {
            Bitmap bitmap;
            using (var bitmap1 = image) {
                MemoryStream ms = new MemoryStream();
                bitmap1.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                var streamBitmap = System.Drawing.Image.FromStream(ms);
                using (Graphics graphics = Graphics.FromImage(streamBitmap)) {
                    //    using (Font arialFont = new Font("Arial", 25)) {
                    //        graphics.DrawString(DateTime.Now.ToString("dd.MM.yyyy"), arialFont, Brushes.Black, location);
                    //    }
                }
                bitmap = new Bitmap(streamBitmap);
            }

            bitmap.Save(fileName);
            bitmap.Dispose();
        }
    }
}
