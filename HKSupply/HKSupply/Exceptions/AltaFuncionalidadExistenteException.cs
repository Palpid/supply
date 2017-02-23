using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Exceptions
{
    [Serializable()]
    class AltaFuncionalidadExistenteException : System.Exception
    {
        public AltaFuncionalidadExistenteException() : base() { }
        public AltaFuncionalidadExistenteException(string message) : base(message) { }
        public AltaFuncionalidadExistenteException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an exception propagates from a remoting server to the client. 
        protected AltaFuncionalidadExistenteException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}
