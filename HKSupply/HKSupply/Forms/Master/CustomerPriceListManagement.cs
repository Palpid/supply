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

namespace HKSupply.Forms.Master
{
    public partial class CustomerPriceListManagement : RibbonFormBase
    {
        #region Enums
        private enum eCustomerPriceListColumns
        {
            IdVer,
            IdSubVer,
            Timestamp,
            IdItemBcn,
            IdCustomer,
            Price,
            Comments,
            IdCurrency,
            PriceBaseCurrency,
            ExchangeRateUsed,
            MinLot,
            IncrLot,
            LeadTime,
        }
        #endregion

        #region Private Members

        CustomerPriceList _customerPriceListUpdate;
        CustomerPriceList _customerPriceListOriginal;
        CustomerPriceListHistory _customerPriceListHistory;

        List<CustomerPriceList> _customersPriceListList;
        List<CustomerPriceListHistory> _customerPriceListHistoryList;

        List<ItemBcn> _itemBcnList;
        List<Customer> _customersList;
        List<Currency> _currenciesList;

        string[] _nonEditingFields = { "lueIdItemBcn", "lueIdCustomer", "txtIdVersion", "txtIdSubversion", "txtTimestamp" };

        int _currentHistoryNumList;

        #endregion

        #region Constructor
        public CustomerPriceListManagement()
        {
            InitializeComponent();

            try
            {
                ConfigureRibbonActions();
                SetUpGrdCustomersPriceList();
                SetUpTextEdit();
                ResetCustomerPriceListUpdate();
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
                var actions = GlobalSetting.FunctionalitiesRoles.FirstOrDefault(fr => fr.Functionality.FormName.Equals(Name));
                Read = actions.Read;
                New = actions.New;
                Modify = actions.Modify;
                RestoreInitState();
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
                _customerPriceListOriginal = null;
                ResetCustomerPriceListUpdate();
                SetFormBinding();
                xtpForm.PageVisible = false;
                xtpList.PageVisible = true;
                sbNewVersion.Visible = false;
                LoadCustomersPriceList();
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
                if (_customerPriceListOriginal == null)
                {
                    MessageBox.Show("No customer price selected");
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

                if (IsValidCustomerPriceList() == false)
                    return;

                DialogResult result = MessageBox.Show(GlobalSetting.ResManager.GetString("SaveChanges"), "", MessageBoxButtons.YesNo);

                if (result != DialogResult.Yes)
                    return;

                Cursor = Cursors.WaitCursor;

                if (_customerPriceListUpdate.Equals(_customerPriceListOriginal))
                {
                    MessageBox.Show(GlobalSetting.ResManager.GetString("NoPendingChanges"));
                    return;
                }

                if (CurrentState == ActionsStates.Edit)
                {
                    res = UpdateCustomerPriceList();

                }
                else if (CurrentState == ActionsStates.New)
                {
                    res = CreateCustomerPriceList();
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

        private void CustomerPriceListManagement_Load(object sender, EventArgs e)
        {
            try
            {
                xtpForm.PageVisible = false;
                sbNewVersion.Visible = false;
                LoadCustomersList();
                LoadItemBcnList();
                LoadCurrenciesList();

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void rootGridViewCustomersPriceList_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                CustomerPriceList customerPriceList = view.GetRow(view.FocusedRowHandle) as CustomerPriceList;
                if (customerPriceList != null)
                {
                    LoadCustomerPriceListForm(customerPriceList);
                    LoadCustomerPriceListHistory(customerPriceList.IdItemBcn, customerPriceList.IdCustomer);
                }
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
                LoadCustomersPriceList();
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

        private void sbNewVersion_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                Validate();

                if (_customerPriceListUpdate.Equals(_customerPriceListOriginal))
                {
                    MessageBox.Show(GlobalSetting.ResManager.GetString("NoPendingChanges"));
                    return;
                }

                if (IsValidCustomerPriceList() == false)
                    return;

                if (UpdateCustomerPriceList(true))
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

        private void sbSetCurrentSubversion_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show(GlobalSetting.ResManager.GetString("SaveChanges"), "", MessageBoxButtons.YesNo);

                if (result != DialogResult.Yes)
                    return;

                Cursor = Cursors.WaitCursor;

                _customerPriceListUpdate = _customerPriceListHistory;
                _customerPriceListUpdate.IdSubVer = _customerPriceListOriginal.IdSubVer;
                _customerPriceListUpdate.IdVer = _customerPriceListOriginal.IdVer;

                if (UpdateCustomerPriceList())
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

                _customerPriceListUpdate = _customerPriceListHistory;
                _customerPriceListUpdate.IdSubVer = _customerPriceListOriginal.IdSubVer;
                _customerPriceListUpdate.IdVer = _customerPriceListOriginal.IdVer;

                if (UpdateCustomerPriceList(true))
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

        private void sbBackward_Click(object sender, EventArgs e)
        {
            try
            {
                SetCurrentSupplierPriceListHistory(_currentHistoryNumList - 1);
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
                SetCurrentSupplierPriceListHistory(_currentHistoryNumList + 1);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Private Methods

        private void ResetCustomerPriceListUpdate()
        {
            _customerPriceListUpdate = new CustomerPriceList();
        }

        private void SetUpGrdCustomersPriceList()
        {
            try
            {
                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                rootGridViewCustomersPriceList.OptionsView.ColumnAutoWidth = false;
                rootGridViewCustomersPriceList.HorzScrollVisibility = ScrollVisibility.Auto;

                //hacer todo el grid no editable
                rootGridViewCustomersPriceList.OptionsBehavior.Editable = false;

                //Columns definition
                GridColumn colIdVer = new GridColumn() { Caption = "Version", Visible = true, FieldName = eCustomerPriceListColumns.IdVer.ToString(), Width = 50 };
                GridColumn colIdSubVer = new GridColumn() { Caption = "Subversion", Visible = true, FieldName = eCustomerPriceListColumns.IdSubVer.ToString(), Width = 70 };
                GridColumn colTimestamp = new GridColumn() { Caption = "Timestamp", Visible = true, FieldName = eCustomerPriceListColumns.Timestamp.ToString(), Width = 130 };
                GridColumn colIdItemBcn = new GridColumn() { Caption = "Item Bcn", Visible = true, FieldName = eCustomerPriceListColumns.IdItemBcn.ToString(), Width = 200 };
                GridColumn colIdCustomer = new GridColumn() { Caption = "Id Supplier", Visible = true, FieldName = eCustomerPriceListColumns.IdCustomer.ToString(), Width = 100 };
                GridColumn colPrice = new GridColumn() { Caption = "Price", Visible = true, FieldName = eCustomerPriceListColumns.Price.ToString(), Width = 80 };
                GridColumn colComments = new GridColumn() { Caption = "Comments", Visible = true, FieldName = eCustomerPriceListColumns.Comments.ToString(), Width = 300 };
                GridColumn colCurrency = new GridColumn() { Caption = "Currency", Visible = true, FieldName = eCustomerPriceListColumns.IdCurrency.ToString(), Width = 70 };
                GridColumn colPriceBaseCurrency = new GridColumn() { Caption = "Price Base Currency", Visible = true, FieldName = eCustomerPriceListColumns.PriceBaseCurrency.ToString(), Width = 120 };
                GridColumn colExchangeRateUsed = new GridColumn() { Caption = "Exchange Rate Used", Visible = true, FieldName = eCustomerPriceListColumns.ExchangeRateUsed.ToString(), Width = 120 };
                GridColumn colMinLot = new GridColumn() { Caption = "Min Lot", Visible = true, FieldName = eCustomerPriceListColumns.MinLot.ToString(), Width = 60 };
                GridColumn colIncrLot = new GridColumn() { Caption = "Incr Lot", Visible = true, FieldName = eCustomerPriceListColumns.IncrLot.ToString(), Width = 60 };
                GridColumn colLeadTime = new GridColumn() { Caption = "Lead Time", Visible = true, FieldName = eCustomerPriceListColumns.LeadTime.ToString(), Width = 70 };

                //Format type 
                colTimestamp.DisplayFormat.FormatType = FormatType.DateTime;

                colPrice.DisplayFormat.FormatType = FormatType.Numeric;
                colPrice.DisplayFormat.FormatString = "F2";

                colPriceBaseCurrency.DisplayFormat.FormatType = FormatType.Numeric;
                colPriceBaseCurrency.DisplayFormat.FormatString = "F2";

                colExchangeRateUsed.DisplayFormat.FormatType = FormatType.Numeric;
                colExchangeRateUsed.DisplayFormat.FormatString = "F2";

                //add columns to grid root view
                rootGridViewCustomersPriceList.Columns.Add(colIdVer);
                rootGridViewCustomersPriceList.Columns.Add(colIdSubVer);
                rootGridViewCustomersPriceList.Columns.Add(colTimestamp);
                rootGridViewCustomersPriceList.Columns.Add(colIdItemBcn);
                rootGridViewCustomersPriceList.Columns.Add(colIdCustomer);
                rootGridViewCustomersPriceList.Columns.Add(colPrice);
                rootGridViewCustomersPriceList.Columns.Add(colComments);
                rootGridViewCustomersPriceList.Columns.Add(colCurrency);
                rootGridViewCustomersPriceList.Columns.Add(colPriceBaseCurrency);
                rootGridViewCustomersPriceList.Columns.Add(colExchangeRateUsed);
                rootGridViewCustomersPriceList.Columns.Add(colExchangeRateUsed);
                rootGridViewCustomersPriceList.Columns.Add(colMinLot);
                rootGridViewCustomersPriceList.Columns.Add(colIncrLot);
                rootGridViewCustomersPriceList.Columns.Add(colLeadTime);

                //Events
                rootGridViewCustomersPriceList.DoubleClick += rootGridViewCustomersPriceList_DoubleClick;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetUpTextEdit()
        {
            try
            {
                txtPrice.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                txtPrice.Properties.Mask.EditMask = "F2"; //Dos decimales
                txtPrice.Properties.Mask.UseMaskAsDisplayFormat = true;
                txtPrice.EditValue = "0.00";

                txtPriceBaseCurrency.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                txtPriceBaseCurrency.Properties.Mask.EditMask = "F2";
                txtPriceBaseCurrency.Properties.Mask.UseMaskAsDisplayFormat = true;
                txtPriceBaseCurrency.EditValue = "0.00";

                txtExchangeRateUsed.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                txtExchangeRateUsed.Properties.Mask.EditMask = "F2";
                txtExchangeRateUsed.Properties.Mask.UseMaskAsDisplayFormat = true;
                txtExchangeRateUsed.EditValue = "0.00";

                txtLeadTime.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                txtLeadTime.Properties.Mask.EditMask = "F2";
                txtLeadTime.Properties.Mask.UseMaskAsDisplayFormat = true;
                txtLeadTime.EditValue = "0.00";

                txtMinLot.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                txtMinLot.Properties.Mask.EditMask = "D";
                txtMinLot.Properties.Mask.UseMaskAsDisplayFormat = true;
                txtMinLot.EditValue = "0";

                txtIncrLot.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                txtIncrLot.Properties.Mask.EditMask = "D";
                txtIncrLot.Properties.Mask.UseMaskAsDisplayFormat = true;
                txtIncrLot.EditValue = "0";

                //History
                txtHPrice.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                txtHPrice.Properties.Mask.EditMask = "F2"; //Dos decimales
                txtHPrice.Properties.Mask.UseMaskAsDisplayFormat = true;
                txtHPrice.EditValue = "0.00";

                txtHPriceBaseCurrency.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                txtHPriceBaseCurrency.Properties.Mask.EditMask = "F2";
                txtHPriceBaseCurrency.Properties.Mask.UseMaskAsDisplayFormat = true;
                txtHPriceBaseCurrency.EditValue = "0.00";

                txtHExchangeRateUsed.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                txtHExchangeRateUsed.Properties.Mask.EditMask = "F2";
                txtHExchangeRateUsed.Properties.Mask.UseMaskAsDisplayFormat = true;
                txtHExchangeRateUsed.EditValue = "0.00";

                txtHLeadTime.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                txtHLeadTime.Properties.Mask.EditMask = "F2";
                txtHLeadTime.Properties.Mask.UseMaskAsDisplayFormat = true;
                txtHLeadTime.EditValue = "0.00";

                txtHMinLot.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                txtHMinLot.Properties.Mask.EditMask = "D";
                txtHMinLot.Properties.Mask.UseMaskAsDisplayFormat = true;
                txtHMinLot.EditValue = "0";

                txtHIncrLot.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                txtHIncrLot.Properties.Mask.EditMask = "D";
                txtHIncrLot.Properties.Mask.UseMaskAsDisplayFormat = true;
                txtHIncrLot.EditValue = "0";

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
                lueIdCurrency.Properties.DataSource = _currenciesList;
                lueIdCurrency.Properties.DisplayMember = "Description";
                lueIdCurrency.Properties.ValueMember = "IdCurrency";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadItemBcnList()
        {
            try
            {
                _itemBcnList = GlobalSetting.ItemBcnService.GetItemsBcn();

                lueIdItemBcn.Properties.DataSource = _itemBcnList;
                lueIdItemBcn.Properties.DisplayMember = "Description";
                lueIdItemBcn.Properties.ValueMember = "IdItemBcn";
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

                lueIdCustomer.Properties.DataSource = _customersList;
                lueIdCustomer.Properties.DisplayMember = "CustomerName";
                lueIdCustomer.Properties.ValueMember = "IdCustomer";

                lueIdCustomer.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("IdCustomer", 20, "Id Customer"));
                lueIdCustomer.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CustomerName", 100, "Name"));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadCustomersPriceList()
        {
            try
            {
                _customersPriceListList = GlobalSetting.CustomerPriceListService.GetCustomersPriceList();

                xgrdCustomersPriceList.DataSource = _customersPriceListList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadCustomerPriceListForm(CustomerPriceList customerPriceList)
        {
            try
            {
                _customerPriceListUpdate = customerPriceList.Clone();
                _customerPriceListOriginal = _customerPriceListUpdate.Clone();
                SetFormBinding();  //refresh binding 
                xtpForm.PageVisible = true;
                xtcGeneral.SelectedTabPage = xtpForm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadCustomerPriceListHistory(string idItembcn, string idCustomer)
        {
            try
            {
                _customerPriceListHistoryList = GlobalSetting.CustomerPriceListService.GetCustomerPriceListHistory(idItembcn, idCustomer);
                SetCurrentSupplierPriceListHistory(0);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void SetCurrentSupplierPriceListHistory(int historyNum)
        {
            try
            {
                if (historyNum >= 0 && historyNum < _customerPriceListHistoryList.Count())
                {
                    _customerPriceListHistory = _customerPriceListHistoryList[historyNum];
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
                txtIdVersion.DataBindings.Add<CustomerPriceList>(_customerPriceListUpdate, (Control c) => c.Text, customerPriceList => customerPriceList.IdVer);
                txtIdSubversion.DataBindings.Add<CustomerPriceList>(_customerPriceListUpdate, (Control c) => c.Text, customerPriceList => customerPriceList.IdSubVer);
                txtTimestamp.DataBindings.Add<CustomerPriceList>(_customerPriceListUpdate, (Control c) => c.Text, customerPriceList => customerPriceList.Timestamp);
                txtPrice.DataBindings.Add<CustomerPriceList>(_customerPriceListUpdate, (Control c) => c.Text, customerPriceList => customerPriceList.Price);
                txtComments.DataBindings.Add<CustomerPriceList>(_customerPriceListUpdate, (Control c) => c.Text, customerPriceList => customerPriceList.Comments);
                txtPriceBaseCurrency.DataBindings.Add<CustomerPriceList>(_customerPriceListUpdate, (Control c) => c.Text, customerPriceList => customerPriceList.PriceBaseCurrency);
                txtExchangeRateUsed.DataBindings.Add<CustomerPriceList>(_customerPriceListUpdate, (Control c) => c.Text, customerPriceList => customerPriceList.ExchangeRateUsed);
                txtMinLot.DataBindings.Add<CustomerPriceList>(_customerPriceListUpdate, (Control c) => c.Text, customerPriceList => customerPriceList.MinLot);
                txtIncrLot.DataBindings.Add<CustomerPriceList>(_customerPriceListUpdate, (Control c) => c.Text, customerPriceList => customerPriceList.IncrLot);
                txtLeadTime.DataBindings.Add<CustomerPriceList>(_customerPriceListUpdate, (Control c) => c.Text, customerPriceList => customerPriceList.LeadTime);

                //LookUpEdit
                lueIdItemBcn.DataBindings.Add<CustomerPriceList>(_customerPriceListUpdate, (LookUpEdit e) => e.EditValue, customerPriceList => customerPriceList.IdItemBcn);
                lueIdCustomer.DataBindings.Add<CustomerPriceList>(_customerPriceListUpdate, (LookUpEdit e) => e.EditValue, customerPriceList => customerPriceList.IdCustomer);
                lueIdCurrency.DataBindings.Add<CustomerPriceList>(_customerPriceListUpdate, (LookUpEdit e) => e.EditValue, customerPriceList => customerPriceList.IdCurrency);

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
                txtHIdVersion.DataBindings.Add<CustomerPriceListHistory>(_customerPriceListHistory, (Control c) => c.Text, customerPriceList => customerPriceList.IdVer);
                txtHIdSubversion.DataBindings.Add<CustomerPriceListHistory>(_customerPriceListHistory, (Control c) => c.Text, customerPriceList => customerPriceList.IdSubVer);
                txtHTimestamp.DataBindings.Add<CustomerPriceListHistory>(_customerPriceListHistory, (Control c) => c.Text, customerPriceList => customerPriceList.Timestamp);
                txtHPrice.DataBindings.Add<CustomerPriceListHistory>(_customerPriceListHistory, (Control c) => c.Text, customerPriceList => customerPriceList.Price);
                txtHComments.DataBindings.Add<CustomerPriceListHistory>(_customerPriceListHistory, (Control c) => c.Text, customerPriceList => customerPriceList.Comments);
                txtHPriceBaseCurrency.DataBindings.Add<CustomerPriceListHistory>(_customerPriceListHistory, (Control c) => c.Text, customerPriceList => customerPriceList.PriceBaseCurrency);
                txtHExchangeRateUsed.DataBindings.Add<CustomerPriceListHistory>(_customerPriceListHistory, (Control c) => c.Text, customerPriceList => customerPriceList.ExchangeRateUsed);
                txtHMinLot.DataBindings.Add<CustomerPriceListHistory>(_customerPriceListHistory, (Control c) => c.Text, customerPriceList => customerPriceList.MinLot);
                txtHIncrLot.DataBindings.Add<CustomerPriceListHistory>(_customerPriceListHistory, (Control c) => c.Text, customerPriceList => customerPriceList.IncrLot);
                txtHLeadTime.DataBindings.Add<CustomerPriceListHistory>(_customerPriceListHistory, (Control c) => c.Text, customerPriceList => customerPriceList.LeadTime);
                txtUser.DataBindings.Add<CustomerPriceListHistory>(_customerPriceListHistory, (Control c) => c.Text, customerPriceList => customerPriceList.User);

                txtHIdItemBcn.DataBindings.Add<CustomerPriceListHistory>(_customerPriceListHistory, (Control c) => c.Text, customerPriceList => customerPriceList.IdItemBcn);
                txtHidCustomer.DataBindings.Add<CustomerPriceListHistory>(_customerPriceListHistory, (Control c) => c.Text, customerPriceList => customerPriceList.IdCustomer);
                txtHIdCurrency.DataBindings.Add<CustomerPriceListHistory>(_customerPriceListHistory, (Control c) => c.Text, customerPriceList => customerPriceList.IdCurrency);

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
                groupControlHistory.Visible = false;
                ResetCustomerPriceListUpdate();
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
        private void MoveGridToSupplier(string idItemBcn, string idCustomer)
        {
            try
            {
                int row = GridControl.InvalidRowHandle;
                for (int i = 0; i < rootGridViewCustomersPriceList.RowCount; i++)
                {
                    if (
                        rootGridViewCustomersPriceList.GetRowCellValue(i, eCustomerPriceListColumns.IdItemBcn.ToString()).Equals(idItemBcn) &&
                        rootGridViewCustomersPriceList.GetRowCellValue(i, eCustomerPriceListColumns.IdCustomer.ToString()).Equals(idCustomer)
                        )
                    {
                        row = i;
                    }
                }
                if (row != GridControl.InvalidRowHandle)
                {
                    rootGridViewCustomersPriceList.FocusedRowHandle = row;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsValidCustomerPriceList()
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
        private bool UpdateCustomerPriceList(bool newVersion = false)
        {
            try
            {
                return GlobalSetting.CustomerPriceListService.UpdateCustomerPriceList(_customerPriceListUpdate, newVersion);
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
        private bool CreateCustomerPriceList()
        {
            try
            {
                _customerPriceListOriginal = _customerPriceListUpdate.Clone();
                return GlobalSetting.CustomerPriceListService.NewCustomerPriceList(_customerPriceListUpdate);
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
                string idItemBcn = _customerPriceListOriginal.IdItemBcn;
                string idCustomer = _customerPriceListOriginal.IdCustomer;
                _customerPriceListOriginal = null;
                ResetCustomerPriceListUpdate();
                SetFormBinding();
                xtpForm.PageVisible = false;
                xtpList.PageVisible = true;
                groupControlHistory.Visible = true;
                LoadCustomersPriceList();
                MoveGridToSupplier(idItemBcn, idCustomer);
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
