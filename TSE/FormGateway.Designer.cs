namespace TSE
{
    partial class FormGateway
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGateway));
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.tabControlInput = new System.Windows.Forms.TabControl();
            this.tabPageInputDevice = new System.Windows.Forms.TabPage();
            this.groupBoxDeviceList = new System.Windows.Forms.GroupBox();
            this.listBoxAllInputDevices = new System.Windows.Forms.ListBox();
            this.tabPageInputNetwork = new System.Windows.Forms.TabPage();
            this.listBoxInputNetworkInterfaces = new InActionLibrary.NetworkInterfaceListBox(this.components);
            this.checkBoxMulticast = new System.Windows.Forms.CheckBox();
            this.textBoxListeningPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxMulticastAddress = new System.Windows.Forms.TextBox();
            this.labelMulticastIp = new System.Windows.Forms.Label();
            this.tabPageInputFile = new System.Windows.Forms.TabPage();
            this.progressBarPlay = new System.Windows.Forms.ProgressBar();
            this.checkBoxLoop = new System.Windows.Forms.CheckBox();
            this.groupBoxBitrate = new System.Windows.Forms.GroupBox();
            this.maskedTextBoxBitrate = new System.Windows.Forms.MaskedTextBox();
            this.groupBoxInputFile = new System.Windows.Forms.GroupBox();
            this.buttonBrowseInput = new System.Windows.Forms.Button();
            this.textBoxInputFile = new System.Windows.Forms.TextBox();
            this.tabControlOutput = new System.Windows.Forms.TabControl();
            this.tabPageOutput = new System.Windows.Forms.TabPage();
            this.groupBoxOutputToDevice = new System.Windows.Forms.GroupBox();
            this.listBoxAllOutputDevices = new System.Windows.Forms.ListBox();
            this.checkBoxOutputToDevice = new System.Windows.Forms.CheckBox();
            this.groupBoxOutputToNetwork = new System.Windows.Forms.GroupBox();
            this.listBoxOutputNetworkInterfaces = new InActionLibrary.NetworkInterfaceListBox(this.components);
            this.textBoxDestinationPort = new System.Windows.Forms.TextBox();
            this.textBoxDestinationIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBoxOutputFile = new System.Windows.Forms.GroupBox();
            this.buttonBrowseOutput = new System.Windows.Forms.Button();
            this.textBoxOutputFile = new System.Windows.Forms.TextBox();
            this.checkBoxOutputToNetwork = new System.Windows.Forms.CheckBox();
            this.checkBoxOutputToFile = new System.Windows.Forms.CheckBox();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonStart = new System.Windows.Forms.Button();
            this.panelStatus = new System.Windows.Forms.Panel();
            this.labelStartTime = new System.Windows.Forms.Label();
            this.labelDuration = new System.Windows.Forms.Label();
            this.labelTotalSize = new System.Windows.Forms.Label();
            this.textBoxStartTime = new System.Windows.Forms.TextBox();
            this.textBoxDuration = new System.Windows.Forms.TextBox();
            this.textBoxTotalSize = new System.Windows.Forms.TextBox();
            this.textBoxBitrate = new System.Windows.Forms.TextBox();
            this.labelBitrate = new System.Windows.Forms.Label();
            this.timerToUpdateUI = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanelMain.SuspendLayout();
            this.tabControlInput.SuspendLayout();
            this.tabPageInputDevice.SuspendLayout();
            this.groupBoxDeviceList.SuspendLayout();
            this.tabPageInputNetwork.SuspendLayout();
            this.tabPageInputFile.SuspendLayout();
            this.groupBoxBitrate.SuspendLayout();
            this.groupBoxInputFile.SuspendLayout();
            this.tabControlOutput.SuspendLayout();
            this.tabPageOutput.SuspendLayout();
            this.groupBoxOutputToDevice.SuspendLayout();
            this.groupBoxOutputToNetwork.SuspendLayout();
            this.groupBoxOutputFile.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.panelStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 1;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Controls.Add(this.tabControlInput, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.tabControlOutput, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.panelBottom, 0, 3);
            this.tableLayoutPanelMain.Controls.Add(this.panelStatus, 0, 2);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 4;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(707, 582);
            this.tableLayoutPanelMain.TabIndex = 0;
            // 
            // tabControlInput
            // 
            this.tabControlInput.Controls.Add(this.tabPageInputDevice);
            this.tabControlInput.Controls.Add(this.tabPageInputNetwork);
            this.tabControlInput.Controls.Add(this.tabPageInputFile);
            this.tabControlInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlInput.Location = new System.Drawing.Point(3, 3);
            this.tabControlInput.Name = "tabControlInput";
            this.tabControlInput.SelectedIndex = 0;
            this.tabControlInput.Size = new System.Drawing.Size(701, 168);
            this.tabControlInput.TabIndex = 0;
            // 
            // tabPageInputDevice
            // 
            this.tabPageInputDevice.Controls.Add(this.groupBoxDeviceList);
            this.tabPageInputDevice.Location = new System.Drawing.Point(4, 22);
            this.tabPageInputDevice.Name = "tabPageInputDevice";
            this.tabPageInputDevice.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageInputDevice.Size = new System.Drawing.Size(693, 142);
            this.tabPageInputDevice.TabIndex = 0;
            this.tabPageInputDevice.Text = "Input From Device";
            this.tabPageInputDevice.UseVisualStyleBackColor = true;
            // 
            // groupBoxDeviceList
            // 
            this.groupBoxDeviceList.Controls.Add(this.listBoxAllInputDevices);
            this.groupBoxDeviceList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxDeviceList.Location = new System.Drawing.Point(3, 3);
            this.groupBoxDeviceList.Name = "groupBoxDeviceList";
            this.groupBoxDeviceList.Size = new System.Drawing.Size(687, 136);
            this.groupBoxDeviceList.TabIndex = 0;
            this.groupBoxDeviceList.TabStop = false;
            this.groupBoxDeviceList.Text = "Device List";
            // 
            // listBoxAllInputDevices
            // 
            this.listBoxAllInputDevices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxAllInputDevices.FormattingEnabled = true;
            this.listBoxAllInputDevices.Location = new System.Drawing.Point(3, 16);
            this.listBoxAllInputDevices.Name = "listBoxAllInputDevices";
            this.listBoxAllInputDevices.Size = new System.Drawing.Size(681, 117);
            this.listBoxAllInputDevices.TabIndex = 3;
            // 
            // tabPageInputNetwork
            // 
            this.tabPageInputNetwork.Controls.Add(this.listBoxInputNetworkInterfaces);
            this.tabPageInputNetwork.Controls.Add(this.checkBoxMulticast);
            this.tabPageInputNetwork.Controls.Add(this.textBoxListeningPort);
            this.tabPageInputNetwork.Controls.Add(this.label2);
            this.tabPageInputNetwork.Controls.Add(this.label3);
            this.tabPageInputNetwork.Controls.Add(this.textBoxMulticastAddress);
            this.tabPageInputNetwork.Controls.Add(this.labelMulticastIp);
            this.tabPageInputNetwork.Location = new System.Drawing.Point(4, 22);
            this.tabPageInputNetwork.Name = "tabPageInputNetwork";
            this.tabPageInputNetwork.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageInputNetwork.Size = new System.Drawing.Size(693, 142);
            this.tabPageInputNetwork.TabIndex = 1;
            this.tabPageInputNetwork.Text = "Input From Network";
            this.tabPageInputNetwork.UseVisualStyleBackColor = true;
            // 
            // listBoxInputNetworkInterfaces
            // 
            this.listBoxInputNetworkInterfaces.FormattingEnabled = true;
            this.listBoxInputNetworkInterfaces.Location = new System.Drawing.Point(7, 23);
            this.listBoxInputNetworkInterfaces.Name = "listBoxInputNetworkInterfaces";
            this.listBoxInputNetworkInterfaces.Size = new System.Drawing.Size(170, 95);
            this.listBoxInputNetworkInterfaces.TabIndex = 0;
            // 
            // checkBoxMulticast
            // 
            this.checkBoxMulticast.AutoSize = true;
            this.checkBoxMulticast.Checked = true;
            this.checkBoxMulticast.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxMulticast.Location = new System.Drawing.Point(346, 7);
            this.checkBoxMulticast.Name = "checkBoxMulticast";
            this.checkBoxMulticast.Size = new System.Drawing.Size(68, 17);
            this.checkBoxMulticast.TabIndex = 2;
            this.checkBoxMulticast.Text = "Multicast";
            this.checkBoxMulticast.UseVisualStyleBackColor = true;
            this.checkBoxMulticast.CheckedChanged += new System.EventHandler(this.checkBoxMulticast_CheckedChanged);
            // 
            // textBoxListeningPort
            // 
            this.textBoxListeningPort.Location = new System.Drawing.Point(199, 23);
            this.textBoxListeningPort.Name = "textBoxListeningPort";
            this.textBoxListeningPort.Size = new System.Drawing.Size(123, 20);
            this.textBoxListeningPort.TabIndex = 1;
            this.textBoxListeningPort.Text = "1234";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Network Interface:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(197, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Listening Port:";
            // 
            // textBoxMulticastAddress
            // 
            this.textBoxMulticastAddress.Location = new System.Drawing.Point(454, 23);
            this.textBoxMulticastAddress.Name = "textBoxMulticastAddress";
            this.textBoxMulticastAddress.Size = new System.Drawing.Size(123, 20);
            this.textBoxMulticastAddress.TabIndex = 3;
            this.textBoxMulticastAddress.Text = "224.0.0.1";
            // 
            // labelMulticastIp
            // 
            this.labelMulticastIp.AutoSize = true;
            this.labelMulticastIp.Location = new System.Drawing.Point(452, 7);
            this.labelMulticastIp.Name = "labelMulticastIp";
            this.labelMulticastIp.Size = new System.Drawing.Size(65, 13);
            this.labelMulticastIp.TabIndex = 12;
            this.labelMulticastIp.Text = "Multicast IP:";
            // 
            // tabPageInputFile
            // 
            this.tabPageInputFile.Controls.Add(this.progressBarPlay);
            this.tabPageInputFile.Controls.Add(this.checkBoxLoop);
            this.tabPageInputFile.Controls.Add(this.groupBoxBitrate);
            this.tabPageInputFile.Controls.Add(this.groupBoxInputFile);
            this.tabPageInputFile.Location = new System.Drawing.Point(4, 22);
            this.tabPageInputFile.Name = "tabPageInputFile";
            this.tabPageInputFile.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageInputFile.Size = new System.Drawing.Size(693, 142);
            this.tabPageInputFile.TabIndex = 2;
            this.tabPageInputFile.Text = "Input From File";
            this.tabPageInputFile.UseVisualStyleBackColor = true;
            // 
            // progressBarPlay
            // 
            this.progressBarPlay.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBarPlay.Location = new System.Drawing.Point(3, 114);
            this.progressBarPlay.Name = "progressBarPlay";
            this.progressBarPlay.Size = new System.Drawing.Size(687, 25);
            this.progressBarPlay.Step = 1;
            this.progressBarPlay.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBarPlay.TabIndex = 10;
            // 
            // checkBoxLoop
            // 
            this.checkBoxLoop.AutoSize = true;
            this.checkBoxLoop.Location = new System.Drawing.Point(401, 8);
            this.checkBoxLoop.Name = "checkBoxLoop";
            this.checkBoxLoop.Size = new System.Drawing.Size(50, 17);
            this.checkBoxLoop.TabIndex = 9;
            this.checkBoxLoop.Text = "Loop";
            this.checkBoxLoop.UseVisualStyleBackColor = true;
            // 
            // groupBoxBitrate
            // 
            this.groupBoxBitrate.Controls.Add(this.maskedTextBoxBitrate);
            this.groupBoxBitrate.Location = new System.Drawing.Point(1, 62);
            this.groupBoxBitrate.Name = "groupBoxBitrate";
            this.groupBoxBitrate.Size = new System.Drawing.Size(370, 53);
            this.groupBoxBitrate.TabIndex = 8;
            this.groupBoxBitrate.TabStop = false;
            this.groupBoxBitrate.Text = "Bitrate(bps):";
            // 
            // maskedTextBoxBitrate
            // 
            this.maskedTextBoxBitrate.HideSelection = false;
            this.maskedTextBoxBitrate.Location = new System.Drawing.Point(8, 22);
            this.maskedTextBoxBitrate.Mask = "00000000000000000000000000";
            this.maskedTextBoxBitrate.Name = "maskedTextBoxBitrate";
            this.maskedTextBoxBitrate.PromptChar = ' ';
            this.maskedTextBoxBitrate.Size = new System.Drawing.Size(168, 20);
            this.maskedTextBoxBitrate.TabIndex = 2;
            // 
            // groupBoxInputFile
            // 
            this.groupBoxInputFile.Controls.Add(this.buttonBrowseInput);
            this.groupBoxInputFile.Controls.Add(this.textBoxInputFile);
            this.groupBoxInputFile.Location = new System.Drawing.Point(3, 7);
            this.groupBoxInputFile.Name = "groupBoxInputFile";
            this.groupBoxInputFile.Size = new System.Drawing.Size(371, 49);
            this.groupBoxInputFile.TabIndex = 7;
            this.groupBoxInputFile.TabStop = false;
            this.groupBoxInputFile.Text = "Input File:";
            // 
            // buttonBrowseInput
            // 
            this.buttonBrowseInput.Location = new System.Drawing.Point(297, 20);
            this.buttonBrowseInput.Name = "buttonBrowseInput";
            this.buttonBrowseInput.Size = new System.Drawing.Size(67, 25);
            this.buttonBrowseInput.TabIndex = 1;
            this.buttonBrowseInput.Text = "Browse";
            this.buttonBrowseInput.UseVisualStyleBackColor = true;
            this.buttonBrowseInput.Click += new System.EventHandler(this.buttonBrowseInput_Click);
            // 
            // textBoxInputFile
            // 
            this.textBoxInputFile.Location = new System.Drawing.Point(6, 22);
            this.textBoxInputFile.Name = "textBoxInputFile";
            this.textBoxInputFile.Size = new System.Drawing.Size(285, 20);
            this.textBoxInputFile.TabIndex = 0;
            // 
            // tabControlOutput
            // 
            this.tabControlOutput.Controls.Add(this.tabPageOutput);
            this.tabControlOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlOutput.Location = new System.Drawing.Point(3, 177);
            this.tabControlOutput.Name = "tabControlOutput";
            this.tabControlOutput.SelectedIndex = 0;
            this.tabControlOutput.Size = new System.Drawing.Size(701, 255);
            this.tabControlOutput.TabIndex = 1;
            // 
            // tabPageOutput
            // 
            this.tabPageOutput.Controls.Add(this.groupBoxOutputToDevice);
            this.tabPageOutput.Controls.Add(this.groupBoxOutputToNetwork);
            this.tabPageOutput.Controls.Add(this.groupBoxOutputFile);
            this.tabPageOutput.Controls.Add(this.checkBoxOutputToNetwork);
            this.tabPageOutput.Controls.Add(this.checkBoxOutputToFile);
            this.tabPageOutput.Location = new System.Drawing.Point(4, 22);
            this.tabPageOutput.Name = "tabPageOutput";
            this.tabPageOutput.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOutput.Size = new System.Drawing.Size(693, 229);
            this.tabPageOutput.TabIndex = 0;
            this.tabPageOutput.Text = "Output";
            this.tabPageOutput.UseVisualStyleBackColor = true;
            // 
            // groupBoxOutputToDevice
            // 
            this.groupBoxOutputToDevice.Controls.Add(this.listBoxAllOutputDevices);
            this.groupBoxOutputToDevice.Controls.Add(this.checkBoxOutputToDevice);
            this.groupBoxOutputToDevice.Location = new System.Drawing.Point(8, 86);
            this.groupBoxOutputToDevice.Name = "groupBoxOutputToDevice";
            this.groupBoxOutputToDevice.Size = new System.Drawing.Size(371, 141);
            this.groupBoxOutputToDevice.TabIndex = 8;
            this.groupBoxOutputToDevice.TabStop = false;
            this.groupBoxOutputToDevice.Text = "Output To Device";
            // 
            // listBoxAllOutputDevices
            // 
            this.listBoxAllOutputDevices.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxAllOutputDevices.FormattingEnabled = true;
            this.listBoxAllOutputDevices.Location = new System.Drawing.Point(6, 39);
            this.listBoxAllOutputDevices.Name = "listBoxAllOutputDevices";
            this.listBoxAllOutputDevices.Size = new System.Drawing.Size(358, 95);
            this.listBoxAllOutputDevices.TabIndex = 5;
            // 
            // checkBoxOutputToDevice
            // 
            this.checkBoxOutputToDevice.AutoSize = true;
            this.checkBoxOutputToDevice.Location = new System.Drawing.Point(6, 22);
            this.checkBoxOutputToDevice.Name = "checkBoxOutputToDevice";
            this.checkBoxOutputToDevice.Size = new System.Drawing.Size(108, 17);
            this.checkBoxOutputToDevice.TabIndex = 0;
            this.checkBoxOutputToDevice.Text = "Output to device:";
            this.checkBoxOutputToDevice.UseVisualStyleBackColor = true;
            this.checkBoxOutputToDevice.CheckedChanged += new System.EventHandler(this.checkBoxOutputToDevice_CheckedChanged);
            // 
            // groupBoxOutputToNetwork
            // 
            this.groupBoxOutputToNetwork.Controls.Add(this.listBoxOutputNetworkInterfaces);
            this.groupBoxOutputToNetwork.Controls.Add(this.textBoxDestinationPort);
            this.groupBoxOutputToNetwork.Controls.Add(this.textBoxDestinationIP);
            this.groupBoxOutputToNetwork.Controls.Add(this.label1);
            this.groupBoxOutputToNetwork.Controls.Add(this.label5);
            this.groupBoxOutputToNetwork.Controls.Add(this.label4);
            this.groupBoxOutputToNetwork.Enabled = false;
            this.groupBoxOutputToNetwork.Location = new System.Drawing.Point(393, 30);
            this.groupBoxOutputToNetwork.Name = "groupBoxOutputToNetwork";
            this.groupBoxOutputToNetwork.Size = new System.Drawing.Size(295, 190);
            this.groupBoxOutputToNetwork.TabIndex = 7;
            this.groupBoxOutputToNetwork.TabStop = false;
            this.groupBoxOutputToNetwork.Text = "Output to network:";
            // 
            // listBoxOutputNetworkInterfaces
            // 
            this.listBoxOutputNetworkInterfaces.FormattingEnabled = true;
            this.listBoxOutputNetworkInterfaces.Location = new System.Drawing.Point(119, 77);
            this.listBoxOutputNetworkInterfaces.Name = "listBoxOutputNetworkInterfaces";
            this.listBoxOutputNetworkInterfaces.Size = new System.Drawing.Size(170, 95);
            this.listBoxOutputNetworkInterfaces.TabIndex = 14;
            // 
            // textBoxDestinationPort
            // 
            this.textBoxDestinationPort.Location = new System.Drawing.Point(119, 47);
            this.textBoxDestinationPort.Name = "textBoxDestinationPort";
            this.textBoxDestinationPort.Size = new System.Drawing.Size(170, 20);
            this.textBoxDestinationPort.TabIndex = 11;
            this.textBoxDestinationPort.Text = "1234";
            // 
            // textBoxDestinationIP
            // 
            this.textBoxDestinationIP.Location = new System.Drawing.Point(119, 17);
            this.textBoxDestinationIP.Name = "textBoxDestinationIP";
            this.textBoxDestinationIP.Size = new System.Drawing.Size(170, 20);
            this.textBoxDestinationIP.TabIndex = 10;
            this.textBoxDestinationIP.Text = "239.0.0.1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Network Interface:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Destination Port:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Destination IP:";
            // 
            // groupBoxOutputFile
            // 
            this.groupBoxOutputFile.Controls.Add(this.buttonBrowseOutput);
            this.groupBoxOutputFile.Controls.Add(this.textBoxOutputFile);
            this.groupBoxOutputFile.Location = new System.Drawing.Point(8, 30);
            this.groupBoxOutputFile.Name = "groupBoxOutputFile";
            this.groupBoxOutputFile.Size = new System.Drawing.Size(371, 49);
            this.groupBoxOutputFile.TabIndex = 6;
            this.groupBoxOutputFile.TabStop = false;
            this.groupBoxOutputFile.Text = "Output File:";
            // 
            // buttonBrowseOutput
            // 
            this.buttonBrowseOutput.Location = new System.Drawing.Point(297, 20);
            this.buttonBrowseOutput.Name = "buttonBrowseOutput";
            this.buttonBrowseOutput.Size = new System.Drawing.Size(67, 25);
            this.buttonBrowseOutput.TabIndex = 1;
            this.buttonBrowseOutput.Text = "Browse";
            this.buttonBrowseOutput.UseVisualStyleBackColor = true;
            this.buttonBrowseOutput.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // textBoxOutputFile
            // 
            this.textBoxOutputFile.Location = new System.Drawing.Point(6, 22);
            this.textBoxOutputFile.Name = "textBoxOutputFile";
            this.textBoxOutputFile.Size = new System.Drawing.Size(285, 20);
            this.textBoxOutputFile.TabIndex = 0;
            // 
            // checkBoxOutputToNetwork
            // 
            this.checkBoxOutputToNetwork.AutoSize = true;
            this.checkBoxOutputToNetwork.Location = new System.Drawing.Point(393, 7);
            this.checkBoxOutputToNetwork.Name = "checkBoxOutputToNetwork";
            this.checkBoxOutputToNetwork.Size = new System.Drawing.Size(114, 17);
            this.checkBoxOutputToNetwork.TabIndex = 0;
            this.checkBoxOutputToNetwork.Text = "Output to network:";
            this.checkBoxOutputToNetwork.UseVisualStyleBackColor = true;
            this.checkBoxOutputToNetwork.CheckedChanged += new System.EventHandler(this.checkBoxOutputToNetwork_CheckedChanged);
            // 
            // checkBoxOutputToFile
            // 
            this.checkBoxOutputToFile.AutoSize = true;
            this.checkBoxOutputToFile.Checked = true;
            this.checkBoxOutputToFile.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxOutputToFile.Location = new System.Drawing.Point(8, 7);
            this.checkBoxOutputToFile.Name = "checkBoxOutputToFile";
            this.checkBoxOutputToFile.Size = new System.Drawing.Size(89, 17);
            this.checkBoxOutputToFile.TabIndex = 0;
            this.checkBoxOutputToFile.Text = "Output to file:";
            this.checkBoxOutputToFile.UseVisualStyleBackColor = true;
            this.checkBoxOutputToFile.CheckedChanged += new System.EventHandler(this.checkBoxLocalFile_CheckedChanged);
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.buttonStop);
            this.panelBottom.Controls.Add(this.buttonStart);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBottom.Location = new System.Drawing.Point(3, 525);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(701, 54);
            this.panelBottom.TabIndex = 2;
            // 
            // buttonStop
            // 
            this.buttonStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStop.Enabled = false;
            this.buttonStop.Location = new System.Drawing.Point(617, 2);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(75, 25);
            this.buttonStop.TabIndex = 1;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonStart
            // 
            this.buttonStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStart.Location = new System.Drawing.Point(516, 3);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(87, 25);
            this.buttonStart.TabIndex = 0;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // panelStatus
            // 
            this.panelStatus.Controls.Add(this.labelStartTime);
            this.panelStatus.Controls.Add(this.labelDuration);
            this.panelStatus.Controls.Add(this.labelTotalSize);
            this.panelStatus.Controls.Add(this.textBoxStartTime);
            this.panelStatus.Controls.Add(this.textBoxDuration);
            this.panelStatus.Controls.Add(this.textBoxTotalSize);
            this.panelStatus.Controls.Add(this.textBoxBitrate);
            this.panelStatus.Controls.Add(this.labelBitrate);
            this.panelStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStatus.Location = new System.Drawing.Point(3, 438);
            this.panelStatus.Name = "panelStatus";
            this.panelStatus.Size = new System.Drawing.Size(701, 81);
            this.panelStatus.TabIndex = 3;
            // 
            // labelStartTime
            // 
            this.labelStartTime.Location = new System.Drawing.Point(350, 54);
            this.labelStartTime.Name = "labelStartTime";
            this.labelStartTime.Size = new System.Drawing.Size(113, 13);
            this.labelStartTime.TabIndex = 2;
            this.labelStartTime.Text = "Start Time:";
            // 
            // labelDuration
            // 
            this.labelDuration.Location = new System.Drawing.Point(6, 54);
            this.labelDuration.Name = "labelDuration";
            this.labelDuration.Size = new System.Drawing.Size(113, 13);
            this.labelDuration.TabIndex = 2;
            this.labelDuration.Text = "Duration(ms):";
            // 
            // labelTotalSize
            // 
            this.labelTotalSize.AutoSize = true;
            this.labelTotalSize.Location = new System.Drawing.Point(350, 14);
            this.labelTotalSize.Name = "labelTotalSize";
            this.labelTotalSize.Size = new System.Drawing.Size(88, 13);
            this.labelTotalSize.TabIndex = 2;
            this.labelTotalSize.Text = "Total Size(bytes):";
            // 
            // textBoxStartTime
            // 
            this.textBoxStartTime.Location = new System.Drawing.Point(469, 54);
            this.textBoxStartTime.Name = "textBoxStartTime";
            this.textBoxStartTime.Size = new System.Drawing.Size(133, 20);
            this.textBoxStartTime.TabIndex = 1;
            // 
            // textBoxDuration
            // 
            this.textBoxDuration.Location = new System.Drawing.Point(125, 54);
            this.textBoxDuration.Name = "textBoxDuration";
            this.textBoxDuration.Size = new System.Drawing.Size(133, 20);
            this.textBoxDuration.TabIndex = 1;
            // 
            // textBoxTotalSize
            // 
            this.textBoxTotalSize.Location = new System.Drawing.Point(470, 14);
            this.textBoxTotalSize.Name = "textBoxTotalSize";
            this.textBoxTotalSize.Size = new System.Drawing.Size(133, 20);
            this.textBoxTotalSize.TabIndex = 1;
            // 
            // textBoxBitrate
            // 
            this.textBoxBitrate.Location = new System.Drawing.Point(126, 14);
            this.textBoxBitrate.Name = "textBoxBitrate";
            this.textBoxBitrate.Size = new System.Drawing.Size(133, 20);
            this.textBoxBitrate.TabIndex = 1;
            // 
            // labelBitrate
            // 
            this.labelBitrate.Location = new System.Drawing.Point(6, 14);
            this.labelBitrate.Name = "labelBitrate";
            this.labelBitrate.Size = new System.Drawing.Size(113, 13);
            this.labelBitrate.TabIndex = 0;
            this.labelBitrate.Text = "Bitrate(bps):";
            // 
            // timerToUpdateUI
            // 
            this.timerToUpdateUI.Interval = 1000;
            this.timerToUpdateUI.Tick += new System.EventHandler(this.timerToUpdateUI_Tick);
            // 
            // FormGateway
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 582);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormGateway";
            this.Text = "Media Gateway";
            this.Shown += new System.EventHandler(this.FormRecord_Shown);
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tabControlInput.ResumeLayout(false);
            this.tabPageInputDevice.ResumeLayout(false);
            this.groupBoxDeviceList.ResumeLayout(false);
            this.tabPageInputNetwork.ResumeLayout(false);
            this.tabPageInputNetwork.PerformLayout();
            this.tabPageInputFile.ResumeLayout(false);
            this.tabPageInputFile.PerformLayout();
            this.groupBoxBitrate.ResumeLayout(false);
            this.groupBoxBitrate.PerformLayout();
            this.groupBoxInputFile.ResumeLayout(false);
            this.groupBoxInputFile.PerformLayout();
            this.tabControlOutput.ResumeLayout(false);
            this.tabPageOutput.ResumeLayout(false);
            this.tabPageOutput.PerformLayout();
            this.groupBoxOutputToDevice.ResumeLayout(false);
            this.groupBoxOutputToDevice.PerformLayout();
            this.groupBoxOutputToNetwork.ResumeLayout(false);
            this.groupBoxOutputToNetwork.PerformLayout();
            this.groupBoxOutputFile.ResumeLayout(false);
            this.groupBoxOutputFile.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.panelStatus.ResumeLayout(false);
            this.panelStatus.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.TabControl tabControlInput;
        private System.Windows.Forms.TabPage tabPageInputDevice;
        private System.Windows.Forms.TabPage tabPageInputNetwork;
        private System.Windows.Forms.TabControl tabControlOutput;
        private System.Windows.Forms.TabPage tabPageOutput;
        private System.Windows.Forms.TextBox textBoxListeningPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxMulticastAddress;
        private System.Windows.Forms.Label labelMulticastIp;
        private System.Windows.Forms.CheckBox checkBoxMulticast;
        private System.Windows.Forms.CheckBox checkBoxOutputToFile;
        private System.Windows.Forms.GroupBox groupBoxOutputFile;
        private System.Windows.Forms.Button buttonBrowseOutput;
        protected System.Windows.Forms.TextBox textBoxOutputFile;
        private System.Windows.Forms.CheckBox checkBoxOutputToNetwork;
        private System.Windows.Forms.GroupBox groupBoxOutputToNetwork;
        private System.Windows.Forms.TextBox textBoxDestinationPort;
        private System.Windows.Forms.TextBox textBoxDestinationIP;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Panel panelStatus;
        private System.Windows.Forms.Label labelBitrate;
        private System.Windows.Forms.TextBox textBoxBitrate;
        private System.Windows.Forms.Timer timerToUpdateUI;
        private System.Windows.Forms.Label labelTotalSize;
        private System.Windows.Forms.TextBox textBoxTotalSize;
        private System.Windows.Forms.Label labelDuration;
        private System.Windows.Forms.TextBox textBoxDuration;
        private System.Windows.Forms.Label labelStartTime;
        private System.Windows.Forms.TextBox textBoxStartTime;
        private System.Windows.Forms.GroupBox groupBoxDeviceList;
        private System.Windows.Forms.ListBox listBoxAllInputDevices;
        private System.Windows.Forms.TabPage tabPageInputFile;
        private System.Windows.Forms.GroupBox groupBoxInputFile;
        private System.Windows.Forms.Button buttonBrowseInput;
        protected System.Windows.Forms.TextBox textBoxInputFile;
        private System.Windows.Forms.GroupBox groupBoxBitrate;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxBitrate;
        private System.Windows.Forms.CheckBox checkBoxLoop;
        private System.Windows.Forms.ProgressBar progressBarPlay;
        private System.Windows.Forms.GroupBox groupBoxOutputToDevice;
        private System.Windows.Forms.CheckBox checkBoxOutputToDevice;
        private System.Windows.Forms.ListBox listBoxAllOutputDevices;
        private System.Windows.Forms.Label label1;
        private InActionLibrary.NetworkInterfaceListBox listBoxOutputNetworkInterfaces;
        private InActionLibrary.NetworkInterfaceListBox listBoxInputNetworkInterfaces;
        private System.Windows.Forms.Label label2;
    }
}