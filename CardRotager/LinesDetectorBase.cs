using System;
using AForge.Imaging;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace CardRotager {
    public class LinesDetectorBase {
        
        protected int[] Dots { get; set; }
        
        protected List<Edge> LineEdges { get; set; }
        
        public static UnmanagedImage prepareBitmap(Bitmap bitmap, out BitmapData imageData) {
            imageData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadWrite, bitmap.PixelFormat);
            return new UnmanagedImage(imageData);
        }

        protected static Edge addLineEdge(List<Edge> lineEdges, int startX, int startY, bool terminate) {
            return addLineEdge(lineEdges, startX, startY, startX, startY, terminate);
        }

        private static Edge addLineEdge(List<Edge> lineEdges, int startX, int startY, int endX, int endY, bool terminate) {
            Edge edge = new Edge(startX, startY, endX, endY, terminate, double.NaN);
            lineEdges.Add(edge);
            return edge;
        }

        protected void resetDots(int x, int cnt, int newY) {
            for (int x2 = x - cnt; x2 <= x; x2++) {
                Dots[x2] = newY;
            }
        }

        /// <summary>
        /// Попадает ли curValue в диапазон checkValue - checkRadius и checkValue + checkRadius (не включая крайние значения)
        /// </summary>
        /// <param name="curValue"></param>
        /// <param name="checkValue"></param>
        /// <param name="checkRadius"></param>
        /// <returns></returns>
        protected static bool inRange(int curValue, int checkValue, int checkRadius) {
            return checkValue - checkRadius < curValue && curValue < checkValue + checkRadius;
        }

        /// <summary>
        /// Попадает ли curValue в диапазон checkValue - checkRadius и checkValue + checkRadius (не включая крайние значения)
        /// </summary>
        /// <param name="curValue"></param>
        /// <param name="checkValue"></param>
        /// <param name="checkRadius"></param>
        /// <returns></returns>
        public static bool inRange(double curValue, double checkValue, double checkRadius) {
            return checkValue - checkRadius < curValue && curValue < checkValue + checkRadius;
        }

        protected static string l(string text) {
            return Program.localize.localize(text);
        }

        public float getBrightness(int R, int G, int B) {
            float r = (float)R / 255.0f;
            float g = (float)G / 255.0f;
            float b = (float)B / 255.0f;

            float max, min;

            max = r; min = r;

            if (g > max) max = g;
            if (b > max) max = b;

            if (g < min) min = g;
            if (b < min) min = b;

            return (max + min) / 2;
        }


    }
}
