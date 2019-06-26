﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InActionLibrary;

namespace TSE
{
    class MessageNotification
    {
        public MessageNotification(MessageId messageId, object messagePayload)
        {
            ID = messageId;
            Payload = messagePayload;
        }
        public MessageId ID
        {
            get;
            set;
        }

        public object Payload
        {
            get;
            set;
        }
    }
}
