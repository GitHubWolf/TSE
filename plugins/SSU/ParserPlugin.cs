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
            return "DVB SSU Section Parser";
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
            return "Plugin to parse DSI/DII/DDB section.";
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
         * @param parentNode parent tree node to display the parsing result.
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

            TreeNode nodeSectionHeader = null;
            Int64 sectionSyntaxIndicator = 0;
            Int64 tableIdExtension = 0;
            Int64 sectionNumber = 0;
            Int64 lastSectionNumber = 0;
            Int64 versionNumber = 0;


            if (result.Fine)
            {
                result = Utility.AddNodeContainer(Position.CHILD, nodeSection, out nodeSectionHeader, "DVB section header", ItemType.ITEM, section, bitOffset, 8*8);

                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeSectionHeader, out newNode, "table_id", ItemType.FIELD, section, ref bitOffset, 8, ref tableId);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeSectionHeader, out newNode, "section_syntax_indicator", ItemType.FIELD, section, ref bitOffset, 1, ref sectionSyntaxIndicator);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeSectionHeader, out newNode, "private_indicator", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeSectionHeader, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeSectionHeader, out newNode, "section_length", ItemType.FIELD, section, ref bitOffset, 12, ref fieldValue);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeSectionHeader, out newNode, "table_id_extension", ItemType.FIELD, section, ref bitOffset, 16, ref tableIdExtension);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeSectionHeader, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeSectionHeader, out newNode, "version_number", ItemType.FIELD, section, ref bitOffset, 5, ref versionNumber);
                }
                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeSectionHeader, out newNode, "current_next_indicator", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeSectionHeader, out newNode, "section_number", ItemType.FIELD, section, ref bitOffset, 8, ref sectionNumber);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeSectionHeader, out newNode, "last_section_number", ItemType.FIELD, section, ref bitOffset, 8, ref lastSectionNumber);
                }
            }
            

            //Parse crc32 first.
            TreeNode crcNode = null;
            if (result.Fine)
            {
                if (1 == sectionSyntaxIndicator)
                {
                    result = Utility.AddCrc32Node(nodeSection, out crcNode, section, ref fieldValue);
                }
            }

            TreeNode treeNodeMessagePayload = null;
            if (result.Fine)
            {
                result = Utility.AddNodeContainer(Position.PEER, nodeSectionHeader, out treeNodeMessagePayload, "SSU Payload", ItemType.ITEM, section, bitOffset, section.GetLeftBitLength());
            }

            String sectionDescription = null;
            if (result.Fine)
            {
                if ((Int64)TableId.DSI_DII == tableId)
                {
                    //DSI or DII.
                    if (1 >= tableIdExtension)
                    {
                        //DSI.
                        sectionDescription = "DSI: ";
                        ParseDSI(section, treeNodeMessagePayload, ref bitOffset);
                    }
                    else
                    { 
                        //DII.
                        sectionDescription = "DII: ";
                        ParseDII(section, treeNodeMessagePayload, ref bitOffset);
                    }
                }
                else if((Int64)TableId.DDB == tableId)
                { 
                    //DDB.
                    sectionDescription = "DDB: ";
                    ParseDDB(section, treeNodeMessagePayload, ref bitOffset);
                }

                sectionDescription += String.Format("table_id 0x{0,2:X2}, table_id_extension 0x{1,4:X4}, version_number 0x{2,2:X2}, section_number 0x{3,2:X2}, last_section_number 0x{4,2:X2}",
                                                tableId, tableIdExtension, versionNumber, sectionNumber, lastSectionNumber);
            }

            if (result.Fine)
            {

            }

            if (result.Fine)
            {
                if (0 != section.GetLeftBitLength())
                {
                    result = Utility.AddNodeContainer(Position.PEER, nodeSectionHeader, out newNode, "unknown_data", ItemType.ERROR, section, bitOffset, section.GetLeftBitLength());
                }

                Utility.UpdateNode(nodeSection, sectionDescription, ItemType.SEARCH_SECTION);
            }
            else
            {
                Utility.UpdateNode(nodeSection, "Section(invalid)", ItemType.ERROR);
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

        private Result ParseDSI(DataStore section, TreeNode nodeParent, ref Int64 bitOffset)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            TreeNode treeNodeHeader = null;
            if (result.Fine)
            {                
                //Add a container node. The length will be decided after ther header is parsed.
                result = Utility.AddNodeContainer(Position.CHILD, nodeParent, out treeNodeHeader, "dsmccMessageHeader", ItemType.ITEM, section, bitOffset, 0);

                if (result.Fine)
                {
                    result = ParseMessageHeader(section, treeNodeHeader, ref bitOffset, "transactionId");
                }
            }

            TreeNode nodePayload = null;
            if (result.Fine)
            {
                //Add a container node.
                result = Utility.AddNodeContainer(Position.CHILD, nodeParent, out nodePayload, "controlMessagePayload", ItemType.ITEM, section, bitOffset, section.GetLeftBitLength());
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodePayload, out newNode, "serverId", ItemType.FIELD, section, ref bitOffset, 20 * 8, ref fieldValue);
            }

            Int64 compatibilityDescriptorLength = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodePayload, out newNode, "compatibilityDescriptorLength", ItemType.FIELD, section, ref bitOffset, 16, ref compatibilityDescriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeContainer(Position.CHILD, nodePayload, out newNode, "compatibilityDescriptor", ItemType.ITEM, section, bitOffset, compatibilityDescriptorLength * 8);

                if (result.Fine && (0 != compatibilityDescriptorLength))
                {
                    //To parse compatibility descriptor here.
                    result = ParseCompatibilityDescriptor(section, newNode, ref bitOffset, compatibilityDescriptorLength * 8);
                }
            }

            Int64 privateDataLength = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodePayload, out newNode, "privateDataLength", ItemType.FIELD, section, ref bitOffset, 16, ref privateDataLength);
            }

            if (0 != privateDataLength)
            {
                Int64 numberOfGroups = 0;
                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodePayload, out newNode, "NumberOfGroups", ItemType.FIELD, section, ref bitOffset, 16, ref numberOfGroups);
                }

                privateDataLength -= 2;//Decrease the length of NumberOfGroups.
                privateDataLength *= 8;//Convert to bits.

                if (result.Fine)
                {
                    for (Int64 i = 0; i < numberOfGroups; i++)
                    {
                        TreeNode nodeGroup = null;
                        Int64 position1 = section.GetLeftBitLength();
                        result = Utility.AddNodeContainerPlus(Position.CHILD, nodePayload, out nodeGroup, "group", ItemType.ITEM, section, bitOffset, 0, privateDataLength);

                        if (result.Fine)
                        {
                            result = Utility.AddNodeFieldPlus(Position.CHILD, nodeGroup, out newNode, "GroupId", ItemType.FIELD, section, ref bitOffset, 32, ref fieldValue, ref privateDataLength);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeFieldPlus(Position.CHILD, nodeGroup, out newNode, "GroupSize", ItemType.FIELD, section, ref bitOffset, 32, ref fieldValue, ref privateDataLength);
                        }

                        Int64 innerCompatibilityDescriptorLength = 0;
                        if (result.Fine)
                        {
                            result = Utility.AddNodeFieldPlus(Position.CHILD, nodeGroup, out newNode, "compatibilityDescriptorLength", ItemType.FIELD, section, ref bitOffset, 16, ref innerCompatibilityDescriptorLength, ref privateDataLength);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeContainerPlus(Position.CHILD, nodeGroup, out newNode, "compatibilityDescriptor", ItemType.FIELD, section, bitOffset, innerCompatibilityDescriptorLength * 8, privateDataLength);

                            if (result.Fine && (0 != innerCompatibilityDescriptorLength))
                            {
                                //To parse compatibility descriptor here.
                                result = ParseCompatibilityDescriptor(section, newNode, ref bitOffset, innerCompatibilityDescriptorLength * 8);
                            }
                        }

                        Int64 groupInfoLength = 0;
                        if (result.Fine)
                        {
                            result = Utility.AddNodeFieldPlus(Position.CHILD, nodeGroup, out newNode, "GroupInfoLength", ItemType.FIELD, section, ref bitOffset, 16, ref groupInfoLength, ref privateDataLength);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeDataPlus(Position.CHILD, nodeGroup, out newNode, "GroupInfoBytes", ItemType.FIELD, section, ref bitOffset, groupInfoLength * 8, ref privateDataLength);
                        }

                        Int64 position2 = section.GetLeftBitLength();
                        if (result.Fine)
                        {
                            Utility.UpdateNodeLength(nodeGroup, "group", ItemType.ITEM, position1 - position2);
                        }
                        else
                        {
                            Utility.UpdateNode(nodeGroup, "group(invalid)", ItemType.ERROR);
                            break;
                        }
                    }
                }

                Int64 innerPrivateDataLength = 0;
                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, nodePayload, out newNode, "privateDataLength", ItemType.FIELD, section, ref bitOffset, 16, ref innerPrivateDataLength, ref privateDataLength);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeDataPlus(Position.CHILD, nodePayload, out newNode, "privateData", ItemType.FIELD, section, ref bitOffset, innerPrivateDataLength * 8, ref privateDataLength);
                }
            }


            return result;
        }

        private Result ParseDII(DataStore section, TreeNode nodeParent, ref Int64 bitOffset)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            TreeNode treeNodeHeader = null;
            if (result.Fine)
            {
                //Add a container node. The length will be decided after ther header is parsed.
                result = Utility.AddNodeContainer(Position.CHILD, nodeParent, out treeNodeHeader, "dsmccMessageHeader", ItemType.ITEM, section, bitOffset, 0);

                if (result.Fine)
                {
                    result = ParseMessageHeader(section, treeNodeHeader, ref bitOffset, "transactionId");
                }
            }

            TreeNode nodePayload = null;
            if (result.Fine)
            {
                //Add a container node.
                result = Utility.AddNodeContainer(Position.CHILD, nodeParent, out nodePayload, "controlMessagePayload", ItemType.ITEM, section, bitOffset, section.GetLeftBitLength());
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodePayload, out newNode, "downloadId", ItemType.FIELD, section, ref bitOffset, 4 * 8, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodePayload, out newNode, "blockSize", ItemType.FIELD, section, ref bitOffset, 2 * 8, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodePayload, out newNode, "windowSize", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodePayload, out newNode, "ackPeriod", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodePayload, out newNode, "tCDownloadWindow", ItemType.FIELD, section, ref bitOffset, 4 * 8, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodePayload, out newNode, "tCDownloadScenario", ItemType.FIELD, section, ref bitOffset, 4 * 8, ref fieldValue);
            }

            Int64 compatibilityDescriptorLength = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodePayload, out newNode, "compatibilityDescriptorLength", ItemType.FIELD, section, ref bitOffset, 16, ref compatibilityDescriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeContainer(Position.CHILD, nodePayload, out newNode, "compatibilityDescriptor", ItemType.ITEM, section, bitOffset, compatibilityDescriptorLength * 8);

                if (result.Fine && (0 != compatibilityDescriptorLength))
                {
                    //To parse compatibility descriptor here.
                    result = ParseCompatibilityDescriptor(section, newNode, ref bitOffset, compatibilityDescriptorLength * 8);
                }
            }

            Int64 numberOfModules = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodePayload, out newNode, "numberOfModules", ItemType.FIELD, section, ref bitOffset, 16, ref numberOfModules);
            }

            if (result.Fine)
            {
                for (Int64 i = 0; i < numberOfModules; i++)
                {
                    TreeNode nodeModule = null;
                    Int64 position1 = section.GetLeftBitLength();
                    result = Utility.AddNodeContainer(Position.CHILD, nodePayload, out nodeModule, "module", ItemType.ITEM, section, bitOffset, 0);

                    if (result.Fine)
                    {
                        result = Utility.AddNodeField(Position.CHILD, nodeModule, out newNode, "moduleId", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue);
                    }

                    if (result.Fine)
                    {
                        result = Utility.AddNodeField(Position.CHILD, nodeModule, out newNode, "moduleSize", ItemType.FIELD, section, ref bitOffset, 32, ref fieldValue);
                    }

                    if (result.Fine)
                    {
                        result = Utility.AddNodeField(Position.CHILD, nodeModule, out newNode, "moduleVersion", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue);
                    }


                    Int64 moduleInfoLength = 0;
                    if (result.Fine)
                    {
                        result = Utility.AddNodeField(Position.CHILD, nodeModule, out newNode, "moduleInfoLength", ItemType.FIELD, section, ref bitOffset, 8, ref moduleInfoLength);
                    }

                    if (result.Fine)
                    {
                        result = Utility.AddNodeData(Position.CHILD, nodeModule, out newNode, "GroupInfoBytes", ItemType.FIELD, section, ref bitOffset, moduleInfoLength * 8);
                    }

                    Int64 position2 = section.GetLeftBitLength();
                    if (result.Fine)
                    {
                        Utility.UpdateNodeLength(nodeModule, "module", ItemType.ITEM, position1 - position2);
                    }
                    else
                    {
                        Utility.UpdateNode(nodeModule, "module(invalid)", ItemType.ERROR);
                        break;
                    }
                }
            }

            Int64 innerPrivateDataLength = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodePayload, out newNode, "privateDataLength", ItemType.FIELD, section, ref bitOffset, 16, ref innerPrivateDataLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeData(Position.CHILD, nodePayload, out newNode, "privateData", ItemType.FIELD, section, ref bitOffset, innerPrivateDataLength * 8);
            }


            return result;
        }

        private Result ParseDDB(DataStore section, TreeNode nodeParent, ref Int64 bitOffset)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            TreeNode treeNodeHeader = null;
            if (result.Fine)
            {
                //Add a container node. The length will be decided after ther header is parsed.
                result = Utility.AddNodeContainer(Position.CHILD, nodeParent, out treeNodeHeader, "dsmccMessageHeader", ItemType.ITEM, section, bitOffset, 0);

                if (result.Fine)
                {
                    result = ParseMessageHeader(section, treeNodeHeader, ref bitOffset, "downloadId");
                }
            }

            TreeNode nodePayload = null;
            if (result.Fine)
            {
                //Add a container node.
                result = Utility.AddNodeContainer(Position.CHILD, nodeParent, out nodePayload, "dataMessagePayload", ItemType.ITEM, section, bitOffset, section.GetLeftBitLength());
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodePayload, out newNode, "moduleId", ItemType.FIELD, section, ref bitOffset, 2 * 8, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodePayload, out newNode, "moduleVersion", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodePayload, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodePayload, out newNode, "blockNumber", ItemType.FIELD, section, ref bitOffset, 2 * 8, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeData(Position.CHILD, nodePayload, out newNode, "blockDataByte", ItemType.FIELD, section, ref bitOffset, section.GetLeftBitLength());
            }
            return result;
        }

        private Result ParseMessageHeader(DataStore section, TreeNode nodeParent, ref Int64 bitOffset, string idStr)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            Int64 bitLength1 = section.GetLeftBitLength();
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeParent, out newNode, "protocolDiscriminator", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeParent, out newNode, "dsmccType", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeParent, out newNode, "messageId", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeParent, out newNode, idStr, ItemType.FIELD, section, ref bitOffset, 32, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeParent, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue);
            }

            Int64 adaptationLength = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeParent, out newNode, "adaptationLength", ItemType.FIELD, section, ref bitOffset, 8, ref adaptationLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeParent, out newNode, "messageLength", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeData(Position.CHILD, nodeParent, out newNode, "adaptation", ItemType.FIELD, section, ref bitOffset, adaptationLength*8);
            }

            if (result.Fine)
            {
                Int64 bitLength2 = section.GetLeftBitLength();
                Utility.UpdateNodeLength(nodeParent, "dsmccMessageHeader", ItemType.ITEM, bitLength1 - bitLength2);
            }
            return result;
        }

        private Result ParseCompatibilityDescriptor(DataStore section, TreeNode nodeParent, ref Int64 bitOffset, Int64 compatibilityDescriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            Int64 descriptorCount = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, nodeParent, out newNode, "descriptorCount", ItemType.FIELD, section, ref bitOffset, 16, ref descriptorCount, ref compatibilityDescriptorLength);
            }


            for (Int64 i = 0; i < descriptorCount; ++i)
            {
                TreeNode treeNodeDescriptor = null;
                Int64 position1 = section.GetLeftBitLength();
                result = Utility.AddNodeContainerPlus(Position.CHILD, nodeParent, out treeNodeDescriptor, "descriptor", ItemType.ITEM, section, bitOffset, 0, compatibilityDescriptorLength);

                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, treeNodeDescriptor, out newNode, "descriptorType", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue, ref compatibilityDescriptorLength); 
                }

                Int64 descriptorLength = 0;
                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, treeNodeDescriptor, out newNode, "descriptorLength", ItemType.FIELD, section, ref bitOffset, 8, ref descriptorLength, ref compatibilityDescriptorLength);
                }

                if (0 != descriptorLength)
                {
                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, treeNodeDescriptor, out newNode, "specifierType", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue, ref compatibilityDescriptorLength);
                    }

                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, treeNodeDescriptor, out newNode, "specifierData", ItemType.FIELD, section, ref bitOffset, 24, ref fieldValue, ref compatibilityDescriptorLength);
                    }

                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, treeNodeDescriptor, out newNode, "model", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref compatibilityDescriptorLength);
                    }

                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, treeNodeDescriptor, out newNode, "version", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref compatibilityDescriptorLength);
                    }

                    Int64 subDescriptorCount = 0;
                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, treeNodeDescriptor, out newNode, "subDescriptorCount", ItemType.FIELD, section, ref bitOffset, 8, ref subDescriptorCount, ref compatibilityDescriptorLength);
                    }

                    if (result.Fine)
                    {
                        result = Utility.AddNodeDataPlus(Position.CHILD, treeNodeDescriptor, out newNode, "subDescriptor", ItemType.FIELD, section, ref bitOffset, (descriptorLength - 9) *8, ref compatibilityDescriptorLength);
                    }
                }


                Int64 position2 = section.GetLeftBitLength();
                if(result.Fine)
                {
                    Utility.UpdateNodeLength(treeNodeDescriptor, "descriptor", ItemType.ITEM, position1 - position2);
                }
                else
                {
                    Utility.UpdateNode(treeNodeDescriptor, "descriptor(invalid)", ItemType.ERROR);
                    break;
                }
            }

            if (0 != compatibilityDescriptorLength)
            {
                result = Utility.AddNodeDataPlus(Position.CHILD, nodeParent, out newNode, "unknown_data", ItemType.WARNING, section, ref bitOffset, compatibilityDescriptorLength, ref compatibilityDescriptorLength);
            }
            return result;
        }
    }
}
