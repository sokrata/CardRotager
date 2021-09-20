using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace CardRotager {
    public class Drawler {

        const int penWidth = 7;
        Pen penFuchsia = new Pen(Color.Fuchsia, penWidth);
        
        /// <summary>
        /// Размер шрифта для текста
        /// </summary>
        private Font FontTextLine { get; set; }

        public Drawler() {
            FontTextLine = new Font("Arial", 120);
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

        public void drawLine(Graphics graphics, Edge line, Pen red, int rowIndex, StringBuilder sb, bool withText = false, int thicknessLine = 0) {
            sb?.AppendFormat("{0}: {1}\r\n", rowIndex + 1, line);

            drawLine(graphics, line, red, thicknessLine);
            if (withText) {
                graphics.DrawString(string.Format("{0}, {1}", line.X, line.Y), FontTextLine, Brushes.Black, line.X, line.Y - 20);
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

        public void drawDot(Graphics graphics, Pen red, int x, int y, int dotSide) {
            graphics.DrawLine(red, x - dotSide, y, x + dotSide, y);
        }

        private void drawCross(Graphics graphics, int x, int y, int CrossRadius) {
            graphics.DrawLine(Pens.Red, x - CrossRadius, y, x + CrossRadius, y);
            graphics.DrawLine(Pens.Red, x, y - CrossRadius, x, y + CrossRadius);
        }

        const int X1 = 500;
        const int X2 = 4961;
        const int X3 = 9129;
        const int Y1 = 364;
        const int Y2 = 3474;
        const int Y3 = 6561;
        const int Y4 = 9760;
        const int Y5 = 12959;

        public List<Rectangle> makeFrame() {
            List<Rectangle> rects = new List<Rectangle> {
                //rects.Add(new Rectangle(X1 - EXTEND_SIDE, Y1 - EXTEND_SIDE, (X2 - X1) + EXTEND_SIDE, (Y2 - Y1) + EXTEND_SIDE));
                //first colunm:
                new Rectangle(X1, Y1, (X2 - X1), (Y2 - Y1)),
                new Rectangle(X1, Y2, (X2 - X1), (Y3 - Y2)),
                new Rectangle(X1, Y3, (X2 - X1), (Y4 - Y3)),
                new Rectangle(X1, Y4, (X2 - X1), (Y5 - Y4)),
                
                //second column:
                new Rectangle(X2, Y1, (X3 - X2), (Y2 - Y1)),
                new Rectangle(X2, Y2, (X3 - X2), (Y3 - Y2)),
                new Rectangle(X2, Y3, (X3 - X2), (Y4 - Y3)),
                new Rectangle(X2, Y4, (X3 - X2), (Y5 - Y4))
            };
            return rects;
        }

        public void drawFrame(Graphics gr, Pen penFrame, List<Rectangle> rectangles) {
            foreach (var item in rectangles) {
                gr.DrawRectangle(penFrame, item);
            }
        }
        public void drawTargetFrame(Graphics gr, Pen penFrame, int Width, int Height) {
            gr.DrawLine(penFrame, X1, 0, X1, Height);
            gr.DrawLine(penFrame, X2, 0, X2, Height);
            gr.DrawLine(penFrame, X3, 0, X3, Height);
            gr.DrawLine(penFrame, 0, Y1, Width, Y1);
            gr.DrawLine(penFrame, 0, Y2, Width, Y2);
            gr.DrawLine(penFrame, 0, Y3, Width, Y3);
            gr.DrawLine(penFrame, 0, Y4, Width, Y4);
            gr.DrawLine(penFrame, 0, Y5, Width, Y5);
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
    }
}
