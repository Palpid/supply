using DevExpress.XtraEditors;
using HKSupply.General;
using HKSupply.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.XtraBars;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using HKSupply.Helpers;

namespace HKSupply.Forms.Master
{
    public partial class BomManagement : RibbonFormBase
    {
        #region Private Members
        List<ItemEy> _itemsEyList;
        List<ItemMt> _itemsMtList;
        List<ItemHw> _itemsHwList;

        List<ItemBom> _itemBom = new List<ItemBom>();
        ItemBom _itemBomOriginal;

        List<Models.Layout> _formLayouts;

        float totalQuantityMt;
        float totalQuantityHw;
        float totalWastageMt;
        float totalWastageHw;
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
                SetUpGrdPlainBom();
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
                //Print and export buttons
                EnablePrintPreview = false;
                EnableExportExcel = true;
                EnableExportCsv = true;
                ConfigurePrintExportOptions();
                //Layout
                EnableLayoutOptions = true;
                ConfigureLayoutOptions();

                LoadFormLayouts();
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
                ActionsAfterCU();
                //dockPanelItemsEy.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
                //dockPanelItemsEy.ShowSliding();

                //xgrdItemsEy.Enabled = true;

                //gridViewItemsMt.DoubleClick -= GridViewItemsMt_DoubleClick;
                //gridViewItemsHw.DoubleClick -= GridViewItemsHw_DoubleClick;

                //SetGrdBomDetailsNonEdit();
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public override void bbiEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            base.bbiEdit_ItemClick(sender, e);

            try
            {

                if (_itemBom == null || _itemBom.Count() == 0)
                {
                    MessageBox.Show(GlobalSetting.ResManager.GetString("NoDataSelected"));
                    RestoreInitState();
                }
                else
                {
                    ConfigureRibbonActionsEditing();
                }
            }
            catch(Exception ex)
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

                if (IsValidBom() == false)
                    return;

                DialogResult result = MessageBox.Show(GlobalSetting.ResManager.GetString("SaveChanges"), "", MessageBoxButtons.YesNo);

                if (result != DialogResult.Yes)
                    return;

                Cursor = Cursors.WaitCursor;

                ItemBom itemBom = _itemBom.FirstOrDefault();

                if (itemBom.Equals(_itemBomOriginal))
                {
                    MessageBox.Show(GlobalSetting.ResManager.GetString("NoPendingChanges"));
                    return;
                }

                res = EditBom(itemBom);

                if (res == true)
                {
                    MessageBox.Show(GlobalSetting.ResManager.GetString("SaveSuccessfully"));
                    ActionsAfterCU();
                }


            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        public override void BbiSaveLayout_ItemClick(object sender, ItemClickEventArgs e)
        {
            base.BbiSaveLayout_ItemClick(sender, e);

            try
            {
                var result = XtraInputBox.Show("Enter Layout Name", "Save Layout", string.Empty);
                if(string.IsNullOrEmpty(result) == false)
                {
                    SaveLayout(result);
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public override void LayoutButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            base.LayoutButton_ItemClick(sender, e);

            try
            {
                RestoreLayaout();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Form Events

        private void BomManagement_Load(object sender, EventArgs e)
        {
            try
            {
                dockPanelGrdBom.Select();

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

                //Sólo agregamos si el usuario hace doble click en una fila con datos, ya que si se pulsa en el header o en un grupo el FocusedRowHandle devuelve la primera fila con datos
                GridHitInfo hitInfo = view.CalcHitInfo((e as MouseEventArgs).Location);
                if (hitInfo.InRowCell)
                {
                    ItemEy item = view.GetRow(view.FocusedRowHandle) as ItemEy;
                    if (item != null)
                    {
                        LoadItemGridBom(item);
                    }
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

                GridHitInfo hitInfo = view.CalcHitInfo((e as MouseEventArgs).Location);
                if (hitInfo.InRowCell)
                {
                    ItemMt itemMt = view.GetRow(view.FocusedRowHandle) as ItemMt;
                    AddRawMaterial(itemMt);
                    LoadBomTreeView();
                    LoadPlainBom();
                }
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

                GridHitInfo hitInfo = view.CalcHitInfo((e as MouseEventArgs).Location);
                if (hitInfo.InRowCell)
                {
                    ItemHw itemHw = view.GetRow(view.FocusedRowHandle) as ItemHw;
                    AddHardware(itemHw);
                    LoadBomTreeView();
                    LoadPlainBom();
                }
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
                    case nameof(ItemBom.Materials):

                        //Ocultamos las columnas que no nos interesan
                        (e.View as GridView).Columns[nameof(DetailBomMt.IdBom)].Visible = false;
                        (e.View as GridView).Columns[nameof(DetailBomMt.Item)].Visible = false;

                        //Seteamos el tamaño de las columnas
                        (e.View as GridView).Columns[nameof(DetailBomMt.IdItemBcn)].Width = 150;

                        //agregamos la columna de descripcion y de unidades
                        GridColumn colDescriptionMt = new GridColumn()
                        {
                            Caption = GlobalSetting.ResManager.GetString("ItemDescription"),
                            Visible = true,
                            FieldName = $"{nameof(DetailBomMt.Item)}.{nameof(ItemMt.ItemDescription)}",
                            Width = 300
                        };

                        GridColumn colUnitMt = new GridColumn()
                        {
                            Caption = GlobalSetting.ResManager.GetString("Unit"),
                            Visible = true,
                            FieldName = $"{nameof(DetailBomMt.Item)}.{nameof(ItemMt.Unit)}",
                            Width = 70
                        };

                        (e.View as GridView).Columns.Add(colDescriptionMt);
                        (e.View as GridView).Columns.Add(colUnitMt);

                        //Formatos
                        (e.View as GridView).Columns[nameof(DetailBomMt.Quantity)].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        (e.View as GridView).Columns[nameof(DetailBomMt.Quantity)].DisplayFormat.FormatString = "n2";
                        (e.View as GridView).Columns[nameof(DetailBomMt.Waste)].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        (e.View as GridView).Columns[nameof(DetailBomMt.Waste)].DisplayFormat.FormatString = "n2";

                        //Orden de las columnas
                        (e.View as GridView).Columns[nameof(DetailBomMt.IdItemBcn)].VisibleIndex = 0;
                        (e.View as GridView).Columns[$"{nameof(DetailBomMt.Item)}.{nameof(ItemMt.ItemDescription)}"].VisibleIndex = 1;
                        (e.View as GridView).Columns[$"{nameof(DetailBomMt.Item)}.{nameof(ItemMt.Unit)}"].VisibleIndex = 2;
                        (e.View as GridView).Columns[nameof(DetailBomMt.Quantity)].VisibleIndex = 3;
                        (e.View as GridView).Columns[nameof(DetailBomMt.Waste)].VisibleIndex = 4;

                        //Events
                        (e.View as GridView).CellValueChanged += grdBomView_CellValueChanged;

                        //Agregamos los Summary
                        (e.View as GridView).OptionsView.ShowFooter = true;
                        (e.View as GridView).Columns[nameof(DetailBomMt.Quantity)].Summary.Add(SummaryItemType.Sum, nameof(DetailBomMt.Quantity), "{0:n}");
                        (e.View as GridView).Columns[nameof(DetailBomMt.Waste)].Summary.Add(SummaryItemType.Sum, nameof(DetailBomMt.Waste), "{0:n}");

                        //Si está en edición al pintar una nueva vista tiene que hacerla editable
                        if (CurrentState == ActionsStates.Edit)
                            SetGrdBomEditColumns();
                        else
                            SetGrdBomDetailsNonEdit();

                        break;

                    case nameof(ItemBom.Hardwares):

                        //Ocultamos las columnas que no nos interesan
                        (e.View as GridView).Columns[nameof(DetailBomHw.IdBom)].Visible = false;
                        (e.View as GridView).Columns[nameof(DetailBomHw.Item)].Visible = false;

                        //Seteamos el tamaño de las columnas
                        (e.View as GridView).Columns[nameof(DetailBomHw.IdItemBcn)].Width = 150;

                        //agregamos la columna de descripcion
                        GridColumn colDescriptionHw = new GridColumn()
                        {
                            Caption = GlobalSetting.ResManager.GetString("ItemDescription"),
                            Visible = true,
                            FieldName = $"{nameof(DetailBomHw.Item)}.{nameof(ItemHw.ItemDescription)}",
                            Width = 300
                        };

                        GridColumn colUnitHw = new GridColumn()
                        {
                            Caption = GlobalSetting.ResManager.GetString("unit"),
                            Visible = true,
                            FieldName = $"{nameof(DetailBomHw.Item)}.{nameof(ItemHw.Unit)}",
                            Width = 70
                        };

                        (e.View as GridView).Columns.Add(colDescriptionHw);
                        (e.View as GridView).Columns.Add(colUnitHw);

                        //Formatos
                        (e.View as GridView).Columns[nameof(DetailBomHw.Quantity)].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        (e.View as GridView).Columns[nameof(DetailBomHw.Quantity)].DisplayFormat.FormatString = "n2";
                        (e.View as GridView).Columns[nameof(DetailBomHw.Waste)].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        (e.View as GridView).Columns[nameof(DetailBomHw.Waste)].DisplayFormat.FormatString = "n2";

                        //Orden de las columnas
                        (e.View as GridView).Columns[nameof(DetailBomHw.IdItemBcn)].VisibleIndex = 0;
                        (e.View as GridView).Columns[$"{nameof(DetailBomHw.Item)}.{nameof(ItemHw.ItemDescription)}"].VisibleIndex = 1;
                        (e.View as GridView).Columns[$"{nameof(DetailBomHw.Item)}.{nameof(ItemHw.Unit)}"].VisibleIndex = 2;
                        (e.View as GridView).Columns[nameof(DetailBomHw.Quantity)].VisibleIndex = 3;
                        (e.View as GridView).Columns[nameof(DetailBomHw.Waste)].VisibleIndex = 4;

                        //Events
                        (e.View as GridView).CellValueChanged += grdBomView_CellValueChanged;

                        //Agregamos los Summary
                        (e.View as GridView).OptionsView.ShowFooter = true;
                        (e.View as GridView).Columns[nameof(DetailBomHw.Quantity)].Summary.Add(SummaryItemType.Sum, nameof(DetailBomHw.Quantity), "{0:n}");
                        (e.View as GridView).Columns[nameof(DetailBomHw.Waste)].Summary.Add(SummaryItemType.Sum, nameof(DetailBomHw.Waste), "{0:n}");

                        //Si está en edición al pintar una nueva vista tiene que hacerla editable
                        if (CurrentState == ActionsStates.Edit)
                            SetGrdBomEditColumns();
                        else
                            SetGrdBomDetailsNonEdit();

                        break;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void grdBomView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                view.UpdateSummary();
                gridViewPlainBom.UpdateSummary();

                LoadBomTreeView();
                LoadPlainBom();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GridViewPlainBom_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            try
            {
                // Get the summary ID. 
                int summaryID = Convert.ToInt32((e.Item as GridSummaryItem).Tag);
                GridView View = sender as GridView;

                // Initialization 
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    totalQuantityMt = 0;
                    totalQuantityHw = 0;
                    totalWastageMt = 0;
                    totalWastageHw = 0;
                }

                // Calculation 
                if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    string itemGroup = (string)View.GetRowCellValue(e.RowHandle, nameof(Classes.PlainBomAux.ItemGroup));

                    switch (summaryID)
                    {
                        case 1: //The total summary MT quantity. 
                            if (itemGroup == Constants.ITEM_GROUP_MT)
                                totalQuantityMt += Convert.ToSingle(e.FieldValue);
                            break;
                        case 2: //// The total summary HW quantity
                            if (itemGroup == Constants.ITEM_GROUP_HW)
                                totalQuantityHw += Convert.ToSingle(e.FieldValue); ;
                            break;
                        case 3: //The total summary MT Wastage. 
                            if (itemGroup == Constants.ITEM_GROUP_MT)
                                totalWastageMt += Convert.ToSingle(e.FieldValue); ;
                            break;
                        case 4: //The total summary HW Wastage. 
                            if (itemGroup == Constants.ITEM_GROUP_HW)
                                totalWastageHw += Convert.ToSingle(e.FieldValue); ;
                            break;
                    }
                }

                // Finalization 
                if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    switch (summaryID)
                    {
                        case 1:
                            e.TotalValue = totalQuantityMt;
                            break;
                        case 2:
                            e.TotalValue = totalQuantityHw;
                            break;
                        case 3:
                            e.TotalValue = totalWastageMt;
                            break;
                        case 4:
                            e.TotalValue = totalWastageHw;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Private Methods

        #region SetUp Form Objects

        private void SetUpDockPanels()
        {
            try
            {
                dockPanelItemsEy.Options.ShowCloseButton = false;
                dockPanelItemsHw.Options.ShowCloseButton = false;
                dockPanelItemsMt.Options.ShowCloseButton = false;
                dockPanelGrdBom.Options.ShowCloseButton = false;
                dockPanelTreeBom.Options.ShowCloseButton = false;
                dockPanelPlainBom.Options.ShowCloseButton = false;
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
                //gridViewItemsMt.DoubleClick += GridViewItemsMt_DoubleClick;

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
                //gridViewItemsHw.DoubleClick += GridViewItemsHw_DoubleClick;
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
                GridColumn colIdItemBcn = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("ItemBCN"), Visible = true, FieldName = nameof(ItemBom.IdItemBcn), Width = 200 };

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

        private void SetUpGrdPlainBom()
        {
            try
            {
                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                gridViewPlainBom.OptionsView.ColumnAutoWidth = false;
                gridViewPlainBom.HorzScrollVisibility = ScrollVisibility.Auto;

                //Hacer todo el grid no editable
                gridViewPlainBom.OptionsBehavior.Editable = false;

                //Columns definition
                GridColumn colIdItemGroup = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("ItemGroup"), Visible = true, FieldName = nameof(Classes.PlainBomAux.ItemGroup), Width = 90 };
                GridColumn colIdItemBcn = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("ItemBCN"), Visible = true, FieldName = nameof(Classes.PlainBomAux.IdItemBcn), Width = 200 };
                GridColumn colDescription = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("ItemBCN"), Visible = true, FieldName = nameof(Classes.PlainBomAux.ItemDescription), Width = 450 };
                GridColumn colQuantity = new GridColumn() { Caption = "Quantity", Visible = true, FieldName = nameof(Classes.PlainBomAux.Quantity), Width = 60 };
                GridColumn colWaste = new GridColumn() { Caption = "Wastage", Visible = true, FieldName = nameof(Classes.PlainBomAux.Waste), Width = 60 };
                GridColumn colUnit = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Unit"), Visible = true, FieldName = nameof(Classes.PlainBomAux.Waste), Width = 70 };

                //Format types
                colQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                colQuantity.DisplayFormat.FormatString = "n2";
                colWaste.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                colWaste.DisplayFormat.FormatString = "n2";

                //Show footer for summaries
                gridViewPlainBom.OptionsView.ShowFooter = true; 

                //Summaries
                GridColumnSummaryItem sumMtQuantity = new GridColumnSummaryItem()
                {
                    SummaryType = SummaryItemType.Custom,
                    FieldName = nameof(Classes.PlainBomAux.Quantity),
                    DisplayFormat = "MT: {0}",
                    Tag = 1
                };

                GridColumnSummaryItem sumHwQuantity = new GridColumnSummaryItem()
                {
                    SummaryType = SummaryItemType.Custom,
                    FieldName = nameof(Classes.PlainBomAux.Quantity),
                    DisplayFormat = "HW: {0}",
                    Tag = 2
                };

                GridColumnSummaryItem sumMtWastage = new GridColumnSummaryItem()
                {
                    SummaryType = SummaryItemType.Custom,
                    FieldName = nameof(Classes.PlainBomAux.Waste),
                    DisplayFormat = "MT: {0}",
                    Tag = 3
                };

                GridColumnSummaryItem sumHwWastage = new GridColumnSummaryItem()
                {
                    SummaryType = SummaryItemType.Custom,
                    FieldName = nameof(Classes.PlainBomAux.Waste),
                    DisplayFormat = "HW: {0}",
                    Tag = 4
                };

                //Add  summaries to columns
                colQuantity.Summary.Add(sumMtQuantity);
                colQuantity.Summary.Add(sumHwQuantity);
                colWaste.Summary.Add(sumMtWastage);
                colWaste.Summary.Add(sumHwWastage);

                //Add columns to grid root view
                gridViewPlainBom.Columns.Add(colIdItemGroup);
                gridViewPlainBom.Columns.Add(colIdItemBcn);
                gridViewPlainBom.Columns.Add(colDescription);
                gridViewPlainBom.Columns.Add(colQuantity);
                gridViewPlainBom.Columns.Add(colWaste);

                //events
                gridViewPlainBom.CustomSummaryCalculate += GridViewPlainBom_CustomSummaryCalculate;

                xgrdItemBom.ProcessGridKey += XgrdItemBom_ProcessGridKey;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void XgrdItemBom_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                if (CurrentState != ActionsStates.Edit)
                    return;

                if (e.KeyCode == Keys.Delete)
                {
                    DialogResult result = XtraMessageBox.Show("Delete row?", "Confirmation", MessageBoxButtons.YesNo);
                    if (result != DialogResult.Yes)
                        return;

                    GridView activeView = xgrdItemBom.FocusedView as GridView;
                    switch (activeView.LevelName)
                    {
                        case nameof(ItemBom.Materials):
                        case nameof(ItemBom.Hardwares):
                            var bomRow = activeView.GetRow(activeView.FocusedRowHandle);

                            if (bomRow.GetType() == typeof(DetailBomMt))
                                DeleteRawMaterial(bomRow as DetailBomMt);
                            else if (bomRow.GetType() == typeof(DetailBomHw))
                                DeleteHardware(bomRow as DetailBomHw);

                            LoadBomTreeView();
                            LoadPlainBom();
                            break;

                    }
                }
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Loads

        private void LoadFormLayouts()
        {
            try
            {
                _formLayouts = LayoutHelper.GetFormLayouts(Name);
                AddRestoreLayoutItems(_formLayouts);
            }
            catch
            {
                throw;
            }
        }

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
                ItemBom itemBom;

                itemBom = GlobalSetting.ItemBomService.GetItemBom(item.IdItemBcn);
                _itemBomOriginal = GlobalSetting.ItemBomService.GetItemBom(item.IdItemBcn);
                //_itemBomOriginal = itemBom.Clone(); //TODO: La extensión para clonar no me funciona con esta clase. Investigar! (no puede serializarlo?)

                if (itemBom == null)
                {
                    itemBom = new ItemBom();
                    itemBom.IdBom = 0;
                    itemBom.IdItemBcn = item.IdItemBcn;
                    itemBom.Item = item;
                    itemBom.IdItemGroup = Constants.ITEM_GROUP_EY;
                    itemBom.Materials = new List<DetailBomMt>();
                    itemBom.Hardwares = new List<DetailBomHw>();

                    _itemBomOriginal = new ItemBom();
                    _itemBomOriginal.IdBom = 0;
                    _itemBomOriginal.IdItemBcn = item.IdItemBcn;
                    _itemBomOriginal.Item = item;
                    _itemBomOriginal.IdItemGroup = Constants.ITEM_GROUP_EY;
                    _itemBomOriginal.Materials = new List<DetailBomMt>();
                    _itemBomOriginal.Hardwares = new List<DetailBomHw>();
                }

                _itemBom.Clear();
                _itemBom.Add(itemBom);
                xgrdItemBom.DataSource = null;
                xgrdItemBom.DataSource = _itemBom;

                grdBomRefreshAndExpand();
                dockPanelGrdBom.Select();

                LoadBomTreeView();
                LoadPlainBom();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadPlainBom()
        {
            try
            {
                xgrdPlainBom.DataSource = null;
                List<Classes.PlainBomAux> plainBom = new List<Classes.PlainBomAux>();
                ItemBom bom = _itemBom.FirstOrDefault();

                foreach (DetailBomMt rm in bom.Materials)
                    plainBom.Add(rm);

                foreach(DetailBomHw h in bom.Hardwares)
                    plainBom.Add(h);

                xgrdPlainBom.DataSource = plainBom;

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Grid BOM

        private void AddRawMaterial(ItemMt itemMt)
        {
            try
            {
                ItemBom itemBom = _itemBom.FirstOrDefault();

                var rawMaterial = itemBom.Materials.Where(a => a.IdItemBcn.Equals(itemMt.IdItemBcn));

                if(rawMaterial == null || rawMaterial.Count() == 0)
                {
                    DetailBomMt detail = new DetailBomMt()
                    {
                        IdBom = itemBom.IdBom,
                        IdItemBcn = itemMt.IdItemBcn,
                        Item = itemMt,
                        Quantity = 0,
                        Waste = 0
                    };

                    itemBom.Materials.Add(detail);
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
                ItemBom itemBom = _itemBom.FirstOrDefault();

                var hardware = itemBom.Hardwares.Where(a => a.IdItemBcn.Equals(itemHw.IdItemBcn));

                if (hardware == null || hardware.Count() == 0)
                {
                    DetailBomHw detail = new DetailBomHw()
                    {
                        IdBom = itemBom.IdBom,
                        IdItemBcn = itemHw.IdItemBcn,
                        Item = itemHw,
                        Quantity = 0,
                        Waste = 0
                    };
                    itemBom.Hardwares.Add(detail);
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

        private void DeleteRawMaterial(DetailBomMt bomRow)
        {
            try
            {
                ItemBom itemBom = _itemBom.FirstOrDefault();
                var material = itemBom.Materials.Where(a => a.IdItemBcn.Equals(bomRow.IdItemBcn)).FirstOrDefault();
                if (material != null)
                {
                    itemBom.Materials.Remove(material);
                    grdBomRefreshAndExpand();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void DeleteHardware(DetailBomHw bomRow)
        {
            try
            {
                ItemBom itemBom = _itemBom.FirstOrDefault();
                var hardware = itemBom.Hardwares.Where(a => a.IdItemBcn.Equals(bomRow.IdItemBcn)).FirstOrDefault();
                if (hardware != null)
                {
                    itemBom.Hardwares.Remove(hardware);
                    grdBomRefreshAndExpand();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void SetGrdBomEditColumns()
        {
            try
            {
                //Edit repositories
                RepositoryItemTextEdit ritxt2Dec = new RepositoryItemTextEdit();
                ritxt2Dec.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                ritxt2Dec.Mask.EditMask = "F2";

                foreach (GridView view in xgrdItemBom.ViewCollection)
                {
                    switch (view.LevelName)
                    {
                        case nameof(ItemBom.Materials):

                            view.OptionsBehavior.Editable = true;

                            //Edit Columns
                            view.Columns[nameof(DetailBomMt.Quantity)].OptionsColumn.AllowEdit = true;
                            view.Columns[nameof(DetailBomMt.Waste)].OptionsColumn.AllowEdit = true;

                            //No edit columns
                            view.Columns[nameof(DetailBomMt.IdItemBcn)].OptionsColumn.AllowEdit = false;
                            view.Columns[$"{nameof(DetailBomMt.Item)}.{nameof(ItemMt.ItemDescription)}"].OptionsColumn.AllowEdit = false;
                            view.Columns[$"{nameof(DetailBomMt.Item)}.{nameof(ItemMt.Unit)}"].OptionsColumn.AllowEdit = false;

                            //Edit repositories
                            view.Columns[nameof(DetailBomMt.Quantity)].ColumnEdit = ritxt2Dec;
                            view.Columns[nameof(DetailBomMt.Waste)].ColumnEdit = ritxt2Dec;

                            break;

                        case nameof(ItemBom.Hardwares):

                            view.OptionsBehavior.Editable = true;

                            //Edit Columns
                            view.Columns[nameof(DetailBomHw.Quantity)].OptionsColumn.AllowEdit = true;
                            view.Columns[nameof(DetailBomHw.Waste)].OptionsColumn.AllowEdit = true;

                            //No edit columns
                            view.Columns[nameof(DetailBomHw.IdItemBcn)].OptionsColumn.AllowEdit = false;
                            view.Columns[$"{nameof(DetailBomHw.Item)}.{nameof(ItemHw.ItemDescription)}"].OptionsColumn.AllowEdit = false;
                            view.Columns[$"{nameof(DetailBomHw.Item)}.{nameof(ItemHw.Unit)}"].OptionsColumn.AllowEdit = false;

                            //Edit repositories
                            view.Columns[nameof(DetailBomMt.Quantity)].ColumnEdit = ritxt2Dec;
                            view.Columns[nameof(DetailBomMt.Waste)].ColumnEdit = ritxt2Dec;

                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetGrdBomDetailsNonEdit()
        {
            try
            {
                foreach (GridView view in xgrdItemBom.ViewCollection)
                {
                    view.OptionsBehavior.Editable = false;
                }
            }
            catch(Exception ex)
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

        #region Tree Bom
        private void LoadBomTreeView()
        {
            try
            {
                treeViewBom.Nodes.Clear();
                treeViewBom.Nodes.Add(GetComponentNode(_itemBom.FirstOrDefault())); 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private TreeNode GetComponentNode(ItemBom item)
        {
            try
            {
                int contRawMaterialsNode = 0;
                int contHardwareNode = 0;

                TreeNode root = new TreeNode(item.IdItemBcn);
                root.Name = item.IdItemBcn; //El Name es el key de un treeNode

                TreeNode rawMatPrimasRoot = new TreeNode("Raw Materials");
                TreeNode HardwareRoot = new TreeNode("Hardware");

                root.Nodes.Add(rawMatPrimasRoot);
                root.Nodes.Add(HardwareRoot);
                root.Expand();

                if (item.Materials != null)
                {
                    foreach (DetailBomMt rawMaterial in item.Materials)
                    {
                        root.Nodes[0].Nodes.Add(rawMaterial.IdItemBcn, $"{rawMaterial.IdItemBcn} : {rawMaterial.Item.ItemDescription}");
                        root.Nodes[0].Nodes[contRawMaterialsNode].Tag = "RawMaterials";
                        root.Nodes[0].Nodes[contRawMaterialsNode].Nodes.Add(
                            new TreeNode($"Quantity : {rawMaterial.Quantity.ToString()}   Wastage : {rawMaterial.Waste.ToString()}")
                            );
                        contRawMaterialsNode++;
                    }
                    root.Nodes[0].Expand();
                }

                if (item.Hardwares != null)
                {
                    foreach (DetailBomHw hardware in item.Hardwares)
                    {
                        root.Nodes[1].Nodes.Add(hardware.IdItemBcn, $"{hardware.IdItemBcn} : {hardware.Item.ItemDescription}");
                        root.Nodes[1].Nodes[contHardwareNode].Tag = "Hardware";
                        root.Nodes[1].Nodes[contHardwareNode].Nodes.Add(
                            new TreeNode($"Quantity : {hardware.Quantity.ToString()}   Wastage : {hardware.Waste.ToString()}")
                            );
                        contHardwareNode++;
                    }
                    root.Nodes[1].Expand();
                }

                return root;
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        #endregion

        #region Configure Ribbon Actions

        private void ConfigureRibbonActionsEditing()
        {
            try
            {
                SetGrdBomEditColumns();

                xgrdItemsEy.Enabled = false;

                dockPanelItemsEy.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide;
                dockPanelItemsEy.HideSliding();

                //Suscribirse a los eventos de los grid para agregar al bom con doble click
                gridViewItemsMt.DoubleClick += GridViewItemsMt_DoubleClick;
                gridViewItemsHw.DoubleClick += GridViewItemsHw_DoubleClick;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Save Layout

        private void SaveLayout(string name)
        {
            try
            {
                List<Models.Layout> layouts = new List<Models.Layout>();

                string base64stringLayoutDockManager = LayoutHelper.SaveLayoutAsBase64String(dockManagerItemBom);
                string base64stringLayoutDocumentManager = LayoutHelper.SaveLayoutAsBase64String(documentManagerBom.View);

                int funcId = GlobalSetting.FunctionalitiesRoles.Where(fr => fr.Functionality.FormName.Equals(Name)).Select(a => a.FunctionalityId).FirstOrDefault();

                Layout tmpLayout = new Models.Layout()
                {
                    FunctionalityId = funcId,
                    UserLogin = GlobalSetting.LoggedUser.UserLogin,
                    ObjectName = nameof(dockManagerItemBom),
                    LayoutString = base64stringLayoutDockManager,
                    LayoutName = name
                };
                layouts.Add(tmpLayout);

                tmpLayout = new Models.Layout()
                {
                    FunctionalityId = funcId,
                    UserLogin = GlobalSetting.LoggedUser.UserLogin,
                    ObjectName = nameof(documentManagerBom),
                    LayoutString = base64stringLayoutDocumentManager,
                    LayoutName = name
                };
                layouts.Add(tmpLayout);

                GlobalSetting.LayoutService.SaveLayout(layouts);

                LoadFormLayouts();

            }
            catch
            {
                throw;
            }
        }

        private void RestoreLayaout()
        {
            try
            {
                LayoutHelper.RestoreLayoutFromBase64String(dockManagerItemBom,
                    LayoutHelper.GetObjectLayout(
                        _formLayouts, 
                        CurrentLayout, 
                        nameof(dockManagerItemBom)));

                LayoutHelper.RestoreLayoutFromBase64String(documentManagerBom.View,
                     LayoutHelper.GetObjectLayout(
                         _formLayouts, 
                         CurrentLayout, 
                         nameof(documentManagerBom)));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region CRUD
        private bool IsValidBom()
        {
            try
            {
                ItemBom bom = _itemBom.FirstOrDefault();

                foreach(var h in bom.Hardwares)
                {
                    if(h.Quantity <= 0)
                    {
                        XtraMessageBox.Show($"Quantity must be greater than Zero ({h.IdItemBcn})", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }

                foreach (var m in bom.Materials)
                {
                    if (m.Quantity <= 0)
                    {
                        XtraMessageBox.Show($"Quantity must be greater than Zero ({m.IdItemBcn})", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private bool EditBom(ItemBom itemBom)
        {
            try
            {
                return GlobalSetting.ItemBomService.EditIteBom(itemBom);
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
                ItemEy item = _itemBomOriginal.Item as ItemEy;
                _itemBomOriginal = null;
                _itemBom.Clear();
                LoadItemGridBom(item);

                //Restore de ribbon to initial states
                RestoreInitState();


                dockPanelItemsEy.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
                dockPanelItemsEy.ShowSliding();

                xgrdItemsEy.Enabled = true;

                gridViewItemsMt.DoubleClick -= GridViewItemsMt_DoubleClick;
                gridViewItemsHw.DoubleClick -= GridViewItemsHw_DoubleClick;

                SetGrdBomDetailsNonEdit();
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
