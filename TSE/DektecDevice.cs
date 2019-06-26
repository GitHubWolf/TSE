using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTAPINET;

namespace TSE
{
    public class DektecDevice
    {
        public DtHwFuncDesc dtHwFuncDesc;

        public DektecDevice(DtHwFuncDesc dtHwFuncDesc)
        {
            this.dtHwFuncDesc = dtHwFuncDesc;
        }

        public override string ToString()
        {
            string deviceDescription = null;

            //Get the device desciption.
            DtGlobal.DtapiDtHwFuncDesc2String(dtHwFuncDesc, DTAPI.HWF2STR_TYPE_AND_LOC, ref deviceDescription, 256);

            return deviceDescription;
        }
    }

}
