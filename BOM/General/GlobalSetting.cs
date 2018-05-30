using BOM.Classes;
using BOM.Services.Implementations;
using BOM.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOM.General
{
    public sealed class GlobalSetting
    {
        private static readonly Lazy<GlobalSetting> lazy = new Lazy<GlobalSetting>(() => new GlobalSetting());

        public static GlobalSetting Instance { get { return lazy.Value; } }

        public GlobalSetting()
        {

        }

        #region Private Members
        static OitmDapper _oitmDapper;
        static BomDapper _bomDapper;
        static OcrdDapper _ocrdDapper;
        #endregion

        #region Public Properties

        public static string DbServer { get { return ConfigurationManager.AppSettings["DbServer"].ToString(); } }

        public static string DbSapName { get { return ConfigurationManager.AppSettings["DbSapName"].ToString(); } }

        public static string UserLogged { get { return Environment.UserName.ToLower(); } }

        public static string ConnectionString
        {
            get { return GetConnectionString(); }
        }

        public static IOitmService OitmService
        {
            get
            {
                if (_oitmDapper == null)
                    _oitmDapper = new OitmDapper();
                return _oitmDapper;
            }
        }

        public static IBomService BomService
        {
            get
            {
                if (_bomDapper == null)
                    _bomDapper = new BomDapper();
                return _bomDapper;
            }
        }

        public static IOcrdService OcrdService
        {
            get
            {
                if (_ocrdDapper == null)
                    _ocrdDapper = new OcrdDapper();
                return _ocrdDapper;
            }
        }

        #endregion

        #region Private Methods
        private static string GetConnectionString()
        {
            string connectionString = $"Data Source={DbServer};Initial Catalog={DbSapName};User Id={Constants.DB_USER};Password={Constants.DB_PASSWORD};MultipleActiveResultSets=true;";
            return connectionString;
        }
        #endregion


    }
}
