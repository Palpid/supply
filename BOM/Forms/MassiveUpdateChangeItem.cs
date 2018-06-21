using BOM.Classes;
using BOM.General;
using DevExpress.XtraEditors;
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

        private bool SaveData()
        {
            try
            {
                int modifiedBoms = GlobalSetting.BomService.MassiveItemChange((string)slueOriginalItem.EditValue, (string)slueChangeItem.EditValue);
                XtraMessageBox.Show($"{modifiedBoms.ToString()} BOMs modified.");
                return true;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Validates
        private bool ValidateFormData()
        {
            try
            {
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
