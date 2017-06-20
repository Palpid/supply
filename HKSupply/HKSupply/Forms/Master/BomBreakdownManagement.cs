using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
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
using DevExpress.XtraBars;
using System.Text.RegularExpressions;
using DevExpress.XtraGrid.Views.Grid;

namespace HKSupply.Forms.Master
{
    public partial class BomBreakdownManagement : RibbonFormBase
    {

        #region Private Members
        List<BomBreakdown> _modifiedBomBreakdowns = new List<BomBreakdown>();
        List<BomBreakdown> _createdBomBreakdowns = new List<BomBreakdown>();
        #endregion

        #region Constructor
        public BomBreakdownManagement()
        {
            InitializeComponent();

            try
            {
                ConfigureRibbonActions();
                SetUpGrdBomBreakdown();
            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                //Print and export buttons
                EnablePrintPreview = false;
                EnableExportExcel = false;
                EnableExportCsv = false;
                ConfigurePrintExportOptions();
                //Layout
                EnableLayoutOptions = false;
                ConfigureLayoutOptions();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void bbiCancel_ItemClick(object sender, ItemClickEventArgs e)
        {
            base.bbiCancel_ItemClick(sender, e);

            try
            {
                LoadAllBomBreakdown();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public override void bbiEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            base.bbiEdit_ItemClick(sender, e);

            try
            {
                if (gridViewBomBreakdown.DataRowCount == 0)
                {
                    MessageBox.Show(GlobalSetting.ResManager.GetString("NoDataSelected"));
                    RestoreInitState();
                }
                else
                {
                    ConfigureRibbonActionsEditing();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public override void bbiNew_ItemClick(object sender, ItemClickEventArgs e)
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

        public override void bbiSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            base.bbiSave_ItemClick(sender, e);

            try
            {
                bool res = false;

                if (IsValidBreakdown() == false)
                    return;

                DialogResult result = MessageBox.Show(GlobalSetting.ResManager.GetString("SaveChanges"), "", MessageBoxButtons.YesNo);

                if (result != DialogResult.Yes)
                    return;

                if (CurrentState == ActionsStates.Edit)
                {
                    if (_modifiedBomBreakdowns.Count() == 0)
                    {
                        MessageBox.Show(GlobalSetting.ResManager.GetString("NoPendingChanges"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        res = UpdateBomBreakdowns();
                    }
                }
                else if (CurrentState == ActionsStates.New)
                {
                    res = CreateBomBrakdown();
                }

                if (res == true)
                {
                    MessageBox.Show(GlobalSetting.ResManager.GetString("SaveSuccessfully"));
                    LoadAllBomBreakdown();
                    RestoreInitState();
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public override void bbiExportCsv_ItemClick(object sender, ItemClickEventArgs e)
        {
            base.bbiExportCsv_ItemClick(sender, e);

            try
            {

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public override void bbiExportExcel_ItemClick(object sender, ItemClickEventArgs e)
        {
            base.bbiExportExcel_ItemClick(sender, e);

            try
            {

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Form Events
        private void BomBreakdownManagement_Load(object sender, EventArgs e)
        {
            try
            {
                LoadAllBomBreakdown();
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GridViewBomBreakdown_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;

                if (CurrentState == ActionsStates.Edit)
                {
                    BomBreakdown breakdown = view.GetRow(view.FocusedRowHandle) as BomBreakdown;
                    BomBreakdown tmpBreakdown = breakdown.Clone();
                    AddModifiedBreakdownToList(tmpBreakdown);
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Private Members

        #region Setup
        private void SetUpGrdBomBreakdown()
        {
            try
            {
                //hide group panel.
                gridViewBomBreakdown.OptionsView.ShowGroupPanel = false;
                gridViewBomBreakdown.OptionsCustomization.AllowGroup = false;
                gridViewBomBreakdown.OptionsCustomization.AllowColumnMoving = false;

                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                gridViewBomBreakdown.OptionsView.ColumnAutoWidth = false;
                gridViewBomBreakdown.HorzScrollVisibility = ScrollVisibility.Auto;

                //Hacer todo el grid no editable
                gridViewBomBreakdown.OptionsBehavior.Editable = false;

                //Columns definition
                GridColumn colIdBomBreakdown = new GridColumn() { Caption = "Id", Visible = true, FieldName = nameof(BomBreakdown.IdBomBreakdown), Width = 150 };
                GridColumn colDescription = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Description"), Visible = true, FieldName = nameof(BomBreakdown.Description), Width = 450 };

                //add columns to grid root view
                gridViewBomBreakdown.Columns.Add(colIdBomBreakdown);
                gridViewBomBreakdown.Columns.Add(colDescription);

                //Events
                //gridViewBomBreakdown.ValidatingEditor += rootGridViewStores_ValidatingEditor;
                gridViewBomBreakdown.CellValueChanged += GridViewBomBreakdown_CellValueChanged;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Loads
        private void LoadAllBomBreakdown()
        {
            try
            {
                _modifiedBomBreakdowns.Clear();
                _createdBomBreakdowns.Clear();

                List<BomBreakdown> bomBreakdowns = GlobalSetting.BomBreakdownService.GetBomBreakdowns();
                xgrdBomBreakdown.DataSource = bomBreakdowns;

                gridViewBomBreakdown.OptionsBehavior.Editable = false;
                gridViewBomBreakdown.Columns[nameof(BomBreakdown.IdBomBreakdown)].AppearanceCell.ForeColor = Color.Black;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Configure Ribbon Actions
        private void ConfigureRibbonActionsEditing()
        {
            try
            {
                gridViewBomBreakdown.OptionsBehavior.Editable = true;
                gridViewBomBreakdown.Columns[nameof(BomBreakdown.IdBomBreakdown)].OptionsColumn.AllowEdit = false;
                gridViewBomBreakdown.Columns[nameof(BomBreakdown.IdBomBreakdown)].AppearanceCell.ForeColor = Color.Gray;
            }
            catch
            {
                throw;
            }
        }

        private void ConfigureActionsStackViewCreating()
        {
            try
            {
                _createdBomBreakdowns.Add(new BomBreakdown());
                xgrdBomBreakdown.DataSource = null;
                xgrdBomBreakdown.DataSource = _createdBomBreakdowns;
                //Allow edit all columns
                gridViewBomBreakdown.OptionsBehavior.Editable = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Aux
        private void AddModifiedBreakdownToList(BomBreakdown modifiedBreakdown)
        {
            try
            {
                var breakdowns = _modifiedBomBreakdowns.FirstOrDefault(a => a.IdBomBreakdown.Equals(modifiedBreakdown.IdBomBreakdown));
                if (breakdowns == null)
                {
                    _modifiedBomBreakdowns.Add(modifiedBreakdown);
                }
                else
                {
                    breakdowns.Description = modifiedBreakdown.Description;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Validates

        private bool IsValidBreakdown()
        {
            try
            {
                if (CurrentState == ActionsStates.Edit)
                    return IsValidModifiedBreakdowns();
                else if (CurrentState == ActionsStates.New)
                    return IsValidCreatedBreakdowns();

                return false;
            }
            catch
            {
                throw;
            }
        }

        private bool IsValidModifiedBreakdowns()
        {
            try
            {
                foreach (var breakdown in _modifiedBomBreakdowns)
                {
                    if (string.IsNullOrEmpty(breakdown.Description))
                    {
                        MessageBox.Show(GlobalSetting.ResManager.GetString("FieldRequired"), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                return true;
            }
            catch
            {
                throw;
            }
        }

        private bool IsValidCreatedBreakdowns()
        {
            try
            {
                //Expresión regular, sólo letras (sin la ñ) y números
                Regex val = new Regex("^[A-Z0-9a-z]*$");

                foreach (var breakdowns in _createdBomBreakdowns)
                {
                    if (string.IsNullOrEmpty(breakdowns.Description) || string.IsNullOrEmpty(breakdowns.IdBomBreakdown))
                    {
                        MessageBox.Show(GlobalSetting.ResManager.GetString("FieldRequired"), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    if (val.IsMatch(breakdowns.IdBomBreakdown) == false)
                    {
                        MessageBox.Show(GlobalSetting.ResManager.GetString("InvalidId"), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                return true;
            }
            catch
            {
                throw;
            }
        }


        #endregion

        #region CRUD
        private bool UpdateBomBreakdowns()
        {
            try
            {
                return GlobalSetting.BomBreakdownService.UpdateBomBreakdown(_modifiedBomBreakdowns);
            }
            catch
            {
                throw;
            }
        }

        private bool CreateBomBrakdown()
        {
            try
            {
                GlobalSetting.BomBreakdownService.NewBomBreakdown(_createdBomBreakdowns.FirstOrDefault());
                return true;
            }
            catch
            {
                throw;
            }

        }
        #endregion

        #endregion

    }
}
