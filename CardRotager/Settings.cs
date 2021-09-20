using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Xml;
using CardRotager.Helper;

namespace CardRotager {
    public class Settings {
        private const int CROP_PADDING_TOP = 80;
        private const int CROP_PADDING_RIGHT = 80;
        private const int PERCENT_HOR_PADDING = 10;
        private static readonly Color CROP_FILL_COLOR = Color.White;
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
        private const string debug = "Отладка";
        private const string main = "Основные";
        private Color cropFillColor;
        private Brush cropFillBrush;

        public Settings() {
            CropPaddingTop = CROP_PADDING_TOP;
            CropPaddingRight = CROP_PADDING_RIGHT;
            CropFillColor = CROP_FILL_COLOR;
            PercentHorizontalPadding = PERCENT_HOR_PADDING;

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
            string stHexColor = element.GetAttribute(xmlCropFillColor);
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
            xmlDoc.Save(fileName);
        }
    }
}