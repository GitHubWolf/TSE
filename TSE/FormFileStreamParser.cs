using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using InActionLibrary;


namespace TSE
{
    public partial class FormFileStreamParser : Form, StreamParserContext
    {
        private TimeSpan timeStart;
        private TimeSpan timeStop;

        private String streamFile = null;

        //To indicate whether this form is being closed.
        private bool isClosing = false;

        //Some top level nodes.
        private TreeNode rootNode = null;
        private TreeNode psiNode = null;
        private TreeNode siNode = null;
        private TreeNode pidNode = null;

        //SectionParser is used to parse the section and display the result in the Form.
        private DataParser dataParser = null;

        //Delegate function prototype.
        private delegate void AddLogDelegate(string log);

        //To save current DataStore of selected FieldItem. In case it is changed, we will need to display the hex string.
        private DataStore selectedDataStore = null;

        //Keep a reference to the PID list.
        private SortedDictionary<UInt16, PidProfile> pidList = null;

        //FileStreamReader to read file stream and parse.
        private WorkerFileRoutineParsing fileStreamParser = null;

        private TreeNode searchNode = null; //Node selected to search data.
        private SearchRequest searchRequest = null;//To save detail search parameters.

        //Save all the message notification from stream parser.
        private Queue<MessageNotification> messageQueue = new Queue<MessageNotification>();

        //A reference to the pid bitrate list.
        private SortedDictionary<UInt16, MuxBitrate> muxBitrateList = null;

        //Form to show realtime bitrate.
        FormPidBitrate realtimeBitrateForm = new FormPidBitrate();

        //TS packet size.
        private int tsPacketSize = 0;

        public FormFileStreamParser()
        {
            InitializeComponent();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void SetStreamFile(String streamFileName)
        {
            streamFile = streamFileName;
        }

        public void WriteLog(string log)
        {
            //In case the form is being closed, we will do nothing.
            if (!isClosing)
            {
                //if (textBoxLogger.InvokeRequired)
                richTextBoxLogger.BeginInvoke(new AddLogDelegate(AddLog), log);
            }
        }

        private void AddLog(string log)
        {
            richTextBoxLogger.AppendText(log);
        }

        /**Form_Shown is used instead of Form_Load, because the form may be closed in case it failed to open the file.
         * But in Form_Load, we will not be able to call the Close() to close the form, since the handle hasn't been created at this step.
         */
        private void StreamParserForm_Shown(object sender, EventArgs e)
        {
            Result result = new Result();

            //Set the file name as the title.
            this.Text = streamFile;

            //Create a file stream reader to read the file.
            fileStreamParser = new WorkerFileRoutineParsing(this, ProcessWorkerMessage);

            //On stream start. Initialize the environment.
            OnStreamStart();

            //Start to parse it. A thread will be created.
            result = fileStreamParser.StartParse(streamFile);//To do normal SI/PSI parsing.

            if (!result.Fine)
            {
                MessageBox.Show("Failed to open the file. Check whether the file exists or it is being occupied by other application.\n", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                //Something is wrong.Close the form directly.
                Close();
            }
            else
            {
                //Create default nodes.
                CreateDefaultNodes();

                //Create SectionParser to parse and show all kinds of sections.
                dataParser = new DataParser(this);
            }
        }

        private void OnStreamStart()
        {
            toolStripStatusLabel1.Text = "Ongoing";
            toolStripProgressBar1.Value = 0;

            treeViewParser.BeginUpdate();            

            timeStart = new TimeSpan(DateTime.Now.Ticks);//Record start time.
            //Enable the timer to update the data.
            timerToUpdateData.Enabled = true;
        }

        private void OnStreamStop()
        {
            toolStripStatusLabel1.Text = "Done";
            toolStripProgressBar1.Value = 100;

            timerToUpdateData.Enabled = false;//Stop the timer now.
            timeStop = new TimeSpan(DateTime.Now.Ticks);//Record stop time.
            
            PrintDuration();

            treeViewParser.EndUpdate();
        }

        private void StreamParserForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            isClosing = true;

            if (null != fileStreamParser)
            {
                fileStreamParser.StopParse();
            }
        }

        //Callback function to process messages from the parser thread.
        private void ProcessWorkerMessage(MessageId messageId, object message)
        {
            if (!isClosing)
            {
                //Now, we are using timer to update the data to the form, so we don't need to use delegate any more.
                PushMessageIntoQueue(messageId, message);
                /*
                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new MessageHandlerDelegate(PushMessageIntoQueue), messageId, message);
                }
                else
                {
                    PushMessageIntoQueue(messageId, message);
                }
                 * */
            }
        }

        private void PushMessageIntoQueue(MessageId messageId, object message)
        {
            lock (this)
            {
                //Put the message into the queue.
                messageQueue.Enqueue(new MessageNotification(messageId, message));
            }            
        }
        
        //Real message handler to process all kinds of messages.
        private void ProcessStreamParserMessage(MessageId messageType, object message)
        {

            //Console.WriteLine("MessageType: " + messageType);
            switch (messageType)
            {
                case MessageId.MESSAGE_TS_PACKET_SIZE:
                    {
                        tsPacketSize = (int)message;
                        break;
                    }
                case MessageId.MESSAGE_STANDARD_SECTION:
                    {
                        DisplayStandardSection((DataStore)message);
                        break;
                    }
                case MessageId.MESSAGE_NEW_PROGRESS:
                    {
                        DisplayProgress((Int64)message);
                        break;
                    }
                case MessageId.MESSAGE_PARSING_DONE:
                    {
                        UpdateAfterDone();

                        //On stream stop. Finalize the environment.
                        OnStreamStop();
                        break;
                    }
                case MessageId.MESSAGE_PID_LIST:
                    {
                        DisplayPidList((SortedDictionary<UInt16, PidProfile>)message);

                        //Save the reference to the PID list.
                        pidList = (SortedDictionary<UInt16, PidProfile>)message;
                        break;
                    }
                case MessageId.MESSAGE_MUX_BITRATE:
                    {
                        this.muxBitrateList = (SortedDictionary<UInt16, MuxBitrate>)message;
                        break;
                    }
                case MessageId.MESSAGE_SEARCHED_DATA:
                    {
                        DisplaySearchedData((DataStore)message);
                        break;
                    }
                case MessageId.MESSAGE_SEARCH_DONE:
                    {
                        //Inform the plugin that the search has completed.
                        searchRequest.SelectedParser.Stop();

                        UpdateAfterSearch();

                        //On stream stop. Finalize the environment.
                        OnStreamStop();
                        break;
                    }
                case MessageId.MESSAGE_PID_BITRATE_DATA:
                    {
                        //To show the bitrate in the form.
                        realtimeBitrateForm.AddRealtimeBitrate((KeyValuePair<DateTime, SortedDictionary<UInt16, PidBitrate>>)message, false);
                        break;
                    }
                case MessageId.MESSAGE_MEASURE_PID_BITRATE_DONE:
                case MessageId.MESSAGE_PID_UPDATE_DONE:
                case MessageId.MESSAGE_PID_DUMP_DONE:
                case MessageId.MESSAGE_DESCRABLING_DONE:
                case MessageId.MESSAGE_SCRAMBLING_DONE:
                    {
                        //On stream stop. Finalize the environment.
                        OnStreamStop();
                        break;
                    }
            }
        }

/*
Value Description
0x00 program_association_section
0x01 conditional_access_section
0x02 program_map_section
0x03 transport_stream_description_section
0x04 to 0x3F reserved
0x40 network_information_section - actual_network
0x41 network_information_section - other_network
0x42 service_description_section - actual_transport_stream
0x43 to 0x45 reserved for future use
0x46 service_description_section - other_transport_stream
0x47 to 0x49 reserved for future use
0x4A bouquet_association_section
0x4B to 0x4D reserved for future use
0x4E event_information_section - actual_transport_stream, present/following
0x4F event_information_section - other_transport_stream, present/following
0x50 to 0x5F event_information_section - actual_transport_stream, schedule
0x60 to 0x6F event_information_section - other_transport_stream, schedule
0x70 time_date_section
0x71 running_status_section
0x72 stuffing_section
0x73 time_offset_section
0x74 to 0x7D reserved for future use
0x7E discontinuity_information_section
0x7F selection_information_section
0x80 to 0xFE user defined
0xFF reserved
*/
        private void DisplayStandardSection(DataStore section)
        {
            //Check table_id, so that we can know its parent node.
            byte[] sectionData = section.GetData();

            //Important!!!!!!!!!!!!!!!!!
            section.Reset();

            //To avoid too much UI update, so that we can get better UI experience.
            //treeViewParser.BeginUpdate();

            switch (sectionData[0])
            {
                case (byte)TableId.PAT://0x00 program_association_section
                    {
                        dataParser.ParsePAT(treeViewParser, psiNode, section);
                        break;
                    }
                case (byte)TableId.CAT://0x01 conditional_access_section
                    {
                        dataParser.ParseCAT(treeViewParser, psiNode, section);
                        break;
                    }
                case (byte)TableId.PMT://0x02 program_map_section
                    {
                        dataParser.ParsePMT(treeViewParser, psiNode, section);
                        break;
                    }
                case (byte)TableId.TSDT://0x03 transport_stream_description_section
                    {
                        dataParser.ParseTSDT(treeViewParser, psiNode, section);
                        break;
                    }
                case (byte)TableId.NIT_ACTUAL://0x40 network_information_section - actual_network
                    {
                        dataParser.ParseNITActual(treeViewParser, psiNode, section);
                        break;
                    }
                case (byte)TableId.NIT_OTHER://0x41 network_information_section - other_network
                    {
                        dataParser.ParseNITOther(treeViewParser, psiNode, section);
                        break;
                    }
                case (byte)TableId.SDT_ACTUAL://0x42 service_description_section - actual_transport_stream
                    {
                        dataParser.ParseSDTActual(treeViewParser, siNode, section);
                        break;
                    }
                case (byte)TableId.SDT_OTHER://0x46 service_description_section - other_transport_stream
                    {
                        dataParser.ParseSDTOther(treeViewParser, siNode, section);
                        break;
                    }
                case (byte)TableId.BAT://0x4A bouquet_association_section
                    {
                        dataParser.ParseBAT(treeViewParser, siNode, section);
                        break;
                    }
                case (byte)TableId.EIT_PF_ACTUAL://0x4E event_information_section - actual_transport_stream, present/following
                    {
                        dataParser.ParseEITActualPF(treeViewParser, siNode, section);
                        break;
                    }
                case (byte)TableId.EIT_PF_OTHER://0x4F event_information_section - other_transport_stream, present/following
                    {
                        dataParser.ParseEITOtherPF(treeViewParser, siNode, section);
                        break;
                    }
                case (byte)TableId.TDT://0x70 time_date_section
                    {
                        dataParser.ParseTDT(treeViewParser, siNode, section);
                        break;
                    }
                case (byte)TableId.RST://0x71 running_status_section
                    {
                        dataParser.ParseRST(treeViewParser, siNode, section);
                        break;
                    }
                case (byte)TableId.TOT://0x73 time_offset_section
                    {
                        dataParser.ParseTOT(treeViewParser, siNode, section);
                        break;
                    }
                case (byte)TableId.DIT://0x7E discontinuity_information_section
                    {
                        dataParser.ParseDIT(treeViewParser, siNode, section);
                        break;
                    }
                case (byte)TableId.SIT://0x7F selection_information_section
                    {
                        dataParser.ParseSIT(treeViewParser, siNode, section);
                        break;
                    }
                default:
                    {
                        if ((sectionData[0] & 0xF0) == (byte)TableId.EIT_SCHEDULE_ACTUAL)
                        { 
                            //0x50 to 0x5F event_information_section - actual_transport_stream, schedule
                            dataParser.ParseEITActualSchedule(treeViewParser, siNode, section);
                        }
                        else if ((sectionData[0] & 0xF0) == (byte)TableId.EIT_SCHEDULE_OTHER)
                        {
                            //0x60 to 0x6F event_information_section - other_transport_stream, schedule
                            dataParser.ParseEITOtherSchedule(treeViewParser, siNode, section);
                        }
                        break;
                    }
            }

            //treeViewParser.EndUpdate();
        }

        private void DisplaySearchedData(DataStore dataFound)
        {
            //Check table_id, so that we can know its parent node.
            byte[] dataToShow = dataFound.GetData();

            //Important!!!!!!!!!!!!!!!!!
            dataFound.Reset();

            //To avoid too much UI update, so that we can get better UI experience.
            //treeViewParser.BeginUpdate();

            //Call the plugin to parse the data.
            searchRequest.SelectedParser.Parse(searchNode, dataFound);

            //treeViewParser.EndUpdate();
        }

        private void CreateDefaultNodes()
        {
            rootNode = Utility.AddChildNode(treeViewParser, null, "Root", streamFile, ItemType.ROOT);
            psiNode = Utility.AddChildNode(treeViewParser, rootNode, "PSI", "PSI Tables[Program Specific Information]", ItemType.PSI);
            siNode = Utility.AddChildNode(treeViewParser, rootNode, "SI", "SI Tables[Service Information]", ItemType.SI);
            pidNode = Utility.AddChildNode(treeViewParser, rootNode, "PID", "PID List", ItemType.PID);

            ExpandToTop(rootNode);
        }

        private void treeViewParser_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode selectedNode = e.Node;
            FieldItem fieldItem = null;

            if (null != selectedNode)
            {
                if (selectedNode.Tag is FieldItem)
                {
                    fieldItem = (FieldItem)selectedNode.Tag;
                }
            }

            int binarySelectStart = 0;
            int binarySelectLength = 0;

            if (null != fieldItem)
            {
                if (selectedDataStore != fieldItem.GetDataStore())
                {
                    richTextBoxHexString.Clear();

                    //Display the hex string.
                    richTextBoxHexString.Text = fieldItem.GetDataStoreHexString();

                    //Set it as current selected.
                    selectedDataStore = fieldItem.GetDataStore();
                }

                //Select the bytes for this field.
                Int64 offsetStart = fieldItem.Offset / 8;
                Int64 offsetEnd = (fieldItem.Offset + fieldItem.Length) / 8;

                //Block start and block end.
                Int64 blockStart = fieldItem.Offset / 64; //To calculate the count of Environment.NewLine.
                Int64 blockEnd = (fieldItem.Offset + fieldItem.Length) / 64;//To calculate the count of Environment.NewLine.

                if (0 != (fieldItem.Offset + fieldItem.Length) % 8)
                {
                    offsetEnd++;
                }

                if (0 == ((fieldItem.Offset + fieldItem.Length) % 64))
                {
                    blockEnd--;
                }

                richTextBoxHexString.Select((int)(offsetStart * 3 + blockStart), (int)((offsetEnd - offsetStart) * 3 + (blockEnd - blockStart)));
                richTextBoxHexString.SelectionColor = Color.Red;
                //richTextBoxHexString.ScrollToCaret();

                //To show the detail field property as below.
                /*
                Binary  :10100010 11000011 00101001 01000001 
                Heximal :   A2C32941
                Decimal : 2730699073
                Length  :          4 byte
                Length  :         32 bit
                Position:         12 byte
                Position:         96 bit
                 */
                String binaryStr = null;
                String hexStr = null;
                String decimalStr = null;

                //We'll show the fieldValue only when the bit length is less then 64.
                if ((64 >= fieldItem.Length) && (fieldItem.Type == ItemType.FIELD))
                {
                    Int64 bytesValue = 0;
                    byte[] data = selectedDataStore.GetData();

                    for (Int64 i = offsetStart; i < offsetEnd; i++)
                    {
                        bytesValue = bytesValue << 8;
                        bytesValue = bytesValue | data[i];
                    }
                    String binary = "Binary  :";
                    binaryStr = binary + Utility.GetValueBinaryString(bytesValue, (offsetEnd - offsetStart) * 8);
                    hexStr = "Heximal :" + Utility.GetValueHexString(fieldItem.Value, fieldItem.Length);
                    decimalStr = "Decimal :" + Utility.GetValueDecimalString(fieldItem.Value, fieldItem.Length);

                    //Select those bits belonged to this field.
                    Int64 spaceCount = (fieldItem.Offset + fieldItem.Length) / 8 - (fieldItem.Offset) / 8;

                    String lengthPositionStr = String.Format("Length  :{0}-byte" + Environment.NewLine
                                                            + "Length  :{1}-bit" + Environment.NewLine
                                                            + "Offset  :{2}-byte" + Environment.NewLine
                                                            + "Offset  :{3}-bit",
                                                            offsetEnd - offsetStart,
                                                            fieldItem.Length,
                                                            fieldItem.Offset / 8,
                                                            fieldItem.Offset);

                    richTextBoxProperty.Text = binaryStr + Environment.NewLine
                                                + hexStr + Environment.NewLine
                                                + decimalStr + Environment.NewLine
                                                + lengthPositionStr + Environment.NewLine;

                    binarySelectStart = (int)(binary.Length + fieldItem.Offset % 8);
                    binarySelectLength = (int)(fieldItem.Length + spaceCount);
                }
                else
                {
                    String lengthPositionStr = String.Format("Length  :{0}-byte" + Environment.NewLine
                                                           + "Length  :{1}-bit" + Environment.NewLine
                                                           + "Offset  :{2}-byte" + Environment.NewLine
                                                           + "Offset  :{3}-bit",
                                                           offsetEnd - offsetStart,
                                                           fieldItem.Length,
                                                           fieldItem.Offset / 8,
                                                           fieldItem.Offset);

                    richTextBoxProperty.Text = binaryStr + Environment.NewLine
                                                + hexStr + Environment.NewLine
                                                + decimalStr + Environment.NewLine
                                                + lengthPositionStr + Environment.NewLine;
                }


                //Show the repeat number.
                String repeatStr = "Repeat  :" + selectedDataStore.GetRepeatTimes();
                richTextBoxProperty.AppendText(repeatStr + Environment.NewLine);

                //Show the packet metadata.
                List<TsPacketMetadata> packetMetadataList = selectedDataStore.GetPacketMetadataList();
                richTextBoxProperty.AppendText("Packet Count: " + packetMetadataList.Count + Environment.NewLine);
                String packetMetadataStr = "(Packet Number,File Offset): ";
                foreach (TsPacketMetadata packetMetadata in packetMetadataList)
                {
                    packetMetadataStr += " (" + packetMetadata.PacketNumber + "," + packetMetadata.FileOffset + ")";
                }
                richTextBoxProperty.AppendText(packetMetadataStr + Environment.NewLine);

                if (binarySelectLength != 0)
                {
                    //Select the binary bits finally. We can't make the selection and then insert new sectionDetail. It will not work in such case.

                    richTextBoxProperty.Select(binarySelectStart, binarySelectLength);
                    richTextBoxProperty.SelectionColor = Color.Red;
                    //richTextBoxProperty.ScrollToCaret();
                }
            }//null != fieldItem
        }

        private void FileStreamParserForm_Resize(object sender, EventArgs e)
        {
            //Note that the autosize property of progressbar should be disabled!!!!!!!!!!!!!!
            toolStripProgressBar1.Width = (int)(this.Width * 0.9);
        }

        private void DisplayProgress(Int64 progress)
        {
            toolStripStatusLabel1.Text = String.Format("{0}%", (int)progress);
            toolStripProgressBar1.Value = (int)progress;

            Console.WriteLine("Current progress is " + progress);
        }

        private void UpdateAfterDone()
        {
            UpdateChildCount(psiNode);
            UpdateChildCount(siNode);

            //UpdateAfterDone(pidNode);
            //Special update for PID list.
            pidNode.Text = pidNode.Text + "(PID Count: " + pidNode.Nodes.Count + ")";

            ExpandToTop(psiNode);
            ExpandToTop(siNode);
            ExpandToTop(pidNode);
        }

        private void UpdateAfterSearch()
        {
            searchNode.Text = searchNode.Text + "(Found: " + searchNode.Nodes.Count + ")";

            Int64 id = 0;
            foreach (TreeNode oneNode in searchNode.Nodes)
            {
                id++;
                oneNode.Text = oneNode.Text + "(ID:" + id + ")";
            }

            ExpandToTop(searchNode);

            treeViewParser.SelectedNode = searchNode;
        }

        private void UpdateChildCount(TreeNode topLevelNode)
        {
            if (null != topLevelNode)
            {
                foreach (TreeNode categoryNode in topLevelNode.Nodes) //To access every second level nodes, like "PAT", "PMT", "CAT" etc.
                {
                    Int64 id = 0;
                    categoryNode.Text = categoryNode.Text + "(Count: " +categoryNode.Nodes.Count + ")";

                    foreach (TreeNode sectionNode in categoryNode.Nodes) //Each section.
                    {
                        id++;
                        FieldItem item = (FieldItem)sectionNode.Tag;
                        if (null != item)
                        {
                            sectionNode.Text = sectionNode.Text + "(Repeat:" + item.GetDataStore().GetRepeatTimes() + ")" + "(ID:" + id + ")";
                        }
                    }
                }
            }
        }

        private void DisplayPidList(SortedDictionary<UInt16, PidProfile> pidList)
        {
            foreach (KeyValuePair<UInt16, PidProfile> pair in pidList)
            {
                if (0 != pair.Value.PacketCount)
                {
                    TreeNode newNode = pidNode.Nodes.Add(GetPidKey(pair.Key), pair.Value.ToString(), Utility.GetItemPicture(ItemType.PID_ITEM), Utility.GetItemPicture(ItemType.PID_ITEM));
                    newNode.Tag = (PidProfile)pair.Value;
                }
            }
        }

        private void treeViewParser_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                Point currentPoint = new Point(e.X, e.Y);
                TreeNode currentNode = treeViewParser.GetNodeAt(currentPoint);

                if (null != currentNode)
                {
                    treeViewParser.SelectedNode = currentNode;

                    //Search context menu will be shown only when we select a PID item.
                    if (currentNode.Tag is PidProfile)
                    {
                        treeViewParser.ContextMenuStrip = contextMenuStripSearch;
                    }
                    else
                    {
                        //No context menu in case other nodes are selected.
                        treeViewParser.ContextMenuStrip = null;
                    }

                }

            }
        }

        private void toolStripMenuItemSearch_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeViewParser.SelectedNode;

            if (selectedNode != null)
            {
                if (selectedNode.Tag is PidProfile)
                {
                    PidProfile currentPidProfile = (PidProfile)selectedNode.Tag;
                    RequestToSearch(currentPidProfile);
                }
            }
        }

        private void RequestToSearch(PidProfile currentPidProfile)
        {
            //Create search form.
            FormSearch searchForm = new FormSearch();

            //Pass PID list to the search form so that it can display the PID for user selection.
            searchForm.PidList = this.pidList;
            searchForm.SetSelectedPidProfile(currentPidProfile);

            DialogResult dialogResult = searchForm.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                searchRequest = searchForm.GetSearchRequest();
                //Call the start to notify the plugin that we have started the search.
                searchRequest.SelectedParser.Start();

                //Create a new node to indicate the search.
                CreateSearchNode(searchRequest);

                WorkerFileDataSearch dataSearcher = new WorkerFileDataSearch(this, ProcessWorkerMessage, searchRequest);

                //On stream start. Initialize the environment.
                OnStreamStart();

                Result result = dataSearcher.StartParse(streamFile);

                if (result.Fine)
                {
                }
                else
                {
                    //Something unexpected happened, we will cancel the search.
                    searchRequest.SelectedParser.Stop();

                    //On stream stop. Finalize the environment.
                    OnStreamStop();
                }
            }
        }

        private void timerToUpdateData_Tick(object sender, EventArgs e)
        {
            MessageNotification notification = null;
            TimeSpan startTime = new TimeSpan(DateTime.Now.Ticks);
            int messageCount = messageQueue.Count;


            //treeViewParser.BeginUpdate();

            while (0 != messageCount)
            {
                lock (this)
                {
                    notification = messageQueue.Dequeue();
                }

                if (null != notification)
                {
                    ProcessStreamParserMessage(notification.ID, notification.Payload);
                    messageCount--;
                }
                else
                {
                    break;
                }

                //Console.WriteLine("messageCount " + messageCount + "----------------timerToUpdateData_Tick Message Count:" + messageQueue.Count);
            }

            //treeViewParser.EndUpdate();
        }//Timer

        private void PrintDuration()
        {
            TimeSpan duration = timeStop.Subtract(timeStart).Duration();
            WriteLog("Time used for parsing: " + duration.ToString() + Environment.NewLine);
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null != fileStreamParser)
            {
                fileStreamParser.StopParse();
                fileStreamParser = null;
            }

            //Clean up all the left messages notifications, except for the last several ones.
            while (messageQueue.Count > 10)
            {
                messageQueue.Dequeue();//Pop out message.
            }
        }
        private void CreateSearchNode(SearchRequest searchRequest)
        {

            if (searchRequest.SelectedPid == PidProfile.SUPER_PID)
            {
                searchNode = pidNode.Nodes.Add(searchRequest.ToString(), searchRequest.ToString(), Utility.GetItemPicture(ItemType.SEARCH_REQUEST), Utility.GetItemPicture(ItemType.SEARCH_REQUEST));
            }
            else
            {
                TreeNode[] nodes = pidNode.Nodes.Find(GetPidKey(searchRequest.SelectedPid), false);
                TreeNode pidNodeFound = nodes[0];

                searchNode = pidNodeFound.Nodes.Add(searchRequest.ToString(), searchRequest.ToString(), Utility.GetItemPicture(ItemType.SEARCH_REQUEST), Utility.GetItemPicture(ItemType.SEARCH_REQUEST));
            }
        }

        private string GetPidKey(UInt16 pid)
        {
            return "PID" + Utility.GetValueHexString(pid, 16);
        }

        private void ExpandToTop(TreeNode newNode)
        {
            while (null != newNode)
            {
                newNode.Expand();

                newNode = newNode.Parent;
            }
        }

        private void bitrateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormMuxBitrate streamBitrateForm = new FormMuxBitrate();
            streamBitrateForm.SetStreamBitrateList(this.muxBitrateList);
            streamBitrateForm.Show();
        }

        private void streamBitrateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormMuxBitrate formMuxBitrate = new FormMuxBitrate();
            formMuxBitrate.ForceEnterBitrate(); //Force the user to select a bitrate.
            formMuxBitrate.SetStreamBitrateList(this.muxBitrateList);

            //Show a message so that the user can be aware of what will happen.
            MessageBox.Show(null, "Please select a bitrate or enter a new bitrate in the next dialog.",
                "Identify a bitrate!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //Reuse StreamBitrateForm as the user's choice.
            if (DialogResult.OK == formMuxBitrate.ShowDialog())
            { 
                //Create the file stream parser and control it with requested bitrate.
                WorkerFilePidBitrate workerPidBitrate = new WorkerFilePidBitrate(this,ProcessWorkerMessage);

                //On stream start. Initialize the environment.
                OnStreamStart();

                //Pass the bitrate to the file stream parser.
                workerPidBitrate.SetStreamBitrate(formMuxBitrate.GetSelectedBitrate());

                Result result = workerPidBitrate.StartParse(streamFile);

                if (result.Fine)
                {
                    realtimeBitrateForm.ResetData();
                    realtimeBitrateForm.ShowDialog();
                }
                else
                {
                    //On stream stop. Finalize the environment.
                    OnStreamStop();
                }

            }
        }//streamBitrateToolStripMenuItem_Click

        private void pIDUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormLocalStreamUpdate pidUpdateForm = new FormLocalStreamUpdate();
            pidUpdateForm.PidListIn = this.pidList;
            if (DialogResult.OK == pidUpdateForm.ShowDialog())
            {
                //Create the file stream parser to go through the whole stream.
                WorkerFilePidUpdate workerPidUpdate = new WorkerFilePidUpdate(this, ProcessWorkerMessage);                

                //On stream start. Initialize the environment.
                OnStreamStart();

                //Set update info to the file stream parser.
                workerPidUpdate.SetPidUpdateInfo(pidUpdateForm.PidListOut, pidUpdateForm.OutputFileName);

                Result result = workerPidUpdate.StartParse(streamFile);

                if (result.Fine)
                {

                }
                else
                {
                    //On stream stop. Finalize the environment.
                    OnStreamStop();
                }
            }
        }//pIDUpdateToolStripMenuItem_Click

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RequestToSearch(null);
        }

        private void pIDDumpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormLocalStreamDump pidDumpForm = new FormLocalStreamDump();
            pidDumpForm.PidListIn = this.pidList;
            if (DialogResult.OK == pidDumpForm.ShowDialog())
            {
                //Create the file stream parser to go through the whole stream.
                WorkerFilePidDump workerPidDump = new WorkerFilePidDump(this, ProcessWorkerMessage);

                //On stream start. Initialize the environment.
                OnStreamStart();

                //Set dump info to the file stream parser.
                workerPidDump.SetPidDumpInfo(pidDumpForm.PidListOut, pidDumpForm.OutputFileName, pidDumpForm.Percentage, pidDumpForm.Enforce188Bytes);

                Result result = workerPidDump.StartParse(streamFile);

                if (result.Fine)
                {

                }
                else
                {
                    //On stream stop. Finalize the environment.
                    OnStreamStop();
                }
            }
        }

        private void descramblerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormLocalStreamDescrambler descramblerForm = new FormLocalStreamDescrambler();
            descramblerForm.PidListIn = this.pidList;

            if (DialogResult.OK == descramblerForm.ShowDialog())
            {
                byte[] byteArray = null;
                bool isHexValid = false;
                int dataLength = 0;


                isHexValid = descramblerForm.GetControlWordSerials(out byteArray, ref dataLength);

                //Create the file stream parser to go through the whole stream.
                WorkerFileDescrambler workerDescrambler = new WorkerFileDescrambler(this, ProcessWorkerMessage);

                //On stream start. Initialize the environment.
                OnStreamStart();

                //Set descrambler info to the file stream parser.
                workerDescrambler.SetDescramblerInfo(descramblerForm.PidListOut, descramblerForm.OutputFileName, descramblerForm.DoEntropy,byteArray,dataLength);

                Result result = workerDescrambler.StartParse(streamFile);

                if (result.Fine)
                {

                }
                else
                {
                    //On stream stop. Finalize the environment.
                    OnStreamStop();
                }
            }
        }

        private void scramblerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormLocalStreamScrambler scramblerForm = new FormLocalStreamScrambler();
            scramblerForm.PidListIn = this.pidList;
            scramblerForm.MuxBitrateList = this.muxBitrateList;

            if (DialogResult.OK == scramblerForm.ShowDialog())
            {

                byte[] byteArray = null;
                bool isHexValid = false;
                int dataLength = 0;


                isHexValid = scramblerForm.GetControlWordSerials(out byteArray, ref dataLength);

                //Create the file stream parser to go through the whole stream.
                WorkerFileScrambler workerScrambler = new WorkerFileScrambler(this, ProcessWorkerMessage);

                //On stream start. Initialize the environment.
                OnStreamStart();

                //Set descrambler info to the file stream parser.
                workerScrambler.SetScramblerInfo(scramblerForm.PidListOut, scramblerForm.OutputFileName, scramblerForm.DoEntropy, byteArray, dataLength, scramblerForm.EvenOddStart, scramblerForm.Bitrate, scramblerForm.CwPeriod);

                Result result = workerScrambler.StartParse(streamFile);

                if (result.Fine)
                {

                }
                else
                {
                    //On stream stop. Finalize the environment.
                    OnStreamStop();
                }
            }
        }

        private void playoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (0 == tsPacketSize)
            {
                //It looks like we have opened one invalid 
                MessageBox.Show(null, "Invalid transport stream.",
                    "Error!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                FormMuxBitrate formMuxBitrate = new FormMuxBitrate();
                formMuxBitrate.ForceEnterBitrate(); //Force the user to select a bitrate.
                formMuxBitrate.SetStreamBitrateList(this.muxBitrateList);

                //Show a message so that the user can be aware of what will happen.
                MessageBox.Show(null, "Please select a bitrate or enter a new bitrate in the next dialog.",
                    "Identify a bitrate!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //Reuse StreamBitrateForm as the user's choice.
                if (DialogResult.OK == formMuxBitrate.ShowDialog())
                {
                    //A ts media gateway.
                    FormGateway recordForm = new FormGateway();
                    recordForm.MdiParent = (Form)this.Parent.Parent;
                    recordForm.WindowState = FormWindowState.Normal;
                    recordForm.SetFileInput(formMuxBitrate.GetSelectedBitrate(), streamFile, tsPacketSize);
                    recordForm.Show();
                } 
            }

        }
    }
}
