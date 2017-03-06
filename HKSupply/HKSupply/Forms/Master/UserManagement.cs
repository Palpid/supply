﻿using HKSupply.Exceptions;
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
    public partial class UserManagement : Form
    {
        #region Enums
        private enum eUserColumns
        {
            Id,
            UserLogin,
            Password,
            Name,
            RoleId,
            UserRole,
            Enabled,
            LastLogin,
            LastLogout,
            Remarks,
        }
        #endregion

        #region Private members
        ResourceManager resManager = new ResourceManager("HKSupply.Resources.HKSupplyRes", typeof(RoleManagement).Assembly);

        CustomControls.StackView actionsStackView;

        List<Role> _roleList;

        List<User> _modifiedUsers = new List<User>();
        List<User> _createdUsers = new List<User>();
        #endregion

        #region Constructor
        public UserManagement()
        {
            InitializeComponent();
        }
        #endregion

        #region Form Events
        private void UserManagement_Load(object sender, EventArgs e)
        {
            ConfigureActionsStackView();
            LoadRoles();
            SetupUsersGrid();
            LoadAllUsers();
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
                grdUsers.EndEdit();

                DialogResult result = MessageBox.Show(resManager.GetString("SaveChanges"), "", MessageBoxButtons.YesNo);

                if (result != DialogResult.Yes)
                    return;

                if (actionsStackView.CurrentState == CustomControls.StackView.ToolbarStates.Edit)
                {
                    if (_modifiedUsers.Count() == 0)
                    {
                        MessageBox.Show(resManager.GetString("NoPendingChanges"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        if (IsValidModifiedUsers())
                        {
                            if (UpdateUsers())
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
                        if (CreateUser())
                        {
                            res = true;
                        }
                    }
                }

                if (res == true)
                {
                    LoadAllUsers();
                    ConfigureRolesColorsStyles();
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

        private void grdUsers_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (actionsStackView.CurrentState == CustomControls.StackView.ToolbarStates.Edit)
                {
                    User tmpUser = new User();
                    tmpUser.Id = (int)grdUsers.Rows[e.RowIndex].Cells[(int)eUserColumns.Id].Value;
                    tmpUser.UserLogin = grdUsers.Rows[e.RowIndex].Cells[(int)eUserColumns.UserLogin].Value.ToString();
                    tmpUser.Password = grdUsers.Rows[e.RowIndex].Cells[(int)eUserColumns.Password].Value.ToString();
                    tmpUser.Name = grdUsers.Rows[e.RowIndex].Cells[(int)eUserColumns.Name].Value.ToString();
                    tmpUser.RoleId = grdUsers.Rows[e.RowIndex].Cells[(int)eUserColumns.RoleId].Value.ToString();
                    tmpUser.Enabled = (bool)grdUsers.Rows[e.RowIndex].Cells[(int)eUserColumns.Enabled].Value;
                    tmpUser.Remarks = (grdUsers.Rows[e.RowIndex].Cells[(int)eUserColumns.Remarks].Value ?? string.Empty).ToString();
                    AddModifiedUserToList(tmpUser);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void grdUsers_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                switch (e.ColumnIndex)
                {
                    case (int)eUserColumns.Password:
                    case (int)eUserColumns.Name:
                        if (string.IsNullOrEmpty(e.FormattedValue.ToString()))
                        {
                            MessageBox.Show(resManager.GetString("FieldRequired"), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            e.Cancel = true;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //-----------------------------------------------------------------------------------------
        void grdUsers_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (grdUsers.Columns[e.ColumnIndex].Index == (int)eUserColumns.Password)
            {
                if (e.Value != null)
                {
                    e.Value = "********";
                    e.FormattingApplied = true;
                }
            }
        }

        void grdUsers_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            TextBox t = e.Control as TextBox;
            if (t != null)
            {
                t.Text = (string)grdUsers.SelectedCells[0].Value;
            }
        }

        //-----------------------------------------------------------------------------------------

        private void grdUsers_DataError(object sender, DataGridViewDataErrorEventArgs e)
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

        private void SetupUsersGrid()
        {
            try
            {
                //Events
                grdUsers.CellValueChanged += grdUsers_CellValueChanged;
                grdUsers.CellValidating += grdUsers_CellValidating;

                //aki test
                grdUsers.CellFormatting += new DataGridViewCellFormattingEventHandler(grdUsers_CellFormatting);
                grdUsers.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(grdUsers_EditingControlShowing);

                //Columns

                //adding Id TextBox
                DataGridViewTextBoxColumn columnId = new DataGridViewTextBoxColumn();
                columnId.HeaderText = eUserColumns.Id.ToString();
                columnId.Width = 50;
                columnId.DataPropertyName = eUserColumns.Id.ToString();

                grdUsers.Columns.Add(columnId);

                //adding Login TextBox
                DataGridViewTextBoxColumn columnLogin = new DataGridViewTextBoxColumn();
                columnLogin.HeaderText = eUserColumns.UserLogin.ToString();
                columnLogin.Width = 120;
                columnLogin.DataPropertyName = eUserColumns.UserLogin.ToString();

                grdUsers.Columns.Add(columnLogin);


                //Adding  Password TextBox
                DataGridViewTextBoxColumn columnPassword = new DataGridViewTextBoxColumn();
                columnPassword.HeaderText = eUserColumns.Password.ToString();
                columnPassword.Width = 120;
                columnPassword.DataPropertyName = eUserColumns.Password.ToString();

                grdUsers.Columns.Add(columnPassword);

                //Adding  Name TextBox
                DataGridViewTextBoxColumn columnName = new DataGridViewTextBoxColumn();
                columnName.HeaderText = eUserColumns.Name.ToString();
                columnName.Width = 150;
                columnName.DataPropertyName = eUserColumns.Name.ToString();

                grdUsers.Columns.Add(columnName);

                //Adding  Role Id Combo
                DataGridViewComboBoxColumn columnRoleId = new DataGridViewComboBoxColumn();
                columnRoleId.HeaderText = eUserColumns.RoleId.ToString();
                columnRoleId.DataPropertyName = eUserColumns.RoleId.ToString();
                columnRoleId.Width = 200;

                columnRoleId.DataSource = _roleList;
                columnRoleId.ValueMember = "RoleId";
                columnRoleId.DisplayMember = "Description";

                grdUsers.Columns.Add(columnRoleId);

                //Adding  Enabled CheckBox
                DataGridViewCheckBoxColumn columnEnabled = new DataGridViewCheckBoxColumn();
                columnEnabled.HeaderText = eUserColumns.Enabled.ToString();
                columnEnabled.Width = 60;
                columnEnabled.DataPropertyName = eUserColumns.Enabled.ToString();

                grdUsers.Columns.Add(columnEnabled);


                //Adding  Last Login TextBox
                DataGridViewTextBoxColumn columnLastLogin = new DataGridViewTextBoxColumn();
                columnLastLogin.HeaderText = eUserColumns.LastLogin.ToString();
                columnLastLogin.Width = 100;
                columnLastLogin.DataPropertyName = eUserColumns.LastLogin.ToString();

                grdUsers.Columns.Add(columnLastLogin);

                //Adding  Last Logoout TextBox
                DataGridViewTextBoxColumn columnLastLogout = new DataGridViewTextBoxColumn();
                columnLastLogout.HeaderText = eUserColumns.LastLogout.ToString();
                columnLastLogout.Width = 100;
                columnLastLogout.DataPropertyName = eUserColumns.LastLogout.ToString();

                grdUsers.Columns.Add(columnLastLogout);

                //Adding  remarks  TextBox
                DataGridViewTextBoxColumn columnRemarks = new DataGridViewTextBoxColumn();
                columnRemarks.HeaderText = eUserColumns.Remarks.ToString();
                columnRemarks.Width = 200;
                columnRemarks.DataPropertyName = eUserColumns.Remarks.ToString();

                grdUsers.Columns.Add(columnRemarks);

                //Adding  user Role
                DataGridViewTextBoxColumn columnUserRole = new DataGridViewTextBoxColumn();
                columnUserRole.HeaderText = eUserColumns.UserRole.ToString();
                columnUserRole.Width = 0;
                columnUserRole.DataPropertyName = eUserColumns.UserRole.ToString();
                columnUserRole.Visible = false;

                grdUsers.Columns.Add(columnUserRole);




            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ConfigureRolesColorsStyles()
        {
            try
            {
                foreach (DataGridViewColumn col in grdUsers.Columns)
                    col.DefaultCellStyle.ForeColor = Color.Black;

                grdUsers.ColumnHeadersDefaultCellStyle.BackColor = AppStyles.EtniaRed;
                grdUsers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                grdUsers.EnableHeadersVisualStyles = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadRoles()
        {
            try
            {
                _roleList = GlobalSetting.RoleCont.GetRoles(false).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void LoadAllUsers()
        {
            try
            {
                _modifiedUsers.Clear();
                _createdUsers.Clear();
                IEnumerable<User> users = GlobalSetting.UserCont.GetAllUsers();
                grdUsers.DataSource = users;
                grdUsers.ReadOnly = true;

                ConfigureRolesColorsStyles();
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
                grdUsers.ReadOnly = false;
                //make columns readonly
                grdUsers.Columns[(int)eUserColumns.Id].ReadOnly = true;
                grdUsers.Columns[(int)eUserColumns.Id].DefaultCellStyle.ForeColor = Color.Gray;

                grdUsers.Columns[(int)eUserColumns.UserLogin].ReadOnly = true;
                grdUsers.Columns[(int)eUserColumns.UserLogin].DefaultCellStyle.ForeColor = Color.Gray;

                grdUsers.Columns[(int)eUserColumns.LastLogin].ReadOnly = true;
                grdUsers.Columns[(int)eUserColumns.LastLogin].DefaultCellStyle.ForeColor = Color.Gray;

                grdUsers.Columns[(int)eUserColumns.LastLogout].ReadOnly = true;
                grdUsers.Columns[(int)eUserColumns.LastLogout].DefaultCellStyle.ForeColor = Color.Gray;
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
                _createdUsers.Add(new User());
                grdUsers.DataSource = null;
                grdUsers.Rows.Clear();
                grdUsers.DataSource = _createdUsers;
                grdUsers.ReadOnly = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AddModifiedUserToList(User modifiedUser)
        {
            try
            {
                var user = _modifiedUsers.FirstOrDefault(u => u.Id.Equals(modifiedUser.Id));
                if (user == null)
                {
                    _modifiedUsers.Add(modifiedUser);
                }
                else
                {
                    user.Password = PasswordHelper.GetHash(modifiedUser.Password);
                    user.Name = modifiedUser.Name;
                    user.RoleId = modifiedUser.RoleId;
                    user.Enabled = modifiedUser.Enabled;
                    user.Remarks = modifiedUser.Remarks;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AddCreatedUserToList(User createdUser)
        {
            try
            {
                var user = _createdUsers.FirstOrDefault(u => u.Id.Equals(createdUser.Id));
                if (user == null)
                {
                    _createdUsers.Add(createdUser);
                }
                else
                {
                    user.Password = PasswordHelper.GetHash(createdUser.Password);
                    user.Name = createdUser.Name;
                    user.RoleId = createdUser.RoleId;
                    user.Enabled = createdUser.Enabled;
                    user.Remarks = createdUser.Remarks;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsValidModifiedUsers()
        {
            try
            {
                foreach (var user in _modifiedUsers)
                {
                    if (string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.Name))
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
                foreach (var user in _createdUsers)
                {
                    if (string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.Name))
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

        private bool UpdateUsers()
        {
            try
            {
                return GlobalSetting.UserCont.UpdateUsers(_modifiedUsers);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool CreateUser()
        {
            try
            {
                GlobalSetting.UserCont.NewUser(_createdUsers.FirstOrDefault());
               return true;
            }
            catch (NewExistingUserException neuex)
            {
                MessageBox.Show(neuex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
