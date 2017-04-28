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
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Services.Implementations
{
    public class EFSupplierPriceList : ISupplierPriceList
    {
        ILog _log = LogManager.GetLogger(typeof(EFSupplierPriceList));

        public bool NewSupplierPriceList(SupplierPriceList newSupplierPriceList)
        {
            try
            {
                if (newSupplierPriceList == null)
                    throw new ArgumentNullException("newSupplierPriceList");

                using (var db = new HKSupplyContext())
                {
                    using (var dbTrans = db.Database.BeginTransaction())
                    {
                        try
                        {

                            var tmpSupplierPriceList = GetSupplierPriceList(newSupplierPriceList.IdItemBcn, newSupplierPriceList.IdSupplier);
                            if (tmpSupplierPriceList != null)
                                throw new Exception("Supplier Price List already exist");

                            newSupplierPriceList.IdVer = 1;
                            newSupplierPriceList.IdSubVer = 0;
                            newSupplierPriceList.Timestamp = DateTime.Now;

                            SupplierPriceListHistory supplierPriceListHistory = (SupplierPriceListHistory)newSupplierPriceList;
                            supplierPriceListHistory.User = GlobalSetting.LoggedUser.UserLogin;

                            db.SuppliersPriceList.Add(newSupplierPriceList);
                            db.SuppliersPriceListHistory.Add(supplierPriceListHistory);
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

        public bool UpdateSupplierPriceList(SupplierPriceList updateSupplierPriceList, bool newVer = false)
        {
            try
            {
                if (updateSupplierPriceList == null)
                    throw new ArgumentNullException("updateSupplierPriceList");

                using (var db = new HKSupplyContext())
                {
                    using (var dbTrans = db.Database.BeginTransaction())
                    {
                        try
                        {

                            updateSupplierPriceList.IdSubVer += 1;
                            if (newVer == true)
                            {
                                updateSupplierPriceList.IdVer += 1;
                                updateSupplierPriceList.IdSubVer = 0;
                            }
                            updateSupplierPriceList.Timestamp = DateTime.Now;

                            SupplierPriceListHistory supplierPriceListHistory = (SupplierPriceListHistory)updateSupplierPriceList;
                            supplierPriceListHistory.User = GlobalSetting.LoggedUser.UserLogin;

                            //Con esto marcaremos todo el objeto como modificado y actualizará todos los campos. 
                            //En este caso nos interesa porque la mayoría de los campos de supplier se pueden modificar
                            db.Entry(updateSupplierPriceList).State = EntityState.Modified;

                            db.SuppliersPriceListHistory.Add(supplierPriceListHistory);
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

        public bool UpdateSuppliersPricesList(IEnumerable<SupplierPriceList> pricesListToUpdate)
        {
            try
            {
                if (pricesListToUpdate == null)
                    throw new ArgumentNullException("pricesListToUpdate");

                using (var db = new HKSupplyContext())
                {
                    using (var dbTrans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            foreach (var priceList in pricesListToUpdate)
                            {
                                priceList.IdSubVer += 1;
                                priceList.Timestamp = DateTime.Now;

                                SupplierPriceListHistory supplierPriceListHistory = (SupplierPriceListHistory)priceList;
                                supplierPriceListHistory.User = GlobalSetting.LoggedUser.UserLogin;

                                //Con esto marcaremos todo el objeto como modificado y actualizará todos los campos. 
                                //En este caso nos interesa porque la mayoría de los campos de supplier se pueden modificar
                                db.Entry(priceList).State = EntityState.Modified;
                                db.SuppliersPriceListHistory.Add(supplierPriceListHistory);
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

        public SupplierPriceList GetSupplierPriceList(string idItemBcn, string idSupplier)
        {
            try
            {
                if (string.IsNullOrEmpty(idItemBcn))
                    throw new ArgumentNullException("idItemBcn");

                if (string.IsNullOrEmpty(idSupplier))
                    throw new ArgumentNullException("idSupplier");

                using (var db = new HKSupplyContext())
                {
                    var supplierPriceList = db.SuppliersPriceList.Where(s => s.IdItemBcn.Equals(idItemBcn) && s.IdSupplier.Equals(idSupplier)).SingleOrDefault();
                    return supplierPriceList;
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

        public List<SupplierPriceList> GetSuppliersPriceList(string idItemBcn = null, string idSupplier = null)
        {
            try
            {
                using (var db = new HKSupplyContext())
                {
                    if (idItemBcn != null && idSupplier != null)
                        return db.SuppliersPriceList.Where(a => a.IdItemBcn.Equals(idItemBcn) && a.IdSupplier.Equals(idSupplier)).ToList();
                    else if (idItemBcn != null && idSupplier == null)
                        return db.SuppliersPriceList.Where(a => a.IdItemBcn.Equals(idItemBcn)).ToList();
                    else if (idItemBcn == null && idSupplier != null)
                        return db.SuppliersPriceList.Where(a => a.IdSupplier.Equals(idSupplier)).ToList();
                    else
                        return db.SuppliersPriceList.ToList();

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

        public List<SupplierPriceListHistory> GetSupplierPriceListHistory(string idItemBcn, string idSupplier)
        {
            if (idItemBcn == null)
                throw new ArgumentNullException("idItemBcn");

            if (idSupplier == null)
                throw new ArgumentNullException("idSupplier");

            try
            {
                using (var db = new HKSupplyContext())
                {
                    return db.SuppliersPriceListHistory
                        .Where(a => a.IdItemBcn.Equals(idItemBcn) && a.IdSupplier.Equals(idSupplier))
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

    }
}
