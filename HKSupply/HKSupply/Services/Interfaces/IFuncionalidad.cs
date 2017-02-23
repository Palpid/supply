using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKSupply.Models;

namespace HKSupply.Services.Interfaces
{
    interface IFuncionalidad
    {
        Funcionalidad GetFuncionalidadById(string funcionalidadId);
        Funcionalidad AltaFuncionalidad(Funcionalidad nuevaFuncionalidad);
        Funcionalidad ModificarFuncionalidad(Funcionalidad modFuncionalidad);
    }
}
