using HKSupply.DB;
using HKSupply.Exceptions;
using HKSupply.Models;
using HKSupply.Services.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Services.Implementations
{
    public class EFRole : IRole
    {

        ILog _log = LogManager.GetLogger(typeof(EFRole));

        #region Public Methods

        public IEnumerable<Role> GetAllRoles()
        {
            try
            {
                using (var db = new HKSupplyContext())
                {
                    return db.Roles.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Role GetRoleById(string roleId)
        {
            try
            {
                if (roleId == null)
                    //throw new ArgumentNullException(nameof(rolId)); //No existe el nameof hasta c# 6 y VS2015
                    throw new ArgumentNullException("roleId");

                using (var db = new HKSupplyContext())
                {
                    var role = db.Roles.FirstOrDefault(r => r.RoleId.Equals(roleId));

                    if (role == null)
                        throw new NonexistentRoleException("El rol indicado no existe");

                    return role;
                }

            }
            catch (ArgumentNullException nrex)
            {
                _log.Error(nrex.Message, nrex);
                throw nrex;
            }
            catch (NonexistentRoleException nerex)
            {
                _log.Error(nerex.Message, nerex);
                throw nerex;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public Role NewRole(Role newRole)
        {
            try
            {
                if (newRole == null)
                    throw new ArgumentNullException("newRole");

                using (var db = new HKSupplyContext())
                {
                    var role = db.Roles.FirstOrDefault(r => r.RoleId.Equals(newRole.RoleId));
                    
                    if (role != null)
                        throw new NewExistingRoleException();
                    db.Roles.Add(newRole);
                    db.SaveChanges();

                    return GetRoleById(newRole.RoleId);
                }

            }
            catch (ArgumentNullException nrex)
            {
                _log.Error(nrex.Message, nrex);
                throw nrex;
            }
            catch (NewExistingRoleException areex)
            {
                _log.Error(areex.Message, areex);
                throw areex;
            }
            catch (NonexistentRoleException nerex)
            {
                _log.Error(nerex.Message, nerex);
                throw nerex;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    _log.Error("Entity of type \"" + eve.Entry.Entity.GetType().Name + "\" in state \"" + eve.Entry.State + "\" has the following validation errors:");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        _log.Error("- Property: \"" + ve.PropertyName + "\", Error: \"" + ve.ErrorMessage + "\"");
                    }
                }
                throw e;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public bool DisableRole(string roleId, string remarks)
        {
            try
            {
                if (roleId == null)
                    throw new ArgumentNullException("roleId");

                using (var db = new HKSupplyContext())
                {
                    var role = db.Roles.FirstOrDefault(r => r.RoleId.Equals(roleId));

                    if (role == null)
                        throw new NonexistentRoleException("El rol indicado no existe");

                    if (role != null)
                    {
                        role.Enabled = false;
                        role.Remarks = remarks;
                        db.SaveChanges();
                    }

                    return true;

                }
            }
            catch (ArgumentNullException nrex)
            {
                _log.Error(nrex.Message, nrex);
                throw nrex;
            }
            catch (NonexistentRoleException nerex)
            {
                _log.Error(nerex.Message, nerex);
                throw nerex;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                throw ex;
            }
        }

        #endregion
    }
}
