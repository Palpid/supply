using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
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

namespace HKSupply.Forms.Master
{
    public partial class PaymentTermsManagement : RibbonFormBase
    {
        #region Enums
        private enum ePaymentTermsColumns
        {
            IdPaymentTerms,
            Description,
            DescriptionZh
        }
        #endregion

        #region Private Members
        List<PaymentTerms> _modifiedPaymentTerms = new List<PaymentTerms>();
        List<PaymentTerms> _createdPaymentTerms = new List<PaymentTerms>();
        #endregion

        #region Constructor
        public PaymentTermsManagement()
        {
            InitializeComponent();

            try
            {
                ConfigureRibbonActions();
                SetUpGrdPaymentTerms();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Ribbon
        private void ConfigureRibbonActions()
        {
            try
            {
                var actions = GlobalSetting.FunctionalitiesRoles.FirstOrDefault(fr => fr.Functionality.FormName.Equals(Name));
                Read = actions.Read;
                New = actions.New;
                Modify = actions.Modify;
                RestoreInitState();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void bbiCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            base.bbiCancel_ItemClick(sender, e);

            try
            {
                LoadAllPaymentTerms();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public override void bbiEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            base.bbiEdit_ItemClick(sender, e);

            try
            {
                ConfigureRibbonActionsEditing();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public override void bbiNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            base.bbiNew_ItemClick(sender, e);

            try
            {
                ConfigureActionsStackViewCreating();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public override void bbiSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            base.bbiSave_ItemClick(sender, e);

            try
            {
                bool res = false;

                if (IsValidPaymentTerms() == false)
                    return;

                DialogResult result = XtraMessageBox.Show(GlobalSetting.ResManager.GetString("SaveChanges"), "", MessageBoxButtons.YesNo);

                if (result != DialogResult.Yes)
                    return;

                Cursor = Cursors.WaitCursor;

                if (CurrentState == ActionsStates.Edit)
                {
                    if (_modifiedPaymentTerms.Count() == 0)
                    {
                        MessageBox.Show(GlobalSetting.ResManager.GetString("NoPendingChanges"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        res = UpdatePaymentTerms();
                    }
                }
                else if (CurrentState == ActionsStates.New)
                {
                    res = CreatePaymentTerm();
                }

                if (res == true)
                {
                    MessageBox.Show(GlobalSetting.ResManager.GetString("SaveSuccessfully"));
                    LoadAllPaymentTerms();
                    RestoreInitState();
                }

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

        #region Form Events
        private void PaymentTermsManagement_Load(object sender, EventArgs e)
        {
            try
            {
                LoadAllPaymentTerms();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Grid Events

        void rootgridViewPaymentTerms_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;

                if (CurrentState == ActionsStates.Edit)
                {
                    PaymentTerms tmpPaymentTerms = new PaymentTerms();

                    object idPaymentTerms = view.GetRowCellValue(view.FocusedRowHandle, ePaymentTermsColumns.IdPaymentTerms.ToString());
                    object description = view.GetRowCellValue(view.FocusedRowHandle, ePaymentTermsColumns.Description.ToString());
                    object descriptionZh = view.GetRowCellValue(view.FocusedRowHandle, ePaymentTermsColumns.DescriptionZh.ToString());


                    tmpPaymentTerms.IdPaymentTerms = (idPaymentTerms ?? string.Empty).ToString();
                    tmpPaymentTerms.Description = (description ?? string.Empty).ToString();
                    tmpPaymentTerms.DescriptionZh = (descriptionZh ?? string.Empty).ToString();

                    AddModifiedPaymentTermToList(tmpPaymentTerms);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void rootgridViewPaymentTerms_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                if (view.FocusedColumn.FieldName == ePaymentTermsColumns.Description.ToString() ||
                    view.FocusedColumn.FieldName == ePaymentTermsColumns.IdPaymentTerms.ToString())
                {
                    if (string.IsNullOrEmpty(e.Value as string))
                    {
                        e.Valid = false;
                        e.ErrorText = GlobalSetting.ResManager.GetString("FieldRequired");
                    }

                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #endregion

        #region Private Methods

        private void SetUpGrdPaymentTerms()
        {
            try
            {
                //hide group panel.
                rootgridViewPaymentTerms.OptionsView.ShowGroupPanel = false;
                rootgridViewPaymentTerms.OptionsCustomization.AllowGroup = false;
                rootgridViewPaymentTerms.OptionsCustomization.AllowColumnMoving = false;

                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                rootgridViewPaymentTerms.OptionsView.ColumnAutoWidth = false;
                rootgridViewPaymentTerms.HorzScrollVisibility = ScrollVisibility.Auto;

                //Columns definition
                GridColumn colIdPaymentTerms = new GridColumn() { Caption = "Id Payment Terms", Visible = true, FieldName = ePaymentTermsColumns.IdPaymentTerms.ToString(), Width = 150 };
                GridColumn colDescription = new GridColumn() { Caption = "Description", Visible = true, FieldName = ePaymentTermsColumns.Description.ToString(), Width = 300 };
                GridColumn colDescriptionZh = new GridColumn() { Caption = "Description (Chinese)", Visible = true, FieldName = ePaymentTermsColumns.DescriptionZh.ToString(), Width = 300 };

                //add columns to grid root view
                rootgridViewPaymentTerms.Columns.Add(colIdPaymentTerms);
                rootgridViewPaymentTerms.Columns.Add(colDescription);
                rootgridViewPaymentTerms.Columns.Add(colDescriptionZh);

                //Events
                rootgridViewPaymentTerms.ValidatingEditor += rootgridViewPaymentTerms_ValidatingEditor;
                rootgridViewPaymentTerms.CellValueChanged += rootgridViewPaymentTerms_CellValueChanged;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        

        private void LoadAllPaymentTerms()
        {
            try
            {
                _modifiedPaymentTerms.Clear();
                _createdPaymentTerms.Clear();
                IEnumerable<PaymentTerms> paymentTerms = GlobalSetting.PaymentTermsService.GetPaymentTerms();

                xgrdPaymentTerms.DataSource = paymentTerms;

                rootgridViewPaymentTerms.Columns[ePaymentTermsColumns.IdPaymentTerms.ToString()].OptionsColumn.AllowEdit = false;
                rootgridViewPaymentTerms.Columns[ePaymentTermsColumns.Description.ToString()].OptionsColumn.AllowEdit = false;
                rootgridViewPaymentTerms.Columns[ePaymentTermsColumns.DescriptionZh.ToString()].OptionsColumn.AllowEdit = false;

                //TODO: gestion de estilos del grid
                rootgridViewPaymentTerms.Columns[ePaymentTermsColumns.IdPaymentTerms.ToString()].AppearanceCell.ForeColor = Color.Black;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ConfigureRibbonActionsEditing()
        {
            try
            {
                //Allow edit some columns
                rootgridViewPaymentTerms.Columns[ePaymentTermsColumns.Description.ToString()].OptionsColumn.AllowEdit = true;
                rootgridViewPaymentTerms.Columns[ePaymentTermsColumns.DescriptionZh.ToString()].OptionsColumn.AllowEdit = true;

                //no edit column
                rootgridViewPaymentTerms.Columns[ePaymentTermsColumns.IdPaymentTerms.ToString()].OptionsColumn.AllowEdit = false;
                //TODO: gestion de estilos del grid
                rootgridViewPaymentTerms.Columns[ePaymentTermsColumns.IdPaymentTerms.ToString()].AppearanceCell.ForeColor = Color.Gray;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        private void ConfigureActionsStackViewCreating()
        {
            try
            {
                _createdPaymentTerms.Add(new PaymentTerms());
                xgrdPaymentTerms.DataSource = null;
                xgrdPaymentTerms.DataSource = _createdPaymentTerms;
                //Allow edit all columns
                rootgridViewPaymentTerms.Columns[ePaymentTermsColumns.IdPaymentTerms.ToString()].OptionsColumn.AllowEdit = true;
                rootgridViewPaymentTerms.Columns[ePaymentTermsColumns.Description.ToString()].OptionsColumn.AllowEdit = true;
                rootgridViewPaymentTerms.Columns[ePaymentTermsColumns.DescriptionZh.ToString()].OptionsColumn.AllowEdit = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AddModifiedPaymentTermToList(PaymentTerms modifiedPaymentTerm)
        {
            try
            {
                var paymentTerm = _modifiedPaymentTerms.FirstOrDefault(a => a.IdPaymentTerms.Equals(modifiedPaymentTerm.IdPaymentTerms));
                if (paymentTerm == null)
                {
                    _modifiedPaymentTerms.Add(modifiedPaymentTerm);
                }
                else
                {
                    paymentTerm.Description = modifiedPaymentTerm.Description;
                    paymentTerm.DescriptionZh = modifiedPaymentTerm.DescriptionZh;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsValidPaymentTerms()
        {
            try
            {
                if (CurrentState == ActionsStates.Edit)
                    return IsValidModifiedPaymentTerms();
                else if (CurrentState == ActionsStates.New)
                    return IsValidCreatedPaymentTerms();

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsValidModifiedPaymentTerms()
        {
            try
            {
                foreach (var paymentTerms in _modifiedPaymentTerms)
                {
                    if (string.IsNullOrEmpty(paymentTerms.Description))
                    {
                        XtraMessageBox.Show(GlobalSetting.ResManager.GetString("FieldRequired"), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsValidCreatedPaymentTerms()
        {
            try
            {
                foreach (var paymentTerms in _createdPaymentTerms)
                {
                    if (string.IsNullOrEmpty(paymentTerms.Description) || string.IsNullOrEmpty(paymentTerms.IdPaymentTerms))
                    {
                        MessageBox.Show(GlobalSetting.ResManager.GetString("FieldRequired"), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool UpdatePaymentTerms()
        {
            try
            {
                return GlobalSetting.PaymentTermsService.UpdatePaymentTerms(_modifiedPaymentTerms);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool CreatePaymentTerm()
        {
            try
            {
                GlobalSetting.PaymentTermsService.NewPaymentTerm(_createdPaymentTerms.FirstOrDefault());
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
