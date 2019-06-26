﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InActionLibrary
{
    //The class to contain the necessary info for a transport packet.
    public class TsPacketMetadata
    {
        //From local file or from IP network.
        public TsPacketSource PacketSource {get;set;}

        //The following info will be valid when the packet is from a local file.
        public Int64 FileOffset{get;set;} //Offset of this packet in the file.
        public Int64 PacketNumber{get;set;} //Packet number. The first TS packet from file will have number ZERO and the second is ONE.
        public TsPacketSize PacketSize{get;set;} //Size of the packet.
    };
}
