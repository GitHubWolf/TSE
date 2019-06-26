using InActionLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSE
{
    public class MuxBitrate
    {
        private bool firstPcrReady = false;
        private Int64 firstPcr = 0;
        private Int64 firstPcrPosition = 0;

        private Int64 secondPcr = 0;
        private Int64 secondPcrPosition = 0;

        private bool bitrateAvailable = false;

        public MuxBitrate(UInt16 pid)
        {
            this.Pid = pid;
        }

        public UInt16 Pid
        {
            get;
            set;
        }

        public bool BitrateAvailable
        {
            get
            {
                return bitrateAvailable;
            }
        }

        public bool FirsPcrReady()
        {
            return firstPcrReady;
        }

        public void SaveFirstPcr(Int64 pcrValue, Int64 positionValue)
        {
            firstPcr = pcrValue;
            firstPcrPosition = positionValue;

            firstPcrReady = true;
        }

        public void SaveSecondPcr(Int64 pcrValue, Int64 positionValue)
        {
            secondPcr = pcrValue;
            secondPcrPosition = positionValue;

            bitrateAvailable = true;//We are done to collect bitrate based on PCR.
        }

        public bool GetBitrate(ref Int64 bitrate)
        {
            bool bitrateReady = false;

            if (bitrateAvailable)
            {
                bitrateReady = true;

                bitrate = (Int64)((secondPcrPosition - firstPcrPosition) * 27000000 *8/ (secondPcr - firstPcr));
            }

            return bitrateReady;
        }
    }
}
