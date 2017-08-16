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
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Services.Implementations
{
    public class EFSupplier : ISupplier
    {

        ILog _log = LogManager.GetLogger(typeof(EFCustomer));

        public bool NewSupplier(Supplier newSupplier)
        {
            try
            {
                if (newSupplier == null)
                    throw new ArgumentNullException("newSupplier");

                using (var db = new HKSupplyContext())
                {
                    using (var dbTrans = db.Database.BeginTransaction())
                    {
                        try
                        {

                            var tmpCustomer = GetSupplierById(newSupplier.IdSupplier);
                            if (tmpCustomer != null)
                                throw new Exception("Customer already exist");

                            newSupplier.IdVer = 1;
                            newSupplier.IdSubVer = 0;
                            newSupplier.Timestamp = DateTime.Now;

                            SupplierHistory SupplierHistory = (SupplierHistory)newSupplier;

                            db.Suppliers.Add(newSupplier);
                            db.SuppliersHistory.Add(SupplierHistory);
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

        public bool UpdateSupplier(Supplier updateSupplier, bool newVer = false)
        {
            try
            {
                if (updateSupplier == null)
                    throw new ArgumentNullException("updateSupplier");

                using (var db = new HKSupplyContext())
                {
                    using (var dbTrans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            //var supplier = updateSupplier.Clone();

                            //db.Entry(updateSupplier).State = EntityState.Deleted;

                            //supplier.IdSubVer += 1;
                            //if (newVer == true)
                            //{
                            //    supplier.IdVer += 1;
                            //    supplier.IdSubVer = 0;
                            //}
                            //supplier.Timestamp = DateTime.Now;

                            //SupplierHistory supplierHistory = (SupplierHistory)supplier;

                            //db.Suppliers.Add(supplier);
                            //db.SuppliersHistory.Add(supplierHistory);
                            //db.SaveChanges();

                            updateSupplier.IdSubVer += 1;
                            if (newVer == true)
                            {
                                updateSupplier.IdVer += 1;
                                updateSupplier.IdSubVer = 0;
                            }
                            updateSupplier.Timestamp = DateTime.Now;

                            SupplierHistory supplierHistory = (SupplierHistory)updateSupplier;

                            //Con esto marcaremos todo el objeto como modificado y actualizará todos los campos. 
                            //En este caso nos interesa porque la mayoría de los campos de supplier se pueden modificar
                            db.Entry(updateSupplier).State = EntityState.Modified;

                            db.SuppliersHistory.Add(supplierHistory);
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

        public Models.Supplier GetSupplierById(string idSupplier)
        {
            try
            {
                if (string.IsNullOrEmpty(idSupplier))
                    throw new ArgumentNullException("idSupplier");

                using (var db = new HKSupplyContext())
                {
                    var supplier = db.Suppliers.Where(s => s.IdSupplier.Equals(idSupplier)).SingleOrDefault();
                    return supplier;
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

        public List<Models.Supplier> GetSuppliers(bool withEtniaHk = false)
        {
            try
            {
                using (var db = new HKSupplyContext())
                {
                    if (withEtniaHk)
                        return db.Suppliers.ToList();
                    else
                        return db.Suppliers.Where(a => a.IdSupplier != Constants.ETNIA_HK_COMPANY_CODE).ToList();
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


        public List<SupplierHistory> GetSupplierHistory(string idSupplier)
        {
            if (idSupplier == null)
                throw new ArgumentNullException("idSupplier");

            try
            {
                using (var db = new HKSupplyContext())
                {
                    return db.SuppliersHistory
                        .Where(a => a.IdSupplier.Equals(idSupplier))
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
