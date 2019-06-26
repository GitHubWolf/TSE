using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InActionLibrary;
using System.IO;

namespace TSE
{
    public class WorkerFileDataSearch : WorkerFile
    {

        //SectionManager will be created only at search mode.
        private ManagerSection sectionManager = null;
        private Int64 receivedCount = 0;


        //To save search request information.
        private SearchRequest searchRequest = null;

        //To perform the demux.
        private StreamDemux streamDemux = null;

        //File stream to dump the data.
        private FileStream fileStreamDump = null;
        private String fileNameDump = null;

        public WorkerFileDataSearch(StreamParserContext owner, MessageHandlerDelegate messageCallbackFromOwner, SearchRequest searchRequest)
            : base(owner, messageCallbackFromOwner)
        {
            this.sectionManager = new ManagerSection(owner);
            this.streamDemux = new StreamDemux(owner);
            this.searchRequest = searchRequest;

            //In search mode. We will not parse standard SI/PSI sections, instead ,we will set up a filter for the expected data.

            Filter filterForSearch = null;
            if (searchRequest.SearchType == DataType.SECTION)
            {
                //To search sections.
                filterForSearch = new Filter(owner, HandleDataFromDemux, searchRequest.SearchType, searchRequest.SelectedPid, searchRequest.FilterMask.Length, searchRequest.FilterMask, searchRequest.FilterMatch);
            }
            else
            {
                //To search TS packet or PES packet.
                filterForSearch = new Filter(owner, HandleDataFromDemux, searchRequest.SearchType, searchRequest.SelectedPid);
            }

            //In order to fitler out what we are expecting.
            streamDemux.AddFilter(filterForSearch);

        }

        public override void ProcessTsPacket(TsPacketMetadata tsPacketMetadata, byte[] packetBuffer, Int64 packetOffsetInBuffer)
        {
            //In search mode, we may need to skip several TS packets according to the request.
            if (tsPacketMetadata.PacketNumber >= searchRequest.CountOfSkipTsPacket)
            {
                //Invoke the stream parser to process a TS packet.
                streamDemux.ProcessTsPacket(tsPacketMetadata, packetBuffer, packetOffsetInBuffer);
            }
        }
        public override void OnStart()
        {
            if (this.searchRequest.DumpToFile)
            {
                //Open a file to dump the data into.
                try
                {
                    fileNameDump = AppDomain.CurrentDomain.BaseDirectory 
                                            + "Data "
                                            + DateTime.Now.Year 
                                            + "-" + DateTime.Now.Month 
                                            + "-" + DateTime.Now.Day 
                                            + "-" + DateTime.Now.Hour 
                                            + "-" + DateTime.Now.Minute 
                                            + "-" +DateTime.Now.Second 
                                            +".txt";
                    fileStreamDump = new FileStream(fileNameDump, FileMode.CreateNew, FileAccess.Write);
                }
                catch (Exception)
                {
                    //result.SetResult(ResultCode.FAILED_TO_OPEN_FILE);
                }
            }
        }
        public override void OnStop()
        {
            //Send a message to notify that we have finished doing everything.
            messageCallback(MessageId.MESSAGE_SEARCH_DONE, null);

            if (this.searchRequest.DumpToFile)
            {
                //Close the file to dump the data.
                if (null != fileStreamDump)
                {
                    fileStreamDump.Close();
                }

                //Open the file with default application.
                System.Diagnostics.Process.Start(fileNameDump);
            }
        }

        //To process data notified from ChannelDataStore.
        private void HandleDataFromDemux(UInt16 pid, byte[] data, Int64 dataOffset, Int64 dataLength, List<TsPacketMetadata> packetMetadataList)
        {
            DataStore section = new DataStore(data, dataOffset, dataLength);
            section.SetPacketMedataList(packetMetadataList);

            //We will need to perform some filtering in order to reduce the messages to the form, so that we can improve GUI performance.
            if ((searchRequest.SearchType == DataType.SECTION) && (searchRequest.ShowDuplicateSection == false))
            {
                bool isNew = sectionManager.AddSection(0, section);

                if (isNew)//New one and we don't want to show duplicate sections.
                {
                    if (receivedCount >= searchRequest.CountOfSkipFound)
                    {
                        if (searchRequest.DumpToFile)
                        {
                            //Dump the HEX data into the file directly.
                            DumpDataToFile(section);
                        }
                        else
                        {
                            messageCallback(MessageId.MESSAGE_SEARCHED_DATA, section);//Pass to the form.
                        }

                        receivedCount++;
                    }
                }
            }
            else
            {
                if (receivedCount >= searchRequest.CountOfSkipFound)
                {
                    if (searchRequest.DumpToFile)
                    {
                        //Dump the HEX data into the file directly.
                        DumpDataToFile(section);
                    }
                    else
                    {
                        messageCallback(MessageId.MESSAGE_SEARCHED_DATA, section);//Pass to the form.
                    }
                }

                receivedCount++;
            }

            if (searchRequest.SearchCount != -1)
            {
                //We are not going to search ALL sections.
                if (receivedCount >= (searchRequest.SearchCount + searchRequest.CountOfSkipFound))
                {
                    StopWorking();
                }
            }

        }//HandleDataFromDemux


        private void DumpDataToFile(DataStore dataStore)
        {
            string hexStr = Utility.GetHexString(dataStore.GetData()) + Environment.NewLine;
            byte[] bytesHex = Encoding.UTF8.GetBytes(hexStr);
            fileStreamDump.Write(bytesHex, 0, bytesHex.Length);
        }
    }
}
