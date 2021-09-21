using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Xml;

namespace CardRotager {
    public class Settings {
        private const int CROP_PADDING_TOP = 80;
        private const int CROP_PADDING_RIGHT = 80;
        private const int PERCENT_HOR_PADDING = 10;
        private static readonly Color CROP_FILL_COLOR = Color.White;
        private static readonly Color CUT_MARK_COLOR = Color.Gray;
        private const int cutMarkRadius = 40;
        private const int cutMarkThick = 7;

        private const int KILL_LENGTH = 140;

        /// <summary>
        /// Минимальная длина линии которая уходит для обработки рамки карты
        /// </summary>
        private const int MIN_LINE_SIZE_X = 15;

        private const int MIN_LINE_SIZE_Y = 60; //для маленькой картинки должно быть поменьше, для большой 100 (>500).
        private const int THICK = 18;
        private const int DOTLINE_Y_PREV_MAX_ADD_HEIGHT = 150;
        private const int DEL_LINE_LESS_SIZE = 27;
        private const int IGNORE_FIRST_Y_PIXELS = 0;
        private const int DOTLINE_Y_CUR_MIN_ADD_HEIGHT = 200;
        
        private const int DOTLINE_X_MIN_ADD_WIDTH = 100;
        
        
        private const string xmlLastFileName = "lastFileName";
        private const string xmlSaveEachRectanglePath = "saveEachRectanglePath";
        private const string xmlCustomSavePath = "customSavePath";
        private const string xmlConvertOpenImage = "convertOpenImage";
        private const string xmlRotateSubImages = "rotateSubImages";
        private const string xmlProcessCycleF4 = "processCycleF4";
        private const string xmlShowImageFoundContour = "showImageFoundContour";
        private const string xmlShowImageTargetFrame = "showImageTargetFrame";
        private const string xmlShowDetailDotsOfImageNumber = "showDetailDotsOfImageNumber";
        private const string xmlShowRuler = "showRuler";
        private const string xmlShowAngleLines = "showAngleLines";
        private const string xmlShowHorVertLines = "showHorVertLines";
        private const string xmlProcessWhenOpen = "processWhenOpen";
        private const string xmlCropFillColor = "cropFillColor";
        private const string xmlCropPaddingTop = "cropPaddingTop";
        private const string xmlCropPaddingRight = "cropPaddingRight";
        private const string xmlFlipHorizontalEachRect = "flipHorizontalEachRect";
        private const string xmlFlipHorizontalEachRectFileMask = "flipHorizontalEachRectFileMask";
        private const string xmlPercentHorizontalPadding = "percentHorizontalPadding";
        
        private const string xmlDotLineXMinAddWidth = "dotLineXMinAddWidth";
        private const string xmlDotLineYMaxSubtractHeight = "dotLineYMaxSubtractHeight";
        private const string xmlDotLineYMinAddHeight = "dotLineYMinAddHeight";
        private const string xmlDelLineLessSize = "delLineLessSize";
        private const string xmlIgnoreYPixels = "ignoreFirstYPixels";
        private const string xmlMinLineSizeX = "minLineSizeX";
        private const string xmlMinLineSizeY = "minLineSizeY";
        private const string xmlKillLength = "killLength";
        private const string xmlHVThickLine = "HVThickLine";
        private const string xmlCutMarkShowOnTargetImageMask = "cutMarkShowOnTargetImageMask";
        private const string xmlCutMarkColor = "cutMarkColor";
        private const string xmlCutMarkRadius = "cutMarkRadius";
        private const string xmlCutMarkThick = "cutMarkThick";
        
        private const string debug = "Отладка";
        private const string main = "Основные";
        private const string process = "Обработка";
        
        private Color cropFillColor;
        private Brush cropFillBrush;

        public Settings() {
            CropPaddingTop = CROP_PADDING_TOP;
            CropPaddingRight = CROP_PADDING_RIGHT;
            CropFillColor = CROP_FILL_COLOR;
            PercentHorizontalPadding = PERCENT_HOR_PADDING;
            DotLineXMinAddWidth = DOTLINE_X_MIN_ADD_WIDTH;
            DotLineYCurMinAddHeight = DOTLINE_Y_CUR_MIN_ADD_HEIGHT;
            DotLineYPrevMaxAddHeight = DOTLINE_Y_PREV_MAX_ADD_HEIGHT;
            DelLineLessSize = DEL_LINE_LESS_SIZE;
            IgnoreFirstYPixels = IGNORE_FIRST_Y_PIXELS;
            MinLineSizeX = MIN_LINE_SIZE_X;
            MinLineSizeY = MIN_LINE_SIZE_Y;
            HVThickLine = THICK;
            KillLength = KILL_LENGTH;
            
            ConvertOpenImage = true;
            LastOpenFileName = "";
            RotateFoundSubImages = false;
            ProcessCycleF4 = false;
            ShowImageFoundContour = false;
            ShowDetailDotsOfImageNumber = 0;
            ShowRuler = false;
            ShowAngleLines = false;
            ShowHorVertLines = false;
            ShowImageTargetFrame = false;
            ProcessWhenOpen = false;
            CutMarkShowOnTargetImageMask = "";
            CutMarkColor = CUT_MARK_COLOR;
            CutMarkRadius = cutMarkRadius;
            CutMarkThick = cutMarkThick;
        }

        [Browsable(true)]
        [Category(main)]
        [Description("Конвертировать открываемую картинку в Серый цвет")]
        [DisplayName("ConvertOpenImage")]
        public bool ConvertOpenImage { get; set; }

        [Browsable(true)]
        [Category(main)]
        [Description("Вращение найденных изображений карт")]
        [DisplayName("RotateFoundSubImages")]
        public bool RotateFoundSubImages { get; set; }

        [Browsable(true)]
        [Category(debug)]
        [Description("F4 действует по кругу")]
        [DisplayName("ProcessCycleF4")]
        public bool ProcessCycleF4 { get; set; }

        [Browsable(true)]
        [Category(debug)]
        [Description("Показывать найденные контуры карт")]
        [DisplayName("ShowImageFoundContour")]
        public bool ShowImageFoundContour { get; set; }

        [Browsable(true)]
        [Category(debug)]
        [Description("Показывать список точек (голубым цветом) для номера карты по которым создаются линии (0 - не показывать)")]
        [DisplayName("ShowDetailDotsOfImageNumber")]
        public int ShowDetailDotsOfImageNumber { get; set; }

        [Browsable(true)]
        [Category(debug)]
        [Description("Отображение линии-текст линейки")]
        [DisplayName("ShowRuler")]
        public bool ShowRuler { get; set; }

        [Browsable(true)]
        [Category(debug)]
        [Description("Отображение горизонтальных и вертикальных линий")]
        [DisplayName("ShowHorVertLines")]
        public bool ShowHorVertLines { get; set; }
        
        [Browsable(true)]
        [Category(debug)]
        [Description("Толщина линий для горизонтальных и вертикальных линий для параметра ShowHorVertLines")]
        [DisplayName("HVThickLine")]
        public int HVThickLine { get; set; }

        [Browsable(true)]
        [Category(main)]
        [Description("Отразить по горизонтали каждый прямоугольник внутри себя и самих прямоугольник относительно друг друга")]
        [DisplayName("FlipHorizontalEachRect")]
        public bool FlipHorizontalEachRect { get; set; }

        [Browsable(true)]
        [Category(main)]
        [Description("Маска имени файла, когда изменить установленный в свойстве FlipHorizontalEachRect статус на противоположный")]
        [DisplayName("FlipHorizontalEachRectFileMask")]
        public string FlipHorizontalEachRectFileMask { get; set; }
        
        [Browsable(true)]
        [Category(process)]
        [Description("Минимальная учитываемая длина лини для определения горизонтальных линий")]
        [DisplayName("MinLineSizeX")]
        [DefaultValue(MIN_LINE_SIZE_X)]
        public int MinLineSizeX { get; set; }
        
        [Browsable(true)]
        [Category(process)]
        [Description("Минимальная учитываемая длина лини для определения вертикальных линий")]
        [DisplayName("MinLineSizeY")]
        [DefaultValue(MIN_LINE_SIZE_Y)]
        public int MinLineSizeY { get; set; }
        
        [Browsable(true)]
        [Category(process)]
        [Description("Насколько пикселей расширить вправо область поиска вертикальной линии (shift X for minX)")]
        [DisplayName("DotLineXMinAddWidth")]
        [DefaultValue(DOTLINE_X_MIN_ADD_WIDTH)]
        public int DotLineXMinAddWidth { get; set; }
        
        [Browsable(true)]
        [Category(process)]
        [Description("Насколько пикселей по вертикали расширить вниз область поиска горизонтальной линии от начала вертикальной линии (shift by Y for minY)")]
        [DisplayName("DotLineYCurMinAddHeight")]
        [DefaultValue(DOTLINE_Y_CUR_MIN_ADD_HEIGHT)]
        public int DotLineYCurMinAddHeight { get; set; }
        
        [Browsable(true)]
        [Category(process)]
        [Description("Насколько пикселей по вертикали нужно отступить вниз для поиска горизонтальной линии от конца предыдущей вертикальной линии (shift by Y for previous maxY)")]
        [DisplayName("DotLineYPrevMaxAddHeight")]
        [DefaultValue(DOTLINE_Y_PREV_MAX_ADD_HEIGHT)]
        public int DotLineYPrevMaxAddHeight { get; set; }
        
        [Browsable(true)]
        [Category(process)]
        [Description("Длина линии до которой пробовать подстраивать позицию Y под предыдущую горизонтальную линию")]
        [DisplayName("DelLineLessSize")]
        [DefaultValue(DEL_LINE_LESS_SIZE)]
        public int DelLineLessSize { get; set; }
        
        [Browsable(true)]
        [Category(process)]
        [Description("Игнорировать Y-пикселей для  определения верхнего ряда горизонтальных линий (игнорировать мусорные пылинки)")]
        [DisplayName("IgnoreFirstYPixels")]
        [DefaultValue(IGNORE_FIRST_Y_PIXELS)]
        public int IgnoreFirstYPixels { get; set; }
       
        [Browsable(true)]
        [Category(process)]
        [Description("Удалим горизонтальные линии меньше указанной длины")]
        [DisplayName("KillLength")]
        public int KillLength { get; set; }

        [Browsable(true)]
        [Category(main)]
        [Description("Для определения наклона линий отступить от края указанный процент (1-50)")]
        [DisplayName("PercentHorizontalPadding")]
        public int PercentHorizontalPadding { get; set; }

        [Browsable(true)]
        [Category(debug)]
        [Description("Отображение линий по которой расчитывает угол")]
        [DisplayName("ShowAngleLines")]
        public bool ShowAngleLines { get; set; }

        [Browsable(true)]
        [Category(debug)]
        [Description("Показывать области куда помещаются карты")]
        [DisplayName("ShowImageTargetFrame")]
        public bool ShowImageTargetFrame { get; set; }
        
        [Browsable(true)]
        [Category(process)]
        [Description("Маска файла для которого добавляются на итоговое изображение точки реза (в виде перекрестий на стыке карт)")]
        [DisplayName("CutMarkShowOnTargetImageMask")]
        public string CutMarkShowOnTargetImageMask { get; set; }
        
        [Browsable(true)]
        [Category(process)]
        [Description("Цвет точки реза (если задан параметр CutMarkShowOnTargetImageMask)")]
        [DisplayName("CutMarkColor")]
        public Color CutMarkColor { get; set; }
        
        [Browsable(true)]
        [Category(process)]
        [Description("Радиус луча для точки реза (если задан параметр CutMarkShowOnTargetImageMask)")]
        [DisplayName("CutMarkRadius")]
        [DefaultValue(cutMarkRadius)]
        public int CutMarkRadius { get; set; }
        
        [Browsable(true)]
        [Category(process)]
        [Description("Толщина линий для точек реза (если задан параметр CutMarkShowOnTargetImageMask)")]
        [DisplayName("CutMarkThick")]
        [DefaultValue(cutMarkThick)]
        public int CutMarkThick { get; set; }

        [Browsable(true)]
        [Category(debug)]
        [Description("При открытии сразу обработать")]
        [DisplayName("ProcessWhenOpen")]
        public bool ProcessWhenOpen { get; set; }

        [Browsable(true)]
        [Category(main)]
        [Description("Обрезать сверху карту (после поворота)")]
        [DisplayName("CropPaddingTop")]
        public int CropPaddingTop { get; set; }

        [Browsable(true)]
        [Category(main)]
        [Description("Обрезать справа карту (после поворота)")]
        [DisplayName("CropPaddingRight")]
        public int CropPaddingRight { get; set; }

        [Browsable(true)]
        [Category(main)]
        [Description("Цвет заполнения для обрезки карты (после поворота)")]
        [DisplayName("CropPaddingColor")]
        public Color CropFillColor {
            get => cropFillColor;
            set {
                cropFillBrush = null;
                cropFillColor = value;
            }
        }

        [Browsable(false)]
        public Brush CropFillBrush {
            get {
                if (cropFillBrush == null) {
                    cropFillBrush = new SolidBrush(CropFillColor);
                }
                return cropFillBrush;
            }
        }

        [Browsable(true)]
        [Description("Путь и имя последнего открытого файла")]
        [DisplayName("LastOpenFileName")]
        [EditorAttribute(typeof(UIFolderNameEditor), typeof(UITypeEditor))]
        public string LastOpenFileName { get; set; }

        [Browsable(true)]
        [Category(debug)]
        [Description("Путь к папке для сохранения найденных карт на картинке (имя img<номер карты>.bmp). Если не заполнено, не сохраняется")]
        [DisplayName("SaveEachRectanglePath")]
        [EditorAttribute(typeof(UIFolderNameEditor), typeof(UITypeEditor))]
        public string SaveEachRectanglePath { get; set; }
        
        [Browsable(true)]
        [Category(main)]
        [Description("Путь к папке для сохранения. Если не заполнено, используется путь предлагаемый диалогом Windows")]
        [DisplayName("CustomSavePath")]
        [EditorAttribute(typeof(UIFolderNameEditor), typeof(UITypeEditor))]
        public string CustomSavePath { get; set; }

        public void LoadFromXml(string fileName) {
            if (!File.Exists(fileName)) {
                return;
            }
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(fileName);

            XmlNode xmlRoot = xmlDoc.DocumentElement.SelectSingleNode("/settings");
            if (!(xmlRoot is XmlElement element)) {
                return;
            }
            LastOpenFileName = element.GetAttribute(xmlLastFileName);
            SaveEachRectanglePath = element.GetAttribute(xmlSaveEachRectanglePath);
            CustomSavePath = element.GetAttribute(xmlCustomSavePath);
            FlipHorizontalEachRectFileMask = element.GetAttribute(xmlFlipHorizontalEachRectFileMask);
            if (bool.TryParse(element.GetAttribute(xmlConvertOpenImage), out bool cb)) {
                ConvertOpenImage = cb;
            }
            if (bool.TryParse(element.GetAttribute(xmlProcessCycleF4), out cb)) {
                ProcessCycleF4 = cb;
            }
            if (bool.TryParse(element.GetAttribute(xmlShowImageFoundContour), out cb)) {
                ShowImageFoundContour = cb;
            }
            if (int.TryParse(element.GetAttribute(xmlShowDetailDotsOfImageNumber), out int intValue)) {
                ShowDetailDotsOfImageNumber = intValue;
            }
            if (int.TryParse(element.GetAttribute(xmlDotLineXMinAddWidth), out intValue)) {
                DotLineXMinAddWidth = intValue;
            }
            if (int.TryParse(element.GetAttribute(xmlDotLineYMinAddHeight), out intValue)) {
                DotLineYPrevMaxAddHeight = intValue;
            }
            if (int.TryParse(element.GetAttribute(xmlDelLineLessSize), out intValue)) {
                DelLineLessSize = intValue;
            }
            if (int.TryParse(element.GetAttribute(xmlIgnoreYPixels), out intValue)) {
                IgnoreFirstYPixels = intValue;
            }
            if (int.TryParse(element.GetAttribute(xmlDotLineYMaxSubtractHeight), out intValue)) {
                DotLineYCurMinAddHeight = intValue;
            }
            if (int.TryParse(element.GetAttribute(xmlMinLineSizeX), out intValue)) {
                MinLineSizeX = intValue;
            }
            if (int.TryParse(element.GetAttribute(xmlMinLineSizeY), out intValue)) {
                MinLineSizeY = intValue;
            }
            if (int.TryParse(element.GetAttribute(xmlHVThickLine), out intValue)) {
                HVThickLine = intValue;
            }
            if (int.TryParse(element.GetAttribute(xmlKillLength), out intValue)) {
                KillLength = intValue;
            }
            
            CutMarkShowOnTargetImageMask = element.GetAttribute(xmlCutMarkShowOnTargetImageMask);
            if (int.TryParse(element.GetAttribute(xmlCutMarkRadius), out intValue)) {
                CutMarkRadius = intValue;
            }
            if (int.TryParse(element.GetAttribute(xmlCutMarkThick), out intValue)) {
                CutMarkThick = intValue;
            }
            string stHexColor = element.GetAttribute(xmlCutMarkColor);
            if (!string.IsNullOrWhiteSpace(stHexColor)) {
                CutMarkColor = Util.ParseHtmlColor(stHexColor);
            }

            if (bool.TryParse(element.GetAttribute(xmlShowRuler), out cb)) {
                ShowRuler = cb;
            }
            if (bool.TryParse(element.GetAttribute(xmlShowAngleLines), out cb)) {
                ShowAngleLines = cb;
            }
            if (bool.TryParse(element.GetAttribute(xmlShowImageTargetFrame), out cb)) {
                ShowImageTargetFrame = cb;
            }
            if (bool.TryParse(element.GetAttribute(xmlRotateSubImages), out cb)) {
                RotateFoundSubImages = cb;
            }
            if (bool.TryParse(element.GetAttribute(xmlProcessWhenOpen), out cb)) {
                ProcessWhenOpen = cb;
            }
            if (bool.TryParse(element.GetAttribute(xmlShowHorVertLines), out cb)) {
                ShowHorVertLines = cb;
            }
            if (bool.TryParse(element.GetAttribute(xmlFlipHorizontalEachRect), out cb)) {
                FlipHorizontalEachRect = cb;
            }
            stHexColor = element.GetAttribute(xmlCropFillColor);
            if (!string.IsNullOrWhiteSpace(stHexColor)) {
                CropFillColor = Util.ParseHtmlColor(stHexColor);
            }
            if (int.TryParse(element.GetAttribute(xmlCropPaddingTop), out intValue)) {
                CropPaddingTop = intValue;
            }
            if (int.TryParse(element.GetAttribute(xmlPercentHorizontalPadding), out intValue)) {
                PercentHorizontalPadding = intValue;
            }
            if (int.TryParse(element.GetAttribute(xmlCropPaddingRight), out intValue)) {
                CropPaddingRight = intValue;
            }
        }

        public void saveToXml(string fileName) {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<settings/>");
            XmlNode xmlRoot = xmlDoc.DocumentElement.SelectSingleNode("/settings");
            XmlElement xmlNode = ((XmlElement) xmlRoot);
            xmlNode.SetAttribute(xmlLastFileName, LastOpenFileName);
            xmlNode.SetAttribute(xmlSaveEachRectanglePath, SaveEachRectanglePath);
            xmlNode.SetAttribute(xmlCustomSavePath, CustomSavePath);
            xmlNode.SetAttribute(xmlRotateSubImages, RotateFoundSubImages.ToString());
            xmlNode.SetAttribute(xmlConvertOpenImage, ConvertOpenImage.ToString());
            xmlNode.SetAttribute(xmlProcessCycleF4, ProcessCycleF4.ToString());
            xmlNode.SetAttribute(xmlShowImageFoundContour, ShowImageFoundContour.ToString());
            xmlNode.SetAttribute(xmlShowImageTargetFrame, ShowImageTargetFrame.ToString());
            xmlNode.SetAttribute(xmlShowDetailDotsOfImageNumber, ShowDetailDotsOfImageNumber.ToString());
            xmlNode.SetAttribute(xmlShowRuler, ShowRuler.ToString());
            xmlNode.SetAttribute(xmlShowAngleLines, ShowAngleLines.ToString());
            xmlNode.SetAttribute(xmlShowHorVertLines, ShowHorVertLines.ToString());
            xmlNode.SetAttribute(xmlProcessWhenOpen, ProcessWhenOpen.ToString());
            xmlNode.SetAttribute(xmlCropFillColor, Util.ToHtml(CropFillColor));
            xmlNode.SetAttribute(xmlCropPaddingTop, CropPaddingTop.ToString());
            xmlNode.SetAttribute(xmlCropPaddingRight, CropPaddingRight.ToString());
            xmlNode.SetAttribute(xmlFlipHorizontalEachRect, FlipHorizontalEachRect.ToString());
            xmlNode.SetAttribute(xmlFlipHorizontalEachRectFileMask, FlipHorizontalEachRectFileMask.ToString());
            xmlNode.SetAttribute(xmlPercentHorizontalPadding, PercentHorizontalPadding.ToString());
            xmlNode.SetAttribute(xmlDotLineXMinAddWidth, DotLineXMinAddWidth.ToString());
            xmlNode.SetAttribute(xmlDotLineYMaxSubtractHeight, DotLineYCurMinAddHeight.ToString());
            xmlNode.SetAttribute(xmlDotLineYMinAddHeight, DotLineYPrevMaxAddHeight.ToString());
            xmlNode.SetAttribute(xmlDelLineLessSize, DelLineLessSize.ToString());
            xmlNode.SetAttribute(xmlIgnoreYPixels, IgnoreFirstYPixels.ToString());
            xmlNode.SetAttribute(xmlMinLineSizeX, MinLineSizeX.ToString());
            xmlNode.SetAttribute(xmlMinLineSizeY, MinLineSizeY.ToString());
            xmlNode.SetAttribute(xmlHVThickLine, HVThickLine.ToString());
            xmlNode.SetAttribute(xmlKillLength, KillLength.ToString());
            
            xmlNode.SetAttribute(xmlCutMarkShowOnTargetImageMask, CutMarkShowOnTargetImageMask.ToString());
            xmlNode.SetAttribute(xmlCutMarkColor, Util.ToHtml(CutMarkColor));
            xmlNode.SetAttribute(xmlCutMarkRadius, CutMarkRadius.ToString());
            xmlNode.SetAttribute(xmlCutMarkThick, CutMarkThick.ToString());
            xmlDoc.Save(fileName);
        }
    }
}