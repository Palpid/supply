using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Exceptions
{
    [Serializable()]
    public class NewExistingRoleException : System.Exception
    {
        public NewExistingRoleException() : base() { }
        public NewExistingRoleException(string message) : base(message) { }
        public NewExistingRoleException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an exception propagates from a remoting server to the client. 
        protected NewExistingRoleException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}
