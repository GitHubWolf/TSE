using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using InActionLibrary;

namespace TSE
{
    public class WorkerFilePidUpdate : WorkerFile
    {
        //Pid replacement and output file name.
        private SortedDictionary<UInt16, PidUpdate> pidUpdateList = null;
        private string outputFileName = null;

        private FileStream outputFileStream = null;

        public WorkerFilePidUpdate(StreamParserContext owner, MessageHandlerDelegate messageCallbackFromOwner)
            : base(owner, messageCallbackFromOwner)
        {
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

            //To update the PID according to the request.
            if (result.Fine)
            {
                PidUpdate pidUpdate = null;

                //If this pid needs update.
                if (pidUpdateList.TryGetValue((UInt16)pid, out pidUpdate))
                {
                    //To update the PID.
                    packetBuffer[packetOffsetInBuffer + 1] = (byte)((packetBuffer[packetOffsetInBuffer + 1] & (byte)0xE0) | (pidUpdate.NewPid >> 8));//To update the high 5 bits of PID.
                    packetBuffer[packetOffsetInBuffer + 2] = (byte)(pidUpdate.NewPid & 0xFF);//To update the low 8 bits of PID.

                    //Write to output file now.
                    outputFileStream.Write(packetBuffer, (int)packetOffsetInBuffer, (int)tsPacketMetadata.PacketSize);
                }
                else
                { 
                    //Write the original packet directly.
                    outputFileStream.Write(packetBuffer, (int)packetOffsetInBuffer, (int)tsPacketMetadata.PacketSize);
                }
            }
        }

        public override void OnStart()
        {
            //Open the file for writing.
            try
            {
                outputFileStream = new FileStream(outputFileName, FileMode.Create, FileAccess.Write);
            }
            catch (Exception)
            {
                GetContext().WriteLog("Failed to create output file." + Environment.NewLine);
                StopWorking();//Stop once we meet errors.
            }

        }

        public override void OnStop()
        {
            messageCallback(MessageId.MESSAGE_PID_UPDATE_DONE, null);
            GetContext().WriteLog("PID update is done." + Environment.NewLine);
            if (null != outputFileStream)
            {
                outputFileStream.Close();
            }
        }

        public void SetPidUpdateInfo(SortedDictionary<UInt16, PidUpdate> pidUpdateList, string outputFileName)
        {
            this.pidUpdateList = pidUpdateList;
            this.outputFileName = outputFileName;
        }
    }
}
