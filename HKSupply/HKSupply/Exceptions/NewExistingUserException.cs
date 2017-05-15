using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Exceptions
{
    /// <summary>
    /// Custom Exception: dar de alta un usuario ya existente en el sistema
    /// </summary>
    [Serializable()]
    public class NewExistingUserException : System.Exception
    {
        public NewExistingUserException() : base() { }
        public NewExistingUserException(string message) : base(message) { }
        public NewExistingUserException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an exception propagates from a remoting server to the client. 
        protected NewExistingUserException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}
