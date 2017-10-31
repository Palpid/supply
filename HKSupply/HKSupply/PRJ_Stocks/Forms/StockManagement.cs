
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
        private Stocks gSTKAct = new Stocks();
        private Stocks.StockItem gSIAct = new Stocks.StockItem();

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
                                
                this.txtFreeStk.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                this.txtAsgStk.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                this.txtTotalStk.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                this.txtOnwStk.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                this.txtNewQTT.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;

                Call_DB_Stocks CallDBS = new Call_DB_Stocks();
                gSTKAct = CallDBS.CallCargaStocks();

                GC_Stocks.DataSource = gSTKAct.LstStocks;
                GC_Movs.DataSource = gSTKAct.LstStockMove;
                               

                //- Inicialitzem els combos dest
                UpdateCombo(CB_OwnDEST, gSTKAct.GetLstOwnerIds());
                UpdateCombo(CB_WareDEST, gSTKAct.GetLstWarehouseNames());
                                
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
            CL.FieldName = "LstDetRes()";
            CL.Caption = "Assignments";
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
               

        private void GC_Stocks_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            ColumnView V = this.GC_Stocks.MainView as ColumnView;
            
            gSIAct = (Stocks.StockItem)V.GetFocusedRow();

            ActulitzaPanel(gSIAct);
                       
            this.Refresh();

            Cursor = Cursors.Default;
        }

        private void UpdateCombo(ComboBox CB, List<string> LstItems)
        {
             
          
            CB.Items.Clear();
            if (LstItems.Count > 0)
            {
                foreach (string itm in LstItems) if (!CB.Items.Contains(itm) && itm!="") CB.Items.Add(itm);

                if (CB.Items.Count == 0) CB.Enabled = false;
                if (CB.Items.Count == 1)
                {
                    CB.Enabled = false;
                    CB.SelectedIndex = 0;
                    BtnFreeOwner.Enabled = true;
                }
                if (CB.Items.Count > 1)
                {
                    CB.Enabled = true;
                    CB.SelectedIndex = -1;
                }
            }
            
        }
        
        private void ActulitzaPanel(Stocks.StockItem SI)
        {
            BtnFreeOwner.Enabled = false;
            BtnStkAdjust.Enabled = false;
            BtnAsingToOwner.Enabled = false;
            BtnMoveStk.Enabled = false;
                                    
            this.txtWare.Text = SI.WareHouseName;
            this.txtWareType.Text = SI.WareHouseTypeName;
            this.txtItem.Text = SI.item.idItem;
            this.txtFreeStk.Text = SI.item.TotalFreeQTT.ToString("n0");
            this.txtAsgStk.Text = SI.item.TotalAssignedQTT.ToString("n0");
            this.txtTotalStk.Text = SI.item.TotalQTT.ToString("n0");
            this.txtOnwStk.Text = "0";            
            this.txtNewQTT.Text = "0";
            UpdateCombo(CB_Owner, SI.LstidOwners());            
            UpdateCombo(CB_WareDEST, gSTKAct.GetLstWarehouseNames());

            if (CB_Owner.SelectedIndex > 1) BtnFreeOwner.Enabled = true;

            if(CB_OwnDEST.SelectedIndex>0 && CB_OwnDEST.SelectedItem.ToString() != "" && SI.item.TotalFreeQTT > 0) BtnAsingToOwner.Enabled = true;
            
        }

        private void BtnFreeOwner_Click(object sender, EventArgs e)
        {
            //Desasignem tot el STOCK de un Owner del item i magazemt donats.
            Classes.Stocks.StockItem SIO = gSIAct;

            string idOwner = (string)CB_Owner.SelectedItem;

            if (idOwner != "")
            {
                int StkReleased = gSTKAct.FreeSockItem(SIO.ware, SIO.idItem, idOwner);
                this.BtnFreeOwner.Enabled = false;                             
            };

            ActulitzaPanel(gSIAct);
            this.Refresh();
                        
        }

        private void CB_Owner_SelectedIndexChanged(object sender, EventArgs e)
        {
            //el combo només esta "viu" si hi a un stockitem seleccionat que te més d'un owner No cal validar gSIAact estigui carregat.
            BtnFreeOwner.Enabled = true;
            string idOwn = CB_Owner.SelectedItem.ToString();
            txtOnwStk.Text = gSIAct.item.StkOwner(idOwn).ToString("n0");
            this.Refresh();
        }

        private void BtnStkAdjust_Click(object sender, EventArgs e)
        {
            //el BOTO només esta "viu" si hi a un stockitem seleccionat que te més d'un owner No cal validar gSIAact estigui carregat.
            Classes.Stocks.StockItem SIO = gSIAct;

            if(CB_Owner.SelectedIndex>=0)            
            {
                MessageBox.Show("Only free stock can be adjusted.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                int ValVol = Int32.Parse(txtNewQTT.Text); // es >0, controlat per el txtbox
                int FreeStk = SIO.item.TotalFreeQTT;
                int Variacio = ValVol - FreeStk;
                int StkAjustat = gSTKAct.AdjustSockItem(gSIAct.ware, gSIAct.idItem, Variacio);
            }

            this.Refresh();    
                    
        }
            
        

        private void CB_OwnDEST_SelectedIndexChanged(object sender, EventArgs e)
        {
            // no cal fer res.
        }

        private void txtNewQTT_EditValueChanged(object sender, EventArgs e)
        {
            BtnStkAdjust.Enabled = false;
            int Val = 0;
            bool ok = false;

            if (int.TryParse(txtNewQTT.Text, out Val)){
                if (Val >= 0) {
                    ok = true; BtnStkAdjust.Enabled = true;
                }
                else
                    ok= false;
                }
            else
            {
                ok = false;                
            }

            if (!ok) MessageBox.Show("New Quantity must be a positive number or zero.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);                
        }

        private void BtnAsingToOwner_Click(object sender, EventArgs e)
        {            
            int QttFree = gSIAct.item.TotalFreeQTT;
            int ValVol = Int32.Parse(txtNewQTT.Text);

            if (ValVol > QttFree)
                MessageBox.Show("There is no enough Free stock to be asgined.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            else {
                string idOwn = CB_OwnDEST.SelectedItem.ToString();
                gSTKAct.AsgnSockItem(gSIAct.ware, gSIAct.idItem, ValVol, idOwn);
                ActulitzaPanel(gSIAct);
                this.Refresh();
            }
           
        }

        private void CB_WareDEST_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CB_WareDEST.SelectedIndex >= 0) BtnMoveStk.Enabled = true;
        }

        private void BtnMoveStk_Click(object sender, EventArgs e)
        {
            string idOwnOrig = "";
            if (CB_Owner.SelectedIndex>=0) idOwnOrig = CB_Owner.SelectedItem.ToString();

            string idOwnDest = "";
            if (CB_OwnDEST.SelectedIndex>=0) idOwnDest = CB_OwnDEST.SelectedItem.ToString();


            Classes.Stocks.Warehouse WDst = new Stocks.Warehouse();
            WDst =  gSTKAct.GetWareHouse(CB_WareDEST.SelectedItem.ToString());
            Classes.Stocks.Warehouse WOrg = gSIAct.ware;
            string idIem = gSIAct.idItem;
            
            int ValVol = Int32.Parse(txtNewQTT.Text);

            int QttOrigen = 0;
            if (idOwnOrig == "")
                QttOrigen = gSIAct.item.TotalFreeQTT;
            else
                QttOrigen = gSIAct.item.StkOwner(idOwnOrig);

            if (ValVol > QttOrigen)
                MessageBox.Show("There is no enough stock to be moved.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                gSTKAct.MoveSockItem(Stocks.StockMovementsType.Transit,WOrg,WDst,ValVol,idIem,idOwnDest);
                ActulitzaPanel(gSIAct);
                this.Refresh();
            }                        
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Call_DB_Stocks CallDBS = new Call_DB_Stocks();
            CallDBS.CallGuardarStocks(gSTKAct);        
        }

        private void BtnClearOwner_Click(object sender, EventArgs e)
        {
            this.CB_OwnDEST.SelectedIndex = -1;
            this.Refresh();
        }

        private void BtnAdjustOwner_Click(object sender, EventArgs e)
        {
            int QttFree = gSIAct.item.TotalFreeQTT;
            int ValVol = Int32.Parse(txtNewQTT.Text);

            if (ValVol > QttFree)
                MessageBox.Show("There is no enough Free stock to be asgined.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                string idOwn = CB_Owner.SelectedItem.ToString();
                gSTKAct.AdjustItemAssing(gSIAct.ware, gSIAct.idItem, ValVol, idOwn);
                ActulitzaPanel(gSIAct);
                this.Refresh();
            }
        }
    }
}

