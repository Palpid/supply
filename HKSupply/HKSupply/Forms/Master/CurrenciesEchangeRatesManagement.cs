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
    public partial class CurrenciesEchangeRatesManagement : RibbonFormBase
    {
        #region Enums
        /*private enum eCurrenciesColumns
        {
            IdCurrency,
            Description,
            DescriptionZh
        }

        private enum eEchangeRatesColumns
        {
            Date,
            IdCurrency1,
            IdCurrency2,
            Ratio
        }*/
        #endregion

        #region Private Members
        List<Currency> _currenciesList = new List<Currency>();
        List<EchangeRate> _echangeRatesList = new List<EchangeRate>();
        #endregion

        #region Constructor
        public CurrenciesEchangeRatesManagement()
        {
            InitializeComponent();

            try
            {
                ConfigureRibbonActions();
                SetUpGrdCurrencies();
                SetUpGrdEchangeRates();
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
        private void CurrenciesEchangeRatesManagement_Load(object sender, EventArgs e)
        {
            try
            {
                LoadAllCurrencies();
                LoadAllEchangeRates();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Private Methods

        private void SetUpGrdCurrencies()
        {
            try
            {
                //hide group panel.
                gridViewCurrencies.OptionsView.ShowGroupPanel = false;
                gridViewCurrencies.OptionsCustomization.AllowGroup = false;
                gridViewCurrencies.OptionsCustomization.AllowColumnMoving = false;

                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                gridViewCurrencies.OptionsView.ColumnAutoWidth = false;
                gridViewCurrencies.HorzScrollVisibility = ScrollVisibility.Auto;

                //hacer todo el grid no editable
                gridViewCurrencies.OptionsBehavior.Editable = false;

                //Columns definition
                GridColumn colIdCurrency = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Currency"), Visible = true, FieldName = nameof(Currency.IdCurrency), Width = 70 };
                GridColumn colDescription = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Description"), Visible = true, FieldName = nameof(Currency.Description), Width = 300 };
                GridColumn colDescriptionZh = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("DescriptionChinese"), Visible = true, FieldName = nameof(Currency.DescriptionZh), Width = 300 };

                //add columns to grid root view
                gridViewCurrencies.Columns.Add(colIdCurrency);
                gridViewCurrencies.Columns.Add(colDescription);
                gridViewCurrencies.Columns.Add(colDescriptionZh);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetUpGrdEchangeRates()
        {
            try
            {
                //hide group panel.
                gridViewEchangeRates.OptionsView.ShowGroupPanel = false;
                gridViewEchangeRates.OptionsCustomization.AllowGroup = false;
                gridViewEchangeRates.OptionsCustomization.AllowColumnMoving = false;

                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                gridViewEchangeRates.OptionsView.ColumnAutoWidth = false;
                gridViewEchangeRates.HorzScrollVisibility = ScrollVisibility.Auto;

                //hacer todo el grid no editable
                gridViewEchangeRates.OptionsBehavior.Editable = false;

                //Columns definition
                GridColumn colDate = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Date"), Visible = true, FieldName = nameof(EchangeRate.Date), Width = 100 };
                GridColumn colIdCurrency1 = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Currency1"), Visible = true, FieldName = nameof(EchangeRate.IdCurrency1), Width = 70 };
                GridColumn colIdCurrency2 = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Currency2"), Visible = true, FieldName = nameof(EchangeRate.IdCurrency2), Width = 70 };
                GridColumn colRatio = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Ratio"), Visible = true, FieldName = nameof(EchangeRate.Ratio), Width = 100 };

                //Format Type
                colRatio.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                colRatio.DisplayFormat.FormatString = "F4";


                //add columns to grid root view
                gridViewEchangeRates.Columns.Add(colDate);
                gridViewEchangeRates.Columns.Add(colIdCurrency1);
                gridViewEchangeRates.Columns.Add(colIdCurrency2);
                gridViewEchangeRates.Columns.Add(colRatio);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadAllCurrencies()
        {
            try 
            {
                _currenciesList = GlobalSetting.CurrencyService.GetCurrencies();
                xgrdCurrencies.DataSource = _currenciesList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadAllEchangeRates()
        {
            try
            {
                _echangeRatesList = GlobalSetting.EchangeRateService.GetEchangeRates();
                xgrdEchangeRates.DataSource = _echangeRatesList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
