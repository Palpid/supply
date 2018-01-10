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
using HKSupply.Styles;
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
    public partial class ReceiptAndQuality : RibbonFormBase
    {

        #region Private Members

        Font _labelDefaultFontBold = AppStyles.LabelDefaultFontBold;
        Font _labelDefaultFont = AppStyles.LabelDefaultFont;

        List<SupplyStatus> _supplyStatusList;
        List<Supplier> _suppliersList;

        DocHead _docHeadPK;
        BindingList<DocLine> _docLinesList;
        BindingList<PackingListItemBatch> _auxItemsBatchList = new BindingList<PackingListItemBatch>();
        List<PackingListItemBatch> _itemsBatchList = new List<PackingListItemBatch>();

        bool _isLoadingPacking = false;

        #endregion

        #region Constructor
        public ReceiptAndQuality()
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
                SetUpGrdLinesPL();
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

        #region Ribbons
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
                if (_docHeadPK == null)
                {
                    MessageBox.Show(GlobalSetting.ResManager.GetString("NoDataSelected"));
                    RestoreInitState();
                }
                else if (_docHeadPK.IdSupplyStatus != Constants.SUPPLY_STATUS_TRANSIT)
                {
                    MessageBox.Show("Only TRANSIT Packing List can be edited.");
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

        #endregion

        #region Form Events
        private void ReceiptAndQuality_Load(object sender, EventArgs e)
        {
            
        }

        private void SbSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPKNumber.Text) == false || string.IsNullOrEmpty(txtSupplierReference.Text) == false)
                {
                    SearchPK();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SbFinishQC_Click(object sender, EventArgs e)
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
                if (_isLoadingPacking)
                    return;

                ResetPK();
                ResetForm(resetPkNumber: false);
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
                    SearchPK();
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
                if (_isLoadingPacking)
                    return;

                ResetPK();
                ResetForm(resetSupplierReference: false);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Grids Events
        private void GridViewLinesPL_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
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

        #region Private Methods

        #region Load/Resets

        private void LoadAuxList()
        {
            try
            {
                _suppliersList = GlobalSetting.SupplierService.GetSuppliers().Where(a => a.Factory == false).ToList();
                _supplyStatusList = GlobalSetting.SupplyDocsService.GetSupplyStatus();
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

                if (string.IsNullOrEmpty(txtPKNumber.Text) == false)
                {
                    ResetForm(resetPkNumber: false);
                    string pkNumber = txtPKNumber.Text;
                    _docHeadPK = GlobalSetting.SupplyDocsService.GetDocPackingList(pkNumber);
                }
                else if (string.IsNullOrEmpty(txtSupplierReference.Text) == false)
                {
                    ResetForm(resetSupplierReference: false);
                    string supplierReference = txtSupplierReference.Text;
                    var docs = GlobalSetting.SupplyDocsService.GetDocsByReference(supplierReference);
                    if (docs.Count == 1)
                        _docHeadPK = docs.FirstOrDefault();
                    else if (docs.Count > 1)
                    {
                        using (DialogForms.SelectDocs form = new DialogForms.SelectDocs())
                        {
                            form.InitData(docs);
                            if (form.ShowDialog() == DialogResult.OK)
                            {
                                _docHeadPK = form.SelectedDoc;
                            }
                        }
                    }
                }
                

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
                txtPKNumber.Text = _docHeadPK.IdDoc;
                lbltxtStatus.Visible = true;
                lbltxtStatus.Text = _docHeadPK.IdSupplyStatus;
                slueSupplier.EditValue = _docHeadPK.IdSupplier;

                dateEditPKDocDate.EditValue = _docHeadPK.DocDate;
                dateEditPKDelivery.EditValue = _docHeadPK.DeliveryDate;

                lblPKDocDateWeek.Text = dateEditPKDocDate.DateTime.GetWeek().ToString();
                lblPKDeliveryWeek.Text = dateEditPKDelivery.DateTime.GetWeek().ToString();

                txtSupplierReference.Text = _docHeadPK.ManualReference;
                memoEditRemarks.Text = _docHeadPK.Remarks;

                //***** Grid Batches *****/
                _auxItemsBatchList = new BindingList<PackingListItemBatch>();
                _itemsBatchList = _docHeadPK.PackingListItemBatches;
                //Remarks: Hacemos antes la carga de los lotes y después cargamos el grid de líneas, al cargar lanzará el evento de cambio de foco en la fila
                //y cargará los datos que corresponden a esa fila en el grid de lotes

                //***** GridLines *****/
                _docLinesList = new BindingList<DocLine>(_docHeadPK.Lines);
                xgrdLinesPL.DataSource = null;
                xgrdLinesPL.DataSource = _docLinesList;

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
                _docLinesList = null;
                _auxItemsBatchList = null;
                _itemsBatchList = null; 
                _docHeadPK = null;
                xgrdLinesPL.DataSource = null;

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
                lblSupplierReference.Font = _labelDefaultFontBold;
                lblRemarks.Font = _labelDefaultFont;

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
                lblSupplierReference.Text = "Supplier Reference";
                lblRemarks.Text = "Remarks";

                /********* Align **********/
                //Headers
                lbltxtStatus.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                lblPKDocDateWeek.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                lblPKDeliveryWeek.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                txtPKNumber.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                txtSupplierReference.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;

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
                sbFinishQC.Click += SbFinishQC_Click;
                dateEditPKDocDate.EditValueChanged += DateEditPKDocDate_EditValueChanged;
                dateEditPKDelivery.EditValueChanged += DateEditPKDelivery_EditValueChanged;
                txtPKNumber.KeyDown += TxtPKNumber_KeyDown;
                txtPKNumber.EditValueChanged += TxtPKNumber_EditValueChanged;
                txtSupplierReference.KeyDown += TxtSupplierReference_KeyDown;
                txtSupplierReference.EditValueChanged += TxtSupplierReference_EditValueChanged;
            }
            catch
            {
                throw;
            }
        }

        private void SetUpGrdLinesPL()
        {
            try
            {
                //Activar que se alternen los colores de las filas del grid
                gridViewLinesPL.OptionsView.EnableAppearanceOddRow = true;
                gridViewLinesPL.OptionsView.EnableAppearanceEvenRow = true;

                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                gridViewLinesPL.OptionsView.ColumnAutoWidth = false;
                gridViewLinesPL.HorzScrollVisibility = ScrollVisibility.Auto;

                //Hacer todo el grid no editable
                gridViewLinesPL.OptionsBehavior.Editable = false;

                //Hide grouping panel
                gridViewLinesPL.OptionsView.ShowGroupPanel = false;

                //Column Definition
                GridColumn colIdDocRelated = new GridColumn() { Caption = "Purchase Order", Visible = true, FieldName = nameof(DocLine.IdDocRelated), Width = 130 };
                GridColumn colIdItemBcn = new GridColumn() { Caption = "Item Code", Visible = true, FieldName = nameof(DocLine.IdItemBcn), Width = 200 };
                GridColumn colDescription = new GridColumn() { Caption = "Description", Visible = true, FieldName = nameof(DocLine.ItemDesc), Width = 350 };
                GridColumn colIdItemGroup = new GridColumn() { Caption = "Type", Visible = true, FieldName = nameof(DocLine.IdItemGroup), Width = 100 };
                GridColumn colQuantity = new GridColumn() { Caption = "Order Quantity", Visible = true, FieldName = nameof(DocLine.Quantity), Width = 110 };
                GridColumn colDeliveredQuantity = new GridColumn() { Caption = "Delivered Qty", Visible = true, FieldName = nameof(DocLine.DeliveredQuantity), Width = 110 };
                GridColumn colRejectedQuantity = new GridColumn() { Caption = "Rejected Qty", Visible = true, FieldName = nameof(DocLine.RejectedQuantity), Width = 110 };
                GridColumn colUnit = new GridColumn() { Caption = "Unit", Visible = true, FieldName = nameof(DocLine.ItemUnit), Width = 85 };
                GridColumn colIdIdSupplyStatus = new GridColumn() { Caption = "Status", Visible = true, FieldName = nameof(DocLine.IdSupplyStatus), Width = 75 };
                GridColumn colRemarks = new GridColumn() { Caption = "Remarks", Visible = true, FieldName = nameof(DocLine.Remarks), Width = 300 };

                //Display Format
                colQuantity.DisplayFormat.FormatType = FormatType.Numeric;
                colQuantity.DisplayFormat.FormatString = "n3";

                colDeliveredQuantity.DisplayFormat.FormatType = FormatType.Numeric;
                colDeliveredQuantity.DisplayFormat.FormatString = "n3";

                colRejectedQuantity.DisplayFormat.FormatType = FormatType.Numeric;
                colRejectedQuantity.DisplayFormat.FormatString = "n3";

                //Edit Repositories
                RepositoryItemTextEdit ritxt3Dec = new RepositoryItemTextEdit();
                ritxt3Dec.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                ritxt3Dec.Mask.EditMask = "F3";
                ritxt3Dec.AllowNullInput = DefaultBoolean.True;

                colDeliveredQuantity.ColumnEdit = ritxt3Dec;
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

                //Add columns to grid root view
                gridViewLinesPL.Columns.Add(colIdDocRelated);
                gridViewLinesPL.Columns.Add(colIdItemBcn);
                gridViewLinesPL.Columns.Add(colDescription);
                gridViewLinesPL.Columns.Add(colIdItemGroup);
                gridViewLinesPL.Columns.Add(colQuantity);
                gridViewLinesPL.Columns.Add(colDeliveredQuantity);
                gridViewLinesPL.Columns.Add(colRejectedQuantity);
                gridViewLinesPL.Columns.Add(colUnit);
                gridViewLinesPL.Columns.Add(colIdIdSupplyStatus);
                gridViewLinesPL.Columns.Add(colRemarks);

                //Events
                gridViewLinesPL.FocusedRowChanged += GridViewLinesPL_FocusedRowChanged;

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
                        sbFinishQC.Visible = true;
                        sbSearch.Visible = false;
                        lbltxtStatus.Visible = true;
                        break;

                    default:
                        sbFinishQC.Visible = false;
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

        private void ResetForm(bool resetPkNumber = true, bool resetSupplier = true, bool resetSupplierReference = true)
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
                if(resetSupplierReference) txtSupplierReference.EditValue = null;
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
                txtPKNumber.ReadOnly = false;
                txtSupplierReference.ReadOnly = false;
                gridViewLinesPL.OptionsBehavior.Editable = false;

                string idPk = _docHeadPK?.IdDoc;
                ResetPK();
                ResetForm();
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

        #region validates
        private bool ValidatePK()
        {
            try
            {
                if (ValidateValidatePackingLines() == false)
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

        private bool ValidateValidatePackingLines(bool validateQuantities = false)
        {
            try
            {

                foreach (var line in _docLinesList)
                {
                    if (line.RejectedQuantity > 0 && string.IsNullOrEmpty(line.Remarks))
                    {
                        MessageBox.Show($"You must indicate remarks for {line.IdItemBcn} ({line.IdDocRelated}) if Rejected Quantity is greater than 0", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    if (validateQuantities && line.RejectedQuantity == 0 && line.DeliveredQuantity == 0)
                    {
                        MessageBox.Show($"You must indicate Delivered Quantity and/or Rejected Quantity {Environment.NewLine}for {line.IdItemBcn} ({line.IdDocRelated})", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    if (line.DeliveredQuantity != qtyItemBatch)
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

        #region Configure Ribbon Actions

        private void ConfigureActionsStackViewEditing()
        {
            try
            {
                txtPKNumber.ReadOnly = true;
                txtSupplierReference.ReadOnly = true;
                dateEditPKDelivery.ReadOnly = false;

                gridViewLinesPL.OptionsBehavior.Editable = true;
                gridViewItemsBatch.OptionsBehavior.Editable = true;

                foreach (GridColumn col in gridViewLinesPL.Columns)
                {
                    if (col.FieldName != nameof(DocLine.Remarks) &&
                        col.FieldName != nameof(DocLine.DeliveredQuantity) &&
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


        #region CRUD

        private bool UpdatePK(bool finishPK = false)
        {
            try
            {
                List<DocLine> sortedLines = _docLinesList.ToList();

                //Si se finaliza cerramos todos las líneas también
                if (finishPK)
                {
                    //The ToList is needed in order to evaluate the select immediately due to lazy evaluation.
                    sortedLines.Select(a => { a.IdSupplyStatus = Constants.SUPPLY_STATUS_CLOSE; return a; }).ToList();
                }

                DocHead packingList = new DocHead()
                {

                    IdDoc = _docHeadPK.IdDoc,
                    IdDocRelated = _docHeadPK.IdDocRelated,
                    IdSupplyDocType = _docHeadPK.IdSupplyDocType,
                    CreationDate = _docHeadPK.CreationDate,
                    DeliveryDate = dateEditPKDelivery.DateTime,
                    DocDate = _docHeadPK.DocDate,
                    IdSupplyStatus = (finishPK ? Constants.SUPPLY_STATUS_CLOSE : _docHeadPK.IdSupplyStatus),
                    IdSupplier = _docHeadPK.IdSupplier,
                    IdCustomer = _docHeadPK.IdCustomer,
                    IdDeliveryTerm = _docHeadPK.IdDeliveryTerm,
                    IdPaymentTerms = _docHeadPK.IdPaymentTerms,
                    IdCurrency = _docHeadPK.IdCurrency,
                    ManualReference = _docHeadPK.ManualReference,
                    Remarks = _docHeadPK.Remarks,
                    Lines = sortedLines,
                    Boxes = _docHeadPK.Boxes,
                    PackingListItemBatches = _itemsBatchList
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

                //Restore de ribbon to initial states
                RestoreInitState();

                //Clear grids
                xgrdLinesPL.DataSource = null;
                xgrdItemsBatch.DataSource = null;
                //Reload Packing list
                LoadPK();
                //not allow grid editing
                gridViewLinesPL.OptionsBehavior.Editable = false;
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
