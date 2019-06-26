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
    public class WorkerFileRoutineParsing : WorkerFile
    {
        StreamParserCore streamParserCore = null;

        public WorkerFileRoutineParsing(StreamParserContext owner, MessageHandlerDelegate messageCallbackFromOwner)
            : base(owner, messageCallbackFromOwner)
        {
            streamParserCore = new StreamParserCore(owner, messageCallbackFromOwner);
        }

        public override void ProcessTsPacket(TsPacketMetadata tsPacketMetadata, byte[] packetBuffer, Int64 packetOffsetInBuffer)
        {
            //Invoke the stream parser to process a TS packet.
            streamParserCore.ProcessTsPacket(tsPacketMetadata, packetBuffer, packetOffsetInBuffer);
        }
        public override void OnStart()
        {
        }
        public override void OnStop()
        {
            //Call Stop so that StreamParser can finalize.
            streamParserCore.Stop();

            //Update the PID list to the form.
            messageCallback(MessageId.MESSAGE_PID_LIST, streamParserCore.GetPidList());

            //Update the stream bitrate to the form. The stream bitrate is calculatd from PCR.
            messageCallback(MessageId.MESSAGE_MUX_BITRATE, streamParserCore.GetStreamBitrateList());

            //Send a message to notify that we have finished doing everything.
            messageCallback(MessageId.MESSAGE_PARSING_DONE, null);
        }
    }
}
