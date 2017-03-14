using HKSupply.DB;
using HKSupply.Exceptions;
using HKSupply.General;
using HKSupply.Models;
using HKSupply.Services.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HKSupply.Services.Implementations
{
    /// <summary>
    /// Controlador Entity Framework para Functionality Role
    /// </summary>
    public class EFFunctionalityRole : IFunctionalityRole
    {
        ILog _log = LogManager.GetLogger(typeof(EFUser));

        /// <summary>
        /// Obtener todas las funcionalidades-roles de la base de datos
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FunctionalityRole> GetAllFunctionalitiesRole()
        {
            try
            {
                using (var db = new HKSupplyContext())
                {
                    var functionalitiesList = db.FunctionalitiesRole
                        .Include(r => r.Role)
                        .Include(f => f.Functionality)
                        .ToList();

                    return functionalitiesList;

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
        /// Obtener un FunctionalityRole dado una funciolidad y un rol
        /// </summary>
        /// <param name="functionalityId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public FunctionalityRole GetFunctionalityRole(int functionalityId, string roleId)
        {
            try
            {
                if (functionalityId == 0)
                    throw new ArgumentNullException("functionalityId");

                if (roleId == null)
                    throw new ArgumentNullException("roleId");

                using (var db = new HKSupplyContext())
                {
                    var functionalityRole = db.FunctionalitiesRole
                        .Include(r => r.Role)
                        .Include(f => f.Functionality)
                        .Where(fr => fr.FunctionalityId.Equals(functionalityId) && fr.RoleId.Equals(roleId) && fr.Role.Enabled.Equals(true))
                        .FirstOrDefault();

                    if (functionalityRole == null)
                        throw new NonexistentFunctionalityRoleException();

                    return functionalityRole;
                }

            }
            catch (ArgumentNullException anex)
            {
                _log.Error(anex.Message, anex);
                throw anex;
            }
            catch (NonexistentFunctionalityRoleException nfrex)
            {
                _log.Error(nfrex.Message, nfrex);
                throw nfrex;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public IEnumerable<FunctionalityRole> GetFunctionalitiesRole(string roleId)
        {
            try
            {
                if (roleId == null)
                    throw new ArgumentNullException("roleId");

                using (var db = new HKSupplyContext())
                {
                    var functionalitiesList = db.FunctionalitiesRole
                        .Include(r => r.Role)
                        .Include(f => f.Functionality)
                        .Where(fr => fr.RoleId.Equals(roleId) && fr.Role.Enabled.Equals(true))
                        .ToList();

                    return functionalitiesList;

                }
            }
            catch (ArgumentNullException anex)
            {
                _log.Error(anex.Message, anex);
                throw anex;
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
        /// Obtener la colección de todos las cartegorías de un rol en concreto
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IEnumerable<string> GetFunctionalitiesCategoriesRole(string roleId)
        {
            try
            {
                if (roleId == null)
                    throw new ArgumentNullException("roleId");

                using (var db = new HKSupplyContext())
                {
                    var categories = db.FunctionalitiesRole
                        .Join(
                            db.Functionalities,
                            fr => fr.FunctionalityId,
                            f => f.FunctionalityId,
                            (fr, f) => new { FunctionalitiesRole = fr, Functionalities = f })
                        .Select(f => f.Functionalities.Category)
                        .Distinct()
                        .ToList();

                    return categories;
                }

            }
            catch (ArgumentNullException anex)
            {
                _log.Error(anex.Message, anex);
                throw anex;
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
        /// Dar de alta un FunctionalityRole
        /// </summary>
        /// <param name="newFunctionalityRole"></param>
        /// <returns></returns>
        public FunctionalityRole NewFunctionalityRole(FunctionalityRole newFunctionalityRole)
        {
            try
            {
                if (newFunctionalityRole == null)
                    throw new ArgumentNullException("newFunctionalityRole");

                using (var db = new HKSupplyContext())
                {
                    var functionalityRole = db.FunctionalitiesRole
                        .Where(fr => fr.FunctionalityId.Equals(newFunctionalityRole.FunctionalityId) && fr.RoleId.Equals(newFunctionalityRole.RoleId))
                        .FirstOrDefault();

                    if (functionalityRole != null)
                        throw new NewExistingFunctionalityRoleException(GlobalSetting.ResManager.GetString("FunctionalityRoleAlreadyExist"));

                    db.FunctionalitiesRole.Add(newFunctionalityRole);
                    db.SaveChanges();

                    return GetFunctionalityRole(newFunctionalityRole.FunctionalityId, newFunctionalityRole.RoleId);
                }
            }
            catch (ArgumentNullException anex)
            {
                _log.Error(anex.Message, anex);
                throw anex;
            }
            catch (NewExistingFunctionalityRoleException nefre)
            {
                _log.Error(nefre.Message, nefre);
                throw nefre;
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
        /// Modificar un FunctionalityRole
        /// </summary>
        /// <param name="modFunctionalityRole"></param>
        /// <returns></returns>
        /// <remarks>
        /// Los campos que se actualizan son los siguientes:
        /// - Read
        /// - New
        /// - Modify
        /// </remarks>
        public FunctionalityRole ModifyFunctionalityRole(FunctionalityRole modFunctionalityRole)
        {
            try
            {
                if (modFunctionalityRole == null)
                    throw new ArgumentNullException("modFunctionalityRole");

                using (var db = new HKSupplyContext())
                {
                    var functionalityRole = db.FunctionalitiesRole
                        .Where(fr => fr.FunctionalityId.Equals(modFunctionalityRole.FunctionalityId) && fr.RoleId.Equals(modFunctionalityRole.RoleId))
                        .FirstOrDefault();

                    if (functionalityRole == null)
                        throw new NonexistentFunctionalityRoleException(GlobalSetting.ResManager.GetString("NoFunctionalityRoleExist"));


                    functionalityRole.Read = modFunctionalityRole.Read;
                    functionalityRole.New = modFunctionalityRole.New;
                    functionalityRole.Modify = modFunctionalityRole.Modify;
                    db.SaveChanges();

                    return GetFunctionalityRole(modFunctionalityRole.FunctionalityId, modFunctionalityRole.RoleId);

                    //return db.FunctionalitiesRole
                    //    .Where(fr => fr.FunctionalityId.Equals(modFunctionalityRole.FunctionalityId) && fr.RoleId.Equals(modFunctionalityRole.RoleId))
                    //    .FirstOrDefault();
                }
            }
            catch (ArgumentNullException anex)
            {
                _log.Error(anex.Message, anex);
                throw anex;
            }
            catch (NonexistentFunctionalityRoleException nfre)
            {
                _log.Error(nfre.Message, nfre);
                throw nfre;
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
            catch (DbEntityValidationException e)
            {
                _log.Error(e.Message, e);
                throw e;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                throw ex;
            }
        }


        /// <summary>
        /// Modificar una colección de FunctionalityRole
        /// </summary>
        /// <param name="functionalitiesRolesToUpdate"></param>
        /// <returns></returns>
        public bool UpdateFunctionalitiesRoles(IEnumerable<FunctionalityRole> functionalitiesRolesToUpdate)
        {
            try
            {
                if (functionalitiesRolesToUpdate == null)
                    throw new ArgumentNullException("functionalitiesRolesToUpdate");

                using (var db = new HKSupplyContext())
                {
                    using (var dbTran = db.Database.BeginTransaction())
                    {
                        try
                        {
                            foreach (var funcRole in functionalitiesRolesToUpdate)
                            {
                                var funcRoleToUpdate = db.FunctionalitiesRole.FirstOrDefault(fr => fr.FunctionalityId.Equals(funcRole.FunctionalityId) &&
                                    fr.RoleId.Equals(funcRole.RoleId));

                                if (funcRoleToUpdate != null)
                                {
                                    funcRoleToUpdate.Read = funcRole.Read;
                                    funcRoleToUpdate.New = funcRole.New;
                                    funcRoleToUpdate.Modify = funcRole.Modify;
                                }
                            }

                            db.SaveChanges();
                            dbTran.Commit();
                            return true;
                        }
                        catch (SqlException sqlex)
                        {
                            dbTran.Rollback();

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
                            dbTran.Rollback();
                            _log.Error(e.Message, e);
                            throw e;
                        }
                        catch (Exception ex)
                        {
                            dbTran.Rollback();
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
