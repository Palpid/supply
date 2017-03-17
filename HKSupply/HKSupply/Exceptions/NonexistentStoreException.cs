using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Exceptions
{
    /// <summary>
    /// Custom Exception: Almacén no existente en el sistema
    /// </summary>
    [Serializable()]
    public class NonexistentStoreException : System.Exception
    {
        public NonexistentStoreException() : base() { }
        public NonexistentStoreException(string message) : base(message) { }
        public NonexistentStoreException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an exception propagates from a remoting server to the client. 
        protected NonexistentStoreException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}
