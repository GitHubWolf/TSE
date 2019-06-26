using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InActionLibrary;

namespace TSE
{
    public class ManagerPidBitrate
    {
        //The list to save PID bitrate for each second.
        private SortedDictionary<UInt16, PidBitrate> pidBitrateList = new SortedDictionary<ushort, PidBitrate>();

        public SortedDictionary<UInt16, PidBitrate> FetchCurrentBitrates( )
        {
            //Return current one.
            SortedDictionary<UInt16, PidBitrate> currentPidBitrateList = pidBitrateList;

            //Create a new list to save the the bitrate for next second.
            pidBitrateList = new SortedDictionary<ushort, PidBitrate>();

            return currentPidBitrateList;
        }

        public void NewTsPacket(TsPacketMetadata tsPacketMetadata, UInt16 pid)
        {
            PidBitrate pidBitrate = null;

            if (!pidBitrateList.TryGetValue(pid, out pidBitrate))
            {
                //Create a new one.
                pidBitrate = new PidBitrate(pid);
                pidBitrateList.Add(pid, pidBitrate);
            }

            //Increase the exsiting size.
            pidBitrate.AddTsPacket(tsPacketMetadata);
        }
    }
}
