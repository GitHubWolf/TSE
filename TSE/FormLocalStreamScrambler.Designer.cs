namespace TSE
{
    partial class FormLocalStreamScrambler
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
            this.tableLayoutPanelBottom = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.maskedTextBoxCwPeriod = new System.Windows.Forms.MaskedTextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.buttonSelect = new System.Windows.Forms.Button();
            this.maskedTextBoxBitrate = new System.Windows.Forms.MaskedTextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.comboBoxEvenOdd = new System.Windows.Forms.ComboBox();
            this.checkBoxEnforceER = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.hexEditControlWordSerials = new InActionLibrary.HexEdit();
            this.tableLayoutPanelBottom.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxPidOut
            // 
            this.listBoxPidOut.Size = new System.Drawing.Size(401, 466);
            // 
            // tableLayoutPanelBottom
            // 
            this.tableLayoutPanelBottom.ColumnCount = 1;
            this.tableLayoutPanelBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelBottom.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanelBottom.Location = new System.Drawing.Point(0, 573);
            this.tableLayoutPanelBottom.Name = "tableLayoutPanelBottom";
            this.tableLayoutPanelBottom.RowCount = 1;
            this.tableLayoutPanelBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelBottom.Size = new System.Drawing.Size(963, 100);
            this.tableLayoutPanelBottom.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.groupBox6);
            this.panel3.Controls.Add(this.groupBox5);
            this.panel3.Controls.Add(this.groupBox4);
            this.panel3.Controls.Add(this.checkBoxEnforceER);
            this.panel3.Controls.Add(this.groupBox3);
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(957, 94);
            this.panel3.TabIndex = 10;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.maskedTextBoxCwPeriod);
            this.groupBox6.Location = new System.Drawing.Point(757, 25);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(138, 42);
            this.groupBox6.TabIndex = 3;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "CW Period(second):";
            // 
            // maskedTextBoxCwPeriod
            // 
            this.maskedTextBoxCwPeriod.HideSelection = false;
            this.maskedTextBoxCwPeriod.Location = new System.Drawing.Point(6, 15);
            this.maskedTextBoxCwPeriod.Mask = "0000";
            this.maskedTextBoxCwPeriod.Name = "maskedTextBoxCwPeriod";
            this.maskedTextBoxCwPeriod.PromptChar = ' ';
            this.maskedTextBoxCwPeriod.Size = new System.Drawing.Size(126, 21);
            this.maskedTextBoxCwPeriod.TabIndex = 2;
            this.maskedTextBoxCwPeriod.Text = "10";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.buttonSelect);
            this.groupBox5.Controls.Add(this.maskedTextBoxBitrate);
            this.groupBox5.Location = new System.Drawing.Point(500, 25);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(251, 42);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Bitrate(bps):";
            // 
            // buttonSelect
            // 
            this.buttonSelect.Location = new System.Drawing.Point(181, 15);
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.Size = new System.Drawing.Size(64, 23);
            this.buttonSelect.TabIndex = 3;
            this.buttonSelect.Text = "Select";
            this.buttonSelect.UseVisualStyleBackColor = true;
            this.buttonSelect.Click += new System.EventHandler(this.buttonSelect_Click);
            // 
            // maskedTextBoxBitrate
            // 
            this.maskedTextBoxBitrate.HideSelection = false;
            this.maskedTextBoxBitrate.Location = new System.Drawing.Point(6, 15);
            this.maskedTextBoxBitrate.Mask = "00000000000000000000000000";
            this.maskedTextBoxBitrate.Name = "maskedTextBoxBitrate";
            this.maskedTextBoxBitrate.PromptChar = ' ';
            this.maskedTextBoxBitrate.Size = new System.Drawing.Size(168, 21);
            this.maskedTextBoxBitrate.TabIndex = 2;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.comboBoxEvenOdd);
            this.groupBox4.Location = new System.Drawing.Point(331, 25);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(143, 45);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Start with Even/Odd:";
            // 
            // comboBoxEvenOdd
            // 
            this.comboBoxEvenOdd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEvenOdd.FormattingEnabled = true;
            this.comboBoxEvenOdd.Items.AddRange(new object[] {
            "Even",
            "Odd"});
            this.comboBoxEvenOdd.Location = new System.Drawing.Point(7, 21);
            this.comboBoxEvenOdd.Name = "comboBoxEvenOdd";
            this.comboBoxEvenOdd.Size = new System.Drawing.Size(121, 20);
            this.comboBoxEvenOdd.TabIndex = 0;
            // 
            // checkBoxEnforceER
            // 
            this.checkBoxEnforceER.AutoSize = true;
            this.checkBoxEnforceER.Checked = true;
            this.checkBoxEnforceER.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxEnforceER.Location = new System.Drawing.Point(331, 3);
            this.checkBoxEnforceER.Name = "checkBoxEnforceER";
            this.checkBoxEnforceER.Size = new System.Drawing.Size(84, 16);
            this.checkBoxEnforceER.TabIndex = 1;
            this.checkBoxEnforceER.Text = "Do Entropy";
            this.checkBoxEnforceER.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.hexEditControlWordSerials);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(325, 94);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Control Word Serials:";
            // 
            // hexEditControlWordSerials
            // 
            this.hexEditControlWordSerials.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hexEditControlWordSerials.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.hexEditControlWordSerials.HideSelection = false;
            this.hexEditControlWordSerials.Location = new System.Drawing.Point(3, 17);
            this.hexEditControlWordSerials.Name = "hexEditControlWordSerials";
            this.hexEditControlWordSerials.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.hexEditControlWordSerials.Size = new System.Drawing.Size(319, 74);
            this.hexEditControlWordSerials.TabIndex = 0;
            this.hexEditControlWordSerials.Text = "";
            this.hexEditControlWordSerials.ToolTip = "";
            // 
            // FormLocalStreamScrambler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(963, 673);
            this.Controls.Add(this.tableLayoutPanelBottom);
            this.Name = "FormLocalStreamScrambler";
            this.Text = "CSA Scrambler";
            this.Controls.SetChildIndex(this.tableLayoutPanelBottom, 0);
            this.tableLayoutPanelBottom.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelBottom;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox checkBoxEnforceER;
        private System.Windows.Forms.GroupBox groupBox3;
        private InActionLibrary.HexEdit hexEditControlWordSerials;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox comboBoxEvenOdd;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxBitrate;
        private System.Windows.Forms.Button buttonSelect;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxCwPeriod;

    }
}
