using HKSupply.DB;
using HKSupply.Exceptions;
using HKSupply.General;
using HKSupply.Helpers;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Mocking
{

    public class ResposeTest
    {
        public string ID_DOC { get; set; }
        public string ID_SUPPLY_DOC_TYPE { get; set; }
    }

    public class TestDb
    {
        ILog _log = LogManager.GetLogger(typeof(TestDb)); //variable para gestión del log de error

        public void CallTestQuery()
        {
            try
            {
                string idDoc = "PLCR201709006";
                string idType = "PL";

                using (HKSupplyContext db = new HKSupplyContext())
                {
                    using (var dbTrans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            TestQuery(db, idDoc, idType);
                            dbTrans.Commit();
                        }
                        catch
                        {
                            dbTrans.Rollback();
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public void TestQuery(HKSupplyContext db, string idDoc, string idDocType)
        {
            try
            {
                string query = $"SELECT ID_DOC, ID_SUPPLY_DOC_TYPE FROM DOC_HEAD WHERE  ID_DOC = '{idDoc}'";
                string query2 = $"SELECT ID_DOC, ID_SUPPLY_DOC_TYPE FROM DOC_HEAD WHERE  ID_SUPPLY_DOC_TYPE = '{idDocType}'";

                ResposeTest res1 = db.Database.SqlQuery<ResposeTest>(query).FirstOrDefault();
                List<ResposeTest> res2 = db.Database.SqlQuery<ResposeTest>(query2).ToList();
                DataTable dataTable = db.DataTable(query2);


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
