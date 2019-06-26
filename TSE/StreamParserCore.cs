using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InActionLibrary;

namespace TSE
{
    public class StreamParserCore : StreamParserSession
    {
        //SectionManager to manage all filtered sections. Duplicate sections will not be notified to the form.
        private ManagerSection sectionManager = null;

        //To build up a servie list. The main purpose is to parse PAT and know the service_id and pmt_pid, so that we can filter out PMT.
        private ManagerService serviceManager = null;

        //Refer to the stream parser instance.
        private StreamDemux streamDemux = null;

        //PidManager to do the statistics.
        private ManagerPid pidManager = null;

        //Bitrate manager to detect stream bitrate by using PCR.
        private ManagerMuxBitrate streamBitrateManager = null;

        //Callback message handler from owner. We will use it to notify all kinds of messages.
        private MessageHandlerDelegate messageCallback = null;

        public StreamParserCore(StreamParserContext owner, MessageHandlerDelegate messageCallback)
            : base(owner)
        {
            this.streamDemux = new StreamDemux(owner);

            /**
             * Create all necessary modules even we don't need all them in some cases.
             * In live environment,i.e. IP network or device, we will need all of them.
             */
            sectionManager = new ManagerSection(owner);

            serviceManager = new ManagerService(owner);

            streamBitrateManager = new ManagerMuxBitrate();

            pidManager = new ManagerPid(owner);

            AddDefaultPidType();

            this.messageCallback = messageCallback;

            //Set up filters to receive standard SI/PSI sections.
            EnableStandardFilters(streamDemux);
        }

        //Process some special sections that we expect.
        private void ProcessSection(DataStore section)
        {
            byte[] data = section.GetData();

            Result result = new Result();

            //Clean up offset and cutoff!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            section.Reset();

            if (data[0] == (byte)TableId.PAT)
            {
                List<Service> serviceList = new List<Service>();
                result = serviceManager.ProcessPatSection(section, serviceList);
                if (serviceList.Count > 0)
                {
                    foreach (Service service in serviceList)
                    {
                        //Check service ID, in case PID for NIT is different from what we expect.
                        if (0 == service.ServiceId)
                        {
                            if ((UInt16)TsPID.NIT != (service.PmtPid))
                            {
                                //Set up filter to filter out NIT if NIT's PID in PAT is different from what we expect.
                                streamDemux.AddFilter(new Filter(GetContext(),ProcessFilteredData,
                                    DataType.SECTION,
                                    service.PmtPid, //PID.
                                    1,                          //Filter depth.
                                    new byte[1] { 0xFE },          //Mask.!!!!!!!!!!!!!!!!!!!!!!!!!!!!!0xFE!!!!!!!!!!!!!!!!!
                                    new byte[1] { (byte)TableId.NIT_ACTUAL }));        //Match.

                                //Set the type.!!!!!!!!!!!!!!!!!
                                pidManager.SetPidTypePlus(service.PmtPid, DataType.SECTION, "NIT");
                            }
                        }
                        else
                        {
                            //Set up filter to filter out PMT.
                            streamDemux.AddFilter(new Filter(GetContext(),ProcessFilteredData,
                                DataType.SECTION,
                                service.PmtPid, //PID.
                                1,                          //Filter depth.
                                new byte[1] { 0xFF },          //Mask.
                                new byte[1] { (byte)TableId.PMT }));        //Match.

                            //Set the type.!!!!!!!!!!!!!!!!!
                            pidManager.SetPidTypePlus(service.PmtPid, DataType.SECTION, "PMT PID for service: " + Utility.GetValueHexString(service.ServiceId, 16));
                        }
                    }
                }
            }//PAT
            else if (data[0] == (byte)TableId.PMT)
            {
                Service service = null;
                result = serviceManager.ProcessPmtSection(section, out service);

                if (result.Fine)
                {
                    pidManager.SetPidTypePlus(service.PcrPid, DataType.TS_PACKET, "PCR PID for service: " + Utility.GetValueHexString(service.ServiceId, 16));

                    List<CaDescriptor> caDescriptorList = service.GetCaDescriptorList();
                    foreach (CaDescriptor caDescriptor in caDescriptorList)
                    {
                        pidManager.SetPidTypePlus(caDescriptor.CaPid, DataType.SECTION, "ECM PID for service: " + Utility.GetValueHexString(service.ServiceId, 16) + " CA_system_ID: " + Utility.GetValueHexString(caDescriptor.CaSystemId, 16));
                    }

                    List<ComponentStream> componentStreamList = service.GetComponentStreamList();
                    foreach (ComponentStream componentStream in componentStreamList)
                    {
                        pidManager.SetPidTypePlus(componentStream.ElementaryPid, DataType.PES_PACKET, "Component stream for service: " + Utility.GetValueHexString(service.ServiceId, 16) + " Type: " + DataParser.GetStreamTypeName(componentStream.StreamType));

                        //To update according to component level CA descriptor.
                        foreach (CaDescriptor caDescriptor in componentStream.GetCaDescriptorList())
                        {
                            pidManager.SetPidTypePlus(caDescriptor.CaPid, DataType.SECTION, "ECM PID for service: " + Utility.GetValueHexString(service.ServiceId, 16) + " CA_system_ID: " + Utility.GetValueHexString(caDescriptor.CaSystemId, 16));
                        }

                    }
                }
            }//PMT
            else if (data[0] == (byte)TableId.CAT)
            {
                List<CaDescriptor> caDescriptorList = new List<CaDescriptor>();
                result = serviceManager.ProcessCatSection(section, caDescriptorList);

                if (result.Fine)
                {
                    foreach (CaDescriptor caDescriptor in caDescriptorList)
                    {
                        pidManager.SetPidTypePlus(caDescriptor.CaPid, DataType.SECTION, "EMM PID for CA_system_ID: " + Utility.GetValueHexString(caDescriptor.CaSystemId, 16));
                    }
                }
            }//CAT
        }//ProcessSection

        private void AddDefaultPidType()
        {
            pidManager.SetPidTypeStandard((UInt16)TsPID.PAT, DataType.SECTION, "PAT");
            pidManager.SetPidTypeStandard((UInt16)TsPID.CAT, DataType.SECTION, "CAT");
            pidManager.SetPidTypeStandard((UInt16)TsPID.TSDT, DataType.SECTION, "TSDT");
            pidManager.SetPidTypeStandard((UInt16)TsPID.NIT, DataType.SECTION, "NIT");
            pidManager.SetPidTypeStandard((UInt16)TsPID.BAT_SDT, DataType.SECTION, "BAT/SDT");
            pidManager.SetPidTypeStandard((UInt16)TsPID.EIT, DataType.SECTION, "EIT");
            pidManager.SetPidTypeStandard((UInt16)TsPID.RST, DataType.SECTION, "RST");
            pidManager.SetPidTypeStandard((UInt16)TsPID.TDT_TOT, DataType.SECTION, "TDT/TOT");
            pidManager.SetPidTypeStandard((UInt16)TsPID.NST, DataType.SECTION, "NST");
            pidManager.SetPidTypeStandard((UInt16)TsPID.IST, DataType.SECTION, "IST");
            pidManager.SetPidTypeStandard((UInt16)TsPID.MT, DataType.SECTION, "MT");
            pidManager.SetPidTypeStandard((UInt16)TsPID.DIT, DataType.SECTION, "DIT");
            pidManager.SetPidTypeStandard((UInt16)TsPID.SIT, DataType.SECTION, "SIT");
            pidManager.SetPidTypeStandard((UInt16)0x1FFF, DataType.TS_PACKET, "Stuffing");
        }//AddDefaultPidType

        public SortedDictionary<UInt16, PidProfile> GetPidList()
        {
            return pidManager.GetPidList();
        }

        public void Stop()
        {
            if (null != pidManager)
            {
                pidManager.CalculatePercentage();
            }
        }

        public SortedDictionary<UInt16, MuxBitrate> GetStreamBitrateList()
        {
            return streamBitrateManager.GetStreamBitrateList();
        }

        public Result ProcessTsPacket(TsPacketMetadata tsPacketMetadata, byte[] packetBuffer, Int64 packetOffsetInBuffer)
        {
            Result result = new Result();
            //Int64 dataLeftInBit = (Int64)tsPacketMetadata.PacketSize;
            Int64 dataLeftInBit = (Int64)TsPacketSize.SIZE_188 * 8;//Important!!!!No matter 188 or 204 bytes, the valid data will always be 188 bytes.16 bytes are checksum that is not useful for parsing.

            Int64 bitOffset = packetOffsetInBuffer * 8;//Key point to set the beginning offset!!!!!

            //Important!!! Pass a copy of the TS packe to the demux module, so that it can assemble ts packet into section or PES packet.
            streamDemux.ProcessTsPacket(tsPacketMetadata, packetBuffer, packetOffsetInBuffer);


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

                    if (result.Fine)
                    {
                        /*Parse adaptation field to get PCR etc.*/
                        streamBitrateManager.ParseAdaptationField(tsPacketMetadata, (UInt16)pid, packetBuffer, result, dataLeftInBit, bitOffset, adaptationFieldControl);
                    }

                    //Skip adaption fields according to the adaption_field_length.
                    if (result.Fine)
                    {
                        result = Utility.ByteArraySkipBits(ref dataLeftInBit, ref bitOffset, (adaptationFieldLength * 8));
                    }
                }
            }

            if (result.Fine)
            {
                //Notify a new packet to PidManager so that it can do the statistics.
                pidManager.AddPacket((UInt16)pid);
            }

            return result;
        }

        //Enable standard filters to receive SI/PSI sections.
        public void EnableStandardFilters(StreamDemux streamDemux)
        {
            //To filter PAT.
            streamDemux.AddFilter(new Filter(GetContext(), ProcessFilteredData,
                DataType.SECTION,
                (UInt16)TsPID.PAT, //PID.
                1,                          //Filter depth.
                new byte[1] { 0xFF },          //Mask.
                new byte[1] { (byte)TableId.PAT }));        //Match.

            //To filter CAT.
            streamDemux.AddFilter(new Filter(GetContext(), ProcessFilteredData,
                DataType.SECTION,
                (UInt16)TsPID.CAT, //PID.
                1,                          //Filter depth.
                new byte[1] { 0xFF },          //Mask.
                new byte[1] { (byte)TableId.CAT }));        //Match.

            //To filter TSDT.
            streamDemux.AddFilter(new Filter(GetContext(), ProcessFilteredData,
                DataType.SECTION,
                (UInt16)TsPID.TSDT, //PID.
                1,                          //Filter depth.
                new byte[1] { 0xFF },          //Mask.
                new byte[1] { (byte)TableId.TSDT }));        //Match.

            //To filter NIT for current network and for other network.
            streamDemux.AddFilter(new Filter(GetContext(), ProcessFilteredData,
                DataType.SECTION,
                (UInt16)TsPID.NIT, //PID.
                1,                          //Filter depth.
                new byte[1] { 0xFE },          //Mask. To filter out table ID 0x40 and 0x41.
                new byte[1] { (byte)TableId.NIT_ACTUAL }));        //Match.

            //To filter SDT for current stream and for other stream.
            streamDemux.AddFilter(new Filter(GetContext(), ProcessFilteredData,
                DataType.SECTION,
                (UInt16)TsPID.BAT_SDT, //PID.
                1,                          //Filter depth.
                new byte[1] { 0xFB },          //Mask.To filter out table ID 0x42 and 0x46.
                new byte[1] { (byte)TableId.SDT_ACTUAL }));        //Match.

            //To filter BAT.
            streamDemux.AddFilter(new Filter(GetContext(), ProcessFilteredData,
                DataType.SECTION,
                (UInt16)TsPID.BAT_SDT, //PID.
                1,                          //Filter depth.
                new byte[1] { 0xFF },          //Mask.
                new byte[1] { (byte)TableId.BAT }));        //Match.

            //To filter EIT.
            streamDemux.AddFilter(new Filter(GetContext(), ProcessFilteredData,
                DataType.SECTION,
                (UInt16)TsPID.EIT, //PID.
                1,                          //Filter depth.
                new byte[1] { 0xFE },          //Mask. To filter out table ID 0x4E and 0x4F.
                new byte[1] { (byte)TableId.EIT_PF_ACTUAL }));        //Match.

            //To filter EIT schedule for current stream.
            streamDemux.AddFilter(new Filter(GetContext(), ProcessFilteredData,
                DataType.SECTION,
                (UInt16)TsPID.EIT, //PID.
                1,                          //Filter depth.
                new byte[1] { 0xF0 },          //Mask. To filter out table ID 0x50 to 0x5F.
                new byte[1] { (byte)TableId.EIT_SCHEDULE_ACTUAL }));        //Match.

            //To filter EIT schedule for other stream.
            streamDemux.AddFilter(new Filter(GetContext(), ProcessFilteredData,
                DataType.SECTION,
                (UInt16)TsPID.EIT, //PID.
                1,                          //Filter depth.
                new byte[1] { 0xF0 },          //Mask. To filter out table ID 0x50 to 0x5F.
                new byte[1] { (byte)TableId.EIT_SCHEDULE_OTHER }));        //Match.

            //To filter RST.
            streamDemux.AddFilter(new Filter(GetContext(), ProcessFilteredData,
                DataType.SECTION,
                (UInt16)TsPID.RST, //PID.
                1,                          //Filter depth.
                new byte[1] { 0xFF },          //Mask.
                new byte[1] { (byte)TableId.RST }));        //Match.

            //To filter TDT.
            streamDemux.AddFilter(new Filter(GetContext(), ProcessFilteredData,
                DataType.SECTION,
                (UInt16)TsPID.TDT_TOT, //PID.
                1,                          //Filter depth.
                new byte[1] { 0xFF },          //Mask.
                new byte[1] { (byte)TableId.TDT }));        //Match.

            //To filter TOT.
            streamDemux.AddFilter(new Filter(GetContext(), ProcessFilteredData,
                DataType.SECTION,
                (UInt16)TsPID.TDT_TOT, //PID.
                1,                          //Filter depth.
                new byte[1] { 0xFF },          //Mask.
                new byte[1] { (byte)TableId.TOT }));        //Match.

            //To filter DIT.
            streamDemux.AddFilter(new Filter(GetContext(), ProcessFilteredData,
                DataType.SECTION,
                (UInt16)TsPID.DIT, //PID.
                1,                          //Filter depth.
                new byte[1] { 0xFF },          //Mask.
                new byte[1] { (byte)TableId.DIT }));        //Match.

            //To filter SIT.
            streamDemux.AddFilter(new Filter(GetContext(), ProcessFilteredData,
                DataType.SECTION,
                (UInt16)TsPID.SIT, //PID.
                1,                          //Filter depth.
                new byte[1] { 0xFF },          //Mask.
                new byte[1] { (byte)TableId.SIT }));        //Match.
        }

        void ProcessFilteredData(UInt16 pid, byte[] data, Int64 dataOffset, Int64 dataLength, List<TsPacketMetadata> packetMetadataList)
        {
            DataStore section = new DataStore(data, dataOffset, dataLength);
            section.SetPacketMedataList(packetMetadataList);

            //Try to add the dataStore into the section manager.
            if (sectionManager.AddSection(pid, section))
            {
                //!!!!!!!!!!!!!!!!Important point. We will need to process some special sections in order to filter out all necessary sections.
                ProcessSection(section);

                //Return value of AddSection is true(it means that this is a new section), we will notify this dataFound to the form.
                messageCallback(MessageId.MESSAGE_STANDARD_SECTION, section);
            }
            else
            {
                //Duplicate sections.
            }
        }
    }
}
