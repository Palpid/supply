using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Exceptions
{
    /// <summary>
    /// Custom Exception: Role no existente en el sistema
    /// </summary>
    [Serializable()]
    public class NonexistentRoleException : System.Exception
    {
        public NonexistentRoleException() : base() { }
        public NonexistentRoleException(string message) : base(message) { }
        public NonexistentRoleException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an exception propagates from a remoting server to the client. 
        protected NonexistentRoleException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}
