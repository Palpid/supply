using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Exceptions
{
    [Serializable()]
    public class NewExistingFunctionalityException : System.Exception
    {
        public NewExistingFunctionalityException() : base() { }
        public NewExistingFunctionalityException(string message) : base(message) { }
        public NewExistingFunctionalityException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an exception propagates from a remoting server to the client. 
        protected NewExistingFunctionalityException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}
