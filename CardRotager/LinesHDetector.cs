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
       
       
        public List<Edge> HLines {
            get => LineEdges;
        }

        public int[] HDots {
            get => Dots;
        }

        /// <summary>
        /// Найти и заполнить все точки 
        /// с возможностью задать ограничения по Y если не заполнены startY/endY
        /// </summary>
        /// <param name="image"></param>
        public void findHDots(UnmanagedImage image, int width, int height, int delLineLessSize, int ignoreYPixels, int startX = -1, int endX = -1, int startY = -1, int endY = -1) {
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
                for (int y = startY + ignoreYPixels; y < endY; y++) {
                    Color color = image.GetPixel(x, y);
                    float lightness = color.GetBrightness();
                    if (lightness >= 0 && lightness < 1) {
                        Dots[x] = y;
                        break;
                    }
                }
            }
            //сформируем shortLine (короткие линии) и если нужно их сгладим их Y во 2 части
            //1 часть.
            List<Point> shortLines = findShortLines(width, delLineLessSize);
            for (int curIndex = 0; curIndex < shortLines.Count; curIndex++) {
                Point curLine = shortLines[curIndex];
                int prevLineDotIndex = curLine.X - 1;
                if (prevLineDotIndex < 0) {
                    continue;
                }
                int nextLineDotIndex = curLine.Y + 1;
                if (nextLineDotIndex >= Dots.Length) {
                    continue;
                }
                int prevLineY = Dots[prevLineDotIndex];
                int nextLineY = Dots[nextLineDotIndex];

                //2 часть: если для двух линий prevY и nextY не отличаются больше чем на 10 (по высоте, Y),
                //а текущий Y дальше 10 пикселей, то заровняем текущую по nextY
                int additionalWidth = inNearLineInRange(shortLines, curIndex, prevLineY, nextLineY, 10, 20);
                if (additionalWidth >= 0) {
                    //от EndX влево затрем Y новым значением nextLineY
                    resetDots(curLine.Y, curLine.Y - curLine.X + additionalWidth, nextLineY);
                }
            }
        }

        private int inNearLineInRange(List<Point> shortLines, int curIndex, int prevLineY, int nextLineY, int diff, int extendSearch) {
            int shiftX = 0;
            Point curLine = shortLines[curIndex];
            if (!inRange(prevLineY, nextLineY, diff)) {
                //начнем со этой же точки т.к. она тоже подходит для зануления
                int curDotXIndex = curLine.X - 1; // - это prevLineDotIndex
                bool found = false;
                while (curDotXIndex > 0 && shiftX < extendSearch) {
                    if (inRange(Dots[curDotXIndex], nextLineY, diff)) {
                        found = true;
                        break;
                    }
                    curDotXIndex--;
                    shiftX++;
                }
                if (!found) {
                    return -1;
                }
            }
            //если не дальше diff то занулять не надо, вернем -1
            return isEdgeOfLineInRange(curLine, nextLineY, diff) ? -1 : shiftX;
        }

        /// <summary>
        /// Проверим что Ystart или Yend в пределах diff от nextLineY
        /// </summary>
        /// <param name="curLine"></param>
        /// <param name="nextLineY"></param>
        /// <param name="diff"></param>
        /// <returns></returns>
        private bool isEdgeOfLineInRange(Point curLine, int nextLineY, int diff) {
            //в x храниться начальная позиция X линии, а в Y - конечная позиция X
            return inRange(Dots[curLine.X], nextLineY, diff) || inRange(Dots[curLine.Y], nextLineY, diff);
        }

        private List<Point> findShortLines(int width, int shortLineLen) {
            List<Point> shortLines = new List<Point>();
            int count = 0;
            for (int curX = 0; curX < width; curX++) {
                int curY = Dots[curX];
                if (curX > 0 && inRange(curY, Dots[curX - 1], 10)) {
                    count++;
                } else {
                    if (count > 0 && count < shortLineLen) {
                        //resetDots(ref dots, curX, count, curY);
                        shortLines.Add(new Point(curX - count, curX - 1));
                    }
                    count = 1;
                }
            }
            return shortLines;
        }

        /// <summary>
        /// Создаем горизонтальные линии в диапазоне по горизонтали startX - width среди всех найденных горизонтальных точек (1 точка по вертикали)
        /// </summary>
        /// <param name="startX"></param>
        /// <param name="width"></param>
        /// <param name="settingsPercentHorizontalPadding"></param>
        /// <param name="killLength"></param>
        /// <param name="angleLines2"></param>
        /// <returns></returns>
        public void fillLineEdges(int startX, int width, Settings settings, out List<Edge> angleLines2) {
            const int checkRadiusY = 12;
            const int DIFF = 100;
            createLineEdges(startX, width);
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
                curLine.prependPoint(curLine.X, curLine.Y);
                curLine.X2 = nextLine.X2;
                curLine.Y2 = nextLine.Y2;
                LineEdges.RemoveAt(i + 1);
            }

            for (int i = 0; i < LineEdges.Count; i++) {
                Edge curLine = LineEdges[i];
                angleLines2.Add(new Edge(curLine.calcAngle(true, settings.PercentHorizontalPadding)));
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
                if (longestLineEdges.Contains(lineIndex) && curLine.Width > settings.KillLength) {
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
        private void createLineEdges(int startX, int width) {
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