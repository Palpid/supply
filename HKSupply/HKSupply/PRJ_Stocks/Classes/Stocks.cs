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

        public List<Warehouse> _LstWareHouses = new List<Warehouse>();
        private List<StockItem> _LstStock = new List<StockItem>();
        private List<StockMove> _LstStockMove = new List<StockMove>();

        public List<Warehouse> LstWareHouses
        {
            get { return _LstWareHouses; }
            set { _LstWareHouses = value; }
        }
        
        public List<StockItem> LstStocks
        {
            get { return _LstStock; }
            set { _LstStock = value; }
        }

        public List<StockMove> LstStockMove
        {
            get { return _LstStockMove; }
            set { _LstStockMove = value; }
        }
               
        public class StockItem
        {
            public Warehouse Ware { get; set; }
            public Item Item { get; set; }
            public string idOwner { get; set; }
            public double QttStock { get; set; }


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
            public int idWareHouseType
            {
                get
                { return (int) Ware.WareHouseType; }
                set
                {
                    if (Ware == null ) Ware = new Warehouse();
                    Ware.WareHouseType = (StockWareHousesType)value;
                }
            }
            public string idItem
            {
                get
                { return (string)Item.idItem; }
                set
                {
                    if (Item == null) Item = new Item();
                    Item.idItem = value;
                }
            }
            public string Lot
            {
                get
                { return (string)Item.Lot; }
                set
                {
                    if (Item == null) Item = new Item();
                    Item.Lot = value;
                }
            }
        }

        public class StockMove
        {
            public Warehouse WareORIG { get; set; }
            public Warehouse WareDEST { get; set; }
            public Item Item { get; set; }
            public string idOwner { get; set; }
            public double QttMove { get; set; }

            public DateTime DTArrival { get; set; }
            public string Remarks { get; set; }
            public List<string> LstIdDocs { get; set; }

            public DateTime TimeStamp { get; set; }
            public string idUser { get; set; }

            public StockMovementsType MoveType { get; set; }
        }

        public class Warehouse
        {

            public string idWareHouse { get; set; }
            public string Descr { get; set; }
            public string Remarks { get; set; }
            public string idOwner { get; set; }
            public StockWareHousesType WareHouseType { get; set; }

            public int idWareHouseType {
                get
                { return (int)WareHouseType;}
                set
                {
                  WareHouseType = (StockWareHousesType) value; 
                }
            }
        }

        public class Item
        {
            public string idItem { get; set; }
            public string ItemName { get; set; }
            public string Lot { get; set; }
        }

        public class Owner
        {
            public string idOwner { get; set; }
            public string OwnerName { get; set; }
        }

        #endregion

        public void AddWareHouse(Warehouse Ware)
        {
            _LstWareHouses.Add(Ware);
        }

        #region OperacionsSobreDomini

        public void AddSockItem(Warehouse ware,  Item item, string idOwner, double Qtt, StockMovementsType MoveType, 
            string IdUser = "", string Remarks = "", List<string> LstidDoc = null)
        {
            // -- Test Param --
            if (ware.idWareHouse == "" | ware.idWareHouse == null) throw new System.ArgumentException("IdWarehouse can't be empty/null");
            if (item.idItem == "" | item.idItem == null) throw new System.ArgumentException("IdItem can't be empty/null");

            // -- REGISTRE STOCK ----------------------------------------------------------------------------------------
            StockItem SI = GetStockItem(ware,item, idOwner);
            if (SI == null)
            {
                if (Qtt < 0)
                    throw new System.InvalidOperationException("Not enough Stock on Origin Warehouse Available: 0, Requested: " + Qtt.ToString("###,###,###,###.00") + ")");
                else
                {
                    SI = new StockItem();
                    SI.Ware.idWareHouse = ware.idWareHouse;
                    SI.Ware.WareHouseType = ware.WareHouseType;
                    SI.Item.idItem = item.idItem;
                    SI.Item.Lot = item.Lot;
                    SI.idOwner = idOwner;
                    SI.QttStock = Qtt;
                    _LstStock.Add(SI);
                }
            }
            else
            {
                if (SI.QttStock + Qtt < 0)
                    throw new System.InvalidOperationException("Not enough Stock on Origin Warehouse (Available: " + SI.QttStock.ToString("###,###,###,###.00") + ", Requested: " + Qtt.ToString("###,###,###,###.00") + ")");
                else
                    SI.QttStock += Qtt; if (SI.QttStock == 0) _LstStock.Remove(SI);
            }

            // -- REGISTRE MOVIMENTS ----------------------------------------------------------------------------------------
            StockMove SM = new StockMove();
            SM.WareORIG.idWareHouse = "";
            SM.WareORIG.WareHouseType = StockWareHousesType.Undefined;
            SM.WareDEST.idWareHouse = ware.idWareHouse;
            SM.WareDEST.WareHouseType = ware.WareHouseType;
            SM.Item.idItem = item.idItem;
            SM.Item.Lot = item.Lot;
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

        public void AssignSockItemToOwner(Warehouse ware, Item item, string idOwner, double Qtt, StockMovementsType MoveType = StockMovementsType.Reservation,string IdUser = "", string remarks = "", List<string> LstidDoc = null)
        {
            // -- Test Param
            if (item.idItem == "" | item.idItem == null) throw new System.ArgumentException("IdItem can't be null");
            if (ware.idWareHouse == "" | ware.idWareHouse == null) throw new System.ArgumentException("idWarehouse can't be null");
            if (idOwner == "" | idOwner == null) throw new System.ArgumentException("idOwner can't be null");
            if (Qtt <= 0) throw new System.ArgumentException("Qtt to assing must be greather than 0", "Qtt");

            // -- REGISTRE STOCK ORIGEN ----------------------------------------------------------------------------------------
            StockItem SIO = GetStockItem(ware,item, "");  //Ha de ser STOCK Lliure (sense Owner)
            if (SIO == null)
                throw new System.InvalidOperationException("idITEM (" + item.idItem + ") & idLOT ( " + item.Lot + ") does not exist on Warehouse (" + ware.idWareHouse + "/" + ware.WareHouseType + ")");
            else
            {
                if (SIO.QttStock < Qtt) throw
                     new System.InvalidOperationException("Not enough free stock on Warehouse (Available:" + SIO.QttStock.ToString("###,###,###,###.00") + ", Requested:" + Qtt.ToString("###,###,###,###.00") + ")");
                else
                    SIO.QttStock -= Qtt; if (SIO.QttStock == 0) _LstStock.Remove(SIO);

            }
            // -- REGISTRE STOCK DESTI ----------------------------------------------------------------------------------------
            StockItem SID = GetStockItem(ware,item, idOwner);
            if (SID == null)
            {
                SID = new StockItem();
                SID.Ware.idWareHouse = ware.idWareHouse;
                SID.Ware.WareHouseType = ware.WareHouseType;
                SID.Item.idItem = item.idItem;
                SID.Item.Lot = item.Lot;
                SID.idOwner = idOwner;
                SID.QttStock = Qtt;
                _LstStock.Add(SID);
            }
            else
                SID.QttStock += Qtt; if (SID.QttStock == 0) _LstStock.Remove(SID);

            // -- REGISTRE MOVIMENTS ----------------------------------------------------------------------------------------
            StockMove SM = new StockMove();
            SM.WareORIG.idWareHouse = "";
            SM.WareORIG.WareHouseType = StockWareHousesType.Undefined;
            SM.WareDEST.idWareHouse = ware.idWareHouse;
            SM.WareDEST.WareHouseType = ware.WareHouseType;
            SM.Item.idItem = item.idItem;
            SM.Item.Lot = item.Lot;
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

        public void UnAssignSockItemToOwner(Warehouse ware, Item item, string idOwner, double Qtt, StockMovementsType MoveType = StockMovementsType.Release, string IdUser = "", string remarks = "", List<string> LstidDoc = null)
        {
            // -- Test Param
            if (item.idItem == "" | item.idItem == null) throw new System.ArgumentException("IdItem can't be null");
            if (ware.idWareHouse == "" | ware.idWareHouse == null) throw new System.ArgumentException("idWarehouse can't be null");
            if (idOwner == "" | idOwner == null) throw new System.ArgumentException("idOwner can't be null");
            if (Qtt <= 0) throw new System.ArgumentException("Qtt to assing must be greather than 0", "Qtt");

            // -- REGISTRE STOCK ORIGEN ----------------------------------------------------------------------------------------
            StockItem SIO = GetStockItem(ware, item, idOwner);  //Ha de ser STOCK reservat (del Owner)
            if (SIO == null)
                throw new System.InvalidOperationException("idITEM (" + item.idItem + ") & idLOT ( " + item.Lot + ") & idOwner ( " + idOwner + " ) does not exist on Warehouse (" + ware.idWareHouse + "/" + ware.WareHouseType + ")");
            else
            {
                if (SIO.QttStock < Qtt) throw
                     new System.InvalidOperationException("Not enough assigned stock on Warehouse (Available:" + SIO.QttStock.ToString("###,###,###,###.00") + ", Requested:" + Qtt.ToString("###,###,###,###.00") + ")");
                else
                    SIO.QttStock -= Qtt; if (SIO.QttStock == 0) _LstStock.Remove(SIO);

            }
            // -- REGISTRE STOCK DESTI ----------------------------------------------------------------------------------------
            StockItem SID = GetStockItem(ware,item, "");
            if (SID == null)
            {
                SID = new StockItem();
                SID.Ware.idWareHouse = ware.idWareHouse;
                SID.Ware.WareHouseType = ware.WareHouseType;
                SID.Item.idItem = item.idItem;
                SID.Item.Lot = item.Lot;
                SID.idOwner = "";
                SID.QttStock = Qtt;
                _LstStock.Add(SID);
            }
            else
                SID.QttStock += Qtt; if (SID.QttStock == 0) _LstStock.Remove(SID);

            // -- REGISTRE MOVIMENTS ----------------------------------------------------------------------------------------
            StockMove SM = new StockMove();
            SM.WareORIG.idWareHouse = "";
            SM.WareORIG.WareHouseType = StockWareHousesType.Undefined;
            SM.WareDEST.idWareHouse = ware.idWareHouse  ;
            SM.WareDEST.WareHouseType = ware.WareHouseType;
            SM.Item.idItem = item.idItem;
            SM.Item.Lot = item.Lot;
            SM.idOwner = "";
            SM.QttMove = Qtt;

            SM.DTArrival = System.DateTime.Now;
            SM.Remarks = remarks;

            SM.LstIdDocs = new List<string>(); if (LstidDoc != null && LstidDoc.Count > 0) foreach (string iD in LstidDoc) SM.LstIdDocs.Add(iD);

            SM.MoveType = MoveType;
            SM.TimeStamp = System.DateTime.Now;
            SM.idUser = IdUser;

            _LstStockMove.Add(SM);
        }

        public void ChangeQttSockItem(Warehouse ware, Item item, string idOwner, double NewQtt, StockMovementsType MoveType = StockMovementsType.Adjustment, string IdUser = "", string Remarks = "", List<string> LstidDoc = null)
        {
            // -- Test Param --
            if (ware.idWareHouse == "" | ware.idWareHouse == null) throw new System.ArgumentException("IdWarehouse can't be null");
            if (item.idItem == "" | item.idItem == null) throw new System.ArgumentException("IdItem can't be null");
            if (NewQtt < 0) throw new System.ArgumentException("Qtt adjusted must be greather or equal to 0", "Qtt");

            // -- REGISTRE STOCK ----------------------------------------------------------------------------------------
            StockItem SI = GetStockItem(ware, item, idOwner);
            double oldStk;
            if (SI == null)
                throw new System.InvalidOperationException("idITEM (" + item.idItem + ") & idLOT ( " + item.Lot + ") & idOwner ( " + idOwner + " ) does not exist on WarehouseORIG (" + ware.idWareHouse + "/" + ware.WareHouseType +")");
            else
            {
                oldStk = SI.QttStock;
                SI.QttStock = NewQtt;
                if (SI.QttStock == 0) _LstStock.Remove(SI);
            }

            // -- REGISTRE MOVIMENTS ----------------------------------------------------------------------------------------
            StockMove SM = new StockMove();
            SM.WareORIG.idWareHouse = "";
            SM.WareORIG.WareHouseType= StockWareHousesType.Undefined;
            SM.WareDEST.idWareHouse = ware.idWareHouse;
            SM.WareDEST.WareHouseType = ware.WareHouseType;
            SM.Item.idItem = item.idItem;
            SM.Item.Lot = item.Lot;
            SM.idOwner = idOwner;
            SM.QttMove = (NewQtt - oldStk);

            SM.DTArrival = System.DateTime.Now;
            SM.Remarks = Remarks;
            SM.LstIdDocs = new List<string>(); if (LstidDoc != null && LstidDoc.Count > 0) foreach (string iD in LstidDoc) SM.LstIdDocs.Add(iD);

            SM.TimeStamp = System.DateTime.Now;
            SM.idUser = IdUser;
            SM.MoveType = MoveType;

            _LstStockMove.Add(SM);
        }

        public void MoveSockItem(Warehouse WareORIG, Warehouse WareDEST, Item item, string idOwner, double Qtt, StockMovementsType MoveType, string IdUser = "", string remarks = "", List<string> LstidDoc = null)
        {
            // -- Test Param
            if (item.idItem == "" | item.idItem == null) throw new System.ArgumentException("IdItem can't be null");
            if (WareORIG.idWareHouse == "" | WareORIG.idWareHouse == null) throw new System.ArgumentException("idWarehouseORIG can't be null");
            if (WareDEST.idWareHouse == "" | WareDEST.idWareHouse == null) throw new System.ArgumentException("idWarehouseDEST can't be null");
            if (Qtt <= 0) throw new System.ArgumentException("Qtt to move must be greather than 0", "Qtt");

            // -- REGISTRE STOCK ORIGEN ----------------------------------------------------------------------------------------
            StockItem SIO = GetStockItem(WareORIG, item, idOwner);
            if (SIO == null)
                throw new System.InvalidOperationException("idITEM (" + item.idItem + ") & idLOT ( " + item.Lot + ") & idOwner ( " + idOwner + " ) does not exist on WarehouseORIG (" + WareORIG.idWareHouse + "/" + WareORIG.WareHouseType +")");
            else
            {
                if (SIO.QttStock < Qtt) throw
                     new System.InvalidOperationException("Not enough Stock on Origin Warehouse (Available:" + SIO.QttStock.ToString("###,###,###,###.00") + ", Requested:" + Qtt.ToString("###,###,###,###.00") + ")");
                else
                    SIO.QttStock -= Qtt; if (SIO.QttStock == 0) _LstStock.Remove(SIO);

            }
            // -- REGISTRE STOCK DESTI ----------------------------------------------------------------------------------------
            StockItem SID = GetStockItem(WareDEST, item, idOwner);
            if (SID == null)
            {
                SID = new StockItem();
                SID.Ware.idWareHouse = WareDEST.idWareHouse;
                SID.Ware.WareHouseType = WareDEST.WareHouseType;
                SID.Item.idItem = item.idItem;
                SID.Item.Lot = item.Lot;
                SID.idOwner = idOwner;
                SID.QttStock = Qtt;
                _LstStock.Add(SID);
            }
            else
                SID.QttStock += Qtt; if (SID.QttStock == 0) _LstStock.Remove(SID);

            // -- REGISTRE MOVIMENTS ----------------------------------------------------------------------------------------
            StockMove SM = new StockMove();
            SM.WareORIG.idWareHouse = WareORIG.idWareHouse;
            SM.WareORIG.WareHouseType = WareORIG.WareHouseType;
            SM.WareDEST.idWareHouse = WareDEST.idWareHouse;
            SM.WareDEST.WareHouseType = WareDEST.WareHouseType;
            SM.Item.idItem = item.idItem;
            SM.Item.Lot = item.Lot;
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

        #endregion

        #region ConsultaDomini

        public Warehouse GetWareHouse(string idWareHouse, StockWareHousesType WareTYpe)
        {
            if (_LstWareHouses != null && _LstWareHouses.Count > 0)
            {
                foreach (Warehouse wr in _LstWareHouses)
                {
                    if (wr.idWareHouse == idWareHouse && wr.WareHouseType == WareTYpe )
                    {
                        return wr;
                    }
                }
            }
            return null;
        }
        
        public StockItem GetStockItem(Warehouse ware,  Item item, string idOwner)
        {
            if (_LstStock != null && _LstStock.Count > 0)
            {
                foreach (StockItem si in _LstStock)
                {
                    if (si.Ware.idWareHouse == ware.idWareHouse && si.Ware.WareHouseType == ware.WareHouseType && si.Item.idItem == item.idItem && si.Item.Lot == item.Lot && si.idOwner == idOwner)
                    {
                        return si;
                    }
                }
            }
            return null;
        }

        public double GetStockItemValue(Warehouse ware, Item item, string idOwner)
        {
            if (_LstStock != null && _LstStock.Count > 0)
            {
                foreach (StockItem si in _LstStock)
                {
                    if (si.Ware.idWareHouse == ware.idWareHouse && si.Ware.WareHouseType == ware.WareHouseType && si.Item.idItem == item.idItem && si.Item.Lot == item.Lot && si.idOwner == idOwner)
                    {
                        return si.QttStock;
                    }
                }
            }
            return 0;
        }

        public List<StockItem> ListStockFilteredOR(Warehouse ware, Item item , string idOwner, double QttMin, double QttMax)
        {
            // Llista de stock items que compleix ALGUNA de les condicions
            List<StockItem> LI = new List<StockItem>();

            if (_LstStock != null && _LstStock.Count > 0)
            {
                foreach (StockItem si in _LstStock)
                {
                    if (si.Ware.idWareHouse == ware.idWareHouse || si.Ware.WareHouseType == ware.WareHouseType || si.Item.idItem == item.idItem || si.Item.Lot == item.Lot || si.idOwner == idOwner || (si.QttStock >= QttMin && si.QttStock <= QttMax))
                    {
                        LI.Add(si);
                    }
                }
            }
            return LI;
        }

        public List<StockItem> ListStockFilteredOR(Warehouse ware, Item item, string idOwner)
        {
            // Llista de stock items que compleix ALGUNA de les condicions
            List<StockItem> LI = new List<StockItem>();

            if (_LstStock != null && _LstStock.Count > 0)
            {
                foreach (StockItem si in _LstStock)
                {
                    if (si.Ware.idWareHouse == ware.idWareHouse || si.Ware.WareHouseType == ware.WareHouseType || si.Item.idItem == item.idItem || si.Item.Lot == item.Lot || si.idOwner == idOwner)
                    {
                        LI.Add(si);
                    }
                }
            }
            return LI;
        }

        public List<StockItem> ListStockFilteredAND(Warehouse ware, Item item, string idOwner, double QttMin, double QttMax)
        {
            // Llista de stock items que compleix TOTES de les condicions
            List<StockItem> LI = new List<StockItem>();

            if (_LstStock != null && _LstStock.Count > 0)
            {
                foreach (StockItem si in _LstStock)
                {
                    if (si.Ware.idWareHouse == ware.idWareHouse && si.Ware.WareHouseType == ware.WareHouseType && si.Item.idItem == item.idItem && si.Item.Lot == item.Lot && si.idOwner == idOwner && (si.QttStock >= QttMin) && (si.QttStock <= QttMax))
                    {
                        LI.Add(si);
                    }
                }
            }
            return LI;
        }

        public List<StockItem> ListStockFilteredAND(Warehouse ware, Item item, string idOwner)
        {
            // Llista de stock items que compleix TOTES de les condicions
            List<StockItem> LI = new List<StockItem>();

            if (_LstStock != null && _LstStock.Count > 0)
            {
                foreach (StockItem si in _LstStock)
                {
                    if (si.Ware.idWareHouse == ware.idWareHouse && si.Ware.WareHouseType == ware.WareHouseType && si.Item.idItem == item.idItem && si.Item.Lot == item.Lot && si.idOwner == idOwner)
                    {
                        LI.Add(si);
                    }
                }
            }
            return LI;
        }

        #endregion

        
    }
}
