using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Menu;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using HKSupply.Classes;
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
    public partial class UserManagement : RibbonFormBase
    {

        #region Enums
        private enum eUserColumns
        {
            Id,
            UserLogin,
            Password,
            Name,
            RoleId,
            Enabled,
            LastLogin,
            LastLogout,
            Remarks,
        }
        #endregion

        #region Private Members

        List<Role> _roleList;

        List<User> _modifiedUsers = new List<User>();
        List<User> _createdUsers = new List<User>();
        
        #endregion

        #region Constructor
        public UserManagement()
        {
            InitializeComponent();

            ConfigureRibbonActions();
            LoadRoles();
            SetUpGrdUsers();
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
                LoadAllUsers();
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
                ConfigureRibbonActionsEditing();
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

                if (IsValidUsers() == false)
                    return;

                DialogResult result = MessageBox.Show(GlobalSetting.ResManager.GetString("SaveChanges"), "", MessageBoxButtons.YesNo);

                if (result != DialogResult.Yes)
                    return;

                if (CurrentState == ActionsStates.Edit)
                {
                    if (_modifiedUsers.Count() == 0)
                    {
                        MessageBox.Show(GlobalSetting.ResManager.GetString("NoPendingChanges"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        res = UpdateUsers();
                    }
                }
                else if (CurrentState == ActionsStates.New)
                {
                    res = CreateUser();
                }

                if (res == true)
                {
                    MessageBox.Show(GlobalSetting.ResManager.GetString("SaveSuccessfully"));
                    LoadAllUsers();
                    RestoreInitState();
                    SetGridStylesByState();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Form Events

        private void UserManagement_Load(object sender, EventArgs e)
        {
            try
            {
                LoadAllUsers();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        #region Grid Events
        void rootGridViewUsers_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                if (CurrentState == ActionsStates.Edit)
                {
                    object id = view.GetRowCellValue(view.FocusedRowHandle, eUserColumns.Id.ToString());
                    object userLogin = view.GetRowCellValue(view.FocusedRowHandle, eUserColumns.UserLogin.ToString());
                    object password = view.GetRowCellValue(view.FocusedRowHandle, eUserColumns.Password.ToString());
                    object name = view.GetRowCellValue(view.FocusedRowHandle, eUserColumns.Name.ToString());
                    object roleid = view.GetRowCellValue(view.FocusedRowHandle, eUserColumns.RoleId.ToString());
                    object enabled = view.GetRowCellValue(view.FocusedRowHandle, eUserColumns.Enabled.ToString());
                    object remarks = view.GetRowCellValue(view.FocusedRowHandle, eUserColumns.Remarks.ToString());

                    User tmpUser = new User();
                    tmpUser.Id = (int)id;
                    tmpUser.UserLogin = (userLogin ?? string.Empty).ToString();
                    tmpUser.Password = (password ?? string.Empty).ToString();
                    tmpUser.Name = (name ?? string.Empty).ToString();
                    tmpUser.RoleId = (roleid ?? string.Empty).ToString();
                    tmpUser.Enabled = (bool)enabled;
                    tmpUser.Remarks = (remarks ?? string.Empty).ToString();
                    AddModifiedUserToList(tmpUser);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void rootGridViewUsers_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                if (view.FocusedColumn.FieldName == eUserColumns.Name.ToString() ||
                    view.FocusedColumn.FieldName == eUserColumns.UserLogin.ToString() ||
                    view.FocusedColumn.FieldName == eUserColumns.Password.ToString())
                {
                    if (string.IsNullOrEmpty(e.Value as string))
                    {
                        e.Valid = false;
                        e.ErrorText = GlobalSetting.ResManager.GetString("FieldRequired");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void rootGridViewUsers_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;

                if (e.MenuType == GridMenuType.Row)
                {
                    int rowHandle = e.HitInfo.RowHandle;
                    e.Menu.Items.Clear();
                    e.Menu.Items.Add(CreateMenuItemChangePassword(view, rowHandle));

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        DXMenuItem CreateMenuItemChangePassword(GridView view, int rowHandle)
        {
            DXMenuItem menuItem = new DXMenuItem(GlobalSetting.ResManager.GetString("ResetPassword"),
                new EventHandler(OnMenuItemChangePasswordClick));
            menuItem.Tag = new RowInfo(view, rowHandle);
            return menuItem;
        }

        void OnMenuItemChangePasswordClick(object sender, EventArgs e)
        {
            try
            {
                DXMenuItem item = sender as DXMenuItem;
                RowInfo info = item.Tag as RowInfo;

                object id = info.View.GetRowCellValue(info.RowHandle, eUserColumns.Id.ToString());

                User user = GlobalSetting.UserService.GetUserById((int)id);
                Form frm = new ChangePassword(user);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #endregion

        #region Private Members
        private void SetUpGrdUsers()
        {
            try
            {
                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                rootGridViewUsers.OptionsView.ColumnAutoWidth = false;
                rootGridViewUsers.HorzScrollVisibility = ScrollVisibility.Auto;

                ////Columns definition
                GridColumn colId = new GridColumn() { Caption = "Id", Visible = false, FieldName = eUserColumns.Id.ToString(), Width = 50 };
                GridColumn colUserLogin = new GridColumn() { Caption = "User Login", Visible = true, FieldName = eUserColumns.UserLogin.ToString(), Width = 150 };
                GridColumn colPassword = new GridColumn() { Caption = "Password", Visible = false, FieldName = eUserColumns.Password.ToString(), Width = 100 };
                GridColumn colName = new GridColumn() { Caption = "Name", Visible = true, FieldName = eUserColumns.Name.ToString(), Width = 200  };
                GridColumn colRoleId = new GridColumn() { Caption = "Role", Visible = true, FieldName = eUserColumns.RoleId.ToString(), Width = 150 };
                GridColumn colEnabled = new GridColumn() { Caption = "Enabled", Visible = true, FieldName = eUserColumns.Enabled.ToString(), Width = 50 };
                GridColumn colLastLogin = new GridColumn() { Caption = "Last Login", Visible = true, FieldName = eUserColumns.LastLogin.ToString(), Width = 120 };
                GridColumn colLastLogout = new GridColumn() { Caption = "Last Logout", Visible = true, FieldName = eUserColumns.LastLogout.ToString(), Width = 120 };
                GridColumn colRemarks = new GridColumn() { Caption = "Remarks", Visible = true, FieldName = eUserColumns.Remarks.ToString(), Width = 300 };

                //Format type
                colLastLogin.DisplayFormat.FormatType = FormatType.DateTime;
                colLastLogout.DisplayFormat.FormatType = FormatType.DateTime;

                //Combobox repository for user Role
                RepositoryItemLookUpEdit riComboRole = new RepositoryItemLookUpEdit();
                riComboRole.DataSource = _roleList;
                riComboRole.ValueMember = "RoleId";
                riComboRole.DisplayMember = "Description";
                riComboRole.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("RoleId", 40, "Item Code"));
                riComboRole.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Description", 60, "Item Name"));

                //Password repository
                RepositoryItemTextEdit ritxtPassword = new RepositoryItemTextEdit();
                ritxtPassword.PasswordChar = '*';


                colRoleId.ColumnEdit = riComboRole;
                colPassword.ColumnEdit = ritxtPassword;

                //add columns to grid root view
                rootGridViewUsers.Columns.Add(colId);
                rootGridViewUsers.Columns.Add(colUserLogin);
                rootGridViewUsers.Columns.Add(colPassword);
                rootGridViewUsers.Columns.Add(colName);
                rootGridViewUsers.Columns.Add(colRoleId);
                rootGridViewUsers.Columns.Add(colEnabled);
                rootGridViewUsers.Columns.Add(colLastLogin);
                rootGridViewUsers.Columns.Add(colLastLogout);
                rootGridViewUsers.Columns.Add(colRemarks);


                //Events
                rootGridViewUsers.ValidatingEditor += rootGridViewUsers_ValidatingEditor;
                rootGridViewUsers.CellValueChanged += rootGridViewUsers_CellValueChanged;
                if (Modify)
                    rootGridViewUsers.PopupMenuShowing += rootGridViewUsers_PopupMenuShowing;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetColumnGridOrder()
        {
            try
            {
                rootGridViewUsers.Columns[eUserColumns.Id.ToString()].VisibleIndex = 0;
                rootGridViewUsers.Columns[eUserColumns.UserLogin.ToString()].VisibleIndex = 1;
                rootGridViewUsers.Columns[eUserColumns.Password.ToString()].VisibleIndex = 2;
                rootGridViewUsers.Columns[eUserColumns.Name.ToString()].VisibleIndex = 3;
                rootGridViewUsers.Columns[eUserColumns.RoleId.ToString()].VisibleIndex = 4;
                rootGridViewUsers.Columns[eUserColumns.Enabled.ToString()].VisibleIndex = 5;
                rootGridViewUsers.Columns[eUserColumns.LastLogin.ToString()].VisibleIndex = 6;
                rootGridViewUsers.Columns[eUserColumns.LastLogout.ToString()].VisibleIndex = 6;
                rootGridViewUsers.Columns[eUserColumns.Remarks.ToString()].VisibleIndex = 6;
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        private void LoadRoles()
        {
            try
            {
                _roleList = GlobalSetting.RoleService.GetRoles(false).ToList();
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
                IEnumerable<User> users = GlobalSetting.UserService.GetAllUsers();
                xgrdUsers.DataSource = users;

                rootGridViewUsers.Columns[eUserColumns.Id.ToString()].OptionsColumn.AllowEdit = false;
                rootGridViewUsers.Columns[eUserColumns.UserLogin.ToString()].OptionsColumn.AllowEdit = false;
                rootGridViewUsers.Columns[eUserColumns.Password.ToString()].OptionsColumn.AllowEdit = false;
                rootGridViewUsers.Columns[eUserColumns.Name.ToString()].OptionsColumn.AllowEdit = false;
                rootGridViewUsers.Columns[eUserColumns.RoleId.ToString()].OptionsColumn.AllowEdit = false;
                rootGridViewUsers.Columns[eUserColumns.Enabled.ToString()].OptionsColumn.AllowEdit = false;
                rootGridViewUsers.Columns[eUserColumns.LastLogin.ToString()].OptionsColumn.AllowEdit = false;
                rootGridViewUsers.Columns[eUserColumns.LastLogout.ToString()].OptionsColumn.AllowEdit = false;
                rootGridViewUsers.Columns[eUserColumns.Remarks.ToString()].OptionsColumn.AllowEdit = false;


                SetGridStylesByState();
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
                CurrentState = ActionsStates.Edit;

                //Allow edit some columns
                rootGridViewUsers.Columns[eUserColumns.Name.ToString()].OptionsColumn.AllowEdit = true;
                rootGridViewUsers.Columns[eUserColumns.RoleId.ToString()].OptionsColumn.AllowEdit = true;
                rootGridViewUsers.Columns[eUserColumns.Enabled.ToString()].OptionsColumn.AllowEdit = true;
                rootGridViewUsers.Columns[eUserColumns.Remarks.ToString()].OptionsColumn.AllowEdit = true;


                //no edit column
                rootGridViewUsers.Columns[eUserColumns.Id.ToString()].OptionsColumn.AllowEdit = false;
                rootGridViewUsers.Columns[eUserColumns.UserLogin.ToString()].OptionsColumn.AllowEdit = false;
                rootGridViewUsers.Columns[eUserColumns.LastLogin.ToString()].OptionsColumn.AllowEdit = false;
                rootGridViewUsers.Columns[eUserColumns.LastLogout.ToString()].OptionsColumn.AllowEdit = false;

                SetGridStylesByState();


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

                _createdUsers.Add(new User());
                xgrdUsers.DataSource = null;
                xgrdUsers.DataSource = _createdUsers;

                //Allow edit some columns
                rootGridViewUsers.Columns[eUserColumns.UserLogin.ToString()].OptionsColumn.AllowEdit = true;
                rootGridViewUsers.Columns[eUserColumns.Password.ToString()].OptionsColumn.AllowEdit = true;
                rootGridViewUsers.Columns[eUserColumns.Name.ToString()].OptionsColumn.AllowEdit = true;
                rootGridViewUsers.Columns[eUserColumns.RoleId.ToString()].OptionsColumn.AllowEdit = true;
                rootGridViewUsers.Columns[eUserColumns.Enabled.ToString()].OptionsColumn.AllowEdit = true;
                rootGridViewUsers.Columns[eUserColumns.Remarks.ToString()].OptionsColumn.AllowEdit = true;

                //no edit column
                rootGridViewUsers.Columns[eUserColumns.LastLogin.ToString()].OptionsColumn.AllowEdit = false;
                rootGridViewUsers.Columns[eUserColumns.LastLogout.ToString()].OptionsColumn.AllowEdit = false;

                SetGridStylesByState();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetGridStylesByState()
        {
            try
            {
                SetColumnGridOrder();

                switch (CurrentState)
                {
                    case ActionsStates.Edit:
                        rootGridViewUsers.ClearGrouping();
                        //hide group panel.
                        rootGridViewUsers.OptionsView.ShowGroupPanel = false;
                        rootGridViewUsers.OptionsCustomization.AllowGroup = false;
                        rootGridViewUsers.OptionsCustomization.AllowColumnMoving = false;
                        //hide columns
                        rootGridViewUsers.Columns[eUserColumns.Id.ToString()].Visible = false;
                        rootGridViewUsers.Columns[eUserColumns.Password.ToString()].Visible = false;
                        rootGridViewUsers.Columns[eUserColumns.LastLogin.ToString()].Visible = false;
                        rootGridViewUsers.Columns[eUserColumns.LastLogout.ToString()].Visible = false;
                        //Change forecolor
                        rootGridViewUsers.Columns[eUserColumns.Id.ToString()].AppearanceCell.ForeColor = Color.Gray;
                        rootGridViewUsers.Columns[eUserColumns.UserLogin.ToString()].AppearanceCell.ForeColor = Color.Gray;
                        rootGridViewUsers.Columns[eUserColumns.LastLogin.ToString()].AppearanceCell.ForeColor = Color.Gray;
                        rootGridViewUsers.Columns[eUserColumns.LastLogout.ToString()].AppearanceCell.ForeColor = Color.Gray;

                        break;
                    case ActionsStates.New:
                        rootGridViewUsers.ClearGrouping();
                        //hide group panel.
                        rootGridViewUsers.OptionsView.ShowGroupPanel = false;
                        rootGridViewUsers.OptionsCustomization.AllowGroup = false;
                        rootGridViewUsers.OptionsCustomization.AllowColumnMoving = false;
                        //hide columns
                        rootGridViewUsers.Columns[eUserColumns.Id.ToString()].Visible = false;
                        rootGridViewUsers.Columns[eUserColumns.LastLogin.ToString()].Visible = false;
                        rootGridViewUsers.Columns[eUserColumns.LastLogout.ToString()].Visible = false;

                        break;

                    default:
                        //unhide group panel.
                        rootGridViewUsers.OptionsView.ShowGroupPanel = true;
                        rootGridViewUsers.OptionsCustomization.AllowGroup = true;
                        rootGridViewUsers.OptionsCustomization.AllowColumnMoving = true;

                        rootGridViewUsers.Columns[eUserColumns.Id.ToString()].Visible = false;
                        rootGridViewUsers.Columns[eUserColumns.LastLogin.ToString()].Visible = true;
                        rootGridViewUsers.Columns[eUserColumns.LastLogout.ToString()].Visible = true;
                        rootGridViewUsers.Columns[eUserColumns.Password.ToString()].Visible = false;

                        rootGridViewUsers.Columns[eUserColumns.Id.ToString()].AppearanceCell.ForeColor = Color.Black;
                        rootGridViewUsers.Columns[eUserColumns.UserLogin.ToString()].AppearanceCell.ForeColor = Color.Black;
                        rootGridViewUsers.Columns[eUserColumns.LastLogin.ToString()].AppearanceCell.ForeColor = Color.Black;
                        rootGridViewUsers.Columns[eUserColumns.LastLogout.ToString()].AppearanceCell.ForeColor = Color.Black;

                        break;
                }

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

        private bool IsValidUsers()
        {
            try
            {
                if (CurrentState == ActionsStates.Edit)
                    return IsValidModifiedUsers();
                else if (CurrentState == ActionsStates.New)
                    return IsValidCreatedRoles();

                return false;
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
                        MessageBox.Show(GlobalSetting.ResManager.GetString("FieldRequired"), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    if (string.IsNullOrEmpty(user.UserLogin) || string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.Name))
                    {
                        MessageBox.Show(GlobalSetting.ResManager.GetString("FieldRequired"), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    if (user.UserLogin.IndexOf(" ") > 0)
                    {
                        //MessageBox.Show(resManager.GetString("FieldRequired"), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        MessageBox.Show("Invalid Login", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                return GlobalSetting.UserService.UpdateUsers(_modifiedUsers);
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
                User user = _createdUsers.FirstOrDefault().Clone();
                user.Password = PasswordHelper.GetHash(user.Password);
                GlobalSetting.UserService.NewUser(user);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }

}
