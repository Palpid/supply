using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
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
    public partial class ItemsLevelsManagement : RibbonFormBase
    {
        #region Enums

        #region Enums Material
        private enum eMaterialL1Columns
        {
            IdMaterialL1,
            Description
        }

        private enum eMaterialL2Columns
        {
            IdMaterialL2,
            IdMaterialL1,
            Description 
        }

        private enum eMaterialL3Columns
        {
            IdMaterialL3,
            IdMaterialL2,
            IdMaterialL1,
            Description
        }
        #endregion

        #region Enums MatType
        private enum eMatTypeL1Columns
        {
            IdMatTypeL1,
            Description
        }

        private enum eMatTypeL2Columns
        {
            IdMatTypeL2,
            IdMatTypeL1,
            Description
        }

        private enum eMatTypeL3Columns
        {
            IdMatTypeL3,
            IdMatTypeL2,
            IdMatTypeL1,
            Description
        }
        #endregion

        #region Enums HwType
        #endregion

        #endregion

        #region Private Members
        #endregion

        #region Constructor
        public ItemsLevelsManagement()
        {
            InitializeComponent();

            try
            {
                ConfigureRibbonActions();
                SetUpGrdMaterialL1();
                SetUpGrdMaterialL2();
                SetUpGrdMaterialL3();
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
        #endregion

        #region Form Events
        private void ItemsLevelsManagement_Load(object sender, EventArgs e)
        {

        }
        #endregion

        #region Private Methods
        private void SetUpGrdMaterialL1()
        {
            try
            {
                //hide group panel.
                gridViewMaterialL1.OptionsView.ShowGroupPanel = false;
                gridViewMaterialL1.OptionsCustomization.AllowGroup = false;
                gridViewMaterialL1.OptionsCustomization.AllowColumnMoving = false;

                //Columns definition
                GridColumn colIdMaterialL1 = new GridColumn() { Caption = "Material L1", Visible = true, FieldName = eMaterialL1Columns.IdMaterialL1.ToString() };
                GridColumn colDescription = new GridColumn() { Caption = "Description", Visible = true, FieldName = eMaterialL1Columns.Description.ToString() };

                //add columns to grid root view
                gridViewMaterialL1.Columns.Add(colIdMaterialL1);
                gridViewMaterialL1.Columns.Add(colDescription);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetUpGrdMaterialL2()
        {
            try
            {
                //hide group panel.
                gridViewMaterialL2.OptionsView.ShowGroupPanel = false;
                gridViewMaterialL2.OptionsCustomization.AllowGroup = false;
                gridViewMaterialL2.OptionsCustomization.AllowColumnMoving = false;

                //Columns definition
                GridColumn colIdMaterialL2 = new GridColumn() { Caption = "Material L2", Visible = true, FieldName = eMaterialL2Columns.IdMaterialL2.ToString() };
                GridColumn colIdMaterialL1 = new GridColumn() { Caption = "Material L1", Visible = true, FieldName = eMaterialL2Columns.IdMaterialL1.ToString() };
                GridColumn colDescription = new GridColumn() { Caption = "Description", Visible = true, FieldName = eMaterialL2Columns.Description.ToString() };

                //add columns to grid root view
                gridViewMaterialL2.Columns.Add(colIdMaterialL2);
                gridViewMaterialL2.Columns.Add(colIdMaterialL1);
                gridViewMaterialL2.Columns.Add(colDescription);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetUpGrdMaterialL3()
        {
            try
            {
                //hide group panel.
                gridViewMaterialL3.OptionsView.ShowGroupPanel = false;
                gridViewMaterialL3.OptionsCustomization.AllowGroup = false;
                gridViewMaterialL3.OptionsCustomization.AllowColumnMoving = false;

                //Columns definition
                GridColumn colIdMaterialL3 = new GridColumn() { Caption = "Material L3", Visible = true, FieldName = eMaterialL3Columns.IdMaterialL3.ToString() };
                GridColumn colIdMaterialL2 = new GridColumn() { Caption = "Material L2", Visible = true, FieldName = eMaterialL3Columns.IdMaterialL2.ToString() };
                GridColumn colIdMaterialL1 = new GridColumn() { Caption = "Material L1", Visible = true, FieldName = eMaterialL3Columns.IdMaterialL1.ToString() };
                GridColumn colDescription = new GridColumn() { Caption = "Description", Visible = true, FieldName = eMaterialL3Columns.Description.ToString() };

                //add columns to grid root view
                gridViewMaterialL3.Columns.Add(colIdMaterialL3);
                gridViewMaterialL3.Columns.Add(colIdMaterialL2);
                gridViewMaterialL3.Columns.Add(colIdMaterialL1);
                gridViewMaterialL3.Columns.Add(colDescription);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
