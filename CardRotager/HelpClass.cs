using System.Drawing;
using System.Drawing.Imaging;

namespace CardRotager {
    public static class HelpClass {
        /// <summary>
        /// https://askdev.ru/q/nepodderzhivaemyy-pikselnyy-format-ishodnogo-ili-shablonnogo-izobrazheniya-izobrazhenie-aforge-474644/
        ///  System.Drawing.Imaging.PixelFormat.Format24bppRgb
        /// </summary>
        /// <param name="image"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static Bitmap ConvertToFormat(this Image image, PixelFormat format) {
            Bitmap copy = new Bitmap(image.Width, image.Height, format);
            using (Graphics gr = Graphics.FromImage(copy)) {
                gr.DrawImage(image, new Rectangle(0, 0, copy.Width, copy.Height));
            }
            return copy;
        }
    }
}
