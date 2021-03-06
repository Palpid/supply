﻿using HKSupply.DB;
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
    public class EFCustomerPriceList : ICustomerPriceList
    {
        ILog _log = LogManager.GetLogger(typeof(EFCustomerPriceList));

        public bool NewCustomerPriceList(CustomerPriceList newCustomerPriceList)
        {
            try
            {
                if (newCustomerPriceList == null)
                    throw new ArgumentNullException("newSupplierPriceList");

                using (var db = new HKSupplyContext())
                {
                    using (var dbTrans = db.Database.BeginTransaction())
                    {
                        try
                        {

                            var tmpCustomerPriceList = GetCustomerPriceList(newCustomerPriceList.IdItemBcn, newCustomerPriceList.IdCustomer);
                            if (tmpCustomerPriceList != null)
                                throw new Exception("Customer Price List already exist");

                            newCustomerPriceList.IdVer = 1;
                            newCustomerPriceList.IdSubVer = 0;
                            newCustomerPriceList.Timestamp = DateTime.Now;

                            CustomerPriceListHistory customerPriceListHistory = (CustomerPriceListHistory)newCustomerPriceList;
                            customerPriceListHistory.User = GlobalSetting.LoggedUser.UserLogin;

                            db.CustomersPriceList.Add(newCustomerPriceList);
                            db.CustomersPriceListHistory.Add(customerPriceListHistory);
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

        public bool UpdateCustomerPriceList(CustomerPriceList updateCustomerPriceList, bool newVer = false)
        {
            try
            {
                if (updateCustomerPriceList == null)
                    throw new ArgumentNullException("updateCustomerPriceList");

                using (var db = new HKSupplyContext())
                {
                    using (var dbTrans = db.Database.BeginTransaction())
                    {
                        try
                        {

                            updateCustomerPriceList.IdSubVer += 1;
                            if (newVer == true)
                            {
                                updateCustomerPriceList.IdVer += 1;
                                updateCustomerPriceList.IdSubVer = 0;
                            }
                            updateCustomerPriceList.Timestamp = DateTime.Now;

                            CustomerPriceListHistory customerPriceListHistory = (CustomerPriceListHistory)updateCustomerPriceList;
                            customerPriceListHistory.User = GlobalSetting.LoggedUser.UserLogin;

                            //Con esto marcaremos todo el objeto como modificado y actualizará todos los campos. 
                            //En este caso nos interesa porque la mayoría de los campos de supplier se pueden modificar
                            db.Entry(updateCustomerPriceList).State = EntityState.Modified;

                            db.CustomersPriceListHistory.Add(customerPriceListHistory);
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

        public bool UpdateCustomersPricesList(IEnumerable<CustomerPriceList> pricesListToUpdate)
        {
            try
            {
                if (pricesListToUpdate == null)
                    throw new ArgumentNullException(nameof(pricesListToUpdate));

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

                                CustomerPriceListHistory customerPriceListHistory = (CustomerPriceListHistory)priceList;
                                customerPriceListHistory.User = GlobalSetting.LoggedUser.UserLogin;

                                //Con esto marcaremos todo el objeto como modificado y actualizará todos los campos. 
                                //En este caso nos interesa porque la mayoría de los campos de supplier se pueden modificar
                                db.Entry(priceList).State = EntityState.Modified;
                                db.CustomersPriceListHistory.Add(customerPriceListHistory);
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

        public CustomerPriceList GetCustomerPriceList(string idItemBcn, string idCustomer)
        {
            try
            {
                if (string.IsNullOrEmpty(idItemBcn))
                    throw new ArgumentNullException("idItemBcn");

                if (string.IsNullOrEmpty(idCustomer))
                    throw new ArgumentNullException("idCustomer");

                using (var db = new HKSupplyContext())
                {
                    var customerPriceList = db.CustomersPriceList.Where(c => c.IdItemBcn.Equals(idItemBcn) && c.IdCustomer.Equals(idCustomer)).SingleOrDefault();
                    return customerPriceList;
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

        public List<CustomerPriceList> GetCustomersPriceList()
        {
            try
            {
                using (var db = new HKSupplyContext())
                {
                    return db.CustomersPriceList.ToList();
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

        public List<CustomerPriceListHistory> GetCustomerPriceListHistory(string idItemBcn, string idCustomer)
        {
            if (idItemBcn == null)
                throw new ArgumentNullException("idItemBcn");

            if (idCustomer == null)
                throw new ArgumentNullException("idCustomer");

            try
            {
                using (var db = new HKSupplyContext())
                {
                    return db.CustomersPriceListHistory
                        .Where(a => a.IdItemBcn.Equals(idItemBcn) && a.IdCustomer.Equals(idCustomer))
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
