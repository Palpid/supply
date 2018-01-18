using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.PRJ_Stocks.Classes
{
    public class Stocks
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
            Transit = 4,
            Rejected = 5
        }


        #region ItemBcn
        public class ItemBcn
        {
            private string _Id;
            private string _Descr;
            private string _Group;
            
            public string ID_ITEM_BCN { get { return _Id; } set {_Id = value; } }
            public string ITEM_DESCRIPTION { get { return _Descr; } set { _Descr = value; } }
            public string ID_ITEM_GROUP { get { return _Group; } set { _Group = value; } }
        }

        private List<ItemBcn> _LstItemsBcn = new List<ItemBcn>();
        public List<ItemBcn> LstItemsBcn
        {
            get { return _LstItemsBcn; }
            set { _LstItemsBcn = value; }
        }
        public void AddItemBcn(ItemBcn itmBcn)
        {
            _LstItemsBcn.Add(itmBcn);
        }
        public bool ItemBcnExists(string idItemBcn)
        {
            foreach(ItemBcn iB in _LstItemsBcn)
                { if (iB.ID_ITEM_BCN == idItemBcn) return true; }
            return false;
        }
        #endregion ItemBcn

        #region Owners

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
        #endregion Owners

        #region Warehouses

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
        public bool WareHouseExists(string idWare, int idTypeWare)
        {
            foreach (Warehouse W in _LstWarehouses)            
            {            
                if ((W.idWareHouse == idWare) && (W.idWareHouseType == idTypeWare)) return true; }
            return false;
        }
#endregion


        public class DetAsg
        {
            public string idOwner { get; set; }
            public decimal Qtt { get; set; }
        }

        public class DetLot
        {
            public string idLot { get; set; }
            public decimal Qtt { get; set; }
        }

        #region Item

        /// <summary>
        ///   Guarda les dades del stock de un item i els seus lots.
        ///   Es crea sempre un lot NULL per a les qtt que no tenen lot.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">No existeix el lot indicat;"</exception>
        public class Item
        {
            public string idItem { get; set; }
            public Dictionary<String, decimal> _Resr = new Dictionary<String, decimal>(); // (ownner, qtt)
            public Dictionary<String, decimal> _Lots = new Dictionary<String, decimal>(); // (ownner, qtt)

            ///    Modifica la QTT del stock. Si QttAfegir positiva incrementa, si negativa resta.
            public decimal AddStk(decimal QttAfegir, string idOwner)
            {
                decimal Variacio = 0;
                decimal QTTAct = 0;
                if (_Resr == null) _Resr = new Dictionary<string, decimal>();
                if (!_Resr.ContainsKey(idOwner)) _Resr.Add(idOwner, 0);
                QTTAct = _Resr[idOwner];
                if ((QTTAct + QttAfegir) < 0) throw new System.InvalidOperationException("Stock final quantity is negative. No negative stocks allowed.)");
                _Resr[idOwner] += QttAfegir;
                Variacio = QttAfegir;
                if (_Resr[idOwner] == 0) _Resr.Remove(idOwner);
                return Variacio;
            }
            public decimal AddLot(decimal QttAfegir, string idLot)
            {
                decimal Variacio = 0;
                decimal QTTAct = 0;
                if (_Lots == null) _Lots = new Dictionary<string, decimal>();
                if (!_Lots.ContainsKey(idLot)) _Lots.Add(idLot, 0);
                QTTAct = _Lots[idLot];
                if ((QTTAct + QttAfegir) < 0) throw new System.InvalidOperationException("Lot final quantity is negative. No negative stocks allowed.)");
                _Lots[idLot] += QttAfegir;
                Variacio = QttAfegir;
                if (_Lots[idLot] == 0) _Lots.Remove(idLot);
                return Variacio;
            }

            /// <summary>
            ///   Retorna el total stock de tots el lots del item.
            /// </summary>
            public decimal TotalQTT
            {
                get
                {
                    decimal Total = 0;
                    foreach (var V in _Resr.Values) Total += V;
                    return Total;
                }
                set { }
            }

            /// <summary>
            ///   Retorna el stock reservat
            /// </summary>
            public decimal TotalAssignedQTT
            {
                get
                {
                    decimal Total = 0;
                    foreach (var Q in _Resr) if (Q.Key != "") Total += Q.Value;
                    return Total;
                }
                set { }
            }

            /// <summary>
            ///   Retorna el stock reservat
            /// </summary>
            public decimal TotalAssignedLotQTT
            {
                get
                {
                    decimal Total = 0;
                    foreach (var Q in _Lots) if (Q.Key != "") Total += Q.Value;
                    return Total;
                }
                set { }
            }

            public decimal TotalFreeLotQTT
            {
                get
                {
                    return TotalQTT - TotalAssignedLotQTT;
                }
                set { }
            }

            /// <summary>
            ///   Retorna el stock reservat
            /// </summary>
            public decimal TotalFreeQTT
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

            public List<DetLot> LstLots
            {
                get
                {
                    List<DetLot> LD = new List<DetLot>();
                    foreach (var L in _Lots)
                    {
                        DetLot nD = new DetLot();
                        nD.idLot = L.Key;
                        nD.Qtt = L.Value;
                        LD.Add(nD);
                    }
                    return LD;
                }
                set { }
            }

            public decimal StkOwner(string idOwner)
            {
                if (_Resr.ContainsKey(idOwner))
                    return _Resr[idOwner];
                else
                    return 0;
            }

            public decimal StkLot(string idLot)
            {
                if (_Lots.ContainsKey(idLot))
                    return _Lots[idLot];
                else
                    return 0;
            }

        }
#endregion Item

        public class StockItem
        {
            public StockItem(string idWarehouse = "", int idWareHouseType = 0, string WareHouseName = "",
                             string idItem = "", string idOwner = "", decimal QTT = 0)
            {
                ware = new Warehouse();
                ware.idWareHouse = idWarehouse; ware.idWareHouseType = idWareHouseType; ware.Descr = WareHouseName;
                item = new Item();
                item.idItem = idItem;
                item.LstAsigs = new List<DetAsg>();
                item.LstLots = new List<DetLot>();
                item.AddStk(QTT, idOwner);
                //item.AddLot(QTT, idLot);               
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

            public decimal FreeStock
            {
                get { return item.TotalFreeQTT; }
            }

            public decimal AsgnStock
            {
                get { return item.TotalAssignedQTT; }
            }

            public decimal TotalStock
            {
                get { return item.TotalAssignedQTT + item.TotalFreeQTT; }
            }

            public decimal FreeLot
            {
                get { return item.TotalFreeLotQTT; }
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

            public List<string> LstLots()
            {
                List<string> LLT = new List<string>();
                foreach (var L in item._Lots)
                {
                    if (!LLT.Contains(L.Key)) LLT.Add(L.Key);
                }
                return LLT;
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

            public List<DetLot> LstDetLots
            {
                get
                {
                    List<DetLot> Ld = new List<DetLot>();
                    foreach (var A in item._Lots)
                    {
                        DetLot nd = new DetLot();
                        nd.idLot = A.Key;
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
            public StockMove(StockMovementsType MovType, string idWR, string DercsWR, int idWareType, string iditem, string owner, string Lot, decimal qttmove)
            {
                Ware = new Warehouse();
                Ware.idWareHouse = idWR;
                Ware.Descr = DercsWR;
                Ware.idWareHouseType = idWareType;

                LstIdDocs = new List<string>();

                idItem = iditem;
                idOwner = owner;
                idLot = Lot;
                QttMove = qttmove;
                DTArrival = DTArrival;

                MoveType = MovType;
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
            public decimal QttMove { get; set; }

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
                get { return (int)MoveType; }
                set { MoveType = (StockMovementsType)value; }
            }
            public string MovementTypeName
            {
                get { return (string) MoveType.ToString(); }
                set { }
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

        /// <summary>
        /// Retorna el valor del item del stock "congelat" al carregar. Per a detectar canvis en STOCK externs (concurencia) en el proces de guardar
        /// </summary>
        /// <param name="ware"></param>
        /// <param name="idItem"></param>
        /// <returns></returns>
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
        ///  Afegim un STKitem a la llista STOKS. Usat en la càrrega only.
        /// </summary>
        /// <param name="STKi"></param>
        public void CargaLotItem(StockItem STKi)
        {
            StockItem S = GetStockItem(STKi.ware, STKi.idItem);
            if (S == null)
                _LstStock.Add(STKi);
            else
            {
                foreach (var A in STKi.item._Lots)
                {
                    S.item.AddLot(A.Value, A.Key);
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
        public void AddSockItem(StockMovementsType MoveType, Warehouse ware, decimal Qtt, string idItem, string idLot = "", string idOwner = "", string Remarks = "", List<string> LstidDoc = null, string IdUser = "")
        {
            // -- Test Param --
            if (ware.idWareHouse == "" | ware.idWareHouse == null) throw new System.ArgumentException("IdWarehouse can't be empty/null");
            if (idItem == "" || idItem == null) throw new System.ArgumentException("IdItem can't be empty/null");

            // -- REGISTRE STOCK ----------------------------------------------------------------------------------------
            StockItem SI = GetStockItem(ware, idItem);
            if (SI == null)
            {
                if ( Qtt < 0) // SI es ZERO!!!
                    throw new System.InvalidOperationException($"Not enough Stock on Warehouse Available: 0, Requested: {Qtt.ToString("###,###,###,###.00")})");
                else
                {
                    try{ 
                    SI = new StockItem();
                    SI.ware.idWareHouse = ware.idWareHouse;
                    SI.ware.WareHouseType = ware.WareHouseType;
                    SI.ware.Descr = ware.Descr;
                    SI.item.idItem = idItem;                    
                    SI.item.AddStk(Qtt, idOwner);
                    SI.item.AddLot(Qtt, idLot);
                    _LstStock.Add(SI);
                    }
                    catch (Exception Ex)
                    {
                        throw new System.InvalidOperationException(Ex.Message);
                    }
                }
            }
            else
            {
                if (SI.item.TotalQTT + Qtt < 0)
                    throw new System.InvalidOperationException("Not enough Stock on Origin Warehouse (Available: " + SI.item.TotalQTT.ToString("###,###,###,###.00") + ", Requested: " + Qtt.ToString("###,###,###,###.00") + ")");
                else
                    SI.item.AddStk(Qtt, idOwner);
                    SI.item.AddLot(Qtt, idLot);
            }

            // -- REGISTRE MOVIMENTS ----------------------------------------------------------------------------------------
            StockMove SM = new StockMove(StockMovementsType.Entry,
                                ware.idWareHouse, ware.Descr, ware.idWareHouseType,
                                idItem, idOwner,idLot, Qtt);

            SM.DTArrival = System.DateTime.Now;
            SM.Remarks = Remarks;
            SM.LstIdDocs = new List<string>(); if (LstidDoc != null && LstidDoc.Count > 0) foreach (string iD in LstidDoc) SM.LstIdDocs.Add(iD);

            SM.MoveType = MoveType;
            SM.TimeStamp = System.DateTime.Now;
            SM.idUser = IdUser;

            _LstStockMove.Add(SM);
            
        }

        public void MoveSockItem(StockMovementsType MoveType, Warehouse WareORIG, Warehouse WareDEST, decimal Qtt, 
                                 string idItem, string idOwner, string idlot,
                                 string remarks = "", List<string> LstidDoc = null, string IdUser = "")
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

            decimal QAct = SIO.item.StkOwner(idOwner);
            if (QAct < Qtt) throw new System.InvalidOperationException("Stock quantity to move (" + Qtt + ") is greather than existing quantity (" + QAct + ")");

            decimal QActL = SIO.item.StkLot(idlot);
            if (QActL < Qtt) throw new System.InvalidOperationException("Lot quantity to move (" + Qtt + ") is greather than existing quantity (" + QActL + ")");

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
                SID.item.AddLot(0, idlot);
                _LstStock.Add(SID);
            }
            SIO.item.AddStk(-Qtt, idOwner);
            SID.item.AddStk(+Qtt, idOwner);
            SIO.item.AddLot(-Qtt, idlot);
            SID.item.AddLot(+Qtt, idlot);


            // -- REGISTRE MOVIMENTS ----------------------------------------------------------------------------------------------
            // -- ORIG.
            StockMove SM = new StockMove(StockMovementsType.Movement,
                            WareORIG.idWareHouse, WareORIG.Descr, WareORIG.idWareHouseType,
                            idItem, idOwner, idlot,
                            -Qtt);
            SM.DTArrival = System.DateTime.Now;
            SM.Remarks = remarks;

            SM.LstIdDocs = new List<string>(); if (LstidDoc != null && LstidDoc.Count > 0) foreach (string iD in LstidDoc) SM.LstIdDocs.Add(iD);

            SM.MoveType = MoveType;
            SM.TimeStamp = System.DateTime.Now;
            SM.idUser = IdUser;

            _LstStockMove.Add(SM);

            // -- DEST
            SM = new StockMove(StockMovementsType.Movement,
                               WareDEST.idWareHouse, WareDEST.Descr, WareDEST.idWareHouseType,
                               idItem, idOwner, idlot, Qtt);
            SM.DTArrival = System.DateTime.Now;
            SM.Remarks = remarks;

            SM.LstIdDocs = new List<string>(); if (LstidDoc != null && LstidDoc.Count > 0) foreach (string iD in LstidDoc) SM.LstIdDocs.Add(iD);

            SM.MoveType = MoveType;
            SM.TimeStamp = System.DateTime.Now;
            SM.idUser = IdUser;

            _LstStockMove.Add(SM);
        }

        private StockItem IniFreeSockItem(Warehouse WareORIG, string idItem, string idOwner)
        {
        
            // -- Test Param
            if (idItem == "" | idItem == null) throw new System.ArgumentException("IdItem can't be null");
            if (WareORIG.idWareHouse == "" | WareORIG.idWareHouse == null) throw new System.ArgumentException("idWarehouseORIG can't be null");

            // -- LOCALITZA STOCK ORIGEN ----------------------------------------------------------------------------------------
            StockItem SIO = GetStockItem(WareORIG, idItem);
            if (SIO == null)
                throw new System.InvalidOperationException("idITEM (" + idItem + ") does not exist on WarehouseORIG (" + WareORIG.idWareHouse + "/" + WareORIG.WareHouseType + ")");

            if (!SIO.item._Resr.ContainsKey(idOwner)) throw new System.InvalidOperationException("idITEM (" + idItem + "), idOwner(" + idOwner + ") does not exist on WarehouseORIG (" + WareORIG.idWareHouse + "/" + WareORIG.WareHouseType + ")");            

            return SIO;
        }

        /// <summary>
        ///  Libera la reserva de stock (quita el stock owner)
        /// </summary>
        /// <param name="WareORIG"></param>
        /// <param name="idItem"></param>
        /// <param name="idOwner"></param>
        /// <param name="idlot"></param>
        /// <param name="remarks"></param>
        /// <param name="LstidDoc"></param>
        /// <param name="IdUser"></param>
        /// <returns></returns>
        public decimal FreeSockItem(Warehouse WareORIG, string idItem, string idOwner, decimal Qtt,string remarks = "", List<string> LstidDoc = null, string IdUser = "" )
            {
            decimal StkLliberat = 0;
            
            StockItem SIO = IniFreeSockItem(WareORIG, idItem, idOwner);            
            decimal QtAct = SIO.item._Resr[idOwner];
            if (QtAct + Qtt<0)
                throw new System.InvalidOperationException($"There is no enough stock of idITEM ({idItem}), idOwner({idOwner}) on the Warehouse ( {WareORIG.idWareHouse}/{WareORIG.WareHouseType}). Existing stock: {QtAct} , Requested: {Qtt}");
            
            SIO.item.AddStk(-Qtt, idOwner);
            SIO.item.AddStk(Qtt, "");
            StkLliberat += Qtt;

            // -- REGISTRE MOVIMENTS ----------------------------------------------------------------------------------------------
            // Descontamos Reserva
            StockMove SM = new StockMove(StockMovementsType.Release,
                                        WareORIG.idWareHouse, WareORIG.Descr, WareORIG.idWareHouseType,
                                        idItem, idOwner, "",-Qtt);
            SM.DTArrival = System.DateTime.Now;
            SM.Remarks = remarks;

            SM.LstIdDocs = new List<string>(); if (LstidDoc != null && LstidDoc.Count > 0) foreach (string iD in LstidDoc) SM.LstIdDocs.Add(iD);

            SM.MoveType = StockMovementsType.Release;
            SM.TimeStamp = System.DateTime.Now;
            SM.idUser = IdUser;

            _LstStockMove.Add(SM);

            // Añadimos a no reserva
            SM = new StockMove(StockMovementsType.Release,
                                WareORIG.idWareHouse, WareORIG.Descr, WareORIG.idWareHouseType,
                                idItem, "", "", Qtt);
            SM.DTArrival = System.DateTime.Now;
            SM.Remarks = remarks;

            SM.LstIdDocs = new List<string>(); if (LstidDoc != null && LstidDoc.Count > 0) foreach (string iD in LstidDoc) SM.LstIdDocs.Add(iD);

            SM.MoveType = StockMovementsType.Release;
            SM.TimeStamp = System.DateTime.Now;
            SM.idUser = IdUser;

            _LstStockMove.Add(SM);

            return StkLliberat;
        }

        public decimal FreeSockItem(Warehouse WareORIG, string idItem, string idOwner, string remarks = "", List<string> LstidDoc = null, string IdUser = "")
        {
            decimal StkLliberat = 0;

            StockItem SIO = IniFreeSockItem(WareORIG, idItem, idOwner);
            decimal QtAct = SIO.item._Resr[idOwner];
            decimal Qtt = QtAct;

            StkLliberat = FreeSockItem(WareORIG, idItem, idOwner, remarks, LstidDoc, IdUser);

            return StkLliberat;
        }

        private StockItem IniFreeLot(Warehouse WareORIG, string idItem, string idlot)
        {
            // -- Test Param
            if (idItem == "" | idItem == null) throw new System.ArgumentException("IdItem can't be null");
            if (WareORIG.idWareHouse == "" | WareORIG.idWareHouse == null) throw new System.ArgumentException("idWarehouseORIG can't be null");

            // -- LOCALITZA STOCK ORIGEN ----------------------------------------------------------------------------------------
            StockItem SIO = GetStockItem(WareORIG, idItem);
            if (SIO == null)
                throw new System.InvalidOperationException("idITEM (" + idItem + ") does not exist on WarehouseORIG (" + WareORIG.idWareHouse + "/" + WareORIG.WareHouseType + ")");

            if (!SIO.item._Lots.ContainsKey(idlot)) throw new System.InvalidOperationException("idITEM (" + idItem + "), Lot(" + idlot + ") does not exist on WarehouseORIG (" + WareORIG.idWareHouse + "/" + WareORIG.WareHouseType + ")");

            return SIO;

        }

        /// <summary>
        ///  Libera QTT del lote del stock (quita el lote)
        /// </summary>
        /// <param name="WareORIG"></param>
        /// <param name="idItem"></param>
        /// <param name="idOwner"></param>
        /// <param name="idlot"></param>
        /// <param name="remarks"></param>
        /// <param name="LstidDoc"></param>
        /// <param name="IdUser"></param>
        /// <returns></returns>
        public decimal FreeLotItem(Warehouse WareORIG, string idItem, string idlot, decimal Qtt, string remarks = "", List<string> LstidDoc = null, string IdUser = "")
        {
            decimal StkLliberat = 0;

            StockItem SIO = IniFreeLot(WareORIG, idItem, idlot);
            decimal QtAct = SIO.item._Lots[idlot];
            if (QtAct + Qtt < 0)
                throw new System.InvalidOperationException($"There is no enough stock of idITEM ({idItem}), Lot({idlot}) on the Warehouse ( {WareORIG.idWareHouse}/{WareORIG.WareHouseType}). Existing stock: {QtAct} , Requested: {Qtt}");


            SIO.item.AddLot(-Qtt, idlot);
            SIO.item.AddLot(Qtt, "");
            StkLliberat += Qtt;

            // -- REGISTRE MOVIMENTS ----------------------------------------------------------------------------------------------
            // Descontamos Reserva
            StockMove SM = new StockMove(StockMovementsType.Release,
                                        WareORIG.idWareHouse, WareORIG.Descr, WareORIG.idWareHouseType,
                                        idItem, "", idlot, -Qtt);
            SM.DTArrival = System.DateTime.Now;
            SM.Remarks = remarks;

            SM.LstIdDocs = new List<string>(); if (LstidDoc != null && LstidDoc.Count > 0) foreach (string iD in LstidDoc) SM.LstIdDocs.Add(iD);

            SM.MoveType = StockMovementsType.Release;
            SM.TimeStamp = System.DateTime.Now;
            SM.idUser = IdUser;

            _LstStockMove.Add(SM);

            // Añadimos a no reserva sin lote
            SM = new StockMove(StockMovementsType.Release,
                                        WareORIG.idWareHouse, WareORIG.Descr, WareORIG.idWareHouseType,
                                        idItem, "", "", Qtt);
            SM.DTArrival = System.DateTime.Now;
            SM.Remarks = remarks;

            SM.LstIdDocs = new List<string>(); if (LstidDoc != null && LstidDoc.Count > 0) foreach (string iD in LstidDoc) SM.LstIdDocs.Add(iD);

            SM.MoveType = StockMovementsType.Release;
            SM.TimeStamp = System.DateTime.Now;
            SM.idUser = IdUser;

            _LstStockMove.Add(SM);

            return StkLliberat;
        }

        /// <summary>
        ///  Libera TODO del lote del stock (quita el lote)
        /// </summary>
        /// <param name="WareORIG"></param>
        /// <param name="idItem"></param>
        /// <param name="idOwner"></param>
        /// <param name="idlot"></param>
        /// <param name="remarks"></param>
        /// <param name="LstidDoc"></param>
        /// <param name="IdUser"></param>
        /// <returns></returns>
        public decimal FreeLotItem(Warehouse WareORIG, string idItem, string idlot, string remarks = "", List<string> LstidDoc = null, string IdUser = "")
        {
            decimal StkLliberat = 0;

            StockItem SIO = IniFreeLot(WareORIG, idItem, idlot);
            decimal QtAct = SIO.item._Lots[idlot];
            decimal Qtt = QtAct;
            if (QtAct + Qtt < 0)
                throw new System.InvalidOperationException($"There is no enough stock of idITEM ({idItem}), Lot({idlot}) on the Warehouse ( {WareORIG.idWareHouse}/{WareORIG.WareHouseType}). Existing stock: {QtAct} , Requested: {Qtt}");

            StkLliberat = FreeLotItem(WareORIG, idItem, idlot, Qtt, remarks, LstidDoc, IdUser);
            
            return StkLliberat;
        }

        public decimal AdjustSockItem(Warehouse WareORIG, string idItem, decimal Qtt, string remarks = "", List<string> LstidDoc = null, string IdUser = "")
        {
            decimal StkLliberat = 0;

            // -- Test Param
            if (idItem == "" | idItem == null) throw new System.ArgumentException("IdItem can't be null");
            if (WareORIG.idWareHouse == "" | WareORIG.idWareHouse == null) throw new System.ArgumentException("idWarehouseORIG can't be null");

            // -- LOCALITZA STOCK ORIGEN ----------------------------------------------------------------------------------------
            StockItem SIO = GetStockItem(WareORIG, idItem);
            if (SIO == null)
                throw new System.InvalidOperationException("idITEM (" + idItem + ") does not exist on WarehouseORIG (" + WareORIG.idWareHouse + "/" + WareORIG.WareHouseType + ")");

            if (!SIO.item._Resr.ContainsKey("")) throw new System.InvalidOperationException("idITEM (" + idItem + ") has no free stock on WarehouseORIG (" + WareORIG.idWareHouse + "/" + WareORIG.WareHouseType + ")");

            if (!SIO.item._Lots.ContainsKey("")) throw new System.InvalidOperationException("idITEM (" + idItem + ") has no free lot stock on WarehouseORIG (" + WareORIG.idWareHouse + "/" + WareORIG.WareHouseType + ")");

            SIO.item.AddStk(Qtt, "");
            SIO.item.AddLot(Qtt, "");

            StkLliberat += Qtt;

            // -- REGISTRE MOVIMENTS ----------------------------------------------------------------------------------------------
            StockMove SM = new StockMove(StockMovementsType.Adjustment,
                                          WareORIG.idWareHouse, WareORIG.Descr, WareORIG.idWareHouseType,
                                          idItem, "", "", Qtt);
            SM.DTArrival = System.DateTime.Now;
            SM.Remarks = remarks;

            SM.LstIdDocs = new List<string>(); if (LstidDoc != null && LstidDoc.Count > 0) foreach (string iD in LstidDoc) SM.LstIdDocs.Add(iD);

            SM.MoveType = StockMovementsType.Adjustment;
            SM.TimeStamp = System.DateTime.Now;
            SM.idUser = IdUser;

            _LstStockMove.Add(SM);

            return StkLliberat;
        }

       

        public decimal AsgnSockItem(Warehouse WareORIG, string idItem, decimal Qtt, string idOwner,string remarks = "", List<string> LstidDoc = null, string IdUser = "")
        {
            decimal StkReservat = 0;

            // -- Test Param
            if (idItem == "" | idItem == null) throw new System.ArgumentException("IdItem can't be null");
            if (WareORIG.idWareHouse == "" | WareORIG.idWareHouse == null) throw new System.ArgumentException("idWarehouseORIG can't be null");

            // -- LOCALITZA STOCK ORIGEN ----------------------------------------------------------------------------------------
            StockItem SIO = GetStockItem(WareORIG, idItem);
            if (SIO == null)
                throw new System.InvalidOperationException("idITEM (" + idItem + ") does not exist on WarehouseORIG (" + WareORIG.idWareHouse + "/" + WareORIG.WareHouseType + ")");

            //-- Asignem el stok LLUIRE --> idOwner=""
            if (!SIO.item._Resr.ContainsKey("")) throw new System.InvalidOperationException("idITEM (" + idItem + ") does not have free stock at" + WareORIG.idWareHouse + "/" + WareORIG.WareHouseType);

            decimal QtAct = SIO.item._Resr[""];
            if (QtAct < Qtt) throw new System.InvalidOperationException("idITEM (" + idItem + ") does not have enough free stock at " + WareORIG.idWareHouse + "/" + WareORIG.WareHouseType + ". Required = " + Qtt + " , Available = " + QtAct + ".");

            SIO.item.AddStk(Qtt, idOwner);
            SIO.item.AddStk(-Qtt, "");
            StkReservat += Qtt;

            // -- REGISTRE MOVIMENTS ----------------------------------------------------------------------------------------------
            // Descomtem Origen
            StockMove SM = new StockMove(StockMovementsType.Reservation,
                                         WareORIG.idWareHouse, WareORIG.Descr, WareORIG.idWareHouseType,
                                         idItem, "","", -Qtt);

            SM.DTArrival = System.DateTime.Now;
            SM.Remarks = remarks;

            SM.LstIdDocs = new List<string>(); if (LstidDoc != null && LstidDoc.Count > 0) foreach (string iD in LstidDoc) SM.LstIdDocs.Add(iD);

            SM.MoveType = StockMovementsType.Reservation;
            SM.TimeStamp = System.DateTime.Now;
            SM.idUser = IdUser;

            _LstStockMove.Add(SM);

            // Afegim Destí
            SM = new StockMove(StockMovementsType.Reservation,
                                         WareORIG.idWareHouse, WareORIG.Descr, WareORIG.idWareHouseType,
                                         idItem, idOwner,"", Qtt);
            SM.DTArrival = System.DateTime.Now;
            SM.Remarks = remarks;

            SM.LstIdDocs = new List<string>(); if (LstidDoc != null && LstidDoc.Count > 0) foreach (string iD in LstidDoc) SM.LstIdDocs.Add(iD);

            SM.MoveType = StockMovementsType.Reservation;
            SM.TimeStamp = System.DateTime.Now;
            SM.idUser = IdUser;

            _LstStockMove.Add(SM);

            return StkReservat;
        }

        public decimal AsgnLotItem(Warehouse WareORIG, string idItem, decimal Qtt, string idLot, string remarks = "", List<string> LstidDoc = null, string IdUser = "")
        {
            decimal StkReservat = 0;

            // -- Test Param
            if (idItem == "" || idItem == null) throw new System.ArgumentException("IdItem can't be null");
            if (WareORIG.idWareHouse == "" || WareORIG.idWareHouse == null) throw new System.ArgumentException("idWarehouseORIG can't be null");
            if (Qtt <=0) throw new System.ArgumentException("Quantity to Assign must be greather than zero");

            // -- LOCALITZA STOCK ORIGEN LLURE DE LOT ------------------------------------------------------

            StockItem SIO = GetStockItem(WareORIG, idItem);
            if (SIO == null)
                throw new System.InvalidOperationException("idITEM (" + idItem + ") does not exist on WarehouseORIG (" + WareORIG.idWareHouse + "/" + WareORIG.WareHouseType + ")");

            //-- Asignem el lot LLUIRE --> Lot=""
            if (!SIO.item._Lots.ContainsKey("")) throw new System.InvalidOperationException("idITEM (" + idItem + ") does not have lot free stock at" + WareORIG.idWareHouse + "/" + WareORIG.WareHouseType);

            decimal QtAct = SIO.item._Lots[""];
            if (QtAct < Qtt) throw new System.InvalidOperationException("idITEM (" + idItem + ") does not have enough lot free stock at " + WareORIG.idWareHouse + "/" + WareORIG.WareHouseType + ". Required = " + Qtt + " , Available = " + QtAct + ".");

            SIO.item.AddLot(Qtt, idLot);
            SIO.item.AddLot(-Qtt, "");
            StkReservat += Qtt;

            // -- REGISTRE MOVIMENTS ------------------------------------------------------------------------
            // Descomtem Origen
            StockMove SM = new StockMove(StockMovementsType.Reservation,
                                         WareORIG.idWareHouse, WareORIG.Descr, WareORIG.idWareHouseType,
                                         idItem, "", "", -Qtt);

            SM.DTArrival = System.DateTime.Now;
            SM.Remarks = remarks;

            SM.LstIdDocs = new List<string>(); if (LstidDoc != null && LstidDoc.Count > 0) foreach (string iD in LstidDoc) SM.LstIdDocs.Add(iD);

            SM.MoveType = StockMovementsType.Reservation;
            SM.TimeStamp = System.DateTime.Now;
            SM.idUser = IdUser;

            _LstStockMove.Add(SM);

            // Afegim Destí
            SM = new StockMove(StockMovementsType.Reservation,
                                         WareORIG.idWareHouse, WareORIG.Descr, WareORIG.idWareHouseType,
                                         idItem, "", idLot, Qtt);
            SM.DTArrival = System.DateTime.Now;
            SM.Remarks = remarks;

            SM.LstIdDocs = new List<string>(); if (LstidDoc != null && LstidDoc.Count > 0) foreach (string iD in LstidDoc) SM.LstIdDocs.Add(iD);

            SM.MoveType = StockMovementsType.Reservation;
            SM.TimeStamp = System.DateTime.Now;
            SM.idUser = IdUser;

            _LstStockMove.Add(SM);

            return StkReservat;
        }

        public decimal AsgnLotItemCarga(Warehouse WareORIG, string idItem, decimal Qtt, string idLot)
        {
            decimal StkAfegit = 0;

            // -- Test Param
            if (idItem == "" || idItem == null) throw new System.ArgumentException("IdItem can't be null");
            if (WareORIG.idWareHouse == "" || WareORIG.idWareHouse == null) throw new System.ArgumentException("idWarehouseORIG can't be null");
            if (Qtt <= 0) throw new System.ArgumentException("Quantity to Assign must be greather than zero");

            // -- LOCALITZA STOCK ORIGEN LLURE DE LOT ------------------------------------------------------

            StockItem SIO = GetStockItem(WareORIG, idItem);
            if (SIO == null)
                throw new System.InvalidOperationException("idITEM (" + idItem + ") does not exist on WarehouseORIG (" + WareORIG.idWareHouse + "/" + WareORIG.WareHouseType + ")");

            SIO.item.AddLot(Qtt, idLot);
            StkAfegit += Qtt;

            return StkAfegit;
        }


        public decimal AdjustItemAssing(Warehouse WareORIG, string idItem, decimal Qtt, string idOwner, string remarks = "", List<string> LstidDoc = null, string IdUser = "")
        {
            decimal StkReservat = 0;

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

            decimal QtAct = SIO.item._Resr[idOwner];
            if (QtAct + Qtt < 0) throw new System.InvalidOperationException("idITEM (" + idItem + ") does not have enough free stock at " + WareORIG.idWareHouse + "/" + WareORIG.WareHouseType + ". Required = " + Qtt + " , Available = " + QtAct + ".");

            SIO.item.AddStk(-Qtt, "");
            SIO.item.AddStk(Qtt, idOwner);
            StkReservat += Qtt;

            // -- REGISTRE MOVIMENTS ----------------------------------------------------------------------------------------------
            // Descomtem Origen
            StockMove SM = new StockMove(StockMovementsType.Adjustment,
                                          WareORIG.idWareHouse, WareORIG.Descr, WareORIG.idWareHouseType,
                                          idItem, idOwner,"", Qtt);

            SM.DTArrival = System.DateTime.Now;
            SM.Remarks = remarks;

            SM.LstIdDocs = new List<string>(); if (LstidDoc != null && LstidDoc.Count > 0) foreach (string iD in LstidDoc) SM.LstIdDocs.Add(iD);

            SM.MoveType = StockMovementsType.Reservation;
            SM.TimeStamp = System.DateTime.Now;
            SM.idUser = IdUser;

            _LstStockMove.Add(SM);

            // Afegim Destí
            SM = new StockMove(StockMovementsType.Adjustment,
                                          WareORIG.idWareHouse, WareORIG.Descr, WareORIG.idWareHouseType,
                                          idItem, "","", -Qtt);
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
                foreach (DetLot DL in S.LstDetLots)
                {
                    //_LstStocksOrig.Add(new StockItem(S.idWareHouse, S.idWareHouseType, S.WareHouseName, S.idItem, DL.idLot, DL.Qtt));
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

        public Warehouse GetWareHouse(string idWareHouse, int idWareTYpe)
        {
            if (_LstWarehouses != null && _LstWarehouses.Count > 0)
            {
                foreach (Warehouse wr in _LstWarehouses)
                {
                    if (wr.idWareHouse == idWareHouse && wr.idWareHouseType == idWareTYpe)
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
