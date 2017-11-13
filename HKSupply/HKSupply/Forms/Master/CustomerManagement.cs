using DevExpress.Utils;
using DevExpress.XtraEditors;
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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace HKSupply.Forms.Master
{
    public partial class CustomerManagement : RibbonFormBase
    {

        #region Enums
        private enum eCustomerColumns
        {
            IdVer,
            IdSubVer,
            Timestamp,
            IdCustomer,
            CustomerName,
            Active,
            VATNum,
            ShippingAddress,
            BillingAddress,
            ContactName,
            ContactPhone,
            Comments,
            IdIncoterm,
            IdPaymentTerms,
            IdDefaultCurrency,
        }
        #endregion

        #region Private Members

        Customer _customerUpdate;
        Customer _customerOriginal;
        CustomerHistory _customerHistory;

        List<Customer> _customersList;
        List<CustomerHistory> _customerHistoryList;
        List<Currency> _currenciesList;
        List<PaymentTerms> _paymentTermsList;
        List<Incoterm> _incotermsList;

        string[] _nonEditingFields = { "txtIdCustomer", "txtIdVersion", "txtIdSubversion", "txtTimestamp" };

        int _currentHistoryNumList;
        #endregion

        #region Constructor
        public CustomerManagement()
        {
            InitializeComponent();

            try
            {
                ConfigureRibbonActions();
                SetUpGrdCustomers();
                ResetCustomerUpdate();
                SetFormBinding();
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
                _customerOriginal = null;
                ResetCustomerUpdate();
                SetFormBinding();
                xtpForm.PageVisible = false;
                xtpList.PageVisible = true;
                sbNewVersion.Visible = false;
                LoadCustomersList();
                SetNonCreatingFieldsVisibility(LayoutVisibility.Always);

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
                if (_customerOriginal == null)
                {
                    MessageBox.Show(GlobalSetting.ResManager.GetString("NoCustomerSelected"));
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

                if (IsValidCustomer() == false)
                    return;

                DialogResult result = MessageBox.Show(GlobalSetting.ResManager.GetString("SaveChanges"), "", MessageBoxButtons.YesNo);

                if (result != DialogResult.Yes)
                    return;

                Cursor = Cursors.WaitCursor;

                if (_customerUpdate.Equals(_customerOriginal))
                {
                    MessageBox.Show(GlobalSetting.ResManager.GetString("NoPendingChanges"));
                    return;
                }

                if (CurrentState == ActionsStates.Edit)
                {
                    res = UpdateCustomer();

                }
                else if (CurrentState == ActionsStates.New)
                {
                    res = CreateCustomer();
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
            if (rootGridViewCustomers.DataRowCount == 0)
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
                    rootGridViewCustomers.ExportToXlsx(ExportExcelFile);

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
            if (rootGridViewCustomers.DataRowCount == 0)
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
                    rootGridViewCustomers.ExportToCsv(ExportCsvFile);

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

        private void CustomerManagement_Load(object sender, EventArgs e)
        {
            try
            {
                xtpForm.PageVisible = false;
                sbNewVersion.Visible = false;
                LoadIncotemrsList();
                LoadPaymentTermsList();
                LoadCurrenciesList();
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
                LoadCustomersList();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void sbNewVersion_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                Validate();

                if (_customerUpdate.Equals(_customerOriginal))
                {
                    MessageBox.Show(GlobalSetting.ResManager.GetString("NoPendingChanges"));
                    return;
                }

                if (IsValidCustomer() == false)
                    return;

                if (UpdateCustomer(true))
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

        void rootGridViewCustomers_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                GridView view = (GridView)sender;
                object idCustomer = view.GetRowCellValue(view.FocusedRowHandle, eCustomerColumns.IdCustomer.ToString());
                if (idCustomer != null)
                {
                    LoadCustomerForm(idCustomer.ToString());
                    LoadCustomerHistory(idCustomer.ToString());
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void sbForward_Click(object sender, EventArgs e)
        {
            try
            {
                SetCurrentCustomerHistory(_currentHistoryNumList + 1);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void sbBackward_Click(object sender, EventArgs e)
        {
            try
            {
                SetCurrentCustomerHistory(_currentHistoryNumList - 1);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                _customerUpdate = _customerHistory;
                _customerUpdate.IdSubVer = _customerOriginal.IdSubVer;
                _customerUpdate.IdVer = _customerOriginal.IdVer;

                if (UpdateCustomer())
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

                _customerUpdate = _customerHistory;
                _customerUpdate.IdSubVer = _customerOriginal.IdSubVer;
                _customerUpdate.IdVer = _customerOriginal.IdVer;

                if (UpdateCustomer(true))
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

        #endregion

        #region Private Methods

        /// <summary>
        /// Resetear el objeto supplier que usamos para la actualización
        /// </summary>
        private void ResetCustomerUpdate()
        {
            _customerUpdate = new Customer();
        }

        private void SetUpGrdCustomers()
        {
            try
            {
                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                rootGridViewCustomers.OptionsView.ColumnAutoWidth = false;
                rootGridViewCustomers.HorzScrollVisibility = ScrollVisibility.Auto;

                //hacer todo el grid no editable
                rootGridViewCustomers.OptionsBehavior.Editable = false;

                //Columns definition
                GridColumn colIdVer = new GridColumn() { Caption = "Version Id", Visible = true, FieldName = eCustomerColumns.IdVer.ToString(), Width = 70 };
                GridColumn colIdSubVer = new GridColumn() { Caption = "Subversion Id", Visible = true, FieldName = eCustomerColumns.IdSubVer.ToString(), Width = 80 };
                GridColumn colTimestamp = new GridColumn() { Caption = "Timestamp", Visible = true, FieldName = eCustomerColumns.Timestamp.ToString(), Width = 130 };
                GridColumn colIdCustomer = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("IdCustomer"), Visible = true, FieldName = eCustomerColumns.IdCustomer.ToString(), Width = 100 };
                GridColumn colCustomerName = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("CustomerName"), Visible = true, FieldName = eCustomerColumns.CustomerName.ToString(), Width = 200 };
                GridColumn colActive = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Active"), Visible = true, FieldName = eCustomerColumns.Active.ToString(), Width = 50 };
                GridColumn colVATNum = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("VATNumber"), Visible = true, FieldName = eCustomerColumns.VATNum.ToString(), Width = 120 };
                GridColumn colShippingAddress = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("ShippingAddress"), Visible = true, FieldName = eCustomerColumns.ShippingAddress.ToString(), Width = 300 };
                GridColumn colShippingAddress2 = new GridColumn() { Caption = "Shipping Address 2", Visible = true, FieldName = nameof(Customer.ShippingAddress2), Width = 300 };
                GridColumn colBillingAddress = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("BillingAddress"), Visible = true, FieldName = eCustomerColumns.BillingAddress.ToString(), Width = 300 };
                GridColumn colBillingAddress2 = new GridColumn() { Caption = "Billing Address 2", Visible = true, FieldName = nameof(Customer.BillingAddress2), Width = 300 };
                GridColumn colContactName = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("ContactName"), Visible = true, FieldName = eCustomerColumns.ContactName.ToString(), Width = 200 };
                GridColumn colContactPhone = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("ContactPhone"), Visible = true, FieldName = eCustomerColumns.ContactPhone.ToString(), Width = 150 };
                GridColumn colComments = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Comments"), Visible = true, FieldName = eCustomerColumns.Comments.ToString(), Width = 300 };
                GridColumn colIdIncoterm = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Incoterm"), Visible = true, FieldName = eCustomerColumns.IdIncoterm.ToString(), Width = 70 };
                GridColumn colIdPaymentTerms = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("PaymentTerms"), Visible = true, FieldName = eCustomerColumns.IdPaymentTerms.ToString(), Width = 100 };
                GridColumn colCurrency = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Currency"), Visible = true, FieldName = eCustomerColumns.IdDefaultCurrency.ToString(), Width = 70 };

                //Format type 
                colTimestamp.DisplayFormat.FormatType = FormatType.DateTime;

                //add columns to grid root view
                rootGridViewCustomers.Columns.Add(colIdVer);
                rootGridViewCustomers.Columns.Add(colIdSubVer);
                rootGridViewCustomers.Columns.Add(colTimestamp);
                rootGridViewCustomers.Columns.Add(colIdCustomer);
                rootGridViewCustomers.Columns.Add(colCustomerName);
                rootGridViewCustomers.Columns.Add(colActive);
                rootGridViewCustomers.Columns.Add(colVATNum);
                rootGridViewCustomers.Columns.Add(colShippingAddress);
                rootGridViewCustomers.Columns.Add(colShippingAddress2);
                rootGridViewCustomers.Columns.Add(colBillingAddress);
                rootGridViewCustomers.Columns.Add(colBillingAddress2);
                rootGridViewCustomers.Columns.Add(colContactName);
                rootGridViewCustomers.Columns.Add(colContactPhone);
                rootGridViewCustomers.Columns.Add(colComments);
                rootGridViewCustomers.Columns.Add(colIdIncoterm);
                rootGridViewCustomers.Columns.Add(colIdPaymentTerms);
                rootGridViewCustomers.Columns.Add(colCurrency);

                //Events
                rootGridViewCustomers.DoubleClick += rootGridViewCustomers_DoubleClick;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadCurrenciesList()
        {
            try
            {
                _currenciesList = GlobalSetting.CurrencyService.GetCurrencies();

                lueIdDefaultCurrency.Properties.DataSource = _currenciesList;
                lueIdDefaultCurrency.Properties.DisplayMember = nameof(Currency.Description);
                lueIdDefaultCurrency.Properties.ValueMember = nameof(Currency.IdCurrency);
                //lueIdDefaultSupplier.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("IdSupplier", 20, "Id Supplier"));
                //lueIdDefaultSupplier.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SupplierName", 100, "Name"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadPaymentTermsList()
        {
            try
            {
                _paymentTermsList = GlobalSetting.PaymentTermsService.GetPaymentTerms();

                lueIdPaymentTerms.Properties.DataSource = _paymentTermsList;
                lueIdPaymentTerms.Properties.DisplayMember = nameof(PaymentTerms.Description);
                lueIdPaymentTerms.Properties.ValueMember = nameof(PaymentTerms.IdPaymentTerms);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadIncotemrsList()
        {
            try
            {
                _incotermsList = GlobalSetting.IncotermService.GetIIncoterms();

                lueIdIncoterm.Properties.DataSource = _incotermsList;
                lueIdIncoterm.Properties.DisplayMember = nameof(Incoterm.Description);
                lueIdIncoterm.Properties.ValueMember = nameof(Incoterm.IdIncoterm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadCustomersList()
        {
            try
            {
                _customersList = GlobalSetting.CustomerService.GetCustomers();
                xgrdCustomers.DataSource = _customersList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadCustomerForm(string idCustomer)
        {
            try
            {
                _customerUpdate = GlobalSetting.CustomerService.GetCustomerById(idCustomer);
                _customerOriginal = _customerUpdate.Clone();
                SetFormBinding();  //refresh binding 
                xtpForm.PageVisible = true;
                xtcGeneral.SelectedTabPage = xtpForm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadCustomerHistory(string idCustomer)
        {
            try
            {
                _customerHistoryList = GlobalSetting.CustomerService.GetCustomerHistory(idCustomer);
                SetCurrentCustomerHistory(0);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void SetCurrentCustomerHistory(int historyNum)
        {
            try
            {
                if (historyNum >= 0 && historyNum < _customerHistoryList.Count())
                {
                    _customerHistory = _customerHistoryList[historyNum];
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
                    else if (ctl.GetType() == typeof(LookUpEdit))
                    {
                        ctl.DataBindings.Clear();
                        ((LookUpEdit)ctl).ReadOnly = true;
                    }
                }

                //txtIdCustomer.DataBindings.Add("Text", _customerUpdate, "IdCustomer");
                //txtIdVersion.DataBindings.Add("Text", _customerUpdate, "idVer");
                //txtIdSubversion.DataBindings.Add("Text", _customerUpdate, "idSubVer");
                //txtTimestamp.DataBindings.Add("Text", _customerUpdate, "Timestamp");
                //txtName.DataBindings.Add("Text", _customerUpdate, "CustName");
                //chkActive.DataBindings.Add("Checked", _customerUpdate, "Active");
                //txtVatNumber.DataBindings.Add("Text", _customerUpdate, "VATNum");
                //txtShippingAddress.DataBindings.Add("Text", _customerUpdate, "ShippingAddress");
                //txtBillingAddress.DataBindings.Add("Text", _customerUpdate, "BillingAddress");
                //txtContactName.DataBindings.Add("Text", _customerUpdate, "ContactName");
                //txtContactPhone.DataBindings.Add("Text", _customerUpdate, "ContactPhone");
                //txtIntercom.DataBindings.Add("Text", _customerUpdate, "IdIncoterm");
                //txtPaymentTerms.DataBindings.Add("Text", _customerUpdate, "IdPaymentTerms");
                //txtCurreny.DataBindings.Add("Text", _customerUpdate, "Currency");

                //Textedit
                txtIdCustomer.DataBindings.Add<Customer>(_customerUpdate, (Control c) => c.Text, supplier => supplier.IdCustomer);
                txtIdVersion.DataBindings.Add<Customer>(_customerUpdate, (Control c) => c.Text, supplier => supplier.IdVer);
                txtIdSubversion.DataBindings.Add<Customer>(_customerUpdate, (Control c) => c.Text, supplier => supplier.IdSubVer);
                txtTimestamp.DataBindings.Add<Customer>(_customerUpdate, (Control c) => c.Text, supplier => supplier.Timestamp);
                txtName.DataBindings.Add<Customer>(_customerUpdate, (Control c) => c.Text, supplier => supplier.CustomerName);
                txtVatNumber.DataBindings.Add<Customer>(_customerUpdate, (Control c) => c.Text, supplier => supplier.VATNum);
                txtShippingAddress.DataBindings.Add<Customer>(_customerUpdate, (Control c) => c.Text, supplier => supplier.ShippingAddress);
                txtShippingAddress2.DataBindings.Add<Customer>(_customerUpdate, (Control c) => c.Text, supplier => supplier.ShippingAddress2);
                txtShippingAddressZh.DataBindings.Add<Customer>(_customerUpdate, (Control c) => c.Text, supplier => supplier.ShippingAddressZh);
                txtShippingAddressZh2.DataBindings.Add<Customer>(_customerUpdate, (Control c) => c.Text, supplier => supplier.ShippingAddressZh2);
                txtBillingAddress.DataBindings.Add<Customer>(_customerUpdate, (Control c) => c.Text, supplier => supplier.BillingAddress);
                txtBillingAddress2.DataBindings.Add<Customer>(_customerUpdate, (Control c) => c.Text, supplier => supplier.BillingAddress2);
                txtBillingAddressZh.DataBindings.Add<Customer>(_customerUpdate, (Control c) => c.Text, supplier => supplier.BillingAddressZh);
                txtBillingAddressZh2.DataBindings.Add<Customer>(_customerUpdate, (Control c) => c.Text, supplier => supplier.BillingAddressZh2);
                txtContactName.DataBindings.Add<Customer>(_customerUpdate, (Control c) => c.Text, supplier => supplier.ContactName);
                txtContactNameZh.DataBindings.Add<Customer>(_customerUpdate, (Control c) => c.Text, supplier => supplier.ContactNameZh);
                txtContactPhone.DataBindings.Add<Customer>(_customerUpdate, (Control c) => c.Text, supplier => supplier.ContactPhone);
                txtComments.DataBindings.Add<Customer>(_customerUpdate, (Control c) => c.Text, supplier => supplier.Comments);

                //CheckEdit
                chkActive.DataBindings.Add<Customer>(_customerUpdate, (CheckEdit chk) => chk.Checked, supplier => supplier.Active);

                //LookUpEdit
                lueIdIncoterm.DataBindings.Add<Customer>(_customerUpdate, (LookUpEdit e) => e.EditValue, supplier => supplier.IdIncoterm);
                lueIdPaymentTerms.DataBindings.Add<Customer>(_customerUpdate, (LookUpEdit e) => e.EditValue, supplier => supplier.IdPaymentTerms);
                lueIdDefaultCurrency.DataBindings.Add<Customer>(_customerUpdate, (LookUpEdit e) => e.EditValue, supplier => supplier.IdDefaultCurrency);
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
                foreach (Control ctl in layoutControlHistory.Controls)
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
                txtHIdCustomer.DataBindings.Add<CustomerHistory>(_customerHistory, (Control c) => c.Text, customer => customer.IdCustomer);
                txtHIdVersion.DataBindings.Add<CustomerHistory>(_customerHistory, (Control c) => c.Text, customer => customer.IdVer);
                txtHIdSubversion.DataBindings.Add<CustomerHistory>(_customerHistory, (Control c) => c.Text, customer => customer.IdSubVer);
                txtHTimestamp.DataBindings.Add<CustomerHistory>(_customerHistory, (Control c) => c.Text, customer => customer.Timestamp);
                txtHName.DataBindings.Add<CustomerHistory>(_customerHistory, (Control c) => c.Text, customer => customer.CustomerName);
                txtHVATNumber.DataBindings.Add<CustomerHistory>(_customerHistory, (Control c) => c.Text, customer => customer.VATNum);
                txtHShippingAddress.DataBindings.Add<CustomerHistory>(_customerHistory, (Control c) => c.Text, customer => customer.ShippingAddress);
                txtHShippingAddress2.DataBindings.Add<CustomerHistory>(_customerHistory, (Control c) => c.Text, customer => customer.ShippingAddress2);
                txtHShippingAddressZh.DataBindings.Add<CustomerHistory>(_customerHistory, (Control c) => c.Text, customer => customer.ShippingAddressZh);
                txtHShippingAddressZh2.DataBindings.Add<CustomerHistory>(_customerHistory, (Control c) => c.Text, customer => customer.ShippingAddressZh2);
                txtHBillingAddress.DataBindings.Add<CustomerHistory>(_customerHistory, (Control c) => c.Text, customer => customer.BillingAddress);
                txtHBillingAddress2.DataBindings.Add<CustomerHistory>(_customerHistory, (Control c) => c.Text, customer => customer.BillingAddress2);
                txtHBillingAddressZh.DataBindings.Add<CustomerHistory>(_customerHistory, (Control c) => c.Text, customer => customer.BillingAddressZh);
                txtHBillingAddressZh2.DataBindings.Add<CustomerHistory>(_customerHistory, (Control c) => c.Text, customer => customer.BillingAddressZh2);
                txtHContactName.DataBindings.Add<CustomerHistory>(_customerHistory, (Control c) => c.Text, customer => customer.ContactName);
                txtHContactNameZh.DataBindings.Add<CustomerHistory>(_customerHistory, (Control c) => c.Text, customer => customer.ContactNameZh);
                txtHContactPhone.DataBindings.Add<CustomerHistory>(_customerHistory, (Control c) => c.Text, customer => customer.ContactPhone);
                txtHComments.DataBindings.Add<CustomerHistory>(_customerHistory, (Control c) => c.Text, customer => customer.Comments);

                txtHIdIncoterm.DataBindings.Add<CustomerHistory>(_customerHistory, (Control c) => c.Text, customer => customer.IdIncoterm);
                txtHIdPaymentTerms.DataBindings.Add<CustomerHistory>(_customerHistory, (Control c) => c.Text, customer => customer.IdPaymentTerms);
                txtHIdDefaultCurrency.DataBindings.Add<CustomerHistory>(_customerHistory, (Control c) => c.Text, customer => customer.IdDefaultCurrency);

                //CheckEdit
                chkHActive.DataBindings.Add<CustomerHistory>(_customerHistory, (CheckEdit chk) => chk.Checked, customer => customer.Active);

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
                xtpList.PageVisible = true;
                sbNewVersion.Visible = true;
                SetEditingFieldsEnabled();
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
                ResetCustomerUpdate();
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

        /// <summary>
        /// Mover la fila activa a la de un supplier en concreto
        /// </summary>
        /// <param name="idSupplier"></param>
        private void MoveGridToSupplier(string idSupplier)
        {
            try
            {
                GridColumn column = rootGridViewCustomers.Columns[eCustomerColumns.IdCustomer.ToString()];
                if (column != null)
                {
                    // locating the row 
                    int rhFound = rootGridViewCustomers.LocateByDisplayText(rootGridViewCustomers.FocusedRowHandle + 1, column, idSupplier);
                    // focusing the cell 
                    if (rhFound != GridControl.InvalidRowHandle)
                    {
                        rootGridViewCustomers.FocusedRowHandle = rhFound;
                        rootGridViewCustomers.FocusedColumn = column;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsValidCustomer()
        {
            try
            {
                foreach (Control ctl in layoutControlForm.Controls)
                {
                    if (ctl.GetType() == typeof(TextEdit))
                    {
                        if (string.IsNullOrEmpty(((TextEdit)ctl).Text))
                        {
                            MessageBox.Show(string.Format(GlobalSetting.ResManager.GetString("NullArgument"), ctl.Name));
                            return false;
                        }
                    }
                    if (ctl.GetType() == typeof(LookUpEdit))
                    {
                        if (string.IsNullOrEmpty(((LookUpEdit)ctl).Text))
                        {
                            MessageBox.Show(string.Format(GlobalSetting.ResManager.GetString("NullArgument"), ctl.Name));
                            return false;
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

        #region Create/Update

        /// <summary>
        /// Actualizar los datos de un customer
        /// </summary>
        /// <param name="newVersion">Si es una versión nueva o una actualización de la existente</param>
        /// <returns></returns>
        private bool UpdateCustomer(bool newVersion = false)
        {
            try
            {
                return GlobalSetting.CustomerService.UpdateCustomer(_customerUpdate, newVersion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Crear un nuevo Customer
        /// </summary>
        /// <returns></returns>
        private bool CreateCustomer()
        {
            try
            {
                _customerOriginal = _customerUpdate.Clone();
                return GlobalSetting.CustomerService.NewCustomer(_customerUpdate);
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
                string id = _customerOriginal.IdCustomer;
                _customerOriginal = null;
                ResetCustomerUpdate();
                SetFormBinding();
                xtpForm.PageVisible = false;
                xtpList.PageVisible = true;
                LoadCustomersList();
                MoveGridToSupplier(id);
                RestoreInitState();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #endregion
    }
}
