using HKSupply.DB;
using HKSupply.Exceptions;
using HKSupply.General;
using HKSupply.Models;
using HKSupply.Services.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Services.Implementations
{
    class EFEchangeRate : IEchangeRate
    {
        ILog _log = LogManager.GetLogger(typeof(EFCurrency));

        public List<EchangeRate> GetEchangeRates()
        {
            try
            {
                using (var db = new HKSupplyContext())
                {
                    return db.EchangeRates.ToList();
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

        public bool UpdateEchangeRates(IEnumerable<EchangeRate> echangeRates)
        {
            try
            {
                if (echangeRates == null)
                    throw new ArgumentNullException(nameof(echangeRates));

                using (var db = new HKSupplyContext())
                {
                    using (var dbTrans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            foreach(var er in echangeRates)
                            {
                                var echangeRatesToUpdate = db.EchangeRates.Where(a => a.IdCurrency1.Equals(er.IdCurrency1) && a.IdCurrency2.Equals(er.IdCurrency2)).FirstOrDefault();

                                if (echangeRatesToUpdate != null)
                                {
                                    db.EchangeRates.Remove(echangeRatesToUpdate);
                                }

                                //insert new value if currencies exist
                                StringBuilder sql = new StringBuilder();
                                sql.Append("select CASE COUNT(ID_CURRENCY)  WHEN 2 THEN CONVERT(bit,1) ELSE CONVERT(bit,0) END AS EXIST ");
                                sql.Append("FROM (");
                                sql.Append("select ID_CURRENCY from CURRENCIES ");
                                sql.Append($"where ID_CURRENCY = '{er.IdCurrency1}' ");
                                sql.Append("union ");
                                sql.Append("select ID_CURRENCY from CURRENCIES ");
                                sql.Append($"where ID_CURRENCY = '{er.IdCurrency2}' ");
                                sql.Append(") a");

                                bool res = db.Database.SqlQuery<bool>(sql.ToString()).FirstOrDefault();

                                if (res == true)
                                {
                                    db.EchangeRates.Add(er);
                                    db.SaveChanges();
                                }


                            }

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
