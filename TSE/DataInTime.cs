using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSE
{
    public class DataInTime
    {
        public Int64 milliseconds = 0;
        public byte[] dataBytes = null;

        public DataInTime(byte[] data, Int64 duration)
        {
            dataBytes = data;
            milliseconds = duration;
        }
    }
}
