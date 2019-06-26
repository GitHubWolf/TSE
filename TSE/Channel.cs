using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InActionLibrary;

namespace TSE
{
    class Channel : StreamParserSession
    {
        private Int64 currentDataLength = 0;
        private Int64 currentOffset = 0;//Useful for section channel ONLY.
        private byte[] dataBuffer = null;
        private DataType dataType = DataType.UNKNOWN;
        private Int64 bufferSize = 0;

        private FilterDataCallback channelDataCallback = null; //Callback function invoked when a section/PES/TS packet is received.
        private UInt16 pid = 0;
        private List<TsPacketMetadata> packetMetadataList = new List<TsPacketMetadata>();


        public Channel(StreamParserContext owner, UInt16 pid, FilterDataCallback dataCallback, DataType dataType)
            : base(owner)
        {

            this.dataType = dataType;
            this.pid = pid;
            this.channelDataCallback = dataCallback;
        }
        public Int64 DataLength
        {
            get
            {
                return currentDataLength;
            }

            set//Not necessary provide set method?
            {
                currentDataLength = value;
            }
        }

        /**
         * Add the data into the channel buffer.
         * 
         * @param tsPacketMetadata packet information.
         * @param newData byte array containing the data to be added into the channel buffer.
         * @param leftDataLengthInBit how many bits left in the byte array.\n
         *        On input, it indicates how many bits left in the byte array.\n
         *        On output, it indicates how many bits left in the byte array after reading out requested data from the byte array.
         * @param newDataOffsetInBit offset of the position to read in the data from the byte array.\n
         *        On input, it indicates the offset to read in data.\n
         *        On output, it indicates the offset after reading out requested data from the byte array.
         * @param newDataLengthInBit how many bits to be read out from the byte array.
         * 
         * @retval result
         */
        public void AddSectionData(TsPacketMetadata tsPacketMetadata, byte[] newData, ref Int64 leftDataLengthInBit, ref Int64 newDataOffsetInBit, Int64 newDataLengthInBit)
        {
            Result result = new Result();

            if (newDataLengthInBit > 0)
            {
                if (null == dataBuffer)
                {
                    this.bufferSize = 5 * 1024;
                    dataBuffer = new byte[bufferSize];
                }

                //Convert bits to bytes.
                Int64 newDataLengthInByte = newDataLengthInBit / 8;
                Int64 newDataOffsetInByte = newDataOffsetInBit / 8;

                //Save the packetMetadata.
                packetMetadataList.Add(tsPacketMetadata);

                if (result.Fine)
                {
                    //Check whether there is enough data left in the buffer.
                    if (newDataLengthInBit > leftDataLengthInBit)
                    {
                        result.SetResult(ResultCode.INSUFFICIENT_DATA);

                        //Report an error.
                        GetContext().WriteLog("Invalid data length has been detected!" + Environment.NewLine);
                    }
                }

                if (result.Fine)
                {
                    //If no enough space to save the data.
                    if ((currentDataLength + currentOffset + newDataLengthInByte) > bufferSize)
                    {
                        //Copy the data to the beginning of the buffer first.
                        Array.Copy(dataBuffer, currentOffset, dataBuffer, 0, currentDataLength);
                        currentOffset = 0;
                    }

                    //Have a check again!
                    if ((currentDataLength + currentOffset + newDataLengthInByte) > bufferSize)
                    {
                        //result.SetResult(ResultCode.INSUFFICIENT_DATA);
                        //GetContext().WriteLog("Error:Insufficient channel buffer!" + Environment.NewLine);

                        //No enough buffer. Allocate a larger buffer to save the content. Increase 4 bytes each time.
                        Int64 newBufferSize = bufferSize + 4 * 1024;//We need this because to endure some "bad" streams!!!!!!!!!!!!!!!!!!

                        byte[] newBuffer = new byte[newBufferSize];

                        Array.Copy(dataBuffer, newBuffer, currentDataLength);//Copy from the old one into the new one.

                        dataBuffer = newBuffer;
                        bufferSize = newBufferSize;
                    }

                    //Enough space.Save the data into the space.
                    Array.Copy(newData, newDataOffsetInByte, dataBuffer, currentOffset + currentDataLength, newDataLengthInByte);//Note that the forth parameter is offset + length!

                    //Increase the data length!
                    currentDataLength += newDataLengthInByte;

                    //Decrease the left data length.
                    leftDataLengthInBit -= newDataLengthInBit;

                    //Increase the offset.
                    newDataOffsetInBit += newDataLengthInBit;

                    if (dataType == DataType.SECTION)
                    {
                        CheckSectionAvailable();//Notify any available section if any!
                    }

                } 
            } 

        }

        /**
         * 
         */
        public void AddTsPacket(TsPacketMetadata tsPacket, byte[] packetBuffer, Int64 packetOffsetInBuffer)
        {
            List<TsPacketMetadata> singlePacketMetadataList = new List<TsPacketMetadata>();
            singlePacketMetadataList.Add(tsPacket);
            
            channelDataCallback(pid, packetBuffer, packetOffsetInBuffer, (Int64)tsPacket.PacketSize, singlePacketMetadataList);
        }//AddTsPacket

        /**
         * Add the data into the channel buffer.
         * 
         * @param tsPacketMetadata packet information.
         * @param newData byte array containing the data to be added into the channel buffer.
         * @param leftDataLengthInBit how many bits left in the byte array.\n
         *        On input, it indicates how many bits left in the byte array.\n
         *        On output, it indicates how many bits left in the byte array after reading out requested data from the byte array.
         * @param newDataOffsetInBit offset of the position to read in the data from the byte array.\n
         *        On input, it indicates the offset to read in data.\n
         *        On output, it indicates the offset after reading out requested data from the byte array.
         * @param newDataLengthInBit how many bits to be read out from the byte array.
         * 
         * @retval result
         */
        public void AddPesData(TsPacketMetadata tsPacketMetadata, Int64 startIndicator, byte[] newData, Int64 newDataOffsetInBit, Int64 newDataLengthInBit)
        {
            Result result = new Result();

            //Convert bits to bytes.
            Int64 newDataLengthInByte = newDataLengthInBit / 8;
            Int64 newDataOffsetInByte = newDataOffsetInBit / 8;

            if (null == dataBuffer)
            {
                bufferSize = 8 * 1024;
                dataBuffer = new byte[bufferSize];//Allocate 8 K bytes as the intial buffer.
            }

            if (1 == startIndicator)
            {
                //Start indicator is set to 1. We will notify all existing data as a PES packet.
                if (currentDataLength != 0)
                {
                    channelDataCallback(pid, dataBuffer, 0, currentDataLength, packetMetadataList);

                    //Clean up everything so that we are ready to accept new packet.
                    Reset();
                }
            }//if (1 == startIndicator)

            //Save the packetMetadata.
            packetMetadataList.Add(tsPacketMetadata);

            if (result.Fine)
            {
                //If no enough space to save the data.
                if ((currentDataLength + newDataLengthInByte) > bufferSize)
                {
                    //No enough buffer. Allocate a larger buffer to save the content. Increase 8K each time.
                    Int64 newBufferSize = bufferSize + 8 * 1024;

                    byte[] newBuffer = new byte[newBufferSize];

                    Array.Copy(dataBuffer, newBuffer, currentDataLength);//Copy from the old one into the new one.

                    dataBuffer = newBuffer;
                    bufferSize = newBufferSize;
                }//if ((currentDataLength + newDataLengthInByte) > bufferSize)

                //Now, we must have enough buffer to hold the incoming data.
                Array.Copy(newData, newDataOffsetInByte, dataBuffer, currentDataLength, newDataLengthInByte);//Note that the forth parameter is indeed the offset!

                //Increase the lenght of current data after appending the new data.
                currentDataLength += newDataLengthInByte;
            }
        }//AddPesData

        public void Reset()
        {
            currentDataLength = 0;
            currentOffset = 0;

            //Create a new one.
            packetMetadataList = new List<TsPacketMetadata>();
        }//Reset

        private void CheckSectionAvailable()
        {
            Result result = new Result();
            Int64 currentDataLengthInBit = 0;
            Int64 currentOffsetInBit = 0;
            Int64 sectionLength = 0;
            Int64 tableId = 0;

            if (null != channelDataCallback)
            {
                while (currentDataLength > 3)
                {
                    currentDataLengthInBit = currentDataLength * 8;
                    currentOffsetInBit = currentOffset * 8;

                    //Check table_id. If table_id is stuffing byte 0xFF, we will clean up the left data.
                    result = Utility.ByteArrayReadBits(dataBuffer, ref currentDataLengthInBit, ref currentOffsetInBit, 8, ref tableId);

                    if (result.Fine)
                    {
                        //If it is stuffing byte. We will disgard the left data since they are purely for stuffing purpose.
                        if (tableId == 0xFF)
                        {
                            result.SetResult(ResultCode.INVALID_DATA);
                        }
                    }

                    if (result.Fine)
                    {
                        //Skip 4 bits.
                        result = Utility.ByteArraySkipBits(ref currentDataLengthInBit, ref currentOffsetInBit, 4);//Skip section_syntax_indicator and reserved bits.
                    }

                    if (result.Fine)
                    {
                        //Get section length and check whether we have receieved enough data.
                        result = Utility.ByteArrayReadBits(dataBuffer, ref currentDataLengthInBit, ref currentOffsetInBit, 12, ref sectionLength);
                    }

                    if (result.Fine)
                    {
                        //Check whether the section length is valid.
                        if ((sectionLength + 3) <= currentDataLength)
                        {
                            //Notify this section and skip it.
                            channelDataCallback(pid, dataBuffer, currentOffset, sectionLength + 3, packetMetadataList);

                            //Decrease the length and increase the offset.
                            currentDataLength -= (sectionLength + 3);
                            currentOffset += (sectionLength + 3);
                        }
                        else
                        {
                            //One section may be longer than one packet. When the program runs to here, it means no enough packets have been received. We will continue to receive more data.
                            break;
                        }
                    }
                    else
                    {
                        //Something is wrong. We will need to disgard this section!
                        Reset();
                        break;
                    }
                }//while (currentDataLength > 3) 
            }//if (null != filterDataCallback)
        }//CheckSectionAvailable
    }
}
