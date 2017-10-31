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
            Transit = 6,
            Movement = 7
        }

        public enum StockWareHousesType : int
        {
            Undefined = 0,
            OnHand = 1,
            Assigned = 2,
            Deliveries = 3,
            Transit = 4
        }

        // owners
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

        // warehouses
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

        public class DetAsg
        {
            public string idOwner { get; set; }
            public int Qtt { get; set; }
        }

        /// <summary>
        ///   Guarda les dades del stock de un item i els seus lots.
        ///   Es crea sempre un lot NULL per a les qtt que no tenen lot.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">No existeix el lot indicat;"</exception>
        public class Item
        {
            public string idItem { get; set; }
            public Dictionary<String, int> _Resr = new Dictionary<String, int>(); // (ownner, qtt)

            ///    Modifica la QTT del stock. Si QttAfegir positiva incrementa, si negativa resta.
            public int AddStk(int QttAfegir, string idOwner)
            {
                int Variacio = 0;
                int QTTAct = 0;
                if (!_Resr.ContainsKey(idOwner)) _Resr.Add(idOwner, 0);
                QTTAct = _Resr[idOwner];
                if ((QTTAct + QttAfegir) < 0) throw new System.InvalidOperationException("Final quantity is negative. No negative stocks allowed.)");
                _Resr[idOwner] += QttAfegir;
                Variacio = QttAfegir;
                if (_Resr[idOwner] == 0) _Resr.Remove(idOwner);
                return Variacio;
            }



            /// <summary>
            ///   Retorna el total stock de tots el lots del item.
            /// </summary>
            public int TotalQTT
            {
                get
                {
                    int Total = 0;
                    foreach (int V in _Resr.Values) Total += V;
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
                    foreach (var Q in _Resr) if (Q.Key != "") Total += Q.Value;
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

            public List<DetAsg> LstAsigs
            {
                get
                {
                    List<DetAsg> LD = new List<DetAsg>();
                    foreach (var R in _Resr)
                    {
                        DetAsg nD = new DetAsg();
                        nD.idOwner = R.Key;
                        nD.Qtt = R.Value;
                        LD.Add(nD);
                    }
                    return LD;
                }
                set { }
            }

            public int StkOwner(string idOwner)
            {
                if (_Resr.ContainsKey(idOwner))
                    return _Resr[idOwner];
                else
                    return 0;
            }

        }

        public class StockItem
        {
            public StockItem(string idWarehouse = "", int idWareHouseType = 0, string WareHouseName = "",
                             string idItem = "", string idOwner = "", int QTT = 0)
            {
                ware = new Warehouse();
                ware.idWareHouse = idWarehouse; ware.idWareHouseType = idWareHouseType; ware.Descr = WareHouseName;
                item = new Item();
                item.idItem = idItem;

                item.AddStk(QTT, idOwner);
                item.LstAsigs = new List<DetAsg>();
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

            public List<string> LstidOwners()
            {
                List<string> Lid = new List<string>();
                foreach (var A in item._Resr)
                {
                    if (!Lid.Contains(A.Key)) Lid.Add(A.Key);
                }
                return Lid;
            }

            public List<DetAsg> LstDetRes
            {
                get
                {
                    List<DetAsg> Ld = new List<DetAsg>();
                    foreach (var A in item._Resr)
                    {
                        DetAsg nd = new DetAsg();
                        nd.idOwner = A.Key;
                        nd.Qtt = A.Value;
                        Ld.Add(nd);
                    }
                    return Ld;
                }
                set { }
            }

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
            public StockMove(StockMovementsType MovType, string idWR, string DercsWR, int idWareType, string iditem, string owner, int qttmove)
            {
                Ware = new Warehouse();
                Ware.idWareHouse = idWR;
                Ware.Descr = DercsWR;
                Ware.idWareHouseType = idWareType;

                LstIdDocs = new List<string>();

                idItem = iditem;
                idOwner = owner;
                QttMove = qttmove;
                DTArrival = DTArrival;
            }

            public StockMove()
            {
                Ware = new Warehouse();
                LstIdDocs = new List<string>();
            }

            public Warehouse Ware { get; set; }
            public string idItem { get; set; }
            public string idLot { get; set; }
            public string idOwner { get; set; }
            public int QttMove { get; set; }

            public DateTime DTArrival { get; set; }
            public string Remarks { get; set; }
            public List<string> LstIdDocs { get; set; }

            public DateTime TimeStamp { get; set; }
            public string idUser { get; set; }

            public StockMovementsType MoveType { get; set; }

            public string idWareHouse
            {
                get
                { return (string)Ware.idWareHouse; }
                set
                {
                    if (Ware == null) Ware = new Warehouse();
                    Ware.idWareHouse = value;
                }
            }
            public string WareHouseName
            {
                get
                { return (string)Ware.Descr; }
                set
                {
                    if (Ware == null) Ware = new Warehouse();
                    Ware.Descr = value;
                }
            }
            public int idWareHouseType
            {
                get
                { return (int)Ware.WareHouseType; }
                set
                {
                    if (Ware == null) Ware = new Warehouse();
                    Ware.WareHouseType = (StockWareHousesType)value;
                }
            }
            public StockWareHousesType WareHouseType
            {
                get
                { return Ware.WareHouseType; }
                set
                {
                    if (Ware == null) Ware = new Warehouse();
                    Ware.WareHouseType = value;
                }
            }
            public string WareHouseTypeName
            {
                get
                { return Ware.WareHouseType.ToString(); }
                set { }
            }

            public int idMovementType
            {
                get
                { return (int)MoveType; }
                set
                {
                    MoveType = (StockMovementsType)value;
                }
            }

        }
        #endregion

        #region OperacionsSobreDomini

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
                    if (si.ware.idWareHouse == ware.idWareHouse && si.ware.WareHouseType == ware.WareHouseType && si.item.idItem == idItem)

                    {
                        return si;
                    }
                }
            }
            return null;
        }

        public StockItem GetStockItemOrig(Warehouse ware, string idItem)
        {
            if (_LstStocksOrig != null && _LstStocksOrig.Count > 0)
            {
                foreach (StockItem si in _LstStocksOrig)
                {
                    if (si.ware.idWareHouse == ware.idWareHouse && si.ware.WareHouseType == ware.WareHouseType && si.item.idItem == idItem)

                    {
                        return si;
                    }
                }
            }
            return null;
        }

        /// <summary>
        ///  Afegim un STKitem a la llista STOKS. Usat en la càrrega only.
        /// </summary>
        /// <param name="STKi"></param>
        public void CargaSockItem(StockItem STKi)
        {
            StockItem S = GetStockItem(STKi.ware, STKi.idItem);
            if (S == null)
                _LstStock.Add(STKi);
            else
            {
                foreach (var A in STKi.item._Resr)
                {
                    S.item.AddStk(A.Value, A.Key);
                }

            }
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
                    SI.item.AddStk(Qtt, idOwner);
                    _LstStock.Add(SI);
                }
            }
            else
            {
                if (SI.item.TotalQTT + Qtt < 0)
                    throw new System.InvalidOperationException("Not enough Stock on Origin Warehouse (Available: " + SI.item.TotalQTT.ToString("###,###,###,###.00") + ", Requested: " + Qtt.ToString("###,###,###,###.00") + ")");
                else
                    SI.item.AddStk(Qtt, idOwner);
            }

            // -- REGISTRE MOVIMENTS ----------------------------------------------------------------------------------------
            StockMove SM = new StockMove();
            SM.Ware.idWareHouse = "";
            SM.Ware.WareHouseType = StockWareHousesType.Undefined;
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

        public void MoveSockItem(StockMovementsType MoveType, Warehouse WareORIG, Warehouse WareDEST, int Qtt, string idItem, string idOwner, string remarks = "", List<string> LstidDoc = null, string IdUser = "")
        {
            // -- Test Param
            if (idItem == "" | idItem == null) throw new System.ArgumentException("IdItem can't be null");
            if (WareORIG.idWareHouse == "" | WareORIG.idWareHouse == null) throw new System.ArgumentException("idWarehouseORIG can't be null");
            if (WareDEST.idWareHouse == "" | WareDEST.idWareHouse == null) throw new System.ArgumentException("idWarehouseDEST can't be null");
            if (Qtt <= 0) throw new System.ArgumentException("Qtt to move must be greather than 0", "Qtt");


            // -- LOCALITZA STOCK ORIGEN ----------------------------------------------------------------------------------------
            StockItem SIO = GetStockItem(WareORIG, idItem);
            if (SIO == null)
                throw new System.InvalidOperationException("idITEM (" + idItem + ") does not exist on WarehouseORIG (" + WareORIG.idWareHouse + "/" + WareORIG.WareHouseType + ")");

            int QAct = SIO.item.StkOwner(idOwner);
            if (QAct < Qtt) throw new System.InvalidOperationException("Quantity to move (" + Qtt + ") is greather than existing quantity (" + QAct + ")");

            // -- LOCALITZA STOCK DESTI ----------------------------------------------------------------------------------------
            StockItem SID = GetStockItem(WareDEST, idItem);
            if (SID == null)
            {
                // Creem una entrada nova}
                SID = new StockItem();
                SID.ware.idWareHouse = WareDEST.idWareHouse;
                SID.ware.Descr = WareDEST.Descr;
                SID.ware.WareHouseType = WareDEST.WareHouseType;
                SID.item.idItem = idItem;
                SID.item.AddStk(0, idOwner);
                _LstStock.Add(SID);
            }
            SIO.item.AddStk(-Qtt, idOwner);
            SID.item.AddStk(+Qtt, idOwner);


            // -- REGISTRE MOVIMENTS ----------------------------------------------------------------------------------------------
            // -- ORIG.
            StockMove SM = new StockMove();
            SM.Ware.idWareHouse = WareORIG.idWareHouse;
            SM.Ware.WareHouseType = WareORIG.WareHouseType;
            SM.idItem = idItem;
            SM.idLot = "";
            SM.idOwner = idOwner;
            SM.QttMove = -Qtt;
            SM.DTArrival = System.DateTime.Now;
            SM.Remarks = remarks;

            SM.LstIdDocs = new List<string>(); if (LstidDoc != null && LstidDoc.Count > 0) foreach (string iD in LstidDoc) SM.LstIdDocs.Add(iD);

            SM.MoveType = MoveType;
            SM.TimeStamp = System.DateTime.Now;
            SM.idUser = IdUser;

            _LstStockMove.Add(SM);

            // -- DEST
            SM = new StockMove();
            SM.Ware.idWareHouse = WareDEST.idWareHouse;
            SM.Ware.WareHouseType = WareDEST.WareHouseType;
            SM.idItem = idItem;
            SM.idLot = "";
            SM.idOwner = idOwner;
            SM.QttMove = Qtt;

            SM.DTArrival = System.DateTime.Now;
            SM.Remarks = remarks;

            SM.LstIdDocs = new List<string>(); if (LstidDoc != null && LstidDoc.Count > 0) foreach (string iD in LstidDoc) SM.LstIdDocs.Add(iD);

            SM.MoveType = MoveType;
            SM.TimeStamp = System.DateTime.Now;
            SM.idUser = IdUser;

            _LstStockMove.Add(SM);
        }

        public int FreeSockItem(Warehouse WareORIG, string idItem, string idOwner, string remarks = "", List<string> LstidDoc = null, string IdUser = "")
        {
            int StkLliberat = 0;

            // -- Test Param
            if (idItem == "" | idItem == null) throw new System.ArgumentException("IdItem can't be null");
            if (WareORIG.idWareHouse == "" | WareORIG.idWareHouse == null) throw new System.ArgumentException("idWarehouseORIG can't be null");

            // -- LOCALITZA STOCK ORIGEN ----------------------------------------------------------------------------------------
            StockItem SIO = GetStockItem(WareORIG, idItem);
            if (SIO == null)
                throw new System.InvalidOperationException("idITEM (" + idItem + ") does not exist on WarehouseORIG (" + WareORIG.idWareHouse + "/" + WareORIG.WareHouseType + ")");

            if (!SIO.item._Resr.ContainsKey(idOwner)) throw new System.InvalidOperationException("idITEM (" + idItem + "), idOwner(" + idOwner + ") does not exist on WarehouseORIG (" + WareORIG.idWareHouse + "/" + WareORIG.WareHouseType + ")");

            int QtAct = SIO.item._Resr[idOwner];
            SIO.item.AddStk(-QtAct, idOwner);
            SIO.item.AddStk(QtAct, "");
            StkLliberat += QtAct;

            // -- REGISTRE MOVIMENTS ----------------------------------------------------------------------------------------------
            // Descontamos Reserva
            StockMove SM = new StockMove();
            SM.Ware.idWareHouse = WareORIG.idWareHouse;
            SM.Ware.WareHouseType = WareORIG.WareHouseType;
            SM.idItem = idItem;
            SM.idOwner = idOwner;
            SM.QttMove = -QtAct;

            SM.DTArrival = System.DateTime.Now;
            SM.Remarks = remarks;

            SM.LstIdDocs = new List<string>(); if (LstidDoc != null && LstidDoc.Count > 0) foreach (string iD in LstidDoc) SM.LstIdDocs.Add(iD);

            SM.MoveType = StockMovementsType.Release;
            SM.TimeStamp = System.DateTime.Now;
            SM.idUser = IdUser;

            _LstStockMove.Add(SM);

            // Añadimos a no reserva
            SM = new StockMove();
            SM.Ware.idWareHouse = WareORIG.idWareHouse;
            SM.Ware.WareHouseType = WareORIG.WareHouseType;
            SM.idItem = idItem;
            SM.idOwner = "";
            SM.QttMove = QtAct;

            SM.DTArrival = System.DateTime.Now;
            SM.Remarks = remarks;

            SM.LstIdDocs = new List<string>(); if (LstidDoc != null && LstidDoc.Count > 0) foreach (string iD in LstidDoc) SM.LstIdDocs.Add(iD);

            SM.MoveType = StockMovementsType.Release;
            SM.TimeStamp = System.DateTime.Now;
            SM.idUser = IdUser;

            _LstStockMove.Add(SM);

            return StkLliberat;
        }

        public int AdjustSockItem(Warehouse WareORIG, string idItem, int Qtt, string remarks = "", List<string> LstidDoc = null, string IdUser = "")
        {
            int StkLliberat = 0;

            // -- Test Param
            if (idItem == "" | idItem == null) throw new System.ArgumentException("IdItem can't be null");
            if (WareORIG.idWareHouse == "" | WareORIG.idWareHouse == null) throw new System.ArgumentException("idWarehouseORIG can't be null");

            // -- LOCALITZA STOCK ORIGEN ----------------------------------------------------------------------------------------
            StockItem SIO = GetStockItem(WareORIG, idItem);
            if (SIO == null)
                throw new System.InvalidOperationException("idITEM (" + idItem + ") does not exist on WarehouseORIG (" + WareORIG.idWareHouse + "/" + WareORIG.WareHouseType + ")");

            if (!SIO.item._Resr.ContainsKey("")) throw new System.InvalidOperationException("idITEM (" + idItem + ") has no free stock WarehouseORIG (" + WareORIG.idWareHouse + "/" + WareORIG.WareHouseType + ")");

            SIO.item.AddStk(Qtt, "");
            StkLliberat += Qtt;

            // -- REGISTRE MOVIMENTS ----------------------------------------------------------------------------------------------
            StockMove SM = new StockMove();
            SM.Ware.idWareHouse = WareORIG.idWareHouse;
            SM.Ware.WareHouseType = WareORIG.WareHouseType;
            SM.idItem = idItem;
            SM.idOwner = "";
            SM.QttMove = Qtt;

            SM.DTArrival = System.DateTime.Now;
            SM.Remarks = remarks;

            SM.LstIdDocs = new List<string>(); if (LstidDoc != null && LstidDoc.Count > 0) foreach (string iD in LstidDoc) SM.LstIdDocs.Add(iD);

            SM.MoveType = StockMovementsType.Adjustment;
            SM.TimeStamp = System.DateTime.Now;
            SM.idUser = IdUser;

            _LstStockMove.Add(SM);

            return StkLliberat;
        }

        public int AsgnSockItem(Warehouse WareORIG, string idItem, int Qtt, string idOwner, string remarks = "", List<string> LstidDoc = null, string IdUser = "")
        {
            int StkReservat = 0;

            // -- Test Param
            if (idItem == "" | idItem == null) throw new System.ArgumentException("IdItem can't be null");
            if (WareORIG.idWareHouse == "" | WareORIG.idWareHouse == null) throw new System.ArgumentException("idWarehouseORIG can't be null");

            // -- LOCALITZA STOCK ORIGEN ----------------------------------------------------------------------------------------
            StockItem SIO = GetStockItem(WareORIG, idItem);
            if (SIO == null)
                throw new System.InvalidOperationException("idITEM (" + idItem + ") does not exist on WarehouseORIG (" + WareORIG.idWareHouse + "/" + WareORIG.WareHouseType + ")");

            //-- Asignem el stok LLUIRE --> idOwner=""
            if (!SIO.item._Resr.ContainsKey("")) throw new System.InvalidOperationException("idITEM (" + idItem + ") does not have free stock at" + WareORIG.idWareHouse + "/" + WareORIG.WareHouseType);

            int QtAct = SIO.item._Resr[""];
            if (QtAct < Qtt) throw new System.InvalidOperationException("idITEM (" + idItem + ") does not have enough free stock at " + WareORIG.idWareHouse + "/" + WareORIG.WareHouseType + ". Required = " + Qtt + " , Available = " + QtAct + ".");

            SIO.item.AddStk(Qtt, idOwner);
            SIO.item.AddStk(-Qtt, "");
            StkReservat += Qtt;

            // -- REGISTRE MOVIMENTS ----------------------------------------------------------------------------------------------
            // Descomtem Origen
            StockMove SM = new StockMove();
            SM.Ware.idWareHouse = WareORIG.idWareHouse;
            SM.Ware.WareHouseType = WareORIG.WareHouseType;
            SM.idItem = idItem;
            SM.idOwner = "";
            SM.QttMove = -Qtt;

            SM.DTArrival = System.DateTime.Now;
            SM.Remarks = remarks;

            SM.LstIdDocs = new List<string>(); if (LstidDoc != null && LstidDoc.Count > 0) foreach (string iD in LstidDoc) SM.LstIdDocs.Add(iD);

            SM.MoveType = StockMovementsType.Reservation;
            SM.TimeStamp = System.DateTime.Now;
            SM.idUser = IdUser;

            _LstStockMove.Add(SM);

            // Afegim Destí
            SM = new StockMove();
            SM.Ware.idWareHouse = WareORIG.idWareHouse;
            SM.Ware.WareHouseType = WareORIG.WareHouseType;
            SM.idItem = idItem;
            SM.idOwner = idOwner;
            SM.QttMove = Qtt;

            SM.DTArrival = System.DateTime.Now;
            SM.Remarks = remarks;

            SM.LstIdDocs = new List<string>(); if (LstidDoc != null && LstidDoc.Count > 0) foreach (string iD in LstidDoc) SM.LstIdDocs.Add(iD);

            SM.MoveType = StockMovementsType.Reservation;
            SM.TimeStamp = System.DateTime.Now;
            SM.idUser = IdUser;

            _LstStockMove.Add(SM);

            return StkReservat;
        }
        public int AdjustItemAssing(Warehouse WareORIG, string idItem, int Qtt, string idOwner, string remarks = "", List<string> LstidDoc = null, string IdUser = "")
        {
            int StkReservat = 0;

            // -- Test Param
            if (idItem == "" | idItem == null) throw new System.ArgumentException("IdItem can't be null");
            if (idOwner == "" | idOwner == null) throw new System.ArgumentException("IdOwner can't be null");
            if (WareORIG.idWareHouse == "" | WareORIG.idWareHouse == null) throw new System.ArgumentException("idWarehouseORIG can't be null");

            // -- LOCALITZA STOCK ORIGEN ----------------------------------------------------------------------------------------
            StockItem SIO = GetStockItem(WareORIG, idItem);
            if (SIO == null)
                throw new System.InvalidOperationException("idITEM (" + idItem + ") does not exist on WarehouseORIG (" + WareORIG.idWareHouse + "/" + WareORIG.WareHouseType + ")");

            //-- Asignem el stok LLUIRE --> idOwner=""
            if (!SIO.item._Resr.ContainsKey(idOwner)) throw new System.InvalidOperationException("idITEM (" + idItem + ") does not have assigned stock to '" + idOwner + "' at" + WareORIG.idWareHouse + "/" + WareORIG.WareHouseType);

            int QtAct = SIO.item._Resr[idOwner];
            if (QtAct + Qtt < 0) throw new System.InvalidOperationException("idITEM (" + idItem + ") does not have enough free stock at " + WareORIG.idWareHouse + "/" + WareORIG.WareHouseType + ". Required = " + Qtt + " , Available = " + QtAct + ".");

            SIO.item.AddStk(-Qtt, "");
            SIO.item.AddStk(Qtt, idOwner);
            StkReservat += Qtt;

            // -- REGISTRE MOVIMENTS ----------------------------------------------------------------------------------------------
            // Descomtem Origen
            StockMove SM = new StockMove();
            SM.Ware.idWareHouse = WareORIG.idWareHouse;
            SM.Ware.WareHouseType = WareORIG.WareHouseType;
            SM.idItem = idItem;
            SM.idOwner = idOwner;
            SM.QttMove = Qtt;

            SM.DTArrival = System.DateTime.Now;
            SM.Remarks = remarks;

            SM.LstIdDocs = new List<string>(); if (LstidDoc != null && LstidDoc.Count > 0) foreach (string iD in LstidDoc) SM.LstIdDocs.Add(iD);

            SM.MoveType = StockMovementsType.Reservation;
            SM.TimeStamp = System.DateTime.Now;
            SM.idUser = IdUser;

            _LstStockMove.Add(SM);

            // Afegim Destí
            SM = new StockMove();
            SM.Ware.idWareHouse = WareORIG.idWareHouse;
            SM.Ware.WareHouseType = WareORIG.WareHouseType;
            SM.idItem = idItem;
            SM.idOwner = "";
            SM.QttMove = -Qtt;

            SM.DTArrival = System.DateTime.Now;
            SM.Remarks = remarks;

            SM.LstIdDocs = new List<string>(); if (LstidDoc != null && LstidDoc.Count > 0) foreach (string iD in LstidDoc) SM.LstIdDocs.Add(iD);

            SM.MoveType = StockMovementsType.Reservation;
            SM.TimeStamp = System.DateTime.Now;
            SM.idUser = IdUser;

            _LstStockMove.Add(SM);

            return StkReservat;
        }


        #endregion

        public void SetStockBase()
        {
            _LstStocksOrig.Clear();
            foreach (StockItem S in _LstStock)
            {
                foreach (DetAsg DA in S.LstDetRes)
                {
                    _LstStocksOrig.Add(new StockItem(S.idWareHouse, S.idWareHouseType, S.WareHouseName, S.idItem, DA.idOwner, DA.Qtt));
                }
            }

        }

        public List<StockItem> LstStkBase()
        {
            return _LstStocksOrig;
        }

        #region ConsultaDomini

        // WAREHOUSES

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

        // OWNERS

        public List<string> GetLstOwnerNames()
        {
            List<string> ON = new List<string>();
            foreach (Owner O in _LstOwners) ON.Add(O.OwnerName);
            return ON;
        }

        public List<string> GetLstOwnerIds()
        {
            List<string> ON = new List<string>();
            foreach (Owner O in _LstOwners) ON.Add(O.idOwner);
            return ON;
        }


        #endregion


    }
}
