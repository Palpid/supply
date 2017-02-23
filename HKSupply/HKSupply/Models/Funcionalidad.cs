using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Models
{
    public class Funcionalidad
    {
        public string FuncionalidadId { get; set; }
        public bool Lectura { get; set; }
        public bool Escritura { get; set; }
        public IEnumerable<Rol> Roles { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Funcionalidad funcionalidad = (Funcionalidad)obj;

            return (FuncionalidadId == funcionalidad.FuncionalidadId);
        }

        public override int GetHashCode()
        {
            return FuncionalidadId.GetHashCode();
        }
    }
}
