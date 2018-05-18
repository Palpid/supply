using BOM.Classes;
using BOM.General;
using BOM.Models;
using BOM.Services.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOM.Services.Implementations
{
    public class OitmDapper : IOitmService
    {
        public List<OitmExt> GetItems()
        {
            try
            {
                List<OitmExt> items;

                using (var connection = new SqlConnection(GlobalSetting.ConnectionString))
                {
                    connection.Open();

                    items = connection.Query<OitmExt, Model, OitmExt>(
                        Properties.Resources.QueryItems,
                        (oitm, model) =>
                        {
                            oitm.Model = model;
                            return oitm;
                        }, splitOn: "Code"
                        ).Distinct().ToList();
                }
                return items;
            }
            catch
            {
                throw;
            }
        }

        public List<OitmExt> GetPossibleItemsForBom()
        {
            try
            {
                List<OitmExt> items;

                using (var connection = new SqlConnection(GlobalSetting.ConnectionString))
                {
                    connection.Open();
                    items = connection.Query<OitmExt>(Properties.Resources.QueryPossibleItemsForBom).ToList();
                }
                return items;
            }
            catch
            {
                throw;
            }
        }
    }
}
