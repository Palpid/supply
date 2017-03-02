using HKSupply.General;
using HKSupply.Models;
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
    public partial class RoleManagement : Form
    {
        #region Private members
        ResourceManager resManager = new ResourceManager("HKSupply.Resources.HKSupplyRes", typeof(RoleManagement).Assembly);
        #endregion

        #region Constructor
        public RoleManagement()
        {
            InitializeComponent();
        }
        #endregion


        #region Form Events

        private void RoleManagement_Load(object sender, EventArgs e)
        {
            ConfigureActionsStackView();
            LoadAllRoles();
        }

        #endregion

        #region Private Methods

        private void ConfigureActionsStackView()
        {
            try
            {
                var actions = GlobalSetting.FunctionalitiesRoles.FirstOrDefault(fr => fr.Functionality.FormName.Equals(Name));

                CustomControls.StackView actionsStackView = new CustomControls.StackView(actions.Read, actions.New, actions.Modify);
                Controls.Add(actionsStackView);

            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        private void LoadAllRoles()
        {
            try
            {
                IEnumerable<Role> appRoles = GlobalSetting.RoleCont.GetAllRoles();
                grdRoles.DataSource = appRoles;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
