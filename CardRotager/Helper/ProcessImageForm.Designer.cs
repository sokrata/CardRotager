
namespace CardRotager {
    partial class ProcessImageForm {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessImageForm));
            this.pbSource = new System.Windows.Forms.PictureBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.lbSource = new System.Windows.Forms.Label();
            this.lbTarget = new System.Windows.Forms.Label();
            this.pbTarget = new System.Windows.Forms.PictureBox();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.contextMenuPropertyGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuContextResetItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuConvertNewItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuConvertSaveItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.menuNewResolutionItem = new System.Windows.Forms.ToolStripButton();
            this.menuOpenResolutionItem = new System.Windows.Forms.ToolStripButton();
            this.menuSaveResolutionItem = new System.Windows.Forms.ToolStripButton();
            this.menuSaveSplitItem = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.cutToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.copyToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.pasteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.helpToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.menuSplitSaveItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize) (this.pbSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.pbTarget)).BeginInit();
            this.contextMenuPropertyGrid.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbSource
            // 
            this.pbSource.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbSource.Location = new System.Drawing.Point(0, 0);
            this.pbSource.Name = "pbSource";
            this.pbSource.Size = new System.Drawing.Size(895, 1390);
            this.pbSource.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbSource.TabIndex = 0;
            this.pbSource.TabStop = false;
            this.pbSource.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.propertyGrid1);
            this.splitContainer1.Size = new System.Drawing.Size(2168, 1390);
            this.splitContainer1.SplitterDistance = 1800;
            this.splitContainer1.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.lbSource);
            this.splitContainer2.Panel1.Controls.Add(this.pbSource);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.lbTarget);
            this.splitContainer2.Panel2.Controls.Add(this.pbTarget);
            this.splitContainer2.Size = new System.Drawing.Size(1800, 1390);
            this.splitContainer2.SplitterDistance = 895;
            this.splitContainer2.TabIndex = 1;
            // 
            // lbSource
            // 
            this.lbSource.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbSource.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.lbSource.Location = new System.Drawing.Point(0, 0);
            this.lbSource.Name = "lbSource";
            this.lbSource.Size = new System.Drawing.Size(895, 1390);
            this.lbSource.TabIndex = 1;
            this.lbSource.Text = "Нажмите здесь, чтобы открыть файл изображения";
            this.lbSource.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbSource.Click += new System.EventHandler(this.lbSource_Click);
            // 
            // lbTarget
            // 
            this.lbTarget.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbTarget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbTarget.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.lbTarget.Location = new System.Drawing.Point(0, 0);
            this.lbTarget.Name = "lbTarget";
            this.lbTarget.Size = new System.Drawing.Size(901, 1390);
            this.lbTarget.TabIndex = 2;
            this.lbTarget.Text = "Нажмите здесь, чтобы обработать изображения";
            this.lbTarget.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbTarget.Click += new System.EventHandler(this.lbResult_Click);
            // 
            // pbTarget
            // 
            this.pbTarget.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbTarget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbTarget.Location = new System.Drawing.Point(0, 0);
            this.pbTarget.Name = "pbTarget";
            this.pbTarget.Size = new System.Drawing.Size(901, 1390);
            this.pbTarget.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbTarget.TabIndex = 1;
            this.pbTarget.TabStop = false;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.ContextMenuStrip = this.contextMenuPropertyGrid;
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(364, 1390);
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
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(2168, 1390);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(2168, 1494);
            this.toolStripContainer1.TabIndex = 1;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(2168, 33);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.menuConvertNewItem, this.openToolStripMenuItem, this.toolStripSeparator2, this.menuConvertSaveItem, this.menuSplitSaveItem, this.saveAsToolStripMenuItem, this.toolStripSeparator3, this.printToolStripMenuItem, this.printPreviewToolStripMenuItem, this.toolStripSeparator4, this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(50, 29);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // menuConvertNewItem
            // 
            this.menuConvertNewItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuConvertNewItem.Name = "menuConvertNewItem";
            this.menuConvertNewItem.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.menuConvertNewItem.Size = new System.Drawing.Size(238, 30);
            this.menuConvertNewItem.Text = "&New";
            this.menuConvertNewItem.Click += new System.EventHandler(this.menuConvertNewItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = ((System.Drawing.Image) (resources.GetObject("openToolStripMenuItem.Image")));
            this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(238, 30);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.menuOpenResolutionItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(235, 6);
            // 
            // menuConvertSaveItem
            // 
            this.menuConvertSaveItem.Image = ((System.Drawing.Image) (resources.GetObject("menuConvertSaveItem.Image")));
            this.menuConvertSaveItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuConvertSaveItem.Name = "menuConvertSaveItem";
            this.menuConvertSaveItem.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.menuConvertSaveItem.Size = new System.Drawing.Size(238, 30);
            this.menuConvertSaveItem.Text = "&Save";
            this.menuConvertSaveItem.Click += new System.EventHandler(this.menuConvertSaveItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(238, 30);
            this.saveAsToolStripMenuItem.Text = "Save &As";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(235, 6);
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Image = ((System.Drawing.Image) (resources.GetObject("printToolStripMenuItem.Image")));
            this.printToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.printToolStripMenuItem.Size = new System.Drawing.Size(238, 30);
            this.printToolStripMenuItem.Text = "&Print";
            // 
            // printPreviewToolStripMenuItem
            // 
            this.printPreviewToolStripMenuItem.Image = ((System.Drawing.Image) (resources.GetObject("printPreviewToolStripMenuItem.Image")));
            this.printPreviewToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
            this.printPreviewToolStripMenuItem.Size = new System.Drawing.Size(238, 30);
            this.printPreviewToolStripMenuItem.Text = "Print Pre&view";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(235, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(238, 30);
            this.exitToolStripMenuItem.Text = "E&xit";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(64, 64);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.menuNewResolutionItem, this.menuOpenResolutionItem, this.menuSaveResolutionItem, this.menuSaveSplitItem, this.toolStripSeparator, this.cutToolStripButton, this.copyToolStripButton, this.pasteToolStripButton, this.toolStripSeparator1, this.helpToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(3, 33);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(284, 71);
            this.toolStrip1.TabIndex = 0;
            // 
            // menuNewResolutionItem
            // 
            this.menuNewResolutionItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.menuNewResolutionItem.Image = ((System.Drawing.Image) (resources.GetObject("menuNewResolutionItem.Image")));
            this.menuNewResolutionItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuNewResolutionItem.Name = "menuNewResolutionItem";
            this.menuNewResolutionItem.Size = new System.Drawing.Size(68, 68);
            this.menuNewResolutionItem.Text = "&New";
            this.menuNewResolutionItem.Click += new System.EventHandler(this.menuConvertNewItem_Click);
            // 
            // menuOpenResolutionItem
            // 
            this.menuOpenResolutionItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.menuOpenResolutionItem.Image = ((System.Drawing.Image) (resources.GetObject("menuOpenResolutionItem.Image")));
            this.menuOpenResolutionItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuOpenResolutionItem.Name = "menuOpenResolutionItem";
            this.menuOpenResolutionItem.Size = new System.Drawing.Size(68, 68);
            this.menuOpenResolutionItem.Text = "&Open";
            this.menuOpenResolutionItem.Click += new System.EventHandler(this.menuOpenResolutionItem_Click);
            // 
            // menuSaveResolutionItem
            // 
            this.menuSaveResolutionItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.menuSaveResolutionItem.Image = ((System.Drawing.Image) (resources.GetObject("menuSaveResolutionItem.Image")));
            this.menuSaveResolutionItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuSaveResolutionItem.Name = "menuSaveResolutionItem";
            this.menuSaveResolutionItem.Size = new System.Drawing.Size(68, 68);
            this.menuSaveResolutionItem.Text = "&Save";
            this.menuSaveResolutionItem.Click += new System.EventHandler(this.menuConvertSaveItem_Click);
            // 
            // menuSaveSplitItem
            // 
            this.menuSaveSplitItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.menuSaveSplitItem.Image = ((System.Drawing.Image) (resources.GetObject("menuSaveSplitItem.Image")));
            this.menuSaveSplitItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuSaveSplitItem.Name = "menuSaveSplitItem";
            this.menuSaveSplitItem.Size = new System.Drawing.Size(68, 68);
            this.menuSaveSplitItem.Text = "&Save with Split";
            this.menuSaveSplitItem.Click += new System.EventHandler(this.menuSaveSplitItem_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 71);
            this.toolStripSeparator.Visible = false;
            // 
            // cutToolStripButton
            // 
            this.cutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cutToolStripButton.Image = ((System.Drawing.Image) (resources.GetObject("cutToolStripButton.Image")));
            this.cutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cutToolStripButton.Name = "cutToolStripButton";
            this.cutToolStripButton.Size = new System.Drawing.Size(68, 68);
            this.cutToolStripButton.Text = "C&ut";
            this.cutToolStripButton.Visible = false;
            // 
            // copyToolStripButton
            // 
            this.copyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.copyToolStripButton.Image = ((System.Drawing.Image) (resources.GetObject("copyToolStripButton.Image")));
            this.copyToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copyToolStripButton.Name = "copyToolStripButton";
            this.copyToolStripButton.Size = new System.Drawing.Size(68, 68);
            this.copyToolStripButton.Text = "&Copy";
            this.copyToolStripButton.Visible = false;
            // 
            // pasteToolStripButton
            // 
            this.pasteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pasteToolStripButton.Image = ((System.Drawing.Image) (resources.GetObject("pasteToolStripButton.Image")));
            this.pasteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pasteToolStripButton.Name = "pasteToolStripButton";
            this.pasteToolStripButton.Size = new System.Drawing.Size(68, 68);
            this.pasteToolStripButton.Text = "&Paste";
            this.pasteToolStripButton.Visible = false;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 71);
            this.toolStripSeparator1.Visible = false;
            // 
            // helpToolStripButton
            // 
            this.helpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.helpToolStripButton.Image = ((System.Drawing.Image) (resources.GetObject("helpToolStripButton.Image")));
            this.helpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.helpToolStripButton.Name = "helpToolStripButton";
            this.helpToolStripButton.Size = new System.Drawing.Size(68, 68);
            this.helpToolStripButton.Text = "He&lp";
            this.helpToolStripButton.Visible = false;
            // 
            // menuSplitSaveItem
            // 
            this.menuSplitSaveItem.Image = ((System.Drawing.Image) (resources.GetObject("menuSplitSaveItem.Image")));
            this.menuSplitSaveItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuSplitSaveItem.Name = "menuSplitSaveItem";
            this.menuSplitSaveItem.ShortcutKeys = ((System.Windows.Forms.Keys) (((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) | System.Windows.Forms.Keys.S)));
            this.menuSplitSaveItem.Size = new System.Drawing.Size(238, 30);
            this.menuSplitSaveItem.Text = "&Save";
            // 
            // ProcessImageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2168, 1494);
            this.Controls.Add(this.toolStripContainer1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ProcessImageForm";
            this.Text = "Операции";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProcessImageForm_FormClosing);
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize) (this.pbSource)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize) (this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize) (this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize) (this.pbTarget)).EndInit();
            this.contextMenuPropertyGrid.ResumeLayout(false);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.ToolStripMenuItem menuSplitSaveItem;
        private System.Windows.Forms.ToolStripButton menuSaveSplitItem;

        #endregion

        private System.Windows.Forms.PictureBox pbSource;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton menuNewResolutionItem;
        private System.Windows.Forms.ToolStripButton menuOpenResolutionItem;
        private System.Windows.Forms.ToolStripButton menuSaveResolutionItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton cutToolStripButton;
        private System.Windows.Forms.ToolStripButton copyToolStripButton;
        private System.Windows.Forms.ToolStripButton pasteToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton helpToolStripButton;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.PictureBox pbTarget;
        private System.Windows.Forms.Label lbSource;
        private System.Windows.Forms.Label lbTarget;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuConvertNewItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem menuConvertSaveItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printPreviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuPropertyGrid;
        private System.Windows.Forms.ToolStripMenuItem menuContextResetItem;
    }
}