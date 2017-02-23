using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Models
{
    public class Rol
    {
        public string RolId { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public string Observaciones { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Rol rol = (Rol)obj;
            return (RolId == rol.RolId);
            
        }

        public override int GetHashCode()
        {
            return RolId.GetHashCode();
        }
    }
}
