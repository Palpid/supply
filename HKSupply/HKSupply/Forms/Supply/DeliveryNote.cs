﻿using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraEditors;
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
using HKSupply.Reports;
using HKSupply.Styles;

namespace HKSupply.Forms.Supply
{
    public partial class DeliveryNote : RibbonFormBase
    {

        #region Enums
        enum eGridLinesSummaries
        {
            totalQuantityMt,
            totalQuantityHw
        }
        #endregion

        #region Private Members

        Font _labelDefaultFontBold = AppStyles.LabelDefaultFontBold;
        Font _labelDefaultFont = AppStyles.LabelDefaultFont;

        List<Customer> _customersList;
        List<Currency> _currenciesList;
        List<DeliveryTerm> _deliveryTermList;
        List<PaymentTerms> _paymentTermsList;

        DocHead _docDeliveyNote;
        BindingList<DocLine> _docLinesDeliveyNoteList;


        decimal _totalQuantityMt;
        int _totalQuantityHw;

        #endregion

        #region Constructor
        public DeliveryNote()
        {
            InitializeComponent();

            try
            {
                Cursor = Cursors.WaitCursor;

                ConfigureRibbonActions();
                LoadAuxList();
                SetUpTabs();
                SetUpLabels();
                SetUpButtons();
                SetUpSearchLookUpEdit();
                SetUpEvents();
                SetUpGrdLines();
                SetupPanelControl();

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
                    gridViewLines.OptionsPrint.PrintFooter = false;
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
                    gridViewLines.OptionsPrint.PrintFooter = false;
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

        public override void BarButtonItemReport_ItemClick(object sender, ItemClickEventArgs e)
        {
            base.BarButtonItemReport_ItemClick(sender, e);

            try
            {
                if (_docDeliveyNote != null)
                {
                    FunctionalityReport tmp = e.Item.Tag as FunctionalityReport;
                    OpenReport(_docDeliveyNote.IdDoc, tmp.ReportFile);
                }
                else
                {
                    XtraMessageBox.Show("No Delivery Note Selected", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Form Events
        private void DeliveryNote_Load(object sender, EventArgs e)
        {

        }

        private void SbSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDNNumber.Text))
                {
                    XtraMessageBox.Show("Type Delivery Note number", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    SearchDeliveryNote();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtDNNumber_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                ResetCurrentDeliveryNote();
                ResetForm();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtDNNumber_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter && txtDNNumber.EditValue != null)
                {
                    SearchDeliveryNote();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GridViewLines_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            try
            {
                // Get the summary ID. 
                eGridLinesSummaries summaryID = (eGridLinesSummaries)(e.Item as GridSummaryItem).Tag;
                GridView view = sender as GridView;

                // Initialization 
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    _totalQuantityHw = 0;
                    _totalQuantityMt = 0;
                }

                // Calculation 
                if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DocLine row = view.GetRow(e.RowHandle) as DocLine;

                    switch (summaryID)
                    {
                        case eGridLinesSummaries.totalQuantityMt:

                            if (row.IdItemGroup == Constants.ITEM_GROUP_MT)
                                _totalQuantityMt += Convert.ToDecimal(e.FieldValue);
                            break;

                        case eGridLinesSummaries.totalQuantityHw:

                            if (row.IdItemGroup == Constants.ITEM_GROUP_HW)
                                _totalQuantityHw += Convert.ToInt32(e.FieldValue);
                            break;
                    }
                }

                // Finalization
                if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    switch (summaryID)
                    {
                        case eGridLinesSummaries.totalQuantityMt:
                            e.TotalValue = _totalQuantityMt;
                            break;
                        case eGridLinesSummaries.totalQuantityHw:
                            e.TotalValue = _totalQuantityHw;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Public Methods
        public void InitData(string idDoc)
        {
            try
            {
                txtDNNumber.EditValue = idDoc;
                SearchDeliveryNote();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Private Methods

        #region Loads/Resets

        private void LoadAuxList()
        {
            try
            {
                _customersList = GlobalSetting.CustomerService.GetCustomers();
                _currenciesList = GlobalSetting.CurrencyService.GetCurrencies();
                _deliveryTermList = GlobalSetting.DeliveryTermsService.GetDeliveryTerms();
                _paymentTermsList = GlobalSetting.PaymentTermsService.GetPaymentTerms();
            }
            catch
            {
                throw;
            }
        }

        private void ResetCurrentDeliveryNote()
        {
            try
            {
                _docDeliveyNote = new DocHead();
                _docLinesDeliveyNoteList = new BindingList<DocLine>();
                xgrdLines.DataSource = null;

            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Setup Form Objects

        private void SetUpTabs()
        {
            try
            {
                xtpDN.AutoScroll = true;
                xtpDN.AutoScrollMargin = new Size(20, 20);
                xtpDN.AutoScrollMinSize = new Size(xtpDN.Width, xtpDN.Height);
            }
            catch
            {
                throw;
            }
        }

        private void SetUpLabels()
        {
            try
            {
                /********* Fonts **********/
                //Header 
                lblPKNumber.Font = _labelDefaultFontBold;
                lblDNNumber.Font = _labelDefaultFontBold;
                lblDate.Font = _labelDefaultFontBold;
                lblWeek.Font = _labelDefaultFontBold;
                lblDNCreationDate.Font = _labelDefaultFontBold;
                lblDNDeliveryDate.Font = _labelDefaultFontBold;
                lblCustomer.Font = _labelDefaultFontBold;
                lblDNCreationDateWeek.Font = _labelDefaultFont;
                lblDNDeliveryDateWeek.Font = _labelDefaultFont;
                txtPKNumber.Font = _labelDefaultFontBold;
                txtDNNumber.Font = _labelDefaultFontBold;
                lblTermsOfDelivery.Font = _labelDefaultFontBold;
                lblCurrency.Font = _labelDefaultFontBold;
                lblManualReference.Font = _labelDefaultFont;
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
                lblDNNumber.Text = "DN Number";
                lblDate.Text = "Date";
                lblWeek.Text = "Week";
                lblDNCreationDate.Text = "CREATION";
                lblDNDeliveryDate.Text = "DELIVERY";
                lblCustomer.Text = "CUSTOMER";
                lblDNCreationDateWeek.Text = string.Empty;
                lblDNDeliveryDateWeek.Text = string.Empty;
                lblTermsOfDelivery.Text = "Terms of Delivery";
                lblCurrency.Text = "Currency";
                lblManualReference.Text = "Manual Reference";
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
                lblDNCreationDateWeek.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                lblDNDeliveryDateWeek.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                txtPKNumber.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                txtDNNumber.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;

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
                txtDNNumber.Properties.Appearance.BackColor = Color.LawnGreen;
                txtDNNumber.Properties.Appearance.BackColor2 = Color.LawnGreen;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Pone como readonly los objetos relaciones con la cabecera del documento. Es una pantalla de visualización principalmente, no hay edición en ella.
        /// </summary>
        /// <remarks>Son pocos, los pongo a mano en lugar de hacer en bucle.</remarks>
        private void SetObjectsReadOnly()
        {
            try
            {
                /********* Headers **********/
                txtPKNumber.ReadOnly = true; 
                dateEditDNCreation.ReadOnly = true;
                dateEditDNDelivery.ReadOnly = true;
                slueCustomer.ReadOnly = true;
                slueDeliveryTerms.ReadOnly = true;
                slueCurrency.ReadOnly = true;
                txtManualReference.ReadOnly = true;
                memoEditRemarks.ReadOnly = true;

                /********* Terms Tab **********/
                sluePaymentTerm.ReadOnly = true;
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
                SetUpSlueCustomer();
                SetUpSlueCurrency();
                SetUpSlueDeliveryTerms();
                SetUpSluePaymentTerms();
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
                slueCustomer.Properties.DataSource = _customersList;
                slueCustomer.Properties.ValueMember = nameof(Customer.IdCustomer);
                slueCustomer.Properties.DisplayMember = nameof(Customer.CustomerName);
                slueCustomer.Properties.View.Columns.AddField(nameof(Customer.IdCustomer)).Visible = true;
                slueCustomer.Properties.View.Columns.AddField(nameof(Customer.CustomerName)).Visible = true;
                slueCustomer.Properties.NullText = string.Empty;
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
                txtDNNumber.EditValueChanged += TxtDNNumber_EditValueChanged;
                txtDNNumber.KeyDown += TxtDNNumber_KeyDown;
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

                //Column Definition
                GridColumn colIdDocRelated = new GridColumn() { Caption = "Sales Order", Visible = true, FieldName = nameof(DocLine.IdDocRelated), Width = 200 };
                GridColumn colIdItemBcn = new GridColumn() { Caption = "Item Code", Visible = true, FieldName = nameof(DocLine.IdItemBcn), Width = 200 };
                GridColumn colDescription = new GridColumn() { Caption = "Description", Visible = true, FieldName = nameof(DocLine.ItemDesc), Width = 350 };
                GridColumn colIdItemGroup = new GridColumn() { Caption = "Type", Visible = true, FieldName = nameof(DocLine.IdItemGroup), Width = 100 };
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

                //Summaries
                gridViewLines.OptionsView.ShowFooter = true;

                colTotalAmount.Summary.Add(SummaryItemType.Sum, nameof(DocLine.TotalAmount), "{0:n4}");

                colQuantity.Summary.AddRange(new GridSummaryItem[] {
                    new GridColumnSummaryItem(SummaryItemType.Custom, nameof(DocLine.Quantity), "{0:n3} Kg", eGridLinesSummaries.totalQuantityMt),
                    new GridColumnSummaryItem(SummaryItemType.Custom, nameof(DocLine.Quantity), "{0} PC", eGridLinesSummaries.totalQuantityHw) });

                //Add columns to grid root view
                gridViewLines.Columns.Add(colIdDocRelated);
                gridViewLines.Columns.Add(colIdItemBcn);
                gridViewLines.Columns.Add(colDescription);
                gridViewLines.Columns.Add(colIdItemGroup);
                gridViewLines.Columns.Add(colQuantity);
                gridViewLines.Columns.Add(colUnit);
                gridViewLines.Columns.Add(colUnitPrice);
                gridViewLines.Columns.Add(colTotalAmount);
                gridViewLines.Columns.Add(colRemarks);

                //Events
                gridViewLines.CustomSummaryCalculate += GridViewLines_CustomSummaryCalculate;
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

        private void ResetForm()
        {
            try
            {
                /********* Head *********/
                txtPKNumber.EditValue = null;
                dateEditDNCreation.EditValue = null;
                dateEditDNDelivery.EditValue = null;
                lblDNCreationDateWeek.Text = string.Empty;
                lblDNDeliveryDateWeek.Text = string.Empty;
                slueCustomer.EditValue = null;
                slueDeliveryTerms.EditValue = null;
                slueCurrency.EditValue = null;
                txtManualReference.EditValue = null;
                memoEditRemarks.EditValue = null;

                /********* Terms Tab *********/
                sluePaymentTerm.EditValue = null;
                lblTxtCompany.Text = string.Empty;
                lblTxtAddress.Text = string.Empty;
                lblTxtContact.Text = string.Empty;
                lblTxtShipTo.Text = string.Empty;
                lblTxtInvoiceTo.Text = string.Empty;
            }
            catch
            {
                throw;
            }
        }

        private void SetFormDocHeadInfo()
        {
            try
            {
                var customer = _customersList.Where(a => a.IdCustomer.Equals(_docDeliveyNote.IdCustomer)).FirstOrDefault();

                /********* Head *********/
                txtPKNumber.EditValue = _docDeliveyNote.IdDoc.Replace(Constants.SUPPLY_DOCTYPE_DN, Constants.SUPPLY_DOCTYPE_PL); //TODO: Esto es un poco chustero...
                dateEditDNCreation.EditValue = _docDeliveyNote.DocDate;
                dateEditDNDelivery.EditValue = _docDeliveyNote.DeliveryDate;
                lblDNCreationDateWeek.Text = _docDeliveyNote.DocDate.GetWeek().ToString();
                lblDNDeliveryDateWeek.Text = _docDeliveyNote.DeliveryDate.GetWeek().ToString();
                slueCustomer.EditValue = _docDeliveyNote.IdCustomer;
                slueDeliveryTerms.EditValue = _docDeliveyNote.IdDeliveryTerm;
                slueCurrency.EditValue = _docDeliveyNote.IdCurrency;
                txtManualReference.EditValue = _docDeliveyNote.ManualReference;
                memoEditRemarks.EditValue = _docDeliveyNote.Remarks;

                /********* Terms Tab *********/
                sluePaymentTerm.EditValue = _docDeliveyNote.IdPaymentTerms;
                lblTxtCompany.Text = customer.CustomerName;
                lblTxtAddress.Text = customer.ShippingAddress;
                lblTxtContact.Text = $"{customer.ContactName} ({customer.ContactPhone})";
                lblTxtShipTo.Text = "??";
                lblTxtInvoiceTo.Text = "??";
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region CRUD

        private void SearchDeliveryNote()
        {
            try
            {
                ResetCurrentDeliveryNote();
                ResetForm();

                string dnNumber = txtDNNumber.Text;

                _docDeliveyNote = GlobalSetting.SupplyDocsService.GetDoc(dnNumber);

                if (_docDeliveyNote == null)
                {
                    XtraMessageBox.Show("No Data Found", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (_docDeliveyNote.IdSupplyDocType != Constants.SUPPLY_DOCTYPE_DN)
                {
                    XtraMessageBox.Show("Document is not a Delivery Note", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //Form
                    SetFormDocHeadInfo();
                    //Grid
                    _docLinesDeliveyNoteList = new BindingList<DocLine>(_docDeliveyNote.Lines);
                    xgrdLines.DataSource = null;
                    xgrdLines.DataSource = _docLinesDeliveyNoteList;
                }

            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Crystal Reports

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
