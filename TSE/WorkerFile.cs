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
    public class WorkerFile : StreamParserSession
    {
        //Read size.
        private const int TS_PACKET_BUFFER_SIZE = 188 * 204;

        //For the parser worker thread.
        private Thread parserThread = null;
        protected volatile bool canRun = true;

        private FileStream fileStream = null;

        protected MessageHandlerDelegate messageCallback = null;

        protected Int64 totalLengthOfDataParsed = 0;
        protected Int64 totalLengthOfDataSkipped = 0;

        public WorkerFile(StreamParserContext owner, MessageHandlerDelegate messageCallbackFromOwner)
            : base(owner)
        {
            messageCallback = messageCallbackFromOwner;
        }

        protected void StopWorking()
        {
            canRun = false;
        }

        public Result StartParse(String streamFile)
        {
            Result result = new Result();

            canRun = true;

            try
            {
                fileStream = new FileStream(streamFile, FileMode.Open, FileAccess.Read);
            }
            catch (Exception)
            {
                result.SetResult(ResultCode.FAILED_TO_OPEN_FILE);
            }

            if (result.Fine)
            {
                //Create a thread to parse the stream.
                parserThread = new Thread(new ParameterizedThreadStart(DoWork));

                //parserThread.Priority = ThreadPriority.Lowest;
                parserThread.Start(this);
            }

            return result;
        }

        public void DoWork(object data)
        {
            Result result = new Result();

            byte[] tsPacketBuffer = new byte[TS_PACKET_BUFFER_SIZE];

            int lengthOfDataReadIn = 0; //Length of data read in from file.
            Int64 lengthOfDataToParse = 0; //Length of data to be parsed.
            Int64 validPacketOffset = 0; //Offset of packet in the buffer.
            TsPacketSize tsPacketSize = TsPacketSize.SIZE_UNKNOWN;
            Int64 packetNumber = 0; //Number of packet.

            //Give the child class to do something.
            OnStart();



            GetContext().WriteLog("Start to parse stream file: " + fileStream.Name + ".\n");

            //To check current datetime.//TIME_LIMIT
            DateTime currentTime = DateTime.Now;

            if (result.Fine)
            {
                //Read in some TS packets to detect the TS packet size.
                lengthOfDataToParse = fileStream.Read(tsPacketBuffer, 0, tsPacketBuffer.Length);

                //We will detect whether it is a valid transport stream first.
                result = StreamDemux.DetectPacketSize(tsPacketBuffer, lengthOfDataToParse, ref validPacketOffset, ref tsPacketSize);//validPacketOffset and tsPacketSize will be changed by this function.

                if (result.Fine)
                {
                    GetContext().WriteLog("Packet size: " + tsPacketSize + ".\n");
                    GetContext().WriteLog("Offset of first packet: " + validPacketOffset + ".\n");
                    GetContext().WriteLog("Stream size: " + fileStream.Length + " bytes.\n");

                    messageCallback(MessageId.MESSAGE_TS_PACKET_SIZE, (int)tsPacketSize);
                }
                else
                {
                    GetContext().WriteLog("Invalid transport stream.\n");
                    result.SetResult(ResultCode.FAILURE);
                }

                //Skip invalid data bytes.
                lengthOfDataToParse -= validPacketOffset;
            }

            Int64 currentProgress = 0;
            Int64 savedProgress = 0;
            Int64 fileSize = fileStream.Length;

            if (result.Fine)
            {
                totalLengthOfDataParsed += validPacketOffset;
                totalLengthOfDataSkipped += validPacketOffset;

                /*TIME_LIMIT
                if ((currentTime.Year * 12 + currentTime.Month) >= 2015 * 12 + 6)
                {
                    canRun = false;
                }

                if ((currentTime.Year * 12 + currentTime.Month) < 2014 * 12 + 5)
                {
                    canRun = false;
                }
                */

                //It is a valid transport stream.
                while (canRun)
                {
                    currentProgress = totalLengthOfDataParsed * 100 / fileSize;
                    if (currentProgress > savedProgress)
                    {
                        //Progress has been updated.
                        savedProgress = currentProgress;

                        //Update the progress shown in the form by sending a message.
                        OnProgress(savedProgress);
                    }

                    //If left data in the buffer is shorter than the size of a TS packet, we will need to read more data before processing it.
                    if (lengthOfDataToParse < (Int64)tsPacketSize)
                    {
                        //The left data will be moved to the beginning of the buffer.

                        //In case there is any data left, we will copy it to the beginning of the buffer.
                        if (0 != lengthOfDataToParse)
                        {
                            Array.Copy(tsPacketBuffer, tsPacketBuffer.Length - lengthOfDataToParse, tsPacketBuffer, 0, lengthOfDataToParse);
                        }

                        //Console.WriteLine("-----lengthOfDataToParse:" + lengthOfDataToParse + " totalLengthOfDataParsed " + totalLengthOfDataParsed + " ts byte :" + tsPacketBuffer[0]);

                        //Read the data from the file into the buffer.
                        lengthOfDataReadIn = fileStream.Read(tsPacketBuffer, (int)lengthOfDataToParse, (int)(tsPacketBuffer.Length - lengthOfDataToParse));

                        //To parse all the data in the buffer.
                        lengthOfDataToParse += lengthOfDataReadIn;

                        //Console.WriteLine("===================lengthOfDataToParse:" + lengthOfDataToParse + " totalLengthOfDataParsed " + totalLengthOfDataParsed + " ts byte :" + tsPacketBuffer[0]);

                        //If there is still no valid TS packet after a read operation, we have reached the end of the file.
                        if (lengthOfDataToParse < (Int64)tsPacketSize)
                        {
                            GetContext().WriteLog("Parsing has been successfully done! There are " + lengthOfDataToParse + " bytes left.\n");
                            break;
                        }

                        //Reset the offset.
                        validPacketOffset = 0;
                    }

                    //Get a TS packet from the block.
                    if (StreamDemux.SYNC_BYTE == tsPacketBuffer[validPacketOffset])
                    {
                        TsPacketMetadata transportPacket = new TsPacketMetadata();

                        //It is a valid TS packet. Process this TS packet.
                        transportPacket.PacketSource = TsPacketSource.SOURCE_FILE;
                        transportPacket.FileOffset = totalLengthOfDataParsed;
                        transportPacket.PacketNumber = packetNumber; //Increase the packet number since we have got one valid TS packet.
                        transportPacket.PacketSize = tsPacketSize;

                        //Invoke the function in the child class to process the TS packet.
                        ProcessTsPacket(transportPacket, tsPacketBuffer, validPacketOffset);

                        //Increase the packet number since we have got one valid TS packet.
                        packetNumber++;

                        //Increase the length of data parsed.
                        totalLengthOfDataParsed += (Int64)tsPacketSize;
                        validPacketOffset += (Int64)tsPacketSize;
                        lengthOfDataToParse -= (Int64)tsPacketSize;

                    }
                    else
                    {
                        //The stream is out of the syncrhonization. We will need to re-locate the position in order to find a valid TS packet.
                        GetContext().WriteLog("Out of synchronization at " + totalLengthOfDataParsed + " bytes of the stream file.\n");
                        String str = String.Format("{0,2:X2} {1,2:X2} {2,2:X2} {3,2:X2} {4,2:X2} {5,2:X2} ", tsPacketBuffer[validPacketOffset], tsPacketBuffer[validPacketOffset + 1], tsPacketBuffer[validPacketOffset + 2], tsPacketBuffer[validPacketOffset + 3], tsPacketBuffer[validPacketOffset + 4], tsPacketBuffer[validPacketOffset + 5]);

                        Console.WriteLine("Out of synchronization at " + totalLengthOfDataParsed + " bytes of the stream file.\n");
                        Console.WriteLine(str);

                        Int64 validPacketOffsetTemp = validPacketOffset;
                        result = StreamDemux.DetectPacketSize(tsPacketBuffer, lengthOfDataToParse, ref validPacketOffset, ref tsPacketSize);//validPacketOffset and tsPacketSize will be changed by this function.

                        if (result.Fine)
                        {
                            //We can continue since we have successfully located a valid TS packet, but we need to know how many bytes we have skipped while relocating.

                            totalLengthOfDataParsed += (validPacketOffset - validPacketOffsetTemp);
                            lengthOfDataToParse -= (validPacketOffset - validPacketOffsetTemp);
                            totalLengthOfDataSkipped += (validPacketOffset - validPacketOffsetTemp);

                            GetContext().WriteLog("Skip " + (validPacketOffset - validPacketOffsetTemp) + " bytes.\n");

                            continue;
                        }
                        else
                        {
                            GetContext().WriteLog("Failed to re-locate the valid TS packet. " + (fileStream.Length - totalLengthOfDataParsed) + " bytes are left.\n");
                            break;
                        }
                    }
                }

                //Give a summary at the end.
                GetContext().WriteLog("Processed packets: " + packetNumber + Environment.NewLine);
                GetContext().WriteLog("There are totally " + totalLengthOfDataSkipped + " bytes skipped. " + 100 * (float)totalLengthOfDataSkipped / fileStream.Length + "% is invalid.\n");

                //Update to 100% no matter how.
                if (100 != savedProgress)
                {
                    OnProgress((Int64)100);
                }
            }

            //Close the stream immediately.
            fileStream.Close();
            fileStream = null;
            parserThread = null;

            //Give the child class to do something.
            OnStop();

        }


        public void StopParse()
        {
            //Set the flag and wait till the thread goes down smoothly.
            canRun = false;

            /*When perform join method, it may block the delegate functions, thus....sorry... dead lock! But if we don't join the thread, we may release the resouces that are still in use...Shit....
             * So, we can only call BeginInvoke instead of Invoke and join the thread.
             */
            if (null != parserThread)
            {
                //Before closing the stream, we need to close the thread first.
                parserThread.Join();
                parserThread = null;
            }

            //Close the stream if it is open.
            if (null != fileStream)
            {
                //Safe to close the stream now.
                fileStream.Close();
            }
        }

        //To be overrided by  by base class.
        public virtual void ProcessTsPacket(TsPacketMetadata tsPacketMetadata, byte[] packetBuffer, Int64 packetOffsetInBuffer)
        {
        }
        public virtual void OnStart()
        {
        }
        public virtual void OnStop()
        { 
        }
        public virtual void OnProgress(Int64 progress)
        {
            messageCallback(MessageId.MESSAGE_NEW_PROGRESS, progress);
        }
    }
}
