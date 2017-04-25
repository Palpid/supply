using DevExpress.XtraEditors;
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
    public partial class FunctionalityManagement : RibbonFormBase
    {
        #region Enums
        private enum eFunctionalityColumns
        {
            FunctionalityId,
            FunctionalityName,
            Category,
            FormName
        }
        #endregion

        #region Private Members

        List<KeyValuePair<string, string>> _categoryList = new List<KeyValuePair<string, string>>() 
        { 
            new KeyValuePair<string, string>("Masters", "Masters"), 
            new KeyValuePair<string, string>("IT", "IT"),
            new KeyValuePair<string, string>("Others", "Others"),
            new KeyValuePair<string, string>("Help", "Help") 
        };

        List<Functionality> _modifiedFunctionalities = new List<Functionality>();
        List<Functionality> _createdFunctionalities = new List<Functionality>();

        #endregion

        #region Constructor
        public FunctionalityManagement()
        {
            InitializeComponent();

            ConfigureRibbonActions();
            SetUpGrdFunctionalities();
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
                LoadAllFunctionalities();
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

                if (IsValidFunctionalities() == false)
                    return;

                DialogResult result = MessageBox.Show(GlobalSetting.ResManager.GetString("SaveChanges"), "", MessageBoxButtons.YesNo);

                if (result != DialogResult.Yes)
                    return;

                if (CurrentState == ActionsStates.Edit)
                {
                    if (_modifiedFunctionalities.Count() == 0)
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
                    res = CreateRol();
                }

                if (res == true)
                {
                    XtraMessageBox.Show(GlobalSetting.ResManager.GetString("SaveSuccessfully"));
                    LoadAllFunctionalities();
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
        private void FunctionalityManagement_Load(object sender, EventArgs e)
        {
            try
            {
                LoadAllFunctionalities();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Grid Events

        void rootGridViewFunctionalities_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                int row = view.FocusedRowHandle;

                if (CurrentState == ActionsStates.Edit)
                {
                    object functionalityId = view.GetRowCellValue(row, eFunctionalityColumns.FunctionalityId.ToString());
                    object functionalityName = view.GetRowCellValue(row, eFunctionalityColumns.FunctionalityName.ToString());
                    object category = view.GetRowCellValue(row, eFunctionalityColumns.Category.ToString());
                    object formName = view.GetRowCellValue(row, eFunctionalityColumns.FormName.ToString());

                    Functionality tmpFunctionality = new Functionality();
                    tmpFunctionality.FunctionalityId = (int)functionalityId;
                    tmpFunctionality.FunctionalityName = (functionalityName ?? string.Empty).ToString();
                    tmpFunctionality.Category = (category ?? string.Empty).ToString();
                    tmpFunctionality.FormName = (formName ?? String.Empty).ToString();
                    AddModifiedFunctionalityToList(tmpFunctionality);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void rootGridViewFunctionalities_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;

                if (view.FocusedColumn.FieldName == eFunctionalityColumns.FunctionalityName.ToString() ||
                    view.FocusedColumn.FieldName == eFunctionalityColumns.Category.ToString() ||
                    view.FocusedColumn.FieldName == eFunctionalityColumns.FormName.ToString())
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
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion


        #endregion

        #region Private Functions
        private void SetUpGrdFunctionalities()
        {
            try
            {
                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                rootGridViewFunctionalities.OptionsView.ColumnAutoWidth = false;
                rootGridViewFunctionalities.HorzScrollVisibility = ScrollVisibility.Auto;

                //Columns definition
                GridColumn colFunctionalityId = new GridColumn() { Caption = "Functionality Id", Visible = true, FieldName = eFunctionalityColumns.FunctionalityId.ToString(), Width = 100 };
                GridColumn colFunctionalityName = new GridColumn() { Caption = "Functionality Name", Visible = true, FieldName = eFunctionalityColumns.FunctionalityName.ToString(), Width = 250 };
                GridColumn colCategory = new GridColumn() { Caption = "Category", Visible = true, FieldName = eFunctionalityColumns.Category.ToString(), Width = 100 };
                GridColumn colFormName = new GridColumn() { Caption = "Form Name", Visible = true, FieldName = eFunctionalityColumns.FormName.ToString(), Width = 250  };

                //Combobox repository for Category
                RepositoryItemLookUpEdit riComboCategory = new RepositoryItemLookUpEdit();
                riComboCategory.DataSource = _categoryList;
                riComboCategory.ValueMember = "Key";
                riComboCategory.DisplayMember = "Value";

                colCategory.ColumnEdit = riComboCategory;

                //add columns to grid root view
                rootGridViewFunctionalities.Columns.Add(colFunctionalityId);
                rootGridViewFunctionalities.Columns.Add(colFunctionalityName);
                rootGridViewFunctionalities.Columns.Add(colCategory);
                rootGridViewFunctionalities.Columns.Add(colFormName);

                //Events
                rootGridViewFunctionalities.ValidatingEditor += rootGridViewFunctionalities_ValidatingEditor;
                rootGridViewFunctionalities.CellValueChanged += rootGridViewFunctionalities_CellValueChanged;
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
                rootGridViewFunctionalities.Columns[eFunctionalityColumns.FunctionalityId.ToString()].VisibleIndex = 0;
                rootGridViewFunctionalities.Columns[eFunctionalityColumns.FunctionalityName.ToString()].VisibleIndex = 1;
                rootGridViewFunctionalities.Columns[eFunctionalityColumns.Category.ToString()].VisibleIndex = 2;
                rootGridViewFunctionalities.Columns[eFunctionalityColumns.FormName.ToString()].VisibleIndex = 3;
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
                        rootGridViewFunctionalities.ClearGrouping();
                        //hide group panel.
                        rootGridViewFunctionalities.OptionsView.ShowGroupPanel = false;
                        rootGridViewFunctionalities.OptionsCustomization.AllowGroup = false;
                        rootGridViewFunctionalities.OptionsCustomization.AllowColumnMoving = false;
                        //Change forecolor
                        rootGridViewFunctionalities.Columns[eFunctionalityColumns.FunctionalityId.ToString()].AppearanceCell.ForeColor = Color.Gray;
                        break;

                    case ActionsStates.New:
                        rootGridViewFunctionalities.ClearGrouping();
                        //hide group panel.
                        rootGridViewFunctionalities.OptionsView.ShowGroupPanel = false;
                        rootGridViewFunctionalities.OptionsCustomization.AllowGroup = false;
                        rootGridViewFunctionalities.OptionsCustomization.AllowColumnMoving = false;
                        //hide columns
                        rootGridViewFunctionalities.Columns[eFunctionalityColumns.FunctionalityId.ToString()].Visible = false;
                        break;

                    default:
                        //unhide group panel.
                        rootGridViewFunctionalities.OptionsView.ShowGroupPanel = true;
                        rootGridViewFunctionalities.OptionsCustomization.AllowGroup = true;
                        rootGridViewFunctionalities.OptionsCustomization.AllowColumnMoving = true;
                        //Change forecolor
                        rootGridViewFunctionalities.Columns[eFunctionalityColumns.FunctionalityId.ToString()].AppearanceCell.ForeColor = Color.Black;
                        break;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadAllFunctionalities()
        {
            try
            {
                _modifiedFunctionalities.Clear();
                _createdFunctionalities.Clear();
                IEnumerable<Functionality> functionalities = GlobalSetting.FunctionalityService.GetAllFunctionalities();
                xgrdFunctionalities.DataSource = functionalities;

                rootGridViewFunctionalities.Columns[eFunctionalityColumns.FunctionalityId.ToString()].OptionsColumn.AllowEdit = false;
                rootGridViewFunctionalities.Columns[eFunctionalityColumns.FunctionalityName.ToString()].OptionsColumn.AllowEdit = false;
                rootGridViewFunctionalities.Columns[eFunctionalityColumns.Category.ToString()].OptionsColumn.AllowEdit = false;
                rootGridViewFunctionalities.Columns[eFunctionalityColumns.FormName.ToString()].OptionsColumn.AllowEdit = false;

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
                //Allow edit some columns
                rootGridViewFunctionalities.Columns[eFunctionalityColumns.FunctionalityName.ToString()].OptionsColumn.AllowEdit = true;
                rootGridViewFunctionalities.Columns[eFunctionalityColumns.Category.ToString()].OptionsColumn.AllowEdit = true;
                rootGridViewFunctionalities.Columns[eFunctionalityColumns.FormName.ToString()].OptionsColumn.AllowEdit = true;
                //no edit column
                rootGridViewFunctionalities.Columns[eFunctionalityColumns.FunctionalityId.ToString()].OptionsColumn.AllowEdit = false;

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
                _createdFunctionalities.Add(new Functionality());
                xgrdFunctionalities.DataSource = null;
                xgrdFunctionalities.DataSource = _createdFunctionalities;

                //Allow edit some columns
                rootGridViewFunctionalities.Columns[eFunctionalityColumns.FunctionalityName.ToString()].OptionsColumn.AllowEdit = true;
                rootGridViewFunctionalities.Columns[eFunctionalityColumns.Category.ToString()].OptionsColumn.AllowEdit = true;
                rootGridViewFunctionalities.Columns[eFunctionalityColumns.FormName.ToString()].OptionsColumn.AllowEdit = true;
                //no edit column
                rootGridViewFunctionalities.Columns[eFunctionalityColumns.FunctionalityId.ToString()].OptionsColumn.AllowEdit = false;
                
                SetGridStylesByState();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AddModifiedFunctionalityToList(Functionality modifiedFunctionality)
        {
            try
            {
                var functionality = _modifiedFunctionalities.FirstOrDefault(f => f.FunctionalityId.Equals(modifiedFunctionality.FunctionalityId));
                if (functionality == null)
                {
                    _modifiedFunctionalities.Add(modifiedFunctionality);
                }
                else
                {
                    functionality.FunctionalityName = modifiedFunctionality.FunctionalityName;
                    functionality.Category = modifiedFunctionality.Category;
                    functionality.FormName = modifiedFunctionality.FormName;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AddCreatedFunctionalityToList(Functionality createdFunctionality)
        {
            try
            {
                var functionality = _createdFunctionalities.FirstOrDefault(f => f.FunctionalityId.Equals(createdFunctionality.FunctionalityId));
                if (functionality == null)
                {
                    _createdFunctionalities.Add(createdFunctionality);
                }
                else
                {
                    functionality.FunctionalityName = createdFunctionality.FunctionalityName;
                    functionality.Category = createdFunctionality.Category;
                    functionality.FormName = createdFunctionality.FormName;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsValidFunctionalities()
        {
            try
            {
                if (CurrentState == ActionsStates.Edit)
                    return IsValidModifiedFunctionalities();
                else if (CurrentState == ActionsStates.New)
                    return IsValidCreatedFunctionalities();

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsValidModifiedFunctionalities()
        {
            try
            {
                foreach (var func in _modifiedFunctionalities)
                {
                    if (string.IsNullOrEmpty(func.FunctionalityName) || string.IsNullOrEmpty(func.FormName) || string.IsNullOrEmpty(func.Category))
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

        private bool IsValidCreatedFunctionalities()
        {
            try
            {
                foreach (var func in _createdFunctionalities)
                {
                    if (string.IsNullOrEmpty(func.FunctionalityName) || string.IsNullOrEmpty(func.FormName))
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

        private bool UpdateRoles()
        {
            try
            {
                return GlobalSetting.FunctionalityService.UpdateFunctionalities(_modifiedFunctionalities);

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
                GlobalSetting.FunctionalityService.NewFunctionality(_createdFunctionalities.FirstOrDefault());
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
