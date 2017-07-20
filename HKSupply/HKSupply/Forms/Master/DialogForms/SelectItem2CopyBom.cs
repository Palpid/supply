using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
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
    public partial class SelectItem2CopyBom : Form
    {
        #region Private Members
        string _idItemDestination;
        string _model;
        string _supplier;
        ItemBom _selectedBom;
        #endregion

        #region Public Properties
        public ItemBom SelectedBom
        {
            get { return _selectedBom; }
        }
        #endregion

        #region Constructor
        public SelectItem2CopyBom()
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

        #region Form Events
        private void SelectItem2CopyBom_Load(object sender, EventArgs e)
        {

        }

        private void CheckedListBoxItems_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
        {
            try
            {
                if (e.NewValue == CheckState.Unchecked) return;

                for (int i = 0; i < checkedListBoxItems.Items.Count; i++)
                {
                    if (i != e.Index)
                        checkedListBoxItems.SetItemChecked(i, false);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SbOk_Click(object sender, EventArgs e)
        {
            try
            {
                ReturnSelectedBom();
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Public Methods
        public void InitData(string idItemDestination, string model, string supplier)
        {
            try
            {
                _idItemDestination = idItemDestination;
                _model = model;
                _supplier = supplier;
                lbltxtSupplier.Text = _supplier;
                LoadCheckedListBoxItems();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Private Methods

        private void LoadCheckedListBoxItems()
        {
            try
            {
                using (var db = new DB.HKSupplyContext())
                {
                    /*
                     
                     SELECT ib.* 
                    FROM ITEMS_BOM ib
                    INNER JOIN ITEMS_EY ey on ey.ID_ITEM_BCN = ib.ID_ITEM_BCN 
                    where 
	                    ey.ID_MODEL = 757 AND
	                    ib.ID_SUPPLIER = 'CV' AND 
	                    ib.ID_ITEM_BCN <> '5 AKANE BLBE'
                     
                     */
                    var items = (from ib in db.ItemsBom
                             join ey in db.ItemsEy on ib.IdItemBcn equals ey.IdItemBcn
                             where
                                ey.IdModel == _model &&
                                ib.IdSupplier == _supplier &&
                                ib.IdItemBcn != _idItemDestination
                             select ib.IdItemBcn
                             ).ToList();

                    foreach (var item in items)
                    {
                        checkedListBoxItems.Items.Add(new CheckedListBoxItem(item, false));
                    }
                }
               

            }
            catch
            {
                throw;
            }
        }

        private void SetFormStyle()
        {
            try
            {
                MaximizeBox = false;
                MinimizeBox = false;
                ShowInTaskbar = false;
                ShowIcon = false;
                FormBorderStyle = FormBorderStyle.FixedDialog;
                Text = string.Empty;

                checkedListBoxItems.CheckOnClick = true;
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
                sbCancel.Click += (o, e) => { Close(); };
                sbOk.Click += SbOk_Click;
                checkedListBoxItems.ItemCheck += CheckedListBoxItems_ItemCheck;

            }
            catch
            {
                throw;
            }
        }

        private void ReturnSelectedBom()
        {
            try
            {
                if (ValidateForm())
                {
                    string idItem = checkedListBoxItems.CheckedItems[0].ToString(); //ya se controla que sólo se pueda seleccionar uno
                    _selectedBom = GlobalSetting.ItemBomService.GetItemSupplierBom(idItem, _supplier);
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            catch
            {
                throw;
            }
        }

        private bool ValidateForm()
        {
            try
            {
                if (checkedListBoxItems.CheckedItems.Count == 0)
                {
                    XtraMessageBox.Show("No Item Selected", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

    }
}
