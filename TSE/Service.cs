using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSE
{
    class Service
    {
        public List<CaDescriptor> programLevelCaDescriptorList = new List<CaDescriptor>();
        public List<ComponentStream> componentStreamList = new List<ComponentStream>();

        public UInt16 ServiceId
        {
            get;set;
        }

        public UInt16 PmtPid
        {
            get;set;
        }

        public UInt16 PcrPid
        {
            get;set;
        }

        public void AddProgramLevelCaDescriptor(CaDescriptor caDescriptor)
        {
            programLevelCaDescriptorList.Add(caDescriptor);
        }

        public List<CaDescriptor> GetCaDescriptorList()
        {
            return programLevelCaDescriptorList;
        }

        public List<ComponentStream> GetComponentStreamList()
        {
            return componentStreamList;
        }

        public void AddComponentStream(ComponentStream componentStream)
        {
            componentStreamList.Add(componentStream);
        }

    }
}
