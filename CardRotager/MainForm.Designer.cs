
using System.Windows.Forms;

namespace CardRotager {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageImage = new System.Windows.Forms.TabPage();
            this.splitContainerImage = new System.Windows.Forms.SplitContainer();
            this.panelOriginal = new System.Windows.Forms.Panel();
            this.lbHintImageOpen = new System.Windows.Forms.Label();
            this.pbSource = new System.Windows.Forms.PictureBox();
            this.panelTarget = new System.Windows.Forms.Panel();
            this.lbHintTarget = new System.Windows.Forms.Label();
            this.pbTarget = new System.Windows.Forms.PictureBox();
            this.tabPageBW = new System.Windows.Forms.TabPage();
            this.horSplitContainer = new System.Windows.Forms.SplitContainer();
            this.panelDraft = new System.Windows.Forms.Panel();
            this.lbHintDraft = new System.Windows.Forms.Label();
            this.pbDraft = new System.Windows.Forms.PictureBox();
            this.splitPanelButtonAndLog = new System.Windows.Forms.SplitContainer();
            this.cbProcessWhenOpen = new System.Windows.Forms.CheckBox();
            this.cbShowHorVertLines = new System.Windows.Forms.CheckBox();
            this.cbShowTargetFrame = new System.Windows.Forms.CheckBox();
            this.cbShowFoundContour = new System.Windows.Forms.CheckBox();
            this.cbProcessCycleF4 = new System.Windows.Forms.CheckBox();
            this.cbConvertOpenImage = new System.Windows.Forms.CheckBox();
            this.cbLastDot = new System.Windows.Forms.CheckBox();
            this.cbShowRuler = new System.Windows.Forms.CheckBox();
            this.cbShowHelpLines = new System.Windows.Forms.CheckBox();
            this.cbRotateFoundSubImages = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbLog = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuImage = new System.Windows.Forms.ToolStripMenuItem();
            this.menuImageOpenItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuImageSaveDraftItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuImageSaveItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuImageCloseItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuImageProcessItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuImageExitItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuView = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuViewFitItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuViewScaleItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuView10PercentItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuForm = new System.Windows.Forms.ToolStripMenuItem();
            this.конверторToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.form2ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveTextForTranslateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLanguage = new System.Windows.Forms.ToolStripMenuItem();
            this.englishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.russianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.menuImageOpenButton = new System.Windows.Forms.ToolStripButton();
            this.menuImageSaveButton = new System.Windows.Forms.ToolStripButton();
            this.printToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.menuCopyButton = new System.Windows.Forms.ToolStripButton();
            this.menuPasteButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuScale10PercentButton = new System.Windows.Forms.ToolStripButton();
            this.menuProcessButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuZoomButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.menuZoom1Button = new System.Windows.Forms.ToolStripMenuItem();
            this.menuZoom5Button = new System.Windows.Forms.ToolStripMenuItem();
            this.menuZoom10Button = new System.Windows.Forms.ToolStripMenuItem();
            this.menuZoom15Button = new System.Windows.Forms.ToolStripMenuItem();
            this.menuZoom25Button = new System.Windows.Forms.ToolStripMenuItem();
            this.menuZoom50Button = new System.Windows.Forms.ToolStripMenuItem();
            this.menuZoom100Button = new System.Windows.Forms.ToolStripMenuItem();
            this.menuZoom150Button = new System.Windows.Forms.ToolStripMenuItem();
            this.menuZoom200Button = new System.Windows.Forms.ToolStripMenuItem();
            this.menuZoom300Button = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.menuZoomFitButton = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.tabControl1.SuspendLayout();
            this.tabPageImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.splitContainerImage)).BeginInit();
            this.splitContainerImage.Panel1.SuspendLayout();
            this.splitContainerImage.Panel2.SuspendLayout();
            this.splitContainerImage.SuspendLayout();
            this.panelOriginal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.pbSource)).BeginInit();
            this.panelTarget.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.pbTarget)).BeginInit();
            this.tabPageBW.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.horSplitContainer)).BeginInit();
            this.horSplitContainer.Panel1.SuspendLayout();
            this.horSplitContainer.Panel2.SuspendLayout();
            this.horSplitContainer.SuspendLayout();
            this.panelDraft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.pbDraft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.splitPanelButtonAndLog)).BeginInit();
            this.splitPanelButtonAndLog.Panel1.SuspendLayout();
            this.splitPanelButtonAndLog.Panel2.SuspendLayout();
            this.splitPanelButtonAndLog.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageImage);
            this.tabControl1.Controls.Add(this.tabPageBW);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(2742, 1574);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPageImage
            // 
            this.tabPageImage.Controls.Add(this.splitContainerImage);
            this.tabPageImage.Location = new System.Drawing.Point(4, 29);
            this.tabPageImage.Name = "tabPageImage";
            this.tabPageImage.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageImage.Size = new System.Drawing.Size(2734, 1541);
            this.tabPageImage.TabIndex = 1;
            this.tabPageImage.Text = "Изображение";
            this.tabPageImage.UseVisualStyleBackColor = true;
            // 
            // splitContainerImage
            // 
            this.splitContainerImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerImage.Location = new System.Drawing.Point(3, 3);
            this.splitContainerImage.Name = "splitContainerImage";
            // 
            // splitContainerImage.Panel1
            // 
            this.splitContainerImage.Panel1.Controls.Add(this.panelOriginal);
            // 
            // splitContainerImage.Panel2
            // 
            this.splitContainerImage.Panel2.Controls.Add(this.panelTarget);
            this.splitContainerImage.Size = new System.Drawing.Size(2728, 1535);
            this.splitContainerImage.SplitterDistance = 908;
            this.splitContainerImage.TabIndex = 1;
            // 
            // panelOriginal
            // 
            this.panelOriginal.AutoScroll = true;
            this.panelOriginal.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panelOriginal.Controls.Add(this.lbHintImageOpen);
            this.panelOriginal.Controls.Add(this.pbSource);
            this.panelOriginal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOriginal.Location = new System.Drawing.Point(0, 0);
            this.panelOriginal.Name = "panelOriginal";
            this.panelOriginal.Size = new System.Drawing.Size(908, 1535);
            this.panelOriginal.TabIndex = 1;
            // 
            // lbHintImageOpen
            // 
            this.lbHintImageOpen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbHintImageOpen.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.lbHintImageOpen.Location = new System.Drawing.Point(0, 0);
            this.lbHintImageOpen.Name = "lbHintImageOpen";
            this.lbHintImageOpen.Size = new System.Drawing.Size(908, 1535);
            this.lbHintImageOpen.TabIndex = 1;
            this.lbHintImageOpen.Text = "Открыть файл изображения с картами...\r\n(щелкните сюда)\r\n";
            this.lbHintImageOpen.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbHintImageOpen.Click += new System.EventHandler(this.lbHintFileOpen_Click);
            // 
            // pbSource
            // 
            this.pbSource.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbSource.Location = new System.Drawing.Point(3, 3);
            this.pbSource.Name = "pbSource";
            this.pbSource.Size = new System.Drawing.Size(841, 66);
            this.pbSource.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbSource.TabIndex = 0;
            this.pbSource.TabStop = false;
            // 
            // panelTarget
            // 
            this.panelTarget.AutoScroll = true;
            this.panelTarget.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panelTarget.Controls.Add(this.lbHintTarget);
            this.panelTarget.Controls.Add(this.pbTarget);
            this.panelTarget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTarget.Location = new System.Drawing.Point(0, 0);
            this.panelTarget.Name = "panelTarget";
            this.panelTarget.Size = new System.Drawing.Size(1816, 1535);
            this.panelTarget.TabIndex = 2;
            // 
            // lbHintTarget
            // 
            this.lbHintTarget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbHintTarget.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.lbHintTarget.Location = new System.Drawing.Point(0, 0);
            this.lbHintTarget.Name = "lbHintTarget";
            this.lbHintTarget.Size = new System.Drawing.Size(1816, 1535);
            this.lbHintTarget.TabIndex = 2;
            this.lbHintTarget.Text = "Сформировать файл изображения с отцентированными изображениями карт\r\n(щелкните сю" + "да)";
            this.lbHintTarget.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbHintTarget.Click += new System.EventHandler(this.lbHintProcess_Click);
            // 
            // pbTarget
            // 
            this.pbTarget.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbTarget.Location = new System.Drawing.Point(3, 3);
            this.pbTarget.Name = "pbTarget";
            this.pbTarget.Size = new System.Drawing.Size(750, 80);
            this.pbTarget.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbTarget.TabIndex = 0;
            this.pbTarget.TabStop = false;
            this.pbTarget.Click += new System.EventHandler(this.pbTarget_Click);
            // 
            // tabPageBW
            // 
            this.tabPageBW.Controls.Add(this.horSplitContainer);
            this.tabPageBW.Location = new System.Drawing.Point(4, 29);
            this.tabPageBW.Name = "tabPageBW";
            this.tabPageBW.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBW.Size = new System.Drawing.Size(2734, 1541);
            this.tabPageBW.TabIndex = 0;
            this.tabPageBW.Text = "Черновик (черно-белый)";
            this.tabPageBW.UseVisualStyleBackColor = true;
            // 
            // horSplitContainer
            // 
            this.horSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.horSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.horSplitContainer.Location = new System.Drawing.Point(3, 3);
            this.horSplitContainer.Name = "horSplitContainer";
            // 
            // horSplitContainer.Panel1
            // 
            this.horSplitContainer.Panel1.Controls.Add(this.panelDraft);
            // 
            // horSplitContainer.Panel2
            // 
            this.horSplitContainer.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.horSplitContainer.Panel2.Controls.Add(this.splitPanelButtonAndLog);
            this.horSplitContainer.Size = new System.Drawing.Size(2728, 1535);
            this.horSplitContainer.SplitterDistance = 2102;
            this.horSplitContainer.TabIndex = 2;
            // 
            // panelDraft
            // 
            this.panelDraft.AutoScroll = true;
            this.panelDraft.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelDraft.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panelDraft.Controls.Add(this.lbHintDraft);
            this.panelDraft.Controls.Add(this.pbDraft);
            this.panelDraft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDraft.Location = new System.Drawing.Point(0, 0);
            this.panelDraft.Name = "panelDraft";
            this.panelDraft.Size = new System.Drawing.Size(2102, 1535);
            this.panelDraft.TabIndex = 1;
            // 
            // lbHintDraft
            // 
            this.lbHintDraft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbHintDraft.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.lbHintDraft.Location = new System.Drawing.Point(0, 0);
            this.lbHintDraft.Name = "lbHintDraft";
            this.lbHintDraft.Size = new System.Drawing.Size(2102, 1535);
            this.lbHintDraft.TabIndex = 2;
            this.lbHintDraft.Text = "Открыть файл изображения с картами...\r\n(щелкните сюда)\r\n";
            this.lbHintDraft.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbHintDraft.Click += new System.EventHandler(this.lbHintImageOpen2_Click);
            // 
            // pbDraft
            // 
            this.pbDraft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbDraft.Location = new System.Drawing.Point(0, 0);
            this.pbDraft.Name = "pbDraft";
            this.pbDraft.Size = new System.Drawing.Size(770, 56);
            this.pbDraft.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbDraft.TabIndex = 0;
            this.pbDraft.TabStop = false;
            // 
            // splitPanelButtonAndLog
            // 
            this.splitPanelButtonAndLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitPanelButtonAndLog.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitPanelButtonAndLog.Location = new System.Drawing.Point(0, 0);
            this.splitPanelButtonAndLog.Name = "splitPanelButtonAndLog";
            this.splitPanelButtonAndLog.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitPanelButtonAndLog.Panel1
            // 
            this.splitPanelButtonAndLog.Panel1.Controls.Add(this.cbProcessWhenOpen);
            this.splitPanelButtonAndLog.Panel1.Controls.Add(this.cbShowHorVertLines);
            this.splitPanelButtonAndLog.Panel1.Controls.Add(this.cbShowTargetFrame);
            this.splitPanelButtonAndLog.Panel1.Controls.Add(this.cbShowFoundContour);
            this.splitPanelButtonAndLog.Panel1.Controls.Add(this.cbProcessCycleF4);
            this.splitPanelButtonAndLog.Panel1.Controls.Add(this.cbConvertOpenImage);
            this.splitPanelButtonAndLog.Panel1.Controls.Add(this.cbLastDot);
            this.splitPanelButtonAndLog.Panel1.Controls.Add(this.cbShowRuler);
            this.splitPanelButtonAndLog.Panel1.Controls.Add(this.cbShowHelpLines);
            this.splitPanelButtonAndLog.Panel1.Controls.Add(this.cbRotateFoundSubImages);
            this.splitPanelButtonAndLog.Panel1.Controls.Add(this.textBox1);
            this.splitPanelButtonAndLog.Panel1.Controls.Add(this.label2);
            this.splitPanelButtonAndLog.Panel1.Controls.Add(this.label1);
            // 
            // splitPanelButtonAndLog.Panel2
            // 
            this.splitPanelButtonAndLog.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitPanelButtonAndLog.Panel2.Controls.Add(this.tbLog);
            this.splitPanelButtonAndLog.Size = new System.Drawing.Size(622, 1535);
            this.splitPanelButtonAndLog.SplitterDistance = 306;
            this.splitPanelButtonAndLog.TabIndex = 14;
            // 
            // cbProcessWhenOpen
            // 
            this.cbProcessWhenOpen.AutoSize = true;
            this.cbProcessWhenOpen.Checked = true;
            this.cbProcessWhenOpen.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbProcessWhenOpen.Location = new System.Drawing.Point(7, 239);
            this.cbProcessWhenOpen.Name = "cbProcessWhenOpen";
            this.cbProcessWhenOpen.Size = new System.Drawing.Size(281, 24);
            this.cbProcessWhenOpen.TabIndex = 12;
            this.cbProcessWhenOpen.Text = "При открытии сразу обработать";
            this.cbProcessWhenOpen.UseVisualStyleBackColor = true;
            // 
            // cbShowHorVertLines
            // 
            this.cbShowHorVertLines.AutoSize = true;
            this.cbShowHorVertLines.Checked = true;
            this.cbShowHorVertLines.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowHorVertLines.Location = new System.Drawing.Point(7, 93);
            this.cbShowHorVertLines.Name = "cbShowHorVertLines";
            this.cbShowHorVertLines.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbShowHorVertLines.Size = new System.Drawing.Size(275, 24);
            this.cbShowHorVertLines.TabIndex = 11;
            this.cbShowHorVertLines.Text = "Отображение гори./верт. линий";
            this.cbShowHorVertLines.UseVisualStyleBackColor = true;
            // 
            // cbShowTargetFrame
            // 
            this.cbShowTargetFrame.AutoSize = true;
            this.cbShowTargetFrame.Checked = true;
            this.cbShowTargetFrame.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowTargetFrame.Location = new System.Drawing.Point(7, 213);
            this.cbShowTargetFrame.Name = "cbShowTargetFrame";
            this.cbShowTargetFrame.Size = new System.Drawing.Size(386, 24);
            this.cbShowTargetFrame.TabIndex = 10;
            this.cbShowTargetFrame.Text = "Показывать области куда помещаются карты";
            this.cbShowTargetFrame.UseVisualStyleBackColor = true;
            // 
            // cbShowFoundContour
            // 
            this.cbShowFoundContour.AutoSize = true;
            this.cbShowFoundContour.Checked = true;
            this.cbShowFoundContour.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowFoundContour.Location = new System.Drawing.Point(7, 183);
            this.cbShowFoundContour.Name = "cbShowFoundContour";
            this.cbShowFoundContour.Size = new System.Drawing.Size(322, 24);
            this.cbShowFoundContour.TabIndex = 9;
            this.cbShowFoundContour.Text = "Показывать найденные контуры карт";
            this.cbShowFoundContour.UseVisualStyleBackColor = true;
            // 
            // cbProcessCycleF4
            // 
            this.cbProcessCycleF4.AutoSize = true;
            this.cbProcessCycleF4.Checked = true;
            this.cbProcessCycleF4.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbProcessCycleF4.Location = new System.Drawing.Point(415, 38);
            this.cbProcessCycleF4.Name = "cbProcessCycleF4";
            this.cbProcessCycleF4.Size = new System.Drawing.Size(202, 24);
            this.cbProcessCycleF4.TabIndex = 8;
            this.cbProcessCycleF4.Text = "F4 действует по кругу";
            this.cbProcessCycleF4.UseVisualStyleBackColor = true;
            // 
            // cbConvertOpenImage
            // 
            this.cbConvertOpenImage.AutoSize = true;
            this.cbConvertOpenImage.Checked = true;
            this.cbConvertOpenImage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbConvertOpenImage.Location = new System.Drawing.Point(7, 269);
            this.cbConvertOpenImage.Name = "cbConvertOpenImage";
            this.cbConvertOpenImage.Size = new System.Drawing.Size(447, 24);
            this.cbConvertOpenImage.TabIndex = 7;
            this.cbConvertOpenImage.Text = "Конвертировать открываемую картинку в Серый цвет";
            this.cbConvertOpenImage.UseVisualStyleBackColor = true;
            // 
            // cbLastDot
            // 
            this.cbLastDot.AutoSize = true;
            this.cbLastDot.Checked = true;
            this.cbLastDot.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbLastDot.Location = new System.Drawing.Point(7, 153);
            this.cbLastDot.Name = "cbLastDot";
            this.cbLastDot.Size = new System.Drawing.Size(384, 24);
            this.cbLastDot.TabIndex = 6;
            this.cbLastDot.Text = "Последние список точек для создания линий";
            this.cbLastDot.UseVisualStyleBackColor = true;
            // 
            // cbShowRuler
            // 
            this.cbShowRuler.AutoSize = true;
            this.cbShowRuler.Checked = true;
            this.cbShowRuler.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowRuler.Location = new System.Drawing.Point(7, 123);
            this.cbShowRuler.Name = "cbShowRuler";
            this.cbShowRuler.Size = new System.Drawing.Size(304, 24);
            this.cbShowRuler.TabIndex = 5;
            this.cbShowRuler.Text = "Отображение линии-текст линейки";
            this.cbShowRuler.UseVisualStyleBackColor = true;
            // 
            // cbShowHelpLines
            // 
            this.cbShowHelpLines.AutoSize = true;
            this.cbShowHelpLines.Checked = true;
            this.cbShowHelpLines.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowHelpLines.Location = new System.Drawing.Point(7, 67);
            this.cbShowHelpLines.Name = "cbShowHelpLines";
            this.cbShowHelpLines.Size = new System.Drawing.Size(351, 24);
            this.cbShowHelpLines.TabIndex = 4;
            this.cbShowHelpLines.Text = "Отображение линий контуров и разметки";
            this.cbShowHelpLines.UseVisualStyleBackColor = true;
            // 
            // cbRotateFoundSubImages
            // 
            this.cbRotateFoundSubImages.AutoSize = true;
            this.cbRotateFoundSubImages.Location = new System.Drawing.Point(7, 38);
            this.cbRotateFoundSubImages.Name = "cbRotateFoundSubImages";
            this.cbRotateFoundSubImages.Size = new System.Drawing.Size(343, 24);
            this.cbRotateFoundSubImages.TabIndex = 3;
            this.cbRotateFoundSubImages.Text = "Вращение найденных изображений карт";
            this.cbRotateFoundSubImages.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(106, 6);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 26);
            this.textBox1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "(масштаб)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.label1.Location = new System.Drawing.Point(352, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(245, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Процесс обработки:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbLog
            // 
            this.tbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLog.Font = new System.Drawing.Font("OpenGost Type A TT", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.tbLog.Location = new System.Drawing.Point(0, 0);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbLog.Size = new System.Drawing.Size(622, 1225);
            this.tbLog.TabIndex = 8;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.menuImage, this.MenuView, this.MenuForm, this.menuLanguage});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(2742, 33);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuImage
            // 
            this.menuImage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.menuImageOpenItem, this.menuImageSaveDraftItem, this.menuImageSaveItem, this.menuImageCloseItem, this.toolStripMenuItem1, this.menuImageProcessItem, this.toolStripMenuItem2, this.menuImageExitItem});
            this.menuImage.Name = "menuImage";
            this.menuImage.Size = new System.Drawing.Size(137, 29);
            this.menuImage.Text = "&Изображение";
            // 
            // menuImageOpenItem
            // 
            this.menuImageOpenItem.Name = "menuImageOpenItem";
            this.menuImageOpenItem.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.menuImageOpenItem.Size = new System.Drawing.Size(362, 30);
            this.menuImageOpenItem.Text = "&Открыть...";
            this.menuImageOpenItem.Click += new System.EventHandler(this.menuImageOpenItem_Click);
            // 
            // menuImageSaveDraftItem
            // 
            this.menuImageSaveDraftItem.Name = "menuImageSaveDraftItem";
            this.menuImageSaveDraftItem.ShortcutKeys = ((System.Windows.Forms.Keys) (((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) | System.Windows.Forms.Keys.S)));
            this.menuImageSaveDraftItem.Size = new System.Drawing.Size(362, 30);
            this.menuImageSaveDraftItem.Text = "&Сохранить черновик...";
            this.menuImageSaveDraftItem.Click += new System.EventHandler(this.menuImageSaveDraftItem_Click);
            // 
            // menuImageSaveItem
            // 
            this.menuImageSaveItem.Name = "menuImageSaveItem";
            this.menuImageSaveItem.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.menuImageSaveItem.Size = new System.Drawing.Size(362, 30);
            this.menuImageSaveItem.Text = "&Сохранить результат...";
            this.menuImageSaveItem.Click += new System.EventHandler(this.menuImageSaveItem_Click);
            // 
            // menuImageCloseItem
            // 
            this.menuImageCloseItem.Name = "menuImageCloseItem";
            this.menuImageCloseItem.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.menuImageCloseItem.Size = new System.Drawing.Size(362, 30);
            this.menuImageCloseItem.Text = "Закрыть";
            this.menuImageCloseItem.Click += new System.EventHandler(this.menuImageCloseItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(359, 6);
            // 
            // menuImageProcessItem
            // 
            this.menuImageProcessItem.Name = "menuImageProcessItem";
            this.menuImageProcessItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.menuImageProcessItem.Size = new System.Drawing.Size(362, 30);
            this.menuImageProcessItem.Text = "Обр&аботка";
            this.menuImageProcessItem.Click += new System.EventHandler(this.menuImageProcessItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(359, 6);
            // 
            // menuImageExitItem
            // 
            this.menuImageExitItem.Name = "menuImageExitItem";
            this.menuImageExitItem.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.X)));
            this.menuImageExitItem.Size = new System.Drawing.Size(362, 30);
            this.menuImageExitItem.Text = "В&ыход";
            // 
            // MenuView
            // 
            this.MenuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.MenuViewFitItem, this.MenuViewScaleItem, this.toolStripMenuItem3, this.MenuView10PercentItem});
            this.MenuView.Name = "MenuView";
            this.MenuView.Size = new System.Drawing.Size(54, 29);
            this.MenuView.Text = "&Вид";
            // 
            // MenuViewFitItem
            // 
            this.MenuViewFitItem.Name = "MenuViewFitItem";
            this.MenuViewFitItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.MenuViewFitItem.Size = new System.Drawing.Size(255, 30);
            this.MenuViewFitItem.Text = "&Заполнить";
            this.MenuViewFitItem.Click += new System.EventHandler(this.MenuViewFitItem_Click);
            // 
            // MenuViewScaleItem
            // 
            this.MenuViewScaleItem.Name = "MenuViewScaleItem";
            this.MenuViewScaleItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.MenuViewScaleItem.Size = new System.Drawing.Size(255, 30);
            this.MenuViewScaleItem.Text = "Масштабировать";
            this.MenuViewScaleItem.Click += new System.EventHandler(this.MenuViewScaleItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(252, 6);
            // 
            // MenuView10PercentItem
            // 
            this.MenuView10PercentItem.Name = "MenuView10PercentItem";
            this.MenuView10PercentItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.MenuView10PercentItem.Size = new System.Drawing.Size(255, 30);
            this.MenuView10PercentItem.Text = "10%";
            this.MenuView10PercentItem.Click += new System.EventHandler(this.menuItem10Percent_Click);
            // 
            // MenuForm
            // 
            this.MenuForm.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.конверторToolStripMenuItem, this.form2ToolStripMenuItem1, this.saveTextForTranslateToolStripMenuItem});
            this.MenuForm.Name = "MenuForm";
            this.MenuForm.Size = new System.Drawing.Size(85, 29);
            this.MenuForm.Text = "Формы";
            // 
            // конверторToolStripMenuItem
            // 
            this.конверторToolStripMenuItem.Name = "конверторToolStripMenuItem";
            this.конверторToolStripMenuItem.Size = new System.Drawing.Size(255, 30);
            this.конверторToolStripMenuItem.Text = "Конвертор";
            this.конверторToolStripMenuItem.Click += new System.EventHandler(this.form2ToolStripMenuItem_Click);
            // 
            // form2ToolStripMenuItem1
            // 
            this.form2ToolStripMenuItem1.Name = "form2ToolStripMenuItem1";
            this.form2ToolStripMenuItem1.Size = new System.Drawing.Size(255, 30);
            this.form2ToolStripMenuItem1.Text = "Form2";
            this.form2ToolStripMenuItem1.Click += new System.EventHandler(this.form2ToolStripMenuItem1_Click);
            // 
            // saveTextForTranslateToolStripMenuItem
            // 
            this.saveTextForTranslateToolStripMenuItem.Name = "saveTextForTranslateToolStripMenuItem";
            this.saveTextForTranslateToolStripMenuItem.Size = new System.Drawing.Size(255, 30);
            this.saveTextForTranslateToolStripMenuItem.Text = "Save text for translate";
            this.saveTextForTranslateToolStripMenuItem.Click += new System.EventHandler(this.saveTextForTranslateToolStripMenuItem_Click);
            // 
            // menuLanguage
            // 
            this.menuLanguage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.englishToolStripMenuItem, this.russianToolStripMenuItem});
            this.menuLanguage.Name = "menuLanguage";
            this.menuLanguage.Size = new System.Drawing.Size(157, 29);
            this.menuLanguage.Text = "Язык (Language)";
            // 
            // englishToolStripMenuItem
            // 
            this.englishToolStripMenuItem.Name = "englishToolStripMenuItem";
            this.englishToolStripMenuItem.Size = new System.Drawing.Size(250, 30);
            this.englishToolStripMenuItem.Text = "Английский (English)";
            this.englishToolStripMenuItem.Click += new System.EventHandler(this.englishToolStripMenuItem_Click);
            // 
            // russianToolStripMenuItem
            // 
            this.russianToolStripMenuItem.Name = "russianToolStripMenuItem";
            this.russianToolStripMenuItem.Size = new System.Drawing.Size(250, 30);
            this.russianToolStripMenuItem.Text = "Русский (Russian)";
            this.russianToolStripMenuItem.Click += new System.EventHandler(this.russianToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(60, 60);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.menuImageOpenButton, this.menuImageSaveButton, this.printToolStripButton, this.toolStripSeparator, this.menuCopyButton, this.menuPasteButton, this.toolStripSeparator1, this.menuScale10PercentButton, this.menuProcessButton, this.toolStripSeparator2, this.menuZoomButton});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(586, 67);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // menuImageOpenButton
            // 
            this.menuImageOpenButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.menuImageOpenButton.Image = ((System.Drawing.Image) (resources.GetObject("menuImageOpenButton.Image")));
            this.menuImageOpenButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuImageOpenButton.Name = "menuImageOpenButton";
            this.menuImageOpenButton.Size = new System.Drawing.Size(64, 64);
            this.menuImageOpenButton.Text = "&Открыть";
            this.menuImageOpenButton.Click += new System.EventHandler(this.menuImageOpenItem_Click);
            // 
            // menuImageSaveButton
            // 
            this.menuImageSaveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.menuImageSaveButton.Image = ((System.Drawing.Image) (resources.GetObject("menuImageSaveButton.Image")));
            this.menuImageSaveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuImageSaveButton.Name = "menuImageSaveButton";
            this.menuImageSaveButton.Size = new System.Drawing.Size(64, 64);
            this.menuImageSaveButton.Text = "&Сохранить";
            this.menuImageSaveButton.Click += new System.EventHandler(this.menuImageSaveItem_Click);
            // 
            // printToolStripButton
            // 
            this.printToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.printToolStripButton.Image = ((System.Drawing.Image) (resources.GetObject("printToolStripButton.Image")));
            this.printToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printToolStripButton.Name = "printToolStripButton";
            this.printToolStripButton.Size = new System.Drawing.Size(64, 64);
            this.printToolStripButton.Text = "&Print";
            this.printToolStripButton.Visible = false;
            this.printToolStripButton.Click += new System.EventHandler(this.buttonProcess_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 67);
            // 
            // menuCopyButton
            // 
            this.menuCopyButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.menuCopyButton.Image = ((System.Drawing.Image) (resources.GetObject("menuCopyButton.Image")));
            this.menuCopyButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuCopyButton.Name = "menuCopyButton";
            this.menuCopyButton.Size = new System.Drawing.Size(64, 64);
            this.menuCopyButton.Text = "&Copy";
            this.menuCopyButton.Click += new System.EventHandler(this.copyToolStripButton_Click);
            // 
            // menuPasteButton
            // 
            this.menuPasteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.menuPasteButton.Image = ((System.Drawing.Image) (resources.GetObject("menuPasteButton.Image")));
            this.menuPasteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuPasteButton.Name = "menuPasteButton";
            this.menuPasteButton.Size = new System.Drawing.Size(64, 64);
            this.menuPasteButton.Text = "&Paste";
            this.menuPasteButton.Click += new System.EventHandler(this.menuPasteButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 67);
            // 
            // menuScale10PercentButton
            // 
            this.menuScale10PercentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.menuScale10PercentButton.Image = ((System.Drawing.Image) (resources.GetObject("menuScale10PercentButton.Image")));
            this.menuScale10PercentButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuScale10PercentButton.Name = "menuScale10PercentButton";
            this.menuScale10PercentButton.Size = new System.Drawing.Size(64, 64);
            this.menuScale10PercentButton.Text = "Масштаб 10%";
            this.menuScale10PercentButton.Click += new System.EventHandler(this.menuItem10Percent_Click);
            // 
            // menuProcessButton
            // 
            this.menuProcessButton.Image = ((System.Drawing.Image) (resources.GetObject("menuProcessButton.Image")));
            this.menuProcessButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuProcessButton.Name = "menuProcessButton";
            this.menuProcessButton.Size = new System.Drawing.Size(166, 64);
            this.menuProcessButton.Text = "&Обработка";
            this.menuProcessButton.Click += new System.EventHandler(this.buttonProcess_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 67);
            // 
            // menuZoomButton
            // 
            this.menuZoomButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menuZoomButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.menuZoom1Button, this.menuZoom5Button, this.menuZoom10Button, this.menuZoom15Button, this.menuZoom25Button, this.menuZoom50Button, this.menuZoom100Button, this.menuZoom150Button, this.menuZoom200Button, this.menuZoom300Button, this.toolStripMenuItem4, this.menuZoomFitButton});
            this.menuZoomButton.Image = ((System.Drawing.Image) (resources.GetObject("menuZoomButton.Image")));
            this.menuZoomButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuZoomButton.Name = "menuZoomButton";
            this.menuZoomButton.Size = new System.Drawing.Size(70, 64);
            this.menuZoomButton.Text = "100%";
            this.menuZoomButton.ToolTipText = "Масштаб";
            // 
            // menuZoom1Button
            // 
            this.menuZoom1Button.Name = "menuZoom1Button";
            this.menuZoom1Button.Size = new System.Drawing.Size(170, 30);
            this.menuZoom1Button.Text = "1%";
            this.menuZoom1Button.Click += new System.EventHandler(this.menuZoom1Button_Click);
            // 
            // menuZoom5Button
            // 
            this.menuZoom5Button.Name = "menuZoom5Button";
            this.menuZoom5Button.Size = new System.Drawing.Size(170, 30);
            this.menuZoom5Button.Text = "5%";
            this.menuZoom5Button.Click += new System.EventHandler(this.menuZoom5Button_Click);
            // 
            // menuZoom10Button
            // 
            this.menuZoom10Button.Name = "menuZoom10Button";
            this.menuZoom10Button.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.menuZoom10Button.Size = new System.Drawing.Size(170, 30);
            this.menuZoom10Button.Text = "10%";
            this.menuZoom10Button.Click += new System.EventHandler(this.menuItem10Percent_Click);
            // 
            // menuZoom15Button
            // 
            this.menuZoom15Button.Name = "menuZoom15Button";
            this.menuZoom15Button.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.menuZoom15Button.Size = new System.Drawing.Size(170, 30);
            this.menuZoom15Button.Text = "15%";
            this.menuZoom15Button.Click += new System.EventHandler(this.menuZoom15Button_Click);
            // 
            // menuZoom25Button
            // 
            this.menuZoom25Button.Name = "menuZoom25Button";
            this.menuZoom25Button.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.menuZoom25Button.Size = new System.Drawing.Size(170, 30);
            this.menuZoom25Button.Text = "25%";
            this.menuZoom25Button.Click += new System.EventHandler(this.menuZoom25Button_Click);
            // 
            // menuZoom50Button
            // 
            this.menuZoom50Button.Name = "menuZoom50Button";
            this.menuZoom50Button.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.menuZoom50Button.Size = new System.Drawing.Size(170, 30);
            this.menuZoom50Button.Text = "50%";
            this.menuZoom50Button.Click += new System.EventHandler(this.menuZoom50Button_Click);
            // 
            // menuZoom100Button
            // 
            this.menuZoom100Button.Name = "menuZoom100Button";
            this.menuZoom100Button.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.menuZoom100Button.Size = new System.Drawing.Size(170, 30);
            this.menuZoom100Button.Text = "100%";
            this.menuZoom100Button.Click += new System.EventHandler(this.menuZoom100Button_Click);
            // 
            // menuZoom150Button
            // 
            this.menuZoom150Button.Name = "menuZoom150Button";
            this.menuZoom150Button.ShortcutKeys = System.Windows.Forms.Keys.F10;
            this.menuZoom150Button.Size = new System.Drawing.Size(170, 30);
            this.menuZoom150Button.Text = "150%";
            this.menuZoom150Button.Click += new System.EventHandler(this.menuZoom150Button_Click);
            // 
            // menuZoom200Button
            // 
            this.menuZoom200Button.Name = "menuZoom200Button";
            this.menuZoom200Button.ShortcutKeys = System.Windows.Forms.Keys.F11;
            this.menuZoom200Button.Size = new System.Drawing.Size(170, 30);
            this.menuZoom200Button.Text = "200%";
            this.menuZoom200Button.Click += new System.EventHandler(this.menuZoom200Button_Click);
            // 
            // menuZoom300Button
            // 
            this.menuZoom300Button.Name = "menuZoom300Button";
            this.menuZoom300Button.ShortcutKeys = System.Windows.Forms.Keys.F12;
            this.menuZoom300Button.Size = new System.Drawing.Size(170, 30);
            this.menuZoom300Button.Text = "300%";
            this.menuZoom300Button.Click += new System.EventHandler(this.menuZoom300Button_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(167, 6);
            // 
            // menuZoomFitButton
            // 
            this.menuZoomFitButton.Name = "menuZoomFitButton";
            this.menuZoomFitButton.Size = new System.Drawing.Size(170, 30);
            this.menuZoomFitButton.Text = "Заполнить";
            this.menuZoomFitButton.Click += new System.EventHandler(this.menuZoomFitButton_Click);
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.tabControl1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(2742, 1574);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 33);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(2742, 1641);
            this.toolStripContainer1.TabIndex = 5;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2742, 1674);
            this.Controls.Add(this.toolStripContainer1);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Переразмещение карт в скане графического файла в центрах указанной сетки";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.mainForm_FormClosed);
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPageImage.ResumeLayout(false);
            this.splitContainerImage.Panel1.ResumeLayout(false);
            this.splitContainerImage.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize) (this.splitContainerImage)).EndInit();
            this.splitContainerImage.ResumeLayout(false);
            this.panelOriginal.ResumeLayout(false);
            this.panelOriginal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize) (this.pbSource)).EndInit();
            this.panelTarget.ResumeLayout(false);
            this.panelTarget.PerformLayout();
            ((System.ComponentModel.ISupportInitialize) (this.pbTarget)).EndInit();
            this.tabPageBW.ResumeLayout(false);
            this.horSplitContainer.Panel1.ResumeLayout(false);
            this.horSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize) (this.horSplitContainer)).EndInit();
            this.horSplitContainer.ResumeLayout(false);
            this.panelDraft.ResumeLayout(false);
            this.panelDraft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize) (this.pbDraft)).EndInit();
            this.splitPanelButtonAndLog.Panel1.ResumeLayout(false);
            this.splitPanelButtonAndLog.Panel1.PerformLayout();
            this.splitPanelButtonAndLog.Panel2.ResumeLayout(false);
            this.splitPanelButtonAndLog.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize) (this.splitPanelButtonAndLog)).EndInit();
            this.splitPanelButtonAndLog.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.CheckBox cbProcessWhenOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem menuZoom15Button;
        public System.Windows.Forms.Label lbHintDraft;
        public System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem menuZoom25Button;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem menuZoomFitButton;
        private System.Windows.Forms.ToolStripMenuItem menuZoom5Button;
        private System.Windows.Forms.ToolStripMenuItem menuZoom150Button;
        private System.Windows.Forms.ToolStripMenuItem menuZoom1Button;
        private System.Windows.Forms.ToolStripMenuItem menuZoom300Button;
        private System.Windows.Forms.ToolStripMenuItem menuZoom100Button;
        private System.Windows.Forms.ToolStripMenuItem menuZoom50Button;
        private System.Windows.Forms.ToolStripMenuItem menuZoom200Button;
        private System.Windows.Forms.ToolStripMenuItem menuZoom10Button;
        private System.Windows.Forms.ToolStripDropDownButton menuZoomButton;
        public System.Windows.Forms.CheckBox cbShowHorVertLines;
        private System.Windows.Forms.CheckBox cbShowTargetFrame;
        private System.Windows.Forms.CheckBox cbShowFoundContour;
        private System.Windows.Forms.CheckBox cbProcessCycleF4;
        private System.Windows.Forms.CheckBox cbConvertOpenImage;

        private System.Windows.Forms.SplitContainer horSplitContainer;
        private System.Windows.Forms.Panel panelDraft;
        private System.Windows.Forms.TabControl tabControl1;
        public System.Windows.Forms.TabPage tabPageBW;
        public System.Windows.Forms.TabPage tabPageImage;
        private System.Windows.Forms.PictureBox pbSource;
        private System.Windows.Forms.Panel panelOriginal;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.SplitContainer splitPanelButtonAndLog;
        private System.Windows.Forms.TextBox tbLog;
        public System.Windows.Forms.PictureBox pbDraft;
        private System.Windows.Forms.ToolStrip toolStrip1;
        public System.Windows.Forms.ToolStripButton menuProcessButton;
        public System.Windows.Forms.ToolStripButton menuImageOpenButton;
        public System.Windows.Forms.ToolStripButton menuImageSaveButton;
        private System.Windows.Forms.ToolStripButton printToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton menuCopyButton;
        private System.Windows.Forms.ToolStripButton menuPasteButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton menuScale10PercentButton;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        public System.Windows.Forms.ToolStripMenuItem menuImage;
        public System.Windows.Forms.ToolStripMenuItem MenuView;
        public System.Windows.Forms.ToolStripMenuItem MenuView10PercentItem;
        public System.Windows.Forms.ToolStripMenuItem MenuForm;
        public System.Windows.Forms.ToolStripMenuItem конверторToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem form2ToolStripMenuItem1;
        private System.Windows.Forms.Panel panelTarget;
        private System.Windows.Forms.PictureBox pbTarget;
        public System.Windows.Forms.ToolStripMenuItem MenuViewFitItem;
        public System.Windows.Forms.ToolStripMenuItem MenuViewScaleItem;
        private System.Windows.Forms.SplitContainer splitContainerImage;
        public System.Windows.Forms.Label lbHintImageOpen;
        public System.Windows.Forms.Label lbHintTarget;
        public System.Windows.Forms.ToolStripMenuItem menuImageOpenItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        public System.Windows.Forms.ToolStripMenuItem menuImageExitItem;
        public System.Windows.Forms.ToolStripMenuItem menuImageProcessItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        public System.Windows.Forms.ToolStripMenuItem menuImageCloseItem;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem saveTextForTranslateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuLanguage;
        private System.Windows.Forms.ToolStripMenuItem englishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem russianToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem menuImageSaveItem;
        public System.Windows.Forms.ToolStripMenuItem menuImageSaveDraftItem;
        public System.Windows.Forms.CheckBox cbRotateFoundSubImages;
        public System.Windows.Forms.CheckBox cbShowHelpLines;
        private System.Windows.Forms.CheckBox cbShowRuler;
        private System.Windows.Forms.CheckBox cbLastDot;
        
    }
}

