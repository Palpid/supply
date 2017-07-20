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

        List<Customer> _customersList;
        List<Currency> _currenciesList;

        List<ItemEy> _itemsEyList;

        List<DocLine> _docLinesList;

        #endregion

        #region Constructor
        public PurchaseOrder()
        {
            InitializeComponent();

            try
            {
                ConfigureRibbonActions();
                LoadAuxList();
                SetUpLabels();
                SetUpSearchLookUpEdit();
                SetUpEvents();
                SetUpGrdLines();


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

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Forms Events
        private void PurchaseOrder_Load(object sender, EventArgs e)
        {

        }

        private void DateEditDocDate_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (dateEditDocDate.EditValue != null)
                {
                    var currentCulture = CultureInfo.CurrentCulture;
                    var weekNo = currentCulture.Calendar.GetWeekOfYear(
                        dateEditDocDate.DateTime,
                        currentCulture.DateTimeFormat.CalendarWeekRule,
                        currentCulture.DateTimeFormat.FirstDayOfWeek);

                    lblDocDateWeek.Text = weekNo.ToString();

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
                    var currentCulture = CultureInfo.CurrentCulture;
                    var weekNo = currentCulture.Calendar.GetWeekOfYear(
                        dateEditDelivery.DateTime,
                        currentCulture.DateTimeFormat.CalendarWeekRule,
                        currentCulture.DateTimeFormat.FirstDayOfWeek);

                    lblDeliveryWeek.Text = weekNo.ToString();

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void SlueCustomer_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (slueSupplier.EditValue != null)
                {
                    SetCustomerInfo();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Private Members

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
                lblDocDateWeek.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                lblDeliveryWeek.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                //Terms Tab
                lblCompany.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                lblAddress.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                lblContact.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                lblShipTo.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                lblInvoiceTo.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                lblTermPayment.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;

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
                dateEditDelivery.EditValueChanged += DateEditDelivery_EditValueChanged;
                dateEditDocDate.EditValueChanged += DateEditDocDate_EditValueChanged;
                slueSupplier.EditValueChanged += SlueCustomer_EditValueChanged;
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
                SetUpSlueCustomer();
                SetUpSlueCurrency();
            }
            catch
            {
                throw;
            }
        }

        private void SetUpSlueCustomer()
        {
            try
            {
                slueSupplier.Properties.DataSource = _customersList;
                slueSupplier.Properties.ValueMember = nameof(Customer.IdCustomer);
                slueSupplier.Properties.DisplayMember = nameof(Customer.CustomerName);
                slueSupplier.Properties.View.Columns.AddField(nameof(Customer.IdCustomer)).Visible = true;
                slueSupplier.Properties.View.Columns.AddField(nameof(Customer.CustomerName)).Visible = true;
                slueSupplier.Properties.NullText = "Select Customer...";
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

                //Column Definition
                GridColumn colIdItemBcn = new GridColumn() { Caption = "Item Code", Visible = true, FieldName = nameof(DocLine.IdItemBcn), Width = 200 };
                GridColumn colDescription = new GridColumn() { Caption = "Description", Visible = true, FieldName = $"{nameof(DocLine.Item)}.{nameof(ItemEy.ItemDescription)}", Width = 350 };
                GridColumn colBatch = new GridColumn() { Caption = "Batch", Visible = true, FieldName = nameof(DocLine.Batch), Width = 85 };
                GridColumn colQuantity = new GridColumn() { Caption = "Quantity", Visible = true, FieldName = nameof(DocLine.Quantity), Width =85 };
                GridColumn colUnitPrice = new GridColumn() { Caption = "Unit Price", Visible = true, FieldName = nameof(DocLine.UnitPrice), Width = 85 };
                GridColumn colTotalAmount = new GridColumn() { Caption = "TotalAmount", Visible = true, FieldName = TOTAL_AMOUNT_COLUMN, Width = 120 };
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
                };
                riItems.View.Columns.AddField(nameof(ItemEy.IdItemBcn)).Visible = true;
                riItems.View.Columns.AddField(nameof(ItemEy.ItemDescription)).Visible = true;

                colIdItemBcn.ColumnEdit = riItems;

                RepositoryItemTextEdit ritxtInt = new RepositoryItemTextEdit();
                ritxtInt.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                ritxtInt.Mask.EditMask = "N";
                ritxtInt.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;

                colQuantity.ColumnEdit = ritxtInt;
                colDeliveredQuantity.ColumnEdit = ritxtInt;


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

            }
            catch
            {
                throw;
            }
        }

        private void GridViewLines_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                DocLine row = view.GetRow(view.FocusedRowHandle) as DocLine;

                switch (view.FocusedColumn.FieldName)
                {
                    case nameof(DocLine.IdItemBcn):
                        var idItem = e.Value.ToString();
                        var item = _itemsEyList.Where(a => a.IdItemBcn.Equals(idItem)).Single().Clone();
                        row.Item = item;
                        break;
                }
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Loads
        private void LoadAuxList()
        {
            try
            {
                _customersList = GlobalSetting.CustomerService.GetCustomers();
                _currenciesList = GlobalSetting.CurrencyService.GetCurrencies();
                _itemsEyList = GlobalSetting.ItemEyService.GetItems();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Random
        private void SetCustomerInfo()
        {
            try
            {
                var customer = _customersList.Where(a => a.IdCustomer.Equals(slueSupplier.EditValue.ToString())).FirstOrDefault();

                if (customer != null)
                {
                    lblTxtCompany.Text = customer.CustomerName;
                    lblTxtAddress.Text = customer.ShippingAddress;
                    lblTxtContact.Text = $"{customer.ContactName} ({customer.ContactPhone})";
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
                _docLinesList = new List<DocLine>();
                _docLinesList.Add(new DocLine());

                xgrdLines.DataSource = null;
                xgrdLines.DataSource = _docLinesList;

                //Allow edit all columns
                gridViewLines.OptionsBehavior.Editable = true;

                //Block not editing columns
                gridViewLines.Columns[nameof(DocLine.UnitPrice)].OptionsColumn.AllowEdit = false;
                gridViewLines.Columns[TOTAL_AMOUNT_COLUMN].OptionsColumn.AllowEdit = false;
                gridViewLines.Columns[nameof(DocLine.QuantityOriginal)].OptionsColumn.AllowEdit = false;
                gridViewLines.Columns[$"{nameof(DocLine.Item)}.{nameof(ItemEy.ItemDescription)}"].OptionsColumn.AllowEdit = false;

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
