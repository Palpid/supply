using HKSupply.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKSupply.Models.Supply;
using log4net;
using HKSupply.Exceptions;
using HKSupply.General;
using System.Data.SqlClient;
using HKSupply.DB;

namespace HKSupply.Services.Implementations
{
    public class EFDocHeadAttachFile : IDocHeadAttachFile
    {
        ILog _log = LogManager.GetLogger(typeof(EFDocHeadAttachFile));

        public DocHeadAttachFile AddDocHeadAttachFile(DocHeadAttachFile attachFile)
        {
            try
            {
                if (attachFile == null)
                    throw new ArgumentNullException();

                using (var db = new HKSupplyContext())
                {
                    db.DocHeadAttachFiles.Add(attachFile);
                    db.SaveChanges();

                    db.Entry(attachFile).GetDatabaseValues();
                    return attachFile;

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

        public List<DocHeadAttachFile> GetDocHeadAttachFile(string idDoc)
        {
            try
            {
                if (string.IsNullOrEmpty(idDoc))
                    throw new ArgumentNullException();

                using (var db = new HKSupplyContext())
                {
                    return db.DocHeadAttachFiles
                        .Where(a => a.IdDocHead.Equals(idDoc))
                        .OrderBy(b => b.CreateDate)
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
