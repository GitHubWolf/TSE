using InActionLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TSE
{
    public partial class FormSectionParser : Form
    {
        private List<Plugin> pluginList = new List<Plugin>();
        private TreeNode rootNode = null;
        //To save current DataStore of selected FieldItem. In case it is changed, we will need to display the hex string.
        private DataStore selectedDataStore = null;

        public FormSectionParser()
        {
            InitializeComponent();
        }

        private void FormSectionParser_Shown(object sender, EventArgs e)
        {
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

            ShowPlugins(DataType.SECTION);

            rootNode = Utility.AddChildNode(treeViewParser, null, "Root", "Section", ItemType.ROOT);
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

        private void listBoxParsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            Plugin plugin = (Plugin)listBoxParsers.SelectedItem;

            if (null != plugin)
            {
                String description = plugin.GetDescription();
                labelPluginDetails.Text = plugin.GetName() + ":" + Environment.NewLine + description;
            }
        }

        private void buttonParse_Click(object sender, EventArgs e)
        {
            byte[] byteArray = null;
            bool isHexValid = false;
            int dataLength = 0;

            Plugin plugin = (Plugin)listBoxParsers.SelectedItem;

            //Delete all existing items.
            rootNode.Remove();
            rootNode = Utility.AddChildNode(treeViewParser, null, "Root", "Section", ItemType.ROOT);

            if (null == plugin)
            {
                MessageBox.Show("No plugin available! Please ensure you at least have one plugin.", "Error", MessageBoxButtons.OK);
            }
            else
            {
                isHexValid = this.hexEditData.GetByteArray(out byteArray, ref dataLength);
                if (isHexValid)
                {
                    ParseSections(byteArray, dataLength);
                }
            }
        }

        private void ParseSections(byte[] byteArray, int dataLength)
        {
            Result result = new Result();
            Int64 dataLeftInBit = dataLength*8;
            Int64 bitOffset = 0;
            Int64 fieldValue = 0;
            Int64 sectionLength = 0;
            Int64 sectionOffset = 0;

            Plugin plugin = (Plugin)listBoxParsers.SelectedItem;
 
            while(result.Fine)
            {
                sectionOffset = bitOffset/8;

                //8-bit table_id.
                result = Utility.ByteArrayReadBits(byteArray, ref dataLeftInBit, ref bitOffset, 8, ref fieldValue);

                if (result.Fine)
                {
                    //Skip 4-bit section_syntax_indicator and reserved bits.
                    result = Utility.ByteArraySkipBits(ref dataLeftInBit, ref bitOffset, 4);
                }

                if (result.Fine)
                {
                    //12-bit section_length.
                    result = Utility.ByteArrayReadBits(byteArray, ref dataLeftInBit, ref bitOffset, 12, ref sectionLength);
                }

                if (result.Fine)
                {
                    //Skip the payload.
                    result = Utility.ByteArraySkipBits(ref dataLeftInBit, ref bitOffset, sectionLength * 8);
                }

                if (result.Fine)
                {
                    //Create a datastore to save this section.
                    DataStore dataStore = new DataStore(byteArray, sectionOffset, (sectionLength + 3));

                    //Call the plugin to parse the section now.
                    plugin.Parse(rootNode, dataStore);
                }
            }

            if (0 != dataLeftInBit)
            {
                MessageBox.Show("Incomplete section data left.", "Error", MessageBoxButtons.OK); 
            }
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

                if (binarySelectLength != 0)
                {
                    //Select the binary bits finally. We can't make the selection and then insert new sectionDetail. It will not work in such case.

                    richTextBoxProperty.Select(binarySelectStart, binarySelectLength);
                    richTextBoxProperty.SelectionColor = Color.Red;
                    //richTextBoxProperty.ScrollToCaret();
                }
            }//null != fieldItem
        }
    }
}
