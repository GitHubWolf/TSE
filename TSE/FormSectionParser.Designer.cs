namespace TSE
{
    partial class FormSectionParser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSectionParser));
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.panelRight = new System.Windows.Forms.Panel();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.richTextBoxHexString = new InActionLibrary.HexEdit();
            this.richTextBoxProperty = new InActionLibrary.HexEdit();
            this.treeViewParser = new System.Windows.Forms.TreeView();
            this.imageListTreeView = new System.Windows.Forms.ImageList(this.components);
            this.panelLeft = new System.Windows.Forms.Panel();
            this.groupBoxTop = new System.Windows.Forms.GroupBox();
            this.hexEditData = new InActionLibrary.HexEdit();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listBoxParsers = new System.Windows.Forms.ListBox();
            this.labelPluginDetails = new System.Windows.Forms.Label();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.buttonParse = new System.Windows.Forms.Button();
            this.tableLayoutPanelMain.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.groupBoxTop.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 2;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanelMain.Controls.Add(this.panelRight, 1, 0);
            this.tableLayoutPanelMain.Controls.Add(this.panelLeft, 0, 0);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 1;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(941, 527);
            this.tableLayoutPanelMain.TabIndex = 0;
            // 
            // panelRight
            // 
            this.panelRight.Controls.Add(this.tableLayoutPanel);
            this.panelRight.Controls.Add(this.treeViewParser);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(285, 3);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(653, 521);
            this.panelRight.TabIndex = 5;
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.richTextBoxHexString, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.richTextBoxProperty, 0, 1);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.tableLayoutPanel.Location = new System.Drawing.Point(309, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(344, 521);
            this.tableLayoutPanel.TabIndex = 5;
            // 
            // richTextBoxHexString
            // 
            this.richTextBoxHexString.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxHexString.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.richTextBoxHexString.HideSelection = false;
            this.richTextBoxHexString.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxHexString.Name = "richTextBoxHexString";
            this.richTextBoxHexString.Size = new System.Drawing.Size(338, 358);
            this.richTextBoxHexString.TabIndex = 0;
            this.richTextBoxHexString.Text = "";
            this.richTextBoxHexString.ToolTip = "";
            // 
            // richTextBoxProperty
            // 
            this.richTextBoxProperty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxProperty.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.richTextBoxProperty.HideSelection = false;
            this.richTextBoxProperty.Location = new System.Drawing.Point(3, 367);
            this.richTextBoxProperty.Name = "richTextBoxProperty";
            this.richTextBoxProperty.Size = new System.Drawing.Size(338, 151);
            this.richTextBoxProperty.TabIndex = 1;
            this.richTextBoxProperty.Text = "";
            this.richTextBoxProperty.ToolTip = "";
            // 
            // treeViewParser
            // 
            this.treeViewParser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewParser.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeViewParser.ImageIndex = 0;
            this.treeViewParser.ImageList = this.imageListTreeView;
            this.treeViewParser.Indent = 25;
            this.treeViewParser.Location = new System.Drawing.Point(0, 0);
            this.treeViewParser.Name = "treeViewParser";
            this.treeViewParser.SelectedImageIndex = 0;
            this.treeViewParser.Size = new System.Drawing.Size(653, 521);
            this.treeViewParser.TabIndex = 0;
            this.treeViewParser.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewParser_AfterSelect);
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
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.groupBoxTop);
            this.panelLeft.Controls.Add(this.groupBox2);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLeft.Location = new System.Drawing.Point(3, 3);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(276, 521);
            this.panelLeft.TabIndex = 6;
            // 
            // groupBoxTop
            // 
            this.groupBoxTop.Controls.Add(this.hexEditData);
            this.groupBoxTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxTop.Location = new System.Drawing.Point(0, 0);
            this.groupBoxTop.Name = "groupBoxTop";
            this.groupBoxTop.Size = new System.Drawing.Size(276, 274);
            this.groupBoxTop.TabIndex = 0;
            this.groupBoxTop.TabStop = false;
            this.groupBoxTop.Text = "HEX Data:";
            // 
            // hexEditData
            // 
            this.hexEditData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hexEditData.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.hexEditData.HideSelection = false;
            this.hexEditData.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.hexEditData.Location = new System.Drawing.Point(3, 17);
            this.hexEditData.Name = "hexEditData";
            this.hexEditData.Size = new System.Drawing.Size(270, 254);
            this.hexEditData.TabIndex = 1;
            this.hexEditData.Text = resources.GetString("hexEditData.Text");
            this.hexEditData.ToolTip = "Please enter HEX data here.";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listBoxParsers);
            this.groupBox2.Controls.Add(this.labelPluginDetails);
            this.groupBox2.Controls.Add(this.panelBottom);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 274);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(276, 247);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Plugins";
            // 
            // listBoxParsers
            // 
            this.listBoxParsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxParsers.HorizontalScrollbar = true;
            this.listBoxParsers.ItemHeight = 12;
            this.listBoxParsers.Location = new System.Drawing.Point(3, 17);
            this.listBoxParsers.Name = "listBoxParsers";
            this.listBoxParsers.Size = new System.Drawing.Size(270, 76);
            this.listBoxParsers.TabIndex = 0;
            this.listBoxParsers.SelectedIndexChanged += new System.EventHandler(this.listBoxParsers_SelectedIndexChanged);
            // 
            // labelPluginDetails
            // 
            this.labelPluginDetails.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelPluginDetails.Location = new System.Drawing.Point(3, 93);
            this.labelPluginDetails.Name = "labelPluginDetails";
            this.labelPluginDetails.Size = new System.Drawing.Size(270, 67);
            this.labelPluginDetails.TabIndex = 3;
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.buttonParse);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(3, 160);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(270, 84);
            this.panelBottom.TabIndex = 1;
            // 
            // buttonParse
            // 
            this.buttonParse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonParse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonParse.Font = new System.Drawing.Font("SimSun", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonParse.ForeColor = System.Drawing.Color.Blue;
            this.buttonParse.Location = new System.Drawing.Point(193, 3);
            this.buttonParse.Name = "buttonParse";
            this.buttonParse.Size = new System.Drawing.Size(73, 75);
            this.buttonParse.TabIndex = 0;
            this.buttonParse.Text = "Parse";
            this.buttonParse.UseVisualStyleBackColor = true;
            this.buttonParse.Click += new System.EventHandler(this.buttonParse_Click);
            // 
            // FormSectionParser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(941, 527);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormSectionParser";
            this.Text = "Section Parser";
            this.Shown += new System.EventHandler(this.FormSectionParser_Shown);
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.panelRight.ResumeLayout(false);
            this.tableLayoutPanel.ResumeLayout(false);
            this.panelLeft.ResumeLayout(false);
            this.groupBoxTop.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.panelBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private InActionLibrary.HexEdit richTextBoxHexString;
        private InActionLibrary.HexEdit richTextBoxProperty;
        private System.Windows.Forms.TreeView treeViewParser;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox listBoxParsers;
        private System.Windows.Forms.GroupBox groupBoxTop;
        private InActionLibrary.HexEdit hexEditData;
        private System.Windows.Forms.Label labelPluginDetails;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Button buttonParse;
        private System.Windows.Forms.ImageList imageListTreeView;
    }
}