﻿using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using HKSupply.Classes;
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
        List<POSelection> _poSelectionList;
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
            try
            {
                if (IsValidFilter() == true)
                    LoadPoSelection();
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GridViewLines_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                POSelection po = view.GetRow(view.FocusedRowHandle) as POSelection;

                if (po != null)
                {
                    OpenPoForm(po);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Private Methods

        #region Loads
        private void LoadAuxList()
        {
            try
            {
                _suppliersList = GlobalSetting.SupplierService.GetSuppliers();
                _supplyStatusList = GlobalSetting.SupplyDocsService.GetSupplyStatus();
            }
            catch
            {
                throw;
            }
        }

        private void LoadPoSelection()
        {
            try
            {
                string idDocPo = txtPONumber.Text;
                string idSupplyStatus = (string)slueStatus.EditValue;
                string idSupplier = (string)slueSupplier.EditValue;
                DateTime PODateIni = dateEditPODateIni.DateTime;
                DateTime PODateEnd = dateEditPODateEnd.DateTime;

                _poSelectionList = GlobalSetting.SupplyDocsService.GetPOSelection(idDocPo, idSupplyStatus, idSupplier, PODateIni, PODateEnd);

                xgrdLines.DataSource = null;
                xgrdLines.DataSource = _poSelectionList;
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
                GridColumn colPoNumber = new GridColumn() { Caption = "PO Number", Visible = true, FieldName = nameof(POSelection.PoNumber), Width = 100 };
                GridColumn colSupplier = new GridColumn() { Caption = "Supplier", Visible = true, FieldName = nameof(POSelection.Supplier), Width = 100 };
                GridColumn colOrderDate = new GridColumn() { Caption = "Order Date", Visible = true, FieldName = nameof(POSelection.OrderDate), Width = 100 };
                GridColumn colDeliveryStart = new GridColumn() { Caption = "Delivery Start", Visible = true, FieldName = nameof(POSelection.DeliveryStart), Width = 100 };
                GridColumn colDeleveryEnd = new GridColumn() { Caption = "Delevery End", Visible = true, FieldName = nameof(POSelection.DeleveryEnd), Width = 100 };
                GridColumn colOriginalQuantity = new GridColumn() { Caption = "Original Quantity", Visible = true, FieldName = nameof(POSelection.OriginalQuantity), Width = 150 };
                GridColumn colTotalQuantity = new GridColumn() { Caption = "Total Quantity", Visible = true, FieldName = nameof(POSelection.TotalQuantity), Width = 150 };
                GridColumn colDeliveredQuantity = new GridColumn() { Caption = "Delivered Quantity", Visible = true, FieldName = nameof(POSelection.DeliveredQuantity), Width = 150 };
                GridColumn colCanceledQuantity = new GridColumn() { Caption = "Canceled Quantity", Visible = true, FieldName = nameof(POSelection.CanceledQuantity), Width = 150 };
                GridColumn colPendingQuantity = new GridColumn() { Caption = "Pending Quantity", Visible = true, FieldName = nameof(POSelection.PendingQuantity), Width = 150 };
                GridColumn colDocStatus = new GridColumn() { Caption = "Status", Visible = true, FieldName = nameof(POSelection.DocStatus), Width = 100 };
                GridColumn colTotalAmount = new GridColumn() { Caption = "Total Amount", Visible = true, FieldName = nameof(POSelection.TotalAmount), Width = 100 };
                GridColumn colFulfillment = new GridColumn() { Caption = "% Fulfillment", Visible = true, FieldName = nameof(POSelection.Fulfillment), Width = 100 };

                //Display Format
                colTotalAmount.DisplayFormat.FormatType = FormatType.Numeric;
                colTotalAmount.DisplayFormat.FormatString = "n2";

                colFulfillment.DisplayFormat.FormatType = FormatType.Numeric;
                colFulfillment.DisplayFormat.FormatString = "n2";

                colOriginalQuantity.DisplayFormat.FormatType = FormatType.Numeric;
                colOriginalQuantity.DisplayFormat.FormatString = "n0";

                colTotalQuantity.DisplayFormat.FormatType = FormatType.Numeric;
                colTotalQuantity.DisplayFormat.FormatString = "n0";

                colDeliveredQuantity.DisplayFormat.FormatType = FormatType.Numeric;
                colDeliveredQuantity.DisplayFormat.FormatString = "n0";

                colCanceledQuantity.DisplayFormat.FormatType = FormatType.Numeric;
                colCanceledQuantity.DisplayFormat.FormatString = "n0";

                colPendingQuantity.DisplayFormat.FormatType = FormatType.Numeric;
                colPendingQuantity.DisplayFormat.FormatString = "n0";

                //Add columns to grid root view
                gridViewLines.Columns.Add(colPoNumber);
                gridViewLines.Columns.Add(colSupplier);
                gridViewLines.Columns.Add(colOrderDate);
                gridViewLines.Columns.Add(colDeliveryStart);
                gridViewLines.Columns.Add(colDeleveryEnd);
                gridViewLines.Columns.Add(colOriginalQuantity);
                gridViewLines.Columns.Add(colTotalQuantity);
                gridViewLines.Columns.Add(colDeliveredQuantity);
                gridViewLines.Columns.Add(colCanceledQuantity);
                gridViewLines.Columns.Add(colPendingQuantity);
                gridViewLines.Columns.Add(colDocStatus);
                gridViewLines.Columns.Add(colTotalAmount);
                gridViewLines.Columns.Add(colFulfillment);

                //Events
                gridViewLines.DoubleClick += GridViewLines_DoubleClick;

            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Validate
        private bool IsValidFilter()
        {
            try
            {
                if ( (dateEditPODateIni.EditValue != null && dateEditPODateEnd.EditValue == null) ||
                     (dateEditPODateIni.EditValue == null && dateEditPODateEnd.EditValue != null))
                {
                    XtraMessageBox.Show("You must select both dates", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (dateEditPODateIni.DateTime > dateEditPODateEnd.DateTime)
                {
                    XtraMessageBox.Show("End date must be greater than start date", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        #region Open
        private void OpenPoForm(POSelection po)
        {
            try
            {
                //Check if is already open
                PurchaseOrder purchaseOrderForm =
                    Application.OpenForms.OfType<PurchaseOrder>()
                    .Where(pre => pre.Name == "PurchaseOrder")
                    .SingleOrDefault();

                if (purchaseOrderForm != null)
                    purchaseOrderForm.Close();

                purchaseOrderForm = new PurchaseOrder();
                purchaseOrderForm.InitData(po.PoNumber);

                purchaseOrderForm.MdiParent = MdiParent;
                purchaseOrderForm.ShowIcon = false;
                purchaseOrderForm.Show();
                purchaseOrderForm.WindowState = FormWindowState.Maximized;
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