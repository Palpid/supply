using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using HKSupply.General;

namespace HKSupply.Forms.Master.DialogForms
{
    public partial class SelectSuppliers : Form
    {

        #region Private Members
        List<string> _suppliers;
        #endregion

        #region Public Properties
        public List<string> SelectedSuppliersSource { get; set; }
        public List<string> SelectedSuppliersDestination { get; set; }
        #endregion

        #region Constructor
        public SelectSuppliers()
        {
            InitializeComponent();

            try
            {
                SetFormStyle();
                SetEvents();
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Form Events
        private void SelectSupplirs_Load(object sender, EventArgs e)
        {

        }

        private void CheckedListBoxSuppliersSource_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
        {
            try
            {
                if (e.NewValue == CheckState.Unchecked) return;
               
                for (int i = 0; i < checkedListBoxSuppliersSource.Items.Count; i++)
                {
                    if (i != e.Index)
                        checkedListBoxSuppliersSource.SetItemChecked(i, false);
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Public Methods
        public void InitData(List<string> suppliers)
        {
            try
            {
                _suppliers = suppliers;
                LoadCheckedListBoxSuppliers();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Private Methods

        private void SetEvents()
        {
            sbCancel.Click += (o, e) => { Close(); };

            sbOk.Click += (o, e) =>
            {
                SelectedSuppliersSource = new List<string>();
                SelectedSuppliersDestination = new List<string>();

                foreach (var itemChecked in checkedListBoxSuppliersSource.CheckedItems)
                {
                    SelectedSuppliersSource.Add(itemChecked.ToString());
                }

                foreach (var itemChecked in checkedListBoxSuppliersDestination.CheckedItems)
                {
                    SelectedSuppliersDestination.Add(itemChecked.ToString());
                }

                if (ValidateSuppliers())
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    XtraMessageBox.Show("Same Supplier as source  and destination", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
            };

            checkedListBoxSuppliersSource.ItemCheck += CheckedListBoxSuppliersSource_ItemCheck;
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
                Text = "Select suppliers";

                checkedListBoxSuppliersDestination.CheckOnClick = true;
                checkedListBoxSuppliersSource.CheckOnClick = true;
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
                foreach (var supplier in _suppliers)
                {
                    checkedListBoxSuppliersSource.Items.Add(new CheckedListBoxItem(supplier, false));
                }

                foreach (var supplier in _suppliers)
                {
                    if (supplier != Constants.INTRANET_ETNIA_BCN)
                        checkedListBoxSuppliersDestination.Items.Add(new CheckedListBoxItem(supplier, false));
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Validar si hemos escogido el mismo supplier como Source y Destination
        /// </summary>
        /// <returns></returns>
        private bool ValidateSuppliers()
        {
            try
            {
                var validate = SelectedSuppliersSource.Intersect(SelectedSuppliersDestination).ToList();
                if (validate.Count == 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}
