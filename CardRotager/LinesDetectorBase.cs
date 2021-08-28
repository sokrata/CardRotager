using AForge.Imaging;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace CardRotager {
    public class LinesDetectorBase {

        public LinesDetectorBase() {
        }

        public static UnmanagedImage prepareBitmap(Bitmap bitmap, out BitmapData imageData) {
            imageData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadWrite, bitmap.PixelFormat);
            return new UnmanagedImage(imageData);
        }

        public static void addNewEdge(List<Edge> list, int x, int y, bool terminate) {
            Edge edge = new Edge(x, y, x, y, terminate, 0);
            insertNewLine(list, true, edge);
            //return edge;
        }
        public static void prependNewEdge(List<Edge> list, int x, int y, bool terminate) {
            Edge edge = new Edge(x, y, x, y, terminate, 0);
            insertNewLine(list, false, edge);
            //return edge;
        }

        public static void insertNewLine(List<Edge> lines, bool toEnd, Edge edge) {
            if (toEnd) {
                lines.Add(edge);
            } else {
                lines.Insert(0, edge);
            }
        }

        /// <summary>
        /// Попадает ли curValue в диапазон checkValue - checkRadius и checkValue + checkRadius
        /// </summary>
        /// <param name="curValue"></param>
        /// <param name="checkValue"></param>
        /// <param name="checkRadius"></param>
        /// <returns></returns>
        public static bool inRange(int curValue, int checkValue, int checkRadius) {
            return checkValue - checkRadius < curValue && curValue < checkValue + checkRadius;
        }
        public static string l(string text) {
            return MainForm.localize.localize(text);
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
