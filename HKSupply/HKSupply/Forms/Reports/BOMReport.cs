using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using HKSupply.DB;
using HKSupply.General;
using HKSupply.Models;
using HKSupply.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HKSupply.Forms.Reports
{
    public partial class BOMReport : RibbonFormBase
    {

        #region Enums

        private enum eGridColumns
        {
            ID_BOM,
            DESCRIPTION, 
            ID_ITEM_BCN, 
            ID_SUPPLIER, 
            PHOTO_URL
        }

        #endregion

        #region Private Members

        List<ItemEy> _itemBcnList;
        List<Model> _modelList;
        List<Supplier> _suppliersList;
        List<StatusCial> _statusCialList;

        DataTable _dtGrid = new DataTable();

        bool _clearingSlue = false;
        #endregion

        #region Constructor
        public BOMReport()
        {
            InitializeComponent();

            try
            {
                LoadAuxList();
                SetUpSlueSupplier();
                SetUpSlueModel();
                SetUpSlueItemEy();
                SetUpSlueStatusCial();
                SetupGrdList();

            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Ribbon
        #endregion

        #region Form Events
        private void BOMReport_Load(object sender, EventArgs e)
        {

        }

        private void sbFilter_Click(object sender, EventArgs e)
        {
            try
            {
                if(slueItem.EditValue != null || slueModel.EditValue != null || slueSupplier.EditValue != null)
                {
                    LoadGrid();
                }
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void sbShowReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (slueItem.EditValue != null || slueModel.EditValue != null || slueSupplier.EditValue != null)
                {
                    OpenReport();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Private Methods

        #region Load
        private void LoadAuxList()
        {
            try
            {
                _suppliersList = GlobalSetting.SupplierService.GetSuppliers();
                _itemBcnList = GlobalSetting.ItemEyService.GetItems();
                _statusCialList = GlobalSetting.StatusCialService.GetStatusCial();

                //TODO
                using (var db = new HKSupplyContext())
                {
                    _modelList = db.Models.ToList();
                }

            }
            catch
            {
                throw;
            }
        }

        private void LoadGrid()
        {
            try
            {
                //TODO
                StringBuilder query = new StringBuilder();
                query.Append($"EXEC GET_BOM_REPORT ");
                query.Append($"'{Constants.ITEMS_PHOTOSWEB_PATH + Constants.ITEM_PHOTOWEB_FOLDER}',");
                query.Append($"'{(string)slueModel.EditValue}',");
                query.Append($"'{(string)slueItem.EditValue}',");
                query.Append($"'{(string)slueSupplier.EditValue}'");

                using (var db = new HKSupplyContext())
                {
                    DataTable dataTable = db.DataTable(query.ToString());
                    xgrdList.DataSource = dataTable;
                }

            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Setup Forms Objects

        private void SetUpSlueSupplier()
        {
            try
            {
                slueSupplier.Properties.DataSource = _suppliersList;
                slueSupplier.Properties.ValueMember = nameof(Supplier.IdSupplier);
                slueSupplier.Properties.DisplayMember = nameof(Supplier.SupplierName);
                slueSupplier.Properties.View.Columns.AddField(nameof(Supplier.IdSupplier)).Visible = true;
                slueSupplier.Properties.View.Columns.AddField(nameof(Supplier.SupplierName)).Visible = true;
                slueSupplier.Properties.NullText = "Select supplier...";
            }
            catch
            {
                throw;
            }
        }

        private void SetUpSlueModel()
        {
            try
            {
                slueModel.Properties.DataSource = _modelList;
                slueModel.Properties.ValueMember = nameof(Model.IdModel);
                slueModel.Properties.DisplayMember = nameof(Model.Description);
                slueModel.Properties.View.Columns.AddField(nameof(Model.Description)).Visible = true;
                slueModel.Properties.NullText = "Select model...";
                slueModel.EditValueChanged += (o, e) => 
                {
                    if (_clearingSlue == false)
                    {
                        _clearingSlue = true;
                        slueItem.EditValue = null;
                    }
                    _clearingSlue = false;
                };
            }
            catch
            {
                throw;
            }
            finally
            {
                _clearingSlue = false;
            }
        }

        private void SetUpSlueItemEy()
        {
            try
            {
                slueItem.Properties.DataSource = _itemBcnList;
                slueItem.Properties.ValueMember = nameof(ItemEy.IdItemBcn);
                slueItem.Properties.DisplayMember = nameof(ItemEy.IdItemBcn);
                slueItem.Properties.View.Columns.AddField(nameof(ItemEy.IdItemBcn)).Visible = true;
                slueItem.Properties.View.Columns.AddField(nameof(ItemEy.ItemDescription)).Visible = true;
                slueItem.Properties.NullText = "Select item...";
                slueItem.EditValueChanged += (o, e) => 
                {
                    if(_clearingSlue == false)
                    {
                        _clearingSlue = true;
                        slueModel.EditValue = null;
                    }
                    _clearingSlue = false;

                };
            }
            catch
            {
                throw;
            }
            finally
            {
                _clearingSlue = false;
            }
        }

        private void SetUpSlueStatusCial()
        {
            try
            {
                slueStatusCial.Properties.DataSource = _statusCialList;
                slueStatusCial.Properties.ValueMember = nameof(StatusCial.IdStatusCial);
                slueStatusCial.Properties.DisplayMember = nameof(StatusCial.IdStatusCial);
            }
            catch
            {
                throw;
            }
        }

        private void SetupGrdList()
        {
            try
            {
                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                gridViewList.OptionsView.ColumnAutoWidth = false;
                gridViewList.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;

                //Hacemos todo el grid no editable
                gridViewList.OptionsBehavior.Editable = false;

                //Columns definition
                GridColumn colIdBom = new GridColumn() { Visible = false, FieldName = eGridColumns.ID_BOM.ToString() };
                GridColumn colDescription = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Model"), Visible = true, FieldName = eGridColumns.DESCRIPTION.ToString(), Width = 150 };
                GridColumn colIdItemBcn = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("IdItemBcn"), Visible = true, FieldName = eGridColumns.ID_ITEM_BCN.ToString(), Width = 250 };
                GridColumn colIdSupplier = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Supplier"), Visible = true, FieldName = eGridColumns.ID_SUPPLIER.ToString(), Width = 100 };
                GridColumn colPhotoUrl = new GridColumn() { Visible = false, FieldName = eGridColumns.PHOTO_URL.ToString() };

                //Add columns to grid root view
                gridViewList.Columns.Add(colIdBom);
                gridViewList.Columns.Add(colDescription);
                gridViewList.Columns.Add(colIdItemBcn);
                gridViewList.Columns.Add(colIdSupplier);
                gridViewList.Columns.Add(colPhotoUrl);


            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Crystal Reports

        private void OpenReport()
        {
            try
            {
                B1Report crReport = new B1Report();
                Dictionary<string, string> m_Parametros = new Dictionary<string, string>();
                m_Parametros.Add("@pPhotoBasePath", Constants.ITEMS_PHOTOSWEB_PATH + Constants.ITEM_PHOTOWEB_FOLDER);
                m_Parametros.Add("@pIdModel", (string)slueModel.EditValue);
                m_Parametros.Add("@pIdItem", (string)slueItem.EditValue);
                m_Parametros.Add("@pIdSupplier", (string)slueSupplier.EditValue);
                crReport.Parametros = m_Parametros;
                crReport.ReportFileName = $"{Application.StartupPath}\\Reports\\Rpt\\BOM.rpt";
                //The easiest way to get the default printer is to create a new PrinterSettings object. It starts with all default values.
                System.Drawing.Printing.PrinterSettings settings = new System.Drawing.Printing.PrinterSettings();
                crReport.PrinterName = settings.PrinterName;

                crReport.PreviewReport();
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #endregion

    }


    public static class DbContextExtensions
    {
        public static DataTable DataTable(this DbContext context, string sqlQuery)
        {
            DbProviderFactory dbFactory = DbProviderFactories.GetFactory(context.Database.Connection);

            using (var cmd = dbFactory.CreateCommand())
            {
                cmd.Connection = context.Database.Connection;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqlQuery;
                using (DbDataAdapter adapter = dbFactory.CreateDataAdapter())
                {
                    adapter.SelectCommand = cmd;

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    return dt;
                }
            }
        }
    }
}
