namespace TSE
{
    partial class FormLocalStreamDescrambler
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
            this.checkBoxEnforceER = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.hexEditControlWordSerials = new InActionLibrary.HexEdit();
            this.tableLayoutPanelBottom.SuspendLayout();
            this.panel3.SuspendLayout();
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
            this.tableLayoutPanelBottom.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.checkBoxEnforceER);
            this.panel3.Controls.Add(this.groupBox3);
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(957, 94);
            this.panel3.TabIndex = 10;
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
            // FormLocalStreamDescrambler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(963, 673);
            this.Controls.Add(this.tableLayoutPanelBottom);
            this.Name = "FormLocalStreamDescrambler";
            this.Text = "CSA Descrambler";
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
        private System.Windows.Forms.CheckBox checkBoxEnforceER;
        private System.Windows.Forms.GroupBox groupBox3;
        private InActionLibrary.HexEdit hexEditControlWordSerials;



    }
}
