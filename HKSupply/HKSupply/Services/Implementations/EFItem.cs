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
    public class EFItem : IItem 
    {
        ILog _log = LogManager.GetLogger(typeof(EFItem));

        public List<Item> GetItems(string idItemGroup)
        {
            try
            {
                using (var db = new HKSupplyContext())
                {
                    return db.Items
                        .Where(i => i.IdItemGroup.Equals(idItemGroup))
                        .Include(i => i.Model)
                        .Include(i => i.ItemGroup)
                        .Include(i => i.Color1)
                        .Include(i => i.Color2)
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

        public Item GetItem(string idItemGroup, string idPrototype, string idItemBcn)
        {
            try
            {
                if (idItemBcn == null)
                    throw new ArgumentNullException("itemCode");

                using (var db = new HKSupplyContext())
                {
                    var item = db.Items
                        .Where(i =>
                            i.IdItemGroup.Equals(idItemGroup) &&
                            i.IdPrototype.Equals(idPrototype) &&
                            i.IdItemBcn.Equals(idItemBcn))
                        .Include(i => i.Model)
                        .Include(i => i.ItemGroup)
                        .Include(i => i.Color1)
                        .Include(i => i.Color2)
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

        public bool UpdateItem(Item updateItem, bool newVersion = false)
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
                            Item itemToUpdate = GetItem(updateItem.IdItemGroup, updateItem.IdPrototype, updateItem.IdItemBcn);

                            if (itemToUpdate == null)
                                return false;

                            //Hay que agregarlo al contexto actual para que lo actualice
                            db.Items.Attach(itemToUpdate);

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

                            ItemHistory itemHistory = (ItemHistory)itemToUpdate;
                            itemHistory.User = GlobalSetting.LoggedUser.UserLogin;

                            db.ItemsHistory.Add(itemHistory);
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
