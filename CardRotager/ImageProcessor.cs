using AForge.Imaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardRotager {
    public class ImageProcessor {

        public const int KillLength = 20;
        public const int MIN_LINE_SIZE = 60;
        public int rowCount = 1;
        public const int THICK = 18;
        public const int mergeValueY = 300;
        public const int ZAPAS_Y = 100;
        public const int ZAPAS_MIN_Y = 20;

        List<Edge> hLinesAll = new List<Edge>();
        List<Edge> vLinesAll = new List<Edge>();
        private readonly Localize localize;
        private readonly LinesHDetector linesHDetector;
        StringBuilder sb;
        private List<Line> dashLines;
        private List<Edge> angleEdges;
        private int[] dots;

        public ImageProcessor(Localize localize, StringBuilder sb) {
            this.localize = localize;
            linesHDetector = new LinesHDetector(sb);
            this.sb = sb;
        }

        public List<Edge> HLinesAll { get => hLinesAll; set => hLinesAll = value; }
        public List<Edge> VLinesAll { get => vLinesAll; set => vLinesAll = value; }
        public List<Line> DashLines { get => dashLines; set => dashLines = value; }
        public List<Edge> AngleEdges { get => angleEdges; set => angleEdges = value; }
        public int[] Dots { get => dots; set => dots = value; }

        /// <summary>
        /// Берем исходник в ч/б (с порогом, сделан в FileOpen - открыть). 
        /// Алгоритм (в общих чертах):
        /// Составим сетку - колонки и строки (из горизонтальных и вертикальных линий контуров карт).
        /// 
        /// 1. Составляем верхние горизонтальные линии (по числу рядов карт) - MakeFirstHLines
        /// 2. Формируем вертикальные линии для каждой колонки взятой из каждой горизонтальной линии - ProcessColumLines
        /// 3. Создаем колонки вертикальных линий (Объединяя рядом стоящие вертикальные линии)
        /// </summary>
        /// <param name="bitmap"></param>
        public void createHorAndVertLines(Bitmap bitmap) {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            int width = bitmap.Width;
            int height = bitmap.Height;

            DashLines = new List<Line>();
            AngleEdges = new List<Edge>();
            dots = null;

            using (UnmanagedImage unmandImage = LinesDetectorBase.prepareBitmap(bitmap, out BitmapData imageData)) {
                try {
                    //горизонтальные линии:
                    sb.AppendFormat(l("== Определение верхнего ряда горизонтальных линий (с мин. длиной: {0}) ==\r\n"), KillLength);

                    hLinesAll = makeFirstHLines(unmandImage, width, height, KillLength, MIN_LINE_SIZE, THICK);
                    int colCount = hLinesAll.Count;

                    //вертикальные линии:
                    sb?.AppendFormat(l("\r\n== Определение вертикальных линий (с мин. длиной: {0}) ==\r\n"), KillLength);

                    for (int colIndex = colCount - 1; colIndex >= 0; colIndex--) {
                        List<Edge> vLines = processColumLines(unmandImage, colIndex, colCount, width, height, sb, out List<Edge> angleLines2);
                        AngleEdges.AddRange(angleLines2);
                        VLinesAll.AddRange(vLines);
                    }

                    stopwatch.Stop();
                    sb?.AppendFormat(l("\r\nОпределение вертикальных линий: Время {0} мс\r\n"), stopwatch.ElapsedMilliseconds);
                    stopwatch.Restart();

                    //второй цикл определения горизонтальных линий для добавленных вертикальных строк:
                    //горизонтальные 2:
                    List<Edge> vLineRows = makeLineRows(sb);
                    sb?.AppendFormat(l("\r\n== Second cycle: Horizontal Lines (minLength: {0}) ==\r\n"), KillLength);

                    for (int rowIndex = rowCount - 1; rowIndex > 0; rowIndex--) {
                        sb?.AppendFormat(l("\r\n- {0} rowIndex:\r\n\r\n"), rowIndex);
                        getRangeY(height, vLineRows, rowIndex, out int minY, out int maxY);

                        //линии верхняя и нижняя для отображения позже на форме
                        DashLines.Add(new Line(0, minY, width, minY));
                        DashLines.Add(new Line(0, maxY, width, maxY));

                        //для текущей строки формируем строку точек ниже minY но выше maxY
                        dots = linesHDetector.findHDots(unmandImage, width, height, minY, maxY);

                        //получаем список всех горизонтальных линий из точек
                        List<Edge> hLines = linesHDetector.createHLine(dots, KillLength, 0, width, debug: false);
                        for (int lineIndex = hLines.Count - 1; lineIndex >= 0; lineIndex--) {
                            if (hLines[lineIndex].Width < MIN_LINE_SIZE) {
                                hLines.RemoveAt(lineIndex);
                            }
                        }
                        if (sb != null) {
                            for (int i = 0; i < hLines.Count; i++) {
                                Edge item = hLines[i];
                                sb?.AppendFormat("{0}: {1}\r\n", i, item);
                            }
                        }
                        hLinesAll.AddRange(hLines);
                    }

                } finally {
                    bitmap.UnlockBits(imageData);
                }
            }

            WinSpecific.clearMemory();

            stopwatch.Stop();
            sb?.AppendFormat(l("\r\nФормирование сетки линий: Время {0} мс\r\n"), stopwatch.ElapsedMilliseconds);
        }

        private string l(string text) {
            return localize.localize(text);
        }

        private static void getRangeY(int height, List<Edge> vLineRows, int rowIndex, out int minY, out int maxY) {
            maxY = vLineRows[rowIndex].Y + ZAPAS_Y;
            if (maxY > height) {
                maxY = height;
            }

            if (rowIndex == 0) {
                minY = 0;
            } else {
                minY = vLineRows[rowIndex - 1].Y2 + ZAPAS_MIN_Y;
            }
            if (minY > height) {
                minY = height;
            }
        }

        private List<Edge> makeLineRows(StringBuilder sb) {
            sb?.Append(l("\r\n== Формируем строчки вертикальных линий ==\r\n\r\n"));

            List<Edge> vLineRows = new List<Edge>();
            vLineRows.AddRange(VLinesAll);
            vLineRows.Sort((x, y) => x.Y.CompareTo(y.Y));
            for (int overallIndex = vLineRows.Count - 1; overallIndex >= 0; overallIndex--) {
                if (overallIndex == vLineRows.Count - 1) {
                    continue;
                }
                if (inLimit(vLineRows[overallIndex + 1].Y - vLineRows[overallIndex].Y, mergeValueY)) {
                    vLineRows.RemoveAt(overallIndex);
                } else if (inLimit(vLineRows[overallIndex].Y - vLineRows[overallIndex + 1].Y, mergeValueY)) {
                    vLineRows.RemoveAt(overallIndex + 1);
                }
            }
            rowCount = vLineRows.Count;
            if (sb != null) {
                for (int i = 0; i < rowCount; i++) {
                    Edge item = vLineRows[i];
                    sb?.AppendFormat("{0}: {1}\r\n", i, item);
                }
            }

            return vLineRows;
        }

        /// <summary>
        /// Обработка колонки линий:
        /// 1. Определяем диапазон обрабатываемых X - GetMinMaxX
        /// 2. Поиск вертикальных линий
        /// </summary>
        /// <param name="unmandImage"></param>
        /// <param name="colIndex"></param>
        /// <param name="colCount"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="sb"></param>
        /// 
        /// <returns></returns>
        private List<Edge> processColumLines(UnmanagedImage unmandImage, int colIndex, int colCount, int width, int height, StringBuilder sb, out List<Edge> angleLines2) {
            getRangeX(colIndex, colCount, width, out int minX, out int maxX);

            int[] dotsVert = LinesVDetector.fillVDotsAll(minX, maxX, height, unmandImage);
            List<Edge> vLines = LinesVDetector.createVLine(dotsVert, sb, KillLength, 0, height, out angleLines2);
            for (int lineIndex = vLines.Count - 1; lineIndex >= 0; lineIndex--) {
                if (vLines[lineIndex].Height < MIN_LINE_SIZE) {
                    vLines.RemoveAt(lineIndex);
                }
            }
            if (sb != null) {
                sb.AppendFormat(l("\r\nколонка: {0}\r\n"), colIndex);
                for (int i = 0; i < vLines.Count; i++) {
                    Edge item = vLines[i];
                    sb.AppendFormat("{0}: {1}\r\n", i, item);
                }
            }
            rowCount = Math.Max(rowCount, vLines.Count);
            return vLines;
        }

        private void getRangeX(int colIndex, int colCount, int width, out int minX, out int maxX) {
            if (colIndex == colCount - 1) {
                maxX = width;
            } else {
                maxX = hLinesAll[colIndex + 1].X;
            }
            if (colIndex == 0) {
                minX = 0;
            } else {
                minX = hLinesAll[colIndex].X;
            }
        }

        private bool inLimit(int checkValue, int limiValue) {
            return checkValue >= 0 && checkValue <= limiValue;
        }

        /// <summary>
        /// Формирование первого ряда горизонтальных линий (первый - значит самых верхних)
        /// Алгоритм:
        /// 1. Находим все точки (контрастный цвет по отношению к фону), 
        ///    по одной в каждой колонке (координата X), 
        ///    идя от верха вниз, останавливаясь на первой же подходящей - вызов метода FindHDots
        /// 2. Создаем из них линии - вызов метода CreateHLine
        /// 3. Удаляем короткие линии (меньше длины MIN_LINE_SIZE), чтобы мусорные пылинки на скане не мешали работать с линией
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="unmandImage"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="killLength"></param>
        /// <param name="MIN_LINE_SIZE"></param>
        /// <param name="THICK"></param>
        /// <returns>Список горизонтальных линий</returns>
        private List<Edge> makeFirstHLines(UnmanagedImage unmandImage, int width, int height, int killLength, int MIN_LINE_SIZE, int THICK) {
            int[] dots = linesHDetector.findHDots(unmandImage, width, height);
            
            List<Edge> hLinesAll = linesHDetector.createHLine(dots, killLength, 0, width);
            
            for (int lineIndex = hLinesAll.Count - 1; lineIndex >= 0; lineIndex--) {
                if (hLinesAll[lineIndex].Width < MIN_LINE_SIZE) {
                    hLinesAll.RemoveAt(lineIndex);
                }
            }

            if (sb != null) {
                for (int i = 0; i < hLinesAll.Count; i++) {
                    Edge item = hLinesAll[i];
                    sb.AppendFormat("{0}: {1}\r\n", i, item);
                }
            }
            return hLinesAll;
        }

    }
}
