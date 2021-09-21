using System;
using System.Drawing;
using System.Globalization;

namespace CardRotager {
    public class Util {
        /// <summary>
        /// Convert an <see cref="HSBColor"/> value to an equivalent <see cref="Color"/> value.
        /// </summary>
        /// <remarks>
        /// This method is based on code from http://www.cs.rit.edu/~ncs/color/ by Eugene Vishnevsky.
        /// </remarks>
        /// <param name="hsbColor">
        /// The <see cref="HSBColor"/> struct to be converted
        /// </param>
        /// <returns>
        /// An equivalent <see cref="Color"/> struct, compatible with the GDI+ library
        /// </returns>
        public static Color ToRGB(HSBColor hsbColor) {
            Color rgbColor = Color.Black;

            // Determine which sector of the color wheel contains this hue
            // hsbColor.H ranges from 0 to 255, and there are 6 sectors, so 42.5 per sector
            int sector = (int)Math.Floor(hsbColor.H / 42.5);

            // Calculate where the hue lies within the sector for interpolation purpose
            double fraction = hsbColor.H / 42.5 - sector;

            double sFrac = hsbColor.S / 255.0;
            byte p = (byte)((hsbColor.B * (1.0 - sFrac)) + 0.5);
            byte q = (byte)((hsbColor.B * (1.0 - sFrac * fraction)) + 0.5);
            byte t = (byte)((hsbColor.B * (1.0 - sFrac * (1.0 - fraction))) + 0.5);

            switch (sector) {
                case 0: // red - yellow
                    rgbColor = Color.FromArgb(hsbColor.A, hsbColor.B, t, p);
                    break;
                case 1: // yellow - green
                    rgbColor = Color.FromArgb(hsbColor.A, q, hsbColor.B, p);
                    break;
                case 2: // green - cyan
                    rgbColor = Color.FromArgb(hsbColor.A, p, hsbColor.B, t);
                    break;
                case 3: // cyan - blue
                    rgbColor = Color.FromArgb(hsbColor.A, p, q, hsbColor.B);
                    break;
                case 4: // blue - magenta
                    rgbColor = Color.FromArgb(hsbColor.A, t, p, hsbColor.B);
                    break;
                case 5:
                default: // magenta - red
                    rgbColor = Color.FromArgb(hsbColor.A, hsbColor.B, p, q);
                    break;
            }

            return rgbColor;
        }

        /// <summary>
        ///  //ColorTranslator.FromHtml(htmlColor); // Color.FromArgb(HtmlColorToArgb(htmlColor));
        /// </summary>
        /// <param name="htmlColor"></param>
        /// <returns></returns>
        public static Color ParseHtmlColor(string htmlColor) {
            string s = htmlColor.TrimStart('#');
            int charIndex = 0;
            int a = 255;
            if (s.Length == 8) {
                if (!int.TryParse(s.Substring(charIndex, 2), NumberStyles.HexNumber, CultureInfo.CurrentCulture, out a)) {
                    return Color.Transparent;
                }
                charIndex += 2;
            }
            if (!int.TryParse(s.Substring(charIndex, 2), NumberStyles.HexNumber, CultureInfo.CurrentCulture, out int r)) {
                return Color.Transparent;
            }
            charIndex += 2;
            if (!int.TryParse(s.Substring(charIndex, 2), NumberStyles.HexNumber, CultureInfo.CurrentCulture, out int g)) {
                return Color.Transparent;
            }
            charIndex += 2;
            if (!int.TryParse(s.Substring(charIndex, 2), NumberStyles.HexNumber, CultureInfo.CurrentCulture, out int b)) {
                return Color.Transparent;
            }
            return Color.FromArgb(a, r, g, b);
        }

        /// <summary>
        /// 0 - прозрачный, 255 (FF) - не прозрачный
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string ToHtml(System.Drawing.Color color) {
            return string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", color.A, color.R, color.G, color.B);
            //return ColorTranslator.ToHtml(color);
        }
    }
}
