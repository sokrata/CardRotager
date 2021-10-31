using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace CardRotager {
    public class Drawler {
        private const int MIN_HOR_PADDING = 300;
        private const int MIN_VERT_PADDING = 300;
        private const int MIN_HOR_SPACING = 0;
        private const int penWidth = 7;
        private Pen penFuchsia = new Pen(Color.Fuchsia, penWidth);
        private StringFormat headerStringFormat;

        private int paddingHor;
        private int paddingVert;
        private int X1;
        private int X2;
        private int X3;
        private List<int> listY;
        private int imageWidth;
        private int imageHeight;
        private int cardWidth;
        private int cardHeight;

        /// <summary>
        /// Размер шрифта для текста
        /// </summary>
        private Font FontTextLine { get; set; }

        public Drawler() {
            listY = new List<int>();
            FontTextLine = new Font("Arial", 120);
            headerStringFormat = new StringFormat(StringFormatFlags.FitBlackBox);
            headerStringFormat.LineAlignment = StringAlignment.Center;
            headerStringFormat.Alignment = StringAlignment.Center;
        }

        public void drawRuler(Graphics graphics, int width, int height) {
            for (int x = 0; x < width; x += 100) {
                drawVLineWithText(graphics, x - 50, 15);
                drawVLineWithText(graphics, x, 30);
            }
            for (int y = 0; y < height; y += 100) {
                drawHLineWithText(graphics, y - 50, 15, width);
                drawHLineWithText(graphics, y, 30, width);

                int rightY = width / 2;
                drawHLineWithText(graphics, y - 50, 15, rightY);
                drawHLineWithText(graphics, y, 30, rightY);
            }
        }

        private StringFormat sf = new StringFormat(StringFormatFlags.DirectionVertical);

        private void drawHLineWithText(Graphics graphics, int y, int lineLen, int width) {
            graphics.DrawLine(Pens.Green, width - lineLen, y, width, y);
            graphics.DrawString(y.ToString(), SystemFonts.MessageBoxFont, SystemBrushes.ControlText, width - lineLen, y, sf);
        }

        private void drawVLineWithText(Graphics graphics, int x, int lineLen) {
            graphics.DrawLine(Pens.Green, x, 0, x, lineLen);
            graphics.DrawString(x.ToString(), SystemFonts.MessageBoxFont, SystemBrushes.ControlText, x + 2, 0);
        }

        public void drawLine(Graphics graphics, Edge line, Pen red, int rowIndex, Logger log, bool withText = false, int thicknessLine = 0) {
            log.AppendFormat("{0}: {1}\r\n", rowIndex + 1, line);

            if (line != null) {
                drawLine(graphics, line, red, thicknessLine);
                if (withText) {
                    graphics.DrawString(string.Format("{0}, {1}", line.X, line.Y), FontTextLine, Brushes.Black, line.X, line.Y - 20);
                }
            }
        }

        public static void drawLine(Graphics graphics, Edge line, Pen pen, int thicknessLine) {
            if (thicknessLine != 1) {
                pen = new Pen(pen.Color, thicknessLine);
            }
            graphics.DrawLine(pen, line.X, line.Y, line.X2 + 1, line.Y2 + 1);
        }

        public static void drawHorizontalDots(Graphics graphics, Pen red, int dotSide, int[] dots) {
            if (dots == null) {
                return;
            }
            for (int x = 0; x < dots.Length; x++) {
                int y = dots[x];
                if (dotSide == 0) {
                    graphics.DrawRectangle(red, x, y, 1, 1);
                } else {
                    graphics.DrawLine(red, x - dotSide, y, x + dotSide, y);
                }
            }
        }

        public static void drawVerticalDots(Graphics graphics, Pen red, int dotSide, int[] dots) {
            if (dots == null) {
                return;
            }

            for (int y = 0; y < dots.Length; y++) {
                int x = dots[y];
                if (dotSide == 0) {
                    graphics.DrawRectangle(red, x, y, 1, 1);
                } else {
                    graphics.DrawLine(red, x - dotSide, y, x + dotSide, y);
                }
            }
        }

        public void drawRect(Graphics graphics, Brush brush, int x, int y, int radiusSide) {
            graphics.FillRectangle(brush, x - radiusSide, y - radiusSide, radiusSide * 2, radiusSide * 2);
        }

        public static void drawCross(Graphics graphics, Pen pen, int x, int y, int CrossRadius) {
            graphics.DrawLine(pen, x - CrossRadius, y, x + CrossRadius, y);
            graphics.DrawLine(pen, x, y - CrossRadius, x, y + CrossRadius);
        }

        public void calcFrame(int imageWidth, int imageHeight, int cardWidth, int cardHeight, int rowCount, bool isCenteredImage) {
            this.imageWidth = imageWidth;
            this.imageHeight = imageHeight;
            this.cardWidth = cardWidth;
            this.cardHeight = cardHeight;
            paddingHor = isCenteredImage ? (imageWidth - cardWidth * 2) / 2 : MIN_HOR_PADDING;
            paddingVert = isCenteredImage ? (imageHeight - cardHeight * 4) / 2 : MIN_VERT_PADDING;
            X1 = paddingHor;
            X2 = isCenteredImage ? imageWidth / 2 : paddingHor + cardWidth + MIN_HOR_SPACING;
            X3 = isCenteredImage ? imageWidth - paddingHor : paddingHor + cardWidth * 2 + MIN_HOR_SPACING;
            listY.Clear();
            for (int rowIndex = 0; rowIndex <= rowCount; rowIndex++) {
                listY.Add(paddingVert + cardHeight * rowIndex);
            }
            //Y5 = imageHeight - paddingVert;
        }

        // const int X1 = 500;
        // const int X2 = 4961;
        // const int X3 = 9129;
        // const int Y1 = 364;
        // const int Y2 = 3474;
        // const int Y3 = 6561;
        // const int Y4 = 9760;
        // const int Y5 = 12959;

        public List<Rectangle> makeFrame(int rowCount) {
            List<Rectangle> rects = new List<Rectangle>();
            // {
            //     //rects.Add(new Rectangle(X1 - EXTEND_SIDE, Y1 - EXTEND_SIDE, (X2 - X1) + EXTEND_SIDE, (Y2 - Y1) + EXTEND_SIDE));
            //     //first colunm:
            //     new Rectangle(X1, listY[0], (X2 - X1), (listY[1] - listY[0])),
            //     new Rectangle(X1, listY[1], (X2 - X1), (listY[2] - listY[1])),
            //     new Rectangle(X1, listY[2], (X2 - X1), (listY[3] - listY[2])),
            //     new Rectangle(X1, listY[3], (X2 - X1), (listY[4] - listY[3])),
            //     
            //
            //     //second column:
            //     new Rectangle(X2, listY[0], (X3 - X2), (listY[1] - listY[0])),
            //     new Rectangle(X2, listY[1], (X3 - X2), (listY[2] - listY[1])),
            //     new Rectangle(X2, listY[2], (X3 - X2), (listY[3] - listY[2])),
            //     new Rectangle(X2, listY[3], (X3 - X2), (listY[4] - listY[3]))
            // };
            for (int rowIndex = 1; rowIndex <= rowCount; rowIndex++) {
               rects.Add(new Rectangle(X1, listY[rowIndex - 1], (X2 - X1), (listY[rowIndex] - listY[rowIndex - 1])));
            }
            for (int rowIndex = 1; rowIndex <= rowCount; rowIndex++) {
                rects.Add(new Rectangle(X2, listY[rowIndex - 1], (X3 - X2), (listY[rowIndex] - listY[rowIndex - 1])));
            }
            return rects;
        }

        public void drawFrame(Graphics gr, Pen penFrame, List<Rectangle> rectangles) {
            foreach (var item in rectangles) {
                gr.DrawRectangle(penFrame, item);
            }
        }

        public void drawTargetFrame(Graphics gr, Pen penFrame) {
            gr.DrawLine(penFrame, X1, 0, X1, imageHeight);
            gr.DrawLine(penFrame, X2, 0, X2, imageHeight);
            gr.DrawLine(penFrame, X3, 0, X3, imageHeight);
            foreach (int y in listY) {
                gr.DrawLine(penFrame, 0, y, imageWidth, y);
            }
        }

        public void drawTargetCutMark(Graphics gr, Pen penCutMark, int crossRadius) {
            foreach (int y in listY) {
                drawLineCross(gr, penCutMark, y, crossRadius);
            }
        }

        private void drawLineCross(Graphics gr, Pen penFrame, int yPos, int crossRadius) {
            drawCross(gr, penFrame, X1, yPos, crossRadius);
            drawCross(gr, penFrame, X2, yPos, crossRadius);
            drawCross(gr, penFrame, X3, yPos, crossRadius);
        }

        public void drawRect(Graphics graphics, List<Rectangle> rectangles) {
            foreach (var item in rectangles) {
                graphics.DrawRectangle(new Pen(RandomColors.RandomColor, 5), item);
            }
        }

        public void drawPolyLine(Graphics graphics, HVLine hvLine) {
            if (hvLine == null) {
                return;
            }
            List<Point> points = hvLine.HLine.getPoints();
            for (int i = 1; i < points.Count; i++) {
                graphics.DrawLine(penFuchsia, points[i - 1], points[i]);
            }
        }

        public void drawText(Graphics graphics, Font headerFont, string text) {
            graphics.DrawString(text, headerFont, SystemBrushes.ControlText, new Rectangle(0, 0, imageWidth, listY[0]), headerStringFormat);
        }
    }
}