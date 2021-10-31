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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageImage = new System.Windows.Forms.TabPage();
            this.splitContainerImage = new System.Windows.Forms.SplitContainer();
            this.panelOriginal = new System.Windows.Forms.Panel();
            this.lbHintSource = new System.Windows.Forms.Label();
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
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.contextMenuPropertyGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuContextResetItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tbLog = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuImage = new System.Windows.Forms.ToolStripMenuItem();
            this.menuImageOpenItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuImageSaveDraftItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuImageQuickSaveAsItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuImageSaveAsItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusBarCaptionLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusBarProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.statusBarInfo = new System.Windows.Forms.ToolStripStatusLabel();
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
            this.contextMenuPropertyGrid.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
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
            this.tabControl1.Size = new System.Drawing.Size(2742, 1544);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPageImage
            // 
            this.tabPageImage.Controls.Add(this.splitContainerImage);
            this.tabPageImage.Location = new System.Drawing.Point(4, 29);
            this.tabPageImage.Name = "tabPageImage";
            this.tabPageImage.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageImage.Size = new System.Drawing.Size(2734, 1511);
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
            this.splitContainerImage.Size = new System.Drawing.Size(2728, 1505);
            this.splitContainerImage.SplitterDistance = 908;
            this.splitContainerImage.TabIndex = 1;
            // 
            // panelOriginal
            // 
            this.panelOriginal.AutoScroll = true;
            this.panelOriginal.AutoSize = true;
            this.panelOriginal.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panelOriginal.Controls.Add(this.lbHintSource);
            this.panelOriginal.Controls.Add(this.pbSource);
            this.panelOriginal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOriginal.Location = new System.Drawing.Point(0, 0);
            this.panelOriginal.Name = "panelOriginal";
            this.panelOriginal.Size = new System.Drawing.Size(908, 1505);
            this.panelOriginal.TabIndex = 1;
            // 
            // lbHintSource
            // 
            this.lbHintSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbHintSource.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.lbHintSource.Location = new System.Drawing.Point(0, 0);
            this.lbHintSource.Name = "lbHintSource";
            this.lbHintSource.Size = new System.Drawing.Size(908, 1505);
            this.lbHintSource.TabIndex = 1;
            this.lbHintSource.Text = "Открыть файл изображения с картами (сейчас только 4 строки и 2 колонки)...\r\n(щелк" + "ните сюда)\r\n";
            this.lbHintSource.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbHintSource.Click += new System.EventHandler(this.lbHintFileOpen_Click);
            // 
            // pbSource
            // 
            this.pbSource.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbSource.Location = new System.Drawing.Point(0, 0);
            this.pbSource.Name = "pbSource";
            this.pbSource.Size = new System.Drawing.Size(908, 1505);
            this.pbSource.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbSource.TabIndex = 0;
            this.pbSource.TabStop = false;
            this.pbSource.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbSource_Click);
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
            this.panelTarget.Size = new System.Drawing.Size(1816, 1505);
            this.panelTarget.TabIndex = 2;
            // 
            // lbHintTarget
            // 
            this.lbHintTarget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbHintTarget.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.lbHintTarget.Location = new System.Drawing.Point(0, 0);
            this.lbHintTarget.Name = "lbHintTarget";
            this.lbHintTarget.Size = new System.Drawing.Size(1816, 1505);
            this.lbHintTarget.TabIndex = 2;
            this.lbHintTarget.Text = "Сформировать файл изображения с отцентированными изображениями карт\r\n(щелкните сю" + "да)";
            this.lbHintTarget.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbHintTarget.Click += new System.EventHandler(this.lbHintProcess_Click);
            // 
            // pbTarget
            // 
            this.pbTarget.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbTarget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbTarget.Location = new System.Drawing.Point(0, 0);
            this.pbTarget.Name = "pbTarget";
            this.pbTarget.Size = new System.Drawing.Size(1816, 1505);
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
            this.tabPageBW.Size = new System.Drawing.Size(2734, 1511);
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
            this.horSplitContainer.Size = new System.Drawing.Size(2728, 1505);
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
            this.panelDraft.Size = new System.Drawing.Size(2102, 1505);
            this.panelDraft.TabIndex = 1;
            // 
            // lbHintDraft
            // 
            this.lbHintDraft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbHintDraft.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.lbHintDraft.Location = new System.Drawing.Point(0, 0);
            this.lbHintDraft.Name = "lbHintDraft";
            this.lbHintDraft.Size = new System.Drawing.Size(2102, 1505);
            this.lbHintDraft.TabIndex = 2;
            this.lbHintDraft.Text = "Открыть файл изображения с картами (сейчас только 4 строки и 2 колонки)...\r\n(щелк" + "ните сюда)\r\n";
            this.lbHintDraft.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbHintDraft.Click += new System.EventHandler(this.lbHintImageOpen2_Click);
            // 
            // pbDraft
            // 
            this.pbDraft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbDraft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbDraft.Location = new System.Drawing.Point(0, 0);
            this.pbDraft.Name = "pbDraft";
            this.pbDraft.Size = new System.Drawing.Size(2102, 1505);
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
            this.splitPanelButtonAndLog.Panel1.Controls.Add(this.propertyGrid1);
            // 
            // splitPanelButtonAndLog.Panel2
            // 
            this.splitPanelButtonAndLog.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitPanelButtonAndLog.Panel2.Controls.Add(this.tbLog);
            this.splitPanelButtonAndLog.Size = new System.Drawing.Size(622, 1505);
            this.splitPanelButtonAndLog.SplitterDistance = 306;
            this.splitPanelButtonAndLog.TabIndex = 14;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.ContextMenuStrip = this.contextMenuPropertyGrid;
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(622, 306);
            this.propertyGrid1.TabIndex = 0;
            // 
            // contextMenuPropertyGrid
            // 
            this.contextMenuPropertyGrid.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuPropertyGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.menuContextResetItem});
            this.contextMenuPropertyGrid.Name = "contextMenuStrip1";
            this.contextMenuPropertyGrid.Size = new System.Drawing.Size(127, 34);
            // 
            // menuContextResetItem
            // 
            this.menuContextResetItem.Name = "menuContextResetItem";
            this.menuContextResetItem.Size = new System.Drawing.Size(126, 30);
            this.menuContextResetItem.Text = "Reset";
            this.menuContextResetItem.Click += new System.EventHandler(this.menuContextResetItem_Click);
            // 
            // tbLog
            // 
            this.tbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLog.Font = new System.Drawing.Font("OpenGost Type A TT", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.tbLog.Location = new System.Drawing.Point(0, 0);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbLog.Size = new System.Drawing.Size(622, 1195);
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
            this.menuImage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.menuImageOpenItem, this.menuImageSaveDraftItem, this.menuImageQuickSaveAsItem, this.menuImageSaveAsItem, this.menuImageCloseItem, this.toolStripMenuItem1, this.menuImageProcessItem, this.toolStripMenuItem2, this.menuImageExitItem});
            this.menuImage.Name = "menuImage";
            this.menuImage.Size = new System.Drawing.Size(137, 29);
            this.menuImage.Text = "&Изображение";
            // 
            // menuImageOpenItem
            // 
            this.menuImageOpenItem.Name = "menuImageOpenItem";
            this.menuImageOpenItem.ShortcutKeyDisplayString = "Ctrl+O; Alt+1";
            this.menuImageOpenItem.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.menuImageOpenItem.Size = new System.Drawing.Size(383, 30);
            this.menuImageOpenItem.Text = "&Открыть...";
            this.menuImageOpenItem.Click += new System.EventHandler(this.menuImageOpenItem_Click);
            // 
            // menuImageSaveDraftItem
            // 
            this.menuImageSaveDraftItem.Name = "menuImageSaveDraftItem";
            this.menuImageSaveDraftItem.ShortcutKeys = ((System.Windows.Forms.Keys) (((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) | System.Windows.Forms.Keys.S)));
            this.menuImageSaveDraftItem.Size = new System.Drawing.Size(383, 30);
            this.menuImageSaveDraftItem.Text = "&Сохранить черновик...";
            this.menuImageSaveDraftItem.Click += new System.EventHandler(this.menuImageSaveDraftItem_Click);
            // 
            // menuImageQuickSaveAsItem
            // 
            this.menuImageQuickSaveAsItem.Name = "menuImageQuickSaveAsItem";
            this.menuImageQuickSaveAsItem.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D2)));
            this.menuImageQuickSaveAsItem.Size = new System.Drawing.Size(383, 30);
            this.menuImageQuickSaveAsItem.Text = "&Быстро сохранить результат...";
            this.menuImageQuickSaveAsItem.Click += new System.EventHandler(this.menuImageQuickSaveAsItem_Click);
            // 
            // menuImageSaveAsItem
            // 
            this.menuImageSaveAsItem.Name = "menuImageSaveAsItem";
            this.menuImageSaveAsItem.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.menuImageSaveAsItem.Size = new System.Drawing.Size(383, 30);
            this.menuImageSaveAsItem.Text = "&Сохранить результат как...";
            this.menuImageSaveAsItem.Click += new System.EventHandler(this.menuImageSaveItem_Click);
            // 
            // menuImageCloseItem
            // 
            this.menuImageCloseItem.Name = "menuImageCloseItem";
            this.menuImageCloseItem.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.menuImageCloseItem.Size = new System.Drawing.Size(383, 30);
            this.menuImageCloseItem.Text = "Закрыть";
            this.menuImageCloseItem.Click += new System.EventHandler(this.menuImageCloseItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(380, 6);
            // 
            // menuImageProcessItem
            // 
            this.menuImageProcessItem.Name = "menuImageProcessItem";
            this.menuImageProcessItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.menuImageProcessItem.Size = new System.Drawing.Size(383, 30);
            this.menuImageProcessItem.Text = "Обр&аботка";
            this.menuImageProcessItem.Click += new System.EventHandler(this.buttonProcess_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(380, 6);
            // 
            // menuImageExitItem
            // 
            this.menuImageExitItem.Name = "menuImageExitItem";
            this.menuImageExitItem.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.X)));
            this.menuImageExitItem.Size = new System.Drawing.Size(383, 30);
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
            this.MenuViewFitItem.Click += new System.EventHandler(this.menuViewFitItem_Click);
            // 
            // MenuViewScaleItem
            // 
            this.MenuViewScaleItem.Name = "MenuViewScaleItem";
            this.MenuViewScaleItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.MenuViewScaleItem.Size = new System.Drawing.Size(255, 30);
            this.MenuViewScaleItem.Text = "Масштабировать";
            this.MenuViewScaleItem.Click += new System.EventHandler(this.menuViewScaleItem_Click);
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
            this.конверторToolStripMenuItem.Size = new System.Drawing.Size(268, 30);
            this.конверторToolStripMenuItem.Text = "Конвертор";
            this.конверторToolStripMenuItem.Click += new System.EventHandler(this.form2ToolStripMenuItem_Click);
            // 
            // form2ToolStripMenuItem1
            // 
            this.form2ToolStripMenuItem1.Name = "form2ToolStripMenuItem1";
            this.form2ToolStripMenuItem1.Size = new System.Drawing.Size(268, 30);
            this.form2ToolStripMenuItem1.Text = "Операции";
            this.form2ToolStripMenuItem1.Click += new System.EventHandler(this.form2ToolStripMenuItem1_Click);
            // 
            // saveTextForTranslateToolStripMenuItem
            // 
            this.saveTextForTranslateToolStripMenuItem.Name = "saveTextForTranslateToolStripMenuItem";
            this.saveTextForTranslateToolStripMenuItem.Size = new System.Drawing.Size(268, 30);
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
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip1);
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.tabControl1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(2742, 1544);
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
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.statusBarCaptionLabel, this.statusBarProgressBar, this.statusBarInfo});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(2742, 30);
            this.statusStrip1.TabIndex = 0;
            // 
            // statusBarCaptionLabel
            // 
            this.statusBarCaptionLabel.Name = "statusBarCaptionLabel";
            this.statusBarCaptionLabel.Size = new System.Drawing.Size(94, 25);
            this.statusBarCaptionLabel.Text = "Прогресс:";
            // 
            // statusBarProgressBar
            // 
            this.statusBarProgressBar.Maximum = 17;
            this.statusBarProgressBar.Name = "statusBarProgressBar";
            this.statusBarProgressBar.Size = new System.Drawing.Size(1500, 24);
            // 
            // statusBarInfo
            // 
            this.statusBarInfo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.statusBarInfo.Name = "statusBarInfo";
            this.statusBarInfo.Size = new System.Drawing.Size(1131, 25);
            this.statusBarInfo.Spring = true;
            this.statusBarInfo.Text = "(Current State)";
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
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.MainForm_PreviewKeyDown);
            this.tabControl1.ResumeLayout(false);
            this.tabPageImage.ResumeLayout(false);
            this.splitContainerImage.Panel1.ResumeLayout(false);
            this.splitContainerImage.Panel1.PerformLayout();
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
            this.splitPanelButtonAndLog.Panel2.ResumeLayout(false);
            this.splitPanelButtonAndLog.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize) (this.splitPanelButtonAndLog)).EndInit();
            this.splitPanelButtonAndLog.ResumeLayout(false);
            this.contextMenuPropertyGrid.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        public System.Windows.Forms.ToolStripMenuItem menuImageQuickSaveAsItem;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
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
        public System.Windows.Forms.Label lbHintSource;
        public System.Windows.Forms.Label lbHintTarget;
        public System.Windows.Forms.ToolStripMenuItem menuImageOpenItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        public System.Windows.Forms.ToolStripMenuItem menuImageExitItem;
        public System.Windows.Forms.ToolStripMenuItem menuImageProcessItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        public System.Windows.Forms.ToolStripMenuItem menuImageCloseItem;
        private System.Windows.Forms.ToolStripMenuItem saveTextForTranslateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuLanguage;
        private System.Windows.Forms.ToolStripMenuItem englishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem russianToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem menuImageSaveAsItem;
        public System.Windows.Forms.ToolStripMenuItem menuImageSaveDraftItem;
        private ContextMenuStrip contextMenuPropertyGrid;
        private ToolStripMenuItem menuContextResetItem;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel statusBarCaptionLabel;
        private System.Windows.Forms.ToolStripProgressBar statusBarProgressBar;
        private ToolStripStatusLabel statusBarInfo;
    }
}

