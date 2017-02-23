using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Exceptions
{
    [Serializable()]
    public class NoExisteUsuarioException : System.Exception
    {
        public NoExisteUsuarioException() : base() { }
        public NoExisteUsuarioException(string message) : base(message) { }
        public NoExisteUsuarioException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an exception propagates from a remoting server to the client. 
        protected NoExisteUsuarioException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}
