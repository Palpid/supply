﻿using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
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
            IdDefaultSupplier,
            IdPrototype,
            PrototypeName,
            PrototypeDescription,
            PrototypeStatus,
            IdModel,
            Model,
            Caliber,
            IdColor1,
            IdColor2,
            IdItemBcn,
            IdItemHK,
            ItemDescription,
            Comments,
            Segment,
            Category,
            Age,
            LaunchDate,
            RemovalDate,
            IdStatusCial,
            IdStatusProd,
            IdUserAttri1,
            IdUserAttri2,
            IdUserAttri3,
        }
        #endregion

        #region Private Members

        Item _itemUpdate;
        Item _itemOriginal;

        List<Item> _itemsList;
        List<Supplier> _supplierList;
        List<StatusHK> _statusProdList;

        string[] _editingFields = { "lueIdDefaultSupplier", "lueIdStatusProd", "txtIdUserAttri1", "txtIdUserAttri2", "txtIdUserAttri3" };	

        #endregion

        #region Constructor
        public ItemManagement()
        {
            InitializeComponent();

            try
            {
                LoadSupplierList();
                LoadStatusProdList();
                ConfigureRibbonActions();
                SetUpGrdItems();
                SetUpTexEdit();
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
                Item  item = view.GetRow(view.FocusedRowHandle) as Item;
                if (item != null)
                    LoadItemForm(item);
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
                GridColumn colIdSubVer = new GridColumn() { Caption = "Subversion Id", Visible = true, FieldName = eItemColumns.IdSubVer.ToString(), Width = 85 };
                GridColumn colTimestamp = new GridColumn() { Caption = "Timestamp", Visible = true, FieldName = eItemColumns.Timestamp.ToString(), Width = 130 };
                GridColumn colIdDefaultSupplier = new GridColumn() { Caption = "Default Supplier", Visible = true, FieldName = eItemColumns.IdDefaultSupplier.ToString(), Width = 110 };
                GridColumn colIdPrototype = new GridColumn() { Caption = "Id Prototype", Visible = true, FieldName = eItemColumns.IdPrototype.ToString(), Width = 150 };
                GridColumn colPrototypeName = new GridColumn() { Caption = "Prototype Name", Visible = true, FieldName = eItemColumns.PrototypeName.ToString(), Width = 150 };
                GridColumn colPrototypeDescription = new GridColumn() { Caption = "Prototype Description", Visible = true, FieldName = eItemColumns.PrototypeDescription.ToString(), Width = 150 };
                GridColumn colPrototypeStatus = new GridColumn() { Caption = "Prototype Status", Visible = true, FieldName = eItemColumns.PrototypeStatus.ToString(), Width = 150 };
                GridColumn colIdModel = new GridColumn() { Caption = "Id Model", Visible = false, FieldName = eItemColumns.IdModel.ToString(), Width = 0 };
                GridColumn colModel = new GridColumn() { Caption = "Model", Visible = true, FieldName = eItemColumns.Model.ToString() + ".Description", Width = 120 };
                GridColumn colCaliber = new GridColumn() { Caption = "Caliber", Visible = true, FieldName = eItemColumns.Caliber.ToString(), Width = 70 };
                GridColumn colIdColor1 = new GridColumn() { Caption = "Color 1", Visible = true, FieldName = eItemColumns.IdColor1.ToString(), Width = 60 };
                GridColumn colIdColor2 = new GridColumn() { Caption = "Color 2", Visible = true, FieldName = eItemColumns.IdColor2.ToString(), Width = 60 };
                GridColumn colIdItemBcn = new GridColumn() { Caption = "Item BCN", Visible = true, FieldName = eItemColumns.IdItemBcn.ToString(), Width = 160 };
                GridColumn colIdItemHK = new GridColumn() { Caption = "Item HK", Visible = true, FieldName = eItemColumns.IdItemHK.ToString(), Width = 160 };
                GridColumn colItemDescription = new GridColumn() { Caption = "Item Description", Visible = true, FieldName = eItemColumns.ItemDescription.ToString(), Width = 300 };

                GridColumn colComments = new GridColumn() { Caption = "Comments", Visible = true, FieldName = eItemColumns.Comments.ToString(), Width = 300 }; //No aparece?
                
                GridColumn colSegment = new GridColumn() { Caption = "Segment", Visible = true, FieldName = eItemColumns.Segment.ToString(), Width = 70 };
                GridColumn colCategory = new GridColumn() { Caption = "Category", Visible = true, FieldName = eItemColumns.Category.ToString(), Width = 70 };
                GridColumn colAge = new GridColumn() { Caption = "Age", Visible = true, FieldName = eItemColumns.Age.ToString(), Width = 70 };

                GridColumn colLaunchedDate = new GridColumn() { Caption = "Launch Date", Visible = true, FieldName = eItemColumns.LaunchDate.ToString(), Width = 90 };
                GridColumn colRemovalDate = new GridColumn() { Caption = "Removal Date", Visible = true, FieldName = eItemColumns.RemovalDate.ToString(), Width = 90 };
                GridColumn colIdStatusCial = new GridColumn() { Caption = "Status Cial", Visible = true, FieldName = eItemColumns.IdStatusCial.ToString(), Width = 90 };
                GridColumn colIdStatusProd = new GridColumn() { Caption = "Status Prod", Visible = true, FieldName = eItemColumns.IdStatusProd.ToString(), Width = 90 };
                GridColumn colIdUserAttri1 = new GridColumn() { Caption = "User Attri. 1", Visible = true, FieldName = eItemColumns.IdUserAttri1.ToString(), Width = 90 };
                GridColumn colIdUserAttri2 = new GridColumn() { Caption = "User Attri. 2", Visible = true, FieldName = eItemColumns.IdUserAttri2.ToString(), Width = 90 };
                GridColumn colIdUserAttri3 = new GridColumn() { Caption = "User Attri. 3", Visible = true, FieldName = eItemColumns.IdUserAttri3.ToString(), Width = 90 };


                //Display Format
                colTimestamp.DisplayFormat.FormatType = FormatType.DateTime;

                colCaliber.DisplayFormat.FormatType = FormatType.Numeric;
                colCaliber.DisplayFormat.FormatString = "n2";

                //Add columns to grid root view
                rootGridViewItems.Columns.Add(colIdVer);
                rootGridViewItems.Columns.Add(colIdSubVer);
                rootGridViewItems.Columns.Add(colTimestamp);
                rootGridViewItems.Columns.Add(colIdDefaultSupplier);
                rootGridViewItems.Columns.Add(colIdPrototype);
                rootGridViewItems.Columns.Add(colPrototypeName);
                rootGridViewItems.Columns.Add(colPrototypeDescription);
                rootGridViewItems.Columns.Add(colPrototypeStatus);
                rootGridViewItems.Columns.Add(colIdModel);
                rootGridViewItems.Columns.Add(colModel);
                rootGridViewItems.Columns.Add(colCaliber);
                rootGridViewItems.Columns.Add(colIdColor1);
                rootGridViewItems.Columns.Add(colIdColor2);
                rootGridViewItems.Columns.Add(colIdItemBcn);
                rootGridViewItems.Columns.Add(colIdItemHK);
                rootGridViewItems.Columns.Add(colItemDescription);
                rootGridViewItems.Columns.Add(colComments);
                rootGridViewItems.Columns.Add(colSegment);
                rootGridViewItems.Columns.Add(colCategory);
                rootGridViewItems.Columns.Add(colAge);
                rootGridViewItems.Columns.Add(colLaunchedDate);
                rootGridViewItems.Columns.Add(colRemovalDate);
                rootGridViewItems.Columns.Add(colIdStatusCial);
                rootGridViewItems.Columns.Add(colIdStatusProd);
                rootGridViewItems.Columns.Add(colIdUserAttri1);
                rootGridViewItems.Columns.Add(colIdUserAttri2);
                rootGridViewItems.Columns.Add(colIdUserAttri3);

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

                txtLaunchDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
                txtLaunchDate.Properties.Mask.EditMask = "dd/MM/yyyy";
                txtLaunchDate.Properties.Mask.UseMaskAsDisplayFormat = true;

                txtRemovalDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
                txtRemovalDate.Properties.Mask.EditMask = "dd/MM/yyyy";
                txtRemovalDate.Properties.Mask.UseMaskAsDisplayFormat = true;

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
                    else if (ctl.GetType() == typeof(LookUpEdit))
                    {
                        ctl.DataBindings.Clear();
                        ((LookUpEdit)ctl).ReadOnly = true;
                    }
                }

                if (_itemUpdate.Model == null) _itemUpdate.Model = new Model(); //para evitar problemas al bindear nested properties

                //TextEdit
                txtIdVersion.DataBindings.Add<Item>(_itemUpdate, (Control c) => c.Text, item => item.IdVer);
                txtIdSubversion.DataBindings.Add<Item>(_itemUpdate, (Control c) => c.Text, item => item.IdSubVer);
                txtTimestamp.DataBindings.Add<Item>(_itemUpdate, (Control c) => c.Text, item => item.Timestamp);
                txtIdPrototype.DataBindings.Add<Item>(_itemUpdate, (Control c) => c.Text, item => item.IdPrototype);
                txtPrototypeName.DataBindings.Add<Item>(_itemUpdate, (Control c) => c.Text, item => item.PrototypeName);
                txtPrototypeDescription.DataBindings.Add<Item>(_itemUpdate, (Control c) => c.Text, item => item.PrototypeDescription);
                //txtModel.DataBindings.Add<Item>(_itemUpdate, (Control c) => c.Text, item => item.Model.Description);
                txtModel.DataBindings.Add("Text", _itemUpdate, "Model.Description");//La custom extension que hice no funciona con propiedades que son clases, bindeo a la antigua
                txtCaliber.DataBindings.Add<Item>(_itemUpdate, (Control c) => c.Text, item => item.Caliber);
                txtIdColor1.DataBindings.Add<Item>(_itemUpdate, (Control c) => c.Text, item => item.IdColor1);
                txtIdColor2.DataBindings.Add<Item>(_itemUpdate, (Control c) => c.Text, item => item.IdColor2);
                txtIdItemBcn.DataBindings.Add<Item>(_itemUpdate, (Control c) => c.Text, item => item.IdItemBcn);
                txtIdItemHK.DataBindings.Add<Item>(_itemUpdate, (Control c) => c.Text, item => item.IdItemHK);
                txtItemDescription.DataBindings.Add<Item>(_itemUpdate, (Control c) => c.Text, item => item.ItemDescription);
                txtComments.DataBindings.Add<Item>(_itemUpdate, (Control c) => c.Text, item => item.Comments);
                txtSegment.DataBindings.Add<Item>(_itemUpdate, (Control c) => c.Text, item => item.Segment);
                txtCategory.DataBindings.Add<Item>(_itemUpdate, (Control c) => c.Text, item => item.Category);
                txtAge.DataBindings.Add<Item>(_itemUpdate, (Control c) => c.Text, item => item.Age);
                txtLaunchDate.DataBindings.Add<Item>(_itemUpdate, (Control c) => c.Text, item => item.LaunchDate);
                txtRemovalDate.DataBindings.Add<Item>(_itemUpdate, (Control c) => c.Text, item => item.RemovalDate);
                txtIdStatusCial.DataBindings.Add<Item>(_itemUpdate, (Control c) => c.Text, item => item.IdStatusCial);
                txtIdUserAttri1.DataBindings.Add<Item>(_itemUpdate, (Control c) => c.Text, item => item.IdUserAttri1);
                txtIdUserAttri2.DataBindings.Add<Item>(_itemUpdate, (Control c) => c.Text, item => item.IdUserAttri2);
                txtIdUserAttri3.DataBindings.Add<Item>(_itemUpdate, (Control c) => c.Text, item => item.IdUserAttri3);

                //LookUpEdit
                lueIdDefaultSupplier.DataBindings.Add<Item>(_itemUpdate, (LookUpEdit e) => e.EditValue, item => item.IdDefaultSupplier);
                lueIdStatusProd.DataBindings.Add<Item>(_itemUpdate, (LookUpEdit e) => e.EditValue, item => item.IdStatusProd);
                
                //CheckEdit
                //chkActive.DataBindings.Add<Item>(_itemUpdate, (CheckEdit chk) => chk.Checked, item => item.Active);
                ////DateEdit
                //deLaunched.DataBindings.Add<Item>(_itemUpdate, (DateEdit d) => d.EditValue, item => item.Launched);
                //deRetired.DataBindings.Add<Item>(_itemUpdate, (DateEdit d) => d.EditValue, item => item.Retired);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Cargar la colección de suppliers del sistema
        /// </summary>
        private void LoadSupplierList()
        {
            try
            {
                _supplierList = GlobalSetting.SupplierService.GetSuppliers();

                lueIdDefaultSupplier.Properties.DataSource = _supplierList;
                lueIdDefaultSupplier.Properties.DisplayMember = "IdSupplier";
                lueIdDefaultSupplier.Properties.ValueMember = "IdSupplier";
                lueIdDefaultSupplier.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("IdSupplier", 20, "Id Supplier"));
                lueIdDefaultSupplier.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SupplierName", 100, "Name"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Cargar la colección de Status Prod. del sistema
        /// </summary>
        private void LoadStatusProdList()
        {
            try
            {
                _statusProdList = GlobalSetting.StatusProdService.GetStatusProd();

                lueIdStatusProd.Properties.DataSource = _statusProdList;
                lueIdStatusProd.Properties.DisplayMember = "Description";
                lueIdStatusProd.Properties.ValueMember = "Id";
                //lueIdStatusProd.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("IdSupplier", 20, "Id Supplier"));
                //lueIdStatusProd.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SupplierName", 100, "Name"));
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
        /// <param name="item"></param>
        private void LoadItemForm(Item item)
        {
            try
            {
                _itemUpdate = item.Clone();
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
                    if (Array.IndexOf(_editingFields, ctl.Name) >= 0)
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
                        else if (ctl.GetType() == typeof(LookUpEdit))
                        {
                            ((LookUpEdit)ctl).ReadOnly = false;
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
                //TODO
                //GridColumn column = rootGridViewItems.Columns[eItemColumns.ItemCode.ToString()];
                //if (column != null)
                //{
                //    // locating the row 
                //    int rhFound = rootGridViewItems.LocateByDisplayText(rootGridViewItems.FocusedRowHandle + 1, column, itemCode);
                //    // focusing the cell 
                //    if (rhFound != GridControl.InvalidRowHandle)
                //    {
                //        rootGridViewItems.FocusedRowHandle = rhFound;
                //        rootGridViewItems.FocusedColumn = column;
                //    }
                //}
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
                string itemCode = _itemOriginal.IdItemBcn;
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
