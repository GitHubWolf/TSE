using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InActionLibrary;

namespace TSE
{
    public class WorkerFilePidBitrate : WorkerFile
    {

        //Bitrate set by the user.
        private Int64 streamBitrate = 0;

        //ManagerPidBitrate to detect realtime PID bitrates in each second.
        private ManagerPidBitrate managerPidBitrate = new ManagerPidBitrate();

        //To save a copy of previous totalLengthOfDataParsed.
        private Int64 previousTotalLengthOfDataParsed = 0;

        public WorkerFilePidBitrate(StreamParserContext owner, MessageHandlerDelegate messageCallbackFromOwner)
            : base(owner, messageCallbackFromOwner)
        {
        }


        public SortedDictionary<UInt16, PidBitrate> FetchCurrentBitrates()
        {
            return managerPidBitrate.FetchCurrentBitrates();
        }

        public override void ProcessTsPacket(TsPacketMetadata tsPacketMetadata, byte[] packetBuffer, Int64 packetOffsetInBuffer)
        {
            Result result = new Result();
            //Int64 dataLeftInBit = (Int64)tsPacketMetadata.PacketSize;
            Int64 dataLeftInBit = (Int64)TsPacketSize.SIZE_188 * 8;//Important!!!!No matter 188 or 204 bytes, the valid data will always be 188 bytes.16 bytes are checksum that is not useful for parsing.

            Int64 bitOffset = packetOffsetInBuffer * 8;//Key point to set the beginning offset!!!!!


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

            //Notify to ManagerPidBitrate to calculate realtime bitrate.
            if (result.Fine)
            {
                managerPidBitrate.NewTsPacket(tsPacketMetadata, (UInt16)pid);
            }

            if ((totalLengthOfDataParsed - previousTotalLengthOfDataParsed) * 8 >= (streamBitrate))
            {
                //Time to fetch current bitstream.
                SortedDictionary<UInt16, PidBitrate> currentBitrate = managerPidBitrate.FetchCurrentBitrates();
                KeyValuePair<DateTime, SortedDictionary<UInt16, PidBitrate>> bitrateForNow =
                    new KeyValuePair<DateTime, SortedDictionary<ushort, PidBitrate>>(DateTime.Now, currentBitrate);
                //Send it to the form now.
                messageCallback(MessageId.MESSAGE_PID_BITRATE_DATA, bitrateForNow);

                //Save the new value as the previous one.
                previousTotalLengthOfDataParsed = totalLengthOfDataParsed;
            }
        }

        public void SetStreamBitrate(Int64 streamBitrate)
        {
            this.streamBitrate = streamBitrate;
        }

        public override void OnStop()
        {
            //Send a message to notify that we have finished doing everything.
            messageCallback(MessageId.MESSAGE_MEASURE_PID_BITRATE_DONE, null);
        }
    }
}
