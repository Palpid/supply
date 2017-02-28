using HKSupply.DB;
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
    public class EFFunctionality : IFunctionality
    {

        ILog _log = LogManager.GetLogger(typeof(EFUser));

        public Functionality GetFunctionalityByIdRol(string functionalityName, string roleId)
        {
            try
            {
                if (functionalityName == null)
                    throw new ArgumentNullException("functionalityName");

                if (roleId == null)
                    throw new ArgumentNullException("roleId");

                using (var db = new HKSupplyContext())
                {

                    //var functionality = db.Functionalities
                    //    .Include("Role")
                    //    .Where(f => f.FunctionalityName.Equals(functionalityName) &&
                    //        f.Role.RoleId.Equals(roleId))
                    //    .FirstOrDefault();

                    var functionality = db.Functionalities
                        .Include(f => f.Role)
                        .Where(f => f.FunctionalityName.Equals(functionalityName) &&
                            f.Role.RoleId.Equals(roleId))
                        .FirstOrDefault();

                    if (functionality == null)
                        throw new NonexistentFunctionalityException();

                    return functionality;
                }

            }
            catch (ArgumentNullException anex)
            {
                _log.Error(anex.Message, anex);
                throw anex;
            }
            catch (NonexistentFunctionalityException nefex)
            {
                _log.Error(nefex.Message, nefex);
                throw nefex;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public Functionality NewFunctionality(Functionality newFunctionality)
        {
            try
            {
                if (newFunctionality == null)
                    throw new ArgumentNullException();

                using (var db = new HKSupplyContext())
                {
                    var functionality = db.Functionalities.FirstOrDefault(f => f.FunctionalityName.Equals(newFunctionality.FunctionalityName)
                        && f.RoleId.Equals(newFunctionality.RoleId));

                    if (functionality != null)
                        throw new NewExistingFunctionalityException();

                    db.Functionalities.Add(newFunctionality);
                    db.SaveChanges();

                    return GetFunctionalityByIdRol(newFunctionality.FunctionalityName, newFunctionality.RoleId);
                }

            }
            catch (ArgumentNullException anex)
            {
                _log.Error(anex.Message, anex);
                throw anex;
            }
            catch (NewExistingFunctionalityException afeex)
            {
                _log.Error(afeex.Message, afeex);
                throw afeex;
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

        public Functionality ModifyFunctionality(Functionality modFunctionality)
        {
            try
            {
                if (modFunctionality == null)
                    throw new ArgumentNullException();

                using (var db = new HKSupplyContext())
                {
                    var functionality = db.Functionalities.FirstOrDefault(f => f.FunctionalityName.Equals(modFunctionality.FunctionalityName)
                        && f.Role.RoleId.Equals(modFunctionality.Role.RoleId));

                    if (functionality == null)
                        throw new NonexistentFunctionalityException();

                    functionality.Read = modFunctionality.Read;
                    functionality.New = modFunctionality.Read;
                    functionality.Modify = modFunctionality.Modify;
                    db.SaveChanges();

                    return functionality;
                }



            }
            catch (ArgumentNullException anex)
            {
                _log.Error(anex.Message, anex);
                throw anex;
            }
            catch (NonexistentFunctionalityException nefex)
            {
                _log.Error(nefex.Message, nefex);
                throw nefex;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                throw ex;
            }
        }
    }
}
