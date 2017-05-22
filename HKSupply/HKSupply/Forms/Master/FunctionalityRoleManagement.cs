using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
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
    public partial class FunctionalityRoleManagement : RibbonFormBase
    {

        #region Enums
        private enum eFunctionalityRoleColumns
        {
            FunctionalityId,
            RoleId,
            Read,
            New,
            Modify,
        }
        #endregion

        #region Private Members
        List<Role> _roleList;
        List<Functionality> _functionalityList;

        List<FunctionalityRole> _modifiedFunctionalityRole = new List<FunctionalityRole>();
        List<FunctionalityRole> _createdFunctionalityRole = new List<FunctionalityRole>();
        #endregion

        #region Constructor
        public FunctionalityRoleManagement()
        {
            InitializeComponent();

            try
            {
                ConfigureRibbonActions();
                LoadRoles();
                LoadFunctionalities();
                SetUpGrdFunctionalitiesRoles();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                LoadAllFunctionalitiesRoles();
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

                if (IsValidFunctionalitiesRoles() == false)
                    return;

                DialogResult result = MessageBox.Show(GlobalSetting.ResManager.GetString("SaveChanges"), "", MessageBoxButtons.YesNo);

                if (result != DialogResult.Yes)
                    return;

                if (CurrentState == ActionsStates.Edit)
                {
                    if (_modifiedFunctionalityRole.Count() == 0)
                    {
                        MessageBox.Show(GlobalSetting.ResManager.GetString("NoPendingChanges"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        res = UpdateFunctionalitiesRoles();
                    }
                }
                else if (CurrentState == ActionsStates.New)
                {
                    res = CreateFunctionalityRoles();
                }

                if (res == true)
                {
                    MessageBox.Show(GlobalSetting.ResManager.GetString("SaveSuccessfully"));
                    LoadAllFunctionalitiesRoles();
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

        #region Form events
        private void FunctionalityRoleManagement_Load(object sender, EventArgs e)
        {
            try
            {
                LoadAllFunctionalitiesRoles();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Grid Events

        void rootGridViewFuncRoles_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                int row = view.FocusedRowHandle;

                if (CurrentState == ActionsStates.Edit)
                {
                    object functionalityId = view.GetRowCellValue(row, eFunctionalityRoleColumns.FunctionalityId.ToString());
                    object roleId = view.GetRowCellValue(row, eFunctionalityRoleColumns.RoleId.ToString());
                    object read = view.GetRowCellValue(row, eFunctionalityRoleColumns.Read.ToString());
                    object _new = view.GetRowCellValue(row, eFunctionalityRoleColumns.New.ToString());
                    object modify = view.GetRowCellValue(row, eFunctionalityRoleColumns.Modify.ToString());

                    FunctionalityRole tmpFunctionalityRole = new FunctionalityRole();
                    tmpFunctionalityRole.FunctionalityId = (int)functionalityId;
                    tmpFunctionalityRole.RoleId = (roleId ?? string.Empty).ToString();
                    tmpFunctionalityRole.Read = (bool)read;
                    tmpFunctionalityRole.New = (bool)_new;
                    tmpFunctionalityRole.Modify = (bool)modify;
                    AddModifiedFuncRoleToList(tmpFunctionalityRole);
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void rootGridViewFuncRoles_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            try
            {
                bool validatingError = false;
                GridView view = sender as GridView;

                if (view.FocusedColumn.FieldName == eFunctionalityRoleColumns.FunctionalityId.ToString() && (int)e.Value < 1)
                    validatingError = true;
                else if (view.FocusedColumn.FieldName == eFunctionalityRoleColumns.RoleId.ToString() && string.IsNullOrEmpty(e.Value as string))
                    validatingError = true;

                if (validatingError)
                {
                    e.Valid = false;
                    e.ErrorText = GlobalSetting.ResManager.GetString("FieldRequired");
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #endregion

        #region Private Functions

        private void SetUpGrdFunctionalitiesRoles()
        {
            try
            {
                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                rootGridViewFuncRoles.OptionsView.ColumnAutoWidth = false;
                rootGridViewFuncRoles.HorzScrollVisibility = ScrollVisibility.Auto;

                //Columns definition
                GridColumn colFunctionalityId = new GridColumn() { Caption = "Functionality", Visible = true, FieldName = eFunctionalityRoleColumns.FunctionalityId.ToString(), Width = 250 };
                GridColumn colRoleId = new GridColumn() { Caption = "Role", Visible = true, FieldName = eFunctionalityRoleColumns.RoleId.ToString(), Width = 250 };
                GridColumn colRead = new GridColumn() { Caption = "Read", Visible = true, FieldName = eFunctionalityRoleColumns.Read.ToString(), Width = 50 };
                GridColumn colNew = new GridColumn() { Caption = "New", Visible = true, FieldName = eFunctionalityRoleColumns.New.ToString(), Width = 50 };
                GridColumn colModify = new GridColumn() { Caption = "Modify", Visible = true, FieldName = eFunctionalityRoleColumns.Modify.ToString(), Width = 50 };

                //Combobox repository for Functionality
                RepositoryItemLookUpEdit riComboFunctionality = new RepositoryItemLookUpEdit();
                riComboFunctionality.DataSource = _functionalityList;
                riComboFunctionality.ValueMember = "FunctionalityId";
                riComboFunctionality.DisplayMember = "FunctionalityName";
                riComboFunctionality.Columns.Add(new LookUpColumnInfo("FunctionalityName", "Name"));
                colFunctionalityId.ColumnEdit = riComboFunctionality;

                //Combobox repository for Role
                RepositoryItemLookUpEdit riComboRole = new RepositoryItemLookUpEdit();
                riComboRole.DataSource = _roleList;
                riComboRole.ValueMember = "RoleId";
                riComboRole.DisplayMember = "Description";
                riComboRole.Columns.Add(new LookUpColumnInfo("Description","Description"));

                colRoleId.ColumnEdit = riComboRole;

                //add columns to grid root view
                rootGridViewFuncRoles.Columns.Add(colFunctionalityId);
                rootGridViewFuncRoles.Columns.Add(colRoleId);
                rootGridViewFuncRoles.Columns.Add(colRead);
                rootGridViewFuncRoles.Columns.Add(colNew);
                rootGridViewFuncRoles.Columns.Add(colModify);

                ////Events
                rootGridViewFuncRoles.ValidatingEditor += rootGridViewFuncRoles_ValidatingEditor;
                rootGridViewFuncRoles.CellValueChanged += rootGridViewFuncRoles_CellValueChanged;
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
                _roleList = GlobalSetting.RoleService.GetRoles(false).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadFunctionalities()
        {
            try
            {
                _functionalityList = GlobalSetting.FunctionalityService.GetAllFunctionalities().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadAllFunctionalitiesRoles()
        {
            try
            {
                _modifiedFunctionalityRole.Clear();
                _createdFunctionalityRole.Clear();
                IEnumerable<FunctionalityRole> funcRoles = GlobalSetting.FunctionalityRoleService.GetAllFunctionalitiesRole();
                xgrdFuncRoles.DataSource = funcRoles;

                rootGridViewFuncRoles.Columns[eFunctionalityRoleColumns.FunctionalityId.ToString()].OptionsColumn.AllowEdit = false;
                rootGridViewFuncRoles.Columns[eFunctionalityRoleColumns.RoleId.ToString()].OptionsColumn.AllowEdit = false;
                rootGridViewFuncRoles.Columns[eFunctionalityRoleColumns.Read.ToString()].OptionsColumn.AllowEdit = false;
                rootGridViewFuncRoles.Columns[eFunctionalityRoleColumns.New.ToString()].OptionsColumn.AllowEdit = false;
                rootGridViewFuncRoles.Columns[eFunctionalityRoleColumns.Modify.ToString()].OptionsColumn.AllowEdit = false;

                SetGridStylesByState();
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
                rootGridViewFuncRoles.Columns[eFunctionalityRoleColumns.FunctionalityId.ToString()].VisibleIndex = 0;
                rootGridViewFuncRoles.Columns[eFunctionalityRoleColumns.RoleId.ToString()].VisibleIndex = 1;
                rootGridViewFuncRoles.Columns[eFunctionalityRoleColumns.Read.ToString()].VisibleIndex = 2;
                rootGridViewFuncRoles.Columns[eFunctionalityRoleColumns.New.ToString()].VisibleIndex = 3;
                rootGridViewFuncRoles.Columns[eFunctionalityRoleColumns.Modify.ToString()].VisibleIndex = 4;
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
                        rootGridViewFuncRoles.ClearGrouping();
                        //hide group panel.
                        rootGridViewFuncRoles.OptionsView.ShowGroupPanel = false;
                        rootGridViewFuncRoles.OptionsCustomization.AllowGroup = false;
                        rootGridViewFuncRoles.OptionsCustomization.AllowColumnMoving = false;
                        //Change forecolor
                        rootGridViewFuncRoles.Columns[eFunctionalityRoleColumns.FunctionalityId.ToString()].AppearanceCell.ForeColor = Color.Gray;
                        rootGridViewFuncRoles.Columns[eFunctionalityRoleColumns.RoleId.ToString()].AppearanceCell.ForeColor = Color.Gray;
                        break;

                    case ActionsStates.New:
                        rootGridViewFuncRoles.ClearGrouping();
                        //hide group panel.
                        rootGridViewFuncRoles.OptionsView.ShowGroupPanel = false;
                        rootGridViewFuncRoles.OptionsCustomization.AllowGroup = false;
                        rootGridViewFuncRoles.OptionsCustomization.AllowColumnMoving = false;
                        break;

                    default:
                        //unhide group panel.
                        rootGridViewFuncRoles.OptionsView.ShowGroupPanel = true;
                        rootGridViewFuncRoles.OptionsCustomization.AllowGroup = true;
                        rootGridViewFuncRoles.OptionsCustomization.AllowColumnMoving = true;
                        //Change forecolor
                        rootGridViewFuncRoles.Columns[eFunctionalityRoleColumns.FunctionalityId.ToString()].AppearanceCell.ForeColor = Color.Black;
                        rootGridViewFuncRoles.Columns[eFunctionalityRoleColumns.RoleId.ToString()].AppearanceCell.ForeColor = Color.Black;
                        break;
                }

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
                //Allow edit some columns
                rootGridViewFuncRoles.Columns[eFunctionalityRoleColumns.Read.ToString()].OptionsColumn.AllowEdit = true;
                rootGridViewFuncRoles.Columns[eFunctionalityRoleColumns.New.ToString()].OptionsColumn.AllowEdit = true;
                rootGridViewFuncRoles.Columns[eFunctionalityRoleColumns.Modify.ToString()].OptionsColumn.AllowEdit = true;
                //no edit column
                rootGridViewFuncRoles.Columns[eFunctionalityRoleColumns.FunctionalityId.ToString()].OptionsColumn.AllowEdit = false;
                rootGridViewFuncRoles.Columns[eFunctionalityRoleColumns.RoleId.ToString()].OptionsColumn.AllowEdit = false;

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
                _createdFunctionalityRole.Add(new FunctionalityRole());
                xgrdFuncRoles.DataSource = null;
                xgrdFuncRoles.DataSource = _createdFunctionalityRole;

                //Allow edit some columns
                rootGridViewFuncRoles.Columns[eFunctionalityRoleColumns.Read.ToString()].OptionsColumn.AllowEdit = true;
                rootGridViewFuncRoles.Columns[eFunctionalityRoleColumns.New.ToString()].OptionsColumn.AllowEdit = true;
                rootGridViewFuncRoles.Columns[eFunctionalityRoleColumns.Modify.ToString()].OptionsColumn.AllowEdit = true;
                rootGridViewFuncRoles.Columns[eFunctionalityRoleColumns.FunctionalityId.ToString()].OptionsColumn.AllowEdit = true;
                rootGridViewFuncRoles.Columns[eFunctionalityRoleColumns.RoleId.ToString()].OptionsColumn.AllowEdit = true;

                SetGridStylesByState();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AddModifiedFuncRoleToList(FunctionalityRole modifiedFuncRole)
        {
            try
            {
                var funcRole = _modifiedFunctionalityRole.Where(fr => fr.FunctionalityId.Equals(modifiedFuncRole.FunctionalityId) &&
                    fr.RoleId.Equals(modifiedFuncRole.RoleId)).FirstOrDefault();

                if (funcRole == null)
                {
                    _modifiedFunctionalityRole.Add(modifiedFuncRole);
                }
                else
                {
                    funcRole.Read = modifiedFuncRole.Read;
                    funcRole.New = modifiedFuncRole.New;
                    funcRole.Modify = modifiedFuncRole.Modify;

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //private void AddCreatedFunctionalityRoleToList(FunctionalityRole createdFunctionalityRole)
        //{
        //    try
        //    {
        //        var funcRole = _createdFunctionalityRole.FirstOrDefault(fr => fr.FunctionalityId.Equals(createdFunctionalityRole.FunctionalityId) &&
        //            fr.RoleId.Equals(createdFunctionalityRole.RoleId));
        //        if (funcRole == null)
        //        {
        //            _createdFunctionalityRole.Add(createdFunctionalityRole);
        //        }
        //        else
        //        {
        //            funcRole.FunctionalityId = createdFunctionalityRole.FunctionalityId;
        //            funcRole.RoleId = createdFunctionalityRole.RoleId;
        //            funcRole.Read = createdFunctionalityRole.Read;
        //            funcRole.New = createdFunctionalityRole.New;
        //            funcRole.Modify = createdFunctionalityRole.Modify;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        private bool IsValidFunctionalitiesRoles()
        {
            try
            {
                if (CurrentState == ActionsStates.Edit)
                    return IsValidModifiedFunctionalitiesRoles();
                else if (CurrentState == ActionsStates.New)
                    return IsValidCreatedFunctionalitiesRoles();

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsValidModifiedFunctionalitiesRoles()
        {
            try
            {
                foreach (var funcRole in _modifiedFunctionalityRole)
                {
                    if (funcRole.FunctionalityId==0 || string.IsNullOrEmpty(funcRole.RoleId))
                    {
                        XtraMessageBox.Show(GlobalSetting.ResManager.GetString("FieldRequired"), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private bool IsValidCreatedFunctionalitiesRoles()
        {
            try
            {
                foreach (var funcRole in _createdFunctionalityRole)
                {
                    if (funcRole.FunctionalityId == 0 || string.IsNullOrEmpty(funcRole.RoleId))
                    {
                        XtraMessageBox.Show(GlobalSetting.ResManager.GetString("FieldRequired"), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private bool UpdateFunctionalitiesRoles()
        {
            try
            {
                return GlobalSetting.FunctionalityRoleService.UpdateFunctionalitiesRoles(_modifiedFunctionalityRole);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool CreateFunctionalityRoles()
        {
            try
            {
                GlobalSetting.FunctionalityRoleService.NewFunctionalityRole(_createdFunctionalityRole.FirstOrDefault());
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
