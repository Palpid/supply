using HKSupply.DB;
using HKSupply.Exceptions;
using HKSupply.General;
using HKSupply.Helpers;
using HKSupply.Models;
using HKSupply.Services.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Services.Implementations
{
    public class EFItemMt : IItemMt
    {
        ILog _log = LogManager.GetLogger(typeof(EFItemHw));

        public ItemMt GetItem(string idItemBcn)
        {
            try
            {
                if (idItemBcn == null)
                    throw new ArgumentNullException("idItemBcn");

                using (var db = new HKSupplyContext())
                {
                    var item = db.ItemsMt
                        .Where(i => i.IdItemBcn.Equals(idItemBcn))
                        .Include(i => i.Model)
                        .Include(i => i.Prototype)
                        .Include(i => i.FamilyHK)
                        .Include(i => i.StatusCial)
                        .Include(i => i.StatusProd)
                        .SingleOrDefault();
                    return item;
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

        public List<ItemMtHistory> GetItemMtHistory(string idItemBcn)
        {
            if (idItemBcn == null)
                throw new ArgumentNullException(nameof(idItemBcn));

            try
            {
                using (var db = new HKSupplyContext())
                {
                    return db.ItemsMtHistory
                        .Where(a => a.IdItemBcn.Equals(idItemBcn))
                        .OrderBy(b => b.Timestamp)
                        .ToList();
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

        public List<ItemMt> GetItems()
        {
            try
            {
                using (var db = new HKSupplyContext())
                {
                    return db.ItemsMt
                        .Include(i => i.Prototype)
                        .Include(i => i.Model)
                        .Include(i => i.FamilyHK)
                        .Include(i => i.StatusCial)
                        .Include(i => i.StatusProd)
                        .ToList();
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

        public bool UpdateItem(ItemMt updateItem, bool newVersion = false)
        {
            try
            {
                if (updateItem == null)
                    throw new ArgumentNullException("updateItem");

                using (var db = new HKSupplyContext())
                {
                    using (var dbTrans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            ItemMt itemToUpdate = GetItem(updateItem.IdItemBcn);

                            if (itemToUpdate == null)
                                return false;

                            //Hay que agregarlo al contexto actual para que lo actualice
                            db.ItemsMt.Attach(itemToUpdate);

                            itemToUpdate.IdSubVer += 1;
                            if (newVersion == true)
                            {
                                itemToUpdate.IdVer += 1;
                                itemToUpdate.IdSubVer = 0;
                            }
                            itemToUpdate.Timestamp = DateTime.Now;

                            //no hacemos  un "entry" en el contexto de db y lo marcamos como modificado, sino que updatamos sólo los pocos campos
                            //que se pueden modificar de un item desde la aplicación para que EF genere el update sólo de esos campos y no de todos
                            itemToUpdate.IdDefaultSupplier = updateItem.IdDefaultSupplier;
                            itemToUpdate.IdFamilyHK = updateItem.IdFamilyHK;
                            itemToUpdate.IdStatusProd = updateItem.IdStatusProd;
                            itemToUpdate.IdUserAttri1 = updateItem.IdUserAttri1;
                            itemToUpdate.IdUserAttri2 = updateItem.IdUserAttri2;
                            itemToUpdate.IdUserAttri3 = updateItem.IdUserAttri3;
                            itemToUpdate.PhotoUrl = updateItem.PhotoUrl;

                            ItemMtHistory itemHistory = (ItemMtHistory)itemToUpdate;
                            itemHistory.User = GlobalSetting.LoggedUser.UserLogin;

                            db.ItemsMtHistory.Add(itemHistory);
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

        public bool UpdateItems(IEnumerable<ItemMt> itemsToUpdate)
        {
            try
            {
                if (itemsToUpdate == null)
                    throw new ArgumentNullException(nameof(itemsToUpdate));

                using (var db = new HKSupplyContext())
                {
                    using (var dbTrans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            foreach (var item in itemsToUpdate)
                            {
                                ItemMt itemToUpdate = db.ItemsMt.Where(a => a.IdItemBcn.Equals(item.IdItemBcn)).FirstOrDefault();

                                itemToUpdate.IdSubVer += 1;
                                itemToUpdate.Timestamp = DateTime.Now;
                                //no hacemos  un "entry" en el contexto de db y lo marcamos como modificado, sino que updatamos sólo los pocos campos
                                //que se pueden modificar de un item desde la aplicación para que EF genere el update sólo de esos campos y no de todos
                                itemToUpdate.IdDefaultSupplier = item.IdDefaultSupplier;
                                itemToUpdate.IdFamilyHK = item.IdFamilyHK;
                                itemToUpdate.IdStatusProd = item.IdStatusProd;
                                itemToUpdate.IdUserAttri1 = item.IdUserAttri1;
                                itemToUpdate.IdUserAttri2 = item.IdUserAttri2;
                                itemToUpdate.IdUserAttri3 = item.IdUserAttri3;

                                ItemMtHistory itemHistory = (ItemMtHistory)itemToUpdate;
                                itemHistory.User = GlobalSetting.LoggedUser.UserLogin;

                                db.ItemsMtHistory.Add(itemHistory);
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

        public bool UpdateItemWithDoc(ItemMt updateItem, ItemDoc itemDoc, bool newVersion = false)
        {
            try
            {
                if (updateItem == null)
                    throw new ArgumentNullException(nameof(updateItem));

                if (itemDoc == null)
                    throw new ArgumentNullException(nameof(itemDoc));

                using (var db = new HKSupplyContext())
                {
                    using (var dbTrans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            ItemMt itemToUpdate = GetItem(updateItem.IdItemBcn);

                            if (itemToUpdate == null)
                                return false;

                            //Hay que agregarlo al contexto actual para que lo actualice
                            db.ItemsMt.Attach(itemToUpdate);

                            itemToUpdate.IdSubVer += 1;
                            if (newVersion == true)
                            {
                                itemToUpdate.IdVer += 1;
                                itemToUpdate.IdSubVer = 0;
                            }
                            itemToUpdate.Timestamp = DateTime.Now;

                            //no hacemos  un "entry" en el contexto de db y lo marcamos como modificado, sino que updatamos sólo los pocos campos
                            //que se pueden modificar de un item desde la aplicación para que EF genere el update sólo de esos campos y no de todos
                            itemToUpdate.IdDefaultSupplier = updateItem.IdDefaultSupplier;
                            itemToUpdate.IdFamilyHK = updateItem.IdFamilyHK;
                            itemToUpdate.IdStatusProd = updateItem.IdStatusProd;
                            itemToUpdate.IdUserAttri1 = updateItem.IdUserAttri1;
                            itemToUpdate.IdUserAttri2 = updateItem.IdUserAttri2;
                            itemToUpdate.IdUserAttri3 = updateItem.IdUserAttri3;
                            itemToUpdate.PhotoUrl = updateItem.PhotoUrl;

                            ItemMtHistory itemHistory = (ItemMtHistory)itemToUpdate;
                            itemHistory.User = GlobalSetting.LoggedUser.UserLogin;

                            //Documento
                            itemDoc.IdVerItem = itemToUpdate.IdVer;
                            itemDoc.IdSubVerItem = itemToUpdate.IdSubVer;
                            itemDoc.CreateDate = DateTime.Now;

                            db.ItemsMtHistory.Add(itemHistory);
                            db.ItemsDoc.Add(itemDoc);

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
