using System;
using System.Drawing;

namespace CardRotager {
    public static class RandomColors {
        static readonly Random random = new Random();

        //static RandomColors() {
        //    colors = "#D32F2F, #B71C1C, #C2185B, #880E4F, #7B1FA2, #4A148C, #512DA8, #311B92, #303F9F, #1A237E, #1976D2, #0D47A1, #0288D1, #01579B, #0097A7, #006064, #00796B, #004D40, #388E3C, #1B5E20, #689F38, #33691E, #AFB42B, #827717, #FBC02D, #F57F17, #FFA000, #FF6F00, #F57C00, #E65100, #E64A19, #BF360C, #5D4037, #3E2723, #616161, #212121, #455A64, #263238"
        //        .Split(new string[] { ",", " " }, StringSplitOptions.RemoveEmptyEntries);
        //}

        static readonly string[] colors = {
"#808000", //Olive - 128, 128, 0
//"#FFFFFF", //White - 255, 255, 255
"#FF00FF", //Fuchsia - 255, 0, 255
"#00FFFF", //Aqua - 0, 255, 255
"#00FF00", //Lime - 0, 255, 0
"#800080", //Purple - 128, 0, 128
"#FF0000", //Red - 255, 0, 0
"#800000", //Maroon - 128, 0, 0
"#0000FF", //Blue - 0, 0, 255
"#FFFF00", //Yellow - 255, 255, 0
"#008000", //Green - 0, 128, 0
"#008080", //Teal - 0, 128, 128
"#C0C0C0", //Silver - 192, 192, 192
"#000080", //Navy - 0, 0, 128
"#808080", //Gray - 128, 128, 128
                };

        public static int curIndex = 0;
        public static Color RandomColor {
            get {
                //random.Next(colors.Length)
                if(curIndex >= colors.Length) {
                    curIndex = 0;
                }
                return Util.ParseHtmlColor(colors[curIndex++]);
            }
        }
    }
}
