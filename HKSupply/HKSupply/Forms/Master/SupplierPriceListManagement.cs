using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout.Utils;
using HKSupply.General;
using HKSupply.Helpers;
using HKSupply.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace HKSupply.Forms.Master
{
    public partial class SupplierPriceListManagement : RibbonFormBase
    {
        #region Enums
        /*private enum eSupplierPriceListColumns
        { 
            IdVer,
            IdSubVer,
            Timestamp,
            IdItemBcn,
            IdSupplier,
            Price,
            Comments,
            IdCurrency,
            PriceBaseCurrency,
            ExchangeRateUsed,
            MinLot,
            IncrLot,
            LeadTime,
        }*/
        #endregion

        #region Private Members

        SupplierPriceList _supplierPriceListUpdate;
        SupplierPriceList _supplierPriceListOriginal;
        SupplierPriceListHistory _supplierPriceListHistory;

        List<SupplierPriceList> _suppliersPriceListList;
        List<SupplierPriceListHistory> _supplierPriceListHistoryList;

        List<SupplierPriceList> _modifiedRowsList = new List<SupplierPriceList>();

        List<ItemBcn> _itemBcnList;
        List<Supplier> _suppliersList;
        List<Currency> _currenciesList;

        string[] _nonEditingFields = { "lueIdItemBcn", "lueIdSupplier", "txtIdVersion", "txtIdSubversion", "txtTimestamp" };
        string[] _mandatoryEditingFields = { "lueIdItemBcn", "lueIdSupplier", "txtPrice", "lueIdCurrency", "txtPriceBaseCurrency", "txtExchangeRateUsed" };

        int _currentHistoryNumList;

        #endregion

        #region Constructor
        public SupplierPriceListManagement()
        {
            InitializeComponent();

            try
            {
                ConfigureRibbonActions();
                LoadCurrenciesList();
                SetUpGrdSuppliersPriceList();
                SetUpTextEdit();
                SetUpSearchLueSupplier();
                SetUpSearchLueItemBcn();
                ResetSupplierPriceListUpdate();
                SetFormBinding();
                LoadSuppliersList();
                LoadItemBcnList();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Ribbon

        private void ConfigureRibbonActions()
        {
            try
            {
                //Task Buttons
                SetActions();
                RestoreInitState();
                //Print and export buttons
                EnablePrintPreview = false;
                EnableExportExcel = true;
                EnableExportCsv = true;
                ConfigurePrintExportOptions();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void bbiCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            base.bbiCancel_ItemClick(sender, e);

            try
            {
                _supplierPriceListOriginal = null;
                ResetSupplierPriceListUpdate();
                SetFormBinding();
                xtpForm.PageVisible = false;
                xtpList.PageVisible = true;
                sbNewVersion.Visible = false;
                LoadSuppliersPriceList();
                SetNonCreatingFieldsVisibility(LayoutVisibility.Always);
                sbLoad.Enabled = true;
                rootGridViewSuppliersPriceList.DoubleClick += rootGridViewSuppliersPriceList_DoubleClick;
                rootGridViewSuppliersPriceList.OptionsBehavior.Editable = false;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public override void bbiEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            base.bbiEdit_ItemClick(sender, e);

            try
            {
                if (xtcGeneral.SelectedTabPage == xtpList && rootGridViewSuppliersPriceList.DataRowCount == 0)
                {
                    MessageBox.Show(GlobalSetting.ResManager.GetString("NoDataSelected"));
                    RestoreInitState();
                }
                //else if (_supplierPriceListOriginal == null)
                //{
                //    MessageBox.Show("No supplier price selected");
                //    RestoreInitState();
                //}
                else
                {
                    ConfigureRibbonActionsEditing();
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public override void bbiNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            base.bbiNew_ItemClick(sender, e);

            try
            {
                ConfigureRibbonActionsCreating();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public override void bbiSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            base.bbiSave_ItemClick(sender, e);

            try
            {
                bool res = false;

                if (IsValidSupplierPriceList() == false)
                    return;

                DialogResult result = MessageBox.Show(GlobalSetting.ResManager.GetString("SaveChanges"), "", MessageBoxButtons.YesNo);

                if (result != DialogResult.Yes)
                    return;

                Cursor = Cursors.WaitCursor;

                if (xtcGeneral.SelectedTabPage == xtpForm)
                {
                    if (_supplierPriceListUpdate.Equals(_supplierPriceListOriginal))
                    {
                        MessageBox.Show(GlobalSetting.ResManager.GetString("NoPendingChanges"));
                        return;
                    }
                }
                else if (xtcGeneral.SelectedTabPage == xtpList)
                {
                    if (_modifiedRowsList.Count == 0)
                    {
                        MessageBox.Show(GlobalSetting.ResManager.GetString("NoPendingChanges"));
                        return;
                    }
                }

                if (CurrentState == ActionsStates.Edit)
                {
                    if (xtcGeneral.SelectedTabPage == xtpForm)
                        res = UpdateSupplierPriceList(); //Actualizamos sólo uno
                    else if (xtcGeneral.SelectedTabPage == xtpList)
                        res = UpdateSuppliersPricesList(); //Actualizamos varios (se ha hecho desde el grid)
                }
                else if (CurrentState == ActionsStates.New)
                {
                    res = CreateSupplierPriceList();
                }

                if (res == true)
                {
                    MessageBox.Show(GlobalSetting.ResManager.GetString("SaveSuccessfully"));
                    ActionsAfterCU();
                }


            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        public override void bbiExportExcel_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (rootGridViewSuppliersPriceList.DataRowCount == 0)
            {
                MessageBox.Show(GlobalSetting.ResManager.GetString("NoDataSelected"));
                return;
            }

            //Abre el dialog de save as
            base.bbiExportExcel_ItemClick(sender, e);

            try
            {
                if (string.IsNullOrEmpty(ExportExcelFile) == false)
                {
                    rootGridViewSuppliersPriceList.ExportToXlsx(ExportExcelFile);

                    DialogResult result = MessageBox.Show(GlobalSetting.ResManager.GetString("OpenFileQuestion"), "", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(ExportExcelFile);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public override void bbiExportCsv_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (rootGridViewSuppliersPriceList.DataRowCount == 0)
            {
                MessageBox.Show(GlobalSetting.ResManager.GetString("NoDataSelected"));
                return;
            }

            //Abre el dialog de save as
            base.bbiExportCsv_ItemClick(sender, e);

            try
            {
                if (string.IsNullOrEmpty(ExportCsvFile) == false)
                {
                    rootGridViewSuppliersPriceList.ExportToCsv(ExportCsvFile);

                    DialogResult result = MessageBox.Show(GlobalSetting.ResManager.GetString("OpenFileQuestion"), "", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(ExportCsvFile);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Form Events

        private void SupplierPriceListManagement_Load(object sender, EventArgs e)
        {
            try
            {
                xtpForm.PageVisible = false;
                sbNewVersion.Visible = false;

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void sbLoad_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                LoadSuppliersPriceList();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        void rootGridViewSuppliersPriceList_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                SupplierPriceList supplierPriceList = view.GetRow(view.FocusedRowHandle) as SupplierPriceList;
                if (supplierPriceList != null)
                {
                    LoadSupplierPriceListForm(supplierPriceList);
                    LoadSupplierPriceListHistory(supplierPriceList.IdItemBcn, supplierPriceList.IdSupplier);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void rootGridViewSuppliersPriceList_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            try
            {
                if (CurrentState == ActionsStates.Edit)
                {
                    GridView view = sender as GridView;
                    SupplierPriceList supplierPriceList = view.GetRow(view.FocusedRowHandle) as SupplierPriceList;
                    if (supplierPriceList != null)
                    {
                        AddModifiedRowToList(supplierPriceList);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void sbNewVersion_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                Validate();

                if (_supplierPriceListUpdate.Equals(_supplierPriceListOriginal))
                {
                    MessageBox.Show(GlobalSetting.ResManager.GetString("NoPendingChanges"));
                    return;
                }

                if (IsValidSupplierPriceListForm() == false)
                    return;

                if (UpdateSupplierPriceList(true))
                {
                    ActionsAfterCU();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void sbSetCurrentSubversion_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show(GlobalSetting.ResManager.GetString("SaveChanges"), "", MessageBoxButtons.YesNo);

                if (result != DialogResult.Yes)
                    return;

                Cursor = Cursors.WaitCursor;

                _supplierPriceListUpdate = _supplierPriceListHistory;
                _supplierPriceListUpdate.IdSubVer = _supplierPriceListOriginal.IdSubVer;
                _supplierPriceListUpdate.IdVer = _supplierPriceListOriginal.IdVer;

                if (UpdateSupplierPriceList())
                {
                    ActionsAfterCU();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void sbSetCurrentVersion_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show(GlobalSetting.ResManager.GetString("SaveChanges"), "", MessageBoxButtons.YesNo);

                if (result != DialogResult.Yes)
                    return;

                Cursor = Cursors.WaitCursor;

                _supplierPriceListUpdate = _supplierPriceListHistory;
                _supplierPriceListUpdate.IdSubVer = _supplierPriceListOriginal.IdSubVer;
                _supplierPriceListUpdate.IdVer = _supplierPriceListOriginal.IdVer;

                if (UpdateSupplierPriceList(true))
                {
                    ActionsAfterCU();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void sbBackward_Click(object sender, EventArgs e)
        {
            try
            {
                SetCurrentSupplierPriceListHistory(_currentHistoryNumList - 1);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void sbForward_Click(object sender, EventArgs e)
        {
            try
            {
                SetCurrentSupplierPriceListHistory(_currentHistoryNumList + 1);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void slueSupplier_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                slueSupplier.EditValue = null;
                e.Handled = true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void slueItemBcn_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                slueItemBcn.EditValue = null;
                e.Handled = true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Private Methods

        private void ResetSupplierPriceListUpdate()
        {
            _supplierPriceListUpdate = new SupplierPriceList();
        }

        #region Setup Form Objects

        private void SetUpGrdSuppliersPriceList()
        {
            try
            {
                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                rootGridViewSuppliersPriceList.OptionsView.ColumnAutoWidth = false;
                rootGridViewSuppliersPriceList.HorzScrollVisibility = ScrollVisibility.Auto;

                //hacer todo el grid no editable
                rootGridViewSuppliersPriceList.OptionsBehavior.Editable = false;

                //Columns definition
                GridColumn colIdVer = new GridColumn() { Caption = "Version", Visible = true, FieldName = nameof(SupplierPriceList.IdVer), Width = 50 };
                GridColumn colIdSubVer = new GridColumn() { Caption = "Subversion", Visible = true, FieldName = nameof(SupplierPriceList.IdSubVer), Width = 70 };
                GridColumn colTimestamp = new GridColumn() { Caption = "Timestamp", Visible = true, FieldName = nameof(SupplierPriceList.Timestamp), Width = 130 };
                GridColumn colIdItemBcn = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("ItemBCN"), Visible = true, FieldName = nameof(SupplierPriceList.IdItemBcn), Width = 200 };
                GridColumn colIdSupplier = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("IdSupplier"), Visible = true, FieldName = nameof(SupplierPriceList.IdSupplier), Width = 100 };
                GridColumn colPrice = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Price"), Visible = true, FieldName = nameof(SupplierPriceList.Price), Width = 80 };
                GridColumn colComments = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Comments"), Visible = true, FieldName = nameof(SupplierPriceList.Comments), Width = 300 };
                GridColumn colCurrency = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Currency"), Visible = true, FieldName = nameof(SupplierPriceList.IdCurrency), Width = 70 };
                GridColumn colPriceBaseCurrency = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("PriceBaseCurrency"), Visible = true, FieldName = nameof(SupplierPriceList.PriceBaseCurrency), Width = 120 };
                GridColumn colExchangeRateUsed = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("ExchangeRateUsed"), Visible = true, FieldName = nameof(SupplierPriceList.ExchangeRateUsed), Width = 120 };
                GridColumn colMinLot = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("MinLot"), Visible = true, FieldName = nameof(SupplierPriceList.MinLot), Width = 60 };
                GridColumn colIncrLot = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("IncrLot"), Visible = true, FieldName = nameof(SupplierPriceList.IncrLot), Width = 60 };
                GridColumn colLeadTime = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("LeadTime"), Visible = true, FieldName = nameof(SupplierPriceList.LeadTime), Width = 70 };

                //Format type 
                colTimestamp.DisplayFormat.FormatType = FormatType.DateTime;

                colPrice.DisplayFormat.FormatType = FormatType.Numeric;
                colPrice.DisplayFormat.FormatString = "F2";

                colPriceBaseCurrency.DisplayFormat.FormatType = FormatType.Numeric;
                colPriceBaseCurrency.DisplayFormat.FormatString = "F2";

                colExchangeRateUsed.DisplayFormat.FormatType = FormatType.Numeric;
                colExchangeRateUsed.DisplayFormat.FormatString = "F2";

                //Edit repositories
                RepositoryItemLookUpEdit riComboCurrency = new RepositoryItemLookUpEdit()
                {
                    DataSource = _currenciesList,
                    ValueMember = nameof(Currency.IdCurrency),
                    DisplayMember = nameof(Currency.Description)
                };
                riComboCurrency.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo(nameof(Currency.IdCurrency), 40, GlobalSetting.ResManager.GetString("Currency")));
                riComboCurrency.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo(nameof(Currency.Description), 60, GlobalSetting.ResManager.GetString("Description")));

                colCurrency.ColumnEdit = riComboCurrency;

                RepositoryItemTextEdit ritxt2Dec = new RepositoryItemTextEdit();
                ritxt2Dec.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                ritxt2Dec.Mask.EditMask = "F2";
                
                colPrice.ColumnEdit = ritxt2Dec;
                colPriceBaseCurrency.ColumnEdit = ritxt2Dec;
                colExchangeRateUsed.ColumnEdit = ritxt2Dec;
                colLeadTime.ColumnEdit = ritxt2Dec;

                RepositoryItemTextEdit ritxtInt = new RepositoryItemTextEdit();
                ritxtInt.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                ritxtInt.Mask.EditMask = "D";

                colMinLot.ColumnEdit = ritxtInt;
                colIncrLot.ColumnEdit = ritxtInt;

                //add columns to grid root view
                rootGridViewSuppliersPriceList.Columns.Add(colIdVer);
                rootGridViewSuppliersPriceList.Columns.Add(colIdSubVer);
                rootGridViewSuppliersPriceList.Columns.Add(colTimestamp);
                rootGridViewSuppliersPriceList.Columns.Add(colIdItemBcn);
                rootGridViewSuppliersPriceList.Columns.Add(colIdSupplier);
                rootGridViewSuppliersPriceList.Columns.Add(colPrice);
                rootGridViewSuppliersPriceList.Columns.Add(colComments);
                rootGridViewSuppliersPriceList.Columns.Add(colCurrency);
                rootGridViewSuppliersPriceList.Columns.Add(colPriceBaseCurrency);
                rootGridViewSuppliersPriceList.Columns.Add(colExchangeRateUsed);
                rootGridViewSuppliersPriceList.Columns.Add(colExchangeRateUsed);
                rootGridViewSuppliersPriceList.Columns.Add(colMinLot);
                rootGridViewSuppliersPriceList.Columns.Add(colIncrLot);
                rootGridViewSuppliersPriceList.Columns.Add(colLeadTime);

                //Events
                rootGridViewSuppliersPriceList.DoubleClick += rootGridViewSuppliersPriceList_DoubleClick;

                rootGridViewSuppliersPriceList.CellValueChanged += rootGridViewSuppliersPriceList_CellValueChanged;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetUpTextEdit()
        {
            try
            {
                txtPrice.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                txtPrice.Properties.Mask.EditMask = "F2"; //Dos decimales
                txtPrice.Properties.Mask.UseMaskAsDisplayFormat = true;
                txtPrice.EditValue = "0.00";

                txtPriceBaseCurrency.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                txtPriceBaseCurrency.Properties.Mask.EditMask = "F2";
                txtPriceBaseCurrency.Properties.Mask.UseMaskAsDisplayFormat = true;
                txtPriceBaseCurrency.EditValue = "0.00";

                txtExchangeRateUsed.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                txtExchangeRateUsed.Properties.Mask.EditMask = "F2";
                txtExchangeRateUsed.Properties.Mask.UseMaskAsDisplayFormat = true;
                txtExchangeRateUsed.EditValue = "0.00";

                txtLeadTime.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                txtLeadTime.Properties.Mask.EditMask = "F2";
                txtLeadTime.Properties.Mask.UseMaskAsDisplayFormat = true;
                txtLeadTime.EditValue = "0.00";

                txtMinLot.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                txtMinLot.Properties.Mask.EditMask = "D";
                txtMinLot.Properties.Mask.UseMaskAsDisplayFormat = true;
                txtMinLot.EditValue = "0";

                txtIncrLot.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                txtIncrLot.Properties.Mask.EditMask = "D";
                txtIncrLot.Properties.Mask.UseMaskAsDisplayFormat = true;
                txtIncrLot.EditValue = "0";

                //History
                txtHPrice.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                txtHPrice.Properties.Mask.EditMask = "F2"; //Dos decimales
                txtHPrice.Properties.Mask.UseMaskAsDisplayFormat = true;
                txtHPrice.EditValue = "0.00";

                txtHPriceBaseCurrency.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                txtHPriceBaseCurrency.Properties.Mask.EditMask = "F2";
                txtHPriceBaseCurrency.Properties.Mask.UseMaskAsDisplayFormat = true;
                txtHPriceBaseCurrency.EditValue = "0.00";

                txtHExchangeRateUsed.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                txtHExchangeRateUsed.Properties.Mask.EditMask = "F2";
                txtHExchangeRateUsed.Properties.Mask.UseMaskAsDisplayFormat = true;
                txtHExchangeRateUsed.EditValue = "0.00";

                txtHLeadTime.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                txtHLeadTime.Properties.Mask.EditMask = "F2";
                txtHLeadTime.Properties.Mask.UseMaskAsDisplayFormat = true;
                txtHLeadTime.EditValue = "0.00";

                txtHMinLot.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                txtHMinLot.Properties.Mask.EditMask = "D";
                txtHMinLot.Properties.Mask.UseMaskAsDisplayFormat = true;
                txtHMinLot.EditValue = "0";

                txtHIncrLot.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                txtHIncrLot.Properties.Mask.EditMask = "D";
                txtHIncrLot.Properties.Mask.UseMaskAsDisplayFormat = true;
                txtHIncrLot.EditValue = "0";

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetUpSearchLueSupplier()
        {
            try
            {
                var suppliers = GlobalSetting.SupplierService.GetSuppliers();
                slueSupplier.Properties.DataSource = suppliers;
                slueSupplier.Properties.ValueMember = nameof(Supplier.IdSupplier);
                slueSupplier.Properties.DisplayMember = nameof(Supplier.SupplierName);
                slueSupplier.Properties.View.Columns.AddField(nameof(Supplier.IdSupplier)).Visible = true;
                slueSupplier.Properties.View.Columns.AddField(nameof(Supplier.SupplierName)).Visible = true;
                slueSupplier.KeyDown += slueSupplier_KeyDown;
                slueSupplier.EditValueChanged += SlueSupplier_EditValueChanged;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SlueSupplier_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                LoadSuppliersPriceList();
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetUpSearchLueItemBcn()
        {
            try
            {
                var itemsBcn = GlobalSetting.ItemBcnService.GetItemsBcn();
                slueItemBcn.Properties.DataSource = itemsBcn;
                slueItemBcn.Properties.ValueMember = nameof(ItemBcn.IdItemBcn);
                slueItemBcn.Properties.DisplayMember = nameof(ItemBcn.Description);
                slueItemBcn.KeyDown += slueItemBcn_KeyDown;
                slueItemBcn.EditValueChanged += SlueItemBcn_EditValueChanged;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SlueItemBcn_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                LoadSuppliersPriceList();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        private void LoadCurrenciesList()
        {
            try
            {
                _currenciesList = GlobalSetting.CurrencyService.GetCurrencies();
                lueIdCurrency.Properties.DataSource = _currenciesList;
                lueIdCurrency.Properties.DisplayMember = nameof(Currency.Description);
                lueIdCurrency.Properties.ValueMember = nameof(Currency.IdCurrency);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadItemBcnList()
        {
            try
            {
                _itemBcnList = GlobalSetting.ItemBcnService.GetItemsBcn();

                lueIdItemBcn.Properties.DataSource = _itemBcnList;
                lueIdItemBcn.Properties.DisplayMember = nameof(ItemBcn.Description);
                lueIdItemBcn.Properties.ValueMember = nameof(ItemBcn.IdItemBcn);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadSuppliersList()
        {
            try
            {
                _modifiedRowsList.Clear();

                _suppliersList = GlobalSetting.SupplierService.GetSuppliers();

                lueIdSupplier.Properties.DataSource = _suppliersList;
                lueIdSupplier.Properties.DisplayMember = nameof(Supplier.SupplierName);
                lueIdSupplier.Properties.ValueMember = nameof(Supplier.IdSupplier);

                lueIdSupplier.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo(nameof(Supplier.IdSupplier), 20, GlobalSetting.ResManager.GetString("IdSupplier")));
                lueIdSupplier.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo(nameof(Supplier.SupplierName), 100, GlobalSetting.ResManager.GetString("Name")));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadSuppliersPriceList()
        {
            try
            {
                _suppliersPriceListList = GlobalSetting.SupplierPriceListService.GetSuppliersPriceList((string)slueItemBcn.EditValue, (string)slueSupplier.EditValue);
                xgrdSuppliersPriceList.DataSource = _suppliersPriceListList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadSupplierPriceListForm(SupplierPriceList supplierPriceList)
        {
            try
            {
                _supplierPriceListUpdate = supplierPriceList.Clone();
                _supplierPriceListOriginal = _supplierPriceListUpdate.Clone();
                SetFormBinding();  //refresh binding 
                xtpForm.PageVisible = true;
                xtcGeneral.SelectedTabPage = xtpForm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadSupplierPriceListHistory(string idItembcn, string idSupplier)
        {
            try
            {
                _supplierPriceListHistoryList = GlobalSetting.SupplierPriceListService.GetSupplierPriceListHistory(idItembcn, idSupplier);
                SetCurrentSupplierPriceListHistory(0);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void SetCurrentSupplierPriceListHistory(int historyNum)
        {
            try
            {
                if (historyNum >= 0 && historyNum < _supplierPriceListHistoryList.Count())
                {
                    _supplierPriceListHistory = _supplierPriceListHistoryList[historyNum];
                    SetHistoryBinding();
                    _currentHistoryNumList = historyNum;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Crear los bindings de los campos del formulario
        /// </summary>
        private void SetFormBinding()
        {
            try
            {
                foreach (Control ctl in layoutControlForm.Controls)
                {
                    if (ctl.GetType() == typeof(TextEdit))
                    {
                        ctl.DataBindings.Clear();
                        ((TextEdit)ctl).ReadOnly = true;
                    }
                    else if (ctl.GetType() == typeof(CheckEdit))
                    {
                        ctl.DataBindings.Clear();
                        ((CheckEdit)ctl).ReadOnly = true;
                    }
                    else if (ctl.GetType() == typeof(DateEdit))
                    {
                        ctl.DataBindings.Clear();
                        ((DateEdit)ctl).ReadOnly = true;
                    }
                    else if (ctl.GetType() == typeof(LookUpEdit))
                    {
                        ctl.DataBindings.Clear();
                        ((LookUpEdit)ctl).ReadOnly = true;
                    }
                }

                //Textedit
                txtIdVersion.DataBindings.Add<SupplierPriceList>(_supplierPriceListUpdate, (Control c) => c.Text, supplierPriceList => supplierPriceList.IdVer);
                txtIdSubversion.DataBindings.Add<SupplierPriceList>(_supplierPriceListUpdate, (Control c) => c.Text, supplierPriceList => supplierPriceList.IdSubVer);
                txtTimestamp.DataBindings.Add<SupplierPriceList>(_supplierPriceListUpdate, (Control c) => c.Text, supplierPriceList => supplierPriceList.Timestamp);
                txtPrice.DataBindings.Add<SupplierPriceList>(_supplierPriceListUpdate, (Control c) => c.Text, supplierPriceList => supplierPriceList.Price);
                txtComments.DataBindings.Add<SupplierPriceList>(_supplierPriceListUpdate, (Control c) => c.Text, supplierPriceList => supplierPriceList.Comments);
                txtPriceBaseCurrency.DataBindings.Add<SupplierPriceList>(_supplierPriceListUpdate, (Control c) => c.Text, supplierPriceList => supplierPriceList.PriceBaseCurrency);
                txtExchangeRateUsed.DataBindings.Add<SupplierPriceList>(_supplierPriceListUpdate, (Control c) => c.Text, supplierPriceList => supplierPriceList.ExchangeRateUsed);
                txtMinLot.DataBindings.Add<SupplierPriceList>(_supplierPriceListUpdate, (Control c) => c.Text, supplierPriceList => supplierPriceList.MinLot);
                txtIncrLot.DataBindings.Add<SupplierPriceList>(_supplierPriceListUpdate, (Control c) => c.Text, supplierPriceList => supplierPriceList.IncrLot);
                txtLeadTime.DataBindings.Add<SupplierPriceList>(_supplierPriceListUpdate, (Control c) => c.Text, supplierPriceList => supplierPriceList.LeadTime);

                //LookUpEdit
                lueIdItemBcn.DataBindings.Add<SupplierPriceList>(_supplierPriceListUpdate, (LookUpEdit e) => e.EditValue, supplierPriceList => supplierPriceList.IdItemBcn);
                lueIdSupplier.DataBindings.Add<SupplierPriceList>(_supplierPriceListUpdate, (LookUpEdit e) => e.EditValue, supplierPriceList => supplierPriceList.IdSupplier);
                lueIdCurrency.DataBindings.Add<SupplierPriceList>(_supplierPriceListUpdate, (LookUpEdit e) => e.EditValue, supplierPriceList => supplierPriceList.IdCurrency);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void SetHistoryBinding()
        {
            try
            {
                foreach (Control ctl in layoutControlSupplierHistory.Controls)
                {
                    if (ctl.GetType() == typeof(TextEdit))
                    {
                        ctl.DataBindings.Clear();
                        ((TextEdit)ctl).ReadOnly = true;
                    }
                    else if (ctl.GetType() == typeof(CheckEdit))
                    {
                        ctl.DataBindings.Clear();
                        ((CheckEdit)ctl).ReadOnly = true;
                    }
                    else if (ctl.GetType() == typeof(DateEdit))
                    {
                        ctl.DataBindings.Clear();
                        ((DateEdit)ctl).ReadOnly = true;
                    }
                    else if (ctl.GetType() == typeof(LookUpEdit))
                    {
                        ctl.DataBindings.Clear();
                        ((LookUpEdit)ctl).ReadOnly = true;
                    }
                }

                //Textedit
                txtHIdVersion.DataBindings.Add<SupplierPriceListHistory>(_supplierPriceListHistory, (Control c) => c.Text, supplierPriceList => supplierPriceList.IdVer);
                txtHIdSubversion.DataBindings.Add<SupplierPriceListHistory>(_supplierPriceListHistory, (Control c) => c.Text, supplierPriceList => supplierPriceList.IdSubVer);
                txtHTimestamp.DataBindings.Add<SupplierPriceListHistory>(_supplierPriceListHistory, (Control c) => c.Text, supplierPriceList => supplierPriceList.Timestamp);
                txtHPrice.DataBindings.Add<SupplierPriceListHistory>(_supplierPriceListHistory, (Control c) => c.Text, supplierPriceList => supplierPriceList.Price);
                txtHComments.DataBindings.Add<SupplierPriceListHistory>(_supplierPriceListHistory, (Control c) => c.Text, supplierPriceList => supplierPriceList.Comments);
                txtHPriceBaseCurrency.DataBindings.Add<SupplierPriceListHistory>(_supplierPriceListHistory, (Control c) => c.Text, supplierPriceList => supplierPriceList.PriceBaseCurrency);
                txtHExchangeRateUsed.DataBindings.Add<SupplierPriceListHistory>(_supplierPriceListHistory, (Control c) => c.Text, supplierPriceList => supplierPriceList.ExchangeRateUsed);
                txtHMinLot.DataBindings.Add<SupplierPriceListHistory>(_supplierPriceListHistory, (Control c) => c.Text, supplierPriceList => supplierPriceList.MinLot);
                txtHIncrLot.DataBindings.Add<SupplierPriceListHistory>(_supplierPriceListHistory, (Control c) => c.Text, supplierPriceList => supplierPriceList.IncrLot);
                txtHLeadTime.DataBindings.Add<SupplierPriceListHistory>(_supplierPriceListHistory, (Control c) => c.Text, supplierPriceList => supplierPriceList.LeadTime);
                txtUser.DataBindings.Add<SupplierPriceListHistory>(_supplierPriceListHistory, (Control c) => c.Text, supplierPriceList => supplierPriceList.User);

                txtHIdItemBcn.DataBindings.Add<SupplierPriceListHistory>(_supplierPriceListHistory, (Control c) => c.Text, supplierPriceList => supplierPriceList.IdItemBcn);
                txtHIdSupplier.DataBindings.Add<SupplierPriceListHistory>(_supplierPriceListHistory, (Control c) => c.Text, supplierPriceList => supplierPriceList.IdSupplier);
                txtHIdCurrency.DataBindings.Add<SupplierPriceListHistory>(_supplierPriceListHistory, (Control c) => c.Text, supplierPriceList => supplierPriceList.IdCurrency);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        private void ConfigureRibbonActionsEditing()
        {
            try
            {
                //En función de si está en seleccionada la tab del formulario o del grid activaremos la edición de esa tab
                if (xtcGeneral.SelectedTabPage == xtpForm)
                {
                    xtpList.PageVisible = false;
                    sbNewVersion.Visible = true;
                    SetEditingFieldsEnabled();   
                }
                else if (xtcGeneral.SelectedTabPage == xtpList)
                {
                    xtpForm.PageVisible = false;

                    rootGridViewSuppliersPriceList.OptionsBehavior.Editable = true;
                    
                    //no edit column
                    rootGridViewSuppliersPriceList.Columns[nameof(SupplierPriceList.IdVer)].OptionsColumn.AllowEdit = false;
                    rootGridViewSuppliersPriceList.Columns[nameof(SupplierPriceList.IdSubVer)].OptionsColumn.AllowEdit = false;
                    rootGridViewSuppliersPriceList.Columns[nameof(SupplierPriceList.Timestamp)].OptionsColumn.AllowEdit = false;
                    rootGridViewSuppliersPriceList.Columns[nameof(SupplierPriceList.IdItemBcn)].OptionsColumn.AllowEdit = false;
                    rootGridViewSuppliersPriceList.Columns[nameof(SupplierPriceList.IdSupplier)].OptionsColumn.AllowEdit = false;

                    //Allow edit some columns
                    rootGridViewSuppliersPriceList.Columns[nameof(SupplierPriceList.Price)].OptionsColumn.AllowEdit = true;
                    rootGridViewSuppliersPriceList.Columns[nameof(SupplierPriceList.Comments)].OptionsColumn.AllowEdit = true;
                    rootGridViewSuppliersPriceList.Columns[nameof(SupplierPriceList.IdCurrency)].OptionsColumn.AllowEdit = true;
                    rootGridViewSuppliersPriceList.Columns[nameof(SupplierPriceList.PriceBaseCurrency)].OptionsColumn.AllowEdit = true;
                    rootGridViewSuppliersPriceList.Columns[nameof(SupplierPriceList.ExchangeRateUsed)].OptionsColumn.AllowEdit = true;
                    rootGridViewSuppliersPriceList.Columns[nameof(SupplierPriceList.MinLot)].OptionsColumn.AllowEdit = true;
                    rootGridViewSuppliersPriceList.Columns[nameof(SupplierPriceList.IncrLot)].OptionsColumn.AllowEdit = true;
                    rootGridViewSuppliersPriceList.Columns[nameof(SupplierPriceList.LeadTime)].OptionsColumn.AllowEdit = true;

                    //desuscribirse al evento del dobleclick mientras editamos el grid
                    rootGridViewSuppliersPriceList.DoubleClick -= rootGridViewSuppliersPriceList_DoubleClick;

                    sbLoad.Enabled = false;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ConfigureRibbonActionsCreating()
        {
            try
            {
                xtpList.PageVisible = false;
                xtpForm.PageVisible = true;
                sbNewVersion.Visible = false;
                groupControlHistory.Visible = false;
                ResetSupplierPriceListUpdate();
                SetFormBinding(); //refresh binding
                SetNonCreatingFieldsVisibility(LayoutVisibility.Never);
                SetCreatingFieldsEnabled();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Poner como editables los campos para el modo de edición
        /// </summary>
        private void SetEditingFieldsEnabled()
        {
            try
            {
                foreach (Control ctl in layoutControlForm.Controls)
                {
                    if (Array.IndexOf(_nonEditingFields, ctl.Name) < 0)
                    {
                        if (ctl.GetType() == typeof(TextEdit))
                        {
                            ((TextEdit)ctl).ReadOnly = false;
                        }
                        else if (ctl.GetType() == typeof(CheckEdit))
                        {
                            ((CheckEdit)ctl).ReadOnly = false;
                        }
                        else if (ctl.GetType() == typeof(LookUpEdit))
                        {
                            ((LookUpEdit)ctl).ReadOnly = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetCreatingFieldsEnabled()
        {
            try
            {
                foreach (Control ctl in layoutControlForm.Controls)
                {
                    if (ctl.GetType() == typeof(TextEdit))
                    {
                        ((TextEdit)ctl).ReadOnly = false;
                    }
                    else if (ctl.GetType() == typeof(CheckEdit))
                    {
                        ((CheckEdit)ctl).ReadOnly = false;
                    }
                    else if (ctl.GetType() == typeof(LookUpEdit))
                    {
                        ((LookUpEdit)ctl).ReadOnly = false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetNonCreatingFieldsVisibility(LayoutVisibility visibility)
        {
            try
            {
                lciIdVersion.Visibility = visibility;
                lciIdSubversion.Visibility = visibility;
                lciTimestamp.Visibility = visibility;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AddModifiedRowToList(SupplierPriceList modifiedRow)
        {
            try
            {
                var supplierPriceList = _modifiedRowsList.FirstOrDefault(a => a.IdSupplier.Equals(modifiedRow.IdSupplier) && a.IdItemBcn.Equals(modifiedRow.IdItemBcn));
                if (supplierPriceList == null)
                {
                    _modifiedRowsList.Add(modifiedRow);
                }
                else
                {
                    supplierPriceList.Price = modifiedRow.Price;
	                supplierPriceList.Comments = modifiedRow.Comments;
	                supplierPriceList.IdCurrency = modifiedRow.IdCurrency;
	                supplierPriceList.PriceBaseCurrency = modifiedRow.PriceBaseCurrency;
	                supplierPriceList.ExchangeRateUsed = modifiedRow.ExchangeRateUsed;
	                supplierPriceList.MinLot = modifiedRow.MinLot;
	                supplierPriceList.IncrLot = modifiedRow.IncrLot;
                    supplierPriceList.LeadTime = modifiedRow.LeadTime;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Mover la fila activa a la de un supplier en concreto
        /// </summary>
        /// <param name="idSupplier"></param>
        private void MoveGridToSupplier(string idItemBcn, string idSupplier)
        {
            try
            {
                int row = GridControl.InvalidRowHandle;
                for (int i = 0; i < rootGridViewSuppliersPriceList.RowCount; i++)
                {
                    if (
                        rootGridViewSuppliersPriceList.GetRowCellValue(i, nameof(SupplierPriceList.IdItemBcn)).Equals(idItemBcn) &&
                        rootGridViewSuppliersPriceList.GetRowCellValue(i, nameof(SupplierPriceList.IdSupplier)).Equals(idSupplier) 
                        )
                    {
                        row = i;
                    }
                }
                if (row != GridControl.InvalidRowHandle)
                {
                    rootGridViewSuppliersPriceList.FocusedRowHandle = row;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsValidSupplierPriceList()
        {
            try
            {
                if (xtcGeneral.SelectedTabPage == xtpList)
                    return IsValidSupplierPriceListGrid();
                else if (xtcGeneral.SelectedTabPage == xtpForm)
                    return IsValidSupplierPriceListForm();
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsValidSupplierPriceListForm()
        {
            try
            {
                foreach (Control ctl in layoutControlForm.Controls)
                {
                    if (Array.IndexOf(_mandatoryEditingFields, ctl.Name) < 0)
                    {
                        if (ctl.GetType() == typeof(TextEdit))
                        {
                            if (string.IsNullOrEmpty(((TextEdit)ctl).Text))
                            {
                                MessageBox.Show(string.Format(GlobalSetting.ResManager.GetString("NullArgument"), ctl.Name));
                                return false;
                            }
                        }
                        else if (ctl.GetType() == typeof(LookUpEdit))
                        {
                            if (string.IsNullOrEmpty(((LookUpEdit)ctl).Text))
                            {
                                MessageBox.Show(string.Format(GlobalSetting.ResManager.GetString("NullArgument"), ctl.Name));
                                return false;
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsValidSupplierPriceListGrid()
        {
            try 
            {
                foreach (var spl in _modifiedRowsList)
                {
                    if (spl.Price <= 0)
                    {
                        XtraMessageBox.Show("Price Field Required", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    if(string.IsNullOrEmpty(spl.IdCurrency))
                    {
                        XtraMessageBox.Show("Currency Field Required", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    if (spl.PriceBaseCurrency <= 0)
                    {
                        XtraMessageBox.Show("Price Base Currency Field Required", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    if (spl.ExchangeRateUsed <= 0)
                    {
                        XtraMessageBox.Show("ExchangeRateUsed Field Required", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Create/Update

        /// <summary>
        /// Actualizar los datos de un supplier
        /// </summary>
        /// <param name="newVersion">Si es una versión nueva o una actualización de la existente</param>
        /// <returns></returns>
        private bool UpdateSupplierPriceList(bool newVersion = false)
        {
            try
            {
                return GlobalSetting.SupplierPriceListService.UpdateSupplierPriceList(_supplierPriceListUpdate, newVersion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Actualizar los datos de varios supplier si se ha editado desde el grid
        /// </summary>
        /// <param name="newVersion"></param>
        /// <returns></returns>
        private bool UpdateSuppliersPricesList()
        {
            try
            {
                return GlobalSetting.SupplierPriceListService.UpdateSuppliersPricesList(_modifiedRowsList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Crear un nuevo Supplier
        /// </summary>
        /// <returns></returns>
        private bool CreateSupplierPriceList()
        {
            try
            {
                _supplierPriceListOriginal = _supplierPriceListUpdate.Clone();
                return GlobalSetting.SupplierPriceListService.NewSupplierPriceList(_supplierPriceListUpdate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ActionsAfterCU()
        {
            try
            {

                string idItemBcn = (_supplierPriceListOriginal == null ? string.Empty : _supplierPriceListOriginal.IdItemBcn);
                string idSupplier = (_supplierPriceListOriginal == null ? string.Empty : _supplierPriceListOriginal.IdSupplier);

                _supplierPriceListOriginal = null;
                ResetSupplierPriceListUpdate();
                SetUpGrdSuppliersPriceList();
                SetFormBinding();
                xtpForm.PageVisible = false;
                xtpList.PageVisible = true;
                groupControlHistory.Visible = true;
                LoadSuppliersPriceList();
                MoveGridToSupplier(idItemBcn, idSupplier);
                RestoreInitState();
                sbLoad.Enabled = true;

                rootGridViewSuppliersPriceList.OptionsBehavior.Editable = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #endregion

        #region Public Methods
        public void InitData(string idSupplier, string idItemBcn)
        {
            try
            {
                slueItemBcn.EditValue = idItemBcn;
                slueSupplier.EditValue = idSupplier;
                Cursor = Cursors.WaitCursor;
                LoadSuppliersPriceList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        #endregion
    }
}
