using InActionLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSE
{
    class ManagerMuxBitrate
    {
        private SortedDictionary<UInt16, MuxBitrate> streamBitrateList = new SortedDictionary<UInt16, MuxBitrate>();

        public SortedDictionary<UInt16, MuxBitrate>  GetStreamBitrateList()
        {
            return streamBitrateList;
        }

        public void ParseAdaptationField(TsPacketMetadata tsPacketMetadata, UInt16 pid, byte[] packetBuffer, Result result, Int64 dataLeftInBit, Int64 bitOffset, Int64 adaptationFieldControl)
        {
            Int64 pcr = 0;
            Int64 discontinuityIndicator = 0;

            MuxBitrate streamBitrate = null;

            bool pcrFound = GetPCR(tsPacketMetadata, packetBuffer, dataLeftInBit, bitOffset, adaptationFieldControl, ref pcr, ref discontinuityIndicator);

            if (pcrFound)
            {
                if (!streamBitrateList.TryGetValue(pid, out streamBitrate))
                {
                    streamBitrate = new MuxBitrate(pid);
                    streamBitrateList.Add(pid, streamBitrate);
                }

                if (!streamBitrate.BitrateAvailable)
                {
                    if (1 == discontinuityIndicator)
                    {
                        //Discontinuity is detected, the new PCR will be used as the first PCR.
                        streamBitrate.SaveFirstPcr(pcr, tsPacketMetadata.FileOffset);
                    }//if (1 == discontinuityIndicator)
                    else
                    {
                        if (streamBitrate.FirsPcrReady())
                        {
                            //Save second PCR if the first one is available.
                            streamBitrate.SaveSecondPcr(pcr, tsPacketMetadata.FileOffset);
                        }
                        else
                        {
                            //Save as the first one.
                            streamBitrate.SaveFirstPcr(pcr, tsPacketMetadata.FileOffset);
                        }
                    }//else
                }//if (!streamBitrate.BitrateAvailable)

            }//if (pcrFound)

        }

        private bool GetPCR(TsPacketMetadata tsPacketMetadata, byte[] data, Int64 dataLeftInBit, Int64 bitOffset, Int64 adaptationFieldLength, ref Int64 pcr, ref Int64 discontinuityIndicator)
        {
            bool pcrValid = false;
            Int64 fieldValue = 0;
            Result result = new Result();

            adaptationFieldLength = adaptationFieldLength * 8;//Convert to bits.

            if (adaptationFieldLength <= 0)
            {
                result.SetResult(ResultCode.DATA_MISMATCH);
            }

            if (result.Fine)
            {
                //discontinuity_indicator
                result = Utility.ByteArrayReadBits(data, ref dataLeftInBit, ref bitOffset, 1, ref discontinuityIndicator);
            }

            if (result.Fine)
            {
                //random_access_indicator,elementary_stream_priority_indicator
                result = Utility.ByteArrayReadBits(data, ref dataLeftInBit, ref bitOffset, 2, ref fieldValue);
            }

            Int64 pcrFlag = 0;
            if (result.Fine)
            {
                result = Utility.ByteArrayReadBits(data, ref dataLeftInBit, ref bitOffset, 1, ref pcrFlag);//PCR_flag
            }

            if (result.Fine)
            {
                //OPCR_flag,splicing_point_flag,transport_private_data_flag,adaptation_field_extension_flag
                result = Utility.ByteArrayReadBits(data, ref dataLeftInBit, ref bitOffset, 4, ref fieldValue);
            }

            if (result.Fine && (1 == pcrFlag))
            {

                Int64 programClockReferenceBase = 0;
                if (result.Fine)
                {
                    result = Utility.ByteArrayReadBits(data, ref dataLeftInBit, ref bitOffset, 33, ref programClockReferenceBase);//program_clock_reference_base
                }

                if (result.Fine)
                {
                    //Reserved
                    result = Utility.ByteArrayReadBits(data, ref dataLeftInBit, ref bitOffset, 1, ref fieldValue);
                }

                Int64 programClockReferenceExtension = 0;
                if (result.Fine)
                {
                    //program_clock_reference_extension
                    result = Utility.ByteArrayReadBits(data, ref dataLeftInBit, ref bitOffset, 9, ref programClockReferenceExtension);
                }

                if (result.Fine)
                {
                    //Convert to complete PCR.
                    pcrValid = true;

                    pcr = programClockReferenceBase * 300 + programClockReferenceExtension;
                }
            }

            return pcrValid;
        }

    }
}
