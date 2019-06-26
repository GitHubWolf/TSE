namespace TSE
{
    partial class FormFileStreamParser
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFileStreamParser));
            this.menuStripStreamParser = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bitrateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.streamBitrateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pIDUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pIDDumpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scramblerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.descramblerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.treeViewParser = new System.Windows.Forms.TreeView();
            this.imageListTreeView = new System.Windows.Forms.ImageList(this.components);
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.richTextBoxHexString = new InActionLibrary.HexEdit();
            this.richTextBoxProperty = new InActionLibrary.HexEdit();
            this.richTextBoxLogger = new InActionLibrary.HexEdit();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.contextMenuStripSearch = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.timerToUpdateData = new System.Windows.Forms.Timer(this.components);
            this.menuStripStreamParser.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.contextMenuStripSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripStreamParser
            // 
            this.menuStripStreamParser.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStripStreamParser.Location = new System.Drawing.Point(0, 0);
            this.menuStripStreamParser.Name = "menuStripStreamParser";
            this.menuStripStreamParser.Size = new System.Drawing.Size(834, 24);
            this.menuStripStreamParser.TabIndex = 0;
            this.menuStripStreamParser.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchToolStripMenuItem,
            this.bitrateToolStripMenuItem,
            this.streamBitrateToolStripMenuItem,
            this.pIDUpdateToolStripMenuItem,
            this.pIDDumpToolStripMenuItem,
            this.scramblerToolStripMenuItem,
            this.descramblerToolStripMenuItem,
            this.playoutToolStripMenuItem,
            this.stopToolStripMenuItem,
            this.toolStripMenuItem1});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.fileToolStripMenuItem.Text = "Stream";
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("searchToolStripMenuItem.Image")));
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.searchToolStripMenuItem.Text = "Search";
            this.searchToolStripMenuItem.Click += new System.EventHandler(this.searchToolStripMenuItem_Click);
            // 
            // bitrateToolStripMenuItem
            // 
            this.bitrateToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("bitrateToolStripMenuItem.Image")));
            this.bitrateToolStripMenuItem.Name = "bitrateToolStripMenuItem";
            this.bitrateToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.bitrateToolStripMenuItem.Text = "Stream Bitrate";
            this.bitrateToolStripMenuItem.Click += new System.EventHandler(this.bitrateToolStripMenuItem_Click);
            // 
            // streamBitrateToolStripMenuItem
            // 
            this.streamBitrateToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("streamBitrateToolStripMenuItem.Image")));
            this.streamBitrateToolStripMenuItem.Name = "streamBitrateToolStripMenuItem";
            this.streamBitrateToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.streamBitrateToolStripMenuItem.Text = "PID Bitrate";
            this.streamBitrateToolStripMenuItem.Click += new System.EventHandler(this.streamBitrateToolStripMenuItem_Click);
            // 
            // pIDUpdateToolStripMenuItem
            // 
            this.pIDUpdateToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pIDUpdateToolStripMenuItem.Image")));
            this.pIDUpdateToolStripMenuItem.Name = "pIDUpdateToolStripMenuItem";
            this.pIDUpdateToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.pIDUpdateToolStripMenuItem.Text = "PID Update";
            this.pIDUpdateToolStripMenuItem.Click += new System.EventHandler(this.pIDUpdateToolStripMenuItem_Click);
            // 
            // pIDDumpToolStripMenuItem
            // 
            this.pIDDumpToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pIDDumpToolStripMenuItem.Image")));
            this.pIDDumpToolStripMenuItem.Name = "pIDDumpToolStripMenuItem";
            this.pIDDumpToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.pIDDumpToolStripMenuItem.Text = "PID Dump";
            this.pIDDumpToolStripMenuItem.Click += new System.EventHandler(this.pIDDumpToolStripMenuItem_Click);
            // 
            // scramblerToolStripMenuItem
            // 
            this.scramblerToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("scramblerToolStripMenuItem.Image")));
            this.scramblerToolStripMenuItem.Name = "scramblerToolStripMenuItem";
            this.scramblerToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.scramblerToolStripMenuItem.Text = "Scrambler";
            this.scramblerToolStripMenuItem.Click += new System.EventHandler(this.scramblerToolStripMenuItem_Click);
            // 
            // descramblerToolStripMenuItem
            // 
            this.descramblerToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("descramblerToolStripMenuItem.Image")));
            this.descramblerToolStripMenuItem.Name = "descramblerToolStripMenuItem";
            this.descramblerToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.descramblerToolStripMenuItem.Text = "Descrambler";
            this.descramblerToolStripMenuItem.Click += new System.EventHandler(this.descramblerToolStripMenuItem_Click);
            // 
            // playoutToolStripMenuItem
            // 
            this.playoutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("playoutToolStripMenuItem.Image")));
            this.playoutToolStripMenuItem.Name = "playoutToolStripMenuItem";
            this.playoutToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.playoutToolStripMenuItem.Text = "Playout";
            this.playoutToolStripMenuItem.Click += new System.EventHandler(this.playoutToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("stopToolStripMenuItem.Image")));
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem1.Image")));
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(148, 22);
            this.toolStripMenuItem1.Text = "Close";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.richTextBoxLogger, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(834, 394);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.Controls.Add(this.treeViewParser, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(828, 289);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // treeViewParser
            // 
            this.treeViewParser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewParser.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeViewParser.ImageIndex = 0;
            this.treeViewParser.ImageList = this.imageListTreeView;
            this.treeViewParser.Indent = 25;
            this.treeViewParser.Location = new System.Drawing.Point(3, 3);
            this.treeViewParser.Name = "treeViewParser";
            this.treeViewParser.SelectedImageIndex = 0;
            this.treeViewParser.Size = new System.Drawing.Size(573, 283);
            this.treeViewParser.TabIndex = 0;
            this.treeViewParser.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewParser_AfterSelect);
            this.treeViewParser.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeViewParser_MouseDown);
            // 
            // imageListTreeView
            // 
            this.imageListTreeView.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTreeView.ImageStream")));
            this.imageListTreeView.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListTreeView.Images.SetKeyName(0, "PID.png");
            this.imageListTreeView.Images.SetKeyName(1, "PSI.png");
            this.imageListTreeView.Images.SetKeyName(2, "Root.png");
            this.imageListTreeView.Images.SetKeyName(3, "SI.png");
            this.imageListTreeView.Images.SetKeyName(4, "PSISection.png");
            this.imageListTreeView.Images.SetKeyName(5, "SISection.png");
            this.imageListTreeView.Images.SetKeyName(6, "Field.png");
            this.imageListTreeView.Images.SetKeyName(7, "Loop.png");
            this.imageListTreeView.Images.SetKeyName(8, "Item.png");
            this.imageListTreeView.Images.SetKeyName(9, "Error.png");
            this.imageListTreeView.Images.SetKeyName(10, "Warning.png");
            this.imageListTreeView.Images.SetKeyName(11, "PidItem.png");
            this.imageListTreeView.Images.SetKeyName(12, "PES.png");
            this.imageListTreeView.Images.SetKeyName(13, "Section.png");
            this.imageListTreeView.Images.SetKeyName(14, "TS.png");
            this.imageListTreeView.Images.SetKeyName(15, "SearchRequest.png");
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.richTextBoxHexString, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.richTextBoxProperty, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(582, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(243, 283);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // richTextBoxHexString
            // 
            this.richTextBoxHexString.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxHexString.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.richTextBoxHexString.HideSelection = false;
            this.richTextBoxHexString.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxHexString.Name = "richTextBoxHexString";
            this.richTextBoxHexString.Size = new System.Drawing.Size(237, 192);
            this.richTextBoxHexString.TabIndex = 3;
            this.richTextBoxHexString.Text = "";
            this.richTextBoxHexString.ToolTip = "";
            // 
            // richTextBoxProperty
            // 
            this.richTextBoxProperty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxProperty.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.richTextBoxProperty.HideSelection = false;
            this.richTextBoxProperty.Location = new System.Drawing.Point(3, 201);
            this.richTextBoxProperty.Name = "richTextBoxProperty";
            this.richTextBoxProperty.Size = new System.Drawing.Size(237, 79);
            this.richTextBoxProperty.TabIndex = 4;
            this.richTextBoxProperty.Text = "";
            this.richTextBoxProperty.ToolTip = "";
            // 
            // richTextBoxLogger
            // 
            this.richTextBoxLogger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxLogger.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.richTextBoxLogger.HideSelection = false;
            this.richTextBoxLogger.Location = new System.Drawing.Point(3, 298);
            this.richTextBoxLogger.Name = "richTextBoxLogger";
            this.richTextBoxLogger.Size = new System.Drawing.Size(828, 93);
            this.richTextBoxLogger.TabIndex = 1;
            this.richTextBoxLogger.Text = "";
            this.richTextBoxLogger.ToolTip = "";
            // 
            // statusStrip1
            // 
            this.statusStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 418);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(834, 32);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.AutoSize = false;
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(600, 26);
            this.toolStripProgressBar1.Step = 1;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.AutoSize = false;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(217, 27);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.Text = "0%";
            // 
            // contextMenuStripSearch
            // 
            this.contextMenuStripSearch.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemSearch});
            this.contextMenuStripSearch.Name = "contextMenuStripSearch";
            this.contextMenuStripSearch.Size = new System.Drawing.Size(110, 26);
            // 
            // toolStripMenuItemSearch
            // 
            this.toolStripMenuItemSearch.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemSearch.Image")));
            this.toolStripMenuItemSearch.Name = "toolStripMenuItemSearch";
            this.toolStripMenuItemSearch.Size = new System.Drawing.Size(109, 22);
            this.toolStripMenuItemSearch.Text = "Search";
            this.toolStripMenuItemSearch.Click += new System.EventHandler(this.toolStripMenuItemSearch_Click);
            // 
            // timerToUpdateData
            // 
            this.timerToUpdateData.Interval = 10;
            this.timerToUpdateData.Tick += new System.EventHandler(this.timerToUpdateData_Tick);
            // 
            // FormFileStreamParser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStripStreamParser);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStripStreamParser;
            this.Name = "FormFileStreamParser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Stream Parser";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StreamParserForm_FormClosing);
            this.Shown += new System.EventHandler(this.StreamParserForm_Shown);
            this.Resize += new System.EventHandler(this.FileStreamParserForm_Resize);
            this.menuStripStreamParser.ResumeLayout(false);
            this.menuStripStreamParser.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.contextMenuStripSearch.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripStreamParser;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TreeView treeViewParser;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.ImageList imageListTreeView;
        private InActionLibrary.HexEdit richTextBoxLogger;
        private InActionLibrary.HexEdit richTextBoxHexString;
        private InActionLibrary.HexEdit richTextBoxProperty;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripSearch;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSearch;
        private System.Windows.Forms.Timer timerToUpdateData;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bitrateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem streamBitrateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pIDUpdateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pIDDumpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem descramblerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scramblerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playoutToolStripMenuItem;
    }
}