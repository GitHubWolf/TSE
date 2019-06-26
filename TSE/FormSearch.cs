using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InActionLibrary;

namespace TSE
{
    public partial class FormSearch : Form
    {
        private SearchRequest searchRequest = new SearchRequest();
        private List<Plugin> pluginList = new List<Plugin>();
        private PidProfile selectedPidProfile = null;

        public SortedDictionary<UInt16, PidProfile> PidList
        {
            get;
            set;
        }


        public FormSearch()
        {
            InitializeComponent();

            comboBoxCount.Items.Add(1);
            comboBoxCount.Items.Add(5);
            comboBoxCount.Items.Add(10);
            comboBoxCount.Items.Add(100);
            comboBoxCount.Items.Add(1000);
            comboBoxCount.Items.Add(-1);

            radioButtonTsPacket.Checked = true;

            comboBoxCount.SelectedIndex = 2;//Select 10 by default.

            searchRequest.SearchType = DataType.TS_PACKET;
        }

        private void SearchForm_Shown(object sender, EventArgs e)
        {
            String pidDescription = null;
            int index = 0;

            if (null != PidList)
            {
                foreach (KeyValuePair<UInt16, PidProfile> pair in PidList)
                {
                    if (0 != pair.Value.PacketCount)
                    {
                        pidDescription = String.Format("PID: 0x{0, 4:X4}, Count: {1, 12:d}, Type: {2}", pair.Key, pair.Value.PacketCount, pair.Value.Description);
                        //ListItem listItem = new ListItem();

                        index = listBoxPid.Items.Add(pair.Value);

                        //If it is the PID selected in the file stream form, we will select it by default.
                        if ((null != selectedPidProfile ) && (selectedPidProfile.Equals(pair.Value)))
                        {
                            listBoxPid.SelectedIndex = index;
                        }

                    }
                }

                if ((null == selectedPidProfile) && (listBoxPid.Items.Count > 0))
                {
                    listBoxPid.SelectedIndex = 0;//Select the first one by default.
                }
            }//if (null != PidListIn)


            //Find out all available plugins.
            DirectoryInfo pluginDirectory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            Plugin plugin = null;
            Result result = null;
            FileInfo[] fileInfoList = pluginDirectory.GetFiles();
            foreach (FileInfo pluginFile in fileInfoList)
            {
                //Try to detect whether it is a valid DLL file that implment all required interfaces.
                plugin = new Plugin();

                //Detect whether it is a valid plugin for TSE.
                result = plugin.LoadPlugin(pluginFile.FullName);
                if (result.Fine)
                {
                    //Add the plugin into the list.
                    pluginList.Add(plugin);

                    //Show it by default if it is a TS packet parser.
                    if (DataType.TS_PACKET == plugin.GetSupportedType())
                    {
                        listBoxParsers.Items.Add(plugin);
                    }
                }

                if (0 != listBoxParsers.Items.Count)
                {
                    //Select the first one by default.
                    listBoxParsers.SetSelected(0, true);
                }
            }
        }

        private void radioButtonTsPacket_Click(object sender, EventArgs e)
        {
            hexEditMask.Enabled = false;
            hexEditMatch.Enabled = false;
            checkBoxShowDuplicateSection.Enabled = false;
            checkBoxIgnorePID.Enabled = true;

            searchRequest.SearchType = DataType.TS_PACKET;
            
            //Show the plugins suitable for current selection.
            ShowPlugins(searchRequest.SearchType);
        }

        private void radioButtonPesPacket_Click(object sender, EventArgs e)
        {
            hexEditMask.Enabled = false;
            hexEditMatch.Enabled = false;
            checkBoxShowDuplicateSection.Enabled = false;
            checkBoxIgnorePID.Enabled = false;

            searchRequest.SearchType = DataType.PES_PACKET;

            //Show the plugins suitable for current selection.
            ShowPlugins(searchRequest.SearchType);
        }

        private void radioButtonSection_Click(object sender, EventArgs e)
        {
            hexEditMask.Enabled = true;
            hexEditMatch.Enabled = true;
            checkBoxShowDuplicateSection.Enabled = true;
            checkBoxIgnorePID.Enabled = false;

            searchRequest.SearchType = DataType.SECTION;

            //Show the plugins suitable for current selection.
            ShowPlugins(searchRequest.SearchType);
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            //Check whether the mask and match is correct.
            byte[] mask = null;
            byte[] match = null;

            int maskLength = 0;
            int matchLength = 0;

            Result result = new Result();

            if (result.Fine)
            {
                if (DataType.SECTION == searchRequest.SearchType)
                {
                    if ((hexEditMask.GetByteArray(out mask, ref maskLength)) && (hexEditMatch.GetByteArray(out match, ref matchLength)))
                    {
                        if (maskLength != matchLength)
                        {
                            MessageBox.Show(null, "Length of mask and length of match must be the same!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            result.SetResult(ResultCode.INVALID_DATA);
                        }
                        else
                        {
                            searchRequest.FilterMask = mask;
                            searchRequest.FilterMatch = match;
                        }
                    }
                }
            }

            if (result.Fine)
            {
                if (listBoxParsers.Items.Count == 0)
                {
                    MessageBox.Show(null, "No plugin available!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    result.SetResult(ResultCode.INVALID_PLUGIN);
                }

                if (result.Fine)
                {
                    if (null == listBoxParsers.SelectedItem)
                    {
                        MessageBox.Show(null, "No plugin is selected! Please select one plugin!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        result.SetResult(ResultCode.INVALID_PLUGIN);
                    }
                }
            }

            if (result.Fine)
            {
                if ((searchRequest.SearchType == DataType.TS_PACKET) && (!checkBoxIgnorePID.Checked) && (null == listBoxPid.SelectedItem))
                {
                    MessageBox.Show(null, "You must select a PID for data search!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    result.SetResult(ResultCode.INVALID_DATA);
                }
            }

            if (result.Fine)
            {
                this.DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void listBoxPid_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedPidProfile = (PidProfile)listBoxPid.SelectedItem;
        }

        public void SetSelectedPidProfile(PidProfile pidProfile)
        {
            selectedPidProfile = pidProfile;
        }

        public SearchRequest GetSearchRequest()
        {
            searchRequest.SearchCount = Convert.ToInt64(comboBoxCount.Text);
            if (maskedTextBoxSkipFound.Text.Trim() == "")
            {
                searchRequest.CountOfSkipFound = 0;
            }
            else
            {
                searchRequest.CountOfSkipFound = Convert.ToInt64(maskedTextBoxSkipFound.Text);
            }

            if (maskedTextBoxSkipTsPackets.Text.Trim() == "")
            {
                searchRequest.CountOfSkipTsPacket = 0;
            }
            else
            {
                searchRequest.CountOfSkipTsPacket = Convert.ToInt64(maskedTextBoxSkipTsPackets.Text);
            }
            
            searchRequest.ShowDuplicateSection = checkBoxShowDuplicateSection.Checked;
            searchRequest.IgnorePID = checkBoxIgnorePID.Checked;
            searchRequest.SelectedParser = (Plugin)listBoxParsers.SelectedItem;
            searchRequest.DumpToFile = checkBoxDumpToFileOnly.Checked;

            /*Additional adjustment!*/
            if ((searchRequest.SearchType == DataType.TS_PACKET) && (searchRequest.IgnorePID))
            {
                //If IgnorePID is enabled, we will force the selected PID to be SUPER_PID.
                searchRequest.SelectedPid = PidProfile.SUPER_PID;
            }
            else
            {
                searchRequest.SelectedPid = selectedPidProfile.PID;
            }

            return searchRequest;
        }

        private void listBoxParsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            Plugin plugin = (Plugin)listBoxParsers.SelectedItem;

            if (null != plugin)
            {
                String description = plugin.GetDescription();
                labelPluginDetails.Text = plugin.GetName() + ":" + Environment.NewLine + description;
            }
        }

        private void ShowPlugins(DataType dataType)
        {
            listBoxParsers.Items.Clear();
            labelPluginDetails.Text = "";

            foreach (Plugin plugin in pluginList)
            {
                if (plugin.GetSupportedType() == dataType)
                {
                    listBoxParsers.Items.Add(plugin); 
                }
            }

            if (0 != listBoxParsers.Items.Count)
            {
                //Selecte the first one by default.
                listBoxParsers.SelectedIndex = 0;
            }
        }
    }
}
