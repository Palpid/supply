using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Exceptions
{
    [Serializable()]
    public class NonexistentUserException : System.Exception
    {
        public NonexistentUserException() : base() { }
        public NonexistentUserException(string message) : base(message) { }
        public NonexistentUserException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an exception propagates from a remoting server to the client. 
        protected NonexistentUserException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}
