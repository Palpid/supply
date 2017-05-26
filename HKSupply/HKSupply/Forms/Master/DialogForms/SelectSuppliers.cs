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

namespace HKSupply.Forms.Master.DialogForms
{
    public partial class SelectSuppliers : Form
    {

        #region Private Members
        List<string> _suppliers;
        #endregion

        #region Public Properties
        public List<string> SelectedSuppliers { get; set; }
        #endregion

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

        private void SelectSupplirs_Load(object sender, EventArgs e)
        {

        }

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
                SelectedSuppliers = new List<string>();
                foreach( var itemChecked in checkedListBoxSuppliers.CheckedItems)
                {
                    SelectedSuppliers.Add(itemChecked.ToString());
                }
                DialogResult = DialogResult.OK;
                Close();
            };
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
                    checkedListBoxSuppliers.Items.Add(new CheckedListBoxItem(supplier, false));
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}
