using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Exceptions
{
    /// <summary>
    /// Custom Exception: dar de alta una funcionalidad-role ya existente en el sistema
    /// </summary>
    [Serializable()]
    public class NewExistingFunctionalityRoleException : System.Exception
    {
        public NewExistingFunctionalityRoleException() : base() { }
        public NewExistingFunctionalityRoleException(string message) : base(message) { }
        public NewExistingFunctionalityRoleException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an exception propagates from a remoting server to the client. 
        protected NewExistingFunctionalityRoleException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}
