using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InActionLibrary;
using System.Windows.Forms;
using Mpeg4VideoPesPacketParser;

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
            return "MPEG4 Video PES Packet Parser";
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
            return "Plugin to parse MPEG4 Video PES packet.";
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
            return DataType.PES_PACKET;
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
        public void Parse(TreeNode parentNode, DataStore dataStore)
        {
            TreeNode newNode = null;
            TreeNode pesPacketNode = null;

            //Add one node to indicate this dataStore.
            Result result = Utility.AddNodeContainer(Position.CHILD, parentNode, out pesPacketNode, "PES Paccket", ItemType.SEARCH_TS, dataStore, 0, dataStore.GetLeftBitLength());

            Int64 bitOffset = 0;
            Int64 fieldValue = 0;

            Int64 packetStartCodePrefix = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, pesPacketNode, out newNode, "packet_start_code_prefix", ItemType.FIELD, dataStore, ref bitOffset, 24, ref packetStartCodePrefix);
            }

            Int64 streamId = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, pesPacketNode, out newNode, "stream_id", ItemType.FIELD, dataStore, ref bitOffset, 8, ref streamId);
            }

            Int64 pesPacketLength = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, pesPacketNode, out newNode, "PES_packet_length", ItemType.FIELD, dataStore, ref bitOffset, 16, ref pesPacketLength);
            }

            if (result.Fine)
            {
                if (1 != packetStartCodePrefix)//0x000001
                {
                    //The content may be scrambled. We will not parse it in such case.
                    result = Utility.AddNodeData(Position.CHILD, pesPacketNode, out newNode, "PES_packet_data_byte", ItemType.FIELD, dataStore, ref bitOffset, dataStore.GetLeftBitLength());
                }
                else
                {
                    //It may be in a clear format. We will continue to do the parsing.


                    //It is a MPEG2 video stream.
                    /*if (StreamIdType.H262_13818_2_11172_2_14496_2_VIDEO_STREAM
                        == GetStreamTypeFromStreamId(streamId))*/
                    {
                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, pesPacketNode, out newNode, "'10'", ItemType.FIELD, dataStore, ref bitOffset, 2, ref fieldValue);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, pesPacketNode, out newNode, "PES_scrambling_control", ItemType.FIELD, dataStore, ref bitOffset, 2, ref fieldValue);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, pesPacketNode, out newNode, "PES_priority", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, pesPacketNode, out newNode, "data_alignment_indicator", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
                        }
                        
                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, pesPacketNode, out newNode, "copyright", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
                        }
                        
                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, pesPacketNode, out newNode, "original_or_copy", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
                        }

                        Int64 PTS_DTS_flags = 0;
                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, pesPacketNode, out newNode, "PTS_DTS_flags", ItemType.FIELD, dataStore, ref bitOffset, 2, ref PTS_DTS_flags);
                        }

                        Int64 ESCR_flag = 0;
                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, pesPacketNode, out newNode, "ESCR_flag", ItemType.FIELD, dataStore, ref bitOffset, 1, ref ESCR_flag);
                        }

                        Int64 ES_rate_flag = 0;
                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, pesPacketNode, out newNode, "ES_rate_flag", ItemType.FIELD, dataStore, ref bitOffset, 1, ref ES_rate_flag);
                        }

                        Int64 DSM_trick_mode_flag = 0;
                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, pesPacketNode, out newNode, "DSM_trick_mode_flag", ItemType.FIELD, dataStore, ref bitOffset, 1, ref DSM_trick_mode_flag);
                        }

                        Int64 additional_copy_info_flag = 0;
                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, pesPacketNode, out newNode, "additional_copy_info_flag", ItemType.FIELD, dataStore, ref bitOffset, 1, ref additional_copy_info_flag);
                        }

                        Int64 PES_CRC_flag = 0;
                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, pesPacketNode, out newNode, "PES_CRC_flag", ItemType.FIELD, dataStore, ref bitOffset, 1, ref PES_CRC_flag);
                        }

                        Int64 PES_extension_flag = 0;
                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, pesPacketNode, out newNode, "PES_extension_flag", ItemType.FIELD, dataStore, ref bitOffset, 1, ref PES_extension_flag);
                        }

                        Int64 PES_header_data_length = 0;
                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, pesPacketNode, out newNode, "PES_header_data_length", ItemType.FIELD, dataStore, ref bitOffset, 8, ref PES_header_data_length);
                        }

                        Int64 pesHeaderBitOffset = bitOffset;//Save it so tht we can know the size of stuffing bytes.
                        if (result.Fine)
                        {
                            result = ParsePtsDts(dataStore, ref newNode, pesPacketNode, ref bitOffset, ref fieldValue, PTS_DTS_flags);
                        }

                        if (result.Fine)
                        {
                            result = ParseEscr(dataStore, ref newNode, pesPacketNode, ref bitOffset, ref fieldValue, ESCR_flag);
                        }

                        if (result.Fine)
                        {
                            result = ParseEsRate(dataStore, ref newNode, pesPacketNode, ref bitOffset, ref fieldValue, ES_rate_flag);
                        }

                        if (result.Fine)
                        {
                            result = ParseDsmTrickMode(dataStore, ref newNode, pesPacketNode, ref bitOffset, ref fieldValue, DSM_trick_mode_flag);
                        }

                        if (result.Fine)
                        {
                            result = ParseAdditionalCopyInfo(dataStore, ref newNode, pesPacketNode, ref bitOffset, ref fieldValue, additional_copy_info_flag);
                        }

                        if (result.Fine)
                        {
                            result = ParsePesCrc(dataStore, ref newNode, pesPacketNode, ref bitOffset, ref fieldValue, PES_CRC_flag);
                        }

                        if (result.Fine)
                        {
                            result = ParsePesExtension(dataStore, ref newNode, pesPacketNode, ref bitOffset, ref fieldValue, PES_extension_flag);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeData(Position.CHILD, pesPacketNode, out newNode, "stuffing_byte", ItemType.FIELD, dataStore, ref bitOffset, PES_header_data_length * 8 - (bitOffset - pesHeaderBitOffset));
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeContainer(Position.CHILD, pesPacketNode, out newNode, "PES_payload", ItemType.ITEM, dataStore, bitOffset, dataStore.GetLeftBitLength());
                            if (result.Fine)
                            {
                                Mpeg4VideoParser parser = new Mpeg4VideoParser();
                                parser.Parse(newNode, dataStore, ref bitOffset);
                            }
                        }

                    }/*
                    else
                    {
                        result = Utility.AddNodeData(Position.CHILD, pesPacketNode, out newNode, "PES_packet_data_byte", ItemType.FIELD, dataStore, ref bitOffset, dataStore.GetLeftBitLength());
                    }*/
                }
            }

            if (result.Fine)
            {
                String pesPacketDescription = String.Format("packet_start_code_prefix:0x{0, 6:X6}, stream_id:0x{1, 2:X2}, PES_packet_length:0x{2, 4:X4}",
                                                packetStartCodePrefix,
                                                streamId,
                                                pesPacketLength);
                Utility.UpdateNode(pesPacketNode, pesPacketDescription, ItemType.SEARCH_PES);
            }
            else
            {
                String pesPacketDescription = String.Format("packet_start_code_prefix:0x{0, 6:X6}, stream_id:0x{1, 2:X2}, PES_packet_length:0x{2, 4:X4}",
                                                packetStartCodePrefix,
                                                streamId,
                                                pesPacketLength);
                Utility.UpdateNode(pesPacketNode, pesPacketDescription, ItemType.ERROR);//Something wrong. Highlight it!
            }
        }


        private Result ParsePesExtension(DataStore dataStore, ref TreeNode newNode, TreeNode pesPacketNode, ref Int64 bitOffset, ref Int64 fieldValue, Int64 PES_extension_flag)
        {
            TreeNode nodeIf = null;
            Result result = new Result();
            if (PES_extension_flag == 1)
            {
                result = Utility.AddNodeContainer(Position.CHILD, pesPacketNode, out nodeIf, "if ( PES_extension_flag == '1')", ItemType.ITEM, dataStore, 0, 0);

                Int64 PES_private_data_flag = 0;
                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeIf, out newNode, "PES_private_data_flag", ItemType.FIELD, dataStore, ref bitOffset, 1, ref PES_private_data_flag);
                }


                Int64 pack_header_field_flag = 0;
                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeIf, out newNode, "pack_header_field_flag", ItemType.FIELD, dataStore, ref bitOffset, 1, ref pack_header_field_flag);
                }

                Int64 program_packet_sequence_counter_flag = 0;
                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeIf, out newNode, "program_packet_sequence_counter_flag", ItemType.FIELD, dataStore, ref bitOffset, 1, ref program_packet_sequence_counter_flag);
                }

                Int64 P_STD_buffer_flag = 0;
                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeIf, out newNode, "P-STD_buffer_flag", ItemType.FIELD, dataStore, ref bitOffset, 1, ref P_STD_buffer_flag);
                }


                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeIf, out newNode, "Reserved", ItemType.FIELD, dataStore, ref bitOffset, 3, ref fieldValue);
                }

                Int64 PES_extension_flag_2 = 0;
                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeIf, out newNode, "PES_extension_flag_2", ItemType.FIELD, dataStore, ref bitOffset, 1, ref PES_extension_flag_2);
                }

                if (result.Fine)
                {
                    TreeNode nodeIf2 = null;
                    if (PES_private_data_flag == 1)
                    {
                        result = Utility.AddNodeContainer(Position.CHILD, nodeIf, out nodeIf2, "if ( PES_private_data_flag == '1')", ItemType.ITEM, dataStore, 0, 0);

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, nodeIf2, out newNode, "PES_private_data", ItemType.FIELD, dataStore, ref bitOffset, 128, ref fieldValue);
                        }
                    }
                }

                if (result.Fine)
                {
                    TreeNode nodeIf2 = null;
                    if (pack_header_field_flag == 1)
                    {
                        result = Utility.AddNodeContainer(Position.CHILD, nodeIf, out nodeIf2, "if (pack_header_field_flag == '1')", ItemType.ITEM, dataStore, 0, 0);

                        Int64 pack_field_length = 0;
                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, nodeIf2, out newNode, "pack_field_length", ItemType.FIELD, dataStore, ref bitOffset, 8, ref pack_field_length);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeData(Position.CHILD, nodeIf2, out newNode, "pack_header", ItemType.ITEM, dataStore, ref bitOffset, pack_field_length*8);
                        }
                    }
                }

                if (result.Fine)
                {
                    TreeNode nodeIf2 = null;
                    if (program_packet_sequence_counter_flag == 1)
                    {
                        result = Utility.AddNodeContainer(Position.CHILD, nodeIf, out nodeIf2, "if ( program_packet_sequence_counter_flag == '1')", ItemType.ITEM, dataStore, 0, 0);

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, nodeIf2, out newNode, "marker_bit", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, nodeIf2, out newNode, "program_packet_sequence_counter", ItemType.FIELD, dataStore, ref bitOffset, 7, ref fieldValue);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, nodeIf2, out newNode, "marker_bit", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, nodeIf2, out newNode, "MPEG1_MPEG2_identifier", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, nodeIf2, out newNode, "original_stuff_length", ItemType.FIELD, dataStore, ref bitOffset, 6, ref fieldValue);
                        }
                    }
                }

                if (result.Fine)
                {
                    TreeNode nodeIf2 = null;
                    if (P_STD_buffer_flag == 1)
                    {
                        result = Utility.AddNodeContainer(Position.CHILD, nodeIf, out nodeIf2, "if ( P-STD_buffer_flag == '1')", ItemType.ITEM, dataStore, 0, 0);

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, nodeIf2, out newNode, "'01'", ItemType.FIELD, dataStore, ref bitOffset, 2, ref fieldValue);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, nodeIf2, out newNode, "P-STD_buffer_scale", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, nodeIf2, out newNode, "P-STD_buffer_size", ItemType.FIELD, dataStore, ref bitOffset, 13, ref fieldValue);
                        }
                    }
                }

                if (result.Fine)
                {
                    TreeNode nodeIf2 = null;
                    if (PES_extension_flag_2 == 1)
                    {
                        result = Utility.AddNodeContainer(Position.CHILD, nodeIf, out nodeIf2, "if ( PES_extension_flag_2 == '1')", ItemType.ITEM, dataStore, 0, 0);

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, nodeIf2, out newNode, "marker_bit", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
                        }

                        Int64 PES_extension_field_length = 0;
                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, nodeIf2, out newNode, "PES_extension_field_length", ItemType.FIELD, dataStore, ref bitOffset, 7, ref PES_extension_field_length);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeData(Position.CHILD, nodeIf2, out newNode, "PES_extension_field", ItemType.FIELD, dataStore, ref bitOffset, PES_extension_field_length*8);
                        }
                    }
                }

            }

            return result;
        }

        private Result ParsePesCrc(DataStore dataStore, ref TreeNode newNode, TreeNode pesPacketNode, ref Int64 bitOffset, ref Int64 fieldValue, Int64 PES_CRC_flag)
        {
            TreeNode nodeIf = null;
            Result result = new Result();
            if (PES_CRC_flag == 1)
            {
                result = Utility.AddNodeContainer(Position.CHILD, pesPacketNode, out nodeIf, "if ( PES_CRC_flag == '1')", ItemType.ITEM, dataStore, 0, 0);

                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeIf, out newNode, "previous_PES_packet_CRC", ItemType.FIELD, dataStore, ref bitOffset, 16, ref fieldValue);
                }
            }

            return result;
        }

        private Result ParseAdditionalCopyInfo(DataStore dataStore, ref TreeNode newNode, TreeNode pesPacketNode, ref Int64 bitOffset, ref Int64 fieldValue, Int64 additional_copy_info_flag)
        {
            TreeNode nodeIf = null;
            Result result = new Result();
            if (additional_copy_info_flag == 1)
            {
                result = Utility.AddNodeContainer(Position.CHILD, pesPacketNode, out nodeIf, "if ( additional_copy_info_flag == '1')", ItemType.ITEM, dataStore, 0, 0);

                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeIf, out newNode, "marker_bit", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeIf, out newNode, "additional_copy_info", ItemType.FIELD, dataStore, ref bitOffset, 7, ref fieldValue);
                }
            }

            return result;
        }


        private Result ParseDsmTrickMode(DataStore dataStore, ref TreeNode newNode, TreeNode pesPacketNode, ref Int64 bitOffset, ref Int64 fieldValue, Int64 DSM_trick_mode_flag)
        {
            TreeNode nodeIf = null;
            Result result = new Result();
            if (DSM_trick_mode_flag == 1)
            {
                result = Utility.AddNodeContainer(Position.CHILD, pesPacketNode, out nodeIf, "if (DSM_trick_mode_flag == '1')", ItemType.ITEM, dataStore, 0, 0);

                Int64 trick_mode_control = 0;
                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeIf, out newNode, "trick_mode_control", ItemType.FIELD, dataStore, ref bitOffset, 3, ref trick_mode_control);
                }

                if (result.Fine)
                {
                    TreeNode nodeIf2 = null;
                    if (trick_mode_control == 0)
                    {
                        result = Utility.AddNodeContainer(Position.CHILD, nodeIf, out nodeIf2, "if ( trick_mode_control == fast_forward )", ItemType.ITEM, dataStore, 0, 0);

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, nodeIf2, out newNode, "field_id", ItemType.FIELD, dataStore, ref bitOffset, 2, ref fieldValue);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, nodeIf2, out newNode, "intra_slice_refresh", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, nodeIf2, out newNode, "frequency_truncation", ItemType.FIELD, dataStore, ref bitOffset, 2, ref fieldValue);
                        }
                    }
                    else if (trick_mode_control == 1)
                    {
                        result = Utility.AddNodeContainer(Position.CHILD, nodeIf, out nodeIf2, "if ( trick_mode_control == slow_motion )", ItemType.ITEM, dataStore, 0, 0);

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, nodeIf2, out newNode, "rep_cntrl", ItemType.FIELD, dataStore, ref bitOffset, 5, ref fieldValue);
                        }
                    }
                    else if (trick_mode_control == 2)
                    {
                        result = Utility.AddNodeContainer(Position.CHILD, nodeIf, out nodeIf2, "if ( trick_mode_control == freeze_frame )", ItemType.ITEM, dataStore, 0, 0);

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, nodeIf2, out newNode, "field_id", ItemType.FIELD, dataStore, ref bitOffset, 2, ref fieldValue);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, nodeIf2, out newNode, "Reserved", ItemType.FIELD, dataStore, ref bitOffset, 3, ref fieldValue);
                        }
                    }
                    else if (trick_mode_control == 3)
                    {
                        result = Utility.AddNodeContainer(Position.CHILD, nodeIf, out nodeIf2, "if ( trick_mode_control == fast_reverse )", ItemType.ITEM, dataStore, 0, 0);

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, nodeIf2, out newNode, "field_id", ItemType.FIELD, dataStore, ref bitOffset, 2, ref fieldValue);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, nodeIf2, out newNode, "intra_slice_refresh", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
                        }


                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, nodeIf2, out newNode, "frequency_truncation", ItemType.FIELD, dataStore, ref bitOffset, 2, ref fieldValue);
                        }
                    }
                    else if (trick_mode_control == 4)
                    {
                        result = Utility.AddNodeContainer(Position.CHILD, nodeIf, out nodeIf2, "if ( trick_mode_control == slow_reverse )", ItemType.ITEM, dataStore, 0, 0);

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, nodeIf2, out newNode, "rep_cntrl", ItemType.FIELD, dataStore, ref bitOffset, 5, ref fieldValue);
                        }
                    }
                    else
                    {
                        result = Utility.AddNodeContainer(Position.CHILD, nodeIf, out nodeIf2, "if ( trick_mode_control == unknown )", ItemType.ITEM, dataStore, 0, 0);

                        if (result.Fine)
                        {
                            result = Utility.AddNodeField(Position.CHILD, nodeIf2, out newNode, "Reserved", ItemType.FIELD, dataStore, ref bitOffset, 5, ref fieldValue);
                        }
                    }
                }
            }

            return result;
        }

        private Result ParseEsRate(DataStore dataStore, ref TreeNode newNode, TreeNode pesPacketNode, ref Int64 bitOffset, ref Int64 fieldValue, Int64 ES_rate_flag)
        {
            TreeNode nodeIf = null;
            Result result = new Result();
            if (ES_rate_flag == 1)
            {
                result = Utility.AddNodeContainer(Position.CHILD, pesPacketNode, out nodeIf, "if (ES_rate_flag == '1')", ItemType.ITEM, dataStore, 0, 0);

                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeIf, out newNode, "marker_bit", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeIf, out newNode, "ES_rate", ItemType.FIELD, dataStore, ref bitOffset, 22, ref fieldValue);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeIf, out newNode, "marker_bit", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
                }
            }

            return result;
        }

        private Result ParseEscr(DataStore dataStore, ref TreeNode newNode, TreeNode pesPacketNode, ref Int64 bitOffset, ref Int64 fieldValue, Int64 ESCR_flag)
        {
            TreeNode nodeIf = null;
            Result result = new Result();
            if (ESCR_flag == 1)
            {
                result = Utility.AddNodeContainer(Position.CHILD, pesPacketNode, out nodeIf, "if (ESCR_flag == '1')", ItemType.ITEM, dataStore, 0, 0);

                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeIf, out newNode, "Reserved", ItemType.FIELD, dataStore, ref bitOffset, 2, ref fieldValue);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeIf, out newNode, "ESCR_base[32..30]", ItemType.FIELD, dataStore, ref bitOffset, 3, ref fieldValue);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeIf, out newNode, "marker_bit", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeIf, out newNode, "ESCR_base[29..15]", ItemType.FIELD, dataStore, ref bitOffset, 15, ref fieldValue);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeIf, out newNode, "marker_bit", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeIf, out newNode, "ESCR_base[14..0]", ItemType.FIELD, dataStore, ref bitOffset, 15, ref fieldValue);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeIf, out newNode, "marker_bit", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeIf, out newNode, "ESCR_extension", ItemType.FIELD, dataStore, ref bitOffset, 9, ref fieldValue);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeIf, out newNode, "marker_bit", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
                }
            }

            return result;
        }

        private Result ParsePtsDts(DataStore dataStore, ref TreeNode newNode, TreeNode pesPacketNode, ref Int64 bitOffset, ref Int64 fieldValue, Int64 PTS_DTS_flags)
        {
            TreeNode nodeIf = null;
            Result result = new Result() ;
            if (PTS_DTS_flags == 2)
            {
                result = Utility.AddNodeContainer(Position.CHILD, pesPacketNode, out nodeIf, "if (PTS_DTS_flags == '10')", ItemType.ITEM, dataStore, 0, 0);

                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeIf, out newNode, "'0010'", ItemType.FIELD, dataStore, ref bitOffset, 4, ref fieldValue);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeIf, out newNode, "PTS [32..30]", ItemType.FIELD, dataStore, ref bitOffset, 3, ref fieldValue);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeIf, out newNode, "marker_bit", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeIf, out newNode, "PTS [29..15]", ItemType.FIELD, dataStore, ref bitOffset, 15, ref fieldValue);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeIf, out newNode, "marker_bit", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeIf, out newNode, "PTS [14..0]", ItemType.FIELD, dataStore, ref bitOffset, 15, ref fieldValue);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeIf, out newNode, "marker_bit", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
                }
            }
            else if (PTS_DTS_flags == 3)
            {
                result = Utility.AddNodeContainer(Position.CHILD, pesPacketNode, out nodeIf, "if (PTS_DTS_flags == '11')", ItemType.ITEM, dataStore, 0, 0);

                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeIf, out newNode, "'0011'", ItemType.FIELD, dataStore, ref bitOffset, 4, ref fieldValue);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeIf, out newNode, "PTS [32..30]", ItemType.FIELD, dataStore, ref bitOffset, 3, ref fieldValue);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeIf, out newNode, "marker_bit", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeIf, out newNode, "PTS [29..15]", ItemType.FIELD, dataStore, ref bitOffset, 15, ref fieldValue);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeIf, out newNode, "marker_bit", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeIf, out newNode, "PTS [14..0]", ItemType.FIELD, dataStore, ref bitOffset, 15, ref fieldValue);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeIf, out newNode, "marker_bit", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
                }


                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeIf, out newNode, "'0001'", ItemType.FIELD, dataStore, ref bitOffset, 4, ref fieldValue);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeIf, out newNode, "DTS [32..30]", ItemType.FIELD, dataStore, ref bitOffset, 3, ref fieldValue);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeIf, out newNode, "marker_bit", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeIf, out newNode, "DTS [29..15]", ItemType.FIELD, dataStore, ref bitOffset, 15, ref fieldValue);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeIf, out newNode, "marker_bit", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeIf, out newNode, "DTS [14..0]", ItemType.FIELD, dataStore, ref bitOffset, 15, ref fieldValue);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeIf, out newNode, "marker_bit", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
                }
            }

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

    }
}
