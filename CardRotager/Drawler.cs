using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace CardRotager {
    public class Drawler {

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
                
        public void drawDotX(Graphics graphics, Pen p, int x, int y, int dotSide = 5) {
            graphics.DrawLine(p, x - dotSide, y, x + dotSide, y);
            graphics.DrawLine(p, x, y - dotSide, x, y + dotSide);
        }

        public void drawLines(Graphics graphics, List<Edge> lines, Pen red, StringBuilder sb, bool withText = false, int thicknessLine = 0) {
            int i = 0;

            //sb.AppendLine();
            //sb.AppendFormat("Result Lines:");
            sb?.AppendLine();

            foreach (Edge item in lines) {
                sb?.AppendFormat("{0}: {1}\r\n", i, item);

                drawLine(graphics, item, red, thicknessLine);
                if (withText) {
                    graphics.DrawString(string.Format("{0}, {1}", item.X, item.Y), FontTextLine, Brushes.Black, item.X, item.Y - 20);
                }

                i++;
            }
        }

        public void drawLine(Graphics graphics, Edge item, Pen pen, int thicknessLine) {
            if (thicknessLine != 1) {
                pen = new Pen(pen.Color, thicknessLine);
            }
            graphics.DrawLine(pen, item.X, item.Y, item.X2 + 1, item.Y2 + 1);
        }


        public void drawDots(Graphics graphics, Pen red, int dotSide, int[] dots, bool isDotX = true) {
            if (dots == null) {
                return;
            }
            for (int x = 0; x < dots.Length; x++) {
                int y = dots[x];
                if (isDotX) {
                    graphics.DrawLine(red, x - dotSide, y, x + dotSide, y);
                } else {
                    graphics.DrawLine(red, y - dotSide, x, y + dotSide, x);
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
                new Rectangle(X1, Y1, (X2 - X1), (Y2 - Y1)),
                new Rectangle(X2, Y1, (X3 - X2), (Y2 - Y1)),

                new Rectangle(X1, Y2, (X2 - X1), (Y3 - Y2)),
                new Rectangle(X2, Y2, (X3 - X2), (Y3 - Y2)),

                new Rectangle(X1, Y3, (X2 - X1), (Y4 - Y3)),
                new Rectangle(X2, Y3, (X3 - X2), (Y4 - Y3)),

                new Rectangle(X1, Y4, (X2 - X1), (Y5 - Y4)),
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
    }
}
