using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InActionLibrary;

namespace TSE
{
    public class PidProfile
    {
        public static UInt16 SUPER_PID = 0xFFFF;
        public PidProfile()
        {
            Description = "Unknown";
            Type = DataType.TS_PACKET;
            PacketCount = 0;
        }
        public UInt16 PID
        {
            get;
            set;
        }

        public Int64 PacketCount
        {
            get;
            set;
        }

        public String Description
        {
            get;
            set;
        }

        //Try to indicate the payload type in the packet, i.e. PES_PACKET or section.
        public DataType Type
        {
            get;
            set;
        }

        //How much percentage of data does this PID occupy in this stream.
        public float Percentage
        {
            get;
            set;
        }

        public override String ToString()
        {
            return String.Format("PID: 0x{0, 4:X4}, Count: {1, 12:d}, Percentage: {2, 8:0.00000}%, Type: {3}", this.PID, this.PacketCount, this.Percentage, this.Description);
        }
    }
}
