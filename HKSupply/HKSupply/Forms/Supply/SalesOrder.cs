using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using HKSupply.General;
using HKSupply.Helpers;
using HKSupply.Models;
using HKSupply.Models.Supply;
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
using DevExpress.XtraEditors.Repository;

namespace HKSupply.Forms.Supply
{
    public partial class SalesOrder : RibbonFormBase
    {
        #region Enums
        enum eGridSummaries
        {
            totalQuantityMt,
            totalQuantityHw
        }
        #endregion

        #region Private Members

        Font _labelDefaultFontBold = new Font("SourceSansProRegular", 8, FontStyle.Bold);
        Font _labelDefaultFont = new Font("SourceSansProRegular", 8, FontStyle.Regular);

        List<Customer> _customersList;

        List<SupplyStatus> _supplyStatusList;

        BindingList<DocLine> _docLinesList;
        DocHead _docHeadSO;
        DocHead _docHeadAssociatedPO;

        int _totalQuantityMt;
        int _totalQuantityHw;

        /// <summary>
        /// para guardar líneas que originalmente estaban abiertas, pero al modificar la cantidad quizás se han cancelado/cerrado, pero se pueden modificar
        /// </summary>
        List<string> _auxIdItemList = new List<string>(); 

        #endregion

        #region Constructor
        public SalesOrder()
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

                SetObjectsReadOnly();
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

        public override void bbiExportCsv_ItemClick(object sender, ItemClickEventArgs e)
        {

            if(gridViewLines.DataRowCount == 0)
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
                    gridViewLines.ExportToXlsx(ExportExcelFile);

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

        public override void bbiEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            base.bbiEdit_ItemClick(sender, e);

            try
            {
                if (_docLinesList?.Count > 0)
                {
                    if (_docHeadSO.IdSupplyStatus == Constants.SUPPLY_STATUS_OPEN)
                    {
                        ConfigureRibbonActionsEditing();
                    }
                    else
                    {
                        MessageBox.Show("Only Open Docs. can be edited");
                        RestoreInitState();
                    }
                    
                }
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

                DialogResult result = MessageBox.Show(GlobalSetting.ResManager.GetString("SaveChanges"), "", MessageBoxButtons.YesNo);

                if (result != DialogResult.Yes)
                    return;

                Cursor = Cursors.WaitCursor;

                if (CurrentState == ActionsStates.Edit)
                {
                    res = UpdateSO();

                    if (res == true)
                    {
                        MessageBox.Show(GlobalSetting.ResManager.GetString("SaveSuccessfully"));
                        ActionsAfterCU();
                    }
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

        #endregion

        #region Form Events

        private void SalesOrder_Load(object sender, EventArgs e)
        {

        }

        private void SbSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSONumber.Text))
                {
                    XtraMessageBox.Show("Type Sales Order number", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    SearchSO();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtSONumber_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                ResetSO();
                ResetForm();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtSONumber_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
               if(e.KeyCode == Keys.Enter && txtSONumber.EditValue !=null)
                {
                    SearchSO();
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
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    _totalQuantityHw = 0;
                    _totalQuantityMt = 0;
                }

                // Calculation 
                if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DocLine row = view.GetRow(e.RowHandle) as DocLine;

                    switch (summaryID)
                    {

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

        private void GridViewLines_ShowingEditor(object sender, CancelEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                DocLine row = view.GetRow(view.FocusedRowHandle) as DocLine;

                switch (view.FocusedColumn.FieldName)
                {
                    case nameof(DocLine.Quantity):
                        if (row.IdSupplyStatus != Constants.SUPPLY_STATUS_OPEN && _auxIdItemList.Contains(row.IdItemBcn) == false) //si está cerrada porque el usuario ha modificado la cantidad si puede modificarla
                        {
                            XtraMessageBox.Show("Only Open lines can be edited", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            e.Cancel = true;
                        }
                        
                        break;
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GridViewLines_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                DocLine row = view.GetRow(e.RowHandle) as DocLine;

                if (row.Quantity == 0)
                {
                    if (_auxIdItemList.Contains(row.IdItemBcn) == false) _auxIdItemList.Add(row.IdItemBcn);
                    row.IdSupplyStatus = Constants.SUPPLY_STATUS_CANCEL;
                }
                else if (row.Quantity == row.DeliveredQuantity)
                {
                    if (_auxIdItemList.Contains(row.IdItemBcn) == false) _auxIdItemList.Add(row.IdItemBcn);
                    row.IdSupplyStatus = Constants.SUPPLY_STATUS_CLOSE;
                }
                else
                {
                    row.IdSupplyStatus = Constants.SUPPLY_STATUS_OPEN;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GridViewLines_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                DocLine line = view.GetRow(e.RowHandle) as DocLine;

                if (line == null)
                    return;

                switch(e.Column.FieldName)
                {
                    case nameof(DocLine.IdSupplyStatus):

                        switch (line.IdSupplyStatus)
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

                        break;
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
                txtSONumber.EditValue = idDoc;
                SearchSO();
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
                lblSONumber.Font = _labelDefaultFontBold;
                lblDate.Font = _labelDefaultFontBold;
                lblWeek.Font = _labelDefaultFontBold;
                lblPODate.Font = _labelDefaultFontBold;
                lblSOCreationDate.Font = _labelDefaultFontBold;
                lblCustomer.Font = _labelDefaultFontBold;
                lblPODateWeek.Font = _labelDefaultFont;
                lblSODateWeek.Font = _labelDefaultFont;
                txtPONumber.Font = _labelDefaultFontBold;
                txtSONumber.Font = _labelDefaultFontBold;
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
                lblSONumber.Text = "SO Number";
                lblDate.Text = "Date";
                lblWeek.Text = "Week";
                lblPODate.Text = "PO DATE";
                lblSOCreationDate.Text = "SO CREATION";
                lblCustomer.Text = "CUSTOMER";
                lblPODateWeek.Text = string.Empty;
                lblSODateWeek.Text = string.Empty;
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
                lblSODateWeek.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                txtPONumber.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                txtSONumber.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;

                //Terms Tab
                lblCompany.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
                lblAddress.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
                lblContact.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
 
                /********* BackColor **********/
                txtPONumber.Properties.Appearance.BackColor = Color.LawnGreen;
                txtPONumber.Properties.Appearance.BackColor2 = Color.LawnGreen;

                txtSONumber.Properties.Appearance.BackColor = Color.LightGoldenrodYellow;
                txtSONumber.Properties.Appearance.BackColor2 = Color.LightGoldenrodYellow;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Pone como readonly los objetos relaciones con la cabecera del documento. Es una pantalla de visualización principalmente, no hay edición en ella.
        /// </summary>
        /// <remarks>Son pocos, los pongo a mano en lugar de hacer en bucle.</remarks>
        private void SetObjectsReadOnly()
        {
            try
            {
                /********* Headers **********/
                txtPONumber.ReadOnly = true;
                dateEditPODate.ReadOnly = true;
                dateEditSODate.ReadOnly = true;
                slueCustomer.ReadOnly = true;
                memoEditRemarks.ReadOnly = true;
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

        private void SetUpEvents()
        {
            try
            {
                sbSearch.Click += SbSearch_Click;
                txtSONumber.EditValueChanged += TxtSONumber_EditValueChanged;
                txtSONumber.KeyDown += TxtSONumber_KeyDown;
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
                GridColumn colIdItemBcn = new GridColumn() { Caption = "Item Code", Visible = true, FieldName = nameof(DocLine.IdItemBcn), Width = 200 };
                GridColumn colDescription = new GridColumn() { Caption = "Description", Visible = true, FieldName = nameof(DocLine.ItemDesc), Width = 350 };
                GridColumn colIdItemGroup = new GridColumn() { Caption = "Type", Visible = true, FieldName = nameof(DocLine.IdItemGroup), Width = 100 };
                GridColumn colQuantityOriginal = new GridColumn() { Caption = "Quantity Original", Visible = true, FieldName = nameof(DocLine.QuantityOriginal), Width = 120 };
                GridColumn colQuantity = new GridColumn() { Caption = "Quantity", Visible = true, FieldName = nameof(DocLine.Quantity), Width = 85 };
                GridColumn colDeliveredQuantity = new GridColumn() { Caption = "Delivered Quantity", Visible = true, FieldName = nameof(DocLine.DeliveredQuantity), Width = 120 };
                GridColumn colUnit = new GridColumn() { Caption = "Unit", Visible = true, FieldName = nameof(DocLine.ItemUnit), Width = 85 };
                GridColumn colUnitPrice = new GridColumn() { Caption = "Unit Price", Visible = true, FieldName = nameof(DocLine.UnitPrice), Width = 85 };
                GridColumn colTotalAmount = new GridColumn() { Caption = "TotalAmount", Visible = true, FieldName = nameof(DocLine.TotalAmount), Width = 120 };
                GridColumn colIdIdSupplyStatus = new GridColumn() { Caption = "Status", Visible = true, FieldName = nameof(DocLine.IdSupplyStatus), Width = 75 };
                GridColumn colRemarks = new GridColumn() { Caption = "Remarks", Visible = true, FieldName = nameof(DocLine.Remarks), Width = 350 };

                //Display Format
                colUnitPrice.DisplayFormat.FormatType = FormatType.Numeric;
                colUnitPrice.DisplayFormat.FormatString = "n2";

                colTotalAmount.DisplayFormat.FormatType = FormatType.Numeric;
                colTotalAmount.DisplayFormat.FormatString = "n2";

                colQuantityOriginal.DisplayFormat.FormatType = FormatType.Numeric;
                colQuantityOriginal.DisplayFormat.FormatString = "n0";

                colQuantity.DisplayFormat.FormatType = FormatType.Numeric;
                colQuantity.DisplayFormat.FormatString = "n0";

                colDeliveredQuantity.DisplayFormat.FormatType = FormatType.Numeric;
                colDeliveredQuantity.DisplayFormat.FormatString = "n0";

                //Edit Repositories

                //Obtenemos la longitud máxima del campo que hemos indicado en el modelo de EF
                var remarksMaxLength = (new DocLine()).GetAttributeFrom<System.ComponentModel.DataAnnotations.StringLengthAttribute>(nameof(DocLine.Remarks)).MaximumLength;
                RepositoryItemTextEdit riTxtRemarks = new RepositoryItemTextEdit()
                {
                    MaxLength = remarksMaxLength
                };
                colRemarks.ColumnEdit = riTxtRemarks;

                RepositoryItemTextEdit ritxtInt = new RepositoryItemTextEdit();
                ritxtInt.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                ritxtInt.Mask.EditMask = "N";
                ritxtInt.AllowNullInput = DefaultBoolean.False;

                colQuantity.ColumnEdit = ritxtInt;

                RepositoryItemSearchLookUpEdit riSupplyStatus = new RepositoryItemSearchLookUpEdit()
                {
                    DataSource = _supplyStatusList,
                    ValueMember = nameof(SupplyStatus.IdSupplyStatus),
                    DisplayMember = nameof(SupplyStatus.Description),
                    ShowClearButton = false,
                    NullText = string.Empty,
                };
                colIdIdSupplyStatus.ColumnEdit = riSupplyStatus;

                //Summaries
                gridViewLines.OptionsView.ShowFooter = true;

                colTotalAmount.Summary.Add(SummaryItemType.Sum, nameof(DocLine.TotalAmount), "{0:n2}");

                colQuantity.Summary.AddRange(new GridSummaryItem[] {
                    new GridColumnSummaryItem(SummaryItemType.Custom, nameof(DocLine.Quantity), "{0} Gr", eGridSummaries.totalQuantityMt),
                    new GridColumnSummaryItem(SummaryItemType.Custom, nameof(DocLine.Quantity), "{0} PC", eGridSummaries.totalQuantityHw) });

                //Add columns to grid root view
                gridViewLines.Columns.Add(colIdItemBcn);
                gridViewLines.Columns.Add(colDescription);
                gridViewLines.Columns.Add(colIdItemGroup);
                gridViewLines.Columns.Add(colQuantityOriginal);
                gridViewLines.Columns.Add(colQuantity);
                gridViewLines.Columns.Add(colDeliveredQuantity);
                gridViewLines.Columns.Add(colUnit);
                gridViewLines.Columns.Add(colUnitPrice);
                gridViewLines.Columns.Add(colTotalAmount);
                gridViewLines.Columns.Add(colIdIdSupplyStatus);
                gridViewLines.Columns.Add(colRemarks);

                //Events
                gridViewLines.CustomSummaryCalculate += GridViewLines_CustomSummaryCalculate;
                gridViewLines.ShowingEditor += GridViewLines_ShowingEditor;
                gridViewLines.CellValueChanged += GridViewLines_CellValueChanged;
                gridViewLines.RowCellStyle += GridViewLines_RowCellStyle;

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
                _supplyStatusList = GlobalSetting.SupplyDocsService.GetSupplyStatus();
            }
            catch
            {
                throw;
            }
        }

        private void ResetSO()
        {
            try
            {
                _docHeadAssociatedPO = null;
                _docHeadSO = null;
                _docLinesList = null;
                xgrdLines.DataSource = null;

                _auxIdItemList = new List<string>();
            }
            catch
            {
                throw;
            }
        }

        private void LoadSO()
        {
            try
            {
                var customer = _customersList.Where(a => a.IdCustomer.Equals(_docHeadSO.IdCustomer)).FirstOrDefault();

                _docHeadAssociatedPO = GlobalSetting.SupplyDocsService.GetDoc(_docHeadSO.IdDoc.Replace(Constants.SUPPLY_DOCTYPE_SO, string.Empty));

                if (_docHeadAssociatedPO == null)
                    throw new Exception("Associated PO not found");

                //***** Header *****/
                slueCustomer.EditValue = _docHeadSO.IdCustomer;

                dateEditPODate.EditValue = _docHeadAssociatedPO.DocDate;
                dateEditSODate.EditValue = _docHeadSO.CreationDate;

                lblPODateWeek.Text = dateEditPODate.DateTime.GetWeek().ToString();
                lblSODateWeek.Text = dateEditSODate.DateTime.GetWeek().ToString();
                txtPONumber.Text = _docHeadAssociatedPO.IdDoc;
                txtSONumber.Text = _docHeadSO.IdDoc;
                memoEditRemarks.Text = _docHeadSO.Remarks;

                //***** Grid *****/
                _docLinesList = new BindingList<DocLine>(_docHeadSO.Lines);

                xgrdLines.DataSource = null;
                xgrdLines.DataSource = _docLinesList;

                //***** Terms Tab *****/
                lblTxtCompany.Text = customer.CustomerName;
                lblTxtAddress.Text = customer.ShippingAddress;
                lblTxtContact.Text = $"{customer.ContactName} ({customer.ContactPhone})";

            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Aux

        private void ResetForm()
        {
            try
            {
                /********* Head *********/
                txtPONumber.EditValue = null;
                dateEditPODate.EditValue = null;
                dateEditSODate.EditValue = null;
                lblPODateWeek.Text = string.Empty;
                lblSODateWeek.Text = string.Empty;
                slueCustomer.EditValue = null;
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
        }

        private void SearchSO()
        {
            try
            {
                ResetSO();
                ResetForm();

                string soNumber = txtSONumber.Text;

                _docHeadSO = GlobalSetting.SupplyDocsService.GetDoc(soNumber);

                if (_docHeadSO == null)
                {
                    XtraMessageBox.Show("No Data Found", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if(_docHeadSO.IdSupplyDocType != Constants.SUPPLY_DOCTYPE_SO)
                {
                    XtraMessageBox.Show("Document is not a Sales Order", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    LoadSO();
                }


                //string customer = slueCustomer.EditValue as string;
                //DateTime soCreateDate = dateEditSOCreationDate.DateTime;

                //var docs = GlobalSetting.SupplyDocsService.GetDocs(idSupplier: null, idCustomer: customer, docDate: soCreateDate, IdSupplyDocType: Constants.SUPPLY_DOCTYPE_SO, idSupplyStatus: null);

                //if (docs.Count == 0)
                //{
                //    XtraMessageBox.Show("No Data Found", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
                ////else if(docs.Count == 1)
                ////{

                ////}
                //else
                //{
                //    using (DialogForms.SelectDocs form = new DialogForms.SelectDocs())
                //    {
                //        form.InitData(docs);
                //        if (form.ShowDialog() == DialogResult.OK)
                //        {
                //            _docHeadSO = form.SelectedDoc;
                //            LoadSO();
                //        }
                //    }
                //}

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
                txtSONumber.ReadOnly = true;
                memoEditRemarks.ReadOnly = false;

                //Sólo es editable la columna de comentarios
                //Allow edit all columns
                gridViewLines.OptionsBehavior.Editable = true;

                foreach (GridColumn col in gridViewLines.Columns)
                {
                    if (col.FieldName != nameof(DocLine.Quantity) && col.FieldName != nameof(DocLine.Remarks))
                        col.OptionsColumn.AllowEdit = false;
                }

            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region CRUD

        private bool UpdateSO()
        {
            try
            {
                var lines = _docLinesList.ToList();

                DocHead salesOrder = new DocHead()
                {
                    IdDoc = _docHeadSO.IdDoc,
                    IdSupplyDocType = _docHeadSO.IdSupplyDocType,
                    CreationDate = _docHeadSO.CreationDate,
                    DeliveryDate = _docHeadSO.DeliveryDate,
                    DocDate = _docHeadSO.DocDate,
                    IdSupplyStatus = _docHeadSO.IdSupplyStatus,
                    IdSupplier = _docHeadSO.IdSupplier,
                    IdCustomer = _docHeadSO.IdCustomer,
                    IdDeliveryTerm = _docHeadSO.IdDeliveryTerm,
                    IdPaymentTerms = _docHeadSO.IdPaymentTerms,
                    IdCurrency = _docHeadSO.IdCurrency,
                    Remarks = memoEditRemarks.EditValue as string,
                    Lines = lines
                };

                DocHead updatedDoc = GlobalSetting.SupplyDocsService.UpdateDoc(salesOrder);
                _docHeadSO = updatedDoc;

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
                txtSONumber.ReadOnly = false;
                memoEditRemarks.ReadOnly = true;

                //Reload Sales Order
                SearchSO();

                gridViewLines.OptionsBehavior.Editable = false;

                //Restore de ribbon to initial states
                RestoreInitState();
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
