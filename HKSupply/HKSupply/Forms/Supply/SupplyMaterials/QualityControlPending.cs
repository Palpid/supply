using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
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

namespace HKSupply.Forms.Supply.SupplyMaterials
{
    public partial class QualityControlPending : RibbonFormBase
    {
        #region Constants
        private const string COL_ACTIONS = "Actions";
        #endregion

        #region Private Classes
        private class AuxItemAction
        {
            public string IdDocRelated { get; set; }
            public string IdItem { get; set; }
            public int Action { get; set; }
        }
        #endregion

        #region Private Members
        Font _labelDefaultFontBold = new Font("SourceSansProRegular", 8, FontStyle.Bold);
        Font _labelDefaultFont = new Font("SourceSansProRegular", 8, FontStyle.Regular);

        List<SupplyStatus> _supplyStatusList;
        List<Supplier> _suppliersList;

        DocHead _docHeadQCP;
        BindingList<DocLine> _docLinesList;
        BindingList<PackingListItemBatch> _auxItemsBatchList = new BindingList<PackingListItemBatch>();
        List<PackingListItemBatch> _itemsBatchList = new List<PackingListItemBatch>();

        List<AuxItemAction> _auxItemActionList = new List<AuxItemAction>();

        bool _isLoadingQCPending = false;

        //Dictionary<int, string> _actionsDic = new Dictionary<int, string>(); //(actionCode, Action Desciption)
        #endregion

        #region Enums
        private enum eActionCodes
        {
            Accept,
            Return,
        }

        private enum eActionDescriptions
        {
            Accept,
            Return,
        }

        #endregion

        #region Constructor
        public QualityControlPending()
        {
            InitializeComponent();

            try
            {
                Cursor = Cursors.WaitCursor;

                ConfigureRibbonActions();
                LoadAuxList();
                SetUpLabels();
                SetObjectsReadOnly();
                SetUpSearchLookUpEdit();
                SetUpEvents();
                SetUpGrdLinesQCP();
                SetupGrdItemsBatch();
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
                EnableExportExcel = false;
                EnableExportCsv = false;
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
                if (_docHeadQCP == null)
                {
                    MessageBox.Show(GlobalSetting.ResManager.GetString("NoDataSelected"));
                    RestoreInitState();
                }
                else if (_docHeadQCP.IdSupplyStatus != Constants.SUPPLY_STATUS_OPEN)
                {
                    MessageBox.Show("Only OPEN Quality Control Pending documents can be edited.");
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

        public override void bbiSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            base.bbiSave_ItemClick(sender, e);
        }

        public override void bbiExportCsv_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewLinesQCP.DataRowCount == 0)
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
                    gridViewLinesQCP.OptionsPrint.PrintFooter = false;
                    gridViewLinesQCP.ExportToCsv(ExportCsvFile);

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
            if (gridViewLinesQCP.DataRowCount == 0)
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
                    gridViewLinesQCP.OptionsPrint.PrintFooter = false;
                    gridViewLinesQCP.ExportToXlsx(ExportExcelFile);

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

        private void QualityControlPending_Load(object sender, EventArgs e)
        {

        }

        private void SbSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtQCPNumber.Text) == false || string.IsNullOrEmpty(txtSupplierReference.Text) == false)
                {
                    SearchQCP();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SbFinishQCP_Click(object sender, EventArgs e)
        {
            try
            {
                bool res = false;

                if (ValidateQCP() == false)
                    return;

                DialogResult result = MessageBox.Show(GlobalSetting.ResManager.GetString("SaveChanges"), "", MessageBoxButtons.YesNo);

                if (result != DialogResult.Yes)
                    return;

                Cursor = Cursors.WaitCursor;

                if (CurrentState == ActionsStates.Edit)
                {
                    res = UpdateQCP(finishQCP: true);
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

        private void DateEditQCPDocDate_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                lblQCPDocDateWeek.Text = dateEditQCPDocDate.DateTime.GetWeek().ToString();

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DateEditQCPDelivery_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                lblQCPDeliveryWeek.Text = dateEditQCPDelivery.DateTime.GetWeek().ToString();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtQCPNumber_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (CurrentState == ActionsStates.Edit || CurrentState == ActionsStates.New)
                    return;

                if (e.KeyCode == Keys.Enter && string.IsNullOrEmpty(txtQCPNumber.Text) == false)
                {
                    SearchQCP();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtQCPNumber_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (_isLoadingQCPending)
                    return;

                ResetQCP();
                ResetForm(resetQcpNumber: false);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtSupplierReference_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (CurrentState == ActionsStates.Edit || CurrentState == ActionsStates.New)
                    return;

                if (e.KeyCode == Keys.Enter && string.IsNullOrEmpty(txtSupplierReference.Text) == false)
                {
                    SearchQCP();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtSupplierReference_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (_isLoadingQCPending)
                    return;

                ResetQCP();
                ResetForm(resetSupplierReference: false);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Grid Events

        private void GridViewLinesQCP_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                DocLine linePL = view.GetRow(view.FocusedRowHandle) as DocLine;

                if (linePL == null || _auxItemsBatchList == null)
                    return;

                //Buscamos los lotes referentes a ese item
                _auxItemsBatchList = new BindingList<PackingListItemBatch>(
                    _itemsBatchList
                    .Where(a => a.IdItemBcn.Equals(linePL.IdItemBcn) && a.IdItemGroup.Equals(linePL.IdItemGroup))
                    .ToList());

                xgrdItemsBatch.DataSource = null;
                xgrdItemsBatch.DataSource = _auxItemsBatchList;

                if (CurrentState == ActionsStates.Edit)
                {
                    _auxItemsBatchList.Add(
                        new PackingListItemBatch()
                        {
                            IdDoc = linePL.IdDoc,
                            IdDocRelated = linePL.IdDocRelated,
                            IdItemBcn = linePL.IdItemBcn,
                            IdItemGroup = linePL.IdItemGroup
                        });
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private void GridViewLinesQCP_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        //{
        //    try
        //    {
        //        DocLine docLine = e.Row as DocLine;

        //        if (docLine == null)
        //            return;

        //        switch (e.Column.FieldName)
        //        {
        //            case COL_ACTIONS:
        //                var action = _auxItemActionList?
        //                    .Where(a => a.IdDocRelated.Equals(docLine.IdDocRelated) && a.IdItem.Equals(docLine.IdItemBcn)).FirstOrDefault();

        //                if (action != null)
        //                {
        //                    e.Value = action.Action;
        //                }

        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        private void GridViewLinesQCP_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                DocLine docLine = view.GetRow(view.FocusedRowHandle) as DocLine;

                if (docLine == null)
                    return;

                switch (e.Column.FieldName)
                {
                    case COL_ACTIONS:

                        int actionCode = Convert.ToInt32(view.ActiveEditor.EditValue);

                        var action = _auxItemActionList
                            .Where(a => a.IdDocRelated.Equals(docLine.IdDocRelated) && a.IdItem.Equals(docLine.IdItemBcn))
                            .FirstOrDefault();

                        if (action == null)
                        {

                            _auxItemActionList.Add(
                                new AuxItemAction()
                                {
                                    IdDocRelated = docLine.IdDocRelated,
                                    IdItem = docLine.IdItemBcn,
                                    Action = actionCode
                                });
                        }
                        else
                        {
                            action.Action = actionCode;
                        }

                        break;
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void XgrdItemsBatch_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                GridView view = xgrdItemsBatch.FocusedView as GridView;
                PackingListItemBatch itemBatch = view.GetRow(view.FocusedRowHandle) as PackingListItemBatch;

                if (itemBatch == null)
                    return;

                if (e.KeyCode == Keys.F4)
                {
                    DialogResult result = XtraMessageBox.Show("Delete row?", "Confirmation", MessageBoxButtons.YesNo);
                    if (result != DialogResult.Yes)
                        return;

                    var batchTmp = _itemsBatchList.Where(a => a.IdItemBcn.Equals(itemBatch.IdItemBcn) && a.Batch.Equals(itemBatch.Batch)).FirstOrDefault();

                    _auxItemsBatchList.Remove(itemBatch);
                    _itemsBatchList.Remove(batchTmp);
                }

                if (e.KeyCode == Keys.Enter)
                {
                    if (view.FocusedRowHandle == view.RowCount - 1)
                    {
                        if (String.IsNullOrEmpty(itemBatch.Batch) == false && itemBatch.Quantity > 0)
                        {
                            _auxItemsBatchList.Add(
                                new PackingListItemBatch()
                                {
                                    IdDoc = itemBatch.IdDoc,
                                    IdDocRelated = itemBatch.IdDocRelated,
                                    IdItemBcn = itemBatch.IdItemBcn,
                                    IdItemGroup = itemBatch.IdItemGroup
                                });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GridViewItemsBatch_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            try
            {
                switch (e.Column.FieldName)
                {
                    case nameof(PackingListItemBatch.Batch):
                        GridView view = sender as GridView;
                        PackingListItemBatch itemBatch = view.GetRow(view.FocusedRowHandle) as PackingListItemBatch;

                        if (itemBatch == null)
                            return;
                        CopyToItemBatchList(itemBatch);
                        break;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #endregion

        #region Private Members

        #region Loads/Resets

        private void LoadAuxList()
        {
            try
            {
                _suppliersList = GlobalSetting.SupplierService.GetSuppliers().Where(a => a.Factory == false).ToList();
                _supplyStatusList = GlobalSetting.SupplyDocsService.GetSupplyStatus();

                //_actionsDic.Add((int)eActionCodes.Accept, eActionDescriptions.Accept.ToString());
                //_actionsDic.Add((int)eActionCodes.Return, eActionDescriptions.Return.ToString());
            }
            catch
            {
                throw;
            }
        }

        private void SearchQCP()
        {
            try
            {
                _isLoadingQCPending = true;
                ResetQCP();

                if (string.IsNullOrEmpty(txtQCPNumber.Text) == false)
                {
                    ResetForm(resetQcpNumber: false);
                    string pkNumber = txtQCPNumber.Text;
                    _docHeadQCP = GlobalSetting.SupplyDocsService.GetDocPackingList(pkNumber);
                }
                else if (string.IsNullOrEmpty(txtSupplierReference.Text) == false)
                {
                    ResetForm(resetSupplierReference: false);
                    string supplierReference = txtSupplierReference.Text;
                    var docs = GlobalSetting.SupplyDocsService.GetDocsByReference(supplierReference);
                    if (docs.Count == 1)
                        _docHeadQCP = docs.FirstOrDefault();
                    else if (docs.Count > 1)
                    {
                        using (DialogForms.SelectDocs form = new DialogForms.SelectDocs())
                        {
                            form.InitData(docs);
                            if (form.ShowDialog() == DialogResult.OK)
                            {
                                _docHeadQCP = form.SelectedDoc;
                            }
                        }
                    }
                }


                if (_docHeadQCP == null)
                {
                    XtraMessageBox.Show("No Data Found", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (_docHeadQCP.IdSupplyDocType != Constants.SUPPLY_DOCTYPE_QCP)
                {
                    XtraMessageBox.Show("Document is not a Quality Control Pending", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (_docHeadQCP.IdCustomer != Constants.ETNIA_HK_COMPANY_CODE)
                {
                    XtraMessageBox.Show("Packing List is not from material supplier", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    LoadQCP();
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                _isLoadingQCPending = false;
            }
        }

        private void LoadQCP()
        {
            try
            {
                var supplier = _suppliersList.Where(a => a.IdSupplier.Equals(_docHeadQCP.IdSupplier)).FirstOrDefault();

                //***** Header *****/
                txtQCPNumber.Text = _docHeadQCP.IdDoc;
                lbltxtStatus.Visible = true;
                lbltxtStatus.Text = _docHeadQCP.IdSupplyStatus;
                slueSupplier.EditValue = _docHeadQCP.IdSupplier;

                dateEditQCPDocDate.EditValue = _docHeadQCP.DocDate;
                dateEditQCPDelivery.EditValue = _docHeadQCP.DeliveryDate;

                lblQCPDocDateWeek.Text = dateEditQCPDocDate.DateTime.GetWeek().ToString();
                lblQCPDeliveryWeek.Text = dateEditQCPDelivery.DateTime.GetWeek().ToString();

                txtSupplierReference.Text = _docHeadQCP.ManualReference;
                memoEditRemarks.Text = _docHeadQCP.Remarks;

                //***** Grid Batches *****/
                _auxItemsBatchList = new BindingList<PackingListItemBatch>();
                _itemsBatchList = _docHeadQCP.PackingListItemBatches;
                //Remarks: Hacemos antes la carga de los lotes y después cargamos el grid de líneas, al cargar lanzará el evento de cambio de foco en la fila
                //y cargará los datos que corresponden a esa fila en el grid de lotes

                //***** GridLines *****/
                _docLinesList = new BindingList<DocLine>(_docHeadQCP.Lines);
                xgrdLinesQCP.DataSource = null;
                xgrdLinesQCP.DataSource = _docLinesList;

                //Initialize actions list
                _auxItemActionList = new List<AuxItemAction>();

            }
            catch
            {
                throw;
            }
        }

        private void ResetQCP()
        {
            try
            {
                _docLinesList = null;
                _auxItemsBatchList = null;
                _itemsBatchList = null;
                _docHeadQCP = null;
                _auxItemActionList = null;
                xgrdLinesQCP.DataSource = null;

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
                lblQCPNumber.Font = _labelDefaultFontBold;
                lbltxtStatus.Font = _labelDefaultFontBold;
                lblDate.Font = _labelDefaultFontBold;
                lblWeek.Font = _labelDefaultFontBold;
                lblPKDocDate.Font = _labelDefaultFontBold;
                lblPKDelivery.Font = _labelDefaultFontBold;
                lblSupplier.Font = _labelDefaultFontBold;
                lblQCPDocDateWeek.Font = _labelDefaultFont;
                lblQCPDeliveryWeek.Font = _labelDefaultFont;
                txtQCPNumber.Font = _labelDefaultFontBold;
                lblSupplierReference.Font = _labelDefaultFontBold;
                lblRemarks.Font = _labelDefaultFont;

                /********* Texts **********/
                //Headers
                lblQCPNumber.Text = "QCP Number";
                lbltxtStatus.Text = string.Empty;
                lblDate.Text = "Date";
                lblWeek.Text = "Week";
                lblPKDocDate.Text = "DATE";
                lblPKDelivery.Text = "DELIVERY";
                lblSupplier.Text = "SUPPLIER";
                lblQCPDocDateWeek.Text = string.Empty;
                lblQCPDeliveryWeek.Text = string.Empty;
                lblSupplierReference.Text = "Supplier Reference";
                lblRemarks.Text = "Remarks";

                /********* Align **********/
                //Headers
                lbltxtStatus.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                lblQCPDocDateWeek.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                lblQCPDeliveryWeek.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                txtQCPNumber.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;

                /********* BackColor **********/
                txtQCPNumber.Properties.Appearance.BackColor = Color.CadetBlue;
                txtQCPNumber.Properties.Appearance.BackColor2 = Color.CadetBlue;

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
                dateEditQCPDocDate.ReadOnly = true;
                dateEditQCPDelivery.ReadOnly = true;
                memoEditRemarks.ReadOnly = true;
                slueSupplier.ReadOnly = true;
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
                slueSupplier.Properties.NullText = string.Empty;
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
                sbFinishQCP.Click += SbFinishQCP_Click;
                dateEditQCPDocDate.EditValueChanged += DateEditQCPDocDate_EditValueChanged;
                dateEditQCPDelivery.EditValueChanged += DateEditQCPDelivery_EditValueChanged;
                txtQCPNumber.KeyDown += TxtQCPNumber_KeyDown;
                txtQCPNumber.EditValueChanged += TxtQCPNumber_EditValueChanged;
                txtSupplierReference.KeyDown += TxtSupplierReference_KeyDown;
                txtSupplierReference.EditValueChanged += TxtSupplierReference_EditValueChanged;
            }
            catch
            {
                throw;
            }
        }

        private void SetUpGrdLinesQCP()
        {
            try
            {
                //Activar que se alternen los colores de las filas del grid
                gridViewLinesQCP.OptionsView.EnableAppearanceOddRow = true;
                gridViewLinesQCP.OptionsView.EnableAppearanceEvenRow = true;

                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                gridViewLinesQCP.OptionsView.ColumnAutoWidth = false;
                gridViewLinesQCP.HorzScrollVisibility = ScrollVisibility.Auto;

                //Hacer todo el grid no editable
                gridViewLinesQCP.OptionsBehavior.Editable = false;

                //Hide grouping panel
                gridViewLinesQCP.OptionsView.ShowGroupPanel = false;

                //Column Definition
                GridColumn colIdDocRelated = new GridColumn() { Caption = "Purchase Order", Visible = true, FieldName = nameof(DocLine.IdDocRelated), Width = 130 };
                GridColumn colIdItemBcn = new GridColumn() { Caption = "Item Code", Visible = true, FieldName = nameof(DocLine.IdItemBcn), Width = 200 };
                GridColumn colDescription = new GridColumn() { Caption = "Description", Visible = true, FieldName = nameof(DocLine.ItemDesc), Width = 350 };
                GridColumn colIdItemGroup = new GridColumn() { Caption = "Type", Visible = true, FieldName = nameof(DocLine.IdItemGroup), Width = 50 };
                GridColumn colQuantityOriginal = new GridColumn() { Caption = "Original Qty", Visible = true, FieldName = nameof(DocLine.QuantityOriginal), Width = 110 };
                GridColumn colQuantity = new GridColumn() { Caption = "Accepted Qty", Visible = true, FieldName = nameof(DocLine.Quantity), Width = 110 };
                GridColumn colRejectedQuantity = new GridColumn() { Caption = "Rejected Qty", Visible = true, FieldName = nameof(DocLine.RejectedQuantity), Width = 110 };
                GridColumn colUnit = new GridColumn() { Caption = "Unit", Visible = true, FieldName = nameof(DocLine.ItemUnit), Width = 85 };
                GridColumn colIdIdSupplyStatus = new GridColumn() { Caption = "Status", Visible = true, FieldName = nameof(DocLine.IdSupplyStatus), Width = 75 };
                GridColumn colRemarks = new GridColumn() { Caption = "Remarks", Visible = true, FieldName = nameof(DocLine.Remarks), Width = 300 };
                //unbound columns
                //GridColumn colActions = new GridColumn() { Caption = "Actions", Visible = true, FieldName = COL_ACTIONS, Width = 60 };

                //Display Format
                colQuantity.DisplayFormat.FormatType = FormatType.Numeric;
                colQuantity.DisplayFormat.FormatString = "n3";

                colQuantityOriginal.DisplayFormat.FormatType = FormatType.Numeric;
                colQuantityOriginal.DisplayFormat.FormatString = "n3";

                colRejectedQuantity.DisplayFormat.FormatType = FormatType.Numeric;
                colRejectedQuantity.DisplayFormat.FormatString = "n3";

                //Unbound columns
                //colActions.UnboundType = DevExpress.Data.UnboundColumnType.String;

                //Edit Repositories
                RepositoryItemTextEdit ritxt3Dec = new RepositoryItemTextEdit();
                ritxt3Dec.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                ritxt3Dec.Mask.EditMask = "F3";
                ritxt3Dec.AllowNullInput = DefaultBoolean.True;

                colQuantityOriginal.ColumnEdit = ritxt3Dec;
                colRejectedQuantity.ColumnEdit = ritxt3Dec;

                RepositoryItemSearchLookUpEdit riSupplyStatus = new RepositoryItemSearchLookUpEdit()
                {
                    DataSource = _supplyStatusList,
                    ValueMember = nameof(SupplyStatus.IdSupplyStatus),
                    DisplayMember = nameof(SupplyStatus.Description),
                    ShowClearButton = false,
                    NullText = string.Empty,
                };
                colIdIdSupplyStatus.ColumnEdit = riSupplyStatus;

                //RepositoryItemLookUpEdit riActions = new RepositoryItemLookUpEdit()
                //{
                //    DataSource = _actionsDic,
                //    NullText = string.Empty,
                //};
                //colActions.ColumnEdit = riActions;

                //Add columns to grid root view
                gridViewLinesQCP.Columns.Add(colIdDocRelated);
                gridViewLinesQCP.Columns.Add(colIdItemBcn);
                gridViewLinesQCP.Columns.Add(colDescription);
                gridViewLinesQCP.Columns.Add(colIdItemGroup);
                gridViewLinesQCP.Columns.Add(colQuantityOriginal);
                gridViewLinesQCP.Columns.Add(colQuantity);
                gridViewLinesQCP.Columns.Add(colRejectedQuantity);
                gridViewLinesQCP.Columns.Add(colUnit);
                gridViewLinesQCP.Columns.Add(colIdIdSupplyStatus);
                //gridViewLinesQCP.Columns.Add(colActions);
                gridViewLinesQCP.Columns.Add(colRemarks);

                //Events
                gridViewLinesQCP.FocusedRowChanged += GridViewLinesQCP_FocusedRowChanged;
                //gridViewLinesQCP.CustomUnboundColumnData += GridViewLinesQCP_CustomUnboundColumnData;
                gridViewLinesQCP.CellValueChanged += GridViewLinesQCP_CellValueChanged;

            }
            catch
            {
                throw;
            }
        }

        private void SetupGrdItemsBatch()
        {
            try
            {
                //Activar que se alternen los colores de las filas del grid
                gridViewItemsBatch.OptionsView.EnableAppearanceEvenRow = true;
                gridViewItemsBatch.OptionsView.EnableAppearanceOddRow = true;

                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                gridViewItemsBatch.OptionsView.ColumnAutoWidth = false;
                gridViewItemsBatch.HorzScrollVisibility = ScrollVisibility.Auto;

                //Hacer todo el grid no editable
                gridViewItemsBatch.OptionsBehavior.Editable = false;

                //Hide grouping panel
                gridViewItemsBatch.OptionsView.ShowGroupPanel = false;

                //Column Definition
                GridColumn colIdItemBcn = new GridColumn() { Caption = "Item Code", Visible = true, FieldName = nameof(PackingListItemBatch.IdItemBcn), Width = 200 };
                GridColumn colBatch = new GridColumn() { Caption = "Batch", Visible = true, FieldName = nameof(PackingListItemBatch.Batch), Width = 200 };
                GridColumn colQuantity = new GridColumn() { Caption = "Quantity", Visible = true, FieldName = nameof(PackingListItemBatch.Quantity), Width = 85 };

                //Display Format
                colQuantity.DisplayFormat.FormatType = FormatType.Numeric;
                colQuantity.DisplayFormat.FormatString = "n3";

                //Edit Repositories
                RepositoryItemTextEdit ritxt3Dec = new RepositoryItemTextEdit();
                ritxt3Dec.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                ritxt3Dec.Mask.EditMask = "F3";
                ritxt3Dec.AllowNullInput = DefaultBoolean.True;

                colQuantity.ColumnEdit = ritxt3Dec;

                //Adding columns
                gridViewItemsBatch.Columns.Add(colIdItemBcn);
                gridViewItemsBatch.Columns.Add(colBatch);
                gridViewItemsBatch.Columns.Add(colQuantity);

                //Disable sorting
                foreach (GridColumn column in gridViewItemsBatch.Columns)
                    column.OptionsColumn.AllowSort = DefaultBoolean.False;

                //Events
                xgrdItemsBatch.ProcessGridKey += XgrdItemsBatch_ProcessGridKey;
                gridViewItemsBatch.CellValueChanged += GridViewItemsBatch_CellValueChanged;
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

                    case ActionsStates.Edit:
                        sbFinishQCP.Visible = true;
                        sbSearch.Visible = false;
                        lbltxtStatus.Visible = true;
                        break;

                    default:
                        sbFinishQCP.Visible = false;
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

        private void ResetForm(bool resetQcpNumber = true, bool resetSupplier = true, bool resetSupplierReference = true)
        {
            try
            {
                /********* Head *********/
                if (resetQcpNumber) txtQCPNumber.EditValue = null;
                lbltxtStatus.Text = string.Empty;
                dateEditQCPDocDate.EditValue = null;
                dateEditQCPDelivery.EditValue = null;
                lblQCPDocDateWeek.Text = string.Empty;
                lblQCPDeliveryWeek.Text = string.Empty;
                if (resetSupplier) slueSupplier.EditValue = null;
                if (resetSupplierReference) txtSupplierReference.EditValue = null;
                memoEditRemarks.EditValue = null;
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
                txtQCPNumber.ReadOnly = false;
                txtSupplierReference.ReadOnly = false;
                gridViewLinesQCP.OptionsBehavior.Editable = false;

                string idQcp = _docHeadQCP?.IdDoc;
                ResetQCP();
                ResetForm();
                SetObjectsReadOnly();

                if (idQcp != null)
                {
                    txtQCPNumber.Text = idQcp;
                    SearchQCP();
                }
                RestoreInitState();
                SetVisiblePropertyByState();
            }
            catch
            {
                throw;
            }
        }

        private void CopyToItemBatchList(PackingListItemBatch itemBatch)
        {
            try
            {
                var reg = _itemsBatchList
                    .Where(a => a.IdDocRelated.Equals(itemBatch.IdDocRelated) &&
                    a.IdItemBcn.Equals(itemBatch.IdItemBcn) &&
                    a.IdItemGroup.Equals(itemBatch.IdItemGroup) &&
                    a.Batch.Equals(itemBatch.Batch))
                    .FirstOrDefault();

                if (reg == null)
                    _itemsBatchList.Add(itemBatch);
                else
                    reg.Batch = itemBatch.Batch;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Configure Ribbon Actions
        private void ConfigureActionsStackViewEditing()
        {
            try
            {
                txtQCPNumber.ReadOnly = true;
                txtSupplierReference.ReadOnly = true;
                dateEditQCPDelivery.ReadOnly = true;

                gridViewLinesQCP.OptionsBehavior.Editable = true;
                gridViewItemsBatch.OptionsBehavior.Editable = true;

                foreach (GridColumn col in gridViewLinesQCP.Columns)
                {
                    if (col.FieldName != nameof(DocLine.Remarks) &&
                        //col.FieldName != COL_ACTIONS &&
                        col.FieldName != nameof(DocLine.Quantity) &&
                        col.FieldName != nameof(DocLine.RejectedQuantity))
                    {
                        col.OptionsColumn.AllowEdit = false;
                    }
                }

                foreach (GridColumn col in gridViewItemsBatch.Columns)
                {
                    if (col.FieldName == nameof(PackingListItemBatch.IdItemBcn))
                        col.OptionsColumn.AllowEdit = false;
                }

                SetVisiblePropertyByState();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region validates

        private bool ValidateQCP()
        {
            try
            {
                if (ValidateValidatePQCPendingLines() == false)
                    return false;

                if (ValidatePackingItemsBatch() == false)
                    return false;

                return true;
            }
            catch
            {
                throw;
            }
        }

        private bool ValidateValidatePQCPendingLines(bool validateQuantities = false)
        {
            try
            {
                
                foreach (var line in _docLinesList)
                {
                    if (string.IsNullOrEmpty(line.Remarks))
                    {
                        MessageBox.Show($"You must indicate remarks for {line.IdItemBcn} ({line.IdDocRelated}) if Rejected Quantity is greater than 0", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    if (validateQuantities && line.RejectedQuantity == 0 && line.Quantity == 0)
                    {
                        MessageBox.Show($"You must indicate Accepted Quantity and/or Rejected Quantity {Environment.NewLine}for {line.IdItemBcn} ({line.IdDocRelated})", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
                
                return true;
            }
            catch
            {
                throw;
            }
        }

        private bool ValidatePackingItemsBatch()
        {
            try
            {
                //validamos que no haya batch duplicados
                var tmpBatches = _itemsBatchList.Select(a => a.Batch).ToList();
                var duplicates = tmpBatches.GroupBy(batch => batch).SelectMany(grp => grp.Skip(1)).ToList();
                if (duplicates.Count > 0)
                {
                    string errorMsg = "Duplicared batch/es:" + Environment.NewLine;
                    foreach (var b in duplicates)
                        errorMsg += b + Environment.NewLine;

                    MessageBox.Show(errorMsg, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }


                foreach (var itemBatch in _itemsBatchList)
                {
                    if (string.IsNullOrEmpty(itemBatch.Batch))
                    {
                        MessageBox.Show($"You must indicate a bath for {itemBatch.IdItemBcn} ({itemBatch.IdDocRelated})", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        xgrdItemsBatch.Focus();
                        return false;
                    }
                }

                foreach (var line in _docLinesList)
                {
                    decimal qtyItemBatch = _itemsBatchList
                        .Where(a => a.IdDocRelated.Equals(line.IdDocRelated) && a.IdItemBcn.Equals(line.IdItemBcn) && a.IdItemGroup.Equals(line.IdItemGroup))
                        .Sum(b => b.Quantity);
                    if (line.Quantity != qtyItemBatch)
                    {
                        MessageBox.Show($"{line.IdItemBcn} ({line.IdDocRelated}): Batch quantity error", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
                
                return true;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region CRUD

        private bool UpdateQCP(bool finishQCP = false)
        {
            try
            {
                
                List<DocLine> sortedLines = _docLinesList.ToList();

                //Si se finaliza cerramos todos las líneas también
                if (finishQCP)
                {
                    //The ToList is needed in order to evaluate the select immediately due to lazy evaluation.
                    sortedLines.Select(a => { a.IdSupplyStatus = Constants.SUPPLY_STATUS_CLOSE; return a; }).ToList();
                }

                DocHead packingList = new DocHead()
                {

                    IdDoc = _docHeadQCP.IdDoc,
                    IdDocRelated = _docHeadQCP.IdDocRelated,
                    IdSupplyDocType = _docHeadQCP.IdSupplyDocType,
                    CreationDate = _docHeadQCP.CreationDate,
                    DeliveryDate = _docHeadQCP.DeliveryDate,
                    DocDate = _docHeadQCP.DocDate,
                    IdSupplyStatus = (finishQCP ? Constants.SUPPLY_STATUS_CLOSE : _docHeadQCP.IdSupplyStatus),
                    IdSupplier = _docHeadQCP.IdSupplier,
                    IdCustomer = _docHeadQCP.IdCustomer,
                    IdDeliveryTerm = _docHeadQCP.IdDeliveryTerm,
                    IdPaymentTerms = _docHeadQCP.IdPaymentTerms,
                    IdCurrency = _docHeadQCP.IdCurrency,
                    ManualReference = _docHeadQCP.ManualReference,
                    Remarks = _docHeadQCP.Remarks,
                    Lines = sortedLines,
                    Boxes = _docHeadQCP.Boxes,
                    PackingListItemBatches = _itemsBatchList
                };

                DocHead updatedDoc = GlobalSetting.SupplyDocsService.UpdateDocSupplyMaterials(packingList, finishDoc: finishQCP);

                _docHeadQCP = updatedDoc;

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
                txtQCPNumber.ReadOnly = false;
                SetObjectsReadOnly();

                //Restore de ribbon to initial states
                RestoreInitState();

                //Clear grids
                xgrdLinesQCP.DataSource = null;
                xgrdItemsBatch.DataSource = null;
                //Reload Packing list
                LoadQCP();
                //not allow grid editing
                gridViewLinesQCP.OptionsBehavior.Editable = false;
                gridViewItemsBatch.OptionsBehavior.Editable = false;

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
