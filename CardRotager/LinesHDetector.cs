using AForge.Imaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;

namespace CardRotager {

    /// <summary>
    /// Нахождение линий
    /// Класс чтеца картинок ImageWrapper: https://www.cyberforum.ru/blogs/529033/blog3507.html
    /// 
    /// </summary>
    public class LinesHDetector : LinesDetectorBase {

        StringBuilder sb;

        public LinesHDetector(StringBuilder sb) {
            this.sb = sb;
        }

        /// <summary>
        /// Найти и заполнить все точки 
        /// с возможностью задать ограничения по Y если не заполнены startY/endY
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public int[] findHDots(UnmanagedImage unmandImage, int width, int height, int startY = -1, int endY = -1) {
            int[] dots = new int[width];
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();


            if (startY == -1) {
                startY = 0;
            }
            if (endY == -1) {
                endY = height;
            }
            for (int x = 0; x < width; x++) {
                dots[x] = -1;
                for (int y = startY; y < endY; y++) {
                    Color color = unmandImage.GetPixel(x, y);
                    float lightness = color.GetBrightness();
                    if (lightness >= 0 && lightness < 1) {
                        dots[x] = y;
                        break;
                    }
                }
            }

            #region test
            //            // sample 2 - converting .NET image into unmanaged
            //            UnmanagedImage unmanagedImage = UnmanagedImage.FromManagedImage(image);
            //            // apply several routines to the unmanaged image
            //            ...
            //// conver to managed image if it is required to display it at some point of time
            //Bitmap managedImage = unmanagedImage.ToManagedImage();

            //sb.Append(string.Format("\r\nx,y = {3}, {4}: h = {0:N2}, s = {1:N2}, b = {2:N2}", hue * 100, saturation * 100, lightness * 100, x, y) +
            //string.Format(":: {0}, {1} = {3}, {4}, {5}, {6} == {2}", x, y, Util.ToHtml(color), color.A, color.R, color.G, color.B));
            #endregion
            stopwatch.Stop();
            Debug.WriteLine(string.Format(l("FillHorLine: {0} мс"), stopwatch.ElapsedMilliseconds));
            return dots;
        }

        /// <summary>
        /// Создаем горизонтальные линии в диапазоне по горизонтали startX - width среди всех найденных горизонтальных точек (1 точка по вертикали)
        /// </summary>
        /// <param name="dots"></param>
        /// <param name="sb"></param>
        /// <param name="killLength"></param>
        /// <param name="startX"></param>
        /// <param name="width"></param>
        /// <param name="debug"></param>
        /// <returns></returns>
        public List<Edge> createHLine(int[] dots, int killLength, int startX, int width, bool debug = false) {
            makeHLine(dots, startX, width, out List<Edge> lines, out int checkRadiusY);

            //удалим короткие линии
            for (int lineIndex = lines.Count - 1; lineIndex >= 0; lineIndex--) {
                Edge curLine = lines[lineIndex];
                if (curLine.Width < killLength) {
                    if (lineIndex > 0) {
                        //поищем кем заменить
                        Edge prevLine = lines[lineIndex - 1];
                        if (prevLine.Terminate || curLine.Terminate) {
                            if (curLine.Terminate) {
                                prevLine.Terminate = curLine.Terminate;
                            }
                        }
                        if (debug) {
                            sb?.AppendFormat(l("delIndex = {0}: {1}\r\n"), lineIndex, curLine);
                        }

                        if (curLine.X - prevLine.X2 < killLength) {
                            bool terminate = true;
                            if (lineIndex + 1 >= lines.Count || lineIndex + 1 < lines.Count && lines[lineIndex + 1].X - curLine.X2 < killLength) {
                                terminate = false;
                            }
                            prevLine.X2 = curLine.X2;
                            prevLine.Terminate = terminate;
                        }
                    }
                    lines.RemoveAt(lineIndex);
                }
            }

            if (sb != null && debug) {
                sb.AppendLine(l("Все длинные линии:"));
                for (int i = 0; i < lines.Count; i++) {
                    sb.AppendFormat("{0}: {1}\r\n", i, lines[i]);
                }
                sb.AppendLine();
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
                if (lines[index].Width > max) {
                    maxIndex = index;
                    max = lines[index].Width;
                }
            }
            if (maxIndex != -1) {
                longestLines.Add(maxIndex);
            }
            if (sb != null && debug) {
                sb.AppendLine(l("Список длинных линий:"));
                for (int i = 0; i < longestLines.Count; i++) {
                    int v1 = longestLines[i];
                    sb.AppendFormat(@"{0} [{1}]: {2}", i, v1, lines[v1]);
                    sb.AppendLine();
                }
                sb.AppendLine();
                sb.AppendLine(l("Формирование единой непрерывной линии:"));
            }

            int sbInsert = 0;
            StringBuilder sb2 = null;
            if (sb != null && debug) {
                sbInsert = sb.Length;
            }

            //объединяем линии в одну (до прерывателя, сначало влево затем вправо)
            if (longestLines.Count != 0) {

                for (int vIndex = longestLines.Count - 1; vIndex >= 0; vIndex--) {
                    maxIndex = longestLines[vIndex];

                    Edge maxLine = lines[maxIndex];

                    if (sb != null && debug) {
                        sb2 = new StringBuilder(string.Format(l("process {0}: {1}\r\n"), maxIndex, maxLine));
                    }
                    //ruler.drawLine(maxLine, Pens.GreenYellow, 0);

                    //есть самый длинный, ищем влево все что на этой линии и объединяем
                    int longestIndex = maxIndex;

                    Edge longEdge = lines[longestIndex];
                    longEdge.calcAngle(true);
                    //поиск влево до прерывателя (terminate) или начала
                    int curIndex = longestIndex - 1;
                    while (curIndex >= 0) {
                        Edge curEdge = lines[curIndex];
                        if (curEdge.Terminate) {
                            break;
                        }
                        if (inRange(longEdge.Y, curEdge.Y2, checkRadiusY)) {
                            longEdge.X = curEdge.X;
                        }
                        if (sb2 != null && debug) {
                            sb2.AppendFormat(l("Назад {0} объединена {2}: {1})\r\n"), maxIndex, longEdge, curIndex);
                        }
                        lines.RemoveAt(curIndex);
                        longestIndex--;
                        curIndex--;
                    }
                    if (sb2 != null && debug) {
                        sb2.AppendLine();
                    }

                    //поиск вправо до прерывателя (terminate) или конца
                    curIndex = longestIndex + 1;
                    while (curIndex < lines.Count) {
                        Edge curEdge = lines[curIndex];
                        if (inRange(longEdge.Y2, curEdge.Y, checkRadiusY)) {
                            longEdge.X2 = curEdge.X2;
                        }
                        if (sb2 != null && debug) {
                            sb2.AppendFormat(l("Вперед {0} объединена {2}: {1})\r\n"), maxIndex, longEdge, curIndex);
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

        /// <summary>
        /// Создаем горизонтальные линии из точек dots в диапазоне startX, width объединяя в линию точки отличающие по Y не более чем +/-checkRadiusY
        /// </summary>
        /// <param name="dots"></param>
        /// <param name="startX"></param>
        /// <param name="width"></param>
        /// <param name="lines"></param>
        /// <param name="checkRadiusY"></param>
        private void makeHLine(int[] dots, int startX, int width, out List<Edge> lines, out int checkRadiusY) {
            lines = new List<Edge>();
            bool wasGap = false;
            int checkRadiusDotY = 5;
            checkRadiusY = 30;
            for (int xCoord = startX; xCoord < width; xCoord++) {
                int dotY = dots[xCoord];
                if (xCoord == startX) {
                    continue;
                }
                if (wasGap && dotY != -1) {
                    if (lines.Count != 0) {
                        lines[lines.Count - 1].Terminate = true;
                    }
                    addNewEdge(lines, xCoord, dotY, false);
                    wasGap = false;
                    continue;
                }
                if (dotY == -1) {
                    wasGap = true;
                    continue;
                }
                //проверяем что предыдущая точка была недалеко по Y
                if (inRange(dotY, dots[xCoord - 1], checkRadiusDotY)) {
                    if (lines.Count == 0) {
                        addNewEdge(lines, xCoord, dotY, false);
                    }
                    Edge lastEdge = lines[lines.Count - 1];
                    if (inRange(dotY, lastEdge.Y2, checkRadiusY)) {
                        lastEdge.X2 = xCoord;
                        lastEdge.Y2 = dotY;
                    } else {
                        addNewEdge(lines, xCoord, dotY, false);
                    }
                }

            }
        }

    }
}
