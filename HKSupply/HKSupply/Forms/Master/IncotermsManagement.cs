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
    public partial class IncotermsManagement : RibbonFormBase
    {
        #region Enums
        private enum eIncotermsColumns
        {
            IdIncoterm,
            Description,
            DescriptionZh
        }
        #endregion

        #region Private Methods
        List<Incoterm> _modifiedIncoterms = new List<Incoterm>();
        List<Incoterm> _createdIncoterms = new List<Incoterm>();
        #endregion

        #region Constructor
        public IncotermsManagement()
        {
            InitializeComponent();

            try
            {
                ConfigureRibbonActions();
                SetUpGrdIncoterms();
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
                LoadAllIncoterms();
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

                if (IsValidIncoterms() == false)
                    return;

                DialogResult result = XtraMessageBox.Show(GlobalSetting.ResManager.GetString("SaveChanges"), "", MessageBoxButtons.YesNo);

                if (result != DialogResult.Yes)
                    return;

                Cursor = Cursors.WaitCursor;

                if (CurrentState == ActionsStates.Edit)
                {
                    if (_modifiedIncoterms.Count() == 0)
                    {
                        MessageBox.Show(GlobalSetting.ResManager.GetString("NoPendingChanges"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        res = UpdateIncoterms();
                    }
                }
                else if (CurrentState == ActionsStates.New)
                {
                    res = CreateIncoterm();
                }

                if (res == true)
                {
                    MessageBox.Show(GlobalSetting.ResManager.GetString("SaveSuccessfully"));
                    LoadAllIncoterms();
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
        private void IncotermsManagement_Load(object sender, EventArgs e)
        {
            try
            {
                LoadAllIncoterms();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Grid Events
        void rootgridViewIncoterms_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;

                if (CurrentState == ActionsStates.Edit)
                {
                    Incoterm tmpIncoterm = new Incoterm();

                    object idIncoterm = view.GetRowCellValue(view.FocusedRowHandle, eIncotermsColumns.IdIncoterm.ToString());
                    object description = view.GetRowCellValue(view.FocusedRowHandle, eIncotermsColumns.Description.ToString());
                    object descriptionZh = view.GetRowCellValue(view.FocusedRowHandle, eIncotermsColumns.DescriptionZh.ToString());


                    tmpIncoterm.IdIncoterm = (idIncoterm ?? string.Empty).ToString();
                    tmpIncoterm.Description = (description ?? string.Empty).ToString();
                    tmpIncoterm.DescriptionZh = (descriptionZh ?? string.Empty).ToString();

                    AddModifiedIncotermsToList(tmpIncoterm);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void rootgridViewIncoterms_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                if (view.FocusedColumn.FieldName == eIncotermsColumns.Description.ToString() ||
                    view.FocusedColumn.FieldName == eIncotermsColumns.IdIncoterm.ToString())
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

        private void SetUpGrdIncoterms()
        {
            try
            {
                //hide group panel.
                rootgridViewIncoterms.OptionsView.ShowGroupPanel = false;
                rootgridViewIncoterms.OptionsCustomization.AllowGroup = false;
                rootgridViewIncoterms.OptionsCustomization.AllowColumnMoving = false;

                //Columns definition
                GridColumn colIdIncoterm = new GridColumn() { Caption = "Id Payment Terms", Visible = true, FieldName = eIncotermsColumns.IdIncoterm.ToString() };
                GridColumn colDescription = new GridColumn() { Caption = "Description", Visible = true, FieldName = eIncotermsColumns.Description.ToString() };
                GridColumn colDescriptionZh = new GridColumn() { Caption = "Description (Chinese)", Visible = true, FieldName = eIncotermsColumns.DescriptionZh.ToString() };

                //add columns to grid root view
                rootgridViewIncoterms.Columns.Add(colIdIncoterm);
                rootgridViewIncoterms.Columns.Add(colDescription);
                rootgridViewIncoterms.Columns.Add(colDescriptionZh);

                //Events
                rootgridViewIncoterms.ValidatingEditor += rootgridViewIncoterms_ValidatingEditor;
                rootgridViewIncoterms.CellValueChanged += rootgridViewIncoterms_CellValueChanged;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadAllIncoterms()
        {
            try
            {
                _modifiedIncoterms.Clear();
                _createdIncoterms.Clear();
                IEnumerable<Incoterm> incoterms = GlobalSetting.IncotermService.GetIIncoterms();

                xgrdIncoterms.DataSource = incoterms;

                rootgridViewIncoterms.Columns[eIncotermsColumns.IdIncoterm.ToString()].OptionsColumn.AllowEdit = false;
                rootgridViewIncoterms.Columns[eIncotermsColumns.Description.ToString()].OptionsColumn.AllowEdit = false;
                rootgridViewIncoterms.Columns[eIncotermsColumns.DescriptionZh.ToString()].OptionsColumn.AllowEdit = false;

                //TODO: gestion de estilos del grid
                rootgridViewIncoterms.Columns[eIncotermsColumns.IdIncoterm.ToString()].AppearanceCell.ForeColor = Color.Black;
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
                rootgridViewIncoterms.Columns[eIncotermsColumns.Description.ToString()].OptionsColumn.AllowEdit = true;
                rootgridViewIncoterms.Columns[eIncotermsColumns.DescriptionZh.ToString()].OptionsColumn.AllowEdit = true;

                //no edit column
                rootgridViewIncoterms.Columns[eIncotermsColumns.IdIncoterm.ToString()].OptionsColumn.AllowEdit = false;
                //TODO: gestion de estilos del grid
                rootgridViewIncoterms.Columns[eIncotermsColumns.IdIncoterm.ToString()].AppearanceCell.ForeColor = Color.Gray;

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
                _createdIncoterms.Add(new Incoterm());
                xgrdIncoterms.DataSource = null;
                xgrdIncoterms.DataSource = _createdIncoterms;
                //Allow edit all columns
                rootgridViewIncoterms.Columns[eIncotermsColumns.IdIncoterm.ToString()].OptionsColumn.AllowEdit = true;
                rootgridViewIncoterms.Columns[eIncotermsColumns.Description.ToString()].OptionsColumn.AllowEdit = true;
                rootgridViewIncoterms.Columns[eIncotermsColumns.DescriptionZh.ToString()].OptionsColumn.AllowEdit = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AddModifiedIncotermsToList(Incoterm modifiedIncoterm)
        {
            try
            {
                var incoterm = _modifiedIncoterms.FirstOrDefault(a => a.IdIncoterm.Equals(modifiedIncoterm.IdIncoterm));
                if (incoterm == null)
                {
                    _modifiedIncoterms.Add(modifiedIncoterm);
                }
                else
                {
                    incoterm.Description = modifiedIncoterm.Description;
                    incoterm.DescriptionZh = modifiedIncoterm.DescriptionZh;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsValidIncoterms()
        {
            try
            {
                if (CurrentState == ActionsStates.Edit)
                    return IsValidModifiedIncoterms();
                else if (CurrentState == ActionsStates.New)
                    return IsValidCreatedIncoterms();

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsValidModifiedIncoterms()
        {
            try
            {
                foreach (var incoterm in _modifiedIncoterms)
                {
                    if (string.IsNullOrEmpty(incoterm.Description))
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

        private bool IsValidCreatedIncoterms()
        {
            try
            {
                foreach (var incoterm in _createdIncoterms)
                {
                    if (string.IsNullOrEmpty(incoterm.Description) || string.IsNullOrEmpty(incoterm.IdIncoterm))
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

        private bool UpdateIncoterms()
        {
            try
            {
                return GlobalSetting.IncotermService.UpdateIncoterm(_modifiedIncoterms);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool CreateIncoterm()
        {
            try
            {
                GlobalSetting.IncotermService.NewIncoterm(_createdIncoterms.FirstOrDefault());
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
