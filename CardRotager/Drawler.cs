using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace CardRotager {
    public class Drawler {
        private Font FontTextLine { get; set; }
        public Drawler(MainForm mainForm) {
            FontTextLine = new Font("Arial", 120);
        }

        //public void DrawRuler(Graphics graphics) {
        //    for (int i = 0; i < mf.pbBlackWhite.Width; i += 100) {
        //        DrawVLineWithText(graphics, i - 50, 15);
        //        DrawVLineWithText(graphics, i, 30);
        //    }
        //}

        private void DrawVLineWithText(Graphics graphics, int x, int v) {
            graphics.DrawLine(Pens.Green, x, 0, x, v);
            graphics.DrawString(x.ToString(), SystemFonts.MessageBoxFont, SystemBrushes.ControlText, x + 2, 0);
        }

        //private void DrawLineWithText(Graphics graphics, int y, int v) {
        //    graphics.DrawLine(Pens.Green, Width - v, y, Width, y);
        //    graphics.DrawString(y.ToString(), SystemFonts.MessageBoxFont, SystemBrushes.ControlText, Width - v, y);
        //}

        public void DrawDotX(Graphics graphics, Pen p, int x, int y, int dotSide = 5) {
            graphics.DrawLine(p, x - dotSide, y, x + dotSide, y);
            graphics.DrawLine(p, x, y - dotSide, x, y + dotSide);
        }

        public void DrawLines(Graphics graphics, List<Edge> lines, Pen red, StringBuilder sb, bool withText = false, int thicknessLine = 0) {
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


        public void DrawDots(Graphics graphics, Pen red, int dotSide, int[] dots, bool isDotX = true) {
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

        public void DrawDot(Graphics graphics, Pen red, int x, int y, int dotSide) {
            graphics.DrawLine(red, x - dotSide, y, x + dotSide, y);
        }

        private void DrawCross(Graphics graphics, int x, int y, int CrossRadius) {
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

        public List<Rectangle> MakeFrame() {
            List<Rectangle> rects = new List<Rectangle>();
            //rects.Add(new Rectangle(X1 - EXTEND_SIDE, Y1 - EXTEND_SIDE, (X2 - X1) + EXTEND_SIDE, (Y2 - Y1) + EXTEND_SIDE));
            rects.Add(new Rectangle(X1, Y1, (X2 - X1), (Y2 - Y1)));
            rects.Add(new Rectangle(X2, Y1, (X3 - X2), (Y2 - Y1)));

            rects.Add(new Rectangle(X1, Y2, (X2 - X1), (Y3 - Y2)));
            rects.Add(new Rectangle(X2, Y2, (X3 - X2), (Y3 - Y2)));

            rects.Add(new Rectangle(X1, Y3, (X2 - X1), (Y4 - Y3)));
            rects.Add(new Rectangle(X2, Y3, (X3 - X2), (Y4 - Y3)));

            rects.Add(new Rectangle(X1, Y4, (X2 - X1), (Y5 - Y4)));
            rects.Add(new Rectangle(X2, Y4, (X3 - X2), (Y5 - Y4)));
            return rects;
        }

        public void DrawFrame(Graphics gr, Pen penFrame, List<Rectangle> rectangles) {
            foreach (var item in rectangles) {
                gr.DrawRectangle(penFrame, item);
            }
        }
        public void DrawTargetFrame(Graphics gr, Pen penFrame, int Width, int Height) {
            gr.DrawLine(penFrame, X1, 0, X1, Height);
            gr.DrawLine(penFrame, X2, 0, X2, Height);
            gr.DrawLine(penFrame, X3, 0, X3, Height);
            gr.DrawLine(penFrame, 0, Y1, Width, Y1);
            gr.DrawLine(penFrame, 0, Y2, Width, Y2);
            gr.DrawLine(penFrame, 0, Y3, Width, Y3);
            gr.DrawLine(penFrame, 0, Y4, Width, Y4);
            gr.DrawLine(penFrame, 0, Y5, Width, Y5);
        }

        internal void DrawRect(Graphics graphics, List<Rectangle> rectangles) {
            foreach (var item in rectangles) {
                graphics.DrawRectangle(new Pen(RandomColors.RandomColor, 5), item);
            }
        }
    }
}
