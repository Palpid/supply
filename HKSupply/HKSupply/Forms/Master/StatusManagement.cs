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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HKSupply.Forms.Master
{
    public partial class StatusManagement : RibbonFormBase
    {
        #region Enums
        private enum eStatusHkColumns
        {
            IdStatusProd,
            Description
        }

        private enum eStatusCialColumns
        {
            IdStatusCial,
            Description
        }
        #endregion

        #region Private Members
        List<StatusHK> _statusHkList;
        List<StatusCial> _statusCialList;
        #endregion

        #region Constructor
        public StatusManagement()
        {
            InitializeComponent();

            try
            {
                ConfigureRibbonActions();
                SetUpGrdStatusHk();
                SetUpGrdStatusCial();
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
        #endregion

        #region Form Events
        private void StatusManagement_Load(object sender, EventArgs e)
        {
            try
            {
                LoadAllStatusHk();
                LoadAllStatusCial();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Private Members
        private void SetUpGrdStatusHk()
        {
            try
            {
                //Hide group panel
                gridViewStatusHK.OptionsView.ShowGroupPanel = false;
                gridViewStatusHK.OptionsCustomization.AllowGroup = false;
                gridViewStatusHK.OptionsCustomization.AllowColumnMoving = false;

                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                gridViewStatusHK.OptionsView.ColumnAutoWidth = false;
                gridViewStatusHK.HorzScrollVisibility = ScrollVisibility.Auto;

                //Hacer todo el grid no editable
                gridViewStatusHK.OptionsBehavior.Editable = false;

                //Columns definition
                GridColumn colIdStatusProd = new GridColumn() { Caption = "Id", Visible = true, FieldName = eStatusHkColumns.IdStatusProd.ToString(), Width = 70 };
                GridColumn colDescription = new GridColumn() { Caption = "Description", Visible = true, FieldName = eStatusHkColumns.Description.ToString(), Width = 200 };

                //Add columns to grid view
                gridViewStatusHK.Columns.Add(colIdStatusProd);
                gridViewStatusHK.Columns.Add(colDescription);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetUpGrdStatusCial()
        {
            try
            {
                //Hide group panel
                gridViewStatusCial.OptionsView.ShowGroupPanel = false;
                gridViewStatusCial.OptionsCustomization.AllowGroup = false;
                gridViewStatusCial.OptionsCustomization.AllowColumnMoving = false;

                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                gridViewStatusCial.OptionsView.ColumnAutoWidth = false;
                gridViewStatusCial.HorzScrollVisibility = ScrollVisibility.Auto;

                //Hacer todo el grid no editable
                gridViewStatusCial.OptionsBehavior.Editable = false;

                //Columns definition
                GridColumn colIdStatusCial = new GridColumn() { Caption = "Id", Visible = true, FieldName = eStatusCialColumns.IdStatusCial.ToString(), Width = 70 };
                GridColumn colDescription = new GridColumn() { Caption = "Description", Visible = true, FieldName = eStatusCialColumns.Description.ToString(), Width = 200 };

                //Add columns to grid view
                gridViewStatusCial.Columns.Add(colIdStatusCial);
                gridViewStatusCial.Columns.Add(colDescription);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadAllStatusHk()
        {
            try
            {
                _statusHkList = GlobalSetting.StatusProdService.GetStatusProd();
                xgrdStatusHK.DataSource = _statusHkList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadAllStatusCial()
        {
            try
            {
                _statusCialList = GlobalSetting.StatusCialService.GetStatusCial();
                xgrdStatusCial.DataSource = _statusCialList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
