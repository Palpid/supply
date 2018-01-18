using HKSupply.DB;
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
        private class LotItemDB
        {
            public LotItemDB(string idItem, string idLot, string idWare, int idWareType, string wareName, decimal qtt)
            {
                IdItem = idItem;
                IdLot = idLot;
                IdWare = idWare;
                IdWareType = idWareType;
                WareName = wareName;
                Qtt = qtt;
            }
            public LotItemDB()
            {
                IdItem = "";
                IdLot = "";
                IdWare = "";
                IdWareType = 0;
                WareName = "";
                Qtt = 0;
            }

            public string IdItem { get; set; }
            public string IdLot { get; set; }
            public string IdWare { get; set; }
            public int IdWareType { get; set; }
            public string WareName { get; set; }
            public decimal Qtt { get; set; }
        }

        ILog _log = LogManager.GetLogger(typeof(BD_Stocks)); //variable para gestión del log de error
        /// <summary>
        ///  Carrega les dades de Stock desde la BD. 
        /// </summary>
        /// <param name="db">La conexió de la BD gestionda per el EF</param>
        /// <param name="idwarehouse">Filtre opcional. Si s'indica es carrega només el magatzem indicat</param>
        /// <returns></returns>
        public Classes.Stocks GetCurrentStock(
                     HKSupplyContext db, 
                     string idwarehouse = "" )
        {
            //ResposeTest res1 = db.Database.SqlQuery<ResposeTest>(query).FirstOrDefault();
            //List<ResposeTest> res2 = db.Database.SqlQuery<ResposeTest>(query2).ToList();
            //DataTable dataTable = db.DataTable(query2);

            // Nom dels camps HAN DE COINCIDIR amb el nom dels atributs de la classe que carreguem Alerta Mayúscules.

            try
            {
                string sSQL = "";
                Classes.Stocks STK = new Classes.Stocks();

                //TODO: Procesat els moviments pendents (trasllats) per veure si han finalitzat.

                // MAGATZEMS
                sSQL = $"SELECT idWarehouse,idWareHouseType,Descr,Remarks,idOwner FROM STK_WAREHOUSES ORDER BY idWarehouse";
                STK.LstWarehouses = db.Database.SqlQuery<Stocks.Warehouse>(sSQL).ToList();

                // ITEMS                
                sSQL = $"SELECT ID_ITEM_BCN, ITEM_DESCRIPTION, ID_ITEM_GROUP FROM V_ALL_ITEMS ORDER BY ID_ITEM_BCN";
                STK.LstItemsBcn = db.Database.SqlQuery<Stocks.ItemBcn>(sSQL).ToList();

                // OWNERS
                sSQL = $"SELECT distinct ID_CUSTOMER as idOwner, CUSTOMER_NAME as OwnerName FROM CUSTOMERS";
                sSQL += " union ";
                sSQL += " SELECT distinct ID_SUPPLIER as idOwner, SUPPLIER_NAME as OwnerName FROM SUPPLIERS";
                sSQL += " Order By idOwner";
                STK.LstOwners = db.Database.SqlQuery<Stocks.Owner>(sSQL).ToList();

                // STOCKS
                sSQL = $"SELECT STK.idWarehouse,STK.idWareHouseType,SW.Descr as WareHouseName, idItem, STK.idOwner as idOwner,QTT as QttMove";
                sSQL += " FROM STK_STOCK STK";
                sSQL += " left join STK_WAREHOUSES SW on STK.idWarehouse = SW.idWarehouse and STK.idWareHouseType = SW.idWareHouseType";
                if (idwarehouse != "") sSQL += " Where idWarehouse='" + idwarehouse.Trim() + "'";
                sSQL += " ORDER BY Descr";
                List<Stocks.StockMove> DTRows = db.Database.SqlQuery<Stocks.StockMove>(sSQL).ToList();

                foreach (Stocks.StockMove rec in DTRows)
                {
                    string idWare = rec.Ware.idWareHouse;
                    int idWareType = rec.Ware.idWareHouseType;
                    string WaraName = rec.Ware.Descr;
                    string IdIt = rec.idItem;
                    string Ow = rec.idOwner;
                    decimal QTT = rec.QttMove;
                    Classes.Stocks.StockItem STKi = new Stocks.StockItem(idWare, idWareType, WaraName, IdIt, Ow, QTT);
                    STK.CargaSockItem(STKi);
                }


                
                // LOTS
                sSQL = $"SELECT idItem as IdItem, STK.idWarehouse as IdWare, STK.idWareHouseType as IdWareType, SW.Descr as WareName, Lot as IdLot, QTT as Qtt";
                sSQL += " FROM STK_LOTS STK";
                sSQL += " left join STK_WAREHOUSES SW on STK.idWarehouse = SW.idWarehouse and STK.idWareHouseType = SW.idWareHouseType";
                if (idwarehouse != "") sSQL += " Where STK.idWarehouse='" + idwarehouse.Trim() + "'";
                sSQL += " ORDER BY idItem,Lot";
                List<LotItemDB> DTRows2 = db.Database.SqlQuery<LotItemDB>(sSQL).ToList();

                // inicializamos Asignacion Lote Vacio al valor del STK
                //foreach (Stocks.StockItem stki in STK.LstStocks)
                //{
                //   stki.item._Lots.Add("", stki.TotalStock);
                //}

                foreach (LotItemDB rec in DTRows2)
                {
                    Stocks.Warehouse WR = new Stocks.Warehouse();
                    WR.idWareHouse = rec.IdWare;
                    WR.idWareHouseType = rec.IdWareType;
                    WR.Descr = rec.WareName;
                    STK.AsgnLotItemCarga(
                        WareORIG: WR,
                        idItem: rec.IdItem,
                        Qtt: rec.Qtt,
                        idLot: rec.IdLot);
                }


                //- Copiem el stock actual a la llsita StockBase per establir el punt base on es guarda.
                STK.SetStockBase();
                

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

        public bool SaveCurrentStockMovs(HKSupplyContext db, Stocks STK)
        {
            try
            {

                // Contrastrem STK actual de la BD amb STK esperat per detectar si hi ha canvis externs fets per altres usuaris/instancies
                // Només comparem els magatzems ORIGEN dels articles que tenen moviment
                // L'estat esperat de la BD es a 

                // Creem la llista dels stocks a verificar
                List<Stocks.StockMove> StkAVerificar = new List<Stocks.StockMove>();
                foreach (Stocks.StockMove SM in STK.LstStockMove)
                {
                    string idWareO = SM.Ware.idWareHouse;
                    int idWareTypeO = SM.Ware.idWareHouseType;
                    string WaraName = SM.Ware.Descr;
                    string IdIt = SM.idItem;
                    string Ow = SM.idOwner;
                    string Lot = SM.idLot;
                    decimal QTT = SM.QttMove;
                    Stocks.StockMovementsType MType = SM.MoveType;
                    Stocks.StockMove STKm = new Stocks.StockMove(MType, idWareO, WaraName, idWareTypeO, IdIt, Ow,Lot, QTT);
                    StkAVerificar.Add(STKm);
                }

                // Carreguem STKBD 
                Stocks STKBD = new Stocks();
                STKBD = GetCurrentStock(db);

                // Comparem STKBD amb STK(original)
                foreach (Stocks.StockMove SIv in StkAVerificar)
                {
                    Stocks.StockItem siEsp = STK.GetStockItemOrig(SIv.Ware, SIv.idItem);
                    if (siEsp == null)
                    {
                        // No Existeix. No hauria d'exisir a destí
                        Stocks.StockItem siBD = STKBD.GetStockItem(SIv.Ware, SIv.idItem);
                        if (siBD != null) throw new System.InvalidOperationException("Stock found for item '" + SIv.idItem + "' at '" + SIv.Ware.Descr + "' warehouse (" + siEsp.item.TotalQTT + "). Expected stock = 0");
                    }
                    else
                    {
                        Stocks.StockItem siBD = STKBD.GetStockItem(SIv.Ware, SIv.idItem);
                        if (siBD == null) throw new System.InvalidOperationException("No stock found for item '" + SIv.idItem + "' at '" + SIv.Ware.Descr + "' warehouse. Expected stock = " + siEsp.item.TotalQTT);

                        foreach (Stocks.DetAsg DA in siEsp.LstDetRes)
                        {
                            decimal ValStkEsp = DA.Qtt;
                            decimal ValStkBD = siBD.item.StkOwner(DA.idOwner);
                            if (ValStkEsp != ValStkBD)
                                throw new System.InvalidOperationException("Expeted stock don't match. Item '" + SIv.idItem + "' assigned to '" + DA.idOwner + "' at '" + SIv.Ware.Descr + "' warehouse. Expected stock = " + ValStkEsp + " found = " + ValStkBD);
                        }
                    }
                }

                // Es Ok Guardar, el stock esperat a la BD es el que tenim. 
                // Fem un recorregut sobre els moviments per a modificar el que toca.


                // NO ES FA UNA TRANSACCIÓ. La funció esta pensada per a ser cridada dins una trasacció ja creada. 
                string sSQL = "";
                Guid G = Guid.NewGuid();

                foreach (Stocks.StockMove SM in STK.LstStockMove)
                {
                    string idWareO = SM.Ware.idWareHouse;
                    int idWareTypeO = SM.Ware.idWareHouseType;
                    string IdIt = SM.idItem;
                    string Ow = SM.idOwner;
                    string Lt = SM.idLot;
                    decimal QTT = SM.QttMove;
                    string Rem = SM.Remarks;
                    string Usr = SM.idUser;
                    List<string> Ldocs = SM.LstIdDocs;

                    DateTime DA = SM.DTArrival;
                    Stocks.StockMovementsType TypeM = SM.MoveType;
                    
                    // -- REGISTRE MOVIMENTS ASOCIATS
                    sSQL = $"INSERT INTO STK_MOVEMENTS (";
                    sSQL += $"idMoveType,idWareHouse,idWareHouseType,idItem,idOwner,Lot,QTT,MovDate,ArrivalDate,Remarks,idUser,TimeStamp,GUID";
                    sSQL += $") VALUES (";
                    sSQL += $"{SM.idMovementType},'{idWareO}',{idWareTypeO},'{IdIt}','{Ow}','',{QTT.ToString(System.Globalization.CultureInfo.InvariantCulture)},";
                    sSQL += $"GETDATE(),convert(datetime,'{DA.ToString("yyyy-MM-dd")}',111),'{Rem}','{Usr}',GETDATE(),'{G.ToString()}'";
                    sSQL += $")";
                    sSQL += "; SELECT @@IDENTITY;";
                    //db.Databas e.ExecuteSqlCommand(sSQL);
                    decimal idMov = db.Database.SqlQuery<decimal>(sSQL).First();
                    
                    int NumRegs = 0;
                    // -- REGITRE STOCKS
                    // Si Existeix update, si no es crea
                    // existeix si count<>0
                    sSQL = $"SELECT COUNT(*) FROM STK_STOCK ";
                    sSQL += $"WHERE idWareHouse ='{idWareO}' AND idWareHouseType ={idWareTypeO} AND idItem ='{IdIt}' AND idOwner='{Ow}'";
                    NumRegs = db.Database.SqlQuery<int>(sSQL).First();
                    if (NumRegs > 0)
                    {
                        sSQL = $"UPDATE STK_STOCK SET QTT=QTT+({QTT.ToString(System.Globalization.CultureInfo.InvariantCulture)}) ";
                        sSQL += $"WHERE idWareHouse ='{idWareO}' AND idWareHouseType ={idWareTypeO} AND idItem ='{IdIt}' AND idOwner='{Ow}'";
                    }
                    else
                    {
                        sSQL = $"INSERT INTO STK_STOCK (";
                        sSQL += $"idWareHouse,idWareHouseType,idItem,idOwner,QTT";
                        sSQL += $") VALUES (";
                        sSQL += $"'{idWareO}',{idWareTypeO},'{IdIt}','{Ow}',{QTT.ToString(System.Globalization.CultureInfo.InvariantCulture)}";
                        sSQL += $")";
                    }
                    db.Database.ExecuteSqlCommand(sSQL);

                    // -- REGITRE LOTS
                    // Si Existeix update, si no es crea
                    // existeix si count<>0
                    sSQL = $"SELECT COUNT(*) FROM STK_LOTS ";
                    sSQL += $"WHERE idWareHouse ='{idWareO}' AND idWareHouseType ={idWareTypeO} AND idItem ='{IdIt}' AND LOt='{Lt}'";
                    NumRegs = db.Database.SqlQuery<int>(sSQL).First();
                    if (NumRegs > 0)
                    {
                        sSQL = $"UPDATE STK_LOTS SET QTT=QTT+({QTT.ToString(System.Globalization.CultureInfo.InvariantCulture)}) ";
                        sSQL += $"WHERE idWareHouse ='{idWareO}' AND idWareHouseType ={idWareTypeO} AND idItem ='{IdIt}' AND Lot='{Lt}'";
                    }
                    else
                    {
                        sSQL = $"INSERT INTO STK_LOTS (";
                        sSQL += $"idWareHouse,idWareHouseType,idItem,Lot,QTT";
                        sSQL += $") VALUES (";
                        sSQL += $"'{idWareO}',{idWareTypeO},'{IdIt}','{Lt}',{QTT.ToString(System.Globalization.CultureInfo.InvariantCulture)}";
                        sSQL += $")";
                    }
                    db.Database.ExecuteSqlCommand(sSQL);

                    // -- LLISTA DOCS ASOCIATS                     
                    //G es el GUID del moviment
                    if(Ldocs!=null && Ldocs.Count>0)
                    {
                        // revisem la llista per a que no tingui repetits
                        List<string> LstDocsNoRepes = new List<string>();
                        foreach (string iddoc in Ldocs)
                        {
                            if (!LstDocsNoRepes.Contains(iddoc)) LstDocsNoRepes.Add(iddoc);
                        }
                        sSQL = $"DELETE FROM STK_MOVEMENTS_DOCS WHERE GUID_Move={G.ToString()}";
                        foreach(string iddoc in LstDocsNoRepes)
                        {
                            sSQL = $"INSERT INTO STK_MOVEMENTS_DOCS (";
                            sSQL += $"idMove,idDoc";
                            sSQL += $") VALUES (";
                            sSQL += $"{idMov},'{iddoc}'";
                            sSQL += $")";
                            db.Database.ExecuteSqlCommand(sSQL);
                        }
                    }
                }
                                
                // Purga STK = 0;
                sSQL = $"DELETE FROM STK_STOCK WHERE QTT<=0";
                db.Database.ExecuteSqlCommand(sSQL);
                sSQL = $"DELETE FROM STK_LOTS WHERE QTT<=0";
                db.Database.ExecuteSqlCommand(sSQL);

                // Esborrem els moviemnts.  
                STK.LstStockMove.Clear();
                                
                // NO CARREGUEM STOKS DE NOU PERQUE HI HA UNA TRANSACCIO OBERTA. Ho farem despres de guardar.
                //STK = GetCurrentStock(db);
                STK.SetStockBase();
                
                return true; // si arriba aqui es que tot es ok. Si no ha petat el SQL i ha sortit per el catch

            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);                
                throw ex;
            }
        }
    }
}
