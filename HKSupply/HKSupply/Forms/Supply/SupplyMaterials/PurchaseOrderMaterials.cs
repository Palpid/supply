using DevExpress.Data;
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
using HKSupply.Reports;
using HKSupply.Styles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HKSupply.Forms.Supply.SupplyMaterials
{
    public partial class PurchaseOrderMaterials : RibbonFormBase
    {
        #region Private Members

        Font _labelDefaultFontBold = AppStyles.LabelDefaultFontBold;
        Font _labelDefaultFont = AppStyles.LabelDefaultFont;

        List<Supplier> _suppliersList;
        List<Currency> _currenciesList;
        List<SupplyStatus> _supplyStatusList;
        List<PaymentTerms> _paymentTermsList;
        List<DeliveryTerm> _deliveryTermList;
        List<SupplierPriceList> _selectedSupplierPriceList;
        List<ItemGroup> _itemGroupList;
        List<ItemMt> _itemsMtList;
        List<ItemHw> _itemsHwList;

        BindingList<DocLine> _docLinesList;
        DocHead _docHeadPO;

        int? _batchCont = null;

        //bool _existQP = false;

        bool _isLoadingPO = false;
        #endregion

        #region Constructor
        public PurchaseOrderMaterials()
        {
            InitializeComponent();

            try
            {
                Cursor = Cursors.WaitCursor;

                ConfigureRibbonActions();
                LoadAuxList();
                SetUpLabels();
                SetUpButtons();
                SetUpSearchLookUpEdit();
                SetUpEvents();
                SetUpGrdLines();
                SetupPanelControl();
                SetUpPictureEdit();
                SetVisiblePropertyByState();
                SetObjectsReadOnly();
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
                if (_docLinesList == null || _docLinesList.Count == 0)
                {
                    MessageBox.Show(GlobalSetting.ResManager.GetString("NoDataSelected"));
                    RestoreInitState();

                }
                else if (_docHeadPO.IdSupplyStatus != Constants.SUPPLY_STATUS_OPEN)
                {
                    MessageBox.Show("Only OPEN Purchase Orders can be edited");
                    RestoreInitState();
                }
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

        public override void bbiNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            base.bbiNew_ItemClick(sender, e);

            try
            {
                ConfigureActionsStackViewCreating();
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

                if (ValidatePO() == false)
                    return;

                DialogResult result = MessageBox.Show(GlobalSetting.ResManager.GetString("SaveChanges"), "", MessageBoxButtons.YesNo);

                if (result != DialogResult.Yes)
                    return;

                Cursor = Cursors.WaitCursor;

                if (CurrentState == ActionsStates.New)
                {
                    res = CreatePO();
                }
                else if (CurrentState == ActionsStates.Edit)
                {
                    res = UpdatePO();
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
            if (gridViewLines.DataRowCount == 0)
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
                    gridViewLines.ExportToXlsx(ExportExcelFile);

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
            if (gridViewLines.DataRowCount == 0)
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
                    gridViewLines.ExportToCsv(ExportCsvFile);

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

        public override void BarButtonItemReport_ItemClick(object sender, ItemClickEventArgs e)
        {
            base.BarButtonItemReport_ItemClick(sender, e);

            try
            {
                if (_docHeadPO != null)
                {
                    FunctionalityReport tmp = e.Item.Tag as FunctionalityReport;
                    OpenReport(_docHeadPO.IdDoc, tmp.ReportFile);
                }
                else
                {
                    XtraMessageBox.Show("No Purchase Order Selected", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Form Events
        private void PurchaseOrderMaterials_Load(object sender, EventArgs e)
        {

        }

        private void SbSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (dateEditDocDate.EditValue != null)
                {
                    SearchPO();
                }
                else
                {
                    XtraMessageBox.Show("Select a Doc. Date", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SbOrder_Click(object sender, EventArgs e)
        {
            try
            {
                if (dateEditDocDate.EditValue != null)
                {
                    OrderDocLines();
                }
                else
                {
                    XtraMessageBox.Show("Select a Doc. Date", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SbImportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (lueItemGroup.EditValue != null)
                {
                    ImportExcel();
                }
                else
                {
                    XtraMessageBox.Show("Select group first", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    lueItemGroup.Focus();
                }
                    
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SbClosePO_Click(object sender, EventArgs e)
        {
            try
            {
                bool res = false;

                DialogResult result = MessageBox.Show("This action will close the Purchase Order and cannot be edited", "", MessageBoxButtons.YesNo);

                if (result != DialogResult.Yes)
                    return;

                Cursor = Cursors.WaitCursor;

                if (CurrentState == ActionsStates.Edit)
                {
                    res = UpdatePO(closePO: true);
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

        private void DateEditDelivery_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (dateEditDelivery.EditValue != null)
                {
                    lblDeliveryWeek.Text = dateEditDelivery.DateTime.GetWeek().ToString().PadLeft(2,'0');
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DateEditDocDate_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (_isLoadingPO) return;

                if (_docHeadPO != null)
                {
                    ResetPO();
                    ResetForm(resetSupplier: true, resetDate: false);

                }

                if (dateEditDocDate.EditValue != null)
                {
                    lblDocDateWeek.Text = dateEditDocDate.DateTime.GetWeek().ToString().PadLeft(2,'0');
                    GetDeliveryDate();
                    if (CurrentState == ActionsStates.New)
                    {
                        CreatePoNumber();
                        UpdateBatch();
                    }
                }
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
                if (_isLoadingPO)
                    return;

                if (CurrentState != ActionsStates.New)
                {
                    if (_docHeadPO != null)
                    {
                        ResetPO();
                        ResetForm(resetSupplier: false, resetDate: true);

                    }
                }
                else if (slueSupplier.EditValue != null)
                {
                    Cursor = Cursors.WaitCursor;
                    SetSupplierInfo();
                    CreatePoNumber();
                    LoadSelectedSupplierPriceList();
                    UpdateItemsPrice();
                    UpdateBatch();
                    GetDeliveryDate();
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

        private void LueItemGroup_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (_isLoadingPO)
                    return;

                //si cambian el grupo se borra el grid

                _docLinesList = new BindingList<DocLine>();
                _docLinesList.Add(new DocLine() { LineState = DocLine.LineStates.New, IdItemGroup = (string)lueItemGroup.EditValue });

                xgrdLines.DataSource = null;
                xgrdLines.DataSource = _docLinesList;


            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Public Methods
        public void InitData(string idDocPo)
        {
            try
            {
                _docHeadPO = GlobalSetting.SupplyDocsService.GetDoc(idDocPo);
                LoadPO();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Private Methods

        #region SetUps Form Objects

        private void SetUpLabels()
        {
            try
            {
                /********* Fonts **********/
                //Header 
                lblPONumber.Font = _labelDefaultFontBold;
                lblDate.Font = _labelDefaultFontBold;
                lblWeek.Font = _labelDefaultFontBold;
                lblDocDate.Font = _labelDefaultFontBold;
                lblDelivery.Font = _labelDefaultFontBold;
                lblSupplier.Font = _labelDefaultFontBold;
                lblTermsOfDelivery.Font = _labelDefaultFontBold;
                lblCurrency.Font = _labelDefaultFontBold;
                lblItemGroup.Font = _labelDefaultFontBold;
                lblDocDateWeek.Font = _labelDefaultFont;
                lblDeliveryWeek.Font = _labelDefaultFont;
                txtPONumber.Font = _labelDefaultFontBold;
                lblRemarks.Font = _labelDefaultFontBold;

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
                lblPONumber.Text = "PO Number";
                lblDate.Text = "Date";
                lblWeek.Text = "Week";
                lblDocDate.Text = "DOC DATE";
                lblDelivery.Text = "DELIVERY";
                lblSupplier.Text = "SUPPLIER";
                lblTermsOfDelivery.Text = "Term of Delivery";
                lblCurrency.Text = "Currency";
                lblItemGroup.Text = "Group";
                lblDocDateWeek.Text = string.Empty;
                lblDeliveryWeek.Text = string.Empty;
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
                lblDocDateWeek.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                lblDeliveryWeek.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                txtPONumber.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;

                //Terms Tab
                lblCompany.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
                lblAddress.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
                lblContact.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
                lblShipTo.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
                lblInvoiceTo.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
                lblTermPayment.Appearance.TextOptions.HAlignment = HorzAlignment.Far;

                /********* ReadOnly **********/
                txtPONumber.ReadOnly = true; //no es un label, lo sé

                /********* BackColor **********/
                txtPONumber.Properties.Appearance.BackColor = AppStyles.EtniaRed;
                txtPONumber.Properties.Appearance.BackColor2 = AppStyles.EtniaRed;

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
                sbImportExcel.Image = Image.FromFile(@"Resources\Images\import_export_excel_icon_32x32.png");
                sbImportExcel.Text = string.Empty;
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
                sbOrder.Click += SbOrder_Click;
                sbImportExcel.Click += SbImportExcel_Click;
                sbClosePO.Click += SbClosePO_Click;
                dateEditDelivery.EditValueChanged += DateEditDelivery_EditValueChanged;
                dateEditDocDate.EditValueChanged += DateEditDocDate_EditValueChanged;
                slueSupplier.EditValueChanged += SlueSupplier_EditValueChanged;
                lueItemGroup.EditValueChanged += LueItemGroup_EditValueChanged;
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
                SetUpSluePaymentTerms();
                SetUpSlueDeliveryTerms();
                SetUpUpLueItemGroup();
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

        private void SetUpUpLueItemGroup()
        {
            try
            {
                lueItemGroup.Properties.DataSource = _itemGroupList;
                lueItemGroup.Properties.ValueMember = nameof(ItemGroup.Id);
                lueItemGroup.Properties.DisplayMember = nameof(ItemGroup.Id);
                lueItemGroup.Properties.NullText = "Select...";
            }
            catch
            {
                throw;
            }
        }


        private void SetUpGrdLines()
        {
            try
            {
                //Activar que se alternen los colores de las filas del grid
                gridViewLines.OptionsView.EnableAppearanceOddRow = true;
                gridViewLines.OptionsView.EnableAppearanceEvenRow = true;

                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                gridViewLines.OptionsView.ColumnAutoWidth = false;
                gridViewLines.HorzScrollVisibility = ScrollVisibility.Auto;

                //Hacer todo el grid no editable
                gridViewLines.OptionsBehavior.Editable = false;

                //Hide grouping panel
                gridViewLines.OptionsView.ShowGroupPanel = false;

                //Disable sorting
                gridViewLines.OptionsCustomization.AllowSort = false;

                //Column Definition
                GridColumn colIdItemBcn = new GridColumn() { Caption = "Item Code", Visible = true, FieldName = nameof(DocLine.IdItemBcn), Width = 200 };
                GridColumn colDescription = new GridColumn() { Caption = "Description", Visible = true, FieldName = $"{nameof(DocLine.Item)}.{nameof(ItemEy.ItemDescription)}", Width = 350 };
                GridColumn colBatch = new GridColumn() { Caption = "Batch", Visible = true, FieldName = nameof(DocLine.Batch), Width = 85 };
                GridColumn colQuantity = new GridColumn() { Caption = "Quantity", Visible = true, FieldName = nameof(DocLine.Quantity), Width = 85 };
                GridColumn colUnitPrice = new GridColumn() { Caption = "Unit Price", Visible = true, FieldName = nameof(DocLine.UnitPrice), Width = 85 };
                GridColumn colTotalAmount = new GridColumn() { Caption = "TotalAmount", Visible = true, FieldName = nameof(DocLine.TotalAmount), Width = 120 };
                GridColumn colQuantityOriginal = new GridColumn() { Caption = "Original Quantity", Visible = true, FieldName = nameof(DocLine.QuantityOriginal), Width = 110 };
                GridColumn colDeliveredQuantity = new GridColumn() { Caption = "Quantity Delivered", Visible = true, FieldName = nameof(DocLine.DeliveredQuantity), Width = 120 };
                GridColumn colIdIdSupplyStatus = new GridColumn() { Caption = "Status", Visible = true, FieldName = nameof(DocLine.IdSupplyStatus), Width = 75 };
                GridColumn colRemarks = new GridColumn() { Caption = "Remarks", Visible = true, FieldName = nameof(DocLine.Remarks), Width = 350 };

                //Display Format
                colUnitPrice.DisplayFormat.FormatType = FormatType.Numeric;
                colUnitPrice.DisplayFormat.FormatString = "n2";

                colTotalAmount.DisplayFormat.FormatType = FormatType.Numeric;
                colTotalAmount.DisplayFormat.FormatString = "n2";

                colQuantity.DisplayFormat.FormatType = FormatType.Numeric;
                colQuantity.DisplayFormat.FormatString = "n0";

                colQuantityOriginal.DisplayFormat.FormatType = FormatType.Numeric;
                colQuantityOriginal.DisplayFormat.FormatString = "n0";

                colDeliveredQuantity.DisplayFormat.FormatType = FormatType.Numeric;
                colDeliveredQuantity.DisplayFormat.FormatString = "n0";

                //Edit Repositories
                RepositoryItemTextEdit ritxtInt = new RepositoryItemTextEdit();
                ritxtInt.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                ritxtInt.Mask.EditMask = "N";
                ritxtInt.AllowNullInput = DefaultBoolean.True;

                colQuantity.ColumnEdit = ritxtInt;
                colDeliveredQuantity.ColumnEdit = ritxtInt;

                RepositoryItemSearchLookUpEdit riSupplyStatus = new RepositoryItemSearchLookUpEdit()
                {
                    DataSource = _supplyStatusList,
                    ValueMember = nameof(SupplyStatus.IdSupplyStatus),
                    DisplayMember = nameof(SupplyStatus.IdSupplyStatus),
                    ShowClearButton = false,
                    NullText = string.Empty,
                };
                colIdIdSupplyStatus.ColumnEdit = riSupplyStatus;

                //Summaries
                gridViewLines.OptionsView.ShowFooter = true;
                colQuantity.Summary.Add(SummaryItemType.Sum, nameof(DocLine.Quantity), "{0:n0}");
                colTotalAmount.Summary.Add(SummaryItemType.Sum, nameof(DocLine.TotalAmount), "{0:n2}");
                colQuantityOriginal.Summary.Add(SummaryItemType.Sum, nameof(DocLine.QuantityOriginal), "{0:n2}");
                colDeliveredQuantity.Summary.Add(SummaryItemType.Sum, nameof(DocLine.DeliveredQuantity), "{0:n2}");

                //Add columns to grid root view
                gridViewLines.Columns.Add(colIdItemBcn);
                gridViewLines.Columns.Add(colDescription);
                gridViewLines.Columns.Add(colBatch);
                gridViewLines.Columns.Add(colQuantity);
                gridViewLines.Columns.Add(colUnitPrice);
                gridViewLines.Columns.Add(colTotalAmount);
                gridViewLines.Columns.Add(colQuantityOriginal);
                gridViewLines.Columns.Add(colDeliveredQuantity);
                gridViewLines.Columns.Add(colIdIdSupplyStatus);
                gridViewLines.Columns.Add(colRemarks);

                //Events
                gridViewLines.ValidatingEditor += GridViewLines_ValidatingEditor;
                gridViewLines.RowCellStyle += GridViewLines_RowCellStyle;
                gridViewLines.CellValueChanged += GridViewLines_CellValueChanged;
                gridViewLines.ShowingEditor += GridViewLines_ShowingEditor;
                gridViewLines.CustomRowCellEditForEditing += GridViewLines_CustomRowCellEditForEditing;
                xgrdLines.ProcessGridKey += XgrdLines_ProcessGridKey;
            }
            catch
            {
                throw;
            }
        }

        private void GridViewLines_CustomRowCellEditForEditing(object sender, CustomRowCellEditEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                DocLine row = view.GetRow(e.RowHandle) as DocLine;

                if (row == null)
                    return;

                switch (e.Column.FieldName)
                {
                    case nameof(DocLine.IdItemBcn):

                        RepositoryItemSearchLookUpEdit riItems = new RepositoryItemSearchLookUpEdit()
                        {
                            ShowClearButton = false,
                            NullText = "Select..."
                        };

                        if (row.IdItemGroup == Constants.ITEM_GROUP_MT)
                        {
                            riItems.DataSource = _itemsMtList;
                            riItems.ValueMember = nameof(ItemMt.IdItemBcn);
                            riItems.DisplayMember = nameof(ItemMt.IdItemBcn);
                            riItems.View.Columns.AddField(nameof(ItemMt.IdItemBcn)).Visible = true;
                            riItems.View.Columns.AddField(nameof(ItemMt.ItemDescription)).Visible = true;

                            e.RepositoryItem = riItems;
                        }
                        else if (row.IdItemGroup == Constants.ITEM_GROUP_HW)
                        {
                            riItems.DataSource = _itemsHwList;
                            riItems.ValueMember = nameof(ItemHw.IdItemBcn);
                            riItems.DisplayMember = nameof(ItemHw.IdItemBcn);
                            riItems.View.Columns.AddField(nameof(ItemHw.IdItemBcn)).Visible = true;
                            riItems.View.Columns.AddField(nameof(ItemHw.ItemDescription)).Visible = true;

                            e.RepositoryItem = riItems;
                        }

                        break;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GridViewLines_ShowingEditor(object sender, CancelEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                DocLine row = view.GetRow(view.FocusedRowHandle) as DocLine;

                switch (view.FocusedColumn.FieldName)
                {
                    case nameof(DocLine.IdItemBcn):
                        if (row.DeliveredQuantity > 0)
                        {
                            XtraMessageBox.Show("Can't edit row with DeliveredQuantity", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            e.Cancel = true;
                        }
                        if (lueItemGroup.EditValue == null)
                        {
                            XtraMessageBox.Show("Select item group first", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            e.Cancel = true;
                        }
                        else
                        {
                            row.IdItemGroup = (string)lueItemGroup.EditValue;
                        }
                        break;
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void XgrdLines_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                if (CurrentState != ActionsStates.Edit && CurrentState != ActionsStates.New)
                    return;

                if (e.KeyCode == Keys.F4)
                {
                    DialogResult result = XtraMessageBox.Show("Delete row?", "Confirmation", MessageBoxButtons.YesNo);
                    if (result != DialogResult.Yes)
                        return;

                    var row = gridViewLines.GetRow(gridViewLines.FocusedRowHandle) as DocLine;
                    if (row.IdItemBcn != null)
                    {
                        if (row.LineState == DocLine.LineStates.New || row.DeliveredQuantity == 0)
                        {
                            _docLinesList.Remove(row);
                            GetDeliveryDate();
                        }
                        else
                        {
                            XtraMessageBox.Show("You can't delete this row", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                    }

                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GridViewLines_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            try
            {
                switch (e.Column.FieldName)
                {
                    case nameof(DocLine.IdItemBcn):
                        GetDeliveryDate();
                        break;
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GridViewLines_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                DocLine row = (DocLine)gridViewLines.GetFocusedRow();

                switch (view.FocusedColumn.FieldName)
                {
                    case nameof(DocLine.IdItemBcn):

                        if (row.IdItemGroup == null)
                            return;

                        var idItem = e.Value.ToString();

                        //No se puede repetir item
                        var exist = _docLinesList.Where(a => string.IsNullOrEmpty(a.IdItemBcn) == false && a.IdItemBcn.Equals(idItem)).FirstOrDefault();
                        if (exist == null)
                        {
                            //clear some fields
                            row.Quantity = 0;
                            row.Remarks = string.Empty;

                            //Item
                            if (row.IdItemGroup == Constants.ITEM_GROUP_MT)
                            {
                                var itemMt = _itemsMtList.Where(a => a.IdItemBcn.Equals(idItem)).Single().Clone();
                                row.Item = itemMt;
                            }
                            else if (row.IdItemGroup == Constants.ITEM_GROUP_HW)
                            {
                                var itemHw = _itemsHwList.Where(a => a.IdItemBcn.Equals(idItem)).Single().Clone();
                                row.Item = itemHw;
                            }

                            //price
                            var supplierPrice = _selectedSupplierPriceList?.Where(a => a.IdItemBcn.Equals(idItem)).FirstOrDefault();
                            if (supplierPrice != null)
                            {
                                row.UnitPrice = supplierPrice.Price;
                                row.UnitPriceBaseCurrency = supplierPrice.PriceBaseCurrency;
                            }
                            else
                            {
                                row.UnitPrice = 0;
                                row.UnitPriceBaseCurrency = 0;
                            }

                            //Status & quantity
                            row.IdSupplyStatus = Constants.SUPPLY_STATUS_OPEN;
                            row.DeliveredQuantity = 0;

                            UpdateBatch();

                            //agregamos una línea nueva salvo que ya exista una en blanco
                            object _x = null;
                            if (row.IdItemGroup == Constants.ITEM_GROUP_MT) _x = new ItemMt();
                            if (row.IdItemGroup == Constants.ITEM_GROUP_HW) _x = new ItemHw();

                            if (_docLinesList.Where(a => a.Item == null).Count() == 0)
                                _docLinesList.Add(new DocLine() { LineState = DocLine.LineStates.New, IdItemGroup = row.IdItemGroup, Item = _x });

                            //gridViewLines.RefreshData();

                        }
                        else
                        {
                            e.Valid = false;
                            e.ErrorText = "Item already exist";
                        }
                        break;

                    case nameof(DocLine.Quantity):

                        int quantity = Convert.ToInt32(e.Value);

                        if (quantity < row.DeliveredQuantity)
                        {
                            e.Valid = false;
                            return;
                        }

                        if (quantity == 0)
                        {
                            row.IdSupplyStatus = Constants.SUPPLY_STATUS_CANCEL;
                        }
                        else if (quantity == row.DeliveredQuantity)
                        {
                            row.IdSupplyStatus = Constants.SUPPLY_STATUS_CLOSE;
                        }
                        else
                        {
                            row.IdSupplyStatus = Constants.SUPPLY_STATUS_OPEN;
                        }

                        break;

                    case nameof(DocLine.DeliveredQuantity):

                        int deliveredQuantity = Convert.ToInt32(e.Value);

                        if (deliveredQuantity >= row.Quantity)
                        {
                            row.IdSupplyStatus = Constants.SUPPLY_STATUS_CLOSE;
                        }
                        else
                        {
                            row.IdSupplyStatus = Constants.SUPPLY_STATUS_OPEN;
                        }

                        break;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GridViewLines_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                GridView View = sender as GridView;
                if (e.Column.FieldName == nameof(DocLine.Quantity))
                {
                    var status = View.GetRowCellValue(e.RowHandle, nameof(DocLine.IdSupplyStatus));

                    switch (status?.ToString())
                    {
                        case Constants.SUPPLY_STATUS_OPEN:
                            e.Appearance.BackColor = AppStyles.SupplyStatusOpnBKGD1;
                            e.Appearance.BackColor2 = AppStyles.SupplyStatusOpnBKGD2;
                            break;

                        case Constants.SUPPLY_STATUS_CLOSE:
                            e.Appearance.BackColor = AppStyles.SupplyStatusClsBKGD1;
                            e.Appearance.BackColor2 = AppStyles.SupplyStatusClsBKGD2;
                            break;

                        case Constants.SUPPLY_STATUS_CANCEL:
                            e.Appearance.BackColor = AppStyles.SupplyStatusCnlBKGD1;
                            e.Appearance.BackColor2 = AppStyles.SupplyStatusCnlBKGD2;
                            break;

                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void SetUpPictureEdit()
        {
            try
            {
                peEy.Properties.ShowMenu = false;
                peEy.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
                peEy.Image = Image.FromFile(@"Resources\Images\Acetate_244.jpg");
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Loads / Resets

        private void LoadAuxList()
        {
            try
            {
                _suppliersList = GlobalSetting.SupplierService.GetSuppliers(withEtniaHk: false).Where(a => a.Factory == false).ToList();
                _currenciesList = GlobalSetting.CurrencyService.GetCurrencies();
                _supplyStatusList = GlobalSetting.SupplyDocsService.GetSupplyStatus();
                _itemsMtList = GlobalSetting.ItemMtService.GetItems();
                _itemsHwList = GlobalSetting.ItemHwService.GetItems();
                _paymentTermsList = GlobalSetting.PaymentTermsService.GetPaymentTerms();
                _deliveryTermList = GlobalSetting.DeliveryTermsService.GetDeliveryTerms();

                //Sólo necesitamos los del raw material y las lo hardware
                _itemGroupList = new List<ItemGroup>();
                _itemGroupList.Add(new ItemGroup() { Id = Constants.ITEM_GROUP_MT, Description = Constants.ITEM_GROUP_MT });
                _itemGroupList.Add(new ItemGroup() { Id = Constants.ITEM_GROUP_HW, Description = Constants.ITEM_GROUP_HW });
            }
            catch
            {
                throw;
            }
        }

        private void LoadPO()
        {
            try
            {
                _isLoadingPO = true;

                var currentCulture = CultureInfo.CurrentCulture;

                slueSupplier.EditValue = _docHeadPO.IdSupplier;
                sluePaymentTerm.EditValue = _docHeadPO.IdPaymentTerms;
                slueCurrency.EditValue = _docHeadPO.IdCurrency;
                slueDeliveryTerms.EditValue = _docHeadPO.IdDeliveryTerm;

                dateEditDocDate.EditValue = _docHeadPO.DocDate;
                dateEditDelivery.EditValue = _docHeadPO.DeliveryDate;

                lblDocDateWeek.Text = dateEditDocDate.DateTime.GetWeek().ToString();
                lblDeliveryWeek.Text = dateEditDelivery.DateTime.GetWeek().ToString();

                memoEditRemarks.EditValue = _docHeadPO.Remarks;

                txtPONumber.Text = _docHeadPO.IdDoc;

                var itemGroup = _docHeadPO.Lines.Select(a => a.IdItemGroup).FirstOrDefault();

                if (itemGroup == Constants.ITEM_GROUP_MT)
                    lueItemGroup.EditValue = Constants.ITEM_GROUP_MT;
                else if (itemGroup == Constants.ITEM_GROUP_HW)
                    lueItemGroup.EditValue = Constants.ITEM_GROUP_HW;

                _docLinesList = new BindingList<DocLine>(_docHeadPO.Lines);

                xgrdLines.DataSource = null;
                xgrdLines.DataSource = _docLinesList;

                LoadSelectedSupplierPriceList();

            }
            catch
            {
                throw;
            }
            finally
            {
                _isLoadingPO = false;
            }
        }

        private void LoadSelectedSupplierPriceList()
        {
            try
            {
                _selectedSupplierPriceList = GlobalSetting.SupplierPriceListService.GetSuppliersPriceList(idItemBcn: null, idSupplier: slueSupplier.EditValue.ToString());
            }
            catch
            {
                throw;
            }
        }

        private void ResetPO()
        {
            try
            {
                _docHeadPO = null;
                _docLinesList = null;
                xgrdLines.DataSource = null;

                _batchCont = null;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Aux

        private void SearchPO()
        {
            try
            {
                ResetPO();

                string supplier = slueSupplier.EditValue as string;
                DateTime docDate = dateEditDocDate.DateTime;

                var docs = GlobalSetting.SupplyDocsService.GetDocs(idSupplier: supplier, idCustomer: null, docDate: docDate, IdSupplyDocType: Constants.SUPPLY_DOCTYPE_PO, idSupplyStatus: null);

                if (docs.Count == 0)
                {
                    XtraMessageBox.Show("No Data Found", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    using (DialogForms.SelectDocs form = new DialogForms.SelectDocs())
                    {
                        form.InitData(docs);
                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            _docHeadPO = form.SelectedDoc;
                            LoadPO();
                        }
                    }
                }

            }
            catch
            {
                throw;
            }
        }

        private void CreatePoNumber()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                string poNumber = string.Empty;

                if (slueSupplier.EditValue != null & dateEditDocDate.EditValue != null)
                {
                    var docs = GlobalSetting.SupplyDocsService.GetDocs(idSupplier: (string)slueSupplier.EditValue, idCustomer: null, docDate: dateEditDocDate.DateTime, IdSupplyDocType: Constants.SUPPLY_DOCTYPE_PO, idSupplyStatus: null);

                    string strCont;
                    if (docs.Count == 0)
                    {
                        strCont = string.Empty;
                        _batchCont = 0;
                    }
                    else
                    {
                        strCont = $"-{(docs.Count + 1).ToString()}";
                        var tmp = docs.OrderByDescending(a => a.IdDoc).FirstOrDefault().Lines.OrderByDescending(b => b.Batch).FirstOrDefault();
                        _batchCont = Convert.ToInt32(tmp.Batch.Substring(5, 1));
                    }

                    poNumber = $"{DateTime.Now.Year.ToString().Substring(3)}{lblDocDateWeek.Text}{slueSupplier.EditValue}{strCont}";
                }

                txtPONumber.Text = poNumber;
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

        private void SetSupplierInfo()
        {
            try
            {
                var supplier = _suppliersList.Where(a => a.IdSupplier.Equals(slueSupplier.EditValue.ToString())).FirstOrDefault();

                if (supplier != null)
                {
                    lblTxtCompany.Text = supplier.SupplierName;
                    lblTxtAddress.Text = $"{supplier.ShippingAddress} {supplier.ShippingAddress2}";
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

        private void UpdateBatch()
        {
            try
            {
                if (string.IsNullOrEmpty(txtPONumber.Text) == true)
                    return;

                string poNumber = txtPONumber.Text.Substring(0, 5);

                int cont = 1;

                if (_batchCont != null)
                    cont = _batchCont.Value + 1;


                var itemsMt = _docLinesList.Where(a => a.IdItemGroup != null && a.IdItemGroup.Equals(Constants.ITEM_GROUP_MT)).Select(a => (a.Item as ItemMt)?.IdItemBcn).Distinct().OrderBy(a => a).ToList();
                var itemsHw = _docLinesList.Where(a => a.IdItemGroup != null && a.IdItemGroup.Equals(Constants.ITEM_GROUP_HW)).Select(a => (a.Item as ItemHw)?.IdItemBcn).Distinct().OrderBy(a => a).ToList();
                var items = itemsMt.Union(itemsHw).Distinct().ToList();

                //var items = _docLinesList.Select(a => a.IdItemBcn).Distinct().Where(a => string.IsNullOrEmpty(a) == false).OrderBy(a => a).ToList();

                foreach (var item in items)
                {
                    foreach (DocLine line in _docLinesList)
                    {
                        if (line.IdItemGroup == Constants.ITEM_GROUP_MT)
                        {
                            if ((line.Item as ItemMt)?.IdItemBcn == item)
                            {
                                line.Batch = $"{poNumber}{cont.ToString()}";
                            }
                        }
                        else if (line.IdItemGroup == Constants.ITEM_GROUP_HW)
                        {
                            if ((line.Item as ItemHw)?.IdItemBcn == item)
                            {
                                line.Batch = $"{poNumber}{cont.ToString()}";
                            }
                        }
                        //if (line.IdItemBcn == item)
                        //{
                        //    line.Batch = $"{poNumber}{cont.ToString()}";
                        //}
                    }

                    cont++;
                }

                gridViewLines.RefreshData();
            }
            catch
            {
                throw;
            }
        }

        private void UpdateItemsPrice()
        {
            try
            {
                if (_docLinesList == null) return;

                foreach (DocLine line in _docLinesList)
                {
                    if (line.IdItemBcn != null)
                    {
                        var supplierPrice = _selectedSupplierPriceList.Where(a => a.IdItemBcn.Equals(line.IdItemBcn)).FirstOrDefault();
                        if (supplierPrice != null)
                        {
                            line.UnitPrice = (short)supplierPrice.Price;
                            line.UnitPriceBaseCurrency = (short)supplierPrice.PriceBaseCurrency;
                        }
                        else
                        {
                            line.UnitPrice = 0;
                            line.UnitPriceBaseCurrency = 0;
                        }
                    }
                }
                gridViewLines.RefreshData();
            }
            catch
            {
                throw;
            }
        }

        private void SetVisiblePropertyByState()
        {
            try
            {
                switch (CurrentState)
                {
                    case ActionsStates.Edit:
                        sbImportExcel.Visible = true;
                        sbOrder.Visible = true;
                        sbSearch.Visible = false;
                        sbClosePO.Visible = true;
                        break;

                    case ActionsStates.New:
                        sbImportExcel.Visible = true;
                        sbOrder.Visible = true;
                        sbSearch.Visible = false;
                        sbClosePO.Visible = false;
                        break;
                    default:
                        sbOrder.Visible = false;
                        sbImportExcel.Visible = false;
                        sbSearch.Visible = true;
                        sbClosePO.Visible = false;
                        break;
                }
            }
            catch
            {
                throw;
            }
        }

        private void OrderDocLines()
        {
            try
            {
                //List<DocLine> sortedLines = _docLinesList.Where(lin => lin.IdItemBcn != null).OrderBy(a => a.Batch).ThenBy(b => b.IdItemBcn).ToList();
                List<DocLine> sortedLines = _docLinesList
                    .Where(lin => lin.IdItemBcn != null)
                    .OrderBy(a => Convert.ToInt32(System.Text.RegularExpressions.Regex.Match(a.Batch, @"\d+$").Value))
                    .ThenBy(b => b.IdItemBcn).ToList();

                _docLinesList.Clear();
                _docLinesList = new BindingList<DocLine>(sortedLines);
                _docLinesList.Add(new DocLine() { LineState = DocLine.LineStates.New });

                xgrdLines.DataSource = null;
                xgrdLines.DataSource = _docLinesList;
            }
            catch
            {
                throw;
            }
        }

        private void ImportExcel()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                Dictionary<string, int> importData = GetExcelImportData();

                if (importData.Count > 0)
                {
                    ImportDataToGrid(importData);
                }
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

        private Dictionary<string, int> GetExcelImportData()
        {
            try
            {
                Dictionary<string, int> itemDic = new Dictionary<string, int>();
                List<string> errorlist = new List<string>();

                OpenFileDialog openFileDialog = new OpenFileDialog()
                {
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    Filter = "XLSX files (*.xlsx)|*.xlsx|XLS files (*.xls)|*.xls|CSV files (*.csv)|*.csv",
                    Multiselect = false,
                    RestoreDirectory = true,
                };
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var excelFile = openFileDialog.FileName;

                    using (DevExpress.XtraSpreadsheet.SpreadsheetControl spreadsheet = new DevExpress.XtraSpreadsheet.SpreadsheetControl())
                    {
                        spreadsheet.LoadDocument(excelFile);

                        DevExpress.Spreadsheet.Worksheet ws = spreadsheet.ActiveWorksheet;
                        DevExpress.Spreadsheet.Range firstColumn = ws.Columns[0];
                        DevExpress.Spreadsheet.Range secondColumn = ws.Columns[1];
                        DevExpress.Spreadsheet.Range range = ws.GetDataRange();

                        for (int i = range.TopRowIndex; i <= range.BottomRowIndex; i++)
                        {
                            var cellItem = firstColumn[i].Value;
                            var cellQuantity = secondColumn[i].Value;

                            //int exist = _itemsEyList.Where(a => a.IdItemBcn.Equals(cellItem.ToString())).Count();
                            int exist = 0;
                            if ((string)lueItemGroup.EditValue == Constants.ITEM_GROUP_MT)
                                exist = _itemsMtList.Where(a => a.IdItemBcn.Equals(cellItem.ToString())).Count();
                            else if ((string)lueItemGroup.EditValue == Constants.ITEM_GROUP_HW)
                                exist = _itemsHwList.Where(a => a.IdItemBcn.Equals(cellItem.ToString())).Count();

                            if (exist > 0)
                            {
                                int quantity;
                                if (int.TryParse(cellQuantity.ToString(), out quantity))
                                {
                                    if (itemDic.ContainsKey(cellItem.ToString()))
                                    {
                                        errorlist.Add($"Line {(i + 1).ToString()}: Item duplicated {cellItem.ToString()}.");
                                    }
                                    else
                                    {
                                        itemDic.Add(cellItem.ToString(), quantity);
                                    }

                                }
                                else
                                {
                                    errorlist.Add($"Line {(i + 1).ToString()}: Item {cellItem.ToString()}. Incorrect Quantity: {cellQuantity.ToString()}");
                                }
                            }
                            else
                            {
                                errorlist.Add($"Line {(i + 1).ToString()}: Item {cellItem.ToString()} doesn't exist.");
                            }
                        }
                    }

                    if (errorlist.Count > 0)
                    {
                        itemDic = new Dictionary<string, int>();
                        XtraMessageBox.Show("Import with errors", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        string errorFile = $"{ System.IO.Path.GetTempPath()}ExcelErrorImport_{DateTime.Now.Ticks.ToString()}";
                        System.IO.File.WriteAllLines(errorFile, errorlist);
                        System.Diagnostics.Process.Start("notepad.exe", errorFile);
                    }

                }

                return itemDic;
            }
            catch
            {
                throw;
            }
        }

        private void ImportDataToGrid(Dictionary<string, int> importData)
        {
            //TODO

            try
            {

                _docLinesList = new BindingList<DocLine>();

                foreach (KeyValuePair<string, int> entry in importData)
                {
                    object item = null;
                    string itemGroup = string.Empty;

                    if ((string)lueItemGroup.EditValue == Constants.ITEM_GROUP_MT)
                    {
                        item = _itemsMtList.Where(a => a.IdItemBcn.Equals(entry.Key)).Single().Clone();
                        itemGroup = Constants.ITEM_GROUP_MT;
                    }
                    else if ((string)lueItemGroup.EditValue == Constants.ITEM_GROUP_HW)
                    {
                        item = _itemsHwList.Where(a => a.IdItemBcn.Equals(entry.Key)).Single().Clone();
                        itemGroup = Constants.ITEM_GROUP_HW;
                    }
                        

                    DocLine line = new DocLine();

                    //Item
                    line.IdItemBcn = entry.Key;
                    line.IdItemGroup = itemGroup;
                    line.Item = item;

                    //price
                    var supplierPrice = _selectedSupplierPriceList?.Where(a => a.IdItemBcn.Equals(line.IdItemBcn)).FirstOrDefault();
                    if (supplierPrice != null)
                    {
                        line.UnitPrice = (short)supplierPrice.Price;
                        line.UnitPriceBaseCurrency = (short)supplierPrice.PriceBaseCurrency;
                    }
                    else
                    {
                        line.UnitPrice = 0;
                        line.UnitPriceBaseCurrency = 0;
                    }

                    //Status & quantity
                    line.IdSupplyStatus = Constants.SUPPLY_STATUS_OPEN;
                    line.Quantity = entry.Value;
                    line.DeliveredQuantity = 0;
                    line.LineState = DocLine.LineStates.New;

                    _docLinesList.Add(line);
                }

                _docLinesList.Add(new DocLine() { LineState = DocLine.LineStates.New });

                UpdateBatch();

                xgrdLines.DataSource = null;
                xgrdLines.DataSource = _docLinesList;

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
                dateEditDocDate.ReadOnly = false;
                slueSupplier.ReadOnly = false;

                string idPO = _docHeadPO?.IdDoc;

                ResetPO();
                ResetForm();
                SetObjectsReadOnly();
                //Hacer el grid no editable
                gridViewLines.OptionsBehavior.Editable = false;

                if (idPO != null)
                {
                    _docHeadPO = GlobalSetting.SupplyDocsService.GetDoc(idPO);
                    LoadPO();
                }

                //Restore de ribbon to initial states
                RestoreInitState();

                SetVisiblePropertyByState();
            }
            catch
            {
                throw;
            }
        }

        private void ResetForm(bool resetSupplier = true, bool resetDate = true)
        {
            try
            {
                /********* Head *********/
                txtPONumber.Text = string.Empty;
                if (resetDate) dateEditDocDate.EditValue = null;
                dateEditDelivery.EditValue = null;
                lblDocDateWeek.Text = string.Empty;
                lblDeliveryWeek.Text = string.Empty;
                if (resetSupplier) slueSupplier.EditValue = null;
                slueDeliveryTerms.EditValue = null;
                slueCurrency.EditValue = null;
                memoEditRemarks.EditValue = null;

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

        private void ShowMessageDocsGenerated()
        {
            try
            {
                var docs = GlobalSetting.SupplyDocsService.GetDocsByRelated(_docHeadPO.IdDoc);

                if (docs.Count > 0)
                {
                    string msg = "The Following documents have been created:" + Environment.NewLine;

                    foreach (var doc in docs)
                    {
                        msg += $"{doc.IdDoc} ({doc.SupplyDocType.Description}){Environment.NewLine}";
                    }

                    XtraMessageBox.Show(msg);
                }

            }
            catch
            {
                throw;
            }
        }

        private void GetDeliveryDate()
        {
            try
            {
                if (dateEditDocDate.EditValue == null || _selectedSupplierPriceList == null || _docLinesList == null)
                    return;

                var itemsList = _docLinesList.Where(l => l.IdItemBcn != null).Select(a => a.IdItemBcn).ToList();
                float maxLeadTime = _selectedSupplierPriceList.Where(a => itemsList.Contains(a.IdItemBcn)).Select(b => b.LeadTime).DefaultIfEmpty(0).Max();

                DateTime deliveryDate = dateEditDocDate.DateTime.AddDays(maxLeadTime);
                dateEditDelivery.EditValue = deliveryDate;
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
                dateEditDelivery.ReadOnly = true;
                slueDeliveryTerms.ReadOnly = true;
                slueCurrency.ReadOnly = true;
                sluePaymentTerm.ReadOnly = true;
                memoEditRemarks.ReadOnly = true;
                lueItemGroup.ReadOnly = true;
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
                dateEditDelivery.ReadOnly = false;
                slueDeliveryTerms.ReadOnly = false;
                sluePaymentTerm.ReadOnly = false;
                memoEditRemarks.ReadOnly = false;
                lueItemGroup.ReadOnly = false;
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
                ResetPO();
                SetObjectsEnableToCreate();

                dateEditDocDate.EditValue = null;
                dateEditDelivery.EditValue = null;
                lblDocDateWeek.Text = string.Empty;
                lblDeliveryWeek.Text = string.Empty;
                slueSupplier.EditValue = null;
                slueDeliveryTerms.EditValue = null;
                slueCurrency.EditValue = null;

                slueSupplier.ReadOnly = false;
                dateEditDocDate.ReadOnly = false;


                _docLinesList = new BindingList<DocLine>();
                _docLinesList.Add(new DocLine() { LineState = DocLine.LineStates.New});

                xgrdLines.DataSource = null;
                xgrdLines.DataSource = _docLinesList;

                //Allow edit all columns
                gridViewLines.OptionsBehavior.Editable = true;

                //Block not editing columns
                gridViewLines.Columns[nameof(DocLine.UnitPrice)].OptionsColumn.AllowEdit = false;
                gridViewLines.Columns[nameof(DocLine.Batch)].OptionsColumn.AllowEdit = false;
                gridViewLines.Columns[nameof(DocLine.TotalAmount)].OptionsColumn.AllowEdit = false;
                gridViewLines.Columns[nameof(DocLine.QuantityOriginal)].OptionsColumn.AllowEdit = false;
                gridViewLines.Columns[$"{nameof(DocLine.Item)}.{nameof(ItemEy.ItemDescription)}"].OptionsColumn.AllowEdit = false;
                gridViewLines.Columns[nameof(DocLine.DeliveredQuantity)].OptionsColumn.AllowEdit = false;
                gridViewLines.Columns[nameof(DocLine.IdSupplyStatus)].OptionsColumn.AllowEdit = false;

                //events
                //SetUpEventsEditing();

                //Visible buttons
                SetVisiblePropertyByState();

            }
            catch
            {
                throw;
            }
        }

        private void ConfigureRibbonActionsEditing()
        {
            try
            {
                //Allow edit all columns
                gridViewLines.OptionsBehavior.Editable = true;

                //Block common not editing columns
                gridViewLines.Columns[nameof(DocLine.UnitPrice)].OptionsColumn.AllowEdit = false;
                gridViewLines.Columns[nameof(DocLine.Batch)].OptionsColumn.AllowEdit = false;
                gridViewLines.Columns[nameof(DocLine.TotalAmount)].OptionsColumn.AllowEdit = false;
                gridViewLines.Columns[nameof(DocLine.QuantityOriginal)].OptionsColumn.AllowEdit = false;
                gridViewLines.Columns[$"{nameof(DocLine.Item)}.{nameof(ItemEy.ItemDescription)}"].OptionsColumn.AllowEdit = false;
                gridViewLines.Columns[nameof(DocLine.IdSupplyStatus)].OptionsColumn.AllowEdit = false;

                gridViewLines.Columns[nameof(DocLine.DeliveredQuantity)].OptionsColumn.AllowEdit = false;

                //agregamos una línea nueva
                _docLinesList.Add(new DocLine() { LineState = DocLine.LineStates.New });
                gridViewLines.RefreshData();


                //Visible buttons
                SetVisiblePropertyByState();

                //No editing form fields
                slueSupplier.ReadOnly = true;
                dateEditDocDate.ReadOnly = true;

                //Editing dorm fields
                memoEditRemarks.ReadOnly = false;

            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region CRUD

        private bool ValidatePO()
        {
            try
            {
                if (slueSupplier.EditValue == null)
                {
                    MessageBox.Show("Field Required: Supplier", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    slueSupplier.Focus();
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

                if (lueItemGroup.EditValue == null)
                {
                    MessageBox.Show("Field Required: Group", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    lueItemGroup.Focus();
                    return false;
                }

                if (sluePaymentTerm.EditValue == null)
                {
                    MessageBox.Show("Field Required: Term of Payment", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    sluePaymentTerm.Focus();
                    return false;
                }

                if (dateEditDocDate.EditValue == null)
                {
                    MessageBox.Show("Field Required: Doc. Date", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dateEditDocDate.Focus();
                    return false;
                }

                if (dateEditDelivery.EditValue == null)
                {
                    MessageBox.Show("Field Required: Delivery Date", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dateEditDelivery.Focus();
                    return false;
                }

                if (_docLinesList.Count == 1)
                {
                    MessageBox.Show("Document without lines", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                //Validamos que no haya ningún item sin precio
                foreach (DocLine line in _docLinesList)
                {
                    if (string.IsNullOrEmpty(line.IdItemBcn) == false && line.TotalAmount == 0)
                    {
                        MessageBox.Show($"Item [{line.IdItemBcn}]. Total Amount cannot be 0", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private bool ValidateLines()
        {
            try
            {
                //TODO ??

                return true;
            }
            catch
            {
                throw;
            }
        }

        private bool CreatePO()
        {
            try
            {
                //List<DocLine> sortedLines = _docLinesList.Where(lin => lin.IdItemBcn != null).OrderBy(a => a.Batch).ThenBy(b => b.IdItemBcn).ToList();
                //para quedarse sólo con la parte final del batch (el número)
                List<DocLine> sortedLines = _docLinesList
                    .Where(lin => lin.IdItemBcn != null)
                    .OrderBy(a => Convert.ToInt32(System.Text.RegularExpressions.Regex.Match(a.Batch, @"\d+$").Value))
                    .ThenBy(b => b.IdItemBcn)
                    .ToList();

                DocHead purchaseOrder = new DocHead()
                {
                    IdDoc = txtPONumber.Text,
                    IdSupplyDocType = Constants.SUPPLY_DOCTYPE_PO,
                    CreationDate = DateTime.Now,
                    DeliveryDate = dateEditDelivery.DateTime,
                    DocDate = dateEditDocDate.DateTime,
                    IdSupplyStatus = Constants.SUPPLY_STATUS_OPEN,
                    IdSupplier = slueSupplier.EditValue as string,
                    IdCustomer = Constants.ETNIA_HK_COMPANY_CODE,
                    IdDeliveryTerm = slueDeliveryTerms.EditValue as string,
                    IdPaymentTerms = sluePaymentTerm.EditValue as string,
                    IdCurrency = slueCurrency.EditValue as string,
                    Remarks = memoEditRemarks.EditValue as string,
                    Lines = sortedLines
                };

                DocHead createdDoc = GlobalSetting.SupplyDocsService.NewDoc(purchaseOrder);

                _docHeadPO = createdDoc;

                return true;
            }
            catch
            {
                throw;
            }
        }

        private bool UpdatePO(bool closePO = false)
        {
            try
            {
                //para quedarse sólo con la parte final del batch (el número)
                List<DocLine> sortedLines = _docLinesList
                    .Where(lin => lin.IdItemBcn != null)
                    .OrderBy(a => Convert.ToInt32(System.Text.RegularExpressions.Regex.Match(a.Batch, @"\d+$").Value))
                    .ThenBy(b => b.IdItemBcn)
                    .ToList();

                //Si se cierra el documento cerramos todas las líneas también
                if (closePO)
                {
                    //The ToList is needed in order to evaluate the select immediately due to lazy evaluation.
                    sortedLines.Select(a => { a.IdSupplyStatus = Constants.SUPPLY_STATUS_CLOSE; return a; }).ToList();
                }

                DocHead purchaseOrder = new DocHead()
                {
                    IdDoc = txtPONumber.Text,
                    IdSupplyDocType = Constants.SUPPLY_DOCTYPE_PO,
                    CreationDate = _docHeadPO.CreationDate,
                    DeliveryDate = dateEditDelivery.DateTime,
                    DocDate = dateEditDocDate.DateTime,
                    IdSupplyStatus = (closePO ? Constants.SUPPLY_STATUS_CLOSE: Constants.SUPPLY_STATUS_OPEN) ,
                    IdSupplier = slueSupplier.EditValue as string,
                    IdCustomer = Constants.ETNIA_HK_COMPANY_CODE,
                    IdDeliveryTerm = slueDeliveryTerms.EditValue as string,
                    IdPaymentTerms = sluePaymentTerm.EditValue as string,
                    IdCurrency = slueCurrency.EditValue as string,
                    Remarks = memoEditRemarks.EditValue as string,
                    Lines = sortedLines
                };

                DocHead updatedDoc = GlobalSetting.SupplyDocsService.UpdateDocSupplyMaterials(purchaseOrder);

                _docHeadPO = updatedDoc;

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
                //Reload PO
                LoadPO();
                //Hacer el grid no editable
                gridViewLines.OptionsBehavior.Editable = false;

                dateEditDocDate.ReadOnly = false;
                slueSupplier.ReadOnly = false;

                //Restore de ribbon to initial states
                RestoreInitState();

                SetObjectsReadOnly();
                SetVisiblePropertyByState();
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Crystal Report
        private void OpenReport(string idDoc, string reportFile)
        {
            try
            {
                if (System.IO.File.Exists($"{Application.StartupPath}{reportFile}") == false)
                    throw new Exception("Report File does not exist");

                B1Report crReport = new B1Report();
                Dictionary<string, string> m_Parametros = new Dictionary<string, string>();
                m_Parametros.Add("@pIdDoc", idDoc);
                crReport.Parametros = m_Parametros;
                crReport.ReportFileName = $"{Application.StartupPath}{reportFile}";
                //The easiest way to get the default printer is to create a new PrinterSettings object. It starts with all default values.
                System.Drawing.Printing.PrinterSettings settings = new System.Drawing.Printing.PrinterSettings();
                crReport.PrinterName = settings.PrinterName;

                crReport.PreviewReport();
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
