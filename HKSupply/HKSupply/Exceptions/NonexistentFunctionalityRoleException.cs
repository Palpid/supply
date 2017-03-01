using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Exceptions
{
    [Serializable()]
    public class NonexistentFunctionalityRoleException : System.Exception
    {
        public NonexistentFunctionalityRoleException() : base() { }
        public NonexistentFunctionalityRoleException(string message) : base(message) { }
        public NonexistentFunctionalityRoleException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an exception propagates from a remoting server to the client. 
        protected NonexistentFunctionalityRoleException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}
