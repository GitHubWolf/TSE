using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InActionLibrary;
using System.Windows.Forms;

namespace CustomizedParser
{
    public class ParserPlugin : IParserPlugin
    {
        /**
         * Get the name of this plugin.
         * 
         * @retval
         *     String to describe the name of this plugin.
         * 
         */
        public string GetName()
        {
            return "Private Section Parser";
        }

        /**
         * Get a string to describle the capability of this parser.
         * 
         * @retval
         *     String to describe the plugin's capabilities.
         * 
         */
        public string GetDescription()
        {
            return "Plugin to parse section as DVB private section. Only section header is parsed.";
        }

        /**
         * Get the data type that this plugin supports.
         * 
         * Only one kind of data type is supported by each plugin, i.e. TS packet, PES packet or section.
         * 
         * @retval
         *     @see DataType
         * 
         */
        public DataType GetSupportedType()
        {
            return DataType.SECTION;
        }

        /**
         * Parse the TS packet, PES packet or section.
         * 
         * @param parentNode parent tree node of to display the parsing result.
         * @param dataStore an instance containing the data to be parsed.
         * 
         * @retval
         *     void
         * 
         */
        public void Parse(TreeNode parentNode, DataStore section)
        {
            TreeNode nodeSection = null;
            TreeNode newNode = null;

            //Add one node to indicate this dataStore.
            Result result = Utility.AddNodeContainer(Position.CHILD, parentNode, out nodeSection, "Section", ItemType.SEARCH_SECTION, section, 0, section.GetLeftBitLength());

            Int64 bitOffset = 0;
            Int64 fieldValue = 0;

            Int64 tableId = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "table_id", ItemType.FIELD, section, ref bitOffset, 8, ref tableId);
            }

            Int64 sectionSyntaxIndicator = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "section_syntax_indicator", ItemType.FIELD, section, ref bitOffset, 1, ref sectionSyntaxIndicator);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "private_indicator", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "private_section_length", ItemType.FIELD, section, ref bitOffset, 12, ref fieldValue);
            }

            if (result.Fine)
            {
                if (0 == sectionSyntaxIndicator)
                {
                    //Add all left data as unknown data.
                    result = Utility.AddNodeData(Position.CHILD, nodeSection, out newNode, "private_data_byte", ItemType.FIELD, section, ref bitOffset, section.GetLeftBitLength());

                    if (!result.Fine)
                    {
                        Utility.UpdateNode(nodeSection, "Section(invalid)", ItemType.ERROR);
                    }

                }//if (0 == sectionSyntaxIndicator)
                else
                {
                    if (result.Fine)
                    {
                        result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "table_id_extension", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue);
                    }
                    
                    if (result.Fine)
                    {
                        result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "Reserved", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue);
                    }

                    Int64 versionNumber = 0;
                    if (result.Fine)
                    {
                        result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "version_number", ItemType.FIELD, section, ref bitOffset, 5, ref versionNumber);
                    }
                    if (result.Fine)
                    {
                        result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "current_next_indicator", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue);
                    }

                    Int64 sectionNumber = 0;
                    if (result.Fine)
                    {
                        result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "section_number", ItemType.FIELD, section, ref bitOffset, 8, ref sectionNumber);
                    }

                    TreeNode peerNode = null;
                    Int64 lastSectionNumber = 0;
                    if (result.Fine)
                    {
                        //The second parameter is speciall!!!!!We will use it later to insert peer node after that.
                        result = Utility.AddNodeField(Position.CHILD, nodeSection, out peerNode, "last_section_number", ItemType.FIELD, section, ref bitOffset, 8, ref lastSectionNumber);
                    }

                    //Parse crc32 first.
                    TreeNode crcNode = null;
                    if (result.Fine)
                    {
                        result = Utility.AddCrc32Node(nodeSection, out crcNode, section, ref fieldValue);
                    }

                    //Add all left data as unknown data.
                    Utility.AddLastSecondNodeData(nodeSection, crcNode, out newNode, section, ItemType.FIELD, "private_data_byte", ref bitOffset, section.GetLeftBitLength());

                    if (result.Fine)
                    {
                        String sectionDescription = String.Format("table_id 0x{0,2:X2}, version_number 0x{1,2:X2}, section_number 0x{2,2:X2}, last_section_number 0x{3,2:X2}",
                                                        tableId, versionNumber, sectionNumber, lastSectionNumber);
                        Utility.UpdateNode(nodeSection, sectionDescription, ItemType.SEARCH_SECTION);
                    }
                    else
                    {
                        Utility.UpdateNode(nodeSection, "Section(invalid)", ItemType.ERROR);
                    }
                }//if (1 == sectionSyntaxIndicator)
            }
        }

        /**
         * Start the plugin.
         * 
         * Before calling other methods in the plugin, this method will be called first. 
         * 
         * @retval
         *     void
         * 
         */
        public void Start()
        {

        }

        /**
         * Stop the plugin.
         * 
         * When the main application has notified all data found to the plugin, it will notify the plugin by calling this method. 
         * 
         * @retval
         *     void
         * 
         */
        public void Stop()
        {

        }
    }
}
