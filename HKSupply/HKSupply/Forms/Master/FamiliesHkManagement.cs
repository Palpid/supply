using DevExpress.XtraEditors;
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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HKSupply.Forms.Master
{
    public partial class FamiliesHkManagement : RibbonFormBase
    {
        #region Enums
        /*private enum eFamiliesHkColumns
        {
            IdFamilyHk,
            Description
        }*/
        #endregion

        #region Private Members
        List<FamilyHK> _modifiedFamiliesHk = new List<FamilyHK>();
        List<FamilyHK> _createdFamiliesHk = new List<FamilyHK>();
        #endregion

        #region Constructor
        public FamiliesHkManagement()
        {
            InitializeComponent();

            try
            {
                ConfigureRibbonActions();
                SetUpGrdFamiliesHk();
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
                LoadAllFamiliesHk();
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

                if (IsValidFamiliesHk() == false)
                    return;

                DialogResult result = XtraMessageBox.Show(GlobalSetting.ResManager.GetString("SaveChanges"), "", MessageBoxButtons.YesNo);

                if (result != DialogResult.Yes)
                    return;

                Cursor = Cursors.WaitCursor;

                if (CurrentState == ActionsStates.Edit)
                {
                    if (_modifiedFamiliesHk.Count() == 0)
                    {
                        MessageBox.Show(GlobalSetting.ResManager.GetString("NoPendingChanges"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        res = UpdateFamiliesHk();
                    }
                }
                else if (CurrentState == ActionsStates.New)
                {
                    res = CreateFamilyHk();
                }

                if (res == true)
                {
                    MessageBox.Show(GlobalSetting.ResManager.GetString("SaveSuccessfully"));
                    LoadAllFamiliesHk();
                    RestoreInitState();
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        #endregion

        #region Form Events
        private void FamiliesHkManagement_Load(object sender, EventArgs e)
        {
            try
            {
                LoadAllFamiliesHk();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Grid Events
        void rootgridViewFamiliesHk_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;

                if (CurrentState == ActionsStates.Edit)
                {
                    FamilyHK tmpFamilyHK = new FamilyHK();

                    object idFamilyHk = view.GetRowCellValue(view.FocusedRowHandle, nameof(FamilyHK.IdFamilyHk));
                    object description = view.GetRowCellValue(view.FocusedRowHandle, nameof(FamilyHK.Description));


                    tmpFamilyHK.IdFamilyHk = (idFamilyHk ?? string.Empty).ToString();
                    tmpFamilyHK.Description = (description ?? string.Empty).ToString();

                    AddModifiedFamilyHkToList(tmpFamilyHK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void rootgridViewFamiliesHk_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                if (view.FocusedColumn.FieldName == nameof(FamilyHK.Description) ||
                    view.FocusedColumn.FieldName == nameof(FamilyHK.IdFamilyHk))
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

        
        private void SetUpGrdFamiliesHk()
        {
            try
            {
                //hide group panel.
                rootgridViewFamiliesHk.OptionsView.ShowGroupPanel = false;
                rootgridViewFamiliesHk.OptionsCustomization.AllowGroup = false;
                rootgridViewFamiliesHk.OptionsCustomization.AllowColumnMoving = false;

                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                rootgridViewFamiliesHk.OptionsView.ColumnAutoWidth = false;
                rootgridViewFamiliesHk.HorzScrollVisibility = ScrollVisibility.Auto;

                //Columns definition
                GridColumn colIdIdFamilyHk = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("IdFamilyHk"), Visible = true, FieldName = nameof(FamilyHK.IdFamilyHk), Width = 90 };
                GridColumn colDescription = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Description"), Visible = true, FieldName = nameof(FamilyHK.Description), Width = 500 };

                //add columns to grid root view
                rootgridViewFamiliesHk.Columns.Add(colIdIdFamilyHk);
                rootgridViewFamiliesHk.Columns.Add(colDescription);

                //Events
                rootgridViewFamiliesHk.ValidatingEditor += rootgridViewFamiliesHk_ValidatingEditor;
                rootgridViewFamiliesHk.CellValueChanged += rootgridViewFamiliesHk_CellValueChanged;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadAllFamiliesHk()
        {
            try
            {
                _modifiedFamiliesHk.Clear();
                _createdFamiliesHk.Clear();
                IEnumerable<FamilyHK> familiesHk = GlobalSetting.FamilyHKService.GetFamiliesHK();

                xgrdFamiliesHk.DataSource = familiesHk;

                rootgridViewFamiliesHk.Columns[nameof(FamilyHK.IdFamilyHk)].OptionsColumn.AllowEdit = false;
                rootgridViewFamiliesHk.Columns[nameof(FamilyHK.Description)].OptionsColumn.AllowEdit = false;

                //TODO: gestion de estilos del grid
                rootgridViewFamiliesHk.Columns[nameof(FamilyHK.IdFamilyHk)].AppearanceCell.ForeColor = Color.Black;
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
                rootgridViewFamiliesHk.Columns[nameof(FamilyHK.Description)].OptionsColumn.AllowEdit = true;
                //no edit column
                rootgridViewFamiliesHk.Columns[nameof(FamilyHK.IdFamilyHk)].OptionsColumn.AllowEdit = false;
                //TODO: gestion de estilos del grid
                rootgridViewFamiliesHk.Columns[nameof(FamilyHK.IdFamilyHk)].AppearanceCell.ForeColor = Color.Gray;

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
                _createdFamiliesHk.Add(new FamilyHK());
                xgrdFamiliesHk.DataSource = null;
                xgrdFamiliesHk.DataSource = _createdFamiliesHk;
                //Allow edit all columns
                rootgridViewFamiliesHk.Columns[nameof(FamilyHK.IdFamilyHk)].OptionsColumn.AllowEdit = true;
                rootgridViewFamiliesHk.Columns[nameof(FamilyHK.Description)].OptionsColumn.AllowEdit = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AddModifiedFamilyHkToList(FamilyHK modifiedFamilyHK)
        {
            try
            {
                var familyHk = _modifiedFamiliesHk.FirstOrDefault(a => a.IdFamilyHk.Equals(modifiedFamilyHK.IdFamilyHk));
                if (familyHk == null)
                {
                    _modifiedFamiliesHk.Add(modifiedFamilyHK);
                }
                else
                {
                    familyHk.Description = modifiedFamilyHK.Description;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsValidFamiliesHk()
        {
            try
            {
                if (CurrentState == ActionsStates.Edit)
                    return IsValidModifiedFamiliesHk();
                else if (CurrentState == ActionsStates.New)
                    return IsValidCreatedFamiliesHk();

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsValidModifiedFamiliesHk()
        {
            try
            {
                foreach (var familyHk in _modifiedFamiliesHk)
                {
                    if (string.IsNullOrEmpty(familyHk.Description))
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

        private bool IsValidCreatedFamiliesHk()
        {
            try
            {
                //Expresión regular, sólo letras (sin la ñ) y números
                Regex val = new Regex("^[A-Z0-9a-z]*$");

                foreach (var familyHk in _createdFamiliesHk)
                {
                    if (string.IsNullOrEmpty(familyHk.Description) || string.IsNullOrEmpty(familyHk.IdFamilyHk))
                    {
                        MessageBox.Show(GlobalSetting.ResManager.GetString("FieldRequired"), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    if (val.IsMatch(familyHk.IdFamilyHk) == false)
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

        private bool UpdateFamiliesHk()
        {
            try
            {
                return GlobalSetting.FamilyHKService.UpdateFamilyHK(_modifiedFamiliesHk);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool CreateFamilyHk()
        {
            try
            {
                GlobalSetting.FamilyHKService.NewFamilyHK(_createdFamiliesHk.FirstOrDefault());
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
