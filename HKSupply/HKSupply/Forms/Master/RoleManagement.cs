using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
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
    public partial class RoleManagement : RibbonFormBase
    {
        #region Enums
        private enum eRoleColumns
        {
            RoleId,
            Description,
            Enabled,
            Remarks
        }
        #endregion

        #region Private Members

        List<Role> _modifiedRoles = new List<Role>();
        List<Role> _createdRoles = new List<Role>();

        #endregion

        #region Constructor
        public RoleManagement()
        {
            InitializeComponent();

            ConfigureRibbonActions();
            SetUpGrdRoles();
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
                ConfigureActionsStackViewCreating();
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

                if (IsValidRoles() == false)
                    return;

                DialogResult result = XtraMessageBox.Show(GlobalSetting.ResManager.GetString("SaveChanges"), "", MessageBoxButtons.YesNo);

                if (result != DialogResult.Yes)
                    return;

                if (CurrentState == ActionsStates.Edit)
                {
                    if (_modifiedRoles.Count() == 0)
                    {
                        MessageBox.Show(GlobalSetting.ResManager.GetString("NoPendingChanges"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        res = UpdateRoles(); 
                    }
                }
                else if (CurrentState == ActionsStates.New)
                {
                    res = CreateRole();
                }

                if (res == true)
                {
                    MessageBox.Show(GlobalSetting.ResManager.GetString("SaveSuccessfully"));
                    LoadAllRoles();
                    RestoreInitState();
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public override void bbiCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            base.bbiCancel_ItemClick(sender, e);

            try
            {
                LoadAllRoles();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Form Events
        private void RoleManagement_Load(object sender, EventArgs e)
        {
            try
            {
                LoadAllRoles();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Grid Events
        void rootGridViewRoles_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                if (view.FocusedColumn.FieldName == eRoleColumns.Description.ToString() || 
                    view.FocusedColumn.FieldName == eRoleColumns.RoleId.ToString())
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

        void rootGridViewRoles_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;

                if (CurrentState == ActionsStates.Edit)
                {
                    Role tmpRole = new Role();

                    object roleId = view.GetRowCellValue(view.FocusedRowHandle, eRoleColumns.RoleId.ToString());
                    object description = view.GetRowCellValue(view.FocusedRowHandle, eRoleColumns.Description.ToString());
                    object enabled = view.GetRowCellValue(view.FocusedRowHandle, eRoleColumns.Enabled.ToString());
                    object remarks = view.GetRowCellValue(view.FocusedRowHandle, eRoleColumns.Remarks.ToString());

                    tmpRole.RoleId = (roleId ?? string.Empty).ToString();
                    tmpRole.Description = (description ?? string.Empty).ToString();
                    tmpRole.Enabled = (bool)enabled;
                    tmpRole.Remarks = (remarks ?? string.Empty).ToString();
                    AddModifiedRolesToList(tmpRole);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #endregion

        #region Private Methods

        private void SetUpGrdRoles()
        {
            try
            {
                //hide group panel.
                rootGridViewRoles.OptionsView.ShowGroupPanel = false;
                rootGridViewRoles.OptionsCustomization.AllowGroup = false;
                rootGridViewRoles.OptionsCustomization.AllowColumnMoving = false;

                //Columns definition
                GridColumn colRoleId = new GridColumn() { Caption = "Role Id", Visible = true, FieldName = eRoleColumns.RoleId.ToString() };
                GridColumn colDescription = new GridColumn() { Caption = "Description", Visible = true, FieldName = eRoleColumns.Description.ToString() };
                GridColumn colEnabled = new GridColumn() { Caption = "Enabled", Visible = true, FieldName = eRoleColumns.Enabled.ToString() };
                GridColumn colRemarks = new GridColumn() { Caption = "Remarks", Visible = true, FieldName = eRoleColumns.Remarks.ToString() };

                //add columns to grid root view
                rootGridViewRoles.Columns.Add(colRoleId);
                rootGridViewRoles.Columns.Add(colDescription);
                rootGridViewRoles.Columns.Add(colEnabled);
                rootGridViewRoles.Columns.Add(colRemarks);

                //Events
                rootGridViewRoles.ValidatingEditor += rootGridViewRoles_ValidatingEditor;
                rootGridViewRoles.CellValueChanged += rootGridViewRoles_CellValueChanged;
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
                _modifiedRoles.Clear();
                _createdRoles.Clear();
                IEnumerable<Role> roles = GlobalSetting.RoleService.GetRoles();

                xgrdRoles.DataSource = roles;

                rootGridViewRoles.Columns[eRoleColumns.RoleId.ToString()].OptionsColumn.AllowEdit = false;
                rootGridViewRoles.Columns[eRoleColumns.Description.ToString()].OptionsColumn.AllowEdit = false;
                rootGridViewRoles.Columns[eRoleColumns.Enabled.ToString()].OptionsColumn.AllowEdit = false;
                rootGridViewRoles.Columns[eRoleColumns.Remarks.ToString()].OptionsColumn.AllowEdit = false;

                //TODO: gestion de estilos del grid
                rootGridViewRoles.Columns[eRoleColumns.RoleId.ToString()].AppearanceCell.ForeColor = Color.Black;
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
                rootGridViewRoles.Columns[eRoleColumns.Description.ToString()].OptionsColumn.AllowEdit = true;
                rootGridViewRoles.Columns[eRoleColumns.Enabled.ToString()].OptionsColumn.AllowEdit = true;
                rootGridViewRoles.Columns[eRoleColumns.Remarks.ToString()].OptionsColumn.AllowEdit = true;

                //no edit column
                rootGridViewRoles.Columns[eRoleColumns.RoleId.ToString()].OptionsColumn.AllowEdit = false;
                //TODO: gestion de estilos del grid
                rootGridViewRoles.Columns[eRoleColumns.RoleId.ToString()].AppearanceCell.ForeColor = Color.Gray;

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
                xgrdRoles.DataSource = null ;
                xgrdRoles.DataSource = _createdRoles;
                //Allow edit all columns
                rootGridViewRoles.Columns[eRoleColumns.RoleId.ToString()].OptionsColumn.AllowEdit = true;
                rootGridViewRoles.Columns[eRoleColumns.Description.ToString()].OptionsColumn.AllowEdit = true;
                rootGridViewRoles.Columns[eRoleColumns.Enabled.ToString()].OptionsColumn.AllowEdit = true;
                rootGridViewRoles.Columns[eRoleColumns.Remarks.ToString()].OptionsColumn.AllowEdit = true;
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

        private bool IsValidRoles()
        {
            try
            {
                if (CurrentState == ActionsStates.Edit)
                    return IsValidModifiedRoles();
                else if (CurrentState == ActionsStates.New)
                    return IsValidCreatedRoles();

                return false;
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
                foreach (var rol in _createdRoles)
                {
                    if (string.IsNullOrEmpty(rol.Description) || string.IsNullOrEmpty(rol.RoleId))
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

        private bool UpdateRoles()
        {
            try
            {
                return GlobalSetting.RoleService.UpdateRoles(_modifiedRoles);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool CreateRole()
        {
            try
            {
                GlobalSetting.RoleService.NewRole(_createdRoles.FirstOrDefault());
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
