using AForge.Imaging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace CardRotager {
    
    /// <summary>
    /// Нахождение линий
    /// Класс чтеца картинок ImageWrapper: https://www.cyberforum.ru/blogs/529033/blog3507.html
    /// 
    /// </summary>
    public class LinesHDetector : LinesDetectorBase {
        
        private const int PERCENT_HOR_PADDING = 5;
        
        public List<Edge> HLines {
            get => LineEdges;
        }

        public int[] HDots {
            get => Dots;
        }

        private List<Point> shortLines;

        /// <summary>
        /// Найти и заполнить все точки 
        /// с возможностью задать ограничения по Y если не заполнены startY/endY
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="bitmap"></param>
        public void findHDots(UnmanagedImage image, int width, int height, int startX = -1, int endX = -1, int startY = -1, int endY = -1) {
            Dots = new int[width];

            if (startX == -1) {
                startX = 0;
            }
            if (endX == -1) {
                endX = width;
            }
            if (startY == -1) {
                startY = 0;
            }
            if (endY == -1) {
                endY = height;
            }
            for (int x = startX; x < endX; x++) {
                Dots[x] = -1;
                for (int y = startY; y < endY; y++) {
                    Color color = image.GetPixel(x, y);
                    float lightness = color.GetBrightness();
                    if (lightness >= 0 && lightness < 1) {
                        Dots[x] = y;
                        break;
                    }
                }
            }

            findShortLines(width);
            for (int i = 0; i < shortLines.Count; i++) {
                Point curLine = shortLines[i];
                int prevLineDotIndex = curLine.X - 1;
                if (prevLineDotIndex < 0) {
                    continue;
                }
                int prevLineY = Dots[prevLineDotIndex];
                int nextLineDotIndex = curLine.Y + 1;
                if (nextLineDotIndex >= Dots.Length) {
                    continue;
                }
                int nextLineY = Dots[nextLineDotIndex];
                if (inRange(prevLineY, nextLineY, 10) && !inRange(curLine.X, nextLineY, 10)) {
                    resetDots(curLine.Y, curLine.Y - curLine.X, nextLineY);
                }
            }
        }

        private void findShortLines(int width) {
            shortLines = new List<Point>();
            int count = 0;
            for (int curX = 0; curX < width; curX++) {
                int curY = Dots[curX];
                if (curX > 0 && inRange(curY, Dots[curX - 1], 10)) {
                    count++;
                } else {
                    if (count > 0 && count < DEL_LINE_LESS_HEIGHT) {
                        //resetDots(ref dots, curX, count, curY);
                        shortLines.Add(new Point(curX - count, curX - 1));
                    }
                    count = 1;
                }
            }
        }

        /// <summary>
        /// Создаем горизонтальные линии в диапазоне по горизонтали startX - width среди всех найденных горизонтальных точек (1 точка по вертикали)
        /// </summary>
        /// <param name="startX"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public void fillLineEdges(int startX, int width, out List<Edge> angleLines2) {
            const int checkRadiusY = 12;
            const int DIFF = 100;
            makeHLines(startX, width);
            angleLines2 = new List<Edge>();
            //1. сформируем список линий чтобы понять общую длину
            //2. отберем те линии что 

            // объединим со следующей линией (что рядом) по горизонтали, если по вертикали линии ближе чем checkRadiusY
            for (int i = LineEdges.Count - 2; i >= 0; i--) {
                Edge curLine = LineEdges[i];
                Edge nextLine = LineEdges[i + 1];
                if (!inRange(curLine.X2, nextLine.X, 2) || !inRange(curLine.Y2, nextLine.Y, checkRadiusY)) {
                    continue;
                }
                curLine.getPoints().AddRange(nextLine.getPoints());
                curLine.prependPoint(curLine.X2, curLine.Y2);
                curLine.X2 = nextLine.X2;
                curLine.Y2 = nextLine.Y2;
                LineEdges.RemoveAt(i + 1);
            }

            for (int i = 0; i < LineEdges.Count; i++) {
                Edge curLine = LineEdges[i];
                curLine.calcAngle(true, PERCENT_HOR_PADDING);
                angleLines2.Add(new Edge(curLine));
            }

            for (int i = LineEdges.Count - 2; i >= 0; i--) {
                Edge curLine = LineEdges[i];
                Edge nextLine = LineEdges[i + 1];
                if (!inRange(curLine.X2, nextLine.X, 2) || !inRange(curLine.MidY, nextLine.MidY, DIFF)) {
                    curLine.Terminate = true;
                }
            }
            
            //найти самую длинную линию
            int max = -1;
            int maxIndex = -1;
            List<int> longestLineEdges = new List<int>();
            for (int index = 0; index < LineEdges.Count; index++) {
                Edge curEdge = LineEdges[index];
                if (curEdge.Width > max) {
                    maxIndex = index;
                    max = curEdge.Width;
                }
                if (curEdge.Terminate) {
                    if (max != -1) {
                        longestLineEdges.Add(maxIndex);
                    }
                    max = -1;
                    maxIndex = -1;
                }
            }
            if (maxIndex != -1) {
                longestLineEdges.Add(maxIndex);
            }
            
            for (int lineIndex = LineEdges.Count - 1; lineIndex >= 0; lineIndex--) {
                Edge curLine = LineEdges[lineIndex];
                if (longestLineEdges.Contains(lineIndex) && curLine.Width > ImageProcessor.KillLength) {
                    continue;
                }
                LineEdges.RemoveAt(lineIndex);
                angleLines2.RemoveAt(lineIndex);
            }
        }

        /// <summary>
        /// Создаем горизонтальные линии из точек dots в диапазоне startX, width объединяя в линию точки отличающие по Y не более чем +/-checkRadiusY
        /// </summary>
        /// <param name="startX"></param>
        /// <param name="width"></param>
        private void makeHLines(int startX, int width) {
            const int checkRadius = 3;
            LineEdges = new List<Edge>();
            Edge curLine = null;
            int count = 0;
            for (int curX = startX; curX < width; curX++) {
                int curY = Dots[curX];
                if (curY == -1) {
                    //случай 2
                    curLine = null;
                    continue;
                }
                if (curLine == null || !inRange(curY, curLine.Y2, checkRadius)) {
                    curLine = addLineEdge(LineEdges, curX, curY, false);
                    count = -1;
                }
                curLine.X2 = curX;
                curLine.Y2 = curY;

                count++;
            }
        }
    }
}