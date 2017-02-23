using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKSupply.Services.Interfaces;
using HKSupply.Helpers.Mocking;
using HKSupply.Exceptions;

namespace HKSupply.Services.Implementations
{
    class ADOFuncionalidad : IFuncionalidad
    {
        public Models.Funcionalidad GetFuncionalidadById(string funcionalidadId)
        {
            try
            {
                if (funcionalidadId == null)
                    throw new ArgumentNullException();

                var funcionalidad = MockData.FuncionalidadesList.FirstOrDefault(f => f.FuncionalidadId.Equals(funcionalidadId));

                if (funcionalidad == null)
                    throw new NoExisteFuncionalidadException();

                return funcionalidad;

            }
            catch (ArgumentNullException anex)
            {
                throw anex;
            }
            catch (NoExisteFuncionalidadException nefex)
            {
                throw nefex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Models.Funcionalidad AltaFuncionalidad(Models.Funcionalidad nuevaFuncionalidad)
        {
            throw new NotImplementedException();
        }
        
        public Models.Funcionalidad ModificarFuncionalidad(Models.Funcionalidad modFuncionalidad)
        {
            throw new NotImplementedException();
        }

    }
}
