using HKSupply.DB;
using HKSupply.Exceptions;
using HKSupply.General;
using HKSupply.Models;
using HKSupply.Services.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Services.Implementations
{
    /// <summary>
    /// Controlador Entity Framework para Role
    /// </summary>
    public class EFRole : IRole
    {

        ILog _log = LogManager.GetLogger(typeof(EFRole));

        #region Public Methods

        /// <summary>
        /// Obtener la colección de roles del sistema.
        /// </summary>
        /// <param name="all">Para indicar si se quieren todos o sólo los activos</param>
        /// <returns></returns>
        public IEnumerable<Role> GetRoles(bool all = true)
        {
            try
            {
                using (var db = new HKSupplyContext())
                {
                    if (all)
                    {
                        return db.Roles.ToList();
                    }
                    else
                    {
                        return db.Roles.Where(r => r.Enabled.Equals(true)).ToList();
                    }

                }
            }
            catch (SqlException sqlex)
            {
                for (int i = 0; i < sqlex.Errors.Count; i++)
                {
                    _log.Error("Index #" + i + "\n" +
                        "Message: " + sqlex.Errors[i].Message + "\n" +
                        "Error Number: " + sqlex.Errors[i].Number + "\n" +
                        "LineNumber: " + sqlex.Errors[i].LineNumber + "\n" +
                        "Source: " + sqlex.Errors[i].Source + "\n" +
                        "Procedure: " + sqlex.Errors[i].Procedure + "\n");

                    switch (sqlex.Errors[i].Number)
                    {
                        case -1: //connection broken
                        case -2: //timeout
                            throw new DBServerConnectionException(GlobalSetting.ResManager.GetString("DBServerConnectionError"));
                    }
                }
                throw sqlex;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                throw ex;
            }
        }

        /// <summary>
        /// Obtener un rol dado su Id
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
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
                        throw new NonexistentRoleException(GlobalSetting.ResManager.GetString("NoRoleExist"));

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
            catch (SqlException sqlex)
            {
                for (int i = 0; i < sqlex.Errors.Count; i++)
                {
                    _log.Error("Index #" + i + "\n" +
                        "Message: " + sqlex.Errors[i].Message + "\n" +
                        "Error Number: " + sqlex.Errors[i].Number + "\n" +
                        "LineNumber: " + sqlex.Errors[i].LineNumber + "\n" +
                        "Source: " + sqlex.Errors[i].Source + "\n" +
                        "Procedure: " + sqlex.Errors[i].Procedure + "\n");

                    switch (sqlex.Errors[i].Number)
                    {
                        case -1: //connection broken
                        case -2: //timeout
                            throw new DBServerConnectionException(GlobalSetting.ResManager.GetString("DBServerConnectionError"));
                    }
                }
                throw sqlex;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                throw ex;
            }
        }

        /// <summary>
        /// Dar de alta un rol en el sistema
        /// </summary>
        /// <param name="newRole"></param>
        /// <returns></returns>
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
                        throw new NewExistingRoleException(GlobalSetting.ResManager.GetString("RoleAlreadyExist"));
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
                _log.Info(areex.Message, areex);
                throw areex;
            }
            catch (NonexistentRoleException nerex)
            {
                _log.Error(nerex.Message, nerex);
                throw nerex;
            }
            catch (DbEntityValidationException e)
            {
                _log.Error(e.Message, e);
                throw e;
            }
            catch (SqlException sqlex)
            {
                for (int i = 0; i < sqlex.Errors.Count; i++)
                {
                    _log.Error("Index #" + i + "\n" +
                        "Message: " + sqlex.Errors[i].Message + "\n" +
                        "Error Number: " + sqlex.Errors[i].Number + "\n" +
                        "LineNumber: " + sqlex.Errors[i].LineNumber + "\n" +
                        "Source: " + sqlex.Errors[i].Source + "\n" +
                        "Procedure: " + sqlex.Errors[i].Procedure + "\n");

                    switch (sqlex.Errors[i].Number)
                    {
                        case -1: //connection broken
                        case -2: //timeout
                            throw new DBServerConnectionException(GlobalSetting.ResManager.GetString("DBServerConnectionError"));
                    }
                }
                throw sqlex;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                throw ex;
            }
        }

        /// <summary>
        /// Deshabilitar un rol
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="remarks"></param>
        /// <returns></returns>
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
                        throw new NonexistentRoleException(GlobalSetting.ResManager.GetString("NoRoleExist"));

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
            catch (SqlException sqlex)
            {
                for (int i = 0; i < sqlex.Errors.Count; i++)
                {
                    _log.Error("Index #" + i + "\n" +
                        "Message: " + sqlex.Errors[i].Message + "\n" +
                        "Error Number: " + sqlex.Errors[i].Number + "\n" +
                        "LineNumber: " + sqlex.Errors[i].LineNumber + "\n" +
                        "Source: " + sqlex.Errors[i].Source + "\n" +
                        "Procedure: " + sqlex.Errors[i].Procedure + "\n");

                    switch (sqlex.Errors[i].Number)
                    {
                        case -1: //connection broken
                        case -2: //timeout
                            throw new DBServerConnectionException(GlobalSetting.ResManager.GetString("DBServerConnectionError"));
                    }
                }
                throw sqlex;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                throw ex;
            }
        }

        #endregion

        /// <summary>
        /// Modificar una colección de roles
        /// </summary>
        /// <param name="rolesToUpdate"></param>
        /// <returns></returns>
        /// <remarks>
        /// Los campos que se actualizan son los siguientes:
        /// - Description
        /// - Enabled
        /// - Remarks
        /// </remarks>
        public bool UpdateRoles(IEnumerable<Role> rolesToUpdate)
        {
            try
            {
                if (rolesToUpdate == null)
                    throw new ArgumentNullException("rolesToUpdate");

                using (var db = new HKSupplyContext())
                {
                    using (var dbTrans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            foreach (var role in rolesToUpdate)
                            {
                                var roleToUpdate = db.Roles.FirstOrDefault(r => r.RoleId.Equals(role.RoleId));
                                if (roleToUpdate != null)
                                {
                                    roleToUpdate.Description = role.Description;
                                    roleToUpdate.Enabled = role.Enabled;
                                    roleToUpdate.Remarks = role.Remarks;
                                }
                            }

                            db.SaveChanges();
                            dbTrans.Commit();
                            return true;
                        }
                        catch (SqlException sqlex)
                        {
                            dbTrans.Rollback();

                            for (int i = 0; i < sqlex.Errors.Count; i++)
                            {
                                _log.Error("Index #" + i + "\n" +
                                    "Message: " + sqlex.Errors[i].Message + "\n" +
                                    "Error Number: " + sqlex.Errors[i].Number + "\n" +
                                    "LineNumber: " + sqlex.Errors[i].LineNumber + "\n" +
                                    "Source: " + sqlex.Errors[i].Source + "\n" +
                                    "Procedure: " + sqlex.Errors[i].Procedure + "\n");

                                switch (sqlex.Errors[i].Number)
                                {
                                    case -1: //connection broken
                                    case -2: //timeout
                                        throw new DBServerConnectionException(GlobalSetting.ResManager.GetString("DBServerConnectionError"));
                                }
                            }
                            throw sqlex;
                        }
                        catch (DbEntityValidationException e)
                        {
                            dbTrans.Rollback();
                            _log.Error(e.Message, e);
                            throw e;
                        }
                        catch (Exception ex)
                        {
                            dbTrans.Rollback();
                            _log.Error(ex.Message, ex);
                            throw ex;
                        }
                    }
                    
                }

            }
            catch (ArgumentNullException nrex)
            {
                _log.Error(nrex.Message, nrex);
                throw nrex;
            }
            
        }
    }
}
