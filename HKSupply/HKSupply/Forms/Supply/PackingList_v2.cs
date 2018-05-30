﻿using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using HKSupply.General;
using HKSupply.Helpers;
using HKSupply.Models;
using HKSupply.Models.Supply;
using HKSupply.Reports;
using HKSupply.Styles;
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

namespace HKSupply.Forms.Supply
{
    public partial class PackingList_v2 : RibbonFormBase
    {
        #region Constants
        private const string COL_SELECTED = "Selected";
        #endregion

        #region Enums
        enum eGridLinesSoSelectionSummaries
        {
            totalQuantityOrderedMt,
            totalQuantityOrderedHw,
            totalQuantityMt,
            totalQuantityHw
        }

        enum eGridLinesDeliveredGoodsSummaries
        {
            totalQuantityOrderedMt,
            totalQuantityOrderedHw,
            totalQuantityMt,
            totalQuantityHw
        }
        #endregion

        #region Private Members

        Font _labelDefaultFontBold = AppStyles.LabelDefaultFontBold;
        Font _labelDefaultFont = AppStyles.LabelDefaultFont;

        List<SupplyStatus> _supplyStatusList;

        List<Customer> _customersList;
        List<Currency> _currenciesList;
        List<DeliveryTerm> _deliveryTermList;
        List<PaymentTerms> _paymentTermsList;

        DocHead _docHeadPK;
        BindingList<DocHead> _docSoSelectionList;
        BindingList<DocLine> _docLinesSoSelectionList;
        BindingList<DocLine> _docLinesDeliveredGoodsList;

        //Test Item Batch INI
        BindingList<PackingListItemBatch> _auxItemsBatchList = new BindingList<PackingListItemBatch>(); 
        List<PackingListItemBatch> _itemsBatchList = new List<PackingListItemBatch>();

        BindingList<PackingListItemBox> _auxItemsBoxList = new BindingList<PackingListItemBox>();
        List<PackingListItemBox> _itemsBoxList = new List<PackingListItemBox>();

        List<Box> _boxList;
        BindingList<DocBox> _docBoxList = new BindingList<DocBox>();
        //Test Item Batch END

        decimal _totalQuantityOrderedMtLinesSo;
        int _totalQuantityOrderedHwLinesSo;
        decimal _totalQuantityMtLinesSo;
        int _totalQuantityHwLinesSo;

        decimal _totalQuantityOrderedMtDeliveredGoods;
        int _totalQuantityOrderedHwDeliveredGoods;
        decimal _totalQuantityMtDeliveredGoods;
        int _totalQuantityHwDeliveredGoods;

        bool _isLoadingPacking = false;
        bool _isCreatingPacking = false;
        bool _isMarkingSoSelFromDeliveredGoods = false;
        #endregion

        #region Constructor
        public PackingList_v2()
        {
            InitializeComponent();

            try
            {
                Cursor = Cursors.WaitCursor;

                ConfigureRibbonActions();
                LoadAuxList();
                SetUpTabs();
                SetUpLabels();
                SetObjectsReadOnly();
                SetUpButtons();
                SetUpSearchLookUpEdit();
                SetUpEvents();
                SetUpGrdSoSelection();
                SetUpGrdLinesSoSelection();
                SetUpGrdLinesDeliveredGoods();
                SetupGrdItemsBatch();//Test Item Batch
                SetupGrdItemsBox();//Test Item Batch
                SetupGrdPackingBoxes();//Test Item Batch
                SetupPanelControl();
                SetVisiblePropertyByState();
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

        public override void bbiEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            base.bbiEdit_ItemClick(sender, e);

            try
            {
                if (_docHeadPK == null)
                {
                    MessageBox.Show(GlobalSetting.ResManager.GetString("NoDataSelected"));
                    RestoreInitState();
                }
                else if (_docHeadPK.IdSupplyStatus != Constants.SUPPLY_STATUS_OPEN)
                {
                    MessageBox.Show("Only OPEN Packing List can be edited.");
                    RestoreInitState();
                }
                else
                {
                    ConfigureActionsStackViewEditing();
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
                if (slueCustomer.EditValue == null)
                {
                    MessageBox.Show("Select a customer");
                    RestoreInitState();
                }
                else
                {
                    //buscamos las SO abiertas del customer
                    SearchCustomersSOs();
                }

                if (gridViewSoSelection.DataRowCount == 0)
                {
                    //Si no ha cargado datos SO restauramos el estado inicial
                    RestoreInitState();
                }
                else if (ValidateIfExistOpenPK() == false) //validamos 
                {
                    RestoreInitState();
                }
                else
                {
                    ConfigureActionsStackViewCreating();
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

                if (ValidatePK() == false)
                    return;

                DialogResult result = MessageBox.Show(GlobalSetting.ResManager.GetString("SaveChanges"), "", MessageBoxButtons.YesNo);

                if (result != DialogResult.Yes)
                    return;

                Cursor = Cursors.WaitCursor;

                if (CurrentState == ActionsStates.New)
                {
                    res = CreatePK();
                }
                else if (CurrentState == ActionsStates.Edit)
                {
                    res = UpdatePK();
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

        public override void bbiExportCsv_ItemClick(object sender, ItemClickEventArgs e)
        {

            if (gridViewLinesDeliveredGoods.DataRowCount == 0)
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
                    gridViewLinesDeliveredGoods.OptionsPrint.PrintFooter = false;
                    gridViewLinesDeliveredGoods.ExportToCsv(ExportCsvFile);

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
            if (gridViewLinesDeliveredGoods.DataRowCount == 0)
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
                    gridViewLinesDeliveredGoods.OptionsPrint.PrintFooter = false;
                    gridViewLinesDeliveredGoods.ExportToXlsx(ExportExcelFile);

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

        public override void BarButtonItemReport_ItemClick(object sender, ItemClickEventArgs e)
        {
            base.BarButtonItemReport_ItemClick(sender, e);
            try
            {
                if (_docHeadPK != null)
                {
                    OpenReport((FunctionalityReport)e.Item.Tag);
                }
                else
                {
                    XtraMessageBox.Show("No Invoice Selected", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Form Events
        private void PackingList_Load(object sender, EventArgs e)
        {

        }

        private void SbSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPKNumber.Text) == false)
                {
                    SearchPK();
                }
                else if (slueCustomer.EditValue != null)
                {
                    SearchCustomersSOs();
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtPKNumber_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (CurrentState == ActionsStates.Edit || CurrentState == ActionsStates.New)
                    return;

                if (e.KeyCode == Keys.Enter && string.IsNullOrEmpty(txtPKNumber.Text) == false)
                {
                    SearchPK();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtPKNumber_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (_isLoadingPacking || _isCreatingPacking)
                    return;

                ResetPK();
                ResetForm(resetPkNumber: false);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SbFinishPK_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    bool res = false;

                    if (ValidatePK() == false)
                        return;

                    DialogResult result = MessageBox.Show(GlobalSetting.ResManager.GetString("SaveChanges"), "", MessageBoxButtons.YesNo);

                    if (result != DialogResult.Yes)
                        return;

                    Cursor = Cursors.WaitCursor;

                    if (CurrentState == ActionsStates.Edit)
                    {
                        res = UpdatePK(finishPK: true);
                    }

                    if (res == true)
                    {
                        MessageBox.Show(GlobalSetting.ResManager.GetString("SaveSuccessfully"));
                        ShowMessageDocsGenerated();
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
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Grids events

        private void GridViewSoSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;

                //Sólo puede haber uno marcado
                if (e.Action == CollectionChangeAction.Add)
                {
                    view.BeginSelection();
                    view.ClearSelection();
                    view.SelectRow(view.FocusedRowHandle);
                    view.EndSelection();

                    if (view.GetRow(view.FocusedRowHandle) is DocHead doc)
                    {
                        _docLinesSoSelectionList = new BindingList<DocLine>(doc.Lines);
                        //cambiamos la cantidad Dummy (aquí la cantida da incluir en el packing) por la cantidad pendiente para facilitar al usuario
                        //The ToList is needed in order to evaluate the select immediately
                        _docLinesSoSelectionList.Select(a => { a.DummyQuantity = a.Quantity - a.DeliveredQuantity; return a; }).ToList();

                        xgrdLinesSoSelection.DataSource = null;
                        xgrdLinesSoSelection.DataSource = _docLinesSoSelectionList;
                        MarkSoSelectionFromDeliveredGoods();
                    }
                }
                else if (e.Action == CollectionChangeAction.Remove)
                {
                    _docLinesSoSelectionList = new BindingList<DocLine>();
                    xgrdLinesSoSelection.DataSource = null;
                    xgrdLinesSoSelection.DataSource = _docLinesSoSelectionList;
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GridViewLinesSoSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                DocLine line = view.GetRow(view.FocusedRowHandle) as DocLine;

                if (line == null)
                    return;

                if (CurrentState == ActionsStates.New || CurrentState == ActionsStates.Edit)
                {
                    if (e.Action == CollectionChangeAction.Add)
                    {
                        if (line.IdSupplyStatus == Constants.SUPPLY_STATUS_OPEN)
                        {
                            CopyToDeliveredGood(line);
                            gridViewLinesSoSelection.UpdateSummary();
                        }
                        else
                        {
                            XtraMessageBox.Show("This line is Close.");
                            view.BeginSelection();
                            gridViewLinesSoSelection.UnselectRow(view.FocusedRowHandle);
                            view.EndSelection();
                        }

                    }
                    else if (e.Action == CollectionChangeAction.Remove)
                    {
                        line.Quantity = line.QuantityOriginal;
                        gridViewLinesSoSelection.RefreshData();
                        DeleteDeliveredGood(line);
                        gridViewLinesSoSelection.UpdateSummary();
                        DeleteFromItemsBatchList(line.IdDoc, line.IdItemBcn, line.IdItemGroup); //Test Item Batch
                        DeleteFromItemsBoxList(line.IdDoc, line.IdItemBcn, line.IdItemGroup); //Test Item Batch
                    }
                    else if (e.Action == CollectionChangeAction.Refresh)
                    {
                        //**** si ha utilizado el marcar/desmarcar todo del header del grid ****//

                        if (_isMarkingSoSelFromDeliveredGoods == true)
                            return;

                        view.BeginSelection();

                        if (view.SelectedRowsCount == 0)
                        {
                            //quitamos todo lo del grid de Delivered goods y del grid de lotes de la SO seleccionada

                            var so = gridViewSoSelection.GetRow(gridViewSoSelection.FocusedRowHandle) as DocHead;

                            var listDG = _docLinesDeliveredGoodsList.Where(a => a.IdDocRelated.Equals(so.IdDoc)).ToList();
                            foreach (var item in listDG)
                                _docLinesDeliveredGoodsList.Remove(item);

                            var listIB = _auxItemsBatchList.Where(a => a.IdDocRelated.Equals(so.IdDoc)).ToList();
                            foreach (var item in listIB)
                                _auxItemsBatchList.Remove(item);

                            _itemsBatchList.RemoveAll(a => a.IdDocRelated.Equals(so.IdDoc));
                        }

                        //procesamos las marcadas
                        for (Int32 i = view.SelectedRowsCount - 1; i >= 0; i--)
                        {
                            var currentRowIndex = view.GetSelectedRows()[i];

                            DocLine selLine = view.GetRow(currentRowIndex) as DocLine;
                            if (selLine.IdSupplyStatus == Constants.SUPPLY_STATUS_OPEN)
                                CopyToDeliveredGood(selLine);
                            else
                                view.UnselectRow(currentRowIndex);

                        }

                        view.EndSelection();
                        gridViewLinesSoSelection.UpdateSummary();

                    }
                }
                else
                {
                    //Si no está editando/creando sólo se marcan las que están en el packing
                    var existInPk = _docLinesDeliveredGoodsList?.Where(a => a.IdItemBcn.Equals(line.IdItemBcn) && a.IdDocRelated.Equals(line.IdDoc)).FirstOrDefault();
                    if (existInPk == null)
                    {
                        view.BeginSelection();
                        gridViewLinesSoSelection.UnselectRow(view.FocusedRowHandle);
                        view.EndSelection();
                    }
                    else
                    {
                        //Tampoco se puede deseleccionar
                        if (e.Action == CollectionChangeAction.Remove)
                        {
                            view.BeginSelection();
                            gridViewLinesSoSelection.SelectRow(view.FocusedRowHandle);
                            view.EndSelection();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GridViewLinesSoSelection_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                if(view.GetRow(view.FocusedRowHandle) is DocLine line)
                    CopyToDeliveredGood(line);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GridViewLinesSoSelection_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;

                DocLine line = view.GetRow(view.FocusedRowHandle) as DocLine;

                switch (view.FocusedColumn.FieldName)
                {

                    case nameof(DocLine.DummyQuantity):

                        decimal qty = Convert.ToDecimal(e.Value);

                        //Sólo los RM puede ser decimales
                        if (line.IdItemGroup != Constants.ITEM_GROUP_MT)
                        {
                            bool isInteger = unchecked(qty == (int)qty);
                            if (isInteger == false)
                            {
                                e.Valid = false;
                                e.ErrorText = "Value must be integer";
                            }

                        }

                        if (qty > (line.Quantity - line.DeliveredQuantity))
                        {
                            e.Valid = false;
                            e.ErrorText = "Invalid quantity";
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GridViewLinesSoSelection_ShowingEditor(object sender, CancelEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                DocLine line = view.GetRow(view.FocusedRowHandle) as DocLine;

                if (gridViewLinesSoSelection.IsRowSelected(view.FocusedRowHandle) == false)
                    e.Cancel = true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GridViewLinesSoSelection_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            try
            {
                // Get the summary ID. 
                eGridLinesSoSelectionSummaries summaryID = (eGridLinesSoSelectionSummaries)(e.Item as GridSummaryItem).Tag;
                GridView view = sender as GridView;

                // Initialization 
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    _totalQuantityOrderedHwLinesSo = 0;
                    _totalQuantityOrderedMtLinesSo = 0;
                    _totalQuantityHwLinesSo = 0;
                    _totalQuantityMtLinesSo = 0;
                }

                // Calculation 
                if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DocLine row = view.GetRow(e.RowHandle) as DocLine;

                    switch (summaryID)
                    {
                        case eGridLinesSoSelectionSummaries.totalQuantityOrderedMt:

                            if (row.IdItemGroup == Constants.ITEM_GROUP_MT)
                                _totalQuantityOrderedMtLinesSo += Convert.ToDecimal(e.FieldValue);
                            break;

                        case eGridLinesSoSelectionSummaries.totalQuantityOrderedHw:

                            if (row.IdItemGroup == Constants.ITEM_GROUP_HW)
                                _totalQuantityOrderedHwLinesSo += Convert.ToInt32(e.FieldValue);
                            break;

                        case eGridLinesSoSelectionSummaries.totalQuantityMt:

                            if (row.IdItemGroup == Constants.ITEM_GROUP_MT && view.IsRowSelected(e.RowHandle) == true)
                                _totalQuantityMtLinesSo += Convert.ToDecimal(e.FieldValue);
                            break;

                        case eGridLinesSoSelectionSummaries.totalQuantityHw:

                            if (row.IdItemGroup == Constants.ITEM_GROUP_HW && view.IsRowSelected(e.RowHandle) == true)
                                _totalQuantityHwLinesSo += Convert.ToInt32(e.FieldValue);
                            break;
                    }
                }


                // Finalization
                if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    switch (summaryID)
                    {
                        case eGridLinesSoSelectionSummaries.totalQuantityOrderedMt:
                            e.TotalValue = _totalQuantityOrderedMtLinesSo;
                            break;
                        case eGridLinesSoSelectionSummaries.totalQuantityOrderedHw:
                            e.TotalValue = _totalQuantityOrderedHwLinesSo;
                            break;
                        case eGridLinesSoSelectionSummaries.totalQuantityMt:
                            e.TotalValue = _totalQuantityMtLinesSo;
                            break;
                        case eGridLinesSoSelectionSummaries.totalQuantityHw:
                            e.TotalValue = _totalQuantityHwLinesSo;
                            break;
                    }
                }


            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GridViewLinesSoSelection_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                if (e.Column.FieldName == "PENDING_QTY" && e.IsGetData)
                    e.Value = (
                        (decimal)view.GetRowCellValue(e.ListSourceRowIndex, nameof(DocLine.Quantity)) -
                        (decimal)view.GetRowCellValue(e.ListSourceRowIndex, nameof(DocLine.DeliveredQuantity))
                        );
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GridViewLinesSoSelection_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;

                if (e.Column.FieldName == nameof(DocLine.IdSupplyStatus))
                {
                    var status = view.GetRowCellValue(e.RowHandle, nameof(DocLine.IdSupplyStatus)) as string;

                    switch (status)
                    {
                        case Constants.SUPPLY_STATUS_OPEN:
                            e.Appearance.BackColor = AppStyles.SupplyStatusOpnBKGD1;
                            e.Appearance.BackColor2 = AppStyles.SupplyStatusOpnBKGD2;
                            break;

                        case Constants.SUPPLY_STATUS_CLOSE:
                            e.Appearance.BackColor = AppStyles.SupplyStatusClsBKGD1;
                            e.Appearance.BackColor2 = AppStyles.SupplyStatusClsBKGD2;
                            break;

                        case Constants.SUPPLY_STATUS_CANCEL:
                            e.Appearance.BackColor = AppStyles.SupplyStatusCnlBKGD1;
                            e.Appearance.BackColor2 = AppStyles.SupplyStatusCnlBKGD2;
                            break;
                    }

                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GridViewLinesDeliveredGoods_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            try
            {
                // Get the summary ID. 
                eGridLinesDeliveredGoodsSummaries summaryID = (eGridLinesDeliveredGoodsSummaries)(e.Item as GridSummaryItem).Tag;
                GridView view = sender as GridView;

                // Initialization 
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    _totalQuantityOrderedHwDeliveredGoods = 0;
                    _totalQuantityOrderedMtDeliveredGoods = 0;
                    _totalQuantityHwDeliveredGoods = 0;
                    _totalQuantityMtDeliveredGoods = 0;
                }

                // Calculation 
                if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DocLine row = view.GetRow(e.RowHandle) as DocLine;

                    switch (summaryID)
                    {
                        case eGridLinesDeliveredGoodsSummaries.totalQuantityOrderedMt:

                            if (row.IdItemGroup == Constants.ITEM_GROUP_MT)
                                _totalQuantityOrderedMtDeliveredGoods += Convert.ToDecimal(e.FieldValue);
                            break;

                        case eGridLinesDeliveredGoodsSummaries.totalQuantityOrderedHw:

                            if (row.IdItemGroup == Constants.ITEM_GROUP_HW)
                                _totalQuantityOrderedHwDeliveredGoods += Convert.ToInt32(e.FieldValue);
                            break;

                        case eGridLinesDeliveredGoodsSummaries.totalQuantityMt:

                            if (row.IdItemGroup == Constants.ITEM_GROUP_MT)
                                _totalQuantityMtDeliveredGoods += Convert.ToDecimal(e.FieldValue);
                            break;

                        case eGridLinesDeliveredGoodsSummaries.totalQuantityHw:

                            if (row.IdItemGroup == Constants.ITEM_GROUP_HW)
                                _totalQuantityHwDeliveredGoods += Convert.ToInt32(e.FieldValue);
                            break;
                    }
                }

                // Finalization
                if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    switch (summaryID)
                    {
                        case eGridLinesDeliveredGoodsSummaries.totalQuantityOrderedMt:
                            e.TotalValue = _totalQuantityOrderedMtDeliveredGoods;
                            break;
                        case eGridLinesDeliveredGoodsSummaries.totalQuantityOrderedHw:
                            e.TotalValue = _totalQuantityOrderedHwDeliveredGoods;
                            break;
                        case eGridLinesDeliveredGoodsSummaries.totalQuantityMt:
                            e.TotalValue = _totalQuantityMtDeliveredGoods;
                            break;
                        case eGridLinesDeliveredGoodsSummaries.totalQuantityHw:
                            e.TotalValue = _totalQuantityHwDeliveredGoods;
                            break;
                    }
                }


            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Test Item Batch INI

        private void GridViewItemsBatch_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            try
            {
                switch (e.Column.FieldName)
                {
                    case nameof(PackingListItemBatch.Batch):
                        GridView view = sender as GridView;
                        PackingListItemBatch itemBatch = view.GetRow(view.FocusedRowHandle) as PackingListItemBatch;

                        if (itemBatch == null)
                            return;
                        CopyToItemBatchList(itemBatch);
                        break;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void XgrdItemsBox_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                if (CurrentState != ActionsStates.Edit && CurrentState != ActionsStates.New)
                    return;

                GridView view = xgrdItemsBox.FocusedView as GridView;
                PackingListItemBox itemBox = view.GetRow(view.FocusedRowHandle) as PackingListItemBox;

                if (itemBox == null)
                    return;

                if (e.KeyCode == Keys.F4)
                {
                    DialogResult result = XtraMessageBox.Show("Delete row?", "Confirmation", MessageBoxButtons.YesNo);
                    if (result != DialogResult.Yes)
                        return;

                    var boxTemp = _itemsBoxList
                        .Where(a => a.IdItemBcn.Equals(itemBox.IdItemBcn) && a.BoxNumber.Equals(itemBox.BoxNumber) && a.IdDocRelated.Equals(itemBox.IdDocRelated))
                        .FirstOrDefault();

                    _auxItemsBoxList.Remove(itemBox);
                    _itemsBoxList.Remove(boxTemp);
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    if (view.FocusedRowHandle == view.RowCount - 1)
                    {
                        if (itemBox.BoxNumber > 0 && itemBox.PcQuantity > 0)
                        {
                            _auxItemsBoxList.Add(
                                new PackingListItemBox()
                                {
                                    IdDoc = itemBox.IdDoc,
                                    IdDocRelated = itemBox.IdDocRelated,
                                    IdItemBcn = itemBox.IdItemBcn,
                                    IdItemGroup = itemBox.IdItemGroup
                                });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GridViewItemsBox_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            try
            {
                switch (e.Column.FieldName)
                {
                    case nameof(PackingListItemBox.BoxNumber):
                        GridView view = sender as GridView;
                        PackingListItemBox itemBox = view.GetRow(view.FocusedRowHandle) as PackingListItemBox;

                        if (itemBox == null)
                            return;
                        CopyToItemBoxList(itemBox);
                        break;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GridViewItemsBox_ShownEditor(object sender, EventArgs e)
        {
            try
            {
                GridView view = sender as GridView;

                switch (view.FocusedColumn.FieldName)
                {
                    case nameof(PackingListItemBox.BoxNumber):
                        LookUpEdit riPackingBoxes = (LookUpEdit)view.ActiveEditor;
                        riPackingBoxes.Properties.DataSource = _docBoxList.Where(a => string.IsNullOrEmpty(a.IdBox) == false).ToList();
                        break;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void XgrdItemsBatch_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                if (CurrentState != ActionsStates.Edit && CurrentState != ActionsStates.New)
                    return;

                GridView view = xgrdItemsBatch.FocusedView as GridView;
                PackingListItemBatch itemBatch = view.GetRow(view.FocusedRowHandle) as PackingListItemBatch;

                if (itemBatch == null)
                    return;

                if (e.KeyCode == Keys.F4)
                {
                    DialogResult result = XtraMessageBox.Show("Delete row?", "Confirmation", MessageBoxButtons.YesNo);
                    if (result != DialogResult.Yes)
                        return;

                    var batchTmp = _itemsBatchList.Where(a => a.IdItemBcn.Equals(itemBatch.IdItemBcn) && a.Batch.Equals(itemBatch.Batch)).FirstOrDefault();

                    _auxItemsBatchList.Remove(itemBatch);
                    _itemsBatchList.Remove(batchTmp);
                }

                if (e.KeyCode == Keys.Enter)
                {
                    if (view.FocusedRowHandle == view.RowCount - 1)
                    {
                        if (String.IsNullOrEmpty(itemBatch.Batch) == false && itemBatch.Quantity > 0)
                        {
                            _auxItemsBatchList.Add(
                                new PackingListItemBatch()
                                {
                                    IdDoc = itemBatch.IdDoc,
                                    IdDocRelated = itemBatch.IdDocRelated,
                                    IdItemBcn = itemBatch.IdItemBcn,
                                    IdItemGroup = itemBatch.IdItemGroup
                                });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void XgrdPackingBoxes_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                if (CurrentState != ActionsStates.Edit && CurrentState != ActionsStates.New)
                    return;

                GridView view = xgrdPackingBoxes.FocusedView as GridView;
                DocBox packingListBox = view.GetRow(view.FocusedRowHandle) as DocBox;

                if (packingListBox == null)
                    return;

                if (e.KeyCode == Keys.F4)
                {
                    DialogResult result = XtraMessageBox.Show("Delete row?", "Confirmation", MessageBoxButtons.YesNo);
                    if (result != DialogResult.Yes)
                        return;

                    _docBoxList.Remove(packingListBox);
                    UpdateBoxNumber();
                    UpdateBoxNumberItemsBox(packingListBox.BoxNumber);
                }

                if (e.KeyCode == Keys.Enter)
                {
                    if (view.FocusedRowHandle == view.RowCount - 1)
                    {
                        if (string.IsNullOrEmpty(packingListBox.IdBox) == false)
                        {
                            _docBoxList.Add(new DocBox() { BoxNumber = view.RowCount + 1, IdDoc = txtPKNumber.Text });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GridViewLinesDeliveredGoods_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                DocLine lineDeliveredGoods = view.GetRow(view.FocusedRowHandle) as DocLine;

                if (lineDeliveredGoods == null || _auxItemsBatchList == null)
                    return;

                //Buscamos los lotes referentes a ese item
                _auxItemsBatchList = new BindingList<PackingListItemBatch>(
                    _itemsBatchList
                    .Where(a => a.IdItemBcn.Equals(lineDeliveredGoods.IdItemBcn) && a.IdItemGroup.Equals(lineDeliveredGoods.IdItemGroup))
                    .ToList());

                xgrdItemsBatch.DataSource = null;
                xgrdItemsBatch.DataSource = _auxItemsBatchList;

                //Buscamos las cajas referentes a ese item
                _auxItemsBoxList = new BindingList<PackingListItemBox>(
                    _itemsBoxList
                    .Where(a => a.IdItemBcn.Equals(lineDeliveredGoods.IdItemBcn) && a.IdItemGroup.Equals(lineDeliveredGoods.IdItemGroup))
                    .ToList());

                xgrdItemsBox.DataSource = null;
                xgrdItemsBox.DataSource = _auxItemsBoxList;

                if (CurrentState == ActionsStates.New || CurrentState == ActionsStates.Edit)
                {
                    _auxItemsBatchList.Add(
                        new PackingListItemBatch()
                        {
                            IdDoc = lineDeliveredGoods.IdDoc,
                            IdDocRelated = lineDeliveredGoods.IdDocRelated,
                            IdItemBcn = lineDeliveredGoods.IdItemBcn,
                            IdItemGroup = lineDeliveredGoods.IdItemGroup
                        });

                    _auxItemsBoxList.Add(
                        new PackingListItemBox()
                        {
                            IdDoc = lineDeliveredGoods.IdDoc,
                            IdDocRelated = lineDeliveredGoods.IdDocRelated,
                            IdItemBcn = lineDeliveredGoods.IdItemBcn,
                            IdItemGroup = lineDeliveredGoods.IdItemGroup
                        });
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Test Item Batch END

        #endregion

        private void DateEditPKCreation_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                lblPKDocDateWeek.Text = dateEditPKDocDate.DateTime.GetWeek().ToString();

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DateEditPKDelivery_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                lblPKDeliveryWeek.Text = dateEditPKDelivery.DateTime.GetWeek().ToString();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SlueCustomer_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (slueCustomer.EditValue != null)
                {
                    if (_isLoadingPacking == false)
                    {
                        var customer = slueCustomer.EditValue;
                        ResetPK();
                        ResetForm(resetCustomer: false);
                        slueCustomer.EditValue = customer;
                    }

                    SetCustomerInfo();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Public Methods
        public void InitData(string idDoc)
        {
            try
            {
                txtPKNumber.EditValue = idDoc;
                SearchPK();

            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Private Methods

        #region Load/Resets
        private void LoadAuxList()
        {
            try
            {
                _customersList = GlobalSetting.CustomerService.GetCustomers().Where(a => a.Factory == true).ToList();
                _currenciesList = GlobalSetting.CurrencyService.GetCurrencies();
                _deliveryTermList = GlobalSetting.DeliveryTermsService.GetDeliveryTerms();
                _paymentTermsList = GlobalSetting.PaymentTermsService.GetPaymentTerms();
                _supplyStatusList = GlobalSetting.SupplyDocsService.GetSupplyStatus();
                _boxList = GlobalSetting.BoxService.GetBoxes(); //Test Item Batch
            }
            catch
            {
                throw;
            }
        }

        private void ResetCustomerSOs()
        {
            try
            {
                _docSoSelectionList = null;
                _docLinesSoSelectionList = null;
                _docLinesDeliveredGoodsList = null;
                _auxItemsBatchList = null; //Test Item Batch
                _itemsBatchList = null; //Test Item Batch
                _auxItemsBoxList = null; //Test Item Batch
                _itemsBoxList = null; //Test Item Batch
                _docBoxList = null; //Test Item Batch
            }
            catch
            {
                throw;
            }
        }

        private void ResetPK()
        {
            try
            {
                ResetCustomerSOs();
                _docHeadPK = null;
                xgrdSoSelection.DataSource = null;
                xgrdLinesSoSelection.DataSource = null;
                xgrdLinesDeliveredGoods.DataSource = null;

            }
            catch
            {
                throw;
            }
        }

        private void Reset4NewPk()
        {
            try
            {
                _docHeadPK = null;
                _docLinesSoSelectionList = null;
                _docLinesDeliveredGoodsList = null;
                _auxItemsBatchList = null; //Test Item Batch
                _itemsBatchList = null; //Test Item Batch
                _auxItemsBoxList = null; //Test Item Batch
                _itemsBoxList = null; //Test Item Batch
                _docBoxList = null; //Test Item Batch

                xgrdLinesSoSelection.DataSource = null;
                xgrdLinesDeliveredGoods.DataSource = null;

                dateEditPKDelivery.EditValue = null;
                lblPKDeliveryWeek.Text = string.Empty;

                slueDeliveryTerms.EditValue = null;
                //slueCurrency.EditValue = null;
                sluePaymentTerm.EditValue = null;

                txtManualReference.EditValue = null;
                memoEditRemarks.EditValue = null;

                gridViewSoSelection.BeginSelection();
                gridViewSoSelection.ClearSelection();
                gridViewSoSelection.EndSelection();

            }
            catch
            {
                throw;
            }
        }

        private void SearchPK()
        {
            try
            {
                _isLoadingPacking = true;
                ResetPK();
                ResetForm(resetPkNumber: false);

                string pkNumber = txtPKNumber.Text;

                //_docHeadPK = GlobalSetting.SupplyDocsService.GetDoc(pkNumber);
                _docHeadPK = GlobalSetting.SupplyDocsService.GetDocPackingList(pkNumber); //Test Item Batch

                if (_docHeadPK == null)
                {
                    XtraMessageBox.Show("No Data Found", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (_docHeadPK.IdSupplyDocType != Constants.SUPPLY_DOCTYPE_PL)
                {
                    XtraMessageBox.Show("Document is not a Packing List", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (_docHeadPK.IdCustomer.IsOneOf(Constants.ETNIA_BCN_COMPANY_CODE, Constants.ETNIA_HK_COMPANY_CODE))
                {
                    XtraMessageBox.Show("Packing List is not to a Factory", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    LoadPK();
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                _isLoadingPacking = false;
            }
        }

        private void LoadPK()
        {
            try
            {
                var customer = _customersList.Where(a => a.IdCustomer.Equals(_docHeadPK.IdCustomer)).FirstOrDefault();

                //***** Header *****/
                lbltxtStatus.Visible = true;
                lbltxtStatus.Text = _docHeadPK.IdSupplyStatus;
                slueCustomer.EditValue = _docHeadPK.IdCustomer;
                slueDeliveryTerms.EditValue = _docHeadPK.IdDeliveryTerm;
                slueCurrency.EditValue = _docHeadPK.IdCurrency;

                dateEditPKDocDate.EditValue = _docHeadPK.DocDate;
                dateEditPKDelivery.EditValue = _docHeadPK.DeliveryDate;

                lblPKDocDateWeek.Text = dateEditPKDocDate.DateTime.GetWeek().ToString();
                lblPKDeliveryWeek.Text = dateEditPKDelivery.DateTime.GetWeek().ToString();

                txtManualReference.Text = _docHeadPK.ManualReference;
                memoEditRemarks.Text = _docHeadPK.Remarks;

                //***** Grid SO Selection *****/
                ResetCustomerSOs();
                var packingSOs = GlobalSetting.SupplyDocsService.GetSalesOrderFromPackingList(_docHeadPK.IdDoc);
                _docSoSelectionList = new BindingList<DocHead>(packingSOs);
                xgrdSoSelection.DataSource = null;
                xgrdSoSelection.DataSource = _docSoSelectionList;

                //Test Item Batch INI

                //***** Grid Packing Boxes *****/
                _docBoxList = new BindingList<DocBox>(_docHeadPK.Boxes);
                xgrdPackingBoxes.DataSource = null;
                xgrdPackingBoxes.DataSource = _docBoxList;

                //***** Grid Batches *****/
                _auxItemsBatchList = new BindingList<PackingListItemBatch>();
                _itemsBatchList = _docHeadPK.PackingListItemBatches;

                //***** Grid Items Boxes *****/
                _auxItemsBoxList = new BindingList<PackingListItemBox>();
                _itemsBoxList = _docHeadPK.PackingListItemBoxes;

                //Remarks: Hacemos antes la carga de los lotes y cajas y después cargamos el grid de delivered goods, al cargar lanzará el evento de cambio de foco en la fila
                //y cargará los datos que corresponden a esa fila en el grid de lotes

                //Test Item Batch END

                //***** Grid Delivered Goods *****/
                _docLinesDeliveredGoodsList = new BindingList<DocLine>(_docHeadPK.Lines);

                xgrdLinesDeliveredGoods.DataSource = null;
                xgrdLinesDeliveredGoods.DataSource = _docLinesDeliveredGoodsList;

                //***** Terms Tab *****/
                lblTxtCompany.Text = customer.CustomerName;
                lblTxtAddress.Text = customer.ShippingAddress;
                lblTxtContact.Text = $"{customer.ContactName} ({customer.ContactPhone})";
                lblTxtShipTo.Text = "??"; //TODO
                lblTxtInvoiceTo.Text = "??"; //TODO
                sluePaymentTerm.EditValue = _docHeadPK.IdPaymentTerms;

            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Setup Form Objects

        private void SetUpTabs()
        {
            try
            {
                xtpPakingList.AutoScroll = true;
                xtpPakingList.AutoScrollMargin = new Size(20, 20);
                xtpPakingList.AutoScrollMinSize = new Size(xtpPakingList.Width, xtpPakingList.Height);
            }
            catch
            {
                throw;
            }
        }

        private void SetUpLabels()
        {
            try
            {
                /********* Fonts **********/
                //Header 
                lblPKNumber.Font = _labelDefaultFontBold;
                lbltxtStatus.Font = _labelDefaultFontBold;
                lblDate.Font = _labelDefaultFontBold;
                lblWeek.Font = _labelDefaultFontBold;
                lblPKDocDate.Font = _labelDefaultFontBold;
                lblPKDelivery.Font = _labelDefaultFontBold;
                lblCustomer.Font = _labelDefaultFontBold;
                lblPKDocDateWeek.Font = _labelDefaultFont;
                lblPKDeliveryWeek.Font = _labelDefaultFont;
                txtPKNumber.Font = _labelDefaultFontBold;
                lblTermsOfDelivery.Font = _labelDefaultFont;
                lblCurrency.Font = _labelDefaultFont;
                lblManualReference.Font = _labelDefaultFont;
                lblRemarks.Font = _labelDefaultFont;

                //Terms Tab
                lblCompany.Font = _labelDefaultFontBold;
                lblAddress.Font = _labelDefaultFontBold;
                lblContact.Font = _labelDefaultFontBold;
                lblShipTo.Font = _labelDefaultFontBold;
                lblInvoiceTo.Font = _labelDefaultFontBold;
                lblTermPayment.Font = _labelDefaultFontBold;
                lblTxtCompany.Font = _labelDefaultFontBold;
                lblTxtAddress.Font = _labelDefaultFont;
                lblTxtContact.Font = _labelDefaultFont;
                lblTxtShipTo.Font = _labelDefaultFont;
                lblTxtInvoiceTo.Font = _labelDefaultFont;

                /********* Texts **********/
                //Headers
                lblPKNumber.Text = "PK Number";
                lbltxtStatus.Text = string.Empty;
                lblDate.Text = "Date";
                lblWeek.Text = "Week";
                lblPKDocDate.Text = "DATE";
                lblPKDelivery.Text = "DELIVERY";
                lblCustomer.Text = "CUSTOMER";
                lblPKDocDateWeek.Text = string.Empty;
                lblPKDeliveryWeek.Text = string.Empty;
                lblTermsOfDelivery.Text = "Terms of Delivery";
                lblCurrency.Text = "Currency";
                lblManualReference.Text = "Manual Reference";
                lblRemarks.Text = "Remarks";

                //Terms Tab
                lblCompany.Text = "Company:";
                lblAddress.Text = "Address:";
                lblContact.Text = "Contact:";
                lblShipTo.Text = "Ship To:";
                lblInvoiceTo.Text = "Invoice To:";
                lblTermPayment.Text = "Term of Payment:";
                lblTxtCompany.Text = string.Empty;
                lblTxtAddress.Text = string.Empty;
                lblTxtContact.Text = string.Empty;
                lblTxtShipTo.Text = string.Empty;
                lblTxtInvoiceTo.Text = string.Empty;

                /********* Align **********/
                //Headers
                lbltxtStatus.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                lblPKDocDateWeek.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                lblPKDeliveryWeek.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                txtPKNumber.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;

                //Terms Tab
                lblCompany.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
                lblAddress.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
                lblContact.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
                lblShipTo.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
                lblInvoiceTo.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
                lblTermPayment.Appearance.TextOptions.HAlignment = HorzAlignment.Far;

                /********* BackColor **********/
                txtPKNumber.Properties.Appearance.BackColor = Color.CadetBlue;
                txtPKNumber.Properties.Appearance.BackColor2 = Color.CadetBlue;

            }
            catch
            {
                throw;
            }
        }

        private void SetObjectsReadOnly()
        {
            try
            {
                dateEditPKDocDate.ReadOnly = true;
                dateEditPKDelivery.ReadOnly = true;
                slueDeliveryTerms.ReadOnly = true;
                slueCurrency.ReadOnly = true;
                sluePaymentTerm.ReadOnly = true;
                txtManualReference.ReadOnly = true;
                memoEditRemarks.ReadOnly = true;
            }
            catch
            {
                throw;
            }
        }

        private void SetObjectsEnableToCreate()
        {
            try
            {
                dateEditPKDelivery.ReadOnly = false;
                slueDeliveryTerms.ReadOnly = false;
                //slueCurrency.ReadOnly = false;
                sluePaymentTerm.ReadOnly = false;
                txtManualReference.ReadOnly = false;
                memoEditRemarks.ReadOnly = false;
            }
            catch
            {
                throw;
            }
        }

        private void SetObjectsEnableToEdit()
        {
            try
            {
                txtManualReference.ReadOnly = false;
                memoEditRemarks.ReadOnly = false;
            }
            catch
            {
                throw;
            }
        }

        private void SetUpButtons()
        {
            try
            {

            }
            catch
            {
                throw;
            }
        }

        private void SetUpSearchLookUpEdit()
        {
            try
            {
                SetUpSlueCustomer();
                SetUpSlueCurrency();
                SetUpSlueDeliveryTerms();
                SetUpSluePaymentTerms();
            }
            catch
            {
                throw;
            }
        }

        private void SetUpSlueCustomer()
        {
            try
            {
                slueCustomer.Properties.DataSource = _customersList;
                slueCustomer.Properties.ValueMember = nameof(Customer.IdCustomer);
                slueCustomer.Properties.DisplayMember = nameof(Customer.CustomerName);
                slueCustomer.Properties.View.Columns.AddField(nameof(Customer.IdCustomer)).Visible = true;
                slueCustomer.Properties.View.Columns.AddField(nameof(Customer.CustomerName)).Visible = true;
                slueCustomer.Properties.NullText = "Select Customer...";
            }
            catch
            {
                throw;
            }
        }

        private void SetUpSlueCurrency()
        {
            try
            {
                slueCurrency.Properties.DataSource = _currenciesList;
                slueCurrency.Properties.ValueMember = nameof(Currency.IdCurrency);
                slueCurrency.Properties.DisplayMember = nameof(Currency.Description);
                slueCurrency.Properties.View.Columns.AddField(nameof(Currency.IdCurrency)).Visible = true;
                slueCurrency.Properties.View.Columns.AddField(nameof(Currency.Description)).Visible = true;
                slueCurrency.Properties.NullText = "Select Currency...";
            }
            catch
            {
                throw;
            }
        }

        private void SetUpSlueDeliveryTerms()
        {
            try
            {
                slueDeliveryTerms.Properties.DataSource = _deliveryTermList;
                slueDeliveryTerms.Properties.ValueMember = nameof(DeliveryTerm.IdDeliveryTerm);
                slueDeliveryTerms.Properties.DisplayMember = nameof(DeliveryTerm.Description);
                slueDeliveryTerms.Properties.View.Columns.AddField(nameof(DeliveryTerm.IdDeliveryTerm)).Visible = true;
                slueDeliveryTerms.Properties.View.Columns.AddField(nameof(DeliveryTerm.Description)).Visible = true;
                slueDeliveryTerms.Properties.NullText = "Select Delivery Term...";
                slueDeliveryTerms.Properties.ShowClearButton = false;
            }
            catch
            {
                throw;
            }
        }

        private void SetUpSluePaymentTerms()
        {
            try
            {
                sluePaymentTerm.Properties.DataSource = _paymentTermsList;
                sluePaymentTerm.Properties.ValueMember = nameof(PaymentTerms.IdPaymentTerms);
                sluePaymentTerm.Properties.DisplayMember = nameof(PaymentTerms.Description);
                sluePaymentTerm.Properties.View.Columns.AddField(nameof(PaymentTerms.IdPaymentTerms)).Visible = true;
                sluePaymentTerm.Properties.View.Columns.AddField(nameof(PaymentTerms.Description)).Visible = true;
                sluePaymentTerm.Properties.NullText = "Select Payment Term...";
                sluePaymentTerm.Properties.ShowClearButton = false;
            }
            catch
            {
                throw;
            }
        }

        private void SetUpEvents()
        {
            try
            {
                sbSearch.Click += SbSearch_Click;
                sbFinishPK.Click += SbFinishPK_Click;
                dateEditPKDocDate.EditValueChanged += DateEditPKCreation_EditValueChanged;
                dateEditPKDelivery.EditValueChanged += DateEditPKDelivery_EditValueChanged;
                slueCustomer.EditValueChanged += SlueCustomer_EditValueChanged;
                txtPKNumber.KeyDown += TxtPKNumber_KeyDown;
                txtPKNumber.EditValueChanged += TxtPKNumber_EditValueChanged;
            }
            catch
            {
                throw;
            }
        }

        private void SetUpGrdSoSelection()
        {
            try
            {
                //Activar que se alternen los colores de las filas del grid
                gridViewSoSelection.OptionsView.EnableAppearanceOddRow = true;
                gridViewSoSelection.OptionsView.EnableAppearanceEvenRow = true;

                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                gridViewSoSelection.OptionsView.ColumnAutoWidth = false;
                gridViewSoSelection.HorzScrollVisibility = ScrollVisibility.Auto;

                //Hacer todo el grid no editable
                gridViewSoSelection.OptionsBehavior.Editable = false;

                //Hide grouping panel
                gridViewSoSelection.OptionsView.ShowGroupPanel = false;

                //select with checbox
                gridViewSoSelection.OptionsSelection.MultiSelect = true;
                gridViewSoSelection.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
                gridViewSoSelection.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DefaultBoolean.False;

                //Disable master/detail for doc lines.
                gridViewSoSelection.OptionsDetail.EnableMasterViewMode = false;

                //Column Definition
                GridColumn colIdDoc = new GridColumn() { Caption = "SALES ORDER", Visible = true, FieldName = nameof(DocHead.IdDoc), Width = 150 };
                GridColumn colCreationDate = new GridColumn() { Caption = "CREATION ORDER", Visible = true, FieldName = nameof(DocHead.CreationDate), Width = 150 };
                GridColumn colDeliveryDate = new GridColumn() { Caption = "DELIVERY DATE", Visible = true, FieldName = nameof(DocHead.DeliveryDate), Width = 150 };
                GridColumn colIdDeliveryTerm = new GridColumn() { Caption = "TOD", Visible = true, FieldName = nameof(DocHead.IdDeliveryTerm), Width = 200 };
                GridColumn colIdCurrency = new GridColumn() { Caption = "CURRENCY", Visible = true, FieldName = nameof(DocHead.IdCurrency), Width = 150 };

                //GridColumn colTotalAmount = new GridColumn() { Caption = "SALES ORDER", Visible = true, FieldName = nameof(DocHead.IdDoc), Width = 100 };

                //Display Format
                //colTotalAmount.DisplayFormat.FormatType = FormatType.Numeric;
                //colTotalAmount.DisplayFormat.FormatString = "n2";

                gridViewSoSelection.Columns.Add(colIdDoc);
                gridViewSoSelection.Columns.Add(colCreationDate);
                gridViewSoSelection.Columns.Add(colDeliveryDate);
                gridViewSoSelection.Columns.Add(colIdDeliveryTerm);
                gridViewSoSelection.Columns.Add(colIdCurrency);
                //gridViewSoSelection.Columns.Add(colTotalAmount);

                //Events
                gridViewSoSelection.SelectionChanged += GridViewSoSelection_SelectionChanged;

            }
            catch
            {
                throw;
            }
        }

        private void SetUpGrdLinesSoSelection()
        {
            try
            {
                //Activar que se alternen los colores de las filas del grid
                gridViewLinesSoSelection.OptionsView.EnableAppearanceOddRow = true;
                gridViewLinesSoSelection.OptionsView.EnableAppearanceEvenRow = true;

                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                gridViewLinesSoSelection.OptionsView.ColumnAutoWidth = false;
                gridViewLinesSoSelection.HorzScrollVisibility = ScrollVisibility.Auto;

                //Hacer todo el grid no editable
                gridViewLinesSoSelection.OptionsBehavior.Editable = false;

                //Hide grouping panel
                gridViewLinesSoSelection.OptionsView.ShowGroupPanel = false;

                //select with checbox
                gridViewLinesSoSelection.OptionsSelection.MultiSelect = true;
                gridViewLinesSoSelection.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
                gridViewLinesSoSelection.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DefaultBoolean.False; //para desactivar el marcar/desmarcar todos del header

                //Column Definition
                GridColumn colIdItemBcn = new GridColumn() { Caption = "Item Code", Visible = true, FieldName = nameof(DocLine.IdItemBcn), Width = 200 };
                GridColumn colDescription = new GridColumn() { Caption = "Description", Visible = true, FieldName = nameof(DocLine.ItemDesc), Width = 350 };
                GridColumn colIdItemGroup = new GridColumn() { Caption = "Type", Visible = true, FieldName = nameof(DocLine.IdItemGroup), Width = 100 };
                GridColumn colQuantity = new GridColumn() { Caption = "Order Quantity", Visible = true, FieldName = nameof(DocLine.Quantity), Width = 110 };
                GridColumn colDeliveredQuantity = new GridColumn() { Caption = "Delivered Qty", Visible = true, FieldName = nameof(DocLine.DeliveredQuantity), Width = 110 };
                GridColumn colDummyQuantity = new GridColumn() { Caption = "Packing Quantity", Visible = true, FieldName = nameof(DocLine.DummyQuantity), Width = 110 };
                GridColumn colUnit = new GridColumn() { Caption = "Unit", Visible = true, FieldName = nameof(DocLine.ItemUnit), Width = 85 };
                GridColumn colUnitPrice = new GridColumn() { Caption = "Unit Price", Visible = true, FieldName = nameof(DocLine.UnitPrice), Width = 85 };
                GridColumn colTotalAmount = new GridColumn() { Caption = "TotalAmount", Visible = true, FieldName = nameof(DocLine.TotalAmount), Width = 120 };
                GridColumn colIdIdSupplyStatus = new GridColumn() { Caption = "Status", Visible = true, FieldName = nameof(DocLine.IdSupplyStatus), Width = 75 };
                //Unbound Column. Para calcular la cantidad pendiente
                GridColumn colPendingQuantity = new GridColumn() { Caption = "Pending Qty", Visible = true, FieldName = "PENDING_QTY", Width = 110, UnboundType = UnboundColumnType.Integer };

                //Display Format
                colUnitPrice.DisplayFormat.FormatType = FormatType.Numeric;
                colUnitPrice.DisplayFormat.FormatString = "n4";

                colTotalAmount.DisplayFormat.FormatType = FormatType.Numeric;
                colTotalAmount.DisplayFormat.FormatString = "n4";

                colQuantity.DisplayFormat.FormatType = FormatType.Numeric;
                colQuantity.DisplayFormat.FormatString = "n3";

                colDeliveredQuantity.DisplayFormat.FormatType = FormatType.Numeric;
                colDeliveredQuantity.DisplayFormat.FormatString = "n3";

                colPendingQuantity.DisplayFormat.FormatType = FormatType.Numeric;
                colPendingQuantity.DisplayFormat.FormatString = "n3";

                colDummyQuantity.DisplayFormat.FormatType = FormatType.Numeric;
                colDummyQuantity.DisplayFormat.FormatString = "n3";

                //Edit Repositories
                RepositoryItemTextEdit ritxt3Dec = new RepositoryItemTextEdit();
                ritxt3Dec.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                ritxt3Dec.Mask.EditMask = "F3";
                ritxt3Dec.AllowNullInput = DefaultBoolean.True;

                colDummyQuantity.ColumnEdit = ritxt3Dec;

                RepositoryItemSearchLookUpEdit riSupplyStatus = new RepositoryItemSearchLookUpEdit()
                {
                    DataSource = _supplyStatusList,
                    ValueMember = nameof(SupplyStatus.IdSupplyStatus),
                    DisplayMember = nameof(SupplyStatus.Description),
                    ShowClearButton = false,
                    NullText = string.Empty,
                };
                riSupplyStatus.ShowClearButton = false;
                colIdIdSupplyStatus.ColumnEdit = riSupplyStatus;

                //Summaries
                gridViewLinesSoSelection.OptionsView.ShowFooter = true;

                colTotalAmount.Summary.Add(SummaryItemType.Sum, nameof(DocLine.TotalAmount), "{0:n4}");

                colDummyQuantity.Summary.AddRange(new GridSummaryItem[] {
                    new GridColumnSummaryItem(SummaryItemType.Custom, nameof(DocLine.DummyQuantity), "{0:n3} KG", eGridLinesSoSelectionSummaries.totalQuantityMt),
                    new GridColumnSummaryItem(SummaryItemType.Custom, nameof(DocLine.DummyQuantity), "{0} PC", eGridLinesSoSelectionSummaries.totalQuantityHw) });

                colQuantity.Summary.AddRange(new GridSummaryItem[] {
                    new GridColumnSummaryItem(SummaryItemType.Custom, nameof(DocLine.Quantity), "{0:n3} KG", eGridLinesSoSelectionSummaries.totalQuantityOrderedMt),
                    new GridColumnSummaryItem(SummaryItemType.Custom, nameof(DocLine.Quantity), "{0} PC", eGridLinesSoSelectionSummaries.totalQuantityOrderedHw) });

                //Add columns to grid root view
                gridViewLinesSoSelection.Columns.Add(colIdItemBcn);
                gridViewLinesSoSelection.Columns.Add(colDescription);
                gridViewLinesSoSelection.Columns.Add(colIdItemGroup);
                gridViewLinesSoSelection.Columns.Add(colQuantity);
                gridViewLinesSoSelection.Columns.Add(colDeliveredQuantity);
                gridViewLinesSoSelection.Columns.Add(colPendingQuantity);
                gridViewLinesSoSelection.Columns.Add(colDummyQuantity);
                gridViewLinesSoSelection.Columns.Add(colUnit);
                gridViewLinesSoSelection.Columns.Add(colUnitPrice);
                gridViewLinesSoSelection.Columns.Add(colTotalAmount);
                gridViewLinesSoSelection.Columns.Add(colIdIdSupplyStatus);

                //Events
                gridViewLinesSoSelection.SelectionChanged += GridViewLinesSoSelection_SelectionChanged;
                gridViewLinesSoSelection.CellValueChanged += GridViewLinesSoSelection_CellValueChanged;
                gridViewLinesSoSelection.CustomSummaryCalculate += GridViewLinesSoSelection_CustomSummaryCalculate;
                gridViewLinesSoSelection.ValidatingEditor += GridViewLinesSoSelection_ValidatingEditor;
                gridViewLinesSoSelection.ShowingEditor += GridViewLinesSoSelection_ShowingEditor;
                gridViewLinesSoSelection.CustomUnboundColumnData += GridViewLinesSoSelection_CustomUnboundColumnData;
                gridViewLinesSoSelection.RowCellStyle += GridViewLinesSoSelection_RowCellStyle;
            }
            catch
            {
                throw;
            }
        }

        private void SetUpGrdLinesDeliveredGoods()
        {
            try
            {
                //Activar que se alternen los colores de las filas del grid
                gridViewLinesDeliveredGoods.OptionsView.EnableAppearanceOddRow = true;
                gridViewLinesDeliveredGoods.OptionsView.EnableAppearanceEvenRow = true;

                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                gridViewLinesDeliveredGoods.OptionsView.ColumnAutoWidth = false;
                gridViewLinesDeliveredGoods.HorzScrollVisibility = ScrollVisibility.Auto;

                //Hacer todo el grid no editable
                gridViewLinesDeliveredGoods.OptionsBehavior.Editable = false;

                //Hide grouping panel
                gridViewLinesDeliveredGoods.OptionsView.ShowGroupPanel = false;

                //Column Definition
                GridColumn colIdDocRelated = new GridColumn() { Caption = "Sales Order", Visible = true, FieldName = nameof(DocLine.IdDocRelated), Width = 200 };
                GridColumn colIdItemBcn = new GridColumn() { Caption = "Item Code", Visible = true, FieldName = nameof(DocLine.IdItemBcn), Width = 200 };
                GridColumn colDescription = new GridColumn() { Caption = "Description", Visible = true, FieldName = nameof(DocLine.ItemDesc), Width = 350 };
                GridColumn colIdItemGroup = new GridColumn() { Caption = "Type", Visible = true, FieldName = nameof(DocLine.IdItemGroup), Width = 100 };
                GridColumn colQuantityOriginal = new GridColumn() { Caption = "Order Quantity", Visible = true, FieldName = nameof(DocLine.QuantityOriginal), Width = 110 };
                GridColumn colQuantity = new GridColumn() { Caption = "Quantity", Visible = true, FieldName = nameof(DocLine.Quantity), Width = 85 };
                GridColumn colUnit = new GridColumn() { Caption = "Unit", Visible = true, FieldName = nameof(DocLine.ItemUnit), Width = 85 };
                GridColumn colUnitPrice = new GridColumn() { Caption = "Unit Price", Visible = true, FieldName = nameof(DocLine.UnitPrice), Width = 85 };
                GridColumn colTotalAmount = new GridColumn() { Caption = "Total Amount", Visible = true, FieldName = nameof(DocLine.TotalAmount), Width = 120 };
                GridColumn colRemarks = new GridColumn() { Caption = "Notes", Visible = true, FieldName = nameof(DocLine.Remarks), Width = 300 };

                //Display Format
                colUnitPrice.DisplayFormat.FormatType = FormatType.Numeric;
                colUnitPrice.DisplayFormat.FormatString = "n4";

                colTotalAmount.DisplayFormat.FormatType = FormatType.Numeric;
                colTotalAmount.DisplayFormat.FormatString = "n4";

                colQuantity.DisplayFormat.FormatType = FormatType.Numeric;
                colQuantity.DisplayFormat.FormatString = "n3";

                colQuantityOriginal.DisplayFormat.FormatType = FormatType.Numeric;
                colQuantityOriginal.DisplayFormat.FormatString = "n3";

                //Summaries
                gridViewLinesDeliveredGoods.OptionsView.ShowFooter = true;

                colTotalAmount.Summary.Add(SummaryItemType.Sum, nameof(DocLine.TotalAmount), "{0:n4}");

                colQuantityOriginal.Summary.AddRange(new GridSummaryItem[] {
                    new GridColumnSummaryItem(SummaryItemType.Custom, nameof(DocLine.QuantityOriginal), "{0:n3} KG", eGridLinesDeliveredGoodsSummaries.totalQuantityOrderedMt),
                    new GridColumnSummaryItem(SummaryItemType.Custom, nameof(DocLine.QuantityOriginal), "{0} PC", eGridLinesDeliveredGoodsSummaries.totalQuantityOrderedHw) });

                colQuantity.Summary.AddRange(new GridSummaryItem[] {
                    new GridColumnSummaryItem(SummaryItemType.Custom, nameof(DocLine.Quantity), "{0:n3} KG", eGridLinesDeliveredGoodsSummaries.totalQuantityMt),
                    new GridColumnSummaryItem(SummaryItemType.Custom, nameof(DocLine.Quantity), "{0} PC", eGridLinesDeliveredGoodsSummaries.totalQuantityHw) });

                
                //Add columns to grid root view
                gridViewLinesDeliveredGoods.Columns.Add(colIdDocRelated);
                gridViewLinesDeliveredGoods.Columns.Add(colIdItemBcn);
                gridViewLinesDeliveredGoods.Columns.Add(colDescription);
                gridViewLinesDeliveredGoods.Columns.Add(colIdItemGroup);
                gridViewLinesDeliveredGoods.Columns.Add(colQuantityOriginal);
                gridViewLinesDeliveredGoods.Columns.Add(colQuantity);
                gridViewLinesDeliveredGoods.Columns.Add(colUnit);
                gridViewLinesDeliveredGoods.Columns.Add(colUnitPrice);
                gridViewLinesDeliveredGoods.Columns.Add(colTotalAmount);
                gridViewLinesDeliveredGoods.Columns.Add(colRemarks);

                //Events
                gridViewLinesDeliveredGoods.CustomSummaryCalculate += GridViewLinesDeliveredGoods_CustomSummaryCalculate;
                gridViewLinesDeliveredGoods.FocusedRowChanged += GridViewLinesDeliveredGoods_FocusedRowChanged; //Test Item Batch

            }
            catch
            {
                throw;
            }
        }

        //Test Item Batch INI
        private void SetupGrdItemsBatch()
        {
            try
            {
                //Activar que se alternen los colores de las filas del grid
                gridViewItemsBatch.OptionsView.EnableAppearanceEvenRow = true;
                gridViewItemsBatch.OptionsView.EnableAppearanceOddRow = true;

                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                gridViewItemsBatch.OptionsView.ColumnAutoWidth = false;
                gridViewItemsBatch.HorzScrollVisibility = ScrollVisibility.Auto;

                //Hacer todo el grid no editable
                gridViewItemsBatch.OptionsBehavior.Editable = false;

                //Hide grouping panel
                gridViewItemsBatch.OptionsView.ShowGroupPanel = false;

                //Column Definition
                GridColumn colIdItemBcn = new GridColumn() { Caption = "Item Code", Visible = true, FieldName = nameof(PackingListItemBatch.IdItemBcn), Width = 200 };
                GridColumn colBatch = new GridColumn() { Caption = "Batch", Visible = true, FieldName = nameof(PackingListItemBatch.Batch), Width = 200 };
                GridColumn colQuantity = new GridColumn() { Caption = "Quantity", Visible = true, FieldName = nameof(PackingListItemBatch.Quantity), Width = 85 };

                //Display Format
                colQuantity.DisplayFormat.FormatType = FormatType.Numeric;
                colQuantity.DisplayFormat.FormatString = "n3";

                //Edit Repositories
                RepositoryItemTextEdit ritxt3Dec = new RepositoryItemTextEdit();
                ritxt3Dec.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                ritxt3Dec.Mask.EditMask = "F3";
                ritxt3Dec.AllowNullInput = DefaultBoolean.True;

                colQuantity.ColumnEdit = ritxt3Dec;

                //Adding columns
                gridViewItemsBatch.Columns.Add(colIdItemBcn);
                gridViewItemsBatch.Columns.Add(colBatch);
                gridViewItemsBatch.Columns.Add(colQuantity);


                //Disable sorting
                foreach (GridColumn column in gridViewItemsBatch.Columns)
                    column.OptionsColumn.AllowSort = DefaultBoolean.False;

                //Events
                xgrdItemsBatch.ProcessGridKey += XgrdItemsBatch_ProcessGridKey;
                gridViewItemsBatch.CellValueChanged += GridViewItemsBatch_CellValueChanged;
            }
            catch
            {
                throw;
            }
        }

        private void SetupGrdItemsBox()
        {
            try
            {
                //Activar que se alternen los colores de las filas del grid
                gridViewItemsBox.OptionsView.EnableAppearanceEvenRow = true;
                gridViewItemsBox.OptionsView.EnableAppearanceOddRow = true;

                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                gridViewItemsBox.OptionsView.ColumnAutoWidth = false;
                gridViewItemsBox.HorzScrollVisibility = ScrollVisibility.Auto;

                //Hacer todo el grid no editable
                gridViewItemsBox.OptionsBehavior.Editable = false;

                //Hide grouping panel
                gridViewItemsBox.OptionsView.ShowGroupPanel = false;

                //Column Definition
                GridColumn colIdItemBcn = new GridColumn() { Caption = "Item Code", Visible = true, FieldName = nameof(PackingListItemBox.IdItemBcn), Width = 200 };
                GridColumn colBoxNumber = new GridColumn() { Caption = "Box", Visible = true, FieldName = nameof(PackingListItemBox.BoxNumber), Width = 50 };
                GridColumn colPcQuantity = new GridColumn() { Caption = "Qty (PCS)", Visible = true, FieldName = nameof(PackingListItemBox.PcQuantity), Width = 70 };
                GridColumn colNetWeight = new GridColumn() { Caption = "Net Weight (KG)", Visible = true, FieldName = nameof(PackingListItemBox.NetWeight), Width = 110 };
                GridColumn colGrossWeight = new GridColumn() { Caption = "Gross Weight (KG)", Visible = true, FieldName = nameof(PackingListItemBox.GrossWeight), Width = 120 };

                //Display Format
                colPcQuantity.DisplayFormat.FormatType = FormatType.Numeric;
                colPcQuantity.DisplayFormat.FormatString = "n0";

                colNetWeight.DisplayFormat.FormatType = FormatType.Numeric;
                colNetWeight.DisplayFormat.FormatString = "n2";

                colGrossWeight.DisplayFormat.FormatType = FormatType.Numeric;
                colGrossWeight.DisplayFormat.FormatString = "n2";

                //Edit Repositories
                RepositoryItemTextEdit ritxtInt = new RepositoryItemTextEdit();
                ritxtInt.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                ritxtInt.Mask.EditMask = "N";
                ritxtInt.AllowNullInput = DefaultBoolean.True;
                colPcQuantity.ColumnEdit = ritxtInt;

                RepositoryItemTextEdit ritxt2Dec = new RepositoryItemTextEdit();
                ritxt2Dec.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                ritxt2Dec.Mask.EditMask = "F2";
                ritxt2Dec.AllowNullInput = DefaultBoolean.True;

                colNetWeight.ColumnEdit = ritxt2Dec;
                colGrossWeight.ColumnEdit = ritxt2Dec;

                //Es necesario crear una lista tonta donde hayan varias cajas, porque aunque se filtra después en el shownEditor, al salir tiene que mostrar el valor 
                //del datasource original. Meto 50 cajas, no creo que se llegue a tanto
                List<DocBox> tmpBoxesList = new List<DocBox>();
                for (int i = 1; i <= 50; i++)
                {
                    tmpBoxesList.Add(new DocBox() { BoxNumber = i });
                }

                RepositoryItemLookUpEdit riPackingBoxes = new RepositoryItemLookUpEdit()
                {
                    DataSource = tmpBoxesList,
                    //DataSource = _packingListBoxes,
                    ValueMember = nameof(DocBox.BoxNumber),
                    DisplayMember = nameof(DocBox.BoxNumber),
                    NullText = "Select Box",
                };
                riPackingBoxes.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo(nameof(DocBox.BoxNumber), "Box Number"));
                colBoxNumber.ColumnEdit = riPackingBoxes;

                //Adding columns
                gridViewItemsBox.Columns.Add(colIdItemBcn);
                gridViewItemsBox.Columns.Add(colBoxNumber);
                gridViewItemsBox.Columns.Add(colPcQuantity);
                gridViewItemsBox.Columns.Add(colNetWeight);
                gridViewItemsBox.Columns.Add(colGrossWeight);

                //Disable sorting
                foreach (GridColumn column in gridViewItemsBatch.Columns)
                    column.OptionsColumn.AllowSort = DefaultBoolean.False;

                //Events
                xgrdItemsBox.ProcessGridKey += XgrdItemsBox_ProcessGridKey;
                gridViewItemsBox.CellValueChanged += GridViewItemsBox_CellValueChanged;
                gridViewItemsBox.ShownEditor += GridViewItemsBox_ShownEditor;

            }
            catch
            {
                throw;
            }
        }

        private void SetupGrdPackingBoxes()
        {
            try
            {
                //Activar que se alternen los colores de las filas del grid
                gridViewPackingBoxes.OptionsView.EnableAppearanceEvenRow = true;
                gridViewPackingBoxes.OptionsView.EnableAppearanceOddRow = true;

                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                gridViewPackingBoxes.OptionsView.ColumnAutoWidth = false;
                gridViewPackingBoxes.HorzScrollVisibility = ScrollVisibility.Auto;

                //Hacer todo el grid no editable
                gridViewPackingBoxes.OptionsBehavior.Editable = false;

                //Hide grouping panel
                gridViewPackingBoxes.OptionsView.ShowGroupPanel = false;

                //Column Definition
                GridColumn colBoxNumber = new GridColumn() { Caption = "Box Number", Visible = true, FieldName = nameof(DocBox.BoxNumber), Width = 100 };
                GridColumn colIdBox = new GridColumn() { Caption = "Box", Visible = true, FieldName = nameof(DocBox.IdBox), Width = 250 };
                GridColumn colNetWeight = new GridColumn() { Caption = "Net Weight (KG)", Visible = true, FieldName = nameof(DocBox.NetWeight), Width = 120 };
                GridColumn colGrossWeight = new GridColumn() { Caption = "Gross Weight (KG)", Visible = true, FieldName = nameof(DocBox.GrossWeight), Width = 120 };

                //Display Format
                colNetWeight.DisplayFormat.FormatType = FormatType.Numeric;
                colNetWeight.DisplayFormat.FormatString = "n2";

                colGrossWeight.DisplayFormat.FormatType = FormatType.Numeric;
                colGrossWeight.DisplayFormat.FormatString = "n2";

                //Edit Repositories
                RepositoryItemTextEdit ritxt2Dec = new RepositoryItemTextEdit();
                ritxt2Dec.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                ritxt2Dec.Mask.EditMask = "F2";
                ritxt2Dec.AllowNullInput = DefaultBoolean.True;

                colNetWeight.ColumnEdit = ritxt2Dec;
                colGrossWeight.ColumnEdit = ritxt2Dec;

                RepositoryItemSearchLookUpEdit riBoxes = new RepositoryItemSearchLookUpEdit()
                {
                    DataSource = _boxList,
                    ValueMember = nameof(Box.IdBox),
                    DisplayMember = nameof(Box.Name),
                    NullText = "Select Item",
                    ShowClearButton = false,
                };
                riBoxes.View.Columns.AddField(nameof(Box.Name)).Visible = true;

                colIdBox.ColumnEdit = riBoxes;

                //Adding columns
                gridViewPackingBoxes.Columns.Add(colBoxNumber);
                gridViewPackingBoxes.Columns.Add(colIdBox);
                gridViewPackingBoxes.Columns.Add(colNetWeight);
                gridViewPackingBoxes.Columns.Add(colGrossWeight);

                //Disable sorting
                foreach (GridColumn column in gridViewPackingBoxes.Columns)
                    column.OptionsColumn.AllowSort = DefaultBoolean.False;

                //Events
                xgrdPackingBoxes.ProcessGridKey += XgrdPackingBoxes_ProcessGridKey;
            }
            catch
            {
                throw;
            }
        }
        //Test Item Batch END

        private void SetupPanelControl()
        {
            try
            {
                var x = pcFilter.LookAndFeel;
                x.UseDefaultLookAndFeel = false;
                pcFilter.BackColor = AppStyles.BackColorAlternative;

            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Aux

        private void SetVisiblePropertyByState()
        {
            try
            {
                switch (CurrentState)
                {

                    case ActionsStates.New:
                        sbFinishPK.Visible = false;
                        sbSearch.Visible = false;
                        lbltxtStatus.Visible = false;
                        break;

                    case ActionsStates.Edit:
                        sbFinishPK.Visible = true;
                        sbSearch.Visible = false;
                        lbltxtStatus.Visible = true;
                        break;

                    default:
                        sbFinishPK.Visible = false;
                        sbSearch.Visible = true;
                        lbltxtStatus.Visible = false;
                        break;
                }
            }
            catch
            {
                throw;
            }
        }

        private void ResetForm(bool resetPkNumber = true, bool resetCustomer = true)
        {
            try
            {
                /********* Head *********/
                if (resetPkNumber) txtPKNumber.EditValue = null;
                lbltxtStatus.Text = string.Empty;
                dateEditPKDocDate.EditValue = null;
                dateEditPKDelivery.EditValue = null;
                lblPKDocDateWeek.Text = string.Empty;
                lblPKDeliveryWeek.Text = string.Empty;
                if (resetCustomer) slueCustomer.EditValue = null;
                slueDeliveryTerms.EditValue = null;
                slueCurrency.EditValue = null;
                txtManualReference.EditValue = null;
                memoEditRemarks.EditValue = null;

                /********* Terms Tab *********/
                lblTxtCompany.Text = string.Empty;
                lblTxtAddress.Text = string.Empty;
                lblTxtContact.Text = string.Empty;
                lblTxtShipTo.Text = string.Empty;
                lblTxtInvoiceTo.Text = string.Empty;
                sluePaymentTerm.EditValue = null;
            }
            catch
            {
                throw;
            }
        }

        private void SetCustomerInfo()
        {
            try
            {
                var customer = _customersList.Where(a => a.IdCustomer.Equals(slueCustomer.EditValue.ToString())).FirstOrDefault();

                if (customer != null)
                {
                    lblTxtCompany.Text = customer.CustomerName;
                    lblTxtAddress.Text = customer.ShippingAddress;
                    lblTxtContact.Text = $"{customer.ContactName} ({customer.ContactPhone})";
                    sluePaymentTerm.EditValue = customer.IdPaymentTerms;
                    slueCurrency.EditValue = customer.IdDefaultCurrency;
                }
            }
            catch
            {
                throw;
            }
        }

        private void CopyToDeliveredGood(DocLine line)
        {
            try
            {
                if (line.Quantity == 0)
                    return;

                //Buscamos si ya existe
                var exist = _docLinesDeliveredGoodsList.Where(a => a.IdItemBcn.Equals(line.IdItemBcn) && a.IdDocRelated.Equals(line.IdDoc)).FirstOrDefault();

                if (exist == null)
                {
                    DocLine tmp = line.DeepCopyByExpressionTree();
                    tmp.IdDoc = txtPKNumber.Text;
                    tmp.IdDocRelated = line.IdDoc;
                    tmp.DeliveredQuantity = 0;
                    tmp.QuantityOriginal = line.Quantity;
                    tmp.Quantity = line.DummyQuantity;
                    tmp.Remarks = null;
                    _docLinesDeliveredGoodsList.Add(tmp);
                }
                else
                {
                    //Actualizamos la cantidad que es el única campo que se puede editar
                    exist.Quantity = line.DummyQuantity;
                    gridViewLinesDeliveredGoods.RefreshData();
                }
            }
            catch
            {
                throw;
            }
        }

        private void DeleteDeliveredGood(DocLine line)
        {
            try
            {
                var deliveredGoodsLine = _docLinesDeliveredGoodsList.Where(a => a.IdItemBcn.Equals(line.IdItemBcn) && a.IdDocRelated.Equals(line.IdDoc)).FirstOrDefault();
                if (deliveredGoodsLine != null)
                    _docLinesDeliveredGoodsList.Remove(deliveredGoodsLine);
            }
            catch
            {
                throw;
            }
        }

        private void MarkSoSelectionFromDeliveredGoods()
        {
            try
            {
                _isMarkingSoSelFromDeliveredGoods = true;

                gridViewLinesSoSelection.BeginSelection();

                for (int i = 0; i < gridViewLinesSoSelection.DataRowCount; i++)
                {
                    DocLine rowSoSelection = gridViewLinesSoSelection.GetRow(i) as DocLine;

                    var rowDeliveredGoods = _docLinesDeliveredGoodsList?.Where(a => a.IdItemBcn.Equals(rowSoSelection.IdItemBcn) && a.IdDocRelated.Equals(rowSoSelection.IdDoc)).FirstOrDefault();

                    if (rowDeliveredGoods != null)
                    {
                        gridViewLinesSoSelection.SelectRow(i);
                        rowSoSelection.DummyQuantity = rowDeliveredGoods.Quantity; //Por si se ha modificado la cantidad, está guardada en la línea de delivered good
                        gridViewLinesSoSelection.RefreshRow(i);
                    }

                }

                gridViewLinesSoSelection.EndSelection();

            }
            catch
            {
                throw;
            }
            finally
            {
                _isMarkingSoSelFromDeliveredGoods = false;
            }

        }

        //Test Item Batch INI
        private void CopyToItemBatchList(PackingListItemBatch itemBatch)
        {
            try
            {
                var reg = _itemsBatchList
                    .Where(a => a.IdDocRelated.Equals(itemBatch.IdDocRelated) && a.IdItemBcn.Equals(itemBatch.IdItemBcn) && a.IdItemGroup.Equals(itemBatch.IdItemGroup) && a.Batch.Equals(itemBatch.Batch))
                    .FirstOrDefault();

                if (reg == null)
                    _itemsBatchList.Add(itemBatch);
                else
                    reg.Batch = itemBatch.Batch;
            }
            catch
            {
                throw;
            }
        }

        private void DeleteFromItemsBatchList(string idDocSO, string idItem, string idItemGroup)
        {
            try
            {
                var tmpList = _itemsBatchList
                    .Where(a => a.IdDocRelated.Equals(idDocSO) && a.IdItemBcn.Equals(idItem) && a.IdItemGroup.Equals(idItemGroup))
                    .ToList();

                foreach (var itemBatch in tmpList)
                    _itemsBatchList.Remove(itemBatch);

            }
            catch
            {
                throw;
            }
        }

        private void CopyToItemBoxList(PackingListItemBox itemBox)
        {
            try
            {
                var reg = _itemsBoxList
                    .Where(a => a.IdDocRelated.Equals(itemBox.IdDocRelated) &&
                           a.IdItemBcn.Equals(itemBox.IdItemBcn) &&
                           a.IdItemGroup.Equals(itemBox.IdItemGroup) &&
                           a.BoxNumber.Equals(itemBox.BoxNumber))
                    .FirstOrDefault();

                if (reg == null)
                    _itemsBoxList.Add(itemBox);
                else
                    reg.BoxNumber = itemBox.BoxNumber;
            }
            catch
            {
                throw;
            }

        }

        private void DeleteFromItemsBoxList(string idDocSO, string idItem, string idItemGroup)
        {
            try
            {
                var tmpList = _itemsBoxList
                    .Where(a => a.IdDocRelated.Equals(idDocSO) && a.IdItemBcn.Equals(idItem) && a.IdItemGroup.Equals(idItemGroup))
                    .ToList();

                foreach (var itemBox in tmpList)
                    _itemsBoxList.Remove(itemBox);

            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Actualizar el número de caja cuando se borra una
        /// </summary>
        private void UpdateBoxNumber()
        {
            try
            {
                int boxNumber = 1;
                foreach (var box in _docBoxList)
                {
                    box.BoxNumber = boxNumber++;
                }
            }
            catch
            {
                throw;
            }
        }

        private void UpdateBoxNumberItemsBox(int boxNumber)
        {
            try
            {
                foreach (var line in _auxItemsBoxList)
                {
                    if (line.BoxNumber >= boxNumber)
                        line.BoxNumber = 0;
                }

                foreach (var line in _itemsBoxList)
                {
                    if (line.BoxNumber >= boxNumber)
                        line.BoxNumber = 0;
                }

                gridViewItemsBox.RefreshData();
            }
            catch
            {
                throw;
            }
        }
        //Test Item Batch END

        #region Validates

        private bool ValidatePK()
        {
            try
            {
                //TODO! Revisar las validaciones

                if (slueDeliveryTerms.EditValue == null)
                {
                    MessageBox.Show("Field Required: Term of Delivery", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    slueDeliveryTerms.Focus();
                    return false;
                }

                if (slueCurrency.EditValue == null)
                {
                    MessageBox.Show("Field Required: Currency", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    slueCurrency.Focus();
                    return false;
                }

                if (sluePaymentTerm.EditValue == null)
                {
                    MessageBox.Show("Field Required: Term of Payment", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    sluePaymentTerm.Focus();
                    return false;
                }

                if (dateEditPKDelivery.EditValue == null)
                {
                    MessageBox.Show("Field Required: Delivery Date", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dateEditPKDelivery.Focus();
                    return false;
                }

                if (_docLinesDeliveredGoodsList.Count == 0)
                {
                    MessageBox.Show("Packing without lines", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                //Test Item Batch INI
                if (ValidatePackingBoxes() == false)
                    return false;

                if (ValidatePackingItemsBatch() == false)
                    return false;

                if (ValidatePackingItemsBox() == false)
                    return false;
                //Test Item Batch END

                return true;
            }
            catch
            {
                throw;
            }
        }

        //Test Item Batch INI
        private bool ValidatePackingBoxes()
        {
            try
            {
                foreach (var box in _docBoxList)
                {
                    if (string.IsNullOrEmpty(box.IdBox) == false)
                    {
                        if (box.NetWeight == 0 || box.GrossWeight == 0)
                        {
                            MessageBox.Show("You must indicate Net Weight and Gross Weight for all boxes", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            xgrdPackingBoxes.Focus();
                            return false;
                        }

                        if (box.NetWeight >= box.GrossWeight)
                        {
                            MessageBox.Show($"Net Weight must be less than Gross Weight for box {box.BoxNumber.ToString()}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            xgrdPackingBoxes.Focus();
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

        private bool ValidatePackingItemsBatch()
        {
            try
            {
                //Validamos que siempre 
                foreach (var itemBatch in _itemsBatchList)
                {
                    if (string.IsNullOrEmpty(itemBatch.Batch))
                    {
                        MessageBox.Show($"You must indicate a bath for {itemBatch.IdItemBcn} ({itemBatch.IdDocRelated})", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        xgrdItemsBatch.Focus();
                        return false;
                    }
                }

                foreach (var line in _docLinesDeliveredGoodsList)
                {
                    decimal qtyItemBatch = _itemsBatchList
                        .Where(a => a.IdDocRelated.Equals(line.IdDocRelated) && a.IdItemBcn.Equals(line.IdItemBcn) && a.IdItemGroup.Equals(line.IdItemGroup))
                        .Sum(b => b.Quantity);
                    if (line.Quantity != qtyItemBatch)
                    {
                        MessageBox.Show($"{line.IdItemBcn} ({line.IdDocRelated}): Batch quantity error", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        xgrdLinesDeliveredGoods.Focus();
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

        private bool ValidatePackingItemsBox()
        {
            try
            {
                foreach (var itemBox in _itemsBoxList)
                {

                    if (itemBox.BoxNumber == 0 && itemBox.PcQuantity == 0) //Es una línea "en blanco", la ignoramos
                        continue;

                    if (itemBox.BoxNumber == 0)
                    {
                        MessageBox.Show($"You must indicate Box Number for {itemBox.IdItemBcn} ({itemBox.IdDocRelated})", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        xgrdItemsBox.Focus();
                        return false;
                    }

                    if (itemBox.PcQuantity == 0)
                    {
                        MessageBox.Show($"You must indicate pcs quantity for {itemBox.IdItemBcn} ({itemBox.IdDocRelated})", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        xgrdItemsBox.Focus();
                        return false;
                    }

                    if (itemBox.NetWeight == 0)
                    {
                        MessageBox.Show($"You must indicate Net Weight for {itemBox.IdItemBcn} ({itemBox.IdDocRelated})", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        xgrdItemsBox.Focus();
                        return false;
                    }

                    if (itemBox.GrossWeight == 0)
                    {
                        MessageBox.Show($"You must indicate Gross Weight for {itemBox.IdItemBcn} ({itemBox.IdDocRelated})", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        xgrdItemsBox.Focus();
                        return false;
                    }

                    if (itemBox.NetWeight >= itemBox.GrossWeight)
                    {
                        MessageBox.Show($"Net Weight must be less than Gross Weightfor {itemBox.IdItemBcn} ({itemBox.IdDocRelated})", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        xgrdItemsBox.Focus();
                        return false;
                    }
                }


                foreach (var line in _docLinesDeliveredGoodsList)
                {
                    var itemBox = _itemsBoxList
                        .Where(a => a.IdDocRelated.Equals(line.IdDocRelated) && a.IdItemBcn.Equals(line.IdItemBcn) && a.IdItemGroup.Equals(line.IdItemGroup))
                        .FirstOrDefault();

                    if (itemBox == null)
                    {
                        MessageBox.Show($"You must indicate Box Number for {line.IdItemBcn} ({line.IdDocRelated})", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        xgrdItemsBox.Focus();
                        return false;
                    }
                }

                //no podemos validar las cantidades con el acetato tenemos las cantidades en kg, en cambio hay indicamos el número de piezas que se envían por caja

                return true;
            }
            catch
            {
                throw;
            }
        }

        //Test Item Batch END

        #endregion

        private void SetGridsEnabled()
        {
            try
            {
                //Ponemos los grid como editables y bloqueamos las columnas que no se puede editar
                gridViewLinesSoSelection.OptionsBehavior.Editable = true;
                gridViewLinesDeliveredGoods.OptionsBehavior.Editable = true;
                gridViewItemsBatch.OptionsBehavior.Editable = true; //Test Item Batch INI
                gridViewItemsBox.OptionsBehavior.Editable = true; //Test Item Batch INI
                gridViewPackingBoxes.OptionsBehavior.Editable = true; //Test Item Batch INI

                foreach (GridColumn col in gridViewLinesSoSelection.Columns)
                {
                    if (col.FieldName != nameof(DocLine.DummyQuantity))
                        col.OptionsColumn.AllowEdit = false;
                }

                foreach (GridColumn col in gridViewLinesDeliveredGoods.Columns)
                {
                    //if (col.FieldName != nameof(DocLine.Remarks))
                    if (col.FieldName != nameof(DocLine.Remarks)) //Test Item Batch
                        col.OptionsColumn.AllowEdit = false;
                }

                //Test Item Batch INI
                foreach (GridColumn col in gridViewItemsBatch.Columns)
                {
                    if (col.FieldName == nameof(PackingListItemBatch.IdItemBcn))
                        col.OptionsColumn.AllowEdit = false;
                }

                foreach (GridColumn col in gridViewItemsBox.Columns)
                {
                    if (col.FieldName == nameof(PackingListItemBox.IdItemBcn))
                        col.OptionsColumn.AllowEdit = false;
                }

                foreach (GridColumn col in gridViewPackingBoxes.Columns)
                {
                    if (col.FieldName == nameof(DocBox.BoxNumber))
                        col.OptionsColumn.AllowEdit = false;
                }
                //Test Item Batch END

                //activamos el seleccionar todo de la cabecera
                gridViewLinesSoSelection.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DefaultBoolean.True; 
            }
            catch
            {
                throw;
            }
        }

        private void SetGridNotEditable()
        {
            try
            {
                //not allow grid editing
                gridViewSoSelection.OptionsBehavior.Editable = false;
                gridViewLinesSoSelection.OptionsBehavior.Editable = false;
                gridViewLinesDeliveredGoods.OptionsBehavior.Editable = false;
                gridViewItemsBatch.OptionsBehavior.Editable = false; //Test Item Batch
                gridViewPackingBoxes.OptionsBehavior.Editable = false; //Test Item Batch
                //desactivamos el seleccionar todo de la cabecera
                gridViewLinesSoSelection.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DefaultBoolean.False;
            }
            catch
            {
                throw;
            }
        }

        private void CancelEdit()
        {
            try
            {
                txtPKNumber.ReadOnly = false;
                slueCustomer.ReadOnly = false;

                string idPk = _docHeadPK?.IdDoc;
                ResetPK();
                ResetForm();

                SetObjectsReadOnly();

                if (idPk != null)
                {
                    txtPKNumber.Text = idPk;
                    SearchPK();
                }
                SetGridNotEditable();
                RestoreInitState();
                SetVisiblePropertyByState();
            }
            catch
            {
                throw;
            }

        }

        private void ShowMessageDocsGenerated()
        {
            try
            {
                var docs = GlobalSetting.SupplyDocsService.GetDocsByRelated(_docHeadPK.IdDoc);

                if (docs.Count > 0)
                {
                    string msg = "The Following documents have been created:" + Environment.NewLine;

                    foreach (var doc in docs)
                    {
                        msg += $"{doc.IdDoc} ({doc.SupplyDocType.Description}){Environment.NewLine}";
                        if (doc.IdSupplyDocType == Constants.SUPPLY_DOCTYPE_DN)
                        {
                            var docs2 = GlobalSetting.SupplyDocsService.GetDocsByRelated(doc.IdDoc);
                            foreach (var doc2 in docs2)
                            {
                                msg += $"{doc2.IdDoc} ({doc2.SupplyDocType.Description}){Environment.NewLine}";
                            }
                        }
                    }

                    XtraMessageBox.Show(msg);
                }

            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Configure Ribbon Actions

        private void ConfigureActionsStackViewCreating()
        {
            try
            {
                _isCreatingPacking = true;

                txtPKNumber.ReadOnly = true;
                slueCustomer.ReadOnly = true;
                dateEditPKDocDate.EditValue = DateTime.Now;

                //clean necessary objects
                Reset4NewPk();
                //Enable objects
                SetObjectsEnableToCreate();

                //generate packing list number
                CreatePkNumber();

                _docLinesDeliveredGoodsList = new BindingList<DocLine>();
                xgrdLinesDeliveredGoods.DataSource = null;
                xgrdLinesDeliveredGoods.DataSource = _docLinesDeliveredGoodsList;
                
                //Test Item Batch INI
                _itemsBatchList = new List<PackingListItemBatch>(); 
                _auxItemsBatchList = new BindingList<PackingListItemBatch>(); 
                xgrdItemsBatch.DataSource = null; 
                xgrdItemsBatch.DataSource = _auxItemsBatchList;

                _itemsBoxList = new List<PackingListItemBox>();
                _auxItemsBoxList = new BindingList<PackingListItemBox>();
                xgrdItemsBox.DataSource = null;
                xgrdItemsBox.DataSource = _auxItemsBoxList;

                _docBoxList = new BindingList<DocBox>();
                _docBoxList.Add(new DocBox() { BoxNumber = 1, IdDoc = txtPKNumber.Text });
                xgrdPackingBoxes.DataSource = null;
                xgrdPackingBoxes.DataSource = _docBoxList;
                //Test Item Batch END


                //habilitar los grids para edición
                SetGridsEnabled();

                SetVisiblePropertyByState();
            }
            catch
            {
                throw;
            }
            finally
            {
                _isCreatingPacking = false;
            }
        }

        private void ConfigureActionsStackViewEditing()
        {
            try
            {
                txtPKNumber.ReadOnly = true;
                slueCustomer.ReadOnly = true;

                SetObjectsEnableToEdit();

                //cargamos las SO abiertas de ese customer aparte de las que incluye el packing a editar
                var soDocsCustomer = GlobalSetting.SupplyDocsService.GetDocs(
                    idSupplier: null,
                    idCustomer: slueCustomer.EditValue as string,
                    docDate: new DateTime(1, 1, 1),  //filtrar sin fecha
                    IdSupplyDocType: Constants.SUPPLY_DOCTYPE_SO,
                    idSupplyStatus: Constants.SUPPLY_STATUS_OPEN);

                if (soDocsCustomer.Count != 0)
                {
                    //Con el union hace el merge y descarta duplicados
                    _docSoSelectionList = new BindingList<DocHead>(_docSoSelectionList.Union(soDocsCustomer).ToList());
                }
                xgrdSoSelection.DataSource = null;
                xgrdSoSelection.DataSource = _docSoSelectionList;

                xgrdLinesSoSelection.DataSource = null;


                SetGridsEnabled(); //habilitar los grids para edición
                SetVisiblePropertyByState();

            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region CRUD

        private void SearchCustomersSOs()
        {
            try
            {
                ResetCustomerSOs();

                string customer = slueCustomer.EditValue as string;

                var soDocsCustomer = GlobalSetting.SupplyDocsService.GetDocs(
                    idSupplier: null,
                    idCustomer: customer,
                    docDate: new DateTime(1, 1, 1),  //filtrar sin fecha
                    IdSupplyDocType: Constants.SUPPLY_DOCTYPE_SO,
                    idSupplyStatus: Constants.SUPPLY_STATUS_OPEN);

                if (soDocsCustomer.Count == 0)
                {
                    XtraMessageBox.Show("No Customer's Sales Orders open found", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _docSoSelectionList = new BindingList<DocHead>(soDocsCustomer);

                    xgrdSoSelection.DataSource = null;
                    xgrdSoSelection.DataSource = _docSoSelectionList;
                }
            }
            catch
            {
                throw;
            }
        }

        private bool ValidateIfExistOpenPK()
        {
            try
            {
                var exist = GlobalSetting.SupplyDocsService.GetDocs(
                    idSupplier: Constants.ETNIA_HK_COMPANY_CODE,
                    idCustomer: (string)slueCustomer.EditValue,
                    //docDate: dateEditPKDocDate.DateTime, 
                    docDate: new DateTime(1, 1, 1),
                    IdSupplyDocType: Constants.SUPPLY_DOCTYPE_PL,
                    idSupplyStatus: Constants.SUPPLY_STATUS_OPEN);

                if (exist.Count > 0)
                {
                    //XtraMessageBox.Show($"Packing List Open ({exist.Select(a => a.IdDoc).FirstOrDefault()}). You cannot craeted new one.");

                    var result = XtraMessageBox.Show($"Packing List Open ({exist.Select(a => a.IdDoc).FirstOrDefault()}). You cannot create new one. Load open Packing List?", "", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        txtPKNumber.Text = exist.Select(a => a.IdDoc).FirstOrDefault();
                        SearchPK();
                    }
                    return false;

                }
                else
                {
                    return true;
                }


            }
            catch
            {
                throw;
            }
        }

        private void CreatePkNumber()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                string pkNumber = GlobalSetting.SupplyDocsService.GetGenericDocHeadNumber(
                    idSupplyDocType: Constants.SUPPLY_DOCTYPE_PL,
                    idCustomer: (string)slueCustomer.EditValue, 
                    idSupplier: string.Empty, 
                    date: DateTime.Now);

                txtPKNumber.Text = pkNumber;
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

        private bool CreatePK()
        {
            try
            {
                List<DocLine> sortedLines = _docLinesDeliveredGoodsList.OrderBy(a => a.IdItemGroup).ThenBy(b => b.IdItemBcn).ToList();
                List<DocBox> boxes = _docBoxList.Where(a => a.IdBox != null).OrderBy(b => b.BoxNumber).ToList();

                DocHead packingList = new DocHead()
                {
                    IdDoc = txtPKNumber.Text,
                    IdSupplyDocType = Constants.SUPPLY_DOCTYPE_PL,
                    CreationDate = DateTime.Now,
                    DeliveryDate = dateEditPKDelivery.DateTime,
                    DocDate = dateEditPKDocDate.DateTime, 
                    IdSupplyStatus = Constants.SUPPLY_STATUS_OPEN,
                    IdSupplier = Constants.ETNIA_HK_COMPANY_CODE,
                    IdCustomer = slueCustomer.EditValue as string,
                    IdDeliveryTerm = slueDeliveryTerms.EditValue as string,
                    IdPaymentTerms = sluePaymentTerm.EditValue as string,
                    IdCurrency = slueCurrency.EditValue as string,
                    ManualReference = txtManualReference.EditValue as string,
                    Remarks = memoEditRemarks.EditValue as string,
                    Lines = sortedLines,
                    Boxes = boxes,
                    PackingListItemBatches = _itemsBatchList.Where(a => a.Quantity > 0 && string.IsNullOrEmpty(a.Batch) == false).ToList(),
                    PackingListItemBoxes = _itemsBoxList.Where(a => a.PcQuantity > 0 && a.BoxNumber > 0).ToList(),
                };

                DocHead createdDoc = GlobalSetting.SupplyDocsService.NewDoc(packingList);

                _docHeadPK = createdDoc;

                return true;
            }
            catch
            {
                throw;
            }
        }

        private bool UpdatePK(bool finishPK = false)
        {
            try
            {
                List<DocLine> sortedLines = _docLinesDeliveredGoodsList.OrderBy(a => a.IdItemGroup).ThenBy(b => b.IdItemBcn).ToList();
                List<DocBox> boxes = _docBoxList.Where(a => a.IdBox != null).OrderBy(b => b.BoxNumber).ToList();

                DocHead packingList = new DocHead()
                {

                    IdDoc = _docHeadPK.IdDoc,
                    IdDocRelated = _docHeadPK.IdDocRelated,
                    IdSupplyDocType = _docHeadPK.IdSupplyDocType,
                    CreationDate = _docHeadPK.CreationDate,
                    DeliveryDate = dateEditPKDelivery.DateTime,
                    DocDate = _docHeadPK.DocDate,
                    IdSupplyStatus = (finishPK ? Constants.SUPPLY_STATUS_CLOSE : Constants.SUPPLY_STATUS_OPEN),
                    IdSupplier = _docHeadPK.IdSupplier,
                    IdCustomer = _docHeadPK.IdCustomer,
                    IdDeliveryTerm = slueDeliveryTerms.EditValue as string,
                    IdPaymentTerms = sluePaymentTerm.EditValue as string,
                    IdCurrency = slueCurrency.EditValue as string,
                    ManualReference = txtManualReference.EditValue as string,
                    Remarks = memoEditRemarks.EditValue as string,
                    Lines = sortedLines,
                    Boxes = boxes,
                    PackingListItemBatches = _itemsBatchList.Where(a => a.Quantity > 0 && string.IsNullOrEmpty(a.Batch) == false).ToList(),
                    PackingListItemBoxes = _itemsBoxList.Where(a => a.PcQuantity > 0 && a.BoxNumber > 0).ToList(),
                };

                DocHead updatedDoc = GlobalSetting.SupplyDocsService.UpdateDoc(packingList, finishDoc: finishPK);

                _docHeadPK = updatedDoc;

                return true;
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
                txtPKNumber.ReadOnly = false;
                SetObjectsReadOnly();
                //Restore de ribbon to initial states
                RestoreInitState();
                //Clear grids
                xgrdSoSelection.DataSource = null;
                xgrdLinesSoSelection.DataSource = null;
                xgrdLinesDeliveredGoods.DataSource = null;
                //Reload Packing list
                LoadPK();
                //not allow grid editing
                SetGridNotEditable();

                SetVisiblePropertyByState();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Crystal Reports

        private void OpenReport(FunctionalityReport functionalityReport)
        {
            try
            {
                if (System.IO.File.Exists($"{Application.StartupPath}{functionalityReport.ReportFile}") == false)
                    throw new Exception("Report File does not exist");

                string idDoc = string.Empty;

                switch (functionalityReport.Code)
                {
                    case Constants.SUPPLY_DOCTYPE_PL:
                        idDoc = _docHeadPK.IdDoc;
                        break;
                }

                B1Report crReport = new B1Report();
                Dictionary<string, string> m_Parametros = new Dictionary<string, string>();
                m_Parametros.Add("@pIdDoc", idDoc);
                crReport.Parametros = m_Parametros;
                crReport.ReportFileName = $"{Application.StartupPath}{functionalityReport.ReportFile}";
                //The easiest way to get the default printer is to create a new PrinterSettings object. It starts with all default values.
                System.Drawing.Printing.PrinterSettings settings = new System.Drawing.Printing.PrinterSettings();
                crReport.PrinterName = settings.PrinterName;

                crReport.PreviewReport();
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