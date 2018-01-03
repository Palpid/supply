using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using HKSupply.General;
using HKSupply.Helpers;
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

namespace HKSupply.Forms.Supply.SupplyMaterials
{
    public partial class ReceiptAndQuality : RibbonFormBase
    {

        #region Private Members

        Font _labelDefaultFontBold = new Font("SourceSansProRegular", 8, FontStyle.Bold);
        Font _labelDefaultFont = new Font("SourceSansProRegular", 8, FontStyle.Regular);

        List<SupplyStatus> _supplyStatusList;
        List<Supplier> _suppliersList;

        DocHead _docHeadPK;
        BindingList<DocLine> _docLinesList;

        bool _isLoadingPacking = false;

        #endregion

        #region Constructor
        public ReceiptAndQuality()
        {
            InitializeComponent();

            try
            {
                Cursor = Cursors.WaitCursor;

                ConfigureRibbonActions();
                LoadAuxList();
                SetUpLabels();
                SetObjectsReadOnly();
                SetUpSearchLookUpEdit();
                SetUpEvents();
                SetUpGrdLinesPL();
                SetVisiblePropertyByState();
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

        #region Ribbons
        private void ConfigureRibbonActions()
        {
            try
            {
                //Task Buttons
                SetActions();
                RestoreInitState();
                //Print and export buttons
                EnablePrintPreview = false;
                EnableExportExcel = false;
                EnableExportCsv = false;
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

        public override void bbiCancel_ItemClick(object sender, ItemClickEventArgs e)
        {
            base.bbiCancel_ItemClick(sender, e);
        }

        public override void bbiEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            base.bbiEdit_ItemClick(sender, e);
        }

        public override void bbiNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            base.bbiNew_ItemClick(sender, e);
        }

        public override void BbiSaveLayout_ItemClick(object sender, ItemClickEventArgs e)
        {
            base.BbiSaveLayout_ItemClick(sender, e);
        }

        #endregion

        #region Form Events
        private void ReceiptAndQuality_Load(object sender, EventArgs e)
        {

        }

        private void SbSearch_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SbFinishQC_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void DateEditPKDocDate_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                lblPKDocDateWeek.Text = dateEditPKDocDate.DateTime.GetWeek().ToString();

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DateEditPKDelivery_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                lblPKDeliveryWeek.Text = dateEditPKDelivery.DateTime.GetWeek().ToString();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtPKNumber_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (CurrentState == ActionsStates.Edit || CurrentState == ActionsStates.New)
                    return;

                if (e.KeyCode == Keys.Enter && string.IsNullOrEmpty(txtPKNumber.Text) == false)
                {
                    SearchPK();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtPKNumber_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (_isLoadingPacking)
                    return;

                ResetPK();
                ResetForm(resetPkNumber: false);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtSupplierReference_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (CurrentState == ActionsStates.Edit || CurrentState == ActionsStates.New)
                    return;

                if (e.KeyCode == Keys.Enter && string.IsNullOrEmpty(txtSupplierReference.Text) == false)
                {
                    SearchPK();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtSupplierReference_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (_isLoadingPacking)
                    return;

                ResetPK();
                ResetForm(resetSupplierReference: false);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Private Methods

        #region Load/Resets

        private void LoadAuxList()
        {
            try
            {
                _suppliersList = GlobalSetting.SupplierService.GetSuppliers().Where(a => a.Factory == false).ToList();
                _supplyStatusList = GlobalSetting.SupplyDocsService.GetSupplyStatus();
            }
            catch
            {
                throw;
            }
        }

        private void SearchPK()
        {
            try
            {
                _isLoadingPacking = true;
                ResetPK();

                if (string.IsNullOrEmpty(txtPKNumber.Text) == false)
                {
                    ResetForm(resetPkNumber: false);
                    string pkNumber = txtPKNumber.Text;
                    _docHeadPK = GlobalSetting.SupplyDocsService.GetDoc(pkNumber);
                }
                else if (string.IsNullOrEmpty(txtSupplierReference.Text) == false)
                {
                    ResetForm(resetSupplierReference: false);
                    string supplierReference = txtSupplierReference.Text;
                    var docs = GlobalSetting.SupplyDocsService.GetDocsByReference(supplierReference);
                    if (docs.Count == 1)
                        _docHeadPK = docs.FirstOrDefault();
                    else
                    {
                        using (DialogForms.SelectDocs form = new DialogForms.SelectDocs())
                        {
                            form.InitData(docs);
                            if (form.ShowDialog() == DialogResult.OK)
                            {
                                _docHeadPK = form.SelectedDoc;
                            }
                        }
                    }
                }
                

                if (_docHeadPK == null)
                {
                    XtraMessageBox.Show("No Data Found", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (_docHeadPK.IdSupplyDocType != Constants.SUPPLY_DOCTYPE_PL)
                {
                    XtraMessageBox.Show("Document is not a Packing List", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (_docHeadPK.IdCustomer != Constants.ETNIA_HK_COMPANY_CODE)
                {
                    XtraMessageBox.Show("Packing List is not from material supplier", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    LoadPK(); 
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                _isLoadingPacking = false;
            }
        }

        private void LoadPK()
        {
            try
            {
                var supplier = _suppliersList.Where(a => a.IdSupplier.Equals(_docHeadPK.IdSupplier)).FirstOrDefault();

                //***** Header *****/
                txtPKNumber.Text = _docHeadPK.IdDoc;
                lbltxtStatus.Visible = true;
                lbltxtStatus.Text = _docHeadPK.IdSupplyStatus;
                slueSupplier.EditValue = _docHeadPK.IdSupplier;

                dateEditPKDocDate.EditValue = _docHeadPK.DocDate;
                dateEditPKDelivery.EditValue = _docHeadPK.DeliveryDate;

                lblPKDocDateWeek.Text = dateEditPKDocDate.DateTime.GetWeek().ToString();
                lblPKDeliveryWeek.Text = dateEditPKDelivery.DateTime.GetWeek().ToString();

                txtSupplierReference.Text = _docHeadPK.ManualReference;
                memoEditRemarks.Text = _docHeadPK.Remarks;

                //***** GridLines *****/
                _docLinesList = new BindingList<DocLine>(_docHeadPK.Lines);
                xgrdLinesPL.DataSource = null;
                xgrdLinesPL.DataSource = _docLinesList;

            }
            catch
            {
                throw;
            }
        }

        private void ResetPK()
        {
            try
            {
                _docLinesList = null;
                _docHeadPK = null;
                xgrdLinesPL.DataSource = null;

            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Setup Form Objects

        private void SetUpLabels()
        {
            try
            {
                /********* Fonts **********/
                //Header 
                lblPKNumber.Font = _labelDefaultFontBold;
                lbltxtStatus.Font = _labelDefaultFontBold;
                lblDate.Font = _labelDefaultFontBold;
                lblWeek.Font = _labelDefaultFontBold;
                lblPKDocDate.Font = _labelDefaultFontBold;
                lblPKDelivery.Font = _labelDefaultFontBold;
                lblSupplier.Font = _labelDefaultFontBold;
                lblPKDocDateWeek.Font = _labelDefaultFont;
                lblPKDeliveryWeek.Font = _labelDefaultFont;
                txtPKNumber.Font = _labelDefaultFontBold;
                lblSupplierReference.Font = _labelDefaultFontBold;
                lblRemarks.Font = _labelDefaultFont;

                /********* Texts **********/
                //Headers
                lblPKNumber.Text = "PK Number";
                lbltxtStatus.Text = string.Empty;
                lblDate.Text = "Date";
                lblWeek.Text = "Week";
                lblPKDocDate.Text = "DATE";
                lblPKDelivery.Text = "DELIVERY";
                lblSupplier.Text = "SUPPLIER";
                lblPKDocDateWeek.Text = string.Empty;
                lblPKDeliveryWeek.Text = string.Empty;
                lblSupplierReference.Text = "Supplier Reference";
                lblRemarks.Text = "Remarks";

                /********* Align **********/
                //Headers
                lbltxtStatus.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                lblPKDocDateWeek.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                lblPKDeliveryWeek.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                txtPKNumber.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;

                /********* BackColor **********/
                txtPKNumber.Properties.Appearance.BackColor = Color.CadetBlue;
                txtPKNumber.Properties.Appearance.BackColor2 = Color.CadetBlue;

            }
            catch
            {
                throw;
            }
        }

        private void SetObjectsReadOnly()
        {
            try
            {
                dateEditPKDocDate.ReadOnly = true;
                dateEditPKDelivery.ReadOnly = true;
                memoEditRemarks.ReadOnly = true;
                slueSupplier.ReadOnly = true;
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
                slueSupplier.Properties.NullText = string.Empty;
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
                //TODO
                sbSearch.Click += SbSearch_Click;
                sbFinishQC.Click += SbFinishQC_Click;
                dateEditPKDocDate.EditValueChanged += DateEditPKDocDate_EditValueChanged;
                dateEditPKDelivery.EditValueChanged += DateEditPKDelivery_EditValueChanged;
                txtPKNumber.KeyDown += TxtPKNumber_KeyDown;
                txtPKNumber.EditValueChanged += TxtPKNumber_EditValueChanged;
                txtSupplierReference.KeyDown += TxtSupplierReference_KeyDown;
                txtSupplierReference.EditValueChanged += TxtSupplierReference_EditValueChanged;
            }
            catch
            {
                throw;
            }
        }

        private void SetUpGrdLinesPL()
        {
            try
            {
                //Activar que se alternen los colores de las filas del grid
                gridViewLinesPL.OptionsView.EnableAppearanceOddRow = true;
                gridViewLinesPL.OptionsView.EnableAppearanceEvenRow = true;

                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                gridViewLinesPL.OptionsView.ColumnAutoWidth = false;
                gridViewLinesPL.HorzScrollVisibility = ScrollVisibility.Auto;

                //Hacer todo el grid no editable
                gridViewLinesPL.OptionsBehavior.Editable = false;

                //Hide grouping panel
                gridViewLinesPL.OptionsView.ShowGroupPanel = false;

                //Column Definition
                GridColumn colIdItemBcn = new GridColumn() { Caption = "Item Code", Visible = true, FieldName = nameof(DocLine.IdItemBcn), Width = 200 };
                GridColumn colDescription = new GridColumn() { Caption = "Description", Visible = true, FieldName = nameof(DocLine.ItemDesc), Width = 350 };
                GridColumn colIdItemGroup = new GridColumn() { Caption = "Type", Visible = true, FieldName = nameof(DocLine.IdItemGroup), Width = 100 };
                GridColumn colQuantity = new GridColumn() { Caption = "Order Quantity", Visible = true, FieldName = nameof(DocLine.Quantity), Width = 110 };
                GridColumn colDeliveredQuantity = new GridColumn() { Caption = "Delivered Qty", Visible = true, FieldName = nameof(DocLine.DeliveredQuantity), Width = 110 };
                GridColumn colRejectedQuantity = new GridColumn() { Caption = "Rejected Qty", Visible = true, FieldName = nameof(DocLine.RejectedQuantity), Width = 110 };
                GridColumn colUnit = new GridColumn() { Caption = "Unit", Visible = true, FieldName = nameof(DocLine.ItemUnit), Width = 85 };
                GridColumn colIdIdSupplyStatus = new GridColumn() { Caption = "Status", Visible = true, FieldName = nameof(DocLine.IdSupplyStatus), Width = 75 };
                GridColumn colRemarks = new GridColumn() { Caption = "Remarks", Visible = true, FieldName = nameof(DocLine.Remarks), Width = 300 };

                //Display Format
                colQuantity.DisplayFormat.FormatType = FormatType.Numeric;
                colQuantity.DisplayFormat.FormatString = "n3";

                colDeliveredQuantity.DisplayFormat.FormatType = FormatType.Numeric;
                colDeliveredQuantity.DisplayFormat.FormatString = "n3";

                colRejectedQuantity.DisplayFormat.FormatType = FormatType.Numeric;
                colRejectedQuantity.DisplayFormat.FormatString = "n3";

                //Edit Repositories
                RepositoryItemTextEdit ritxt3Dec = new RepositoryItemTextEdit();
                ritxt3Dec.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                ritxt3Dec.Mask.EditMask = "F3";
                ritxt3Dec.AllowNullInput = DefaultBoolean.True;

                colDeliveredQuantity.ColumnEdit = ritxt3Dec;
                colRejectedQuantity.ColumnEdit = ritxt3Dec;

                RepositoryItemSearchLookUpEdit riSupplyStatus = new RepositoryItemSearchLookUpEdit()
                {
                    DataSource = _supplyStatusList,
                    ValueMember = nameof(SupplyStatus.IdSupplyStatus),
                    DisplayMember = nameof(SupplyStatus.Description),
                    ShowClearButton = false,
                    NullText = string.Empty,
                };
                colIdIdSupplyStatus.ColumnEdit = riSupplyStatus;

                //Add columns to grid root view
                gridViewLinesPL.Columns.Add(colIdItemBcn);
                gridViewLinesPL.Columns.Add(colDescription);
                gridViewLinesPL.Columns.Add(colIdItemGroup);
                gridViewLinesPL.Columns.Add(colQuantity);
                gridViewLinesPL.Columns.Add(colDeliveredQuantity);
                gridViewLinesPL.Columns.Add(colRejectedQuantity);
                gridViewLinesPL.Columns.Add(colUnit);
                gridViewLinesPL.Columns.Add(colIdIdSupplyStatus);
                gridViewLinesPL.Columns.Add(colRemarks);

                //Events
                //TODO

            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Aux

        private void SetVisiblePropertyByState()
        {
            try
            {
                switch (CurrentState)
                {

                    case ActionsStates.New:
                        sbFinishQC.Visible = false;
                        sbSearch.Visible = false;
                        lbltxtStatus.Visible = false;
                        break;

                    case ActionsStates.Edit:
                        sbFinishQC.Visible = true;
                        sbSearch.Visible = false;
                        lbltxtStatus.Visible = true;
                        break;

                    default:
                        sbFinishQC.Visible = false;
                        sbSearch.Visible = true;
                        lbltxtStatus.Visible = false;
                        break;
                }
            }
            catch
            {
                throw;
            }
        }

        private void ResetForm(bool resetPkNumber = true, bool resetSupplier = true, bool resetSupplierReference = true)
        {
            try
            {
                /********* Head *********/
                if (resetPkNumber) txtPKNumber.EditValue = null;
                lbltxtStatus.Text = string.Empty;
                dateEditPKDocDate.EditValue = null;
                dateEditPKDelivery.EditValue = null;
                lblPKDocDateWeek.Text = string.Empty;
                lblPKDeliveryWeek.Text = string.Empty;
                if (resetSupplier) slueSupplier.EditValue = null;
                if(resetSupplierReference) txtSupplierReference.EditValue = null;
                memoEditRemarks.EditValue = null;
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
