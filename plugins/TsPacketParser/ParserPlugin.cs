using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InActionLibrary;
using System.Windows.Forms;

namespace CustomizedParser
{
    public class ParserPlugin: IParserPlugin
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
            return "TS Packet Parser";
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
            return "Plugin to parse individual TS packet.";
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
            return DataType.TS_PACKET;
        }

        /**
         * Parse the TS packet.
         * 
         * @param parentNode parent tree node to display the parsing result.
         * @param dataStore an instance containing the data to be parsed.
         * 
         * @retval
         *     void
         * 
         */
        public void Parse(TreeNode parentNode, DataStore dataStore)
        {
            TreeNode newNode = null;
            TreeNode tsPacketNode = null;

            //Add one node to indicate this dataStore.
            Result result = Utility.AddNodeContainer(Position.CHILD, parentNode, out tsPacketNode, "TS Paccket", ItemType.SEARCH_TS, dataStore, 0, dataStore.GetLeftBitLength());

            Int64 bitOffset = 0;
            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, tsPacketNode, out newNode, "sync_byte", ItemType.FIELD, dataStore, ref bitOffset, 8, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, tsPacketNode, out newNode, "transport_error_indicator", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
            }

            Int64 payloadUnitStartIndicator = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, tsPacketNode, out newNode, "payload_unit_start_indicator", ItemType.FIELD, dataStore, ref bitOffset, 1, ref payloadUnitStartIndicator);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, tsPacketNode, out newNode, "transport_priority", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
            }

            Int64 pid = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, tsPacketNode, out newNode, "PID", ItemType.FIELD, dataStore, ref bitOffset, 13, ref pid);
            }

            Int64 transportScramblingControl = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, tsPacketNode, out newNode, "transport_scrambling_control", ItemType.FIELD, dataStore, ref bitOffset, 2, ref transportScramblingControl);
            }

            Int64 adaptationFieldControl = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, tsPacketNode, out newNode, "adaptation_field_control", ItemType.FIELD, dataStore, ref bitOffset, 2, ref adaptationFieldControl);
            }

            Int64 continuityCounter = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, tsPacketNode, out newNode, "continuity_counter", ItemType.FIELD, dataStore, ref bitOffset, 4, ref continuityCounter);
            }

            if (result.Fine)
            {
                if ((2 == adaptationFieldControl) || (3 == adaptationFieldControl))
                {
                    TreeNode ifItemNode1 = null;
                    result = Utility.AddNodeContainer(Position.CHILD, tsPacketNode, out ifItemNode1, "if(adaptation_field_control = = '10' || adaptation_field_control = = '11')", ItemType.ITEM, dataStore, bitOffset, 0);

                    if (result.Fine)
                    {
                        Int64 adaptationFieldLength = 0;
                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, ifItemNode1, out newNode, "adaptation_field_length", ItemType.FIELD, dataStore, ref bitOffset, 8, ref adaptationFieldLength);
                        }

                        if (result.Fine)
                        {
                            result = ParseAdaptationField(dataStore, ifItemNode1, ref newNode, ref bitOffset, ref fieldValue, ref adaptationFieldLength);
                        }
                        
                    }
                }//if ((2 == adaptationFieldControl) || (3 == adaptationFieldControl))
            }

            if (result.Fine)
            {
                if ((1 == adaptationFieldControl) || (3 == adaptationFieldControl))
                {
                    TreeNode ifItemNode1 = null;
                    result = Utility.AddNodeContainer(Position.CHILD, tsPacketNode, out ifItemNode1, "if(adaptation_field_control = = '01' || adaptation_field_control = = '11')", ItemType.ITEM, dataStore, bitOffset, 0);

                    if (result.Fine)
                    {
                        result = Utility.AddNodeData(Position.CHILD, ifItemNode1, out newNode, "data_byte", ItemType.FIELD, dataStore, ref bitOffset, dataStore.GetLeftBitLength());
                    }

                }//if ((1 == adaptationFieldControl) || (3 == adaptationFieldControl))
            }

            //Check whether there is still data left.
            if (dataStore.GetLeftBitLength() > 0)
            {
                result = Utility.AddNodeData(Position.CHILD, tsPacketNode, out newNode, "unknown_data", ItemType.ERROR, dataStore, ref bitOffset, dataStore.GetLeftBitLength());

                //Set the result to failure in case some data left.
                result.SetResult(ResultCode.INVALID_DATA);
            }

            if (result.Fine)
            {
                String tsPacketDescription = String.Format("pid 0x{0,4:X4},payload_unit_start_indicator {1}, {2}, {3}, continuity_counter {4,2}",
                                                pid,
                                                payloadUnitStartIndicator,
                                                GetTransportScramblingControlName(transportScramblingControl),
                                                GetAdaptationFieldControlName(adaptationFieldControl),
                                                continuityCounter);
                Utility.UpdateNode(tsPacketNode, tsPacketDescription, ItemType.SEARCH_TS);
            }
            else
            {
                String tsPacketDescription = String.Format("pid 0x{0,4:X4},payload_unit_start_indicator {1}, {2}, {3}, continuity_counter {4,2}",
                                                pid,
                                                payloadUnitStartIndicator,
                                                GetTransportScramblingControlName(transportScramblingControl),
                                                GetAdaptationFieldControlName(adaptationFieldControl),
                                                continuityCounter);
                Utility.UpdateNode(tsPacketNode, tsPacketDescription, ItemType.ERROR);//Something wrong. Highlight it!
            }
        }

        private static Result ParseAdaptationField(DataStore dataStore, TreeNode parentNode, ref TreeNode newNode, ref Int64 bitOffset, ref Int64 fieldValue, ref Int64 adaptationFieldLength)
        {
            Result result = new Result();

            adaptationFieldLength = adaptationFieldLength * 8;//Convert to bits.
            if (adaptationFieldLength > 0)
            {
                TreeNode adaptationNode = null;
                result = Utility.AddNodeContainer(Position.CHILD, parentNode, out adaptationNode, "if (adaptation_field_length > 0)", ItemType.ITEM, dataStore, bitOffset, adaptationFieldLength);

                //To parse adaptation field.
                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, adaptationNode, out newNode, "discontinuity_indicator", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue, ref adaptationFieldLength);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, adaptationNode, out newNode, "random_access_indicator", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue, ref adaptationFieldLength);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, adaptationNode, out newNode, "elementary_stream_priority_indicator", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue, ref adaptationFieldLength);
                }

                Int64 pcrFlag = 0;
                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, adaptationNode, out newNode, "PCR_flag", ItemType.FIELD, dataStore, ref bitOffset, 1, ref pcrFlag, ref adaptationFieldLength);
                }

                Int64 opcrFlag = 0;
                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, adaptationNode, out newNode, "OPCR_flag", ItemType.FIELD, dataStore, ref bitOffset, 1, ref opcrFlag, ref adaptationFieldLength);
                }

                Int64 splicingPointFlag = 0;
                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, adaptationNode, out newNode, "splicing_point_flag", ItemType.FIELD, dataStore, ref bitOffset, 1, ref splicingPointFlag, ref adaptationFieldLength);
                }

                Int64 transportPrivateDataFlag = 0;
                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, adaptationNode, out newNode, "transport_private_data_flag", ItemType.FIELD, dataStore, ref bitOffset, 1, ref transportPrivateDataFlag, ref adaptationFieldLength);
                }

                Int64 adaptationFieldExtensionFlag = 0;
                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, adaptationNode, out newNode, "adaptation_field_extension_flag", ItemType.FIELD, dataStore, ref bitOffset, 1, ref adaptationFieldExtensionFlag, ref adaptationFieldLength);
                }

                if (result.Fine)
                {
                    if (1 == pcrFlag)
                    {
                        TreeNode ifItemNode = null;
                        result = Utility.AddNodeContainer(Position.CHILD, adaptationNode, out ifItemNode, "if (PCR_flag = =  '1')", ItemType.ITEM, dataStore, bitOffset, 0);

                        if (result.Fine)
                        {
                            result = Utility.AddNodeFieldPlus(Position.CHILD, ifItemNode, out newNode, "program_clock_reference_base", ItemType.FIELD, dataStore, ref bitOffset, 33, ref fieldValue, ref adaptationFieldLength);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeFieldPlus(Position.CHILD, ifItemNode, out newNode, "Reserved", ItemType.FIELD, dataStore, ref bitOffset, 6, ref fieldValue, ref adaptationFieldLength);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeFieldPlus(Position.CHILD, ifItemNode, out newNode, "program_clock_reference_extension", ItemType.FIELD, dataStore, ref bitOffset, 9, ref fieldValue, ref adaptationFieldLength);
                        }

                    }//if (1 == pcrFlag)
                }

                if (result.Fine)
                {
                    if (1 == opcrFlag)
                    {
                        TreeNode ifItemNode = null;
                        result = Utility.AddNodeContainer(Position.CHILD, adaptationNode, out ifItemNode, "if (OPCR_flag = = '1') ", ItemType.ITEM, dataStore, bitOffset, 0);

                        if (result.Fine)
                        {
                            result = Utility.AddNodeFieldPlus(Position.CHILD, ifItemNode, out newNode, "original_program_clock_reference_base", ItemType.FIELD, dataStore, ref bitOffset, 33, ref fieldValue, ref adaptationFieldLength);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeFieldPlus(Position.CHILD, ifItemNode, out newNode, "Reserved", ItemType.FIELD, dataStore, ref bitOffset, 6, ref fieldValue, ref adaptationFieldLength);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeFieldPlus(Position.CHILD, ifItemNode, out newNode, "original_program_clock_reference_extension", ItemType.FIELD, dataStore, ref bitOffset, 9, ref fieldValue, ref adaptationFieldLength);
                        }

                    }//if (1 == opcrFlag)
                }

                if (result.Fine)
                {
                    if (1 == splicingPointFlag)
                    {
                        TreeNode ifItemNode = null;
                        result = Utility.AddNodeContainer(Position.CHILD, adaptationNode, out ifItemNode, "if (splicing_point_flag = = '1')", ItemType.ITEM, dataStore, bitOffset, 0);

                        if (result.Fine)
                        {
                            result = Utility.AddNodeFieldPlus(Position.CHILD, ifItemNode, out newNode, "splice_countdown", ItemType.FIELD, dataStore, ref bitOffset, 8, ref fieldValue, ref adaptationFieldLength);
                        }

                    }//if (1 == splicingPointFlag)
                }

                if (result.Fine)
                {
                    if (1 == transportPrivateDataFlag)
                    {
                        TreeNode ifItemNode = null;
                        result = Utility.AddNodeContainer(Position.CHILD, adaptationNode, out ifItemNode, "if (transport_private_data_flag = = '1')", ItemType.ITEM, dataStore, bitOffset, 0);

                        Int64 transportPrivateDataLength = 0;
                        if (result.Fine)
                        {
                            result = Utility.AddNodeFieldPlus(Position.CHILD, ifItemNode, out newNode, "transport_private_data_length", ItemType.FIELD, dataStore, ref bitOffset, 8, ref transportPrivateDataLength, ref adaptationFieldLength);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeDataPlus(Position.CHILD, ifItemNode, out newNode, "private_data_byte", ItemType.FIELD, dataStore, ref bitOffset, 8 * transportPrivateDataLength, ref adaptationFieldLength);
                        }

                    }//if (1 == transportPrivateDataFlag)
                }

                if (result.Fine)
                {
                    if (1 == adaptationFieldExtensionFlag)
                    {
                        TreeNode ifItemNode = null;
                        result = Utility.AddNodeContainer(Position.CHILD, adaptationNode, out ifItemNode, "if (adaptation_field_extension_flag = = '1') ", ItemType.ITEM, dataStore, bitOffset, 0);

                        Int64 adaptationFieldExtensionLength = 0;
                        if (result.Fine)
                        {
                            result = Utility.AddNodeFieldPlus(Position.CHILD, ifItemNode, out newNode, "adaptation_field_extension_length", ItemType.FIELD, dataStore, ref bitOffset, 8, ref adaptationFieldExtensionLength, ref adaptationFieldLength);
                        }

                        //Make a copy of adaptationFieldLength, so that we can know the length of adaptation field extension.
                        Int64 adaptationFieldLengthTemp1 = adaptationFieldLength;

                        Int64 ltwFlag = 0;
                        if (result.Fine)
                        {
                            result = Utility.AddNodeFieldPlus(Position.CHILD, ifItemNode, out newNode, "ltw_flag", ItemType.FIELD, dataStore, ref bitOffset, 1, ref ltwFlag, ref adaptationFieldLength);
                        }

                        Int64 piecewiseRateFlag = 0;
                        if (result.Fine)
                        {
                            result = Utility.AddNodeFieldPlus(Position.CHILD, ifItemNode, out newNode, "piecewise_rate_flag", ItemType.FIELD, dataStore, ref bitOffset, 1, ref piecewiseRateFlag, ref adaptationFieldLength);
                        }

                        Int64 seamlessSpliceFlag = 0;
                        if (result.Fine)
                        {
                            result = Utility.AddNodeFieldPlus(Position.CHILD, ifItemNode, out newNode, "seamless_splice_flag", ItemType.FIELD, dataStore, ref bitOffset, 1, ref seamlessSpliceFlag, ref adaptationFieldLength);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeFieldPlus(Position.CHILD, ifItemNode, out newNode, "Reserved", ItemType.FIELD, dataStore, ref bitOffset, 5, ref fieldValue, ref adaptationFieldLength);
                        }

                        if (result.Fine)
                        {
                            if (1 == ltwFlag)
                            {
                                TreeNode ifItemNode4 = null;
                                result = Utility.AddNodeContainer(Position.CHILD, ifItemNode, out ifItemNode4, "if (ltw_flag = = '1')", ItemType.ITEM, dataStore, bitOffset, 0);

                                if (result.Fine)
                                {
                                    result = Utility.AddNodeFieldPlus(Position.CHILD, ifItemNode4, out newNode, "ltw_valid_flag", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue, ref adaptationFieldLength);
                                }

                                if (result.Fine)
                                {
                                    result = Utility.AddNodeFieldPlus(Position.CHILD, ifItemNode4, out newNode, "ltw_offset", ItemType.FIELD, dataStore, ref bitOffset, 15, ref fieldValue, ref adaptationFieldLength);
                                }
                            }//if (1 == ltwFlag)
                        }

                        if (result.Fine)
                        {
                            if (1 == piecewiseRateFlag)
                            {
                                TreeNode innerIfItemNode = null;
                                result = Utility.AddNodeContainer(Position.CHILD, ifItemNode, out innerIfItemNode, "if (piecewise_rate_flag = = '1')", ItemType.ITEM, dataStore, bitOffset, 0);

                                if (result.Fine)
                                {
                                    result = Utility.AddNodeFieldPlus(Position.CHILD, innerIfItemNode, out newNode, "reserved", ItemType.FIELD, dataStore, ref bitOffset, 2, ref fieldValue, ref adaptationFieldLength);
                                }

                                if (result.Fine)
                                {
                                    result = Utility.AddNodeFieldPlus(Position.CHILD, innerIfItemNode, out newNode, "piecewise_rate", ItemType.FIELD, dataStore, ref bitOffset, 22, ref fieldValue, ref adaptationFieldLength);
                                }
                            }//if (1 == piecewiseRateFlag)
                        }

                        if (result.Fine)
                        {
                            if (1 == seamlessSpliceFlag)
                            {
                                TreeNode innerIfItemNode = null;
                                result = Utility.AddNodeContainer(Position.CHILD, ifItemNode, out innerIfItemNode, "if (seamless_splice_flag = = '1')", ItemType.ITEM, dataStore, bitOffset, 0);

                                if (result.Fine)
                                {
                                    result = Utility.AddNodeFieldPlus(Position.CHILD, innerIfItemNode, out newNode, "Splice_type", ItemType.FIELD, dataStore, ref bitOffset, 4, ref fieldValue, ref adaptationFieldLength);
                                }

                                if (result.Fine)
                                {
                                    result = Utility.AddNodeFieldPlus(Position.CHILD, innerIfItemNode, out newNode, "DTS_next_AU[32..30]", ItemType.FIELD, dataStore, ref bitOffset, 3, ref fieldValue, ref adaptationFieldLength);
                                }

                                if (result.Fine)
                                {
                                    result = Utility.AddNodeFieldPlus(Position.CHILD, innerIfItemNode, out newNode, "marker_bit", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue, ref adaptationFieldLength);
                                }

                                if (result.Fine)
                                {
                                    result = Utility.AddNodeFieldPlus(Position.CHILD, innerIfItemNode, out newNode, "DTS_next_AU[29..15]", ItemType.FIELD, dataStore, ref bitOffset, 15, ref fieldValue, ref adaptationFieldLength);
                                }

                                if (result.Fine)
                                {
                                    result = Utility.AddNodeFieldPlus(Position.CHILD, innerIfItemNode, out newNode, "marker_bit", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue, ref adaptationFieldLength);
                                }

                                if (result.Fine)
                                {
                                    result = Utility.AddNodeFieldPlus(Position.CHILD, innerIfItemNode, out newNode, "DTS_next_AU[14..0]", ItemType.FIELD, dataStore, ref bitOffset, 15, ref fieldValue, ref adaptationFieldLength);
                                }

                                if (result.Fine)
                                {
                                    result = Utility.AddNodeFieldPlus(Position.CHILD, innerIfItemNode, out newNode, "marker_bit", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue, ref adaptationFieldLength);
                                }
                            }//if (1 == seamlessSpliceFlag)
                        }

                        //Make the second copy of adaptationFieldLength.
                        Int64 adaptationFieldLengthTemp2 = adaptationFieldLength;
                        if (result.Fine)
                        {
                            //Reserved byte. Its length should be adaptationFieldExtensionLength*8 - (adaptationFieldLengthTemp2 - adaptationFieldLengthTemp1)
                            Int64 reservedByteLength = adaptationFieldExtensionLength * 8 - (adaptationFieldLengthTemp1 - adaptationFieldLengthTemp2);
                            result = Utility.AddNodeDataPlus(Position.CHILD, ifItemNode, out newNode, "reserved", ItemType.FIELD, dataStore, ref bitOffset, reservedByteLength, ref adaptationFieldLength);
                        }
                    }//if (1 == adaptationFieldExtensionFlag)
                }


                //Make the left as stuffing bytes.
                if (result.Fine)
                {
                    result = Utility.AddNodeDataPlus(Position.CHILD, adaptationNode, out newNode, "stuffing_byte", ItemType.FIELD, dataStore, ref bitOffset, adaptationFieldLength, ref adaptationFieldLength);
                }

            }//if (adaptationFieldLength > 0)

            return result;
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

        private string GetTransportScramblingControlName(Int64 transportScramblingControl)
        {
            string name = null;
            switch (transportScramblingControl)
            {
                case 0:
                    name = "Not scrambled";
                    break;
                case 1:
                    name = "Reserved";
                    break;
                case 2:
                    name = "Scrambled with Even Key";
                    break;
                case 3:
                    name = "Scrambled with Odd Key";
                    break;
                default:
                    name = "Unknown";
                    break;
            }

            return name;
        }

        private string GetAdaptationFieldControlName(Int64 adaptationFieldControl)
        {
            string name = null;
            switch (adaptationFieldControl)
            {
                case 0:
                    name = @"Reserved for future use by ISO/IEC";
                    break;
                case 1:
                    name = @"No adaptation_field, payload only";
                    break;
                case 2:
                    name = @"Adaptation_field only, no payload";
                    break;
                case 3:
                    name = @"Adaptation_field followed by payload";
                    break;
                default:
                    name = @"Unknown";
                    break;
            }

            return name;
        }
    }//Class
}//Namespace
