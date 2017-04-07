using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout.Utils;
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
    public partial class ItemManagement : RibbonFormBase
    {
        #region Enums
        private enum eItemColumns
        {
            IdVer,
            IdSubVer,
            Timestamp,
            ItemCode,
            ItemName,
            Model,
            Active,
            IdStatus,
            Launched,
            Retired,
            MmFront,
            Size,
            CategoryName,
            Caliber,
        }
        #endregion

        #region Private Members

        Item _itemUpdate;
        Item _itemOriginal;

        List<Item> _itemsList;

        string[] _nonEditingFields = { "txtItemCode", "txtIdVersion", "txtIdSubversion", "txtTimestamp" };	

        #endregion

        #region Constructor
        public ItemManagement()
        {
            InitializeComponent();

            try
            {
                ConfigureRibbonActions();
                SetUpGrdItems();
                SetUpTexEdit();
                SetupDateEdit();
                ResetItemUpdate();
                SetFormBinding();
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
                _itemOriginal = null;
                ResetItemUpdate();
                SetFormBinding();
                xtpForm.PageVisible = false;
                xtpList.PageVisible = true;
                sbNewVersion.Visible = false;
                LoadItemsList();
                SetNonCreatingFieldsVisibility(LayoutVisibility.Always);
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
                if (_itemOriginal == null)
                {
                    MessageBox.Show("No item selected");
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

                if (IsValidItem() == false)
                    return;

                DialogResult result = MessageBox.Show(GlobalSetting.ResManager.GetString("SaveChanges"), "", MessageBoxButtons.YesNo);

                if (result != DialogResult.Yes)
                    return;

                Cursor = Cursors.WaitCursor;

                if (_itemUpdate.Equals(_itemOriginal))
                {
                    MessageBox.Show(GlobalSetting.ResManager.GetString("NoPendingChanges"));
                    return;
                }

                if (CurrentState == ActionsStates.Edit)
                {
                    res = UpdateItem();
                }
                else if (CurrentState == ActionsStates.New)
                {
                    res = CreateItem();
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
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        #endregion

        #region Form Events

        private void ItemManagement_Load(object sender, EventArgs e)
        {
            try
            {
                xtpForm.PageVisible = false;
                //sbNewVersion.Visible = false; //TODO
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void rootGridViewItems_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                object itemCode = view.GetRowCellValue(view.FocusedRowHandle, eItemColumns.ItemCode.ToString());
                if (itemCode != null)
                    LoadItemForm(itemCode.ToString());
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void sbLoad_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                LoadItemsList();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void sbNewVersion_Click(object sender, EventArgs e)
        {
            try
            {
                Validate();

                if (_itemUpdate.Equals(_itemOriginal))
                {
                    XtraMessageBox.Show(GlobalSetting.ResManager.GetString("NoPendingChanges"));
                    return;
                }

                if (IsValidItem() == false)
                    return;

                DialogResult result = MessageBox.Show(GlobalSetting.ResManager.GetString("SaveChanges"), "", MessageBoxButtons.YesNo);

                if (result != DialogResult.Yes)
                    return;
                
                Cursor = Cursors.WaitCursor;

                if (UpdateItem(true))
                {
                    ActionsAfterCU();
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

        #region Private Methods

        /// <summary>
        /// Resetear el objeto item que usamos para la actualización
        /// </summary>
        private void ResetItemUpdate()
        {
            _itemUpdate = new Item();
        }

        private void SetUpGrdItems()
        {
            try
            {
                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                rootGridViewItems.OptionsView.ColumnAutoWidth = false;
                rootGridViewItems.HorzScrollVisibility = ScrollVisibility.Auto;

                //hacer todo el grid no editable
                rootGridViewItems.OptionsBehavior.Editable = false;

                //Columns definition
                GridColumn colIdVer = new GridColumn() { Caption = "Version Id", Visible = true, FieldName = eItemColumns.IdVer.ToString(), Width = 70 };
                GridColumn colIdSubVer = new GridColumn() { Caption = "Subversion Id", Visible = true, FieldName = eItemColumns.IdSubVer.ToString(), Width = 80 };
                GridColumn colTimestamp = new GridColumn() { Caption = "Timestamp", Visible = true, FieldName = eItemColumns.Timestamp.ToString(), Width = 130 };
                GridColumn colItemCode = new GridColumn() { Caption = "Item Code", Visible = true, FieldName = eItemColumns.ItemCode.ToString(), Width = 150 };
                GridColumn colItemName = new GridColumn() { Caption = "Item Name", Visible = true, FieldName = eItemColumns.ItemName.ToString(), Width = 250 };
                GridColumn colModel = new GridColumn() { Caption = "Model", Visible = true, FieldName = eItemColumns.Model.ToString(), Width = 120 };
                GridColumn colActive = new GridColumn() { Caption = "Active", Visible = true, FieldName = eItemColumns.Active.ToString(), Width = 50 };
                GridColumn colIdStatus = new GridColumn() { Caption = "Id Status", Visible = true, FieldName = eItemColumns.IdStatus.ToString(), Width = 60 };
                GridColumn colLaunched = new GridColumn() { Caption = "Launched", Visible = true, FieldName = eItemColumns.Launched.ToString(), Width = 90 };
                GridColumn colRetired = new GridColumn() { Caption = "Retired", Visible = true, FieldName = eItemColumns.Retired.ToString(), Width = 90 };
                GridColumn colMmFront = new GridColumn() { Caption = "Front (mm)", Visible = true, FieldName = eItemColumns.MmFront.ToString(), Width = 70 };
                GridColumn colSize = new GridColumn() { Caption = "Size", Visible = true, FieldName = eItemColumns.Size.ToString(), Width = 70 };
                GridColumn colCategoryName = new GridColumn() { Caption = "Category Name", Visible = true, FieldName = eItemColumns.CategoryName.ToString(), Width = 90 };
                GridColumn colCaliber = new GridColumn() { Caption = "Caliber", Visible = true, FieldName = eItemColumns.Caliber.ToString(), Width = 70 };

                //Display Format
                colMmFront.DisplayFormat.FormatType = FormatType.Numeric;
                colMmFront.DisplayFormat.FormatString = "n2"; //2 Decimales
                colCaliber.DisplayFormat.FormatType = FormatType.Numeric;
                colCaliber.DisplayFormat.FormatString = "n2";

                //Add columns to grid root view
                rootGridViewItems.Columns.Add(colIdVer);
                rootGridViewItems.Columns.Add(colIdSubVer);
                rootGridViewItems.Columns.Add(colTimestamp);
                rootGridViewItems.Columns.Add(colItemCode);
                rootGridViewItems.Columns.Add(colItemName);
                rootGridViewItems.Columns.Add(colModel);
                rootGridViewItems.Columns.Add(colActive);
                rootGridViewItems.Columns.Add(colIdStatus);
                rootGridViewItems.Columns.Add(colLaunched);
                rootGridViewItems.Columns.Add(colRetired);
                rootGridViewItems.Columns.Add(colMmFront);
                rootGridViewItems.Columns.Add(colSize);
                rootGridViewItems.Columns.Add(colCategoryName);
                rootGridViewItems.Columns.Add(colCaliber);

                //Events
                rootGridViewItems.DoubleClick += rootGridViewItems_DoubleClick;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetUpTexEdit()
        {
            try
            {
                txtCaliber.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                txtCaliber.Properties.Mask.EditMask = "F2"; //Dos decimales
                txtCaliber.Properties.Mask.UseMaskAsDisplayFormat = true;
                txtCaliber.EditValue = "0.00";

                txtMmFront.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                txtMmFront.Properties.Mask.EditMask = "F2";
                txtMmFront.Properties.Mask.UseMaskAsDisplayFormat = true;
                txtMmFront.EditValue = "0.00";

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetupDateEdit()
        {
            try
            {
                //deLaunched.Properties.Buttons.

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetFormBinding()
        {
            try
            {
                foreach (Control ctl in layoutControlForm.Controls)
                {
                    if (ctl.GetType() == typeof(TextEdit))
                    {
                        ctl.DataBindings.Clear();
                        ((TextEdit)ctl).ReadOnly = true;
                    }
                    else if (ctl.GetType() == typeof(CheckEdit))
                    {
                        ctl.DataBindings.Clear();
                        ((CheckEdit)ctl).ReadOnly = true;
                    }
                    else if (ctl.GetType() == typeof(DateEdit))
                    {
                        ctl.DataBindings.Clear();
                        ((DateEdit)ctl).ReadOnly = true;
                    }
                }

                //TextEdit
                txtItemCode.DataBindings.Add<Item>(_itemUpdate, (Control c) => c.Text, item => item.ItemCode);
                txtIdVersion.DataBindings.Add<Item>(_itemUpdate, (Control c) => c.Text, item => item.IdVer);
                txtIdSubversion.DataBindings.Add<Item>(_itemUpdate, (Control c) => c.Text, item => item.IdSubVer);
                txtTimestamp.DataBindings.Add<Item>(_itemUpdate, (Control c) => c.Text, item => item.Timestamp);
                txtItemName.DataBindings.Add<Item>(_itemUpdate, (Control c) => c.Text, item => item.ItemName);
                txtModel.DataBindings.Add<Item>(_itemUpdate, (Control c) => c.Text, item => item.Model);
                txtStatus.DataBindings.Add<Item>(_itemUpdate, (Control c) => c.Text, item => item.IdStatus);
                txtMmFront.DataBindings.Add<Item>(_itemUpdate, (Control c) => c.Text, item => item.MmFront);
                txtSize.DataBindings.Add<Item>(_itemUpdate, (Control c) => c.Text, item => item.Size);
                txtCategoryName.DataBindings.Add<Item>(_itemUpdate, (Control c) => c.Text, item => item.CategoryName); ;
                txtCaliber.DataBindings.Add<Item>(_itemUpdate, (Control c) => c.Text, item => item.Caliber);
                //CheckEdit
                chkActive.DataBindings.Add<Item>(_itemUpdate, (CheckEdit chk) => chk.Checked, item => item.Active);
                //DateEdit
                deLaunched.DataBindings.Add<Item>(_itemUpdate, (DateEdit d) => d.EditValue, item => item.Launched);
                deRetired.DataBindings.Add<Item>(_itemUpdate, (DateEdit d) => d.EditValue, item => item.Retired);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // <summary>
        /// cargar la colección de Items del sistema
        /// </summary>
        /// <remarks></remarks>
        private void LoadItemsList()
        {
            try
            {
                _itemsList = GlobalSetting.ItemService.GetItems();
                xgrdItems.DataSource = _itemsList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Cargar los datos de un customer en concreto
        /// </summary>
        /// <param name="itemCode"></param>
        private void LoadItemForm(string itemCode)
        {
            try
            {
                _itemUpdate = GlobalSetting.ItemService.GetItemByItemCode(itemCode);
                _itemOriginal = _itemUpdate.Clone();
                SetFormBinding(); //refresh binding
                xtpForm.PageVisible = true;
                xtcGeneral.SelectedTabPage = xtpForm;
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
                xtpList.PageVisible = true;
                //sbNewVersion.Visible = true;
                SetEditingFieldsEnabled();
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
                xtpList.PageVisible = false;
                xtpForm.PageVisible = true;
                sbNewVersion.Visible = false;
                ResetItemUpdate();
                SetFormBinding(); //refresh binding
                SetNonCreatingFieldsVisibility(LayoutVisibility.Never);
                SetCreatingFieldsEnabled();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Poner como editables los campos para el modo de edición
        /// </summary>
        private void SetEditingFieldsEnabled()
        {
            try
            {
                foreach (Control ctl in layoutControlForm.Controls)
                {
                    if (Array.IndexOf(_nonEditingFields, ctl.Name) < 0)
                    {
                        if (ctl.GetType() == typeof(TextEdit))
                        {
                            ((TextEdit)ctl).ReadOnly = false;
                        }
                        else if (ctl.GetType() == typeof(CheckEdit))
                        {
                            ((CheckEdit)ctl).ReadOnly = false;
                        }
                        else if (ctl.GetType() == typeof(DateEdit))
                        {
                            ((DateEdit)ctl).ReadOnly = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetCreatingFieldsEnabled()
        {
            try
            {
                foreach (Control ctl in layoutControlForm.Controls)
                {
                    if (ctl.GetType() == typeof(TextEdit))
                    {
                        ((TextEdit)ctl).ReadOnly = false;
                    }
                    else if (ctl.GetType() == typeof(CheckEdit))
                    {
                        ((CheckEdit)ctl).ReadOnly = false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetNonCreatingFieldsVisibility(LayoutVisibility visibility)
        {
            try
            {
                lciIdVersion.Visibility = visibility;
                lciIdSubversion.Visibility = visibility;
                lciTimestamp.Visibility = visibility;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Mover la fila activa a un item en concreto
        /// </summary>
        /// <param name="itemCode"></param>
        private void MoveGridToItem(string itemCode)
        {
            try
            {
                GridColumn column = rootGridViewItems.Columns[eItemColumns.ItemCode.ToString()];
                if (column != null)
                {
                    // locating the row 
                    int rhFound = rootGridViewItems.LocateByDisplayText(rootGridViewItems.FocusedRowHandle + 1, column, itemCode);
                    // focusing the cell 
                    if (rhFound != GridControl.InvalidRowHandle)
                    {
                        rootGridViewItems.FocusedRowHandle = rhFound;
                        rootGridViewItems.FocusedColumn = column;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsValidItem()
        {
            try
            {
                foreach (Control ctl in layoutControlForm.Controls)
                {
                    if (ctl.GetType() == typeof(TextEdit))
                    {
                        if (string.IsNullOrEmpty(((TextEdit)ctl).Text))
                        {
                            MessageBox.Show(string.Format(GlobalSetting.ResManager.GetString("NullArgument"), ctl.Name));
                            return false;
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Actualizar los datos de un item
        /// </summary>
        /// <param name="newVersion">Si es una versión nueva o una actualización de la existente</param>
        /// <returns></returns>
        private bool UpdateItem(bool newVersion = false)
        {
            try
            {
                return GlobalSetting.ItemService.UpdateItem(_itemUpdate, newVersion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Crear un nuevo item
        /// </summary>
        /// <returns></returns>
        private bool CreateItem()
        {
            try
            {
                _itemOriginal = _itemUpdate.Clone();
                return GlobalSetting.ItemService.newItem(_itemUpdate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ActionsAfterCU()
        {
            try
            {
                string itemCode = _itemOriginal.ItemCode;
                _itemOriginal = null;
                ResetItemUpdate();
                SetFormBinding();
                xtpForm.PageVisible = false;
                xtpList.PageVisible = true;
                LoadItemsList();
                MoveGridToItem(itemCode);
                RestoreInitState();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }

}
