using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using HKSupply.General;
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
            IdIncoterm,
            IdPaymentTerms,
            Currency,
        }
        #endregion

        #region Private Members

        Supplier _supplierUpdate;
        Supplier _supplierOriginal;

        List<Supplier> _suppliersList;

        #endregion

        #region Constructor
        public SupplierManagement()
        {
            InitializeComponent();

            try
            {
                ConfigureRibbonActions();
                SetUpGrdSuppliers();
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
        }

        public override void bbiEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            base.bbiEdit_ItemClick(sender, e);
        }

        public override void bbiNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            base.bbiNew_ItemClick(sender, e);
        }

        public override void bbiSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            base.bbiSave_ItemClick(sender, e);
        }

        #endregion

        #region Form events

        private void SupplierManagement_Load(object sender, EventArgs e)
        {
            try
            {

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

        void rootGridViewSuppliers_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                GridView view = (GridView)sender;
                object idSupplier = view.GetRowCellValue(view.FocusedRowHandle, eSupplierColumns.IdSupplier.ToString());
                //if (idSupplier != null)
                //    LoadSupplierForm(idSupplier.ToString());
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Private Methods

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
                GridColumn colIdSupplier = new GridColumn() { Caption = "Id Supplier", Visible = true, FieldName = eSupplierColumns.IdSupplier.ToString(), Width = 100 };
                GridColumn colSupplierName = new GridColumn() { Caption = "Supplier Name", Visible = true, FieldName = eSupplierColumns.SupplierName.ToString(), Width = 200 };
                GridColumn colActive = new GridColumn() { Caption = "Active", Visible = true, FieldName = eSupplierColumns.Active.ToString(), Width = 50 };
                GridColumn colVATNum = new GridColumn() { Caption = "VAT Number", Visible = true, FieldName = eSupplierColumns.VATNum.ToString(), Width = 120 };
                GridColumn colShippingAddress = new GridColumn() { Caption = "Shipping Address", Visible = true, FieldName = eSupplierColumns.ShippingAddress.ToString(), Width = 300 };
                GridColumn colBillingAddress = new GridColumn() { Caption = "Billing Address", Visible = true, FieldName = eSupplierColumns.BillingAddress.ToString(), Width = 300 };
                GridColumn colContactName = new GridColumn() { Caption = "Contact Name", Visible = true, FieldName = eSupplierColumns.ContactName.ToString(), Width = 200 };
                GridColumn colContactPhone = new GridColumn() { Caption = "Contact Phone", Visible = true, FieldName = eSupplierColumns.ContactPhone.ToString(), Width = 150 };
                GridColumn colIdIncoterm = new GridColumn() { Caption = "Id Incoterm", Visible = true, FieldName = eSupplierColumns.IdIncoterm.ToString(), Width = 70 };
                GridColumn colIdPaymentTerms = new GridColumn() { Caption = "Id Payment Terms", Visible = true, FieldName = eSupplierColumns.IdPaymentTerms.ToString(), Width = 100 };
                GridColumn colCurrency = new GridColumn() { Caption = "Currency", Visible = true, FieldName = eSupplierColumns.Currency.ToString(), Width = 70 };

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
                rootGridViewSuppliers.Columns.Add(colBillingAddress);
                rootGridViewSuppliers.Columns.Add(colContactName);
                rootGridViewSuppliers.Columns.Add(colContactPhone);
                rootGridViewSuppliers.Columns.Add(colIdIncoterm);
                rootGridViewSuppliers.Columns.Add(colIdPaymentTerms);
                rootGridViewSuppliers.Columns.Add(colCurrency);

                //Events
                rootGridViewSuppliers.DoubleClick += rootGridViewSuppliers_DoubleClick;
                //rootGridViewFunctionalities.ValidatingEditor += rootGridViewFunctionalities_ValidatingEditor;
                //rootGridViewFunctionalities.CellValueChanged += rootGridViewFunctionalities_CellValueChanged;
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

        /// <summary>
        /// Crear los bindings de los campos del formulario
        /// </summary>
        private void SetFormBinding()
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
                    ((CheckEdit)ctl).Enabled = false;
                }
                //else if (ctl.GetType() == typeof(TextBox))
                //{
                //    ctl.DataBindings.Clear();
                //    ((TextBox)ctl).ReadOnly = true;
                //}
                //else if (ctl.GetType() == typeof(CheckBox))
                //{
                //    ctl.DataBindings.Clear();
                //    ((CheckBox)ctl).Enabled = false;
                //}
            }

            Binding bindingIdSupplier = new Binding("Text", _supplierUpdate, "IdSupplier");
            bindingIdSupplier.NullValue = string.Empty;
            Binding bindingIdVersion = new Binding("Text", _supplierUpdate, "IdVer");
            Binding bindingIdSubVer = new Binding("Text", _supplierUpdate, "IdSubVer");
            Binding bindingITimestamp = new Binding("Text", _supplierUpdate, "Timestamp");
            bindingITimestamp.NullValue = string.Empty;
            Binding bindingSupplierName = new Binding("Text", _supplierUpdate, "SupplierName");
            bindingSupplierName.NullValue = string.Empty;

            Binding bindingActive = new Binding("Checked", _supplierUpdate, "Active");
            
            txtIdSupplier.DataBindings.Add(bindingIdSupplier);
            txtIdVersion.DataBindings.Add(bindingIdVersion);
            txtIdSubversion.DataBindings.Add(bindingIdSubVer);
            txtTimestamp.DataBindings.Add(bindingITimestamp);
            txtName.DataBindings.Add(bindingSupplierName);

            chkActive.DataBindings.Add(bindingActive);

            txtVatNumber.DataBindings.Add("Text", _supplierUpdate, "VATNum");
            txtShippingAddress.DataBindings.Add("Text", _supplierUpdate, "ShippingAddress");
            txtBillingAddress.DataBindings.Add("Text", _supplierUpdate, "BillingAddress");
            txtContactName.DataBindings.Add("Text", _supplierUpdate, "ContactName");
            txtContactPhone.DataBindings.Add("Text", _supplierUpdate, "ContactPhone");
            txtIntercom.DataBindings.Add("Text", _supplierUpdate, "IdIncoterm");
            txtPaymentTerms.DataBindings.Add("Text", _supplierUpdate, "IdPaymentTerms");
            txtCurreny.DataBindings.Add("Text", _supplierUpdate, "Currency");

            //txtIdSupplier.DataBindings.Add("Text", _supplierUpdate, "IdSupplier");
            //txtIdVersion.DataBindings.Add("Text", _supplierUpdate, "idVer");
            //txtIdSubversion.DataBindings.Add("Text", _supplierUpdate, "idSubVer");
            //txtTimestamp.DataBindings.Add("Text", _supplierUpdate, "Timestamp");
            //txtName.DataBindings.Add("Text", _supplierUpdate, "SupplierName");
            //chkActive.DataBindings.Add("Checked", _supplierUpdate, "Active");
            //txtVatNumber.DataBindings.Add("Text", _supplierUpdate, "VATNum");
            //txtShippingAddress.DataBindings.Add("Text", _supplierUpdate, "ShippingAddress");
            //txtBillingAddress.DataBindings.Add("Text", _supplierUpdate, "BillingAddress");
            //txtContactName.DataBindings.Add("Text", _supplierUpdate, "ContactName");
            //txtContactPhone.DataBindings.Add("Text", _supplierUpdate, "ContactPhone");
            //txtIntercom.DataBindings.Add("Text", _supplierUpdate, "IdIncoterm");
            //txtPaymentTerms.DataBindings.Add("Text", _supplierUpdate, "IdPaymentTerms");
            //txtCurreny.DataBindings.Add("Text", _supplierUpdate, "Currency");
        }
        #endregion

    }
}
