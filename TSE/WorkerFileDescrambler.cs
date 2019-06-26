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
    public class WorkerFileDescrambler : WorkerFile
    {
        //Pid to be processed and output file name.
        private SortedDictionary<UInt16, PidUpdate> pidUpdateList = null;
        private string outputFileName = null;
        private bool doEntropy = false;
        private byte[] controlWordSerials = null;
        private int controlWordLength = 0;
        private int controlWordCount = 0; //The value will be controlWordLength/8;
        private int currentControlWord = 0;
        private int currentEvenOdd = -1;//We will need to detect the even/odd change in the TS packet, so that we can change the control word.

        private FileStream outputFileStream = null;

        private CSA csaDescrambler = new CSA();

        public WorkerFileDescrambler(StreamParserContext owner, MessageHandlerDelegate messageCallbackFromOwner)
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


            Int64 scrambled = 0;
            if (result.Fine)
            {
                //1-bit scrambling indicator. If 1, scrambled, else clear.
                result = Utility.ByteArrayReadBits(packetBuffer, ref dataLeftInBit, ref bitOffset, 1, ref scrambled);
            }

            Int64 evenOdd = 0;
            if (result.Fine)
            {
                //1-bit even/odd indicator.
                result = Utility.ByteArrayReadBits(packetBuffer, ref dataLeftInBit, ref bitOffset, 1, ref evenOdd);
            }

            //To process the PID according to the request.
            if (result.Fine)
            {
                PidUpdate pidToProcess = null;

                //If this pid needs to be processed.
                if (pidUpdateList.TryGetValue((UInt16)pid, out pidToProcess))
                {
                    if (1 == scrambled)
                    {
                        //Descramble the packet.
                        
                        //We will need to check even/odd change.
                        if (evenOdd != currentEvenOdd)
                        {
                            //Get the control word.
                            int realControlWordId = currentControlWord;

                            currentControlWord++;//Increase so that we can pick up next control word.
                            currentEvenOdd = (int)evenOdd;//Save current flag.

                            realControlWordId = realControlWordId % controlWordCount;//We like to loop back if necessary.

                            byte[] csaKey = new byte[8];
                            Array.Copy(controlWordSerials, realControlWordId * 8, csaKey, 0, 8);//Copy the key into a new buffer.

                            csaDescrambler.SetCW(csaKey, doEntropy);//Set the CW.
                        }

                        //Descramble the packet in place.
                        csaDescrambler.DecryptTSPacket(packetBuffer, (int)packetOffsetInBuffer, (int)TsPacketSize.SIZE_188);
                    }
                }

                //Write the output no matter clear or descrambled.
                outputFileStream.Write(packetBuffer, (int)packetOffsetInBuffer, (int)tsPacketMetadata.PacketSize);
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
            messageCallback(MessageId.MESSAGE_DESCRABLING_DONE, null);
            GetContext().WriteLog("Descrambling is done." + Environment.NewLine);
            if (null != outputFileStream)
            {
                outputFileStream.Close();
            }
        }

        public void SetDescramblerInfo(SortedDictionary<UInt16, PidUpdate> pidUpdateList, string outputFileName, bool doEntropy, byte[] controlWordSerials,int controlWordLength)
        {
            this.pidUpdateList = pidUpdateList;
            this.outputFileName = outputFileName;
            this.doEntropy = doEntropy;
            this.controlWordSerials = controlWordSerials;
            this.controlWordLength = controlWordLength;

            this.controlWordCount = controlWordLength / 8;
        }
    }
}
