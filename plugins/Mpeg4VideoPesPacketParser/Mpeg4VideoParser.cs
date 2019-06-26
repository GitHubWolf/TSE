using InActionLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mpeg4VideoPesPacketParser
{
    class Mpeg4VideoParser
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
            Int64 startCodeBitOffsetDifference = 0;
            Mpeg4VideoInfo mpeg4VideoInfo = new Mpeg4VideoInfo();

            bool isFinalItem = false;
            while (result.Fine)
            {
                bitOffsetStart = bitOffset;

                startCodeBitOffsetDifference = 0;

                result = FindStartCode(dataStore, ref bitOffsetStart, ref lengthBetween2StartCode);//bitOffsetStart may be updated!!!!!!

                //Skip several bits if necessary.
                if (result.Fine)
                {
                    if (bitOffset != bitOffsetStart)
                    {
                        startCodeBitOffsetDifference = bitOffsetStart - bitOffset;
                        //result = Utility.AddNodeData(Position.CHILD, parentNode, out nodeItem, "Ignored data", ItemType.FIELD, dataStore, ref bitOffset, (bitOffsetStart - bitOffset ));
                    }

                }
                else
                {
                    isFinalItem = true;
                }

                nodeItem = null;
                if (result.Fine)
                {
                    currentItemLength = lengthBetween2StartCode * 8 + startCodeBitOffsetDifference;

                    //Add a node to contain this item.
                    result = Utility.AddNodeContainer(Position.CHILD, parentNode, out nodeItem, "NAL Unit", ItemType.ITEM, dataStore, bitOffset, currentItemLength);
                    
                }
                else
                {
                    currentItemLength = dataStore.GetLeftBitLength();
                    if (0 != currentItemLength)
                    {
                        //Add a node to contain this item.
                        result = Utility.AddNodeContainer(Position.CHILD, parentNode, out nodeItem, "NAL Unit", ItemType.ITEM, dataStore, bitOffset, currentItemLength);
                    }

                }

                //Save offset.
                currentItemOffsetStart = bitOffset;
                /*
                    0x0	Reserved for external use
                 * 
                    0x1	Coded slice
                    slice_layer_no_partitioning_rbsp( )
                 * 
                    0x2	Coded data partition A (DPA)
                    dpa_layer_rbsp( )
                 * 
                    0x3	Coded data partition B (DPB)
                    dpb_layer_rbsp( )
                 * 
                    0x4	Coded data partition C (DPC)
                    dpc_layer_rbsp( )
                 * 
                    0x5	Coded slice of an IDR picture
                    slice_layer_no_partitioning_rbsp( )
                 * 
                    0x6	Supplemental Enhancement Information (SEI)
                    sei_rbsp( )
                 *                  * 
                    0x7	Sequence Parameter Set (SPS)
                    seq_parameter_set_rbsp( )
                 * 
                    0x8	Picture Parameter Set (PPS)
                    pic_parameter_set_rbsp( )
                 * 
                    0x9	Access unit delimiter
                    access_unit_delimiter_rbsp(( )
                 * 
                    0xA	End of sequence
                    end_of_seq_rbsp( )
                 * 
                 *  0xB End of stream
                 *  end_of_stream_rbsp( )
                 *  
                 *  0xC	Filler data
                 *  filler_data_rbsp( )
                 * 
                    0xD – 0x17	Reserved
                 * 
                    0x18 – 0x1F	For external use
                 * 

                 */
                if (null != nodeItem)
                {
                    //The start code may be 4 bytes(to indicate that the slice is the start of the frame) or 3 bytes.
                    if (startCodeBitOffsetDifference != 0)
                    {
                        result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "start code for new frame", ItemType.FIELD, dataStore, ref bitOffset, 24 + startCodeBitOffsetDifference, ref fieldValue);
                        //Utility.ExpandToTop(newNode);
                    }
                    else
                    {
                        //The last item in the PES packet may have 4 bytes start code or 3 bytes start code.


                        if (isFinalItem)
                        {
                            result = Check4ByteStartCode(dataStore, ref bitOffset);
                            if (result.Fine)
                            {
                                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "start code for new frame", ItemType.FIELD, dataStore, ref bitOffset, 32, ref fieldValue);
                            }
                            else
                            {
                                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "start code", ItemType.FIELD, dataStore, ref bitOffset, 24, ref fieldValue);
                            }
                        }
                        else
                        {
                            result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "start code", ItemType.FIELD, dataStore, ref bitOffset, 24, ref fieldValue);
                        }
                    }


                    if (result.Fine)
                    {
                        result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "forbidden_bit", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
                    }

                    if (result.Fine)
                    {
                        result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "nal_storage_idc", ItemType.FIELD, dataStore, ref bitOffset, 2, ref fieldValue);
                    }

                    Int64 nal_unit_type = 0;
                    if (result.Fine)
                    {
                        result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "nal_unit_type", ItemType.FIELD, dataStore, ref bitOffset, 5, ref nal_unit_type);
                    }



                    if (result.Fine)
                    {
                        if (nal_unit_type == 0x1)//Coded slice of a non-IDR picture
                        {
                            Utility.UpdateNode(nodeItem, "Coded slice of a non-IDR picture", ItemType.ITEM);

                            //result = ParsePictureHeader(dataStore, nodeItem, ref bitOffset, ref fieldValue);                            
                        }
                        else if (nal_unit_type == 0x02)//Coded data partition A (DPA)
                        {
                            result = Utility.UpdateNode(nodeItem, "Coded data partition A (DPA)", ItemType.ITEM);
                        }
                        else if (nal_unit_type == 0x03)//Coded data partition B (DPB)
                        {
                            Utility.UpdateNode(nodeItem, "Coded data partition B (DPB)", ItemType.ITEM);
                            //result = ParseSequenceHeader(dataStore, nodeItem, ref bitOffset, ref fieldValue);
                        }
                        else if (nal_unit_type == 0x04)//Coded data partition C (DPC)
                        {
                            result = Utility.UpdateNode(nodeItem, "Coded data partition C (DPC)", ItemType.ITEM);
                        }
                        else if (nal_unit_type == 0x05)//Coded slice of an IDR picture
                        {
                            Utility.UpdateNode(nodeItem, "Coded slice of an IDR picture", ItemType.ITEM);
                            //result = ParseExtension(dataStore, nodeItem, ref bitOffset, ref fieldValue);
                        }
                        else if (nal_unit_type == 0x06)//Supplemental Enhancement Information (SEI)
                        {
                            result = Utility.UpdateNode(nodeItem, "Supplemental Enhancement Information (SEI)", ItemType.ITEM);
                        }
                        else if (nal_unit_type == 0x07)//Sequence Parameter Set (SPS)
                        {
                            Utility.UpdateNode(nodeItem, "Sequence Parameter Set (SPS)", ItemType.ITEM);
                            result = ParseSeqParameterSetRbsp(mpeg4VideoInfo, dataStore, nodeItem, ref bitOffset, ref fieldValue);
                        }
                        else if (nal_unit_type == 0x08)//Picture Parameter Set (PPS)
                        {
                            Utility.UpdateNode(nodeItem, "Picture Parameter Set (PPS)", ItemType.ITEM);
                            //result = ParsePicParameterSetRbsp(mpeg4VideoInfo, dataStore, nodeItem, ref bitOffset, ref fieldValue);
                        }
                        else if (nal_unit_type == 0x09)//Access unit delimiter (AUD)
                        {
                            Utility.UpdateNode(nodeItem, "Access unit delimiter (AUD)", ItemType.ITEM);
                            result = ParseAccessUnitDelimiterRbsp(mpeg4VideoInfo, dataStore, nodeItem, ref bitOffset, ref fieldValue);
                        }
                        else if (nal_unit_type == 0x0A)//End of sequence
                        {
                            Utility.UpdateNode(nodeItem, "End of sequence", ItemType.ITEM);
                            //result = ParseGroupOfPicturesHeader(dataStore, nodeItem, ref bitOffset, ref fieldValue);
                        }
                        else if (nal_unit_type == 0x0B)//End of stream
                        {
                            Utility.UpdateNode(nodeItem, "End of stream", ItemType.ITEM);
                            //result = ParseGroupOfPicturesHeader(dataStore, nodeItem, ref bitOffset, ref fieldValue);
                        }
                        else if (nal_unit_type == 0x0C)//Filler Data (FD)
                        {
                            Utility.UpdateNode(nodeItem, "Filler Data (FD)", ItemType.ITEM);
                            //result = ParseGroupOfPicturesHeader(dataStore, nodeItem, ref bitOffset, ref fieldValue);
                        }
                        else
                        {
                            result = Utility.UpdateNode(nodeItem, "unkown_unit", ItemType.ITEM);
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

        private Result ParseSeqParameterSetRbsp(Mpeg4VideoInfo mpeg4VideoInfo, DataStore dataStore, TreeNode nodeItem, ref long bitOffset, ref long fieldValue)
        {
            Result result = new Result();
            TreeNode newNode = null;

            Int64 profile_idc = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "profile_idc", ItemType.FIELD, dataStore, ref bitOffset, 8, ref profile_idc);
            }


            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "constraint_set0_flag", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "constraint_set1_flag", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "constraint_set2_flag", ItemType.FIELD, dataStore, ref bitOffset, 1, ref fieldValue);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "reserved_zero_5bits", ItemType.FIELD, dataStore, ref bitOffset, 5, ref fieldValue);
            }

            Int64 level_idc = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "level_idc", ItemType.FIELD, dataStore, ref bitOffset, 8, ref level_idc);
            }

            if (result.Fine)
            {
                mpeg4VideoInfo.profileIdc = profile_idc;
                mpeg4VideoInfo.level_idc = level_idc;
            }

            return result;
        }


        private Result ParseAccessUnitDelimiterRbsp(Mpeg4VideoInfo mpeg4VideoInfo, DataStore dataStore, TreeNode nodeItem, ref long bitOffset, ref Int64 fieldValue)
        {
            Result result = new Result();
            TreeNode newNode = null;

            Int64 primary_pic_type = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, nodeItem, out newNode, "primary_pic_type", ItemType.FIELD, dataStore, ref bitOffset, 6, ref primary_pic_type);
            }

            if (result.Fine)
            {
                mpeg4VideoInfo.primaryPicType = primary_pic_type;
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
                    && ((dataBytes[byteOffsetCurrent + 2] & 0xFF) == 0x01)
                    )
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
                        && ((dataBytes[byteOffsetCurrent + 2] & 0xFF) == 0x01)
                        )
                    {
                        result.SetResult(ResultCode.SUCCESS);//Perfect! We already find what we want.

                        if ((dataBytes[byteOffsetCurrent-1] & 0xFF) == 0x00)
                        {
                            //Calculate the length between 2 start codes.
                            lengthBetween2StartCode = byteOffsetCurrent - byteOffsetStart - 1;
                        }
                        else
                        {
                            //Calculate the length between 2 start codes.
                            lengthBetween2StartCode = byteOffsetCurrent - byteOffsetStart;
                        }


                        break;
                    }

                    byteOffsetCurrent++;
                    dataLengthInByte--;
                }
            }

            return result;
        }

        Result Check4ByteStartCode(DataStore dataStore, ref long bitOffsetStart)
        {
            Result result = new Result();

            long byteOffsetCurrent = bitOffsetStart / 8;
            long dataLengthInByte = dataStore.GetLeftBitLength() / 8;
            byte[] dataBytes = dataStore.GetData();

            result.SetFailure();

            if (dataLengthInByte >= 4)
            {
                if (((dataBytes[byteOffsetCurrent] & 0xFF) == 0x00)
                        && ((dataBytes[byteOffsetCurrent + 1] & 0xFF) == 0x00)
                        && ((dataBytes[byteOffsetCurrent + 2] & 0xFF) == 0x00)
                        && ((dataBytes[byteOffsetCurrent + 3] & 0xFF) == 0x01)
                   )
                {
                    result.SetResult(ResultCode.SUCCESS);
                }
            }

            return result;
        }
    }
}
