using HKSupply.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKSupply.Forms.Supply.Dashboard;
using log4net;
using System.Data.SqlClient;
using HKSupply.Exceptions;
using HKSupply.General;
using HKSupply.DB;

namespace HKSupply.Services.Implementations
{
    public class EFSupplyDashboard : ISupplyDashboard
    {
        ILog _log = LogManager.GetLogger(typeof(EFSupplyDashboard));

        List<AuxDashboardQPStoredProcedure> ISupplyDashboard.GetDashboardQP(string queryType, string factories, string weeks, string itemGroup)
        {
            try
            {
                if (string.IsNullOrEmpty(queryType))
                    throw new ArgumentNullException(nameof(queryType));

                if (string.IsNullOrEmpty(factories))
                    throw new ArgumentNullException(nameof(factories));

                if (string.IsNullOrEmpty(weeks))
                    throw new ArgumentNullException(nameof(weeks));

                if (string.IsNullOrEmpty(itemGroup))
                    throw new ArgumentNullException(nameof(itemGroup));


                using (var db = new HKSupplyContext())
                {

                    SqlParameter param1 = new SqlParameter("@pQueryType", queryType);
                    SqlParameter param2 = new SqlParameter("@pFactories ", factories); 
                    SqlParameter param3 = new SqlParameter("@pWeeks", weeks);
                    SqlParameter param4 = new SqlParameter("@pItemGroup", itemGroup);

                    List<AuxDashboardQPStoredProcedure> data = db.Database
                        .SqlQuery<AuxDashboardQPStoredProcedure>("SUPPLY_DASHBOARD_QP @pQueryType, @pFactories, @pWeeks, @pItemGroup", param1, param2, param3, param4).ToList();

                    return data;
                }

            }
            catch (ArgumentNullException nrex)
            {
                _log.Error(nrex.Message, nrex);
                throw nrex;
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
