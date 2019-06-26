using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InActionLibrary;

namespace TSE
{
    public class PidBitrate
    {
        private UInt16 pid = 0;
        private Int64 totalSize = 0;

        public PidBitrate(UInt16 pid)
        {
            this.pid = pid;
        }

        public void AddTsPacket(TsPacketMetadata tsPacketMetadata)
        {
            totalSize += (Int64)tsPacketMetadata.PacketSize;
        }

        public Int64 Bitrate
        {
            get
            {
                return totalSize * 8;
            }
        }
    }
}
