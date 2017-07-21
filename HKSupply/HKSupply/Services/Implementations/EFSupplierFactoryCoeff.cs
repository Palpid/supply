using HKSupply.DB;
using HKSupply.Exceptions;
using HKSupply.General;
using HKSupply.Services.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKSupply.Models;
using System.Data.Entity.Validation;

namespace HKSupply.Services.Implementations
{
    public class EFSupplierFactoryCoeff : ISupplierFactoryCoeff
    {
        ILog _log = LogManager.GetLogger(typeof(EFSupplierFactoryCoeff));

        public List<SupplierFactoryCoeff> GetAllSupplierFactoryCoeff()
        {
            try
            {
                using (var db = new HKSupplyContext())
                {
                    return db.SupplierFactoryCoeff
                        .OrderBy(a => a.IdItemGroup)
                        .ThenBy(a => a.IdSupplier)
                        .ThenBy(a => a.IdFactory)
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

        public SupplierFactoryCoeff NewSupplierFactoryCoeff(SupplierFactoryCoeff newSupplierFactoryCoeff)
        {
            try
            {
                if (newSupplierFactoryCoeff == null)
                    throw new ArgumentNullException(nameof(newSupplierFactoryCoeff));

                using (var db = new HKSupplyContext())
                {
                    var supplierFactoryCoeff = db.SupplierFactoryCoeff.FirstOrDefault(a => a.IdItemGroup.Equals(newSupplierFactoryCoeff.IdItemGroup) && a.IdFactory.Equals(newSupplierFactoryCoeff.IdFactory) && a.IdSupplier.Equals(newSupplierFactoryCoeff.IdSupplier));

                    if (supplierFactoryCoeff != null)
                        throw new Exception("Supplier/Factory Already Exist");

                    db.SupplierFactoryCoeff.Add(newSupplierFactoryCoeff);
                    db.SaveChanges();

                    db.Entry(newSupplierFactoryCoeff).GetDatabaseValues();
                    return newSupplierFactoryCoeff;
                }

            }
            catch (ArgumentNullException nrex)
            {
                _log.Error(nrex.Message, nrex);
                throw nrex;
            }
            catch (NewExistingRoleException areex)
            {
                _log.Info(areex.Message, areex);
                throw areex;
            }
            catch (NonexistentRoleException nerex)
            {
                _log.Error(nerex.Message, nerex);
                throw nerex;
            }
            catch (DbEntityValidationException e)
            {
                _log.Error(e.Message, e);
                throw e;
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

        public bool UpdateSupplierFactoryCoeff(IEnumerable<SupplierFactoryCoeff> SupplierFactoryCoeffsToUpdate)
        {
            try
            {
                if (SupplierFactoryCoeffsToUpdate == null)
                    throw new ArgumentNullException(nameof(SupplierFactoryCoeffsToUpdate));

                using (var db = new HKSupplyContext())
                {
                    using (var dbTrans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            foreach (var supplierFactory in SupplierFactoryCoeffsToUpdate)
                            {
                                var supplierFactoryToUpdate = db.SupplierFactoryCoeff
                                    .Where(a => a.IdItemGroup.Equals(supplierFactory.IdItemGroup) && a.IdSupplier.Equals(supplierFactory.IdSupplier) && a.IdFactory.Equals(supplierFactory.IdFactory))
                                    .FirstOrDefault();

                                if (supplierFactoryToUpdate != null)
                                {
                                    supplierFactoryToUpdate.Density = supplierFactory.Density;
                                    supplierFactoryToUpdate.Coefficient1 = supplierFactory.Coefficient1;
                                    supplierFactoryToUpdate.Coefficient2 = supplierFactory.Coefficient2;
                                    supplierFactoryToUpdate.Scrap = supplierFactory.Scrap;
                                }
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
    }
}
