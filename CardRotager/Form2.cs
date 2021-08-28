using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Math.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace CardRotager {
    public partial class Form2 : Form {

        int i = 0;

        public Form2() {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e) {
            Bitmap bitmap = new Bitmap(@"E:\YandexDisk\Файлы\C# Sources\CardRotager\17+18. Оборот-.jpg");
            Bitmap gsImage = Grayscale.CommonAlgorithms.BT709.Apply(bitmap);

            DifferenceEdgeDetector filter = new DifferenceEdgeDetector();
            Bitmap image = filter.Apply(gsImage);



            // process image with blob counter
            BlobCounter blobCounter = new BlobCounter();
            blobCounter.ProcessImage(image);
            Blob[] blobs = blobCounter.GetObjectsInformation();

            // create convex hull searching algorithm
            GrahamConvexHull hullFinder = new GrahamConvexHull();

            // lock image to draw on it
            BitmapData data = image.LockBits(
                new Rectangle(0, 0, image.Width, image.Height),
                    ImageLockMode.ReadWrite, image.PixelFormat);
            i = 0;
            // process each blob
            foreach (Blob blob in blobs) {
                List<IntPoint> leftPoints, rightPoints, edgePoints = new List<IntPoint>();

                // get blob edge points
                blobCounter.GetBlobsLeftAndRightEdges(blob, out leftPoints, out rightPoints);

                edgePoints.AddRange(leftPoints);
                edgePoints.AddRange(rightPoints);

                // blob convex hull
                List<IntPoint> hull = hullFinder.FindHull(edgePoints);


                Drawing.Polygon(data, hull, Color.Red);
                i++;
            }

            image.UnlockBits(data);


            pictureBox1.Image = image;

        }

        private void pictureBox1_Click(object sender, EventArgs e) {
            MessageBox.Show("Found: " + i + " Objects");
        }
    }
}
