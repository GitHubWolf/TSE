namespace TSE
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.toolStripMainForm = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonCrc32 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonUdp = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSection = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonManual = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAbout = new System.Windows.Forms.ToolStripButton();
            this.menuStripMainForm = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemRecentFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.calculateCRC32ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uDPHelperToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sectionParserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileHorizontalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileVerticalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cascadeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButtonRecord = new System.Windows.Forms.ToolStripButton();
            this.recordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainForm.SuspendLayout();
            this.menuStripMainForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMainForm
            // 
            this.toolStripMainForm.AutoSize = false;
            this.toolStripMainForm.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.toolStripMainForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonOpen,
            this.toolStripButtonRecord,
            this.toolStripButtonCrc32,
            this.toolStripButtonUdp,
            this.toolStripButtonSection,
            this.toolStripButtonManual,
            this.toolStripButtonAbout});
            this.toolStripMainForm.Location = new System.Drawing.Point(0, 25);
            this.toolStripMainForm.Name = "toolStripMainForm";
            this.toolStripMainForm.Size = new System.Drawing.Size(689, 53);
            this.toolStripMainForm.TabIndex = 1;
            // 
            // toolStripButtonOpen
            // 
            this.toolStripButtonOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonOpen.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonOpen.Image")));
            this.toolStripButtonOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOpen.Name = "toolStripButtonOpen";
            this.toolStripButtonOpen.Size = new System.Drawing.Size(52, 50);
            this.toolStripButtonOpen.Text = "Open";
            this.toolStripButtonOpen.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripButtonCrc32
            // 
            this.toolStripButtonCrc32.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonCrc32.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonCrc32.Image")));
            this.toolStripButtonCrc32.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCrc32.Name = "toolStripButtonCrc32";
            this.toolStripButtonCrc32.Size = new System.Drawing.Size(52, 50);
            this.toolStripButtonCrc32.Text = "CRC32";
            this.toolStripButtonCrc32.Click += new System.EventHandler(this.calculateCRC32ToolStripMenuItem_Click);
            // 
            // toolStripButtonUdp
            // 
            this.toolStripButtonUdp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonUdp.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonUdp.Image")));
            this.toolStripButtonUdp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonUdp.Name = "toolStripButtonUdp";
            this.toolStripButtonUdp.Size = new System.Drawing.Size(52, 50);
            this.toolStripButtonUdp.Text = "UDP Helper";
            this.toolStripButtonUdp.Click += new System.EventHandler(this.uDPHelperToolStripMenuItem_Click);
            // 
            // toolStripButtonSection
            // 
            this.toolStripButtonSection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSection.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSection.Image")));
            this.toolStripButtonSection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSection.Name = "toolStripButtonSection";
            this.toolStripButtonSection.Size = new System.Drawing.Size(52, 50);
            this.toolStripButtonSection.Text = "Section Parser";
            this.toolStripButtonSection.Click += new System.EventHandler(this.sectionParserToolStripMenuItem_Click);
            // 
            // toolStripButtonManual
            // 
            this.toolStripButtonManual.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonManual.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonManual.Image")));
            this.toolStripButtonManual.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonManual.Name = "toolStripButtonManual";
            this.toolStripButtonManual.Size = new System.Drawing.Size(52, 50);
            this.toolStripButtonManual.Text = "Manual";
            this.toolStripButtonManual.Visible = false;
            // 
            // toolStripButtonAbout
            // 
            this.toolStripButtonAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAbout.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAbout.Image")));
            this.toolStripButtonAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAbout.Name = "toolStripButtonAbout";
            this.toolStripButtonAbout.Size = new System.Drawing.Size(52, 50);
            this.toolStripButtonAbout.Text = "About";
            this.toolStripButtonAbout.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // menuStripMainForm
            // 
            this.menuStripMainForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStripMainForm.Location = new System.Drawing.Point(0, 0);
            this.menuStripMainForm.Name = "menuStripMainForm";
            this.menuStripMainForm.Size = new System.Drawing.Size(689, 25);
            this.menuStripMainForm.TabIndex = 3;
            this.menuStripMainForm.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.toolStripMenuItemRecentFiles,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(39, 21);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.DropDownOpening += new System.EventHandler(this.fileToolStripMenuItem_DropDownOpening);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripMenuItemRecentFiles
            // 
            this.toolStripMenuItemRecentFiles.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemRecentFiles.Image")));
            this.toolStripMenuItemRecentFiles.Name = "toolStripMenuItemRecentFiles";
            this.toolStripMenuItemRecentFiles.Size = new System.Drawing.Size(144, 22);
            this.toolStripMenuItemRecentFiles.Text = "Recent Files";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(141, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("exitToolStripMenuItem.Image")));
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.recordToolStripMenuItem,
            this.calculateCRC32ToolStripMenuItem,
            this.uDPHelperToolStripMenuItem,
            this.sectionParserToolStripMenuItem,
            this.optionToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(52, 21);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // calculateCRC32ToolStripMenuItem
            // 
            this.calculateCRC32ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("calculateCRC32ToolStripMenuItem.Image")));
            this.calculateCRC32ToolStripMenuItem.Name = "calculateCRC32ToolStripMenuItem";
            this.calculateCRC32ToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.calculateCRC32ToolStripMenuItem.Text = "Calculate CRC32";
            this.calculateCRC32ToolStripMenuItem.Click += new System.EventHandler(this.calculateCRC32ToolStripMenuItem_Click);
            // 
            // uDPHelperToolStripMenuItem
            // 
            this.uDPHelperToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("uDPHelperToolStripMenuItem.Image")));
            this.uDPHelperToolStripMenuItem.Name = "uDPHelperToolStripMenuItem";
            this.uDPHelperToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.uDPHelperToolStripMenuItem.Text = "UDP Helper";
            this.uDPHelperToolStripMenuItem.Click += new System.EventHandler(this.uDPHelperToolStripMenuItem_Click);
            // 
            // sectionParserToolStripMenuItem
            // 
            this.sectionParserToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("sectionParserToolStripMenuItem.Image")));
            this.sectionParserToolStripMenuItem.Name = "sectionParserToolStripMenuItem";
            this.sectionParserToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.sectionParserToolStripMenuItem.Text = "Section Parser";
            this.sectionParserToolStripMenuItem.Click += new System.EventHandler(this.sectionParserToolStripMenuItem_Click);
            // 
            // optionToolStripMenuItem
            // 
            this.optionToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("optionToolStripMenuItem.Image")));
            this.optionToolStripMenuItem.Name = "optionToolStripMenuItem";
            this.optionToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.optionToolStripMenuItem.Text = "Option";
            this.optionToolStripMenuItem.Visible = false;
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tileHorizontalToolStripMenuItem,
            this.tileVerticalToolStripMenuItem,
            this.cascadeToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(47, 21);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // tileHorizontalToolStripMenuItem
            // 
            this.tileHorizontalToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("tileHorizontalToolStripMenuItem.Image")));
            this.tileHorizontalToolStripMenuItem.Name = "tileHorizontalToolStripMenuItem";
            this.tileHorizontalToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.tileHorizontalToolStripMenuItem.Text = "Tile Horizontal";
            this.tileHorizontalToolStripMenuItem.Click += new System.EventHandler(this.tileHorizontalToolStripMenuItem_Click);
            // 
            // tileVerticalToolStripMenuItem
            // 
            this.tileVerticalToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("tileVerticalToolStripMenuItem.Image")));
            this.tileVerticalToolStripMenuItem.Name = "tileVerticalToolStripMenuItem";
            this.tileVerticalToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.tileVerticalToolStripMenuItem.Text = "Tile Vertical";
            this.tileVerticalToolStripMenuItem.Click += new System.EventHandler(this.tileVerticalToolStripMenuItem_Click);
            // 
            // cascadeToolStripMenuItem
            // 
            this.cascadeToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cascadeToolStripMenuItem.Image")));
            this.cascadeToolStripMenuItem.Name = "cascadeToolStripMenuItem";
            this.cascadeToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.cascadeToolStripMenuItem.Text = "Cascade";
            this.cascadeToolStripMenuItem.Click += new System.EventHandler(this.cascadeToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.manualToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(47, 21);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("aboutToolStripMenuItem.Image")));
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // manualToolStripMenuItem
            // 
            this.manualToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("manualToolStripMenuItem.Image")));
            this.manualToolStripMenuItem.Name = "manualToolStripMenuItem";
            this.manualToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.manualToolStripMenuItem.Text = "Manual";
            this.manualToolStripMenuItem.Visible = false;
            // 
            // toolStripButtonRecord
            // 
            this.toolStripButtonRecord.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRecord.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRecord.Image")));
            this.toolStripButtonRecord.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRecord.Name = "toolStripButtonRecord";
            this.toolStripButtonRecord.Size = new System.Drawing.Size(52, 50);
            this.toolStripButtonRecord.Text = "Record";
            this.toolStripButtonRecord.Click += new System.EventHandler(this.toolStripButtonRecord_Click);
            // 
            // recordToolStripMenuItem
            // 
            this.recordToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("recordToolStripMenuItem.Image")));
            this.recordToolStripMenuItem.Name = "recordToolStripMenuItem";
            this.recordToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.recordToolStripMenuItem.Text = "Record";
            this.recordToolStripMenuItem.Click += new System.EventHandler(this.toolStripButtonRecord_Click);
            // 
            // FormMain
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(689, 358);
            this.Controls.Add(this.toolStripMainForm);
            this.Controls.Add(this.menuStripMainForm);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStripMainForm;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transport Stream Expert";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.toolStripMainForm.ResumeLayout(false);
            this.toolStripMainForm.PerformLayout();
            this.menuStripMainForm.ResumeLayout(false);
            this.menuStripMainForm.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripMainForm;
        private System.Windows.Forms.ToolStripButton toolStripButtonOpen;
        private System.Windows.Forms.MenuStrip menuStripMainForm;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tileHorizontalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tileVerticalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cascadeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemRecentFiles;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manualToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButtonManual;
        private System.Windows.Forms.ToolStripMenuItem calculateCRC32ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uDPHelperToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButtonCrc32;
        private System.Windows.Forms.ToolStripButton toolStripButtonUdp;
        private System.Windows.Forms.ToolStripButton toolStripButtonAbout;
        private System.Windows.Forms.ToolStripMenuItem sectionParserToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButtonSection;
        private System.Windows.Forms.ToolStripButton toolStripButtonRecord;
        private System.Windows.Forms.ToolStripMenuItem recordToolStripMenuItem;

    }
}

