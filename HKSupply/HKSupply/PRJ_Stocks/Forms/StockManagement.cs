
using HKSupply.General;
using HKSupply.PRJ_Stocks.DB;
using HKSupply.PRJ_Stocks.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevExpress.XtraGrid.Views.Base;

using HKSupply.DB;


namespace HKSupply.PRJ_Stocks.Forms
{
    public partial class StockManagement : Form
    {
        private Stocks STKAct = new Stocks();

        public StockManagement()
            
        {            
            InitializeComponent();
                      
        }

        private void StockManagement_Load(object sender, EventArgs e)
        {
            try
            {
                this.Show();

                var actions = GlobalSetting.FunctionalitiesRoles.FirstOrDefault(fr => fr.Functionality.FormName.Equals(Name));
                // Permisos que te l'usuari
                bool CanRead = actions.Read;
                bool CanNew = actions.New;
                bool CanModify = actions.Modify;

                FormatejaGridStk(this.GC_Stocks);

                Call_DB_Stocks CallDBS = new Call_DB_Stocks();
                STKAct = CallDBS.CallCargaStocks();

                GC_Stocks.DataSource = STKAct.LstStocks;

                //- Inicialitzem els combos
                this.CB_WareDEST.Items.Clear();
                foreach (string S in STKAct.GetLstWarehouseNames())
                {
                    CB_WareDEST.Items.Add(S);
                }

                this.CB_WareTypeDEST.Items.Clear();
                //CB_WareDEST.Items.Add(Stocks.StockWareHousesType.OnHand.ToString());
                //CB_WareDEST.Items.Add(Stocks.StockWareHousesType.Assigned.ToString());
                //CB_WareDEST.Items.Add(Stocks.StockWareHousesType.Deliveries.ToString());
                //CB_WareDEST.Items.Add(Stocks.StockWareHousesType.Transit.ToString());
                foreach (Stocks.Warehouse W in STKAct.LstWarehouses)
                {
                    if (!CB_WareTypeDEST.Items.Contains(W.idWareHouseType.ToString())) CB_WareTypeDEST.Items.Add(W.WareHouseType.ToString());
                }

                //this.CB_NewOwner.Items.Clear();
                //foreach (Stocks.Item I in STKAct.LstItems)
                //{
                //    if (!CB_WareDEST.Items.Contains(W.Descr)) CB_WareDEST.Items.Add(W.Descr);
                //}
                
                this.Refresh();
            }
            catch
            {
                throw;
            }
        }

        private void FormatejaGridStk(DevExpress.XtraGrid.GridControl GC)
        {

            ColumnView V = GC.MainView as ColumnView;
            V.Columns.Clear();
            V.OptionsBehavior.AutoPopulateColumns = false;


            var GV = GC.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
            GV.OptionsView.ColumnAutoWidth = false;
            GV.HorzScrollVisibility = ScrollVisibility.Auto;
            GV.VertScrollVisibility = ScrollVisibility.Auto;



            DevExpress.XtraGrid.Columns.GridColumn CL;
            int VI = 0;
            
            CL = new DevExpress.XtraGrid.Columns.GridColumn();
            V.Columns.Add(CL);
            CL.FieldName = "WareHouseName";
            CL.Caption = "Warehouse";
            CL.OptionsColumn.AllowEdit = true;
            CL.Width = 30;
            CL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.None;
            CL.VisibleIndex = VI;
            CL.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;                       
            CL.BestFit();

            VI++;
            CL = new DevExpress.XtraGrid.Columns.GridColumn();
            V.Columns.Add(CL);
            CL.FieldName = "Ware.WareHouseType";
            CL.Caption = "Warehouse Type";
            CL.OptionsColumn.AllowEdit = true;
            CL.Width = 30;
            CL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.None;
            CL.VisibleIndex = VI;
            CL.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            CL.BestFit();

            VI++;
            CL = new DevExpress.XtraGrid.Columns.GridColumn();
            V.Columns.Add(CL);
            CL.FieldName = "Item.idItem";
            CL.Caption = "Item";
            CL.OptionsColumn.AllowEdit = true;
            CL.Width = 30;
            CL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.None;
            CL.VisibleIndex = VI;
            CL.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            CL.BestFit();

            VI++;
            CL = new DevExpress.XtraGrid.Columns.GridColumn();
            V.Columns.Add(CL);
            CL.FieldName = "Item.Lot";
            CL.Caption = "Lot";
            CL.OptionsColumn.AllowEdit = true;
            CL.Width = 30;
            CL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.None;
            CL.VisibleIndex = VI;
            CL.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            CL.BestFit();

            VI++;
            CL = new DevExpress.XtraGrid.Columns.GridColumn();
            V.Columns.Add(CL);
            CL.FieldName = "idOwner";
            CL.Caption = "Owner";
            CL.OptionsColumn.AllowEdit = true;
            CL.Width = 30;
            CL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.None;
            CL.VisibleIndex = VI;
            CL.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            CL.BestFit();
                      

            VI++;
            CL = new DevExpress.XtraGrid.Columns.GridColumn();
            V.Columns.Add(CL);
            CL.FieldName = "QttStock";            
            CL.Caption = "Quantity";
            CL.OptionsColumn.AllowEdit = true;
            CL.Width = 30;                       
            CL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            CL.DisplayFormat.FormatString = "n0";
            CL.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            CL.VisibleIndex = VI;
            CL.BestFit();
                        
        }
    }
}
