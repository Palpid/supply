using DevExpress.XtraEditors;
using HKSupply.General;
using HKSupply.Models;
using HKSupply.Helpers;
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
    public partial class MyCompanyManagement : RibbonFormBase
    {
        #region Private Members

        Font _labelDefaultFontBold = new Font("SourceSansProRegular", 8, FontStyle.Bold);
        Font _labelDefaultFont = new Font("SourceSansProRegular", 8, FontStyle.Regular);

        List<Currency> _currenciesList;
        MyCompany _myCompany;
        #endregion

        #region Constructor
        public MyCompanyManagement()
        {
            InitializeComponent();

            try
            {
                Cursor = Cursors.WaitCursor;

                ConfigureRibbonActions();
                LoadAuxList();
                LoadMyCompany();
                SetUpLabels();
                SetUpLookUpEdit();
                SetFormBinding();
                //SetUpEvents();
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
                EnableExportExcel = false;
                EnableExportCsv = false;
                ConfigurePrintExportOptions();
                //Layout
                EnableLayoutOptions = false;
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
        }

        public override void bbiEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            base.bbiEdit_ItemClick(sender, e);

            try
            {
                ConfigureRibbonActionsEditing();
            }
            catch(Exception ex)
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

                DialogResult result = MessageBox.Show(GlobalSetting.ResManager.GetString("SaveChanges"), "", MessageBoxButtons.YesNo);

                if (result != DialogResult.Yes)
                    return;

                Cursor = Cursors.WaitCursor;

                res = UpdateMyCompany();

                if (res == true)
                {
                    MessageBox.Show(GlobalSetting.ResManager.GetString("SaveSuccessfully"));
                    ActionsAfterUpdate();
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
        private void MyCompanyManagement_Load(object sender, EventArgs e)
        {

        }
        #endregion

        #region Private Methods

        #region SetUpForm Objects

        private void SetUpLabels()
        {
            try
            {
                /********* Fonts **********/
                lblId.Font = _labelDefaultFontBold;
                lblName.Font = _labelDefaultFontBold;
                lblVatNumber.Font = _labelDefaultFontBold;
                lblShippingAddress.Font = _labelDefaultFontBold;
                lblShippingAddressZh.Font = _labelDefaultFontBold;
                lblBillingAddress.Font = _labelDefaultFontBold;
                lblBillingAddressZh.Font = _labelDefaultFontBold;
                lblIdCurrency.Font = _labelDefaultFontBold;
                lblContactName.Font = _labelDefaultFontBold;
                lblContactNameZh.Font = _labelDefaultFontBold;
                lblContactPhone.Font = _labelDefaultFontBold;

                /********* Texts **********/
                lblId.Text = "Id";
                lblName.Text = "Name";
                lblVatNumber.Text = "VAT Number";
                lblShippingAddress.Text = "Shipping Address";
                lblShippingAddressZh.Text = "Shipping Address (Chinese)";
                lblBillingAddress.Text = "Billing Address";
                lblBillingAddressZh.Text = "Billing Address (Chinese)";
                lblIdCurrency.Text = "Currency";
                lblContactName.Text = "Contact Name";
                lblContactNameZh.Text = "Contact Name (Chinese)";
                lblContactPhone.Text = "Contact Phone";
            }
            catch
            {
                throw;
            }
        }

        private void SetUpLookUpEdit()
        {
            try
            {
                lueCurrency.Properties.DataSource = _currenciesList;
                lueCurrency.Properties.ValueMember = nameof(Currency.IdCurrency);
                lueCurrency.Properties.DisplayMember = nameof(Currency.Description);
            }
            catch
            {
                throw;
            }
        }

        private void SetFormBinding()
        {
            try
            {
                ResetBinding(xtpMyCompany.Controls);

                //TextEdit
                txtId.DataBindings.Add<MyCompany>(_myCompany, (Control c) => c.Text, mc => mc.IdMyCompany);
                txtName.DataBindings.Add<MyCompany>(_myCompany, (Control c) => c.Text, mc => mc.Name);
                txtVatNumber.DataBindings.Add<MyCompany>(_myCompany, (Control c) => c.Text, mc => mc.VATNum);
                txtShippingAddress.DataBindings.Add<MyCompany>(_myCompany, (Control c) => c.Text, mc => mc.ShippingAddress);
                txtShippingAddressZh.DataBindings.Add<MyCompany>(_myCompany, (Control c) => c.Text, mc => mc.ShippingAddressZh);
                txtBillingAddress.DataBindings.Add<MyCompany>(_myCompany, (Control c) => c.Text, mc => mc.BillingAddress);
                txtBillingAddressZh.DataBindings.Add<MyCompany>(_myCompany, (Control c) => c.Text, mc => mc.BillingAddressZh);
                txtContactName.DataBindings.Add<MyCompany>(_myCompany, (Control c) => c.Text, mc => mc.ContactName);
                txtContactNameZh.DataBindings.Add<MyCompany>(_myCompany, (Control c) => c.Text, mc => mc.ContactNameZh);
                txtContactPhone.DataBindings.Add<MyCompany>(_myCompany, (Control c) => c.Text, mc => mc.ContactPhone);

                //LookUpEdit
                lueCurrency.DataBindings.Add<MyCompany>(_myCompany, (LookUpEdit e) => e.EditValue, mc => mc.IdDefaultCurrency);

                //MemoEdit
                memoEditComments.DataBindings.Add("Text", _myCompany, nameof(MyCompany.Comments));
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Loads
        private void LoadAuxList()
        {
            try
            {
                _currenciesList = GlobalSetting.CurrencyService.GetCurrencies();
            }
            catch
            {
                throw;
            }
        }

        private void LoadMyCompany()
        {
            try
            {
                _myCompany = GlobalSetting.MyCompanyService.GetMyCompany();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Configure Ribbon Actions
        private void ConfigureRibbonActionsEditing()
        {
            try
            {
                SetEditingFieldsEnabled();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Aux
        private void ResetBinding (Control.ControlCollection controls)
        {
            try
            {
                foreach(Control ctl in controls)
                {
                    if (ctl.GetType() == typeof(TextEdit))
                    {
                        ctl.DataBindings.Clear();
                        ((TextEdit)ctl).ReadOnly = true;
                    }
                    else if (ctl.GetType() == typeof(LookUpEdit))
                    {
                        ctl.DataBindings.Clear();
                        ((LookUpEdit)ctl).ReadOnly = true;
                    }
                    else if (ctl.GetType() == typeof(MemoEdit))
                    {
                        ctl.DataBindings.Clear();
                        ((MemoEdit)ctl).ReadOnly = true;
                    }
                    else if(ctl.HasChildren)
                    {
                        ResetBinding(ctl.Controls);
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        private void SetEditingFieldsEnabled()
        {
            try
            {
                EnableFields(xtpMyCompany.Controls);
            }
            catch
            {
                throw;
            }
        }

        private void EnableFields(Control.ControlCollection controls)
        {
            try
            {
                foreach (Control ctl in controls)
                {
                    if (ctl.Name != nameof(txtId))
                    {
                        if (ctl.GetType() == typeof(TextEdit))
                        {
                            ((TextEdit)ctl).ReadOnly = false;
                        }
                        else if (ctl.GetType() == typeof(LookUpEdit))
                        {
                            ((LookUpEdit)ctl).ReadOnly = false;
                        }
                        else if (ctl.GetType() == typeof(MemoEdit))
                        {
                            ((MemoEdit)ctl).ReadOnly = false;
                        }
                        else if (ctl.HasChildren)
                        {
                            EnableFields(ctl.Controls);
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Update
        private bool UpdateMyCompany()
        {
            try
            {
                return GlobalSetting.MyCompanyService.UpdateMyCompany(_myCompany);
            }
            catch
            {
                throw;
            }
        }

        private void ActionsAfterUpdate()
        {
            try
            {
                LoadMyCompany();
                SetFormBinding();
                RestoreInitState();
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
