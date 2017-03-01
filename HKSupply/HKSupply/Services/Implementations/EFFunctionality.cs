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


        public Functionality GetFunctionalityById(int functionalityId)
        {
            try
            {
                if (functionalityId == 0)
                    throw new ArgumentNullException("functionalityName");

                using (var db = new HKSupplyContext())
                {

                    var functionality = db.Functionalities
                        .Where (f => f.FunctionalityId.Equals(functionalityId))
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

        public Functionality GetFunctionalityByName(string functionalityName)
        {
            try
            {
                if (functionalityName == null)
                    throw new ArgumentNullException("functionalityName");

                using (var db = new HKSupplyContext())
                {

                    var functionality = db.Functionalities
                        .Where(f => f.FunctionalityName.Equals(functionalityName))
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
                    var functionality = db.Functionalities.FirstOrDefault(f => f.FunctionalityName.Equals(newFunctionality.FunctionalityName));

                    if (functionality != null)
                        throw new NewExistingFunctionalityException();

                    db.Functionalities.Add(newFunctionality);
                    db.SaveChanges();

                    return GetFunctionalityByName(newFunctionality.FunctionalityName);
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
                    var functionality = db.Functionalities.FirstOrDefault(f => f.FunctionalityId.Equals(modFunctionality.FunctionalityId));

                    if (functionality == null)
                        throw new NonexistentFunctionalityException();

                    functionality.Category = modFunctionality.Category;
                    functionality.FormName = modFunctionality.FormName;
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

        /*
        public Functionality GetFunctionalityByNameRol(string functionalityName, string roleId)
        {
            try
            {
                if (functionalityName == null)
                    throw new ArgumentNullException("functionalityName");

                if (roleId == null)
                    throw new ArgumentNullException("roleId");

                using (var db = new HKSupplyContext())
                {

                    //TODO
                    var functionality = db.Functionalities
                        //.Include(f => f.Role)
                        //.Where(f => f.FunctionalityName.Equals(functionalityName) &&
                        //    f.Role.RoleId.Equals(roleId))
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
        */

        /*
        public IEnumerable<Functionality> GetFunctionalitiesByRol(string roleId)
        {
            try
            {
                if (roleId == null)
                    throw new ArgumentNullException("roleId");

                using (var db = new HKSupplyContext())
                {
                    //TODO
                    var roleFunctionalities = db.Functionalities
                        //.Where(f => f.RoleId.Equals(roleId))
                        .ToList();

                    return roleFunctionalities;
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
        */

        /*
        public IEnumerable<string> GetFunctionalitiesCategoriesRole(string roleId)
        {
            try
            {
                if (roleId == null)
                    throw new ArgumentNullException("roleId");

                using (var db = new HKSupplyContext())
                {
                    var categories = db.Functionalities
                        //.Where(f => f.RoleId.Equals(roleId))
                        .Select(f => f.Category)
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
        */

        /*
        public Functionality NewFunctionality(Functionality newFunctionality)
        {
            try
            {
                if (newFunctionality == null)
                    throw new ArgumentNullException();

                using (var db = new HKSupplyContext())
                {
                    //TODO
                    var functionality = db.Functionalities.FirstOrDefault(f => f.FunctionalityName.Equals(newFunctionality.FunctionalityName));
                        //&& f.RoleId.Equals(newFunctionality.RoleId));

                    if (functionality != null)
                        throw new NewExistingFunctionalityException();

                    db.Functionalities.Add(newFunctionality);
                    db.SaveChanges();

                    //TODO
                    return new Functionality();
                    //return GetFunctionalityByNameRol(newFunctionality.FunctionalityName, newFunctionality.RoleId);
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
        */

        /*
        public Functionality ModifyFunctionality(Functionality modFunctionality)
        {
            try
            {
                if (modFunctionality == null)
                    throw new ArgumentNullException();

                using (var db = new HKSupplyContext())
                {
                    //TODO
                    //var functionality = db.Functionalities.FirstOrDefault(f => f.FunctionalityName.Equals(modFunctionality.FunctionalityName)
                    //    && f.Role.RoleId.Equals(modFunctionality.Role.RoleId));

                    //if (functionality == null)
                    //    throw new NonexistentFunctionalityException();

                    //functionality.Read = modFunctionality.Read;
                    //functionality.New = modFunctionality.Read;
                    //functionality.Modify = modFunctionality.Modify;
                    //db.SaveChanges();

                    //return functionality;

                    return new Functionality();
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
        */


    }
}
