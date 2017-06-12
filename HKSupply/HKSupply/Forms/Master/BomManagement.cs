﻿using DevExpress.XtraEditors;
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
using System.Data.Entity;
using System.ComponentModel;
using DevExpress.XtraEditors.Controls;

namespace HKSupply.Forms.Master
{
    public partial class BomManagement : RibbonFormBase
    {

        #region Constants
        private const string EDIT_COLUMN = "EditHf";
        #endregion


        #region Private Members
        List<ItemEy> _itemsEyList;
        List<ItemHf> _itemsHfList;
        List<ItemHf> _itemsHfDetailBomList;
        List<ItemMt> _itemsMtList;
        List<ItemHw> _itemsHwList;

        object _currentItem;
        List<ItemBom> _itemBomList = new List<ItemBom>();
        //BindingList<ItemBom> _itemBomList = new BindingList<ItemBom>();
        ItemBom _itemBomOriginal;

        List<Supplier> _suppliersList;

        List<Models.Layout> _formLayouts;

        float totalQuantityMt;
        float totalQuantityHw;
        float totalScrapMt;
        float totalScrapHw;
        #endregion

        #region Constructor
        public BomManagement()
        {
            InitializeComponent();

            try
            {
                ConfigureRibbonActions();
                LoadAuxList();
                SetBasicEvents();
                SetUpDockPanels();
                SetUpSearchLueSupplier();
                SetUpGrdItemsEy();
                SetUpGrdItemsHf();
                //SetUpGrdItemsMt();
                //SetUpGrdItemsHw();
                //SetUpGrdItemsHfDetail();
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

                if (_itemBomList == null || _itemBomList.Count() == 0)
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

                //TEST.INI!!
                //ShowHalfFinishedMessageInfo();
                //GlobalSetting.ItemBomService.EditItemSuppliersBom(_itemBomList);
                //TEST.FIN!!

                bool res = false;

                if (IsValidBom() == false)
                    return;

                DialogResult result = MessageBox.Show(GlobalSetting.ResManager.GetString("SaveChanges"), "", MessageBoxButtons.YesNo);

                if (result != DialogResult.Yes)
                    return;

                Cursor = Cursors.WaitCursor;

                ////ItemBom itemBom = _itemBomList.FirstOrDefault();

                ////if (itemBom.Equals(_itemBomOriginal))
                ////{
                ////    MessageBox.Show(GlobalSetting.ResManager.GetString("NoPendingChanges"));
                ////    return;
                ////}

                //res = EditBom(itemBom);

                DeleteLastRowIfEmpty();

                res = EditBom();

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

        public override void bbiExportCsv_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewPlainBom.DataRowCount == 0)
            {
                MessageBox.Show(GlobalSetting.ResManager.GetString("NoDataSelected"));
                return;
            }

            base.bbiExportCsv_ItemClick(sender, e);

            try
            {
                if (string.IsNullOrEmpty(ExportCsvFile) == false)
                {
                    gridViewPlainBom.ExportToCsv(ExportCsvFile);

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
            
            if (gridViewPlainBom.DataRowCount == 0)
            {
                MessageBox.Show(GlobalSetting.ResManager.GetString("NoDataSelected"));
                return;
            }

            base.bbiExportExcel_ItemClick(sender, e);

            try
            {
                if (string.IsNullOrEmpty(ExportExcelFile) == false)
                {

                    gridViewPlainBom.OptionsPrint.PrintFooter = false;
                    gridViewPlainBom.ExportToXlsx(ExportExcelFile);

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

        #region Form Events

        private void BomManagement_Load(object sender, EventArgs e)
        {
            try
            {
                ShowAddBomSupplier(false);

                dockPanelGrdBom.Select();

                LoadItemsListEy();
                LoadItemsListHf();
                LoadItemsListMt();
                LoadItemsListHw();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SbAddBomSupplier_Click(object sender, EventArgs e)
        {
            try
            {
                if (slueSupplier.EditValue != null && _itemBomList != null && _itemBomList.Count() > 0)
                    AddSupplierBom();
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
                        _currentItem = item;
                        LoadItemGridBom(item);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GridViewItemsHf_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                GridView view = sender as GridView;

                //Sólo agregamos si el usuario hace doble click en una fila con datos, ya que si se pulsa en el header o en un grupo el FocusedRowHandle devuelve la primera fila con datos
                GridHitInfo hitInfo = view.CalcHitInfo((e as MouseEventArgs).Location);
                if (hitInfo.InRowCell)
                {
                    ItemHf item = view.GetRow(view.FocusedRowHandle) as ItemHf;
                    if (item != null)
                    {
                        _currentItem = item;
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
                //var selectedSuppliers = OpenSelectSuppliersForm();

                //if (selectedSuppliers.Count == 0)
                //{
                //    XtraMessageBox.Show("No selected Supplier", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}



                //GridView view = sender as GridView;

                //GridHitInfo hitInfo = view.CalcHitInfo((e as MouseEventArgs).Location);
                //if (hitInfo.InRowCell)
                //{
                //    ItemMt itemMt = view.GetRow(view.FocusedRowHandle) as ItemMt;

                //    foreach(var supplier in selectedSuppliers)
                //    {
                //        AddRawMaterial(itemMt, supplier);
                //    }


                //    LoadBomTreeView();
                //    LoadPlainBom();
                //}

                //TEST AKI

                GridView view = sender as GridView;
                GridHitInfo hitInfo = view.CalcHitInfo((e as MouseEventArgs).Location);

                if (hitInfo.InRowCell)
                {
                    ItemMt itemMt = view.GetRow(view.FocusedRowHandle) as ItemMt;

                    GridView activeBomView = xgrdItemBom.FocusedView as GridView;

                    if (activeBomView == null)
                    {
                        XtraMessageBox.Show("Select BOM node first", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    //------------------------------------------------------------------------

                    var rowtmp = activeBomView.GetRow(activeBomView.FocusedRowHandle);

                    object rowParent;

                    if (rowtmp.GetType().BaseType == typeof(ItemBom) || rowtmp.GetType() == typeof(ItemBom) ||
                        rowtmp.GetType().BaseType == typeof(DetailBomHf) || rowtmp.GetType() == typeof(DetailBomHf)
                        )
                    {
                        rowParent = rowtmp;
                    }
                    else
                    {
                        BaseView parent = activeBomView.ParentView;

                        rowParent = parent.GetRow(activeBomView.SourceRowHandle);
                    }


                    if (rowParent.GetType().BaseType == typeof(ItemBom) || rowParent.GetType() == typeof(ItemBom))
                    {
                        var rawMaterial = (rowParent as ItemBom).Materials.Where(a => a.IdItemBcn != null && a.IdItemBcn.Equals(itemMt.IdItemBcn));

                        if (rawMaterial == null || rawMaterial.Count() == 0)
                        {
                            DetailBomMt detail = new DetailBomMt()
                            {
                                IdBom = (rowParent as ItemBom).IdBom,
                                IdItemBcn = itemMt.IdItemBcn,
                                Item = itemMt,
                                Quantity = 0,
                                Scrap = 0,
                            };

                            (rowParent as ItemBom).Materials.Add(detail);

                            //activeBomView.RefreshData(); //--> OK
                            activeBomView.BeginDataUpdate();
                            grdBomRefreshAndExpand();
                            activeBomView.EndDataUpdate();


                            //grdBomRefreshAndExpand();
                            //xgrdItemBom.RefreshDataSource();
                            //NavigateDetails(gridViewItemBom);
                            //gridViewItemBom.ExpandMasterRow(0, nameof(ItemBom.Hardwares)); --> Medio OK
                            //xgrdItemBom.FocusedView = gridViewItemBom.GetDetailView(gridViewItemBom.FocusedRowHandle, 1); --> Medio OK


                        }
                        else
                        {
                            XtraMessageBox.Show($"Raw Material already exist for supplier {(rowParent as ItemBom).IdSupplier}");
                        }
                    }
                    else if (rowParent.GetType().BaseType == typeof(DetailBomHf) || rowParent.GetType() == typeof(DetailBomHf))
                    {
                        //var rawMaterial = (rowParent as ItemBom).Materials.Where(a => a.IdItemBcn.Equals(itemMt.IdItemBcn));
                        var rawMaterial = (rowParent as DetailBomHf).DetailItemBom.Materials.Where(a => a.IdItemBcn != null && a.IdItemBcn.Equals(itemMt.IdItemBcn)); 
                        if (rawMaterial == null || rawMaterial.Count() == 0)
                        {
                            DetailBomMt detail = new DetailBomMt()
                            {
                                IdBom = (rowParent as DetailBomHf).IdBom,
                                IdItemBcn = itemMt.IdItemBcn,
                                Item = itemMt,
                                Quantity = 0,
                                Scrap = 0,
                            };

                            (rowParent as DetailBomHf).DetailItemBom.Materials.Add(detail);

                            //activeBomView.RefreshData(); //--> OK
                            activeBomView.BeginDataUpdate();
                            grdBomRefreshAndExpand();
                            activeBomView.EndDataUpdate();


                            //xgrdItemBom.RefreshDataSource();
                            //ExpandAllRows(activeBomView);

                            //grdBomRefreshAndExpand();
                            //xgrdItemBom.RefreshDataSource();
                            //NavigateDetails(gridViewItemBom);
                            //gridViewItemBom.ExpandMasterRow(0, nameof(ItemBom.Hardwares));
                            //xgrdItemBom.FocusedView = gridViewItemBom.GetDetailView(gridViewItemBom.FocusedRowHandle, 1);


                        }
                        else
                        {
                            XtraMessageBox.Show($"Raw Material already exist for supplier {(rowParent as ItemBom).IdSupplier}");
                        }
                    }



                    //switch (activeBomView.LevelName)
                    //{
                    //    case nameof(ItemBom.Materials):


                        //        DetailBomMt row = activeBomView.GetRow(activeBomView.FocusedRowHandle) as DetailBomMt;
                        //        BaseView parent = activeBomView.ParentView;
                        //        var rowParent = parent.GetRow(activeBomView.SourceRowHandle);

                        //        var rawMaterial = (rowParent as ItemBom).Materials.Where(a => a.IdItemBcn.Equals(itemMt.IdItemBcn));

                        //        if (rawMaterial == null || rawMaterial.Count() == 0)
                        //        {
                        //            DetailBomMt detail = new DetailBomMt()
                        //            {
                        //                IdBom = (rowParent as ItemBom).IdBom,
                        //                IdItemBcn = itemMt.IdItemBcn,
                        //                Item = itemMt,
                        //                Quantity = 0,
                        //                Scrap = 0,
                        //            };

                        //            (rowParent as ItemBom).Materials.Add(detail);

                        //            grdBomRefreshAndExpand();

                        //            //------------------------------ PRUEBAS -----------------------------------//
                        //            //xgrdItemBom.FocusedView = parent;
                        //            gridViewItemBom.FocusedRowHandle = 0;
                        //            GridView childView1 = gridViewItemBom.GetDetailView(0, 1) as GridView;

                        //            childView1.FocusedRowHandle = 0;
                        //            childView1.ExpandMasterRow(0);
                        //            GridView childView2 = childView1.GetDetailView(0, 0) as GridView;


                        //        }
                        //        else
                        //        {
                        //            XtraMessageBox.Show($"Raw Material already exist for supplier {(rowParent as ItemBom).IdSupplier}");
                        //        }

                        //        break;

                        //    default:
                        //        XtraMessageBox.Show("Select BOM node first", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //        break;

                        //}
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
                var selectedSuppliers = OpenSelectSuppliersForm();

                if (selectedSuppliers.Count == 0)
                {
                    XtraMessageBox.Show("No selected Supplier", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                GridView view = sender as GridView;

                GridHitInfo hitInfo = view.CalcHitInfo((e as MouseEventArgs).Location);
                if (hitInfo.InRowCell)
                {
                    ItemHw itemHw = view.GetRow(view.FocusedRowHandle) as ItemHw;

                    foreach (var supplier in selectedSuppliers)
                    {
                        AddHardware(itemHw, supplier);
                    }
                        
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
                    case nameof(DetailBomHf.DetailItemBom) + "." + nameof(ItemBom.Materials): 
                    case nameof(ItemBom.Materials):

                        (e.View as GridView).DetailHeight = 1000;

                        //Ocultamos las columnas que no nos interesan
                        (e.View as GridView).Columns[nameof(DetailBomMt.IdBom)].Visible = false;
                        (e.View as GridView).Columns[nameof(DetailBomMt.Item)].Visible = false;

                        //Seteamos el tamaño de las columnas
                        (e.View as GridView).Columns[nameof(DetailBomMt.IdItemBcn)].Width = 150;
                        (e.View as GridView).Columns[nameof(DetailBomMt.Length)].Width = 60;
                        (e.View as GridView).Columns[nameof(DetailBomMt.Width)].Width = 60;
                        (e.View as GridView).Columns[nameof(DetailBomMt.Height)].Width = 60;
                        (e.View as GridView).Columns[nameof(DetailBomMt.Density)].Width = 60;
                        (e.View as GridView).Columns[nameof(DetailBomMt.NumberOfParts)].Width = 105;
                        (e.View as GridView).Columns[nameof(DetailBomMt.Scrap)].Width = 60;

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
                        (e.View as GridView).Columns[nameof(DetailBomMt.Length)].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        (e.View as GridView).Columns[nameof(DetailBomMt.Length)].DisplayFormat.FormatString = "n2";

                        (e.View as GridView).Columns[nameof(DetailBomMt.Width)].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        (e.View as GridView).Columns[nameof(DetailBomMt.Width)].DisplayFormat.FormatString = "n2";

                        (e.View as GridView).Columns[nameof(DetailBomMt.Height)].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        (e.View as GridView).Columns[nameof(DetailBomMt.Height)].DisplayFormat.FormatString = "n2";

                        (e.View as GridView).Columns[nameof(DetailBomMt.Density)].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        (e.View as GridView).Columns[nameof(DetailBomMt.Density)].DisplayFormat.FormatString = "n2";

                        (e.View as GridView).Columns[nameof(DetailBomMt.NumberOfParts)].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        (e.View as GridView).Columns[nameof(DetailBomMt.NumberOfParts)].DisplayFormat.FormatString = "n0";

                        (e.View as GridView).Columns[nameof(DetailBomMt.Coefficient1)].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        (e.View as GridView).Columns[nameof(DetailBomMt.Coefficient1)].DisplayFormat.FormatString = "n2";
                        (e.View as GridView).Columns[nameof(DetailBomMt.Coefficient2)].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        (e.View as GridView).Columns[nameof(DetailBomMt.Coefficient2)].DisplayFormat.FormatString = "n2";


                        (e.View as GridView).Columns[nameof(DetailBomMt.Scrap)].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        (e.View as GridView).Columns[nameof(DetailBomMt.Scrap)].DisplayFormat.FormatString = "n2";

                        (e.View as GridView).Columns[nameof(DetailBomMt.Quantity)].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        (e.View as GridView).Columns[nameof(DetailBomMt.Quantity)].DisplayFormat.FormatString = "n2";
                        

                        //Orden de las columnas
                        (e.View as GridView).Columns[nameof(DetailBomMt.IdItemBcn)].VisibleIndex = 0;
                        (e.View as GridView).Columns[$"{nameof(DetailBomMt.Item)}.{nameof(ItemMt.ItemDescription)}"].VisibleIndex = 1;
                        (e.View as GridView).Columns[$"{nameof(DetailBomMt.Item)}.{nameof(ItemMt.Unit)}"].VisibleIndex = 2;
                        (e.View as GridView).Columns[nameof(DetailBomMt.Length)].VisibleIndex = 3;
                        (e.View as GridView).Columns[nameof(DetailBomMt.Width)].VisibleIndex = 4;
                        (e.View as GridView).Columns[nameof(DetailBomMt.Height)].VisibleIndex = 5;
                        (e.View as GridView).Columns[nameof(DetailBomMt.Density)].VisibleIndex = 6;
                        (e.View as GridView).Columns[nameof(DetailBomMt.NumberOfParts)].VisibleIndex = 7;
                        (e.View as GridView).Columns[nameof(DetailBomMt.Coefficient1)].VisibleIndex = 8;
                        (e.View as GridView).Columns[nameof(DetailBomMt.Coefficient2)].VisibleIndex = 9;
                        (e.View as GridView).Columns[nameof(DetailBomMt.Scrap)].VisibleIndex = 10;
                        (e.View as GridView).Columns[nameof(DetailBomMt.Quantity)].VisibleIndex = 11;

                        //Events
                        (e.View as GridView).CellValueChanged += grdBomView_CellValueChanged;
                        //Test
                        //(e.View as GridView).ValidateRow += BomManagementMaterials_ValidateRow;
                        (e.View as GridView).ValidatingEditor += BomManagementMaterials_ValidatingEditor;

                       //Agregamos los Summary
                       (e.View as GridView).OptionsView.ShowFooter = true;
                        (e.View as GridView).Columns[nameof(DetailBomMt.Quantity)].Summary.Add(SummaryItemType.Sum, nameof(DetailBomMt.Quantity), "{0:n}");
                        (e.View as GridView).Columns[nameof(DetailBomMt.Scrap)].Summary.Add(SummaryItemType.Sum, nameof(DetailBomMt.Scrap), "{0:n}");

                        //Si está en edición al pintar una nueva vista tiene que hacerla editable
                        if (CurrentState == ActionsStates.Edit)
                            SetGrdBomEditColumns();
                        else
                            SetGrdBomDetailsNonEdit();

                        break;

                    case nameof(DetailBomHf.DetailItemBom) + "." + nameof(ItemBom.Hardwares):
                    case nameof(ItemBom.Hardwares):

                        (e.View as GridView).DetailHeight = 1000;

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
                        (e.View as GridView).Columns[nameof(DetailBomHw.Scrap)].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        (e.View as GridView).Columns[nameof(DetailBomHw.Scrap)].DisplayFormat.FormatString = "n2";

                        //Orden de las columnas
                        (e.View as GridView).Columns[nameof(DetailBomHw.IdItemBcn)].VisibleIndex = 0;
                        (e.View as GridView).Columns[$"{nameof(DetailBomHw.Item)}.{nameof(ItemHw.ItemDescription)}"].VisibleIndex = 1;
                        (e.View as GridView).Columns[$"{nameof(DetailBomHw.Item)}.{nameof(ItemHw.Unit)}"].VisibleIndex = 2;
                        (e.View as GridView).Columns[nameof(DetailBomHw.Quantity)].VisibleIndex = 3;
                        (e.View as GridView).Columns[nameof(DetailBomHw.Scrap)].VisibleIndex = 4;

                        //Events
                        (e.View as GridView).CellValueChanged += grdBomView_CellValueChanged;
                        (e.View as GridView).ValidatingEditor += BomManagementHardwares_ValidatingEditor;

                        //Agregamos los Summary
                        (e.View as GridView).OptionsView.ShowFooter = true;
                        (e.View as GridView).Columns[nameof(DetailBomHw.Quantity)].Summary.Add(SummaryItemType.Sum, nameof(DetailBomHw.Quantity), "{0:n}");
                        (e.View as GridView).Columns[nameof(DetailBomHw.Scrap)].Summary.Add(SummaryItemType.Sum, nameof(DetailBomHw.Scrap), "{0:n}");

                        //Si está en edición al pintar una nueva vista tiene que hacerla editable
                        if (CurrentState == ActionsStates.Edit)
                            SetGrdBomEditColumns();
                        else
                            SetGrdBomDetailsNonEdit();

                        break;

                    case nameof(DetailBomHf.DetailItemBom) + "." + nameof(ItemBom.HalfFinished):
                    case nameof(ItemBom.HalfFinishedNM):

                        (e.View as GridView).DetailHeight = 1000;

                        //Ocultamos todas las columnas menos el item que no nos interesan
                        foreach(GridColumn col in (e.View as GridView).Columns)
                        {
                            if (col.FieldName != nameof(ItemBom.IdItemBcn))
                                col.Visible = false;
                        }

                        //Seteamos el tamaño de las columnas
                        (e.View as GridView).Columns[nameof(ItemBom.IdItemBcn)].Width = 150;

                        //agregamos la columna de descripcion
                        GridColumn colDescriptionItemHf = new GridColumn()
                        {
                            Caption = GlobalSetting.ResManager.GetString("ItemDescription"),
                            Visible = true,
                            FieldName = $"{nameof(ItemBom.Item)}.{nameof(ItemHf.ItemDescription)}",
                            Width = 300
                        };

                        (e.View as GridView).Columns.Add(colDescriptionItemHf);

                        //Si está en edición al pintar una nueva vista tiene que hacerla editable
                        if (CurrentState == ActionsStates.Edit)
                            SetGrdBomEditColumns();
                        else
                            SetGrdBomDetailsNonEdit();

                        //aki
                        ExpandAllRows((e.View as GridView));
                        break;


                    case nameof(ItemBom.HalfFinished):
                        
                        (e.View as GridView).DetailHeight = 1000;


                        //Ocultamos las columnas que no nos interesan
                        foreach (GridColumn col in (e.View as GridView).Columns)
                        {
                            if (col.FieldName != nameof(DetailBomHf.Quantity))
                                col.Visible = false;
                        }

                        //agregamos la columna de descripcion
                        GridColumn colIdItemHf = new GridColumn()
                        {
                            Caption = GlobalSetting.ResManager.GetString("ItemDescription"),
                            Visible = true,
                            FieldName = $"{nameof(DetailBomHf.DetailItemBom)}.{nameof(ItemBom.IdItemBcn)}",
                            Width = 300,
                        };

                        (e.View as GridView).Columns.Add(colIdItemHf);

                        //No queremos que se muestra la columna, pero al ser listas tenemos que generarlo para que se monte el hijo con sus tabs
                        GridColumn ListMaterials = new GridColumn()
                        {
                            Caption = " ",
                            Visible = false,
                            FieldName = $"{nameof(DetailBomHf.DetailItemBom)}.{nameof(ItemBom.Materials)}",
                            Width = 300
                        };
                        (e.View as GridView).Columns.Add(ListMaterials);

                        GridColumn ListHardwares = new GridColumn()
                        {
                            Caption = " ",
                            Visible = false,
                            FieldName = $"{nameof(DetailBomHf.DetailItemBom)}.{nameof(ItemBom.Hardwares)}",
                            Width = 300
                        };
                        (e.View as GridView).Columns.Add(ListHardwares);

                        GridColumn ListHalfFinished = new GridColumn()
                        {
                            Caption = " ",
                            Visible = false,
                            FieldName = $"{nameof(DetailBomHf.DetailItemBom)}.{nameof(ItemBom.HalfFinished)}",
                            Width = 300
                        };
                        (e.View as GridView).Columns.Add(ListHalfFinished);

                        //Columna con el botón para editar el semielaborado
                        GridColumn colEditHfButton = new GridColumn() { Caption = "Edit", Visible = false, FieldName = EDIT_COLUMN, Width = 50 };
                        (e.View as GridView).Columns.Add(colEditHfButton);

                        //Formats
                        (e.View as GridView).Columns[nameof(DetailBomHf.Quantity)].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        (e.View as GridView).Columns[nameof(DetailBomHf.Quantity)].DisplayFormat.FormatString = "n2";

                        //Columns order
                        (e.View as GridView).Columns[$"{nameof(DetailBomHf.DetailItemBom)}.{nameof(ItemBom.IdItemBcn)}"].VisibleIndex = 0;
                        (e.View as GridView).Columns[nameof(DetailBomHf.Quantity)].VisibleIndex = 1;

                        //Events
                        (e.View as GridView).ShownEditor += BomManagementHalfFinished_ShownEditor;
                        (e.View as GridView).ValidatingEditor += BomManagementHalfFinished_ValidatingEditor;

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
                    totalScrapMt = 0;
                    totalScrapHw = 0;
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
                        case 3: //The total summary MT Scrap. 
                            if (itemGroup == Constants.ITEM_GROUP_MT)
                                totalScrapMt += Convert.ToSingle(e.FieldValue); ;
                            break;
                        case 4: //The total summary HW Scrap. 
                            if (itemGroup == Constants.ITEM_GROUP_HW)
                                totalScrapHw += Convert.ToSingle(e.FieldValue); ;
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
                            e.TotalValue = totalScrapMt;
                            break;
                        case 4:
                            e.TotalValue = totalScrapHw;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RepEditHf_Click(object sender, EventArgs e)
        {
            try
            {
                GridView activeView = xgrdItemBom.FocusedView as GridView;

                DetailBomHf row = activeView.GetRow(activeView.FocusedRowHandle) as DetailBomHf;

                if(row.IdBomDetail > 0)
                {
                    OpenEditHfBom(row.DetailItemBom);
                    activeView.RefreshData();
                }
                else
                    XtraMessageBox.Show("Select Half-finished first", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion

        #region Private Methods

        #region SetUp Form Objects

        private void SetBasicEvents()
        {
            try
            {
                sbAddBomSupplier.Click += SbAddBomSupplier_Click;
            }
            catch
            {
                throw;
            }
        }

        private void SetUpDockPanels()
        {
            try
            {
                dockPanelItemsEy.Options.ShowCloseButton = false;
                dockPanelItemsHf.Options.ShowCloseButton = false;
                //dockPanelItemsHw.Options.ShowCloseButton = false;
                //dockPanelItemsMt.Options.ShowCloseButton = false;
                dockPanelGrdBom.Options.ShowCloseButton = false;
                dockPanelTreeBom.Options.ShowCloseButton = false;
                dockPanelPlainBom.Options.ShowCloseButton = false;

                dockPanelItemsHw.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
                dockPanelItemsMt.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
                dockPanelItemsHfDetail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
                panelContainer1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetUpSearchLueSupplier()
        {
            try
            {
                slueSupplier.Properties.DataSource = _suppliersList;
                slueSupplier.Properties.ValueMember = nameof(Supplier.IdSupplier);
                slueSupplier.Properties.DisplayMember = nameof(Supplier.SupplierName);
                slueSupplier.Properties.View.Columns.AddField(nameof(Supplier.IdSupplier)).Visible = true;
                slueSupplier.Properties.View.Columns.AddField(nameof(Supplier.SupplierName)).Visible = true;
            }
            catch
            {
                throw;
            }
        }

        private void SetUpGrdItemsEy()
        {
            try
            {
                //Ocultamos el nombre de las columnas agrupadas
                gridViewItemsEy.GroupFormat = "[#image]{1} {2}";

                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                gridViewItemsEy.OptionsView.ColumnAutoWidth = false;
                gridViewItemsEy.HorzScrollVisibility = ScrollVisibility.Auto;

                //Hacer todo el grid no editable
                gridViewItemsEy.OptionsBehavior.Editable = false;

                //Columns definition
                GridColumn colIdItemBcn = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("ItemBCN"), Visible = true, FieldName = nameof(ItemEy.IdItemBcn), Width = 250 };
                GridColumn colFamilyHK = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("FamilyHk"), Visible = false, FieldName = nameof(ItemEy.IdFamilyHK), Width = 10 };
                GridColumn colModel = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Model"), Visible = true, FieldName = $"{nameof(ItemEy.Model)}.{nameof(Model.Description)}", Width = 10 };
                GridColumn colDefaultSupplier = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Supplier"), Visible = false, FieldName = nameof(ItemEy.IdDefaultSupplier), Width = 10 };

                //Add columns to grid root view
                gridViewItemsEy.Columns.Add(colIdItemBcn);
                gridViewItemsEy.Columns.Add(colFamilyHK);
                gridViewItemsEy.Columns.Add(colModel);
                gridViewItemsEy.Columns.Add(colDefaultSupplier);

                //Grouping
                gridViewItemsEy.OptionsView.ShowGroupPanel = false;

                gridViewItemsEy.Columns[nameof(ItemEy.IdFamilyHK)].GroupIndex = 0;
                gridViewItemsEy.Columns[$"{nameof(ItemEy.Model)}.{nameof(Model.Description)}"].GroupIndex = 1;
                gridViewItemsEy.Columns[nameof(ItemEy.IdDefaultSupplier)].GroupIndex = 2;

                gridViewItemsEy.DoubleClick += GridViewItemsEy_DoubleClick;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetUpGrdItemsHf()
        {
            try
            {
                //Ocultamos el nombre de las columnas agrupadas
                gridViewItemsHf.GroupFormat = "[#image]{1} {2}";

                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                gridViewItemsHf.OptionsView.ColumnAutoWidth = false;
                gridViewItemsHf.HorzScrollVisibility = ScrollVisibility.Auto;

                //Hacer todo el grid no editable
                gridViewItemsHf.OptionsBehavior.Editable = false;

                //Columns definition
                GridColumn colIdItemBcn = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("ItemBCN"), Visible = true, FieldName = nameof(ItemHf.IdItemBcn), Width = 250 };
                GridColumn colFamilyHK = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("FamilyHk"), Visible = false, FieldName = nameof(ItemHf.IdFamilyHK), Width = 10 };
                GridColumn colModel = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Model"), Visible = true, FieldName = $"{nameof(ItemHf.Model)}.{nameof(Model.Description)}", Width = 10 };
                GridColumn colDefaultSupplier = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Supplier"), Visible = false, FieldName = nameof(ItemHf.IdDefaultSupplier), Width = 10 };

                //Add columns to grid root view
                gridViewItemsHf.Columns.Add(colIdItemBcn);
                gridViewItemsHf.Columns.Add(colFamilyHK);
                gridViewItemsHf.Columns.Add(colModel);
                gridViewItemsHf.Columns.Add(colDefaultSupplier);

                //Grouping
                gridViewItemsHf.OptionsView.ShowGroupPanel = false;

                gridViewItemsHf.Columns[nameof(ItemHf.IdFamilyHK)].GroupIndex = 0;
                gridViewItemsHf.Columns[$"{nameof(ItemHf.Model)}.{nameof(Model.Description)}"].GroupIndex = 1;
                gridViewItemsHf.Columns[nameof(ItemHf.IdDefaultSupplier)].GroupIndex = 2;

                gridViewItemsHf.DoubleClick += GridViewItemsHf_DoubleClick;

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
                //Ocultamos el nombre de las columnas agrupadas
                gridViewItemsMt.GroupFormat = "[#image]{1} {2}";

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
                //Ocultamos el nombre de las columnas agrupadas
                gridViewItemsHw.GroupFormat = "[#image]{1} {2}";

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

        private void SetUpGrdItemsHfDetail()
        {
            try
            {
                //Ocultamos el nombre de las columnas agrupadas
                gridViewItemsHfDetail.GroupFormat = "[#image]{1} {2}";

                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                gridViewItemsHfDetail.OptionsView.ColumnAutoWidth = false;
                gridViewItemsHfDetail.HorzScrollVisibility = ScrollVisibility.Auto;

                //Hacer todo el grid no editable
                gridViewItemsHfDetail.OptionsBehavior.Editable = false;

                //Columns definition
                GridColumn colIdItemBcn = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("ItemBCN"), Visible = true, FieldName = nameof(ItemHf.IdItemBcn), Width = 160 };
                GridColumn colItemDescription = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("ItemDescription"), Visible = true, FieldName = nameof(ItemHf.ItemDescription), Width = 300 };
                GridColumn colModel = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Model"), Visible = true, FieldName = $"{nameof(ItemHf.Model)}.{nameof(Model.Description)}", Width = 10 };

                //Add columns to grid root view
                gridViewItemsHfDetail.Columns.Add(colIdItemBcn);
                gridViewItemsHfDetail.Columns.Add(colItemDescription);
                gridViewItemsHfDetail.Columns.Add(colModel);

                //Grouping
                gridViewItemsHfDetail.OptionsView.ShowGroupPanel = false;

                gridViewItemsHfDetail.Columns[$"{nameof(ItemHf.Model)}.{nameof(Model.Description)}"].GroupIndex = 0;

            }
            catch
            {
                throw;
            }
        }

        private void SetUpGrdItemBom()
        {
            try
            {
                //Ocultamos el nombre de las columnas agrupadas
                gridViewItemBom.GroupFormat = "[#image]{1} {2}";

                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                gridViewItemBom.OptionsView.ColumnAutoWidth = false;
                gridViewItemBom.HorzScrollVisibility = ScrollVisibility.Auto;

                //Hacer todo el grid no editable
                gridViewItemBom.OptionsBehavior.Editable = false;

                //Columns Definition
                GridColumn colIdItemBcn = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("ItemBCN"), Visible = true, FieldName = nameof(ItemBom.IdItemBcn), Width = 200 };
                GridColumn colIdSupplier = new GridColumn() { Caption = "", Visible = true, FieldName = nameof(ItemBom.IdSupplier), Width = 100 };

                //Add columns to grid root view
                gridViewItemBom.Columns.Add(colIdItemBcn);
                gridViewItemBom.Columns.Add(colIdSupplier);

                //Events
                xgrdItemBom.ViewRegistered += XgrdItemBom_ViewRegistered;

                gridViewItemBom.PopupMenuShowing += GridViewItemBom_PopupMenuShowing;

                //Hide group panel
                gridViewItemBom.OptionsView.ShowGroupPanel = false;

                gridViewItemBom.Columns[nameof(ItemBom.IdItemBcn)].GroupIndex = 0;

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
                //Ocultamos el nombre de las columnas agrupadas
                gridViewPlainBom.GroupFormat = "[#image]{1} {2}";

                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                gridViewPlainBom.OptionsView.ColumnAutoWidth = false;
                gridViewPlainBom.HorzScrollVisibility = ScrollVisibility.Auto;

                //Hacer todo el grid no editable
                gridViewPlainBom.OptionsBehavior.Editable = false;

                //Columns definition
                GridColumn colIdSupplier = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Supplier"), Visible = true, FieldName = nameof(Classes.PlainBomAux.IdSupplier), Width = 60 };
                GridColumn colIdItemGroup = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("ItemGroup"), Visible = true, FieldName = nameof(Classes.PlainBomAux.ItemGroup), Width = 90 };
                GridColumn colIdItemBcn = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("ItemBCN"), Visible = true, FieldName = nameof(Classes.PlainBomAux.IdItemBcn), Width = 200 };
                GridColumn colDescription = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("ItemDescription"), Visible = true, FieldName = nameof(Classes.PlainBomAux.ItemDescription), Width = 450 };
                GridColumn colQuantity = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Quantity"), Visible = true, FieldName = nameof(Classes.PlainBomAux.Quantity), Width = 60 };
                //GridColumn colScrap = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Scrap"), Visible = true, FieldName = nameof(Classes.PlainBomAux.Scrap), Width = 60 };
                //GridColumn colUnit = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Unit"), Visible = true, FieldName = nameof(Classes.PlainBomAux.Scrap), Width = 70 };

                //Format types
                colQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                colQuantity.DisplayFormat.FormatString = "n2";
                //colScrap.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                //colScrap.DisplayFormat.FormatString = "n2";

                //Show footer for summaries
                //gridViewPlainBom.OptionsView.ShowFooter = true; 

                //Summaries
                //GridColumnSummaryItem sumMtQuantity = new GridColumnSummaryItem()
                //{
                //    SummaryType = SummaryItemType.Custom,
                //    FieldName = nameof(Classes.PlainBomAux.Quantity),
                //    DisplayFormat = "MT: {0}",
                //    Tag = 1
                //};

                //GridColumnSummaryItem sumHwQuantity = new GridColumnSummaryItem()
                //{
                //    SummaryType = SummaryItemType.Custom,
                //    FieldName = nameof(Classes.PlainBomAux.Quantity),
                //    DisplayFormat = "HW: {0}",
                //    Tag = 2
                //};

                //GridColumnSummaryItem sumMtWaste = new GridColumnSummaryItem()
                //{
                //    SummaryType = SummaryItemType.Custom,
                //    FieldName = nameof(Classes.PlainBomAux.Scrap),
                //    DisplayFormat = "MT: {0}",
                //    Tag = 3
                //};

                //GridColumnSummaryItem sumHwWaste = new GridColumnSummaryItem()
                //{
                //    SummaryType = SummaryItemType.Custom,
                //    FieldName = nameof(Classes.PlainBomAux.Scrap),
                //    DisplayFormat = "HW: {0}",
                //    Tag = 4
                //};

                //Add  summaries to columns
                //colQuantity.Summary.Add(sumMtQuantity);
                //colQuantity.Summary.Add(sumHwQuantity);
                //colScrap.Summary.Add(sumMtWaste);
                //colScrap.Summary.Add(sumHwWaste);

                //Add columns to grid root view
                gridViewPlainBom.Columns.Add(colIdSupplier);
                gridViewPlainBom.Columns.Add(colIdItemGroup);
                gridViewPlainBom.Columns.Add(colIdItemBcn);
                gridViewPlainBom.Columns.Add(colDescription);
                gridViewPlainBom.Columns.Add(colQuantity);
                //gridViewPlainBom.Columns.Add(colScrap);

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

                if (e.KeyCode == Keys.F4)
                {
                    DialogResult result = XtraMessageBox.Show("Delete row?", "Confirmation", MessageBoxButtons.YesNo);
                    if (result != DialogResult.Yes)
                        return;

                    GridView activeView = xgrdItemBom.FocusedView as GridView;
                    BaseView parent = activeView.ParentView;
                    var rowParent = parent.GetRow(activeView.SourceRowHandle);

                    switch (activeView.LevelName)
                    {
                        case nameof(ItemBom.Materials):
                        case nameof(ItemBom.Hardwares):
                        case nameof(ItemBom.HalfFinished):

                            var bomRow = activeView.GetRow(activeView.FocusedRowHandle);

                            //if (bomRow.GetType() == typeof(DetailBomMt))
                            //    DeleteRawMaterial(bomRow as DetailBomMt);
                            //else if (bomRow.GetType() == typeof(DetailBomHw))
                            //    DeleteHardware(bomRow as DetailBomHw);

                            if (rowParent.GetType().BaseType == typeof(ItemBom) || rowParent.GetType() == typeof(ItemBom))
                            {
                                if (bomRow.GetType() == typeof(DetailBomMt))
                                    (rowParent as ItemBom).Materials.Remove(bomRow as DetailBomMt);
                                else if (bomRow.GetType() == typeof(DetailBomHw))
                                    (rowParent as ItemBom).Hardwares.Remove(bomRow as DetailBomHw);
                                else if (bomRow.GetType() == typeof(DetailBomHf))
                                    (rowParent as ItemBom).HalfFinished.Remove(bomRow as DetailBomHf);
                            }

                            activeView.RefreshData();

                            LoadBomTreeView();
                            LoadPlainBom();
                            break;

                    }
                }

                if (e.KeyCode == Keys.Enter)
                {
                    GridView activeView = xgrdItemBom.FocusedView as GridView;


                    switch (activeView.LevelName)
                    {
                        case nameof(DetailBomHf.DetailItemBom) + "." + nameof(ItemBom.Materials):
                        case nameof(ItemBom.Materials):
                            if (activeView.FocusedRowHandle == activeView.RowCount - 1)
                            {
                                e.SuppressKeyPress = true;
                                e.Handled = true;

                                BeginInvoke(new MethodInvoker(delegate
                                {
                                    if (activeView.ValidateEditor())
                                    {
                                        activeView.CloseEditor();

                                        DetailBomMt row = activeView.GetRow(activeView.FocusedRowHandle) as DetailBomMt;
                                        BaseView parent = activeView.ParentView;
                                        var rowParent = parent.GetRow(activeView.SourceRowHandle);

                                        if (row.Quantity > 0 && string.IsNullOrEmpty(row.IdItemBcn) == false)
                                        {
                                            if (rowParent.GetType().BaseType == typeof(ItemBom) || rowParent.GetType() == typeof(ItemBom))
                                            {
                                                (rowParent as ItemBom).Materials.Add(new DetailBomMt());
                                            }
                                            else if (rowParent.GetType().BaseType == typeof(DetailBomHf) || rowParent.GetType() == typeof(DetailBomHf))
                                            {
                                                (rowParent as DetailBomHf).DetailItemBom.Materials.Add(new DetailBomMt());
                                            }
                                            activeView.RefreshData();

                                            //activeView.BeginDataUpdate();
                                            //grdBomRefreshAndExpand();
                                            //activeView.EndDataUpdate();
                                        }



                                        //var x = activeView.GetDetailView(0, 0);

                                        // var x = activeView.GetDetailView(activeView.FocusedRowHandle, activeView.GetRelationIndex(activeView.FocusedRowHandle, activeView.LevelName));

                                        //int intLinhaGroup = activeView.FocusedRowHandle;
                                        //int intIndexLevel = activeView.GetRelationIndex(intLinhaGroup, activeView.LevelName.ToString());
                                        //var x = activeView.GetDetailView(intLinhaGroup, intIndexLevel);
                                        //var x = activeView.GetFocusedDataRow();

                                        //activeView.FocusedColumn = activeView.Columns[nameof(DetailBomMt.IdItemBcn)];
                                        //activeView.ShowEditor();

                                    }
                                }));

                            }

                            break;


                        case nameof(ItemBom.Hardwares):

                            if (activeView.FocusedRowHandle == activeView.RowCount - 1)
                            {
                                BeginInvoke(new MethodInvoker(delegate
                                {
                                    if (activeView.ValidateEditor())
                                    {
                                        activeView.CloseEditor();

                                        DetailBomHw row = activeView.GetRow(activeView.FocusedRowHandle) as DetailBomHw;
                                        BaseView parent = activeView.ParentView;
                                        var rowParent = parent.GetRow(activeView.SourceRowHandle);

                                        if (row.Quantity > 0 && string.IsNullOrEmpty(row.IdItemBcn) == false)
                                        {
                                            if (rowParent.GetType().BaseType == typeof(ItemBom) || rowParent.GetType() == typeof(ItemBom))
                                            {
                                                (rowParent as ItemBom).Hardwares.Add(new DetailBomHw());
                                            }
                                            //else if (rowParent.GetType().BaseType == typeof(DetailBomHf) || rowParent.GetType() == typeof(DetailBomHf))
                                            //{
                                            //    (rowParent as DetailBomHf).DetailItemBom.Materials.Add(new DetailBomMt());
                                            //}
                                            activeView.RefreshData();

                                            //activeView.BeginDataUpdate();
                                            //grdBomRefreshAndExpand();
                                            //activeView.EndDataUpdate();
                                        }

                                    }
                                }));

                            }

                            break;

                        case nameof(ItemBom.HalfFinished):

                            if (activeView.FocusedRowHandle == activeView.RowCount - 1)
                            {
                                BeginInvoke(new MethodInvoker(delegate
                                {
                                    if (activeView.ValidateEditor())
                                    {
                                        activeView.CloseEditor();

                                        DetailBomHf row = activeView.GetRow(activeView.FocusedRowHandle) as DetailBomHf;
                                        BaseView parent = activeView.ParentView;
                                        var rowParent = parent.GetRow(activeView.SourceRowHandle);

                                        if (row.Quantity > 0 && row.DetailItemBom != null)
                                        {
                                            if (rowParent.GetType().BaseType == typeof(ItemBom) || rowParent.GetType() == typeof(ItemBom))
                                            {
                                                DetailBomHf tmpDetHf = new DetailBomHf()
                                                {
                                                    IdBom = (rowParent as ItemBom).IdBom,
                                                };
                                                (rowParent as ItemBom).HalfFinished.Add(tmpDetHf);
                                            }
                                            //else if (rowParent.GetType().BaseType == typeof(DetailBomHf) || rowParent.GetType() == typeof(DetailBomHf))
                                            //{
                                            //    (rowParent as DetailBomHf).DetailItemBom.Materials.Add(new DetailBomMt());
                                            //}
                                            activeView.RefreshData();

                                            //activeView.BeginDataUpdate();
                                            //grdBomRefreshAndExpand();
                                            //activeView.EndDataUpdate();
                                        }

                                    }
                                }));

                            }

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

        private void LoadAuxList()
        {
            try
            {
                _suppliersList = GlobalSetting.SupplierService.GetSuppliers();
            }
            catch
            {
                throw;
            }
        }

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
            catch
            {
                throw;
            }
        }

        private void LoadItemsListHf()
        {
            try
            {
                _itemsHfList = GlobalSetting.ItemHfService.GetItems();
                xgrdItemsHf.DataSource = _itemsHfList;

                //Para el detalle, ya que un semielaborado se puede editar independientemente su bom o formar parte del BOM de otro item
                _itemsHfDetailBomList = GlobalSetting.ItemHfService.GetItems();
                xgrdItemsHfDetail.DataSource = _itemsHfDetailBomList;
            }
            catch
            {
                throw;
            }
        }

        private void LoadItemsListMt()
        {
            try
            {
                _itemsMtList = GlobalSetting.ItemMtService.GetItems();
                xgrdItemsMt.DataSource = _itemsMtList;
            }
            catch
            {
                throw;
            }
        }

        private void LoadItemsListHw()
        {
            try
            {
                _itemsHwList = GlobalSetting.ItemHwService.GetItems();
                xgrdItemsHw.DataSource = _itemsHwList;
            }
            catch
            {
                throw;
            }
        }

        //private void LoadItemGridBom(ItemEy item)
        private void LoadItemGridBom(object item)
        {
            try
            {
                //ItemBom itemBom;

                //itemBom = GlobalSetting.ItemBomService.GetItemBom(item.IdItemBcn);
                //_itemBomOriginal = GlobalSetting.ItemBomService.GetItemBom(item.IdItemBcn);

                //itemBom = GlobalSetting.ItemBomService.GetItemBom(item.IdItemBcn, getPoco : true);
                //_itemBomOriginal = itemBom.DeepCopyByExpressionTree();

                //TODO: Las extensiones para clonar no me funcionan con esta clase. Al no ser el tipo base sino el proxy que genera EF hace cosas raras
                //_itemBomOriginal = itemBom.Clone(); 
                //_itemBomOriginal = itemBom.DeepCopyByExpressionTree();

                Cursor = Cursors.WaitCursor;

                _itemBomList.Clear();

                string idIdItemBcn = string.Empty;

                if (item.GetType() == typeof(ItemEy))
                {
                    idIdItemBcn = (item as ItemEy).IdItemBcn;
                }
                else
                {
                    idIdItemBcn = (item as ItemHf).IdItemBcn;
                }


                _itemBomList = GlobalSetting.ItemBomService.GetItemBom(idIdItemBcn);



                if (_itemBomList == null)
                {
                    ItemBom itemBom = new ItemBom();
                    itemBom.IdBom = 0;
                    itemBom.IdItemBcn = idIdItemBcn;
                    itemBom.Item = item;
                    itemBom.IdItemGroup = (item.GetType() == typeof(ItemEy) ? Constants.ITEM_GROUP_EY: Constants.ITEM_GROUP_HF);
                    itemBom.Materials = new List<DetailBomMt>();
                    itemBom.Hardwares = new List<DetailBomHw>();
                    itemBom.HalfFinishedNM = new List<ItemBom>();

                    _itemBomList.Add(itemBom);

                    //_itemBomList.Add(itemBom);
                    //_itemBomOriginal = new ItemBom();
                    //_itemBomOriginal.IdBom = 0;
                    //_itemBomOriginal.IdItemBcn = item.IdItemBcn;
                    //_itemBomOriginal.Item = item;
                    //_itemBomOriginal.IdItemGroup = Constants.ITEM_GROUP_EY;
                    //_itemBomOriginal.Materials = new List<DetailBomMt>();
                    //_itemBomOriginal.Hardwares = new List<DetailBomHw>();
                    //_itemBomOriginal.HalfFinishedNM = new List<ItemBom>();
                }

                //TEST
                ////var x = GlobalSetting.ItemBomService.GetItemBom("8 AKANE BLBE/FRE");
                ////var xx = _itemBomList.Where(a => a.IdSupplier.Equals("N/D")).FirstOrDefault();
                ////xx.HalfFinishedNM = x;

                ////DetailBomHf detailBomHfTest= new DetailBomHf();
                ////detailBomHfTest.IdBom = xx.IdBom;
                ////detailBomHfTest.IdBomDetail = x.Select(a => a.IdBom).FirstOrDefault();
                ////detailBomHfTest.DetailItemBom = x.FirstOrDefault();
                ////List<DetailBomHf> detailBomHfListTest = new List<DetailBomHf>();
                ////detailBomHfListTest.Add(detailBomHfTest);
                ////xx.HalfFinished = detailBomHfListTest;

                xgrdItemBom.DataSource = null;
                xgrdItemBom.DataSource = _itemBomList;

                grdBomRefreshAndExpand();
                dockPanelGrdBom.Select();

                LoadBomTreeView();
                LoadPlainBom();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void LoadPlainBom()
        {
            try
            {
                return; //TODO
                xgrdPlainBom.DataSource = null;
                List<Classes.PlainBomAux> plainBom = new List<Classes.PlainBomAux>();

                foreach(var bom in _itemBomList)
                {
                    foreach (DetailBomMt rm in bom.Materials)
                    {
                        Classes.PlainBomAux tmp = rm;
                        tmp.IdSupplier = bom.IdSupplier;
                        plainBom.Add(tmp);
                    }

                    foreach (DetailBomHw h in bom.Hardwares)
                    {
                        Classes.PlainBomAux tmp = h;
                        tmp.IdSupplier = bom.IdSupplier;
                        plainBom.Add(tmp);
                    }

                    foreach(var hf in bom.HalfFinishedNM)
                    {
                        Classes.PlainBomAux tmp = new Classes.PlainBomAux()
                        {
                            IdSupplier = hf.IdSupplier,
                            IdItemBcn = hf.IdItemBcn,
                            ItemDescription = (hf.Item as ItemHf).ItemDescription,
                            ItemGroup = Constants.ITEM_GROUP_HF,
                        };
                        plainBom.Add(tmp);
                    }
                }

                

                xgrdPlainBom.DataSource = plainBom;

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Grid BOM

        private void AddRawMaterial(ItemMt itemMt, string supplier)
        {
            try
            {
                ItemBom itemBom = _itemBomList.Where(a => a.IdSupplier.Equals(supplier)).FirstOrDefault();

                var rawMaterial = itemBom.Materials.Where(a => a.IdItemBcn.Equals(itemMt.IdItemBcn));

                if(rawMaterial == null || rawMaterial.Count() == 0)
                {
                    DetailBomMt detail = new DetailBomMt()
                    {
                        IdBom = itemBom.IdBom,
                        IdItemBcn = itemMt.IdItemBcn,
                        Item = itemMt,
                        Quantity = 0,
                        Scrap = 0,
                    };

                    itemBom.Materials.Add(detail);
                    grdBomRefreshAndExpand();
                }
                else
                {
                    XtraMessageBox.Show($"Raw Material already exist for supplier {supplier}");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AddHardware(ItemHw itemHw, string supplier)
        {
            try
            {
                ItemBom itemBom = _itemBomList.Where(a => a.IdSupplier.Equals(supplier)).FirstOrDefault();

                var hardware = itemBom.Hardwares.Where(a => a.IdItemBcn.Equals(itemHw.IdItemBcn));

                if (hardware == null || hardware.Count() == 0)
                {
                    DetailBomHw detail = new DetailBomHw()
                    {
                        IdBom = itemBom.IdBom,
                        IdItemBcn = itemHw.IdItemBcn,
                        Item = itemHw,
                        Quantity = 0,
                        Scrap = 0
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

        //private void DeleteRawMaterial(DetailBomMt bomRow)
        //{
        //    try
        //    {
        //        ItemBom itemBom = _itemBomList.FirstOrDefault();
        //        var material = itemBom.Materials.Where(a => a.IdItemBcn.Equals(bomRow.IdItemBcn)).FirstOrDefault();
        //        if (material != null)
        //        {
        //            itemBom.Materials.Remove(material);
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //private void DeleteHardware(DetailBomHw bomRow)
        //{
        //    try
        //    {
        //        ItemBom itemBom = _itemBomList.FirstOrDefault();
        //        var hardware = itemBom.Hardwares.Where(a => a.IdItemBcn.Equals(bomRow.IdItemBcn)).FirstOrDefault();
        //        if (hardware != null)
        //        {
        //            itemBom.Hardwares.Remove(hardware);
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        private void SetGrdBomEditColumns()
        {
            try
            {
                //Common Edit repositories
                RepositoryItemTextEdit ritxt2Dec = new RepositoryItemTextEdit();
                ritxt2Dec.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                ritxt2Dec.Mask.EditMask = "F2";
                ritxt2Dec.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;

                RepositoryItemTextEdit ritxtInt = new RepositoryItemTextEdit();
                ritxtInt.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                ritxtInt.Mask.EditMask = "N";
                ritxtInt.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;

                foreach (GridView view in xgrdItemBom.ViewCollection)
                {
                    switch (view.LevelName)
                    {
                        case nameof(DetailBomHf.DetailItemBom) + "." + nameof(ItemBom.Materials):
                        case nameof(ItemBom.Materials):

                            //Specific Edit repositories
                            RepositoryItemSearchLookUpEdit riItemsMt = new RepositoryItemSearchLookUpEdit()
                            {
                                DataSource = _itemsMtList,
                                ValueMember = nameof(ItemMt.IdItemBcn),
                                DisplayMember = nameof(ItemMt.IdItemBcn),
                                ShowClearButton = false,
                                NullText = "Select Item",
                            };
                            riItemsMt.View.Columns.AddField(nameof(ItemMt.IdItemBcn)).Visible = true;
                            riItemsMt.View.Columns.AddField(nameof(ItemMt.ItemDescription)).Visible = true;


                            view.OptionsBehavior.Editable = true;

                            //Edit Columns
                            view.Columns[nameof(DetailBomMt.Length)].OptionsColumn.AllowEdit = true;
                            view.Columns[nameof(DetailBomMt.Width)].OptionsColumn.AllowEdit = true;
                            view.Columns[nameof(DetailBomMt.Height)].OptionsColumn.AllowEdit = true;
                            view.Columns[nameof(DetailBomMt.Density)].OptionsColumn.AllowEdit = true;
                            view.Columns[nameof(DetailBomMt.Coefficient1)].OptionsColumn.AllowEdit = true;
                            view.Columns[nameof(DetailBomMt.Coefficient2)].OptionsColumn.AllowEdit = true;
                            view.Columns[nameof(DetailBomMt.Scrap)].OptionsColumn.AllowEdit = true;
                            view.Columns[nameof(DetailBomMt.Scrap)].OptionsColumn.AllowEdit = true;
                            view.Columns[nameof(DetailBomMt.Quantity)].OptionsColumn.AllowEdit = true;

                            view.Columns[nameof(DetailBomMt.IdItemBcn)].OptionsColumn.AllowEdit = true;

                            //No edit columns
                            //view.Columns[nameof(DetailBomMt.IdItemBcn)].OptionsColumn.AllowEdit = false;
                            view.Columns[$"{nameof(DetailBomMt.Item)}.{nameof(ItemMt.ItemDescription)}"].OptionsColumn.AllowEdit = false;
                            view.Columns[$"{nameof(DetailBomMt.Item)}.{nameof(ItemMt.Unit)}"].OptionsColumn.AllowEdit = false;

                            //Edit repositories
                            view.Columns[nameof(DetailBomMt.Length)].ColumnEdit = ritxt2Dec;
                            view.Columns[nameof(DetailBomMt.Width)].ColumnEdit = ritxt2Dec;
                            view.Columns[nameof(DetailBomMt.Height)].ColumnEdit = ritxt2Dec;
                            view.Columns[nameof(DetailBomMt.Density)].ColumnEdit = ritxt2Dec;
                            view.Columns[nameof(DetailBomMt.Coefficient1)].ColumnEdit = ritxt2Dec;
                            view.Columns[nameof(DetailBomMt.Coefficient2)].ColumnEdit = ritxt2Dec;
                            view.Columns[nameof(DetailBomMt.Scrap)].ColumnEdit = ritxt2Dec;
                            view.Columns[nameof(DetailBomMt.Quantity)].ColumnEdit = ritxt2Dec;
                            view.Columns[nameof(DetailBomMt.NumberOfParts)].ColumnEdit = ritxtInt;

                            view.Columns[nameof(DetailBomMt.IdItemBcn)].ColumnEdit = riItemsMt;

                            //view.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;


                            break;

                        case nameof(DetailBomHf.DetailItemBom) + "." + nameof(ItemBom.Hardwares):
                        case nameof(ItemBom.Hardwares):

                            //Specific Edit repositories
                            RepositoryItemSearchLookUpEdit riItemsHw = new RepositoryItemSearchLookUpEdit()
                            {
                                DataSource = _itemsHwList,
                                ValueMember = nameof(ItemHw.IdItemBcn),
                                DisplayMember = nameof(ItemHw.IdItemBcn),
                                ShowClearButton = false,
                                NullText = "Select Item",
                            };
                            riItemsHw.View.Columns.AddField(nameof(ItemHw.IdItemBcn)).Visible = true;
                            riItemsHw.View.Columns.AddField(nameof(ItemHw.ItemDescription)).Visible = true;


                            view.OptionsBehavior.Editable = true;

                            //Edit Columns
                            //view.Columns[nameof(DetailBomHw.Quantity)].OptionsColumn.AllowEdit = true;
                            //view.Columns[nameof(DetailBomHw.Scrap)].OptionsColumn.AllowEdit = true;
                            //view.Columns[nameof(DetailBomHw.IdItemBcn)].OptionsColumn.AllowEdit = true;

                            //No edit columns
                            view.Columns[$"{nameof(DetailBomHw.Item)}.{nameof(ItemHw.ItemDescription)}"].OptionsColumn.AllowEdit = false;
                            view.Columns[$"{nameof(DetailBomHw.Item)}.{nameof(ItemHw.Unit)}"].OptionsColumn.AllowEdit = false;

                            //Edit repositories
                            view.Columns[nameof(DetailBomHw.Quantity)].ColumnEdit = ritxt2Dec;
                            view.Columns[nameof(DetailBomHw.Scrap)].ColumnEdit = ritxt2Dec;

                            view.Columns[nameof(DetailBomHw.IdItemBcn)].ColumnEdit = riItemsHw;

                            break;


                        case (nameof(ItemBom.HalfFinished)):

                            //Specific Edit repositories
                            var idsHf = _itemsHfList.Select(a => a.IdItemBcn).ToList();

                            RepositoryItemSearchLookUpEdit riItemsHf = new RepositoryItemSearchLookUpEdit()
                            {
                                DataSource = idsHf,
                                //ValueMember = nameof(ItemHw.IdItemBcn),
                                //DisplayMember = nameof(ItemHw.IdItemBcn),
                                ShowClearButton = false,
                                NullText = "Select Item",
                            };
                            //riItemsHf.View.Columns.AddField(nameof(ItemHw.IdItemBcn)).Visible = true;
                            //riItemsHf.View.Columns.AddField(nameof(ItemHw.ItemDescription)).Visible = true;

                            RepositoryItemButtonEdit repEditHf = new RepositoryItemButtonEdit()
                            {
                                Name = "btnEditHf",
                                TextEditStyle = TextEditStyles.HideTextEditor,
                            };
                            repEditHf.Buttons[0].Kind = ButtonPredefines.Glyph;
                            //repEditHf.Buttons[0].Image = System.Drawing.Image.FromFile(@"Resources\Images\Edit_16x16.png");
                            repEditHf.Buttons[0].Image = DevExpress.Images.ImageResourceCache.Default.GetImage("images/edit/edit_16x16.png");
                            repEditHf.Click += RepEditHf_Click;


                            view.OptionsBehavior.Editable = true;

                            view.Columns[nameof(DetailBomHf.Quantity)].ColumnEdit = ritxt2Dec;
                            view.Columns[$"{nameof(DetailBomHf.DetailItemBom)}.{nameof(ItemBom.IdItemBcn)}"].ColumnEdit = riItemsHf;
                            view.Columns[EDIT_COLUMN].ColumnEdit = repEditHf;
                            view.Columns[EDIT_COLUMN].Visible = true;


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
                view.ExpandAllGroups();

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
            //TODO
            return;

            try
            {
                string idItem = string.Empty;

                if (_currentItem.GetType() == typeof(ItemEy))
                {
                    idItem = (_currentItem as ItemEy).IdItemBcn;
                }
                treeViewBom.Nodes.Clear();

                TreeNode root = new TreeNode(idItem);
                foreach (var bom in _itemBomList)
                {
                    root.Nodes.Add(GetComponentNode(bom));
                    root.Expand();
                }
                treeViewBom.Nodes.Add(root);
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

                TreeNode root = new TreeNode(item.IdSupplier);
                root.Name = item.IdSupplier; //El Name es el key de un treeNode

                TreeNode rawMatPrimasRoot = new TreeNode("Raw Materials");
                TreeNode HardwareRoot = new TreeNode("Hardware");
                TreeNode HalfFinishedRoot = new TreeNode("Half-Finished");

                root.Nodes.Add(rawMatPrimasRoot);
                root.Nodes.Add(HardwareRoot);
                root.Nodes.Add(HalfFinishedRoot);
                root.Expand();

                if (item.Materials != null)
                {
                    foreach (DetailBomMt rawMaterial in item.Materials)
                    {
                        root.Nodes[0].Nodes.Add(rawMaterial.IdItemBcn, $"{rawMaterial.IdItemBcn} : {rawMaterial.Item.ItemDescription}");
                        root.Nodes[0].Nodes[contRawMaterialsNode].Tag = "RawMaterials";
                        root.Nodes[0].Nodes[contRawMaterialsNode].Nodes.Add(
                            new TreeNode($"Quantity : {rawMaterial.Quantity.ToString()}")
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
                            new TreeNode($"Quantity : {hardware.Quantity.ToString()}")
                            );
                        contHardwareNode++;
                    }
                    root.Nodes[1].Expand();
                }

                if (item.HalfFinishedNM != null)
                {
                    foreach(ItemBom ib in item.HalfFinishedNM)
                    {
                        root.Nodes[2].Nodes.Add(GetComponentNode(ib));
                        root.Nodes[2].Expand();
                    }
                }


                return root;
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        #endregion

        #region Supplier Bom
        private void AddSupplierBom()
        {
            try
            {
                string idSupplier = slueSupplier.EditValue as string;
                var exist = _itemBomList.Where(a => a.IdSupplier.Equals(idSupplier)).FirstOrDefault();

                string idItemBcn = string.Empty;
                string idItemGroup = string.Empty;
              

                if (exist == null)
                {
                    if (_currentItem.GetType() == typeof(ItemEy))
                    {
                        idItemBcn = (_currentItem as ItemEy).IdItemBcn;
                        idItemGroup = Constants.ITEM_GROUP_EY;
                    }
                    else if (_currentItem.GetType() == typeof(ItemHf))
                    {
                        idItemBcn = (_currentItem as ItemHf).IdItemBcn;
                        idItemGroup = Constants.ITEM_GROUP_HF;
                    }


                    ItemBom itemBom = new ItemBom();
                    itemBom.IdBom = 0;
                    itemBom.IdItemBcn = idItemBcn;
                    itemBom.Item = _currentItem;
                    itemBom.IdItemGroup = idItemGroup;
                    itemBom.IdSupplier = idSupplier;
                    itemBom.Materials = new List<DetailBomMt>();
                    itemBom.Hardwares = new List<DetailBomHw>();
                    itemBom.HalfFinishedNM = new List<ItemBom>();

                    _itemBomList.Add(itemBom);
                    xgrdItemBom.DataSource = null;
                    xgrdItemBom.DataSource = _itemBomList;

                    grdBomRefreshAndExpand();
                    dockPanelGrdBom.Select();

                    LoadBomTreeView();
                    LoadPlainBom();



                }
                else
                {
                    XtraMessageBox.Show("Supplier already exist in BOM");
                }
                
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Dialog Form

        private List<string> OpenSelectSuppliersForm()
        {
            try
            {
                

                var suppliers = _itemBomList.Select(a => a.IdSupplier).ToList();
                List <string> selectedSuppliers = new List<string>();

                using (DialogForms.SelectSuppliers form = new DialogForms.SelectSuppliers())
                {
                    form.InitData(suppliers);

                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        selectedSuppliers = form.SelectedSuppliers;
                    }

                }

                return selectedSuppliers;

            }
            catch
            {
                throw;
            }
        }

        private void OpenEditHfBom(ItemBom bom)
        {
            try
            {
                using (DialogForms.EditHfBom form = new DialogForms.EditHfBom())
                {

                    //No se le puede pasar directamente el objeto porque se pasaría un puntero y desde el otro formulario, si modifican y cancelan se vería reflejado aquí.
                    //También me da un error al intentar clonarlo
                    form.InitData(bom.IdItemBcn, bom.IdSupplier);

                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        bom = GlobalSetting.ItemBomService.GetItemSupplierBom(bom.IdItemBcn, bom.IdSupplier);
                    }
                    //else
                    //{
                    //    MessageBox.Show("KO");
                    //}
                }
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
                SetGrdBomEditColumns();

                ShowAddBomSupplier(true);

                xgrdItemsEy.Enabled = false;
                xgrdItemsHf.Enabled = false;

                dockPanelItemsEy.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide;
                dockPanelItemsEy.HideSliding();

                dockPanelItemsHf.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide;
                dockPanelItemsHf.HideSliding();

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
                foreach (var bom in _itemBomList)
                {
                    foreach (var m in bom.Materials)
                    {
                        if (string.IsNullOrEmpty(m.IdItemBcn) == false && m.Quantity <= 0)
                        {
                            XtraMessageBox.Show($"Quantity must be greater than Zero ({m.IdItemBcn})", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }

                    foreach (var h in bom.Hardwares)
                    {
                        if (string.IsNullOrEmpty(h.IdItemBcn) == false && h.Quantity <= 0)
                        {
                            XtraMessageBox.Show($"Quantity must be greater than Zero ({h.IdItemBcn})", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }

                    foreach (var hf in bom.HalfFinished)
                    {
                        if (hf.DetailItemBom != null && hf.Quantity <= 0)
                        {
                            XtraMessageBox.Show($"Half-finished must be greater than Zero ({hf.DetailItemBom.IdItemBcn})", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }

                }

                return true;
            }
            catch
            {
                throw;
            }
        }

        private void DeleteLastRowIfEmpty()
        {
            try
            {
                foreach (var bom in _itemBomList)
                {
                    //Hay que hacer el loop al inverso para poder ir borrando y que no de error

                    for (int i = bom.Materials.Count - 1; i >= 0; i--)
                    {
                        if (string.IsNullOrEmpty(bom.Materials[i].IdItemBcn))
                        {
                            bom.Materials.RemoveAt(i);
                        }
                    }

                    for (int i = bom.Hardwares.Count - 1; i >= 0; i--)
                    {
                        if (string.IsNullOrEmpty(bom.Hardwares[i].IdItemBcn))
                        {
                            bom.Hardwares.RemoveAt(i);
                        }
                    }

                    for (int i = bom.HalfFinished.Count - 1; i >= 0; i--)
                    {
                        if (bom.HalfFinished[i].DetailItemBom == null)
                        {
                            bom.HalfFinished.RemoveAt(i);
                        }
                    }

                }
            }
            catch
            {
                throw;
            }
        }

        private bool ShowHalfFinishedMessageInfo()
        {
            try
            {
                string msg = "This change will affect the following items:" + Environment.NewLine;
                bool find = false;

                if (_currentItem.GetType() == typeof(ItemHf))
                {
                    foreach (var item in _itemBomList)
                    {
                        using (var db = new DB.HKSupplyContext())
                        {
                            var list = db.ItemsBom
                                .Join(
                                    db.DetailsBomHf,
                                    itemBom => itemBom.IdBom,
                                    detail => detail.IdBom,
                                    (itemBom, detail) => new { ItemBom = itemBom, DetailBomHf = detail }
                                    )
                                .Where(a => a.DetailBomHf.IdBomDetail.Equals(item.IdBom))
                                .ToList();

                            foreach (var x in list)
                            {
                                find = true;
                                msg += x.ItemBom.IdItemBcn + Environment.NewLine;
                            }
                        }
                    }

                    if (find)
                    {
                        msg += Environment.NewLine + "Continue?" + Environment.NewLine;
                        DialogResult result = MessageBox.Show(msg, "", MessageBoxButtons.YesNo);

                        if (result == DialogResult.Yes)
                            return true;
                        else
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

        //private bool EditBom(ItemBom itemBom)
        //{
        //    try
        //    {
        //        return GlobalSetting.ItemBomService.EditItemBom(itemBom);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        private bool EditBom()
        {
            try
            {
                return GlobalSetting.ItemBomService.EditItemSuppliersBom(_itemBomList);
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
                _itemBomList.Clear();
                LoadItemGridBom(item);

                //Restore de ribbon to initial states
                RestoreInitState();


                dockPanelItemsEy.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
                dockPanelItemsEy.ShowSliding();

                dockPanelItemsHf.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
                dockPanelItemsHf.ShowSliding();

                xgrdItemsEy.Enabled = true;
                xgrdItemsHf.Enabled = true;

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

        #region Aux

        private void ShowAddBomSupplier(bool show)
        {
            try
            {
                lblSupplier.Visible = slueSupplier.Visible = sbAddBomSupplier.Visible = show;
            }
            catch
            {
                throw;
            }
        }

        #endregion


        #region PopupMenu
        private void GridViewItemBom_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            try
            {
                if (CurrentState != ActionsStates.Edit)
                    return;

                GridView view = sender as GridView;

                if (view.FocusedRowHandle < 0)
                    return;

                if (e.MenuType == GridMenuType.Row)
                {

                    var row = view.GetRow(view.FocusedRowHandle);

                    if (row.GetType().BaseType == typeof(ItemBom) || row.GetType() == typeof(ItemBom))
                    {
                        int rowHandle = e.HitInfo.RowHandle;
                        e.Menu.Items.Clear();

                        if ((row as ItemBom).Materials == null || (row as ItemBom).Materials.Count() == 0)
                            e.Menu.Items.Add(CreateMenuItemAddINewLineMaterial(view, rowHandle));

                        if ((row as ItemBom).Hardwares == null || (row as ItemBom).Hardwares.Count() == 0)
                            e.Menu.Items.Add(CreateMenuItemAddINewLineHardware(view, rowHandle));

                        if ((row as ItemBom).HalfFinished == null || (row as ItemBom).HalfFinished.Count() == 0)
                            e.Menu.Items.Add(CreateMenuItemAddINewLineHalfFinished(view, rowHandle));
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        DevExpress.Utils.Menu.DXMenuItem CreateMenuItemAddINewLineMaterial(GridView view, int rowHandle)
        {
            DevExpress.Utils.Menu.DXMenuItem menuItem = new DevExpress.Utils.Menu.DXMenuItem("Add Material",
                new EventHandler(OnMenuItemAddnewLineMaterialClick));
            menuItem.Tag = new Classes.RowInfo(view, rowHandle);
            return menuItem;
        }

        DevExpress.Utils.Menu.DXMenuItem CreateMenuItemAddINewLineHardware(GridView view, int rowHandle)
        {
            DevExpress.Utils.Menu.DXMenuItem menuItem = new DevExpress.Utils.Menu.DXMenuItem("Add Hardware",
                new EventHandler(OnMenuItemAddnewLineHardwareClick));
            menuItem.Tag = new Classes.RowInfo(view, rowHandle);
            return menuItem;
        }

        DevExpress.Utils.Menu.DXMenuItem CreateMenuItemAddINewLineHalfFinished(GridView view, int rowHandle)
        {
            DevExpress.Utils.Menu.DXMenuItem menuItem = new DevExpress.Utils.Menu.DXMenuItem("Add Half-finished",
                new EventHandler(OnMenuItemAddnewLineHalfFinishedClick));
            menuItem.Tag = new Classes.RowInfo(view, rowHandle);
            return menuItem;
        }

        void OnMenuItemAddnewLineMaterialClick(object sender, EventArgs e)
        {
            try
            {
                DevExpress.Utils.Menu.DXMenuItem item = sender as DevExpress.Utils.Menu.DXMenuItem;
                Classes.RowInfo info = item.Tag as Classes.RowInfo;

                var row = info.View.GetRow(info.View.FocusedRowHandle);
                (row as ItemBom).Materials.Add(new DetailBomMt());
                info.View.ExpandMasterRow(info.View.FocusedRowHandle, nameof(ItemBom.Materials));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void OnMenuItemAddnewLineHardwareClick(object sender, EventArgs e)
        {
            try
            {
                DevExpress.Utils.Menu.DXMenuItem item = sender as DevExpress.Utils.Menu.DXMenuItem;
                Classes.RowInfo info = item.Tag as Classes.RowInfo;

                var row = info.View.GetRow(info.View.FocusedRowHandle);
                (row as ItemBom).Hardwares.Add(new DetailBomHw());
                info.View.ExpandMasterRow(info.View.FocusedRowHandle, nameof(ItemBom.Hardwares));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void OnMenuItemAddnewLineHalfFinishedClick(object sender, EventArgs e)
        {
            try
            {
                DevExpress.Utils.Menu.DXMenuItem item = sender as DevExpress.Utils.Menu.DXMenuItem;
                Classes.RowInfo info = item.Tag as Classes.RowInfo;

                var row = info.View.GetRow(info.View.FocusedRowHandle);

                if ((row as ItemBom).HalfFinished == null)
                    (row as ItemBom).HalfFinished = new List<DetailBomHf>();
                (row as ItemBom).HalfFinished.Add(new DetailBomHf() { IdBom = (row as ItemBom).IdBom, });

                info.View.ExpandMasterRow(info.View.FocusedRowHandle, nameof(ItemBom.HalfFinished));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #endregion

        #region TEST
        //private void BomManagementMaterials_ValidateRow(object sender, ValidateRowEventArgs e)
        //{
        //    try
        //    {
        //        GridView view = sender as GridView;
        //        DetailBomMt row = view.GetRow(view.FocusedRowHandle) as DetailBomMt;
        //        BaseView parent = view.ParentView;
        //        var rowParent = parent.GetRow(view.SourceRowHandle);

        //        if (row.Quantity > 0 && string.IsNullOrEmpty(row.IdItemBcn) == false)
        //        {
        //            (rowParent as ItemBom).Materials.Add(new DetailBomMt());
        //            grdBomRefreshAndExpand();

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}


        private void BomManagementMaterials_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {

            try
            {
                decimal qty = 0;
                GridView view = sender as GridView;
                DetailBomMt row = view.GetRow(view.FocusedRowHandle) as DetailBomMt;

                BaseView parent = view.ParentView;
                var rowParent = parent.GetRow(view.SourceRowHandle);

                switch (view.FocusedColumn.FieldName)
                {
                    case nameof(DetailBomMt.IdItemBcn):
                        //No se pueden repetir materiales 
                        var idItemMt = e.Value.ToString();

                        object exist = null;
                        if (rowParent.GetType().BaseType == typeof(ItemBom) || rowParent.GetType() == typeof(ItemBom))
                        {
                            exist = (rowParent as ItemBom).Materials.Where(a => a.IdItemBcn != null && a.IdItemBcn.Equals(idItemMt)).FirstOrDefault();
                        }
                        else if (rowParent.GetType().BaseType == typeof(DetailBomHf) || rowParent.GetType() == typeof(DetailBomHf))
                        {
                            exist = (rowParent as DetailBomHf).DetailItemBom.Materials.Where(a => a.IdItemBcn != null && a.IdItemBcn.Equals(idItemMt)).FirstOrDefault();
                        }


                        if (exist == null)
                        {
                            var itemMt = _itemsMtList.Where(a => a.IdItemBcn.Equals(idItemMt)).Single().Clone();
                            row.Item = itemMt;
                        }
                        else
                        {
                            e.Valid = false;
                        }

                        break;

                    case nameof(DetailBomMt.Quantity):
                        //Si se indica una cantidad que es diferente al cálculo del resto de elementos, ésta tiene prioridad y se limpían el resto
                        qty = (row.Length ?? 0) *
                            (row.Width ?? 0) *
                            (row.Height ?? 0) *
                            (row.Density ?? 0) *
                            (row.NumberOfParts ?? 0) *
                            (row.Coefficient1 ?? 0) *
                            (row.Coefficient2 ?? 0) *
                            (row.Scrap ?? 0);

                        if (qty != (e.Value == null ? 0 : (decimal)e.Value))
                        {
                            row.Length = null;
                            row.Width = null;
                            row.Height = null;
                            row.Density = null;
                            row.NumberOfParts = null;
                            row.Scrap = null;
                            row.Coefficient1 = null;
                            row.Coefficient2 = null;
                            row.Scrap = null;
                        }

                        break;

                    case nameof(DetailBomMt.Length):

                        qty = (e.Value == null ? 0 : (decimal)e.Value) *
                            (row.Width ?? 0) *
                            (row.Height ?? 0) *
                            (row.Density ?? 0) *
                            (row.NumberOfParts ?? 0) *
                            (row.Coefficient1 ?? 0) *
                            (row.Coefficient2 ?? 0) *
                            (row.Scrap ?? 0);

                        row.Quantity = qty;
                        break;

                    case nameof(DetailBomMt.Width):

                        qty = (row.Length ?? 0) *
                            (e.Value == null ? 0 : (decimal)e.Value) *
                            (row.Height ?? 0) *
                            (row.Density ?? 0) *
                            (row.NumberOfParts ?? 0) *
                            (row.Coefficient1 ?? 0) *
                            (row.Coefficient2 ?? 0) *
                            (row.Scrap ?? 0);

                        row.Quantity = qty;

                        break;

                    case nameof(DetailBomMt.Height):

                        qty = (row.Length ?? 0) *
                            (row.Width ?? 0) *
                            (e.Value == null ? 0 : (decimal)e.Value) *
                            (row.Density ?? 0) *
                            (row.NumberOfParts ?? 0) *
                            (row.Coefficient1 ?? 0) *
                            (row.Coefficient2 ?? 0) *
                            (row.Scrap ?? 0);

                        row.Quantity = qty;

                        break;

                    case nameof(DetailBomMt.Density):

                        qty = (row.Length ?? 0) *
                            (row.Width ?? 0) *
                            (row.Height ?? 0) *
                            (e.Value == null ? 0 : (decimal)e.Value) *
                            (row.NumberOfParts ?? 0) *
                            (row.Coefficient1 ?? 0) *
                            (row.Coefficient2 ?? 0) *
                            (row.Scrap ?? 0);

                        row.Quantity = qty;

                        break;

                    case nameof(DetailBomMt.NumberOfParts):

                        //Me hace cosas raras en el parse + valdiación y tengo que controlarlo a mano
                        int numParts;
                        if (int.TryParse(e.Value.ToString(), out numParts) == false)
                        {
                            e.Valid = false;
                        }
                        else
                        {
                            qty = (row.Length ?? 0) *
                            (row.Width ?? 0) *
                            (row.Height ?? 0) *
                            (row.Density ?? 0) *
                            numParts *
                            (row.Coefficient1 ?? 0) *
                            (row.Coefficient2 ?? 0) *
                            (row.Scrap ?? 0);
                        }

                        row.Quantity = qty;

                        break;

                    case nameof(DetailBomMt.Coefficient1):

                        qty = (row.Length ?? 0) *
                            (row.Width ?? 0) *
                            (row.Height ?? 0) *
                            (row.Density ?? 0) *
                            (row.NumberOfParts ?? 0) *
                            (e.Value == null ? 0 : (decimal)e.Value) *
                            (row.Coefficient2 ?? 0) *
                            (row.Scrap ?? 0);

                        row.Quantity = qty;

                        break;

                    case nameof(DetailBomMt.Coefficient2):

                        qty = (row.Length ?? 0) *
                            (row.Width ?? 0) *
                            (row.Height ?? 0) *
                            (row.Density ?? 0) *
                            (row.NumberOfParts ?? 0) *
                            (row.Coefficient1 ?? 0) *
                            (e.Value == null ? 0 : (decimal)e.Value) *
                            (row.Scrap ?? 0);

                        row.Quantity = qty;

                        break;

                    case nameof(DetailBomMt.Scrap):

                        qty = (row.Length ?? 0) *
                            (row.Width ?? 0) *
                            (row.Height ?? 0) *
                            (row.Density ?? 0) *
                            (row.NumberOfParts ?? 0) *
                            (row.Coefficient1 ?? 0) *
                            (row.Coefficient2 ?? 0) *
                            (e.Value == null ? 0 : (decimal)e.Value);

                        row.Quantity = qty;

                        break;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BomManagementHardwares_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                DetailBomHw row = view.GetRow(view.FocusedRowHandle) as DetailBomHw;

                BaseView parent = view.ParentView;
                var rowParent = parent.GetRow(view.SourceRowHandle);

                switch (view.FocusedColumn.FieldName)
                {
                    case nameof(DetailBomHw.IdItemBcn):
                        //No se pueden repetir materiales 
                        var idItemHw = e.Value.ToString();

                        object exist = null;
                        if (rowParent.GetType().BaseType == typeof(ItemBom) || rowParent.GetType() == typeof(ItemBom))
                        {
                            exist = (rowParent as ItemBom).Hardwares.Where(a => a.IdItemBcn != null && a.IdItemBcn.Equals(idItemHw)).FirstOrDefault();
                        }

                        if (exist == null)
                        {
                            var itemHw = _itemsHwList.Where(a => a.IdItemBcn.Equals(idItemHw)).Single().Clone();
                            row.Item = itemHw;
                        }
                        else
                        {
                            e.Valid = false;
                        }

                        break;
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void BomManagementHalfFinished_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                GridView view = sender as GridView;
                DetailBomHf row = view.GetRow(view.FocusedRowHandle) as DetailBomHf;

                BaseView parent = view.ParentView;
                var rowParent = parent.GetRow(view.SourceRowHandle);

                //Nota: A veces no se reajusta automáticamente el tamaño cuando se van desplegando los detalles. No sé por qué, pero poniendo un valor alto el grid se va reajustando correctamemte. 
                //Quizás en alguna futura versión de DexExpress se arregla esto.
                view.DetailHeight = 1000;

                switch (view.FocusedColumn.FieldName)
                {
                    case nameof(DetailBomHf.DetailItemBom) + "." + nameof(ItemBom.IdItemBcn):

                        view.CollapseMasterRow(view.FocusedRowHandle);

                        //No se pueden repetir los semielaborados 
                        string idItemHf = e.Value.ToString();

                        object exist = null;

                        if (rowParent.GetType().BaseType == typeof(ItemBom) || rowParent.GetType() == typeof(ItemBom))
                        {
                            
                            exist = (rowParent as ItemBom).HalfFinished.Where(a => a.DetailItemBom != null && a.DetailItemBom.IdItemBcn.Equals(idItemHf)).FirstOrDefault();
                        }

                        if (exist == null)
                        {
                            var itemBom = GlobalSetting.ItemBomService.GetItemSupplierBom(idItemHf, (rowParent as ItemBom).IdSupplier);

                            row.DetailItemBom = itemBom;
                            row.IdBomDetail = itemBom.IdBom;
                            row.Quantity = 1;
                        }
                        else
                        {
                            e.Valid = false;
                        }


                        


                        break;
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

        private void BomManagementHalfFinished_ShownEditor(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                GridView view = sender as GridView;
                DetailBomHf row = view.GetRow(view.FocusedRowHandle) as DetailBomHf;

                BaseView parent = view.ParentView;
                var rowParent = parent.GetRow(view.SourceRowHandle);

                switch (view.FocusedColumn.FieldName)
                {
                    case nameof(DetailBomHf.DetailItemBom) + "." + nameof(ItemBom.IdItemBcn):
                        SearchLookUpEdit riItemsHf = (SearchLookUpEdit)view.ActiveEditor;
                        var supplierItemsHf = GlobalSetting.ItemHfService.GetISupplierHfItems((rowParent as ItemBom).IdSupplier);
                        var ids= supplierItemsHf.Select(a => a.IdItemBcn).ToList();
                        riItemsHf.Properties.DataSource = ids;
                        break;
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

        #region Test 2
        public void NavigateDetails(ColumnView inspectedView)
        {
            if (inspectedView == null) return;
            // Prevent excessive visual updates. 
            inspectedView.BeginUpdate();
            try
            {



                GridView gridView = inspectedView as GridView;
                // Get the number of data rows in the View. 
                int dataRowCount;
                if (gridView == null)
                    dataRowCount = inspectedView.RowCount;
                else
                    dataRowCount = gridView.DataRowCount;
                // Traverse View's rows. 
                for (int rowHandle = 0; rowHandle < dataRowCount; rowHandle++)
                {
                    // Place your code here to process the current row. 
                    // ...                     
                    if (gridView != null)
                    {
                        if(gridView.LevelName == nameof(ItemBom.HalfFinishedNM))
                        {
                            //xgrdItemBom.FocusedView = gridView.get;
                            var x = gridView.GetDetailView(rowHandle, 0);
                            //xgrdItemBom.FocusedView = x ;
                            //(x as GridView).ExpandMasterRow(0);
                            gridView.ExpandMasterRow(0);
                        }

                        // Get the number of master-detail relationships for the current row. 
                        int relationCount = gridView.GetRelationCount(rowHandle);
                        // Iterate through master-detail relationships. 
                        for (int relationIndex = 0; relationIndex < relationCount; relationIndex++)
                        {
                            // Store expansion status of the corresponding detail View. 
                            bool wasExpanded = gridView.GetMasterRowExpandedEx(rowHandle, relationIndex);
                            // Expand the detail View. 
                            if (!wasExpanded)
                                gridView.SetMasterRowExpandedEx(rowHandle, relationIndex, true);
                            // Navigate the detail View. 
                            NavigateDetails((ColumnView)gridView.GetDetailView(rowHandle, relationIndex));
                            // Restore the row's expansion status. 
                            gridView.SetMasterRowExpandedEx(rowHandle, relationIndex, wasExpanded);
                        }
                    }
                }
            }
            finally
            {
                // Enable visual updates. 
                inspectedView.EndUpdate();
            }
        }

        #endregion


    }
}
