using InActionLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mpeg2VideoPesPacketParser
{
    class Mpeg2VideoParser
    {
        public Result Parse(TreeNode parentNode, DataStore dataStore, ref long bitOffset)
        {
            Result result = new Result();

            long bitOffsetStart = bitOffset;
            long lengthBetween2StartCode = 0;

            TreeNode nodeItem = null;
            TreeNode newNode = null;
            Int64 currentItemLength = 0;
            Int64 currentItemOffsetStart = 0;
            Int64 currentItemOffsetEnd = 0;
            Int64 fieldValue = 0;
            while (result.Fine)
            {
                bitOffsetStart = bitOffset;

                result = FindStartCode(dataStore, ref bitOffsetStart, ref lengthBetween2StartCode);//bitOffsetStart may be updated!!!!!!

                //Skip several bits if necessary.
                if(result.Fine)
                {
                    if(bitOffset != bitOffsetStart)
                    {
                        result = Utility.AddNodeData(Position.CHILD, parentNode, out nodeItem, "Ignored data", ItemType.FIELD, dataStore, ref bitOffset, (bitOffsetStart - bitOffset ));
                    }

                }

                nodeItem = null;
                if (result.Fine)
                {
                    currentItemLength = lengthBetween2StartCode * 8;

                    //Add a node to contain this item.
                    result = Utility.AddNodeContainer(Position.CHILD, parentNode, out nodeItem, "Picture", ItemType.ITEM, dataStore, bitOffset, currentItemLength);
                    
                }
                else
                {
                    currentItemLength = dataStore.GetLeftBitLength();
                    if (0 != currentItemLength)
                    {
                        //Add a node to contain this item.
                        result = Utility.AddNodeContainer(Position.CHILD, parentNode, out nodeItem, "Picture", ItemType.ITEM, dataStore, bitOffset, currentItemLength);
                    }

                }

                //Save offset.
                currentItemOffsetStart = bitOffset;
                /*
                 *  picture_start_code	00
                    slice_start_code	01 through AF
                    reserved	B0
                    reserved	B1
                    user_data_start_code	B2
                    sequence_header_code	B3
                    sequence_error_code	B4
                    extension_start_code	B5
                    reserved	B6
                    sequence_end_code	B7
                    group_start_code	B8
                    system start codes (see note)	B9 through FF

                 */
                Int64 startCode = 0;
                if (null != nodeItem)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "start code", ItemType.FIELD, dataStore, ref bitOffset, 32, ref startCode);

                    if (result.Fine)
                    {
                        if (startCode == 0x00000100)//picture_start_code
                        {
                            Utility.UpdateNode(nodeItem, "picture_start_code", ItemType.ITEM);

                            result = ParsePictureHeader(dataStore, nodeItem, ref bitOffset, ref fieldValue);                            
                        }
                        else if ((startCode >= 0x00000101) && (startCode <= 0x000001AF))//slice_start_code
                        {
                            result = Utility.UpdateNode(nodeItem, "slice_start_code", ItemType.ITEM);
                        }
                        else if (startCode == 0x000001B2)//user_data_start_code
                        {
                            result = Utility.UpdateNode(nodeItem, "user_data_start_code", ItemType.ITEM);
                        }
                        else if (startCode == 0x000001B3)//sequence_header_code
                        {
                            Utility.UpdateNode(nodeItem, "sequence_header_code", ItemType.ITEM);
                            result = ParseSequenceHeader(dataStore, nodeItem, ref bitOffset, ref fieldValue);
                        }
                        else if (startCode == 0x000001B4)//sequence_error_code
                        {
                            result = Utility.UpdateNode(nodeItem, "sequence_error_code", ItemType.ITEM);
                        }
                        else if (startCode == 0x000001B5)//extension_start_code
                        {
                            Utility.UpdateNode(nodeItem, "extension_start_code", ItemType.ITEM);
                            result = ParseExtension(dataStore, nodeItem, ref bitOffset, ref fieldValue);
                        }
                        else if (startCode == 0x000001B7)//sequence_end_code
                        {
                            result = Utility.UpdateNode(nodeItem, "sequence_end_code", ItemType.ITEM);
                        }
                        else if (startCode == 0x000001B8)//group_start_code
                        {
                            Utility.UpdateNode(nodeItem, "group_start_code", ItemType.ITEM);
                            result = ParseGroupOfPicturesHeader(dataStore, nodeItem, ref bitOffset, ref fieldValue);
                        }
                        else
                        {
                            result = Utility.UpdateNode(nodeItem, "unkown_start_code", ItemType.ITEM);
                        }
                    }
                }

                currentItemOffsetEnd = bitOffset;
                //Skip others.
                if ((currentItemOffsetEnd - currentItemOffsetStart) != currentItemLength)
                {
                    result = Utility.AddNodeData(Position.CHILD, nodeItem, out newNode, "Ignored data", ItemType.FIELD, dataStore, ref bitOffset, (currentItemLength - (currentItemOffsetEnd - currentItemOffsetStart)));
                }
            }

            return result;
        }

        private Result ParseExtension(DataStore dataStore, TreeNode nodeItem, ref long bitOffset, ref Int64 fieldValue)
        {
            Result result = new Result();
            TreeNode newNode = null;

            Int64 extension_start_code_identifier = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "extension_start_code_identifier", ItemType.FIELD, dataStore, ref bitOffset, 4, ref extension_start_code_identifier);
            }

            if (result.Fine)
            {
                /*
                 *  0000	reserved
                    0001	Sequence Extension ID
                    0010	Sequence Display Extension ID
                    0011	Quant Matrix Extension ID
                    0100 	Copyright Extension ID
                    0101	Sequence Scalable Extension ID
                    0110	reserved
                    0111	Picture Display Extension ID
                    1000	Picture Coding Extension ID
                    1001	Picture Spatial Scalable Extension ID
                    1010	Picture Temporal Scalable Extension ID
                    1011	reserved
                    1100	reserved
                    ...	...
                    1111	reserved

                 */
                switch (extension_start_code_identifier)
                {
                    case 0x1:
                        {
                            Utility.UpdateNode(nodeItem, "extension_start_code" + ": Sequence Extension ID", ItemType.ITEM);

                            result = ParseSequenceExtension(dataStore, nodeItem, ref bitOffset, ref fieldValue);
                            break;
                        }
                    case 0x2:
                        {
                            Utility.UpdateNode(nodeItem, "extension_start_code" + ": Sequence Display Extension ID", ItemType.ITEM);
                            result = ParseSequenceDisplayExtension(dataStore, nodeItem, ref bitOffset, ref fieldValue);    
                            break;
                        }
                    case 0x3:
                        {
                            Utility.UpdateNode(nodeItem, "extension_start_code" + ": Quant Matrix Extension ID", ItemType.ITEM);
                            result = ParseQuantMatrixExtension(dataStore, nodeItem, ref bitOffset, ref fieldValue);    
                            break;
                        }
                    case 0x4:
                        {
                            Utility.UpdateNode(nodeItem, "extension_start_code" + ": Copyright Extension ID", ItemType.ITEM);
                            result = ParseCopyrightExtension(dataStore, nodeItem, ref bitOffset, ref fieldValue);                                
                            break;
                        }
                    case 0x5:
                        {
                            Utility.UpdateNode(nodeItem, "extension_start_code" + ": Sequence Scalable Extension ID", ItemType.ITEM);
                            result = ParsSequenceScalableExtension(dataStore, nodeItem, ref bitOffset, ref fieldValue); 
                            break;
                        }
                    case 0x7:
                        {
                            Utility.UpdateNode(nodeItem, "extension_start_code" + ": Picture Display Extension ID", ItemType.ITEM);
                            break;
                        }
                    case 0x8:
                        {
                            Utility.UpdateNode(nodeItem, "extension_start_code" + ": Picture Coding Extension ID", ItemType.ITEM);
                            result = ParsePictureCodingExtension(dataStore, nodeItem, ref bitOffset, ref fieldValue); 
                            break;
                        }
                    case 0x9:
                        {
                            Utility.UpdateNode(nodeItem, "extension_start_code" + ": Picture Spatial Scalable Extension ID", ItemType.ITEM);
                            break;
                        }
                    case 0x10:
                        {
                            Utility.UpdateNode(nodeItem, "extension_start_code" + ": Picture Temporal Scalable Extension ID", ItemType.ITEM);
                            break;
                        }
                    default:
                        {
                            Utility.UpdateNode(nodeItem, "extension_start_code" + ": " + String.Format("0x{0,2:X2}", extension_start_code_identifier), ItemType.ITEM);
                            break;
                        }
                }
            }

            return result;
        }

        private Result ParsePictureCodingExtension(DataStore dataStore, TreeNode nodeItem, ref long bitOffset, ref Int64 fieldValue)
        {
            Result result = new Result();
            TreeNode newNode = null;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "f_code[0][0]", ItemType.FIELD, dataStore, ref bitOffset, 4, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "f_code[0][1]", ItemType.FIELD, dataStore, ref bitOffset, 4, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "f_code[1][0]", ItemType.FIELD, dataStore, ref bitOffset, 4, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "f_code[1][1]", ItemType.FIELD, dataStore, ref bitOffset, 4, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "intra_dc_precision", ItemType.FIELD, dataStore, ref bitOffset, 2, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "picture_structure", ItemType.FIELD, dataStore, ref bitOffset, 2, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "top_field_first", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "frame_pred_frame_dct", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "concealment_motion_vectors", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "q_scale_type", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
            }
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "intra_vlc_format", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
            }
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "alternate_scan", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
            }
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "repeat_first_field", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
            }
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "chroma_420_type", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
            }
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "progressive_frame", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
            }

            Int64 composite_display_flag = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "composite_display_flag", ItemType.FIELD, dataStore, ref bitOffset, 1, ref composite_display_flag);
            }

            if (result.Fine)
            {
                if (1 == composite_display_flag)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "v_axis", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);

                    if (result.Fine)
                    {
                        result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "field_sequence", ItemType.FIELD, dataStore, ref bitOffset, 3, ref fieldValue);
                    }
                    if (result.Fine)
                    {
                        result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "sub_carrier", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
                    }
                    if (result.Fine)
                    {
                        result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "burst_amplitude", ItemType.FIELD, dataStore, ref bitOffset, 7, ref fieldValue);
                    }
                    if (result.Fine)
                    {
                        result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "sub_carrier_phase", ItemType.FIELD, dataStore, ref bitOffset, 8, ref fieldValue);
                    }
                }
            }

            return result;
        }


        private Result ParsSequenceScalableExtension(DataStore dataStore, TreeNode nodeItem, ref long bitOffset, ref Int64 fieldValue)
        {
            Result result = new Result();
            TreeNode newNode = null;

            /**
             * scalable_mode	Meaning
                00	data partitioning
                01	spatial scalability
	
                10	SNR scalability
                11	temporal scalability
             */ 
            Int64 scalable_mode = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "scalable_mode", ItemType.FIELD, dataStore, ref bitOffset, 2, ref scalable_mode);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "layer_id", ItemType.FIELD, dataStore, ref bitOffset, 4, ref fieldValue);
            }

            if (result.Fine)
            {
                if (1 == scalable_mode)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "lower_layer_prediction_horizontal_size", ItemType.FIELD, dataStore, ref bitOffset, 14, ref fieldValue);

                    if (result.Fine)
                    {
                        result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "marker_bit", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
                    }

                    if (result.Fine)
                    {
                        result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "lower_layer_prediction_vertical_size", ItemType.FIELD, dataStore, ref bitOffset, 14, ref fieldValue);
                    }

                    if (result.Fine)
                    {
                        result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "horizontal_subsampling_factor_m", ItemType.FIELD, dataStore, ref bitOffset, 5, ref fieldValue);
                    }

                    if (result.Fine)
                    {
                        result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "horizontal_subsampling_factor_n", ItemType.FIELD, dataStore, ref bitOffset, 5, ref fieldValue);
                    }

                    if (result.Fine)
                    {
                        result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "vertical_subsampling_factor_m", ItemType.FIELD, dataStore, ref bitOffset, 5, ref fieldValue);
                    }

                    if (result.Fine)
                    {
                        result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "vertical_subsampling_factor_n", ItemType.FIELD, dataStore, ref bitOffset, 5, ref fieldValue);
                    }
                }
                else if (3 == scalable_mode)
                {
                    Int64 picture_mux_enable = 0;
                    result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "picture_mux_enable", ItemType.FIELD, dataStore, ref bitOffset, 1, ref picture_mux_enable);

                    if (result.Fine)
                    {
                        if (1 == picture_mux_enable)
                        {
                            result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "marker_bit", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
                        }
                        
                    }

                    if (result.Fine)
                    {
                        result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "picture_mux_order", ItemType.FIELD, dataStore, ref bitOffset, 3, ref fieldValue);
                    }

                    if (result.Fine)
                    {
                        result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "picture_mux_factor", ItemType.FIELD, dataStore, ref bitOffset, 3, ref fieldValue);
                    }
                }
            }


            return result;
        }


        private Result ParseCopyrightExtension(DataStore dataStore, TreeNode nodeItem, ref long bitOffset, ref Int64 fieldValue)
        {
            Result result = new Result();
            TreeNode newNode = null;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "copyright_flag", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "copyright_identifier", ItemType.FIELD, dataStore, ref bitOffset, 8, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "original_or_copy", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "reserved", ItemType.FIELD, dataStore, ref bitOffset, 7, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "marker_bit", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "copyright_number_1", ItemType.FIELD, dataStore, ref bitOffset, 20, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "marker_bit", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "copyright_number_2", ItemType.FIELD, dataStore, ref bitOffset, 22, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "marker_bit", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "copyright_number_3", ItemType.FIELD, dataStore, ref bitOffset, 22, ref fieldValue);
            }

            return result;
        }

        private Result ParseQuantMatrixExtension(DataStore dataStore, TreeNode nodeItem, ref long bitOffset, ref Int64 fieldValue)
        {
            Result result = new Result();
            TreeNode newNode = null;

            Int64 load_intra_quantiser_matrix = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "load_intra_quantiser_matrix", ItemType.FIELD, dataStore, ref bitOffset, 1, ref load_intra_quantiser_matrix);
            }

            if (result.Fine)
            {
                if (1 == load_intra_quantiser_matrix)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "intra_quantiser_matrix[64]", ItemType.FIELD, dataStore, ref bitOffset, 8*64, ref fieldValue);
                }
            }

            Int64 load_non_intra_quantiser_matrix = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "load_non_intra_quantiser_matrix", ItemType.FIELD, dataStore, ref bitOffset, 1, ref load_non_intra_quantiser_matrix);
            }

            if (result.Fine)
            {
                if (1 == load_non_intra_quantiser_matrix)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "non_intra_quantiser_matrix[64]", ItemType.FIELD, dataStore, ref bitOffset, 8 * 64, ref fieldValue);
                }
            }

            Int64 load_chroma_intra_quantiser_matrix = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "load_chroma_intra_quantiser_matrix", ItemType.FIELD, dataStore, ref bitOffset, 1, ref load_chroma_intra_quantiser_matrix);
            }

            if (result.Fine)
            {
                if (1 == load_chroma_intra_quantiser_matrix)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "chroma_intra_quantiser_matrix[64]", ItemType.FIELD, dataStore, ref bitOffset, 8 * 64, ref fieldValue);
                }
            }

            Int64 load_chroma_non_intra_quantiser_matrix = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "load_chroma_non_intra_quantiser_matrix", ItemType.FIELD, dataStore, ref bitOffset, 1, ref load_chroma_non_intra_quantiser_matrix);
            }

            if (result.Fine)
            {
                if (1 == load_chroma_non_intra_quantiser_matrix)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "chroma_non_intra_quantiser_matrix[64]", ItemType.FIELD, dataStore, ref bitOffset, 8 * 64, ref fieldValue);
                }
            }

            return result;
        }

        private Result ParseSequenceDisplayExtension(DataStore dataStore, TreeNode nodeItem, ref long bitOffset, ref Int64 fieldValue)
        {
            Result result = new Result();
            TreeNode newNode = null;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "video_format", ItemType.FIELD, dataStore, ref bitOffset, 3, ref fieldValue);
            }

            Int64 colour_description = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "colour_description", ItemType.FIELD, dataStore, ref bitOffset, 1, ref colour_description);
            }

            if (result.Fine)
            {
                if (1 == colour_description)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "colour_primaries", ItemType.FIELD, dataStore, ref bitOffset, 8, ref fieldValue);

                    if (result.Fine)
                    {
                        result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "transfer_characteristics", ItemType.FIELD, dataStore, ref bitOffset, 8, ref fieldValue);
                    }
                    if (result.Fine)
                    {
                        result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "matrix_coefficients", ItemType.FIELD, dataStore, ref bitOffset, 8, ref fieldValue);
                    }

                }
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "display_horizontal_size", ItemType.FIELD, dataStore, ref bitOffset, 14, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "marker_bit", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "display_vertical_size", ItemType.FIELD, dataStore, ref bitOffset, 14, ref fieldValue);
            }
            return result;
        }

        private Result ParseSequenceExtension(DataStore dataStore, TreeNode nodeItem, ref long bitOffset, ref Int64 fieldValue)
        {
            Result result = new Result();
            TreeNode newNode = null;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "profile_and_level_indication", ItemType.FIELD, dataStore, ref bitOffset, 8, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "progressive_sequence", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "chroma_format", ItemType.FIELD, dataStore, ref bitOffset, 2, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "horizontal_size_extension", ItemType.FIELD, dataStore, ref bitOffset, 2, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "vertical_size_extension", ItemType.FIELD, dataStore, ref bitOffset, 2, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "bit_rate_extension", ItemType.FIELD, dataStore, ref bitOffset, 12, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "marker_bit", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "vbv_buffer_size_extension", ItemType.FIELD, dataStore, ref bitOffset, 8, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "low_delay", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "frame_rate_extension_n", ItemType.FIELD, dataStore, ref bitOffset, 2, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "frame_rate_extension_d", ItemType.FIELD, dataStore, ref bitOffset, 5, ref fieldValue);
            }

            return result;
        }


        private Result ParseGroupOfPicturesHeader(DataStore dataStore, TreeNode nodeItem, ref long bitOffset, ref Int64 fieldValue)
        {
            Result result = new Result();
            TreeNode newNode = null;

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "time_code", ItemType.FIELD, dataStore, ref bitOffset, 25, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "closed_gop", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "broken_link", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
            }

            return result;
        }

        private Result ParsePictureHeader(DataStore dataStore, TreeNode nodeItem, ref long bitOffset, ref Int64 fieldValue)
        {
            Result result = new Result();
            TreeNode newNode = null;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "temporal_reference", ItemType.FIELD, dataStore, ref bitOffset, 10, ref fieldValue);
            }

            Int64 picture_coding_type = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "picture_coding_type", ItemType.FIELD, dataStore, ref bitOffset, 3, ref picture_coding_type);

                if (result.Fine)
                {
                    result = Utility.UpdateNode(nodeItem, "Picture:  " + GetPictureCodingTypeName(picture_coding_type), ItemType.PSI);
                }
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "vbv_delay", ItemType.FIELD, dataStore, ref bitOffset, 16, ref fieldValue);
            }

            if (result.Fine)
            {
                if ((picture_coding_type == 2) || (picture_coding_type == 3))
                {
                    if (result.Fine)
                    {
                        result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "full_pel_forward_vector", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
                    }

                    if (result.Fine)
                    {
                        result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "forward_f_code", ItemType.FIELD, dataStore, ref bitOffset, 3, ref fieldValue);
                    }
                }
            }

            if (result.Fine)
            {
                if (picture_coding_type == 3)
                {
                    if (result.Fine)
                    {
                        result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "full_pel_backward_vector", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
                    }

                    if (result.Fine)
                    {
                        result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "backward_f_code", ItemType.FIELD, dataStore, ref bitOffset, 3, ref fieldValue);
                    }
                }
            }

            return result;
        }


        private Result ParseSequenceHeader(DataStore dataStore, TreeNode nodeItem, ref long bitOffset,  ref Int64 fieldValue)
        {
            Result result = new Result();
            TreeNode newNode = null;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "horizontal_size_value", ItemType.FIELD, dataStore, ref bitOffset, 12, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "vertical_size_value", ItemType.FIELD, dataStore, ref bitOffset, 12, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "aspect_ratio_information", ItemType.FIELD, dataStore, ref bitOffset, 4, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "frame_rate_code", ItemType.FIELD, dataStore, ref bitOffset, 4, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "bit_rate_value", ItemType.FIELD, dataStore, ref bitOffset, 18, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "marker_bit", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "vbv_buffer_size_value", ItemType.FIELD, dataStore, ref bitOffset, 10, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "constrained_parameters_flag", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
            }

            Int64 load_intra_quantiser_matrix = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "load_intra_quantiser_matrix", ItemType.FIELD, dataStore, ref bitOffset, 1, ref load_intra_quantiser_matrix);
            }

            if (result.Fine)
            {
                if (1 == load_intra_quantiser_matrix)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "intra_quantiser_matrix[64]", ItemType.FIELD, dataStore, ref bitOffset, 8*64, ref fieldValue);
                }
            }

            Int64 load_non_intra_quantiser_matrix = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "load_non_intra_quantiser_matrix", ItemType.FIELD, dataStore, ref bitOffset, 1, ref load_non_intra_quantiser_matrix);
            }

            if (result.Fine)
            {
                if (1 == load_non_intra_quantiser_matrix)
                {
                    result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "non_intra_quantiser_matrix[64]", ItemType.FIELD, dataStore, ref bitOffset, 8 * 64, ref fieldValue);
                }
            }

            return result;
        }

        Result FindStartCode(DataStore dataStore, ref long bitOffsetStart, ref long lengthBetween2StartCode)
        {
            Result result = new Result();

            long byteOffsetStart = 0;
            long byteOffsetCurrent = bitOffsetStart/8;
            long dataLengthInByte = dataStore.GetLeftBitLength() / 8;
            byte[] dataBytes = dataStore.GetData();

            //Set to faiure by default.
            result.SetFailure();

            //Find first start code.
            while (dataLengthInByte >= 4)
            {
                if (((dataBytes[byteOffsetCurrent] & 0xFF) == 0x00)
                    && ((dataBytes[byteOffsetCurrent + 1] & 0xFF) == 0x00)
                    && ((dataBytes[byteOffsetCurrent + 2] & 0xFF) == 0x01))
                {
                    result.SetResult(ResultCode.SUCCESS);//Got one.
                    bitOffsetStart = byteOffsetCurrent * 8;//Save it.

                    byteOffsetStart = byteOffsetCurrent;

                    byteOffsetCurrent++;
                    dataLengthInByte--;

                    break;
                }
                else
                {
                    byteOffsetCurrent++;
                    dataLengthInByte--;
                }
            }

            //Find the second start code.
            if (result.Fine)
            {
                result.SetFailure();//Reset it again!!!!!!!!!!!!!!!


                while (dataLengthInByte >= 4)
                {
                    if (((dataBytes[byteOffsetCurrent] & 0xFF) == 0x00)
                        && ((dataBytes[byteOffsetCurrent + 1] & 0xFF) == 0x00)
                        && ((dataBytes[byteOffsetCurrent + 2] & 0xFF) == 0x01))
                    {
                        result.SetResult(ResultCode.SUCCESS);//Perfect! We already find what we want.
                        
                        //Calculate the length between 2 start codes.
                        lengthBetween2StartCode = byteOffsetCurrent - byteOffsetStart;

                        break;
                    }

                    byteOffsetCurrent++;
                    dataLengthInByte--;
                }
            }

            return result;
        }

        string GetPictureCodingTypeName(Int64 parameter)
        {
            switch (parameter)
            {
                case 0x0:
                    return "forbidden";
                case 0x1:
                    return "I intra-coded";
                case 0x2:
                    return "P predictive-coded";
                case 0x3:
                    return "B bidirectionally-predictive-coded";
                case 0x4:
                    return "D dc intra-coded";
                default:
                    return "Reserved";
            }
        }
    }
}
