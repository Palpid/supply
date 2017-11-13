using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using HKSupply.Models.Supply;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HKSupply.Forms.Supply.DialogForms
{
    public partial class SelectDocs : Form
    {
        #region Private Members
        DocHead _selectedDoc;
        List<DocHead> _docsList;
        #endregion

        #region Public Properties
        public DocHead SelectedDoc { get { return _selectedDoc; }  }
        #endregion

        #region Constructor
        public SelectDocs()
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
        private void SelectDocs_Load(object sender, EventArgs e)
        {

        }

        private void CheckedListBoxDocs_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
        {
            try
            {
                if (e.NewValue == CheckState.Unchecked) return;

                for (int i = 0; i < checkedListBoxDocs.Items.Count; i++)
                {
                    if (i != e.Index)
                        checkedListBoxDocs.SetItemChecked(i, false);
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Public Methods
        public void InitData(List<DocHead> docs)
        {
            try
            {
                _docsList = docs;
                LoadCheckedListBoxDocs();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Private Methods

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

                checkedListBoxDocs.CheckOnClick = true;

            }
            catch
            {
                throw;
            }
        }

        private void SetEvents()
        {
            sbCancel.Click += (o, e) => { Close(); };

            sbOk.Click += (o, e) =>
            {
                if (checkedListBoxDocs.CheckedItems.Count == 0)
                {
                    XtraMessageBox.Show("No selected Document");
                    return;
                }

                CheckedListBoxItem ci = checkedListBoxDocs.CheckedItems[0] as CheckedListBoxItem; //Controlamos que sólo se pueda marcar 1 documento
                _selectedDoc = ci.Value as DocHead;
                DialogResult = DialogResult.OK;
                Close();
            };

            checkedListBoxDocs.ItemCheck += CheckedListBoxDocs_ItemCheck;
        }

        private void LoadCheckedListBoxDocs()
        {
            try
            {
                foreach (var doc in _docsList)
                {
                    checkedListBoxDocs.Items.Add(new CheckedListBoxItem(doc, doc.IdDoc, false));
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
