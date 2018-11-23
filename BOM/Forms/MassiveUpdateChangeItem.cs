using BOM.Classes;
using BOM.General;
using BOM.Models;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BOM.Forms
{
    public partial class MassiveUpdateChangeItem : Form
    {
        #region Private Members
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        BindingList<OitmExt> _itemsforBomList;
        //BindingList<BomHeadExt> _componentBom;
        BindingList<MassiveUpdateRow> _componentBom;
        #endregion

        #region Constructor
        public MassiveUpdateChangeItem()
        {
            InitializeComponent();

            try
            {
                Cursor = Cursors.WaitCursor;

                SetUpEvents();
                LoadAuxList();
                SetUpSlueItems();
                SetUpGrdBom();
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


        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateFormData())
                {
                    SaveData();
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (slueOriginalItem.EditValue == null)
                {
                    XtraMessageBox.Show($"Select original item", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    slueOriginalItem.Focus();
                    return;
                }

                LoadComponentBom();
                btnSave.Enabled = true;

            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void SlueChangeItem_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                txtChangeItemName.EditValue = null;
                if (slueChangeItem.EditValue != null)
                    txtChangeItemName.EditValue = GetItemFromList((string)slueChangeItem.EditValue)?.ItemName;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SlueOriginalItem_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                txtOriginalItemName.EditValue = null;
                if (slueOriginalItem.EditValue != null)
                    txtOriginalItemName.EditValue = GetItemFromList((string)slueOriginalItem.EditValue)?.ItemName;

                btnSave.Enabled = false;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Private Methods

        #region Setup Form Objetcs
        private void SetUpEvents()
        {
            try
            {
                btnSave.Click += BtnSave_Click;
                btnSearch.Click += BtnSearch_Click;
                slueOriginalItem.EditValueChanged += SlueOriginalItem_EditValueChanged;
                slueChangeItem.EditValueChanged += SlueChangeItem_EditValueChanged;
            }
            catch
            {
                throw;
            }
        }

        private void SetUpSlueItems()
        {
            try
            {
                slueOriginalItem.Properties.DataSource = _itemsforBomList;
                slueOriginalItem.Properties.ValueMember = nameof(OitmExt.ItemCode);
                slueOriginalItem.Properties.DisplayMember = nameof(OitmExt.ItemCode);
                slueOriginalItem.Properties.View.Columns.AddField(nameof(OitmExt.ItemCode)).Visible = true;
                slueOriginalItem.Properties.View.Columns.AddField(nameof(OitmExt.ItemName)).Visible = true;
                slueOriginalItem.Properties.View.Columns.AddField(nameof(OitmExt.TipArtDesc)).Visible = true;

                slueChangeItem.Properties.DataSource = _itemsforBomList;
                slueChangeItem.Properties.ValueMember = nameof(OitmExt.ItemCode);
                slueChangeItem.Properties.DisplayMember = nameof(OitmExt.ItemCode);
                slueChangeItem.Properties.View.Columns.AddField(nameof(OitmExt.ItemCode)).Visible = true;
                slueChangeItem.Properties.View.Columns.AddField(nameof(OitmExt.ItemName)).Visible = true;
                slueChangeItem.Properties.View.Columns.AddField(nameof(OitmExt.TipArtDesc)).Visible = true;

            }
            catch
            {
                throw;
            }
        }

        private void SetUpGrdBom()
        {
            try
            {
                gridViewBom.OptionsView.ColumnAutoWidth = false;
                gridViewBom.HorzScrollVisibility = ScrollVisibility.Auto;

                //Todo el Grid no editable
                gridViewBom.OptionsBehavior.Editable = false;

                //multiselección con checkbox
                gridViewBom.OptionsSelection.MultiSelect = true;
                gridViewBom.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;

                //Mostrar el navegador en el footer para ver el total de registros
                grdBom.UseEmbeddedNavigator = true;

                //Columns
                GridColumn colFactory = new GridColumn() { Caption = "Factory Code", Visible = true, FieldName = nameof(MassiveUpdateRow.Factory), Width = 200 };
                GridColumn colFactoryName = new GridColumn() { Caption = "Factory Name", Visible = true, FieldName = nameof(MassiveUpdateRow.FactoryName), Width = 200 };
                GridColumn colItemCode = new GridColumn() { Caption = "Item Code", Visible = true, FieldName = nameof(MassiveUpdateRow.ItemCode), Width = 200 };
                GridColumn Version = new GridColumn() { Caption = "Version", Visible = true, FieldName = nameof(MassiveUpdateRow.Version), Width = 200 };
                GridColumn colVersionDate = new GridColumn() { Caption = "Version Date", Visible = true, FieldName = nameof(MassiveUpdateRow.VersionDate), Width = 200 };
                GridColumn colU_ETN_stat = new GridColumn() { Caption = "Item Status", Visible = true, FieldName = nameof(MassiveUpdateRow.U_ETN_stat), Width = 200 };
                GridColumn colComponentCode = new GridColumn() { Caption = "Component Code", Visible = true, FieldName = nameof(MassiveUpdateRow.ComponentCode), Width = 200 };
                GridColumn colComponentQuantity = new GridColumn() { Caption = "Component Quantity", Visible = true, FieldName = nameof(MassiveUpdateRow.ComponentQuantity), Width = 200 };

                //Display format
                colVersionDate.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                gridViewBom.Columns.Add(colFactory);
                gridViewBom.Columns.Add(colFactoryName);
                gridViewBom.Columns.Add(colItemCode);
                gridViewBom.Columns.Add(Version);
                gridViewBom.Columns.Add(colVersionDate);
                gridViewBom.Columns.Add(colU_ETN_stat);
                gridViewBom.Columns.Add(colComponentCode);
                gridViewBom.Columns.Add(colComponentQuantity);



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
                _itemsforBomList = new BindingList<OitmExt>(GlobalSetting.OitmService.GetPossibleItemsForBom());
            }
            catch
            {
                throw;
            }
        }

        private void LoadComponentBom()
        {
            try
            {
                _componentBom = new BindingList<MassiveUpdateRow>(GlobalSetting.BomService.GetComponentBom((string)slueOriginalItem.EditValue));
                grdBom.DataSource = null;
                grdBom.DataSource = _componentBom;
                gridViewBom.BestFitColumns();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private bool SaveData()
        {
            try
            {
                string bomCodes = string.Empty;
                //obtenemos las líneas seleccionadas.
                for(int i = gridViewBom.SelectedRowsCount -1; i >=0; i--)
                {
                    var currentRowIndex = gridViewBom.GetSelectedRows()[i];
                    MassiveUpdateRow row = gridViewBom.GetRow(currentRowIndex) as MassiveUpdateRow;
                    bomCodes += $",{row.Code}";
                }

                int modifiedBoms = GlobalSetting.BomService.MassiveItemChangeFromBomList(bomCodes.Substring(1), (string)slueOriginalItem.EditValue, (string)slueChangeItem.EditValue);
                XtraMessageBox.Show($"{modifiedBoms.ToString()} BOMs modified.");
                return true;
            }
            catch
            {
                throw;
            }
            finally
            {
                grdBom.DataSource = null;
            }
        }
        #endregion

        #region Validates
        private bool ValidateFormData()
        {
            try
            {

                if(gridViewBom.SelectedRowsCount == 0)
                {
                    XtraMessageBox.Show($"No BOM selected", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if(slueOriginalItem.EditValue == null)
                {
                    XtraMessageBox.Show($"Select original item", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    slueOriginalItem.Focus();
                    return false;
                }

                if (slueChangeItem.EditValue == null)
                {
                    XtraMessageBox.Show($"Select item to change for", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    slueChangeItem.Focus();
                    return false;
                }

                if(slueOriginalItem.EditValue == slueChangeItem.EditValue)
                {
                    XtraMessageBox.Show("Original item and item to change for are equals.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                OitmExt originalItem = GetItemFromList((string)slueOriginalItem.EditValue);
                OitmExt changeItem = GetItemFromList((string)slueChangeItem.EditValue);

                if(originalItem.U_ETN_TIPART != changeItem.U_ETN_TIPART)
                {
                    XtraMessageBox.Show("Can not change between different item types", string.Empty,MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }


                return true;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Aux Methods

        private OitmExt GetItemFromList(string itemCode)
        {
            try
            {
                return _itemsforBomList.Where(a => a.ItemCode.Equals(itemCode)).FirstOrDefault();
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
