using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InActionLibrary;

namespace TSE
{
    class ManagerPid : StreamParserSession
    {
        private SortedDictionary<UInt16, PidProfile> pidList = new SortedDictionary<ushort, PidProfile>();
        private Int64 totalPacketCount = 0;

        public ManagerPid(StreamParserContext owner)
            : base(owner)
        {
 
        }

        public void AddPacket(UInt16 pidValue)
        {
            PidProfile pidProfile = null;

            if (pidList.TryGetValue(pidValue, out pidProfile))
            {
                //We have find an existing item in the list.
            }
            else
            {
                //Create a new one.
                pidProfile = new PidProfile();

                //Add it into the list.
                pidList.Add(pidValue, pidProfile);

                //Save the value.
                pidProfile.PID = pidValue;
            }

            pidProfile.PacketCount++;
            totalPacketCount++;
        }

        public void SetPidTypeStandard(UInt16 pidValue, DataType dataType, String description)
        { 
            SetPidType(pidValue, dataType, description);
        }

        public void SetPidTypePlus(UInt16 pidValue, DataType dataType, String description)
        { 
            //Check whether the new PID is a pre-defined PID, if yes, give an error notification.
            Array predefinedPidValues = Enum.GetValues(typeof(TsPID));

            foreach (UInt16 pid in predefinedPidValues)
            {
                if (pid == pidValue)
                {
                    String warningMessage = "Warning: PID " + Utility.GetValueHexString(pid, 16) + ", " + description + " is using predefined PID! Errors may happen!" + Environment.NewLine;
                    GetContext().WriteLog(warningMessage);
                    break;
                }
            }

            SetPidType(pidValue, dataType, description);
        }

        private void SetPidType(UInt16 pidValue, DataType dataType, String description)
        {
            PidProfile pidProfile = null;

            if (pidList.TryGetValue(pidValue, out pidProfile))
            {
                //We have find an existing item in the list.
            }
            else
            {
                //Create a new one.
                pidProfile = new PidProfile();

                //Add it into the list.
                pidList.Add(pidValue, pidProfile);

                //Save the value.
                pidProfile.PID = pidValue;

            }

            pidProfile.Type = dataType;
            pidProfile.Description = description;
        }

        public void CalculatePercentage()
        {
            foreach (KeyValuePair<UInt16, PidProfile> pair in pidList)
            {
                PidProfile pidProfile = pair.Value;

                pidProfile.Percentage = (float)pidProfile.PacketCount * 100 / (float)totalPacketCount;
            }
        }

        public SortedDictionary<UInt16, PidProfile> GetPidList()
        {
            return pidList;
        }
    }
}
