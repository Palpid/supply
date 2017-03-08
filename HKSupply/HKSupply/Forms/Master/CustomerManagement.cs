using HKSupply.Exceptions;
using HKSupply.General;
using HKSupply.Models;
using HKSupply.Styles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HKSupply.Forms.Master
{
    public partial class CustomerManagement : Form
    {
        #region Private members
        CustomControls.StackView actionsStackView;

        Customer _customer;
        #endregion

        #region Constructor
        public CustomerManagement()
        {
            InitializeComponent();
        }
        #endregion

        #region Form Events

        private void CustomerManagement_Load(object sender, System.EventArgs e)
        {
            ConfigureActionsStackView();
        }

        #region Action toolbar events

        private void actionsStackView_EditButtonClick(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Edit Button");
                //ConfigureActionsStackViewEditing();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void actionsStackView_NewButtonClick(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("New Button");
                //ConfigureActionsStackViewCreating();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void actionsStackView_SaveButtonClick(object sender, EventArgs e)
        {
            try
            {
                //bool res = false;
                ////indicamos que ha dejado de editar el grid, por si modifica una celda y sin salir pulsa sobre guardar
                //grdRoles.EndEdit();

                //DialogResult result = MessageBox.Show(resManager.GetString("SaveChanges"), "", MessageBoxButtons.YesNo);

                //if (result != DialogResult.Yes)
                //    return;

                //if (actionsStackView.CurrentState == CustomControls.StackView.ToolbarStates.Edit)
                //{
                //    if (_modifiedRoles.Count() == 0)
                //    {
                //        MessageBox.Show(resManager.GetString("NoPendingChanges"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    }
                //    else
                //    {
                //        if (IsValidModifiedRoles())
                //        {
                //            if (UpdateRoles())
                //            {
                //                res = true;
                //            }
                //        }

                //    }
                //}
                //else if (actionsStackView.CurrentState == CustomControls.StackView.ToolbarStates.New)
                //{
                //    if (IsValidCreatedRoles())
                //    {
                //        if (CreateRol())
                //        {
                //            res = true;
                //        }
                //    }
                //}

                //if (res == true)
                //{
                //    LoadAllRoles();
                //    ConfigureRolesGridDefaultStyles();
                //    actionsStackView.RestoreInitState();
                //}

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void actionsStackView_CancelButtonClick(object sender, EventArgs e)
        {
            try
            {
                //LoadAllRoles();
                //SetupRolesGrid();
                actionsStackView.RestoreInitState();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

        #endregion

        #region Private Methods
        private void ConfigureActionsStackView()
        {
            try
            {
                var actions = GlobalSetting.FunctionalitiesRoles.FirstOrDefault(fr => fr.Functionality.FormName.Equals(Name));

                //CustomControls.StackView actionsStackView = new CustomControls.StackView(actions.Read, actions.New, actions.Modify);
                actionsStackView = new CustomControls.StackView(actions.Read, actions.New, actions.Modify);
                actionsStackView.EditButtonClick += actionsStackView_EditButtonClick;
                actionsStackView.NewButtonClick += actionsStackView_NewButtonClick;
                actionsStackView.SaveButtonClick += actionsStackView_SaveButtonClick;
                actionsStackView.CancelButtonClick += actionsStackView_CancelButtonClick;

                Controls.Add(actionsStackView);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void SetBinding()
        {
            txtIdCustomer.DataBindings.Add("Text", _customer, _customer.idCustomer);
            txtIdVersion.DataBindings.Add("Text", _customer, _customer.idVer.ToString());
            txtIdSubversion.DataBindings.Add("Text", _customer, _customer.idSubVer.ToString());
            txtTimestamp.DataBindings.Add("Text", _customer, _customer.Timestamp.ToString());
            txtName.DataBindings.Add("Text", _customer, _customer.CustName);
            chkActive.DataBindings.Add("Checked",_customer,_customer.Active.ToString());
            txtVatNumber.DataBindings.Add("Text", _customer, _customer.VATNum);
            txtShippingAddress.DataBindings.Add("Text", _customer, _customer.ShippingAddress);
            txtBillingAddress.DataBindings.Add("Text", _customer, _customer.BillingAddress);
            txtContactName.DataBindings.Add("Text", _customer, _customer.ContactName);
            txtContactPhone.DataBindings.Add("Text", _customer, _customer.ContactPhone);
            txtIntercom.DataBindings.Add("Text", _customer, _customer.idIncoterm.ToString());
            txtPaymentTerms.DataBindings.Add("Text", _customer, _customer.idPaymentTerms.ToString());
            txtCurreny.DataBindings.Add("Text", _customer, _customer.Currency);
        }
        #endregion


    }
}
