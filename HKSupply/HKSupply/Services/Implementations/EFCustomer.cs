using HKSupply.DB;
using HKSupply.Exceptions;
using HKSupply.Helpers;
using HKSupply.Models;
using HKSupply.Services.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
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
                            newCustomer.idVer = 1;
                            newCustomer.idSubVer = 0;
                            newCustomer.Timestamp = DateTime.Now;

                            CustomerHistory customerHistory = (CustomerHistory)newCustomer;

                            db.Customers.Add(newCustomer);
                            db.CustomersHistory.Add(customerHistory);
                            db.SaveChanges();

                            dbTrans.Commit();
                            return true;

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
                            var customer = updateCustomer.Clone();

                            db.Entry(updateCustomer).State = EntityState.Deleted;

                            customer.idSubVer += 1;
                            if (newVer == true)
                            {
                                customer.idVer += 1;
                                customer.idSubVer = 0;
                            }
                            customer.Timestamp = DateTime.Now;

                            CustomerHistory customerHistory = (CustomerHistory)customer;

                            db.Customers.Add(customer);
                            db.CustomersHistory.Add(customerHistory);
                            db.SaveChanges();

                            dbTrans.Commit();
                            return true;

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
                    var cust = db.Customers.Where(c => c.idCustomer.Equals(idCustomer)).SingleOrDefault();
                    return cust;
                }
            }
            catch (ArgumentNullException anex)
            {
                _log.Error(anex.Message, anex);
                throw anex;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                throw ex;
            }
        }
    }
}
