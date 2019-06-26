using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InActionLibrary;

namespace TSE
{
    public class PidUpdate
    {
        private PidProfile oldPid = null;
        private UInt16 newPid = 0;
        private bool enforceNewPid = false;

        public PidUpdate(PidProfile oldPidInfo, UInt16 newPidValue, bool enforceNewPid)
        {
            this.oldPid = oldPidInfo;
            this.newPid = newPidValue;
            this.enforceNewPid = enforceNewPid;
        }

        public override string ToString()
        {
            if (enforceNewPid)
            {
                return "Old PID: " + Utility.GetValueHexString(oldPid.PID, 16) + " -----To-----> New PID:" + Utility.GetValueHexString(newPid, 16);
            }
            else
            {
                return oldPid.ToString();
            }

        }

        public PidProfile GetOldPidProfile()
        {
            return oldPid;
        }

        //PID to be updated or to be descrambled.
        public UInt16 OldPid
        {
            get
            {
                return oldPid.PID;
            }
        }

        //PID to be updated to or to be descrambled.
        public UInt16 NewPid
        {
            get
            {
                return newPid;
            }
        }
    }
}
