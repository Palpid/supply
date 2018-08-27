using BOM.Classes;
using BOM.General;
using BOM.Helpers;
using BOM.Models;
using Dapper;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BOM.Forms
{
    public partial class BomManagement : Form
    {

        #region Enums
        public enum ActionsStates
        {
            Read,
            Edit,
        }
        #endregion

        #region Private Members
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        ActionsStates currentState;

        BindingList<Ocrd> _factoriesList;
        BindingList<BomBreakdown> _bomBreakdownList;
        BindingList<OitmExt> _itemsforBomList;
        BindingList<OitmExt> _itemsList;
        BindingList<Bom> _itemBoms;

        OitmExt _currentSelectedItem;

        #endregion

        #region Constructor
        public BomManagement()
        {
            InitializeComponent();

            try
            {
                Cursor = Cursors.WaitCursor;

                SetUpEvents();
                LoadAuxList();
                SetUpDockPanels();
                SetUpSlueFactories();
                SetUpGrids();
                LoadGridItems();
                ShowAddBomFactoryAndCopyBom(false);

            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        #endregion

        #region Form Events

        private void BomManagement_Load(object sender, EventArgs e)
        {
            try
            {
                currentState = ActionsStates.Read;
                ConfigureActionButtonbyState();
                //Test();
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAcciones_Click(object sender, EventArgs e)
        {
            try
            {
                if(currentState == ActionsStates.Read)
                {
                    if(_itemBoms == null || _itemBoms.Count() == 0)
                    {
                        XtraMessageBox.Show("No data selected");
                        return;
                    }
                    currentState = ActionsStates.Edit;
                    ConfigFormToEdit();
                }
                else if (currentState == ActionsStates.Edit)
                {
                    if (IsValidBom())
                    {
                        DialogResult result = XtraMessageBox.Show("Save Changes", "", MessageBoxButtons.YesNo);

                        if (result != DialogResult.Yes)
                            return;

                        if (SaveBom())
                        {
                            MessageBox.Show("Save Successfully");
                            InitForm();
                        }
                    }

                    
                }

                ConfigureActionButtonbyState();
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAddBomFactory_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentState != ActionsStates.Edit)
                    return;
                if (slueFactory.EditValue != null && _itemBoms != null && _itemBoms.Count() > 0)
                    AddFactoryBom();
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCopyBom_Click(object sender, EventArgs e)
        {
            try
            {
                if (_itemBoms != null && _itemBoms.Count() > 0)
                    OpenSelectSuppliersForm2Copy();
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void GridViewItems_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                GridView view = sender as GridView;

                //Sólo agregamos si el usuario hace doble click en una fila con datos, ya que si se pulsa en el header o en un grupo el FocusedRowHandle devuelve la primera fila con datos
                GridHitInfo hitInfo = view.CalcHitInfo((e as MouseEventArgs).Location);
                if (hitInfo.InRowCell)
                {
                    if(view.GetRow(view.FocusedRowHandle) is OitmExt item)
                    {
                        _currentSelectedItem = item.DeepCopyByExpressionTree();
                        LoadItemGridBom(item);
                    }
                }

            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Grid Bom Events

        private void GrdItemBom_ViewRegistered(object sender, DevExpress.XtraGrid.ViewOperationEventArgs e)
        {
            try
            {
                switch (e.View.LevelName)
                {
                    case nameof(Bom.Lines):

                        GridView view = e.View as GridView;

                        //Ocultamos las columnas que no nos interesan
                        view.Columns[nameof(BomDetail.CodeBom)].Visible = false;
                        view.Columns[nameof(BomDetail.Breakdown)].Visible = false;
                        view.Columns[nameof(BomDetail.Item)].Visible = false;
                        view.Columns[nameof(BomDetail.BomBreakdown)].Visible = false;

                        //Columnas que no queremos que aparezan en el Column Chooser
                        view.Columns[nameof(BomDetail.CodeBom)].OptionsColumn.ShowInCustomizationForm = false;
                        view.Columns[nameof(BomDetail.Breakdown)].OptionsColumn.ShowInCustomizationForm = false;
                        view.Columns[nameof(BomDetail.Item)].OptionsColumn.ShowInCustomizationForm = false;
                        view.Columns[nameof(BomDetail.BomBreakdown)].OptionsColumn.ShowInCustomizationForm = false;

                        //Captions
                        view.Columns[nameof(BomDetail.Length)].Caption = "Length (mm)";
                        view.Columns[nameof(BomDetail.Width)].Caption = "Width (mm)";
                        view.Columns[nameof(BomDetail.Height)].Caption = "Height (mm)";
                        view.Columns[nameof(BomDetail.BomBreakdown)].Caption = "Breakdown";

                        //agregamos algunas columnas extras 
                        GridColumn colItemDescription = new GridColumn()
                        {
                            Caption = "Description",
                            Visible = true,
                            FieldName = $"{nameof(BomDetail.Item)}.{nameof(OitmExt.ItemName)}",
                            Width = 300
                        };

                        GridColumn colItemTipArt = new GridColumn()
                        {
                            Caption = "Item Type",
                            Visible = true,
                            FieldName = $"{nameof(BomDetail.Item)}.{nameof(OitmExt.TipArtDesc)}",
                            Width = 50
                        };

                        view.Columns.Add(colItemDescription);
                        view.Columns.Add(colItemTipArt);

                        //Formatos
                        view.Columns[nameof(BomDetail.Length)].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        view.Columns[nameof(BomDetail.Length)].DisplayFormat.FormatString = "n2";

                        view.Columns[nameof(BomDetail.Width)].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        view.Columns[nameof(BomDetail.Width)].DisplayFormat.FormatString = "n2";

                        view.Columns[nameof(BomDetail.Height)].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        view.Columns[nameof(BomDetail.Height)].DisplayFormat.FormatString = "n2";

                        view.Columns[nameof(BomDetail.Density)].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        view.Columns[nameof(BomDetail.Density)].DisplayFormat.FormatString = "n2";

                        view.Columns[nameof(BomDetail.NumberOfParts)].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        view.Columns[nameof(BomDetail.NumberOfParts)].DisplayFormat.FormatString = "n0";

                        view.Columns[nameof(BomDetail.Coefficient1)].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        view.Columns[nameof(BomDetail.Coefficient1)].DisplayFormat.FormatString = "n6";
                        view.Columns[nameof(BomDetail.Coefficient2)].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        view.Columns[nameof(BomDetail.Coefficient2)].DisplayFormat.FormatString = "n6";


                        view.Columns[nameof(BomDetail.Scrap)].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        view.Columns[nameof(BomDetail.Scrap)].DisplayFormat.FormatString = "n2";

                        view.Columns[nameof(BomDetail.Quantity)].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        view.Columns[nameof(BomDetail.Quantity)].DisplayFormat.FormatString = "n2";

                        //Orden de las columnas
                        int orderColRm = 0;
                        view.Columns[nameof(BomDetail.ItemCode)].VisibleIndex = orderColRm++;
                        view.Columns[$"{nameof(BomDetail.Item)}.{nameof(OitmExt.ItemName)}"].VisibleIndex = orderColRm++;
                        view.Columns[$"{nameof(BomDetail.Item)}.{nameof(OitmExt.TipArtDesc)}"].VisibleIndex = orderColRm++;
                        view.Columns[nameof(BomDetail.BomBreakdown)].VisibleIndex = orderColRm++;
                        view.Columns[nameof(BomDetail.Length)].VisibleIndex = orderColRm++;
                        view.Columns[nameof(BomDetail.Width)].VisibleIndex = orderColRm++;
                        view.Columns[nameof(BomDetail.Height)].VisibleIndex = orderColRm++;
                        view.Columns[nameof(BomDetail.Density)].VisibleIndex = orderColRm++;
                        view.Columns[nameof(BomDetail.NumberOfParts)].VisibleIndex = orderColRm++;
                        view.Columns[nameof(BomDetail.Coefficient1)].VisibleIndex = orderColRm++;
                        view.Columns[nameof(BomDetail.Coefficient2)].VisibleIndex = orderColRm++;
                        view.Columns[nameof(BomDetail.Scrap)].VisibleIndex = orderColRm++;
                        view.Columns[nameof(BomDetail.Quantity)].VisibleIndex = orderColRm++;

                        //EditRepositories
                        SetBomDetailEditRepositories(view);

                        //Events
                        view.ValidatingEditor += DetailBomView_ValidatingEditor;
                        view.CellValueChanged += DetailBomView_CellValueChanged;
                        view.ShowingEditor += View_ShowingEditor;

                        //Ajustamos las columnas
                        view.BestFitColumns();

                        //Si estamos en modo edición el grid tiene que ser editable cuando lo pinte
                        if (currentState == ActionsStates.Edit)
                            ConfigFormToEdit();

                        break;
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GrdItemBom_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                if (currentState != ActionsStates.Edit)
                    return;

                if (e.KeyCode == Keys.F4)
                {

                }
                else if (e.KeyCode == Keys.Enter)
                {
                    GridView activeView = grdItemBom.FocusedView as GridView;

                    switch (activeView.LevelName)
                    {
                        case nameof(Bom.Lines):
                            if (activeView.FocusedRowHandle == activeView.RowCount - 1)
                            {
                                e.SuppressKeyPress = true;
                                e.Handled = true;

                                BeginInvoke(new MethodInvoker(delegate
                                {
                                    if (activeView.ValidateEditor())
                                    {
                                        activeView.CloseEditor();

                                        BomDetail row = activeView.GetRow(activeView.FocusedRowHandle) as BomDetail;
                                        BaseView parent = activeView.ParentView;
                                        Bom rowParent = parent.GetRow(activeView.SourceRowHandle) as Bom;

                                        if (row.Quantity > 0 && string.IsNullOrEmpty(row.ItemCode) == false && string.IsNullOrEmpty(row.BomBreakdown) == false)
                                        {
                                            rowParent.Lines.Add(new BomDetail() { CodeBom = rowParent.Code });
                                        }
                                        activeView.RefreshData();
                                    }
                                }));

                            }
                            break;
                    }
                }

            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DetailBomView_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                BomDetail row = view.GetRow(view.FocusedRowHandle) as BomDetail;

                BaseView parent = view.ParentView;
                var rowParent = parent.GetRow(view.SourceRowHandle) as Bom;

                BomDetail exist = null;

                decimal qty = 0;

                switch (view.FocusedColumn.FieldName)
                {
                    case nameof(BomDetail.ItemCode):
                        //No se pueden repetir item/breakdown
                        string itemCode = e.Value.ToString();

                        exist = rowParent.Lines
                            .Where(a => (a.ItemCode ?? string.Empty).Equals(itemCode) && (a.BomBreakdown ?? string.Empty).Equals(row.BomBreakdown ?? string.Empty))
                            .FirstOrDefault();

                        if (exist == null)
                        {
                            var item = _itemsforBomList.Where(a => a.ItemCode.Equals(itemCode)).Single().DeepCopyByExpressionTree();
                            row.Item = item;

                            row.Length = null;
                            row.Width = null;
                            row.Height = null;
                            row.Density = null;
                            row.NumberOfParts = null;
                            row.Coefficient1 = null;
                            row.Coefficient2 = null;
                            row.Scrap = null;
                            row.Quantity = null;
                        }
                        else
                        {
                            e.Valid = false;
                        }

                        break;


                    case nameof(BomDetail.BomBreakdown):
                        //No se pueden repetir materiales/breakdown
                        var idBomBreakdown = e.Value.ToString();

                        exist = rowParent.Lines
                            .Where(a => (a.ItemCode ?? string.Empty).Equals(row.ItemCode) && (a.BomBreakdown ?? string.Empty).Equals(idBomBreakdown ?? string.Empty))
                            .FirstOrDefault();

                        if (exist != null)
                        {
                            e.Valid = false;
                        }

                        break;


                    case nameof(BomDetail.Quantity):
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

                    case nameof(BomDetail.Length):

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

                    case nameof(BomDetail.Width):

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

                    case nameof(BomDetail.Height):

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

                    case nameof(BomDetail.Density):

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

                    case nameof(BomDetail.NumberOfParts):

                        //Me hace cosas raras en el parse + validación y tengo que controlarlo a mano
                        int numParts;
                        if (int.TryParse(e.Value.ToString(), out numParts) == false)
                        {
                            e.Valid = false;
                        }
                        else if (numParts == 0)
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

                    case nameof(BomDetail.Coefficient1):

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

                    case nameof(BomDetail.Coefficient2):

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

                    case nameof(BomDetail.Scrap):

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
                _log.Error(ex.Message, ex);
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DetailBomView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                BaseView parent = view.ParentView;
                var rowParent = parent.GetRow(view.SourceRowHandle) as Bom;

                if (e.Column.FieldName == nameof(BomDetail.ItemCode))
                {
                    BomDetail row = view.GetRow(e.RowHandle) as BomDetail;
                    row.Coefficient1 = null;
                    row.Coefficient2 = null;
                    row.Density = null;
                    row.Height = null;
                    row.Length = null;
                    row.NumberOfParts = null;
                    row.Scrap = null;
                    row.Width = null;
                }

                view.BestFitColumns();
                rowParent.Edited = true;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void View_ShowingEditor(object sender, CancelEventArgs e)
        {
            try
            {

                GridView view = sender as GridView;
                BomDetail row = view.GetRow(view.FocusedRowHandle) as BomDetail;

                BaseView parent = view.ParentView;
                var rowParent = parent.GetRow(view.SourceRowHandle);

                switch (view.FocusedColumn.FieldName)
                {
                    case nameof(BomDetail.Coefficient1):
                    case nameof(BomDetail.Coefficient2):
                    case nameof(BomDetail.Density):
                    case nameof(BomDetail.Height):
                    case nameof(BomDetail.Length):
                    case nameof(BomDetail.NumberOfParts):
                    case nameof(BomDetail.Scrap):
                    case nameof(BomDetail.Width):

                        if (string.IsNullOrEmpty(row.ItemCode))
                        {
                            XtraMessageBox.Show("Select item first");
                            e.Cancel = true;
                        }
                        else
                        {

                            if (row.Item.ItmsGrpCod != Constants.TIPART_RAWMATERIAL_HK)
                            {
                                e.Cancel = true;
                            }
                        }

                        break;
                }


            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #endregion

        #region Private Methods

        #region Setup Form Objetcs

        private void SetUpEvents()
        {
            try
            {
                Load += BomManagement_Load;
                btnAcciones.Click += BtnAcciones_Click;
                btnAddBomFactory.Click += BtnAddBomFactory_Click;
                btnCopyBom.Click += BtnCopyBom_Click;
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
                dockPanelItems.Options.ShowCloseButton = false;
                dockPanelBom.Options.ShowCloseButton = false;
            }
            catch
            {
                throw;
            }
        }

        private void SetUpSlueFactories()
        {
            try
            {
                slueFactory.Properties.DataSource = _factoriesList;

                slueFactory.Properties.ValueMember = nameof(Ocrd.CardCode);
                slueFactory.Properties.DisplayMember = nameof(Ocrd.CardName);
                slueFactory.Properties.View.Columns.AddField(nameof(Ocrd.CardCode)).Visible = true;
                slueFactory.Properties.View.Columns.AddField(nameof(Ocrd.CardName)).Visible = true;
            }
            catch
            {
                throw;
            }
        }

        private void SetUpGrids()
        {
            try
            {
                SetUpGrdItems();
                SetUpGrdItemBom();
            }
            catch
            {
                throw;
            }
        }

        private void SetUpGrdItems()
        {
            try
            {
                //Ocultamos el nombre de las columnas agrupadas
                gridViewItems.GroupFormat = "[#image]{1} {2}";

                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                gridViewItems.OptionsView.ColumnAutoWidth = false;
                gridViewItems.HorzScrollVisibility = ScrollVisibility.Auto;

                //Todo el Grid no editable
                gridViewItems.OptionsBehavior.Editable = false;

                //Columns definition
                GridColumn colItemCode = new GridColumn() { Caption = "ItemCode", Visible = true, FieldName = nameof(OitmExt.ItemCode), Width = 200 };
                GridColumn colModelName = new GridColumn() { Caption = "ItemCode", Visible = true, FieldName = $"{nameof(OitmExt.Model)}.{nameof(Model.Name)}", Width = 200 };

                //Add columns to grid root view
                gridViewItems.Columns.Add(colItemCode);
                gridViewItems.Columns.Add(colModelName);

                //Grouping
                gridViewItems.OptionsView.ShowGroupPanel = false;
                gridViewItems.Columns[$"{nameof(OitmExt.Model)}.{nameof(Model.Name)}"].GroupIndex = 0;

                gridViewItems.DoubleClick += GridViewItems_DoubleClick;
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
                GridColumn colItemCode = new GridColumn() { Caption = "Item Code", Visible = true, FieldName = nameof(Bom.ItemCode), Width = 200 };
                GridColumn colFactory = new GridColumn() { Caption = " ", Visible = true, FieldName = nameof(Bom.Factory), Width = 100 };

                //Add columns to grid root view
                gridViewItemBom.Columns.Add(colItemCode);
                gridViewItemBom.Columns.Add(colFactory);

                //Events
                grdItemBom.ViewRegistered += GrdItemBom_ViewRegistered;
                grdItemBom.ProcessGridKey += GrdItemBom_ProcessGridKey;
                //gridViewItemBom.PopupMenuShowing += GridViewItemBom_PopupMenuShowing;

                //Hide group panel
                gridViewItemBom.OptionsView.ShowGroupPanel = false;

                gridViewItemBom.Columns[nameof(Bom.ItemCode)].GroupIndex = 0;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region  CRUD

        private void LoadAuxList()
        {
            try
            {
                _itemsList = new BindingList<OitmExt>(GlobalSetting.OitmService.GetItems());
                _factoriesList = new BindingList<Ocrd>(GlobalSetting.OcrdService.GetFactories());
                _bomBreakdownList = new BindingList<BomBreakdown>(GlobalSetting.BomService.GetBromBreakdown());
                _itemsforBomList = new BindingList<OitmExt>(GlobalSetting.OitmService.GetPossibleItemsForBom());
            }
            catch
            {
                throw;
            }
        }


        private bool SaveBom()
        {
            try
            {
                var boms = _itemBoms.Where(a => a.Edited == true).ToList();
                GlobalSetting.BomService.EditBom(boms);
                return true;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Loads

        private void LoadGridItems()
        {
            try
            {
                grdItems.DataSource = null;
                grdItems.DataSource = _itemsList;
            }
            catch
            {
                throw;
            }
        }

        private void LoadItemGridBom(Oitm item)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                _itemBoms = new BindingList<Bom>(GlobalSetting.BomService.GetItemBom(item.ItemCode));

                grdItemBom.DataSource = null;
                grdItemBom.DataSource = _itemBoms;

                ExpandBomDefaultFactory();

                if (_itemBoms.Count == 0)
                    XtraMessageBox.Show("SKU without BOM ");
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

        #region Aux Methods

        private void ConfigureActionButtonbyState()
        {
            try
            {
                if (currentState == ActionsStates.Read)
                    btnAcciones.Text = "Edit";
                else if(currentState == ActionsStates.Edit)
                    btnAcciones.Text = "Save";
            }
            catch
            {
                throw;
            }
        }

        private void InitForm()
        {
            try
            {
                currentState = ActionsStates.Read;
                LoadItemGridBom(_currentSelectedItem);

            }
            catch
            {
                throw;
            }
        }

        #region Edit Aux Methods

        private void SetBomDetailEditRepositories(GridView view)
        {
            try
            {
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
                    NullText = "",
                };

                RepositoryItemSearchLookUpEdit riItems = new RepositoryItemSearchLookUpEdit()
                {
                    DataSource = _itemsforBomList,
                    ValueMember = nameof(OitmExt.ItemCode),
                    DisplayMember = nameof(OitmExt.ItemCode),
                    ShowClearButton = false,
                    NullText = "Select Item",
                };
                riItems.View.Columns.AddField(nameof(OitmExt.ItemCode)).Visible = true;
                riItems.View.Columns.AddField(nameof(OitmExt.ItemName)).Visible = true;
                riItems.View.Columns.AddField(nameof(OitmExt.TipArtDesc)).Visible = true;

                view.Columns[nameof(BomDetail.BomBreakdown)].ColumnEdit = riBomBreakdown;
                view.Columns[nameof(BomDetail.Length)].ColumnEdit = ritxt2Dec;
                view.Columns[nameof(BomDetail.Width)].ColumnEdit = ritxt2Dec;
                view.Columns[nameof(BomDetail.Height)].ColumnEdit = ritxt2Dec;
                view.Columns[nameof(BomDetail.Density)].ColumnEdit = ritxt2Dec;
                view.Columns[nameof(BomDetail.Coefficient1)].ColumnEdit = ritxt6Dec;
                view.Columns[nameof(BomDetail.Coefficient2)].ColumnEdit = ritxt6Dec;
                view.Columns[nameof(BomDetail.Scrap)].ColumnEdit = ritxt2Dec;
                view.Columns[nameof(BomDetail.Quantity)].ColumnEdit = ritxt2Dec;
                view.Columns[nameof(BomDetail.NumberOfParts)].ColumnEdit = ritxtInt;

                view.Columns[nameof(BomDetail.ItemCode)].ColumnEdit = riItems;
            }
            catch
            {
                throw;
            }
        }

        private void ConfigFormToEdit()
        {
            try
            {
                ShowAddBomFactoryAndCopyBom(true);

                foreach (GridView view in grdItemBom.ViewCollection)
                {
                    switch (view.LevelName)
                    {
                        case nameof(Bom.Lines):

                            view.OptionsBehavior.Editable = true;

                            view.Columns[$"{nameof(BomDetail.Item)}.{nameof(OitmExt.ItemName)}"].OptionsColumn.AllowEdit = false;
                            view.Columns[$"{nameof(BomDetail.Item)}.{nameof(OitmExt.TipArtDesc)}"].OptionsColumn.AllowEdit = false;

                            break;
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        private void AddFactoryBom()
        {
            try
            {
                string idFactory = slueFactory.EditValue as string;
                var exist = _itemBoms.Where(a => a.Factory.Equals(idFactory)).FirstOrDefault();

                if (exist == null)
                {
                    Bom itemBom = new Bom()
                    {
                        Code = 0,
                        Factory = idFactory,
                        ItemCode = _currentSelectedItem.ItemCode,
                    };
                    itemBom.Lines.Add(new BomDetail() { CodeBom = 0 });

                    _itemBoms.Add(itemBom);

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

        private void OpenSelectSuppliersForm2Copy()
        {
            try
            {
                List<string> factories = _itemBoms.Select(a => a.Factory).ToList();
                List<string> selectedFactoriesSource = new List<string>();
                List<string> selectedFactoriesDestination = new List<string>();

                using (DialogForms.SelectFactories form = new DialogForms.SelectFactories())
                {
                    form.InitData(factories);

                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        selectedFactoriesSource = form.SelectedFactoriesSource;
                        selectedFactoriesDestination = form.SelectedFactoriesDestination;

                        CopySupplierBom2SupplierBom(selectedFactoriesSource.Single(), selectedFactoriesDestination);
                       
                    }

                }
            }
            catch
            {
                throw;
            }
        }

        private void CopySupplierBom2SupplierBom(string selectedFactorySource, List<string> selectedFactoriesDestination)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                var bomSource = _itemBoms.Where(a => a.Factory.Equals(selectedFactorySource)).Single();

                foreach (var factoryDestination in selectedFactoriesDestination)
                {
                    var bomDestination = _itemBoms.Where(a => a.Factory.Equals(factoryDestination)).Single();
                    bomDestination.Lines = new List<BomDetail>();

                    foreach(var line in bomSource.Lines)
                    {
                        var tmp = line.DeepCopyByExpressionTree();
                        tmp.CodeBom = bomDestination.Code;
                        bomDestination.Lines.Add(tmp);
                    }
                }

                grdItemBom.RefreshDataSource();
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

        //private decimal GetQuantityCalc(decimal length, decimal width, decimal height, decimal density, int numberOfParts, decimal coefficient1, decimal coefficient2, decimal scrap)
        //{
        //    try
        //    {
        //        decimal qty = length * width * height * density * (1 / numberOfParts) * coefficient1 * coefficient2 * scrap;
        //        return qty;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        private void ShowAddBomFactoryAndCopyBom(bool show)
        {
            try
            {
                lblFactory.Visible = slueFactory.Visible = btnAddBomFactory.Visible = lblCopyBom.Visible = btnCopyBom.Visible = show;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Grid Aux Methods

        private void ExpandBomDefaultFactory()
        {
            try
            {
                gridViewItemBom.BeginUpdate();
                string defaultFactory = _currentSelectedItem.CardCode;

                gridViewItemBom.ExpandAllGroups();

                for (int rHandle = 0; rHandle < gridViewItemBom.DataRowCount; rHandle++)
                {
                    object factory = gridViewItemBom.GetRowCellValue(rHandle, nameof(Bom.Factory));
                    if (factory != null && factory.ToString() == defaultFactory)
                        gridViewItemBom.SetMasterRowExpanded(rHandle, true);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                gridViewItemBom.EndUpdate();
            }
        }

        #endregion

        #endregion

        #region Validates
        private bool IsValidBom()
        {
            try
            {
                foreach(Bom bom in _itemBoms)
                {
                    foreach(BomDetail line in bom.Lines)
                    {
                        if (string.IsNullOrEmpty(line.ItemCode) == false && line.Quantity <= 0)
                        {
                            XtraMessageBox.Show($"Quantity must be greater than Zero ({line.ItemCode})", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }

                        if (string.IsNullOrEmpty(line.ItemCode) == false && string.IsNullOrEmpty(line.BomBreakdown))
                        {
                            XtraMessageBox.Show($"Select breakdown ({line.ItemCode})", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        #endregion

        #endregion

        private void Test()
        {
            try
            {
                //string sql = "select * from [@ETN_MODELS] where Code = '171'";

                //using (var connection = new SqlConnection(GlobalSetting.ConnectionString))
                //{
                //    connection.Open();

                //    var modelsDetails = connection.Query<Model>(sql).ToList();

                //    Console.WriteLine(modelsDetails.Count);
                //}


                //string sql = "";
                //sql += "select T0.ItemCode,T0.ItemName,T0.U_ETN_MODEL,T0.U_OPN_CATSUP,T0.ItmsGrpCod,T0.U_ETN_TIPART,T0.U_ETN_ETN_SUBTIPART,T0.U_OPN_CAT,T1.Code,T1.[Name] ";
                //sql += "from OITM T0 LEFT JOIN [@ETN_MODELS] T1 ON T1.Code = T0.U_ETN_MODEL ";
                //sql += "WHERE U_OPN_CATSUP = '01'and (ItemCode like '%LAPA SUN%' OR ItemCode like '%MARAIS SUN%')";

                //using (var connection = new SqlConnection(GlobalSetting.ConnectionString))
                //{
                //    connection.Open();

                //    var items = connection.Query<Oitm, Model, Oitm>(
                //        sql,
                //        (oitm, model) => 
                //        {
                //            oitm.Model = model;
                //            return oitm;
                //        }, splitOn : "Code"
                //        ).Distinct().ToList();
                //}

                //var x = GlobalSetting.BomService.GetItemBom("x");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
