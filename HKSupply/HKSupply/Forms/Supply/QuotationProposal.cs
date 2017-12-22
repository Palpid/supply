using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using HKSupply.General;
using HKSupply.Models;
using HKSupply.Models.Supply;
using HKSupply.Styles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid;
using HKSupply.Helpers;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraPrinting;
using DevExpress.Printing.ExportHelpers;
using DevExpress.Export;
using DevExpress.Export.Xl;
using HKSupply.PRJ_Stocks.Classes;
using HKSupply.PRJ_Stocks.DB;
using System.IO;

namespace HKSupply.Forms.Supply
{
    public partial class QuotationProposal : RibbonFormBase
    {
        #region Stocks (improvops)
        private Stocks _STKAct = new Stocks();
        private Stocks.Warehouse _whEtniaHkOnHand;
        private Stocks.Warehouse _whEtniaHkAssigned;
        private Stocks.Warehouse _whEtniaHkTransit;
        #endregion

        #region Constants
        private const string VIEW_COLUMN = "View";
        #endregion

        #region Constants
        private const string TOTAL_AMOUNT_COLUMN = "TotalAmount";
        private const string COL_STOCK_ONHAND = "StockOnHand";
        private const string COL_STOCK_ASSIGNED = "Assigned";
        private const string COL_STOCK_TRANSIT = "Transit";
        #endregion

        #region Enums
        enum eGridSummaries
        {
            totalQuantityBomMt,
            totalQuantityBomHw,
            totalQuantityMt,
            totalQuantityHw
        }
        #endregion

        #region Private Members

        Font _labelDefaultFontBold = new Font("SourceSansProRegular", 8, FontStyle.Bold);
        Font _labelDefaultFont = new Font("SourceSansProRegular", 8, FontStyle.Regular);

        List<Customer> _customersList;
        List<ItemGroup> _itemGroupList;

        List<ItemMt> _itemsMtList;
        List<ItemHw> _itemsHwList;

        BindingList<DocLine> _docLinesList;
        DocHead _docHeadQP;
        DocHead _docHeadAssociatedPO;
        DocHead _docHeadAssociatedSO;
        BindingList<DocHeadAttachFile> _docHeadAttachFileList;

        decimal _totalQuantityBomMt;
        int _totalQuantityBomHw;
        decimal _totalQuantityMt;
        int _totalQuantityHw;

        bool _isLoadingQP = false;
        #endregion

        #region Constructor
        public QuotationProposal()
        {
            InitializeComponent();

            try
            {
                Cursor = Cursors.WaitCursor;

                ConfigureRibbonActions();
                LoadAuxList();
                SetUpLabels();
                SetUpButtons();
                SetUpSearchLookUpEdit();
                SetUpEvents();
                SetUpGrdLines();
                SetUpGrdFiles();
                SetupPanelControl();
                SetUpPictureEdit();

                SetVisiblePropertyByState();
                SetObjectsReadOnly();

                GetAllStock();
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
                if (_docLinesList?.Count > 0)
                    ConfigureRibbonActionsEditing();
                else
                {
                    MessageBox.Show(GlobalSetting.ResManager.GetString("NoDataSelected"));
                    RestoreInitState();
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

                if (ValidateQP() == false)
                    return;

                DialogResult result = MessageBox.Show(GlobalSetting.ResManager.GetString("SaveChanges"), "", MessageBoxButtons.YesNo);

                if (result != DialogResult.Yes)
                    return;

                Cursor = Cursors.WaitCursor;

                if (CurrentState == ActionsStates.Edit)
                {
                    res = UpdateQP();
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

            if (gridViewLines.DataRowCount == 0)
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
                    gridViewLines.ExportToCsv(ExportCsvFile);

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
            if (gridViewLines.DataRowCount == 0)
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

                    XlsxExportOptionsEx op = new XlsxExportOptionsEx();
                    op.CustomizeSheetHeader += Op_CustomizeSheetHeader;
                    op.CustomizeSheetFooter += Op_CustomizeSheetFooter;
                    op.CustomizeCell += Op_CustomizeCell;
                    op.ApplyFormattingToEntireColumn = DefaultBoolean.False;
                    //op.RawDataMode = true;
                    //op.ShowGridLines = true;

                    //gridViewLines.OptionsPrint.PrintHorzLines = false;
                    //gridViewLines.OptionsPrint.PrintVertLines = false;

                    gridViewLines.OptionsPrint.PrintFooter = false;
                    gridViewLines.ExportToXlsx(ExportExcelFile, op);

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
        private void QuotationProposal_Load(object sender, EventArgs e)
        {

        }

        private void SbSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (dateEditQPCreationDate.EditValue != null || txtQPNumber.EditValue != null)
                {
                    SearchQP();
                }
                else
                {
                    XtraMessageBox.Show("Select a Doc. Date or QP number", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtQPNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtQPNumber.EditValue != null)
            {
                SearchQP();
            }
        }

        private void TxtQPNumber_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (_isLoadingQP)
                    return;

                ResetQP();
                ResetForm(resetQpNumber: false);
            }
            catch
            {
                throw;
            }
        }

        private void SbFinishQP_Click(object sender, EventArgs e)
        {
            try
            {
                bool res = false;

                if (ValidateQP() == false)
                    return;

                DialogResult result = MessageBox.Show("Save change and finish Quotation Proposal", "", MessageBoxButtons.YesNo);

                if (result != DialogResult.Yes)
                    return;

                Cursor = Cursors.WaitCursor;

                if (CurrentState == ActionsStates.Edit)
                {
                    res = UpdateQP(finishQP:true);
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

        private void SlueCustomer_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (_isLoadingQP)
                    return;

                ResetQP();
                ResetForm(resetCustomer:false);
            }
            catch
            {
                throw;
            }
        }

        private void DateEditQPCreationDate_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (_isLoadingQP)
                    return;

                ResetQP();
                ResetForm(resetQpDate: false);
            }
            catch
            {
                throw;
            }
        }

        private void GridViewLines_ShowingEditor(object sender, CancelEventArgs e)
        {
            try
            {

                GridView view = sender as GridView;
                DocLine row = view.GetRow(view.FocusedRowHandle) as DocLine;

                if (row.LineState == DocLine.LineStates.New)
                    return;

                switch (view.FocusedColumn.FieldName)
                {
                    case nameof(DocLine.IdItemGroup):
                    case nameof(DocLine.IdItemBcn):
                        e.Cancel = true;
                        break;
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GridViewLines_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                DocLine row = view.GetRow(e.RowHandle) as DocLine;

                if (row == null)
                    return;

                switch (e.Column.FieldName)
                {
                    case nameof(DocLine.IdItemBcn):

                        RepositoryItemSearchLookUpEdit riItems = new RepositoryItemSearchLookUpEdit()
                        {
                            ShowClearButton = false,
                            NullText = "Select..."
                        };

                        if (row.IdItemGroup == Constants.ITEM_GROUP_MT)
                        {
                            riItems.DataSource = _itemsMtList;
                            riItems.ValueMember = nameof(ItemMt.IdItemBcn);
                            riItems.DisplayMember = nameof(ItemMt.IdItemBcn);
                            riItems.View.Columns.AddField(nameof(ItemMt.IdItemBcn)).Visible = true;
                            riItems.View.Columns.AddField(nameof(ItemMt.ItemDescription)).Visible = true;

                            e.RepositoryItem = riItems;
                        }
                        else if (row.IdItemGroup == Constants.ITEM_GROUP_HW)
                        {
                            riItems.DataSource = _itemsHwList;
                            riItems.ValueMember = nameof(ItemHw.IdItemBcn);
                            riItems.DisplayMember = nameof(ItemHw.IdItemBcn);
                            riItems.View.Columns.AddField(nameof(ItemHw.IdItemBcn)).Visible = true;
                            riItems.View.Columns.AddField(nameof(ItemHw.ItemDescription)).Visible = true;

                            e.RepositoryItem = riItems;
                        }

                        break;

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GridViewLines_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                DocLine row = view.GetRow(view.FocusedRowHandle) as DocLine;

                if (row == null)
                    return;

                switch (view.FocusedColumn.FieldName)
                {
                    case nameof(DocLine.IdItemBcn):

                        if (row.IdItemGroup == null)
                            return;

                        var idItem = e.Value.ToString();

                        //Can't repeat item
                        int existItem = _docLinesList.Where(a => a.IdItemBcn != null && a.IdItemBcn.Equals(idItem)).Count();
                        if (existItem > 0)
                        {
                            e.Valid = false;
                            e.ErrorText = "Item already exist";
                            return;
                        }

                        //clear some fields
                        row.Quantity = 0;
                        row.Remarks = string.Empty;

                        //Item
                        if (row.IdItemGroup == Constants.ITEM_GROUP_MT)
                        {
                            var itemMt = _itemsMtList.Where(a => a.IdItemBcn.Equals(idItem)).Single().Clone();
                            row.Item = itemMt;
                        }
                        else if(row.IdItemGroup == Constants.ITEM_GROUP_HW)
                        {
                            var itemHw = _itemsHwList.Where(a => a.IdItemBcn.Equals(idItem)).Single().Clone();
                            row.Item = itemHw;
                        }

                        //Price 
                        //TODO!!! tarifas de etnia hacia las fábricas??
                        row.UnitPrice = 0;
                        row.UnitPriceBaseCurrency = 0;

                        //Status & quantity
                        row.IdSupplyStatus = Constants.SUPPLY_STATUS_OPEN;
                        row.DeliveredQuantity = 0;

                        //TODO: batch en las QP?? Es necesario porque forma parte de la PK, formato??
                        //De momento los hago consecutivos para las nuevas líneas
                        row.Batch = txtQPNumber.Text + _docLinesList.Count.ToString();

                        //agregamos una línea nueva salvo que ya exista una en blanco
                        if (_docLinesList.Where(a => a.Item == null).Count() == 0)
                            _docLinesList.Add(new DocLine() { LineState = DocLine.LineStates.New });

                        break;

                    case nameof(DocLine.Quantity):
                        decimal qty = Convert.ToDecimal(e.Value);

                        //Sólo los RM puede ser decimales
                        if (row.IdItemGroup != Constants.ITEM_GROUP_MT)
                        {
                            bool isInteger = unchecked(qty == (int)qty);
                            if (isInteger == false)
                            {
                                e.Valid = false;
                                e.ErrorText = "Value must be integer";
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GridViewLines_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            try
            {
                // Get the summary ID. 
                eGridSummaries summaryID = (eGridSummaries)(e.Item as GridSummaryItem).Tag;
                GridView view = sender as GridView;

                // Initialization 
                if(e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    _totalQuantityBomHw = 0;
                    _totalQuantityBomMt = 0;
                    _totalQuantityHw = 0;
                    _totalQuantityMt = 0;
                }

                // Calculation 
                if(e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DocLine row = view.GetRow(e.RowHandle) as DocLine;

                    switch (summaryID)
                    {
                        case eGridSummaries.totalQuantityBomMt: 

                            if (row.IdItemGroup == Constants.ITEM_GROUP_MT)
                                _totalQuantityBomMt += Convert.ToDecimal(e.FieldValue);
                            break;

                        case eGridSummaries.totalQuantityBomHw: 

                            if (row.IdItemGroup == Constants.ITEM_GROUP_HW)
                                _totalQuantityBomHw += Convert.ToInt32(e.FieldValue);
                            break;

                        case eGridSummaries.totalQuantityMt: 

                            if (row.IdItemGroup == Constants.ITEM_GROUP_MT)
                                _totalQuantityMt += Convert.ToDecimal(e.FieldValue);
                            break;

                        case eGridSummaries.totalQuantityHw: 

                            if (row.IdItemGroup == Constants.ITEM_GROUP_HW)
                                _totalQuantityHw += Convert.ToInt32(e.FieldValue);
                            break;
                    }

                }

                // Finalization
                if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    switch (summaryID)
                    {
                        case eGridSummaries.totalQuantityBomMt:
                            e.TotalValue = _totalQuantityBomMt;
                            break;
                        case eGridSummaries.totalQuantityBomHw:
                            e.TotalValue = _totalQuantityBomHw;
                            break;
                        case eGridSummaries.totalQuantityMt:
                            e.TotalValue = _totalQuantityMt;
                            break;
                        case eGridSummaries.totalQuantityHw:
                            e.TotalValue = _totalQuantityHw;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GridViewLines_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            try
            {
                DocLine docLine = e.Row as DocLine;

                if (docLine == null)
                    return;

                switch (e.Column.FieldName)
                {
                    case COL_STOCK_ONHAND:

                        var stockOnHand = _STKAct.GetStockItem(_whEtniaHkOnHand, docLine.IdItemBcn);
                        e.Value = stockOnHand?.FreeStock;
                        break;

                    case COL_STOCK_ASSIGNED:

                        //var stockAssigned = _STKAct.GetStockItem(_whEtniaHkAssigned, docLine.IdItemBcn);
                        //e.Value = stockAssigned?.FreeStock;

                        //Asignados en el stock on hand de Etnia HK a alguien, no los que están en almacén assigned
                        var stockAssigned = _STKAct.GetStockItem(_whEtniaHkOnHand, docLine.IdItemBcn);
                        e.Value = stockAssigned?.AsgnStock;
                        break;

                    case COL_STOCK_TRANSIT:

                        var stockTransit = _STKAct.GetStockItem(_whEtniaHkTransit, docLine.IdItemBcn);
                        e.Value = stockTransit?.FreeStock;
                        break;
                }
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SbViewNewFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPathNewFile.Text) == false)
                {
                    DocHelper.OpenDoc(txtPathNewFile.Text);
                }
                else
                {
                    XtraMessageBox.Show(GlobalSetting.ResManager.GetString("NoFileSelected"), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SbOpenFile_Click(object sender, EventArgs e)
        {
            try
            {

                OpenFileDialog openFileDialog = new OpenFileDialog()
                {
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    Filter = "PDF files (*.pdf)|*.pdf|JPG files(*.jpg)|*.jpg|PNG files (*.png)|*.png",
                    Multiselect = false,
                    RestoreDirectory = true,
                };
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtPathNewFile.Text = openFileDialog.FileName;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SbAttachFile_Click(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(txtPathNewFile.Text) == false || System.IO.File.Exists(txtPathNewFile.Text))
                {
                    AttachFile();
                }
                
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RepoButton_Click(object sender, EventArgs e)
        {
            try
            {
                DocHeadAttachFile attachFile = gridViewFiles.GetRow(gridViewFiles.FocusedRowHandle) as DocHeadAttachFile;

                if (attachFile !=null)
                {
                    DocHelper.OpenDoc(Constants.SUPPLY_ATTACH_FILES_PATH + attachFile.FilePath);
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
                txtQPNumber.EditValue = idDoc;
                SearchQP();
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Private Methods

        #region SetUps Form Objects

        private void SetUpLabels()
        {
            try
            {
                /********* Fonts **********/
                //Header 
                lblPONumber.Font = _labelDefaultFontBold;
                lblQPNumber.Font = _labelDefaultFontBold;
                lblDate.Font = _labelDefaultFontBold;
                lblWeek.Font = _labelDefaultFontBold;
                lblPODate.Font = _labelDefaultFontBold;
                lblQPCreationDate.Font = _labelDefaultFontBold;
                lblCustomer.Font = _labelDefaultFontBold;
                lblPODateWeek.Font = _labelDefaultFont;
                lblQPCreationDateWeek.Font = _labelDefaultFont;
                txtPONumber.Font = _labelDefaultFontBold;
                txtQPNumber.Font = _labelDefaultFontBold;
                lblRemarks.Font = _labelDefaultFontBold;

                //Terms Tab
                lblCompany.Font = _labelDefaultFontBold;
                lblAddress.Font = _labelDefaultFontBold;
                lblContact.Font = _labelDefaultFontBold;
                lblTxtCompany.Font = _labelDefaultFontBold;
                lblTxtAddress.Font = _labelDefaultFont;
                lblTxtContact.Font = _labelDefaultFont;

                /********* Texts **********/
                //Headers
                lblPONumber.Text = "PO Number";
                lblQPNumber.Text = "Q. Proposal Number";
                lblDate.Text = "Date";
                lblWeek.Text = "Week";
                lblPODate.Text = "PO DATE";
                lblQPCreationDate.Text = "QP CREATION";
                lblCustomer.Text = "CUSTOMER";
                lblPODateWeek.Text = string.Empty;
                lblQPCreationDateWeek.Text = string.Empty;
                lblRemarks.Text = "Remarks";
                //Terms Tab
                lblCompany.Text = "Company:";
                lblAddress.Text = "Address:";
                lblContact.Text = "Contact:";
                lblTxtCompany.Text = string.Empty;
                lblTxtAddress.Text = string.Empty;
                lblTxtContact.Text = string.Empty;

                /********* Align **********/
                //Headers
                lblPODateWeek.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                lblQPCreationDateWeek.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                txtPONumber.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                txtQPNumber.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;

                //Terms Tab
                lblCompany.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
                lblAddress.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
                lblContact.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
                /********* ReadOnly **********/
                txtPONumber.ReadOnly = true; //no es un label, lo sé

                /********* BackColor **********/
                txtPONumber.Properties.Appearance.BackColor = AppStyles.EtniaRed;
                txtPONumber.Properties.Appearance.BackColor2 = AppStyles.EtniaRed;

                txtQPNumber.Properties.Appearance.BackColor = Color.OrangeRed;
                txtQPNumber.Properties.Appearance.BackColor2 = Color.OrangeRed;
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
                //DevExpress.Skins.Skin currentSkinsbFinishQP = DevExpress.Skins.CommonSkins.GetSkin(sbFinishQP.LookAndFeel);
                //lookAndFeelButton.UseDefaultLookAndFeel = false;
                //sbFinishQP.ImageOptions.Image = Image.FromFile(@"Resources\Images\button_red.png");
                //var currentSkinSbFinishQP = DevExpress.Skins.CommonSkins.GetSkin(sbFinishQP.LookAndFeel);
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
                sbFinishQP.Click += SbFinishQP_Click;
                txtQPNumber.KeyDown += TxtQPNumber_KeyDown;
                txtQPNumber.EditValueChanged += TxtQPNumber_EditValueChanged;
                slueCustomer.EditValueChanged += SlueCustomer_EditValueChanged;
                dateEditQPCreationDate.EditValueChanged += DateEditQPCreationDate_EditValueChanged;
                sbViewNewFile.Click += SbViewNewFile_Click;
                sbOpenFile.Click += SbOpenFile_Click;
                sbAttachFile.Click += SbAttachFile_Click;
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

        private void SetUpGrdLines()
        {
            try
            {
                //Activar que se alternen los colores de las filas del grid
                gridViewLines.OptionsView.EnableAppearanceOddRow = true;
                gridViewLines.OptionsView.EnableAppearanceEvenRow = true;

                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                gridViewLines.OptionsView.ColumnAutoWidth = false;
                gridViewLines.HorzScrollVisibility = ScrollVisibility.Auto;

                //Hacer todo el grid no editable
                gridViewLines.OptionsBehavior.Editable = false;

                //Hide grouping panel
                gridViewLines.OptionsView.ShowGroupPanel = false;

                //Disable sorting
                gridViewLines.OptionsCustomization.AllowSort = false;

                //Column Definition
                GridColumn colIdItemGroup = new GridColumn() { Caption = "Group", Visible = true, FieldName = nameof(DocLine.IdItemGroup), Width = 100 };
                GridColumn colIdItemBcn = new GridColumn() { Caption = "Item Code", Visible = true, FieldName = nameof(DocLine.IdItemBcn), Width = 200 };
                GridColumn colDescription = new GridColumn() { Caption = "Description", Visible = true, FieldName = nameof(DocLine.ItemDesc), Width = 350 };
                GridColumn colBatch = new GridColumn() { Caption = "Batch", Visible = true, FieldName = nameof(DocLine.Batch), Width = 100 };
                GridColumn colQuantityOriginal = new GridColumn() { Caption = "Quantity BOM", Visible = true, FieldName = nameof(DocLine.QuantityOriginal), Width = 110 };
                GridColumn colQuantity = new GridColumn() { Caption = "Quantity", Visible = true, FieldName = nameof(DocLine.Quantity), Width = 85 };
                GridColumn colUnit = new GridColumn() { Caption = "Unit", Visible = true, FieldName = nameof(DocLine.ItemUnit), Width = 85 };
                GridColumn colUnitPrice = new GridColumn() { Caption = "Unit Price", Visible = true, FieldName = nameof(DocLine.UnitPrice), Width = 85 };
                GridColumn colTotalAmount = new GridColumn() { Caption = "TotalAmount", Visible = true, FieldName = nameof(DocLine.TotalAmount), Width = 120 };
                GridColumn colRemarks = new GridColumn() { Caption = "Remarks", Visible = true, FieldName = nameof(DocLine.Remarks), Width = 350 };
                GridColumn colStockOnHand = new GridColumn() { Caption = "On Hand HK", Visible = true, FieldName = COL_STOCK_ONHAND, Width = 100 };
                GridColumn colStockAssigned = new GridColumn() { Caption = "Assigned to customers", Visible = true, FieldName = COL_STOCK_ASSIGNED, Width = 150 };
                GridColumn colStockTransit = new GridColumn() { Caption = "Transit to HK", Visible = true, FieldName = COL_STOCK_TRANSIT, Width = 100 };

                //Display Format
                colUnitPrice.DisplayFormat.FormatType = FormatType.Numeric;
                colUnitPrice.DisplayFormat.FormatString = "n4";

                colTotalAmount.DisplayFormat.FormatType = FormatType.Numeric;
                colTotalAmount.DisplayFormat.FormatString = "n4";

                colQuantity.DisplayFormat.FormatType = FormatType.Numeric;
                colQuantity.DisplayFormat.FormatString = "n3";

                colQuantityOriginal.DisplayFormat.FormatType = FormatType.Numeric;
                colQuantityOriginal.DisplayFormat.FormatString = "n3";

                colStockOnHand.DisplayFormat.FormatType = FormatType.Numeric;
                colStockOnHand.DisplayFormat.FormatString = "n3";

                colStockAssigned.DisplayFormat.FormatType = FormatType.Numeric;
                colStockAssigned.DisplayFormat.FormatString = "n3";

                colStockTransit.DisplayFormat.FormatType = FormatType.Numeric;
                colStockTransit.DisplayFormat.FormatString = "n3";

                //Unbound Columns
                colStockOnHand.UnboundType = UnboundColumnType.Decimal;
                colStockAssigned.UnboundType = UnboundColumnType.Decimal;
                colStockTransit.UnboundType = UnboundColumnType.Decimal;

                //Edit Repositories
                RepositoryItemSearchLookUpEdit riItemGroup = new RepositoryItemSearchLookUpEdit()
                {
                    DataSource = _itemGroupList,
                    ValueMember = nameof(ItemGroup.Id),
                    DisplayMember = nameof(ItemGroup.Description),
                    ShowClearButton = false,
                    NullText = "Select..."
                };

                colIdItemGroup.ColumnEdit = riItemGroup;

                RepositoryItemTextEdit ritxt3Dec = new RepositoryItemTextEdit();
                ritxt3Dec.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                ritxt3Dec.Mask.EditMask = "F3";
                ritxt3Dec.AllowNullInput = DefaultBoolean.True;

                colQuantity.ColumnEdit = ritxt3Dec;

                //Summaries
                gridViewLines.OptionsView.ShowFooter = true;

                colTotalAmount.Summary.Add(SummaryItemType.Sum, nameof(DocLine.TotalAmount), "{0:n4}");

                colQuantityOriginal.Summary.AddRange(new GridSummaryItem[] {
                    new GridColumnSummaryItem(SummaryItemType.Custom, nameof(DocLine.QuantityOriginal), "{0:n3} Kg", eGridSummaries.totalQuantityBomMt),
                    new GridColumnSummaryItem(SummaryItemType.Custom, nameof(DocLine.QuantityOriginal), "{0} PC", eGridSummaries.totalQuantityBomHw) });

                colQuantity.Summary.AddRange(new GridSummaryItem[] {
                    new GridColumnSummaryItem(SummaryItemType.Custom, nameof(DocLine.Quantity), "{0:n3} Kg", eGridSummaries.totalQuantityMt),
                    new GridColumnSummaryItem(SummaryItemType.Custom, nameof(DocLine.Quantity), "{0} PC", eGridSummaries.totalQuantityHw) });

                //Export Columns
                colIdItemGroup.OptionsColumn.Printable = DefaultBoolean.False;
                colDescription.OptionsColumn.Printable = DefaultBoolean.False;
                colBatch.OptionsColumn.Printable = DefaultBoolean.False;
                colUnitPrice.OptionsColumn.Printable = DefaultBoolean.False;
                colTotalAmount.OptionsColumn.Printable = DefaultBoolean.False;
                colStockOnHand.OptionsColumn.Printable = DefaultBoolean.False;
                colStockAssigned.OptionsColumn.Printable = DefaultBoolean.False;
                colStockTransit.OptionsColumn.Printable = DefaultBoolean.False;

                //Add columns to grid root view
                gridViewLines.Columns.Add(colIdItemGroup);
                gridViewLines.Columns.Add(colIdItemBcn);
                gridViewLines.Columns.Add(colDescription);
                gridViewLines.Columns.Add(colBatch);
                gridViewLines.Columns.Add(colQuantityOriginal);
                gridViewLines.Columns.Add(colQuantity);
                gridViewLines.Columns.Add(colUnit);
                gridViewLines.Columns.Add(colUnitPrice);
                gridViewLines.Columns.Add(colTotalAmount);
                gridViewLines.Columns.Add(colStockOnHand);
                gridViewLines.Columns.Add(colStockAssigned);
                gridViewLines.Columns.Add(colStockTransit);
                gridViewLines.Columns.Add(colRemarks);

                //Events
                gridViewLines.ShowingEditor += GridViewLines_ShowingEditor;
                gridViewLines.CustomRowCellEdit += GridViewLines_CustomRowCellEdit;
                gridViewLines.ValidatingEditor += GridViewLines_ValidatingEditor;
                gridViewLines.CustomSummaryCalculate += GridViewLines_CustomSummaryCalculate;
                gridViewLines.CustomUnboundColumnData += GridViewLines_CustomUnboundColumnData;

            }
            catch
            {
                throw;
            }
        }

        private void SetUpGrdFiles()
        {
            try
            {
                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                gridViewFiles.OptionsView.ColumnAutoWidth = false;
                gridViewFiles.HorzScrollVisibility = ScrollVisibility.Auto;

                //Columns definition
                GridColumn colFileName = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("FileName"), Visible = true, FieldName = nameof(DocHeadAttachFile.FileName), Width = 350 };
                GridColumn colUser= new GridColumn() { Caption = GlobalSetting.ResManager.GetString("User"), Visible = true, FieldName = nameof(DocHeadAttachFile.User), Width = 100 };
                GridColumn colCreateDate = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("CreateDate"), Visible = true, FieldName = nameof(DocHeadAttachFile.CreateDate), Width = 130 };
                GridColumn colViewButton = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("View"), Visible = true, FieldName = VIEW_COLUMN, Width = 50 };

                //Display Format
                colCreateDate.DisplayFormat.FormatType = FormatType.DateTime;

                //View button
                colViewButton.UnboundType = UnboundColumnType.Object;
                RepositoryItemButtonEdit repoButtonViewFile = new RepositoryItemButtonEdit()
                {
                    Name = "btnViewFile",
                    TextEditStyle = TextEditStyles.HideTextEditor
                };

                repoButtonViewFile.Click += RepoButton_Click;

                colViewButton.ShowButtonMode = ShowButtonModeEnum.ShowAlways;
                colViewButton.ColumnEdit = repoButtonViewFile;

                //Only allow edit the button to allow click
                colFileName.OptionsColumn.AllowEdit = false;
                colUser.OptionsColumn.AllowEdit = false;
                colCreateDate.OptionsColumn.AllowEdit = false;

                //Add columns to grid root view
                gridViewFiles.Columns.Add(colFileName);
                gridViewFiles.Columns.Add(colUser);
                gridViewFiles.Columns.Add(colCreateDate);
                gridViewFiles.Columns.Add(colViewButton);

            }
            catch
            {
                throw;
            }
        }

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

        private void SetUpPictureEdit()
        {
            try
            {
                peRm.Properties.ShowMenu = false;
                peRm.Properties.SizeMode = PictureSizeMode.Zoom;
                peRm.Image = Image.FromFile(@"Resources\Images\Acetate_244.jpg");
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Loads / Resets
        private void LoadAuxList()
        {
            try
            {
                _customersList = GlobalSetting.CustomerService.GetCustomers().Where(a => a.Factory == true).ToList();

                _itemsMtList = GlobalSetting.ItemMtService.GetItems();
                _itemsHwList = GlobalSetting.ItemHwService.GetItems();

                _itemGroupList = new List<ItemGroup>();
                _itemGroupList.Add(new ItemGroup() { Id = Constants.ITEM_GROUP_MT, Description = Constants.ITEM_GROUP_MT });
                _itemGroupList.Add(new ItemGroup() { Id = Constants.ITEM_GROUP_HW, Description = Constants.ITEM_GROUP_HW });
            }
            catch
            {
                throw;
            }
        }

        private void ResetQP()
        {
            try
            {
                _docHeadAssociatedPO = null;
                _docHeadAssociatedSO = null;
                _docHeadQP = null;
                _docLinesList = null;
                xgrdLines.DataSource = null;
                _docLinesList = null;
                xgrdFiles.DataSource = null;
            }
            catch
            {
                throw;
            }
        }

        private void LoadQP()
        {
            try
            {
                _isLoadingQP = true;

                if (_docHeadAssociatedPO == null)
                    throw new Exception("Associated PO not found");

                slueCustomer.EditValue = _docHeadQP.IdCustomer;

                dateEditPODate.EditValue = _docHeadAssociatedPO.DocDate;
                dateEditQPCreationDate.EditValue = _docHeadQP.CreationDate;

                lblPODateWeek.Text = dateEditPODate.DateTime.GetWeek().ToString();
                lblQPCreationDateWeek.Text = dateEditQPCreationDate.DateTime.GetWeek().ToString();

                txtPONumber.Text = _docHeadAssociatedPO.IdDoc;
                txtQPNumber.Text = _docHeadQP.IdDoc;

                memoEditRemarks.EditValue = _docHeadQP.Remarks;

                lblTxtAddress.Text = $"{_docHeadQP.Customer?.ShippingAddress} {_docHeadQP.Customer?.ShippingAddress2}";
                lblTxtCompany.Text = _docHeadQP.Customer.CustomerName;
                lblTxtContact.Text = $"{_docHeadQP.Customer.ContactName} ({_docHeadQP.Customer.ContactPhone})";

                _docLinesList = new BindingList<DocLine>(_docHeadQP.Lines);

                xgrdLines.DataSource = null;
                xgrdLines.DataSource = _docLinesList;

                xgrdFiles.DataSource = null;
                xgrdFiles.DataSource = _docHeadAttachFileList;

            }
            catch
            {
                throw;
            }
            finally
            {
                _isLoadingQP = false;
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
                    case ActionsStates.Edit:
                    case ActionsStates.New:
                        sbFinishQP.Visible = (_docHeadAssociatedSO == null); //Si ya se ha generado la SO no se puede finalizar de nuevo;
                        sbOrder.Visible = false; // true;
                        sbSearch.Visible = false;
                        break;

                    default:
                        sbFinishQP.Visible = false;
                        sbOrder.Visible = false;
                        sbSearch.Visible = true;
                        break;
                }
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
                dateEditPODate.ReadOnly = true;
                memoEditRemarks.ReadOnly = true;
            }
            catch
            {
                throw;
            }
        }

        private void SearchQP()
        {
            try
            {
                ResetQP();

                string idDocQP = txtQPNumber.Text;
                string customer = slueCustomer.EditValue as string;
                DateTime qpCreateDate = dateEditQPCreationDate.DateTime;

                if (string.IsNullOrEmpty(idDocQP) == false)
                {
                    _docHeadQP = GlobalSetting.SupplyDocsService.GetDoc(idDocQP);

                    if (_docHeadQP == null)
                    {
                        XtraMessageBox.Show("No Data Found", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (_docHeadQP.IdSupplyDocType != Constants.SUPPLY_DOCTYPE_QP)
                    {
                        XtraMessageBox.Show("Document is not a Sales Order", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        _docHeadAssociatedPO = GlobalSetting.SupplyDocsService.GetDoc(_docHeadQP.IdDocRelated);
                        _docHeadAssociatedSO = GlobalSetting.SupplyDocsService.GetDocByRelated(_docHeadQP.IdDoc);
                        _docHeadAttachFileList = new BindingList<DocHeadAttachFile>(GlobalSetting.DocHeadAttachFileService.GetDocHeadAttachFile(_docHeadQP.IdDoc));
                        LoadQP();
                    }
                  
                }
                else
                {
                    var docs = GlobalSetting.SupplyDocsService.GetDocs( idSupplier: null, idCustomer: customer, docDate: qpCreateDate, IdSupplyDocType: Constants.SUPPLY_DOCTYPE_QP, idSupplyStatus: null);

                    if (docs.Count == 0)
                    {
                        XtraMessageBox.Show("No Data Found", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    //else if(docs.Count == 1)
                    //{

                    //}
                    else
                    {
                        using (DialogForms.SelectDocs form = new DialogForms.SelectDocs())
                        {
                            form.InitData(docs);
                            if (form.ShowDialog() == DialogResult.OK)
                            {
                                _docHeadQP = form.SelectedDoc;
                                _docHeadAssociatedPO = GlobalSetting.SupplyDocsService.GetDoc(_docHeadQP.IdDocRelated);
                                LoadQP();
                            }
                        }
                    }
                }
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
                var docs = GlobalSetting.SupplyDocsService.GetDocsByRelated(_docHeadQP.IdDoc);

                if (docs.Count > 0)
                {
                    string msg = "The Following documents have been created:" + Environment.NewLine;

                    foreach (var doc in docs)
                    {
                        msg += $"{doc.IdDoc} ({doc.SupplyDocType.Description}){Environment.NewLine}";
                    }

                    XtraMessageBox.Show(msg);
                }

            }
            catch
            {
                throw;
            }
        }

        private void ResetForm(bool resetQpNumber = true, bool resetQpDate = true, bool resetCustomer = true)
        {
            try
            {
                //desuscribirse a los eventos de edit value changed para evitar que se lancen al modificar los valores
                slueCustomer.EditValueChanged -= SlueCustomer_EditValueChanged;
                dateEditQPCreationDate.EditValueChanged -= DateEditQPCreationDate_EditValueChanged;
                txtQPNumber.EditValueChanged -= TxtQPNumber_EditValueChanged;

                /********* Head *********/
                txtPONumber.EditValue = null;
                if (resetQpNumber) txtQPNumber.EditValue = null;
                if (resetCustomer) slueCustomer.EditValue = null;
                dateEditPODate.EditValue = null;
                lblPODateWeek.Text = string.Empty;
                if (resetQpDate)
                {
                    dateEditQPCreationDate.EditValue = null;
                    lblQPCreationDateWeek.Text = string.Empty;
                }

                memoEditRemarks.EditValue = null;

                /********* Terms Tab *********/
                lblTxtCompany.Text = string.Empty;
                lblTxtAddress.Text = string.Empty;
                lblTxtContact.Text = string.Empty;
            }
            catch
            {
                throw;
            }
            finally
            {
                slueCustomer.EditValueChanged += SlueCustomer_EditValueChanged;
                dateEditQPCreationDate.EditValueChanged += DateEditQPCreationDate_EditValueChanged;
                txtQPNumber.EditValueChanged += TxtQPNumber_EditValueChanged;
            }
        }

        #endregion

        #region Configure Ribbon Actions

        private void ConfigureRibbonActionsEditing()
        {
            try
            {
                //Fields cannot been edited
                txtQPNumber.ReadOnly = true;
                slueCustomer.ReadOnly = true;
                dateEditQPCreationDate.ReadOnly = true;

                //Fields can been edited
                memoEditRemarks.ReadOnly = false;

                //Allow edit all columns
                gridViewLines.OptionsBehavior.Editable = true;

                //Block common not editing columns
                gridViewLines.Columns[nameof(DocLine.ItemDesc)].OptionsColumn.AllowEdit = false;
                gridViewLines.Columns[nameof(DocLine.Batch)].OptionsColumn.AllowEdit = false;
                gridViewLines.Columns[nameof(DocLine.QuantityOriginal)].OptionsColumn.AllowEdit = false;
                gridViewLines.Columns[nameof(DocLine.UnitPrice)].OptionsColumn.AllowEdit = false;
                gridViewLines.Columns[TOTAL_AMOUNT_COLUMN].OptionsColumn.AllowEdit = false;


                //agregamos una línea nueva
                _docLinesList.Add(new DocLine() { LineState = DocLine.LineStates.New });
                gridViewLines.RefreshData();

                //Visible buttons
                SetVisiblePropertyByState();

                /*
                if (_existQP == true)
                {
                    gridViewLines.Columns[nameof(DocLine.IdItemBcn)].OptionsColumn.AllowEdit = false;
                }
                else
                {
                    gridViewLines.Columns[nameof(DocLine.DeliveredQuantity)].OptionsColumn.AllowEdit = false;

                    //agregamos una línea nueva
                    _docLinesList.Add(new DocLine() { LineState = DocLine.LineStates.New });
                    gridViewLines.RefreshData();
                }



                //events
                SetUpEventsEditing();

                //No editing form fields
                slueSupplier.ReadOnly = true;
                dateEditDocDate.ReadOnly = true;
                */

            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region CRUD
        private bool ValidateQP()
        {
            try
            {
                //TODO Validaciones ??!
                return true;
            }
            catch
            {
                throw;
            }
        }

        private bool UpdateQP(bool finishQP = false)
        {
            try
            {
                //para quedarse sólo con la parte final del batch (el número)
                List<DocLine> sortedLines = _docLinesList
                    .Where(lin => lin.IdItemBcn != null)
                    .OrderBy(a => Convert.ToInt32(System.Text.RegularExpressions.Regex.Match(a.Batch, @"\d+$").Value))
                    .ToList();

                DocHead quotationProposal = new DocHead()
                {
                    IdDoc = _docHeadQP.IdDoc,
                    IdSupplyDocType = _docHeadQP.IdSupplyDocType,
                    CreationDate = _docHeadQP.CreationDate,
                    DeliveryDate = _docHeadQP.DeliveryDate,
                    DocDate = _docHeadQP.DocDate,
                    IdSupplyStatus = _docHeadQP.IdSupplyStatus,
                    IdSupplier = _docHeadQP.IdSupplier,
                    IdCustomer = _docHeadQP.IdCustomer,
                    IdDeliveryTerm = _docHeadQP.IdDeliveryTerm,
                    IdPaymentTerms = _docHeadQP.IdPaymentTerms,
                    IdCurrency = _docHeadQP.IdCurrency,
                    Remarks = memoEditRemarks.EditValue as string,
                    Lines = sortedLines
                };

                DocHead updatedDoc = GlobalSetting.SupplyDocsService.UpdateDoc(quotationProposal, finishDoc: finishQP);

                ResetQP();

                _docHeadQP = updatedDoc;
                _docHeadAssociatedPO = GlobalSetting.SupplyDocsService.GetDoc(_docHeadQP.IdDocRelated);
                _docHeadAssociatedSO = GlobalSetting.SupplyDocsService.GetDocByRelated(_docHeadQP.IdDoc);

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
                //Actualizamos el stock por si ha habido algún movimiento
                GetAllStock();

                ResetForm();
                gridViewLines.OptionsBehavior.Editable = false;
                //Reload PO
                LoadQP();
                //Hacer el grid no editable
                gridViewLines.OptionsBehavior.Editable = false;

                //Restore de ribbon to initial states
                RestoreInitState();

                SetVisiblePropertyByState();

                txtQPNumber.ReadOnly = false;
                slueCustomer.ReadOnly = false;
                dateEditQPCreationDate.ReadOnly = false;
                memoEditRemarks.ReadOnly = true;

                
            }
            catch
            {
                throw;
            }
        }

        private void AttachFile()
        {
            try
            {
                string fileName = Path.GetFileName(txtPathNewFile.Text);
                string fileNameNoExtension = Path.GetFileNameWithoutExtension(txtPathNewFile.Text);
                string extension = Path.GetExtension(txtPathNewFile.Text);

                string attachedFilename = $"{fileNameNoExtension}_{DateTime.Now.ToString("yyyyMMddHHmmss")}{extension}";

                //Creamos los directorios si no existen
                new FileInfo(Constants.SUPPLY_ATTACH_FILES_PATH + _docHeadQP.IdDoc + "\\").Directory.Create();

                DocHeadAttachFile attachFile = new DocHeadAttachFile()
                {
                    IdDocHead = _docHeadQP.IdDoc,
                    FileName = fileName,
                    FileExtension = extension,
                    FilePath = _docHeadQP.IdDoc + "\\" + attachedFilename,
                    User = GlobalSetting.LoggedUser.UserLogin,
                    CreateDate = DateTime.Now
                };

                //move to file server
                File.Copy(txtPathNewFile.Text, Constants.SUPPLY_ATTACH_FILES_PATH + attachFile.FilePath, overwrite: true);

                //Save to database
                GlobalSetting.DocHeadAttachFileService.AddDocHeadAttachFile(attachFile);

                MessageBox.Show(GlobalSetting.ResManager.GetString("SaveSuccessfully"));

                txtPathNewFile.Text = string.Empty;

                //Reaload grid
                _docHeadAttachFileList = new BindingList<DocHeadAttachFile>(GlobalSetting.DocHeadAttachFileService.GetDocHeadAttachFile(_docHeadQP.IdDoc));
                xgrdFiles.DataSource = null;
                xgrdFiles.DataSource = _docHeadAttachFileList;

            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Stocks
        //Notes: La API de Stocks está desarrollada por improvops, sólo realizamos las llamadas a ella
        private void GetAllStock()
        {
            try
            {
                Call_DB_Stocks CallDBS = new Call_DB_Stocks();
                _STKAct = CallDBS.CallCargaStocks();

                _whEtniaHkOnHand = _STKAct.GetWareHouse(Constants.ETNIA_HK_COMPANY_CODE, Stocks.StockWareHousesType.OnHand);
                _whEtniaHkAssigned = _STKAct.GetWareHouse(Constants.ETNIA_HK_COMPANY_CODE, Stocks.StockWareHousesType.Assigned);
                _whEtniaHkTransit = _STKAct.GetWareHouse(Constants.ETNIA_HK_COMPANY_CODE, Stocks.StockWareHousesType.Transit);


            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Export to Excel

        private void Op_CustomizeSheetHeader(ContextEventArgs e)
        {
            try
            {

                //Formato de las celdas 
                var rowFormattingUnboldCenter = CreateXlFormattingObject(bold: false, size: 11);
                rowFormattingUnboldCenter.Alignment.HorizontalAlignment = XlHorizontalAlignment.Center;
                rowFormattingUnboldCenter.Border = new XlBorder() { Outline = false };

                var rowFormattingUnboldLeft = CreateXlFormattingObject(bold: false, size: 11);
                rowFormattingUnboldLeft.Alignment.HorizontalAlignment = XlHorizontalAlignment.Left;
                rowFormattingUnboldLeft.Border = new XlBorder() { Outline = false };

                //*************** Logo ***************//
                Image logo = Image.FromFile(@"Resources\Images\etnia_logo.jpg");
                e.ExportContext.InsertImage(logo, new XlCellRange(new XlCellPosition(0, 1), new XlCellPosition(1, 0)));
                //hay que agregar el mismo número de líneas que hemos usado en el logo más una de separación con el siguiente texto
                e.ExportContext.AddRow();
                e.ExportContext.AddRow();
                e.ExportContext.AddRow();

                //*************** Título ***************//
                var titleRow = new CellObject();
                titleRow.Value = @"HARDWARE & RAW MATERIAL REQUIREMENT";
                var rowFormattingTitle = CreateXlFormattingObject(true, 18);
                rowFormattingTitle.Alignment.HorizontalAlignment = XlHorizontalAlignment.Center;
                rowFormattingTitle.Border = new XlBorder() { Outline = false };
                titleRow.Formatting = rowFormattingTitle;
                e.ExportContext.AddRow(new[] { titleRow });
                // hacemos el merge de las celdas donde va el título
                var titleRange = new XlCellRange(new XlCellPosition(0, 3), new XlCellPosition(4, 3));
                e.ExportContext.MergeCells(titleRange);

                e.ExportContext.AddRow();

                //*************** 1a línea cabecera ***************//
                var factoryLitCell = new CellObject() { Value = "FACTORY: ", Formatting = rowFormattingUnboldLeft };
                var factoryCell = new CellObject() { Value = _docHeadQP.Customer.CustomerName, Formatting = rowFormattingUnboldLeft };

                var deliveryDateLitCell = new CellObject() { Value = "DELIVERY DATE:", Formatting = rowFormattingUnboldCenter };
                var deliveryDateCell = new CellObject() { Value = _docHeadQP.DeliveryDate.ToShortDateString(), Formatting = rowFormattingUnboldLeft };

                e.ExportContext.AddRow(new[] { factoryLitCell, factoryCell, deliveryDateLitCell, deliveryDateCell });

                //*************** 2a línea cabecera ***************//
                var poDateLitCell = new CellObject() { Value = "PO DATE:", Formatting = rowFormattingUnboldLeft };
                var poDateCell = new CellObject() { Value = _docHeadAssociatedPO.DocDate.ToShortDateString(), Formatting = rowFormattingUnboldLeft };

                var poLitCell = new CellObject() { Value = "PO#:", Formatting = rowFormattingUnboldCenter };
                var poCell = new CellObject() { Value = _docHeadAssociatedPO.IdDoc, Formatting = rowFormattingUnboldLeft };

                e.ExportContext.AddRow(new[] { poDateLitCell, poDateCell, poLitCell, poCell });

                //*************** 3a línea cabecera ***************//
                var modelLitCell = new CellObject() { Value = string.Empty, Formatting = rowFormattingUnboldCenter };
                var modelCell = new CellObject() { Value = string.Empty, Formatting = rowFormattingUnboldCenter };

                var qpLitCell = new CellObject() { Value = "QP#:", Formatting = rowFormattingUnboldCenter };
                var qpCell = new CellObject() { Value = _docHeadQP.IdDoc, Formatting = rowFormattingUnboldLeft };

                e.ExportContext.AddRow(new[] { modelLitCell, modelCell, qpLitCell, qpCell });


                e.ExportContext.AddRow(); // Línea en blanco de separación
            }
            catch
            {
                throw;
            }
        }

        private void Op_CustomizeSheetFooter(ContextEventArgs e)
        {
            try
            {
                //Calculamos los totales
                var totalMt = _docLinesList.Where(a => a.IdItemBcn != null && a.IdItemGroup.Equals(Constants.ITEM_GROUP_MT)).Sum(b => b.QuantityOriginal);
                var totalHw = _docLinesList.Where(a => a.IdItemBcn != null && a.IdItemGroup.Equals(Constants.ITEM_GROUP_HW)).Sum(b => b.QuantityOriginal);


                //Formato de las celdas (con negrita y sin negriya)
                var rowFormattingBoldCenter = CreateXlFormattingObject(bold: true, size: 11);
                rowFormattingBoldCenter.Alignment.HorizontalAlignment = XlHorizontalAlignment.Center;

                var rowFormattingUnboldCenter = CreateXlFormattingObject(bold: false, size: 11);
                rowFormattingUnboldCenter.Alignment.HorizontalAlignment = XlHorizontalAlignment.Center;
                rowFormattingUnboldCenter.Border = new XlBorder()
                {
                    Outline = false,
                };

                //Celda vacía
                //var dummyCell = new CellObject() { Value = string.Empty, Formatting = rowFormattingUnboldCenter };

                //Líneas con los totales personalizamos, el summary que exporta del grid lo hace sin formato
                var totalGrLitCell = new CellObject() { Value = "TOTAL GR", Formatting = rowFormattingBoldCenter };
                var totalGrCell = new CellObject() { Value = totalMt.ToString(), Formatting = rowFormattingBoldCenter };

                var totalPcLitCell = new CellObject() { Value = "TOTAL PC", Formatting = rowFormattingBoldCenter };
                var totalPcCell = new CellObject() { Value = totalHw.ToString(), Formatting = rowFormattingBoldCenter };

                e.ExportContext.AddRow(new[] { totalGrLitCell, totalGrCell });
                e.ExportContext.AddRow(new[] { totalPcLitCell, totalPcCell });

                //Líneas finales con literailes
                e.ExportContext.AddRow();
                var totalNwLitCell = new CellObject() { Value = "Total N.W. 总冫争重", Formatting = rowFormattingUnboldCenter };
                var dateLitCell = new CellObject() { Value = "DATE:", Formatting = rowFormattingUnboldCenter };
                var signatureLitCell = new CellObject() { Value = "SIGNATURE", Formatting = rowFormattingUnboldCenter };

                e.ExportContext.AddRow(new[] { totalNwLitCell });
                e.ExportContext.AddRow();
                e.ExportContext.AddRow(new[] { dateLitCell });
                e.ExportContext.AddRow();
                e.ExportContext.AddRow(new[] { signatureLitCell });

            }
            catch
            {
                throw;
            }
        }

        private void Op_CustomizeCell(CustomizeCellEventArgs e)
        {
            try
            {
                //Para exportar a excel la columna de cantidad la queremos en blanco ya que en principio 
                //el excel se enviará a la fábrica y ella pone las cantidades que desea.
                if (e.ColumnFieldName == nameof(DocLine.Quantity))
                {
                    //Descartamos la cabecera que sí queremos que se pinte
                    if ((e.Value is string && (string)e.Value == gridViewLines.Columns[nameof(DocLine.Quantity)].Caption) == false)
                    {
                        e.Handled = true;
                        e.Value = string.Empty;
                    }

                }
            }
            catch
            {
                throw;
            }
        }

        static XlFormattingObject CreateXlFormattingObject(bool bold, double size)
        {
            var cellFormat = new XlFormattingObject
            {
                Font = new XlCellFont
                {
                    Bold = bold,
                    Size = size
                },
                Alignment = new XlCellAlignment
                {
                    RelativeIndent = 10,
                    HorizontalAlignment = XlHorizontalAlignment.Center,
                    VerticalAlignment = XlVerticalAlignment.Center
                }
            };
            return cellFormat;
        }

        #endregion

        #endregion
    }
}
