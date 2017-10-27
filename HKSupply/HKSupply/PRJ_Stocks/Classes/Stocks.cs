using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.PRJ_Stocks.Classes
{
    class Stocks
    {
        #region Def

        public enum StockMovementsType : int
        {
            Undefined = 0,
            Entry = 1,
            Exit = 2,
            Adjustment = 3,
            Reservation = 4,
            Release = 5,
            Transit = 6
        }

        public enum StockWareHousesType : int
        {
            Undefined = 0,
            OnHand = 1,
            Assigned = 2,
            Deliveries = 3,
            Transit = 4
        }

        public class LotData
        {
            public string idLot { get; set; }
            public int QttLot { get; set; }
        }
        public class OwnData
        {
            public string idOwner { get; set; }
            public int QttOwner { get; set; }
        }
        public class Asg
        {
            public string idOwner { get; set; }
            public string idLot { get; set; }
            public int Qtt { get; set; }
        }

        public class Owner
        {

            public string idOwner { get; set; }
            public string OwnerName { get; set; }
        }
        private List<Owner> _LstOwners = new List<Owner>();
        public List<Owner> LstOwners
        {
            get { return _LstOwners; }
            set { _LstOwners = value; }
        }
        public void AddOwner(Owner Own)
        {
            _LstOwners.Add(Own);
        }

        /// <summary>
        ///   Guarda les dades del stock de un item i els seus lots.
        ///   Es crea sempre un lot NULL per a les qtt que no tenen lot.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">No existeix el lot indicat;"</exception>
        public class Item
        {
            public string idItem { get; set; }
            private Dictionary<string, int> _DicLots = new Dictionary<string, int>(); // lot, qtt
            private Dictionary<string, int> _DicOwns = new Dictionary<string, int>(); // owner, qtt
            private List<Asg> _LstAsg = new List<Asg>(); // owner, lot, qtt

            /// <summary>
            ///    Modifica la QTT del lot. Si QttAfegir positiva incrementa, si negativa resta.
            ///    Si no s'indica lot es modifica el total, no es guarda el detall
            /// </summary>
            /// <param name="QttAfegir">La cantitat a incrementar/decrementar del lot</param>
            /// <param name="idlot"></param>
            public void AddLot(int QttAfegir, string idlot = "")
            {

                if (_DicLots.ContainsKey(idlot))
                {
                    //El lot existeix. El modifiquem si es pot. ( Qtt final>0 )
                    if ((_DicLots[idlot] + QttAfegir < 0)) throw new System.InvalidOperationException("Final quantity is negative. No negative stocks allowed.)");

                    _DicLots[idlot] += QttAfegir;
                    if (_DicLots[idlot] == 0) _DicLots.Remove(idlot);
                }
                else
                {
                    //El lot NO exiteix, es crea pero deixem INCREMENTAR
                    if ((QttAfegir < 0)) throw new System.InvalidOperationException("Final quantity is negative. No negative stocks allowed.)");
                    _DicLots.Add(idlot, QttAfegir);
                }
            }

            /// <summary>
            ///    Modifica la QTT a reservar (de la lliure). Si QttAfegir positiva incrementa, si negativa resta.
            ///    Si no s'indica Owner es modifica el total, no es guarda el detall
            /// </summary>
            /// <param name="QttAfegir">La cantitat a incrementar/decrementar del lot</param>            
            /// <param name="idOwner"></param>
            public void AddRes(int QttAfegir, string idOwner)
            {
                if (idOwner == "") throw new System.ArgumentException("IdOwner can't be empty.");

                if (_DicOwns.ContainsKey(idOwner))
                {
                    //El owner existeix. El modifiquem si es pot. ( Qtt final>0 )
                    if ((_DicOwns[idOwner] + QttAfegir < 0)) throw new System.InvalidOperationException("Final quantity is negative. No negative stocks allowed.)");

                    _DicOwns[idOwner] += QttAfegir;
                    if (_DicOwns[idOwner] == 0) _DicOwns.Remove(idOwner);
                }
                else
                {
                    //El owner NO exiteix, es crea pero deixem INCREMENTAR
                    if ((QttAfegir < 0)) throw new System.InvalidOperationException("Final quantity is negative. No negative stocks allowed.)");
                    _DicOwns.Add(idOwner, QttAfegir);
                }
            }

            /// <summary>
            ///   Retorna el total stock de tots el lots del item.
            /// </summary>
            public int TotalQTT
            {
                get
                {
                    int Total = 0;
                    foreach (int LV in _DicLots.Values) Total += LV;
                    return Total;
                }
                set { }
            }

            /// <summary>
            ///   Retorna el stock reservat
            /// </summary>
            public int TotalAssignedQTT
            {
                get
                {
                    int Total = 0;
                    foreach (int LV in _DicOwns.Values) Total += LV;
                    return Total;
                }
                set { }
            }

            /// <summary>
            ///   Retorna el stock reservat
            /// </summary>
            public int TotalFreeQTT
            {
                get
                {
                    return TotalQTT - TotalAssignedQTT;
                }
                set { }
            }

            /// <summary>
            /// Retorna la Qtt d'un lot concret
            /// </summary>
            /// <param name="idlot"></param>
            /// <returns></returns>
            public int QttLot(string idlot)
            {
                int Q = 0;
                if (_DicLots.ContainsKey(idlot))
                {
                    Q = _DicLots[idlot];
                }
                else
                {
                    throw new System.InvalidOperationException("Lot does '" + idlot + "'not exist)");
                }
                return Q;
            }

            /// <summary>
            /// Retorna la llista de QTTs en cada Lot. La que no te lot te idlot = ""
            /// </summary>                        
            public List<LotData> LstQttLots()
            {
                List<LotData> LLD = new List<LotData>();
                foreach (var V in _DicLots)
                {
                    LotData N = new LotData();
                    N.idLot = V.Key;
                    N.QttLot = V.Value;
                    LLD.Add(N);
                }
                return LLD;
            }

            /// <summary>
            /// Retorna la llista de QTTs en cada ownwe (reserva). La que no te lot te idlot = ""
            /// </summary>            
            public List<OwnData> LstQttOwners()
            {
                List<OwnData> LLD = new List<OwnData>();
                foreach (var V in _DicOwns)
                {
                    OwnData N = new OwnData();
                    N.idOwner = V.Key;
                    N.QttOwner = V.Value;
                    LLD.Add(N);
                }
                return LLD;
            }

            /// <summary>
            ///    Elimina dels diccionaris de lots els idlot amb QTT=0
            /// </summary>
            public void Purga()
            {
                List<string> LstLotBorrar = new List<string>();
                foreach (var LD in _DicLots) { if (LD.Value == 0) LstLotBorrar.Add(LD.Key); }
                foreach (string IdLot in LstLotBorrar) _DicLots.Remove(IdLot);
                LstLotBorrar = new List<string>();
                foreach (var LD in _DicOwns) { if (LD.Value == 0) LstLotBorrar.Add(LD.Key); }
                foreach (string IdOwn in LstLotBorrar) _DicOwns.Remove(IdOwn);
                List<Asg> LstAsgBorrar = new List<Asg>();
                foreach (var LA in _LstAsg) { if (LA.Qtt == 0) LstAsgBorrar.Add(LA); }
                foreach (Asg A in LstAsgBorrar) _LstAsg.Remove(A);
            }
        }

        public class StockItem
        {
            public StockItem(string idWarehouse = "", int idWareHouseType = 0, string WareHouseName = "",
                             string idItem = "", string Lot = "",
                             string idOwner = "",
                             int QTT = 0)
            {
                ware = new Warehouse();
                ware.idWareHouse = idWareHouse; ware.idWareHouseType = idWareHouseType; ware.Descr = WareHouseName;
                item = new Item();
                item.idItem = idItem;
                item.AddLot(QTT, Lot);
                if (idOwner != "") item.AddRes(QTT, idOwner);
            }

            public Warehouse ware { get; set; }
            public Item item { get; set; }

            public string idWareHouse
            {
                get
                { return (string)ware.idWareHouse; }
                set
                {
                    if (ware == null) ware = new Warehouse();
                    ware.idWareHouse = value;
                }
            }
            public string WareHouseName
            {
                get
                { return (string)ware.Descr; }
                set
                {
                    if (ware == null) ware = new Warehouse();
                    ware.Descr = value;
                }
            }
            public int idWareHouseType
            {
                get
                { return (int)ware.WareHouseType; }
                set
                {
                    if (ware == null) ware = new Warehouse();
                    ware.WareHouseType = (StockWareHousesType)value;
                }
            }
            public StockWareHousesType WareHouseType
            {
                get
                { return ware.WareHouseType; }
                set
                {
                    if (ware == null) ware = new Warehouse();
                    ware.WareHouseType = value;
                }
            }

            public string WareHouseTypeName
            {
                get
                { return ware.WareHouseType.ToString(); }
                set { }
            }

            public string idItem
            {
                get
                { return (string)item.idItem; }
                set
                {
                    if (item == null) item = new Item();
                    item.idItem = value;
                }
            }

            public int FreeStock
            {
                get { return item.TotalFreeQTT; }
            }

            public int AsgnStock
            {
                get { return item.TotalAssignedQTT; }
            }

            public List<LotData> LstLots
            {
                get { return item.LstQttLots(); }
            }

            public List<OwnData> LstOwnerss
            {
                get { return item.LstQttOwners(); }
            }
        }

        public class Warehouse
        {

            public string idWareHouse { get; set; }
            public string Descr { get; set; }
            public string Remarks { get; set; }
            public StockWareHousesType WareHouseType { get; set; }

            public int idWareHouseType
            {
                get
                { return (int)WareHouseType; }
                set
                {
                    WareHouseType = (StockWareHousesType)value;
                }
            }

            public string Key()
            {
                return idWareHouse + "|" + idWareHouseType;
            }

        }

        private List<Warehouse> _LstWarehouses = new List<Warehouse>();
        public List<Warehouse> LstWarehouses
        {
            get { return _LstWarehouses; }
            set { _LstWarehouses = value; }
        }

        public void AddWareHouse(Warehouse Ware)
        {
            _LstWarehouses.Add(Ware);
        }



        private List<StockMove> _LstStockMove = new List<StockMove>();
        private List<StockItem> _LstStock = new List<StockItem>();
        private List<StockItem> _LstStocksOrig = new List<StockItem>();


        public List<StockMove> LstStockMove
        {
            get { return _LstStockMove; }
            set { _LstStockMove = value; }
        }

        public List<StockItem> LstStocks
        {
            get { return _LstStock; }
            set { _LstStock = value; }
        }

        public class StockMove
        {
            public Warehouse WareORIG { get; set; }
            public Warehouse WareDEST { get; set; }
            public string idItem { get; set; }
            public string idLot { get; set; }
            public string idOwner { get; set; }
            public double QttMove { get; set; }

            public DateTime DTArrival { get; set; }
            public string Remarks { get; set; }
            public List<string> LstIdDocs { get; set; }

            public DateTime TimeStamp { get; set; }
            public string idUser { get; set; }

            public StockMovementsType MoveType { get; set; }
        }
        #endregion

        #region OperacionsSobreDomini

        /// <summary>
        ///  Afegim un STKitem a la llista STOKS. Usat en la càrrega only.
        /// </summary>
        /// <param name="STKi"></param>
        public void CargaSockItem(StockItem STKi)
        {
            _LstStock.Add(STKi);
        }

        /// <summary>
        ///    Aumenta o resta la QTT asginada del iditem/lot al magatzem. Si el Lot (dins item) es null o buit "" s'ignora el lot. 
        /// </summary>
        /// <param name="MoveType">Tipus moviement, per al log.</param>
        /// <param name="ware"> Magatzem en el que es modifica la qtt </param>
        /// <param name="Qtt">La Qtt. Positiva suma, negativa resta. Si queda negativa es llença una excepció</param>
        /// <param name="idtem"> idItem del que es modifica la QTT.</param>
        /// <param name="idLot"> idLot es null o "" s'ignora el lot.</param>
        /// <param name="idOwner">El client assignat al stock.</param>
        /// <param name="Remarks">Notes del usuari/sistema</param>
        /// <param name="LstidDoc">Llista iddocs associats al moviment</param>
        /// <param name="IdUser">IdUser logat que fa la operació. Per al log.</param>
        ///
        /// <exception cref="System.ArgumentException">IdWarehouse can't be empty/null</exception>
        /// <exception cref="System.ArgumentException">IdItem can't be empty/null</exception>
        /// <exception cref="System.InvalidOperationException">Not enough Stock on Warehouse</exception>
        public void AddSockItem(StockMovementsType MoveType, Warehouse ware, int Qtt, string idItem, string idLot = "", string idOwner = "", string Remarks = "", List<string> LstidDoc = null, string IdUser = "")
        {
            // -- Test Param --
            if (ware.idWareHouse == "" | ware.idWareHouse == null) throw new System.ArgumentException("IdWarehouse can't be empty/null");
            if (idItem == "" || idItem == null) throw new System.ArgumentException("IdItem can't be empty/null");

            // -- REGISTRE STOCK ----------------------------------------------------------------------------------------
            StockItem SI = GetStockItem(ware, idItem);
            if (SI == null)
            {
                if (Qtt < 0)
                    throw new System.InvalidOperationException("Not enough Stock on Warehouse Available: " + SI.item.TotalQTT + ", Requested: " + Qtt.ToString("###,###,###,###.00") + ")");
                else
                {
                    SI = new StockItem();
                    SI.ware.idWareHouse = ware.idWareHouse;
                    SI.ware.WareHouseType = ware.WareHouseType;
                    SI.item.idItem = idItem;
                    SI.item.AddLot(Qtt, idLot);
                    SI.item.AddRes(Qtt, idOwner);
                    _LstStock.Add(SI);
                }
            }
            else
            {
                if (SI.item.TotalQTT + Qtt < 0)
                    throw new System.InvalidOperationException("Not enough Stock on Origin Warehouse (Available: " + SI.item.TotalQTT.ToString("###,###,###,###.00") + ", Requested: " + Qtt.ToString("###,###,###,###.00") + ")");
                else
                    SI.item.AddLot(Qtt, idLot);
                SI.item.AddRes(Qtt, idOwner);
                //SI.item.Purga();
            }

            // -- REGISTRE MOVIMENTS ----------------------------------------------------------------------------------------
            StockMove SM = new StockMove();
            SM.WareORIG.idWareHouse = "";
            SM.WareORIG.WareHouseType = StockWareHousesType.Undefined;
            SM.WareDEST.idWareHouse = ware.idWareHouse;
            SM.WareDEST.WareHouseType = ware.WareHouseType;
            SM.idItem = idItem;
            SM.idLot = idLot;
            SM.idOwner = idOwner;
            SM.QttMove = Qtt;

            SM.DTArrival = System.DateTime.Now;
            SM.Remarks = Remarks;
            SM.LstIdDocs = new List<string>(); if (LstidDoc != null && LstidDoc.Count > 0) foreach (string iD in LstidDoc) SM.LstIdDocs.Add(iD);

            SM.MoveType = MoveType;
            SM.TimeStamp = System.DateTime.Now;
            SM.idUser = IdUser;

            _LstStockMove.Add(SM);
        }

        /// <summary>
        ///    ASigna STOCK a un client "idOwner". El sotck esta RESERVAT si te OWNER. Lluire si no.
        /// </summary>
        /// <param name="MoveType">Tipus moviement, per al log.</param>
        /// <param name="ware"> Magatzem en el que es modifica la qtt </param>
        /// <param name="Qtt">La Qtt. Positiva suma, negativa resta. Si queda negativa es llença una excepció</param>
        /// <param name="idOwner">El client assignat al stock.</param>
        /// <param name="idtem"> idItem del que es modifica la QTT.</param>
        /// <param name="idLot"> idLot es null o "" s'ignora el lot.</param>
        /// <param name="Remarks">Notes del usuari/sistema</param>
        /// <param name="LstidDoc">Llista iddocs associats al moviment</param>
        /// <param name="IdUser">IdUser logat que fa la operació. Per al log.</param>
        /// 
        /// <exception cref="System.ArgumentException">IdWarehouse can't be empty/null</exception>
        /// <exception cref="System.ArgumentException">IdItem can't be empty/null</exception>
        /// <exception cref="System.InvalidOperationException">Not enough Stock on Warehouse</exception>        
        public void AddReserva(StockMovementsType MoveType, Warehouse ware, int Qtt, string idOwner, string idItem, string idLot = "", string Remarks = "", List<string> LstidDoc = null, string IdUser = "")
        {
            // -- Test Param
            if (idItem == "" || idItem == null) throw new System.ArgumentException("IdItem can't be null");
            if (ware.idWareHouse == "" || ware.idWareHouse == null) throw new System.ArgumentException("idWarehouse can't be null");
            if (idOwner == "") throw new System.ArgumentException("idOwner can't be null");
            if (Qtt <= 0) throw new System.ArgumentException("Qtt to assing must be greather than 0", "Qtt");

            // -- REGISTRE STOCK ORIGEN ----------------------------------------------------------------------------------------
            StockItem SIO = GetStockItem(ware, idItem);  //Ha de ser STOCK Lliure (sense Owner)
            if (SIO == null)
                throw new System.InvalidOperationException("idITEM (" + idItem + ") & idLOT ( " + idLot + ") does not exist on Warehouse (" + ware.idWareHouse + "/" + ware.WareHouseType + ")");
            else
            {
                if (SIO.item.QttLot(idLot) < Qtt) throw
                     new System.InvalidOperationException("Not enough free stock on Warehouse of lot '" + idLot + "' (Available:" + SIO.item.QttLot(idLot).ToString("###,###,###,###.00") + ", Requested:" + Qtt.ToString("###,###,###,###.00") + ")");
                else
                    SIO.item.AddLot(Qtt, idLot); if (SIO.item.QttLot(idLot) == 0) SIO.item.Purga();

            }
            // -- REGISTRE STOCK DESTI (amb OwnerAsignat) ----------------------------------------------------------------------------------------
            StockItem SID = GetStockItem(ware, idItem);
            if (SID == null)
            {
                SID = new StockItem();
                SID.ware.idWareHouse = ware.idWareHouse;
                SID.ware.WareHouseType = ware.WareHouseType;
                SID.item.idItem = idItem;
                SID.item.AddLot(Qtt, idLot);
                SID.item.AddRes(Qtt, idLot);
                _LstStock.Add(SID);
            }
            else
                SID.item.AddLot(Qtt, idLot); if (SID.item.QttLot(idLot) == 0) SID.item.Purga();

            // -- REGISTRE MOVIMENTS ----------------------------------------------------------------------------------------
            StockMove SM = new StockMove();
            SM.WareORIG.idWareHouse = "";
            SM.WareORIG.WareHouseType = StockWareHousesType.Undefined;
            SM.WareDEST.idWareHouse = ware.idWareHouse;
            SM.WareDEST.WareHouseType = ware.WareHouseType;
            SM.idItem = idItem;
            SM.idLot = idLot;
            SM.idOwner = idOwner;
            SM.QttMove = Qtt;

            SM.DTArrival = System.DateTime.Now;
            SM.Remarks = Remarks;

            SM.LstIdDocs = new List<string>(); if (LstidDoc != null && LstidDoc.Count > 0) foreach (string iD in LstidDoc) SM.LstIdDocs.Add(iD);

            SM.MoveType = MoveType;
            SM.TimeStamp = System.DateTime.Now;
            SM.idUser = IdUser;

            _LstStockMove.Add(SM);
        }

        //        {
        //            // -- Test Param
        //            if (idItem == "" || idItem == null) throw new System.ArgumentException("IdItem can't be null");
        //            if (ware.idWareHouse == "" || ware.idWareHouse == null) throw new System.ArgumentException("idWarehouse can't be null");
        //            if (idOwner == "") throw new System.ArgumentException("idOwner can't be null");
        //            if (Qtt <= 0) throw new System.ArgumentException("Qtt to assing must be greather than 0", "Qtt");

        //            // -- REGISTRE STOCK ORIGEN ----------------------------------------------------------------------------------------
        //            StockItem SIO = GetStockItem(ware, idItem);  //Ha de ser STOCK Lliure (sense Owner)
        //            if (SIO == null)
        //                throw new System.InvalidOperationException("idITEM (" + idItem + ") & idLOT ( " + idLot + ") does not exist on Warehouse (" + ware.idWareHouse + "/" + ware.WareHouseType + ")");
        //            else
        //            {
        //                if (SIO.item.QttLot(idLot) < Qtt) throw
        //                     new System.InvalidOperationException("Not enough free stock on Warehouse of lot '"+idLot+"' (Available:" + SIO.item.QttLot(idLot).ToString("###,###,###,###.00") + ", Requested:" + Qtt.ToString("###,###,###,###.00") + ")");
        //                else
        //                    SIO.item.AddLot(Qtt, idLot); if (SIO.item.QttLot(idLot) == 0) SIO.item.PurgaLots();

        //            }
        //    // -- REGISTRE STOCK DESTI ----------------------------------------------------------------------------------------
        //    StockItem SID = GetStockItem(ware, idItem, idOwner);
        //            if (SID == null)
        //            {
        //                SID = new StockItem();
        //    SID.ware.idWareHouse = ware.idWareHouse;
        //                SID.ware.WareHouseType = ware.WareHouseType;
        //                SID.item.idItem = idItem;
        //                SID.item.AddLot(Qtt, idLot);
        //                SID.idOwner = idOwner;
        //                _LstStock.Add(SID);
        //            }
        //            else
        //                SID.QttStock += Qtt; if (SID.QttStock == 0) _LstStock.Remove(SID);

        //            // -- REGISTRE MOVIMENTS ----------------------------------------------------------------------------------------
        //            StockMove SM = new StockMove();
        //SM.WareORIG.idWareHouse = "";
        //            SM.WareORIG.WareHouseType = StockWareHousesType.Undefined;
        //            SM.WareDEST.idWareHouse = ware.idWareHouse;
        //            SM.WareDEST.WareHouseType = ware.WareHouseType;
        //            SM.Item.idItem = item.idItem;
        //            SM.Item.Lot = item.Lot;
        //            SM.idOwner = idOwner;
        //            SM.QttMove = Qtt;

        //            SM.DTArrival = System.DateTime.Now;
        //            SM.Remarks = remarks;

        //            SM.LstIdDocs = new List<string>(); if (LstidDoc != null && LstidDoc.Count > 0) foreach (string iD in LstidDoc) SM.LstIdDocs.Add(iD);

        //            SM.MoveType = MoveType;
        //            SM.TimeStamp = System.DateTime.Now;
        //            SM.idUser = IdUser;

        //            _LstStockMove.Add(SM);
        //        }


        public void MoveSockItem(StockMovementsType MoveType, Warehouse WareORIG, Warehouse WareDEST, int Qtt, string idItem, string idLot = "", string idOwner = "", string remarks = "", List<string> LstidDoc = null, string IdUser = "")
        {
            //    // -- Test Param
            //    if (item.idItem == "" | item.idItem == null) throw new System.ArgumentException("IdItem can't be null");
            //    if (WareORIG.idWareHouse == "" | WareORIG.idWareHouse == null) throw new System.ArgumentException("idWarehouseORIG can't be null");
            //    if (WareDEST.idWareHouse == "" | WareDEST.idWareHouse == null) throw new System.ArgumentException("idWarehouseDEST can't be null");
            //    if (Qtt <= 0) throw new System.ArgumentException("Qtt to move must be greather than 0", "Qtt");

            //    // -- REGISTRE STOCK ORIGEN ----------------------------------------------------------------------------------------
            //    StockItem SIO = GetStockItem(WareORIG, item, idOwner);
            //    if (SIO == null)
            //        throw new System.InvalidOperationException("idITEM (" + item.idItem + ") & idLOT ( " + item.Lot + ") & idOwner ( " + idOwner + " ) does not exist on WarehouseORIG (" + WareORIG.idWareHouse + "/" + WareORIG.WareHouseType + ")");
            //    else
            //    {
            //        if (SIO.QttStock < Qtt) throw
            //             new System.InvalidOperationException("Not enough Stock on Origin Warehouse (Available:" + SIO.QttStock.ToString("###,###,###,###.00") + ", Requested:" + Qtt.ToString("###,###,###,###.00") + ")");
            //        else
            //            SIO.QttStock -= Qtt; if (SIO.QttStock == 0) _LstStock.Remove(SIO);

            //    }
            //    // -- REGISTRE STOCK DESTI ----------------------------------------------------------------------------------------
            //    StockItem SID = GetStockItem(WareDEST, item, idOwner);
            //    if (SID == null)
            //    {
            //        SID = new StockItem();
            //        SID.Ware.idWareHouse = WareDEST.idWareHouse;
            //        SID.Ware.WareHouseType = WareDEST.WareHouseType;
            //        SID.Item.idItem = item.idItem;
            //        SID.Item.Lot = item.Lot;
            //        SID.idOwner = idOwner;
            //        SID.QttStock = Qtt;
            //        _LstStock.Add(SID);
            //    }
            //    else
            //        SID.QttStock += Qtt; if (SID.QttStock == 0) _LstStock.Remove(SID);

            //    // -- REGISTRE MOVIMENTS ----------------------------------------------------------------------------------------
            //    StockMove SM = new StockMove();
            //    SM.WareORIG.idWareHouse = WareORIG.idWareHouse;
            //    SM.WareORIG.WareHouseType = WareORIG.WareHouseType;
            //    SM.WareDEST.idWareHouse = WareDEST.idWareHouse;
            //    SM.WareDEST.WareHouseType = WareDEST.WareHouseType;
            //    SM.Item.idItem = item.idItem;
            //    SM.Item.Lot = item.Lot;
            //    SM.idOwner = idOwner;
            //    SM.QttMove = Qtt;

            //    SM.DTArrival = System.DateTime.Now;
            //    SM.Remarks = remarks;

            //    SM.LstIdDocs = new List<string>(); if (LstidDoc != null && LstidDoc.Count > 0) foreach (string iD in LstidDoc) SM.LstIdDocs.Add(iD);

            //    SM.MoveType = MoveType;
            //    SM.TimeStamp = System.DateTime.Now;
            //    SM.idUser = IdUser;

            //    _LstStockMove.Add(SM);
        }

        #endregion

        public void SetStockBase()
        {
            _LstStocksOrig.Clear();
            foreach (StockItem S in _LstStock) _LstStocksOrig.Add(S);
        }

        #region ConsultaDomini

        public Warehouse GetWareHouse(string WareHouseDescr)
        {
            if (_LstWarehouses != null && _LstWarehouses.Count > 0)
            {
                foreach (Warehouse wr in _LstWarehouses)
                {
                    if (wr.Descr == WareHouseDescr)
                    {
                        return wr;
                    }
                }
            }
            return null;
        }

        /// <summary>
        ///    Esto jace e erlksñdlkfgj ñsdlfkjg ñ
        /// </summary>
        /// <param name="idWareHouse"> tiene que ser > 0</param>
        /// <param name="WareTYpe"></param>
        /// <returns></returns>
        public Warehouse GetWareHouse(string idWareHouse, StockWareHousesType WareTYpe)
        {
            if (_LstWarehouses != null && _LstWarehouses.Count > 0)
            {
                foreach (Warehouse wr in _LstWarehouses)
                {
                    if (wr.idWareHouse == idWareHouse && wr.WareHouseType == WareTYpe)
                    {
                        return wr;
                    }
                }
            }
            return null;
        }

        public List<Warehouse> GetLstWarehouses()
        {
            List<Warehouse> LW = new List<Warehouse>();
            foreach (Warehouse W in _LstWarehouses) LW.Add(W);
            return LW;
        }

        public List<string> GetLstWarehouseNames()
        {
            List<string> LN = new List<string>();
            foreach (Warehouse W in _LstWarehouses) LN.Add(W.Descr);
            return LN;
        }

        public List<string> GetLstWarehousesTypes()
        {
            List<string> LWT = new List<string>();
            foreach (Warehouse W in _LstWarehouses)
            {
                if (!LWT.Contains(W.idWareHouseType.ToString())) LWT.Add(W.idWareHouseType.ToString());
            }
            return LWT;
        }

        /// <summary>
        /// Retorna el item amb la informació del stock en el magatzme
        /// </summary>
        /// <param name="ware"></param>
        /// <param name="idItem"></param>        
        /// <param name="idOwner"></param>
        /// <returns></returns>
        public StockItem GetStockItem(Warehouse ware, string idItem)
        {
            if (_LstStock != null && _LstStock.Count > 0)
            {
                foreach (StockItem si in _LstStock)
                {
                    if (si.ware.idWareHouse == ware.idWareHouse && si.ware.WareHouseType == ware.WareHouseType
                        && si.item.idItem == idItem)

                    {
                        return si;
                    }
                }
            }
            return null;
        }

        //public List<string> ListOwners()
        //{
        //   List<string> LO = new List<string>();
        //    foreach (StockItem SI in _LstStock)
        //    {
        //        if(!LO.Contains(SI.idOwner)) LO.Add(SI.idOwner);
        //    }
        //    return LO;

        //}

        //public List<StockItem> ListStockFilteredOR(Warehouse ware, string idItem , string idOwner)
        //{
        //    // Llista de stock items que compleix ALGUNA de les condicions
        //    List<StockItem> LI = new List<StockItem>();

        //    if (_LstStock != null && _LstStock.Count > 0)
        //    {
        //        foreach (StockItem si in _LstStock)
        //        {
        //            if (si.ware.idWareHouse == ware.idWareHouse || si.ware.WareHouseType == ware.WareHouseType 
        //                || ( si.item.idItem == idItem || idItem=="") )

        //            {
        //                foreach(AsgData AD in si.Lst)
        //                LI.Add(si);
        //            }
        //        }
        //    }
        //    return LI;
        //}

        #endregion


    }
}
