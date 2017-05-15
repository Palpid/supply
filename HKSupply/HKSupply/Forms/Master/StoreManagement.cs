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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HKSupply.Forms.Master
{
    public partial class StoreManagement : RibbonFormBase
    {
        #region Enums
        private enum eStoreColumns
        {
            IdStore,
            Name,
            Active
        }
        #endregion

        #region Private Members
        List<Store> _modifiedStores = new List<Store>();
        List<Store> _createdStores = new List<Store>();
        #endregion

        #region Constructor
        public StoreManagement()
        {
            InitializeComponent();

            try
            {
                ConfigureRibbonActions();
                SetUpGrdStores();
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
                var actions = GlobalSetting.FunctionalitiesRoles.FirstOrDefault(fr => fr.Functionality.FormName.Equals(Name));
                SetRibbonText($"{actions.Functionality.Category} > {actions.Functionality.FunctionalityName}");
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
                LoadAllStores();
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

                if (IsValidStores() == false)
                    return;

                DialogResult result = MessageBox.Show(GlobalSetting.ResManager.GetString("SaveChanges"), "", MessageBoxButtons.YesNo);

                if (result != DialogResult.Yes)
                    return;

                if (CurrentState == ActionsStates.Edit)
                {
                    if (_modifiedStores.Count() == 0)
                    {
                        MessageBox.Show(GlobalSetting.ResManager.GetString("NoPendingChanges"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        res = UpdateStores();
                    }
                }
                else if (CurrentState == ActionsStates.New)
                {
                    res = CreateStore();
                }

                if (res == true)
                {
                    MessageBox.Show(GlobalSetting.ResManager.GetString("SaveSuccessfully"));
                    LoadAllStores();
                    RestoreInitState();
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Forms Events

        private void StoreManagement_Load(object sender, EventArgs e)
        {
            try
            {
                LoadAllStores();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Grid Events

        void rootGridViewStores_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;

                if (CurrentState == ActionsStates.Edit)
                {
                    object idStore = view.GetRowCellValue(view.FocusedRowHandle, eStoreColumns.IdStore.ToString());
                    object name = view.GetRowCellValue(view.FocusedRowHandle, eStoreColumns.Name.ToString());
                    object active = view.GetRowCellValue(view.FocusedRowHandle, eStoreColumns.Active.ToString());

                    Store tmpStore = new Store();
                    tmpStore.IdStore = (idStore ?? string.Empty).ToString();
                    tmpStore.Name = (name ?? string.Empty).ToString();
                    tmpStore.Active = (bool)active;
                    AddModifiedStoresToList(tmpStore);
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void rootGridViewStores_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;

                if (view.FocusedColumn.FieldName == eStoreColumns.Name.ToString())
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

        #region Private Methods

        private void SetUpGrdStores()
        {
            try
            {
                //hide group panel.
                rootGridViewStores.OptionsView.ShowGroupPanel = false;
                rootGridViewStores.OptionsCustomization.AllowGroup = false;
                rootGridViewStores.OptionsCustomization.AllowColumnMoving = false;

                //Columns definition
                GridColumn colIdStore = new GridColumn() { Caption = "Store Id", Visible = true, FieldName = eStoreColumns.IdStore.ToString() };
                GridColumn colName = new GridColumn() { Caption = "Name", Visible = true, FieldName = eStoreColumns.Name.ToString() };
                GridColumn colActive = new GridColumn() { Caption = "Active", Visible = true, FieldName = eStoreColumns.Active.ToString() };

                //add columns to grid root view
                rootGridViewStores.Columns.Add(colIdStore);
                rootGridViewStores.Columns.Add(colName);
                rootGridViewStores.Columns.Add(colActive);

                //Events
                rootGridViewStores.ValidatingEditor += rootGridViewStores_ValidatingEditor;
                rootGridViewStores.CellValueChanged += rootGridViewStores_CellValueChanged;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadAllStores()
        {
            try
            {
                _modifiedStores.Clear();
                _createdStores.Clear();
                IEnumerable<Store> stores = GlobalSetting.StoreService.GetAllStores();

                xgrdStores.DataSource = stores;

                rootGridViewStores.Columns[eStoreColumns.IdStore.ToString()].OptionsColumn.AllowEdit = false;
                rootGridViewStores.Columns[eStoreColumns.Name.ToString()].OptionsColumn.AllowEdit = false;
                rootGridViewStores.Columns[eStoreColumns.Active.ToString()].OptionsColumn.AllowEdit = false;

                //TODO: gestion de estilos del grid
                rootGridViewStores.Columns[eStoreColumns.IdStore.ToString()].AppearanceCell.ForeColor = Color.Black;
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
                rootGridViewStores.Columns[eStoreColumns.Name.ToString()].OptionsColumn.AllowEdit = true;
                rootGridViewStores.Columns[eStoreColumns.Active.ToString()].OptionsColumn.AllowEdit = true;
                //no edit column
                rootGridViewStores.Columns[eStoreColumns.IdStore.ToString()].OptionsColumn.AllowEdit = false;
                //TODO: gestion de estilos del grid
                rootGridViewStores.Columns[eStoreColumns.IdStore.ToString()].AppearanceCell.ForeColor = Color.Gray;
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
                _createdStores.Add(new Store());
                xgrdStores.DataSource = null;
                xgrdStores.DataSource = _createdStores;
                //Allow edit all columns
                rootGridViewStores.Columns[eStoreColumns.IdStore.ToString()].OptionsColumn.AllowEdit = true;
                rootGridViewStores.Columns[eStoreColumns.Name.ToString()].OptionsColumn.AllowEdit = true;
                rootGridViewStores.Columns[eStoreColumns.Active.ToString()].OptionsColumn.AllowEdit = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AddModifiedStoresToList(Store modifiedStore)
        {
            try
            {
                var store = _modifiedStores.FirstOrDefault(s => s.IdStore.Equals(modifiedStore.IdStore));
                if (store == null)
                {
                    _modifiedStores.Add(modifiedStore);
                }
                else
                {
                    store.Name = modifiedStore.Name;
                    store.Active = modifiedStore.Active;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsValidStores()
        {
            try
            {
                if (CurrentState == ActionsStates.Edit)
                    return IsValidModifiedStores();
                else if (CurrentState == ActionsStates.New)
                    return IsValidCreatedStores();

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsValidModifiedStores()
        {
            try
            {
                foreach (var store in _modifiedStores)
                {
                    if (string.IsNullOrEmpty(store.Name))
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

        private bool IsValidCreatedStores()
        {
            try
            {
                //Expresión regular, sólo letras (sin la ñ) y números
                Regex val = new Regex("^[A-Z0-9a-z]*$");

                foreach (var store in _createdStores)
                {
                    if (string.IsNullOrEmpty(store.Name) || string.IsNullOrEmpty(store.IdStore))
                    {
                        MessageBox.Show(GlobalSetting.ResManager.GetString("FieldRequired"), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    if (val.IsMatch(store.IdStore) == false)
                    {
                        MessageBox.Show(GlobalSetting.ResManager.GetString("InvalidId"), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private bool UpdateStores()
        {
            try
            {
                return GlobalSetting.StoreService.UpdateStore(_modifiedStores);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool CreateStore()
        {
            try
            {
                GlobalSetting.StoreService.NewStore(_createdStores.FirstOrDefault());
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
