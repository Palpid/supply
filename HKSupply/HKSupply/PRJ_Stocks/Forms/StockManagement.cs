
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
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

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
            //try
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
                //this.CB_WareDEST.Items.Clear();
                //foreach (string S in STKAct.GetLstWarehouseNames())
                //{
                //    if (!CB_WareDEST.Items.Contains(S)) CB_WareDEST.Items.Add(S);
                //}
             
                //this.CB_NewOwner.Items.Clear();
                //foreach (Stocks.Owner O in STKAct.LstOwners)
                //{
                //    if (!CB_NewOwner.Items.Contains(O.OwnerName)) CB_NewOwner.Items.Add(O.OwnerName);
                //}                
                //this.Refresh();
            }
            //catch
            //{
            //    throw;
            //}
        }

        private void FormatejaGridStk(DevExpress.XtraGrid.GridControl GC)
        {          
            var GV = GC.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
            GV.OptionsView.ColumnAutoWidth = false;
            GV.HorzScrollVisibility = ScrollVisibility.Auto;
            GV.VertScrollVisibility = ScrollVisibility.Auto;

            GV.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            GV.OptionsView.ShowFooter = true;
            GV.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.ShowAlways;
            GV.OptionsView.ShowAutoFilterRow = true;
                    
            ColumnView V = GC.MainView as ColumnView;
            V.Columns.Clear();
            V.OptionsBehavior.AutoPopulateColumns = false;
            V.OptionsBehavior.Editable = false;

            GridGroupSummaryItem sitem = new GridGroupSummaryItem();
            sitem.FieldName = "Free";
            sitem.DisplayFormat = "{0:n0}";
            sitem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            sitem.ShowInGroupColumnFooter = V.Columns["FreeStock"];
            GV.GroupSummary.Add(sitem);

            GridGroupSummaryItem sitem2 = new GridGroupSummaryItem();
            sitem2.FieldName = "Assigned";
            sitem2.DisplayFormat = "{0:n0}";
            sitem2.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            sitem2.ShowInGroupColumnFooter = GV.Columns["FreeStock"];
            GV.GroupSummary.Add(sitem2);

            

            DevExpress.XtraGrid.Columns.GridColumn CL;
            int VI = 0;
            
            CL = new DevExpress.XtraGrid.Columns.GridColumn();
            V.Columns.Add(CL);
            CL.FieldName = "WareHouseName";
            CL.Caption = "Warehouse";
            CL.OptionsColumn.AllowEdit = false;
            CL.Width = 260;
            CL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.None;
            CL.VisibleIndex = VI;
            CL.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;                       
            //CL.BestFit();

            VI++;
            CL = new DevExpress.XtraGrid.Columns.GridColumn();
            V.Columns.Add(CL);
            CL.FieldName = "WareHouseType";
            CL.Caption = "Warehouse Type";
            CL.OptionsColumn.AllowEdit = false;
            CL.Width = 125;
            CL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.None;
            CL.VisibleIndex = VI;
            CL.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            //CL.BestFit();

            VI++;
            CL = new DevExpress.XtraGrid.Columns.GridColumn();
            V.Columns.Add(CL);
            CL.FieldName = "idItem";
            CL.Caption = "Item";
            CL.OptionsColumn.AllowEdit = false;
            CL.Width = 180;
            CL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.None;
            CL.VisibleIndex = VI;            
            CL.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            //CL.BestFit();

            VI++;
            CL = new DevExpress.XtraGrid.Columns.GridColumn();
            V.Columns.Add(CL);
            CL.FieldName = "item";
            CL.Caption = "Item";
            CL.OptionsColumn.AllowEdit = false;
            //CL.Width = 30;
            CL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.None;
            CL.VisibleIndex = VI;
            CL.Visible = false;
            CL.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            //CL.BestFit();

            VI++;
            CL = new DevExpress.XtraGrid.Columns.GridColumn();
            V.Columns.Add(CL);
            CL.FieldName = "FreeStock";            
            CL.Caption = "Free Stk.";
            CL.OptionsColumn.AllowEdit = false;
            CL.Width = 90;                       
            CL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            CL.DisplayFormat.FormatString = "n0";
            CL.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            CL.VisibleIndex = VI;
            //CL.BestFit();

            VI++;
            CL = new DevExpress.XtraGrid.Columns.GridColumn();
            V.Columns.Add(CL);
            CL.FieldName = "item.TotalAssignedQTT";
            CL.Caption = "Assigned.Stk.";
            CL.OptionsColumn.AllowEdit = false;
            CL.Width = 90;
            CL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            CL.DisplayFormat.FormatString = "n0";
            CL.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            CL.VisibleIndex = VI;
            //CL.BestFit();
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }
    }
}
