using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Exceptions
{
    [Serializable()]
    class NoExisteFuncionalidadException : System.Exception
    {
        public NoExisteFuncionalidadException() : base() { }
        public NoExisteFuncionalidadException(string message) : base(message) { }
        public NoExisteFuncionalidadException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an exception propagates from a remoting server to the client. 
        protected NoExisteFuncionalidadException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}
