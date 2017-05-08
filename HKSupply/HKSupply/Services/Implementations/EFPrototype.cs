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
    public class EFPrototype : IPrototype
    {
        ILog _log = LogManager.GetLogger(typeof(EFPrototype));

        public List<Prototype> GetPrototypes()
        {
            try
            {
                using (var db = new HKSupplyContext())
                {
                    return db.Prototypes
                        .Include(p => p.DefaultSupplier)
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
