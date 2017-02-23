using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Models
{
    public class Usuario
    {
        public string UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string Password { get; set; }
        public Rol UserRol { get; set; }
        public bool Activo { get; set; }
        public DateTime? LastLogout { get; set; }
        public string Observaciones { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Usuario usuario = (Usuario)obj;

            return (UsuarioId == usuario.UsuarioId);
        }

        public override int GetHashCode()
        {
            return UsuarioId.GetHashCode();
        }
    }
}
