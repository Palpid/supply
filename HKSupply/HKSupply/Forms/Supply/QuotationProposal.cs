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

namespace HKSupply.Forms.Supply
{
    public partial class QuotationProposal : RibbonFormBase
    {
        #region Constants
        private const string TOTAL_AMOUNT_COLUMN = "TotalAmount";
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

        int _totalQuantityBomMt;
        int _totalQuantityBomHw;
        int _totalQuantityMt;
        int _totalQuantityHw;

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
        }

        public override void bbiEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            base.bbiEdit_ItemClick(sender, e);

            try
            {
                if (_docLinesList?.Count > 0)
                    ConfigureRibbonActionsEditing();
                else
                    MessageBox.Show(GlobalSetting.ResManager.GetString("NoDataSelected"));

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
        private void QuotationProposal_Load(object sender, EventArgs e)
        {

        }

        private void SbSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (dateEditQPCreationDate.EditValue != null)
                {
                    SearchQP();
                }
                else
                {
                    XtraMessageBox.Show("Select a Doc. Date", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                                _totalQuantityBomMt += Convert.ToInt32(e.FieldValue);
                            break;

                        case eGridSummaries.totalQuantityBomHw: 

                            if (row.IdItemGroup == Constants.ITEM_GROUP_HW)
                                _totalQuantityBomHw += Convert.ToInt32(e.FieldValue);
                            break;

                        case eGridSummaries.totalQuantityMt: 

                            if (row.IdItemGroup == Constants.ITEM_GROUP_MT)
                                _totalQuantityMt += Convert.ToInt32(e.FieldValue);
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
                //TODO
                sbSearch.Click += SbSearch_Click;
                //sbOrder.Click += SbOrder_Click;
                //sbFinishPO.Click += SbFinishPO_Click;

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
                GridColumn colBatch = new GridColumn() { Caption = "Batch", Visible = true, FieldName = nameof(DocLine.Batch), Width = 85 };
                GridColumn colQuantityOriginal = new GridColumn() { Caption = "Quantity BOM", Visible = true, FieldName = nameof(DocLine.QuantityOriginal), Width = 110 };
                GridColumn colQuantity = new GridColumn() { Caption = "Quantity", Visible = true, FieldName = nameof(DocLine.Quantity), Width = 85 };
                GridColumn colUnit = new GridColumn() { Caption = "Unit", Visible = true, FieldName = nameof(DocLine.ItemUnit), Width = 85 };
                GridColumn colUnitPrice = new GridColumn() { Caption = "Unit Price", Visible = true, FieldName = nameof(DocLine.UnitPrice), Width = 85 };
                GridColumn colTotalAmount = new GridColumn() { Caption = "TotalAmount", Visible = true, FieldName = nameof(DocLine.TotalAmount), Width = 120 };
                GridColumn colRemarks = new GridColumn() { Caption = "Remarks", Visible = true, FieldName = nameof(DocLine.Remarks), Width = 350 };

                //Display Format
                colUnitPrice.DisplayFormat.FormatType = FormatType.Numeric;
                colUnitPrice.DisplayFormat.FormatString = "n2";

                colTotalAmount.DisplayFormat.FormatType = FormatType.Numeric;
                colTotalAmount.DisplayFormat.FormatString = "n2";

                colQuantity.DisplayFormat.FormatType = FormatType.Numeric;
                colQuantity.DisplayFormat.FormatString = "n0";

                colQuantityOriginal.DisplayFormat.FormatType = FormatType.Numeric;
                colQuantityOriginal.DisplayFormat.FormatString = "n0";

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

                RepositoryItemTextEdit ritxtInt = new RepositoryItemTextEdit();
                ritxtInt.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                ritxtInt.Mask.EditMask = "N";
                ritxtInt.AllowNullInput = DefaultBoolean.True;

                colQuantity.ColumnEdit = ritxtInt;

                //Summaries
                gridViewLines.OptionsView.ShowFooter = true;

                colTotalAmount.Summary.Add(SummaryItemType.Sum, nameof(DocLine.TotalAmount), "{0:n2}");

                colQuantityOriginal.Summary.AddRange(new GridSummaryItem[] {
                    new GridColumnSummaryItem(SummaryItemType.Custom, nameof(DocLine.QuantityOriginal), "{0} Gr", eGridSummaries.totalQuantityBomMt),
                    new GridColumnSummaryItem(SummaryItemType.Custom, nameof(DocLine.QuantityOriginal), "{0} PC", eGridSummaries.totalQuantityBomHw) });

                colQuantity.Summary.AddRange(new GridSummaryItem[] {
                    new GridColumnSummaryItem(SummaryItemType.Custom, nameof(DocLine.Quantity), "{0} Gr", eGridSummaries.totalQuantityMt),
                    new GridColumnSummaryItem(SummaryItemType.Custom, nameof(DocLine.Quantity), "{0} PC", eGridSummaries.totalQuantityHw) });


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
                gridViewLines.Columns.Add(colRemarks);

                //Evets
                gridViewLines.ShowingEditor += GridViewLines_ShowingEditor;
                gridViewLines.CustomRowCellEdit += GridViewLines_CustomRowCellEdit;
                gridViewLines.ValidatingEditor += GridViewLines_ValidatingEditor;
                gridViewLines.CustomSummaryCalculate += GridViewLines_CustomSummaryCalculate;
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
                _customersList = GlobalSetting.CustomerService.GetCustomers();

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

        private void ResetPO()
        {
            try
            {
                _docHeadAssociatedPO = null;
                _docHeadQP = null;
                _docLinesList = null;
                xgrdLines.DataSource = null;
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

                _docHeadAssociatedPO = GlobalSetting.SupplyDocsService.GetDoc(_docHeadQP.IdDoc.Replace(Constants.SUPPLY_DOCTYPE_QP,string.Empty));

                if (_docHeadAssociatedPO == null)
                    throw new Exception("Associated PO not found");

                slueCustomer.EditValue = _docHeadQP.IdCustomer;

                dateEditPODate.EditValue = _docHeadAssociatedPO.DocDate;
                dateEditQPCreationDate.EditValue = _docHeadQP.CreationDate;

                lblPODateWeek.Text = GetWeek(dateEditPODate.DateTime).ToString();
                lblQPCreationDateWeek.Text = GetWeek(dateEditQPCreationDate.DateTime).ToString();

                txtPONumber.Text = _docHeadAssociatedPO.IdDoc;
                txtQPNumber.Text = _docHeadQP.IdDoc;

                _docLinesList = new BindingList<DocLine>(_docHeadQP.Lines);

                xgrdLines.DataSource = null;
                xgrdLines.DataSource = _docLinesList;

            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Random

        private void SetVisiblePropertyByState()
        {
            try
            {
                switch (CurrentState)
                {
                    case ActionsStates.Edit:
                    case ActionsStates.New:
                        sbFinishQP.Visible = true;
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

        private void SearchQP()
        {
            try
            {
                ResetPO();

                string customer = slueCustomer.EditValue as string;
                DateTime qpCreateDate = dateEditQPCreationDate.DateTime;

                var docs = GlobalSetting.SupplyDocsService.GetDocs( idSupplier: null, idCustomer: customer, docDate: qpCreateDate, IdSupplyDocType: Constants.SUPPLY_DOCTYPE_QP);

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
                            LoadQP();
                        }
                    }
                }

            }
            catch
            {
                throw;
            }
        }

        private int GetWeek(DateTime date)
        {
            try
            {
                var currentCulture = CultureInfo.CurrentCulture;
                var weekNo = currentCulture.Calendar.GetWeekOfYear(
                date,
                currentCulture.DateTimeFormat.CalendarWeekRule,
                currentCulture.DateTimeFormat.FirstDayOfWeek);

                return weekNo;
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
                /*
                //Si no existe Quotation Proposal asociado al PO puede editar practicamente todo y agregar nuevas líneas, 
                //en caso contrario sólo puede editar cantidades y comentarios y no se pueden agregar líneas nuevas
                var qp = GlobalSetting.SupplyDocsService.GetDoc($"{Constants.SUPPLY_DOCTYPE_QP}{_docHeadPO.IdDoc}");

                if (qp != null)
                    _existQP = true;
                */
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

                DocHead purchaseOrder = new DocHead()
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
                    Lines = sortedLines
                };

                DocHead createdDoc = GlobalSetting.SupplyDocsService.UpdateDoc(purchaseOrder);

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
