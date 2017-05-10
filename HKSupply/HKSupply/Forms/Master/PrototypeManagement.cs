using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.Utils;
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
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Columns;

namespace HKSupply.Forms.Master
{
    public partial class PrototypeManagement : RibbonFormBase
    {
        #region Private Members
        List<Prototype> _prototypesList;
        #endregion

        #region Constructor
        public PrototypeManagement()
        {
            InitializeComponent();

            try
            {
                ConfigureRibbonActions();
                SetUpGrdProtos();
                LoadPrototypesList();
            }
            catch(Exception ex)
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
                SetRibbonText($"{actions.Functionality.Category} > {actions.Functionality.FunctionalityName}");
                //Task Buttons
                Read = actions.Read;
                New = actions.New;
                Modify = actions.Modify;
                RestoreInitState();
                //Print and export buttons
                EnablePrintPreview = false;
                EnableExportExcel = true;
                EnableExportCsv = true;
                ConfigurePrintExportOptions();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void bbiExportExcel_ItemClick(object sender, ItemClickEventArgs e)
        {
            base.bbiExportExcel_ItemClick(sender, e);
        }

        public override void bbiExportCsv_ItemClick(object sender, ItemClickEventArgs e)
        {
            base.bbiExportCsv_ItemClick(sender, e);
        }

        #endregion

        #region Form Events

        private void PrototypesManagement_Load(object sender, EventArgs e)
        {

        }

        #endregion

        #region Private Methods

        #region SetUp Form Object

        private void SetUpGrdProtos()
        {
            try
            {
                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                rootGridViewProtos.OptionsView.ColumnAutoWidth = false;
                rootGridViewProtos.HorzScrollVisibility = ScrollVisibility.Auto;

                //hacer todo el grid no editable
                rootGridViewProtos.OptionsBehavior.Editable = false;

                //Columns definition
                GridColumn colIdPrototype = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Id"), Visible = true, FieldName = nameof(Prototype.IdPrototype), Width = 70 };
                GridColumn colPrototypeName = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Name"), Visible = true, FieldName = nameof(Prototype.PrototypeName), Width = 150 };
                GridColumn colIdPrototypeDescription = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Description"), Visible = true, FieldName = nameof(Prototype.PrototypeDescription), Width = 200 };
                GridColumn colPrototypeStatus = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Status"), Visible = true, FieldName = nameof(Prototype.PrototypeStatus), Width = 70 };
                GridColumn colDefaultSupplier = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("DefaultSupplier"), Visible = true, FieldName = $"{nameof(Prototype.DefaultSupplier)}.{nameof(Supplier.ContactName)}", Width = 150 };
                GridColumn colCaliber = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Caliber"), Visible = true, FieldName = nameof(Prototype.Caliber), Width = 70 };
                GridColumn colLaunchDate = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("LaunchDate"), Visible = true, FieldName = nameof(Prototype.LaunchDate), Width = 90 };
                GridColumn colCreateDate = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("CreateDate"), Visible = true, FieldName = nameof(Prototype.CreateDate), Width = 120 };

                //Display Format
                colLaunchDate.DisplayFormat.FormatType = FormatType.DateTime;
                colCreateDate.DisplayFormat.FormatType = FormatType.DateTime;

                colCaliber.DisplayFormat.FormatType = FormatType.Numeric;
                colCaliber.DisplayFormat.FormatString = "N2";

                //Addd columns to grid root view;
                rootGridViewProtos.Columns.Add(colIdPrototype);
                rootGridViewProtos.Columns.Add(colPrototypeName);
                rootGridViewProtos.Columns.Add(colIdPrototypeDescription);
                rootGridViewProtos.Columns.Add(colPrototypeStatus);
                rootGridViewProtos.Columns.Add(colDefaultSupplier);
                rootGridViewProtos.Columns.Add(colCaliber);
                rootGridViewProtos.Columns.Add(colLaunchDate);
                rootGridViewProtos.Columns.Add(colCreateDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Loads
        private void LoadPrototypesList()
        {
            try
            {
                _prototypesList = GlobalSetting.PrototypeService.GetPrototypes();
                xgrdProtos.DataSource = _prototypesList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #endregion
    }
}
