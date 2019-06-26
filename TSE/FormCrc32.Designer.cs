namespace TSE
{
    partial class FormCrc32
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCrc32));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.hexEditData = new InActionLibrary.HexEdit();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonCalculate = new System.Windows.Forms.Button();
            this.hexEditCrc32Result = new InActionLibrary.HexEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.hexEditData, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(499, 319);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // hexEditData
            // 
            this.hexEditData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hexEditData.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.hexEditData.HideSelection = false;
            this.hexEditData.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.hexEditData.Location = new System.Drawing.Point(3, 3);
            this.hexEditData.Name = "hexEditData";
            this.hexEditData.Size = new System.Drawing.Size(493, 265);
            this.hexEditData.TabIndex = 0;
            this.hexEditData.Text = "";
            this.hexEditData.ToolTip = "Please enter data to calculate CRC-32 here.";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.flowLayoutPanel1.Controls.Add(this.buttonCalculate);
            this.flowLayoutPanel1.Controls.Add(this.hexEditCrc32Result);
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 274);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(493, 42);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // buttonCalculate
            // 
            this.buttonCalculate.Location = new System.Drawing.Point(393, 3);
            this.buttonCalculate.Name = "buttonCalculate";
            this.buttonCalculate.Size = new System.Drawing.Size(97, 23);
            this.buttonCalculate.TabIndex = 2;
            this.buttonCalculate.Text = "Calculate";
            this.buttonCalculate.UseVisualStyleBackColor = true;
            this.buttonCalculate.Click += new System.EventHandler(this.buttonCalculate_Click);
            // 
            // hexEditCrc32Result
            // 
            this.hexEditCrc32Result.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.hexEditCrc32Result.HideSelection = false;
            this.hexEditCrc32Result.Location = new System.Drawing.Point(300, 3);
            this.hexEditCrc32Result.Name = "hexEditCrc32Result";
            this.hexEditCrc32Result.ReadOnly = true;
            this.hexEditCrc32Result.Size = new System.Drawing.Size(87, 23);
            this.hexEditCrc32Result.TabIndex = 4;
            this.hexEditCrc32Result.Text = "";
            this.hexEditCrc32Result.ToolTip = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(211, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "CRC32 Result:";
            // 
            // Crc32Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 319);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Crc32Form";
            this.Text = "CRC 32 Generation";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private InActionLibrary.HexEdit hexEditData;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button buttonCalculate;
        private System.Windows.Forms.Label label1;
        private InActionLibrary.HexEdit hexEditCrc32Result;

    }
}