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
    /// Controlador Entity Framework para Functionality
    /// </summary>
    public class EFFunctionality : IFunctionality
    {

        ILog _log = LogManager.GetLogger(typeof(EFUser));

        /// <summary>
        /// Obtener todas las funcionalidades de la base de datos
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Functionality> GetAllFunctionalities()
        {
            try
            {
                using (var db = new HKSupplyContext())
                {
                    return db.Functionalities.ToList();
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
        /// Obtener una funcionalidad a través de un Id
        /// </summary>
        /// <param name="functionalityId"></param>
        /// <returns></returns>
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
                        throw new NonexistentFunctionalityException(GlobalSetting.ResManager.GetString("NoFunctionalityExist"));

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
        /// Obtener una funcionalidad por su nombre
        /// </summary>
        /// <param name="functionalityName"></param>
        /// <returns></returns>
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
                        throw new NonexistentFunctionalityException(GlobalSetting.ResManager.GetString("NoFunctionalityExist"));

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
        /// Dar de alta una funcionalidad
        /// </summary>
        /// <param name="newFunctionality"></param>
        /// <returns></returns>
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
                        throw new NewExistingFunctionalityException(GlobalSetting.ResManager.GetString("FunctionalityAlreadyExist"));

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
        /// Modificar una funcionalidad
        /// </summary>
        /// <param name="modFunctionality"></param>
        /// <returns>Objeto Functionality con la funcioliadad modificada</returns>
        /// <remarks>
        /// Modifica los campos:
        /// - Category
        /// - FormName
        /// </remarks>
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
                        throw new NonexistentFunctionalityException(GlobalSetting.ResManager.GetString("NoFunctionalityExist"));

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
        /// Modificar un enumerable de funcionalidades
        /// </summary>
        /// <param name="functionalitiesToUpdate"></param>
        /// <returns>bool</returns>
        public bool UpdateFunctionalities(IEnumerable<Functionality> functionalitiesToUpdate)
        {
            try
            {
                if (functionalitiesToUpdate == null)
                    throw new ArgumentException("functionalitiesToUpdate");

                using (var db = new HKSupplyContext())
                {
                    foreach (var func in functionalitiesToUpdate)
                    {
                        var functionalityToUpdate = db.Functionalities.FirstOrDefault(f => f.FunctionalityId.Equals(func.FunctionalityId));
                        if (functionalityToUpdate != null)
                        {
                            functionalityToUpdate.FunctionalityName = func.FunctionalityName;
                            functionalityToUpdate.Category = func.Category;
                            functionalityToUpdate.FormName = func.FormName;
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

    }
}
