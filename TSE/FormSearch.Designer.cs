namespace TSE
{
    partial class FormSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSearch));
            this.groupBoxSearchType = new System.Windows.Forms.GroupBox();
            this.radioButtonSection = new System.Windows.Forms.RadioButton();
            this.radioButtonPesPacket = new System.Windows.Forms.RadioButton();
            this.radioButtonTsPacket = new System.Windows.Forms.RadioButton();
            this.groupBoxSearchRange = new System.Windows.Forms.GroupBox();
            this.checkBoxIgnorePID = new System.Windows.Forms.CheckBox();
            this.maskedTextBoxSkipTsPackets = new System.Windows.Forms.MaskedTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.maskedTextBoxSkipFound = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxCount = new System.Windows.Forms.ComboBox();
            this.groupBoxFilter = new System.Windows.Forms.GroupBox();
            this.hexEditMatch = new InActionLibrary.HexEdit();
            this.hexEditMask = new InActionLibrary.HexEdit();
            this.checkBoxShowDuplicateSection = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listBoxPid = new System.Windows.Forms.ListBox();
            this.toolTipCount = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelPluginDetails = new System.Windows.Forms.Label();
            this.listBoxParsers = new System.Windows.Forms.ListBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.checkBoxDumpToFileOnly = new System.Windows.Forms.CheckBox();
            this.groupBoxSearchType.SuspendLayout();
            this.groupBoxSearchRange.SuspendLayout();
            this.groupBoxFilter.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxSearchType
            // 
            this.groupBoxSearchType.Controls.Add(this.radioButtonSection);
            this.groupBoxSearchType.Controls.Add(this.radioButtonPesPacket);
            this.groupBoxSearchType.Controls.Add(this.radioButtonTsPacket);
            this.groupBoxSearchType.Location = new System.Drawing.Point(13, 13);
            this.groupBoxSearchType.Name = "groupBoxSearchType";
            this.groupBoxSearchType.Size = new System.Drawing.Size(238, 96);
            this.groupBoxSearchType.TabIndex = 0;
            this.groupBoxSearchType.TabStop = false;
            this.groupBoxSearchType.Text = "Search Type";
            // 
            // radioButtonSection
            // 
            this.radioButtonSection.Location = new System.Drawing.Point(6, 69);
            this.radioButtonSection.Name = "radioButtonSection";
            this.radioButtonSection.Size = new System.Drawing.Size(156, 22);
            this.radioButtonSection.TabIndex = 3;
            this.radioButtonSection.TabStop = true;
            this.radioButtonSection.Text = "Section";
            this.radioButtonSection.UseVisualStyleBackColor = true;
            this.radioButtonSection.Click += new System.EventHandler(this.radioButtonSection_Click);
            // 
            // radioButtonPesPacket
            // 
            this.radioButtonPesPacket.Location = new System.Drawing.Point(6, 45);
            this.radioButtonPesPacket.Name = "radioButtonPesPacket";
            this.radioButtonPesPacket.Size = new System.Drawing.Size(156, 24);
            this.radioButtonPesPacket.TabIndex = 1;
            this.radioButtonPesPacket.TabStop = true;
            this.radioButtonPesPacket.Text = "PES Packet";
            this.radioButtonPesPacket.UseVisualStyleBackColor = true;
            this.radioButtonPesPacket.Click += new System.EventHandler(this.radioButtonPesPacket_Click);
            // 
            // radioButtonTsPacket
            // 
            this.radioButtonTsPacket.Location = new System.Drawing.Point(6, 21);
            this.radioButtonTsPacket.Name = "radioButtonTsPacket";
            this.radioButtonTsPacket.Size = new System.Drawing.Size(156, 24);
            this.radioButtonTsPacket.TabIndex = 0;
            this.radioButtonTsPacket.TabStop = true;
            this.radioButtonTsPacket.Text = "TS Packet";
            this.radioButtonTsPacket.UseVisualStyleBackColor = true;
            this.radioButtonTsPacket.Click += new System.EventHandler(this.radioButtonTsPacket_Click);
            // 
            // groupBoxSearchRange
            // 
            this.groupBoxSearchRange.Controls.Add(this.checkBoxIgnorePID);
            this.groupBoxSearchRange.Controls.Add(this.maskedTextBoxSkipTsPackets);
            this.groupBoxSearchRange.Controls.Add(this.label5);
            this.groupBoxSearchRange.Controls.Add(this.maskedTextBoxSkipFound);
            this.groupBoxSearchRange.Controls.Add(this.label2);
            this.groupBoxSearchRange.Controls.Add(this.label1);
            this.groupBoxSearchRange.Controls.Add(this.comboBoxCount);
            this.groupBoxSearchRange.Location = new System.Drawing.Point(13, 117);
            this.groupBoxSearchRange.Name = "groupBoxSearchRange";
            this.groupBoxSearchRange.Size = new System.Drawing.Size(238, 206);
            this.groupBoxSearchRange.TabIndex = 1;
            this.groupBoxSearchRange.TabStop = false;
            this.groupBoxSearchRange.Text = "Search Range";
            // 
            // checkBoxIgnorePID
            // 
            this.checkBoxIgnorePID.AutoSize = true;
            this.checkBoxIgnorePID.Location = new System.Drawing.Point(10, 185);
            this.checkBoxIgnorePID.Name = "checkBoxIgnorePID";
            this.checkBoxIgnorePID.Size = new System.Drawing.Size(84, 16);
            this.checkBoxIgnorePID.TabIndex = 4;
            this.checkBoxIgnorePID.Text = "Ignore PID";
            this.checkBoxIgnorePID.UseVisualStyleBackColor = true;
            // 
            // maskedTextBoxSkipTsPackets
            // 
            this.maskedTextBoxSkipTsPackets.Location = new System.Drawing.Point(10, 150);
            this.maskedTextBoxSkipTsPackets.Mask = "9999999999";
            this.maskedTextBoxSkipTsPackets.Name = "maskedTextBoxSkipTsPackets";
            this.maskedTextBoxSkipTsPackets.PromptChar = ' ';
            this.maskedTextBoxSkipTsPackets.Size = new System.Drawing.Size(121, 21);
            this.maskedTextBoxSkipTsPackets.TabIndex = 3;
            this.maskedTextBoxSkipTsPackets.Text = "0";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(8, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 21);
            this.label5.TabIndex = 2;
            this.label5.Text = "Skip TS Packets:";
            // 
            // maskedTextBoxSkipFound
            // 
            this.maskedTextBoxSkipFound.Location = new System.Drawing.Point(9, 99);
            this.maskedTextBoxSkipFound.Mask = "9999999999";
            this.maskedTextBoxSkipFound.Name = "maskedTextBoxSkipFound";
            this.maskedTextBoxSkipFound.PromptChar = ' ';
            this.maskedTextBoxSkipFound.Size = new System.Drawing.Size(121, 21);
            this.maskedTextBoxSkipFound.TabIndex = 3;
            this.maskedTextBoxSkipFound.Text = "0";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(7, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "Skip Found:";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Count:";
            // 
            // comboBoxCount
            // 
            this.comboBoxCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCount.FormattingEnabled = true;
            this.comboBoxCount.Location = new System.Drawing.Point(9, 45);
            this.comboBoxCount.Name = "comboBoxCount";
            this.comboBoxCount.Size = new System.Drawing.Size(121, 20);
            this.comboBoxCount.TabIndex = 1;
            this.toolTipCount.SetToolTip(this.comboBoxCount, "How many packet/section to search. -1 means All.");
            // 
            // groupBoxFilter
            // 
            this.groupBoxFilter.Controls.Add(this.checkBoxDumpToFileOnly);
            this.groupBoxFilter.Controls.Add(this.hexEditMatch);
            this.groupBoxFilter.Controls.Add(this.hexEditMask);
            this.groupBoxFilter.Controls.Add(this.checkBoxShowDuplicateSection);
            this.groupBoxFilter.Controls.Add(this.label4);
            this.groupBoxFilter.Controls.Add(this.label3);
            this.groupBoxFilter.Location = new System.Drawing.Point(13, 329);
            this.groupBoxFilter.Name = "groupBoxFilter";
            this.groupBoxFilter.Size = new System.Drawing.Size(238, 191);
            this.groupBoxFilter.TabIndex = 2;
            this.groupBoxFilter.TabStop = false;
            this.groupBoxFilter.Text = "Section Filter Setting";
            // 
            // hexEditMatch
            // 
            this.hexEditMatch.Enabled = false;
            this.hexEditMatch.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.hexEditMatch.HideSelection = false;
            this.hexEditMatch.Location = new System.Drawing.Point(9, 106);
            this.hexEditMatch.Name = "hexEditMatch";
            this.hexEditMatch.Size = new System.Drawing.Size(222, 19);
            this.hexEditMatch.TabIndex = 5;
            this.hexEditMatch.Text = "00";
            this.hexEditMatch.ToolTip = "";
            // 
            // hexEditMask
            // 
            this.hexEditMask.Enabled = false;
            this.hexEditMask.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.hexEditMask.HideSelection = false;
            this.hexEditMask.Location = new System.Drawing.Point(10, 48);
            this.hexEditMask.Name = "hexEditMask";
            this.hexEditMask.Size = new System.Drawing.Size(222, 19);
            this.hexEditMask.TabIndex = 5;
            this.hexEditMask.Text = "00";
            this.hexEditMask.ToolTip = "";
            // 
            // checkBoxShowDuplicateSection
            // 
            this.checkBoxShowDuplicateSection.Enabled = false;
            this.checkBoxShowDuplicateSection.Location = new System.Drawing.Point(8, 130);
            this.checkBoxShowDuplicateSection.Name = "checkBoxShowDuplicateSection";
            this.checkBoxShowDuplicateSection.Size = new System.Drawing.Size(223, 24);
            this.checkBoxShowDuplicateSection.TabIndex = 4;
            this.checkBoxShowDuplicateSection.Text = "Show duplicate sections";
            this.checkBoxShowDuplicateSection.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(7, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(225, 23);
            this.label4.TabIndex = 2;
            this.label4.Text = "Filter Match:(e.g. 00 43 50 59)";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(7, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(225, 23);
            this.label3.TabIndex = 0;
            this.label3.Text = "Filter Mask:(e.g. FF 00 00 FF)";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listBoxPid);
            this.groupBox1.Location = new System.Drawing.Point(262, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(594, 279);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PID List";
            // 
            // listBoxPid
            // 
            this.listBoxPid.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBoxPid.FormattingEnabled = true;
            this.listBoxPid.HorizontalScrollbar = true;
            this.listBoxPid.ItemHeight = 11;
            this.listBoxPid.Location = new System.Drawing.Point(6, 16);
            this.listBoxPid.Name = "listBoxPid";
            this.listBoxPid.ScrollAlwaysVisible = true;
            this.listBoxPid.Size = new System.Drawing.Size(587, 257);
            this.listBoxPid.TabIndex = 1;
            this.listBoxPid.SelectedIndexChanged += new System.EventHandler(this.listBoxPid_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.labelPluginDetails);
            this.groupBox2.Controls.Add(this.listBoxParsers);
            this.groupBox2.Location = new System.Drawing.Point(262, 297);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(486, 223);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Plugins";
            // 
            // labelPluginDetails
            // 
            this.labelPluginDetails.Location = new System.Drawing.Point(253, 18);
            this.labelPluginDetails.Name = "labelPluginDetails";
            this.labelPluginDetails.Size = new System.Drawing.Size(227, 136);
            this.labelPluginDetails.TabIndex = 1;
            // 
            // listBoxParsers
            // 
            this.listBoxParsers.FormattingEnabled = true;
            this.listBoxParsers.HorizontalScrollbar = true;
            this.listBoxParsers.ItemHeight = 12;
            this.listBoxParsers.Location = new System.Drawing.Point(6, 18);
            this.listBoxParsers.Name = "listBoxParsers";
            this.listBoxParsers.Size = new System.Drawing.Size(238, 196);
            this.listBoxParsers.TabIndex = 0;
            this.listBoxParsers.SelectedIndexChanged += new System.EventHandler(this.listBoxParsers_SelectedIndexChanged);
            // 
            // buttonSearch
            // 
            this.buttonSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonSearch.BackgroundImage")));
            this.buttonSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonSearch.Location = new System.Drawing.Point(766, 442);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(85, 78);
            this.buttonSearch.TabIndex = 5;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // checkBoxDumpToFileOnly
            // 
            this.checkBoxDumpToFileOnly.AutoSize = true;
            this.checkBoxDumpToFileOnly.Location = new System.Drawing.Point(8, 169);
            this.checkBoxDumpToFileOnly.Name = "checkBoxDumpToFileOnly";
            this.checkBoxDumpToFileOnly.Size = new System.Drawing.Size(126, 16);
            this.checkBoxDumpToFileOnly.TabIndex = 6;
            this.checkBoxDumpToFileOnly.Text = "Dump to file only";
            this.checkBoxDumpToFileOnly.UseVisualStyleBackColor = true;
            // 
            // FormSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(863, 532);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBoxFilter);
            this.Controls.Add(this.groupBoxSearchRange);
            this.Controls.Add(this.groupBoxSearchType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormSearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Search";
            this.Shown += new System.EventHandler(this.SearchForm_Shown);
            this.groupBoxSearchType.ResumeLayout(false);
            this.groupBoxSearchRange.ResumeLayout(false);
            this.groupBoxSearchRange.PerformLayout();
            this.groupBoxFilter.ResumeLayout(false);
            this.groupBoxFilter.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxSearchType;
        private System.Windows.Forms.GroupBox groupBoxSearchRange;
        private System.Windows.Forms.RadioButton radioButtonSection;
        private System.Windows.Forms.RadioButton radioButtonPesPacket;
        private System.Windows.Forms.RadioButton radioButtonTsPacket;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxSkipFound;
        private System.Windows.Forms.GroupBox groupBoxFilter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBoxShowDuplicateSection;
        private InActionLibrary.HexEdit hexEditMatch;
        private InActionLibrary.HexEdit hexEditMask;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolTip toolTipCount;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label labelPluginDetails;
        private System.Windows.Forms.ListBox listBoxPid;
        private System.Windows.Forms.ListBox listBoxParsers;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxSkipTsPackets;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBoxIgnorePID;
        private System.Windows.Forms.CheckBox checkBoxDumpToFileOnly;
    }
}