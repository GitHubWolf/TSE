namespace TSE
{
    partial class FormLocalStreamWorker
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLocalStreamWorker));
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBoxOutputFile = new System.Windows.Forms.GroupBox();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.textBoxOutputFile = new System.Windows.Forms.TextBox();
            this.tableLayoutPanelTop = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonStart = new System.Windows.Forms.Button();
            this.groupBoxNewPid = new System.Windows.Forms.GroupBox();
            this.maskedTextBoxNewPid = new System.Windows.Forms.MaskedTextBox();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listBoxPidIn = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listBoxPidOut = new System.Windows.Forms.ListBox();
            this.tableLayoutPanelAll = new System.Windows.Forms.TableLayoutPanel();
            this.panel2.SuspendLayout();
            this.groupBoxOutputFile.SuspendLayout();
            this.tableLayoutPanelTop.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBoxNewPid.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanelAll.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBoxOutputFile);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 509);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(957, 51);
            this.panel2.TabIndex = 1;
            // 
            // groupBoxOutputFile
            // 
            this.groupBoxOutputFile.Controls.Add(this.buttonBrowse);
            this.groupBoxOutputFile.Controls.Add(this.textBoxOutputFile);
            this.groupBoxOutputFile.Location = new System.Drawing.Point(3, 3);
            this.groupBoxOutputFile.Name = "groupBoxOutputFile";
            this.groupBoxOutputFile.Size = new System.Drawing.Size(371, 45);
            this.groupBoxOutputFile.TabIndex = 5;
            this.groupBoxOutputFile.TabStop = false;
            this.groupBoxOutputFile.Text = "Output File:";
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Location = new System.Drawing.Point(297, 18);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(67, 23);
            this.buttonBrowse.TabIndex = 1;
            this.buttonBrowse.Text = "Browse";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // textBoxOutputFile
            // 
            this.textBoxOutputFile.Location = new System.Drawing.Point(6, 20);
            this.textBoxOutputFile.Name = "textBoxOutputFile";
            this.textBoxOutputFile.Size = new System.Drawing.Size(285, 21);
            this.textBoxOutputFile.TabIndex = 0;
            // 
            // tableLayoutPanelTop
            // 
            this.tableLayoutPanelTop.ColumnCount = 3;
            this.tableLayoutPanelTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43F));
            this.tableLayoutPanelTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tableLayoutPanelTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43F));
            this.tableLayoutPanelTop.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanelTop.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanelTop.Controls.Add(this.groupBox2, 2, 0);
            this.tableLayoutPanelTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelTop.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanelTop.Name = "tableLayoutPanelTop";
            this.tableLayoutPanelTop.RowCount = 1;
            this.tableLayoutPanelTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelTop.Size = new System.Drawing.Size(957, 500);
            this.tableLayoutPanelTop.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonStart);
            this.panel1.Controls.Add(this.groupBoxNewPid);
            this.panel1.Controls.Add(this.buttonRemove);
            this.panel1.Controls.Add(this.buttonAdd);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(414, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(127, 494);
            this.panel1.TabIndex = 5;
            // 
            // buttonStart
            // 
            this.buttonStart.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonStart.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonStart.BackgroundImage")));
            this.buttonStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonStart.Location = new System.Drawing.Point(18, 443);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(81, 51);
            this.buttonStart.TabIndex = 3;
            this.buttonStart.Text = "DoIt";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // groupBoxNewPid
            // 
            this.groupBoxNewPid.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBoxNewPid.Controls.Add(this.maskedTextBoxNewPid);
            this.groupBoxNewPid.Location = new System.Drawing.Point(18, 144);
            this.groupBoxNewPid.Name = "groupBoxNewPid";
            this.groupBoxNewPid.Size = new System.Drawing.Size(81, 52);
            this.groupBoxNewPid.TabIndex = 2;
            this.groupBoxNewPid.TabStop = false;
            this.groupBoxNewPid.Text = "New PID:";
            this.groupBoxNewPid.Visible = false;
            // 
            // maskedTextBoxNewPid
            // 
            this.maskedTextBoxNewPid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.maskedTextBoxNewPid.Location = new System.Drawing.Point(7, 21);
            this.maskedTextBoxNewPid.Name = "maskedTextBoxNewPid";
            this.maskedTextBoxNewPid.Size = new System.Drawing.Size(68, 21);
            this.maskedTextBoxNewPid.TabIndex = 0;
            this.maskedTextBoxNewPid.Text = "0";
            // 
            // buttonRemove
            // 
            this.buttonRemove.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonRemove.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonRemove.BackgroundImage")));
            this.buttonRemove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonRemove.Location = new System.Drawing.Point(18, 72);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(81, 51);
            this.buttonRemove.TabIndex = 1;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonAdd.BackgroundImage")));
            this.buttonAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonAdd.Location = new System.Drawing.Point(18, 6);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(81, 51);
            this.buttonAdd.TabIndex = 0;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listBoxPidIn);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(405, 494);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PID Input:";
            // 
            // listBoxPidIn
            // 
            this.listBoxPidIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxPidIn.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBoxPidIn.FormattingEnabled = true;
            this.listBoxPidIn.HorizontalScrollbar = true;
            this.listBoxPidIn.ItemHeight = 11;
            this.listBoxPidIn.Location = new System.Drawing.Point(3, 17);
            this.listBoxPidIn.Name = "listBoxPidIn";
            this.listBoxPidIn.ScrollAlwaysVisible = true;
            this.listBoxPidIn.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxPidIn.Size = new System.Drawing.Size(399, 474);
            this.listBoxPidIn.TabIndex = 5;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listBoxPidOut);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(547, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(407, 494);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "PID Output:";
            // 
            // listBoxPidOut
            // 
            this.listBoxPidOut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxPidOut.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBoxPidOut.FormattingEnabled = true;
            this.listBoxPidOut.HorizontalScrollbar = true;
            this.listBoxPidOut.ItemHeight = 11;
            this.listBoxPidOut.Location = new System.Drawing.Point(3, 17);
            this.listBoxPidOut.Name = "listBoxPidOut";
            this.listBoxPidOut.ScrollAlwaysVisible = true;
            this.listBoxPidOut.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxPidOut.Size = new System.Drawing.Size(401, 474);
            this.listBoxPidOut.TabIndex = 4;
            // 
            // tableLayoutPanelAll
            // 
            this.tableLayoutPanelAll.ColumnCount = 1;
            this.tableLayoutPanelAll.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelAll.Controls.Add(this.tableLayoutPanelTop, 0, 0);
            this.tableLayoutPanelAll.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanelAll.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanelAll.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelAll.Name = "tableLayoutPanelAll";
            this.tableLayoutPanelAll.RowCount = 2;
            this.tableLayoutPanelAll.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanelAll.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanelAll.Size = new System.Drawing.Size(963, 563);
            this.tableLayoutPanelAll.TabIndex = 0;
            // 
            // FormLocalStreamWorker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(963, 673);
            this.Controls.Add(this.tableLayoutPanelAll);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormLocalStreamWorker";
            this.Text = "FormLocalStreamWorker";
            this.Shown += new System.EventHandler(this.FormLocalStreamWorker_Shown);
            this.panel2.ResumeLayout(false);
            this.groupBoxOutputFile.ResumeLayout(false);
            this.groupBoxOutputFile.PerformLayout();
            this.tableLayoutPanelTop.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBoxNewPid.ResumeLayout(false);
            this.groupBoxNewPid.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanelAll.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBoxOutputFile;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelTop;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBoxNewPid;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxNewPid;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox listBoxPidIn;
        private System.Windows.Forms.GroupBox groupBox2;
        protected System.Windows.Forms.TableLayoutPanel tableLayoutPanelAll;
        protected System.Windows.Forms.ListBox listBoxPidOut;
        protected System.Windows.Forms.TextBox textBoxOutputFile;
        private System.Windows.Forms.Button buttonStart;

    }
}