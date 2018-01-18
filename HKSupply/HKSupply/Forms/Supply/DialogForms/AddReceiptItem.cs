using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using HKSupply.General;
using HKSupply.Helpers;
using HKSupply.Models.Supply;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HKSupply.Forms.Supply.DialogForms
{
    public partial class AddReceiptItem : Form
    {

        #region Constants
        private const string COL_SELECTED = "Selected";
        private const string VIEW_COLUMN = "View";
        private const string COL_PENDING_QTY = "PENDING_QTY";
        private const string COL_QTY_IN_OTHER_PK = "QTY_IN_OTHERS_PK";
        #endregion

        #region Private Members

        string _idSupplier;
        string _idDoc;

        List<SupplyStatus> _supplyStatusList;

        BindingList<DocHead> _docPoSelectionList;
        BindingList<DocLine> _docLinesPoSelectionList;

        List<DocLine> _selectedDocLinesList = new List<DocLine>();
        #endregion

        #region Public properties
        public List<DocLine> SelectedDocLinesList { get { return _selectedDocLinesList; } }
        #endregion

        #region Constructor
        public AddReceiptItem()
        {
            InitializeComponent();

            try
            {
                Cursor = Cursors.WaitCursor;

                LoadAuxList();
                SetFormStyle();
                SetUpTabs();
                SetUpEvents();
                SetUpGrdPoSelection();
                SetUpGrdLinesPoSelection();
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
        private void AddReceiptItem_Load(object sender, EventArgs e)
        {

        }

        #region Grid Events
        private void GridViewPoSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
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

                    DocHead doc = view.GetRow(view.FocusedRowHandle) as DocHead;
                    if (doc != null)
                    {
                        _docLinesPoSelectionList = new BindingList<DocLine>(doc.Lines);
                        //cambiamos la cantidad Dummy (aquí la cantida da incluir en el packing) por la cantidad pendiente para facilitar al usuario
                        //The ToList is needed in order to evaluate the select immediately
                        _docLinesPoSelectionList.Select(a => { a.DummyQuantity = a.Quantity - a.DeliveredQuantity; return a; }).ToList();

                        xgrdLinesPoSelection.DataSource = null;
                        xgrdLinesPoSelection.DataSource = _docLinesPoSelectionList;
                        MarkSoSelection();

                    }
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GridViewLinesPoSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                DocLine line = view.GetRow(view.FocusedRowHandle) as DocLine;

                if (line == null)
                    return;

                if (e.Action == CollectionChangeAction.Add)
                {
                    if (line.IdSupplyStatus == Constants.SUPPLY_STATUS_OPEN)
                    {
                        CopyToSelectedList(line);
                        gridViewLinesPoSelection.UpdateSummary();
                    }
                    else
                    {
                        XtraMessageBox.Show("This line is Close.");
                        view.BeginSelection();
                        gridViewLinesPoSelection.UnselectRow(view.FocusedRowHandle);
                        view.EndSelection();
                    }

                }
                else if (e.Action == CollectionChangeAction.Remove)
                {
                    line.Quantity = line.QuantityOriginal;
                    gridViewLinesPoSelection.RefreshData();
                    DeleteSelectedList(line);
                    gridViewLinesPoSelection.UpdateSummary();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #endregion

        #region Public Methods
        public void InitData(string idSupplier, string idDocPacking)
        {
            try
            {
                _idSupplier = idSupplier;
                _idDoc = idDocPacking;
                SearchSuppliersPOs();
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
                _supplyStatusList = GlobalSetting.SupplyDocsService.GetSupplyStatus();
            }
            catch
            {
                throw;
            }
        }

        private void ResetSupplierPOs()
        {
            try
            {
                _docPoSelectionList = null;
                _docLinesPoSelectionList = null;

            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Setup Form Objects
        private void SetFormStyle()
        {
            try
            {
                MaximizeBox = false;
                MinimizeBox = false;
                ShowInTaskbar = false;
                ShowIcon = false;
                FormBorderStyle = FormBorderStyle.FixedDialog;
                Text = "Select items";
            }
            catch
            {
                throw;
            }
        }

        private void SetUpTabs()
        {
            try
            {

                xtpPOSelection.AutoScroll = true;
                xtpPOSelection.AutoScrollMargin = new Size(20, 20);
                xtpPOSelection.AutoScrollMinSize = new Size(xtpPOSelection.Width, xtpPOSelection.Height);

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
                sbOk.Click += (s, e) => 
                {
                    DialogResult = DialogResult.OK;
                    Close();
                };

                sbCancel.Click += (s, e) => { Close(); };
            }
            catch
            {
                throw;
            }
        }

        private void SetUpGrdPoSelection()
        {
            try
            {
                //Activar que se alternen los colores de las filas del grid
                gridViewPoSelection.OptionsView.EnableAppearanceOddRow = true;
                gridViewPoSelection.OptionsView.EnableAppearanceEvenRow = true;

                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                gridViewPoSelection.OptionsView.ColumnAutoWidth = false;
                gridViewPoSelection.HorzScrollVisibility = ScrollVisibility.Auto;

                //Hacer todo el grid no editable
                gridViewPoSelection.OptionsBehavior.Editable = false;

                //Hide grouping panel
                gridViewPoSelection.OptionsView.ShowGroupPanel = false;

                //select with checbox
                gridViewPoSelection.OptionsSelection.MultiSelect = true;
                gridViewPoSelection.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
                gridViewPoSelection.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DefaultBoolean.False;

                //Disable master/detail for doc lines.
                gridViewPoSelection.OptionsDetail.EnableMasterViewMode = false;

                //Column Definition
                GridColumn colIdDoc = new GridColumn() { Caption = "SALES ORDER", Visible = true, FieldName = nameof(DocHead.IdDoc), Width = 150 };
                GridColumn colCreationDate = new GridColumn() { Caption = "CREATION ORDER", Visible = true, FieldName = nameof(DocHead.CreationDate), Width = 150 };
                GridColumn colDeliveryDate = new GridColumn() { Caption = "DELIVERY DATE", Visible = true, FieldName = nameof(DocHead.DeliveryDate), Width = 150 };
                GridColumn colIdCurrency = new GridColumn() { Caption = "CURRENCY", Visible = true, FieldName = nameof(DocHead.IdCurrency), Width = 150 };

                gridViewPoSelection.Columns.Add(colIdDoc);
                gridViewPoSelection.Columns.Add(colCreationDate);
                gridViewPoSelection.Columns.Add(colDeliveryDate);
                gridViewPoSelection.Columns.Add(colIdCurrency);

                //Events
                gridViewPoSelection.SelectionChanged += GridViewPoSelection_SelectionChanged;


            }
            catch
            {
                throw;
            }
        }

        private void SetUpGrdLinesPoSelection()
        {
            try
            {
                //Activar que se alternen los colores de las filas del grid
                gridViewLinesPoSelection.OptionsView.EnableAppearanceOddRow = true;
                gridViewLinesPoSelection.OptionsView.EnableAppearanceEvenRow = true;

                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                gridViewLinesPoSelection.OptionsView.ColumnAutoWidth = false;
                gridViewLinesPoSelection.HorzScrollVisibility = ScrollVisibility.Auto;

                //Hacer todo el grid no editable
                gridViewLinesPoSelection.OptionsBehavior.Editable = false;

                //Hide grouping panel
                gridViewLinesPoSelection.OptionsView.ShowGroupPanel = false;

                //select with checbox
                gridViewLinesPoSelection.OptionsSelection.MultiSelect = true;
                gridViewLinesPoSelection.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
                gridViewLinesPoSelection.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DefaultBoolean.False;

                //Column Definition
                GridColumn colIdItemBcn = new GridColumn() { Caption = "Item Code", Visible = true, FieldName = nameof(DocLine.IdItemBcn), Width = 130 };
                GridColumn colDescription = new GridColumn() { Caption = "Description", Visible = true, FieldName = nameof(DocLine.ItemDesc), Width = 350 };
                GridColumn colIdItemGroup = new GridColumn() { Caption = "Type", Visible = true, FieldName = nameof(DocLine.IdItemGroup), Width = 50 };
                GridColumn colQuantity = new GridColumn() { Caption = "Order Quantity", Visible = true, FieldName = nameof(DocLine.Quantity), Width = 110 };
                GridColumn colDeliveredQuantity = new GridColumn() { Caption = "Delivered Qty", Visible = true, FieldName = nameof(DocLine.DeliveredQuantity), Width = 110 };
                //GridColumn colDummyQuantity = new GridColumn() { Caption = "Packing Quantity", Visible = true, FieldName = nameof(DocLine.DummyQuantity), Width = 110 };
                GridColumn colUnit = new GridColumn() { Caption = "Unit", Visible = true, FieldName = nameof(DocLine.ItemUnit), Width = 70 };
                GridColumn colIdIdSupplyStatus = new GridColumn() { Caption = "Status", Visible = true, FieldName = nameof(DocLine.IdSupplyStatus), Width = 75 };
                //Unbound Column. Para calcular la cantidad pendiente
                //GridColumn colPendingQuantity = new GridColumn() { Caption = "Pending Qty", Visible = true, FieldName = COL_PENDING_QTY, Width = 110, UnboundType = UnboundColumnType.Integer };
                //GridColumn colQtyInOthersPackings = new GridColumn() { Caption = "Qty in others Packings", Visible = true, FieldName = COL_QTY_IN_OTHER_PK, Width = 150, UnboundType = UnboundColumnType.Decimal };

                colQuantity.DisplayFormat.FormatType = FormatType.Numeric;
                colQuantity.DisplayFormat.FormatString = "n3";

                colDeliveredQuantity.DisplayFormat.FormatType = FormatType.Numeric;
                colDeliveredQuantity.DisplayFormat.FormatString = "n3";

                //colPendingQuantity.DisplayFormat.FormatType = FormatType.Numeric;
                //colPendingQuantity.DisplayFormat.FormatString = "n3";

                //colDummyQuantity.DisplayFormat.FormatType = FormatType.Numeric;
                //colDummyQuantity.DisplayFormat.FormatString = "n3";

                //colQtyInOthersPackings.DisplayFormat.FormatType = FormatType.Numeric;
                //colQtyInOthersPackings.DisplayFormat.FormatString = "n3";

                //Edit Repositories
                //RepositoryItemTextEdit ritxt3Dec = new RepositoryItemTextEdit();
                //ritxt3Dec.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                //ritxt3Dec.Mask.EditMask = "F3";
                //ritxt3Dec.AllowNullInput = DefaultBoolean.True;

                //colDummyQuantity.ColumnEdit = ritxt3Dec;

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
                gridViewLinesPoSelection.OptionsView.ShowFooter = true;

                //colTotalAmount.Summary.Add(SummaryItemType.Sum, nameof(DocLine.TotalAmount), "{0:n4}");
                //colDummyQuantity.Summary.Add(SummaryItemType.Sum, nameof(DocLine.DummyQuantity), "{0:n3}");
                colQuantity.Summary.Add(SummaryItemType.Sum, nameof(DocLine.Quantity), "{0:n3}");
                colDeliveredQuantity.Summary.Add(SummaryItemType.Sum, nameof(DocLine.DeliveredQuantity), "{0:n3}");
                //colPendingQuantity.Summary.Add(SummaryItemType.Sum, COL_PENDING_QTY, "{0:n3}");
                //colQtyInOthersPackings.Summary.Add(SummaryItemType.Sum, COL_QTY_IN_OTHER_PK, "{0:n3}");

                //Add columns to grid root view
                gridViewLinesPoSelection.Columns.Add(colIdItemBcn);
                gridViewLinesPoSelection.Columns.Add(colDescription);
                gridViewLinesPoSelection.Columns.Add(colIdItemGroup);
                gridViewLinesPoSelection.Columns.Add(colQuantity);
                gridViewLinesPoSelection.Columns.Add(colDeliveredQuantity);
                //gridViewLinesPoSelection.Columns.Add(colPendingQuantity);
                //gridViewLinesPoSelection.Columns.Add(colDummyQuantity);
                gridViewLinesPoSelection.Columns.Add(colUnit);
                gridViewLinesPoSelection.Columns.Add(colIdIdSupplyStatus);
                //gridViewLinesPoSelection.Columns.Add(colQtyInOthersPackings);

                //Events
                gridViewLinesPoSelection.SelectionChanged += GridViewLinesPoSelection_SelectionChanged;
                //gridViewLinesPoSelection.CellValueChanged += GridViewLinesPoSelection_CellValueChanged;
                //gridViewLinesPoSelection.ValidatingEditor += GridViewLinesPoSelection_ValidatingEditor;
                //gridViewLinesPoSelection.ShowingEditor += GridViewLinesPoSelection_ShowingEditor;
                //gridViewLinesPoSelection.CustomUnboundColumnData += GridViewLinesPoSelection_CustomUnboundColumnData;

                //tooltip
                //xgrdLinesPoSelection.ToolTipController = toolTipController1;

                gridViewLinesPoSelection.OptionsLayout.StoreAppearance = true;
                gridViewLinesPoSelection.OptionsLayout.StoreAllOptions = true;

            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Aux
        private void MarkSoSelection()
        {
            try
            {
                gridViewLinesPoSelection.BeginSelection();

                for (int i = 0; i < gridViewLinesPoSelection.DataRowCount; i++)
                {
                    DocLine rowSoSelection = gridViewLinesPoSelection.GetRow(i) as DocLine;

                    var rowSelected = _selectedDocLinesList?.Where(a => a.IdItemBcn.Equals(rowSoSelection.IdItemBcn) && a.IdDocRelated.Equals(rowSoSelection.IdDoc)).FirstOrDefault();

                    if (rowSelected != null)
                    {
                        gridViewLinesPoSelection.SelectRow(i);
                        //rowSoSelection.DummyQuantity = rowSelected.Quantity; //Por si se ha modificado la cantidad, está guardada en la línea de delivered good
                        gridViewLinesPoSelection.RefreshRow(i);
                    }

                }

                gridViewLinesPoSelection.EndSelection();
            }
            catch
            {
                throw;
            }
        }

        private void CopyToSelectedList(DocLine line)
        {
            try
            {
                if (line.Quantity == 0)
                    return;

                //Buscamos si ya existe
                var exist = _selectedDocLinesList.Where(a => a.IdItemBcn.Equals(line.IdItemBcn) && a.IdDocRelated.Equals(line.IdDoc)).FirstOrDefault();

                if (exist == null)
                {
                    DocLine tmp = line.DeepCopyByExpressionTree();
                    tmp.IdDoc = _idDoc;
                    tmp.IdDocRelated = line.IdDoc;
                    tmp.DeliveredQuantity = 0;
                    tmp.QuantityOriginal = line.Quantity;
                    tmp.Quantity = line.DummyQuantity;
                    tmp.Remarks = null;
                    _selectedDocLinesList.Add(tmp);
                }
                //else
                //{
                //    //Actualizamos la cantidad que es el única campo que se puede editar
                //    exist.Quantity = line.DummyQuantity;
                //    gridViewLinesDeliveredGoods.RefreshData();
                //}
            }
            catch
            {
                throw;
            }
        }

        private void DeleteSelectedList(DocLine line)
        {
            try
            {
                var selectedLine = _selectedDocLinesList.Where(a => a.IdItemBcn.Equals(line.IdItemBcn) && a.IdDocRelated.Equals(line.IdDoc)).FirstOrDefault();
                if (selectedLine != null)
                    _selectedDocLinesList.Remove(selectedLine);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region CRUD
        private void SearchSuppliersPOs()
        {
            try
            {
                ResetSupplierPOs();

                var poDocsSupplier = GlobalSetting.SupplyDocsService.GetDocs(
                    idSupplier: _idSupplier,
                    idCustomer: null,
                    docDate: new DateTime(1, 1, 1),  //filtrar sin fecha
                    IdSupplyDocType: Constants.SUPPLY_DOCTYPE_PO,
                    idSupplyStatus: Constants.SUPPLY_STATUS_OPEN);

                if (poDocsSupplier.Count == 0)
                {
                    XtraMessageBox.Show("No Supplier's Purchase Orders open found", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                else
                {
                    _docPoSelectionList = new BindingList<DocHead>(poDocsSupplier);

                    xgrdPoSelection.DataSource = null;
                    xgrdPoSelection.DataSource = _docPoSelectionList;

                }
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
