using HKSupply.Exceptions;
using HKSupply.General;
using HKSupply.Helpers;
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

    public partial class ChangePassword : Form
    {
        #region Constants
        private const int MSG_TIME = 2000;
        #endregion

        #region enums
        enum eMode
        {
            Admin,
            User
        }
        #endregion

        #region Private members

        User _userChangePassword;
        eMode _currentMode = eMode.User;
        #endregion

        #region Constructor
        public ChangePassword()
        {
            InitializeComponent();
            _userChangePassword = GlobalSetting.LoggedUser;
        }

        public ChangePassword(User user)
        {
            InitializeComponent();
            InitData(user);
        }
        #endregion

        #region Events

        private void ChangePassword_Load(object sender, EventArgs e)
        {
            try
            {
                InitializeFormStyles();
                InitializeTexts();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValidForm())
                {
                    if (Save())
                    {
                        MessageBox.Show(GlobalSetting.ResManager.GetString("PasswordChangedSuccessfully")); 
                        Close();
                    }
                
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Private Methods

        private void InitData(User user)
        {
            try
            {
                _userChangePassword = user;
                _currentMode = eMode.Admin;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void InitializeFormStyles()
        {
            try
            {
                //Save button
                btnSave.BackColor = AppStyles.EtniaRed;
                btnSave.FlatStyle = FlatStyle.Flat;
                btnSave.FlatAppearance.BorderColor = AppStyles.EtniaRed;
                btnSave.FlatAppearance.BorderSize = 1;

                lblOldPassword.Visible = _currentMode == eMode.Admin ? false : true;
                txtOldPassword.Visible = _currentMode == eMode.Admin ? false : true;

                //Message bar
                lblMsgBar.Text = string.Empty;
                lblMsgBar.Visible = false;
                lblMsgBar.ForeColor = AppStyles.EtniaRed;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void InitializeTexts()
        {
            try
            {
                lblOldPassword.Text = GlobalSetting.ResManager.GetString("OldPassword");
                lblNewPassword.Text = GlobalSetting.ResManager.GetString("NewPassword");
                lblRepeatPassword.Text = GlobalSetting.ResManager.GetString("RepeatPassword");
                btnSave.Text = GlobalSetting.ResManager.GetString("Save");

                txtOldPassword.Text = string.Empty;
                txtNewPassword.Text = string.Empty;
                txtRepeatPassword.Text = string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private async void ShowMessage(string msg)
        {
            try
            {
                lblMsgBar.Text = msg;
                lblMsgBar.Visible = true;
                await Task.Delay(MSG_TIME);
                lblMsgBar.Visible = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsValidForm()
        {
            try
            {
                if (_currentMode == eMode.User)
                {
                    if (string.IsNullOrEmpty(txtOldPassword.Text))
                    {
                        ShowMessage(GlobalSetting.ResManager.GetString("OldPasswordMandatory"));
                        return false;
                    }

                    if (PasswordHelper.ValidatePass(txtOldPassword.Text, _userChangePassword.Password) == false)
                    {
                        ShowMessage(GlobalSetting.ResManager.GetString("OldPasswordError"));
                        return false;
                    }

                }


                if (string.IsNullOrEmpty(txtNewPassword.Text))
                {
                    ShowMessage(GlobalSetting.ResManager.GetString("NewPasswordMandatory"));
                    return false;
                }

                if (string.IsNullOrEmpty(txtRepeatPassword.Text))
                {
                    ShowMessage(GlobalSetting.ResManager.GetString("RepeatPasswordMandatory"));
                    return false;
                }

                if (txtNewPassword.Text != txtRepeatPassword.Text)
                {
                    ShowMessage(GlobalSetting.ResManager.GetString("RepeatPasswordError"));
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool Save()
        {
            try
            {
                return GlobalSetting.UserService.ChangePassword(_userChangePassword.Id, PasswordHelper.GetHash(txtNewPassword.Text));
                 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}
