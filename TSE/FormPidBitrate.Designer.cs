namespace TSE
{
    partial class FormPidBitrate
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPidBitrate));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageDetails = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridViewRealtimeBitrate = new System.Windows.Forms.DataGridView();
            this.buttonExportToExcel = new System.Windows.Forms.Button();
            this.tabPageLineChart = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.chartRealtimeBitrate = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonColumnChart = new System.Windows.Forms.Button();
            this.buttonPointChart = new System.Windows.Forms.Button();
            this.buttonLine = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPageDetails.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRealtimeBitrate)).BeginInit();
            this.tabPageLineChart.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartRealtimeBitrate)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageDetails);
            this.tabControl1.Controls.Add(this.tabPageLineChart);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(694, 415);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageDetails
            // 
            this.tabPageDetails.Controls.Add(this.tableLayoutPanel1);
            this.tabPageDetails.Location = new System.Drawing.Point(4, 22);
            this.tabPageDetails.Name = "tabPageDetails";
            this.tabPageDetails.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDetails.Size = new System.Drawing.Size(686, 389);
            this.tabPageDetails.TabIndex = 0;
            this.tabPageDetails.Text = "Details";
            this.tabPageDetails.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonExportToExcel, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(680, 383);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridViewRealtimeBitrate);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(674, 338);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Realtime Bitrate(bps):";
            // 
            // dataGridViewRealtimeBitrate
            // 
            this.dataGridViewRealtimeBitrate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRealtimeBitrate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewRealtimeBitrate.EnableHeadersVisualStyles = false;
            this.dataGridViewRealtimeBitrate.Location = new System.Drawing.Point(3, 17);
            this.dataGridViewRealtimeBitrate.Name = "dataGridViewRealtimeBitrate";
            this.dataGridViewRealtimeBitrate.Size = new System.Drawing.Size(668, 318);
            this.dataGridViewRealtimeBitrate.TabIndex = 0;
            // 
            // buttonExportToExcel
            // 
            this.buttonExportToExcel.Location = new System.Drawing.Point(3, 347);
            this.buttonExportToExcel.Name = "buttonExportToExcel";
            this.buttonExportToExcel.Size = new System.Drawing.Size(104, 21);
            this.buttonExportToExcel.TabIndex = 1;
            this.buttonExportToExcel.Text = "Export To Excel";
            this.buttonExportToExcel.UseVisualStyleBackColor = true;
            this.buttonExportToExcel.Click += new System.EventHandler(this.buttonExportToExcel_Click);
            // 
            // tabPageLineChart
            // 
            this.tabPageLineChart.Controls.Add(this.tableLayoutPanel2);
            this.tabPageLineChart.Location = new System.Drawing.Point(4, 22);
            this.tabPageLineChart.Name = "tabPageLineChart";
            this.tabPageLineChart.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLineChart.Size = new System.Drawing.Size(686, 389);
            this.tabPageLineChart.TabIndex = 1;
            this.tabPageLineChart.Text = "Chart";
            this.tabPageLineChart.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.chartRealtimeBitrate, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(680, 383);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // chartRealtimeBitrate
            // 
            chartArea1.Name = "ChartAreaLine";
            this.chartRealtimeBitrate.ChartAreas.Add(chartArea1);
            this.chartRealtimeBitrate.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "LegendLine";
            this.chartRealtimeBitrate.Legends.Add(legend1);
            this.chartRealtimeBitrate.Location = new System.Drawing.Point(3, 3);
            this.chartRealtimeBitrate.Name = "chartRealtimeBitrate";
            this.chartRealtimeBitrate.Size = new System.Drawing.Size(674, 338);
            this.chartRealtimeBitrate.TabIndex = 1;
            this.chartRealtimeBitrate.Text = "Realtime Bitrate";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.buttonColumnChart);
            this.flowLayoutPanel1.Controls.Add(this.buttonPointChart);
            this.flowLayoutPanel1.Controls.Add(this.buttonLine);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 347);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(674, 33);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // buttonColumnChart
            // 
            this.buttonColumnChart.Location = new System.Drawing.Point(3, 3);
            this.buttonColumnChart.Name = "buttonColumnChart";
            this.buttonColumnChart.Size = new System.Drawing.Size(96, 21);
            this.buttonColumnChart.TabIndex = 3;
            this.buttonColumnChart.Text = "Cloumn Chart";
            this.buttonColumnChart.UseVisualStyleBackColor = true;
            this.buttonColumnChart.Click += new System.EventHandler(this.buttonColumnChart_Click);
            // 
            // buttonPointChart
            // 
            this.buttonPointChart.Location = new System.Drawing.Point(105, 3);
            this.buttonPointChart.Name = "buttonPointChart";
            this.buttonPointChart.Size = new System.Drawing.Size(92, 21);
            this.buttonPointChart.TabIndex = 5;
            this.buttonPointChart.Text = "Point Chart";
            this.buttonPointChart.UseVisualStyleBackColor = true;
            this.buttonPointChart.Click += new System.EventHandler(this.buttonPointChart_Click);
            // 
            // buttonLine
            // 
            this.buttonLine.Location = new System.Drawing.Point(203, 3);
            this.buttonLine.Name = "buttonLine";
            this.buttonLine.Size = new System.Drawing.Size(75, 21);
            this.buttonLine.TabIndex = 6;
            this.buttonLine.Text = "Line Chart";
            this.buttonLine.UseVisualStyleBackColor = true;
            this.buttonLine.Click += new System.EventHandler(this.buttonLine_Click);
            // 
            // FormPidBitrate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 415);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormPidBitrate";
            this.Text = "Realtime Bitrate";
            this.tabControl1.ResumeLayout(false);
            this.tabPageDetails.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRealtimeBitrate)).EndInit();
            this.tabPageLineChart.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartRealtimeBitrate)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageDetails;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridViewRealtimeBitrate;
        private System.Windows.Forms.Button buttonExportToExcel;
        private System.Windows.Forms.TabPage tabPageLineChart;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartRealtimeBitrate;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button buttonColumnChart;
        private System.Windows.Forms.Button buttonPointChart;
        private System.Windows.Forms.Button buttonLine;

    }
}