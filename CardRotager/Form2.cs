using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace CardRotager {
    public partial class Form2 : Form {

        int i = 0;
        //PictureBox pictureBox1;
        public Form2() {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e) {
            Bitmap bitmap = new Bitmap(@"S:\YandexDisk\Файлы\C# Sources\CardRotager\Захват1.png");
           
            pictureBox1.Image = bitmap;

        }

        private void pictureBox1_Click(object sender, EventArgs e) {

            //pictureBox1.Angle = 2;
            Image img = pictureBox1.Image;
            Rectangle fromRegion = new Rectangle(0, 0, 100, 100);
            Bitmap bmp = new Bitmap(pictureBox1.Image, new Size(100, 100));
            using (Graphics toGraphic = Graphics.FromImage(bmp)) {

                //e.Graphics.TranslateTransform(Image.Width + .0f, Image.Height + .0f);
                //e.Graphics.RotateTransform(Angle);
                //e.Graphics.TranslateTransform(-Image.Width + .0f, -Image.Height + .0f);

                toGraphic.Clear(Color.White);
                float centerX = (fromRegion.Left + fromRegion.Right) * 0.5f;
                float centerY = (fromRegion.Top + fromRegion.Bottom) * 0.5f;
                toGraphic.TranslateTransform(centerX, centerY);
                toGraphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                toGraphic.RotateTransform(2);
                toGraphic.TranslateTransform(-centerX, -centerY);
                float scale = (float)bmp.Width / bmp.Width;
                toGraphic.ScaleTransform(scale, scale);

                toGraphic.DrawImage(img, 0, 0, img.Width, img.Height);
            }
            //pictureBox1.Image = bmp;
            //bmp.RotateFlip(RotateFlipType.Rotate270FlipNone);

            copyRegionIntoImage(Graphics.FromImage(pictureBox1.Image), new Rectangle(0, 0, 100, 100), bmp, fromRegion, 0, 0);
            pictureBox1.Invalidate();
        }


        public static void copyRegionIntoImage(Graphics toGraphic, Rectangle toRegion, Bitmap fromBitmap, Rectangle fromRegion, float angle, int extend) {
            fromRegion.Inflate(new Size(extend, extend));
            int addX = (toRegion.Width - fromRegion.Width) / 2;
            int addY = (toRegion.Height - fromRegion.Height) / 2;
            float centerX = (fromRegion.Left + fromRegion.Right) * 0.5f;
            float centerY = (fromRegion.Top + fromRegion.Bottom) * 0.5f;

            toGraphic.DrawImage(fromBitmap, toRegion.X + addX, toRegion.Y + addY, fromRegion, GraphicsUnit.Pixel);
            //toGraphic.RotateTransform(-45);
        }
    }
}
