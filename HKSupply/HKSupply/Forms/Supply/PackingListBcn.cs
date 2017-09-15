using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using HKSupply.General;
using HKSupply.Helpers;
using HKSupply.Models;
using HKSupply.Models.Supply;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace HKSupply.Forms.Supply
{
    public partial class PackingListBcn : RibbonFormBase
    {
        #region Constants
        private const string COL_SELECTED = "Selected";
        #endregion

        #region Enums
        enum eGridLinesSoSelectionSummaries
        {
            totalQuantityOrderedMt,
            totalQuantityOrderedHw,
            totalQuantityMt,
            totalQuantityHw
        }

        enum eGridLinesDeliveredGoodsSummaries
        {
            totalQuantityOrderedMt,
            totalQuantityOrderedHw,
            totalQuantityMt,
            totalQuantityHw
        }
        #endregion

        #region Private Members

        Font _labelDefaultFontBold = new Font("SourceSansProRegular", 8, FontStyle.Bold);
        Font _labelDefaultFont = new Font("SourceSansProRegular", 8, FontStyle.Regular);

        Customer _customersEtniaBcn;

        List<SupplyStatus> _supplyStatusList;
        
        List<Currency> _currenciesList;
        List<DeliveryTerm> _deliveryTermList;
        List<PaymentTerms> _paymentTermsList;

        DocHead _docHeadPK;
        BindingList<DocHead> _docPoSelectionList;
        BindingList<DocLine> _docLinesPoSelectionList;
        BindingList<DocLine> _docLinesDeliveredGoodsList;

        int _totalQuantityOrderedMtDeliveredGoods;
        int _totalQuantityOrderedHwDeliveredGoods;
        int _totalQuantityMtDeliveredGoods;
        int _totalQuantityHwDeliveredGoods;

        #endregion

        #region #Constructor
        public PackingListBcn()
        {
            InitializeComponent();

            try
            {
                Cursor = Cursors.WaitCursor;

                ConfigureRibbonActions();
                LoadAuxList();
                SetUpLabels();
                SetObjectsReadOnly();
                SetUpButtons();
                SetUpSearchLookUpEdit();
                SetUpEvents();
                SetUpGrdPoSelection();
                SetUpGrdLinesPoSelection();
                SetUpGrdLinesDeliveredGoods();
                SetVisiblePropertyByState();
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
                //Layout
                EnableLayoutOptions = true;
                ConfigureLayoutOptions();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void bbiCancel_ItemClick(object sender, ItemClickEventArgs e)
        {
            base.bbiCancel_ItemClick(sender, e);

            try
            {
                CancelEdit();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public override void bbiEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            base.bbiEdit_ItemClick(sender, e);

            try
            {
                if (_docHeadPK == null)
                {
                    MessageBox.Show(GlobalSetting.ResManager.GetString("NoDataSelected"));
                    RestoreInitState();
                }
                else if (_docHeadPK.IdSupplyStatus != Constants.SUPPLY_STATUS_OPEN)
                {
                    MessageBox.Show("Only OPEN Packing List can be edited.");
                    RestoreInitState();
                }
                else
                {
                    ConfigureActionsStackViewEditing();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public override void bbiNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            base.bbiNew_ItemClick(sender, e);

            try
            {
                //buscamos las PO abiertas para Etnia Barcelona
                SearchCustomersPOs();

                if (gridViewPoSelection.DataRowCount == 0)
                {
                    //Si no ha cargado datos SO restauramos el estado inicial
                    RestoreInitState();
                }
                else if (ValidateIfExistOpenPK() == false) //validamos 
                {
                    RestoreInitState();
                }
                else
                {
                    ConfigureActionsStackViewCreating();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public override void bbiSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            base.bbiSave_ItemClick(sender, e);

            try
            {
                bool res = false;

                if (ValidatePK() == false)
                    return;

                DialogResult result = MessageBox.Show(GlobalSetting.ResManager.GetString("SaveChanges"), "", MessageBoxButtons.YesNo);

                if (result != DialogResult.Yes)
                    return;

                Cursor = Cursors.WaitCursor;

                if (CurrentState == ActionsStates.New)
                {
                    res = CreatePK();
                }
                else if (CurrentState == ActionsStates.Edit)
                {
                    res = UpdatePK();
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

        public override void bbiExportCsv_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewLinesDeliveredGoods.DataRowCount == 0)
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
                    gridViewLinesDeliveredGoods.OptionsPrint.PrintFooter = false;
                    gridViewLinesDeliveredGoods.ExportToCsv(ExportCsvFile);

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

        public override void bbiExportExcel_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewLinesDeliveredGoods.DataRowCount == 0)
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
                    gridViewLinesDeliveredGoods.OptionsPrint.PrintFooter = false;
                    gridViewLinesDeliveredGoods.ExportToXlsx(ExportExcelFile);

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

        #endregion

        #region Form Events

        private void SbSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPKNumber.Text) == false)
                {
                    SearchPK(); 
                }
                else
                {
                    SearchCustomersPOs(); 
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtPKNumber_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter && string.IsNullOrEmpty(txtPKNumber.Text) == false)
                {
                    SearchPK();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SbFinishPK_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    bool res = false;

                    if (ValidatePK() == false)
                        return;

                    DialogResult result = MessageBox.Show(GlobalSetting.ResManager.GetString("SaveChanges"), "", MessageBoxButtons.YesNo);

                    if (result != DialogResult.Yes)
                        return;

                    Cursor = Cursors.WaitCursor;

                    if (CurrentState == ActionsStates.Edit)
                    {
                        res = UpdatePK(finishPK: true); 
                    }

                    if (res == true)
                    {
                        MessageBox.Show(GlobalSetting.ResManager.GetString("SaveSuccessfully"));
                        ShowMessageDocsGenerated();
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
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GridViewSoSelection_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;

                //Sólo puede haber uno marcado
                if (e.Action == CollectionChangeAction.Add)
                {
                    view.BeginSelection();
                    view.ClearSelection();
                    view.SelectRow(view.FocusedRowHandle);
                    view.EndSelection();

                    DocHead doc = view.GetRow(view.FocusedRowHandle) as DocHead;
                    if (doc != null)
                    {
                        _docLinesPoSelectionList = new BindingList<DocLine>(doc.Lines);
                        //cambiamos la cantidad Dummy (aquí la cantida da incluir en el packing) por la cantidad pendiente para facilitar al usuario
                        //The ToList is needed in order to evaluate the select immediately
                        _docLinesPoSelectionList.Select(a => { a.DummyQuantity = a.Quantity - a.DeliveredQuantity; return a; }).ToList();

                        xgrdLinesPoSelection.DataSource = null;
                        xgrdLinesPoSelection.DataSource = _docLinesPoSelectionList;
                        MarkSoSelectionFromDeliveredGoods();
                    }
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GridViewLinesSoSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                DocLine line = view.GetRow(view.FocusedRowHandle) as DocLine;

                if (CurrentState == ActionsStates.New || CurrentState == ActionsStates.Edit)
                {
                    if (e.Action == CollectionChangeAction.Add)
                    {
                        if (line.IdSupplyStatus == Constants.SUPPLY_STATUS_OPEN)
                        {
                            CopyToDeliveredGood(line);
                            gridViewLinesPoSelection.UpdateSummary();
                        }
                        else
                        {
                            XtraMessageBox.Show("This line is Close.");
                            view.BeginSelection();
                            gridViewLinesPoSelection.UnselectRow(view.FocusedRowHandle);
                            view.EndSelection();
                        }

                    }
                    else if (e.Action == CollectionChangeAction.Remove)
                    {
                        line.Quantity = line.QuantityOriginal;
                        gridViewLinesPoSelection.RefreshData();
                        DeleteDeliveredGood(line);
                        gridViewLinesPoSelection.UpdateSummary();
                    }
                }
                else
                {
                    //Si no está editando/creando sólo se marcan las que están en el packing
                    var existInPk = _docLinesDeliveredGoodsList?.Where(a => a.IdItemBcn.Equals(line.IdItemBcn) && a.IdDocRelated.Equals(line.IdDoc)).FirstOrDefault();
                    if (existInPk == null)
                    {
                        view.BeginSelection();
                        gridViewLinesPoSelection.UnselectRow(view.FocusedRowHandle);
                        view.EndSelection();
                    }
                    else
                    {
                        //Tampoco se puede deseleccionar
                        if (e.Action == CollectionChangeAction.Remove)
                        {
                            view.BeginSelection();
                            gridViewLinesPoSelection.SelectRow(view.FocusedRowHandle);
                            view.EndSelection();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GridViewLinesSoSelection_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                DocLine line = view.GetRow(view.FocusedRowHandle) as DocLine;
                if (line != null)
                    CopyToDeliveredGood(line);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GridViewLinesSoSelection_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;

                DocLine line = view.GetRow(view.FocusedRowHandle) as DocLine;

                switch (view.FocusedColumn.FieldName)
                {
                    case nameof(DocLine.Quantity):
                        int qty = Convert.ToInt32(e.Value);
                        if (qty > (line.QuantityOriginal - line.DeliveredQuantity))
                        {
                            e.Valid = false;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GridViewLinesSoSelection_ShowingEditor(object sender, CancelEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                DocLine line = view.GetRow(view.FocusedRowHandle) as DocLine;

                if (gridViewLinesPoSelection.IsRowSelected(view.FocusedRowHandle) == false)
                    e.Cancel = true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GridViewLinesSoSelection_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                if (e.Column.FieldName == "PENDING_QTY" && e.IsGetData)
                    e.Value = (
                        (int)view.GetRowCellValue(e.ListSourceRowIndex, nameof(DocLine.Quantity)) -
                        (int)view.GetRowCellValue(e.ListSourceRowIndex, nameof(DocLine.DeliveredQuantity))
                        );
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GridViewLinesDeliveredGoods_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            try
            {
                // Get the summary ID. 
                eGridLinesDeliveredGoodsSummaries summaryID = (eGridLinesDeliveredGoodsSummaries)(e.Item as GridSummaryItem).Tag;
                GridView view = sender as GridView;

                // Initialization 
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    _totalQuantityOrderedHwDeliveredGoods = 0;
                    _totalQuantityOrderedMtDeliveredGoods = 0;
                    _totalQuantityHwDeliveredGoods = 0;
                    _totalQuantityMtDeliveredGoods = 0;
                }

                // Calculation 
                if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DocLine row = view.GetRow(e.RowHandle) as DocLine;

                    switch (summaryID)
                    {
                        case eGridLinesDeliveredGoodsSummaries.totalQuantityOrderedMt:

                            if (row.IdItemGroup == Constants.ITEM_GROUP_MT)
                                _totalQuantityOrderedMtDeliveredGoods += Convert.ToInt32(e.FieldValue);
                            break;

                        case eGridLinesDeliveredGoodsSummaries.totalQuantityOrderedHw:

                            if (row.IdItemGroup == Constants.ITEM_GROUP_HW)
                                _totalQuantityOrderedHwDeliveredGoods += Convert.ToInt32(e.FieldValue);
                            break;

                        case eGridLinesDeliveredGoodsSummaries.totalQuantityMt:

                            if (row.IdItemGroup == Constants.ITEM_GROUP_MT)
                                _totalQuantityMtDeliveredGoods += Convert.ToInt32(e.FieldValue);
                            break;

                        case eGridLinesDeliveredGoodsSummaries.totalQuantityHw:

                            if (row.IdItemGroup == Constants.ITEM_GROUP_HW)
                                _totalQuantityHwDeliveredGoods += Convert.ToInt32(e.FieldValue);
                            break;
                    }
                }

                // Finalization
                if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    switch (summaryID)
                    {
                        case eGridLinesDeliveredGoodsSummaries.totalQuantityOrderedMt:
                            e.TotalValue = _totalQuantityOrderedMtDeliveredGoods;
                            break;
                        case eGridLinesDeliveredGoodsSummaries.totalQuantityOrderedHw:
                            e.TotalValue = _totalQuantityOrderedHwDeliveredGoods;
                            break;
                        case eGridLinesDeliveredGoodsSummaries.totalQuantityMt:
                            e.TotalValue = _totalQuantityMtDeliveredGoods;
                            break;
                        case eGridLinesDeliveredGoodsSummaries.totalQuantityHw:
                            e.TotalValue = _totalQuantityHwDeliveredGoods;
                            break;
                    }
                }


            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DateEditPKCreation_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                lblPKDocDateWeek.Text = dateEditPKDocDate.DateTime.GetWeek().ToString();

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DateEditPKDelivery_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                lblPKDeliveryWeek.Text = dateEditPKDelivery.DateTime.GetWeek().ToString();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Public Methods
        #endregion

        #region Private Methods

        #region Load/Reset

        private void LoadAuxList()
        {
            try
            {
                _customersEtniaBcn = GlobalSetting.CustomerService.GetCustomerById(Constants.ETNIA_BCN_COMPANY_CODE);
                _currenciesList = GlobalSetting.CurrencyService.GetCurrencies();
                _deliveryTermList = GlobalSetting.DeliveryTermsService.GetDeliveryTerms();
                _paymentTermsList = GlobalSetting.PaymentTermsService.GetPaymentTerms();
                _supplyStatusList = GlobalSetting.SupplyDocsService.GetSupplyStatus();
            }
            catch
            {
                throw;
            }
        }

        private void ResetCustomerPOs()
        {
            try
            {
                _docPoSelectionList = null;
                _docLinesPoSelectionList = null;
                _docLinesDeliveredGoodsList = null;
            }
            catch
            {
                throw;
            }
        }

        private void ResetPK()
        {
            try
            {
                ResetCustomerPOs();
                _docHeadPK = null;
                xgrdPoSelection.DataSource = null;
                xgrdLinesPoSelection.DataSource = null;
                xgrdLinesDeliveredGoods.DataSource = null;

            }
            catch
            {
                throw;
            }
        }

        private void Reset4NewPk()
        {
            try
            {
                _docHeadPK = null;
                _docLinesPoSelectionList = null;
                _docLinesDeliveredGoodsList = null;

                xgrdLinesPoSelection.DataSource = null;
                xgrdLinesDeliveredGoods.DataSource = null;

                dateEditPKDelivery.EditValue = null;
                lblPKDeliveryWeek.Text = string.Empty;

                slueDeliveryTerms.EditValue = null;
                slueCurrency.EditValue = null;
                sluePaymentTerm.EditValue = null;

                gridViewPoSelection.BeginSelection();
                gridViewPoSelection.ClearSelection();
                gridViewPoSelection.EndSelection();

            }
            catch
            {
                throw;
            }
        }

        private void SearchPK()
        {
            try
            {
                ResetPK();
                ResetForm();

                string pkNumber = txtPKNumber.Text;

                _docHeadPK = GlobalSetting.SupplyDocsService.GetDoc(pkNumber);

                if (_docHeadPK == null)
                {
                    XtraMessageBox.Show("No Data Found", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (_docHeadPK.IdSupplyDocType != Constants.SUPPLY_DOCTYPE_PL)
                {
                    XtraMessageBox.Show("Document is not a Packing List", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (_docHeadPK.IdCustomer != Constants.ETNIA_BCN_COMPANY_CODE)
                {
                    XtraMessageBox.Show("Packing List is not to Etnia Barcelona", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    LoadPK();
                }
            }
            catch
            {
                throw;
            }
        }

        private void LoadPK()
        {
            try
            {
                //***** Header *****/
                lbltxtStatus.Visible = true;
                lbltxtStatus.Text = _docHeadPK.IdSupplyStatus;
                slueDeliveryTerms.EditValue = _docHeadPK.IdDeliveryTerm;
                slueCurrency.EditValue = _docHeadPK.IdCurrency;

                dateEditPKDocDate.EditValue = _docHeadPK.DocDate;
                dateEditPKDelivery.EditValue = _docHeadPK.DeliveryDate;

                lblPKDocDateWeek.Text = dateEditPKDocDate.DateTime.GetWeek().ToString();
                lblPKDeliveryWeek.Text = dateEditPKDelivery.DateTime.GetWeek().ToString();

                //***** Grid SO Selection *****/
                ResetCustomerPOs();
                var packingSOs = GlobalSetting.SupplyDocsService.GetSalesOrderFromPackingList(_docHeadPK.IdDoc);
                _docPoSelectionList = new BindingList<DocHead>(packingSOs);
                xgrdPoSelection.DataSource = null;
                xgrdPoSelection.DataSource = _docPoSelectionList;

                //***** Grid Delivered Goods*****/
                _docLinesDeliveredGoodsList = new BindingList<DocLine>(_docHeadPK.Lines);

                xgrdLinesDeliveredGoods.DataSource = null;
                xgrdLinesDeliveredGoods.DataSource = _docLinesDeliveredGoodsList;

                //***** Terms Tab *****/
                lblTxtCompany.Text = _customersEtniaBcn.CustomerName;
                lblTxtAddress.Text = _customersEtniaBcn.ShippingAddress;
                lblTxtContact.Text = $"{_customersEtniaBcn.ContactName} ({_customersEtniaBcn.ContactPhone})";
                lblTxtShipTo.Text = "??"; //TODO
                lblTxtInvoiceTo.Text = "??"; //TODO
                sluePaymentTerm.EditValue = _docHeadPK.IdPaymentTerms;

            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Setup Form Objects

        private void SetUpLabels()
        {
            try
            {
                /********* Fonts **********/
                //Header 
                lblPKNumber.Font = _labelDefaultFontBold;
                lbltxtStatus.Font = _labelDefaultFontBold;
                lblDate.Font = _labelDefaultFontBold;
                lblWeek.Font = _labelDefaultFontBold;
                lblPKDocDate.Font = _labelDefaultFontBold;
                lblPKDelivery.Font = _labelDefaultFontBold;
                lblCustomer.Font = _labelDefaultFontBold;
                lbltxtCustomer.Font = _labelDefaultFont;
                lblPKDocDateWeek.Font = _labelDefaultFont;
                lblPKDeliveryWeek.Font = _labelDefaultFont;
                txtPKNumber.Font = _labelDefaultFontBold;
                lblTermsOfDelivery.Font = _labelDefaultFont;
                lblCurrency.Font = _labelDefaultFont;
                //Terms Tab
                lblCompany.Font = _labelDefaultFontBold;
                lblAddress.Font = _labelDefaultFontBold;
                lblContact.Font = _labelDefaultFontBold;
                lblShipTo.Font = _labelDefaultFontBold;
                lblInvoiceTo.Font = _labelDefaultFontBold;
                lblTermPayment.Font = _labelDefaultFontBold;
                lblTxtCompany.Font = _labelDefaultFontBold;
                lblTxtAddress.Font = _labelDefaultFont;
                lblTxtContact.Font = _labelDefaultFont;
                lblTxtShipTo.Font = _labelDefaultFont;
                lblTxtInvoiceTo.Font = _labelDefaultFont;

                /********* Texts **********/
                //Headers
                lblPKNumber.Text = "PK Number";
                lbltxtStatus.Text = string.Empty;
                lblDate.Text = "Date";
                lblWeek.Text = "Week";
                lblPKDocDate.Text = "DATE";
                lblPKDelivery.Text = "DELIVERY";
                lblCustomer.Text = "CUSTOMER";
                lbltxtCustomer.Text = _customersEtniaBcn.CustomerName;
                lblPKDocDateWeek.Text = string.Empty;
                lblPKDeliveryWeek.Text = string.Empty;
                lblTermsOfDelivery.Text = "Terms of Delivery";
                lblCurrency.Text = "Currency";
                //Terms Tab
                lblCompany.Text = "Company:";
                lblAddress.Text = "Address:";
                lblContact.Text = "Contact:";
                lblShipTo.Text = "Ship To:";
                lblInvoiceTo.Text = "Invoice To:";
                lblTermPayment.Text = "Term of Payment:";
                lblTxtCompany.Text = string.Empty;
                lblTxtAddress.Text = string.Empty;
                lblTxtContact.Text = string.Empty;
                lblTxtShipTo.Text = string.Empty;
                lblTxtInvoiceTo.Text = string.Empty;

                /********* Align **********/
                //Headers
                lbltxtStatus.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                lblPKDocDateWeek.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                lblPKDeliveryWeek.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                txtPKNumber.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;

                //Terms Tab
                lblCompany.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
                lblAddress.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
                lblContact.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
                lblShipTo.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
                lblInvoiceTo.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
                lblTermPayment.Appearance.TextOptions.HAlignment = HorzAlignment.Far;

                /********* ReadOnly **********/
                //txtPKNumber.ReadOnly = true; //no es un label, lo sé

                /********* BackColor **********/
                txtPKNumber.Properties.Appearance.BackColor = Color.CadetBlue;
                txtPKNumber.Properties.Appearance.BackColor2 = Color.CadetBlue;

            }
            catch
            {
                throw;
            }
        }

        private void SetObjectsReadOnly()
        {
            try
            {
                dateEditPKDocDate.ReadOnly = true;
                dateEditPKDelivery.ReadOnly = true;
                slueDeliveryTerms.ReadOnly = true;
                slueCurrency.ReadOnly = true;
                sluePaymentTerm.ReadOnly = true;
            }
            catch
            {
                throw;
            }
        }

        private void SetObjectsEnableToCreate()
        {
            try
            {
                dateEditPKDelivery.ReadOnly = false; //TODO --> no tiene que ser editable cuando tengamos lead time y se calcule 
                slueDeliveryTerms.ReadOnly = false;
                slueCurrency.ReadOnly = false;
                sluePaymentTerm.ReadOnly = false;
            }
            catch
            {
                throw;
            }
        }

        private void SetUpButtons()
        {
            try
            {

            }
            catch
            {
                throw;
            }
        }

        private void SetUpSearchLookUpEdit()
        {
            try
            {
                SetUpSlueCurrency();
                SetUpSlueDeliveryTerms();
                SetUpSluePaymentTerms();
            }
            catch
            {
                throw;
            }
        }

        private void SetUpSlueCurrency()
        {
            try
            {
                slueCurrency.Properties.DataSource = _currenciesList;
                slueCurrency.Properties.ValueMember = nameof(Currency.IdCurrency);
                slueCurrency.Properties.DisplayMember = nameof(Currency.Description);
                slueCurrency.Properties.View.Columns.AddField(nameof(Currency.IdCurrency)).Visible = true;
                slueCurrency.Properties.View.Columns.AddField(nameof(Currency.Description)).Visible = true;
                slueCurrency.Properties.NullText = "Select Currency...";
            }
            catch
            {
                throw;
            }
        }

        private void SetUpSlueDeliveryTerms()
        {
            try
            {
                slueDeliveryTerms.Properties.DataSource = _deliveryTermList;
                slueDeliveryTerms.Properties.ValueMember = nameof(DeliveryTerm.IdDeliveryTerm);
                slueDeliveryTerms.Properties.DisplayMember = nameof(DeliveryTerm.IdDeliveryTerm);
                slueDeliveryTerms.Properties.View.Columns.AddField(nameof(DeliveryTerm.IdDeliveryTerm)).Visible = true;
                slueDeliveryTerms.Properties.View.Columns.AddField(nameof(DeliveryTerm.Description)).Visible = true;
                slueDeliveryTerms.Properties.NullText = "Select Delivery Term...";
                slueDeliveryTerms.Properties.ShowClearButton = false;
            }
            catch
            {
                throw;
            }
        }

        private void SetUpSluePaymentTerms()
        {
            try
            {
                sluePaymentTerm.Properties.DataSource = _paymentTermsList;
                sluePaymentTerm.Properties.ValueMember = nameof(PaymentTerms.IdPaymentTerms);
                sluePaymentTerm.Properties.DisplayMember = nameof(PaymentTerms.IdPaymentTerms);
                sluePaymentTerm.Properties.View.Columns.AddField(nameof(PaymentTerms.IdPaymentTerms)).Visible = true;
                sluePaymentTerm.Properties.View.Columns.AddField(nameof(PaymentTerms.Description)).Visible = true;
                sluePaymentTerm.Properties.NullText = "Select Payment Term...";
                sluePaymentTerm.Properties.ShowClearButton = false;
            }
            catch
            {
                throw;
            }
        }

        private void SetUpEvents()
        {
            try
            {
                sbSearch.Click += SbSearch_Click;
                sbFinishPK.Click += SbFinishPK_Click;
                dateEditPKDocDate.EditValueChanged += DateEditPKCreation_EditValueChanged;
                dateEditPKDelivery.EditValueChanged += DateEditPKDelivery_EditValueChanged;
                txtPKNumber.KeyDown += TxtPKNumber_KeyDown;
            }
            catch
            {
                throw;
            }
        }

        private void SetUpGrdPoSelection()
        {
            try
            {
                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                gridViewPoSelection.OptionsView.ColumnAutoWidth = false;
                gridViewPoSelection.HorzScrollVisibility = ScrollVisibility.Auto;

                //Hacer todo el grid no editable
                gridViewPoSelection.OptionsBehavior.Editable = false;

                //Hide grouping panel
                gridViewPoSelection.OptionsView.ShowGroupPanel = false;

                //select with checbox
                gridViewPoSelection.OptionsSelection.MultiSelect = true;
                gridViewPoSelection.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
                gridViewPoSelection.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DefaultBoolean.False;

                //Disable master/detail for doc lines.
                gridViewPoSelection.OptionsDetail.EnableMasterViewMode = false;

                //Column Definition
                GridColumn colIdDoc = new GridColumn() { Caption = "SALES ORDER", Visible = true, FieldName = nameof(DocHead.IdDoc), Width = 150 };
                GridColumn colCreationDate = new GridColumn() { Caption = "CREATION ORDER", Visible = true, FieldName = nameof(DocHead.CreationDate), Width = 150 };
                GridColumn colDeliveryDate = new GridColumn() { Caption = "DELIVERY DATE", Visible = true, FieldName = nameof(DocHead.DeliveryDate), Width = 150 };
                GridColumn colIdDeliveryTerm = new GridColumn() { Caption = "TOD", Visible = true, FieldName = nameof(DocHead.IdDeliveryTerm), Width = 200 };
                GridColumn colIdCurrency = new GridColumn() { Caption = "CURRENCY", Visible = true, FieldName = nameof(DocHead.IdCurrency), Width = 150 };

                //GridColumn colTotalAmount = new GridColumn() { Caption = "SALES ORDER", Visible = true, FieldName = nameof(DocHead.IdDoc), Width = 100 };

                //Display Format
                //colTotalAmount.DisplayFormat.FormatType = FormatType.Numeric;
                //colTotalAmount.DisplayFormat.FormatString = "n2";

                gridViewPoSelection.Columns.Add(colIdDoc);
                gridViewPoSelection.Columns.Add(colCreationDate);
                gridViewPoSelection.Columns.Add(colDeliveryDate);
                gridViewPoSelection.Columns.Add(colIdDeliveryTerm);
                gridViewPoSelection.Columns.Add(colIdCurrency);
                //gridViewSoSelection.Columns.Add(colTotalAmount);

                //Events
                gridViewPoSelection.SelectionChanged += GridViewSoSelection_SelectionChanged;

            }
            catch
            {
                throw;
            }
        }

        private void SetUpGrdLinesPoSelection()
        {
            try
            {
                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                gridViewLinesPoSelection.OptionsView.ColumnAutoWidth = false;
                gridViewLinesPoSelection.HorzScrollVisibility = ScrollVisibility.Auto;

                //Hacer todo el grid no editable
                gridViewLinesPoSelection.OptionsBehavior.Editable = false;

                //Hide grouping panel
                gridViewLinesPoSelection.OptionsView.ShowGroupPanel = false;

                //select with checbox
                gridViewLinesPoSelection.OptionsSelection.MultiSelect = true;
                gridViewLinesPoSelection.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
                gridViewLinesPoSelection.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DefaultBoolean.False;

                //Column Definition
                GridColumn colIdItemBcn = new GridColumn() { Caption = "Item Code", Visible = true, FieldName = nameof(DocLine.IdItemBcn), Width = 200 };
                GridColumn colDescription = new GridColumn() { Caption = "Description", Visible = true, FieldName = nameof(DocLine.ItemDesc), Width = 350 };
                GridColumn colQuantity = new GridColumn() { Caption = "Order Quantity", Visible = true, FieldName = nameof(DocLine.Quantity), Width = 110 };
                GridColumn colDeliveredQuantity = new GridColumn() { Caption = "Delivered Qty", Visible = true, FieldName = nameof(DocLine.DeliveredQuantity), Width = 110 };
                GridColumn colDummyQuantity = new GridColumn() { Caption = "Packing Quantity", Visible = true, FieldName = nameof(DocLine.DummyQuantity), Width = 110 };
                GridColumn colUnit = new GridColumn() { Caption = "Unit", Visible = true, FieldName = nameof(DocLine.ItemUnit), Width = 85 };
                GridColumn colUnitPrice = new GridColumn() { Caption = "Unit Price", Visible = true, FieldName = nameof(DocLine.UnitPrice), Width = 85 };
                GridColumn colTotalAmount = new GridColumn() { Caption = "TotalAmount", Visible = true, FieldName = nameof(DocLine.TotalAmount), Width = 120 };
                GridColumn colIdIdSupplyStatus = new GridColumn() { Caption = "Status", Visible = true, FieldName = nameof(DocLine.IdSupplyStatus), Width = 75 };
                //Unbound Column. Para calcular la cantidad pendiente
                GridColumn colPendingQuantity = new GridColumn() { Caption = "Pending Qty", Visible = true, FieldName = "PENDING_QTY", Width = 110, UnboundType = UnboundColumnType.Integer };

                //Display Format
                colUnitPrice.DisplayFormat.FormatType = FormatType.Numeric;
                colUnitPrice.DisplayFormat.FormatString = "n2";

                colTotalAmount.DisplayFormat.FormatType = FormatType.Numeric;
                colTotalAmount.DisplayFormat.FormatString = "n2";

                colQuantity.DisplayFormat.FormatType = FormatType.Numeric;
                colQuantity.DisplayFormat.FormatString = "n0";

                colDeliveredQuantity.DisplayFormat.FormatType = FormatType.Numeric;
                colDeliveredQuantity.DisplayFormat.FormatString = "n0";

                colPendingQuantity.DisplayFormat.FormatType = FormatType.Numeric;
                colPendingQuantity.DisplayFormat.FormatString = "n0";

                colDummyQuantity.DisplayFormat.FormatType = FormatType.Numeric;
                colDummyQuantity.DisplayFormat.FormatString = "n0";

                //Edit Repositories
                RepositoryItemTextEdit ritxtInt = new RepositoryItemTextEdit();
                ritxtInt.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                ritxtInt.Mask.EditMask = "N";
                ritxtInt.AllowNullInput = DefaultBoolean.True;

                colQuantity.ColumnEdit = ritxtInt;

                RepositoryItemSearchLookUpEdit riSupplyStatus = new RepositoryItemSearchLookUpEdit()
                {
                    DataSource = _supplyStatusList,
                    ValueMember = nameof(SupplyStatus.IdSupplyStatus),
                    DisplayMember = nameof(SupplyStatus.Description),
                    ShowClearButton = false,
                    NullText = string.Empty,
                };
                colIdIdSupplyStatus.ColumnEdit = riSupplyStatus;

                //Summaries
                gridViewLinesPoSelection.OptionsView.ShowFooter = true;

                colTotalAmount.Summary.Add(SummaryItemType.Sum, nameof(DocLine.TotalAmount), "{0:n2}");

                colDummyQuantity.Summary.Add(SummaryItemType.Sum, nameof(DocLine.DummyQuantity), "{0:n2}");
                colQuantity.Summary.Add(SummaryItemType.Sum, nameof(DocLine.Quantity), "{0:n2}");

                //Add columns to grid root view
                gridViewLinesPoSelection.Columns.Add(colIdItemBcn);
                gridViewLinesPoSelection.Columns.Add(colDescription);
                gridViewLinesPoSelection.Columns.Add(colQuantity);
                gridViewLinesPoSelection.Columns.Add(colDeliveredQuantity);
                gridViewLinesPoSelection.Columns.Add(colPendingQuantity);
                gridViewLinesPoSelection.Columns.Add(colDummyQuantity);
                gridViewLinesPoSelection.Columns.Add(colUnit);
                gridViewLinesPoSelection.Columns.Add(colUnitPrice);
                gridViewLinesPoSelection.Columns.Add(colTotalAmount);
                gridViewLinesPoSelection.Columns.Add(colIdIdSupplyStatus);

                //Events
                gridViewLinesPoSelection.SelectionChanged += GridViewLinesSoSelection_SelectionChanged;
                gridViewLinesPoSelection.CellValueChanged += GridViewLinesSoSelection_CellValueChanged;
                gridViewLinesPoSelection.ValidatingEditor += GridViewLinesSoSelection_ValidatingEditor;
                gridViewLinesPoSelection.ShowingEditor += GridViewLinesSoSelection_ShowingEditor;
                gridViewLinesPoSelection.CustomUnboundColumnData += GridViewLinesSoSelection_CustomUnboundColumnData;
            }
            catch
            {
                throw;
            }
        }

        private void SetUpGrdLinesDeliveredGoods()
        {
            try
            {
                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                gridViewLinesDeliveredGoods.OptionsView.ColumnAutoWidth = false;
                gridViewLinesDeliveredGoods.HorzScrollVisibility = ScrollVisibility.Auto;

                //Hacer todo el grid no editable
                gridViewLinesDeliveredGoods.OptionsBehavior.Editable = false;

                //Hide grouping panel
                gridViewLinesDeliveredGoods.OptionsView.ShowGroupPanel = false;

                //Column Definition
                GridColumn colIdDocRelated = new GridColumn() { Caption = "Sales Order", Visible = true, FieldName = nameof(DocLine.IdDocRelated), Width = 200 };
                GridColumn colIdItemBcn = new GridColumn() { Caption = "Item Code", Visible = true, FieldName = nameof(DocLine.IdItemBcn), Width = 200 };
                GridColumn colDescription = new GridColumn() { Caption = "Description", Visible = true, FieldName = nameof(DocLine.ItemDesc), Width = 350 };
                GridColumn colQuantityOriginal = new GridColumn() { Caption = "Order Quantity", Visible = true, FieldName = nameof(DocLine.QuantityOriginal), Width = 110 };
                GridColumn colQuantity = new GridColumn() { Caption = "Quantity", Visible = true, FieldName = nameof(DocLine.Quantity), Width = 85 };
                GridColumn colUnit = new GridColumn() { Caption = "Unit", Visible = true, FieldName = nameof(DocLine.ItemUnit), Width = 85 };
                GridColumn colUnitPrice = new GridColumn() { Caption = "Unit Price", Visible = true, FieldName = nameof(DocLine.UnitPrice), Width = 85 };
                GridColumn colTotalAmount = new GridColumn() { Caption = "Total Amount", Visible = true, FieldName = nameof(DocLine.TotalAmount), Width = 120 };
                GridColumn colRemarks = new GridColumn() { Caption = "Notes", Visible = true, FieldName = nameof(DocLine.Remarks), Width = 300 };

                //Display Format
                colUnitPrice.DisplayFormat.FormatType = FormatType.Numeric;
                colUnitPrice.DisplayFormat.FormatString = "n2";

                colTotalAmount.DisplayFormat.FormatType = FormatType.Numeric;
                colTotalAmount.DisplayFormat.FormatString = "n2";

                colQuantity.DisplayFormat.FormatType = FormatType.Numeric;
                colQuantity.DisplayFormat.FormatString = "n0";

                colQuantityOriginal.DisplayFormat.FormatType = FormatType.Numeric;
                colQuantityOriginal.DisplayFormat.FormatString = "n0";

                //Summaries
                gridViewLinesDeliveredGoods.OptionsView.ShowFooter = true;

                colTotalAmount.Summary.Add(SummaryItemType.Sum, nameof(DocLine.TotalAmount), "{0:n2}");
                colQuantityOriginal.Summary.Add(SummaryItemType.Sum, nameof(DocLine.QuantityOriginal), "{0:n2}");
                colQuantity.Summary.Add(SummaryItemType.Sum, nameof(DocLine.Quantity), "{0:n2}");

                //Add columns to grid root view
                gridViewLinesDeliveredGoods.Columns.Add(colIdDocRelated);
                gridViewLinesDeliveredGoods.Columns.Add(colIdItemBcn);
                gridViewLinesDeliveredGoods.Columns.Add(colDescription);
                gridViewLinesDeliveredGoods.Columns.Add(colQuantityOriginal);
                gridViewLinesDeliveredGoods.Columns.Add(colQuantity);
                gridViewLinesDeliveredGoods.Columns.Add(colUnit);
                gridViewLinesDeliveredGoods.Columns.Add(colUnitPrice);
                gridViewLinesDeliveredGoods.Columns.Add(colTotalAmount);
                gridViewLinesDeliveredGoods.Columns.Add(colRemarks);

                //Events
                gridViewLinesDeliveredGoods.CustomSummaryCalculate += GridViewLinesDeliveredGoods_CustomSummaryCalculate;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Aux

        private void SetVisiblePropertyByState()
        {
            try
            {
                switch (CurrentState)
                {

                    case ActionsStates.New:
                        sbFinishPK.Visible = false;
                        sbSearch.Visible = false;
                        lbltxtStatus.Visible = false;
                        break;

                    case ActionsStates.Edit:
                        sbFinishPK.Visible = true;
                        sbSearch.Visible = false;
                        lbltxtStatus.Visible = true;
                        break;

                    default:
                        sbFinishPK.Visible = false;
                        sbSearch.Visible = true;
                        lbltxtStatus.Visible = false;
                        break;
                }
            }
            catch
            {
                throw;
            }
        }

        private void ResetForm()
        {
            try
            {
                /********* Head *********/
                //txtPKNumber.EditValue = null;
                dateEditPKDocDate.EditValue = null;
                dateEditPKDelivery.EditValue = null;
                lblPKDocDateWeek.Text = string.Empty;
                lblPKDeliveryWeek.Text = string.Empty;
                slueDeliveryTerms.EditValue = null;
                slueCurrency.EditValue = null;

                /********* Terms Tab *********/
                lblTxtCompany.Text = string.Empty;
                lblTxtAddress.Text = string.Empty;
                lblTxtContact.Text = string.Empty;
                lblTxtShipTo.Text = string.Empty;
                lblTxtInvoiceTo.Text = string.Empty;
                sluePaymentTerm.EditValue = null;
            }
            catch
            {
                throw;
            }
        }

        private void ResetFormChangeCustomer(bool resetCustomer = true)
        {
            try
            {
                /********* Head *********/
                txtPKNumber.EditValue = null;
                lbltxtStatus.Text = string.Empty;
                dateEditPKDocDate.EditValue = null;
                dateEditPKDelivery.EditValue = null;
                lblPKDocDateWeek.Text = string.Empty;
                lblPKDeliveryWeek.Text = string.Empty;
                slueDeliveryTerms.EditValue = null;
                slueCurrency.EditValue = null;

                /********* Terms Tab *********/
                lblTxtCompany.Text = string.Empty;
                lblTxtAddress.Text = string.Empty;
                lblTxtContact.Text = string.Empty;
                lblTxtShipTo.Text = string.Empty;
                lblTxtInvoiceTo.Text = string.Empty;
                sluePaymentTerm.EditValue = null;
            }
            catch
            {
                throw;
            }
        }

        private void SetCustomerInfo()
        {
            try
            {
                lblTxtCompany.Text = _customersEtniaBcn.CustomerName;
                lblTxtAddress.Text = _customersEtniaBcn.ShippingAddress;
                lblTxtContact.Text = $"{_customersEtniaBcn.ContactName} ({_customersEtniaBcn.ContactPhone})";
                sluePaymentTerm.EditValue = _customersEtniaBcn.IdPaymentTerms;
            }
            catch
            {
                throw;
            }
        }

        private void CopyToDeliveredGood(DocLine line)
        {
            try
            {
                if (line.Quantity == 0)
                    return;

                //Buscamos si ya existe
                var exist = _docLinesDeliveredGoodsList.Where(a => a.IdItemBcn.Equals(line.IdItemBcn) && a.IdDocRelated.Equals(line.IdDoc)).FirstOrDefault();

                if (exist == null)
                {
                    DocLine tmp = line.DeepCopyByExpressionTree();
                    tmp.IdDoc = txtPKNumber.Text;
                    tmp.IdDocRelated = line.IdDoc;
                    tmp.DeliveredQuantity = 0;
                    tmp.QuantityOriginal = line.Quantity;
                    tmp.Quantity = line.DummyQuantity;
                    tmp.Remarks = null;
                    _docLinesDeliveredGoodsList.Add(tmp);
                }
                else
                {
                    //Actualizamos la cantidad que es el única campo que se puede editar
                    exist.Quantity = line.DummyQuantity;
                    gridViewLinesDeliveredGoods.RefreshData();
                }
            }
            catch
            {
                throw;
            }
        }

        private void DeleteDeliveredGood(DocLine line)
        {
            try
            {
                var deliveredGoodsLine = _docLinesDeliveredGoodsList.Where(a => a.IdItemBcn.Equals(line.IdItemBcn) && a.IdDocRelated.Equals(line.IdDoc)).FirstOrDefault();
                if (deliveredGoodsLine != null)
                    _docLinesDeliveredGoodsList.Remove(deliveredGoodsLine);
            }
            catch
            {
                throw;
            }
        }

        private void MarkSoSelectionFromDeliveredGoods()
        {
            try
            {
                gridViewLinesPoSelection.BeginSelection();

                for (int i = 0; i < gridViewLinesPoSelection.DataRowCount; i++)
                {
                    DocLine rowSoSelection = gridViewLinesPoSelection.GetRow(i) as DocLine;

                    var rowDeliveredGoods = _docLinesDeliveredGoodsList?.Where(a => a.IdItemBcn.Equals(rowSoSelection.IdItemBcn) && a.IdDocRelated.Equals(rowSoSelection.IdDoc)).FirstOrDefault();

                    if (rowDeliveredGoods != null)
                    {
                        gridViewLinesPoSelection.SelectRow(i);
                        rowSoSelection.DummyQuantity = rowDeliveredGoods.Quantity; //Por si se ha modificado la cantidad, está guardada en la línea de delivered good
                        gridViewLinesPoSelection.RefreshRow(i);
                    }

                }

                gridViewLinesPoSelection.EndSelection();

            }
            catch
            {
                throw;
            }

        }

        private bool ValidatePK()
        {
            try
            {
                //TODO! Revisar las validaciones

                if (slueDeliveryTerms.EditValue == null)
                {
                    MessageBox.Show("Field Required: Term of Delivery", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    slueDeliveryTerms.Focus();
                    return false;
                }

                if (slueCurrency.EditValue == null)
                {
                    MessageBox.Show("Field Required: Currency", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    slueCurrency.Focus();
                    return false;
                }

                if (sluePaymentTerm.EditValue == null)
                {
                    MessageBox.Show("Field Required: Term of Payment", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    sluePaymentTerm.Focus();
                    return false;
                }

                if (dateEditPKDelivery.EditValue == null)
                {
                    MessageBox.Show("Field Required: Delivery Date", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dateEditPKDelivery.Focus();
                    return false;
                }

                if (_docLinesDeliveredGoodsList.Count == 0)
                {
                    MessageBox.Show("Packing without lines", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                return true;
            }
            catch
            {
                throw;
            }
        }

        private void SetGridsEnabled()
        {
            try
            {
                //Ponemos los grid como editables y bloqueamos las columnas que no se puede editar
                gridViewLinesPoSelection.OptionsBehavior.Editable = true;
                gridViewLinesDeliveredGoods.OptionsBehavior.Editable = true;

                foreach (GridColumn col in gridViewLinesPoSelection.Columns)
                {
                    if (col.FieldName != nameof(DocLine.DummyQuantity))
                        col.OptionsColumn.AllowEdit = false;
                }

                foreach (GridColumn col in gridViewLinesDeliveredGoods.Columns)
                {
                    if (col.FieldName != nameof(DocLine.Remarks))
                        col.OptionsColumn.AllowEdit = false;
                }
            }
            catch
            {
                throw;
            }
        }

        private void CancelEdit()
        {
            try
            {
                txtPKNumber.ReadOnly = false;

                string idPk = _docHeadPK?.IdDoc;
                ResetPK();
                ResetForm();
                txtPKNumber.Text = string.Empty;
                SetObjectsReadOnly();

                if (idPk != null)
                {
                    txtPKNumber.Text = idPk;
                    SearchPK();
                }
                RestoreInitState();
                SetVisiblePropertyByState();
            }
            catch
            {
                throw;
            }

        }

        private void ShowMessageDocsGenerated()
        {
            try
            {
                var docs = GlobalSetting.SupplyDocsService.GetDocsByRelated(_docHeadPK.IdDoc);

                if (docs.Count > 0)
                {
                    string msg = "The Following documents have been created:" + Environment.NewLine;

                    foreach (var doc in docs)
                    {
                        msg += $"{doc.IdDoc} ({doc.SupplyDocType.Description}){Environment.NewLine}";

                        if (doc.IdSupplyDocType == Constants.SUPPLY_DOCTYPE_DN)
                        {
                            var docs2 = GlobalSetting.SupplyDocsService.GetDocsByRelated(doc.IdDoc);
                            foreach (var doc2 in docs2)
                            {
                                msg += $"{doc2.IdDoc} ({doc2.SupplyDocType.Description}){Environment.NewLine}";
                            }
                        }
                    }

                    XtraMessageBox.Show(msg);
                }

            }
            catch
            {
                throw;
            }
        }


        #endregion

        #region Configure Ribbon Actions

        private void ConfigureActionsStackViewCreating()
        {
            try
            {
                dateEditPKDocDate.EditValue = DateTime.Now;

                //clean necessary objects
                Reset4NewPk();
                //Enable objects
                SetObjectsEnableToCreate();

                //generate packing list number
                CreatePkNumber();

                _docLinesDeliveredGoodsList = new BindingList<DocLine>();
                xgrdLinesDeliveredGoods.DataSource = null;
                xgrdLinesDeliveredGoods.DataSource = _docLinesDeliveredGoodsList;

                //habilitar los grids para edición
                SetGridsEnabled();

                SetVisiblePropertyByState();
            }
            catch
            {
                throw;
            }
        }

        private void ConfigureActionsStackViewEditing()
        {
            try
            {
                txtPKNumber.ReadOnly = true;
                //TODO: Revisar!!
                //cargamos las PO abiertas de Etnia Barcelona aparte de las que incluye el packing a editar
                var soDocsCustomer = GlobalSetting.SupplyDocsService.GetDocs(
                    idSupplier: null,
                    idCustomer: _customersEtniaBcn.IdCustomer,
                    docDate: new DateTime(1, 1, 1),  //filtrar sin fecha
                    IdSupplyDocType: Constants.SUPPLY_DOCTYPE_PO,
                    idSupplyStatus: Constants.SUPPLY_STATUS_OPEN);

                if (soDocsCustomer.Count != 0)
                {
                    //Con el union hace el merge y descarta duplicados
                    _docPoSelectionList = new BindingList<DocHead>(_docPoSelectionList.Union(soDocsCustomer).ToList());
                }
                xgrdPoSelection.DataSource = null;
                xgrdPoSelection.DataSource = _docPoSelectionList;

                xgrdLinesPoSelection.DataSource = null;


                SetGridsEnabled(); //habilitar los grids para edición
                SetVisiblePropertyByState();

            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region CRUD

        private void SearchCustomersPOs()
        {
            try
            {
                ResetCustomerPOs();

                var soDocsCustomer = GlobalSetting.SupplyDocsService.GetDocs(
                    idSupplier: Constants.ETNIA_HK_COMPANY_CODE,
                    idCustomer: _customersEtniaBcn.IdCustomer,
                    docDate: new DateTime(1, 1, 1),  //filtrar sin fecha
                    IdSupplyDocType: Constants.SUPPLY_DOCTYPE_PO,
                    idSupplyStatus: Constants.SUPPLY_STATUS_OPEN);

                if (soDocsCustomer.Count == 0)
                {
                    XtraMessageBox.Show("No Customer's Sales Orders open found", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _docPoSelectionList = new BindingList<DocHead>(soDocsCustomer);

                    xgrdPoSelection.DataSource = null;
                    xgrdPoSelection.DataSource = _docPoSelectionList;
                }
            }
            catch
            {
                throw;
            }
        }

        private bool ValidateIfExistOpenPK()
        {
            try
            {
                var exist = GlobalSetting.SupplyDocsService.GetDocs(
                    idSupplier: Constants.ETNIA_HK_COMPANY_CODE,
                    idCustomer: _customersEtniaBcn.IdCustomer,
                    //docDate: dateEditPKDocDate.DateTime, 
                    docDate: new DateTime(1, 1, 1),
                    IdSupplyDocType: Constants.SUPPLY_DOCTYPE_PL,
                    idSupplyStatus: Constants.SUPPLY_STATUS_OPEN);

                if (exist.Count > 0)
                {
                    XtraMessageBox.Show($"Packing List Open ({exist.Select(a => a.IdDoc).FirstOrDefault()}). You cannot craeted new one.");
                    return false;
                }
                else
                {
                    return true;
                }


            }
            catch
            {
                throw;
            }
        }

        private void CreatePkNumber()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                //string strCont;
                //string pkNumber = string.Empty;

                //var pakingsDocs = GlobalSetting.SupplyDocsService.GetDocs(
                //    idSupplier: null,
                //    idCustomer: (string)slueCustomer.EditValue,
                //    docDate: dateEditPKDocDate.DateTime,
                //    IdSupplyDocType: Constants.SUPPLY_DOCTYPE_PL,
                //    idSupplyStatus: null);

                //strCont = $"{(pakingsDocs.Count + 1).ToString().PadLeft(3, '0')}";

                //pkNumber = $"{Constants.SUPPLY_DOCTYPE_PL}{slueCustomer.EditValue}{DateTime.Now.Year.ToString()}{DateTime.Now.Month.ToString("d2")}{strCont}";

                string pkNumber = GlobalSetting.SupplyDocsService.GetPackingListNumber(_customersEtniaBcn.IdCustomer, DateTime.Now);

                txtPKNumber.Text = pkNumber;
            }
            catch
            {
                throw;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private bool CreatePK()
        {
            try
            {
                List<DocLine> sortedLines = _docLinesDeliveredGoodsList.OrderBy(a => a.IdItemGroup).ThenBy(b => b.IdItemBcn).ToList();

                DocHead packingList = new DocHead()
                {
                    IdDoc = txtPKNumber.Text,
                    IdSupplyDocType = Constants.SUPPLY_DOCTYPE_PL,
                    CreationDate = DateTime.Now,
                    DeliveryDate = dateEditPKDelivery.DateTime,
                    DocDate = dateEditPKDocDate.DateTime, //DateTime.Now,
                    IdSupplyStatus = Constants.SUPPLY_STATUS_OPEN,
                    IdSupplier = Constants.ETNIA_HK_COMPANY_CODE,
                    IdCustomer = _customersEtniaBcn.IdCustomer,
                    IdDeliveryTerm = slueDeliveryTerms.EditValue as string,
                    IdPaymentTerms = sluePaymentTerm.EditValue as string,
                    IdCurrency = slueCurrency.EditValue as string,
                    Lines = sortedLines
                };

                DocHead createdDoc = GlobalSetting.SupplyDocsService.NewDoc(packingList);

                _docHeadPK = createdDoc;

                return true;
            }
            catch
            {
                throw;
            }
        }

        private bool UpdatePK(bool finishPK = false)
        {
            try
            {
                List<DocLine> sortedLines = _docLinesDeliveredGoodsList.OrderBy(a => a.IdItemGroup).ThenBy(b => b.IdItemBcn).ToList();

                DocHead packingList = new DocHead()
                {

                    IdDoc = _docHeadPK.IdDoc,
                    IdDocRelated = _docHeadPK.IdDocRelated,
                    IdSupplyDocType = _docHeadPK.IdSupplyDocType,
                    CreationDate = _docHeadPK.CreationDate,
                    DeliveryDate = dateEditPKDelivery.DateTime,
                    DocDate = _docHeadPK.DocDate,
                    IdSupplyStatus = (finishPK ? Constants.SUPPLY_STATUS_CLOSE : Constants.SUPPLY_STATUS_OPEN),
                    IdSupplier = _docHeadPK.IdSupplier,
                    IdCustomer = _docHeadPK.IdCustomer,
                    IdDeliveryTerm = slueDeliveryTerms.EditValue as string,
                    IdPaymentTerms = sluePaymentTerm.EditValue as string,
                    IdCurrency = slueCurrency.EditValue as string,
                    Lines = sortedLines
                };

                DocHead updatedDoc = GlobalSetting.SupplyDocsService.UpdateDoc(packingList, finishDoc: finishPK);

                _docHeadPK = updatedDoc;

                return true;
            }
            catch
            {
                throw;
            }

        }

        private void ActionsAfterCU()
        {
            try
            {
                //Clear grids
                xgrdPoSelection.DataSource = null;
                xgrdLinesPoSelection.DataSource = null;
                xgrdLinesDeliveredGoods.DataSource = null;
                //Reload Packing list
                LoadPK();
                //not allow grid editing
                gridViewPoSelection.OptionsBehavior.Editable = false;
                gridViewLinesPoSelection.OptionsBehavior.Editable = false;
                gridViewLinesDeliveredGoods.OptionsBehavior.Editable = false;
                //Restore de ribbon to initial states
                RestoreInitState();

                SetVisiblePropertyByState();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #endregion


    }
}
