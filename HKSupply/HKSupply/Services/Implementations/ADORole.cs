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
    public class ADORole : IRole
    {

        #region Constructor
        public ADORole()
        {
            
        }
        #endregion

        #region Public Methods
        public Role GetRoleById(string roleId)
        {
            try
            {
                if (roleId == null)
                    //throw new ArgumentNullException(nameof(rolId)); //No existe el nameof hasta c# 6 y VS2015
                    throw new ArgumentNullException("roleId");

                var rol = MockData.RolesList.FirstOrDefault(r => r.RoleId.Equals(roleId));

                if (rol == null)
                    throw new NonexistentRoleException("El rol indicado no existe");

                return rol;

            }
            catch (ArgumentNullException nrex)
            {
                throw nrex;
            }
            catch (NonexistentRoleException nerex)
            {
                throw nerex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Role NewRole(Role newRole)
        {
            try
            {
                if (newRole == null)
                    throw new ArgumentNullException("nuevoRol");

                var rol = MockData.RolesList.FirstOrDefault(r => r.RoleId.Equals(newRole.RoleId));
                if (rol != null)
                    throw new NewExistingRoleException();

                MockData.RolesList.Add(newRole);

                return GetRoleById(newRole.RoleId);
            }
            catch (ArgumentNullException nrex)
            {
                throw nrex;
            }
            catch (NewExistingRoleException areex)
            {
                throw areex;
            }
            catch (NonexistentRoleException nerex)
            {
                throw nerex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DisableRole(string roleId, string remarks)
        {
            try
            {
                if (roleId == null)
                    throw new ArgumentNullException("roleId");

                var rol = MockData.RolesList.FirstOrDefault(r => r.RoleId.Equals(roleId));

                if (rol == null)
                    throw new NonexistentRoleException("El rol indicado no existe");

                if (rol != null)
                {
                    rol.Enabled = false;
                    rol.Remarks = remarks;
                }

                return true;
            }
            catch (ArgumentNullException nrex)
            {
                throw nrex;
            }
            catch (NonexistentRoleException nerex)
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
