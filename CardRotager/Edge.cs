using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace CardRotager {

    public class Edge {
        private int x;
        private int y;
        private int x2;
        private int y2;
        private double angle;
        private bool terminate;
        private List<Point> points = new List<Point>();

        public Edge(int x, int y, int x2, int y2, bool terminate, double angle) {
            this.X = x;
            this.Y = y;
            this.X2 = x2;
            this.Y2 = y2;
            this.Terminate = terminate;
            this.angle = angle;
        }

        public Edge(Edge edge) {
            x = edge.X;
            x2 = edge.X2;
            y = edge.Y;
            y2 = edge.Y2;
            terminate = edge.Terminate;
            angle = edge.Angle;
        }

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public int X2 { get => x2; set => x2 = value; }
        public int Y2 { get => y2; set => y2 = value; }
        
        /// <summary>
        /// Угол в градусах (для вертикальной линии считается как отклонение от вертикальной оси (т.е. вертикальная линия это 0 градусов,
        /// а для горизонтальной - от 0 градусов, т.е. горизонтальная линия это 0 градусов )
        /// </summary>
        public double Angle { get => angle; set => angle = value; }
        public int Width { get => x2 - x; }
        public int Height { get => y2 - y; }
        public bool Terminate { get => terminate; set => terminate = value; }
        public int MidY { get => Y + (Y2 - Y) /2; }

        public override string ToString() {
            return string.Format($"x = [{{0,4}}, {{2,4}}], y = [{{1}}, {{3}}]{{4}}, angle = {{5:N2}}; [w = {{6}}, h = {{7}}], points = {{8}}", X, Y, X2, Y2, terminate ? ", TERMIN" : "", Angle, Width, Height, points.Count);
        }

        private double triangleLength() {
            if (Height == 0) {
                return Width;
            } else if (Width == 0) {
                return Height;
            }
            return Math.Sqrt(Math.Pow(X2 - X, 2) + Math.Pow(Y2 - Y, 2));
        }

        public void calcAngle(bool horizontal) {
            angle = getAngle(horizontal);
        }

        public Edge calcAngle(bool horizontal, int percentHorPadding) {
            if (percentHorPadding == 0 || points.Count == 0) {
                calcAngle(horizontal);
                return this;
            }
            if (horizontal) {
                int horPadding = (int)Math.Ceiling(((double) percentHorPadding / 100) * Width);
                int cropW = Width;
                int startPointIndex = 0;
                int startX = X;
                int endX = X2;
                int startY = Y;
                int endY = Y2;
                for (int pointIndex = 0; pointIndex < points.Count; pointIndex++) {
                    Point curPoint = points[pointIndex];
                    if (X + horPadding < curPoint.X) {
                        cropW -= curPoint.X;
                        startX += curPoint.X - X;
                        startY = curPoint.Y;
                        startPointIndex = pointIndex;
                        
                        break;
                    }
                }
                for (int pointIndex = points.Count - 1; pointIndex >= startPointIndex; pointIndex--) {
                    Point curPoint = points[pointIndex];
                    if (curPoint.X <= X + Width - horPadding) {
                        cropW -= Width - curPoint.X;
                        endY = curPoint.Y;
                        endX -= Width - curPoint.X + X;
                        break;
                    }
                }
                angle = computeAngle(true, cropW, endY - startY);
                return new Edge(startX, startY, endX, endY, Terminate, angle);
            } else {
                calcAngle(false);
            }
            return this;
        }

        private double getAngle(bool horizontal) {
            return computeAngle(horizontal, Width, Height);
        }

        private static double computeAngle(bool horizontal, int w, int h) {
            double radian;
            if (horizontal) {
                if (h == 0) {
                    return 0;
                }
                radian = Math.Atan(w / h);
            } else {
                if (w == 0) {
                    return 0;
                }
                radian = Math.Atan(h / w);
            }
            double res = radian * (180 / Math.PI);
            res = res < 0 ? -90D - res : 90D - res;
            return res;
        }

        public void setAngle(double angle) {
            this.angle = angle;
        }

        public int maxY() {
            return Math.Max(Y, Y2);
        }

        public int maxX() {
            return Math.Max(X, X2);
        }

        public Line toLine() {
            return new Line(x, y, x2, y2);
        }

        public void prependPoint(int x, int y) {
            points.Insert(0, new Point(x, y));
        }

        public List<Point> getPoints() {
            return points;
        }
    }
}