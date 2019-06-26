using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSE
{
    public class StreamParserSession
    {
        protected StreamParserContext owner;
        
        public StreamParserSession(StreamParserContext owner)
        {
            this.owner = owner;
        }

        public StreamParserContext GetContext()
        {
            return owner;
        }
    }
}
