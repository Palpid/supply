using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Exceptions
{
    [Serializable()]
    public class AltaRolExistenteException : System.Exception
    {
        public AltaRolExistenteException() : base() { }
        public AltaRolExistenteException(string message) : base(message) { }
        public AltaRolExistenteException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an exception propagates from a remoting server to the client. 
        protected AltaRolExistenteException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}
