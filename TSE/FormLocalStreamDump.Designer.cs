namespace TSE
{
    partial class FormLocalStreamDump
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
            this.tableLayoutPanelBottom = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.comboBoxPercentage = new System.Windows.Forms.ComboBox();
            this.checkBoxTo188 = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanelBottom.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxPidOut
            // 
            this.listBoxPidOut.Size = new System.Drawing.Size(399, 466);
            // 
            // tableLayoutPanelBottom
            // 
            this.tableLayoutPanelBottom.ColumnCount = 1;
            this.tableLayoutPanelBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelBottom.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanelBottom.Location = new System.Drawing.Point(0, 567);
            this.tableLayoutPanelBottom.Name = "tableLayoutPanelBottom";
            this.tableLayoutPanelBottom.RowCount = 1;
            this.tableLayoutPanelBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelBottom.Size = new System.Drawing.Size(960, 61);
            this.tableLayoutPanelBottom.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.checkBoxTo188);
            this.panel3.Controls.Add(this.groupBox3);
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(954, 55);
            this.panel3.TabIndex = 10;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.comboBoxPercentage);
            this.groupBox3.Location = new System.Drawing.Point(0, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(136, 44);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Percentage:";
            // 
            // comboBoxPercentage
            // 
            this.comboBoxPercentage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPercentage.FormattingEnabled = true;
            this.comboBoxPercentage.Items.AddRange(new object[] {
            "100",
            "90",
            "80",
            "70",
            "60",
            "50",
            "40",
            "30",
            "20",
            "15",
            "10",
            "5",
            "1"});
            this.comboBoxPercentage.Location = new System.Drawing.Point(9, 20);
            this.comboBoxPercentage.Name = "comboBoxPercentage";
            this.comboBoxPercentage.Size = new System.Drawing.Size(121, 20);
            this.comboBoxPercentage.TabIndex = 1;
            // 
            // checkBoxTo188
            // 
            this.checkBoxTo188.AutoSize = true;
            this.checkBoxTo188.Location = new System.Drawing.Point(154, 15);
            this.checkBoxTo188.Name = "checkBoxTo188";
            this.checkBoxTo188.Size = new System.Drawing.Size(126, 16);
            this.checkBoxTo188.TabIndex = 4;
            this.checkBoxTo188.Text = "Enforce 188 bytes";
            this.checkBoxTo188.UseVisualStyleBackColor = true;
            // 
            // FormLocalStreamDump
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(960, 628);
            this.Controls.Add(this.tableLayoutPanelBottom);
            this.Name = "FormLocalStreamDump";
            this.Text = "Extract Packets";
            this.Controls.SetChildIndex(this.tableLayoutPanelBottom, 0);
            this.tableLayoutPanelBottom.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelBottom;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox comboBoxPercentage;
        private System.Windows.Forms.CheckBox checkBoxTo188;

    }
}
