using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKSupply.Models;

namespace HKSupply.Services.Interfaces
{
    interface IRol
    {
        Rol GetRolById(string rolId);
        Rol AltaRol(Rol nuevoRol);
        bool DesactivarRol(string rolId, string observaciones);
    }
}
