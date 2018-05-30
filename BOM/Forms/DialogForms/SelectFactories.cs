using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
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
    public partial class SelectFactories : Form
    {
        #region Private Members
        List<string> _factories;
        #endregion

        #region Public Properties
        public List<string> SelectedFactoriesSource { get; set; }
        public List<string> SelectedFactoriesDestination { get; set; }
        #endregion

        #region Constructor
        public SelectFactories()
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

        #region Form Events
        private void CheckedListBoxFactoriesSource_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
        {
            try
            {
                if (e.NewValue == CheckState.Unchecked) return;

                for (int i = 0; i < checkedListBoxFactoriesSource.Items.Count; i++)
                {
                    if (i != e.Index)
                        checkedListBoxFactoriesSource.SetItemChecked(i, false);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Public Methods
        public void InitData(List<string> factories)
        {
            try
            {
                _factories = factories;
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
                SelectedFactoriesSource = new List<string>();
                SelectedFactoriesDestination = new List<string>();

                foreach (var itemChecked in checkedListBoxFactoriesSource.CheckedItems)
                {
                    SelectedFactoriesSource.Add(itemChecked.ToString());
                }

                foreach (var itemChecked in checkedListBoxFactoriesDestination.CheckedItems)
                {
                    SelectedFactoriesDestination.Add(itemChecked.ToString());
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

            checkedListBoxFactoriesSource.ItemCheck += CheckedListBoxFactoriesSource_ItemCheck;
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

                checkedListBoxFactoriesDestination.CheckOnClick = true;
                checkedListBoxFactoriesSource.CheckOnClick = true;
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
                    checkedListBoxFactoriesSource.Items.Add(new CheckedListBoxItem(factory, false));
                    checkedListBoxFactoriesDestination.Items.Add(new CheckedListBoxItem(factory, false));
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
                var validate = SelectedFactoriesSource.Intersect(SelectedFactoriesDestination).ToList();
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

        #endregion
    }
}
