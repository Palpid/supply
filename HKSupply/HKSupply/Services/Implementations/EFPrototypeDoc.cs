﻿using HKSupply.DB;
using HKSupply.Exceptions;
using HKSupply.General;
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
    public class EFPrototypeDoc : IPrototypeDoc
    {
        ILog _log = LogManager.GetLogger(typeof(EFPrototypeDoc));

        public List<PrototypeDoc> GetLastPrototypeDocs(string idPrototype)
        {
            try
            {
                using (var db = new HKSupplyContext())
                {
                    //TODO: intentar unificar esto en una sola consulta
                    var idDocs = db.PrototypesDocs
                        .Where(a => a.IdPrototype.Equals(idPrototype))
                        .GroupBy(a => a.IdDocType)
                        .Select(i => i.Max(a => a.IdDoc))
                        .ToList();

                    var docs = db.PrototypesDocs
                        .Where(a => idDocs.Contains(a.IdDoc))
                        .Include(b => b.DocType)
                        .ToList();

                    return docs;
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

        public List<PrototypeDoc> GetPrototypeDocs(string idPrototype)
        {
            try
            {
                using (var db = new HKSupplyContext())
                {
                    return db.PrototypesDocs
                        .Where(a => a.IdPrototype.Equals(idPrototype))
                        .Include(b => b.DocType)
                        .OrderBy(c => c.CreateDate)
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
