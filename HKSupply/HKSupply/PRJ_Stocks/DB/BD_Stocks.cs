﻿using HKSupply.DB;
using HKSupply.Exceptions;
using HKSupply.General;
using HKSupply.Helpers;
using HKSupply.PRJ_Stocks;
using HKSupply.PRJ_Stocks.Classes;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// CRIDES A LA BD. El db Contex es un parametre que rebem

namespace HKSupply.PRJ_Stocks.DB
{
    class BD_Stocks
    {
        ILog _log = LogManager.GetLogger(typeof(BD_Stocks)); //variable para gestión del log de error

        public Classes.Stocks GetCurrentStock(HKSupplyContext db)
        {
            try
            {
                string sSQL = "";
                Classes.Stocks STK = new Classes.Stocks();

                //TODO: Procesat els moviments pendents (trasllats) per veure si han finalitzat.

                sSQL = $"SELECT idWarehouse,idWareHouseType,Descr,Remarks,idOwner FROM STK_WAREHOUSES ORDER BY idWarehouse";
                STK.LstWarehouses = db.Database.SqlQuery<Stocks.Warehouse>(sSQL).ToList();

                sSQL = $"SELECT STK.idWarehouse,STK.idWareHouseType,SW.Descr as WareHouseName, idItem, Lot, STK.idOwner,QTT as QttStock" +
                        " FROM STK_STOCK STK" +
                        " left join STK_WAREHOUSES SW on STK.idWarehouse = SW.idWarehouse and STK.idWareHouseType = SW.idWareHouseType" +
                        " ORDER BY Descr";
                STK.LstStocks = db.Database.SqlQuery<Stocks.StockItem>(sSQL).ToList();

                STK.SetStockBase(); //- Copiem el stock actual a la llsita StockBase per establir el punt base on es guarda.

                //string query = $"SELECT ID_DOC, ID_SUPPLY_DOC_TYPE FROM DOC_HEAD WHERE  ID_DOC = '{idDoc}'";

                //ResposeTest res1 = db.Database.SqlQuery<ResposeTest>(query).FirstOrDefault();
                //List<ResposeTest> res2 = db.Database.SqlQuery<ResposeTest>(query2).ToList();
                //DataTable dataTable = db.DataTable(query2);

                return STK;
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
