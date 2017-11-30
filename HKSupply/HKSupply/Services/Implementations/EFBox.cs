using HKSupply.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using HKSupply.Models.Supply;
using log4net;
using HKSupply.DB;
using System.Data.SqlClient;
using HKSupply.Exceptions;
using HKSupply.General;

namespace HKSupply.Services.Implementations
{
    public class EFBox : IBox
    {
        ILog _log = LogManager.GetLogger(typeof(EFBox));

        public List<Box> GetBoxes()
        {
            try
            {
                using (var db = new HKSupplyContext())
                {
                    return db.Boxes
                        .OrderBy(a => a.Length)
                        .ThenBy(b => b.Width)
                        .ThenBy(c => c.Height)
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
