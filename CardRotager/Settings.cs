using System.Drawing;
using System.IO;
using System.Xml;

namespace CardRotager {
    public class Settings {
        private const int CROP_PADDING_TOP = 80;
        private const int CROP_PADDING_RIGHT = 80;
        private const int PERCENT_HOR_PADDING = 10;
        private static readonly Color CROP_PADDING_COLOR = Color.White;
        private static readonly Color CUT_MARK_COLOR = Color.Gray;
        private const int CUT_MARK_RADIUS = 80;
        private const int CUT_MARK_THICK = 7;
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
        private const bool CONVERT_OPEN_IMAGE = true;
        private const int DOTLINE_X_MIN_ADD_WIDTH = 100;
        private const string debug = "Отладка";
        private const string main = "Основные";
        private const string process = "Обработка";
        private const string xmlConvertOpenImage = "convertOpenImage";
        private const string xmlRotateFoundSubImages = "rotateFoundSubImages";
        private const string xmlShowImageFoundContour = "showImageFoundContour";
        private const string xmlProcessCycleF4 = "processCycleF4";
        private const string xmlShowDetailDotsOfImageNumber = "showDetailDotsOfImageNumber";
        private const string xmlShowRuler = "showRuler";
        private const string xmlHVThickLine = "HVThickLine";
        private const string xmlFlipHorizontalEachRect = "flipHorizontalEachRect";
        private const string xmlMinLineSizeX = "minLineSizeX";
        private const string xmlMinLineSizeY = "minLineSizeY";
        private const string xmlDotLineXMinAddWidth = "dotLineXMinAddWidth";
        private const string xmlDotLineYCurMinAddHeight = "dotLineYCurMinAddHeight";
        private const string xmlDotLineYPrevMaxAddHeight = "dotLineYPrevMaxAddHeight";
        private const string xmlDelLineLessSize = "delLineLessSize";
        private const string xmlIgnoreFirstYPixels = "ignoreFirstYPixels";
        private const string xmlKillLength = "killLength";
        private const string xmlPercentHorizontalPadding = "percentHorizontalPadding";
        private const string xmlShowAngleLines = "showAngleLines";
        private const string xmlShowImageTargetFrame = "showImageTargetFrame";
        private const string xmlCutMarkShowOnTargetImageMask = "cutMarkShowOnTargetImageMask";
        private const string xmlCutMarkColor = "cutMarkColor";
        private const string xmlCutMarkRadius = "cutMarkRadius";
        private const string xmlCutMarkThick = "cutMarkThick";
        private const string xmlProcessWhenOpen = "processWhenOpen";
        private const string xmlCropPaddingTop = "cropPaddingTop";
        private const string xmlCropPaddingRight = "cropPaddingRight";
        private const string xmlCropPaddingColor = "cropPaddingColor";
        private const string xmlLastOpenFileName = "lastOpenFileName";
        private const string xmlSaveEachRectangleFileName = "saveEachRectangleFileName";
        private const string xmlCustomSavePath = "customSavePath";
        private const string xmlFlipHorizontalEachRectFileMask = "flipHorizontalEachRectFileMask";
        private const string xmlShowHorVertLines = "showHorVertLines";
        public PropertyObject PropertyObject { get; }

        public Settings() {
            PropertyObject = new PropertyObject(this);

            PropertyObject.addParam(main, xmlConvertOpenImage, CONVERT_OPEN_IMAGE, l("Конвертировать открываемую картинку в Серый цвет"));
            PropertyObject.addParam(main, xmlRotateFoundSubImages, false, l("Вращение найденных изображений карт"));
            PropertyObject.addParam(main, xmlLastOpenFileName, "", l("Путь и имя последнего открытого файла"), true, false, new UIFolderNameEditor());
            PropertyObject.addParam(main, xmlFlipHorizontalEachRect, false, l("Отразить по горизонтали каждый прямоугольник внутри себя и самих прямоугольник относительно друг друга"));
            PropertyObject.addParam(main, xmlPercentHorizontalPadding, PERCENT_HOR_PADDING, l("Для определения наклона линий отступить от края указанный процент (1-50)"));
            PropertyObject.addParam(main, xmlFlipHorizontalEachRectFileMask, "", l("Маска имени файла, когда изменить установленный в свойстве FlipHorizontalEachRect статус на противоположный"));
            PropertyObject.addParam(main, xmlCustomSavePath, "", l("Путь к папке для сохранения. Если не заполнено, используется путь предлагаемый диалогом Windows"), true, false, new UIFolderNameEditor());

            PropertyObject.addParam(debug, xmlProcessCycleF4, false, l("F4 действует по кругу"));
            PropertyObject.addParam(debug, xmlShowImageFoundContour, false, l("Показывать найденные контуры карт"));
            PropertyObject.addParam(debug, xmlShowDetailDotsOfImageNumber, 0, l("Показывать список точек (голубым цветом) для номера карты по которым создаются линии (0 - не показывать)"));
            PropertyObject.addParam(debug, xmlShowRuler, false, l("Отображение линии-текст линейки"));
            PropertyObject.addParam(debug, xmlShowHorVertLines, false, l("Отображение горизонтальных и вертикальных линий"));
            PropertyObject.addParam(debug, xmlHVThickLine, THICK, l("Толщина линий для горизонтальных и вертикальных линий для параметра ShowHorVertLines"));
            PropertyObject.addParam(debug, xmlShowAngleLines, false, l("Отображение линий по которой рассчитывает угол"));
            PropertyObject.addParam(debug, xmlShowImageTargetFrame, false, l("Показывать области куда помещаются карты"));
            PropertyObject.addParam(debug, xmlProcessWhenOpen, CONVERT_OPEN_IMAGE, l("При открытии сразу обработать"));
            PropertyObject.addParam(debug, xmlSaveEachRectangleFileName, "", l("Путь к папке и имя для сохранения найденных карт на картинке (пример: c:\\temp\\{fno}\\img{#}.bmp). Доступны автозамены: {#} на <номер карты>, {fn} на имя главного файла, {fno} на имя глав.файла без расширения и точки. Вместо bmp можно подставить расширения jpg, png, tif. Если не заполнено, не сохраняется"), true, false, new UIFolderNameEditor());

            PropertyObject.addParam(process, xmlCropPaddingTop, CROP_PADDING_TOP, l("Обрезать сверху карту (после поворота)"));
            PropertyObject.addParam(process, xmlCropPaddingRight, CROP_PADDING_RIGHT, l("Обрезать справа карту (после поворота)"));
            PropertyObject.addParam(process, xmlCropPaddingColor, CROP_PADDING_COLOR, l("Цвет заполнения для обрезки карты (после поворота)"));
            PropertyObject.addParam(process, xmlMinLineSizeX, MIN_LINE_SIZE_X, l("Минимальная учитываемая длина линии для определения горизонтальных линий"));
            PropertyObject.addParam(process, xmlMinLineSizeY, MIN_LINE_SIZE_Y, l("Минимальная учитываемая длина линии для определения вертикальных линий"));
            PropertyObject.addParam(process, xmlDotLineXMinAddWidth, DOTLINE_X_MIN_ADD_WIDTH, l("Насколько пикселей расширить вправо область поиска вертикальной линии (shift X for minX)"));
            PropertyObject.addParam(process, xmlDotLineYCurMinAddHeight, DOTLINE_Y_CUR_MIN_ADD_HEIGHT, l("Насколько пикселей по вертикали расширить вниз область поиска горизонтальной линии от начала вертикальной линии (shift by Y for minY)"));
            PropertyObject.addParam(process, xmlDotLineYPrevMaxAddHeight, DOTLINE_Y_PREV_MAX_ADD_HEIGHT, l("Насколько пикселей по вертикали нужно отступить вниз для поиска горизонтальной линии от конца предыдущей вертикальной линии (shift by Y for previous maxY)"));
            PropertyObject.addParam(process, xmlDelLineLessSize, DEL_LINE_LESS_SIZE, l("Длина линии до которой пробовать подстраивать позицию Y под предыдущую горизонтальную линию"));
            PropertyObject.addParam(process, xmlIgnoreFirstYPixels, IGNORE_FIRST_Y_PIXELS, l("Игнорировать Y-пикселей для  определения верхнего ряда горизонтальных линий (игнорировать мусорные пылинки)"));
            PropertyObject.addParam(process, xmlKillLength, KILL_LENGTH, l("Удалим горизонтальные линии меньше указанной длины"));
            PropertyObject.addParam(process, xmlCutMarkShowOnTargetImageMask, "", l("Маска файла для которого добавляются на итоговое изображение точки реза (в виде перекрестий на стыке карт)"));
            PropertyObject.addParam(process, xmlCutMarkColor, CUT_MARK_COLOR, l("Показывать список точек (голубым цветом) для номера карты по которым создаются линии (0 - не показывать)"));
            PropertyObject.addParam(process, xmlCutMarkRadius, CUT_MARK_RADIUS, l("Радиус луча для точки реза (если задан параметр CutMarkShowOnTargetImageMask)"));
            PropertyObject.addParam(process, xmlCutMarkThick, CUT_MARK_THICK, l("Толщина линий для точек реза (если задан параметр CutMarkShowOnTargetImageMask)"));
        }

        private string l(string l) {
            return l;
        }

        public bool ConvertOpenImage {
            get => (bool) PropertyObject[xmlConvertOpenImage].Value;
            set => PropertyObject[xmlConvertOpenImage].Value = value;
        }

        public bool RotateFoundSubImages {
            get => (bool) PropertyObject[xmlRotateFoundSubImages].Value;
            set => PropertyObject[xmlRotateFoundSubImages].Value = value;
        }

        public bool ProcessCycleF4 {
            get => (bool) PropertyObject[xmlProcessCycleF4].Value;
            set => PropertyObject[xmlProcessCycleF4].Value = value;
        }

        public bool ShowImageFoundContour {
            get => (bool) PropertyObject[xmlShowImageFoundContour].Value;
            set => PropertyObject[xmlShowImageFoundContour].Value = value;
        }

        public int ShowDetailDotsOfImageNumber {
            get => (int) PropertyObject[xmlShowDetailDotsOfImageNumber].Value;
            set => PropertyObject[xmlShowDetailDotsOfImageNumber].Value = value;
        }

        public bool ShowRuler {
            get => (bool) PropertyObject[xmlShowRuler].Value;
            set => PropertyObject[xmlShowRuler].Value = value;
        }

        public bool ShowHorVertLines {
            get => (bool) PropertyObject[xmlShowHorVertLines].Value;
            set => PropertyObject[xmlShowHorVertLines].Value = value;
        }

        public int HVThickLine {
            get => (int) PropertyObject[xmlHVThickLine].Value;
            set => PropertyObject[xmlHVThickLine].Value = value;
        }

        public bool FlipHorizontalEachRect {
            get => (bool) PropertyObject[xmlFlipHorizontalEachRect].Value;
            set => PropertyObject[xmlFlipHorizontalEachRect].Value = value;
        }

        public string FlipHorizontalEachRectFileMask {
            get => (string) PropertyObject[xmlFlipHorizontalEachRectFileMask].Value;
            set => PropertyObject[xmlFlipHorizontalEachRectFileMask].Value = value;
        }

        public int MinLineSizeX {
            get => (int) PropertyObject[xmlMinLineSizeX].Value;
            set => PropertyObject[xmlMinLineSizeX].Value = value;
        }

        public int MinLineSizeY {
            get => (int) PropertyObject[xmlMinLineSizeY].Value;
            set => PropertyObject[xmlMinLineSizeY].Value = value;
        }

        public int DotLineXMinAddWidth {
            get => (int) PropertyObject[xmlDotLineXMinAddWidth].Value;
            set => PropertyObject[xmlDotLineXMinAddWidth].Value = value;
        }

        public int DotLineYCurMinAddHeight {
            get => (int) PropertyObject[xmlDotLineYCurMinAddHeight].Value;
            set => PropertyObject[xmlDotLineYCurMinAddHeight].Value = value;
        }

        public int DotLineYPrevMaxAddHeight {
            get => (int) PropertyObject[xmlDotLineYPrevMaxAddHeight].Value;
            set => PropertyObject[xmlDotLineYPrevMaxAddHeight].Value = value;
        }

        public int DelLineLessSize {
            get => (int) PropertyObject[xmlDelLineLessSize].Value;
            set => PropertyObject[xmlDelLineLessSize].Value = value;
        }

        public int IgnoreFirstYPixels {
            get => (int) PropertyObject[xmlIgnoreFirstYPixels].Value;
            set => PropertyObject[xmlIgnoreFirstYPixels].Value = value;
        }

        public int KillLength {
            get => (int) PropertyObject[xmlKillLength].Value;
            set => PropertyObject[xmlKillLength].Value = value;
        }

        public int PercentHorizontalPadding {
            get => (int) PropertyObject[xmlPercentHorizontalPadding].Value;
            set => PropertyObject[xmlPercentHorizontalPadding].Value = value;
        }

        public bool ShowAngleLines {
            get => (bool) PropertyObject[xmlShowAngleLines].Value;
            set => PropertyObject[xmlShowAngleLines].Value = value;
        }

        public bool ShowImageTargetFrame {
            get => (bool) PropertyObject[xmlShowImageTargetFrame].Value;
            set => PropertyObject[xmlShowImageTargetFrame].Value = value;
        }

        public string CutMarkShowOnTargetImageMask {
            get => (string) PropertyObject[xmlCutMarkShowOnTargetImageMask].Value;
            set => PropertyObject[xmlCutMarkShowOnTargetImageMask].Value = value;
        }

        public Color CutMarkColor {
            get => (Color) PropertyObject[xmlCutMarkColor].Value;
            set => PropertyObject[xmlCutMarkColor].Value = value;
        }

        public int CutMarkRadius {
            get => (int) PropertyObject[xmlCutMarkRadius].Value;
            set => PropertyObject[xmlCutMarkRadius].Value = value;
        }

        public int CutMarkThick {
            get => (int) PropertyObject[xmlCutMarkThick].Value;
            set => PropertyObject[xmlCutMarkThick].Value = value;
        }

        public bool ProcessWhenOpen {
            get => (bool) PropertyObject[xmlProcessWhenOpen].Value;
            set => PropertyObject[xmlProcessWhenOpen].Value = value;
        }

        public int CropPaddingTop {
            get => (int) PropertyObject[xmlCropPaddingTop].Value;
            set => PropertyObject[xmlCropPaddingTop].Value = value;
        }

        public int CropPaddingRight {
            get => (int) PropertyObject[xmlCropPaddingRight].Value;
            set => PropertyObject[xmlCropPaddingRight].Value = value;
        }

        public Color CropPaddingColor {
            get => (Color) PropertyObject[xmlCropPaddingColor].Value;
            set => PropertyObject[xmlCropPaddingColor].Value = value;
        }

        public string LastOpenFileName {
            get => (string) PropertyObject[xmlLastOpenFileName].Value;
            set => PropertyObject[xmlLastOpenFileName].Value = value;
        }

        public string SaveEachRectangleFileName {
            get => (string) PropertyObject[xmlSaveEachRectangleFileName].Value;
            set => PropertyObject[xmlSaveEachRectangleFileName].Value = value;
        }

        public string CustomSavePath {
            get => (string) PropertyObject[xmlCustomSavePath].Value;
            set => PropertyObject[xmlCustomSavePath].Value = value;
        }

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
            foreach (XmlNode property in element.Attributes) {
                string name = property.Name;
                if (PropertyObject.ContainProperty(name)) {
                    PropertyObject[name].StringValue = property.Value;
                }
            }
        }

        public void saveToXml(string fileName) {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<settings/>");
            XmlNode xmlRoot = xmlDoc.DocumentElement.SelectSingleNode("/settings");
            XmlElement xmlNode = ((XmlElement) xmlRoot);
            foreach (DynProperty property in PropertyObject) {
                xmlNode.SetAttribute(property.Name, property.StringValue);
            }
            xmlDoc.Save(fileName);
        }
    }
}