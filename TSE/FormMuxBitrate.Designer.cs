namespace TSE
{
    partial class FormMuxBitrate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMuxBitrate));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridViewStreamBitrate = new System.Windows.Forms.DataGridView();
            this.Column1Pid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1Bitrate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonOK = new System.Windows.Forms.Button();
            this.maskedTextBoxBitrate = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStreamBitrate)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridViewStreamBitrate);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(379, 404);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Stream bitrate calculated from PCR from TS packet";
            // 
            // dataGridViewStreamBitrate
            // 
            this.dataGridViewStreamBitrate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewStreamBitrate.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1Pid,
            this.Column1Bitrate});
            this.dataGridViewStreamBitrate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewStreamBitrate.Location = new System.Drawing.Point(3, 16);
            this.dataGridViewStreamBitrate.MultiSelect = false;
            this.dataGridViewStreamBitrate.Name = "dataGridViewStreamBitrate";
            this.dataGridViewStreamBitrate.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewStreamBitrate.Size = new System.Drawing.Size(373, 342);
            this.dataGridViewStreamBitrate.TabIndex = 1;
            this.dataGridViewStreamBitrate.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewStreamBitrate_CellEnter);
            // 
            // Column1Pid
            // 
            this.Column1Pid.HeaderText = "Pid";
            this.Column1Pid.Name = "Column1Pid";
            this.Column1Pid.Width = 150;
            // 
            // Column1Bitrate
            // 
            this.Column1Bitrate.HeaderText = "Bitrate";
            this.Column1Bitrate.Name = "Column1Bitrate";
            this.Column1Bitrate.Width = 150;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonOK);
            this.panel1.Controls.Add(this.maskedTextBoxBitrate);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 358);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(373, 43);
            this.panel1.TabIndex = 0;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(295, 13);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // maskedTextBoxBitrate
            // 
            this.maskedTextBoxBitrate.HideSelection = false;
            this.maskedTextBoxBitrate.Location = new System.Drawing.Point(92, 13);
            this.maskedTextBoxBitrate.Mask = "00000000000000000000000000";
            this.maskedTextBoxBitrate.Name = "maskedTextBoxBitrate";
            this.maskedTextBoxBitrate.PromptChar = ' ';
            this.maskedTextBoxBitrate.Size = new System.Drawing.Size(168, 20);
            this.maskedTextBoxBitrate.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bitrate(bps):";
            // 
            // StreamBitrateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 404);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "StreamBitrateForm";
            this.Text = "Stream Bitrate";
            this.Shown += new System.EventHandler(this.StreamBitrateForm_Shown);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStreamBitrate)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridViewStreamBitrate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxBitrate;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1Pid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1Bitrate;

    }
}