using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InActionLibrary;


namespace TSE
{
    public class StreamDemux : StreamParserSession
    {
        //Sync_byte, 0x47, the byte to indicate the start of a TS packet.
        public const byte SYNC_BYTE = 0x47;

        //List to contain all the listeners which are expecting to receive data from the stream parser.
        private List<Filter> filterList = null;

        //Dictionary to map PID to ChannelDataStore.
        private Dictionary<UInt16, Channel> channelList = new Dictionary<UInt16, Channel>();

        public StreamDemux(StreamParserContext owner)
            : base(owner)
        {
            filterList = new List<Filter>();
        }

        public static Result DetectPacketSize(byte[] tsPacketBuffer, Int64 totalDataLength, ref Int64 validPacketOffset, ref TsPacketSize tsPacketSize)
        {
            Result result = new Result();

            tsPacketSize = TsPacketSize.SIZE_UNKNOWN;
            Int64 validPacketOffsetTemp = validPacketOffset;

            if (totalDataLength < (Int64)TsPacketSize.SIZE_188)
            {
                result.SetFailure();
            }

            if (result.Fine)
            {
                if ((SYNC_BYTE == tsPacketBuffer[validPacketOffsetTemp]) && (totalDataLength == (Int64)TsPacketSize.SIZE_204))
                {
                    tsPacketSize = TsPacketSize.SIZE_204;
                }
                else if ((SYNC_BYTE == tsPacketBuffer[validPacketOffsetTemp]) && (totalDataLength == (Int64)TsPacketSize.SIZE_188))
                {
                    tsPacketSize = TsPacketSize.SIZE_188;
                }
                else
                {
                    //Else, we may need to locate several packets to detect the size.
                    if ((validPacketOffsetTemp + (Int64)TsPacketSize.SIZE_188 * 2) <= tsPacketBuffer.Length)
                    {
                        //Find the first sync_byte.
                        for (Int64 i = validPacketOffsetTemp; i < (validPacketOffsetTemp + (Int64)TsPacketSize.SIZE_188); ++i)
                        {
                            if ((SYNC_BYTE == tsPacketBuffer[i]) && (SYNC_BYTE == tsPacketBuffer[i + (Int64)TsPacketSize.SIZE_188]))
                            {
                                tsPacketSize = TsPacketSize.SIZE_188;
                                validPacketOffset = i;
                                break;
                            }
                        }
                    }
                    
                    if(tsPacketSize == TsPacketSize.SIZE_UNKNOWN)
                    {
                        if ((validPacketOffsetTemp + (Int64)TsPacketSize.SIZE_204 * 2) <= tsPacketBuffer.Length)
                        {
                            //Find the first sync_byte.
                            for (Int64 i = validPacketOffsetTemp; i < (validPacketOffsetTemp + (Int64)TsPacketSize.SIZE_204); ++i)
                            {
                                if ((SYNC_BYTE == tsPacketBuffer[i]) && (SYNC_BYTE == tsPacketBuffer[i + (Int64)TsPacketSize.SIZE_204]))
                                {
                                    tsPacketSize = TsPacketSize.SIZE_204;
                                    validPacketOffset = i;
                                    break;
                                }
                            }
                        }

                    }
                }
            }

            if (TsPacketSize.SIZE_UNKNOWN == tsPacketSize)
            {
                result.SetResult(ResultCode.INVALID_STREAM);
            }
            return result;
        }

        public Result ProcessTsPacket(TsPacketMetadata tsPacketMetadata, byte[] packetBuffer, Int64 packetOffsetInBuffer)
        {
            Result result = new Result();
            //Int64 dataLeftInBit = (Int64)tsPacketMetadata.PacketSize;
            Int64 dataLeftInBit = (Int64)TsPacketSize.SIZE_188 * 8;//Important!!!!No matter 188 or 204 bytes, the valid data will always be 188 bytes.16 bytes are checksum that is not useful for parsing.

            Int64 bitOffset = packetOffsetInBuffer*8;//Key point to set the beginning offset!!!!!


            if (result.Fine)
            {
                //8-bit sync_byte.
                result = Utility.ByteArraySkipBits(ref dataLeftInBit, ref bitOffset, 8);
            }

            if (result.Fine)
            {
                //1-bit transport_error_indicator.
                result = Utility.ByteArraySkipBits(ref dataLeftInBit, ref bitOffset, 1);
            }

            Int64 payloadUnitStartIndicator = 0;
            if (result.Fine)
            {
                //1-bit payload_unit_start_indicator.
                result = Utility.ByteArrayReadBits(packetBuffer, ref dataLeftInBit, ref bitOffset, 1, ref payloadUnitStartIndicator);
                //GetContext().WriteLog(Utility.GetValueBinaryString(fieldValue, 1) + Environment.NewLine);
            }

            if (result.Fine)
            {
                //1-bit transport_priority.
                result = Utility.ByteArraySkipBits(ref dataLeftInBit, ref bitOffset, 1);
            }

            Int64 pid = 0;
            if (result.Fine)
            {
                //13-bit PID.
                result = Utility.ByteArrayReadBits(packetBuffer, ref dataLeftInBit, ref bitOffset, 13, ref pid);
                //GetContext().WriteLog(Utility.GetValueBinaryString(fieldValue, 13) + Environment.NewLine);
            }
            
            if (result.Fine)
            {
                //2-bit transport_scrambling_control.
                result = Utility.ByteArraySkipBits(ref dataLeftInBit, ref bitOffset, 2);
            }

            Int64 adaptationFieldControl = 0;
            if (result.Fine)
            {
                //2-bit adaptation_field_control.
                result = Utility.ByteArrayReadBits(packetBuffer, ref dataLeftInBit, ref bitOffset, 2, ref adaptationFieldControl);
                //GetContext().WriteLog(Utility.GetValueBinaryString(fieldValue, 2) + Environment.NewLine);
            }

            if (result.Fine)
            {
                //4-bit continuity_counter.
                result = Utility.ByteArraySkipBits(ref dataLeftInBit, ref bitOffset, 4);
            }

            //To parse adaption fields.
            Int64 adaptationFieldLength = 0;
            if (result.Fine)
            {
                //If adaptationFieldControl is 0b10(Adaptation_field only, no payload) or 0b11(Adaptation_field followed by payload),there is a adaption_field. We need to skip the adaption fields.
                if ((0x2 == adaptationFieldControl) || (0x3 == adaptationFieldControl))
                {
                    //8-bit adaptation_field_length
                    result = Utility.ByteArrayReadBits(packetBuffer, ref dataLeftInBit, ref bitOffset, 8, ref adaptationFieldLength);

                    //Skip adaption fields according to the adaption_field_length.
                    if (result.Fine)
                    {
                        result = Utility.ByteArraySkipBits(ref dataLeftInBit, ref bitOffset, (adaptationFieldLength * 8));
                    }
                }
            }

            //If the packet is what we want.
            if (result.Fine)
            {
                DataType pidDataType = GetChannelType((UInt16)pid);
                if (DataType.SECTION == pidDataType)
                {
                    Channel channelDataStore = null;
                    //Check start indicator.
                    if (0 == payloadUnitStartIndicator)
                    {
                        //No complete section is expected.

                        /*
                         * At the first beginning of parsing, we may receive some TS packets with payloadUnitStartIndicator ZERO. 
                         * We will ignore these packets if there is nothing in the buffer, because these packets are not expected contain the start of a section.
                         * Having these kinds of TS packets is useless.
                         */
                        if (channelList.TryGetValue((UInt16)pid, out channelDataStore))
                        {
                            //We already get the item in the channel list. Save it into the ChannelDataStore.
                            channelDataStore.AddSectionData(tsPacketMetadata, packetBuffer, ref dataLeftInBit, ref bitOffset, dataLeftInBit);
                        }
                    }
                    else
                    {
                        //Start indicator is set to 1. A new section is expected. Get the pointer field.

                        //8-bit pointer_field.
                        Int64 pointerField = 0;
                        result = Utility.ByteArrayReadBits(packetBuffer, ref dataLeftInBit, ref bitOffset, 8, ref pointerField);

                        if (result.Fine)
                        {
                            if (!channelList.TryGetValue((UInt16)pid, out channelDataStore))
                            {
                                //No existing Channel. Create a new one to store section.
                                channelDataStore = new Channel(owner, (UInt16)pid, HandleDataFromChannel, DataType.SECTION);

                                //Insert to the channel list.
                                channelList.Add((UInt16)pid, channelDataStore);
                            }
                        }

                        //Save the data belonged to the previous section and have a clean-up first.
                        if (result.Fine)
                        {
                            //Save the data with length indicated pointer_field. The data belongs to the previous section.
                            channelDataStore.AddSectionData(tsPacketMetadata, packetBuffer, ref dataLeftInBit, ref bitOffset, pointerField*8);//Pay attention to the third parameter.

                            if (result.Fine)
                            {
                                //Clean up data NO MATTER HOW in case we may have received some invalid data that may disturb the parsing!!!!!!!!!!!!!!!!!!!!!!
                                channelDataStore.Reset();
                            }
                        }

                        if (result.Fine)
                        {
                            //Save the data belonged to the new section.
                            channelDataStore.AddSectionData(tsPacketMetadata, packetBuffer, ref dataLeftInBit, ref bitOffset, dataLeftInBit);
                        }
                    }
                }//PID for section filtering.
                else if (DataType.PES_PACKET == pidDataType)
                {
                    Channel channelDataStore = null;
                    //Check start indicator.
                    if (0 == payloadUnitStartIndicator)
                    {
                        //No complete PES is expected.

                        /*
                         * At the first beginning of parsing, we may receive some TS packets with payloadUnitStartIndicator ZERO. 
                         * We will ignore these packets if there is nothing in the buffer, because these packets will not contain the start of a PES.
                         * Having these kinds of TS packets is useless.
                         */
                        if (channelList.TryGetValue((UInt16)pid, out channelDataStore))//Existing Channel for this PID.
                        {
                            //We already get the item in the channel list. Save it into the ChannelDataStore.
                            channelDataStore.AddPesData(tsPacketMetadata, payloadUnitStartIndicator, packetBuffer, bitOffset, dataLeftInBit);
                        }
                    }
                    else
                    {
                        //Start indicator is set to 1. A new PES is expected. Send out all existing data as a PES packet.

                        if (result.Fine)
                        {
                            if (!channelList.TryGetValue((UInt16)pid, out channelDataStore))//No existing Channel for this PID.
                            {
                                //No existing ChannelDataStore. Create a new one to store PES data.
                                channelDataStore = new Channel(owner, (UInt16)pid, HandleDataFromChannel, DataType.PES_PACKET);

                                //Insert to the channel list.
                                channelList.Add((UInt16)pid, channelDataStore);
                            }
                        }

                        if (result.Fine)
                        {
                            //Save the data belonged to the new PES.
                            channelDataStore.AddPesData(tsPacketMetadata, payloadUnitStartIndicator, packetBuffer, bitOffset, dataLeftInBit);
                        }
                    }

                }//PID for PES filtering.
                else if (DataType.TS_PACKET == pidDataType)
                {
                    Channel channelDataStore = null;

                    if (!channelList.TryGetValue((UInt16)pid, out channelDataStore))
                    {
                        //No existing Channel. Create a new one to store the TS.
                        channelDataStore = new Channel(owner, (UInt16)pid, HandleDataFromChannel, DataType.TS_PACKET);

                        //Insert to the channel list.
                        channelList.Add((UInt16)pid, channelDataStore);
                    }

                    //Add it into the channel buffer.
                    channelDataStore.AddTsPacket(tsPacketMetadata, packetBuffer, packetOffsetInBuffer);

                }//PID for TS packet filtering.

                
            }

            
            return result;
        }


        public Result AddFilter(Filter filterCondition)
        {
            Result result = new Result();

            filterList.Add(filterCondition);

            return result;
 
        }

        private DataType GetChannelType(UInt16 pid)
        {
            DataType dataType = DataType.UNKNOWN;
            foreach (Filter filter in filterList)
            {
                if(filter.PidIsSame(pid))
                {
                    dataType = filter.GetFilterType();
                    break;
                }
            }

            return dataType;
        }

        public void Start()
        { 
        }

        public void Stop()
        {

        }

        //To process data notified from ChannelDataStore.
        private void HandleDataFromChannel(UInt16 pid, byte[] data, Int64 dataOffset, Int64 dataLength, List<TsPacketMetadata> packetMetadataList)
        {
            DataStore section = new DataStore(data, dataOffset, dataLength);
            section.SetPacketMedataList(packetMetadataList);

            //Check if the data is what we want!
            foreach (Filter filter in filterList)
            {
                if (filter.Match(pid, data, dataOffset, dataLength))
                {
                    //Invoke the callback to notify the data.
                    filter.DataCallback(pid, data, dataOffset, dataLength, packetMetadataList);

                    //Once matched, we will break immediately.
                    break;
                }
            }
        }//HandleDataFromChannel
    }
}
