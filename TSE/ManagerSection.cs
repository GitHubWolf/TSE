using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InActionLibrary;

namespace TSE
{
    //In order not to flush the form by all kinds of duplicate sections, we use SectionCounter as a "filter". Duplicate sections will not be posted to the form for displaying.
    class ManagerSection: StreamParserSession
    {
        private Dictionary<UInt16, List<DataStore>> channelSectionList = new Dictionary<ushort,List<DataStore>>();

        public ManagerSection(StreamParserContext owner)
            : base(owner)
        {
 
        }

        //The return fieldValue will be used to indicate whether the section is a new one or an existing one. If new, return fieldValue will be true, else false.
        public bool AddSection(UInt16 pid, DataStore newSection)
        {
            bool isNew = true;
            List<DataStore> sectionListForPid = null;

            if (channelSectionList.TryGetValue(pid, out sectionListForPid))
            {
                foreach (DataStore section in sectionListForPid)
                {
                    if (section.Compare(newSection))
                    {
                        isNew = false;
                        break;
                    }
                }
            }
            else
            {
                sectionListForPid = new List<DataStore>();

                //We don't have the PID in the channelSectionList. Create a new one.
                channelSectionList.Add(pid, sectionListForPid);
            }

            //If it is a new one, insert it.
            if (isNew)
            {
                sectionListForPid.Add(newSection);
            }

            return isNew;
 
        }
    }
}
