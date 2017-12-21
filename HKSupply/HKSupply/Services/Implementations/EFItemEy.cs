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
    public class EFItemEy : IItemEy 
    {
        ILog _log = LogManager.GetLogger(typeof(EFItemEy));

        public List<ItemEy> GetItems(bool withDocWarning = false)
        {
            try
            {
                using (var db = new HKSupplyContext())
                {

                    var itemsEy =  db.ItemsEy
                        .Include(i => i.Model)
                        .Include(i => i.Prototype) 
                        .Include(i => i.FamilyHK)
                        .Include(i => i.StatusCial)
                        .Include(i => i.StatusProd)
                        .OrderBy(i => i.IdItemBcn)
                        .ToList();

                    if (withDocWarning)
                    {
                        foreach (var item in itemsEy)
                        {
                            Classes.ItemDocWarning docWarning = db.Database.SqlQuery<Classes.ItemDocWarning>($"SELECT ID_ITEM_BCN,WARNING_COL_CODE,WARNING_COL_DESC,WARNING_COL_COLOR FROM dbo.V_DOCS_WARNINGS WHERE ID_ITEM_BCN = '{item.IdItemBcn}'").FirstOrDefault();
                            item.DocWarning = docWarning;
                        }
                    }

                    return itemsEy;
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

        public List<ItemEy> GetItems(string idDefaultSupplier, bool withDocWarning = false)
        {
            try
            {
                using (var db = new HKSupplyContext())
                {

                    //var itemsEy = db.ItemsEy
                    //    .Include(i => i.Model)
                    //    .Include(i => i.Prototype)
                    //    .Include(i => i.FamilyHK)
                    //    .Include(i => i.StatusCial)
                    //    .Include(i => i.StatusProd)
                    //    .OrderBy(i => i.IdItemBcn)
                    //    .Where(a => a.IdDefaultSupplier.Equals(idDefaultSupplier))
                    //    .ToList();

                    //Recuperamos los items que tengan ese proveedor por defecto o que en algun momento hayan trabajado con el item
                    var itemsBom = db.ItemsBom
                        .Where(a => a.IdItemGroup.Equals(Constants.ITEM_GROUP_EY) && a.IdSupplier.Equals(idDefaultSupplier))
                        .Select(b => b.IdItemBcn).ToList();

                    var itemsEy = db.ItemsEy
                        .Include(i => i.Model)
                        .Include(i => i.Prototype)
                        .Include(i => i.FamilyHK)
                        .Include(i => i.StatusCial)
                        .Include(i => i.StatusProd)
                        .OrderBy(i => i.IdItemBcn)
                        .Where(a => a.IdDefaultSupplier.Equals(idDefaultSupplier))
                        .Union(
                        db.ItemsEy
                        .Include(i => i.Model)
                        .Include(i => i.Prototype)
                        .Include(i => i.FamilyHK)
                        .Include(i => i.StatusCial)
                        .Include(i => i.StatusProd)
                        .OrderBy(i => i.IdItemBcn)
                        .Where(a => itemsBom.Contains(a.IdItemBcn))
                        )
                        .ToList();

                    if (withDocWarning)
                    {
                        foreach (var item in itemsEy)
                        {
                            Classes.ItemDocWarning docWarning = db.Database.SqlQuery<Classes.ItemDocWarning>($"SELECT ID_ITEM_BCN,WARNING_COL_CODE,WARNING_COL_DESC,WARNING_COL_COLOR FROM dbo.V_DOCS_WARNINGS WHERE ID_ITEM_BCN = '{item.IdItemBcn}'").FirstOrDefault();
                            item.DocWarning = docWarning;
                        }
                    }

                    return itemsEy;
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

        public ItemEy GetItem(string idItemBcn)
        {
            try
            {
                if (idItemBcn == null)
                    throw new ArgumentNullException("idItemBcn");

                using (var db = new HKSupplyContext())
                {
                    var item = db.ItemsEy
                        .Where(i =>i.IdItemBcn.Equals(idItemBcn))
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

        public bool UpdateItem(ItemEy updateItem, bool newVersion = false)
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
                            ItemEy itemToUpdate = GetItem(updateItem.IdItemBcn);

                            if (itemToUpdate == null)
                                return false;

                            //Hay que agregarlo al contexto actual para que lo actualice
                            db.ItemsEy.Attach(itemToUpdate);

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

                            ItemEyHistory itemHistory = (ItemEyHistory)itemToUpdate;
                            itemHistory.User = GlobalSetting.LoggedUser.UserLogin;

                            db.ItemsEyHistory.Add(itemHistory);
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

        public bool UpdateItemWithDoc(ItemEy updateItem, ItemDoc itemDoc, bool newVersion = false)
        {
            try
            {
                if (updateItem == null)
                    throw new ArgumentNullException("updateItem");

                if (itemDoc == null)
                    throw new ArgumentNullException("itemDoc");

                using (var db = new HKSupplyContext())
                {
                    using (var dbTrans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            ItemEy itemToUpdate = GetItem(updateItem.IdItemBcn);

                            if (itemToUpdate == null)
                                return false;

                            //Hay que agregarlo al contexto actual para que lo actualice
                            db.ItemsEy.Attach(itemToUpdate);

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

                            ItemEyHistory itemHistory = (ItemEyHistory)itemToUpdate;
                            itemHistory.User = GlobalSetting.LoggedUser.UserLogin;

                            //Documento
                            itemDoc.IdVerItem = itemToUpdate.IdVer;
                            itemDoc.IdSubVerItem = itemToUpdate.IdSubVer;
                            itemDoc.CreateDate = DateTime.Now;

                            db.ItemsEyHistory.Add(itemHistory);
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

        public bool UpdateItems(IEnumerable<ItemEy> itemsToUpdate)
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
                                ItemEy itemToUpdate = db.ItemsEy.Where(a => a.IdItemBcn.Equals(item.IdItemBcn)).FirstOrDefault();

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

                                ItemEyHistory itemHistory = (ItemEyHistory)itemToUpdate;
                                itemHistory.User = GlobalSetting.LoggedUser.UserLogin;

                                db.ItemsEyHistory.Add(itemHistory);
 
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

        public List<ItemEyHistory> GetItemEyHistory(string idItemBcn)
        {
            if (idItemBcn == null)
                throw new ArgumentNullException("idItemBcn");

            try
            {
                using (var db = new HKSupplyContext())
                {
                    return db.ItemsEyHistory
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

        //public bool newItem(Item newItem)
        //{
        //    try
        //    {
        //        if(newItem == null)
        //            throw new ArgumentNullException("newItem ");

        //        using (var db = new HKSupplyContext())
        //        {
        //            using (var dbTrans = db.Database.BeginTransaction())
        //            {
        //                try
        //                {
        //                    var tmpItem = GetItem(newItem.IdItemBcn);
        //                    if (tmpItem != null)
        //                        throw new Exception("Item already exist");

        //                    newItem.IdVer = 1;
        //                    newItem.IdSubVer = 0;
        //                    newItem.Timestamp = DateTime.Now;

        //                    ItemHistory itemHistory = (ItemHistory)newItem;

        //                    db.Items.Add(newItem);
        //                    db.ItemsHistory.Add(itemHistory);
        //                    db.SaveChanges();
        //                    dbTrans.Commit();

        //                    return true;                           

        //                }
        //                catch (DbEntityValidationException e)
        //                {
        //                    dbTrans.Rollback();
        //                    _log.Error(e.Message, e);
        //                    throw e;
        //                }
        //                catch (SqlException sqlex)
        //                {
        //                    dbTrans.Rollback();

        //                    for (int i = 0; i < sqlex.Errors.Count; i++)
        //                    {
        //                        _log.Error("Index #" + i + "\n" +
        //                            "Message: " + sqlex.Errors[i].Message + "\n" +
        //                            "Error Number: " + sqlex.Errors[i].Number + "\n" +
        //                            "LineNumber: " + sqlex.Errors[i].LineNumber + "\n" +
        //                            "Source: " + sqlex.Errors[i].Source + "\n" +
        //                            "Procedure: " + sqlex.Errors[i].Procedure + "\n");

        //                        switch (sqlex.Errors[i].Number)
        //                        {
        //                            case -1: //connection broken
        //                            case -2: //timeout
        //                                throw new DBServerConnectionException(GlobalSetting.ResManager.GetString("DBServerConnectionError"));
        //                        }
        //                    }
        //                    throw sqlex;
        //                }
        //                catch (Exception ex)
        //                {
        //                    dbTrans.Rollback();
        //                    _log.Error(ex.Message, ex);
        //                    throw ex;
        //                }

        //            }
        //        }
        //    }
        //    catch (ArgumentNullException anex)
        //    {
        //        _log.Error(anex.Message, anex);
        //        throw anex;
        //    }

        //}




    }
}
