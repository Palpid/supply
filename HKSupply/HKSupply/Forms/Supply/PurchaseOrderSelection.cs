using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using HKSupply.General;
using HKSupply.Models;
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

namespace HKSupply.Forms.Supply
{
    public partial class PurchaseOrderSelection : RibbonFormBase
    {
        #region Private Members

        Font _labelDefaultFontBold = new Font("SourceSansProRegular", 8, FontStyle.Bold);
        Font _labelDefaultFont = new Font("SourceSansProRegular", 8, FontStyle.Regular);

        List<Supplier> _suppliersList;
        List<SupplyStatus> _supplyStatusList;
        #endregion

        #region Constructor
        public PurchaseOrderSelection()
        {
            InitializeComponent();

            try
            {
                Cursor = Cursors.WaitCursor;

                ConfigureRibbonActions();
                LoadAuxList();
                SetUpLabels();
                SetUpSearchLookUpEdit();
                SetUpEvents();
                SetUpGrdLines();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        #endregion

        #region Ribbon
        private void ConfigureRibbonActions()
        {
            try
            {
                //Task Buttons
                SetActions();
                RestoreInitState();
                //Print and export buttons
                EnablePrintPreview = false;
                EnableExportExcel = true;
                EnableExportCsv = true;
                ConfigurePrintExportOptions();
                //Layout
                EnableLayoutOptions = true;
                ConfigureLayoutOptions();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Form Events

        private void PurchaseOrderSelection_Load(object sender, EventArgs e)
        {

        }

        private void SbFilter_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private Methods

        #region Loads
        private void LoadAuxList()
        {
            try
            {
                _suppliersList = GlobalSetting.SupplierService.GetSuppliers();
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region SetUp Form objects

        private void SetUpLabels()
        {
            try
            {
                /********* Fonts **********/
                //Filter 
                lblPONumber.Font = _labelDefaultFontBold;
                lblSupplier.Font = _labelDefaultFontBold;
                lblStatus.Font = _labelDefaultFontBold;
                lblDocDate.Font = _labelDefaultFontBold;
                lblAnd.Font = _labelDefaultFontBold;
                txtPONumber.Font = _labelDefaultFontBold;

                /********* Texts **********/
                //Headers
                lblPONumber.Text = "PO Number";
                lblSupplier.Text = "Supplier";
                lblStatus.Text = "Status";
                lblDocDate.Text = "PO Date          between";
                lblAnd.Text = "and";
                txtPONumber.Text = string.Empty;
            }
            catch
            {
                throw;
            }
        }

        private void SetUpSearchLookUpEdit()
        {
            try
            {
                SetUpSlueSupplier();
                SetUpSlueStatus();
            }
            catch
            {
                throw;
            }
        }

        private void SetUpSlueSupplier()
        {
            try
            {
                slueSupplier.Properties.DataSource = _suppliersList;
                slueSupplier.Properties.ValueMember = nameof(Supplier.IdSupplier);
                slueSupplier.Properties.DisplayMember = nameof(Supplier.SupplierName);
                slueSupplier.Properties.View.Columns.AddField(nameof(Supplier.IdSupplier)).Visible = true;
                slueSupplier.Properties.View.Columns.AddField(nameof(Supplier.SupplierName)).Visible = true;
                slueSupplier.Properties.NullText = "Select Supplier...";
            }
            catch
            {
                throw;
            }
        }

        private void SetUpSlueStatus()
        {
            try
            {
                slueStatus.Properties.DataSource = _supplyStatusList;
                slueStatus.Properties.ValueMember = nameof(SupplyStatus.IdSupplyStatus);
                slueStatus.Properties.DisplayMember = nameof(SupplyStatus.Description);
                slueStatus.Properties.View.Columns.AddField(nameof(SupplyStatus.IdSupplyStatus)).Visible = true;
                slueStatus.Properties.View.Columns.AddField(nameof(SupplyStatus.Description)).Visible = true;
                slueStatus.Properties.NullText = "Select Status...";
            }
            catch
            {
                throw;
            }
        }

        private void SetUpEvents()
        {
            try
            {
                sbFilter.Click += SbFilter_Click;
            }
            catch
            {
                throw;
            }
        }

        private void SetUpGrdLines()
        {
            try
            {
                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                gridViewLines.OptionsView.ColumnAutoWidth = false;
                gridViewLines.HorzScrollVisibility = ScrollVisibility.Auto;

                //Hacer todo el grid no editable
                gridViewLines.OptionsBehavior.Editable = false;

                //Column Definition
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
