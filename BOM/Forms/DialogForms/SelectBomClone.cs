using BOM.Classes;
using BOM.General;
using BOM.Models;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BOM.Forms.DialogForms
{
    public partial class SelectBomClone : Form
    {
        #region Private Members

        List<string> _factories;
        List<BomHeadExt> _bomHeadList;
        
        #endregion

        #region Constructor
        public SelectBomClone()
        {
            InitializeComponent();

            try
            {
                SetFormStyle();
                SetEvents();
                GetAllBomHead();
                SetUpGrdBomFactory();
                SetupSlue();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Form Events

        private void CheckedListBoxFactories_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
        {
            try
            {
                if (e.NewValue == CheckState.Unchecked) return;

                for (int i = 0; i < checkedListBoxFactories.Items.Count; i++)
                {
                    if (i != e.Index)
                        checkedListBoxFactories.SetItemChecked(i, false);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SlueItemSource_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                LoadGrid();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GridViewBomFactory_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
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
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Public Methods
        public void InitData(List<string> factories, string itemCode)
        {
            try
            {
                _factories = factories;
                LoadCheckedListBoxSuppliers();

                var items = _bomHeadList.Where(a => a.ItemCode != itemCode).Select(obj => new { ItemCode = obj.ItemCode }).Distinct().ToList();
                slueItemSource.Properties.DataSource = items;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Private Methods

        
       
        
        #region SetUp Objects
        private void SetEvents()
        {
            try
            {
                checkedListBoxFactories.ItemCheck += CheckedListBoxFactories_ItemCheck;

                sbCancel.Click += (o, e) => { Close(); };

                sbOk.Click += (o, e) =>
                {

                };
            }
            catch
            {
                throw;
            }
        }

        private void SetUpGrdBomFactory()
        {
            try
            {
                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                gridViewBomFactory.OptionsView.ColumnAutoWidth = false;
                gridViewBomFactory.HorzScrollVisibility = ScrollVisibility.Auto;

                //Todo el Grid no editable
                gridViewBomFactory.OptionsBehavior.Editable = false;

                //selection checkbox
                gridViewBomFactory.OptionsSelection.MultiSelect = true;
                gridViewBomFactory.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
                gridViewBomFactory.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.False;


                //Columns definition
                GridColumn colFactoryName = new GridColumn() { Caption = "Factory", Visible = true, FieldName = $"{nameof(BomHeadExt.FactoryDet)}.{nameof(Ocrd.CardFName)}", Width = 200 };
                GridColumn colVersion = new GridColumn() { Caption = "Version", Visible = true, FieldName = nameof(BomHeadExt.Version), Width = 200 };

                //Add columns to grid root view
                gridViewBomFactory.Columns.Add(colFactoryName);
                gridViewBomFactory.Columns.Add(colVersion);

                //Grouping
                gridViewBomFactory.OptionsView.ShowGroupPanel = false;

                //Events
                gridViewBomFactory.SelectionChanged += GridViewBomFactory_SelectionChanged;
            }
            catch
            {
                throw;
            }
        }

        private void SetupSlue()
        {
            try
            {
                slueItemSource.Properties.DisplayMember = nameof(BomHeadExt.ItemCode);
                slueItemSource.Properties.ValueMember = nameof(BomHeadExt.ItemCode);
                slueItemSource.Properties.View.Columns.AddField(nameof(BomHeadExt.ItemCode)).Visible = true;
                slueItemSource.EditValueChanged += SlueItemSource_EditValueChanged;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Aux

        private void SetFormStyle()
        {
            try
            {
                MaximizeBox = false;
                MinimizeBox = false;
                ShowInTaskbar = false;
                ShowIcon = false;
                FormBorderStyle = FormBorderStyle.FixedDialog;
                Text = "Select suppliers";

                checkedListBoxFactories.CheckOnClick = true;
            }
            catch
            {
                throw;
            }
        }

        private void LoadCheckedListBoxSuppliers()
        {
            try
            {
                foreach (var factory in _factories)
                {
                    checkedListBoxFactories.Items.Add(new CheckedListBoxItem(factory, false));
                }
            }
            catch
            {
                throw;
            }
        }
        
        private void LoadGrid()
        {
            try
            {
                grdBomFactory.DataSource = null;

                if (slueItemSource.EditValue != null)
                {
                    var itemBoms = _bomHeadList.Where(a => a.ItemCode.Equals((string)slueItemSource.EditValue)).ToList();
                    grdBomFactory.DataSource = itemBoms;
                    gridViewBomFactory.BestFitColumns();
                }

            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region CRUD

        private void GetAllBomHead()
        {
            try
            {
                _bomHeadList = GlobalSetting.BomService.GetAllBomHead();
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
