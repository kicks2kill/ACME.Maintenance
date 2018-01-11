﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ACME.Maintenance.Domain.Exceptions
{
   public class PartNotFoundException :Exception, ISerializable
    {
        public PartNotFoundException(): base() { }
        public PartNotFoundException(string message) : base(message) { }
        public PartNotFoundException(string message, Exception inner) : base(message, inner) { }
        public PartNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
