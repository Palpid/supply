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
    public class EFItemHw : IItemHw
    {
        ILog _log = LogManager.GetLogger(typeof(EFItemHw));

        public List<ItemHw> GetItems()
        {
            try
            {
                using (var db = new HKSupplyContext())
                {
                    return db.ItemsHw
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

        public ItemHw GetItem(string idItemBcn)
        {
            try
            {
                if (idItemBcn == null)
                    throw new ArgumentNullException("idItemBcn");

                using (var db = new HKSupplyContext())
                {
                    var item = db.ItemsHw
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

        public bool UpdateItem(ItemHw updateItem, bool newVersion = false)
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
                            ItemHw itemToUpdate = GetItem(updateItem.IdItemBcn);

                            if (itemToUpdate == null)
                                return false;

                            //Hay que agregarlo al contexto actual para que lo actualice
                            db.ItemsHw.Attach(itemToUpdate);

                            itemToUpdate.IdSubVer += 1;
                            if (newVersion == true)
                            {
                                itemToUpdate.IdVer += 1;
                                itemToUpdate.IdSubVer = 0;
                            }
                            itemToUpdate.Timestamp = DateTime.Now;

                            //no hacemos  un "entry" en el contexto de db y lo marcamos como modificado, sino que updatamos sólo los campos
                            //que se pueden modificar de un item desde la aplicación para que EF genere el update sólo de esos campos y no de todos
                            itemToUpdate.IdPrototype = updateItem.IdPrototype;
                            itemToUpdate.IdColor1 = updateItem.IdColor1;
                            itemToUpdate.IdColor2 = updateItem.IdColor2;
                            itemToUpdate.IdDefaultSupplier = updateItem.IdDefaultSupplier;
                            itemToUpdate.IdFamilyHK = updateItem.IdFamilyHK;
                            itemToUpdate.IdItemHK = updateItem.IdItemHK;
                            itemToUpdate.IdFamilyHK = updateItem.IdFamilyHK;
                            itemToUpdate.ItemDescription = updateItem.ItemDescription;
                            itemToUpdate.IdStatusCial = updateItem.IdStatusCial;
                            itemToUpdate.IdStatusProd = updateItem.IdStatusProd;
                            itemToUpdate.IdUserAttri1 = updateItem.IdUserAttri1;
                            itemToUpdate.IdUserAttri2 = updateItem.IdUserAttri2;
                            itemToUpdate.IdUserAttri3 = updateItem.IdUserAttri3;
                            itemToUpdate.Comments = updateItem.Comments;
                            itemToUpdate.LaunchDate = updateItem.LaunchDate;
                            itemToUpdate.RemovalDate = updateItem.RemovalDate;
                            itemToUpdate.PhotoUrl = updateItem.PhotoUrl;
                            itemToUpdate.Unit = updateItem.Unit;

                            ItemHwHistory itemHistory = (ItemHwHistory)itemToUpdate;
                            itemHistory.User = GlobalSetting.LoggedUser.UserLogin;

                            db.ItemsHwHistory.Add(itemHistory);
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

        public bool UpdateItemWithDoc(ItemHw updateItem, ItemDoc itemDoc, bool newVersion = false)
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
                            ItemHw itemToUpdate = GetItem(updateItem.IdItemBcn);

                            if (itemToUpdate == null)
                                return false;

                            //Hay que agregarlo al contexto actual para que lo actualice
                            db.ItemsHw.Attach(itemToUpdate);

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
                            itemToUpdate.IdItemHK = updateItem.IdItemHK;
                            itemToUpdate.IdStatusProd = updateItem.IdStatusProd;
                            itemToUpdate.IdUserAttri1 = updateItem.IdUserAttri1;
                            itemToUpdate.IdUserAttri2 = updateItem.IdUserAttri2;
                            itemToUpdate.IdUserAttri3 = updateItem.IdUserAttri3;
                            itemToUpdate.PhotoUrl = updateItem.PhotoUrl;

                            ItemHwHistory itemHistory = (ItemHwHistory)itemToUpdate;
                            itemHistory.User = GlobalSetting.LoggedUser.UserLogin;

                            //Documento
                            itemDoc.IdVerItem = itemToUpdate.IdVer;
                            itemDoc.IdSubVerItem = itemToUpdate.IdSubVer;
                            itemDoc.CreateDate = DateTime.Now;

                            db.ItemsHwHistory.Add(itemHistory);
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

        public bool UpdateItems(IEnumerable<ItemHw> itemsToUpdate)
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
                                ItemHw itemToUpdate = db.ItemsHw.Where(a => a.IdItemBcn.Equals(item.IdItemBcn)).FirstOrDefault();

                                itemToUpdate.IdSubVer += 1;
                                itemToUpdate.Timestamp = DateTime.Now;
                                //no hacemos  un "entry" en el contexto de db y lo marcamos como modificado, sino que updatamos sólo los pocos campos
                                //que se pueden modificar de un item desde la aplicación para que EF genere el update sólo de esos campos y no de todos
                                itemToUpdate.IdDefaultSupplier = item.IdDefaultSupplier;
                                itemToUpdate.IdFamilyHK = item.IdFamilyHK;
                                itemToUpdate.IdItemHK = item.IdItemHK;
                                itemToUpdate.IdStatusProd = item.IdStatusProd;
                                itemToUpdate.IdUserAttri1 = item.IdUserAttri1;
                                itemToUpdate.IdUserAttri2 = item.IdUserAttri2;
                                itemToUpdate.IdUserAttri3 = item.IdUserAttri3;

                                ItemHwHistory itemHistory = (ItemHwHistory)itemToUpdate;
                                itemHistory.User = GlobalSetting.LoggedUser.UserLogin;

                                db.ItemsHwHistory.Add(itemHistory);
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

        public List<ItemHwHistory> GetItemHwHistory(string idItemBcn)
        {
            if (idItemBcn == null)
                throw new ArgumentNullException("idItemBcn");

            try
            {
                using (var db = new HKSupplyContext())
                {
                    return db.ItemsHwHistory
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

        public bool newItem(ItemHw newItemHw)
        {
            try
            {
                if (newItemHw == null)
                    throw new ArgumentNullException("newItemHw ");

                using (var db = new HKSupplyContext())
                {
                    using (var dbTrans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var tmpItem = GetItem(newItemHw.IdItemBcn);
                            if (tmpItem != null)
                                throw new Exception("Item already exist");

                            newItemHw.Model = null;
                            newItemHw.DefaultSupplier = null;
                            newItemHw.Prototype = null;

                            newItemHw.IdVer = 1;
                            newItemHw.IdSubVer = 0;
                            newItemHw.Timestamp = DateTime.Now;
                            newItemHw.CreateDate = DateTime.Now;
                            newItemHw.IdGroupType = Constants.HW_GROUP_TYPE_PRODUCTION; //Los hw que se crean desde esta aplicación a día de hoy son todos del tipo "producción"

                            ItemHwHistory itemHwHistory = (ItemHwHistory)newItemHw;
                            itemHwHistory.User = GlobalSetting.LoggedUser.UserLogin;

                            db.ItemsHw.Add(newItemHw);
                            db.ItemsHwHistory.Add(itemHwHistory);
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
