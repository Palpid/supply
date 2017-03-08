using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Exceptions
{
    /// <summary>
    /// Custom Exception para una contraseña incorrecta
    /// </summary>
    [Serializable()]
    public class InvalidPasswordException : System.Exception
    {
                public InvalidPasswordException() : base() { }
        public InvalidPasswordException(string message) : base(message) { }
        public InvalidPasswordException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an exception propagates from a remoting server to the client. 
        protected InvalidPasswordException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}
