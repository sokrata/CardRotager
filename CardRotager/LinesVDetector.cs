using AForge.Imaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace CardRotager {
    
    /// <summary>
    /// Нахождение линий
    /// Класс чтеца картинок ImageWrapper: https://www.cyberforum.ru/blogs/529033/blog3507.html
    /// 
    /// </summary>
    public class LinesVDetector : LinesDetectorBase {
        public List<Line> ShowLines { get; } = new List<Line>();
        
        public List<Edge> VLines { get => LineEdges; }

        public int[] DotsVertical {
            get => Dots;
        }

        /// <summary>
        /// Поиск точек контрастного цвета и удаление мусора (точек короче minLineSize)
        /// </summary>
        /// <param name="xMin"></param>
        /// <param name="xMax"></param>
        /// <param name="yMax"></param>
        /// <param name="unmanagedImage"></param>
        /// <param name="delLineLessSize"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public void fillVDotsAll(int xMin, int xMax, int yMax, UnmanagedImage unmanagedImage, int delLineLessSize, int ignoreXPixels) {
            Dots = new int[yMax];
            for (int y = 0; y < yMax; y++) {
                Dots[y] = -1;
                for (int x = xMax - 1 - ignoreXPixels; x >= xMin; x--) {
                    Color color = unmanagedImage.GetPixel(x, y);
                    float lightness = color.GetBrightness();
                    if (lightness >= 0 && lightness < 1) {
                        Dots[y] = x;
                        break;
                    }
                }
            }

            int count = 0;
            for (int curY = 0; curY < yMax; curY++) {
                int curX = Dots[curY];
                if (curY > 0 && inRange(curX, Dots[curY - 1], 10)) {
                    count++;
                } else {
                    if (count > 0 && count < delLineLessSize) {
                        resetDots(curY, count, curX);
                    }
                    count = 1;
                }
            }
        }

        /// <summary>
        /// Расчет угла для указанных сторон треугольника
        /// </summary>
        /// <param name="lastX1">x1</param>
        /// <param name="lastY1">y1</param>
        /// <param name="x2">x2</param>
        /// <param name="y2">y2</param>
        /// <returns></returns>
        private static double getAngle(int lastX1, int lastY1, int x2, int y2) {
            double width = x2 - lastX1;
            double height = y2 - lastY1;
            double radian = Math.Atan(Math.Abs(height) / Math.Abs(width));

            return radian * (180 / Math.PI);
        }

        public void createVLine(StringBuilder sb, int minLineSizeX, int killLength, out List<Edge> angleLines2, bool debug = false) {
            angleLines2 = new List<Edge>();

            //если нет точки - пропуск
            //если точка есть:
            // и она от предыдущей не больше minLineSize, то накопим в линию
            // отрицание: то сложим в накопитель высоты
            // если 
            LineEdges = new List<Edge>();
            //случай 1: дошли до линии (выше пустота)
            //случай 2: закончилась линия (началась пустота)
            //случай 3: изменился отступ и не большой высоты
            //случай 4: изменился отступ, но большой высоты

            Edge curLine = null;
            int count = 0;
            for (int curY = 0; curY < Dots.Length; curY++) {
                int curX = Dots[curY];
                if (curX == -1) {
                    //случай 2
                    curLine = null;
                    continue;
                }
                if (curLine == null || !inRange(curX, curLine.X, minLineSizeX)) {
                    curLine = addLineEdge(LineEdges, curX, curY, false);
                    count = -1;
                }
                curLine.X2 = curX;
                curLine.Y2 = curY;
                
                count++;
            }
            for (int i = 0; i < LineEdges.Count; i++) {
                curLine = LineEdges[i];
                curLine.calcAngle(false, 0);
                angleLines2.Add(new Edge(curLine));
                if (i != 0) {
                    Edge prevLine = LineEdges[i - 1];
                    prevLine.Terminate = !inRange(curLine.Y, prevLine.Y2, 2);
                }
            }
            
            //найти самую длинную линию
            int max = -1;
            int maxIndex = -1;
            List<int> longestLineEdges = new List<int>();
            for (int index = 0; index < LineEdges.Count; index++) {
                Edge curEdge = LineEdges[index];
                // curEdge.calcAngle(false);
                if (curEdge.Height > max) {
                    maxIndex = index;
                    max = curEdge.Height;
                }
                if (curEdge.Terminate) {
                    if (max != -1) {
                        longestLineEdges.Add(maxIndex);
                    }
                    max = -1;
                    maxIndex = -1;
                    continue;
                }
                ShowLines.Add(curEdge.toLine());
            }
            if (maxIndex != -1) {
                longestLineEdges.Add(maxIndex);
            }
            
            int checkRadiusX = 30;
            StringBuilder sb2 = null;
            
             for (int vIndex = longestLineEdges.Count - 1; vIndex >= 0; vIndex--) {
                maxIndex = longestLineEdges[vIndex];

                Edge maxLine = LineEdges[maxIndex];

                if (sb != null && debug) {
                    sb2 = new StringBuilder(string.Format(l("обработка {0}: {1}\r\n"), maxIndex, maxLine));
                }

                //есть самый длинный, ищем вверх все что на этой линии и объединяем
                int longestIndex = maxIndex;

                Edge longEdge = LineEdges[longestIndex];

                //поиск вверх до прерывателя (terminate) или начала
                int curIndex = longestIndex - 1;
                while (curIndex >= 0) {
                    Edge curEdge = LineEdges[curIndex];
                    if (curEdge.Terminate) {
                        //не надо объединять с предыдущим, т.к. terminate означает что обрыв на нижнем конце (если нужен верхний, то проставляется всегда у предыдущего)
                        break;
                    }
                    if (inRange(longEdge.X, curEdge.X2, checkRadiusX)) {
                        longEdge.Y = curEdge.Y;
                    }
                    if (sb2 != null && debug) {
                        sb2.AppendFormat(l("Назад {0} объединена {2}: {1})\r\n"), maxIndex, longEdge, curIndex);
                    }
                    longestIndex--;
                    angleLines2.RemoveAt(curIndex);
                    LineEdges.RemoveAt(curIndex);
                    curIndex--;
                }
                if (sb2 != null && debug) {
                    sb2.AppendLine();
                }

                //поиск вниз до прерывателя (terminate) или конца
                curIndex = longestIndex + 1;
                if (longEdge.Terminate) {
                    continue;
                }
                while (curIndex < LineEdges.Count) {
                    Edge curEdge = LineEdges[curIndex];
                    if (inRange(longEdge.X2, curEdge.X, checkRadiusX)) {
                        longEdge.Y2 = curEdge.Y2;
                        longEdge.X2 = curEdge.X;
                    }
                    if (sb2 != null && debug) {
                        sb2.AppendFormat(l("Вперед {0} объединена {2}: {1})\r\n"), maxIndex, longEdge, curIndex);
                    }
                    angleLines2.RemoveAt(curIndex);
                    LineEdges.RemoveAt(curIndex);
                    if (curEdge.Terminate) {
                        break;
                    }
                }
             }

             for (int curIndex = LineEdges.Count - 1; curIndex >= 0; curIndex--) {
                 Edge currentLine = LineEdges[curIndex];
                 if (currentLine.Height > killLength) {
                     continue;
                 }
                 angleLines2.RemoveAt(curIndex);
                 LineEdges.RemoveAt(curIndex);
             }
        }
    }
}