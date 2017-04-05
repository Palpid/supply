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
            CustName,
            Active,
            VATNum,
            ShippingAddress,
            BillingAddress,
            ContactName,
            ContactPhone,
            IdIncoterm,
            IdPaymentTerms,
            Currency,
        }
        #endregion

        #region Private Members

        Customer _customerUpdate;
        Customer _customerOriginal;

        List<Customer> _customersList;

        string[] _nonEditingFields = { "txtIdCustomer", "txtIdVersion", "txtIdSubversion", "txtTimestamp" };

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
                    MessageBox.Show("No customer selected");
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

        #endregion

        #region Form Events

        private void CustomerManagement_Load(object sender, EventArgs e)
        {
            try
            {
                xtpForm.PageVisible = false;
                sbNewVersion.Visible = false;
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
                    LoadCustomerForm(idCustomer.ToString());
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                GridColumn colIdSupplier = new GridColumn() { Caption = "Id Supplier", Visible = true, FieldName = eCustomerColumns.IdCustomer.ToString(), Width = 100 };
                GridColumn colSupplierName = new GridColumn() { Caption = "Supplier Name", Visible = true, FieldName = eCustomerColumns.CustName.ToString(), Width = 200 };
                GridColumn colActive = new GridColumn() { Caption = "Active", Visible = true, FieldName = eCustomerColumns.Active.ToString(), Width = 50 };
                GridColumn colVATNum = new GridColumn() { Caption = "VAT Number", Visible = true, FieldName = eCustomerColumns.VATNum.ToString(), Width = 120 };
                GridColumn colShippingAddress = new GridColumn() { Caption = "Shipping Address", Visible = true, FieldName = eCustomerColumns.ShippingAddress.ToString(), Width = 300 };
                GridColumn colBillingAddress = new GridColumn() { Caption = "Billing Address", Visible = true, FieldName = eCustomerColumns.BillingAddress.ToString(), Width = 300 };
                GridColumn colContactName = new GridColumn() { Caption = "Contact Name", Visible = true, FieldName = eCustomerColumns.ContactName.ToString(), Width = 200 };
                GridColumn colContactPhone = new GridColumn() { Caption = "Contact Phone", Visible = true, FieldName = eCustomerColumns.ContactPhone.ToString(), Width = 150 };
                GridColumn colIdIncoterm = new GridColumn() { Caption = "Id Incoterm", Visible = true, FieldName = eCustomerColumns.IdIncoterm.ToString(), Width = 70 };
                GridColumn colIdPaymentTerms = new GridColumn() { Caption = "Id Payment Terms", Visible = true, FieldName = eCustomerColumns.IdPaymentTerms.ToString(), Width = 100 };
                GridColumn colCurrency = new GridColumn() { Caption = "Currency", Visible = true, FieldName = eCustomerColumns.Currency.ToString(), Width = 70 };

                //Format type 
                colTimestamp.DisplayFormat.FormatType = FormatType.DateTime;

                //add columns to grid root view
                rootGridViewCustomers.Columns.Add(colIdVer);
                rootGridViewCustomers.Columns.Add(colIdSubVer);
                rootGridViewCustomers.Columns.Add(colTimestamp);
                rootGridViewCustomers.Columns.Add(colIdSupplier);
                rootGridViewCustomers.Columns.Add(colSupplierName);
                rootGridViewCustomers.Columns.Add(colActive);
                rootGridViewCustomers.Columns.Add(colVATNum);
                rootGridViewCustomers.Columns.Add(colShippingAddress);
                rootGridViewCustomers.Columns.Add(colBillingAddress);
                rootGridViewCustomers.Columns.Add(colContactName);
                rootGridViewCustomers.Columns.Add(colContactPhone);
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
                }

                txtIdCustomer.DataBindings.Add("Text", _customerUpdate, "IdCustomer");
                txtIdVersion.DataBindings.Add("Text", _customerUpdate, "idVer");
                txtIdSubversion.DataBindings.Add("Text", _customerUpdate, "idSubVer");
                txtTimestamp.DataBindings.Add("Text", _customerUpdate, "Timestamp");
                txtName.DataBindings.Add("Text", _customerUpdate, "CustName");
                chkActive.DataBindings.Add("Checked", _customerUpdate, "Active");
                txtVatNumber.DataBindings.Add("Text", _customerUpdate, "VATNum");
                txtShippingAddress.DataBindings.Add("Text", _customerUpdate, "ShippingAddress");
                txtBillingAddress.DataBindings.Add("Text", _customerUpdate, "BillingAddress");
                txtContactName.DataBindings.Add("Text", _customerUpdate, "ContactName");
                txtContactPhone.DataBindings.Add("Text", _customerUpdate, "ContactPhone");
                txtIntercom.DataBindings.Add("Text", _customerUpdate, "IdIncoterm");
                txtPaymentTerms.DataBindings.Add("Text", _customerUpdate, "IdPaymentTerms");
                txtCurreny.DataBindings.Add("Text", _customerUpdate, "Currency");
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
