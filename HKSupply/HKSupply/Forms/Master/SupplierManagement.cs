using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraLayout;
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
    public partial class SupplierManagement : RibbonFormBase
    {
        #region Enums
        private enum eSupplierColumns
        {
            IdVer,
            IdSubVer,
            Timestamp,
            IdSupplier,
            SupplierName,
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

        Supplier _supplierUpdate;
        Supplier _supplierOriginal;
        SupplierHistory _supplierHistory;

        List<Supplier> _suppliersList;
        List<SupplierHistory> _supplierHistoryList;
        List<Currency> _currenciesList;
        List<PaymentTerms> _paymentTermsList;
        List<Incoterm> _incotermsList;

        string[] _nonEditingFields = { "txtIdSupplier", "txtIdVersion", "txtIdSubversion", "txtTimestamp" };

        int _currentHistoryNumList;
        #endregion

        #region Constructor
        public SupplierManagement()
        {
            InitializeComponent();

            try
            {
                ConfigureRibbonActions();
                SetUpGrdSuppliers();
                ResetSupplierUpdate();
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
                _supplierOriginal = null;
                ResetSupplierUpdate();
                SetFormBinding();
                xtpForm.PageVisible = false;
                xtpList.PageVisible = true;
                sbNewVersion.Visible = false;
                LoadSuppliersList();
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
                if (_supplierOriginal == null)
                {
                    MessageBox.Show("No supplier selected");
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

                if (IsValidSupplier() == false)
                    return;

                DialogResult result = MessageBox.Show(GlobalSetting.ResManager.GetString("SaveChanges"), "", MessageBoxButtons.YesNo);

                if (result != DialogResult.Yes)
                    return;

                Cursor = Cursors.WaitCursor;

                if (_supplierUpdate.Equals(_supplierOriginal))
                {
                    MessageBox.Show(GlobalSetting.ResManager.GetString("NoPendingChanges"));
                    return;
                }

                if (CurrentState == ActionsStates.Edit)
                {
                    res = UpdateSupplier();

                }
                else if (CurrentState == ActionsStates.New)
                {
                    res = CreateSupplier();
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
            if (rootGridViewSuppliers.DataRowCount == 0)
            {
                MessageBox.Show("No data selected");
                return;
            }

            //Abre el dialog de save as
            base.bbiExportExcel_ItemClick(sender, e);

            try
            {
                if (string.IsNullOrEmpty(ExportExcelFile) == false)
                {
                    rootGridViewSuppliers.ExportToXlsx(ExportExcelFile);

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
            if (rootGridViewSuppliers.DataRowCount == 0)
            {
                MessageBox.Show("No data selected");
                return;
            }

            //Abre el dialog de save as
            base.bbiExportCsv_ItemClick(sender, e);

            try
            {
                if (string.IsNullOrEmpty(ExportCsvFile) == false)
                {
                    rootGridViewSuppliers.ExportToCsv(ExportCsvFile);

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

        #region Form events

        private void SupplierManagement_Load(object sender, EventArgs e)
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
                LoadSuppliersList();
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

                if (_supplierUpdate.Equals(_supplierOriginal))
                {
                    MessageBox.Show(GlobalSetting.ResManager.GetString("NoPendingChanges"));
                    return;
                }

                if (IsValidSupplier() == false)
                    return;

                if (UpdateSupplier(true))
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

        void rootGridViewSuppliers_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                GridView view = (GridView)sender;
                object idSupplier = view.GetRowCellValue(view.FocusedRowHandle, eSupplierColumns.IdSupplier.ToString());
                if (idSupplier != null)
                {
                    LoadSupplierForm(idSupplier.ToString());
                    LoadSupplierHistory(idSupplier.ToString());
                }
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
                SetCurrentSupplierHistory(_currentHistoryNumList + 1);
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
                SetCurrentSupplierHistory(_currentHistoryNumList - 1);
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

                _supplierUpdate = _supplierHistory;
                _supplierUpdate.IdSubVer = _supplierOriginal.IdSubVer;
                _supplierUpdate.IdVer = _supplierOriginal.IdVer;
                
                if (UpdateSupplier())
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

                _supplierUpdate = _supplierHistory;
                _supplierUpdate.IdSubVer = _supplierOriginal.IdSubVer;
                _supplierUpdate.IdVer = _supplierOriginal.IdVer;

                if (UpdateSupplier(true))
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
        private void ResetSupplierUpdate()
        {
            _supplierUpdate = new Supplier();
        }

        private void SetUpGrdSuppliers()
        {
            try
            {
                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                rootGridViewSuppliers.OptionsView.ColumnAutoWidth = false;
                rootGridViewSuppliers.HorzScrollVisibility = ScrollVisibility.Auto;

                //hacer todo el grid no editable
                rootGridViewSuppliers.OptionsBehavior.Editable = false; 

                //Columns definition
                GridColumn colIdVer = new GridColumn() { Caption = "Version Id", Visible = true, FieldName = eSupplierColumns.IdVer.ToString(), Width = 70 };
                GridColumn colIdSubVer = new GridColumn() { Caption = "Subversion Id", Visible = true, FieldName = eSupplierColumns.IdSubVer.ToString(), Width = 80 };
                GridColumn colTimestamp = new GridColumn() { Caption = "Timestamp", Visible = true, FieldName = eSupplierColumns.Timestamp.ToString(), Width = 130 };
                GridColumn colIdSupplier = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("IdSupplier"), Visible = true, FieldName = eSupplierColumns.IdSupplier.ToString(), Width = 100 };
                GridColumn colSupplierName = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("SupplierName"), Visible = true, FieldName = eSupplierColumns.SupplierName.ToString(), Width = 200 };
                GridColumn colActive = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Active"), Visible = true, FieldName = eSupplierColumns.Active.ToString(), Width = 50 };
                GridColumn colVATNum = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("VATNumber"), Visible = true, FieldName = eSupplierColumns.VATNum.ToString(), Width = 120 };
                GridColumn colShippingAddress = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("ShippingAddress"), Visible = true, FieldName = eSupplierColumns.ShippingAddress.ToString(), Width = 300 };
                GridColumn colShippingAddress2 = new GridColumn() { Caption = "Shipping Address 2", Visible = true, FieldName = nameof(Supplier.ShippingAddress2), Width = 300 };
                GridColumn colBillingAddress = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("BillingAddress"), Visible = true, FieldName = eSupplierColumns.BillingAddress.ToString(), Width = 300 };
                GridColumn colBillingAddress2 = new GridColumn() { Caption = "Billing Address 2", Visible = true, FieldName = nameof(Supplier.BillingAddress2), Width = 300 };
                GridColumn colContactName = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("ContactName"), Visible = true, FieldName = eSupplierColumns.ContactName.ToString(), Width = 200 };
                GridColumn colContactPhone = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("ContactPhone"), Visible = true, FieldName = eSupplierColumns.ContactPhone.ToString(), Width = 150 };
                GridColumn colIdIncoterm = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Incoterm"), Visible = true, FieldName = eSupplierColumns.IdIncoterm.ToString(), Width = 70 };
                GridColumn colIdPaymentTerms = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("PaymentTerms"), Visible = true, FieldName = eSupplierColumns.IdPaymentTerms.ToString(), Width = 100 };
                GridColumn colCurrency = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Currency"), Visible = true, FieldName = eSupplierColumns.IdDefaultCurrency.ToString(), Width = 70 };

                //Format type 
                colTimestamp.DisplayFormat.FormatType = FormatType.DateTime;

                //add columns to grid root view
                rootGridViewSuppliers.Columns.Add(colIdVer);
                rootGridViewSuppliers.Columns.Add(colIdSubVer);
                rootGridViewSuppliers.Columns.Add(colTimestamp);
                rootGridViewSuppliers.Columns.Add(colIdSupplier);
                rootGridViewSuppliers.Columns.Add(colSupplierName);
                rootGridViewSuppliers.Columns.Add(colActive);
                rootGridViewSuppliers.Columns.Add(colVATNum);
                rootGridViewSuppliers.Columns.Add(colShippingAddress);
                rootGridViewSuppliers.Columns.Add(colShippingAddress2);
                rootGridViewSuppliers.Columns.Add(colBillingAddress);
                rootGridViewSuppliers.Columns.Add(colBillingAddress2);
                rootGridViewSuppliers.Columns.Add(colContactName);
                rootGridViewSuppliers.Columns.Add(colContactPhone);
                rootGridViewSuppliers.Columns.Add(colIdIncoterm);
                rootGridViewSuppliers.Columns.Add(colIdPaymentTerms);
                rootGridViewSuppliers.Columns.Add(colCurrency);

                //Events
                rootGridViewSuppliers.DoubleClick += rootGridViewSuppliers_DoubleClick;
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

        private void LoadSuppliersList()
        {
            try
            {
                _suppliersList = GlobalSetting.SupplierService.GetSuppliers();

                xgrdSuppliers.DataSource = _suppliersList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadSupplierForm(string idSupplier)
        {
            try
            {
                _supplierUpdate = GlobalSetting.SupplierService.GetSupplierById(idSupplier);
                _supplierOriginal = _supplierUpdate.Clone();
                SetFormBinding();  //refresh binding 
                xtpForm.PageVisible = true;
                xtcGeneral.SelectedTabPage = xtpForm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadSupplierHistory(string idSupplier)
        {
            try
            {
                _supplierHistoryList = GlobalSetting.SupplierService.GetSupplierHistory(idSupplier);
                SetCurrentSupplierHistory(0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        private void SetCurrentSupplierHistory(int historyNum)
        {
            try
            {
                if (historyNum >= 0 && historyNum < _supplierHistoryList.Count())
                {
                    _supplierHistory = _supplierHistoryList[historyNum];
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
                txtIdSupplier.DataBindings.Add<Supplier>(_supplierUpdate, (Control c) => c.Text, supplier => supplier.IdSupplier);
                txtIdVersion.DataBindings.Add<Supplier>(_supplierUpdate, (Control c) => c.Text, supplier => supplier.IdVer);
                txtIdSubversion.DataBindings.Add<Supplier>(_supplierUpdate, (Control c) => c.Text, supplier => supplier.IdSubVer);
                txtTimestamp.DataBindings.Add<Supplier>(_supplierUpdate, (Control c) => c.Text, supplier => supplier.Timestamp);
                txtName.DataBindings.Add<Supplier>(_supplierUpdate, (Control c) => c.Text, supplier => supplier.SupplierName);
                txtVatNumber.DataBindings.Add<Supplier>(_supplierUpdate, (Control c) => c.Text, supplier => supplier.VATNum);
                txtShippingAddress.DataBindings.Add<Supplier>(_supplierUpdate, (Control c) => c.Text, supplier => supplier.ShippingAddress);
                txtShippingAddress2.DataBindings.Add<Supplier>(_supplierUpdate, (Control c) => c.Text, supplier => supplier.ShippingAddress2);
                txtShippingAddressZh.DataBindings.Add<Supplier>(_supplierUpdate, (Control c) => c.Text, supplier => supplier.ShippingAddressZh);
                txtShippingAddressZh2.DataBindings.Add<Supplier>(_supplierUpdate, (Control c) => c.Text, supplier => supplier.ShippingAddressZh2);
                txtBillingAddress.DataBindings.Add<Supplier>(_supplierUpdate, (Control c) => c.Text, supplier => supplier.BillingAddress);
                txtBillingAddress2.DataBindings.Add<Supplier>(_supplierUpdate, (Control c) => c.Text, supplier => supplier.BillingAddress2);
                txtBillingAddressZh.DataBindings.Add<Supplier>(_supplierUpdate, (Control c) => c.Text, supplier => supplier.BillingAddressZh);
                txtBillingAddressZh2.DataBindings.Add<Supplier>(_supplierUpdate, (Control c) => c.Text, supplier => supplier.BillingAddressZh2);
                txtContactName.DataBindings.Add<Supplier>(_supplierUpdate, (Control c) => c.Text, supplier => supplier.ContactName);
                txtContactNameZh.DataBindings.Add<Supplier>(_supplierUpdate, (Control c) => c.Text, supplier => supplier.ContactNameZh);
                txtContactPhone.DataBindings.Add<Supplier>(_supplierUpdate, (Control c) => c.Text, supplier => supplier.ContactPhone);
                txtComments.DataBindings.Add<Supplier>(_supplierUpdate, (Control c) => c.Text, supplier => supplier.Comments);

                //CheckEdit
                chkActive.DataBindings.Add<Supplier>(_supplierUpdate, (CheckEdit chk) => chk.Checked, supplier => supplier.Active);

                //LookUpEdit
                lueIdIncoterm.DataBindings.Add<Supplier>(_supplierUpdate, (LookUpEdit e) => e.EditValue, supplier => supplier.IdIncoterm);
                lueIdPaymentTerms.DataBindings.Add<Supplier>(_supplierUpdate, (LookUpEdit e) => e.EditValue, supplier => supplier.IdPaymentTerms);
                lueIdDefaultCurrency.DataBindings.Add<Supplier>(_supplierUpdate, (LookUpEdit e) => e.EditValue, supplier => supplier.IdDefaultCurrency);

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
                txtHIdSupplier.DataBindings.Add<Supplier>(_supplierHistory, (Control c) => c.Text, supplier => supplier.IdSupplier);
                txtHIdVersion.DataBindings.Add<Supplier>(_supplierHistory, (Control c) => c.Text, supplier => supplier.IdVer);
                txtHIdSubversion.DataBindings.Add<Supplier>(_supplierHistory, (Control c) => c.Text, supplier => supplier.IdSubVer);
                txtHTimestamp.DataBindings.Add<Supplier>(_supplierHistory, (Control c) => c.Text, supplier => supplier.Timestamp);
                txtHName.DataBindings.Add<Supplier>(_supplierHistory, (Control c) => c.Text, supplier => supplier.SupplierName);
                txtHVatNumber.DataBindings.Add<Supplier>(_supplierHistory, (Control c) => c.Text, supplier => supplier.VATNum);
                txtHShippingAddress.DataBindings.Add<Supplier>(_supplierHistory, (Control c) => c.Text, supplier => supplier.ShippingAddress);
                txtHShippingAddress2.DataBindings.Add<Supplier>(_supplierHistory, (Control c) => c.Text, supplier => supplier.ShippingAddress2);
                txtHShippingAddressZh.DataBindings.Add<Supplier>(_supplierHistory, (Control c) => c.Text, supplier => supplier.ShippingAddressZh);
                txtHShippingAddressZh2.DataBindings.Add<Supplier>(_supplierHistory, (Control c) => c.Text, supplier => supplier.ShippingAddressZh2);
                txtHBillingAddress.DataBindings.Add<Supplier>(_supplierHistory, (Control c) => c.Text, supplier => supplier.BillingAddress);
                txtHBillingAddress2.DataBindings.Add<Supplier>(_supplierHistory, (Control c) => c.Text, supplier => supplier.BillingAddress2);
                txtHBillingAddressZh.DataBindings.Add<Supplier>(_supplierHistory, (Control c) => c.Text, supplier => supplier.BillingAddressZh);
                txtHBillingAddressZh2.DataBindings.Add<Supplier>(_supplierHistory, (Control c) => c.Text, supplier => supplier.BillingAddressZh2);
                txtHContactName.DataBindings.Add<Supplier>(_supplierHistory, (Control c) => c.Text, supplier => supplier.ContactName);
                txtHContactNameZh.DataBindings.Add<Supplier>(_supplierHistory, (Control c) => c.Text, supplier => supplier.ContactNameZh);
                txtHContactPhone.DataBindings.Add<Supplier>(_supplierHistory, (Control c) => c.Text, supplier => supplier.ContactPhone);
                txtHComments.DataBindings.Add<Supplier>(_supplierHistory, (Control c) => c.Text, supplier => supplier.Comments);

                txtHIdIncoterm.DataBindings.Add<Supplier>(_supplierHistory, (Control c) => c.Text, supplier => supplier.IdIncoterm);
                txtHIdPaymentTerms.DataBindings.Add<Supplier>(_supplierHistory, (Control c) => c.Text, supplier => supplier.IdPaymentTerms);
                txtHIdDefaultCurrency.DataBindings.Add<Supplier>(_supplierHistory, (Control c) => c.Text, supplier => supplier.IdDefaultCurrency);

                //CheckEdit
                chkHActive.DataBindings.Add<Supplier>(_supplierHistory, (CheckEdit chk) => chk.Checked, supplier => supplier.Active);

                //LookUpEdit
                //lueIdIncoterm.DataBindings.Add<Supplier>(_supplierUpdate, (LookUpEdit e) => e.EditValue, supplier => supplier.IdIncoterm);
                //lueIdPaymentTerms.DataBindings.Add<Supplier>(_supplierUpdate, (LookUpEdit e) => e.EditValue, supplier => supplier.IdPaymentTerms);
                //lueIdDefaultCurrency.DataBindings.Add<Supplier>(_supplierUpdate, (LookUpEdit e) => e.EditValue, supplier => supplier.IdDefaultCurrency);

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
                ResetSupplierUpdate();
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
                GridColumn column = rootGridViewSuppliers.Columns[eSupplierColumns.IdSupplier.ToString()];
                if (column != null)
                {
                    // locating the row 
                    int rhFound = rootGridViewSuppliers.LocateByDisplayText(rootGridViewSuppliers.FocusedRowHandle + 1, column, idSupplier);
                    // focusing the cell 
                    if (rhFound != GridControl.InvalidRowHandle)
                    {
                        rootGridViewSuppliers.FocusedRowHandle = rhFound;
                        rootGridViewSuppliers.FocusedColumn = column;
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsValidSupplier()
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
                    else if (ctl.GetType() == typeof(LookUpEdit))
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
        /// Actualizar los datos de un supplier
        /// </summary>
        /// <param name="newVersion">Si es una versión nueva o una actualización de la existente</param>
        /// <returns></returns>
        private bool UpdateSupplier(bool newVersion = false)
        {
            try
            {

                return GlobalSetting.SupplierService.UpdateSupplier(_supplierUpdate, newVersion);
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
        private bool CreateSupplier()
        {
            try
            {
                _supplierOriginal = _supplierUpdate.Clone();
                return GlobalSetting.SupplierService.NewSupplier(_supplierUpdate);
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
                string id = _supplierOriginal.IdSupplier;
                _supplierOriginal = null;
                ResetSupplierUpdate();
                SetFormBinding();
                xtpForm.PageVisible = false;
                xtpList.PageVisible = true;
                LoadSuppliersList();
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
