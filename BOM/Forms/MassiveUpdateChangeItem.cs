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
                slueOriginalItem.Properties.DisplayMember = nameof(OitmExt.ItemName);
                slueOriginalItem.Properties.View.Columns.AddField(nameof(OitmExt.ItemCode)).Visible = true;
                slueOriginalItem.Properties.View.Columns.AddField(nameof(OitmExt.ItemName)).Visible = true;
                slueOriginalItem.Properties.View.Columns.AddField(nameof(OitmExt.TipArtDesc)).Visible = true;

                slueChangeItem.Properties.DataSource = _itemsforBomList;
                slueChangeItem.Properties.ValueMember = nameof(OitmExt.ItemCode);
                slueChangeItem.Properties.DisplayMember = nameof(OitmExt.ItemName);
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
        #endregion

        #endregion
    }
}
