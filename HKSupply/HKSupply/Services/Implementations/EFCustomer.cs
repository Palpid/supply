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
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Services.Implementations
{
    public class EFCustomer : ICustomer
    {
        ILog _log = LogManager.GetLogger(typeof(EFCustomer));

        public bool NewCustomer(Customer newCustomer)
        {
            try
            {
                if (newCustomer == null)
                    throw new ArgumentNullException("newCustomer");

                using (var db = new HKSupplyContext())
                {
                    using (var dbTrans = db.Database.BeginTransaction())
                    {
                        try
                        {

                            var tmpCustomer = GetCustomerById(newCustomer.IdCustomer);
                            if (tmpCustomer != null)
                                throw new Exception("Customer already exist");

                            newCustomer.IdVer = 1;
                            newCustomer.IdSubVer = 0;
                            newCustomer.Timestamp = DateTime.Now;

                            CustomerHistory customerHistory = (CustomerHistory)newCustomer;

                            db.Customers.Add(newCustomer);
                            db.CustomersHistory.Add(customerHistory);
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

        public bool UpdateCustomer(Customer updateCustomer, bool newVer = false)
        {
            try
            {
                if (updateCustomer == null)
                    throw new ArgumentNullException("updateCustomer");
                
                using (var db = new HKSupplyContext())
                {
                    using (var dbTrans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            //var customer = updateCustomer.Clone();

                            //db.Entry(updateCustomer).State = EntityState.Deleted;

                            //customer.IdSubVer += 1;
                            //if (newVer == true)
                            //{
                            //    customer.IdVer += 1;
                            //    customer.IdSubVer = 0;
                            //}
                            //customer.Timestamp = DateTime.Now;

                            //CustomerHistory customerHistory = (CustomerHistory)customer;

                            //db.Customers.Add(customer);
                            //db.CustomersHistory.Add(customerHistory);
                            //db.SaveChanges();

                            updateCustomer.IdSubVer += 1;
                            if (newVer == true)
                            {
                                updateCustomer.IdVer += 1;
                                updateCustomer.IdSubVer = 0;
                            }
                            updateCustomer.Timestamp = DateTime.Now;

                            CustomerHistory customerHistory = (CustomerHistory)updateCustomer;

                            //Con esto marcaremos todo el objeto como modificado y actualizará todos los campos. 
                            //En este caso nos interesa porque la mayoría de los campos de customer se pueden modificar
                            db.Entry(updateCustomer).State = EntityState.Modified;

                            db.CustomersHistory.Add(customerHistory);
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

        public Customer GetCustomerById(string idCustomer)
        {
            try
            {
                if (string.IsNullOrEmpty(idCustomer))
                    throw new ArgumentNullException("idCustomer");

                using (var db = new HKSupplyContext())
                {
                    var cust = db.Customers.Where(c => c.IdCustomer.Equals(idCustomer)).SingleOrDefault();
                    return cust;
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

        public List<Customer> GetCustomers()
        {
            try
            {
                using (var db = new HKSupplyContext())
                {
                    return db.Customers.ToList();
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
