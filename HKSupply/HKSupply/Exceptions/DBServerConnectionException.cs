using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Exceptions
{
    /// <summary>
    /// Custom Exception para los errores de conexión con la base de datos
    /// </summary>
    [Serializable()]
    public class DBServerConnectionException : System.Exception
    {
        public DBServerConnectionException() : base() { }
        public DBServerConnectionException(string message) : base(message) { }
        public DBServerConnectionException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an exception propagates from a remoting server to the client. 
        protected DBServerConnectionException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}
