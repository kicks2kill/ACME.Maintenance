﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ACME.Maintenance.Domain.Exceptions
{
    public class ExpiredContractException :Exception, ISerializable
    {
        public ExpiredContractException() : base()
        {

        }

        public ExpiredContractException(string message) : base(message)
        {

        }

        public ExpiredContractException(string message, Exception inner) : base(message, inner)
        {

        }

        protected ExpiredContractException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
