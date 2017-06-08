using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using HKSupply.General;
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
        object _currentItem;
        List<ItemBom> _itemBomList = new List<ItemBom>();
        #endregion

        #region Constructor
        public EditHfBom()
        {
            InitializeComponent();

            try
            {
                SetFormStyle();
                SetEvents();
                            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Public Methods
        public void InitData(object currentItem , List<ItemBom> itemBomList)
        {
            try
            {
                _currentItem = currentItem;
                _itemBomList = itemBomList;

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
            sbCancel.Click += (o, e) => { Close(); };

            //sbOk.Click += (o, e) =>
            //{
            //    SelectedSuppliers = new List<string>();
            //    foreach (var itemChecked in checkedListBoxSuppliers.CheckedItems)
            //    {
            //        SelectedSuppliers.Add(itemChecked.ToString());
            //    }
            //    DialogResult = DialogResult.OK;
            //    Close();
            //};
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
                //xgrdItemBom.ViewRegistered += XgrdItemBom_ViewRegistered;

                //gridViewItemBom.PopupMenuShowing += GridViewItemBom_PopupMenuShowing;

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

        #endregion

    }
}
