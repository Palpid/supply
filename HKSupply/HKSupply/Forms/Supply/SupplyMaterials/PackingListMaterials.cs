using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using HKSupply.General;
using HKSupply.Helpers;
using HKSupply.Models;
using HKSupply.Models.Supply;
using HKSupply.Styles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HKSupply.Forms.Supply.SupplyMaterials
{
    public partial class PackingListMaterials : RibbonFormBase
    {
        #region Constants
        private const string COL_SELECTED = "Selected";
        private const string VIEW_COLUMN = "View";
        #endregion

        #region Private Members

        Font _labelDefaultFontBold = new Font("SourceSansProRegular", 8, FontStyle.Bold);
        Font _labelDefaultFont = new Font("SourceSansProRegular", 8, FontStyle.Regular);

        List<SupplyStatus> _supplyStatusList;

        List<Supplier> _suppliersList;
        List<Currency> _currenciesList;
        List<DeliveryTerm> _deliveryTermList;
        List<PaymentTerms> _paymentTermsList;

        DocHead _docHeadPK;
        BindingList<DocHead> _docPoSelectionList;
        BindingList<DocLine> _docLinesPoSelectionList;
        BindingList<DocLine> _docLinesDeliveredGoodsList;
        BindingList<DocHeadAttachFile> _docHeadAttachFileList;

        bool _isLoadingPacking = false;
        bool _isCreatingPacking = false;
        #endregion

        #region Constructor
        public PackingListMaterials()
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
                SetUpGrdFiles();
                SetupPanelControl();
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
                if (slueSupplier.EditValue == null)
                {
                    MessageBox.Show("Select a supplier");
                    RestoreInitState();
                }
                else
                {
                    //buscamos las PO abiertas del supplier
                    SearchSuppliersPOs();
                }

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
                else if (slueSupplier.EditValue != null)
                {
                    SearchSuppliersPOs();
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

        private void DateEditPKDocDate_EditValueChanged(object sender, EventArgs e)
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

        private void SlueSupplier_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (slueSupplier.EditValue != null)
                {
                    if (_isLoadingPacking == false)
                    {
                        var supplier = slueSupplier.EditValue;
                        ResetPK();
                        ResetForm(resetSupplier: false);
                        slueSupplier.EditValue = supplier;
                    }

                    SetSupplierInfo();
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
                if (CurrentState == ActionsStates.Edit || CurrentState == ActionsStates.New)
                    return;

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

        private void TxtPKNumber_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (_isLoadingPacking || _isCreatingPacking)
                    return;

                ResetPK();
                ResetForm(resetPkNumber: false);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SbViewNewFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPathNewFile.Text) == false)
                {
                    DocHelper.OpenDoc(txtPathNewFile.Text);
                }
                else
                {
                    XtraMessageBox.Show(GlobalSetting.ResManager.GetString("NoFileSelected"), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SbOpenFile_Click(object sender, EventArgs e)
        {
            try
            {

                OpenFileDialog openFileDialog = new OpenFileDialog()
                {
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    Filter = "PDF files (*.pdf)|*.pdf|JPG files(*.jpg)|*.jpg|PNG files (*.png)|*.png",
                    Multiselect = false,
                    RestoreDirectory = true,
                };
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtPathNewFile.Text = openFileDialog.FileName;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SbAttachFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPathNewFile.Text) == false || System.IO.File.Exists(txtPathNewFile.Text))
                {
                    AttachFile();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GridViewPoSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
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

        private void GridViewLinesPoSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                DocLine line = view.GetRow(view.FocusedRowHandle) as DocLine;

                if (line == null)
                    return;

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

        private void GridViewLinesPoSelection_CellValueChanged(object sender, CellValueChangedEventArgs e)
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

        private void GridViewLinesPoSelection_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            //TODO: los pedidos de acetato llevan decimales??
            //try
            //{
            //    GridView view = sender as GridView;

            //    DocLine line = view.GetRow(view.FocusedRowHandle) as DocLine;

            //    switch (view.FocusedColumn.FieldName)
            //    {

            //        case nameof(DocLine.DummyQuantity):

            //            decimal qty = Convert.ToDecimal(e.Value);

            //            //Sólo los RM puede ser decimales
            //            if (line.IdItemGroup != Constants.ITEM_GROUP_MT)
            //            {
            //                bool isInteger = unchecked(qty == (int)qty);
            //                if (isInteger == false)
            //                {
            //                    e.Valid = false;
            //                    e.ErrorText = "Value must be integer";
            //                }

            //            }

            //            if (qty > (line.Quantity - line.DeliveredQuantity))
            //            {
            //                e.Valid = false;
            //                e.ErrorText = "Invalid quantity";
            //            }
            //            break;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void GridViewLinesPoSelection_ShowingEditor(object sender, CancelEventArgs e)
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

        private void GridViewLinesPoSelection_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                if (e.Column.FieldName == "PENDING_QTY" && e.IsGetData)
                    e.Value = (
                        (decimal)view.GetRowCellValue(e.ListSourceRowIndex, nameof(DocLine.Quantity)) -
                        (decimal)view.GetRowCellValue(e.ListSourceRowIndex, nameof(DocLine.DeliveredQuantity))
                        );
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RepoButton_Click(object sender, EventArgs e)
        {
            try
            {
                DocHeadAttachFile attachFile = gridViewFiles.GetRow(gridViewFiles.FocusedRowHandle) as DocHeadAttachFile;

                if (attachFile != null)
                {
                    DocHelper.OpenDoc(Constants.SUPPLY_ATTACH_FILES_PATH + attachFile.FilePath);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion


        #region Private Methods

        #region Load/Resets

        private void LoadAuxList()
        {
            try
            {
                _suppliersList = GlobalSetting.SupplierService.GetSuppliers().Where(a => a.Factory == false).ToList();
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

        private void ResetSupplierPOs()
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
                ResetSupplierPOs();
                _docHeadPK = null;
                xgrdPoSelection.DataSource = null;
                xgrdLinesPoSelection.DataSource = null;
                xgrdLinesDeliveredGoods.DataSource = null;
                xgrdFiles.DataSource = null;

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
                sluePaymentTerm.EditValue = null;

                txtSupplierReference.EditValue = null;
                memoEditRemarks.EditValue = null;

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
                _isLoadingPacking = true;
                ResetPK();
                ResetForm(resetPkNumber: false);

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
                else if (_docHeadPK.IdCustomer != Constants.ETNIA_HK_COMPANY_CODE)
                {
                    XtraMessageBox.Show("Packing List is not from material supplier", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //load attached files
                    _docHeadAttachFileList = new BindingList<DocHeadAttachFile>(GlobalSetting.DocHeadAttachFileService.GetDocHeadAttachFile(_docHeadPK.IdDoc));
                    LoadPK();
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                _isLoadingPacking = false;
            }
        }
        
        private void LoadPK()
        {
            try
            {
                var supplier = _suppliersList.Where(a => a.IdSupplier.Equals(_docHeadPK.IdSupplier)).FirstOrDefault();

                //***** Header *****/
                lbltxtStatus.Visible = true;
                lbltxtStatus.Text = _docHeadPK.IdSupplyStatus;
                slueSupplier.EditValue = _docHeadPK.IdSupplier;
                slueDeliveryTerms.EditValue = _docHeadPK.IdDeliveryTerm;
                slueCurrency.EditValue = _docHeadPK.IdCurrency;

                dateEditPKDocDate.EditValue = _docHeadPK.DocDate;
                dateEditPKDelivery.EditValue = _docHeadPK.DeliveryDate;

                lblPKDocDateWeek.Text = dateEditPKDocDate.DateTime.GetWeek().ToString();
                lblPKDeliveryWeek.Text = dateEditPKDelivery.DateTime.GetWeek().ToString();

                txtSupplierReference.Text = _docHeadPK.ManualReference;
                memoEditRemarks.Text = _docHeadPK.Remarks;

                //***** Grid SO Selection *****/
                ResetSupplierPOs();
                var packingSOs = GlobalSetting.SupplyDocsService.GetSalesOrderFromPackingList(_docHeadPK.IdDoc);
                _docPoSelectionList = new BindingList<DocHead>(packingSOs);
                xgrdPoSelection.DataSource = null;
                xgrdPoSelection.DataSource = _docPoSelectionList;

                //***** Grid Delivered Goods*****/
                _docLinesDeliveredGoodsList = new BindingList<DocLine>(_docHeadPK.Lines);

                xgrdLinesDeliveredGoods.DataSource = null;
                xgrdLinesDeliveredGoods.DataSource = _docLinesDeliveredGoodsList;

                //***** Terms Tab *****/
                lblTxtCompany.Text = supplier.SupplierName;
                lblTxtAddress.Text = supplier.ShippingAddress;
                lblTxtContact.Text = $"{supplier.ContactName} ({supplier.ContactPhone})";
                lblTxtShipTo.Text = "??"; //TODO
                lblTxtInvoiceTo.Text = "??"; //TODO
                sluePaymentTerm.EditValue = _docHeadPK.IdPaymentTerms;

                //***** Files Tab *****/
                xgrdFiles.DataSource = null;
                xgrdFiles.DataSource = _docHeadAttachFileList;
                xtpFiles.PageVisible = true;

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
                lblSupplier.Font = _labelDefaultFontBold;
                lblPKDocDateWeek.Font = _labelDefaultFont;
                lblPKDeliveryWeek.Font = _labelDefaultFont;
                txtPKNumber.Font = _labelDefaultFontBold;
                lblTermsOfDelivery.Font = _labelDefaultFont;
                lblCurrency.Font = _labelDefaultFont;
                lblSupplierReference.Font = _labelDefaultFont;
                lblRemarks.Font = _labelDefaultFont;

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
                lblSupplier.Text = "SUPPLIER";
                lblPKDocDateWeek.Text = string.Empty;
                lblPKDeliveryWeek.Text = string.Empty;
                lblTermsOfDelivery.Text = "Terms of Delivery";
                lblCurrency.Text = "Currency";
                lblSupplierReference.Text = "Supplier Reference";
                lblRemarks.Text = "Remarks";

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
                txtSupplierReference.ReadOnly = true;
                memoEditRemarks.ReadOnly = true;
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
                dateEditPKDelivery.ReadOnly = false;
                slueDeliveryTerms.ReadOnly = false;
                //slueCurrency.ReadOnly = false;
                sluePaymentTerm.ReadOnly = false;
                txtSupplierReference.ReadOnly = false;
                memoEditRemarks.ReadOnly = false;
            }
            catch
            {
                throw;
            }
        }

        private void SetObjectsEnableToEdit()
        {
            try
            {
                txtSupplierReference.ReadOnly = false;
                memoEditRemarks.ReadOnly = false;
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
                SetUpSlueSupplier();
                SetUpSlueCurrency();
                SetUpSlueDeliveryTerms();
                SetUpSluePaymentTerms();
            }
            catch
            {
                throw;
            }
        }

        private void SetUpSlueSupplier()
        {
            try
            {
                slueSupplier.Properties.DataSource = _suppliersList;
                slueSupplier.Properties.ValueMember = nameof(Supplier.IdSupplier);
                slueSupplier.Properties.DisplayMember = nameof(Supplier.SupplierName);
                slueSupplier.Properties.View.Columns.AddField(nameof(Supplier.IdSupplier)).Visible = true;
                slueSupplier.Properties.View.Columns.AddField(nameof(Supplier.SupplierName)).Visible = true;
                slueSupplier.Properties.NullText = "Select Supplier...";
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
                dateEditPKDocDate.EditValueChanged += DateEditPKDocDate_EditValueChanged;
                dateEditPKDelivery.EditValueChanged += DateEditPKDelivery_EditValueChanged;
                slueSupplier.EditValueChanged += SlueSupplier_EditValueChanged;
                txtPKNumber.KeyDown += TxtPKNumber_KeyDown;
                txtPKNumber.EditValueChanged += TxtPKNumber_EditValueChanged;
                sbViewNewFile.Click += SbViewNewFile_Click;
                sbOpenFile.Click += SbOpenFile_Click;
                sbAttachFile.Click += SbAttachFile_Click;
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
                //Activar que se alternen los colores de las filas del grid
                gridViewPoSelection.OptionsView.EnableAppearanceOddRow = true;
                gridViewPoSelection.OptionsView.EnableAppearanceEvenRow = true;

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
                //GridColumn colIdDeliveryTerm = new GridColumn() { Caption = "TOD", Visible = true, FieldName = nameof(DocHead.IdDeliveryTerm), Width = 200 };
                GridColumn colIdCurrency = new GridColumn() { Caption = "CURRENCY", Visible = true, FieldName = nameof(DocHead.IdCurrency), Width = 150 };

                //GridColumn colTotalAmount = new GridColumn() { Caption = "SALES ORDER", Visible = true, FieldName = nameof(DocHead.IdDoc), Width = 100 };

                //Display Format
                //colTotalAmount.DisplayFormat.FormatType = FormatType.Numeric;
                //colTotalAmount.DisplayFormat.FormatString = "n2";

                gridViewPoSelection.Columns.Add(colIdDoc);
                gridViewPoSelection.Columns.Add(colCreationDate);
                gridViewPoSelection.Columns.Add(colDeliveryDate);
                //gridViewPoSelection.Columns.Add(colIdDeliveryTerm);
                gridViewPoSelection.Columns.Add(colIdCurrency);
                //gridViewSoSelection.Columns.Add(colTotalAmount);

                //Events
                gridViewPoSelection.SelectionChanged += GridViewPoSelection_SelectionChanged;

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
                //Activar que se alternen los colores de las filas del grid
                gridViewLinesPoSelection.OptionsView.EnableAppearanceOddRow = true;
                gridViewLinesPoSelection.OptionsView.EnableAppearanceEvenRow = true;

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
                GridColumn colIdItemGroup = new GridColumn() { Caption = "Type", Visible = true, FieldName = nameof(DocLine.IdItemGroup), Width = 100 };
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
                colUnitPrice.DisplayFormat.FormatString = "n4";

                colTotalAmount.DisplayFormat.FormatType = FormatType.Numeric;
                colTotalAmount.DisplayFormat.FormatString = "n4";

                colQuantity.DisplayFormat.FormatType = FormatType.Numeric;
                colQuantity.DisplayFormat.FormatString = "n3";

                colDeliveredQuantity.DisplayFormat.FormatType = FormatType.Numeric;
                colDeliveredQuantity.DisplayFormat.FormatString = "n3";

                colPendingQuantity.DisplayFormat.FormatType = FormatType.Numeric;
                colPendingQuantity.DisplayFormat.FormatString = "n3";

                colDummyQuantity.DisplayFormat.FormatType = FormatType.Numeric;
                colDummyQuantity.DisplayFormat.FormatString = "n3";

                //Edit Repositories
                RepositoryItemTextEdit ritxt3Dec = new RepositoryItemTextEdit();
                ritxt3Dec.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                ritxt3Dec.Mask.EditMask = "F3";
                ritxt3Dec.AllowNullInput = DefaultBoolean.True;

                colDummyQuantity.ColumnEdit = ritxt3Dec;

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

                colTotalAmount.Summary.Add(SummaryItemType.Sum, nameof(DocLine.TotalAmount), "{0:n4}");
                colDummyQuantity.Summary.Add(SummaryItemType.Sum, nameof(DocLine.TotalAmount), "{0}");

                //Add columns to grid root view
                gridViewLinesPoSelection.Columns.Add(colIdItemBcn);
                gridViewLinesPoSelection.Columns.Add(colDescription);
                gridViewLinesPoSelection.Columns.Add(colIdItemGroup);
                gridViewLinesPoSelection.Columns.Add(colQuantity);
                gridViewLinesPoSelection.Columns.Add(colDeliveredQuantity);
                gridViewLinesPoSelection.Columns.Add(colPendingQuantity);
                gridViewLinesPoSelection.Columns.Add(colDummyQuantity);
                gridViewLinesPoSelection.Columns.Add(colUnit);
                gridViewLinesPoSelection.Columns.Add(colUnitPrice);
                gridViewLinesPoSelection.Columns.Add(colTotalAmount);
                gridViewLinesPoSelection.Columns.Add(colIdIdSupplyStatus);

                //Events
                gridViewLinesPoSelection.SelectionChanged += GridViewLinesPoSelection_SelectionChanged;
                gridViewLinesPoSelection.CellValueChanged += GridViewLinesPoSelection_CellValueChanged;
                gridViewLinesPoSelection.ValidatingEditor += GridViewLinesPoSelection_ValidatingEditor;
                gridViewLinesPoSelection.ShowingEditor += GridViewLinesPoSelection_ShowingEditor;
                gridViewLinesPoSelection.CustomUnboundColumnData += GridViewLinesPoSelection_CustomUnboundColumnData;
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
                //Activar que se alternen los colores de las filas del grid
                gridViewLinesDeliveredGoods.OptionsView.EnableAppearanceOddRow = true;
                gridViewLinesDeliveredGoods.OptionsView.EnableAppearanceEvenRow = true;

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
                GridColumn colIdItemGroup = new GridColumn() { Caption = "Type", Visible = true, FieldName = nameof(DocLine.IdItemGroup), Width = 100 };
                GridColumn colQuantityOriginal = new GridColumn() { Caption = "Order Quantity", Visible = true, FieldName = nameof(DocLine.QuantityOriginal), Width = 110 };
                GridColumn colQuantity = new GridColumn() { Caption = "Quantity", Visible = true, FieldName = nameof(DocLine.Quantity), Width = 85 };
                GridColumn colUnit = new GridColumn() { Caption = "Unit", Visible = true, FieldName = nameof(DocLine.ItemUnit), Width = 85 };
                GridColumn colUnitPrice = new GridColumn() { Caption = "Unit Price", Visible = true, FieldName = nameof(DocLine.UnitPrice), Width = 85 };
                GridColumn colTotalAmount = new GridColumn() { Caption = "Total Amount", Visible = true, FieldName = nameof(DocLine.TotalAmount), Width = 120 };
                GridColumn colRemarks = new GridColumn() { Caption = "Notes", Visible = true, FieldName = nameof(DocLine.Remarks), Width = 300 };

                //Display Format
                colUnitPrice.DisplayFormat.FormatType = FormatType.Numeric;
                colUnitPrice.DisplayFormat.FormatString = "n4";

                colTotalAmount.DisplayFormat.FormatType = FormatType.Numeric;
                colTotalAmount.DisplayFormat.FormatString = "n4";

                colQuantity.DisplayFormat.FormatType = FormatType.Numeric;
                colQuantity.DisplayFormat.FormatString = "n3";

                colQuantityOriginal.DisplayFormat.FormatType = FormatType.Numeric;
                colQuantityOriginal.DisplayFormat.FormatString = "n3";

                //Summaries
                gridViewLinesDeliveredGoods.OptionsView.ShowFooter = true;

                colTotalAmount.Summary.Add(SummaryItemType.Sum, nameof(DocLine.TotalAmount), "{0:n4}");

                colQuantityOriginal.Summary.Add(SummaryItemType.Sum, nameof(DocLine.TotalAmount), "{0}");
                colQuantity.Summary.Add(SummaryItemType.Sum, nameof(DocLine.TotalAmount), "{0}");

                //Add columns to grid root view
                gridViewLinesDeliveredGoods.Columns.Add(colIdDocRelated);
                gridViewLinesDeliveredGoods.Columns.Add(colIdItemBcn);
                gridViewLinesDeliveredGoods.Columns.Add(colDescription);
                gridViewLinesDeliveredGoods.Columns.Add(colIdItemGroup);
                gridViewLinesDeliveredGoods.Columns.Add(colQuantityOriginal);
                gridViewLinesDeliveredGoods.Columns.Add(colQuantity);
                gridViewLinesDeliveredGoods.Columns.Add(colUnit);
                gridViewLinesDeliveredGoods.Columns.Add(colUnitPrice);
                gridViewLinesDeliveredGoods.Columns.Add(colTotalAmount);
                gridViewLinesDeliveredGoods.Columns.Add(colRemarks);

                //Events
                //gridViewLinesDeliveredGoods.CustomSummaryCalculate += GridViewLinesDeliveredGoods_CustomSummaryCalculate; //TODO?
            }
            catch
            {
                throw;
            }
        }

        private void SetUpGrdFiles()
        {
            try
            {
                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                gridViewFiles.OptionsView.ColumnAutoWidth = false;
                gridViewFiles.HorzScrollVisibility = ScrollVisibility.Auto;

                //Columns definition
                GridColumn colFileName = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("FileName"), Visible = true, FieldName = nameof(DocHeadAttachFile.FileName), Width = 350 };
                GridColumn colUser = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("User"), Visible = true, FieldName = nameof(DocHeadAttachFile.User), Width = 100 };
                GridColumn colCreateDate = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("CreateDate"), Visible = true, FieldName = nameof(DocHeadAttachFile.CreateDate), Width = 130 };
                GridColumn colViewButton = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("View"), Visible = true, FieldName = VIEW_COLUMN, Width = 50 };

                //Display Format
                colCreateDate.DisplayFormat.FormatType = FormatType.DateTime;

                //View button
                colViewButton.UnboundType = UnboundColumnType.Object;
                RepositoryItemButtonEdit repoButtonViewFile = new RepositoryItemButtonEdit()
                {
                    Name = "btnViewFile",
                    TextEditStyle = TextEditStyles.HideTextEditor
                };

                repoButtonViewFile.Click += RepoButton_Click;

                colViewButton.ShowButtonMode = ShowButtonModeEnum.ShowAlways;
                colViewButton.ColumnEdit = repoButtonViewFile;

                //Only allow edit the button to allow click
                colFileName.OptionsColumn.AllowEdit = false;
                colUser.OptionsColumn.AllowEdit = false;
                colCreateDate.OptionsColumn.AllowEdit = false;

                //Add columns to grid root view
                gridViewFiles.Columns.Add(colFileName);
                gridViewFiles.Columns.Add(colUser);
                gridViewFiles.Columns.Add(colCreateDate);
                gridViewFiles.Columns.Add(colViewButton);

            }
            catch
            {
                throw;
            }
        }

        private void SetupPanelControl()
        {
            try
            {
                var x = pcFilter.LookAndFeel;
                x.UseDefaultLookAndFeel = false;
                pcFilter.BackColor = AppStyles.BackColorAlternative;

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
                        xtpFiles.PageVisible = false;
                        break;

                    case ActionsStates.Edit:
                        sbFinishPK.Visible = true;
                        sbSearch.Visible = false;
                        lbltxtStatus.Visible = true;
                        xtpFiles.PageVisible = true;
                        break;

                    default:
                        sbFinishPK.Visible = false;
                        sbSearch.Visible = true;
                        lbltxtStatus.Visible = false;
                        xtpFiles.PageVisible = false;
                        break;
                }
            }
            catch
            {
                throw;
            }
        }

        private void ResetForm(bool resetPkNumber = true, bool resetSupplier = true)
        {
            try
            {
                /********* Head *********/
                if (resetPkNumber) txtPKNumber.EditValue = null;
                lbltxtStatus.Text = string.Empty;
                dateEditPKDocDate.EditValue = null;
                dateEditPKDelivery.EditValue = null;
                lblPKDocDateWeek.Text = string.Empty;
                lblPKDeliveryWeek.Text = string.Empty;
                if (resetSupplier) slueSupplier.EditValue = null;
                slueDeliveryTerms.EditValue = null;
                slueCurrency.EditValue = null;
                txtSupplierReference.EditValue = null;
                memoEditRemarks.EditValue = null;

                /********* Terms Tab *********/
                lblTxtCompany.Text = string.Empty;
                lblTxtAddress.Text = string.Empty;
                lblTxtContact.Text = string.Empty;
                lblTxtShipTo.Text = string.Empty;
                lblTxtInvoiceTo.Text = string.Empty;
                sluePaymentTerm.EditValue = null;

                /********* Attached files Tab *********/
                txtPathNewFile.Text = string.Empty;
                xtpFiles.PageVisible = false;
            }
            catch
            {
                throw;
            }
        }

        private void SetSupplierInfo()
        {
            try
            {
                var supplier = _suppliersList.Where(a => a.IdSupplier.Equals(slueSupplier.EditValue.ToString())).FirstOrDefault();

                if (supplier != null)
                {
                    lblTxtCompany.Text = supplier.SupplierName;
                    lblTxtAddress.Text = supplier.ShippingAddress;
                    lblTxtContact.Text = $"{supplier.ContactName} ({supplier.ContactPhone})";
                    sluePaymentTerm.EditValue = supplier.IdPaymentTerms;
                    slueCurrency.EditValue = supplier.IdDefaultCurrency;
                }
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

                if (string.IsNullOrEmpty(txtSupplierReference.Text))
                {
                    MessageBox.Show("Field Required: Manual Reference", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSupplierReference.Focus();
                    return false;
                }

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
                slueSupplier.ReadOnly = false;

                string idPk = _docHeadPK?.IdDoc;
                ResetPK();
                ResetForm();
                //txtPKNumber.Text = string.Empty;
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

        #endregion

        #region Configure Ribbon Actions

        private void ConfigureActionsStackViewCreating()
        {
            try
            {
                _isCreatingPacking = true;

                txtPKNumber.ReadOnly = true;
                slueSupplier.ReadOnly = true;
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
            finally
            {
                _isCreatingPacking = false;
            }
        }

        private void ConfigureActionsStackViewEditing()
        {
            try
            {
                txtPKNumber.ReadOnly = true;
                slueSupplier.ReadOnly = true;

                SetObjectsEnableToEdit();

                //cargamos las PO abiertas de ese supplier aparte de las que incluye el packing a editar
                var soDocsSupplier = GlobalSetting.SupplyDocsService.GetDocs(
                    idSupplier: slueSupplier.EditValue as string,
                    idCustomer: null,
                    docDate: new DateTime(1, 1, 1),  //filtrar sin fecha
                    IdSupplyDocType: Constants.SUPPLY_DOCTYPE_PO,
                    idSupplyStatus: Constants.SUPPLY_STATUS_OPEN);

                if (soDocsSupplier.Count != 0)
                {
                    //Con el union hace el merge y descarta duplicados
                    _docPoSelectionList = new BindingList<DocHead>(_docPoSelectionList.Union(soDocsSupplier).ToList());
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

        private void SearchSuppliersPOs()
        {
            try
            {
                ResetSupplierPOs();

                string supplier = slueSupplier.EditValue as string;

                var poDocsSupplier = GlobalSetting.SupplyDocsService.GetDocs(
                    idSupplier: supplier,
                    idCustomer: null,
                    docDate: new DateTime(1, 1, 1),  //filtrar sin fecha
                    IdSupplyDocType: Constants.SUPPLY_DOCTYPE_PO,
                    idSupplyStatus: Constants.SUPPLY_STATUS_OPEN);

                if (poDocsSupplier.Count == 0)
                {
                    XtraMessageBox.Show("No Supplier's Purchase Orders open found", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _docPoSelectionList = new BindingList<DocHead>(poDocsSupplier);

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
                    idSupplier: (string)slueSupplier.EditValue,
                    idCustomer: Constants.ETNIA_HK_COMPANY_CODE,
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

                string pkNumber = GlobalSetting.SupplyDocsService.GetPackingListNumber((string)slueSupplier.EditValue, DateTime.Now);

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
                    DocDate = dateEditPKDocDate.DateTime,
                    IdSupplyStatus = Constants.SUPPLY_STATUS_OPEN,
                    IdSupplier = slueSupplier.EditValue as string,
                    IdCustomer = Constants.ETNIA_HK_COMPANY_CODE,
                    IdDeliveryTerm = slueDeliveryTerms.EditValue as string,
                    IdPaymentTerms = sluePaymentTerm.EditValue as string,
                    IdCurrency = slueCurrency.EditValue as string,
                    ManualReference = txtSupplierReference.EditValue as string,
                    Remarks = memoEditRemarks.EditValue as string,
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
                    ManualReference = txtSupplierReference.EditValue as string,
                    Remarks = memoEditRemarks.EditValue as string,
                    Lines = sortedLines
                };

                DocHead updatedDoc = GlobalSetting.SupplyDocsService.UpdateDocSupplyMaterials(packingList, finishDoc: finishPK);

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
                txtPKNumber.ReadOnly = false;
                SetObjectsReadOnly();

                //Clear grids
                xgrdPoSelection.DataSource = null;
                xgrdLinesPoSelection.DataSource = null;
                xgrdLinesDeliveredGoods.DataSource = null;
                xgrdFiles.DataSource = null;
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

        private void AttachFile()
        {
            try
            {
                string fileName = Path.GetFileName(txtPathNewFile.Text);
                string fileNameNoExtension = Path.GetFileNameWithoutExtension(txtPathNewFile.Text);
                string extension = Path.GetExtension(txtPathNewFile.Text);

                string attachedFilename = $"{fileNameNoExtension}_{DateTime.Now.ToString("yyyyMMddHHmmss")}{extension}";

                //Creamos los directorios si no existen
                new FileInfo(Constants.SUPPLY_ATTACH_FILES_PATH + _docHeadPK.IdDoc + "\\").Directory.Create();

                DocHeadAttachFile attachFile = new DocHeadAttachFile()
                {
                    IdDocHead = _docHeadPK.IdDoc,
                    FileName = fileName,
                    FileExtension = extension,
                    FilePath = _docHeadPK.IdDoc + "\\" + attachedFilename,
                    User = GlobalSetting.LoggedUser.UserLogin,
                    CreateDate = DateTime.Now
                };

                //move to file server
                File.Copy(txtPathNewFile.Text, Constants.SUPPLY_ATTACH_FILES_PATH + attachFile.FilePath, overwrite: true);

                //Save to database
                GlobalSetting.DocHeadAttachFileService.AddDocHeadAttachFile(attachFile);

                MessageBox.Show(GlobalSetting.ResManager.GetString("SaveSuccessfully"));

                txtPathNewFile.Text = string.Empty;

                //Reaload grid
                _docHeadAttachFileList = new BindingList<DocHeadAttachFile>(GlobalSetting.DocHeadAttachFileService.GetDocHeadAttachFile(_docHeadPK.IdDoc));
                xgrdFiles.DataSource = null;
                xgrdFiles.DataSource = _docHeadAttachFileList;

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
