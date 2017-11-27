using HKSupply.DB;
using HKSupply.PRJ_Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Crea els contecx i accedeix a la clase que "ataca" la BD

namespace HKSupply.PRJ_Stocks.DB
{
    class Call_DB_Stocks
    {
        //ILog _log = LogManager.GetLogger(typeof(TestDb)); //variable para gestión del log de error

        public Classes.Stocks CallCargaStocks()
        {
            try
            {
                using (HKSupplyContext db = new HKSupplyContext())
                {
                    var BDSTK = new BD_Stocks();
                    Classes.Stocks CurrentStock = BDSTK.GetCurrentStock(db);
                    return CurrentStock;
                }
            }
            catch
            {
                throw;
            }
        }

        public void CallGuardarStocks(Classes.Stocks STK)
        {
            try
            {
                using (HKSupplyContext db = new HKSupplyContext())
                {

                    using (var dbTrans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var BDSTK = new BD_Stocks();

                            BDSTK.SaveCurrentStockMovs(db, STK);
                            dbTrans.Commit();

                        }
                        catch (Exception ex)
                        {
                            dbTrans.Rollback();
                            throw ex;
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
