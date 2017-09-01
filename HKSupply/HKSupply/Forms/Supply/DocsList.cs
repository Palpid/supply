﻿using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
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
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;

namespace HKSupply.Forms.Supply
{
    public partial class DocsList : RibbonFormBase
    {

        #region Private Members

        Font _labelDefaultFontBold = new Font("SourceSansProRegular", 8, FontStyle.Bold);
        Font _labelDefaultFont = new Font("SourceSansProRegular", 8, FontStyle.Regular);

        List<Supplier> _suppliersList;
        List<Customer> _customersList;
        List<SupplyStatus> _supplyStatusList;
        List<SupplyDocType> _supplyDocTypeList;
        List<DocHead> _docsList;
        #endregion

        #region Constructor
        public DocsList()
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

        public override void bbiExportExcel_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewLines.DataRowCount == 0)
            {
                MessageBox.Show(GlobalSetting.ResManager.GetString("NoDataSelected"));
                return;
            }

            //Abre el dialog de save as
            base.bbiExportExcel_ItemClick(sender, e);

            try
            {
                if (string.IsNullOrEmpty(ExportExcelFile) == false)
                {
                    gridViewLines.ExportToXlsx(ExportExcelFile);

                    DialogResult result = MessageBox.Show(GlobalSetting.ResManager.GetString("OpenFileQuestion"), "", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(ExportExcelFile);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public override void bbiExportCsv_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewLines.DataRowCount == 0)
            {
                MessageBox.Show(GlobalSetting.ResManager.GetString("NoDataSelected"));
                return;
            }

            //Abre el dialog de save as
            base.bbiExportCsv_ItemClick(sender, e);

            try
            {
                if (string.IsNullOrEmpty(ExportCsvFile) == false)
                {
                    gridViewLines.ExportToCsv(ExportCsvFile);

                    DialogResult result = MessageBox.Show(GlobalSetting.ResManager.GetString("OpenFileQuestion"), "", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(ExportCsvFile);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Form Events

        private void xtpDocsList_Click(object sender, EventArgs e)
        {

        }

        private void SbFilter_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValidFilter() == true)
                    LoadList();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GridViewLines_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                DocHead doc = view.GetRow(view.FocusedRowHandle) as DocHead;

                if (doc != null)
                {
                    OpenDocForm(doc);
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
                _customersList = GlobalSetting.CustomerService.GetCustomers();
                _supplyStatusList = GlobalSetting.SupplyDocsService.GetSupplyStatus();
                _supplyDocTypeList = GlobalSetting.SupplyDocsService.GetSupplyDocTypes();
            }
            catch
            {
                throw;
            }
        }
        private void LoadList()
        {
            try
            {
                string idSupplyDocType = (string)slueDocType.EditValue;
                string idDoc = txtDocNumber.Text;
                string idSupplyStatus = (string)slueStatus.EditValue;
                string idSupplier = (string)slueSupplier.EditValue;
                string idCustomer = (string)slueCustomer.EditValue;
                DateTime dateIni = dateEditPODateIni.DateTime;
                DateTime dateEnd = dateEditPODateEnd.DateTime;

                _docsList = GlobalSetting.SupplyDocsService.GetDocs(
                    idDoc: idDoc, 
                    idSupplier: idSupplier, 
                    idCustomer: idCustomer, 
                    docDateIni: dateIni, 
                    docDateEnd: dateEnd, 
                    IdSupplyDocType: idSupplyDocType,
                    idSupplyStatus: idSupplyStatus);

                xgrdLines.DataSource = null;

                if (_docsList.Count == 0)
                    XtraMessageBox.Show("No Data Found", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    xgrdLines.DataSource = _docsList;
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
                lblDocType.Font = _labelDefaultFontBold;
                lblDocNumber.Font = _labelDefaultFontBold;
                lblSupplier.Font = _labelDefaultFontBold;
                lblCustomer.Font = _labelDefaultFontBold;
                lblStatus.Font = _labelDefaultFontBold;
                lblDocDate.Font = _labelDefaultFontBold;
                lblAnd.Font = _labelDefaultFontBold;
                txtDocNumber.Font = _labelDefaultFontBold;

                /********* Texts **********/
                //Headers
                lblDocType.Text = "Type";
                lblDocNumber.Text = "Doc Number";
                lblSupplier.Text = "Supplier";
                lblCustomer.Text = "Customer";
                lblStatus.Text = "Status";
                lblDocDate.Text = "Doc. Date        between";
                lblAnd.Text = "and";
                txtDocNumber.Text = string.Empty;
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
                SetUpSlueCustomer();
                SetUpSlueStatus();
                SetUpSlueSupplyDocType();
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

        private void SetUpSlueCustomer()
        {
            try
            {
                slueCustomer.Properties.DataSource = _customersList;
                slueCustomer.Properties.ValueMember = nameof(Customer.IdCustomer);
                slueCustomer.Properties.DisplayMember = nameof(Customer.CustomerName);
                slueCustomer.Properties.View.Columns.AddField(nameof(Customer.IdCustomer)).Visible = true;
                slueCustomer.Properties.View.Columns.AddField(nameof(Customer.CustomerName)).Visible = true;
                slueCustomer.Properties.NullText = "Select Customer...";
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

        private void SetUpSlueSupplyDocType()
        {
            try
            {
                slueDocType.Properties.DataSource = _supplyDocTypeList;
                slueDocType.Properties.ValueMember = nameof(SupplyDocType.IdSupplyDocType);
                slueDocType.Properties.DisplayMember = nameof(SupplyDocType.Description);
                slueDocType.Properties.NullText = "Select Doc. Type...";
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

                //Deshabilitar el detalle para no mostrar las líneas del documento
                gridViewLines.OptionsDetail.EnableMasterViewMode = false;

                //Column Definition
                GridColumn colIdDoc = new GridColumn() { Caption = "Doc. Number", Visible = true, FieldName = nameof(DocHead.IdDoc), Width = 100 };
                GridColumn colIdSupplyDocType = new GridColumn() { Caption = "Type", Visible = true, FieldName = nameof(DocHead.IdSupplyDocType), Width = 150 };
                GridColumn colDocDate = new GridColumn() { Caption = "Doc. Date", Visible = true, FieldName = nameof(DocHead.DocDate), Width = 100 };
                GridColumn colDeliveryDate = new GridColumn() { Caption = "Delivery Date", Visible = true, FieldName = nameof(DocHead.DeliveryDate), Width = 100 };
                GridColumn colIdSupplyStatus = new GridColumn() { Caption = "Status", Visible = true, FieldName = nameof(DocHead.IdSupplyStatus), Width = 100 };
                GridColumn colIdSupplier = new GridColumn() { Caption = "Supplier", Visible = true, FieldName = nameof(DocHead.IdSupplier), Width = 100 };
                GridColumn colIdCustomer = new GridColumn() { Caption = "Customer", Visible = true, FieldName = nameof(DocHead.IdCustomer), Width = 100 };
                GridColumn colIdDocRelated = new GridColumn() { Caption = "Doc. Related", Visible = true, FieldName = nameof(DocHead.IdDocRelated), Width = 100 };

                //Repositories
                RepositoryItemSearchLookUpEdit riDocTypes = new RepositoryItemSearchLookUpEdit()
                {
                    DataSource = _supplyDocTypeList,
                    ValueMember = nameof(SupplyDocType.IdSupplyDocType),
                    DisplayMember = nameof(SupplyDocType.Description)
                };
                colIdSupplyDocType.ColumnEdit = riDocTypes;

                //Add columns to grid root view
                gridViewLines.Columns.Add(colIdDoc);
                gridViewLines.Columns.Add(colIdSupplyDocType);
                gridViewLines.Columns.Add(colDocDate);
                gridViewLines.Columns.Add(colDeliveryDate);
                gridViewLines.Columns.Add(colIdSupplyStatus);
                gridViewLines.Columns.Add(colIdSupplier);
                gridViewLines.Columns.Add(colIdCustomer);
                gridViewLines.Columns.Add(colIdDocRelated);

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
                if ((dateEditPODateIni.EditValue != null && dateEditPODateEnd.EditValue == null) ||
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
        private void OpenDocForm(DocHead doc)
        {
            try
            {
                switch (doc.IdSupplyDocType)
                {
                    case Constants.SUPPLY_DOCTYPE_PO:

                        //Check if is already open
                        PurchaseOrder purchaseOrderForm =
                            Application.OpenForms.OfType<PurchaseOrder>()
                            .Where(pre => pre.Name == "PurchaseOrder")
                            .SingleOrDefault();

                        if (purchaseOrderForm != null)
                            purchaseOrderForm.Close();

                        purchaseOrderForm = new PurchaseOrder();
                        purchaseOrderForm.InitData(doc.IdDoc);

                        purchaseOrderForm.MdiParent = MdiParent;
                        purchaseOrderForm.ShowIcon = false;
                        purchaseOrderForm.Show();
                        purchaseOrderForm.WindowState = FormWindowState.Maximized;

                        break;

                    case Constants.SUPPLY_DOCTYPE_SO:

                        SalesOrder salesOrderForm =
                            Application.OpenForms.OfType<SalesOrder>()
                            .Where(pre => pre.Name == nameof(SalesOrder))
                            .SingleOrDefault();

                        if (salesOrderForm != null)
                            salesOrderForm.Close();

                        salesOrderForm = new SalesOrder();
                        salesOrderForm.InitData(doc.IdDoc);

                        salesOrderForm.MdiParent = MdiParent;
                        salesOrderForm.ShowIcon = false;
                        salesOrderForm.Show();
                        salesOrderForm.WindowState = FormWindowState.Maximized;

                        break;

                    case Constants.SUPPLY_DOCTYPE_DN:

                        DeliveryNote deliveryNoteForm =
                            Application.OpenForms.OfType<DeliveryNote>()
                            .Where(pre => pre.Name == nameof(DeliveryNote))
                            .SingleOrDefault();

                        if (deliveryNoteForm != null)
                            deliveryNoteForm.Close();

                        deliveryNoteForm = new DeliveryNote();
                        deliveryNoteForm.InitData(doc.IdDoc);

                        deliveryNoteForm.MdiParent = MdiParent;
                        deliveryNoteForm.ShowIcon = false;
                        deliveryNoteForm.Show();
                        deliveryNoteForm.WindowState = FormWindowState.Maximized;

                        break;

                    //case Constants.SUPPLY_DOCTYPE_PK:

                    //    PackingList packingListForm =
                    //       Application.OpenForms.OfType<PackingList>()
                    //       .Where(pre => pre.Name == nameof(PackingList))
                    //       .SingleOrDefault();

                    //    if (packingListForm != null)
                    //        packingListForm.Close();

                    //    packingListForm = new PackingList();
                    //    packingListForm.InitData(doc.IdDoc);

                    //    packingListForm.MdiParent = MdiParent;
                    //    packingListForm.ShowIcon = false;
                    //    packingListForm.Show();
                    //    packingListForm.WindowState = FormWindowState.Maximized;
                    //    break;

                    case Constants.SUPPLY_DOCTYPE_INV:

                        Invoice invoiceForm =
                            Application.OpenForms.OfType<Invoice>()
                            .Where(pre => pre.Name == nameof(Invoice))
                            .SingleOrDefault();

                        if (invoiceForm != null)
                            invoiceForm.Close();

                        invoiceForm = new Invoice();
                        invoiceForm.InitData(doc.IdDoc);

                        invoiceForm.MdiParent = MdiParent;
                        invoiceForm.ShowIcon = false;
                        invoiceForm.Show();
                        invoiceForm.WindowState = FormWindowState.Maximized;

                        break;
                }
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
