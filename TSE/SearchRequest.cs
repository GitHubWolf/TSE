using InActionLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSE
{
    public class SearchRequest
    {
        public UInt16 SelectedPid
        {
            get;
            set;
        }

        public DataType SearchType
        {
            get;
            set;
        }

        public byte[] FilterMask
        {
            get;
            set;
        }

        public byte[] FilterMatch
        {
            get;
            set;
        }

        public Int64 SearchCount
        {
            get;
            set;
        }

        public Int64 CountOfSkipFound
        {
            get;
            set;
        }

        /**
         * How many TS packets will be skipped(without checking the PID).
         */
        public Int64 CountOfSkipTsPacket
        {
            get;
            set;
        }

        public bool ShowDuplicateSection
        {
            get;
            set;
        }

        public bool IgnorePID
        {
            get;
            set;
        }


        public bool DumpToFile
        {
            get;
            set;
        }

        public override string ToString()
        {
            String info = null;
            switch (SearchType)
            {
                case DataType.PES_PACKET:
                    info = "Search Type: PES Packet";
                    break;
                case DataType.SECTION:
                    info = "Search Type: Section";
                    break;
                case DataType.TS_PACKET:
                    info = "Search Type: TS Packet";
                    break;
                case DataType.UNKNOWN:
                    info = "Search Type: Unknown";
                    break;
            }
            info += ", Search Count: " + SearchCount + ", Skip Found: " + CountOfSkipFound + ", Skip TS Packets: " + CountOfSkipTsPacket;

            return info;
        }

        public Plugin SelectedParser
        {
            get;
            set;
        }
    }
}
