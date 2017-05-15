using DevExpress.XtraEditors;
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
using DevExpress.XtraBars;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;


namespace HKSupply.Forms.Master
{
    public partial class BomManagement : RibbonFormBase
    {
        #region Private Members
        List<ItemEy> _itemsEyList;
        List<ItemMt> _itemsMtList;
        List<ItemHw> _itemsHwList;

        List<Classes.ItemEyBom> _itemBom = new List<Classes.ItemEyBom>();
        #endregion

        #region Constructor
        public BomManagement()
        {
            InitializeComponent();

            try
            {
                ConfigureRibbonActions();
                SetUpDockPanels();
                SetUpGrdItemsEy();
                SetUpGrdItemsMt();
                SetUpGrdItemsHw();
                SetUpGrdItemBom();
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
                //Task Buttons
                Read = actions.Read;
                New = actions.New;
                Modify = actions.Modify;
                RestoreInitState();
                //Print and export buttons
                EnablePrintPreview = false;
                EnableExportExcel = false;
                EnableExportCsv = false;
                ConfigurePrintExportOptions();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void bbiCancel_ItemClick(object sender, ItemClickEventArgs e)
        {
            base.bbiCancel_ItemClick(sender, e);
        }

        public override void bbiEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            base.bbiEdit_ItemClick(sender, e);
        }

        public override void bbiSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            base.bbiSave_ItemClick(sender, e);
        }

        #endregion

        #region Form Events
        private void BomManagement_Load(object sender, EventArgs e)
        {
            try
            {
                LoadItemsListEy();
                LoadItemsListMt();
                LoadItemsListHw();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GridViewItemsEy_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                ItemEy item = view.GetRow(view.FocusedRowHandle) as ItemEy;
                if (item != null)
                {
                    LoadItemGridBom(item);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void GridViewItemsMt_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                ItemMt itemMt = view.GetRow(view.FocusedRowHandle) as ItemMt;
                AddRawMaterial(itemMt);

            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GridViewItemsHw_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                ItemHw itemHw = view.GetRow(view.FocusedRowHandle) as ItemHw;
                AddHardware(itemHw);
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void XgrdItemBom_ViewRegistered(object sender, ViewOperationEventArgs e)
        {
            try
            {
                switch (e.View.LevelName)
                {
                    case nameof(Classes.ItemEyBom.RawMaterials):
                    case nameof(Classes.ItemEyBom.Hardware):

                        //Ocultamos las columnas que no nos interesan
                        (e.View as GridView).Columns[nameof(Classes.Bom.Id)].Visible = false;
                        (e.View as GridView).Columns[nameof(Classes.Bom.IdItemGroup)].Visible = false;

                        //Seteamos el tamaño de las columnas

                   
                        break;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Private Members

        #region SetUp Form Objects

        private void SetUpDockPanels()
        {
            try
            {
                dockPanelItemsEy.Options.ShowCloseButton = false;
                dockPanelItemsHw.Options.ShowCloseButton = false;
                dockPanelItemsMt.Options.ShowCloseButton = false;
                dockPanelGrdBom.Options.ShowCloseButton = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetUpGrdItemsEy()
        {
            try
            {
                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                gridViewItemsEy.OptionsView.ColumnAutoWidth = false;
                gridViewItemsEy.HorzScrollVisibility = ScrollVisibility.Auto;

                //Hacer todo el grid no editable
                gridViewItemsEy.OptionsBehavior.Editable = false;

                //Columns definition
                GridColumn colIdItemBcn = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("ItemBCN"), Visible = true, FieldName = nameof(ItemEy.IdItemBcn), Width = 250 };
                GridColumn colFamilyHK = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("FamilyHk"), Visible = false, FieldName = nameof(ItemEy.IdFamilyHK), Width = 10 };
                GridColumn colModel = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Model"), Visible = true, FieldName = $"{nameof(ItemEy.Model)}.{nameof(Model.Description)}", Width = 10 };
                GridColumn colIdColor1 = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Color1"), Visible = false, FieldName = nameof(ItemEy.IdColor1), Width = 10 };
                GridColumn colIdColor2 = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Color2"), Visible = false, FieldName = nameof(ItemEy.IdColor2), Width = 10 };

                //Add columns to grid root view
                gridViewItemsEy.Columns.Add(colIdItemBcn);
                gridViewItemsEy.Columns.Add(colFamilyHK);
                gridViewItemsEy.Columns.Add(colModel);
                gridViewItemsEy.Columns.Add(colIdColor1);
                gridViewItemsEy.Columns.Add(colIdColor2);

                //Grouping
                gridViewItemsEy.OptionsView.ShowGroupPanel = false;

                gridViewItemsEy.Columns[nameof(ItemEy.IdFamilyHK)].GroupIndex = 0;
                gridViewItemsEy.Columns[$"{nameof(ItemEy.Model)}.{nameof(Model.Description)}"].GroupIndex = 1;
                gridViewItemsEy.Columns[nameof(ItemEy.IdColor1)].GroupIndex = 2;
                gridViewItemsEy.Columns[nameof(ItemEy.IdColor2)].GroupIndex = 3;

                gridViewItemsEy.DoubleClick += GridViewItemsEy_DoubleClick;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetUpGrdItemsMt()
        {
            try
            {
                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                gridViewItemsMt.OptionsView.ColumnAutoWidth = false;
                gridViewItemsMt.HorzScrollVisibility = ScrollVisibility.Auto;

                //Hacer todo el grid no editable
                gridViewItemsMt.OptionsBehavior.Editable = false;

                //Columns definition
                GridColumn colIdItemBcn = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("ItemBCN"), Visible = true, FieldName = nameof(ItemMt.IdItemBcn), Width = 160 };
                GridColumn colItemDescription = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("ItemDescription"), Visible = true, FieldName = nameof(ItemMt.ItemDescription), Width = 300 };
                GridColumn colIdMatTypeL1 = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("MatTypeL1"), Visible = false, FieldName = nameof(ItemMt.IdMatTypeL1), Width = 100 };
                GridColumn colIdMatTypeL2 = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("MatTypeL2"), Visible = false, FieldName = nameof(ItemMt.IdMatTypeL2), Width = 100 };
                GridColumn colIdMatTypeL3 = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("MatTypeL3"), Visible = false, FieldName = nameof(ItemMt.IdMatTypeL3), Width = 100 };

                //Add columns to grid root view
                gridViewItemsMt.Columns.Add(colIdItemBcn);
                gridViewItemsMt.Columns.Add(colItemDescription);
                gridViewItemsMt.Columns.Add(colIdMatTypeL1);
                gridViewItemsMt.Columns.Add(colIdMatTypeL2);
                gridViewItemsMt.Columns.Add(colIdMatTypeL3);

                //Grouping
                gridViewItemsMt.OptionsView.ShowGroupPanel = false;

                gridViewItemsMt.Columns[nameof(ItemMt.IdMatTypeL1)].GroupIndex = 0;
                gridViewItemsMt.Columns[nameof(ItemMt.IdMatTypeL2)].GroupIndex = 1;
                //gridViewItemsMt.Columns[nameof(ItemMt.IdMatTypeL3)].GroupIndex = 2;

                //Events
                gridViewItemsMt.DoubleClick += GridViewItemsMt_DoubleClick;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetUpGrdItemsHw()
        {
            try
            {
                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                gridViewItemsHw.OptionsView.ColumnAutoWidth = false;
                gridViewItemsHw.HorzScrollVisibility = ScrollVisibility.Auto;

                //Hacer todo el grid no editable
                gridViewItemsHw.OptionsBehavior.Editable = false;

                //Columns definition
                GridColumn colIdItemBcn = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("ItemBCN"), Visible = true, FieldName = nameof(ItemHw.IdItemBcn), Width = 160 };
                GridColumn colItemDescription = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("ItemDescription"), Visible = true, FieldName = nameof(ItemHw.ItemDescription), Width = 300 };
                GridColumn colIdHwTypeL1 = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("MatTypeL1"), Visible = false, FieldName = nameof(ItemHw.IdHwTypeL1), Width = 100 };
                GridColumn colIdHwTypeL2 = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("MatTypeL2"), Visible = false, FieldName = nameof(ItemHw.IdHwTypeL2), Width = 100 };
                GridColumn colIdHwTypeL3 = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("MatTypeL3"), Visible = false, FieldName = nameof(ItemHw.IdHwTypeL3), Width = 100 };

                //Add columns to grid root view
                gridViewItemsHw.Columns.Add(colIdItemBcn);
                gridViewItemsHw.Columns.Add(colItemDescription);
                gridViewItemsHw.Columns.Add(colIdHwTypeL1);
                gridViewItemsHw.Columns.Add(colIdHwTypeL2);
                gridViewItemsHw.Columns.Add(colIdHwTypeL3);

                //Grouping
                gridViewItemsHw.OptionsView.ShowGroupPanel = false;

                gridViewItemsHw.Columns[nameof(ItemHw.IdHwTypeL1)].GroupIndex = 0;
                gridViewItemsHw.Columns[nameof(ItemHw.IdHwTypeL2)].GroupIndex = 1;
                //gridViewItemsHw.Columns[nameof(ItemHw.IdHwTypeL3)].GroupIndex = 2;

                //Events
                gridViewItemsHw.DoubleClick += GridViewItemsHw_DoubleClick;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetUpGrdItemBom()
        {
            try
            {
                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                gridViewItemBom.OptionsView.ColumnAutoWidth = false;
                gridViewItemBom.HorzScrollVisibility = ScrollVisibility.Auto;

                //Hacer todo el grid no editable
                gridViewItemBom.OptionsBehavior.Editable = false;

                //Columns Definition
                GridColumn colIdItemBcn = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("ItemBCN"), Visible = true, FieldName = $"{nameof(Classes.ItemEyBom.ItemEy)}.{nameof(ItemEy.IdItemBcn)}", Width = 200 };

                //Add columns to grid root view
                gridViewItemBom.Columns.Add(colIdItemBcn);

                //Events
                xgrdItemBom.ViewRegistered += XgrdItemBom_ViewRegistered;

                //Hide group panel
                gridViewItemBom.OptionsView.ShowGroupPanel = false;

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Loads

        private void LoadItemsListEy()
        {
            try
            {
                _itemsEyList = GlobalSetting.ItemEyService.GetItems();
                xgrdItemsEy.DataSource = _itemsEyList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadItemsListMt()
        {
            try
            {
                _itemsMtList = GlobalSetting.ItemMtService.GetItems();
                xgrdItemsMt.DataSource = _itemsMtList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadItemsListHw()
        {
            try
            {
                _itemsHwList = GlobalSetting.ItemHwService.GetItems();
                xgrdItemsHw.DataSource = _itemsHwList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadItemGridBom(ItemEy item)
        {
            try
            {
                Classes.ItemEyBom itemEyBom = new Classes.ItemEyBom();
                itemEyBom.ItemEy = item;
                itemEyBom.RawMaterials = new List<Classes.Bom>();
                itemEyBom.Hardware = new List<Classes.Bom>();

                _itemBom.Clear();
                _itemBom.Add(itemEyBom);
                xgrdItemBom.DataSource = _itemBom;

                dockPanelGrdBom.Select();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region BOM

        private void AddRawMaterial(ItemMt itemMt)
        {
            try
            {
                Classes.ItemEyBom itemEyBom = _itemBom.FirstOrDefault();

                var rawMaterial = itemEyBom.RawMaterials.Where(a => a.IdItemBcn.Equals(itemMt.IdItemBcn));

                if(rawMaterial == null || rawMaterial.Count() == 0)
                {
                    Classes.Bom bom = new Classes.Bom();
                    bom.IdItemBcn = itemMt.IdItemBcn;
                    bom.Description = itemMt.ItemDescription;
                    bom.IdItemGroup = Constants.ITEM_GROUP_MT;
                    bom.Quantity = 0;
                    bom.Waste = 0;
                    itemEyBom.RawMaterials.Add(bom);
                    grdBomRefreshAndExpand();
                }
                else
                {
                    XtraMessageBox.Show("Raw Material already exist");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AddHardware(ItemHw itemHw)
        {
            try
            {
                Classes.ItemEyBom itemEyBom = _itemBom.FirstOrDefault();

                var hardware = itemEyBom.Hardware.Where(a => a.IdItemBcn.Equals(itemHw.IdItemBcn));

                if (hardware == null || hardware.Count() == 0)
                {
                    Classes.Bom bom = new Classes.Bom();
                    bom.IdItemBcn = itemHw.IdItemBcn;
                    bom.Description = itemHw.ItemDescription;
                    bom.IdItemGroup = Constants.ITEM_GROUP_MT;
                    bom.Quantity = 0;
                    bom.Waste = 0;
                    itemEyBom.Hardware.Add(bom);
                    grdBomRefreshAndExpand();
                }
                else
                {
                    XtraMessageBox.Show("Raw Material already exist");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region Grids Aux

        private void grdBomRefreshAndExpand()
        {
            xgrdItemBom.RefreshDataSource();
            ExpandAllRows(gridViewItemBom);
        }

        void ExpandAllRows(GridView view)
        {
            view.BeginUpdate();
            try
            {
                int dataRowCount = view.DataRowCount;
                for (int rHandle = 0; rHandle < dataRowCount; rHandle++)
                    view.SetMasterRowExpanded(rHandle, true);
            }
            finally
            {
                view.EndUpdate();
            }
        }

        #endregion

        #endregion
    }
}
