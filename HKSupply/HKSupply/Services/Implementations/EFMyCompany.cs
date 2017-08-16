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
    public class EFMyCompany : IMyCompany
    {
        ILog _log = LogManager.GetLogger(typeof(EFMyCompany));

        public MyCompany GetMyCompany()
        {
            try
            {
                using (var db = new HKSupplyContext())
                {
                    return db.MyCompany.FirstOrDefault();
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

        public bool UpdateMyCompany(MyCompany myCompany)
        {
            try
            {
                if (myCompany == null)
                    throw new ArgumentNullException("myCompany");

                using (var db = new HKSupplyContext())
                {
                    using (var dbTrans = db.Database.BeginTransaction())
                    {
                        try
                        {

                            //Con esto marcaremos todo el objeto como modificado y actualizará todos los campos. 
                            //En este caso nos interesa porque la mayoría de los campos de customer se pueden modificar
                            db.Entry(myCompany).State = EntityState.Modified;

                            //Actualizamos los datos del equivalente en Customer y Supplier, ya que desde aquí realmente mantenemos unificados
                            //los datos de Etnia HK como Customer y como Supplier

                            //Customer
                            var customer = db.Customers.Where(a => a.IdCustomer.Equals(myCompany.IdMyCompany)).FirstOrDefault();
                            if (customer == null)
                                throw new Exception("Customer unknown");

                            customer.IdSubVer += 1;
                            customer.Timestamp = DateTime.Now;

                            customer.VATNum = myCompany.VATNum;
                            customer.CustomerName = myCompany.Name;
                            customer.ShippingAddress = myCompany.ShippingAddress;
                            customer.ShippingAddressZh = myCompany.ShippingAddressZh;
                            customer.BillingAddress = myCompany.BillingAddress;
                            customer.BillingAddressZh = myCompany.BillingAddressZh;
                            customer.ContactName = myCompany.ContactName;
                            customer.ContactNameZh = myCompany.ContactNameZh;
                            customer.ContactPhone = myCompany.ContactPhone;
                            customer.Comments = myCompany.Comments;
                            customer.IdDefaultCurrency = myCompany.IdDefaultCurrency;

                            CustomerHistory customerHistory = (CustomerHistory)customer;
                            db.CustomersHistory.Add(customerHistory);

                            //Supplier
                            var supplier = db.Suppliers.Where(a => a.IdSupplier.Equals(myCompany.IdMyCompany)).FirstOrDefault();
                            if (supplier == null)
                                throw new Exception("Supplier unknown");

                            supplier.IdSubVer += 1;
                            supplier.Timestamp = DateTime.Now;

                            supplier.VATNum = myCompany.VATNum;
                            supplier.SupplierName = myCompany.Name;
                            supplier.ShippingAddress = myCompany.ShippingAddress;
                            supplier.ShippingAddressZh = myCompany.ShippingAddressZh;
                            supplier.BillingAddress = myCompany.BillingAddress;
                            supplier.BillingAddressZh = myCompany.BillingAddressZh;
                            supplier.ContactName = myCompany.ContactName;
                            supplier.ContactNameZh = myCompany.ContactNameZh;
                            supplier.ContactPhone = myCompany.ContactPhone;
                            supplier.Comments = myCompany.Comments;
                            supplier.IdDefaultCurrency = myCompany.IdDefaultCurrency;

                            SupplierHistory supplierHistory = (SupplierHistory)supplier;
                            db.SuppliersHistory.Add(supplierHistory);

                            //Save
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
    }
}
