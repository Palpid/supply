using HKSupply.DB;
using HKSupply.Exceptions;
using HKSupply.General;
using HKSupply.Services.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKSupply.Models;

namespace HKSupply.Services.Implementations
{
    public class EFLayout : ILayout
    {
        ILog _log = LogManager.GetLogger(typeof(EFLayout));

        public IEnumerable<Layout> GetLayouts(int functionalityId, string user)
        {
            try
            {
                if (functionalityId == 0)
                    throw new ArgumentNullException(nameof(functionalityId));
                if (string.IsNullOrEmpty(user))
                    throw new ArgumentNullException(nameof(user));

                using (var db = new HKSupplyContext())
                {
                    return db.Layouts
                        .Where(a => a.FunctionalityId.Equals(functionalityId) && a.UserLogin.Equals(user))
                        .OrderBy(a => a.LayoutName)
                        .ToList();
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

        public IEnumerable<Layout> GetLayouts(int functionalityId, string objectName, string user)
        {
            try
            {
                if (functionalityId == 0)
                    throw new ArgumentNullException(nameof(functionalityId));
                if (string.IsNullOrEmpty(objectName))
                    throw new ArgumentNullException(nameof(objectName));
                if (string.IsNullOrEmpty(user))
                    throw new ArgumentNullException(nameof(user));

                using (var db = new HKSupplyContext())
                {
                    return db.Layouts
                        .Where(a => a.FunctionalityId.Equals(functionalityId) && a.ObjectName.Equals(objectName) && a.UserLogin.Equals(user))
                        .OrderBy(a => a.LayoutName)
                        .ToList();
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

        public bool SaveLayout(Layout newLayout)
        {
            try
            {
                if (newLayout == null)
                    throw new ArgumentNullException(nameof(newLayout));

                using (var db = new HKSupplyContext())
                {
                    db.Layouts.Add(newLayout);
                    db.SaveChanges();

                    return true;
                }
            }
            catch (ArgumentNullException nrex)
            {
                _log.Error(nrex.Message, nrex);
                throw nrex;
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

        public bool SaveLayout(List<Layout> layouts)
        {
            try
            {
                if (layouts == null)
                    throw new ArgumentNullException(nameof(layouts));

                using (var db = new HKSupplyContext())
                {
                    using (var dbTrans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            foreach (var layout in layouts)
                            {
                                db.Layouts.Add(layout);
                                db.SaveChanges();
                            }

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

        public bool UpdateLayout(Layout updateLayout)
        {
            try
            {
                if (updateLayout == null)
                    throw new ArgumentNullException(nameof(updateLayout));

                using (var db = new HKSupplyContext())
                {
                    using (var dbTrans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            //Buscamos el item por id o por functionality, user, object name y layout name
                            Layout layoutToUpdate;
                            if (updateLayout.IdLayout != 0)
                            {
                                layoutToUpdate = db.Layouts.Where(a => a.IdLayout.Equals(updateLayout.IdLayout)).FirstOrDefault();
                            }
                            else
                            {
                                layoutToUpdate = db.Layouts
                                    .Where(
                                    a => a.FunctionalityId.Equals(updateLayout.FunctionalityId) 
                                    && a.UserLogin.Equals(updateLayout.UserLogin)
                                    && a.ObjectName.Equals(updateLayout.ObjectName)
                                    && a.LayoutName.Equals(updateLayout.LayoutName)
                                    ).FirstOrDefault();
                            }

                            if (layoutToUpdate == null)
                                return false;

                            layoutToUpdate.LayoutString = updateLayout.LayoutString; 
                            db.SaveChanges();
                            dbTrans.Commit();
                            return true;
                        }
                        catch (DbEntityValidationException e)
                        {
                            dbTrans.Rollback();
                            _log.Error(e.Message, e);
                            throw e;
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
                        catch (Exception ex)
                        {
                            dbTrans.Rollback();
                            _log.Error(ex.Message, ex);
                            throw ex;
                        }
                    }
                }
            }
            catch (ArgumentNullException anex)
            {
                _log.Error(anex.Message, anex);
                throw anex;
            }
        }
    }
}
