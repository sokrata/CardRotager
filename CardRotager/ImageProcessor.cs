using AForge.Imaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;

namespace CardRotager {
    public class ImageProcessor {
        private readonly Localize localize;
        private readonly LinesHDetector linesHDetector;
        private readonly LinesVDetector linesVDetector;
        private readonly StringBuilder sb;
        private readonly Settings settings;
        private readonly ToolStripProgressBar statusBarProgressBar;

        public ImageProcessor(Localize localize, StringBuilder sb, Settings settings, ToolStripProgressBar statusBarProgressBar) {
            this.localize = localize;
            linesHDetector = new LinesHDetector();
            linesVDetector = new LinesVDetector();
            this.sb = sb;
            this.settings = settings;
            this.statusBarProgressBar = statusBarProgressBar;
        }

        public List<List<HVLine>> LinesAll { get; set; }
        public List<Line> DashLines { get; set; }
        public List<Edge> AngleEdges { get; set; }
        public List<Line> ShowLines { get; set; }

        /// <summary>
        /// Все обработанные массивы точек для горизонтальных линии
        /// </summary>
        public int[] DotsHorizontal { get; set; }

        /// <summary>
        /// Все обработанные массивы точек для вертикальных линии
        /// </summary>
        public int[] DotsVertical { get; set; }

        /// <summary>
        /// Берем исходник в ч/б (с порогом, сделан в FileOpen - открыть). 
        /// Алгоритм (в общих чертах):
        /// Составим сетку - колонки и строки (из горизонтальных и вертикальных линий контуров карт).
        /// 
        /// 1. Составляем верхние горизонтальные линии (по числу рядов карт) - MakeFirstHLines
        /// 2. Формируем вертикальные линии для каждой колонки взятой из каждой горизонтальной линии - ProcessColumnLines
        /// 3. Создаем колонки вертикальных линий (Объединяя рядом стоящие вертикальные линии)
        /// </summary>
        /// <param name="bitmap"></param>
        public bool fillHVLinesAll(Bitmap bitmap) {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            int width = bitmap.Width;
            int height = bitmap.Height;

            DashLines = new List<Line>();
            AngleEdges = new List<Edge>();

            using (UnmanagedImage unmanImage = LinesDetectorBase.prepareBitmap(bitmap, out BitmapData imageData)) {
                try {
                    //горизонтальные линии:
                    sb?.AppendFormat(l("== Определение всех горизонтальных линий верхнего ряда (с мин. длиной: {0}) ==\r\n"), settings.MinLineSizeY);

                    List<Edge> firstHLines = makeFirstHLines(unmanImage, width, height);
                    int hCount = firstHLines.Count;
                    progressIncrement();
                    LinesAll = new List<List<HVLine>>(hCount);
                    for (int colIndex = 0; colIndex < hCount; colIndex++) {
                        Edge hLine = firstHLines[colIndex];
                        LinesAll.Add(new List<HVLine>(1) {new HVLine(hLine, null)});
                    }
                    stopwatch.Stop();
                    sb?.AppendFormat(l("\r\nИтоговое время определения верхнего ряда: {0} мс\r\n"), stopwatch.ElapsedMilliseconds);
                    if (hCount != 2) {
                        string msg = string.Format(l("Поскольку количество колонок не 2, а {0}, обработка невозможна. Поддерживается обработка картинок содержащих только 2 колонки и 4 строки."), hCount);
                        if (sb == null) {
                            MessageBox.Show(msg, l("Ошибка"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        } else {
                            sb.AppendFormat(l("\r\nОбъяснение: {0}\r\nОБРАБОТКА прервана!\r\n\r\n"), msg);
                        }
                        return false;
                    }
                    stopwatch.Restart();

                    int colCount = LinesAll.Count;
                    sb?.AppendFormat("Определено {0} колон(ки/ок)\r\n", colCount);
                    // вертикальные линии:
                    int rowCount = 1;
                    for (int colIndex = colCount - 1; colIndex >= 0; colIndex--) {
                        sb?.AppendFormat(l("\r\n== Определение вертикальных линий для {0} колонки (с мин. длиной: {1}) ==\r\n"), colIndex + 1, settings.MinLineSizeY);
                        //получим вертикальные линии в указанной колонке
                        List<Edge> columnVLines = processColumnVLines(unmanImage, colIndex, colCount, width, height);
                        progressIncrement();
                        rowCount = Math.Max(rowCount, columnVLines.Count);
                        initLineAllRow(colIndex, columnVLines.Count);
                        for (int rowIndex = 0; rowIndex < columnVLines.Count; rowIndex++) {
                            LinesAll[colIndex][rowIndex].VLine = columnVLines[rowIndex];
                        }

                        //второй цикл определения горизонтальных линий для добавленных вертикальных строк:
                        //горизонтальные 2:
                        List<Edge> vLineRows = makeLineRows(sb, colIndex, columnVLines, width, out int endX);
                        int startX = colIndex > 0 ? LinesAll[colIndex - 1][0].HLine.X2 + settings.DotLineXMinAddWidth : 0;
                        sb?.AppendFormat(l("\r\n== Определение горизонтальных линий остальных рядов для {0} колонки (с мин. длиной: {1}) ==\r\n"), colIndex + 1, settings.MinLineSizeY);

                        for (int rowIndex = rowCount - 1; rowIndex > 0; rowIndex--) {
                            //добавляем горизонтальные линии в колонке
                            List<Edge> hLines = processRowLines(unmanImage, colIndex, rowIndex, startX, endX, height, vLineRows, rowCount);
                            if (hLines == null || hLines.Count == 0) {
                                string msg = string.Format(l("Не удалось найти линию для colIndex = {0}, rowIndex = {1} обработка невозможна."), colIndex + 1, rowIndex + 1);
                                if (sb == null) {
                                    MessageBox.Show(msg, l("Ошибка"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                } else {
                                    sb.AppendFormat(l("\r\nОбъяснение: {0}\r\nОБРАБОТКА прервана!\r\n\r\n"), msg);
                                }
                                return false;
                            }
                            LinesAll[colIndex][rowIndex].HLine = hLines[0];
                            progressIncrement();
                        }

                        stopwatch.Stop();
                        sb?.AppendFormat(l("\r\nИтоговое время определения всех линий {0} колонки: {1} мс\r\n"), colIndex + 1, stopwatch.ElapsedMilliseconds);
                        stopwatch.Restart();
                    }
                } finally {
                    bitmap.UnlockBits(imageData);
                }
            }

            WinSpecific.clearMemory();

            stopwatch.Stop();
            sb?.AppendFormat(l("\r\nФормирование сетки линий: Время {0} мс\r\n"), stopwatch.ElapsedMilliseconds);
            return true;
        }

        private void progressIncrement() {
            statusBarProgressBar.Increment(1);
            Application.DoEvents();
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
        /// <param name="image"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns>Список горизонтальных линий</returns>
        private List<Edge> makeFirstHLines(UnmanagedImage image, int width, int height) {
            linesHDetector.findHDots(image, width, height, settings.DelLineLessSize, settings.IgnoreFirstYPixels);
            linesHDetector.fillLineEdges(0, width, settings, out List<Edge> angleLines2);
            AngleEdges.AddRange(angleLines2);
            addSbInfo(linesHDetector.HLines);
            return linesHDetector.HLines;
        }

        private void addSbInfo(List<Edge> hLinesAll) {
            if (sb == null) {
                return;
            }

            for (int i = 0; i < hLinesAll.Count; i++) {
                sb.AppendFormat("{0}: {1}\r\n", i + 1, hLinesAll[i]);
            }
        }

        private void initLineAllRow(int colIndex, int rowCount) {
            List<HVLine> rowLines = LinesAll[colIndex];
            if (rowCount > rowLines.Count) {
                for (int rowIndex = 0; rowIndex < rowCount; rowIndex++) {
                    if (rowIndex >= rowLines.Count) {
                        rowLines.Add(new HVLine(null, null));
                    }
                }
            }
        }

        /// <summary>
        /// Обработка колонки линий:
        /// 1. Определяем диапазон обрабатываемых X - GetMinMaxX
        /// 2. Поиск вертикальных линий
        /// </summary>
        /// <param name="unmanagedImage"></param>
        /// <param name="colIndex"></param>
        /// <param name="colCount"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private List<Edge> processColumnVLines(UnmanagedImage unmanagedImage, int colIndex, int colCount, int width, int height) {
            getRangeX(colIndex, colCount, width, out int minX, out int maxX);

            linesVDetector.fillVDotsAll(minX, maxX, height, unmanagedImage, settings.DelLineLessSize, 0);//colCount - 1 == colIndex ? 70 : 0);
            linesVDetector.createVLine(sb, settings.MinLineSizeY, settings.KillLength, out List<Edge> angleLines2);
            List<Edge> vLines = linesVDetector.VLines;

            if (linesVDetector.ShowLines != null) {
                if (ShowLines == null) {
                    ShowLines = new List<Line>();
                }
                ShowLines.AddRange(linesVDetector.ShowLines);
            }

            return vLines;
        }

        private List<Edge> processRowLines(UnmanagedImage unmanagedImage, int colIndex, int rowIndex, int startX, int endX, int height, List<Edge> vLineRows, int rowCount) {
            if (vLineRows.Count <= rowIndex) {
                return null;
            }
            getRangeY(height, vLineRows, rowIndex, out int minY, out int maxY);
            
            //линии верхняя и нижняя для отображения позже на форме
            DashLines.Add(new Line(startX, minY, endX, minY));
            DashLines.Add(new Line(startX, maxY, endX, maxY));

            //для текущей строки формируем строку точек ниже minY но выше maxY
            linesHDetector.findHDots(unmanagedImage, endX, height,settings.DelLineLessSize, 0, startX, endX, minY, maxY);
            if (settings.ShowDetailDotsOfImageNumber > 0) {
                if (rowCount * colIndex + rowIndex == settings.ShowDetailDotsOfImageNumber - 1) {
                    DotsHorizontal = linesHDetector.HDots;
                }
            }

            //получаем список всех горизонтальных линий из точек
            linesHDetector.fillLineEdges(startX, endX, settings, out List<Edge> angleLines2);
            List<Edge> hLines = linesHDetector.HLines;
            for (int lineIndex = hLines.Count - 1; lineIndex >= 0; lineIndex--) {
                if (hLines[lineIndex].Width < settings.MinLineSizeY) {
                    hLines.RemoveAt(lineIndex);
                }
            }
            AngleEdges.AddRange(angleLines2);
            if (sb != null) {
                if (hLines.Count != 1) {
                    sb.AppendFormat("число строк не равно 1 (hLines.Count = {0}, rowIndex = {1}\r\n", hLines.Count, rowIndex + 1);
                }
                sb.AppendFormat("{0}: {1}\r\n", rowIndex + 1, hLines[0]);
            }
            return hLines;
        }

        private string l(string text) {
            return localize.localize(text);
        }

        private void getRangeY(int height, List<Edge> vLineRows, int rowIndex, out int minY, out int maxY) {
            maxY = vLineRows[rowIndex].Y + settings.DotLineYCurMinAddHeight;
            if (maxY > height) {
                maxY = height;
            }

            if (rowIndex == 0) {
                minY = 0;
            } else {
                minY = vLineRows[rowIndex - 1].Y2 + settings.DotLineYPrevMaxAddHeight;
            }
            if (minY > height) {
                minY = height;
            }
        }

        private static List<Edge> makeLineRows(StringBuilder sb, int colIndex, List<Edge> columnLines, int width, out int endX) {
            List<Edge> vLineRows = new List<Edge>();
            vLineRows.AddRange(columnLines);
            vLineRows.Sort((x, y) => x.Y.CompareTo(y.Y));
            endX = int.MinValue;
            for (int overallIndex = vLineRows.Count - 1; overallIndex >= 0; overallIndex--) {
                endX = Math.Max(endX, vLineRows[overallIndex].X2);
            }
            if (endX == int.MinValue) {
                endX = width;
            }
            int rowCount = vLineRows.Count;
            if (sb != null) {
                for (int i = 0; i < rowCount; i++) {
                    sb.AppendFormat("{0}: {1}\r\n", i + 1, vLineRows[i]);
                }
            }

            return vLineRows;
        }

        private void getRangeX(int colIndex, int colCount, int width, out int minX, out int maxX) {
            const int rowIndex = 0;
            if (colIndex == colCount - 1) {
                maxX = width;
            } else {
                maxX = LinesAll[colIndex][rowIndex].HLine.X2 + (LinesAll[colIndex + 1][rowIndex].HLine.X - LinesAll[colIndex][rowIndex].HLine.X2) / 2;
            }
            if (colIndex == rowIndex) {
                minX = rowIndex;
            } else {
                minX = LinesAll[colIndex][rowIndex].HLine.X;
            }
        }

        public HVLine getLineByImageIndex(int imageIndex) {
            if (imageIndex < 0 || LinesAll.Count <= 0 || imageIndex >= getImageCount()) {
                return null;
            }
            int linesRowCountInFirstColumn = LinesAll[0].Count;
            return LinesAll[imageIndex / linesRowCountInFirstColumn][imageIndex % linesRowCountInFirstColumn];
        }

        private int getImageCount() {
            int count = 0;
            if (LinesAll == null || LinesAll.Count == 0) {
                return count;
            }
            for (int colIndex = 0; colIndex < LinesAll.Count; colIndex++) {
                count += LinesAll[colIndex].Count;
            }
            return count;
        }
    }
}