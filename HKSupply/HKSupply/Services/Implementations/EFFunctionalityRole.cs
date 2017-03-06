﻿using HKSupply.DB;
using HKSupply.Exceptions;
using HKSupply.Models;
using HKSupply.Services.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HKSupply.Services.Implementations
{
    public class EFFunctionalityRole : IFunctionalityRole
    {
        ILog _log = LogManager.GetLogger(typeof(EFUser));

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
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                throw ex;
            }
        }

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
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                throw ex;
            }
        }

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
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                throw ex;
            }
        }

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
                        throw new NewExistingFunctionalityRoleException();

                    db.FunctionalitiesRole.Add(newFunctionalityRole);
                    db.SaveChanges();

                    return GetFunctionalityRole(newFunctionalityRole.FunctionalityId, newFunctionalityRole.RoleId);
                    //return db.FunctionalitiesRole
                    //    .Where(fr => fr.FunctionalityId.Equals(newFunctionalityRole.FunctionalityId) && fr.RoleId.Equals(newFunctionalityRole.RoleId))
                    //    .FirstOrDefault();
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
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                throw ex;
            }
        }

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
                        throw new NonexistentFunctionalityRoleException();


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
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                throw ex;
            }
        }



        public bool UpdateFunctionalitiesRoles(IEnumerable<FunctionalityRole> functionalitiesRolesToUpdate)
        {
            try
            {
                if (functionalitiesRolesToUpdate == null)
                    throw new ArgumentException("functionalitiesRolesToUpdate");

                using (var db = new HKSupplyContext())
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
                    return true;
                }

            }
            catch (ArgumentNullException nrex)
            {
                _log.Error(nrex.Message, nrex);
                throw nrex;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                throw ex;
            }
        }
    }
}
