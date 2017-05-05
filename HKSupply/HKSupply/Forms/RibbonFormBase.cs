using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars;
using System.ComponentModel.DataAnnotations;

namespace HKSupply.Forms
{
    public partial class RibbonFormBase : DevExpress.XtraBars.Ribbon.RibbonForm
    {

        #region Enums
        public enum ActionsStates
        {
            OnlyRead,
            OnlyEdit,
            OnlyEditNew,
            Edit,
            New
        }
        #endregion

        #region Private Members

        private ActionsStates _currentState;

        #endregion

        #region Public Properties

        public bool Read { get; set; }
        public bool New { get; set; }
        public bool Modify { get; set; }

        public bool ShowPrintPreview { get; set; }
        public bool ShowExportExcel { get; set; }
        public bool ShowExportCsv { get; set; }

        public string ExportExcelFile { get; set; }
        public string ExportCsvFile { get; set; }

        public ActionsStates CurrentState 
        { 
            get { return _currentState; }
            set 
            {
                _currentState = value;
                ConfigureByState(_currentState);
            }
        }

        #endregion

        #region Constructor
        public RibbonFormBase()
        {
            InitializeComponent();

            ConfigureRibbonEvents();

            ConfigurePrintExportOptions(); //Si no se ha definido lo contrario por defecto no mostramos este panel
        }
        #endregion

        #region Public Functions
        public void RestoreInitState()
        {
            ConfigureActions();
        }

        public void ConfigurePrintExportOptions()
        {
            bbiPrintPreview.Visibility = (ShowPrintPreview ? BarItemVisibility.Always : BarItemVisibility.Never);
            bbiExportExcel.Visibility = (ShowExportExcel ? BarItemVisibility.Always : BarItemVisibility.Never);
            bbiExportCsv.Visibility = (ShowExportCsv ? BarItemVisibility.Always : BarItemVisibility.Never);

            if (ShowPrintPreview == false && ShowExportExcel == false && ShowExportCsv == false)
                ribbonPageGroup2.Visible = false;
            else
                ribbonPageGroup2.Visible = true;
        }
        #endregion

        #region Private Functions

        private void ConfigureActions()
        {
            if (Read == true && New == false && Modify == false)
                ConfigureByState(ActionsStates.OnlyRead);
            else if (Read == true && New == true && Modify == true)
                ConfigureByState(ActionsStates.OnlyEditNew);
            else if (Read == true && New == false && Modify == true)
                ConfigureByState(ActionsStates.OnlyEdit);
        }

        private void ConfigureByState(ActionsStates state)
        {
            _currentState = state;
            switch (state)
            {
                case ActionsStates.OnlyRead:
                    ribbonPageGroup1.Visible = false;
                    break;
                case ActionsStates.OnlyEdit:
                    ribbonPageGroup1.Visible = true;
                    bbiEdit.Visibility = BarItemVisibility.Always;
                    bbiNew.Visibility = BarItemVisibility.Never;
                    bbiSave.Visibility = BarItemVisibility.Never;
                    bbiCancel.Visibility = BarItemVisibility.Never;

                    bbiExportExcel.Enabled = true;
                    bbiExportCsv.Enabled = true;
                    bbiPrintPreview.Enabled = true;

                    break;
                case ActionsStates.OnlyEditNew:
                    ribbonPageGroup1.Visible = true;
                    bbiEdit.Visibility = BarItemVisibility.Always;
                    bbiNew.Visibility = BarItemVisibility.Always;
                    bbiSave.Visibility = BarItemVisibility.Never;
                    bbiCancel.Visibility = BarItemVisibility.Never;

                    bbiExportExcel.Enabled = true;
                    bbiExportCsv.Enabled = true;
                    bbiPrintPreview.Enabled = true;

                    break;
                case ActionsStates.Edit:
                case ActionsStates.New:
                    ribbonPageGroup1.Visible = true;
                    bbiEdit.Visibility = BarItemVisibility.Never;
                    bbiNew.Visibility = BarItemVisibility.Never;
                    bbiSave.Visibility = BarItemVisibility.Always;
                    bbiCancel.Visibility = BarItemVisibility.Always;

                    bbiExportExcel.Enabled = false;
                    bbiExportCsv.Enabled = false;
                    bbiPrintPreview.Enabled = false;
                    break;
            }
        }

        private void ConfigureRibbonEvents()
        {
            try
            {
                bbiEdit.ItemClick += bbiEdit_ItemClick;
                bbiNew.ItemClick += bbiNew_ItemClick;
                bbiCancel.ItemClick += bbiCancel_ItemClick;
                bbiSave.ItemClick += bbiSave_ItemClick;
                bbiClose.ItemClick += bbiClose_ItemClick;

                bbiPrintPreview.ItemClick += bbiPrintPreview_ItemClick;
                bbiExportExcel.ItemClick += bbiExportExcel_ItemClick;
                bbiExportCsv.ItemClick += bbiExportCsv_ItemClick;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Events

        #region Task Buttons

        public virtual void bbiSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                //Lanzamos el validate para finalizar cualquier accion de edición del formulario, ya que este control no tiene focus nunca y no lo lanza por si mismo
                Validate();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual void bbiCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                RestoreInitState();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual void bbiNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                CurrentState = ActionsStates.New;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual void bbiEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                CurrentState = ActionsStates.Edit;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void bbiClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Print and Export Buttons

        public virtual void bbiPrintPreview_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        public virtual void bbiExportExcel_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog()
                {
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    Filter = "xlsx files (*.xlsx)|*.xlsx",
                    FilterIndex = 1,
                    RestoreDirectory = true,
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ExportExcelFile = saveFileDialog.FileName;
                }
                else
                {
                    ExportExcelFile = string.Empty;
                }
            }
            catch
            {
                throw;
            }
        }

        public virtual void bbiExportCsv_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog()
                {
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    Filter = "csv files (*.csv)|*.csv",
                    FilterIndex = 1,
                    RestoreDirectory = true,
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ExportCsvFile = saveFileDialog.FileName;
                }
                else
                {
                    ExportCsvFile = string.Empty;
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