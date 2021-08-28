using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace foundContours
{
    public partial class Form1 : Form
    {
        private Image<Bgr, byte> inputimage = null;


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //openFileDialog1.InitialDirectory= @"E:\YandexDisk\Файлы\C# Sources\CardRotager\";
            //openFileDialog1.FileName = @"E:\YandexDisk\Файлы\C# Sources\CardRotager\01+02. Оборот.jpg";
            
            openFileDialog1.InitialDirectory= @"S:\YandexDisk\Файлы\C# Sources\CardRotager\";
            openFileDialog1.FileName = @"S:\YandexDisk\Файлы\C# Sources\CardRotager\01+02. Оборот.jpg";

            if (openFileDialog1.ShowDialog() != DialogResult.OK) {
                return;
            }
            inputimage = new Image<Bgr, byte>(openFileDialog1.FileName);
            pictureBox1.Image = inputimage.Bitmap;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Image<Gray, byte> outputImage = inputimage.Convert<Gray, byte>().ThresholdBinary(new Gray(100), new Gray(200));
            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
            Mat hierarchy = new Mat();
            CvInvoke.FindContours(outputImage, contours, hierarchy, Emgu.CV.CvEnum.RetrType.External, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);
            if (checkBox1.Checked) {
                Image<Gray, byte> blackBackground = new Image<Gray, byte>(inputimage.Width, inputimage.Height, new Gray(0));
                CvInvoke.DrawContours(blackBackground, contours, -1, new MCvScalar(0, 255, 255));
                //pictureBox2.Image = blackBackground.Bitmap;
            } else {
                CvInvoke.DrawContours(inputimage, contours, -1, new MCvScalar(0,255, 255), 2, Emgu.CV.CvEnum.LineType.Filled);
                StringBuilder sb = new StringBuilder();
                for(int i = 0; i < contours.Size; i++) {
                    for (int j = 0; j < contours[i].Size; j++) {
                        Point p = contours[i][j];
                        sb.AppendFormat("x = {0}, y = {1},", p.X, p.Y);
                    }
                    sb.AppendLine();
                }
                textBox1.Text = sb.ToString();
                //pictureBox2.Image = inputimage.Bitmap;
    }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox1.Image.Save(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "output.jpg"));
        }
    }
}
