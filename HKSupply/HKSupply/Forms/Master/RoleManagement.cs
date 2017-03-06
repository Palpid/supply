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
    public partial class RoleManagement : Form
    {

        #region Enums
        private enum eRoleColumns
        {
            roleId,
            Description,
            Enabled,
            Remarks
        }
        #endregion

        #region Private members
        ResourceManager resManager = new ResourceManager("HKSupply.Resources.HKSupplyRes", typeof(RoleManagement).Assembly);

        CustomControls.StackView actionsStackView;

        List<Role> _modifiedRoles = new List<Role>();
        List<Role> _createdRoles = new List<Role>();
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
            SetupRolesGrid();
            LoadAllRoles();
        }

        #region Action toolbar events

        private void actionsStackView_EditButtonClick(object sender, EventArgs e)
        {
            try
            {
                //MessageBox.Show("Edit Button");
                ConfigureActionsStackViewEditing();
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
                //MessageBox.Show("New Button");
                ConfigureActionsStackViewCreating();
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
                bool res = false;
                //indicamos que ha dejado de editar el grid, por si modifica una celda y sin salir pulsa sobre guardar
                grdRoles.EndEdit();

                DialogResult result = MessageBox.Show(resManager.GetString("SaveChanges"), "", MessageBoxButtons.YesNo);

                if (result != DialogResult.Yes)
                    return;

                if (actionsStackView.CurrentState == CustomControls.StackView.ToolbarStates.Edit)
                {
                    if (_modifiedRoles.Count() == 0)
                    {
                        MessageBox.Show(resManager.GetString("NoPendingChanges"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        if (IsValidModifiedRoles())
                        {
                            if (UpdateRoles())
                            {
                                res = true;
                            }
                        }

                    }
                }
                else if (actionsStackView.CurrentState == CustomControls.StackView.ToolbarStates.New)
                {
                    if (IsValidCreatedRoles())
                    {
                        if (CreateRol())
                        {
                            res = true;
                        }
                    }
                }

                if (res == true)
                {
                    LoadAllRoles();
                    ConfigureRolesGridDefaultStyles();
                    actionsStackView.RestoreInitState();
                }

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
                MessageBox.Show("Cancel Button");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

        #region Grid events

        private void grdRoles_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (actionsStackView.CurrentState == CustomControls.StackView.ToolbarStates.Edit)
                {
                    Role tmpRole = new Role();
                    tmpRole.RoleId = grdRoles.Rows[e.RowIndex].Cells[(int)eRoleColumns.roleId].Value.ToString();
                    tmpRole.Description = (grdRoles.Rows[e.RowIndex].Cells[(int)eRoleColumns.Description].Value ?? string.Empty).ToString();
                    tmpRole.Enabled = (bool)grdRoles.Rows[e.RowIndex].Cells[(int)eRoleColumns.Enabled].Value;
                    tmpRole.Remarks = (grdRoles.Rows[e.RowIndex].Cells[(int)eRoleColumns.Remarks].Value ?? String.Empty).ToString();
                    AddModifiedRolesToList(tmpRole);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void grdRoles_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == (int)eRoleColumns.Description)
                {
                    if (string.IsNullOrEmpty(e.FormattedValue.ToString()))
                    {
                        MessageBox.Show(resManager.GetString("FieldRequired"), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void grdRoles_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //show this message when the user enter incorrect value in a cell
            MessageBox.Show(resManager.GetString("CellDataError"), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void SetupRolesGrid()
        {
            try
            {
                grdRoles.CellValueChanged += grdRoles_CellValueChanged;
                grdRoles.CellValidating += grdRoles_CellValidating;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ConfigureRolesGridDefaultStyles()
        {
            try
            {
                grdRoles.Columns[(int)eRoleColumns.roleId].Width = 100;
                grdRoles.Columns[(int)eRoleColumns.Description].Width = 200;
                grdRoles.Columns[(int)eRoleColumns.Enabled].Width = 100;
                grdRoles.Columns[(int)eRoleColumns.Remarks].Width = 300;

                grdRoles.Columns[(int)eRoleColumns.roleId].DefaultCellStyle.ForeColor = Color.Black;

                grdRoles.ColumnHeadersDefaultCellStyle.BackColor = AppStyles.EtniaRed;
                grdRoles.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                grdRoles.EnableHeadersVisualStyles = false;
            } 
            catch (Exception ex)
            {
                throw ex;
            }
        }
           


        private void LoadAllRoles()
        {
            try
            {
                _modifiedRoles.Clear();
                _createdRoles.Clear();
                IEnumerable<Role> appRoles = GlobalSetting.RoleCont.GetRoles();
                grdRoles.DataSource = appRoles;
                grdRoles.ReadOnly = true;

                ConfigureRolesGridDefaultStyles();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ConfigureActionsStackViewEditing()
        {
            try
            {
                grdRoles.ReadOnly = false;
                grdRoles.Columns[(int)eRoleColumns.roleId].ReadOnly = true; //make the id column readonly
                grdRoles.Columns[(int)eRoleColumns.roleId].DefaultCellStyle.ForeColor = Color.Gray;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ConfigureActionsStackViewCreating()
        {
            try
            {
                _createdRoles.Add(new Role());
                grdRoles.DataSource = null;
                grdRoles.Rows.Clear();
                grdRoles.DataSource = _createdRoles;
                grdRoles.ReadOnly = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AddModifiedRolesToList(Role modifiedRole)
        {
            try
            {
                var role = _modifiedRoles.FirstOrDefault(r => r.RoleId.Equals(modifiedRole.RoleId));
                if (role == null)
                {
                    _modifiedRoles.Add(modifiedRole);
                }
                else
                {
                    role.Description = modifiedRole.Description;
                    role.Enabled = modifiedRole.Enabled;
                    role.Remarks = modifiedRole.Remarks;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AddCreatedRolesToList(Role createdRole)
        {
            try
            {
                var role = _createdRoles.FirstOrDefault(r => r.RoleId.Equals(createdRole.RoleId));
                if (role == null)
                {
                    _createdRoles.Add(createdRole);
                }
                else
                {
                    role.Description = createdRole.Description;
                    role.Enabled = createdRole.Enabled;
                    role.Remarks = createdRole.Remarks;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsValidModifiedRoles()
        {
            try
            {
                foreach (var rol in _modifiedRoles)
                {
                    if (string.IsNullOrEmpty(rol.Description))
                    {
                        MessageBox.Show(resManager.GetString("FieldRequired"), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsValidCreatedRoles()
        {
            try
            {
                foreach (var rol in _createdRoles)
                {
                    if (string.IsNullOrEmpty(rol.Description) || string.IsNullOrEmpty(rol.RoleId))
                    {
                        MessageBox.Show(resManager.GetString("FieldRequired"), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool UpdateRoles()
        {
            try
            {
                return GlobalSetting.RoleCont.UpdateRoles(_modifiedRoles);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool CreateRol()
        {
            try
            {
                GlobalSetting.RoleCont.NewRole(_createdRoles.FirstOrDefault());
                return true;
            }
            catch (NewExistingRoleException nerex)
            {
                MessageBox.Show(nerex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
