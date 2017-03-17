using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Exceptions
{
    /// <summary>
    /// Custom Exception: dar de alta un almacén ya existente en el sistema
    /// </summary>
    [Serializable()]
    class NewExistingStoreException : System.Exception
    {
        public NewExistingStoreException() : base() { }
        public NewExistingStoreException(string message) : base(message) { }
        public NewExistingStoreException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an exception propagates from a remoting server to the client. 
        protected NewExistingStoreException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}
