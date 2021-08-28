using AForge.Imaging;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace CardRotager {

    /// <summary>
    /// Нахождение линий
    /// Класс чтеца картинок ImageWrapper: https://www.cyberforum.ru/blogs/529033/blog3507.html
    /// 
    /// </summary>
    public class LinesVDetector : LinesDetectorBase {
        public LinesVDetector() {
        }

        /// <summary>
        /// Поиск точек контрастного цвет
        /// </summary>
        /// <param name="xMin"></param>
        /// <param name="xMax"></param>
        /// <param name="yMax"></param>
        /// <param name="unmandImage"></param>
        /// <returns></returns>
        public static int[] fillVDotsAll(int xMin, int xMax, int yMax, UnmanagedImage unmandImage) {
            int[] dots = new int[yMax];
            for (int y = 0; y < yMax; y++) {
                dots[y] = -1;
                for (int x = xMax - 1; x >= xMin; x--) {
                    Color color = unmandImage.GetPixel(x, y);
                    float lightness = color.GetBrightness();
                    if (lightness >= 0 && lightness < 1) {
                        dots[y] = x;
                        break;
                    }
                }
            }
            return dots;
        }

        private static void makeVLine(ref int[] dots, int startY, int height, out List<Edge> lines, out int checkRadiusX) {
            lines = new List<Edge>();
            bool wasGap = false;
            int checkRadiusY = 5;
            checkRadiusX = 30;
            for (int yCoord = startY; yCoord < height; yCoord++) {
                int dotX = dots[yCoord];
                if (yCoord == startY) {
                    continue;
                }
                if (wasGap && dotX != -1) {
                    AddNewEdge(lines, dotX, yCoord, true);
                    wasGap = false;
                    continue;
                }
                if (dotX == -1) {
                    wasGap = true;
                    continue;
                }
                if (LinesDetectorBase.InRange(dotX, dots[yCoord - 1], checkRadiusY)) {
                    if (lines.Count == 0) {
                        AddNewEdge(lines, dotX, yCoord, false);
                    }
                    Edge lastEdge = lines[lines.Count - 1];
                    if (dotX != -1 && InRange(dotX, lastEdge.X2, checkRadiusX)) {
                        lastEdge.Y2 = yCoord;
                        lastEdge.X2 = dotX;
                    } else {
                        AddNewEdge(lines, dotX, yCoord, false);
                    }
                }
            }
        }

        public static List<Edge> createVLine(int[] dots, StringBuilder sb, int killLength, int startY, int height, bool debug = false) {
            List<Edge> lines = new List<Edge>();
            makeVLine(ref dots, startY, height, out lines, out int checkRadiusX);

            //удалим короткие линии
            for (int lineIndex = lines.Count - 1; lineIndex >= 0; lineIndex--) {
                if (lines[lineIndex].Height < killLength) {
                    if (lineIndex > 0) {
                        //поищем кем заменить
                        if (lines[lineIndex - 1].Terminate || lines[lineIndex].Terminate) {
                            if (lines[lineIndex].Terminate) {
                                lines[lineIndex - 1].Terminate = lines[lineIndex].Terminate;
                            }
                        }
                        if (debug)
                            sb?.AppendFormat("delIndex = {0}: {1}\r\n", lineIndex, lines[lineIndex]);
                        if (lines[lineIndex].Y - lines[lineIndex - 1].Y2 < killLength) {
                            bool terminate = true;
                            if (lineIndex + 1 >= lines.Count
                                || lineIndex + 1 < lines.Count && lines[lineIndex + 1].Y - lines[lineIndex].Y2 < killLength) {
                                terminate = false;
                            }
                            lines[lineIndex - 1].Y2 = lines[lineIndex].Y2;
                            lines[lineIndex - 1].Terminate = terminate;
                        }
                    }
                    lines.RemoveAt(lineIndex);
                }
            }

            //найти самую длинную линию
            int max = -1;
            int maxIndex = -1;
            List<int> longestLines = new List<int>();
            for (int index = 0; index < lines.Count; index++) {
                if (lines[index].Terminate) {
                    if (max != -1) {
                        longestLines.Add(maxIndex);
                    }
                    max = -1;
                    maxIndex = -1;
                    continue;
                }
                if (lines[index].Height > max) {
                    maxIndex = index;
                    max = lines[index].Height;
                }
            }
            if (maxIndex != -1) {
                longestLines.Add(maxIndex);
            }

            if (sb != null && debug) {
                sb.AppendLine("Список длинных линий:");
                for (int i = 0; i < longestLines.Count; i++) {
                    int v1 = longestLines[i];
                    sb.AppendFormat(@"{0} [{1}]: {2}", i, v1, lines[v1]);
                    sb.AppendLine();
                }
                sb.AppendLine();
                sb.AppendLine("Формирование единой непрерывной линии:");
            }

            int sbInsert = 0;
            StringBuilder sb2 = null;
            if (sb != null && debug) {
                sbInsert = sb.Length;
            }
            if (longestLines.Count != 0) {

                for (int vIndex = longestLines.Count - 1; vIndex >= 0; vIndex--) {
                    maxIndex = longestLines[vIndex];

                    Edge maxLine = lines[maxIndex];

                    if (sb != null && debug) {
                        sb2 = new StringBuilder(string.Format("обработка {0}: {1}\r\n", maxIndex, maxLine));
                    }

                    //есть самый длинный, ищем вверх все что на этой линии и объединяем
                    int longestIndex = maxIndex;

                    Edge longEdge = lines[longestIndex];
                    longEdge.calcAngle(false);

                    //поиск влево до прерывателя (terminate) или начала
                    int curIndex = longestIndex - 1;
                    while (curIndex >= 0) {
                        Edge curEdge = lines[curIndex];
                        if (curEdge.Terminate) {
                            break;
                        }
                        if (InRange(longEdge.X, curEdge.X2, checkRadiusX)) {
                            longEdge.Y = curEdge.Y;
                        }
                        if (sb2 != null && debug) {
                            sb2.AppendFormat("Назад {0} объединена {2}: {1})\r\n", maxIndex, longEdge, curIndex);
                        }
                        lines.RemoveAt(curIndex);
                        longestIndex--;
                        curIndex--;
                    }
                    if (sb2 != null && debug) {
                        sb2.AppendLine();
                    }

                    //поиск вниз до прерывателя (terminate) или конца
                    curIndex = longestIndex + 1;
                    while (curIndex < lines.Count) {
                        Edge curEdge = lines[curIndex];
                        if (InRange(longEdge.X2, curEdge.X, checkRadiusX)) {
                            longEdge.Y2 = curEdge.Y2;
                        }
                        if (sb2 != null && debug) {
                            sb2.AppendFormat("Вперед {0} объединена {2}: {1})\r\n", maxIndex, longEdge, curIndex);
                        }
                        lines.RemoveAt(curIndex);
                        if (curEdge.Terminate) {
                            break;
                        }
                    }
                }
                if (sb != null && debug) {
                    sb2.AppendLine();
                    sb.Insert(sbInsert, sb2.ToString());
                }
            }

            return lines;
        }


    }
}
