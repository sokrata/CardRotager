using System;

namespace CardRotager {

    public class Edge {
        private int x;
        private int y;
        private int x2;
        private int y2;
        private double angle;
        private bool terminate;

        public Edge(int x, int y, int x2, int y2, bool terminate, int angle) {
            this.X = x;
            this.Y = y;
            this.X2 = x2;
            this.Y2 = y2;
            this.Terminate = terminate;
            this.angle = angle;
        }

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public int X2 { get => x2; set => x2 = value; }
        public int Y2 { get => y2; set => y2 = value; }
        public double Angle { get => angle; set => angle = value; }
        public int Width { get => x2 - x; }
        public int Height { get => y2 - y; }
        public bool Terminate { get => terminate; set => terminate = value; }

        override public string ToString() {
            //return string.Format("x = {0}, y = {1}, x2 = {2}, y2 = {3}; {4}, {5} == {6:N1}", X, Y, X2, Y2, direction, terminate, TriangleLentgh());
            return string.Format($"x = [{{0,4}}, {{2,4}}], y = [{{1}}, {{3}}]{{4}}, angle = {{5}}; Len = {{6:N2}}", X, Y, X2, Y2, terminate ? ", TERMIN" : "", Angle, triangleLentgh());
        }

        public double triangleLentgh() {
            if(Height == 0) {
                return Width;
            } else if(Width == 0) {
                return Height;
            }
            return Math.Sqrt(Math.Pow(X2 - X, 2) + Math.Pow(Y2 - Y, 2));
        }

        public void calcAngle(bool horizontal) {
            if (horizontal) {
                angle = (Height == 0 ? Double.NaN : Math.Atan(Math.Abs(Width) / Math.Abs(Height)));
            } else {
                angle = (Width == 0 ? Double.NaN : Math.Atan(Math.Abs(Height) / Math.Abs(Width)));
            }
        }
        public int maxY() {
            return Math.Max(Y, Y2);
        }

        public int maxX() {
            return Math.Max(X, X2);
        }
    }
}