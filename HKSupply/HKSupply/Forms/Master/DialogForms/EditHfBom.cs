using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using HKSupply.General;
using HKSupply.Helpers;
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

namespace HKSupply.Forms.Master.DialogForms
{
    public partial class EditHfBom : Form
    {
        #region Private Members
        //object _currentItem;
        List<ItemBom> _itemBomList = new List<ItemBom>();

        List<ItemHf> _itemsHfList;
        List<ItemMt> _itemsMtList;
        List<ItemHw> _itemsHwList;
        #endregion

        #region Constructor
        public EditHfBom()
        {
            InitializeComponent();

            try
            {
                LoadAuxList();
                SetFormStyle();
                SetEvents();
                SetUpGrdItemBom();
                            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Events

        private void EditHfBom_Load(object sender, EventArgs e)
        {

        }

        #region Grid Bom Events
        private void XgrdItemBom_ViewRegistered(object sender, DevExpress.XtraGrid.ViewOperationEventArgs e)
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
                        //TODO
                        //(e.View as GridView).CellValueChanged += grdBomView_CellValueChanged;
                        (e.View as GridView).ValidatingEditor += BomManagementMaterials_ValidatingEditor;

                        //Agregamos los Summary
                        //(e.View as GridView).OptionsView.ShowFooter = true;
                        //(e.View as GridView).Columns[nameof(DetailBomMt.Quantity)].Summary.Add(SummaryItemType.Sum, nameof(DetailBomMt.Quantity), "{0:n}");
                        //(e.View as GridView).Columns[nameof(DetailBomMt.Scrap)].Summary.Add(SummaryItemType.Sum, nameof(DetailBomMt.Scrap), "{0:n}");

                        //Si está en edición al pintar una nueva vista tiene que hacerla editable
                        //TODO
                        //if (CurrentState == ActionsStates.Edit)
                            SetGrdBomEditColumns();
                        //else
                        //    SetGrdBomDetailsNonEdit();

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
                        //TODO
                        //(e.View as GridView).CellValueChanged += grdBomView_CellValueChanged;
                        (e.View as GridView).ValidatingEditor += BomManagementHardwares_ValidatingEditor;

                        //Agregamos los Summary
                        //(e.View as GridView).OptionsView.ShowFooter = true;
                        //(e.View as GridView).Columns[nameof(DetailBomHw.Quantity)].Summary.Add(SummaryItemType.Sum, nameof(DetailBomHw.Quantity), "{0:n}");
                        //(e.View as GridView).Columns[nameof(DetailBomHw.Scrap)].Summary.Add(SummaryItemType.Sum, nameof(DetailBomHw.Scrap), "{0:n}");

                        //Si está en edición al pintar una nueva vista tiene que hacerla editable
                        //TODO
                        //if (CurrentState == ActionsStates.Edit)
                            SetGrdBomEditColumns();
                        //else
                        //    SetGrdBomDetailsNonEdit();

                        break;

                    //case nameof(DetailBomHf.DetailItemBom) + "." + nameof(ItemBom.HalfFinished):
                    //case nameof(ItemBom.HalfFinishedNM):

                    //    (e.View as GridView).DetailHeight = 1000;

                    //    //Ocultamos todas las columnas menos el item que no nos interesan
                    //    foreach (GridColumn col in (e.View as GridView).Columns)
                    //    {
                    //        if (col.FieldName != nameof(ItemBom.IdItemBcn))
                    //            col.Visible = false;
                    //    }

                    //    //Seteamos el tamaño de las columnas
                    //    (e.View as GridView).Columns[nameof(ItemBom.IdItemBcn)].Width = 150;

                    //    //agregamos la columna de descripcion
                    //    GridColumn colDescriptionItemHf = new GridColumn()
                    //    {
                    //        Caption = GlobalSetting.ResManager.GetString("ItemDescription"),
                    //        Visible = true,
                    //        FieldName = $"{nameof(ItemBom.Item)}.{nameof(ItemHf.ItemDescription)}",
                    //        Width = 300
                    //    };

                    //    (e.View as GridView).Columns.Add(colDescriptionItemHf);

                    //    //Si está en edición al pintar una nueva vista tiene que hacerla editable
                    //    if (CurrentState == ActionsStates.Edit)
                    //        SetGrdBomEditColumns();
                    //    else
                    //        SetGrdBomDetailsNonEdit();

                    //    //aki
                    //    ExpandAllRows((e.View as GridView));
                        //break;


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
                        //GridColumn colEditHfButton = new GridColumn() { Caption = "Edit", Visible = false, FieldName = EDIT_COLUMN, Width = 50 };
                        //(e.View as GridView).Columns.Add(colEditHfButton);

                        //Formats
                        (e.View as GridView).Columns[nameof(DetailBomHf.Quantity)].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        (e.View as GridView).Columns[nameof(DetailBomHf.Quantity)].DisplayFormat.FormatString = "n2";

                        //Columns order
                        (e.View as GridView).Columns[$"{nameof(DetailBomHf.DetailItemBom)}.{nameof(ItemBom.IdItemBcn)}"].VisibleIndex = 0;
                        (e.View as GridView).Columns[nameof(DetailBomHf.Quantity)].VisibleIndex = 1;

                        //Events
                        (e.View as GridView).ShownEditor += BomManagementHalfFinished_ShownEditor;
                        (e.View as GridView).ValidatingEditor += BomManagementHalfFinished_ValidatingEditor;

                        //if (CurrentState == ActionsStates.Edit)
                            SetGrdBomEditColumns();
                        //else
                        //    SetGrdBomDetailsNonEdit();

                        break;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

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
                        var ids = supplierItemsHf.Select(a => a.IdItemBcn).ToList();
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

        private void XgrdItemBom_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
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

                                        if (row.Quantity > 0 && string.IsNullOrEmpty(row.IdItemBcn) == false)
                                        {
                                            if (rowParent.GetType().BaseType == typeof(ItemBom) || rowParent.GetType() == typeof(ItemBom))
                                            {
                                                (rowParent as ItemBom).Hardwares.Add(new DetailBomHw());
                                            }
                                            activeView.RefreshData();

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
                                            activeView.RefreshData();

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

        #endregion

        #region Public Methods
        //public void InitData(object currentItem , List<ItemBom> itemBomList)
        //{
        //    try
        //    {
        //        _currentItem = currentItem;
        //        _itemBomList = itemBomList;

        //        LoadGridBom();
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        public void InitData(string idItemBcn, string idSupplier)
        {
            try
            {
                var bom =  GlobalSetting.ItemBomService.GetItemSupplierBom(idItemBcn, idSupplier);

                _itemBomList.Add(bom);
                LoadGridBom();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Private Methods

        #region Setups

        private void SetFormStyle()
        {
            try
            {
                MaximizeBox = false;
                MinimizeBox = false;
                ShowInTaskbar = false;
                ShowIcon = false;
                FormBorderStyle = FormBorderStyle.FixedDialog;
                Text = "Edit BOM";

            }
            catch
            {
                throw;
            }
        }

        private void SetEvents()
        {
            try
            {
                sbCancel.Click += (o, e) =>
                {
                    Close();
                };

                sbSave.Click += (o, e) =>
                {
                    if (IsValidBom())
                    {
                        DeleteLasRowIfEmpty();

                        DialogResult = DialogResult.OK;
                        Close();
                    }
                };

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

        #endregion

        #region Loads

        private void LoadAuxList()
        {
            try
            {
                _itemsMtList = GlobalSetting.ItemMtService.GetItems();
                _itemsHwList = GlobalSetting.ItemHwService.GetItems();
                _itemsHfList = GlobalSetting.ItemHfService.GetItems();
            }
            catch
            {
                throw;
            }
        }

        private void LoadGridBom()
        {
            try
            {
                xgrdItemBom.DataSource = null;
                xgrdItemBom.DataSource = _itemBomList;
                grdBomRefreshAndExpand();
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Grid BOM

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

                            //RepositoryItemButtonEdit repEditHf = new RepositoryItemButtonEdit()
                            //{
                            //    Name = "btnEditHf",
                            //    TextEditStyle = TextEditStyles.HideTextEditor,
                            //};
                            //repEditHf.Buttons[0].Kind = ButtonPredefines.Glyph;
                            //repEditHf.Buttons[0].Image = DevExpress.Images.ImageResourceCache.Default.GetImage("images/edit/edit_16x16.png");
                            //repEditHf.Click += RepEditHf_Click;


                            view.OptionsBehavior.Editable = true;

                            view.Columns[nameof(DetailBomHf.Quantity)].ColumnEdit = ritxt2Dec;
                            view.Columns[$"{nameof(DetailBomHf.DetailItemBom)}.{nameof(ItemBom.IdItemBcn)}"].ColumnEdit = riItemsHf;
                            //view.Columns[EDIT_COLUMN].ColumnEdit = repEditHf;
                            //view.Columns[EDIT_COLUMN].Visible = true;


                            break;
                    }
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

        #region PopupMenu
        private void GridViewItemBom_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            try
            {

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

                    foreach(var hf in bom.HalfFinished)
                    {
                        if(hf.DetailItemBom != null && hf.Quantity <= 0)
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

        private void DeleteLasRowIfEmpty()
        {
            try
            {
                foreach (var bom in _itemBomList)
                {
                    //Hay que hacer el loop al inverso para poder ir borrando y que no de error

                    for (int i = bom.Materials.Count -1; i >= 0; i--)
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

        #endregion

        #endregion

    }
}
