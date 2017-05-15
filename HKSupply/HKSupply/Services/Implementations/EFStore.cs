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

namespace HKSupply.Services.Implementations
{
    public class EFStore : IStore
    {
        ILog _log = LogManager.GetLogger(typeof(EFRole));

        public IEnumerable<Models.Store> GetAllStores()
        {
            try
            {
                using (var db = new HKSupplyContext())
                {
                    return db.Stores.ToList();

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

        public Models.Store GetStoreById(string idStore)
        {
            try
            {
                if (idStore == null)
                    throw new ArgumentNullException("idStore");

                using (var db = new HKSupplyContext())
                {
                    var store = db.Stores.FirstOrDefault(s => s.IdStore.Equals(idStore));

                    if (store == null)
                        throw new NonexistentStoreException(GlobalSetting.ResManager.GetString("NoStoreExist"));

                    return store;
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

        public Models.Store NewStore(Models.Store newStore)
        {
            try
            {
                if (newStore == null)
                    throw new ArgumentNullException("newStore");

                using (var db = new HKSupplyContext())
                {
                    var store = db.Stores.FirstOrDefault(s => s.IdStore.Equals(newStore.IdStore));

                    if (store != null)
                        throw new NewExistingStoreException(GlobalSetting.ResManager.GetString("StoreAlreadyExist"));
                    db.Stores.Add(newStore);
                    db.SaveChanges();

                    return GetStoreById(newStore.IdStore);
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

        public bool UpdateStore(IEnumerable<Models.Store> storesToUpdate)
        {
            try
            {
                if (storesToUpdate == null)
                    throw new ArgumentNullException("storesToUpdate");

                using (var db = new HKSupplyContext())
                {
                    using (var dbTrans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            foreach (var store in storesToUpdate)
                            {
                                var storeToUpdate = db.Stores.FirstOrDefault(s => s.IdStore.Equals(store.IdStore));
                                if (storeToUpdate != null)
                                {
                                    storeToUpdate.Name = store.Name;
                                    storeToUpdate.Active = store.Active;
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
