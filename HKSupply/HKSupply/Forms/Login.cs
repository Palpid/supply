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
                //Si el usuario logado en Windows existe en la aplicación no mostramos la pantalla de login local
                //LoginWithWindowsUser();

                InitializeFormStyles();
                InitializeTexts();
                txtPassword.KeyDown +=txtPassword_KeyDown;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                lblLogin.Text = GlobalSetting.ResManager.GetString("Login");
                lblPassword.Text = GlobalSetting.ResManager.GetString("Password");
                btnSignIn.Text = GlobalSetting.ResManager.GetString("SignIn");

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

        private bool IsValidForm()
        {
            try
            {
                if (string.IsNullOrEmpty(txtLogin.Text))
                {
                    ShowMessage(GlobalSetting.ResManager.GetString("LoginMandatory"));
                    return false;
                }

                if (string.IsNullOrEmpty(txtPassword.Text))
                {
                    ShowMessage(GlobalSetting.ResManager.GetString("PasswordMandatory"));
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoginWithWindowsUser()
        {
            try
            {

                if (System.Diagnostics.Debugger.IsAttached) return;

                var windowsUser = Environment.UserName;
                var user = GlobalSetting.UserService.GetUserByLogin(windowsUser);
                if (user != null)
                {
                //    //Obtenemos si es un usuario de una fábrica. Tienen que estar en un grupo llamado FACT_DELIV_xxxx  donde xxxx es el código de la fábrica

                    string userFactory = null;

                //    //user.RoleId = Constants.ROLE_FACTORY; //TEST

                //    if (user.RoleId == Constants.ROLE_FACTORY)
                //    {
                //        using (PrincipalContext ctx = new PrincipalContext(ContextType.Domain))
                //        {
                //            // find a user
                //            UserPrincipal userPrincipal = UserPrincipal.FindByIdentity(ctx, Environment.UserName);

                //            if (userPrincipal != null)
                //            {
                //                // get the user's groups
                //                var groups = userPrincipal.GetAuthorizationGroups();

                //                foreach (GroupPrincipal group in groups)
                //                {
                //                    //TODO: hacerlo, en para test lo pongo a piñon
                                    //userFactory = "FA";
                //                }
                //            }

                //        }
                //    }

                    GlobalSetting.UserFactory = userFactory;

                    GlobalSetting.LoggedUser = user;
                    var functionalitiesRoles = GlobalSetting.FunctionalityRoleService.GetFunctionalitiesRole(GlobalSetting.LoggedUser.RoleId);
                    GlobalSetting.FunctionalitiesRoles = functionalitiesRoles;
                    DialogResult = DialogResult.OK;
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Form events
        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSignIn_Click(this, new EventArgs());
            }
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValidForm())
                {
                    var user = GlobalSetting.UserService.GetUserByLoginPassword(txtLogin.Text, txtPassword.Text);
                    GlobalSetting.LoggedUser = user;
                    var functionalitiesRoles = GlobalSetting.FunctionalityRoleService.GetFunctionalitiesRole(GlobalSetting.LoggedUser.RoleId);
                    GlobalSetting.FunctionalitiesRoles = functionalitiesRoles;
                    
                    DialogResult = DialogResult.OK;
                    
                }
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message);
            }
        }
        #endregion
    }
}
