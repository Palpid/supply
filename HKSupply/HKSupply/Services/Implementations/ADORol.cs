using HKSupply.Exceptions;
using HKSupply.Helpers.Mocking;
using HKSupply.Models;
using HKSupply.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Services.Implementations
{
    public class ADORol : IRol
    {

        #region Constructor
        public ADORol()
        {
            
        }
        #endregion

        #region Public Methods
        public Rol GetRolById(string rolId)
        {
            try
            {
                if (rolId == null)
                    //throw new ArgumentNullException(nameof(rolId)); //No existe el nameof hasta c# 6 y VS2015
                    throw new ArgumentNullException("rolId");

                var rol = MockData.RolesList.FirstOrDefault(r => r.RolId.Equals(rolId));

                if (rol == null)
                    throw new NoExisteRolException("El rol indicado no existe");

                return rol;

            }
            catch (ArgumentNullException nrex)
            {
                throw nrex;
            }
            catch (NoExisteRolException nerex)
            {
                throw nerex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Rol AltaRol(Models.Rol nuevoRol)
        {
            try
            {
                if (nuevoRol == null)
                    throw new ArgumentNullException("nuevoRol");

                var rol = MockData.RolesList.FirstOrDefault(r => r.RolId.Equals(nuevoRol.RolId));
                if (rol != null)
                    throw new AltaRolExistenteException();

                MockData.RolesList.Add(nuevoRol);

                return GetRolById(nuevoRol.RolId);
            }
            catch (ArgumentNullException nrex)
            {
                throw nrex;
            }
            catch (AltaRolExistenteException areex)
            {
                throw areex;
            }
            catch (NoExisteRolException nerex)
            {
                throw nerex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DesactivarRol(string rolId, string observaciones)
        {
            try
            {
                if (rolId == null)
                    throw new ArgumentNullException("rolId");

                var rol = MockData.RolesList.FirstOrDefault(r => r.RolId.Equals(rolId));

                if (rol == null)
                    throw new NoExisteRolException("El rol indicado no existe");

                if (rol != null)
                {
                    rol.Activo = false;
                    rol.Observaciones = observaciones;
                }

                return true;
            }
            catch (ArgumentNullException nrex)
            {
                throw nrex;
            }
            catch (NoExisteRolException nerex)
            {
                throw nerex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}
