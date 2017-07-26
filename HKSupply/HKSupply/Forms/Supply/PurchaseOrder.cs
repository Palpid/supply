using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using System.Globalization;
using HKSupply.General;
using HKSupply.Models;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;
using HKSupply.Models.Supply;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using HKSupply.Helpers;
using HKSupply.Styles;

namespace HKSupply.Forms.Supply
{
    public partial class PurchaseOrder : RibbonFormBase
    {

        #region Constants
        private const string TOTAL_AMOUNT_COLUMN = "TotalAmount";
        #endregion

        #region Private Members

        Font _labelDefaultFontBold = new Font("SourceSansProRegular", 8, FontStyle.Bold);
        Font _labelDefaultFont = new Font("SourceSansProRegular", 8, FontStyle.Regular);

        List<Supplier> _suppliersList;
        List<Currency> _currenciesList;
        List<SupplyStatus> _supplyStatusList;
        List<PaymentTerms> _paymentTermsList;
        List<DeliveryTerm> _deliveryTermList;
        List<SupplierPriceList> _selectedSupplierPriceList;
        List<ItemEy> _itemsEyList;

        BindingList<DocLine> _docLinesList;
        DocHead _docHeadPO;

        int? _batchCont = null;

        #endregion

        #region Constructor
        public PurchaseOrder()
        {
            InitializeComponent();

            try
            {
                Cursor = Cursors.WaitCursor;

                ConfigureRibbonActions();
                LoadAuxList();
                SetUpLabels();
                SetUpSearchLookUpEdit();
                SetUpEvents();
                SetUpGrdLines();

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
                if (_docLinesList?.Count > 0)
                    ConfigureRibbonActionsEditing();
                else
                    MessageBox.Show(GlobalSetting.ResManager.GetString("NoDataSelected"));

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

        #region Forms Events
        private void PurchaseOrder_Load(object sender, EventArgs e)
        {
            try
            {
                //DocHead temp = GlobalSetting.SupplyDocsService.GetDoc("730CR");
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DateEditDocDate_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (dateEditDocDate.EditValue != null)
                {
                    lblDocDateWeek.Text = GetWeek(dateEditDocDate.DateTime).ToString();
                    CreatePoNumber();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DateEditDelivery_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (dateEditDelivery.EditValue != null)
                {
                    lblDeliveryWeek.Text = GetWeek(dateEditDelivery.DateTime).ToString();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void SlueSupplierEditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (slueSupplier.EditValue != null)
                {
                    Cursor = Cursors.WaitCursor;
                    SetSupplierInfo();
                    CreatePoNumber();
                    LoadSelectedSupplierPriceList();
                    UpdateItemsPrice();
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

        private void SbSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if(dateEditDocDate.EditValue != null)
                {
                    SearchPO();
                }
                else
                {
                    XtraMessageBox.Show("Select a Doc. Date", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch(Exception ex)
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
                lblDocDateWeek.Font = _labelDefaultFont;
                lblDeliveryWeek.Font = _labelDefaultFont;
                txtPONumber.Font = _labelDefaultFontBold;

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
                lblDocDateWeek.Text = string.Empty;
                lblDeliveryWeek.Text = string.Empty;
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

        private void SetUpEvents()
        {
            try
            {
                sbSearch.Click += SbSearch_Click;
                sbOrder.Click += SbOrder_Click;
            }
            catch
            {
                throw;
            }
        }

        private void SetUpEventsEditing()
        {
            try
            {
                dateEditDelivery.EditValueChanged += DateEditDelivery_EditValueChanged;
                dateEditDocDate.EditValueChanged += DateEditDocDate_EditValueChanged;
                slueSupplier.EditValueChanged += SlueSupplierEditValueChanged;
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

        private void SetUpGrdLines()
        {
            try
            {
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
                GridColumn colQuantity = new GridColumn() { Caption = "Quantity", Visible = true, FieldName = nameof(DocLine.Quantity), Width =85 };
                GridColumn colUnitPrice = new GridColumn() { Caption = "Unit Price", Visible = true, FieldName = nameof(DocLine.UnitPrice), Width = 85 };
                //GridColumn colTotalAmount = new GridColumn() { Caption = "TotalAmount", Visible = true, FieldName = TOTAL_AMOUNT_COLUMN, Width = 120 };
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
                RepositoryItemSearchLookUpEdit riItems = new RepositoryItemSearchLookUpEdit()
                {
                    DataSource = _itemsEyList,
                    ValueMember = nameof(ItemEy.IdItemBcn),
                    DisplayMember = nameof(ItemEy.IdItemBcn),
                    ShowClearButton = false,
                    NullText = "Select..."
                };
                riItems.View.Columns.AddField(nameof(ItemEy.IdItemBcn)).Visible = true;
                riItems.View.Columns.AddField(nameof(ItemEy.ItemDescription)).Visible = true;

                colIdItemBcn.ColumnEdit = riItems;

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
                colTotalAmount.Summary.Add(SummaryItemType.Sum, nameof(DocLine.TotalAmount),"{0:n2}");
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

                //gridViewLines.Columns[nameof(DocLine.Quantity)].Summary.Add(DevExpress.Data.SummaryItemType.Sum, nameof(DocLine.Quantity), "Total = {0:n2}");

                //Events
                gridViewLines.ValidatingEditor += GridViewLines_ValidatingEditor;

                //Test
                //gridViewLines.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;
                //gridViewLines.RowUpdated += GridViewLines_RowUpdated;
                gridViewLines.RowCellStyle += GridViewLines_RowCellStyle;
            }
            catch
            {
                throw;
            }
        }

        private void GridViewLines_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            try
            {
                GridView View = sender as GridView;
                if(e.Column.FieldName == nameof(DocLine.Quantity))
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

                        //TODO: falta el status OPD que no sé qué significa
                        //case "":
                        //    break;
                    }
                }
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private void GridViewLines_RowUpdated(object sender, RowObjectEventArgs e)
        //{
        //    try
        //    {
        //        gridViewLines.RefreshData();
        //    }
        //    catch(Exception ex)
        //    {
        //        XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }

        //}

        private void GridViewLines_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                DocLine row = (DocLine)gridViewLines.GetFocusedRow();

                switch (view.FocusedColumn.FieldName)
                {
                    case nameof(DocLine.IdItemBcn):

                        var idItem = e.Value.ToString();

                        //clear some fields
                        row.Quantity = 0;
                        row.Remarks = string.Empty;

                        //item
                        var item = _itemsEyList.Where(a => a.IdItemBcn.Equals(idItem)).Single().Clone();
                        row.Item = item;

                        (row.Item as ItemEy).ItemDescription = item.ItemDescription;

                        row.IdItemGroup = Constants.ITEM_GROUP_EY;

                        //price
                        var supplierPrice = _selectedSupplierPriceList.Where(a => a.IdItemBcn.Equals(idItem)).FirstOrDefault();
                        if (supplierPrice != null)
                        {
                            row.UnitPrice = (short)supplierPrice.Price;
                            row.UnitPriceBaseCurrency = (short)supplierPrice.PriceBaseCurrency;
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
                        if (_docLinesList.Where(a => a.Item == null).Count() == 0)
                            _docLinesList.Add(new DocLine() { LineState = DocLine.LineStates.New});

                        //gridViewLines.RefreshData();

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
                        //else if (quantity >= row.QuantityOriginal)
                        //{
                        //    row.IdSupplyStatus = Constants.SUPPLY_STATUS_CLOSE;
                        //}


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
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Loads / Resets
        private void LoadAuxList()
        {
            try
            {
                _suppliersList = GlobalSetting.SupplierService.GetSuppliers();
                _currenciesList = GlobalSetting.CurrencyService.GetCurrencies();
                _supplyStatusList = GlobalSetting.SupplyDocsService.GetSupplyStatus();
                _itemsEyList = GlobalSetting.ItemEyService.GetItems();
                _paymentTermsList = GlobalSetting.PaymentTermsService.GetPaymentTerms();
                _deliveryTermList = GlobalSetting.DeliveryTermsService.GetDeliveryTerms();
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
                var currentCulture = CultureInfo.CurrentCulture;

                slueSupplier.EditValue = _docHeadPO.IdSupplier;
                sluePaymentTerm.EditValue = _docHeadPO.IdPaymentTerms;
                slueCurrency.EditValue = _docHeadPO.IdCurrency;
                slueDeliveryTerms.EditValue = _docHeadPO.IdDeliveryTerm;

                dateEditDocDate.EditValue = _docHeadPO.DocDate;
                dateEditDelivery.EditValue = _docHeadPO.DeliveryDate;

                lblDocDateWeek.Text = GetWeek(dateEditDocDate.DateTime).ToString();
                lblDeliveryWeek.Text = GetWeek(dateEditDelivery.DateTime).ToString();

                txtPONumber.Text = _docHeadPO.IdDoc;

                _docLinesList = new BindingList<DocLine>(_docHeadPO.Lines);

                xgrdLines.DataSource = null;
                xgrdLines.DataSource = _docLinesList;

                LoadSelectedSupplierPriceList();
            }
            catch
            {
                throw;
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

                //foreach(Control ctl in gbHeader.Controls)
                //{
                //    if (ctl.GetType() == typeof(TextEdit))
                //    {
                //        ((TextEdit)ctl).EditValue = null;
                //    }

                //    if (ctl.GetType() == typeof(DateEdit))
                //    {
                //        ((DateEdit)ctl).EditValue = null;
                //    }

                //    if (ctl.GetType() == typeof(SearchLookUpEdit))
                //    {
                //        ((SearchLookUpEdit)ctl).EditValue = null;
                //    }
                //}

                //lblDocDateWeek.Text = string.Empty;
                //lblDelivery.Text = string.Empty;

                //foreach(Control ctl in xtpTerms.Controls)
                //{
                //    if (ctl.GetType() == typeof(SearchLookUpEdit))
                //    {
                //        ((SearchLookUpEdit)ctl).EditValue = null;
                //    }

                //    if (ctl.GetType() == typeof(LabelControl) && ctl.Name.Contains("lbltxt") == true)
                //    {
                //        ((LabelControl)ctl).Text = string.Empty;
                //    }
                //}
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Random

        private void SearchPO()
        {
            try
            {
                ResetPO();

                string supplier = slueSupplier.EditValue as string;
                DateTime docDate = dateEditDocDate.DateTime;

                var docs = GlobalSetting.SupplyDocsService.GetDocs(supplier, docDate);

                if (docs.Count == 0)
                {
                    XtraMessageBox.Show("No Data Found", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //else if(docs.Count == 1)
                //{

                //}
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
                    var docs = GlobalSetting.SupplyDocsService.GetDocs((string)slueSupplier.EditValue, dateEditDocDate.DateTime);
                    //string strCont = (docs.Count == 0 ? string.Empty : (docs.Count + 1).ToString());

                    string strCont;
                    if (docs.Count == 0)
                    {
                        strCont = string.Empty;
                        _batchCont = 0;
                    }
                    else
                    {
                        strCont = (docs.Count + 1).ToString();
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

        private int GetWeek(DateTime date)
        {
            try
            {
                var currentCulture = CultureInfo.CurrentCulture;
                var weekNo = currentCulture.Calendar.GetWeekOfYear(
                date,
                currentCulture.DateTimeFormat.CalendarWeekRule,
                currentCulture.DateTimeFormat.FirstDayOfWeek);

                return weekNo;
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
                if (txtPONumber.Text == null)
                    return;

                string poNumber = txtPONumber.Text.Substring(0,5);

                int cont = 1;

                if (_batchCont != null)
                    cont = _batchCont.Value + 1;

                var models = _docLinesList.Select(a => (a.Item as ItemEy)?.Model.Description).Distinct().OrderBy(a => a).ToList();


                foreach (var model in models)
                {
                    foreach(DocLine line in _docLinesList)
                    {
                        if((line.Item as ItemEy)?.Model.Description == model)
                        { 
                            line.Batch = $"{poNumber}{cont.ToString()}";
                        }
                    }

                    cont++;
                }

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
                    if(line.IdItemBcn != null)
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
                    case ActionsStates.New:
                        sbOrder.Visible = true;
                        sbSearch.Visible = false;
                        break;

                    default:
                        sbOrder.Visible = false;
                        sbSearch.Visible = true;
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
                List<DocLine> sortedLines = _docLinesList.Where(lin => lin.IdItemBcn != null).OrderBy(a => a.Batch).ThenBy(b => b.IdItemBcn).ToList();

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
        #endregion

        #region Configure Ribbon Actions
        private void ConfigureActionsStackViewCreating()
        {
            try
            {
                ResetPO();

                _docLinesList = new BindingList<DocLine>();
                _docLinesList.Add(new DocLine() { LineState = DocLine.LineStates.New});

                xgrdLines.DataSource = null;
                xgrdLines.DataSource = _docLinesList;

                //Allow edit all columns
                gridViewLines.OptionsBehavior.Editable = true;

                //Block not editing columns
                gridViewLines.Columns[nameof(DocLine.UnitPrice)].OptionsColumn.AllowEdit = false;
                gridViewLines.Columns[TOTAL_AMOUNT_COLUMN].OptionsColumn.AllowEdit = false;
                gridViewLines.Columns[nameof(DocLine.QuantityOriginal)].OptionsColumn.AllowEdit = false;
                gridViewLines.Columns[$"{nameof(DocLine.Item)}.{nameof(ItemEy.ItemDescription)}"].OptionsColumn.AllowEdit = false;
                gridViewLines.Columns[nameof(DocLine.DeliveredQuantity)].OptionsColumn.AllowEdit = false;
                gridViewLines.Columns[nameof(DocLine.IdSupplyStatus)].OptionsColumn.AllowEdit = false;

                //events
                SetUpEventsEditing();

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
                //agregamos una línea nueva
                _docLinesList.Add(new DocLine() { LineState = DocLine.LineStates.New });
                gridViewLines.RefreshData();

                //Allow edit all columns
                gridViewLines.OptionsBehavior.Editable = true;

                //Block not editing columns
                gridViewLines.Columns[nameof(DocLine.UnitPrice)].OptionsColumn.AllowEdit = false;
                gridViewLines.Columns[TOTAL_AMOUNT_COLUMN].OptionsColumn.AllowEdit = false;
                gridViewLines.Columns[nameof(DocLine.QuantityOriginal)].OptionsColumn.AllowEdit = false;
                gridViewLines.Columns[$"{nameof(DocLine.Item)}.{nameof(ItemEy.ItemDescription)}"].OptionsColumn.AllowEdit = false;
                gridViewLines.Columns[nameof(DocLine.IdSupplyStatus)].OptionsColumn.AllowEdit = false;

                //events
                SetUpEventsEditing();

                //Visible buttons
                SetVisiblePropertyByState();

                //No editing fields
                slueSupplier.ReadOnly = true;
                dateEditDocDate.ReadOnly = true;

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
                if(slueSupplier.EditValue == null)
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

                if (sluePaymentTerm.EditValue == null)
                {
                    MessageBox.Show("Field Required: Term of Payment", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    sluePaymentTerm.Focus();
                    return false;
                }

                if(dateEditDocDate.EditValue == null)
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

                if(_docLinesList.Count == 1)
                {
                    MessageBox.Show("Document without lines", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
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
                List<DocLine> sortedLines = _docLinesList.Where(lin => lin.IdItemBcn != null).OrderBy(a => a.Batch).ThenBy(b => b.IdItemBcn).ToList();

                DocHead purchaseOrder = new DocHead()
                {
                    IdDoc = txtPONumber.Text,
                    IdSupplyDocType = Constants.SUPPLY_DOCTYPE_PO,
                    CreationDate = DateTime.Now,
                    DeliveryDate = dateEditDelivery.DateTime,
                    DocDate = dateEditDocDate.DateTime,
                    IdSupplyStatus = Constants.SUPPLY_STATUS_OPEN,
                    IdSupplier = slueSupplier.EditValue as string,
                    //IdCustomer = //TODO: Etnia a piñon??
                    IdDeliveryTerm = slueDeliveryTerms.EditValue as string,
                    IdPaymentTerms = sluePaymentTerm.EditValue as string,
                    IdCurrency = slueCurrency.EditValue as string,
                    Lines = sortedLines
                };

                DocHead createdDoc = GlobalSetting.SupplyDocsService.NewDoc(purchaseOrder);

                return true;
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
