using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InActionLibrary;


namespace TSE
{
    class DataParser: StreamParserSession
    {
        public DataParser(StreamParserContext owner)
            : base(owner)
        {
 
        }

        public static String GetStreamTypeName(Int64 streamType)
        {
            String name = null;

            switch (streamType)
            {
                case 0x00: name = "ITU-T | ISO/IEC Reserved"; break;
                case 0x01: name = "ISO/IEC 11172-2 Video"; break;
                case 0x02: name = "ITU-T Rec. H.262 | ISO/IEC 13818-2 Video or ISO/IEC 11172-2 constrained parameter video stream"; break;
                case 0x03: name = "ISO/IEC 11172-3 Audio"; break;
                case 0x04: name = "ISO/IEC 13818-3 Audio"; break;
                case 0x05: name = "ITU-T Rec. H.222.0 | ISO/IEC 13818-1 private_sections"; break;
                case 0x06: name = "ITU-T Rec. H.222.0 | ISO/IEC 13818-1 PES_PACKET packets containing private data"; break;
                case 0x07: name = "ISO/IEC 13522 MHEG"; break;
                case 0x08: name = "ITU-T Rec. H.222.0 | ISO/IEC 13818-1 Annex A DSM-CC"; break;
                case 0x09: name = "ITU-T Rec. H.222.1"; break;
                case 0x0A: name = "ISO/IEC 13818-6 type A"; break;
                case 0x0B: name = "ISO/IEC 13818-6 type B"; break;
                case 0x0C: name = "ISO/IEC 13818-6 type C"; break;
                case 0x0D: name = "ISO/IEC 13818-6 type D"; break;
                case 0x0E: name = "ITU-T Rec. H.222.0 | ISO/IEC 13818-1 auxiliary"; break;
                case 0x0F: name = "ISO/IEC 13818-7 Audio with ADTS transport syntax"; break;
                case 0x10: name = "ISO/IEC 14496-2 Visual"; break;
                case 0x11: name = "ISO/IEC 14496-3 Audio with the LATM transport syntax as defined in ISO/IEC 14496-3"; break;
                case 0x12: name = "ISO/IEC 14496-1 SL-packetized stream or FlexMux stream carried in PES_PACKET packets"; break;
                case 0x13: name = "ISO/IEC 14496-1 SL-packetized stream or FlexMux stream carried in ISO/IEC 14496_sections"; break;
                case 0x14: name = "ISO/IEC 13818-6 Synchronized Download Protocol"; break;
                case 0x15: name = "Metadata carried in PES_PACKET packets"; break;
                case 0x16: name = "Metadata carried in metadata_sections"; break;
                case 0x17: name = "Metadata carried in ISO/IEC 13818-6 Data Carousel"; break;
                case 0x18: name = "Metadata carried in ISO/IEC 13818-6 Object Carousel"; break;
                case 0x19: name = "Metadata carried in ISO/IEC 13818-6 Synchronized Download Protocol"; break;
                case 0x1A: name = "IPMP stream (defined in ISO/IEC 13818-11, MPEG-2 IPMP)"; break;
                case 0x1B: name = "AVC video stream as defined in ITU-T Rec. H.264 | ISO/IEC 14496-10 Video"; break;
                case 0x7F: name = "IPMP stream"; break;
                default:
                    {
                        if ((streamType >= 0x1C) && (streamType <= 0x7E))
                        { 
                            name = "ITU-T Rec. H.222.0 | ISO/IEC 13818-1 Reserved"; break;
                        }
                        else if ((streamType >= 0x80) && (streamType <= 0xFF))
                        {
                            name = "User Private"; break;
                        }
                        else
                        {
                            name = "Unknown Stream"; break;
                        }
                    }
            }

            return name;
 
        }

        public void ParsePAT(TreeView treeView, TreeNode parent, DataStore section)
        {
            //Check whether there is an existing item named "PAT" to group all PAT sections.
            TreeNode[] nodesFound = parent.Nodes.Find("PAT", false);
            TreeNode nodeGroup = null;
            TreeNode nodeSection = null;
            TreeNode newNode = null;

            if (0 == nodesFound.Length)
            {
                //No item is named "PAT". Let's create a new one.
                nodeGroup = Utility.AddChildNode(treeView, parent, "PAT", "PAT [Program Association Table]", ItemType.PSI);
            }
            else
            {
                //Add the new section under the "PAT" item.
                nodeGroup = nodesFound[0];
            }

            //Add one node to indicate this section.
            Result result = Utility.AddNodeContainer(Position.CHILD, nodeGroup, out nodeSection, "PAT", ItemType.PSI_SECTION, section, 0, section.GetLeftBitLength());

            Int64 bitOffset = 0;
            Int64 fieldValue = 0;

            Int64 tableId = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "table_id", ItemType.FIELD, section, ref bitOffset, 8, ref tableId);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "section_syntax_indicator", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "'0'", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "section_length", ItemType.FIELD, section, ref bitOffset, 12, ref fieldValue);
            }

            Int64 transportStreamId = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "transport_stream_id", ItemType.FIELD, section, ref bitOffset, 16, ref transportStreamId);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue);
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

            Int64 lastSectionNumber = 0;
            TreeNode peerNode = null;//Special point!!!!!!!!
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

            TreeNode loopNode = null;
            if (result.Fine)
            {
                if (result.Fine)
                {
                    result = Utility.AddNodeContainer(Position.PEER, peerNode, out loopNode, "loop", ItemType.LOOP, section, bitOffset, section.GetLeftBitLength());
                }

                if (result.Fine)
                {
                    TreeNode itemNode = null;
                    Int64 programNumber = 0;
                    Int64 pmtPid = 0;
                    //A loop to read in all program_nubmer and PMT_pid.
                    while (section.GetLeftBitLength() >= (4 * 8))
                    {
                        result = Utility.AddNodeContainer(Position.CHILD, loopNode, out itemNode, "item", ItemType.ITEM, section, bitOffset, 4 * 8);

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, itemNode, out newNode, "program_number", ItemType.FIELD, section, ref bitOffset, 16, ref programNumber);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, itemNode, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 3, ref fieldValue);
                        }

                        if (result.Fine)
                        {
                            if (0 == programNumber)
                            {
                                result = Utility.AddNodeField(Position.CHILD, itemNode, out newNode, "network_PID", ItemType.FIELD, section, ref bitOffset, 13, ref pmtPid);

                                if (result.Fine)
                                {
                                    Utility.UpdateNode(itemNode, "program_number: " + Utility.GetValueHexString(programNumber, 16) + ", network_PID: " + Utility.GetValueHexString(pmtPid, 16), ItemType.ITEM);
                                }
                            }
                            else
                            {
                                result = Utility.AddNodeField(Position.CHILD, itemNode, out newNode, "program_map_PID", ItemType.FIELD, section, ref bitOffset, 13, ref pmtPid);

                                if (result.Fine)
                                {
                                    result = Utility.UpdateNode(itemNode, "program_number: " + Utility.GetValueHexString(programNumber, 16) + ", program_map_PID:" + Utility.GetValueHexString(pmtPid, 16), ItemType.ITEM); 
                                }
                            }

                        }

                        if (!result.Fine)
                        {
                            break;
                        }
                    }
                }
            }

            //Add all left data as unknown data.
            Utility.AddLastSecondNodeData(nodeSection, crcNode, out newNode, section, ItemType.ERROR, "unknown_data", ref bitOffset, section.GetLeftBitLength());

            if (result.Fine)
            {
                String sectionDescription = String.Format("table_id 0x{0,2:X2}, transport_stream_id 0x{1,4:X4}, version_number 0x{2,2:X2}, section_number 0x{3,2:X2}, last_section_number 0x{4,2:X2}",
                                                tableId, transportStreamId, versionNumber, sectionNumber, lastSectionNumber);
                Utility.UpdateNode(nodeSection, sectionDescription, ItemType.PSI_SECTION);
            }
            else
            {
                Utility.UpdateNode(nodeSection, "PAT(invalid)", ItemType.ERROR);
            }
        }
        public void ParsePMT(TreeView treeView, TreeNode parent, DataStore section)
        {
            //Check whether there is an existing item named "PMT" to group all PMT sections.
            TreeNode[] nodesFound = parent.Nodes.Find("PMT", false);
            TreeNode nodeGroup = null;
            TreeNode nodeSection = null;
            TreeNode newNode = null;

            if (0 == nodesFound.Length)
            {
                //No item is named "PMT". Let's create a new one.
                nodeGroup = Utility.AddChildNode(treeView, parent, "PMT", "PMT [Program Map Table]", ItemType.PSI);
            }
            else
            {
                //Add the new section under the "PMT" item.
                nodeGroup = nodesFound[0];
            }

            //Add one node to indicate this section.
            Result result = Utility.AddNodeContainer(Position.CHILD, nodeGroup, out nodeSection, "PMT", ItemType.PSI_SECTION, section, 0, section.GetLeftBitLength());

            Int64 bitOffset = 0;
            Int64 fieldValue = 0;

            Int64 tableId = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "table_id", ItemType.FIELD, section, ref bitOffset, 8, ref tableId);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "section_syntax_indicator", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "'0'", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "section_length", ItemType.FIELD, section, ref bitOffset, 12, ref fieldValue);
            }

            Int64 programNumber = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "program_number", ItemType.FIELD, section, ref bitOffset, 16, ref programNumber);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue);
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

            Int64 lastSectionNumber = 0;
            TreeNode peerNode = null;//Special point!!!!!!!!
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

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.PEER, peerNode, out peerNode, "reserved", ItemType.FIELD, section, ref bitOffset, 3, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.PEER, peerNode, out peerNode, "PCR_PID", ItemType.FIELD, section, ref bitOffset, 13, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.PEER, peerNode, out peerNode, "reserved", ItemType.FIELD, section, ref bitOffset, 4, ref fieldValue);
            }

            Int64 programInfoLength = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.PEER, peerNode, out peerNode, "program_info_length", ItemType.FIELD, section, ref bitOffset, 12, ref programInfoLength);
            }

            //Parse program level descriptors according to programInfoLength.
            if (result.Fine)
            {
                //Convert to bits.
                programInfoLength = programInfoLength * 8;

                if (result.Fine)
                {
                    result = Utility.AddNodeContainer(Position.PEER, peerNode, out peerNode, "program_info_descriptor_loop", ItemType.LOOP, section, bitOffset, programInfoLength);
                }

                if (result.Fine)
                {
                    result = Descriptor.ParseDescriptorLoop(Scope.MPEG, peerNode, section, ref bitOffset, ref programInfoLength);
                }
            }

            if (result.Fine)
            {
                result = Utility.AddNodeContainer(Position.PEER, peerNode, out peerNode, "stream_loop", ItemType.LOOP, section, bitOffset, section.GetLeftBitLength());

                if (result.Fine)
                {
                    Int64 streamType = 0;
                    Int64 elementaryPID = 0;
                    Int64 esInfoLength = 0;

                    TreeNode itemNode = null;
                    while (section.GetLeftBitLength() >= 5 * 8) //At least 5 bytes for each stream.
                    {
                        //Add a stream container.!!!!!!!!!!!!!!!!!!!!!!We will need to update the bit length for this node, since we currently doesn't know whether there are descriptors.
                        result = Utility.AddNodeContainer(Position.CHILD, peerNode, out itemNode, "stream", ItemType.LOOP, section, bitOffset, 5 * 8);

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, itemNode, out newNode, "stream_type", ItemType.FIELD, section, ref bitOffset, 8, ref streamType);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, itemNode, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 3, ref fieldValue);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, itemNode, out newNode, "elementary_PID", ItemType.FIELD, section, ref bitOffset, 13, ref elementaryPID);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, itemNode, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 4, ref fieldValue);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, itemNode, out newNode, "ES_info_length", ItemType.FIELD, section, ref bitOffset, 12, ref esInfoLength);
                        }

                        Int64 esInfoLengthInBits = 0;
                        if (result.Fine)
                        {
                            esInfoLengthInBits = esInfoLength * 8;//Convert to bits.
                            if (result.Fine)
                            {
                                result = Utility.AddNodeContainer(Position.CHILD, itemNode, out newNode, "ES_info_descriptor_loop", ItemType.LOOP, section, bitOffset, esInfoLengthInBits);
                            }

                            if (result.Fine)
                            {
                                result = Descriptor.ParseDescriptorLoop(Scope.MPEG, newNode, section, ref bitOffset, ref esInfoLengthInBits);
                            }
                        }

                        if (result.Fine)
                        {
                            //Update the detail of item node.
                            Utility.UpdateNodeLength(itemNode, "elementary_PID: " + Utility.GetValueHexString(elementaryPID, 16) + " stream_type: " + GetStreamTypeName(streamType), ItemType.ITEM, (5 + esInfoLength) * 8);
                        }
                        else
                        {
                            Utility.UpdateNode(itemNode, "stream(invalid)", ItemType.ERROR);

                            //Something is wrong! Break right away!
                            break;
                        }
                    }//while (section.GetLeftBitLength() >= 5 * 8)
                }
            }

            //Add all left data as unknown data.
            Utility.AddLastSecondNodeData(nodeSection, crcNode, out newNode, section, ItemType.ERROR, "unknown_data", ref bitOffset, section.GetLeftBitLength());

            if (result.Fine)
            {
                String sectionDescription = String.Format("table_id 0x{0,2:X2}, program_number 0x{1,4:X4}, version_number 0x{2,2:X2}, section_number 0x{3,2:X2}, last_section_number 0x{4,2:X2}",
                                                tableId, programNumber, versionNumber, sectionNumber, lastSectionNumber);
                Utility.UpdateNode(nodeSection, sectionDescription, ItemType.PSI_SECTION);
            }
            else
            {
                Utility.UpdateNode(nodeSection, "PMT(invalid)", ItemType.ERROR);
            }
        }

        public void ParseTSDT(TreeView treeView, TreeNode parent, DataStore section)
        {
            //Check whether there is an existing item named "TSDT" to group all TSDT sections.
            TreeNode[] nodesFound = parent.Nodes.Find("TSDT", false);
            TreeNode nodeGroup = null;
            TreeNode nodeSection = null;
            TreeNode newNode = null;

            if (0 == nodesFound.Length)
            {
                //No item is named "TSDT". Let's create a new one.
                nodeGroup = Utility.AddChildNode(treeView, parent, "TSDT", "TSDT [Transport Stream Description Table]", ItemType.PSI);
            }
            else
            {
                //Add the new section under the "TSDT" item.
                nodeGroup = nodesFound[0];
            }

            //Add one node to indicate this section.
            Result result = Utility.AddNodeContainer(Position.CHILD, nodeGroup, out nodeSection, "TSDT", ItemType.PSI_SECTION, section, 0, section.GetLeftBitLength());

            Int64 bitOffset = 0;
            Int64 fieldValue = 0;

            Int64 tableId = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "table_id", ItemType.FIELD, section, ref bitOffset, 8, ref tableId);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "section_syntax_indicator", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "'0'", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "section_length", ItemType.FIELD, section, ref bitOffset, 12, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "Reserved", ItemType.FIELD, section, ref bitOffset, 18, ref fieldValue);
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

            //Parse descriptors.
            if (result.Fine)
            {
                TreeNode itemNode = null;//Special point!!!!!!!!
                Int64 descriptorLoopLength = section.GetLeftBitLength();
                if (result.Fine)
                {
                    result = Utility.AddNodeContainer(Position.PEER, peerNode, out itemNode, "descriptor_loop", ItemType.LOOP, section, bitOffset, descriptorLoopLength);
                }

                if (result.Fine)
                {
                    result = Descriptor.ParseDescriptorLoop(Scope.MPEG, itemNode, section, ref bitOffset, ref descriptorLoopLength);
                }
            }

            //Add all left data as unknown data.
            Utility.AddLastSecondNodeData(nodeSection, crcNode, out newNode, section, ItemType.ERROR, "unknown_data", ref bitOffset, section.GetLeftBitLength());

            if (result.Fine)
            {
                String sectionDescription = String.Format("table_id 0x{0,2:X2}, version_number 0x{1,2:X2}, section_number 0x{2,2:X2}, last_section_number 0x{3,2:X2}",
                                                tableId, versionNumber, sectionNumber, lastSectionNumber);
                Utility.UpdateNode(nodeSection, sectionDescription, ItemType.PSI_SECTION);
            }
            else
            {
                Utility.UpdateNode(nodeSection, "TSDT(invalid)", ItemType.ERROR);
            }
        }

        public void ParseCAT(TreeView treeView, TreeNode parent, DataStore section)
        {
            //Check whether there is an existing item named "CAT" to group all CAT sections.
            TreeNode[] nodesFound = parent.Nodes.Find("CAT", false);
            TreeNode nodeGroup = null;
            TreeNode nodeSection = null;
            TreeNode newNode = null;

            if (0 == nodesFound.Length)
            {
                //No item is named "CAT". Let's create a new one.
                nodeGroup = Utility.AddChildNode(treeView, parent, "CAT", "CAT [Conditional Access Table]", ItemType.PSI);
            }
            else
            {
                //Add the new section under the "CAT" item.
                nodeGroup = nodesFound[0];
            }

            //Add one node to indicate this section.
            Result result = Utility.AddNodeContainer(Position.CHILD, nodeGroup, out nodeSection, "CAT", ItemType.PSI_SECTION, section, 0, section.GetLeftBitLength());

            Int64 bitOffset = 0;
            Int64 fieldValue = 0;

            Int64 tableId = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "table_id", ItemType.FIELD, section, ref bitOffset, 8, ref tableId);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "section_syntax_indicator", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "'0'", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "section_length", ItemType.FIELD, section, ref bitOffset, 12, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "Reserved", ItemType.FIELD, section, ref bitOffset, 18, ref fieldValue);
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

            //Parse descriptors.
            if (result.Fine)
            {
                TreeNode itemNode = null;//Special point!!!!!!!!
                Int64 descriptorLoopLength = section.GetLeftBitLength();
                if (result.Fine)
                {
                    result = Utility.AddNodeContainer(Position.PEER, peerNode, out itemNode, "descriptor_loop", ItemType.LOOP, section, bitOffset, descriptorLoopLength);
                }

                if (result.Fine)
                {
                    result = Descriptor.ParseDescriptorLoop(Scope.MPEG, itemNode, section, ref bitOffset, ref descriptorLoopLength);
                }
            }

            //Add all left data as unknown data.
            Utility.AddLastSecondNodeData(nodeSection, crcNode, out newNode, section, ItemType.ERROR, "unknown_data", ref bitOffset, section.GetLeftBitLength());

            if (result.Fine)
            {
                String sectionDescription = String.Format("table_id 0x{0,2:X2}, version_number 0x{1,2:X2}, section_number 0x{2,2:X2}, last_section_number 0x{3,2:X2}",
                                                tableId, versionNumber, sectionNumber, lastSectionNumber);
                Utility.UpdateNode(nodeSection, sectionDescription, ItemType.PSI_SECTION);
            }
            else
            {
                Utility.UpdateNode(nodeSection, "CAT(invalid)", ItemType.ERROR);
            }
        }

        public void ParseNITActual(TreeView treeView, TreeNode parent, DataStore section)
        {
            ParseNIT(treeView, parent, section, "NITActual", "NIT [Network Information Table-Actual Network]");
        }

        public void ParseNITOther(TreeView treeView, TreeNode parent, DataStore section)
        {
            ParseNIT(treeView, parent, section, "NITOther", "NIT [Network Information Table-Other Network]");
        }

        public void ParseNIT(TreeView treeView, TreeNode parent, DataStore section, String sectionGroup, String sectionDetail)
        {
            //Check whether there is an existing item named like "NITActual" to group all NIT sections.
            TreeNode[] nodesFound = parent.Nodes.Find(sectionGroup, false);
            TreeNode nodeGroup = null;
            TreeNode nodeSection = null;
            TreeNode newNode = null;

            if (0 == nodesFound.Length)
            {
                //No item is named like "NITActual". Let's create a new one.
                nodeGroup = Utility.AddChildNode(treeView, parent, sectionGroup, sectionDetail, ItemType.PSI);
            }
            else
            {
                //Add the new section under the "NITActual"-like item.
                nodeGroup = nodesFound[0];
            }

            //Add one node to indicate this section.
            Result result = Utility.AddNodeContainer(Position.CHILD, nodeGroup, out nodeSection, sectionGroup, ItemType.PSI_SECTION, section, 0, section.GetLeftBitLength());

            Int64 bitOffset = 0;
            Int64 fieldValue = 0;

            Int64 tableId = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "table_id", ItemType.FIELD, section, ref bitOffset, 8, ref tableId);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "section_syntax_indicator", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "reserved_future_use", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "section_length", ItemType.FIELD, section, ref bitOffset, 12, ref fieldValue);
            }

            Int64 networkId = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "network_id", ItemType.FIELD, section, ref bitOffset, 16, ref networkId);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue);
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

            Int64 lastSectionNumber = 0;
            TreeNode peerNode = null;//Special point!!!!!!!!
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

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.PEER, peerNode, out peerNode, "reserved_future_use", ItemType.FIELD, section, ref bitOffset, 4, ref fieldValue);
            }

            Int64 networkDescriptorsLength = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.PEER, peerNode, out peerNode, "network_descriptors_length", ItemType.FIELD, section, ref bitOffset, 12, ref networkDescriptorsLength);
            }

            //Parse descriptors according to networkDescriptorsLength.
            if (result.Fine)
            {
                //Convert to bits.
                networkDescriptorsLength = networkDescriptorsLength * 8;

                if (result.Fine)
                {
                    result = Utility.AddNodeContainer(Position.PEER, peerNode, out peerNode, "descriptor_loop", ItemType.LOOP, section, bitOffset, networkDescriptorsLength);
                }

                if (result.Fine)
                {
                    result = Descriptor.ParseDescriptorLoop(Scope.MPEG, peerNode, section, ref bitOffset, ref networkDescriptorsLength);
                }
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.PEER, peerNode, out peerNode, "reserved_future_use", ItemType.FIELD, section, ref bitOffset, 4, ref fieldValue);
            }

            Int64 transportStreamLoopLength = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.PEER, peerNode, out peerNode, "transport_stream_loop_length", ItemType.FIELD, section, ref bitOffset, 12, ref transportStreamLoopLength);
            }

            if (result.Fine)
            {
                //Convert to bits.
                transportStreamLoopLength = transportStreamLoopLength * 8;

                result = Utility.AddNodeContainer(Position.PEER, peerNode, out peerNode, "transport_stream_loop", ItemType.LOOP, section, bitOffset, transportStreamLoopLength);

                if (result.Fine)
                {
                    Int64 transportStreamId = 0;
                    Int64 originalNetworkId = 0;
                    Int64 transportDescriptorsLength = 0;

                    TreeNode itemNode = null;
                    while (section.GetLeftBitLength() >= 6 * 8) //At least 6 bytes for each stream.
                    {
                        //Add a stream container.!!!!!!!!!!!!!!!!!!!!!!We will need to update the bit length for this node, since we currently doesn't know whether there are descriptors.
                        result = Utility.AddNodeContainer(Position.CHILD, peerNode, out itemNode, "transport_stream", ItemType.LOOP, section, bitOffset, 6 * 8);

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, itemNode, out newNode, "transport_stream_id", ItemType.FIELD, section, ref bitOffset, 16, ref transportStreamId);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, itemNode, out newNode, "original_network_id", ItemType.FIELD, section, ref bitOffset, 16, ref originalNetworkId);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, itemNode, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 4, ref fieldValue);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, itemNode, out newNode, "transport_descriptors_length", ItemType.FIELD, section, ref bitOffset, 12, ref transportDescriptorsLength);
                        }

                        if (result.Fine)
                        {
                            Int64 transportDescriptorsLengthInBits = transportDescriptorsLength * 8;//Convert to bits.

                            if (result.Fine)
                            {
                                result = Utility.AddNodeContainer(Position.CHILD, itemNode, out newNode, "descriptor_loop", ItemType.LOOP, section, bitOffset, transportDescriptorsLengthInBits);
                            }

                            if (result.Fine)
                            {
                                result = Descriptor.ParseDescriptorLoop(Scope.MPEG, newNode, section, ref bitOffset, ref transportDescriptorsLengthInBits);
                            }
                        }

                        if (result.Fine)
                        {
                            //Update the sectionDetail of item node.
                            Utility.UpdateNodeLength(itemNode, "transport_stream_id: " + Utility.GetValueHexString(transportStreamId, 16) + " original_network_id: " + Utility.GetValueHexString(originalNetworkId, 16) , ItemType.ITEM, (6 + transportDescriptorsLength) * 8);
                        }
                        else
                        {
                            Utility.UpdateNode(itemNode, "transport_stream(invalid)", ItemType.ERROR);

                            //Something is wrong! Break right away!
                            break;
                        }
                    }
                }
            }

            //Add all left data as unknown data.
            Utility.AddLastSecondNodeData(nodeSection, crcNode, out newNode, section, ItemType.ERROR, "unknown_data", ref bitOffset, section.GetLeftBitLength());

            if (result.Fine)
            {
                String sectionDescription = String.Format("table_id 0x{0,2:X2}, network_id 0x{1,4:X4}, version_number 0x{2,2:X2}, section_number 0x{3,2:X2}, last_section_number 0x{4,2:X2}",
                                                tableId, networkId, versionNumber, sectionNumber, lastSectionNumber);
                Utility.UpdateNode(nodeSection, sectionDescription, ItemType.PSI_SECTION);
            }
            else
            {
                Utility.UpdateNode(nodeSection, sectionDetail + "(invalid)", ItemType.ERROR);
            }
        }//ParseNIT

        public void ParseSDTActual(TreeView treeView, TreeNode parent, DataStore section)
        {
            ParseSDT(treeView, parent, section, "SDTActual", "SDT [Service Description Table-Actual Stream]");
        }

        public void ParseSDTOther(TreeView treeView, TreeNode parent, DataStore section)
        {
            ParseSDT(treeView, parent, section, "SDTOther", "SDT [Service Description Table-Other Stream]");
        }

        public void ParseSDT(TreeView treeView, TreeNode parent, DataStore section, String sectionGroup, String sectionDetail)
        {
            //Check whether there is an existing item named like "SDTActual" to group all SDT sections.
            TreeNode[] nodesFound = parent.Nodes.Find(sectionGroup, false);
            TreeNode nodeGroup = null;
            TreeNode nodeSection = null;
            TreeNode newNode = null;

            if (0 == nodesFound.Length)
            {
                //No item is named like "SDTActual". Let's create a new one.
                nodeGroup = Utility.AddChildNode(treeView, parent, sectionGroup, sectionDetail, ItemType.SI);
            }
            else
            {
                //Add the new section under the "SDTActual"-like item.
                nodeGroup = nodesFound[0];
            }

            //Add one node to indicate this section.
            Result result = Utility.AddNodeContainer(Position.CHILD, nodeGroup, out nodeSection, sectionGroup, ItemType.SI_SECTION, section, 0, section.GetLeftBitLength());

            Int64 bitOffset = 0;
            Int64 fieldValue = 0;

            Int64 tableId = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "table_id", ItemType.FIELD, section, ref bitOffset, 8, ref tableId);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "section_syntax_indicator", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "'0'", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "section_length", ItemType.FIELD, section, ref bitOffset, 12, ref fieldValue);
            }

            Int64 transportStreamId = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "transport_stream_id", ItemType.FIELD, section, ref bitOffset, 16, ref transportStreamId);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue);
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

            Int64 lastSectionNumber = 0;
            TreeNode peerNode = null;//Special point!!!!!!!!
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

            Int64 originalNetworkId = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.PEER, peerNode, out peerNode, "original_network_id", ItemType.FIELD, section, ref bitOffset, 16, ref originalNetworkId);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.PEER, peerNode, out peerNode, "reserved_future_use", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeContainer(Position.PEER, peerNode, out peerNode, "service_loop", ItemType.LOOP, section, bitOffset, section.GetLeftBitLength());

                if (result.Fine)
                {
                    Int64 serviceId = 0;
                    Int64 descriptorsLoopLength = 0;

                    TreeNode itemNode = null;
                    while (section.GetLeftBitLength() >= 5 * 8) //At least 5 bytes for each service.
                    {
                        //Add a service container.!!!!!!!!!!!!!!!!!!!!!!We will need to update the bit length for this node, since we currently doesn't know whether there are descriptors.
                        result = Utility.AddNodeContainer(Position.CHILD, peerNode, out itemNode, "service", ItemType.LOOP, section, bitOffset, 5 * 8);

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, itemNode, out newNode, "service_id", ItemType.FIELD, section, ref bitOffset, 16, ref serviceId);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, itemNode, out newNode, "reserved_future_use", ItemType.FIELD, section, ref bitOffset, 6, ref fieldValue);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, itemNode, out newNode, "EIT_schedule_flag", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, itemNode, out newNode, "EIT_present_following_flag", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, itemNode, out newNode, "running_status", ItemType.FIELD, section, ref bitOffset, 3, ref fieldValue);
                        }

                        Int64 freeCaMode = 0;
                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, itemNode, out newNode, "free_CA_mode", ItemType.FIELD, section, ref bitOffset, 1, ref freeCaMode);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, itemNode, out newNode, "descriptors_loop_length", ItemType.FIELD, section, ref bitOffset, 12, ref descriptorsLoopLength);
                        }

                        Int64 descriptorsLoopLengthInBits = 0;
                        if (result.Fine)
                        {
                            descriptorsLoopLengthInBits = descriptorsLoopLength * 8;//Convert to bits.
                            if (result.Fine)
                            {
                                result = Utility.AddNodeContainer(Position.CHILD, itemNode, out newNode, "service_descriptor_loop", ItemType.LOOP, section, bitOffset, descriptorsLoopLengthInBits);
                            }

                            if (result.Fine)
                            {
                                result = Descriptor.ParseDescriptorLoop(Scope.SI, newNode, section, ref bitOffset, ref descriptorsLoopLengthInBits);
                            }
                        }

                        if (result.Fine)
                        {
                            //Update the detail of item node.
                            Utility.UpdateNodeLength(itemNode, "service_id: " + Utility.GetValueHexString(serviceId, 16) + " freeCaMode: " + freeCaMode, ItemType.ITEM, (5 + descriptorsLoopLength) * 8);
                        }
                        else
                        {
                            Utility.UpdateNode(itemNode, "service(invalid)", ItemType.ERROR);

                            //Something is wrong! Break right away!
                            break;
                        }
                    }//while (section.GetLeftBitLength() >= 5 * 8)
                }
            }

            //Add all left data as unknown data.
            Utility.AddLastSecondNodeData(nodeSection, crcNode, out newNode, section, ItemType.ERROR, "unknown_data", ref bitOffset, section.GetLeftBitLength());

            if (result.Fine)
            {
                String sectionDescription = String.Format("table_id 0x{0,2:X2}, transport_stream_id 0x{1,4:X4}, version_number 0x{2,2:X2}, section_number 0x{3,2:X2}, last_section_number 0x{4,2:X2}",
                                                tableId, transportStreamId, versionNumber, sectionNumber, lastSectionNumber);
                Utility.UpdateNode(nodeSection, sectionDescription, ItemType.SI_SECTION);
            }
            else
            {
                Utility.UpdateNode(nodeSection, "SDT(invalid)", ItemType.ERROR);
            }
        }//ParseSDT

        public void ParseBAT(TreeView treeView, TreeNode parent, DataStore section)
        {
            //Check whether there is an existing item named "BAT" to group all BAT sections.
            TreeNode[] nodesFound = parent.Nodes.Find("BAT", false);
            TreeNode nodeGroup = null;
            TreeNode nodeSection = null;
            TreeNode newNode = null;

            if (0 == nodesFound.Length)
            {
                //No item is named "BAT". Let's create a new one.
                nodeGroup = Utility.AddChildNode(treeView, parent, "BAT", "BAT [Bouquet Association Table]", ItemType.SI);
            }
            else
            {
                //Add the new section under the "BAT" item.
                nodeGroup = nodesFound[0];
            }

            //Add one node to indicate this section.
            Result result = Utility.AddNodeContainer(Position.CHILD, nodeGroup, out nodeSection, "BAT", ItemType.SI_SECTION, section, 0, section.GetLeftBitLength());

            Int64 bitOffset = 0;
            Int64 fieldValue = 0;

            Int64 tableId = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "table_id", ItemType.FIELD, section, ref bitOffset, 8, ref tableId);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "section_syntax_indicator", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "reserved_future_use", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "section_length", ItemType.FIELD, section, ref bitOffset, 12, ref fieldValue);
            }

            Int64 bouquetId = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "bouquet_id", ItemType.FIELD, section, ref bitOffset, 16, ref bouquetId);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue);
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

            Int64 lastSectionNumber = 0;
            TreeNode peerNode = null;//Special point!!!!!!!!
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

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.PEER, peerNode, out peerNode, "reserved", ItemType.FIELD, section, ref bitOffset, 4, ref fieldValue);
            }

            Int64 bouquetDescriptorsLength = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.PEER, peerNode, out peerNode, "bouquet_descriptors_length", ItemType.FIELD, section, ref bitOffset, 12, ref bouquetDescriptorsLength);
            }

            //Parse first level descriptors according to descriptorsLoopLength.
            if (result.Fine)
            {
                //Convert to bits.
                bouquetDescriptorsLength = bouquetDescriptorsLength * 8;

                if (result.Fine)
                {
                    result = Utility.AddNodeContainer(Position.PEER, peerNode, out peerNode, "bouquet_descriptor_loop", ItemType.LOOP, section, bitOffset, bouquetDescriptorsLength);
                }

                if (result.Fine)
                {
                    result = Descriptor.ParseDescriptorLoop(Scope.SI, peerNode, section, ref bitOffset, ref bouquetDescriptorsLength);
                }
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.PEER, peerNode, out peerNode, "reserved", ItemType.FIELD, section, ref bitOffset, 4, ref fieldValue);
            }

            Int64 transportStreamLoopLength = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.PEER, peerNode, out peerNode, "transport_stream_loop_length", ItemType.FIELD, section, ref bitOffset, 12, ref transportStreamLoopLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeContainer(Position.PEER, peerNode, out peerNode, "transport_stream_loop", ItemType.LOOP, section, bitOffset, section.GetLeftBitLength());

                if (result.Fine)
                {
                    Int64 transportStreamId = 0;
                    Int64 originalNetworkId = 0;
                    Int64 transportDescriptorsLength = 0;

                    TreeNode itemNode = null;
                    while (section.GetLeftBitLength() >= 6 * 8) //At least 6 bytes for each stream.
                    {
                        //Add a stream container.!!!!!!!!!!!!!!!!!!!!!!We will need to update the bit length for this node, since we currently doesn't know whether there are descriptors.
                        result = Utility.AddNodeContainer(Position.CHILD, peerNode, out itemNode, "stream", ItemType.LOOP, section, bitOffset, 5 * 8);

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, itemNode, out newNode, "transport_stream_id", ItemType.FIELD, section, ref bitOffset, 16, ref transportStreamId);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, itemNode, out newNode, "original_network_id", ItemType.FIELD, section, ref bitOffset, 16, ref originalNetworkId);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, itemNode, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 4, ref fieldValue);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, itemNode, out newNode, "transport_descriptors_length", ItemType.FIELD, section, ref bitOffset, 12, ref transportDescriptorsLength);
                        }

                        Int64 transportDescriptorsLengthInBits = 0;
                        if (result.Fine)
                        {
                            transportDescriptorsLengthInBits = transportDescriptorsLength * 8;//Convert to bits.
                            if (result.Fine)
                            {
                                result = Utility.AddNodeContainer(Position.CHILD, itemNode, out newNode, "transport_stream_descriptor_loop", ItemType.LOOP, section, bitOffset, transportDescriptorsLengthInBits);
                            }

                            if (result.Fine)
                            {
                                result = Descriptor.ParseDescriptorLoop(Scope.SI, newNode, section, ref bitOffset, ref transportDescriptorsLengthInBits);
                            }
                        }

                        if (result.Fine)
                        {
                            //Update the detail of item node.
                            Utility.UpdateNodeLength(itemNode, "transport_stream_id: " + Utility.GetValueHexString(transportStreamId, 16) + " original_network_id: " + Utility.GetValueHexString(originalNetworkId, 16), ItemType.ITEM, (6 + transportDescriptorsLength) * 8);
                        }
                        else
                        {
                            Utility.UpdateNode(itemNode, "stream(invalid)", ItemType.ERROR);

                            //Something is wrong! Break right away!
                            break;
                        }
                    }//while (section.GetLeftBitLength() >= 6 * 8)
                }
            }

            //Add all left data as unknown data.
            Utility.AddLastSecondNodeData(nodeSection, crcNode, out newNode, section, ItemType.ERROR, "unknown_data", ref bitOffset, section.GetLeftBitLength());

            if (result.Fine)
            {
                String sectionDescription = String.Format("table_id 0x{0,2:X2}, bouquet_id 0x{1,4:X4}, version_number 0x{2,2:X2}, section_number 0x{3,2:X2}, last_section_number 0x{4,2:X2}",
                                                tableId, bouquetId, versionNumber, sectionNumber, lastSectionNumber);
                Utility.UpdateNode(nodeSection, sectionDescription, ItemType.SI_SECTION);
            }
            else
            {
                Utility.UpdateNode(nodeSection, "BAT(invalid)", ItemType.ERROR);
            }
        }//ParseBAT

        public void ParseEITActualPF(TreeView treeView, TreeNode parent, DataStore section)
        {
            ParseEIT(treeView, parent, section, "EITActualPF", "EIT [ Event Information Table-Actual Stream, Present/Following]");
        }

        public void ParseEITOtherPF(TreeView treeView, TreeNode parent, DataStore section)
        {
            ParseEIT(treeView, parent, section, "EITOtherPF", "EIT [ Event Information Table-Other Stream, Present/Following]");
        }

        public void ParseEITActualSchedule(TreeView treeView, TreeNode parent, DataStore section)
        {
            ParseEIT(treeView, parent, section, "EITActualSchedule", "EIT [ Event Information Table-Actual Stream, Schedule]");
        }

        public void ParseEITOtherSchedule(TreeView treeView, TreeNode parent, DataStore section)
        {
            ParseEIT(treeView, parent, section, "EITOtherSchedule", "EIT [ Event Information Table-Other Stream, Schedule]");
        }

        public void ParseEIT(TreeView treeView, TreeNode parent, DataStore section, String sectionGroup, String sectionDetail)
        {
            //Check whether there is an existing item named like "EITActualPF" to group all SDT sections.
            TreeNode[] nodesFound = parent.Nodes.Find(sectionGroup, false);
            TreeNode nodeGroup = null;
            TreeNode nodeSection = null;
            TreeNode newNode = null;

            if (0 == nodesFound.Length)
            {
                //No item is named like "EITActualPF". Let's create a new one.
                nodeGroup = Utility.AddChildNode(treeView, parent, sectionGroup, sectionDetail, ItemType.SI);
            }
            else
            {
                //Add the new section under the "EITActualPF"-like item.
                nodeGroup = nodesFound[0];
            }

            //Add one node to indicate this section.
            Result result = Utility.AddNodeContainer(Position.CHILD, nodeGroup, out nodeSection, sectionGroup, ItemType.SI_SECTION, section, 0, section.GetLeftBitLength());

            Int64 bitOffset = 0;
            Int64 fieldValue = 0;

            Int64 tableId = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "table_id", ItemType.FIELD, section, ref bitOffset, 8, ref tableId);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "section_syntax_indicator", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "reserved_future_use", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "section_length", ItemType.FIELD, section, ref bitOffset, 12, ref fieldValue);
            }

            Int64 serviceId = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "service_id", ItemType.FIELD, section, ref bitOffset, 16, ref serviceId);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue);
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

            Int64 lastSectionNumber = 0;
            TreeNode peerNode = null;//Special point!!!!!!!!
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

            Int64 transportStreamId = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.PEER, peerNode, out peerNode, "transport_stream_id", ItemType.FIELD, section, ref bitOffset, 16, ref transportStreamId);
            }

            Int64 originalNetworkId = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.PEER, peerNode, out peerNode, "original_network_id", ItemType.FIELD, section, ref bitOffset, 16, ref originalNetworkId);
            }

            Int64 segmentLastSectionNumber = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.PEER, peerNode, out peerNode, "segment_last_section_number", ItemType.FIELD, section, ref bitOffset, 8, ref segmentLastSectionNumber);
            }

            Int64 lastTableId = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.PEER, peerNode, out peerNode, "last_table_id", ItemType.FIELD, section, ref bitOffset, 8, ref lastTableId);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeContainer(Position.PEER, peerNode, out peerNode, "event_loop", ItemType.LOOP, section, bitOffset, section.GetLeftBitLength());

                if (result.Fine)
                {
                    Int64 eventId = 0;
                    Int64 startMjd = 0;
                    Int64 startTime = 0;
                    Int64 duration = 0;
                    Int64 descriptorsLoopLength = 0;

                    TreeNode itemNode = null;
                    while (section.GetLeftBitLength() >= 12 * 8) //At least 12 bytes for each event.
                    {
                        //Add a service container.!!!!!!!!!!!!!!!!!!!!!!We will need to update the bit length for this node, since we currently doesn't know whether there are descriptors.
                        result = Utility.AddNodeContainer(Position.CHILD, peerNode, out itemNode, "event", ItemType.LOOP, section, bitOffset, 12 * 8);

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, itemNode, out newNode, "event_id", ItemType.FIELD, section, ref bitOffset, 16, ref eventId);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, itemNode, out newNode, "start_MJD", ItemType.FIELD, section, ref bitOffset, 16, ref startMjd);
                        }
                        
                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, itemNode, out newNode, "start_time", ItemType.FIELD, section, ref bitOffset, 24, ref startTime);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, itemNode, out newNode, "duration", ItemType.FIELD, section, ref bitOffset, 24, ref duration);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, itemNode, out newNode, "running_status", ItemType.FIELD, section, ref bitOffset, 3, ref fieldValue);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, itemNode, out newNode, "free_CA_mode", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, itemNode, out newNode, "descriptors_loop_length", ItemType.FIELD, section, ref bitOffset, 12, ref descriptorsLoopLength);
                        }

                        Int64 descriptorsLoopLengthInBits = 0;
                        if (result.Fine)
                        {
                            descriptorsLoopLengthInBits = descriptorsLoopLength * 8;//Convert to bits.
                            if (result.Fine)
                            {
                                result = Utility.AddNodeContainer(Position.CHILD, itemNode, out newNode, "event_descriptor_loop", ItemType.LOOP, section, bitOffset, descriptorsLoopLengthInBits);
                            }

                            if (result.Fine)
                            {
                                result = Descriptor.ParseDescriptorLoop(Scope.SI, newNode, section, ref bitOffset, ref descriptorsLoopLengthInBits);
                            }
                        }

                        if (result.Fine)
                        {
                            Int64 year = 0;
                            Int64 month = 0;
                            Int64 day = 0;

                            Utility.MjdToYmd(startMjd, ref year, ref month, ref day);

                            String eventDescription = String.Format("event_id: 0x{0, 4:X4}, start_time: {1, 4:D4}-{2, 2:D2}-{3, 2:D2} {4, 2:X2}:{5, 2:X2}:{6, 2:X2}, duration: {7, 2:X2}:{8, 2:X2}:{9, 2:X2}",
                                                            eventId,
                                                            year,
                                                            month,
                                                            day,
                                                            (startTime >> 16)&0xFF,
                                                            (startTime >> 8) & 0xFF,
                                                            (startTime) & 0xFF,
                                                            (duration >> 16) & 0xFF,
                                                            (duration >> 8) & 0xFF,
                                                            (duration) & 0xFF);

                            //Update the detail of item node.
                            Utility.UpdateNodeLength(itemNode, eventDescription, ItemType.ITEM, (12 + descriptorsLoopLength) * 8);
                        }
                        else
                        {
                            Utility.UpdateNode(itemNode, "event(invalid)", ItemType.ERROR);

                            //Something is wrong! Break right away!
                            break;
                        }
                    }//while (section.GetLeftBitLength() >= 12 * 8)
                }
            }

            //Add all left data as unknown data.
            Utility.AddLastSecondNodeData(nodeSection, crcNode, out newNode, section, ItemType.ERROR, "unknown_data", ref bitOffset, section.GetLeftBitLength());

            if (result.Fine)
            {
                String sectionDescription = String.Format("table_id 0x{0,2:X2}, service_id 0x{1,4:X4}, version_number 0x{2,2:X2}, section_number 0x{3,2:X2}, last_section_number 0x{4,2:X2}",
                                                tableId, serviceId, versionNumber, sectionNumber, lastSectionNumber);
                Utility.UpdateNode(nodeSection, sectionDescription, ItemType.SI_SECTION);
            }
            else
            {
                Utility.UpdateNode(nodeSection, "EIT(invalid)", ItemType.ERROR);
            }
        }//ParseEIT

        public void ParseTOT(TreeView treeView, TreeNode parent, DataStore section)
        {
            //Check whether there is an existing item named "TOT" to group all TOT sections.
            TreeNode[] nodesFound = parent.Nodes.Find("TOT", false);
            TreeNode nodeGroup = null;
            TreeNode nodeSection = null;
            TreeNode newNode = null;

            if (0 == nodesFound.Length)
            {
                //No item is named "TOT". Let's create a new one.
                nodeGroup = Utility.AddChildNode(treeView, parent, "TOT", "TOT [Time Offset Table]", ItemType.SI);
            }
            else
            {
                //Add the new section under the "TOT" item.
                nodeGroup = nodesFound[0];
            }

            //Add one node to indicate this section.
            Result result = Utility.AddNodeContainer(Position.CHILD, nodeGroup, out nodeSection, "TOT", ItemType.SI_SECTION, section, 0, section.GetLeftBitLength());

            Int64 bitOffset = 0;
            Int64 fieldValue = 0;

            Int64 tableId = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "table_id", ItemType.FIELD, section, ref bitOffset, 8, ref tableId);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "section_syntax_indicator", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "reserved_future_use", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "section_length", ItemType.FIELD, section, ref bitOffset, 12, ref fieldValue);
            }

            Int64 mjd = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "MJD", ItemType.FIELD, section, ref bitOffset, 16, ref mjd);
            }

            Int64 time = 0;
            TreeNode peerNode = null;//Special point!!!!!!!!
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out peerNode, "time", ItemType.FIELD, section, ref bitOffset, 24, ref time);
            }

            //Parse crc32 first.
            TreeNode crcNode = null;
            if (result.Fine)
            {
                result = Utility.AddCrc32Node(nodeSection, out crcNode, section, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.PEER, peerNode, out peerNode, "reserved", ItemType.FIELD, section, ref bitOffset, 4, ref fieldValue);
            }

            Int64 descriptorsLoopLength = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.PEER, peerNode, out peerNode, "descriptors_loop_length", ItemType.FIELD, section, ref bitOffset, 12, ref descriptorsLoopLength);
            }

            //Parse first level descriptors according to descriptorsLoopLength.
            if (result.Fine)
            {
                //Convert to bits.
                descriptorsLoopLength = descriptorsLoopLength * 8;

                if (result.Fine)
                {
                    result = Utility.AddNodeContainer(Position.PEER, peerNode, out peerNode, "descriptor_loop", ItemType.LOOP, section, bitOffset, descriptorsLoopLength);
                }

                if (result.Fine)
                {
                    result = Descriptor.ParseDescriptorLoop(Scope.SI, peerNode, section, ref bitOffset, ref descriptorsLoopLength);
                }
            }

            //Add all left data as unknown data.
            Utility.AddLastSecondNodeData(nodeSection, crcNode, out newNode, section, ItemType.ERROR, "unknown_data", ref bitOffset, section.GetLeftBitLength());

            if (result.Fine)
            {
                Int64 year = 0;
                Int64 month = 0;
                Int64 day = 0;

                Utility.MjdToYmd(mjd, ref year, ref month, ref day);

                String totDescription = String.Format("date_time: {0, 4:D4}-{1, 2:D2}-{2, 2:D2} {3, 2:X2}:{4, 2:X2}:{5, 2:X2}",
                                                year,
                                                month,
                                                day,
                                                (time >> 16) & 0xFF,
                                                (time >> 8) & 0xFF,
                                                (time) & 0xFF);

                Utility.UpdateNode(nodeSection, totDescription, ItemType.SI_SECTION);
            }
            else
            {
                Utility.UpdateNode(nodeSection, "TOT(invalid)", ItemType.ERROR);
            }
        }//ParseTOT

        public void ParseTDT(TreeView treeView, TreeNode parent, DataStore section)
        {
            //Check whether there is an existing item named "TDT" to group all TDT sections.
            TreeNode[] nodesFound = parent.Nodes.Find("TDT", false);
            TreeNode nodeGroup = null;
            TreeNode nodeSection = null;
            TreeNode newNode = null;

            if (0 == nodesFound.Length)
            {
                //No item is named "TDT". Let's create a new one.
                nodeGroup = Utility.AddChildNode(treeView, parent, "TDT", "TDT [Time Date Table]", ItemType.SI);
            }
            else
            {
                //Add the new section under the "TDT" item.
                nodeGroup = nodesFound[0];
            }

            //Add one node to indicate this section.
            Result result = Utility.AddNodeContainer(Position.CHILD, nodeGroup, out nodeSection, "TDT", ItemType.SI_SECTION, section, 0, section.GetLeftBitLength());

            Int64 bitOffset = 0;
            Int64 fieldValue = 0;

            Int64 tableId = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "table_id", ItemType.FIELD, section, ref bitOffset, 8, ref tableId);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "section_syntax_indicator", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "reserved_future_use", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "section_length", ItemType.FIELD, section, ref bitOffset, 12, ref fieldValue);
            }

            Int64 mjd = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "MJD", ItemType.FIELD, section, ref bitOffset, 16, ref mjd);
            }

            Int64 time = 0;
            TreeNode peerNode = null;//Special point!!!!!!!!
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out peerNode, "time", ItemType.FIELD, section, ref bitOffset, 24, ref time);
            }

            if (section.GetLeftBitLength() > 0)
            {
                //Add all left data as unknown data.
                Utility.AddNodeData(Position.CHILD, nodeSection, out newNode, "unknown_data", ItemType.ERROR, section, ref bitOffset, section.GetLeftBitLength());
            }


            if (result.Fine)
            {
                Int64 year = 0;
                Int64 month = 0;
                Int64 day = 0;

                Utility.MjdToYmd(mjd, ref year, ref month, ref day);

                String tdtDescription = String.Format("date_time: {0, 4:D4}-{1, 2:D2}-{2, 2:D2} {3, 2:X2}:{4, 2:X2}:{5, 2:X2}",
                                                year,
                                                month,
                                                day,
                                                (time >> 16) & 0xFF,
                                                (time >> 8) & 0xFF,
                                                (time) & 0xFF);

                Utility.UpdateNode(nodeSection, tdtDescription, ItemType.SI_SECTION);
            }
            else
            {
                Utility.UpdateNode(nodeSection, "TOT(invalid)", ItemType.ERROR);
            }
        }//ParseTDT

        public void ParseRST(TreeView treeView, TreeNode parent, DataStore section)
        {
            //Check whether there is an existing item named "RST" to group all RST sections.
            TreeNode[] nodesFound = parent.Nodes.Find("RST", false);
            TreeNode nodeGroup = null;
            TreeNode nodeSection = null;
            TreeNode newNode = null;

            if (0 == nodesFound.Length)
            {
                //No item is named "RST". Let's create a new one.
                nodeGroup = Utility.AddChildNode(treeView, parent, "RST", "RST [Running Status Table]", ItemType.SI);
            }
            else
            {
                //Add the new section under the "RST" item.
                nodeGroup = nodesFound[0];
            }

            //Add one node to indicate this section.
            Result result = Utility.AddNodeContainer(Position.CHILD, nodeGroup, out nodeSection, "RST", ItemType.SI_SECTION, section, 0, section.GetLeftBitLength());

            Int64 bitOffset = 0;
            Int64 fieldValue = 0;

            Int64 tableId = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "table_id", ItemType.FIELD, section, ref bitOffset, 8, ref tableId);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "section_syntax_indicator", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "reserved_future_use", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue);
            }

            Int64 sectionLength = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "section_length", ItemType.FIELD, section, ref bitOffset, 12, ref sectionLength);
            }

            if (result.Fine)
            {
                TreeNode loopNode = null;
                result = Utility.AddNodeContainer(Position.CHILD, nodeSection, out loopNode, "event_loop", ItemType.LOOP, section, bitOffset, section.GetLeftBitLength());

                if (result.Fine)
                {
                    Int64 transportStreamId = 0;
                    Int64 originalNetworkId = 0;
                    Int64 serviceId = 0;
                    Int64 eventId = 0;
                    Int64 runningStatus = 0;

                    TreeNode itemNode = null;
                    while (section.GetLeftBitLength() >= 9 * 8) //At least 6 bytes for each stream.
                    {
                        //Add an event container.
                        result = Utility.AddNodeContainer(Position.CHILD, loopNode, out itemNode, "event", ItemType.LOOP, section, bitOffset, 9 * 8);

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, itemNode, out newNode, "transport_stream_id", ItemType.FIELD, section, ref bitOffset, 16, ref transportStreamId);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, itemNode, out newNode, "original_network_id", ItemType.FIELD, section, ref bitOffset, 16, ref originalNetworkId);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, itemNode, out newNode, "service_id", ItemType.FIELD, section, ref bitOffset, 16, ref serviceId);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, itemNode, out newNode, "event_id", ItemType.FIELD, section, ref bitOffset, 16, ref eventId);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, itemNode, out newNode, "reserved_future_use", ItemType.FIELD, section, ref bitOffset, 5, ref fieldValue);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, itemNode, out newNode, "running_status", ItemType.FIELD, section, ref bitOffset, 3, ref runningStatus);
                        }
                       
                        if (result.Fine)
                        {
                            //Update the detail of item node.
                            Utility.UpdateNode(itemNode, "transport_stream_id: " + Utility.GetValueHexString(transportStreamId, 16)
                                                            + " service_id: " + Utility.GetValueHexString(serviceId, 16)
                                                            + " event_id: " + Utility.GetValueHexString(eventId, 16)
                                                            + " running_status: " + Utility.GetValueHexString(runningStatus, 8), 
                                                            ItemType.ITEM);
                        }
                        else
                        {
                            Utility.UpdateNode(itemNode, "event(invalid)", ItemType.ERROR);

                            //Something is wrong! Break right away!
                            break;
                        }
                    }//while (section.GetLeftBitLength() >= 9 * 8)
                }
            }

            if (section.GetLeftBitLength() > 0)
            {
                //Add all left data as unknown data.
                Utility.AddNodeData(Position.CHILD, nodeSection, out newNode, "unknown_data", ItemType.ERROR, section, ref bitOffset, section.GetLeftBitLength());
            }

            if (result.Fine)
            {
                String sectionDescription = String.Format("table_id 0x{0,2:X2}, section_length 0x{1,4:X4}",
                                                tableId,
                                                sectionLength);
                Utility.UpdateNode(nodeSection, sectionDescription, ItemType.SI_SECTION);
            }
            else
            {
                Utility.UpdateNode(nodeSection, "RST(invalid)", ItemType.ERROR);
            }
        }//ParseRST

        public void ParseDIT(TreeView treeView, TreeNode parent, DataStore section)
        {
            //Check whether there is an existing item named "DIT" to group all DIT sections.
            TreeNode[] nodesFound = parent.Nodes.Find("DIT", false);
            TreeNode nodeGroup = null;
            TreeNode nodeSection = null;
            TreeNode newNode = null;

            if (0 == nodesFound.Length)
            {
                //No item is named "DIT". Let's create a new one.
                nodeGroup = Utility.AddChildNode(treeView, parent, "DIT", "DIT [Discontinuity Information Table]", ItemType.SI);
            }
            else
            {
                //Add the new section under the "DIT" item.
                nodeGroup = nodesFound[0];
            }

            //Add one node to indicate this section.
            Result result = Utility.AddNodeContainer(Position.CHILD, nodeGroup, out nodeSection, "DIT", ItemType.SI_SECTION, section, 0, section.GetLeftBitLength());

            Int64 bitOffset = 0;
            Int64 fieldValue = 0;

            Int64 tableId = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "table_id", ItemType.FIELD, section, ref bitOffset, 8, ref tableId);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "section_syntax_indicator", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "reserved_future_use", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue);
            }

            Int64 sectionLength = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "section_length", ItemType.FIELD, section, ref bitOffset, 12, ref sectionLength);
            }

            Int64 transitionFlag = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "transition_flag", ItemType.FIELD, section, ref bitOffset, 1, ref transitionFlag);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 7, ref fieldValue);
            }

            if (section.GetLeftBitLength() > 0)
            {
                //Add all left data as unknown data.
                Utility.AddNodeData(Position.CHILD, nodeSection, out newNode, "unknown_data", ItemType.ERROR, section, ref bitOffset, section.GetLeftBitLength());
            }

            if (result.Fine)
            {
                String sectionDescription = String.Format("table_id 0x{0,2:X2}, section_length 0x{1,4:X4}, transition_flag 0x{2,2:X2}",
                                                tableId,
                                                sectionLength,
                                                transitionFlag);
                Utility.UpdateNode(nodeSection, sectionDescription, ItemType.SI_SECTION);
            }
            else
            {
                Utility.UpdateNode(nodeSection, "DIT(invalid)", ItemType.ERROR);
            }
        }//ParseDIT

        public void ParseSIT(TreeView treeView, TreeNode parent, DataStore section)
        {
            //Check whether there is an existing item named "SIT" to group all SIT sections.
            TreeNode[] nodesFound = parent.Nodes.Find("SIT", false);
            TreeNode nodeGroup = null;
            TreeNode nodeSection = null;
            TreeNode newNode = null;

            if (0 == nodesFound.Length)
            {
                //No item is named "SIT". Let's create a new one.
                nodeGroup = Utility.AddChildNode(treeView, parent, "SIT", "SIT [Selection Information Table]", ItemType.SI);
            }
            else
            {
                //Add the new section under the "SIT" item.
                nodeGroup = nodesFound[0];
            }

            //Add one node to indicate this section.
            Result result = Utility.AddNodeContainer(Position.CHILD, nodeGroup, out nodeSection, "SIT", ItemType.SI_SECTION, section, 0, section.GetLeftBitLength());

            Int64 bitOffset = 0;
            Int64 fieldValue = 0;

            Int64 tableId = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "table_id", ItemType.FIELD, section, ref bitOffset, 8, ref tableId);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "section_syntax_indicator", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "DVB_reserved_future_use", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "ISO_reserved", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "section_length", ItemType.FIELD, section, ref bitOffset, 12, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "DVB_reserved_future_use", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeSection, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue);
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

            Int64 lastSectionNumber = 0;
            TreeNode peerNode = null;//Special point!!!!!!!!
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

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.PEER, peerNode, out peerNode, "DVB_reserved_for_future_use", ItemType.FIELD, section, ref bitOffset, 4, ref fieldValue);
            }

            Int64 transmissionInfoLoopLength = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.PEER, peerNode, out peerNode, "transmission_info_loop_length", ItemType.FIELD, section, ref bitOffset, 12, ref transmissionInfoLoopLength);
            }

            //Parse first level descriptors according to descriptorsLoopLength.
            if (result.Fine)
            {
                //Convert to bits.
                transmissionInfoLoopLength = transmissionInfoLoopLength * 8;

                if (result.Fine)
                {
                    result = Utility.AddNodeContainer(Position.PEER, peerNode, out peerNode, "descriptor_loop", ItemType.LOOP, section, bitOffset, transmissionInfoLoopLength);
                }

                if (result.Fine)
                {
                    result = Descriptor.ParseDescriptorLoop(Scope.SI, peerNode, section, ref bitOffset, ref transmissionInfoLoopLength);
                }
            }

            if (result.Fine)
            {
                result = Utility.AddNodeContainer(Position.PEER, peerNode, out peerNode, "service_loop", ItemType.LOOP, section, bitOffset, section.GetLeftBitLength());

                if (result.Fine)
                {
                    Int64 serviceId = 0;
                    Int64 runningStatus = 0;
                    Int64 serviceLoopLength = 0;

                    TreeNode itemNode = null;
                    while (section.GetLeftBitLength() >= 4 * 8) //At least 4 bytes for each service.
                    {
                        //Add a service container.!!!!!!!!!!!!!!!!!!!!!!We will need to update the bit length for this node, since we currently doesn't know whether there are descriptors.
                        result = Utility.AddNodeContainer(Position.CHILD, peerNode, out itemNode, "service", ItemType.LOOP, section, bitOffset, 4 * 8);

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, itemNode, out newNode, "service_id", ItemType.FIELD, section, ref bitOffset, 16, ref serviceId);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, itemNode, out newNode, "DVB_reserved_future_use", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, itemNode, out newNode, "running_status", ItemType.FIELD, section, ref bitOffset, 3, ref runningStatus);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, itemNode, out newNode, "service_loop_length", ItemType.FIELD, section, ref bitOffset, 12, ref serviceLoopLength);
                        }

                        Int64 serviceLoopLengthInBits = 0;
                        if (result.Fine)
                        {
                            serviceLoopLengthInBits = serviceLoopLength * 8;//Convert to bits.
                            if (result.Fine)
                            {
                                result = Utility.AddNodeContainer(Position.CHILD, itemNode, out newNode, "service_descriptor_loop", ItemType.LOOP, section, bitOffset, serviceLoopLengthInBits);
                            }

                            if (result.Fine)
                            {
                                result = Descriptor.ParseDescriptorLoop(Scope.SI, newNode, section, ref bitOffset, ref serviceLoopLengthInBits);
                            }
                        }

                        if (result.Fine)
                        {
                            //Update the detail of item node.
                            Utility.UpdateNodeLength(itemNode, "service_id: " + Utility.GetValueHexString(serviceId, 16) + " running_status: " + Utility.GetValueHexString(runningStatus, 8), ItemType.ITEM, (4 + serviceLoopLength) * 8);
                        }
                        else
                        {
                            Utility.UpdateNode(itemNode, "service(invalid)", ItemType.ERROR);

                            //Something is wrong! Break right away!
                            break;
                        }
                    }//while (section.GetLeftBitLength() >= 4 * 8)
                }
            }

            //Add all left data as unknown data.
            Utility.AddLastSecondNodeData(nodeSection, crcNode, out newNode, section, ItemType.ERROR, "unknown_data", ref bitOffset, section.GetLeftBitLength());

            if (result.Fine)
            {
                String sectionDescription = String.Format("table_id 0x{0,2:X2}, version_number 0x{1,2:X2}, section_number 0x{2,2:X2}, last_section_number 0x{3,2:X2}",
                                                tableId, versionNumber, sectionNumber, lastSectionNumber);
                Utility.UpdateNode(nodeSection, sectionDescription, ItemType.SI_SECTION);
            }
            else
            {
                Utility.UpdateNode(nodeSection, "SIT(invalid)", ItemType.ERROR);
            }
        }//ParseSIT
    }
}
