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
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;
using HKSupply.General;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using HKSupply.Helpers;
using DevExpress.XtraEditors.Repository;

namespace HKSupply.Forms.Master
{
    public partial class SupplierFactoryCoeffManagement : RibbonFormBase
    {

        #region Private Members
        List<Supplier> _supplierList;

        List<SupplierFactoryCoeff> _modifiedSupplierFactoryCoeffs = new List<SupplierFactoryCoeff>();
        List<SupplierFactoryCoeff> _createdSupplierFactoryCoeffs = new List<SupplierFactoryCoeff>();
        #endregion

        #region Constructor
        public SupplierFactoryCoeffManagement()
        {
            InitializeComponent();

            try
            {
                ConfigureRibbonActions();
                LoadAuxList();
                SetUpGrdSupplierFactoryCoeff();
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
                LoadAllSupplierFactoryCoeff();
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
                if (gridViewSupplierFactoryCoeff.DataRowCount == 0)
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

                if (IsValidSupplierFactoryCoeffs() == false)
                    return;

                DialogResult result = MessageBox.Show(GlobalSetting.ResManager.GetString("SaveChanges"), "", MessageBoxButtons.YesNo);

                if (result != DialogResult.Yes)
                    return;

                if (CurrentState == ActionsStates.Edit)
                {
                    if (_modifiedSupplierFactoryCoeffs.Count() == 0)
                    {
                        MessageBox.Show(GlobalSetting.ResManager.GetString("NoPendingChanges"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        res = UpdateSupplierFactoryCoeffs();
                    }
                }
                else if (CurrentState == ActionsStates.New)
                {
                    res = CreateSupplierFactoryCoeff();
                }

                if (res == true)
                {
                    MessageBox.Show(GlobalSetting.ResManager.GetString("SaveSuccessfully"));
                    LoadAllSupplierFactoryCoeff();
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
        private void SupplierFactoryCoeffManagement_Load(object sender, EventArgs e)
        {
            try
            {
                LoadAllSupplierFactoryCoeff();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GridViewSupplierFactoryCoeff_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;

                if (CurrentState == ActionsStates.Edit)
                {
                    SupplierFactoryCoeff breakdown = view.GetRow(view.FocusedRowHandle) as SupplierFactoryCoeff;
                    SupplierFactoryCoeff tmpSupplierFactoryCoeff = breakdown.Clone();
                    AddModifiedSupplierFactoryCoeffToList(tmpSupplierFactoryCoeff);
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Private Methods

        #region Setup
        private void SetUpGrdSupplierFactoryCoeff()
        {
            try
            {
                //hide group panel.
                //gridViewSupplierFactoryCoeff.OptionsView.ShowGroupPanel = false;
                //gridViewSupplierFactoryCoeff.OptionsCustomization.AllowGroup = false;
                //gridViewSupplierFactoryCoeff.OptionsCustomization.AllowColumnMoving = false;

                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                gridViewSupplierFactoryCoeff.OptionsView.ColumnAutoWidth = false;
                gridViewSupplierFactoryCoeff.HorzScrollVisibility = ScrollVisibility.Auto;

                //Hacer todo el grid no editable
                gridViewSupplierFactoryCoeff.OptionsBehavior.Editable = false;

                //Columns definition
                GridColumn colIdSupplier = new GridColumn() { Caption = "Supplier", Visible = true, FieldName = nameof(SupplierFactoryCoeff.IdSupplier), Width = 150 };
                GridColumn colIdFactory = new GridColumn() { Caption = "Factory", Visible = true, FieldName = nameof(SupplierFactoryCoeff.IdFactory), Width = 150 };
                GridColumn colCoefficient1 = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Coefficient1"), Visible = true, FieldName = nameof(SupplierFactoryCoeff.Coefficient1), Width = 150 };
                GridColumn colCoefficient2 = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Coefficient2"), Visible = true, FieldName = nameof(SupplierFactoryCoeff.Coefficient2), Width = 150 };
                GridColumn colScrap = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Scrap"), Visible = true, FieldName = nameof(SupplierFactoryCoeff.Scrap), Width = 150 };

                //Display Format
                colCoefficient1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                colCoefficient2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                colScrap.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;

                colCoefficient1.DisplayFormat.FormatString = "n6";
                colCoefficient2.DisplayFormat.FormatString = "n6";
                colScrap.DisplayFormat.FormatString = "n6";

                //Edit repositories
                RepositoryItemSearchLookUpEdit riSupplierFactory = new RepositoryItemSearchLookUpEdit()
                {
                    DataSource = _supplierList,
                    ValueMember = nameof(Supplier.IdSupplier),
                    DisplayMember = nameof(Supplier.IdSupplier),
                    ShowClearButton = false
                };
                riSupplierFactory.View.Columns.AddField(nameof(Supplier.IdSupplier)).Visible = true;
                riSupplierFactory.View.Columns.AddField(nameof(Supplier.SupplierName)).Visible = true;

                colIdSupplier.ColumnEdit = riSupplierFactory;
                colIdFactory.ColumnEdit = riSupplierFactory;

                RepositoryItemTextEdit ritxt2Dec = new RepositoryItemTextEdit();
                ritxt2Dec.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                ritxt2Dec.Mask.EditMask = "F6";

                colCoefficient1.ColumnEdit = ritxt2Dec;
                colCoefficient2.ColumnEdit = ritxt2Dec;
                colScrap.ColumnEdit = ritxt2Dec;

                //add columns to grid root view
                gridViewSupplierFactoryCoeff.Columns.Add(colIdSupplier);
                gridViewSupplierFactoryCoeff.Columns.Add(colIdFactory);
                gridViewSupplierFactoryCoeff.Columns.Add(colCoefficient1);
                gridViewSupplierFactoryCoeff.Columns.Add(colCoefficient2);
                gridViewSupplierFactoryCoeff.Columns.Add(colScrap);

                //Events
                gridViewSupplierFactoryCoeff.CellValueChanged += GridViewSupplierFactoryCoeff_CellValueChanged;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Loads

        private void LoadAuxList()
        {
            try
            {
                _supplierList = GlobalSetting.SupplierService.GetSuppliers();
            }
            catch
            {
                throw;
            }
        }

        private void LoadAllSupplierFactoryCoeff()
        {
            try
            {
                _modifiedSupplierFactoryCoeffs.Clear();
                _createdSupplierFactoryCoeffs.Clear();

                List<SupplierFactoryCoeff> supplierFactoryCoeffs = GlobalSetting.SupplierFactoryCoeffService.GetAllSupplierFactoryCoeff();
                xgrdSupplierFactoryCoeff.DataSource = supplierFactoryCoeffs;

                gridViewSupplierFactoryCoeff.OptionsBehavior.Editable = false;
                gridViewSupplierFactoryCoeff.Columns[nameof(SupplierFactoryCoeff.IdFactory)].AppearanceCell.ForeColor = Color.Black;
                gridViewSupplierFactoryCoeff.Columns[nameof(SupplierFactoryCoeff.IdSupplier)].AppearanceCell.ForeColor = Color.Black;
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
                gridViewSupplierFactoryCoeff.OptionsBehavior.Editable = true;
                gridViewSupplierFactoryCoeff.Columns[nameof(SupplierFactoryCoeff.IdFactory)].OptionsColumn.AllowEdit = false;
                gridViewSupplierFactoryCoeff.Columns[nameof(SupplierFactoryCoeff.IdSupplier)].OptionsColumn.AllowEdit = false;

                gridViewSupplierFactoryCoeff.Columns[nameof(SupplierFactoryCoeff.IdFactory)].AppearanceCell.ForeColor = Color.Gray;
                gridViewSupplierFactoryCoeff.Columns[nameof(SupplierFactoryCoeff.IdSupplier)].AppearanceCell.ForeColor = Color.Gray;
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
                _createdSupplierFactoryCoeffs.Add(new SupplierFactoryCoeff(Constants.ITEM_GROUP_MT));
                xgrdSupplierFactoryCoeff.DataSource = null;
                xgrdSupplierFactoryCoeff.DataSource = _createdSupplierFactoryCoeffs;
                //Allow edit all columns
                gridViewSupplierFactoryCoeff.OptionsBehavior.Editable = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Aux
        private void AddModifiedSupplierFactoryCoeffToList(SupplierFactoryCoeff modifiedSupplierFactoryCoeff)
        {
            try
            {
                var supplierFactoryCoeff = _modifiedSupplierFactoryCoeffs
                    .FirstOrDefault(a => a.IdFactory.Equals(modifiedSupplierFactoryCoeff.IdFactory) && a.IdSupplier.Equals(modifiedSupplierFactoryCoeff.IdSupplier));

                if (supplierFactoryCoeff == null)
                {
                    _modifiedSupplierFactoryCoeffs.Add(modifiedSupplierFactoryCoeff);
                }
                else
                {
                    supplierFactoryCoeff.Coefficient1 = modifiedSupplierFactoryCoeff.Coefficient1;
                    supplierFactoryCoeff.Coefficient2 = modifiedSupplierFactoryCoeff.Coefficient2;
                    supplierFactoryCoeff.Scrap  = modifiedSupplierFactoryCoeff.Scrap;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Validates

        private bool IsValidSupplierFactoryCoeffs()
        {
            try
            {
                if (CurrentState == ActionsStates.Edit)
                    return IsValidModifiedSupplierFactoryCoeffs();
                else if (CurrentState == ActionsStates.New)
                    return IsValidCreatedSupplierFactoryCoeff();

                return false;
            }
            catch
            {
                throw;
            }
        }

        private bool IsValidModifiedSupplierFactoryCoeffs()
        {
            try
            {
                foreach (var supplierFactoryCoeff in _modifiedSupplierFactoryCoeffs)
                {

                    if (supplierFactoryCoeff.Coefficient1 == 0)
                    {
                        MessageBox.Show($"Coefficient 1 must be greater than Zero ({supplierFactoryCoeff.IdSupplier}/{supplierFactoryCoeff.IdFactory})", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    if (supplierFactoryCoeff.Coefficient2 == 0)
                    {
                        MessageBox.Show($"Coefficient 2 must be greater than Zero ({supplierFactoryCoeff.IdSupplier}/{supplierFactoryCoeff.IdFactory})", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    if (supplierFactoryCoeff.Scrap == 0)
                    {
                        MessageBox.Show($"Scrap must be greater than Zero ({supplierFactoryCoeff.IdSupplier}/{supplierFactoryCoeff.IdFactory})", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private bool IsValidCreatedSupplierFactoryCoeff()
        {
            try
            {

                foreach (var supplierFactoryCoeff in _modifiedSupplierFactoryCoeffs)
                {

                    if(string.IsNullOrEmpty(supplierFactoryCoeff.IdFactory))
                    {
                        MessageBox.Show(GlobalSetting.ResManager.GetString("FieldRequired"), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    if (string.IsNullOrEmpty(supplierFactoryCoeff.IdSupplier))
                    {
                        MessageBox.Show(GlobalSetting.ResManager.GetString("FieldRequired"), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    if (supplierFactoryCoeff.Coefficient1 == 0)
                    {
                        MessageBox.Show($"Coefficient 1 must be greater than Zero ({supplierFactoryCoeff.IdSupplier}/{supplierFactoryCoeff.IdFactory})", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    if (supplierFactoryCoeff.Coefficient2 == 0)
                    {
                        MessageBox.Show($"Coefficient 2 must be greater than Zero ({supplierFactoryCoeff.IdSupplier}/{supplierFactoryCoeff.IdFactory})", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    if (supplierFactoryCoeff.Scrap == 0)
                    {
                        MessageBox.Show($"Scrap must be greater than Zero ({supplierFactoryCoeff.IdSupplier}/{supplierFactoryCoeff.IdFactory})", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private bool UpdateSupplierFactoryCoeffs()
        {
            try
            {
                return GlobalSetting.SupplierFactoryCoeffService.UpdateSupplierFactoryCoeff(_modifiedSupplierFactoryCoeffs);
            }
            catch
            {
                throw;
            }
        }

        private bool CreateSupplierFactoryCoeff()
        {
            try
            {
                GlobalSetting.SupplierFactoryCoeffService.NewSupplierFactoryCoeff(_createdSupplierFactoryCoeffs.FirstOrDefault());
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
