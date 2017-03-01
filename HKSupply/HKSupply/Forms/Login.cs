using HKSupply.Exceptions;
using HKSupply.General;
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

namespace HKSupply.Forms
{
    public partial class Login : Form
    {
        #region Constants
        private const int MSG_TIME = 2000;
        #endregion

        #region Private members
       ResourceManager resManager = new ResourceManager("HKSupply.Resources.HKSupplyRes", typeof(Login).Assembly);
        #endregion

        #region  Constructor
        public Login()
        {
            InitializeComponent();
        }
        #endregion

        #region Private Methods

        private void Login_Load(object sender, EventArgs e)
        {
            try
            {
                InitializeFormStyles();
                InitializeTexts();
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
                //Logo
                pbLogo.Image = Image.FromFile(@"Resources\Images\etnia_logo.jpg");

                //Sign in button
                btnSignIn.BackColor = AppStyles.EtniaRed;
                btnSignIn.FlatStyle = FlatStyle.Flat;
                btnSignIn.FlatAppearance.BorderColor = AppStyles.EtniaRed;
                btnSignIn.FlatAppearance.BorderSize = 1;

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
                lblLogin.Text = resManager.GetString("Login");
                lblPassword.Text = resManager.GetString("Password");
                btnSignIn.Text = resManager.GetString("SignIn");

                txtLogin.Text = string.Empty;
                txtPassword.Text = string.Empty;
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

        private bool IsValidFrom()
        {
            try
            {
                if (string.IsNullOrEmpty(txtLogin.Text))
                {
                    ShowMessage(resManager.GetString("LoginMandatory"));
                    return false;
                }

                if (string.IsNullOrEmpty(txtPassword.Text))
                {
                    ShowMessage(resManager.GetString("PasswordMandatory"));
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Form events
        private void btnSignIn_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValidFrom())
                {
                    var user = GlobalSetting.UserCont.GetUserByLoginPassword(txtLogin.Text, txtPassword.Text);
                    GlobalSetting.LoggedUser = user;
                    
                    DialogResult = DialogResult.OK;
                    
                    //Main frm = new Main();
                    //frm.Show();
                    
                }
            }
            catch (ArgumentNullException anex)
            {
                throw anex;
            }
            catch (NonexistentUserException)
            {
                ShowMessage(resManager.GetString("InvalidUser"));
            }
            catch (InvalidPasswordException)
            {
                ShowMessage(resManager.GetString("InvalidPassword"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
