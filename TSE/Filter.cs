using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InActionLibrary;

namespace TSE
{
    public class Filter : StreamParserSession
    {
        private UInt16 pid;//TS Packet ID.
        private DataType dataType;


        //The following info is valid only for section filter.
        private Int64 filterDepth;
        private byte[] mask;
        private byte[] match;

        public Filter(StreamParserContext owner, FilterDataCallback filterDataCallback, DataType dataType, UInt16 pid, Int64 filterDepth, byte[] mask, byte[] match)
            : base(owner)
        {
            this.DataCallback = filterDataCallback;
            SetChannelCondition(dataType, pid);
            SetFilterCondition(filterDepth, mask, match);//Additional filter conditions for section filter.
        }

        public Filter(StreamParserContext owner, FilterDataCallback filterDataCallback, DataType dataType, UInt16 pid)
            : base(owner)
        {
            this.DataCallback = filterDataCallback;
            SetChannelCondition(dataType, pid);
        }

        private void SetChannelCondition(DataType dataType, UInt16 pid)
        {
            this.pid = pid;
            this.dataType = dataType;
        }

        private void SetFilterCondition(Int64 filterDepth, byte[] mask, byte[] match)
        {
            this.filterDepth = filterDepth;
            this.mask = mask;
            this.match = match;
        }

        //Match the data against the filter condition. In case it is matched, return SUCCESS in case matched.
        public bool Match(UInt16 pid, byte[] data, Int64 dataOffset, Int64 dataLength)
        {
            Result result = new Result();
            bool matched = true;
            if (result.Fine)
            {
                if (this.pid != PidProfile.SUPER_PID)
                {
                    if (pid != this.pid)
                    {
                        result.SetResult(ResultCode.PID_MISMATCH);
                    }
                }
            }

            if (result.Fine)
            {
                if (DataType.SECTION == dataType)
                {
                    if (filterDepth > dataLength)
                    {
                        result.SetResult(ResultCode.INSUFFICIENT_DATA);
                    }

                    if (result.Fine)
                    {
                        for (Int64 i = 0; i < filterDepth; ++i)
                        {
                            //Compare each byte.
                            if ((data[dataOffset + i] & mask[i]) != (match[i] & mask[i]))
                            {
                                result.SetResult(ResultCode.DATA_MISMATCH);
                                break;
                            }
                        }
                    } 
                }
            }

            if (!result.Fine)
            {
                matched = false;
            }
            return matched;
        }

        public bool PidIsSame(UInt16 pidToCheck)
        {
            bool result = false;
            
            //Special check to SUPER_PID.
            if (this.pid == PidProfile.SUPER_PID)
            {
                result = true;
            }
            else
            {
                if (pidToCheck == pid)
                {
                    result = true;
                } 
            }

            return result;
        }

        public DataType GetFilterType()
        {
            return dataType;
        }

        //Callback function invoked when a section/PES/ts packet is received.
        public FilterDataCallback DataCallback
        {
            get;
            set;
        }

    }
}
