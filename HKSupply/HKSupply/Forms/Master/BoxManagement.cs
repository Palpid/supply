using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using HKSupply.General;
using HKSupply.Models.Supply;

namespace HKSupply.Forms.Master
{
    public partial class BoxManagement : RibbonFormBase
    {
        #region Private members
        BindingList<Box> _boxList = new BindingList<Box>();
        #endregion

        #region Constructor
        public BoxManagement()
        {
            InitializeComponent();

            try
            {
                Cursor = Cursors.WaitCursor;

                ConfigureRibbonActions();
                SetUpGrdBoxes();

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
                EnableExportExcel = true;
                EnableExportCsv = true;
                ConfigurePrintExportOptions();
                //Layout
                EnableLayoutOptions = true;
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
                CancelEdit();
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

        public override void bbiEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            base.bbiEdit_ItemClick(sender, e);

            try
            {
                ConfigureActionsStackViewEditing();
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

                if (ValidateBoxes() == false)
                    return;

                DialogResult result = MessageBox.Show(GlobalSetting.ResManager.GetString("SaveChanges"), "", MessageBoxButtons.YesNo);

                if (result != DialogResult.Yes)
                    return;

                Cursor = Cursors.WaitCursor;

                if (CurrentState == ActionsStates.New)
                {
                    res = CreateBoxes();
                }
                else if (CurrentState == ActionsStates.Edit)
                {
                    res = UpdateBoxes();
                }


                if (res == true)
                {
                    MessageBox.Show(GlobalSetting.ResManager.GetString("SaveSuccessfully"));
                    ActionsAfterCU();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public override void bbiExportCsv_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewBoxes.DataRowCount == 0)
            {
                MessageBox.Show(GlobalSetting.ResManager.GetString("NoDataSelected"));
                return;
            }

            //Abre el dialog de save as
            base.bbiExportCsv_ItemClick(sender, e);

            try
            {
                if (string.IsNullOrEmpty(ExportCsvFile) == false)
                {
                    gridViewBoxes.OptionsPrint.PrintFooter = false;
                    gridViewBoxes.ExportToCsv(ExportCsvFile);

                    DialogResult result = MessageBox.Show(GlobalSetting.ResManager.GetString("OpenFileQuestion"), "", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(ExportCsvFile);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public override void bbiExportExcel_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewBoxes.DataRowCount == 0)
            {
                MessageBox.Show(GlobalSetting.ResManager.GetString("NoDataSelected"));
                return;
            }

            //Abre el dialog de save as
            base.bbiExportExcel_ItemClick(sender, e);

            try
            {
                if (string.IsNullOrEmpty(ExportExcelFile) == false)
                {
                    gridViewBoxes.OptionsPrint.PrintFooter = false;
                    gridViewBoxes.ExportToXlsx(ExportExcelFile);

                    DialogResult result = MessageBox.Show(GlobalSetting.ResManager.GetString("OpenFileQuestion"), "", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(ExportExcelFile);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Form events
        private void BoxManagement_Load(object sender, EventArgs e)
        {
            try
            {
                LoadBoxes();
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void XgrdBoxes_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                if (CurrentState != ActionsStates.New)
                    return;

                GridView view = xgrdBoxes.FocusedView as GridView;
                Box box = view.GetRow(view.FocusedRowHandle) as Box;

                if (e.KeyCode == Keys.Enter)
                {

                    if (view.FocusedRowHandle == view.RowCount - 1)
                    {
                        if (string.IsNullOrEmpty(box.Description) == false && box.Length > 0 && box.Width > 0 && box.Height > 0)
                        {
                            _boxList.Add(new Box());
                        }
                    }
                }
                else if(e.KeyCode == Keys.F4)
                {
                    DialogResult result = XtraMessageBox.Show("Delete row?", "Confirmation", MessageBoxButtons.YesNo);
                    if (result != DialogResult.Yes)
                        return;

                    _boxList.Remove(box);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Private Methods

        #region Load/Resets

        private void LoadBoxes()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                _boxList = new BindingList<Box>(GlobalSetting.BoxService.GetBoxes());

                xgrdBoxes.DataSource = null;
                xgrdBoxes.DataSource = _boxList;
            }
            catch
            {
                throw;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        #endregion

        #region Setup Form Objects

        private void SetUpGrdBoxes()
        {
            try
            {
                //Activar que se alternen los colores de las filas del grid
                gridViewBoxes.OptionsView.EnableAppearanceOddRow = true;
                gridViewBoxes.OptionsView.EnableAppearanceEvenRow = true;

                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                gridViewBoxes.OptionsView.ColumnAutoWidth = false;
                gridViewBoxes.HorzScrollVisibility = ScrollVisibility.Auto;

                //Hacer todo el grid no editable
                gridViewBoxes.OptionsBehavior.Editable = false;

                //Column Definition
                GridColumn colIdBox = new GridColumn() { Caption = "Id", Visible = true, FieldName = nameof(Box.IdBox), Width = 150 };
                GridColumn colDescription = new GridColumn() { Caption = "Description", Visible = true, FieldName = nameof(Box.Description), Width = 150 };
                GridColumn colLength = new GridColumn() { Caption = "Length", Visible = true, FieldName = nameof(Box.Length), Width = 80 };
                GridColumn colWidth = new GridColumn() { Caption = "Width", Visible = true, FieldName = nameof(Box.Width), Width = 80 };
                GridColumn colHeight = new GridColumn() { Caption = "Height", Visible = true, FieldName = nameof(Box.Height), Width = 80 };

                //Display Format
                colLength.DisplayFormat.FormatType = FormatType.Numeric;
                colLength.DisplayFormat.FormatString = "n0";

                colWidth.DisplayFormat.FormatType = FormatType.Numeric;
                colWidth.DisplayFormat.FormatString = "n0";

                colHeight.DisplayFormat.FormatType = FormatType.Numeric;
                colHeight.DisplayFormat.FormatString = "n0";

                //Edit Repositories
                RepositoryItemTextEdit ritxtInt = new RepositoryItemTextEdit();
                ritxtInt.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                ritxtInt.Mask.EditMask = "N";

                colLength.ColumnEdit = ritxtInt;
                colWidth.ColumnEdit = ritxtInt;
                colHeight.ColumnEdit = ritxtInt;


                //Add columns to grid view
                gridViewBoxes.Columns.Add(colIdBox);
                gridViewBoxes.Columns.Add(colDescription);
                gridViewBoxes.Columns.Add(colLength);
                gridViewBoxes.Columns.Add(colWidth);
                gridViewBoxes.Columns.Add(colHeight);

                //events
                xgrdBoxes.ProcessGridKey += XgrdBoxes_ProcessGridKey;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region aux
        private void CancelEdit()
        {
            try
            {
                LoadBoxes();
                //Hacer todo el grid no editable
                gridViewBoxes.OptionsBehavior.Editable = false;
            }
            catch
            {
                throw;
            }
        }

        private void ConfigureActionsStackViewEditing()
        {
            try
            {
                //Ponemos el grid como editable y bloqueamos las columnas que no se puede editar
                gridViewBoxes.OptionsBehavior.Editable = true;

                foreach (GridColumn col in gridViewBoxes.Columns)
                {
                    if (col.FieldName == nameof(Box.IdBox))
                        col.OptionsColumn.AllowEdit = false;
                }
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
                _boxList = new BindingList<Box>();
                _boxList.Add(new Box());

                xgrdBoxes.DataSource = null;
                xgrdBoxes.DataSource = _boxList;

                gridViewBoxes.Columns[nameof(Box.IdBox)].Visible = false;

                ConfigureActionsStackViewEditing();

            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region CRUD

        private bool ValidateBoxes()
        {
            try
            {
                foreach (var box in _boxList)
                {

                    if (string.IsNullOrEmpty(box.Description))
                    {
                        MessageBox.Show("You must indicate Description for all boxes", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    if (box.Length == 0 || box.Width == 0 || box.Height == 0)
                    {
                        MessageBox.Show("You must indicate Length, Width and Height for all boxes", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private bool CreateBoxes()
        {
            try
            {
                return GlobalSetting.BoxService.CreateBoxes(_boxList.ToList());
            }
            catch
            {
                throw;
            }
        }

        private bool UpdateBoxes()
        {
            try
            {
                return GlobalSetting.BoxService.UpdateBoxes(_boxList.ToList());
            }
            catch
            {
                throw;
            }
        }

        private void ActionsAfterCU()
        {
            try
            {
                RestoreInitState(); //Restore de ribbon to initial states
                LoadBoxes();
                gridViewBoxes.Columns[nameof(Box.IdBox)].Visible = true;
                gridViewBoxes.OptionsBehavior.Editable = false;

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
