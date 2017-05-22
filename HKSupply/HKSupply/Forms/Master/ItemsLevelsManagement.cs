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
    public partial class ItemsLevelsManagement : RibbonFormBase
    {
        #region Enums
        /*
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
        private enum eHwTypeL1Columns
        {
            IdHwTypeL1,
            Description
        }

        private enum eHwTypeL2Columns
        {
            IdHwTypeL2,
            IdHwTypeL1,
            Description
        }

        private enum eHwTypeL3Columns
        {
            IdHwTypeL3,
            IdHwTypeL2,
            IdHwTypeL1,
            Description
        }

        #endregion
        */
        #endregion

        #region Private Members
        List<MaterialL1> _materialL1List;
        List<MaterialL2> _materialL2List;
        List<MaterialL3> _materialL3List;
        
        List<MatTypeL1> _matTypeL1;
        List<MatTypeL2> _matTypeL2;
        List<MatTypeL3> _matTypeL3;

        List<HwTypeL1> _hwTypeL1;
        List<HwTypeL2> _hwTypeL2;
        List<HwTypeL3> _hwTypeL3;
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

                SetUpGrdMatTypeL1();
                SetUpGrdMatTypeL2();
                SetUpGrdMatTypelL3();

                SetUpGrdHwTypeL1();
                SetUpGrdHwTypeL2();
                SetUpGrdHwTypeL3();
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
                //Task Buttons
                SetActions();
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
            try
            {
                LoadMaterials();
                LoadMatsType();
                LoadHwsType();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Private Methods

        #region SetUp Grids
        private void SetUpGrdMaterialL1()
        {
            try
            {
                //hide group panel.
                gridViewMaterialL1.OptionsView.ShowGroupPanel = false;
                gridViewMaterialL1.OptionsCustomization.AllowGroup = false;
                gridViewMaterialL1.OptionsCustomization.AllowColumnMoving = false;

                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                gridViewMaterialL1.OptionsView.ColumnAutoWidth = false;
                gridViewMaterialL1.HorzScrollVisibility = ScrollVisibility.Auto;

                //Columns definition
                GridColumn colIdMaterialL1 = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("MaterialL1"), Visible = true, FieldName = nameof(MaterialL1.IdMaterialL1), Width = 200 };
                GridColumn colDescription = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Description"), Visible = true, FieldName = nameof(MaterialL1.Description), Width = 400 };

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

                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                gridViewMaterialL2.OptionsView.ColumnAutoWidth = false;
                gridViewMaterialL2.HorzScrollVisibility = ScrollVisibility.Auto;

                //Columns definition
                GridColumn colIdMaterialL2 = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("MaterialL2"), Visible = true, FieldName = nameof(MaterialL2.IdMaterialL2), Width = 200 };
                GridColumn colDescription = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Description"), Visible = true, FieldName = nameof(MaterialL2.Description), Width = 400 };
                GridColumn colIdMaterialL1 = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("MaterialL1"), Visible = true, FieldName = nameof(MaterialL2.IdMaterialL1), Width = 200 };

                //add columns to grid root view
                gridViewMaterialL2.Columns.Add(colIdMaterialL2);
                gridViewMaterialL2.Columns.Add(colDescription);
                gridViewMaterialL2.Columns.Add(colIdMaterialL1);
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

                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                gridViewMaterialL3.OptionsView.ColumnAutoWidth = false;
                gridViewMaterialL3.HorzScrollVisibility = ScrollVisibility.Auto;

                //Columns definition
                GridColumn colIdMaterialL3 = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("MaterialL3"), Visible = true, FieldName = nameof(MaterialL3.IdMaterialL3), Width = 200 };
                GridColumn colDescription = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Description"), Visible = true, FieldName = nameof(MaterialL3.Description), Width = 400 };
                GridColumn colIdMaterialL2 = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("MaterialL2"), Visible = true, FieldName = nameof(MaterialL3.IdMaterialL2), Width = 200 };
                GridColumn colIdMaterialL1 = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("MaterialL1"), Visible = true, FieldName = nameof(MaterialL3.IdMaterialL1), Width = 200 };
                
                //add columns to grid root view
                gridViewMaterialL3.Columns.Add(colIdMaterialL3);
                gridViewMaterialL3.Columns.Add(colDescription);
                gridViewMaterialL3.Columns.Add(colIdMaterialL2);
                gridViewMaterialL3.Columns.Add(colIdMaterialL1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetUpGrdMatTypeL1()
        {
            try
            {
                //hide group panel.
                gridViewMatTypeL1.OptionsView.ShowGroupPanel = false;
                gridViewMatTypeL1.OptionsCustomization.AllowGroup = false;
                gridViewMatTypeL1.OptionsCustomization.AllowColumnMoving = false;

                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                gridViewMatTypeL1.OptionsView.ColumnAutoWidth = false;
                gridViewMatTypeL1.HorzScrollVisibility = ScrollVisibility.Auto;

                //Columns definition
                GridColumn colIdMatTypeL1 = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("MatTypeL1"), Visible = true, FieldName = nameof(MatTypeL1.IdMatTypeL1), Width = 200 };
                GridColumn colDescription = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Description"), Visible = true, FieldName = nameof(MatTypeL1.Description), Width = 400 };

                //add columns to grid root view
                gridViewMatTypeL1.Columns.Add(colIdMatTypeL1);
                gridViewMatTypeL1.Columns.Add(colDescription);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetUpGrdMatTypeL2()
        {
            try
            {
                //hide group panel.
                gridViewMatTypeL2.OptionsView.ShowGroupPanel = false;
                gridViewMatTypeL2.OptionsCustomization.AllowGroup = false;
                gridViewMatTypeL2.OptionsCustomization.AllowColumnMoving = false;

                gridViewMatTypeL2.OptionsView.ColumnAutoWidth = false;
                gridViewMatTypeL2.HorzScrollVisibility = ScrollVisibility.Auto;

                //Columns definition
                GridColumn colIdMatTypeL2 = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("MatTypeL2"), Visible = true, FieldName = nameof(MatTypeL2.IdMatTypeL2), Width = 200 };
                GridColumn colDescription = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Description"), Visible = true, FieldName = nameof(MatTypeL2.Description), Width = 400 };
                GridColumn colIdMatTypeL1 = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("MatTypeL1"), Visible = true, FieldName = nameof(MatTypeL2.IdMatTypeL1), Width = 200 };

                //add columns to grid root view
                gridViewMatTypeL2.Columns.Add(colIdMatTypeL2);
                gridViewMatTypeL2.Columns.Add(colDescription);
                gridViewMatTypeL2.Columns.Add(colIdMatTypeL1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetUpGrdMatTypelL3()
        {
            try
            {
                //hide group panel.
                gridViewMatTypeL3.OptionsView.ShowGroupPanel = false;
                gridViewMatTypeL3.OptionsCustomization.AllowGroup = false;
                gridViewMatTypeL3.OptionsCustomization.AllowColumnMoving = false;

                gridViewMatTypeL3.OptionsView.ColumnAutoWidth = false;
                gridViewMatTypeL3.HorzScrollVisibility = ScrollVisibility.Auto;

                //Columns definition
                GridColumn colIdMatTypeL3 = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("MatTypeL3"), Visible = true, FieldName = nameof(MatTypeL3.IdMatTypeL3), Width = 200 };
                GridColumn colDescription = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Description"), Visible = true, FieldName = nameof(MatTypeL3.Description), Width = 400 };
                GridColumn colIdMatTypeL2 = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("MatTypeL2"), Visible = true, FieldName = nameof(MatTypeL3.IdMatTypeL2), Width = 200 };
                GridColumn colIdMatTypeL1 = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("MatTypeL1"), Visible = true, FieldName = nameof(MatTypeL3.IdMatTypeL1), Width = 200 };

                //add columns to grid root view
                gridViewMatTypeL3.Columns.Add(colIdMatTypeL3);
                gridViewMatTypeL3.Columns.Add(colDescription);
                gridViewMatTypeL3.Columns.Add(colIdMatTypeL2);
                gridViewMatTypeL3.Columns.Add(colIdMatTypeL1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetUpGrdHwTypeL1()
        {
            try 
            {
                //hide group panel.
                gridViewHwTypeL1.OptionsView.ShowGroupPanel = false;
                gridViewHwTypeL1.OptionsCustomization.AllowGroup = false;
                gridViewHwTypeL1.OptionsCustomization.AllowColumnMoving = false;

                gridViewHwTypeL1.OptionsView.ColumnAutoWidth = false;
                gridViewHwTypeL1.HorzScrollVisibility = ScrollVisibility.Auto;

                //Columns definition
                GridColumn colIdHwTypeL1 = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("HwTypeL1"), Visible = true, FieldName = nameof(HwTypeL1.IdHwTypeL1), Width = 200 };
                GridColumn colDescription = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Description"), Visible = true, FieldName = nameof(HwTypeL1.Description), Width = 400 };

                //add columns to grid root view
                gridViewHwTypeL1.Columns.Add(colIdHwTypeL1);
                gridViewHwTypeL1.Columns.Add(colDescription);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetUpGrdHwTypeL2()
        {
            try
            {
                //hide group panel.
                gridViewHwTypeL2.OptionsView.ShowGroupPanel = false;
                gridViewHwTypeL2.OptionsCustomization.AllowGroup = false;
                gridViewHwTypeL2.OptionsCustomization.AllowColumnMoving = false;

                gridViewHwTypeL2.OptionsView.ColumnAutoWidth = false;
                gridViewHwTypeL2.HorzScrollVisibility = ScrollVisibility.Auto;

                //Columns definition
                GridColumn colIdHwTypeL2 = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("HwTypeL2"), Visible = true, FieldName = nameof(HwTypeL2.IdHwTypeL2), Width = 200 };
                GridColumn colDescription = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Description"), Visible = true, FieldName = nameof(HwTypeL2.Description), Width = 400 };
                GridColumn colIdHwTypeL1 = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("HwTypeL1"), Visible = true, FieldName = nameof(HwTypeL2.IdHwTypeL1), Width = 200 };

                //add columns to grid root view
                gridViewHwTypeL2.Columns.Add(colIdHwTypeL2);
                gridViewHwTypeL2.Columns.Add(colDescription);
                gridViewHwTypeL2.Columns.Add(colIdHwTypeL1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetUpGrdHwTypeL3()
        {
            try
            {
                //hide group panel.
                gridViewHwTypeL3.OptionsView.ShowGroupPanel = false;
                gridViewHwTypeL3.OptionsCustomization.AllowGroup = false;
                gridViewHwTypeL3.OptionsCustomization.AllowColumnMoving = false;

                gridViewHwTypeL3.OptionsView.ColumnAutoWidth = false;
                gridViewHwTypeL3.HorzScrollVisibility = ScrollVisibility.Auto;

                //Columns definition
                GridColumn colIdHwTypeL3 = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("HwTypeL3"), Visible = true, FieldName = nameof(HwTypeL3.IdHwTypeL3), Width = 200 };
                GridColumn colDescription = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Description"), Visible = true, FieldName = nameof(HwTypeL3.Description), Width = 400 };
                GridColumn colIdHwTypeL2 = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("HwTypeL2"), Visible = true, FieldName = nameof(HwTypeL3.IdHwTypeL2), Width = 200 };
                GridColumn colIdHwTypeL1 = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("HwTypeL1"), Visible = true, FieldName = nameof(HwTypeL3.IdHwTypeL1), Width = 200 };

                //add columns to grid root view
                gridViewHwTypeL3.Columns.Add(colIdHwTypeL3);
                gridViewHwTypeL3.Columns.Add(colDescription);
                gridViewHwTypeL3.Columns.Add(colIdHwTypeL2);
                gridViewHwTypeL3.Columns.Add(colIdHwTypeL1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Load Grids
        private void LoadMaterials()
        {
            try
            {
                LoadMaterialL1();
                LoadMaterialL2();
                LoadMaterialL3();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadMaterialL1()
        {
            try
            {
                _materialL1List = GlobalSetting.MaterialService.GetMaterialsL1();
                xgrdMaterialL1.DataSource = _materialL1List;

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void LoadMaterialL2()
        {
            try
            {
                _materialL2List = GlobalSetting.MaterialService.GetMaterialsL2();
                xgrdMaterialL2.DataSource = _materialL2List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadMaterialL3()
        {
            try
            {
                _materialL3List = GlobalSetting.MaterialService.GetMaterialsL3();
                xgrdMaterialL3.DataSource = _materialL3List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadMatsType()
        {
            try
            {
                LoadMatTypeL1();
                LoadMatTypeL2();
                LoadMatTypeL3();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadMatTypeL1()
        {
            try
            {
                _matTypeL1 = GlobalSetting.MatTypeService.GetMatsTypeL1();
                xgrdMatTypeL1.DataSource = _matTypeL1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadMatTypeL2()
        {
            try
            {
                _matTypeL2 = GlobalSetting.MatTypeService.GetMatsTypeL2();
                xgrdMatTypeL2.DataSource = _matTypeL2;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadMatTypeL3()
        {
            try
            {
                _matTypeL3 = GlobalSetting.MatTypeService.GetMatsTypeL3();
                xgrdMatTypeL3.DataSource = _matTypeL3;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadHwsType()
        {
            try
            {
                LoadHwTypeL1();
                LoadHwTypeL2();
                LoadHwTypeL3();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadHwTypeL1()
        {
            try
            {
                _hwTypeL1 = GlobalSetting.HwTypeService.GetHwsTypeL1();
                xgrdHwTypeL1.DataSource = _hwTypeL1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadHwTypeL2()
        {
            try
            {
                _hwTypeL2 = GlobalSetting.HwTypeService.GetHwsTypeL2();
                xgrdHwTypeL2.DataSource = _hwTypeL2;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadHwTypeL3()
        {
            try
            {
                _hwTypeL3 = GlobalSetting.HwTypeService.GetHwsTypeL3();
                xgrdHwTypeL3.DataSource = _hwTypeL3;
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
