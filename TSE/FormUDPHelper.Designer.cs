namespace TSE
{
    partial class FormUDPHelper
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormUDPHelper));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxSend = new System.Windows.Forms.GroupBox();
            this.textBoxDataToSend = new InActionLibrary.HexEdit();
            this.groupBoxReceive = new System.Windows.Forms.GroupBox();
            this.textBoxPacketsReceived = new InActionLibrary.HexEdit();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBoxListeningPort = new System.Windows.Forms.TextBox();
            this.buttonLeaveMulticastGroup = new System.Windows.Forms.Button();
            this.buttonJoinMulticastGroup = new System.Windows.Forms.Button();
            this.buttonStopListening = new System.Windows.Forms.Button();
            this.buttonStartListening = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxMulticastAddress = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBoxDestinationPort = new System.Windows.Forms.TextBox();
            this.buttonSendData = new System.Windows.Forms.Button();
            this.textBoxDestinationIP = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBoxSend.SuspendLayout();
            this.groupBoxReceive.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.groupBoxSend, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxReceive, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 1);
            this.tableLayoutPanel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(758, 563);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBoxSend
            // 
            this.groupBoxSend.Controls.Add(this.textBoxDataToSend);
            this.groupBoxSend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxSend.Location = new System.Drawing.Point(382, 3);
            this.groupBoxSend.Name = "groupBoxSend";
            this.groupBoxSend.Size = new System.Drawing.Size(373, 388);
            this.groupBoxSend.TabIndex = 12;
            this.groupBoxSend.TabStop = false;
            this.groupBoxSend.Text = "Send";
            // 
            // textBoxDataToSend
            // 
            this.textBoxDataToSend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxDataToSend.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxDataToSend.HideSelection = false;
            this.textBoxDataToSend.Location = new System.Drawing.Point(3, 17);
            this.textBoxDataToSend.Name = "textBoxDataToSend";
            this.textBoxDataToSend.Size = new System.Drawing.Size(367, 368);
            this.textBoxDataToSend.TabIndex = 0;
            this.textBoxDataToSend.Text = "";
            this.textBoxDataToSend.ToolTip = "";
            // 
            // groupBoxReceive
            // 
            this.groupBoxReceive.Controls.Add(this.textBoxPacketsReceived);
            this.groupBoxReceive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxReceive.Location = new System.Drawing.Point(3, 3);
            this.groupBoxReceive.Name = "groupBoxReceive";
            this.groupBoxReceive.Size = new System.Drawing.Size(373, 388);
            this.groupBoxReceive.TabIndex = 10;
            this.groupBoxReceive.TabStop = false;
            this.groupBoxReceive.Text = "Receive";
            // 
            // textBoxPacketsReceived
            // 
            this.textBoxPacketsReceived.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxPacketsReceived.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxPacketsReceived.HideSelection = false;
            this.textBoxPacketsReceived.Location = new System.Drawing.Point(3, 17);
            this.textBoxPacketsReceived.Name = "textBoxPacketsReceived";
            this.textBoxPacketsReceived.Size = new System.Drawing.Size(367, 368);
            this.textBoxPacketsReceived.TabIndex = 0;
            this.textBoxPacketsReceived.Text = "";
            this.textBoxPacketsReceived.ToolTip = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBoxListeningPort);
            this.panel1.Controls.Add(this.buttonLeaveMulticastGroup);
            this.panel1.Controls.Add(this.buttonJoinMulticastGroup);
            this.panel1.Controls.Add(this.buttonStopListening);
            this.panel1.Controls.Add(this.buttonStartListening);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.textBoxMulticastAddress);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 397);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(373, 163);
            this.panel1.TabIndex = 11;
            // 
            // textBoxListeningPort
            // 
            this.textBoxListeningPort.Location = new System.Drawing.Point(9, 31);
            this.textBoxListeningPort.Name = "textBoxListeningPort";
            this.textBoxListeningPort.Size = new System.Drawing.Size(123, 21);
            this.textBoxListeningPort.TabIndex = 1;
            this.textBoxListeningPort.Text = "1234";
            // 
            // buttonLeaveMulticastGroup
            // 
            this.buttonLeaveMulticastGroup.Location = new System.Drawing.Point(167, 99);
            this.buttonLeaveMulticastGroup.Name = "buttonLeaveMulticastGroup";
            this.buttonLeaveMulticastGroup.Size = new System.Drawing.Size(112, 21);
            this.buttonLeaveMulticastGroup.TabIndex = 7;
            this.buttonLeaveMulticastGroup.Text = "Leave Multicast Group";
            this.buttonLeaveMulticastGroup.UseVisualStyleBackColor = true;
            this.buttonLeaveMulticastGroup.Click += new System.EventHandler(this.buttonLeaveMulticastGroup_Click);
            // 
            // buttonJoinMulticastGroup
            // 
            this.buttonJoinMulticastGroup.Location = new System.Drawing.Point(167, 72);
            this.buttonJoinMulticastGroup.Name = "buttonJoinMulticastGroup";
            this.buttonJoinMulticastGroup.Size = new System.Drawing.Size(112, 21);
            this.buttonJoinMulticastGroup.TabIndex = 6;
            this.buttonJoinMulticastGroup.Text = "Join Multicast Group";
            this.buttonJoinMulticastGroup.UseVisualStyleBackColor = true;
            this.buttonJoinMulticastGroup.Click += new System.EventHandler(this.buttonJoinMulticastGroup_Click);
            // 
            // buttonStopListening
            // 
            this.buttonStopListening.Enabled = false;
            this.buttonStopListening.Location = new System.Drawing.Point(167, 31);
            this.buttonStopListening.Name = "buttonStopListening";
            this.buttonStopListening.Size = new System.Drawing.Size(110, 21);
            this.buttonStopListening.TabIndex = 3;
            this.buttonStopListening.Text = "Stop Listening";
            this.buttonStopListening.UseVisualStyleBackColor = true;
            this.buttonStopListening.Click += new System.EventHandler(this.buttonStopListening_Click);
            // 
            // buttonStartListening
            // 
            this.buttonStartListening.Location = new System.Drawing.Point(167, 4);
            this.buttonStartListening.Name = "buttonStartListening";
            this.buttonStartListening.Size = new System.Drawing.Size(110, 21);
            this.buttonStartListening.TabIndex = 2;
            this.buttonStartListening.Text = "Start Listening";
            this.buttonStartListening.UseVisualStyleBackColor = true;
            this.buttonStartListening.Click += new System.EventHandler(this.buttonStartListening_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "Listening Port:";
            // 
            // textBoxMulticastAddress
            // 
            this.textBoxMulticastAddress.Location = new System.Drawing.Point(9, 99);
            this.textBoxMulticastAddress.Name = "textBoxMulticastAddress";
            this.textBoxMulticastAddress.Size = new System.Drawing.Size(123, 21);
            this.textBoxMulticastAddress.TabIndex = 5;
            this.textBoxMulticastAddress.Text = "239.0.0.1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "Multicast IP:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.textBoxDestinationPort);
            this.panel2.Controls.Add(this.buttonSendData);
            this.panel2.Controls.Add(this.textBoxDestinationIP);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(382, 397);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(373, 163);
            this.panel2.TabIndex = 13;
            // 
            // textBoxDestinationPort
            // 
            this.textBoxDestinationPort.Location = new System.Drawing.Point(119, 28);
            this.textBoxDestinationPort.Name = "textBoxDestinationPort";
            this.textBoxDestinationPort.Size = new System.Drawing.Size(123, 21);
            this.textBoxDestinationPort.TabIndex = 1;
            this.textBoxDestinationPort.Text = "1234";
            // 
            // buttonSendData
            // 
            this.buttonSendData.Location = new System.Drawing.Point(6, 99);
            this.buttonSendData.Name = "buttonSendData";
            this.buttonSendData.Size = new System.Drawing.Size(75, 21);
            this.buttonSendData.TabIndex = 2;
            this.buttonSendData.Text = "Send";
            this.buttonSendData.UseVisualStyleBackColor = true;
            this.buttonSendData.Click += new System.EventHandler(this.buttonSendData_Click);
            // 
            // textBoxDestinationIP
            // 
            this.textBoxDestinationIP.Location = new System.Drawing.Point(119, 1);
            this.textBoxDestinationIP.Name = "textBoxDestinationIP";
            this.textBoxDestinationIP.Size = new System.Drawing.Size(123, 21);
            this.textBoxDestinationIP.TabIndex = 0;
            this.textBoxDestinationIP.Text = "239.0.0.1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "Destination Port:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "Destination IP:";
            // 
            // UdpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(758, 563);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UdpForm";
            this.Text = "UDP Helper";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UdpForm_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBoxSend.ResumeLayout(false);
            this.groupBoxReceive.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBoxReceive;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBoxListeningPort;
        private System.Windows.Forms.Button buttonLeaveMulticastGroup;
        private System.Windows.Forms.Button buttonJoinMulticastGroup;
        private System.Windows.Forms.Button buttonStopListening;
        private System.Windows.Forms.Button buttonStartListening;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxMulticastAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxSend;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBoxDestinationPort;
        private System.Windows.Forms.Button buttonSendData;
        private System.Windows.Forms.TextBox textBoxDestinationIP;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private InActionLibrary.HexEdit textBoxPacketsReceived;
        private InActionLibrary.HexEdit textBoxDataToSend;
    }
}