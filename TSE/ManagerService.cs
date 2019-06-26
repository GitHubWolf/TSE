using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InActionLibrary;

namespace TSE
{
    class ManagerService : StreamParserSession
    {
        //Program list.
        private SortedDictionary<UInt16, Service> serviceList = new SortedDictionary<UInt16, Service>();

        public ManagerService(StreamParserContext owner)
            : base(owner)
        {
 
        }

        public Result ProcessPatSection(DataStore section, List<Service> serviceList)
        {
            Result result = new Result();
            byte[] data = section.GetData();

            //PAT must at least have 8 bytes header and 4 bytes crc32.
            if (section.GetLeftBitLength() <= (12*8))
            {
                result.SetResult(ResultCode.INSUFFICIENT_DATA);
            }

            if (result.Fine)
            {
                //Check CRC32.
                if (0 != Utility.GetCrc32(data, 0, data.Length))
                {
                    result.SetResult(ResultCode.INVALID_DATA);//CRC error.
                }
            }

            Int64 bitoffset  = 0;
            if (result.Fine)
            {
                //Skip 8 bytes header.
                result = section.SkipBits(ref bitoffset, 8 * 8);
            }

            if (result.Fine)
            {
                //To remove crc32.
                result = section.CutOff(32);
            }

            if (result.Fine)
            {
                Int64 programNumber = 0;
                Int64 pmtPid =0;
                while (section.GetLeftBitLength() >= (4*8))
                {
                    //16-bit program_number
                    section.ReadBits(ref bitoffset, 16, ref programNumber);
                    section.SkipBits(ref bitoffset, 3);// 3-bit reserved.
                    section.ReadBits(ref bitoffset, 13, ref pmtPid);

                    Service service = new Service();
                    service.ServiceId = (UInt16)programNumber;
                    service.PmtPid = (UInt16)pmtPid;
                    
                    serviceList.Add(service);
                }
            }

            return result;
        }

        public Result ProcessPmtSection(DataStore section, out Service service)
        {
            Result result = new Result();
            byte[] data = section.GetData();

            //PMT must at least have 8 bytes header and 4 bytes crc32.
            if (section.GetLeftBitLength() <= (12 * 8))
            {
                result.SetResult(ResultCode.INSUFFICIENT_DATA);
            }

            if (result.Fine)
            {
                //Check CRC32.
                if (0 != Utility.GetCrc32(data, 0, data.Length))
                {
                    result.SetResult(ResultCode.INVALID_DATA);//CRC error.
                }
            }

            Int64 bitoffset = 0;
            if (result.Fine)
            {
                //Skip 3 bytes header.
                result = section.SkipBits(ref bitoffset, 3 * 8);
            }

            Int64 programNumber = 0;
            if (result.Fine)
            {
                //Read program number.
                result = section.ReadBits(ref bitoffset, 16, ref programNumber);
            }

            if (result.Fine)
            {
                //Skip version_number, section_number and last_section_number and 3 bits reserved.
                result = section.SkipBits(ref bitoffset, 3 * 8 + 3);
            }

            Int64 pcrPID = 0;
            if (result.Fine)
            {
                result = section.ReadBits(ref bitoffset, 13, ref pcrPID);
            }


            if (result.Fine)
            {
                //To remove crc32.
                result = section.CutOff(32);
            }

            service = null;
            if (result.Fine)
            {
                service = new Service();
                service.PcrPid = (UInt16)pcrPID;
                service.ServiceId = (UInt16)programNumber;

                //Skip 4 bits reserved.
                result = section.SkipBits(ref bitoffset, 4);
            }

            Int64 programInfoLength = 0;
            if (result.Fine)
            {
                result = section.ReadBits(ref bitoffset, 12, ref programInfoLength);
            }

            if (result.Fine)
            {
                //Get all program level CA descriptors.
                result = FindAllCaDescriptor(section, ref bitoffset, service.GetCaDescriptorList(), ref programInfoLength);
            }

            if (result.Fine)
            {
                Int64 streamType = 0;
                Int64 elementaryPid = 0;
                Int64 esInfoLength = 0;
                while (section.GetLeftBitLength() > 0)
                {
                    result = section.ReadBits(ref bitoffset, 8, ref streamType);

                    if (result.Fine)
                    {
                        result = section.SkipBits(ref bitoffset, 3);//Skip 3 bits reserved.
                    }

                    if (result.Fine)
                    {
                        result = section.ReadBits(ref bitoffset, 13, ref elementaryPid);
                    }

                    if (result.Fine)
                    {
                        result = section.SkipBits(ref bitoffset, 4);//Skip 4 bits reserved.
                    }

                    if (result.Fine)
                    {
                        result = section.ReadBits(ref bitoffset, 12, ref esInfoLength);
                    }

                    if (result.Fine)
                    {
                        ComponentStream componentStream = new ComponentStream();

                        componentStream.StreamType = (byte)streamType;
                        componentStream.ElementaryPid = (UInt16)elementaryPid;
                        
                        //Get all stream level CA descriptors.
                        result = FindAllCaDescriptor(section, ref bitoffset, componentStream.GetCaDescriptorList(), ref esInfoLength);

                        if (result.Fine)
                        {
                            service.AddComponentStream(componentStream);
                        }
                    }

                    if (!result.Fine)
                    {
                        break;
                    }
                }
            }

            return result;
        }

        private Result FindAllCaDescriptor(DataStore section, ref Int64 bitoffset, List<CaDescriptor> descriptorList, ref Int64 descriptorLoopLength)
        {
            Result result = new Result();
            Int64 descriptorTag = 0;
            Int64 descriptorLength = 0;
            Int64 caSystemId = 0;
            Int64 caPid = 0;


            while (descriptorLoopLength > 0)
            {
                result = section.ReadBits(ref bitoffset, 8, ref descriptorTag);

                if (result.Fine)
                {
                    result = section.ReadBits(ref bitoffset, 8, ref descriptorLength);
                }

                if (result.Fine)
                {
                    if (descriptorTag == 0x09)
                    {
                        result = section.ReadBits(ref bitoffset, 16, ref caSystemId);

                        if (result.Fine)
                        {
                            result = section.SkipBits(ref bitoffset, 3);
                        }

                        if (result.Fine)
                        {
                            result = section.ReadBits(ref bitoffset, 13, ref caPid);
                        }

                        if (result.Fine)
                        {
                            result = section.SkipBits(ref bitoffset, (descriptorLength - 4) * 8);
                        }

                        if (result.Fine)
                        {
                            //Add it into the program level descriptor.

                            CaDescriptor caDescriptor = new CaDescriptor();
                            caDescriptor.CaPid = (UInt16)caPid;
                            caDescriptor.CaSystemId = (UInt16)caSystemId;

                            descriptorList.Add(caDescriptor);
                        }
                    }
                    else
                    {
                        //Skip the payload.
                        result = section.SkipBits(ref bitoffset, descriptorLength * 8);
                    }
                }

                if (result.Fine)
                {
                    descriptorLoopLength -= (2 + descriptorLength);//Decrease by the length of the whole descriptor.
                }
                else
                {
                    break;
                }
            }

            if (result.Fine)
            {
                if (descriptorLoopLength != 0)
                {
                    //We expect no data left at the end.
                    result.SetResult(ResultCode.INVALID_DATA);
                }
            }
            return result;
        }

        public Result ProcessCatSection(DataStore section, List<CaDescriptor> caDescriptorList)
        {
            Result result = new Result();
            byte[] data = section.GetData();

            //CAT must at least have 8 bytes header and 4 bytes crc32.
            if (section.GetLeftBitLength() <= (12 * 8))
            {
                result.SetResult(ResultCode.INSUFFICIENT_DATA);
            }

            if (result.Fine)
            {
                //Check CRC32.
                if (0 != Utility.GetCrc32(data, 0, data.Length))
                {
                    result.SetResult(ResultCode.INVALID_DATA);//CRC error.
                }
            }

            Int64 bitoffset = 0;
            if (result.Fine)
            {
                //Skip 8 bytes header.
                result = section.SkipBits(ref bitoffset, 8 * 8);
            }

            if (result.Fine)
            {
                //To remove crc32.
                result = section.CutOff(32);
            }

            Int64 leftBits = section.GetLeftBitLength()/8;
            if (result.Fine)
            {
                //Get all program level CA descriptors.
                result = FindAllCaDescriptor(section, ref bitoffset, caDescriptorList, ref leftBits);
            }

            return result;
        }
    }
}
