using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mpeg4VideoPesPacketParser
{
    class Mpeg4VideoInfo
    {
        public Int64 primaryPicType
        {
            get;
            set;
        }

        public Int64 profileIdc
        {
            get;
            set;
        }

        public Int64 level_idc
        {
            get;
            set;
        }
    }
}
