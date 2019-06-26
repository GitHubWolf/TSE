using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSE
{
    class ComponentStream
    {
        private List<CaDescriptor> streamLevelCaDescriptorList = new List<CaDescriptor>();

        public byte StreamType
        {
            get;
            set;
        }

        public UInt16 ElementaryPid
        {
            get;
            set;
        }

        public List<CaDescriptor> GetCaDescriptorList()
        {
            return streamLevelCaDescriptorList;
        }
    }
}
