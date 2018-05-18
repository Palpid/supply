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
    public class OcrdDapper : IOcrdService
    {
        public List<Ocrd> GetFactories()
        {
            try
            {
                try
                {
                    List<Ocrd> factories;

                    using (var connection = new SqlConnection(GlobalSetting.ConnectionString))
                    {
                        connection.Open();

                        factories = connection.Query<Ocrd>(Properties.Resources.QueryFactories).ToList();
                    }
                    return factories;
                }
                catch
                {
                    throw;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
