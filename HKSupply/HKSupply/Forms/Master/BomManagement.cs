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
using System.Data.Entity;
using System.ComponentModel;
using DevExpress.XtraEditors.Controls;
using System.Drawing;

namespace HKSupply.Forms.Master
{
    public partial class BomManagement : RibbonFormBase
    {

        #region Constants
        private const string EDIT_COLUMN = "EditHf";
        private const string DYNAMIC_PANEL_DRAWING = "DynamicPanelDrawing";
        #endregion


        #region Private Members
        List<ItemEy> _itemsEyList;
        List<ItemHf> _itemsHfList;
        List<ItemHf> _itemsHfDetailBomList;
        List<ItemMt> _itemsMtList;
        List<ItemHw> _itemsHwList;

        object _currentItem;
        List<ItemBom> _itemBomList = new List<ItemBom>();
        List<ItemDoc> _itemLastDocsList;

        List<Supplier> _suppliersList;
        List<BomBreakdown> _bomBreakdownList;
        List<SupplierFactoryCoeff> _supplierFactoryCoeffList;

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
                SetUpGrdSummaryBom();
                SetUpGrdSummaryUnitBom();

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

                if (IsValidBom() == false)
                    return;

                DialogResult result = MessageBox.Show(GlobalSetting.ResManager.GetString("SaveChanges"), "", MessageBoxButtons.YesNo);

                if (result != DialogResult.Yes)
                    return;

                if (ShowHalfFinishedMessageInfo() == false)
                    return;

                Cursor = Cursors.WaitCursor;

                DeleteLastRowIfEmpty();

                res = EditBom();

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

        public override void BbiSaveLayout_ItemClick(object sender, ItemClickEventArgs e)
        {
            base.BbiSaveLayout_ItemClick(sender, e);

            try
            {
                var result = XtraInputBox.Show("Enter Layout Name", "Save Layout", string.Empty);
                if (string.IsNullOrEmpty(result) == false)
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
            if (gridViewSummaryBom.DataRowCount == 0)
            {
                MessageBox.Show(GlobalSetting.ResManager.GetString("NoDataSelected"));
                return;
            }

            base.bbiExportCsv_ItemClick(sender, e);

            try
            {
                if (string.IsNullOrEmpty(ExportCsvFile) == false)
                {
                    gridViewSummaryBom.ExportToCsv(ExportCsvFile);

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

            if (gridViewSummaryBom.DataRowCount == 0)
            {
                MessageBox.Show(GlobalSetting.ResManager.GetString("NoDataSelected"));
                return;
            }

            base.bbiExportExcel_ItemClick(sender, e);

            try
            {
                if (string.IsNullOrEmpty(ExportExcelFile) == false)
                {

                    gridViewSummaryBom.OptionsPrint.PrintFooter = false;
                    gridViewSummaryBom.ExportToXlsx(ExportExcelFile);

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
                ShowAddBomSupplierAndCopyBom(false);

                dockPanelGrdBom.Select();

                LoadItemsListEy();
                LoadItemsListHf();
                //LoadItemsListMt();
                //LoadItemsListHw();

                dockPanelDrawing.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide;
                dockPanelPdfColor.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide;
                dockPanelPhoto.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide;
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

        private void SbCopyBom_Click(object sender, EventArgs e)
        {
            try
            {
                if (_itemBomList != null && _itemBomList.Count() > 0)
                    OpenSelectSuppliersForm2Copy();
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

        //private void GridViewItemsMt_DoubleClick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //var selectedSuppliers = OpenSelectSuppliersForm();

        //        //if (selectedSuppliers.Count == 0)
        //        //{
        //        //    XtraMessageBox.Show("No selected Supplier", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        //    return;
        //        //}



        //        //GridView view = sender as GridView;

        //        //GridHitInfo hitInfo = view.CalcHitInfo((e as MouseEventArgs).Location);
        //        //if (hitInfo.InRowCell)
        //        //{
        //        //    ItemMt itemMt = view.GetRow(view.FocusedRowHandle) as ItemMt;

        //        //    foreach(var supplier in selectedSuppliers)
        //        //    {
        //        //        AddRawMaterial(itemMt, supplier);
        //        //    }


        //        //    LoadBomTreeView();
        //        //    LoadPlainBom();
        //        //}

        //        GridView view = sender as GridView;
        //        GridHitInfo hitInfo = view.CalcHitInfo((e as MouseEventArgs).Location);

        //        if (hitInfo.InRowCell)
        //        {
        //            ItemMt itemMt = view.GetRow(view.FocusedRowHandle) as ItemMt;

        //            GridView activeBomView = xgrdItemBom.FocusedView as GridView;

        //            if (activeBomView == null)
        //            {
        //                XtraMessageBox.Show("Select BOM node first", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                return;
        //            }

        //            //------------------------------------------------------------------------

        //            var rowtmp = activeBomView.GetRow(activeBomView.FocusedRowHandle);

        //            object rowParent;

        //            if (rowtmp.GetType().BaseType == typeof(ItemBom) || rowtmp.GetType() == typeof(ItemBom) ||
        //                rowtmp.GetType().BaseType == typeof(DetailBomHf) || rowtmp.GetType() == typeof(DetailBomHf)
        //                )
        //            {
        //                rowParent = rowtmp;
        //            }
        //            else
        //            {
        //                BaseView parent = activeBomView.ParentView;

        //                rowParent = parent.GetRow(activeBomView.SourceRowHandle);
        //            }


        //            if (rowParent.GetType().BaseType == typeof(ItemBom) || rowParent.GetType() == typeof(ItemBom))
        //            {
        //                var rawMaterial = (rowParent as ItemBom).Materials.Where(a => a.IdItemBcn != null && a.IdItemBcn.Equals(itemMt.IdItemBcn));

        //                if (rawMaterial == null || rawMaterial.Count() == 0)
        //                {
        //                    DetailBomMt detail = new DetailBomMt()
        //                    {
        //                        IdBom = (rowParent as ItemBom).IdBom,
        //                        IdItemBcn = itemMt.IdItemBcn,
        //                        Item = itemMt,
        //                        Quantity = 0,
        //                        Scrap = 0,
        //                    };

        //                    (rowParent as ItemBom).Materials.Add(detail);

        //                    //activeBomView.RefreshData(); //--> OK
        //                    activeBomView.BeginDataUpdate();
        //                    GrdBomRefreshAndExpand();
        //                    activeBomView.EndDataUpdate();


        //                    //grdBomRefreshAndExpand();
        //                    //xgrdItemBom.RefreshDataSource();
        //                    //NavigateDetails(gridViewItemBom);
        //                    //gridViewItemBom.ExpandMasterRow(0, nameof(ItemBom.Hardwares)); --> Medio OK
        //                    //xgrdItemBom.FocusedView = gridViewItemBom.GetDetailView(gridViewItemBom.FocusedRowHandle, 1); --> Medio OK


        //                }
        //                else
        //                {
        //                    XtraMessageBox.Show($"Raw Material already exist for supplier {(rowParent as ItemBom).IdSupplier}");
        //                }
        //            }
        //            else if (rowParent.GetType().BaseType == typeof(DetailBomHf) || rowParent.GetType() == typeof(DetailBomHf))
        //            {
        //                //var rawMaterial = (rowParent as ItemBom).Materials.Where(a => a.IdItemBcn.Equals(itemMt.IdItemBcn));
        //                var rawMaterial = (rowParent as DetailBomHf).DetailItemBom.Materials.Where(a => a.IdItemBcn != null && a.IdItemBcn.Equals(itemMt.IdItemBcn)); 
        //                if (rawMaterial == null || rawMaterial.Count() == 0)
        //                {
        //                    DetailBomMt detail = new DetailBomMt()
        //                    {
        //                        IdBom = (rowParent as DetailBomHf).IdBom,
        //                        IdItemBcn = itemMt.IdItemBcn,
        //                        Item = itemMt,
        //                        Quantity = 0,
        //                        Scrap = 0,
        //                    };

        //                    (rowParent as DetailBomHf).DetailItemBom.Materials.Add(detail);

        //                    //activeBomView.RefreshData(); //--> OK
        //                    activeBomView.BeginDataUpdate();
        //                    GrdBomRefreshAndExpand();
        //                    activeBomView.EndDataUpdate();


        //                    //xgrdItemBom.RefreshDataSource();
        //                    //ExpandAllRows(activeBomView);

        //                    //grdBomRefreshAndExpand();
        //                    //xgrdItemBom.RefreshDataSource();
        //                    //NavigateDetails(gridViewItemBom);
        //                    //gridViewItemBom.ExpandMasterRow(0, nameof(ItemBom.Hardwares));
        //                    //xgrdItemBom.FocusedView = gridViewItemBom.GetDetailView(gridViewItemBom.FocusedRowHandle, 1);


        //                }
        //                else
        //                {
        //                    XtraMessageBox.Show($"Raw Material already exist for supplier {(rowParent as ItemBom).IdSupplier}");
        //                }
        //            }



        //            //switch (activeBomView.LevelName)
        //            //{
        //            //    case nameof(ItemBom.Materials):


        //                //        DetailBomMt row = activeBomView.GetRow(activeBomView.FocusedRowHandle) as DetailBomMt;
        //                //        BaseView parent = activeBomView.ParentView;
        //                //        var rowParent = parent.GetRow(activeBomView.SourceRowHandle);

        //                //        var rawMaterial = (rowParent as ItemBom).Materials.Where(a => a.IdItemBcn.Equals(itemMt.IdItemBcn));

        //                //        if (rawMaterial == null || rawMaterial.Count() == 0)
        //                //        {
        //                //            DetailBomMt detail = new DetailBomMt()
        //                //            {
        //                //                IdBom = (rowParent as ItemBom).IdBom,
        //                //                IdItemBcn = itemMt.IdItemBcn,
        //                //                Item = itemMt,
        //                //                Quantity = 0,
        //                //                Scrap = 0,
        //                //            };

        //                //            (rowParent as ItemBom).Materials.Add(detail);

        //                //            grdBomRefreshAndExpand();

        //                //            //------------------------------ PRUEBAS -----------------------------------//
        //                //            //xgrdItemBom.FocusedView = parent;
        //                //            gridViewItemBom.FocusedRowHandle = 0;
        //                //            GridView childView1 = gridViewItemBom.GetDetailView(0, 1) as GridView;

        //                //            childView1.FocusedRowHandle = 0;
        //                //            childView1.ExpandMasterRow(0);
        //                //            GridView childView2 = childView1.GetDetailView(0, 0) as GridView;


        //                //        }
        //                //        else
        //                //        {
        //                //            XtraMessageBox.Show($"Raw Material already exist for supplier {(rowParent as ItemBom).IdSupplier}");
        //                //        }

        //                //        break;

        //                //    default:
        //                //        XtraMessageBox.Show("Select BOM node first", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                //        break;

        //                //}
        //        }


        //    }
        //    catch(Exception ex)
        //    {
        //        XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //private void GridViewItemsHw_DoubleClick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        var selectedSuppliers = OpenSelectSuppliersForm();

        //        if (selectedSuppliers.Count == 0)
        //        {
        //            XtraMessageBox.Show("No selected Supplier", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            return;
        //        }

        //        GridView view = sender as GridView;

        //        GridHitInfo hitInfo = view.CalcHitInfo((e as MouseEventArgs).Location);
        //        if (hitInfo.InRowCell)
        //        {
        //            ItemHw itemHw = view.GetRow(view.FocusedRowHandle) as ItemHw;

        //            foreach (var supplier in selectedSuppliers)
        //            {
        //                AddHardware(itemHw, supplier);
        //            }

        //            LoadBomTreeView();
        //            LoadSummaryBom();
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

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
                        (e.View as GridView).Columns[nameof(DetailBomMt.BomBreakdown)].Visible = false;
                        
                        //Columnas que no queremos que aparezan en el Column Chooser
                        (e.View as GridView).Columns[nameof(DetailBomMt.IdBom)].OptionsColumn.ShowInCustomizationForm = false;
                        (e.View as GridView).Columns[nameof(DetailBomMt.Item)].OptionsColumn.ShowInCustomizationForm = false;
                        (e.View as GridView).Columns[nameof(DetailBomMt.BomBreakdown)].OptionsColumn.ShowInCustomizationForm = false;

                        //Seteamos el tamaño de las columnas
                        (e.View as GridView).Columns[nameof(DetailBomMt.IdItemBcn)].Width = 150;
                        (e.View as GridView).Columns[nameof(DetailBomMt.Length)].Width = 80;
                        (e.View as GridView).Columns[nameof(DetailBomMt.Width)].Width = 80;
                        (e.View as GridView).Columns[nameof(DetailBomMt.Height)].Width = 80;
                        (e.View as GridView).Columns[nameof(DetailBomMt.Density)].Width = 60;
                        (e.View as GridView).Columns[nameof(DetailBomMt.NumberOfParts)].Width = 105;
                        (e.View as GridView).Columns[nameof(DetailBomMt.Scrap)].Width = 60;
                        (e.View as GridView).Columns[nameof(DetailBomMt.IdBomBreakdown)].Width = 130;

                        //Captions
                        (e.View as GridView).Columns[nameof(DetailBomMt.Length)].Caption = "Length (mm)";
                        (e.View as GridView).Columns[nameof(DetailBomMt.Width)].Caption = "Width (mm)";
                        (e.View as GridView).Columns[nameof(DetailBomMt.Height)].Caption = "Height (mm)";
                        (e.View as GridView).Columns[nameof(DetailBomMt.IdBomBreakdown)].Caption = "Breakdown";



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
                        (e.View as GridView).Columns[nameof(DetailBomMt.Coefficient1)].DisplayFormat.FormatString = "n6";
                        (e.View as GridView).Columns[nameof(DetailBomMt.Coefficient2)].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        (e.View as GridView).Columns[nameof(DetailBomMt.Coefficient2)].DisplayFormat.FormatString = "n6";


                        (e.View as GridView).Columns[nameof(DetailBomMt.Scrap)].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        (e.View as GridView).Columns[nameof(DetailBomMt.Scrap)].DisplayFormat.FormatString = "n2";

                        (e.View as GridView).Columns[nameof(DetailBomMt.Quantity)].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        (e.View as GridView).Columns[nameof(DetailBomMt.Quantity)].DisplayFormat.FormatString = "n2";


                        //Orden de las columnas
                        int orderColMt = 0;
                        (e.View as GridView).Columns[nameof(DetailBomMt.IdItemBcn)].VisibleIndex = orderColMt++;
                        (e.View as GridView).Columns[$"{nameof(DetailBomMt.Item)}.{nameof(ItemMt.ItemDescription)}"].VisibleIndex = orderColMt++;
                        (e.View as GridView).Columns[nameof(DetailBomMt.IdBomBreakdown)].VisibleIndex = orderColMt++;
                        (e.View as GridView).Columns[$"{nameof(DetailBomMt.Item)}.{nameof(ItemMt.Unit)}"].VisibleIndex = orderColMt++;
                        (e.View as GridView).Columns[nameof(DetailBomMt.Length)].VisibleIndex = orderColMt++;
                        (e.View as GridView).Columns[nameof(DetailBomMt.Width)].VisibleIndex = orderColMt++;
                        (e.View as GridView).Columns[nameof(DetailBomMt.Height)].VisibleIndex = orderColMt++;
                        (e.View as GridView).Columns[nameof(DetailBomMt.Density)].VisibleIndex = orderColMt++;
                        (e.View as GridView).Columns[nameof(DetailBomMt.NumberOfParts)].VisibleIndex = orderColMt++;
                        (e.View as GridView).Columns[nameof(DetailBomMt.Coefficient1)].VisibleIndex = orderColMt++;
                        (e.View as GridView).Columns[nameof(DetailBomMt.Coefficient2)].VisibleIndex = orderColMt++;
                        (e.View as GridView).Columns[nameof(DetailBomMt.Scrap)].VisibleIndex = orderColMt++;
                        (e.View as GridView).Columns[nameof(DetailBomMt.Quantity)].VisibleIndex = orderColMt++;

                        //Events
                        (e.View as GridView).CellValueChanged += GrdBomView_CellValueChanged;
                        (e.View as GridView).ValidatingEditor += BomManagementMaterials_ValidatingEditor;

                        //Agregamos los Summary
                        (e.View as GridView).OptionsView.ShowFooter = true;
                        (e.View as GridView).Columns[nameof(DetailBomMt.Quantity)].Summary.Add(SummaryItemType.Sum, nameof(DetailBomMt.Quantity), "{0:n}");
                        //(e.View as GridView).Columns[nameof(DetailBomMt.Scrap)].Summary.Add(SummaryItemType.Sum, nameof(DetailBomMt.Scrap), "{0:n}");

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
                        (e.View as GridView).Columns[nameof(DetailBomHw.BomBreakdown)].Visible = false;

                        //Columnas que no queremos que aparezan en el Column Chooser
                        (e.View as GridView).Columns[nameof(DetailBomHw.IdBom)].OptionsColumn.ShowInCustomizationForm = false;
                        (e.View as GridView).Columns[nameof(DetailBomHw.Item)].OptionsColumn.ShowInCustomizationForm = false;
                        (e.View as GridView).Columns[nameof(DetailBomHw.BomBreakdown)].OptionsColumn.ShowInCustomizationForm = false;

                        //Seteamos el tamaño de las columnas
                        (e.View as GridView).Columns[nameof(DetailBomHw.IdItemBcn)].Width = 150;
                        (e.View as GridView).Columns[nameof(DetailBomHw.IdBomBreakdown)].Width = 130;

                        //Captions
                        (e.View as GridView).Columns[nameof(DetailBomHw.IdBomBreakdown)].Caption = "Breakdown";

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
                        int orderColHw = 0;
                        (e.View as GridView).Columns[nameof(DetailBomHw.IdItemBcn)].VisibleIndex = orderColHw++;
                        (e.View as GridView).Columns[$"{nameof(DetailBomHw.Item)}.{nameof(ItemHw.ItemDescription)}"].VisibleIndex = orderColHw++;
                        (e.View as GridView).Columns[nameof(DetailBomHw.IdBomBreakdown)].VisibleIndex = orderColHw++;
                        (e.View as GridView).Columns[$"{nameof(DetailBomHw.Item)}.{nameof(ItemHw.Unit)}"].VisibleIndex = orderColHw++;
                        (e.View as GridView).Columns[nameof(DetailBomHw.Quantity)].VisibleIndex = orderColHw++;
                        (e.View as GridView).Columns[nameof(DetailBomHw.Scrap)].VisibleIndex = orderColHw++;

                        //Events
                        (e.View as GridView).CellValueChanged += GrdBomView_CellValueChanged;
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

                        (e.View as GridView).DetailHeight = 1000;

                        ////Ocultamos todas las columnas menos el item que no nos interesan
                        //foreach(GridColumn col in (e.View as GridView).Columns)
                        //{
                        //    if (col.FieldName != nameof(ItemBom.IdItemBcn))
                        //        col.Visible = false;
                        //}

                        ////Seteamos el tamaño de las columnas
                        //(e.View as GridView).Columns[nameof(ItemBom.IdItemBcn)].Width = 150;

                        ////agregamos la columna de descripcion
                        //GridColumn colDescriptionItemHf = new GridColumn()
                        //{
                        //    Caption = GlobalSetting.ResManager.GetString("ItemDescription"),
                        //    Visible = true,
                        //    FieldName = $"{nameof(ItemBom.Item)}.{nameof(ItemHf.ItemDescription)}",
                        //    Width = 300
                        //};

                        //(e.View as GridView).Columns.Add(colDescriptionItemHf);

                        //------------------------------------------------------------------------------------------//
                        //Ocultamos las columnas que no nos interesan
                        foreach (GridColumn col in (e.View as GridView).Columns)
                        {
                            if (col.FieldName != nameof(DetailBomHf.Quantity))
                                col.Visible = false;
                        }

                        //agregamos la columna de descripcion
                        GridColumn colIdItemHfDet = new GridColumn()
                        {
                            Caption = GlobalSetting.ResManager.GetString("ItemDescription"),
                            Visible = true,
                            FieldName = $"{nameof(DetailBomHf.DetailItemBom)}.{nameof(ItemBom.IdItemBcn)}",
                            Width = 300,
                        };

                        (e.View as GridView).Columns.Add(colIdItemHfDet);

                        //No queremos que se muestra la columna, pero al ser listas tenemos que generarlo para que se monte el hijo con sus tabs
                        GridColumn ListMaterialsDet = new GridColumn()
                        {
                            Caption = " ",
                            Visible = false,
                            FieldName = $"{nameof(DetailBomHf.DetailItemBom)}.{nameof(ItemBom.Materials)}",
                            Width = 300
                        };
                        (e.View as GridView).Columns.Add(ListMaterialsDet);

                        GridColumn ListHardwaresDet = new GridColumn()
                        {
                            Caption = " ",
                            Visible = false,
                            FieldName = $"{nameof(DetailBomHf.DetailItemBom)}.{nameof(ItemBom.Hardwares)}",
                            Width = 300
                        };
                        (e.View as GridView).Columns.Add(ListHardwaresDet);

                        GridColumn ListHalfFinishedDet = new GridColumn()
                        {
                            Caption = " ",
                            Visible = false,
                            FieldName = $"{nameof(DetailBomHf.DetailItemBom)}.{nameof(ItemBom.HalfFinished)}",
                            Width = 300
                        };
                        (e.View as GridView).Columns.Add(ListHalfFinishedDet);

                        //Formats
                        (e.View as GridView).Columns[nameof(DetailBomHf.Quantity)].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        (e.View as GridView).Columns[nameof(DetailBomHf.Quantity)].DisplayFormat.FormatString = "n2";

                        //Columns order
                        (e.View as GridView).Columns[$"{nameof(DetailBomHf.DetailItemBom)}.{nameof(ItemBom.IdItemBcn)}"].VisibleIndex = 0;
                        (e.View as GridView).Columns[nameof(DetailBomHf.Quantity)].VisibleIndex = 1;

                        //---------------------------------------------------------------------------------------------//

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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GrdBomView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                view.UpdateSummary();
                gridViewSummaryBom.UpdateSummary();

                LoadBomTreeView();
                LoadSummaryBom();
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

                if (row.IdBomDetail > 0)
                {
                    OpenEditHfBom(row.DetailItemBom);
                    activeView.CollapseMasterRow(activeView.FocusedRowHandle);
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
                sbCopyBom.Click += SbCopyBom_Click;
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
                dockPanelGrdBom.Options.ShowCloseButton = false;
                dockPanelTreeBom.Options.ShowCloseButton = false;
                dockPanelPlainBom.Options.ShowCloseButton = false;
                dockPanelDrawing.Options.ShowCloseButton = false;
                dockPanelPdfColor.Options.ShowCloseButton = false;
                dockPanelPhoto.Options.ShowCloseButton = false;
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
                //gridViewItemsEy.Columns[nameof(ItemEy.IdDefaultSupplier)].GroupIndex = 2;

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
                //gridViewItemsHf.Columns[nameof(ItemHf.IdDefaultSupplier)].GroupIndex = 2;

                gridViewItemsHf.DoubleClick += GridViewItemsHf_DoubleClick;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //private void SetUpGrdItemsMt()
        //{
        //    try
        //    {
        //        //Ocultamos el nombre de las columnas agrupadas
        //        gridViewItemsMt.GroupFormat = "[#image]{1} {2}";

        //        //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
        //        gridViewItemsMt.OptionsView.ColumnAutoWidth = false;
        //        gridViewItemsMt.HorzScrollVisibility = ScrollVisibility.Auto;

        //        //Hacer todo el grid no editable
        //        gridViewItemsMt.OptionsBehavior.Editable = false;

        //        //Columns definition
        //        GridColumn colIdItemBcn = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("ItemBCN"), Visible = true, FieldName = nameof(ItemMt.IdItemBcn), Width = 160 };
        //        GridColumn colItemDescription = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("ItemDescription"), Visible = true, FieldName = nameof(ItemMt.ItemDescription), Width = 300 };
        //        GridColumn colIdMatTypeL1 = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("MatTypeL1"), Visible = false, FieldName = nameof(ItemMt.IdMatTypeL1), Width = 100 };
        //        GridColumn colIdMatTypeL2 = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("MatTypeL2"), Visible = false, FieldName = nameof(ItemMt.IdMatTypeL2), Width = 100 };
        //        GridColumn colIdMatTypeL3 = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("MatTypeL3"), Visible = false, FieldName = nameof(ItemMt.IdMatTypeL3), Width = 100 };

        //        //Add columns to grid root view
        //        gridViewItemsMt.Columns.Add(colIdItemBcn);
        //        gridViewItemsMt.Columns.Add(colItemDescription);
        //        gridViewItemsMt.Columns.Add(colIdMatTypeL1);
        //        gridViewItemsMt.Columns.Add(colIdMatTypeL2);
        //        gridViewItemsMt.Columns.Add(colIdMatTypeL3);

        //        //Grouping
        //        gridViewItemsMt.OptionsView.ShowGroupPanel = false;

        //        gridViewItemsMt.Columns[nameof(ItemMt.IdMatTypeL1)].GroupIndex = 0;
        //        gridViewItemsMt.Columns[nameof(ItemMt.IdMatTypeL2)].GroupIndex = 1;
        //        //gridViewItemsMt.Columns[nameof(ItemMt.IdMatTypeL3)].GroupIndex = 2;

        //        //Events
        //        //gridViewItemsMt.DoubleClick += GridViewItemsMt_DoubleClick;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //private void SetUpGrdItemsHw()
        //{
        //    try
        //    {
        //        //Ocultamos el nombre de las columnas agrupadas
        //        gridViewItemsHw.GroupFormat = "[#image]{1} {2}";

        //        //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
        //        gridViewItemsHw.OptionsView.ColumnAutoWidth = false;
        //        gridViewItemsHw.HorzScrollVisibility = ScrollVisibility.Auto;

        //        //Hacer todo el grid no editable
        //        gridViewItemsHw.OptionsBehavior.Editable = false;

        //        //Columns definition
        //        GridColumn colIdItemBcn = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("ItemBCN"), Visible = true, FieldName = nameof(ItemHw.IdItemBcn), Width = 160 };
        //        GridColumn colItemDescription = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("ItemDescription"), Visible = true, FieldName = nameof(ItemHw.ItemDescription), Width = 300 };
        //        GridColumn colIdHwTypeL1 = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("MatTypeL1"), Visible = false, FieldName = nameof(ItemHw.IdHwTypeL1), Width = 100 };
        //        GridColumn colIdHwTypeL2 = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("MatTypeL2"), Visible = false, FieldName = nameof(ItemHw.IdHwTypeL2), Width = 100 };
        //        GridColumn colIdHwTypeL3 = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("MatTypeL3"), Visible = false, FieldName = nameof(ItemHw.IdHwTypeL3), Width = 100 };

        //        //Add columns to grid root view
        //        gridViewItemsHw.Columns.Add(colIdItemBcn);
        //        gridViewItemsHw.Columns.Add(colItemDescription);
        //        gridViewItemsHw.Columns.Add(colIdHwTypeL1);
        //        gridViewItemsHw.Columns.Add(colIdHwTypeL2);
        //        gridViewItemsHw.Columns.Add(colIdHwTypeL3);

        //        //Grouping
        //        gridViewItemsHw.OptionsView.ShowGroupPanel = false;

        //        gridViewItemsHw.Columns[nameof(ItemHw.IdHwTypeL1)].GroupIndex = 0;
        //        gridViewItemsHw.Columns[nameof(ItemHw.IdHwTypeL2)].GroupIndex = 1;
        //        //gridViewItemsHw.Columns[nameof(ItemHw.IdHwTypeL3)].GroupIndex = 2;

        //        //Events
        //        //gridViewItemsHw.DoubleClick += GridViewItemsHw_DoubleClick;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //private void SetUpGrdItemsHfDetail()
        //{
        //    try
        //    {
        //        //Ocultamos el nombre de las columnas agrupadas
        //        gridViewItemsHfDetail.GroupFormat = "[#image]{1} {2}";

        //        //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
        //        gridViewItemsHfDetail.OptionsView.ColumnAutoWidth = false;
        //        gridViewItemsHfDetail.HorzScrollVisibility = ScrollVisibility.Auto;

        //        //Hacer todo el grid no editable
        //        gridViewItemsHfDetail.OptionsBehavior.Editable = false;

        //        //Columns definition
        //        GridColumn colIdItemBcn = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("ItemBCN"), Visible = true, FieldName = nameof(ItemHf.IdItemBcn), Width = 160 };
        //        GridColumn colItemDescription = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("ItemDescription"), Visible = true, FieldName = nameof(ItemHf.ItemDescription), Width = 300 };
        //        GridColumn colModel = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Model"), Visible = true, FieldName = $"{nameof(ItemHf.Model)}.{nameof(Model.Description)}", Width = 10 };

        //        //Add columns to grid root view
        //        gridViewItemsHfDetail.Columns.Add(colIdItemBcn);
        //        gridViewItemsHfDetail.Columns.Add(colItemDescription);
        //        gridViewItemsHfDetail.Columns.Add(colModel);

        //        //Grouping
        //        gridViewItemsHfDetail.OptionsView.ShowGroupPanel = false;

        //        gridViewItemsHfDetail.Columns[$"{nameof(ItemHf.Model)}.{nameof(Model.Description)}"].GroupIndex = 0;

        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

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
                xgrdItemBom.ProcessGridKey += XgrdItemBom_ProcessGridKey;
                gridViewItemBom.PopupMenuShowing += GridViewItemBom_PopupMenuShowing;

                //Hide group panel
                gridViewItemBom.OptionsView.ShowGroupPanel = false;

                gridViewItemBom.Columns[nameof(ItemBom.IdItemBcn)].GroupIndex = 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetUpGrdSummaryBom()
        {
            try
            {
                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                gridViewSummaryBom.OptionsView.ColumnAutoWidth = false;
                gridViewSummaryBom.HorzScrollVisibility = ScrollVisibility.Auto;

                //Hacer todo el grid no editable
                gridViewSummaryBom.OptionsBehavior.Editable = false;

                //Hide group panel
                gridViewSummaryBom.OptionsView.ShowGroupPanel = false;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetUpGrdSummaryUnitBom()
        {
            try
            {
                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                gridViewSummaryUnitBom.OptionsView.ColumnAutoWidth = false;
                gridViewSummaryUnitBom.HorzScrollVisibility = ScrollVisibility.Auto;

                //Hacer todo el grid no editable
                gridViewSummaryUnitBom.OptionsBehavior.Editable = false;

                //Hide group panel
                gridViewSummaryUnitBom.OptionsView.ShowGroupPanel = false;

            }
            catch
            {
                throw;
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
                            LoadSummaryBom();
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

                                        if (row.Quantity > 0 && string.IsNullOrEmpty(row.IdItemBcn) == false && string.IsNullOrEmpty(row.IdBomBreakdown) == false)
                                        {
                                            if (rowParent.GetType().BaseType == typeof(ItemBom) || rowParent.GetType() == typeof(ItemBom))
                                            {
                                                (rowParent as ItemBom).Materials.Add(new DetailBomMt() { IdBom = (rowParent as ItemBom).IdBom });
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

                                        if (row.Quantity > 0 && string.IsNullOrEmpty(row.IdItemBcn) == false && string.IsNullOrEmpty(row.IdBomBreakdown) == false)
                                        {
                                            if (rowParent.GetType().BaseType == typeof(ItemBom) || rowParent.GetType() == typeof(ItemBom))
                                            {
                                                (rowParent as ItemBom).Hardwares.Add(new DetailBomHw() { IdBom = (rowParent as ItemBom).IdBom });
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
            catch (Exception ex)
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
                _bomBreakdownList = GlobalSetting.BomBreakdownService.GetBomBreakdowns();
                _supplierFactoryCoeffList = GlobalSetting.SupplierFactoryCoeffService.GetAllSupplierFactoryCoeff();

                _itemsMtList = GlobalSetting.ItemMtService.GetItems();
                _itemsHwList = GlobalSetting.ItemHwService.GetItems();
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
                //xgrdItemsHfDetail.DataSource = _itemsHfDetailBomList;
            }
            catch
            {
                throw;
            }
        }

        //private void LoadItemsListMt()
        //{
        //    try
        //    {
        //        xgrdItemsMt.DataSource = _itemsMtList;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        //private void LoadItemsListHw()
        //{
        //    try
        //    {
        //        xgrdItemsHw.DataSource = _itemsHwList;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        //private void LoadItemGridBom(ItemEy item)
        private void LoadItemGridBom(object item)
        {
            try
            {

                Cursor = Cursors.WaitCursor;

                _itemBomList.Clear();

                string idIdItemBcn = string.Empty;
                string idSupplier = string.Empty;
                string photo = string.Empty;

                if (item.GetType() == typeof(ItemEy))
                {
                    idIdItemBcn = (item as ItemEy).IdItemBcn;
                    idSupplier = (item as ItemEy).IdDefaultSupplier;
                    photo = (item as ItemEy).PhotoUrl;
                    _itemLastDocsList = GlobalSetting.ItemDocService.GetLastItemsDocs((item as ItemEy).IdItemBcn, Constants.ITEM_GROUP_EY);
                }
                else
                {
                    idIdItemBcn = (item as ItemHf).IdItemBcn;
                    idSupplier = (item as ItemHf).IdDefaultSupplier;
                    photo = (item as ItemHf).PhotoPath;
                    _itemLastDocsList = GlobalSetting.ItemDocService.GetLastItemsDocs((item as ItemHf).IdItemBcn, Constants.ITEM_GROUP_HF);
                }

                _itemBomList = GlobalSetting.ItemBomService.GetItemBom(idIdItemBcn);

                if (_itemBomList == null || _itemBomList.Count == 0)
                {
                    if (string.IsNullOrEmpty(idSupplier))
                    {
                        XtraMessageBox.Show("Item without default supplier. You must define one first", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    ItemBom itemBom = new ItemBom();
                    itemBom.IdBom = 0;
                    itemBom.IdItemBcn = idIdItemBcn;
                    itemBom.Item = item;
                    itemBom.IdItemGroup = (item.GetType() == typeof(ItemEy) ? Constants.ITEM_GROUP_EY : Constants.ITEM_GROUP_HF);
                    itemBom.IdSupplier = idSupplier;
                    itemBom.Materials = new List<DetailBomMt>();
                    itemBom.Hardwares = new List<DetailBomHw>();
                    itemBom.HalfFinished = new List<DetailBomHf>();

                    _itemBomList.Add(itemBom);

                }

                xgrdItemBom.DataSource = null;
                xgrdItemBom.DataSource = _itemBomList;

                GrdBomRefreshAndExpand();

                dockPanelGrdBom.Select();

                LoadBomTreeView();
                LoadSummaryBom();
                LoadDrawingPanel();
                LoadPdfColorgPanel();
                LoadPhotoPanel(photo);

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


        private void LoadSummaryBom()
        {
            try
            {
                xgrdSummaryBom.DataSource = null;


                DataTable tableSummary = new DataTable();
                tableSummary.Columns.Add("Group", typeof(string));
                tableSummary.Columns.Add("Item", typeof(string));
                tableSummary.PrimaryKey = new DataColumn[] { tableSummary.Columns["Item"] };

                DataTable tableSummaryUnit = new DataTable();
                tableSummaryUnit.Columns.Add("Group", typeof(string));
                tableSummaryUnit.Columns.Add("Unit", typeof(string));
                tableSummaryUnit.PrimaryKey = new DataColumn[] { tableSummaryUnit.Columns["Group"], tableSummaryUnit.Columns["Unit"] };

                //Obtenemos los supplier para generar una columna por cada uno de ellos
                var suppliers = _itemBomList.Select(a => a.IdSupplier).ToList();

                foreach (var supplier in suppliers)
                {
                    tableSummary.Columns.Add(supplier, typeof(decimal));
                    tableSummaryUnit.Columns.Add(supplier, typeof(decimal));
                }

                //rellenamos el datatable
                foreach (var bom in _itemBomList)
                {
                    FillDtHfSummary(tableSummary, tableSummaryUnit, suppliers, bom);
                }

                xgrdSummaryBom.DataSource = tableSummary;
                xgrdSummaryUnitBom.DataSource = tableSummaryUnit;

                //Una vez cargado, modificamos el estilo de los grid de resumen

                //Summary item/supplier
                foreach (GridColumn col in gridViewSummaryBom.Columns)
                {
                    if (col.FieldName == "Item")
                    {
                        col.Width = 150;
                    }
                    else if (col.FieldName == "Group")
                    {
                        col.Width = 60;
                    }
                    else
                    {
                        col.Width = 90;
                        col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        col.DisplayFormat.FormatString = "n2";
                    }
                }

                //Summaty Unit/Supplier
                foreach (GridColumn col in gridViewSummaryUnitBom.Columns)
                {
                    if (col.FieldName == "Unit")
                    {
                        col.Width = 150;
                    }
                    else if (col.FieldName == "Group")
                    {
                        col.Width = 60;
                    }
                    else
                    {
                        col.Width = 90;
                        col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        col.DisplayFormat.FormatString = "n2";
                    }
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadDrawingPanel()
        {
            try
            {

                //eliminamos los paneles de planos si existen
                var panelstoDel = dockManagerItemBom.Panels.Where(a => a.Name.Contains(DYNAMIC_PANEL_DRAWING)).ToList();
                foreach (var panel in panelstoDel)
                {
                    dockManagerItemBom.RemovePanel(panel);
                }

                //ocultamos el panel estático del drawing que ahora ha quedado para mensajes
                dockPanelDrawing.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;

                //Borramos si ya hemos cargado algo en al dockpanel de Drawing
                ClearPdfPanels(dockPanelDrawing);

                string docType = string.Empty;
                if (_currentItem.GetType() == typeof(ItemEy))
                    docType = "PDFDRAWING";
                else if (_currentItem.GetType() == typeof(ItemHf))
                    docType = "PDFDRAWING_HF";

                string drawingPath = Constants.ITEMS_DOCS_PATH + _itemLastDocsList.Where(a => a.IdDocType.Equals(docType)).Select(b => b.FilePath).FirstOrDefault();


                if (System.IO.File.Exists(drawingPath))
                {

                    //V1: un sólo plano por item
                    //PDFViewer pdfViewer = new PDFViewer();
                    //pdfViewer.TopLevel = false;
                    //pdfViewer.MinimizeBox = false;
                    //pdfViewer.MaximizeBox = false;
                    //pdfViewer.pdfFile = drawingPath;
                    //pdfViewer.FormClosing += (o, e) => { e.Cancel = true; }; //No queremos que puedan cerrar el formulario del viewer incrustado dentro del dockpanel
                    //dockPanelDrawing.Controls.Add(pdfViewer);
                    //pdfViewer.Dock = DockStyle.Fill;
                    //pdfViewer.Visible = true;

                    //--------------------------------------------------------------------------------------------------
                    //V2: Planos por item/supplier en un panel y tabs por cada plano de fábrica
                    //TabControl tcDrawings = new TabControl();

                    //var drawings = _itemLastDocsList.Where(a => a.IdDocType.Equals(docType)).ToList();

                    //foreach (var drawing in drawings)
                    //{
                    //    TabPage tpDrawind = new TabPage();
                    //    tpDrawind.Text = drawing.IdSupplier;

                    //    string path = Constants.ITEMS_DOCS_PATH + drawing.FilePath;

                    //    if (System.IO.File.Exists(path))
                    //    {
                    //        //Creamos el pdf Viewer
                    //        PDFViewer pdfViewer = new PDFViewer();
                    //        pdfViewer.TopLevel = false;
                    //        pdfViewer.MinimizeBox = false;
                    //        pdfViewer.MaximizeBox = false;
                    //        pdfViewer.pdfFile = path;
                    //        pdfViewer.FormClosing += (o, e) => { e.Cancel = true; }; //No queremos que puedan cerrar el formulario del viewer incrustado dentro del dockpanel

                    //        tpDrawind.Controls.Add(pdfViewer);
                    //        pdfViewer.Dock = DockStyle.Fill;
                    //        pdfViewer.Visible = true;
                    //    }
                    //    else
                    //    {
                    //        LabelControl lbl = new LabelControl();
                    //        lbl.Text = "No Drawing doc";
                    //        tpDrawind.Controls.Add(lbl);
                    //    }


                    //    //agregamos el tabpage al tabcontrol
                    //    tcDrawings.TabPages.Add(tpDrawind);
                    //}

                    //dockPanelDrawing.Controls.Add(tcDrawings);
                    //tcDrawings.Dock = DockStyle.Fill;

                    //--------------------------------------------------------------------------------------------------

                    //V3: Planos por item/supplier cada uno en un panel diferente
                    var drawings = _itemLastDocsList.Where(a => a.IdDocType.Equals(docType)).ToList();
                    foreach (var drawing in drawings)
                    {

                        //Creamos el panel
                        DevExpress.XtraBars.Docking.DockPanel tmpPanel = new DevExpress.XtraBars.Docking.DockPanel();
                        tmpPanel.Name = $"{DYNAMIC_PANEL_DRAWING}{drawing.IdSupplier}";
                        tmpPanel.Text = $"Drawing {drawing.IdSupplier}";
                        tmpPanel.Options.ShowCloseButton = false;
                        //creamos el ControlContainer y le agregamos el pdfViewer
                        DevExpress.XtraBars.Docking.ControlContainer tmpControlContainer = new DevExpress.XtraBars.Docking.ControlContainer();
                        tmpControlContainer.Name = $"{DYNAMIC_PANEL_DRAWING}{drawing.IdSupplier}";

                        string path = Constants.ITEMS_DOCS_PATH + drawing.FilePath;
                        if (System.IO.File.Exists(path))
                        {
                            //Creamos el pdf Viewer
                            PDFViewer pdfViewer = new PDFViewer();
                            pdfViewer.TopLevel = false;
                            pdfViewer.MinimizeBox = false;
                            pdfViewer.MaximizeBox = false;
                            pdfViewer.pdfFile = path;
                            pdfViewer.FormClosing += (o, e) => { e.Cancel = true; }; //No queremos que puedan cerrar el formulario del viewer incrustado dentro del dockpanel
                            //Agregamos el pdfViewer al ControlContainer
                            tmpControlContainer.Controls.Add(pdfViewer);
                            pdfViewer.Dock = DockStyle.Fill;
                            pdfViewer.Visible = true;
                        }
                        else
                        {
                            LabelControl lbl = new LabelControl();
                            lbl.Text = "Drawing File doesn't exist";
                            tmpControlContainer.Controls.Add(lbl);
                        }

                        //Agregamos el ControlContainer al panel, con el plano en pdf o con el label de que no existe el fichero en el servidor
                        tmpPanel.Controls.Add(tmpControlContainer);
                        //Agregamos el panel al dock manager del formulario
                        dockManagerItemBom.AddPanel(DevExpress.XtraBars.Docking.DockingStyle.Right, tmpPanel);
                        dockManagerItemBom.Panels[tmpPanel.Name].Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide;
                    }

                }
                else
                {
                    LabelControl lbl = new LabelControl();
                    lbl.Text = "No Drawing doc";
                    dockPanelDrawing.Controls.Add(lbl);
                    dockPanelDrawing.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide;
                }
            }
            catch
            {
                throw;
            }
        }

        private void LoadPdfColorgPanel()
        {
            try
            {
                //Borramos si ya hemos cargado algo en al dockpanel
                ClearPdfPanels(dockPanelPdfColor);

                string docType = string.Empty;
                if (_currentItem.GetType() == typeof(ItemEy))
                    docType = "PDFCOLOR";
                else if (_currentItem.GetType() == typeof(ItemHf))
                    docType = "PDFCOLOR_HF";

                string pdfColorPath = Constants.ITEMS_DOCS_PATH + _itemLastDocsList.Where(a => a.IdDocType.Equals(docType)).Select(b => b.FilePath).FirstOrDefault();

                if (System.IO.File.Exists(pdfColorPath))
                {
                    PDFViewer pdfViewer = new PDFViewer();
                    pdfViewer.TopLevel = false;
                    pdfViewer.MinimizeBox = false;
                    pdfViewer.MaximizeBox = false;
                    pdfViewer.pdfFile = pdfColorPath;
                    pdfViewer.FormClosing += (o, e) => { e.Cancel = true; }; //No queremos que puedan cerrar el formulario del viewer incrustado dentro del dockpanel
                    dockPanelPdfColor.Controls.Add(pdfViewer);
                    pdfViewer.Dock = DockStyle.Fill;
                    pdfViewer.Visible = true;

                }
                else
                {
                    LabelControl lbl = new LabelControl();
                    lbl.Text = "No PDF Color doc";
                    dockPanelPdfColor.Controls.Add(lbl);
                }

            }
            catch
            {
                throw;
            }
        }

        private void LoadPhotoPanel(string photo)
        {
            try
            {
                ClearPdfPanels(dockPanelPhoto);

                if (string.IsNullOrEmpty(photo) == false)
                {
                    string fullPath = $"{Constants.ITEMS_PHOTOSWEB_PATH}{Constants.ITEM_PHOTOWEB_FOLDER}{photo}";

                    if (System.IO.File.Exists(fullPath))
                    {
                        PictureEdit peItemImage = new PictureEdit();
                        peItemImage.Properties.ShowMenu = false;
                        peItemImage.Properties.SizeMode = PictureSizeMode.Zoom;
                        peItemImage.Size = new Size(400, 400);

                        Image img = Bitmap.FromFile(fullPath);

                        peItemImage.Image = img;

                        dockPanelPhoto.Controls.Add(peItemImage);
                    }
                    else
                    {
                        LabelControl lbl = new LabelControl();
                        lbl.Text = "No Photo";
                        dockPanelPhoto.Controls.Add(lbl);
                    }

                }
                else
                {
                    LabelControl lbl = new LabelControl();
                    lbl.Text = "No Photo";
                    dockPanelPhoto.Controls.Add(lbl);
                }

            }
            catch
            {
                throw;
            }
        }

        private void ClearPdfPanels(DevExpress.XtraBars.Docking.DockPanel panelPdf)
        {
            try
            {
                foreach (Control control in panelPdf.Controls)
                {
                    if (control.GetType() == typeof(DevExpress.XtraBars.Docking.ControlContainer))
                    {
                        foreach (Control subcontrol in control.Controls)
                        {
                            control.Controls.Remove(subcontrol);
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }
        
        private void FillDtHfSummary(DataTable tableSummary, DataTable tableSummaryUnit, List<string> suppliers, ItemBom bom)
        {
            try
            {

                foreach (DetailBomMt rm in bom.Materials.EmptyIfNull())
                {
                    if (string.IsNullOrEmpty(rm.IdItemBcn))
                        continue;


                    //Summary por item/Supplier

                    DataRow row = tableSummary.Rows.Find(rm.IdItemBcn);

                    if (row == null)
                    {
                        //insert
                        var dr = tableSummary.NewRow();
                        dr["Group"] = Constants.ITEM_GROUP_MT;
                        dr["Item"] = rm.IdItemBcn;
                        foreach (var supplier in suppliers)
                            dr[supplier] = (supplier == bom.IdSupplier ? rm.Quantity : 0);
                        tableSummary.Rows.Add(dr);
                    }
                    else
                    {
                        //update value
                        row[bom.IdSupplier] = (decimal)row[bom.IdSupplier] + rm.Quantity;
                    }

                    //Summary por Unit/Supplir
                    DataRow rowSummaryUnit = tableSummaryUnit.Rows.Find(new string[] { Constants.ITEM_GROUP_MT, rm.Item.Unit ?? string.Empty });

                    if (rowSummaryUnit == null)
                    {
                        var drSummaryUnit = tableSummaryUnit.NewRow();
                        drSummaryUnit["Group"] = Constants.ITEM_GROUP_MT;
                        drSummaryUnit["Unit"] = rm.Item.Unit ?? string.Empty;
                        foreach (var supplier in suppliers)
                            drSummaryUnit[supplier] = (supplier == bom.IdSupplier ? rm.Quantity : 0);
                        tableSummaryUnit.Rows.Add(drSummaryUnit);
                    }
                    else
                    {
                        rowSummaryUnit[bom.IdSupplier] = (decimal)rowSummaryUnit[bom.IdSupplier] + rm.Quantity;
                    }
                }


                foreach (DetailBomHw hw in bom.Hardwares.EmptyIfNull())
                {
                    if (string.IsNullOrEmpty(hw.IdItemBcn))
                        continue;

                    //Summary por item/Supplier
                    DataRow row = tableSummary.Rows.Find(hw.IdItemBcn);

                    if (row == null)
                    {
                        //insert
                        var dr = tableSummary.NewRow();
                        dr["Group"] = Constants.ITEM_GROUP_HW;
                        dr["Item"] = hw.IdItemBcn;
                        foreach (var supplier in suppliers)
                            dr[supplier] = (supplier == bom.IdSupplier ? hw.Quantity : 0);
                        tableSummary.Rows.Add(dr);
                    }
                    else
                    {
                        //update value
                        row[bom.IdSupplier] = (decimal)row[bom.IdSupplier] + hw.Quantity;
                    }

                    //Summary por Unit/Supplir
                    DataRow rowSummaryUnit = tableSummaryUnit.Rows.Find(new string[] { Constants.ITEM_GROUP_HW, hw.Item.Unit ?? string.Empty });

                    if (rowSummaryUnit == null)
                    {
                        var drSummaryUnit = tableSummaryUnit.NewRow();
                        drSummaryUnit["Group"] = Constants.ITEM_GROUP_HW;
                        drSummaryUnit["Unit"] = hw.Item.Unit ?? string.Empty;
                        foreach (var supplier in suppliers)
                            drSummaryUnit[supplier] = (supplier == bom.IdSupplier ? hw.Quantity : 0);
                        tableSummaryUnit.Rows.Add(drSummaryUnit);
                    }
                    else
                    {
                        rowSummaryUnit[bom.IdSupplier] = (decimal)rowSummaryUnit[bom.IdSupplier] + hw.Quantity;
                    }
                }

                foreach (var hf in bom.HalfFinished.EmptyIfNull())
                {
                    if (hf.DetailItemBom == null)
                        continue;

                    FillDtHfSummary(tableSummary, tableSummaryUnit, suppliers, hf.DetailItemBom);
                }

            }
            catch
            {
                throw;
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
                    GrdBomRefreshAndExpand();
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
                    GrdBomRefreshAndExpand();
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


        private void SetGrdBomEditColumns()
        {
            try
            {
                //Common Edit repositories
                RepositoryItemTextEdit ritxt2Dec = new RepositoryItemTextEdit();
                ritxt2Dec.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                ritxt2Dec.Mask.EditMask = "F2";
                ritxt2Dec.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;

                RepositoryItemTextEdit ritxt6Dec = new RepositoryItemTextEdit();
                ritxt6Dec.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                ritxt6Dec.Mask.EditMask = "F6";
                ritxt6Dec.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;

                RepositoryItemTextEdit ritxtInt = new RepositoryItemTextEdit();
                ritxtInt.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                ritxtInt.Mask.EditMask = "N";
                ritxtInt.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;

                RepositoryItemSearchLookUpEdit riBomBreakdown = new RepositoryItemSearchLookUpEdit()
                {
                    DataSource = _bomBreakdownList,
                    ValueMember = nameof(BomBreakdown.IdBomBreakdown),
                    DisplayMember = nameof(BomBreakdown.Description),
                    NullText = "Select Item",
                };

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

                            //Edit Columns
                            view.OptionsBehavior.Editable = true;

                            //No edit columns
                            view.Columns[$"{nameof(DetailBomMt.Item)}.{nameof(ItemMt.ItemDescription)}"].OptionsColumn.AllowEdit = false;
                            view.Columns[$"{nameof(DetailBomMt.Item)}.{nameof(ItemMt.Unit)}"].OptionsColumn.AllowEdit = false;

                            //Edit repositories
                            view.Columns[nameof(DetailBomMt.IdBomBreakdown)].ColumnEdit = riBomBreakdown;
                            view.Columns[nameof(DetailBomMt.Length)].ColumnEdit = ritxt2Dec;
                            view.Columns[nameof(DetailBomMt.Width)].ColumnEdit = ritxt2Dec;
                            view.Columns[nameof(DetailBomMt.Height)].ColumnEdit = ritxt2Dec;
                            view.Columns[nameof(DetailBomMt.Density)].ColumnEdit = ritxt2Dec;
                            view.Columns[nameof(DetailBomMt.Coefficient1)].ColumnEdit = ritxt6Dec;
                            view.Columns[nameof(DetailBomMt.Coefficient2)].ColumnEdit = ritxt6Dec;
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

                            //No edit columns
                            view.Columns[$"{nameof(DetailBomHw.Item)}.{nameof(ItemHw.ItemDescription)}"].OptionsColumn.AllowEdit = false;
                            view.Columns[$"{nameof(DetailBomHw.Item)}.{nameof(ItemHw.Unit)}"].OptionsColumn.AllowEdit = false;

                            //Edit repositories
                            view.Columns[nameof(DetailBomHw.IdBomBreakdown)].ColumnEdit = riBomBreakdown;
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

        private void GrdBomRefreshAndExpand()
        {
            xgrdItemBom.RefreshDataSource();
            ExpandAllRows(gridViewItemBom);
        }

        void ExpandAllRows(GridView view)
        {
            view.BeginUpdate();
            try
            {
                string defaultSupplier = string.Empty;

                if (_currentItem.GetType() == typeof(ItemEy))
                    defaultSupplier = (_currentItem as ItemEy).IdDefaultSupplier;
                else if (_currentItem.GetType() == typeof(ItemHf))
                    defaultSupplier = (_currentItem as ItemHf).IdDefaultSupplier;

                view.ExpandAllGroups();

                int dataRowCount = view.DataRowCount;
                for (int rHandle = 0; rHandle < dataRowCount; rHandle++)
                {
                    object idSupplier = view.GetRowCellValue(rHandle, nameof(ItemBom.IdSupplier));
                    if (idSupplier != null && idSupplier.ToString() == defaultSupplier)
                        view.SetMasterRowExpanded(rHandle, true);
                }
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
                string idItem = string.Empty;

                if (_currentItem.GetType() == typeof(ItemEy))
                {
                    idItem = (_currentItem as ItemEy).IdItemBcn;
                }
                else if (_currentItem.GetType() == typeof(ItemHf))
                {
                    idItem = (_currentItem as ItemHf).IdItemBcn;
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

                foreach (DetailBomMt rawMaterial in item.Materials.EmptyIfNull())
                {
                    if (string.IsNullOrEmpty(rawMaterial.IdItemBcn))
                        continue;

                    root.Nodes[0].Nodes.Add(rawMaterial.IdItemBcn, $"{rawMaterial.IdItemBcn} : {rawMaterial.Item.ItemDescription}");
                    root.Nodes[0].Nodes[contRawMaterialsNode].Tag = "RawMaterials";
                    root.Nodes[0].Nodes[contRawMaterialsNode].Nodes.Add(
                        new TreeNode($"Quantity : {rawMaterial.Quantity.ToString()} ({rawMaterial.Item.Unit})")
                        );
                    contRawMaterialsNode++;
                }
                root.Nodes[0].Expand();

                foreach (DetailBomHw hardware in item.Hardwares.EmptyIfNull())
                {
                    if (string.IsNullOrEmpty(hardware.IdItemBcn))
                        continue;

                    root.Nodes[1].Nodes.Add(hardware.IdItemBcn, $"{hardware.IdItemBcn} : {hardware.Item.ItemDescription}");
                    root.Nodes[1].Nodes[contHardwareNode].Tag = "Hardware";
                    root.Nodes[1].Nodes[contHardwareNode].Nodes.Add(
                        new TreeNode($"Quantity : {hardware.Quantity.ToString()} ({hardware.Item.Unit})")
                        );
                    contHardwareNode++;
                }
                root.Nodes[1].Expand();

                foreach(DetailBomHf ib in item.HalfFinished.EmptyIfNull())
                {
                    if (ib.DetailItemBom == null)
                        continue;

                    root.Nodes[2].Nodes.Add(GetComponentNode(ib.DetailItemBom));
                    root.Nodes[2].Expand();
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
                    itemBom.HalfFinished = new List<DetailBomHf>();

                    _itemBomList.Add(itemBom);
                    xgrdItemBom.DataSource = null;
                    xgrdItemBom.DataSource = _itemBomList;

                    GrdBomRefreshAndExpand();
                    dockPanelGrdBom.Select();

                    LoadBomTreeView();
                    LoadSummaryBom();



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
                List <string> selectedSuppliersSource = new List<string>();

                using (DialogForms.SelectSuppliers form = new DialogForms.SelectSuppliers())
                {
                    form.InitData(suppliers);

                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        selectedSuppliersSource = form.SelectedSuppliersSource;
                    }

                }

                return selectedSuppliersSource;

            }
            catch
            {
                throw;
            }
        }

        private bool OpenSelectSuppliersForm2Copy()
        {
            try
            {
                var suppliers = _itemBomList.Select(a => a.IdSupplier).ToList();
                List<string> selectedSuppliersSource = new List<string>();
                List<string> selectedSuppliersDestination = new List<string>();

                using (DialogForms.SelectSuppliers form = new DialogForms.SelectSuppliers())
                {
                    form.InitData(suppliers);

                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        selectedSuppliersSource = form.SelectedSuppliersSource;
                        selectedSuppliersDestination = form.SelectedSuppliersDestination;

                        CopySupplierBom2SupplierBom(selectedSuppliersSource.Single(), selectedSuppliersDestination);
                    }

                }
                return true;
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
                        var modifiedBom = GlobalSetting.ItemBomService.GetItemSupplierBom(bom.IdItemBcn, bom.IdSupplier);

                        //Actualizamos la versión del padre, ya que al modificar un semielaborado, también modifica la versión de los que lo incluyen
                        var parent = _itemBomList.Where(a => a.IdSupplier.Equals(bom.IdSupplier)).Single();
                        var parentNewVersion = GlobalSetting.ItemBomService.GetItemSupplierBom(parent.IdItemBcn, parent.IdSupplier);
                        parent.IdVer = parentNewVersion.IdVer;
                        parent.IdSubVer = parentNewVersion.IdSubVer;
                        parent.Timestamp = parentNewVersion.Timestamp;

                        //actualizamos el que tenemos en pantalla por el que hemos actualizado
                        bom.IdVer = modifiedBom.IdVer;
                        bom.IdSubVer = modifiedBom.IdSubVer;
                        bom.Timestamp = modifiedBom.Timestamp;

                        bom.Materials = new List<DetailBomMt>();
                        foreach(var mt in modifiedBom.Materials)
                        {
                            bom.Materials.Add(mt.Clone());
                        }

                        bom.Hardwares = new List<DetailBomHw>();
                        foreach(var hw in modifiedBom.Hardwares)
                        {
                            bom.Hardwares.Add(hw.Clone());
                        }

                        //TODO: faltan los semielaborados!

                        //Tenemos que recargar todo ya que si hemos actualizado un semielaborado, éste ha cambiado la versión de los BOM que lo incluyen, incluyendo este mismo que lo ha lanzado
                        //LoadItemGridBom(_currentItem);
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

        private ItemBom OpenSelectItem2CopyBomForm(string idItemDestination, string model, string supplier)
        {
            try
            {
                ItemBom bom = null;

                using (DialogForms.SelectItem2CopyBom form = new DialogForms.SelectItem2CopyBom())
                {
                    form.InitData(idItemDestination, model, supplier);

                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        bom = form.SelectedBom;
                    }
                }

                return bom;

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

                ShowAddBomSupplierAndCopyBom(true);

                xgrdItemsEy.Enabled = false;
                xgrdItemsHf.Enabled = false;

                dockPanelItemsEy.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide;
                dockPanelItemsEy.HideSliding();

                dockPanelItemsHf.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide;
                dockPanelItemsHf.HideSliding();

                //Suscribirse a los eventos de los grid para agregar al bom con doble click
                //gridViewItemsMt.DoubleClick += GridViewItemsMt_DoubleClick;
                //gridViewItemsHw.DoubleClick += GridViewItemsHw_DoubleClick;
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

                    //if( (bom.Materials == null || bom.Materials.Count == 0) && 
                    //    (bom.Hardwares == null || bom.Hardwares.Count == 0) && 
                    //    (bom.HalfFinished == null || bom.HalfFinished.Count == 0))
                    //{
                    //    XtraMessageBox.Show($"BOM is empty for supplier {bom.IdSupplier}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    return false;
                    //}

                    //if((bom.Materials != null && bom.Materials.Count == 1 && string.IsNullOrEmpty(bom.Materials.Select(a => a.IdItemBcn).FirstOrDefault())) &&
                    //    (bom.Hardwares != null && bom.Hardwares.Count == 1 && string.IsNullOrEmpty(bom.Hardwares.Select(a => a.IdItemBcn).FirstOrDefault())))
                    //{
                    //    XtraMessageBox.Show($"BOM is empty for supplier {bom.IdSupplier}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    return false;
                    //}

                    foreach (var m in bom.Materials)
                    {
                        if (string.IsNullOrEmpty(m.IdItemBcn) == false && m.Quantity <= 0 )
                        {
                            XtraMessageBox.Show($"Quantity must be greater than Zero ({m.IdItemBcn})", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }

                        if (string.IsNullOrEmpty(m.IdItemBcn) == false && string.IsNullOrEmpty(m.IdBomBreakdown))
                        {
                            XtraMessageBox.Show($"Select breakdown ({m.IdItemBcn})", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                        if (string.IsNullOrEmpty(h.IdItemBcn) == false && string.IsNullOrEmpty(h.IdBomBreakdown))
                        {
                            XtraMessageBox.Show($"Select breakdown ({h.IdItemBcn})", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }

                    if (bom.HalfFinished != null)
                    {
                        foreach (var hf in bom.HalfFinished)
                        {
                            if (hf.DetailItemBom != null && hf.Quantity <= 0)
                            {
                                XtraMessageBox.Show($"Half-finished must be greater than Zero ({hf.DetailItemBom.IdItemBcn})", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return false;
                            }
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

                    for (int i = bom.Materials.EmptyIfNull().Count - 1; i >= 0; i--)
                    {
                        if (string.IsNullOrEmpty(bom.Materials[i].IdItemBcn))
                        {
                            bom.Materials.RemoveAt(i);
                        }
                    }

                    for (int i = bom.Hardwares.EmptyIfNull().Count - 1; i >= 0; i--)
                    {
                        if (string.IsNullOrEmpty(bom.Hardwares[i].IdItemBcn))
                        {
                            bom.Hardwares.RemoveAt(i);
                        }
                    }

                    for (int i = bom.HalfFinished.EmptyIfNull().Count - 1; i >= 0; i--)
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

                        var list = GlobalSetting.ItemBomService.GetRelatedItemBom(item.IdBom);
                        foreach (var x in list)
                        {
                            find = true;
                            msg += $"{x.IdItemBcn} ({x.IdSupplier}) {Environment.NewLine}";
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
                //ItemEy item = _itemBomOriginal.Item as ItemEy;
                //_itemBomOriginal = null;
                _itemBomList.Clear();
                LoadItemGridBom(_currentItem);

                //Restore de ribbon to initial states
                RestoreInitState();

                dockPanelItemsHf.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
                dockPanelItemsHf.ShowSliding();

                dockPanelItemsEy.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
                dockPanelItemsEy.ShowSliding();

                xgrdItemsEy.Enabled = true;
                xgrdItemsHf.Enabled = true;

                //gridViewItemsMt.DoubleClick -= GridViewItemsMt_DoubleClick;
                //gridViewItemsHw.DoubleClick -= GridViewItemsHw_DoubleClick;

                SetGrdBomDetailsNonEdit();
                ShowAddBomSupplierAndCopyBom(false);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Aux

        private void ShowAddBomSupplierAndCopyBom(bool show)
        {
            try
            {
                lblSupplier.Visible = slueSupplier.Visible = sbAddBomSupplier.Visible = lblCopyBom.Visible = sbCopyBom.Visible = show;
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

                        e.Menu.Items.Add(CreateMenuItemCopyBomFromItem(view, rowHandle));

                        //if ((row as ItemBom).Materials == null || (row as ItemBom).Materials.Count() == 0)
                        //    e.Menu.Items.Add(CreateMenuItemAddINewLineMaterial(view, rowHandle));

                        //if ((row as ItemBom).Hardwares == null || (row as ItemBom).Hardwares.Count() == 0)
                        //    e.Menu.Items.Add(CreateMenuItemAddINewLineHardware(view, rowHandle));

                        //if ((row as ItemBom).HalfFinished == null || (row as ItemBom).HalfFinished.Count() == 0)
                        //    e.Menu.Items.Add(CreateMenuItemAddINewLineHalfFinished(view, rowHandle));

                        if (((row as ItemBom).Materials == null || (row as ItemBom).Materials.Count() == 0) && 
                            ((row as ItemBom).HalfFinished == null || (row as ItemBom).HalfFinished.Count() == 0)) 
                        {
                            e.Menu.Items.Add(CreateMenuItemAddINewLineMaterial(view, rowHandle));
                        }

                        if (((row as ItemBom).Hardwares == null || (row as ItemBom).Hardwares.Count() == 0) && 
                            ((row as ItemBom).HalfFinished == null || (row as ItemBom).HalfFinished.Count() == 0))
                        {
                            e.Menu.Items.Add(CreateMenuItemAddINewLineHardware(view, rowHandle));
                        }

                        if (((row as ItemBom).HalfFinished == null || (row as ItemBom).HalfFinished.Count() == 0) && 
                            ((row as ItemBom).Materials == null || (row as ItemBom).Materials.Count() == 0) && 
                            ((row as ItemBom).Hardwares == null || (row as ItemBom).Hardwares.Count() == 0))
                        {
                            e.Menu.Items.Add(CreateMenuItemAddINewLineHalfFinished(view, rowHandle));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        DevExpress.Utils.Menu.DXMenuItem CreateMenuItemCopyBomFromItem(GridView view, int rowHandle)
        {
            DevExpress.Utils.Menu.DXMenuItem menuItem = new DevExpress.Utils.Menu.DXMenuItem("Copy BOM from Item",
                new EventHandler(OnMenuItemCopyBomFromItemClick));
            menuItem.Tag = new Classes.RowInfo(view, rowHandle);
            return menuItem;
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
                (row as ItemBom).Materials.Add(new DetailBomMt() { IdBom = (row as ItemBom).IdBom });
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
                (row as ItemBom).Hardwares.Add(new DetailBomHw() { IdBom = (row as ItemBom).IdBom });
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

        void OnMenuItemCopyBomFromItemClick(object sender, EventArgs e)
        {
            try
            {
                DevExpress.Utils.Menu.DXMenuItem item = sender as DevExpress.Utils.Menu.DXMenuItem;
                Classes.RowInfo info = item.Tag as Classes.RowInfo;
                string idModel = string.Empty;

                var row = info.View.GetRow(info.View.FocusedRowHandle);
                if((row as ItemBom).Item.GetType().BaseType == typeof(ItemEy) || (row as ItemBom).Item.GetType() == typeof(ItemEy))
                {
                    var tmp = (row as ItemBom).Item;
                    idModel = (tmp as ItemEy).IdModel;
                }

                ItemBom bom = OpenSelectItem2CopyBomForm((row as ItemBom).IdItemBcn, idModel, (row as ItemBom).IdSupplier);

                if (bom != null)
                {
                    CopyBomFromModelItem((row as ItemBom), bom);
                    info.View.Columns[nameof(ItemBom.IdSupplier)].AppearanceCell.BackColor = Color.Salmon;
                    info.View.Columns[nameof(ItemBom.IdSupplier)].AppearanceCell.BackColor2 = Color.SeaShell;
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Copy Bom
        
        #region Copy Supplier BOM to other Supplier BOM
        private bool CopySupplierBom2SupplierBom(string selectedSupplierSource, List<string> selectedSuppliersDestination)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                string msgHalfFinished = string.Empty;

                var bomSource = _itemBomList.Where(a => a.IdSupplier.Equals(selectedSupplierSource)).Single();

                foreach(var supplierDestination in selectedSuppliersDestination)
                {
                    var bomDestination = _itemBomList.Where(a => a.IdSupplier.Equals(supplierDestination)).Single();

                    bomDestination.Materials = new List<DetailBomMt>();
                    bomDestination.Hardwares = new List<DetailBomHw>();
                    bomDestination.HalfFinished = new List<DetailBomHf>();

                    foreach (var m in bomSource.Materials)
                    {
                        var tmpMat = m.Clone();
                        tmpMat.IdBom = bomDestination.IdBom;
                        bomDestination.Materials.Add(tmpMat);
                    }

                    foreach(var hw in bomSource.Hardwares)
                    {
                        var tmpHw = hw.Clone();
                        tmpHw.IdBom = bomDestination.IdBom;
                        bomDestination.Hardwares.Add(tmpHw);
                    }

                    foreach(var hf in bomSource.HalfFinished)
                    {
                        //Los semielaborados no se copían directamente, hay que buscar si existe un bom en el sistema para ese item/supplier y copiar ese.
                        var itemBomHf = GlobalSetting.ItemBomService.GetItemSupplierBom(hf.DetailItemBom.IdItemBcn, supplierDestination);

                        if (itemBomHf != null)
                        {
                            DetailBomHf detailBomHf = new DetailBomHf()
                            {
                                DetailItemBom = itemBomHf,
                                IdBomDetail = itemBomHf.IdBom,
                                Quantity = hf.Quantity

                            };
                            bomDestination.HalfFinished.Add(detailBomHf);
                        }
                        else
                        {
                            msgHalfFinished += $"{hf.DetailItemBom.IdItemBcn} is not defined for supplier {supplierDestination}.{Environment.NewLine}";
                        }
                    }
                }

                if (msgHalfFinished != string.Empty)
                {
                    XtraMessageBox.Show(msgHalfFinished, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                GrdBomRefreshAndExpand();

                return true;
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

        #region Copy BOM from item (same model)
        private bool CopyBomFromModelItem(ItemBom itemBomOri, ItemBom bomCopy)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                itemBomOri.Materials = new List<DetailBomMt>();
                itemBomOri.Hardwares = new List<DetailBomHw>();
                itemBomOri.HalfFinished = new List<DetailBomHf>();

                foreach(var m in bomCopy.Materials)
                {
                    var tmpMat = m.Clone();
                    tmpMat.IdBom = itemBomOri.IdBom;
                    itemBomOri.Materials.Add(tmpMat);
                }

                foreach (var hw in bomCopy.Hardwares)
                {
                    var tmpHw = hw.Clone();
                    tmpHw.IdBom = itemBomOri.IdBom;
                    itemBomOri.Hardwares.Add(tmpHw);
                }

                foreach (var hf in bomCopy.HalfFinished)
                {
                    var itemBomHf = GlobalSetting.ItemBomService.GetItemSupplierBom(hf.DetailItemBom.IdItemBcn, itemBomOri.IdSupplier);
                    DetailBomHf detailBomHf = new DetailBomHf()
                    {
                        DetailItemBom = itemBomHf,
                        IdBomDetail = itemBomOri.IdBom,
                        Quantity = hf.Quantity

                    };
                    itemBomOri.HalfFinished.Add(detailBomHf);
                }

                GrdBomRefreshAndExpand();

                return true;
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

        #endregion

        #endregion

        #region BomManagement ValidatingEditor

        private void BomManagementMaterials_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {

            try
            {
                decimal qty = 0;
                GridView view = sender as GridView;
                DetailBomMt row = view.GetRow(view.FocusedRowHandle) as DetailBomMt;

                BaseView parent = view.ParentView;
                var rowParent = parent.GetRow(view.SourceRowHandle);
                string factory = string.Empty;

                switch (view.FocusedColumn.FieldName)
                {
                    case nameof(DetailBomMt.IdItemBcn):
                        //No se pueden repetir materiales/breakdown
                        var idItemMt = e.Value.ToString();
                        object exist = null;
                        if (rowParent.GetType().BaseType == typeof(ItemBom) || rowParent.GetType() == typeof(ItemBom))
                        {
                            //exist = (rowParent as ItemBom).Materials.Where(a => a.IdItemBcn != null && a.IdItemBcn.Equals(idItemMt)).FirstOrDefault();
                            exist = (rowParent as ItemBom).Materials
                                .Where(a => 
                                    (a.IdItemBcn ?? "").Equals(idItemMt) && (a.IdBomBreakdown ?? "").Equals(row.IdBomBreakdown ?? "")
                                    )
                                .FirstOrDefault();

                            factory = (rowParent as ItemBom).IdSupplier;
                        }
                        else if (rowParent.GetType().BaseType == typeof(DetailBomHf) || rowParent.GetType() == typeof(DetailBomHf))
                        {
                            //exist = (rowParent as DetailBomHf).DetailItemBom.Materials.Where(a => a.IdItemBcn != null && a.IdItemBcn.Equals(idItemMt)).FirstOrDefault();
                            exist = (rowParent as DetailBomHf).DetailItemBom.Materials
                                .Where(a =>
                                     (a.IdItemBcn ?? "").Equals(idItemMt) && (a.IdBomBreakdown ?? "").Equals(row.IdBomBreakdown ?? "")
                                    )
                                .FirstOrDefault();

                            factory = (rowParent as DetailBomHf).DetailItemBom?.IdSupplier;
                        }


                        if (exist == null)
                        {
                            var itemMt = _itemsMtList.Where(a => a.IdItemBcn.Equals(idItemMt)).Single().Clone();
                            row.Item = itemMt;

                            //Buscamos los valores de coeficientes y scrap para ese item/factory
                            SupplierFactoryCoeff supplierFactoryCoeff = _supplierFactoryCoeffList
                                .Where(a => a.IdSupplier.Equals(itemMt.IdDefaultSupplier) && a.IdFactory.Equals(factory) && a.IdItemGroup.Equals(Constants.ITEM_GROUP_MT))
                                .FirstOrDefault();
                            if (supplierFactoryCoeff != null)
                            {
                                row.Density = supplierFactoryCoeff.Density;
                                row.Coefficient1 = supplierFactoryCoeff.Coefficient1;
                                row.Coefficient2 = supplierFactoryCoeff.Coefficient2;
                                row.Scrap = supplierFactoryCoeff.Scrap;
                            }
                            else
                            {
                                row.Density = null;
                                row.Coefficient1 = null;
                                row.Coefficient2 = null;
                                row.Scrap = null;
                            }
                        }
                        else
                        {
                            e.Valid = false;
                        }

                        break;

                    case nameof(DetailBomMt.IdBomBreakdown):

                        //No se pueden repetir materiales/breakdown
                        var idBomBreakdown = e.Value.ToString();
                        object existBB = null;
                        if (rowParent.GetType().BaseType == typeof(ItemBom) || rowParent.GetType() == typeof(ItemBom))
                        {
                            existBB = (rowParent as ItemBom).Materials
                                .Where(a =>
                                    (a.IdItemBcn ?? "").Equals(row.IdItemBcn) && (a.IdBomBreakdown ?? "").Equals(idBomBreakdown)
                                    )
                                .FirstOrDefault();
                        }
                        else if (rowParent.GetType().BaseType == typeof(DetailBomHf) || rowParent.GetType() == typeof(DetailBomHf))
                        {
                            existBB = (rowParent as DetailBomHf).DetailItemBom.Materials
                                .Where(a =>
                                     (a.IdItemBcn ?? "").Equals(row.IdItemBcn) && (a.IdBomBreakdown ?? "").Equals(idBomBreakdown)
                                    )
                                .FirstOrDefault();
                        }


                        if (existBB != null)
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
                            ((decimal)1 / (decimal)(row.NumberOfParts ?? 1)) *
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
                            ((decimal)1 / (decimal)(row.NumberOfParts ?? 1)) *
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
                            ((decimal)1 / (decimal)(row.NumberOfParts ?? 1)) *
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
                            ((decimal)1 / (decimal)(row.NumberOfParts ?? 1)) *
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
                            ((decimal)1 / (decimal)(row.NumberOfParts ?? 1)) *
                            (row.Coefficient1 ?? 0) *
                            (row.Coefficient2 ?? 0) *
                            (row.Scrap ?? 0);

                        row.Quantity = qty;

                        break;

                    case nameof(DetailBomMt.NumberOfParts):

                        //Me hace cosas raras en el parse + validación y tengo que controlarlo a mano
                        int numParts;
                        if (int.TryParse(e.Value.ToString(), out numParts) == false)
                        {
                            e.Valid = false;
                        }
                        else if(numParts == 0)
                        {
                            e.Valid = false;
                        }
                        else
                        {
                            qty = (row.Length ?? 0) *
                            (row.Width ?? 0) *
                            (row.Height ?? 0) *
                            (row.Density ?? 0) *
                            ((decimal)1 / (decimal)numParts) *
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
                            ((decimal)1 / (decimal)(row.NumberOfParts ?? 1)) *
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
                            ((decimal)1 / (decimal)(row.NumberOfParts ?? 1)) *
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
                            ((decimal)1 / (decimal)(row.NumberOfParts ?? 1)) *
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
                if (e.Value == null)
                    return;

                GridView view = sender as GridView;
                DetailBomHw row = view.GetRow(view.FocusedRowHandle) as DetailBomHw;

                BaseView parent = view.ParentView;
                var rowParent = parent.GetRow(view.SourceRowHandle);
                string factory = string.Empty;

                switch (view.FocusedColumn.FieldName)
                {
                    case nameof(DetailBomHw.IdItemBcn):

                        //No se pueden repetir hardware/breakdown 
                        var idItemHw = e.Value.ToString();

                        object exist = null;
                        if (rowParent.GetType().BaseType == typeof(ItemBom) || rowParent.GetType() == typeof(ItemBom))
                        {
                            //exist = (rowParent as ItemBom).Hardwares.Where(a => a.IdItemBcn != null && a.IdItemBcn.Equals(idItemHw)).FirstOrDefault();
                            exist = (rowParent as ItemBom).Hardwares
                                .Where(a =>
                                    (a.IdItemBcn ?? "").Equals(idItemHw) && (a.IdBomBreakdown ?? "").Equals(row.IdBomBreakdown ?? "")
                                    )
                                .FirstOrDefault();

                            factory = (rowParent as ItemBom).IdSupplier;
                        }

                        if (exist == null)
                        {
                            var itemHw = _itemsHwList.Where(a => a.IdItemBcn.Equals(idItemHw)).Single().Clone();
                            row.Item = itemHw;

                            //Buscamos los valores de coeficientes y scrap para ese item/factory
                            SupplierFactoryCoeff supplierFactoryCoeff = _supplierFactoryCoeffList
                                .Where(a => a.IdSupplier.Equals(itemHw.IdDefaultSupplier) && a.IdFactory.Equals(factory) && a.IdItemGroup.Equals(Constants.ITEM_GROUP_HW))
                                .FirstOrDefault();
                            if (supplierFactoryCoeff != null)
                            {
                                //row.Density = supplierFactoryCoeff.Density;
                                //row.Coefficient1 = supplierFactoryCoeff.Coefficient1;
                                //row.Coefficient2 = supplierFactoryCoeff.Coefficient2;
                                row.Scrap = supplierFactoryCoeff.Scrap;
                            }
                            else
                            {
                                //row.Density = null;
                                //row.Coefficient1 = null;
                                //row.Coefficient2 = null;
                                row.Scrap = null;
                            }
                        }
                        else
                        {
                            e.Valid = false;
                        }

                        break;

                    case nameof(DetailBomHw.IdBomBreakdown):

                        //No se pueden repetir hardware/breakdown 
                        var idBomBreakdown = e.Value.ToString();

                        object existBB = null;
                        if (rowParent.GetType().BaseType == typeof(ItemBom) || rowParent.GetType() == typeof(ItemBom))
                        {
                            existBB = (rowParent as ItemBom).Hardwares
                                .Where(a =>
                                    (a.IdItemBcn ?? "").Equals(row.IdItemBcn) && (a.IdBomBreakdown ?? "").Equals(idBomBreakdown)
                                    )
                                .FirstOrDefault();
                        }

                        if (existBB != null)
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
                            //No se puede agregar el mismo como HF para evitar referencias circulares
                            if ((rowParent as ItemBom).IdItemBcn == idItemHf)
                                e.Valid = false;

                            exist = (rowParent as ItemBom).HalfFinished.Where(a => a.DetailItemBom != null && a.DetailItemBom.IdItemBcn.Equals(idItemHf)).FirstOrDefault();
                        }

                        if (e.Valid == true && exist == null)
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
                        if(gridView.LevelName == nameof(ItemBom.HalfFinished))
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
