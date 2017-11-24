using DevExpress.Data.PivotGrid;
using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraPivotGrid;
using HKSupply.General;
using HKSupply.Helpers;
using HKSupply.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace HKSupply.Forms.Supply.Dashboard
{
    public partial class DashboardQP : RibbonFormBase
    {
        #region Constants
        //private const string FIELD_PORCENTUAL_DEVIATION = "UnboundFieldPorcentualDeviation";
        #endregion

        #region Private Members

        Font _labelDefaultFontBold = new Font("SourceSansProRegular", 8, FontStyle.Bold);
        Font _labelDefaultFont = new Font("SourceSansProRegular", 8, FontStyle.Regular);

        List<Customer> _customersList;
        List<ItemGroup> _itemGroupList;
        List<string> _typeList;
        #endregion

        #region Constructor
        public DashboardQP()
        {
            InitializeComponent();

            try
            {
                Cursor = Cursors.WaitCursor;

                ConfigureRibbonActions();
                LoadAuxList();
                SetUpLabels();
                SetUpCheckedComboBoxEdit();
                SetUpLookUpEdit();
                SetUpEvents();
                SetupPivotGrid();
                SetUpSplitContainer();
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

        public override void bbiExportExcel_ItemClick(object sender, ItemClickEventArgs e)
        {

            //Abre el dialog de save as
            base.bbiExportExcel_ItemClick(sender, e);

            try
            {
                if (string.IsNullOrEmpty(ExportExcelFile) == false)
                {
                    pivotGridDashboardQP.ExportToXlsx(ExportExcelFile);
                }

                DialogResult result = MessageBox.Show(GlobalSetting.ResManager.GetString("OpenFileQuestion"), "", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(ExportExcelFile);
                }
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Form Events

        private void DashboardQP_Load(object sender, EventArgs e)
        {

        }

        private void DateEditDateIni_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                lblDateIniWeek.Text = dateEditDateIni.DateTime.GetWeek().ToString();

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DateEditDateEnd_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                lblDateEndWeek.Text = dateEditDateEnd.DateTime.GetWeek().ToString();

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SbGenerate_Click(object sender, EventArgs e)
        {
            try
            {

                if (ValidateFilters())
                {
                    LoadDashboard();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Grid Events

        //private void PivotGridDashboardQP_CustomUnboundFieldData(object sender, CustomFieldDataEventArgs e)
        //{
        //    try
        //    {
        //        if (e.Field == null) return;

        //        if (e.Field.FieldName == FIELD_PORCENTUAL_DEVIATION)
        //        {
        //            decimal porcentualDeviation = (decimal)e.GetListSourceColumnValue(nameof(AuxDashboardQPStored.PORCENTUAL_DEVIATION));
        //            e.Value = porcentualDeviation / 100;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        private void PivotGridDashboardQP_CustomAppearance(object sender, PivotCustomAppearanceEventArgs e)
        {
            try
            {
                //if (e.DataField.FieldName == FIELD_PORCENTUAL_DEVIATION
                if (e.DataField.FieldName == nameof(AuxDashboardQPStoredProcedure.PERCENTAGE_DEVIATION) 
                    && e.ColumnValueType == PivotGridValueType.Value
                    && e.RowValueType == PivotGridValueType.Value)
                {
                    if (e.Value != null && (decimal)e.Value > 0.05m && (decimal)e.Value < 0.30m)
                    {
                        e.Appearance.BackColor = Color.Orange;
                        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    }
                    else if (e.Value != null && (decimal)e.Value > 0.30m)
                    {
                        e.Appearance.BackColor = Color.Red;
                        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PivotGridDashboardQP_CustomCellValue(object sender, PivotCellValueEventArgs e)
        {
            try
            {
                // Exits if the processed cell does not belong to a Custom Total.
                if (e.ColumnCustomTotal == null && e.RowCustomTotal == null) return;


                // Obtains a list of summary values against which
                // the Custom Total will be calculated.
                ArrayList summaryValues = GetSummaryValues(e);

                // Obtains the name of the Custom Total that should be calculated.
                string customTotalName = GetCustomTotalName(e);

                // Calculates the Custom Total value and assigns it to the Value event parameter.
                e.Value = GetCustomTotalValue(summaryValues, customTotalName, e.DataField.FieldName);

                //Special case, this summany depends other summaries
                //if (customTotalName == "CustomSummary01" && e.DataField.FieldName == FIELD_PORCENTUAL_DEVIATION)
                if (customTotalName == "CustomSummary01" && e.DataField.FieldName == nameof(AuxDashboardQPStoredProcedure.PERCENTAGE_DEVIATION))
                {
                    e.Value = GetCustomSummaryPorcentualDeviation(e);
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PivotGridDashboardQP_FieldValueDisplayText(object sender, PivotFieldDisplayTextEventArgs e)
        {
            try
            {
                if (e.ValueType == PivotGridValueType.CustomTotal
                    && e.Field.FieldName == nameof(AuxDashboardQPStoredProcedure.ID_CUSTOMER))
                {
                    e.DisplayText += " TOTAL AVERAGE (MT & HW)";
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PivotGridDashboardQP_CellDoubleClick(object sender, PivotCellEventArgs e)
        {
            try
            {
                ShowDrilldown(e.CreateDrillDownDataSource());
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PivotGridDashboardQP_CustomDrawFieldValue(object sender, PivotCustomDrawFieldValueEventArgs e)
        {
            try
            {
                if (e.Area == PivotArea.ColumnArea)
                {
                    Rectangle rect = e.Bounds;
                    ControlPaint.DrawBorder3D(e.Graphics, e.Bounds);
                    Brush brush = e.GraphicsCache.GetSolidBrush(Color.AliceBlue);
                    rect.Inflate(-1, -1);
                    e.Graphics.FillRectangle(brush, rect);
                    e.Appearance.DrawString(e.GraphicsCache, e.Info.Caption, e.Info.CaptionRect);
                    e.Painter.DrawIndicator(e.Info);
                    e.Handled = true;
                }
                else if (e.Area == PivotArea.RowArea)
                {
                    e.Painter.DrawObject(e.Info);
                    e.Painter.DrawIndicator(e.Info);
                    //e.Graphics.FillRectangle(e.GraphicsCache.GetSolidBrush(Color.FromArgb(50, 0, 0, 200)), e.Bounds);
                    e.Graphics.FillRectangle(e.GraphicsCache.GetSolidBrush(Color.FromArgb(100, 200, 0, 0)), e.Bounds);
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region Chart Events
        //private void ChartObject_CustomDrawAxisLabel(object sender, CustomDrawAxisLabelEventArgs e)
        //{
        //    try
        //    {
        //        AxisBase axis = e.Item.Axis;
        //        if (axis is AxisX || axis is AxisX3D || axis is RadarAxisX)
        //        {
        //            double axisValue = (double)e.Item.AxisValue;
        //            e.Item.Text = Math.Truncate(axisValue).ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
        #endregion

        #endregion

        #region Private Methods

        #region Load/Resets

        private void LoadAuxList()
        {
            try
            {
                _customersList = GlobalSetting.CustomerService.GetCustomers();

                //Está feo, pero llamar a la db para recuperar todos y después filtrar dos a fuego es igual de feo y hace una llamada
                _itemGroupList = new List<ItemGroup>();
                _itemGroupList.Add(new ItemGroup() { Id = Constants.ITEM_GROUP_HW, Description = Constants.ITEM_GROUP_HW });
                _itemGroupList.Add(new ItemGroup() { Id = Constants.ITEM_GROUP_MT, Description = Constants.ITEM_GROUP_MT });

                _typeList = new List<string>();
                _typeList.Add("DETAIL");
                _typeList.Add("SUMMARY");
            }
            catch
            {
                throw;
            }
        }

        private void LoadDashboard()
        {
            try
            {
                LoadDashboardData();
                LoadDashboardChart();
            }
            catch
            {
                throw;
            }
        }

        private void LoadDashboardData()
        {
            try
            {
                var factories = ccbeCustomer.Properties.GetCheckedItems();
                var itemGroupType = ccbeItemGroup.Properties.GetCheckedItems();
                var weeks = string.Empty;
                var type = (string)lueType.EditValue;

                for (int i = Int32.Parse(lblDateIniWeek.Text); i<=Int32.Parse(lblDateEndWeek.Text); i++)
                {
                    weeks += i.ToString() + ",";
                }

                var data = GlobalSetting.SupplyDashboardService.GetDashboardQP(
                    queryType: type,
                    factories: factories.ToString(), 
                    weeks: weeks, 
                    itemGroup: itemGroupType.ToString());

                pivotGridDashboardQP.DataSource = null;
                pivotGridDashboardQP.DataSource = data;

                GenerateBarChartDashboard(data, chartControlPercDesv, "% DESV");

            }
            catch
            {
                throw;
            }
        }

        private void LoadDashboardChart()
        {
            try
            {
                //Get chart data
                var factories = ccbeCustomer.Properties.GetCheckedItems();
                var itemGroupType = ccbeItemGroup.Properties.GetCheckedItems();
                var weeks = string.Empty;

                for (int i = Int32.Parse(lblDateIniWeek.Text); i <= Int32.Parse(lblDateEndWeek.Text); i++)
                {
                    weeks += i.ToString() + ",";
                }

                var chartData = GlobalSetting.SupplyDashboardService.GetDashboardQP(
                    queryType: "SUMMARY",
                    factories: factories.ToString(),
                    weeks: weeks,
                    itemGroup: itemGroupType.ToString());

                GenerateChartRawMaterialHardware(chartData);

            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region SetUp Form Objects

        private void SetUpLabels()
        {
            try
            {
                /********* Fonts **********/
                //Header 
                lblCustomer.Font = _labelDefaultFontBold;
                lblItemGroup.Font = _labelDefaultFontBold;
                lblType.Font = _labelDefaultFontBold;
                lblDateIni.Font = _labelDefaultFontBold;
                lblDateEnd.Font = _labelDefaultFontBold;
                lblDate.Font = _labelDefaultFont;
                lblWeek.Font = _labelDefaultFont;

                /********* Texts **********/
                //Headers
                lblCustomer.Text = "FACTORY";
                lblItemGroup.Text = "ITEM GROUP";
                lblType.Text = "TYPE";
                lblDateIni.Text = "START";
                lblDateEnd.Text = "END";
                lblDate.Text = "Date";
                lblWeek.Text = "Week";
                lblDateIniWeek.Text = string.Empty;
                lblDateEndWeek.Text = string.Empty;

                /********* Align **********/
                //Headers
                lblDateIniWeek.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                lblDateEndWeek.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                lblDate.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                lblWeek.Appearance.TextOptions.HAlignment = HorzAlignment.Center;

            }
            catch
            {
                throw;
            }
        }
        
        private void SetUpCheckedComboBoxEdit()
        {
            try
            {
                SetUpCcbeCustomer();
                SetUpCcbeItemGroup();
            }
            catch
            {

            }
        }

        private void SetUpLookUpEdit()
        {
            try
            {
                lueType.Properties.DataSource = _typeList;
                lueType.Properties.NullText = "[Select a type]";
            }
            catch
            {
                throw;
            }
        }

        private void SetUpCcbeCustomer()
        {
            try
            {
                ccbeCustomer.Properties.DataSource = _customersList;
                ccbeCustomer.Properties.ValueMember = nameof(Customer.IdCustomer);
                ccbeCustomer.Properties.DisplayMember = nameof(Customer.IdCustomer);
            }
            catch
            {

            }
        }

        private void SetUpCcbeItemGroup()
        {
            try
            {
                ccbeItemGroup.Properties.DataSource = _itemGroupList;
                ccbeItemGroup.Properties.ValueMember = nameof(ItemGroup.Id);
                ccbeItemGroup.Properties.DisplayMember = nameof(ItemGroup.Description);
            }
            catch
            {

            }
        }

        private void SetUpEvents()
        {
            try
            {
                dateEditDateIni.EditValueChanged += DateEditDateIni_EditValueChanged;
                dateEditDateEnd.EditValueChanged += DateEditDateEnd_EditValueChanged;
                sbGenerate.Click += SbGenerate_Click;
            }
            catch
            {
                throw;
            }
        }

        private void SetupPivotGrid()
        {
            try
            {

                //Create pivot grid fields
                PivotGridField fieldWeekNum = new PivotGridField() { FieldName = nameof(AuxDashboardQPStoredProcedure.WEEK_NUM), Caption = "Week", Area = PivotArea.ColumnArea, Width = 100, };
                PivotGridField fieldIdCustomer = new PivotGridField() { FieldName = nameof(AuxDashboardQPStoredProcedure.ID_CUSTOMER), Caption = "Factory", Area = PivotArea.RowArea, Width = 100 };
                PivotGridField fieldIdItemBcn = new PivotGridField() { FieldName = nameof(AuxDashboardQPStoredProcedure.ID_ITEM_BCN), Caption = "Item", Area = PivotArea.RowArea, Width = 150 };
                PivotGridField fieldIdItemGroup = new PivotGridField() { FieldName = nameof(AuxDashboardQPStoredProcedure.ID_ITEM_GROUP), Caption = "Type", Area = PivotArea.RowArea, Width = 70 };
                PivotGridField fieldUnit = new PivotGridField() { FieldName = nameof(AuxDashboardQPStoredProcedure.UNIT), Caption = "Unit", Area = PivotArea.RowArea, Width = 70 };
                PivotGridField fieldTotalQtyOriginal = new PivotGridField() { FieldName = nameof(AuxDashboardQPStoredProcedure.TOTAL_QTY_ORIGINAL), Caption = "QTY Original", Area = PivotArea.DataArea, Width = 100};
                PivotGridField fieldTotalQty = new PivotGridField() { FieldName = nameof(AuxDashboardQPStoredProcedure.TOTAL_QTY), Caption = "QTY Real", Area = PivotArea.DataArea, Width = 100 };
                PivotGridField fieldAbsoluteDeviation = new PivotGridField() { FieldName = nameof(AuxDashboardQPStoredProcedure.ABSOLUTE_DEVIATION), Caption = "Abs Deviation", Area = PivotArea.DataArea, Width = 105 };
                PivotGridField fieldPorcentualDeviation = new PivotGridField() { FieldName = nameof(AuxDashboardQPStoredProcedure.PERCENTAGE_DEVIATION), Caption = "% Deviation", Area = PivotArea.DataArea, Width = 95 };

                //Devuelve el valor correcto desde la db para después hacer el % y nos ahorramos el cálculo (ex : 0.24 -> 24%, en lugar de devolver el 24 directamente)
                //Unbound field para el % porque si le das formato porcentual a la celda multiplica por 100
                //PivotGridField fieldPorcentualDeviation = new PivotGridField()
                //{
                //    FieldName = FIELD_PORCENTUAL_DEVIATION,
                //    Caption = "% Deviation",
                //    Area = PivotArea.DataArea,
                //    Width = 85,
                //    UnboundFieldName = FIELD_PORCENTUAL_DEVIATION,
                //    UnboundType = DevExpress.Data.UnboundColumnType.Decimal
                //};

                //Formating some fields

                fieldTotalQtyOriginal.CellFormat.FormatType = FormatType.Numeric;
                fieldTotalQtyOriginal.CellFormat.FormatString = "n3";

                fieldTotalQty.CellFormat.FormatType = FormatType.Numeric;
                fieldTotalQty.CellFormat.FormatString = "n3";

                fieldAbsoluteDeviation.CellFormat.FormatType = FormatType.Numeric;
                fieldAbsoluteDeviation.CellFormat.FormatString = "n3";

                fieldPorcentualDeviation.CellFormat.FormatType = FormatType.Numeric;
                fieldPorcentualDeviation.CellFormat.FormatString = "P2";

                //Disable drag to fix de pivot format
                fieldWeekNum.Options.AllowDrag = DefaultBoolean.False;
                fieldIdCustomer.Options.AllowDrag = DefaultBoolean.False;
                fieldIdItemBcn.Options.AllowDrag = DefaultBoolean.False;
                fieldIdItemGroup.Options.AllowDrag = DefaultBoolean.False;
                fieldUnit.Options.AllowDrag = DefaultBoolean.False;
                fieldTotalQtyOriginal.Options.AllowDrag = DefaultBoolean.False;
                fieldTotalQty.Options.AllowDrag = DefaultBoolean.False;
                fieldAbsoluteDeviation.Options.AllowDrag = DefaultBoolean.False;
                fieldPorcentualDeviation.Options.AllowDrag = DefaultBoolean.False;

                //-----------------------------------------------------------

                //Custom Summaries

                fieldUnit.TotalsVisibility = PivotTotalsVisibility.CustomTotals;
                fieldIdCustomer.TotalsVisibility = PivotTotalsVisibility.CustomTotals;
                //fieldUnit.CustomTotals.Add(PivotSummaryType.Sum);
                //fieldUnit.CustomTotals.Add(PivotSummaryType.Average);


                PivotGridCustomTotal CustomSummary01 = new PivotGridCustomTotal(PivotSummaryType.Custom);
                PivotGridCustomTotal CustomSummaryAverage = new PivotGridCustomTotal(PivotSummaryType.Custom);

                // Specifies a unique PivotGridCustomTotal.Tag property value that will be used to distinguish between Custom Totals.
                CustomSummary01.Tag = "CustomSummary01";
                CustomSummaryAverage.Tag = "CustomSummaryAverage";

                // Specifies formatting settings that will be used to display 
                // Custom Total column/row headers.

                CustomSummary01.Format.FormatString = "{0:n3}";
                CustomSummary01.Format.FormatType = FormatType.Custom;

                CustomSummaryAverage.Format.FormatString = "{0:n3}";
                CustomSummaryAverage.Format.FormatType = FormatType.Custom;

                // Adds the Custom Totals for the fields.
                fieldUnit.CustomTotals.Add(CustomSummary01);
                fieldIdCustomer.CustomTotals.Add(CustomSummaryAverage);

                //-----------------------------------------------------------

                //Hide total row for Factory (customer) because we mix differents unit
                //fieldIdCustomer.Options.ShowTotals = false;

                // Add the fields to the control's field collection. 
                pivotGridDashboardQP.Fields.Add(fieldWeekNum);
                pivotGridDashboardQP.Fields.Add(fieldIdCustomer);
                pivotGridDashboardQP.Fields.Add(fieldUnit);
                pivotGridDashboardQP.Fields.Add(fieldIdItemBcn);
                pivotGridDashboardQP.Fields.Add(fieldIdItemGroup);
                pivotGridDashboardQP.Fields.Add(fieldTotalQtyOriginal);
                pivotGridDashboardQP.Fields.Add(fieldTotalQty);
                pivotGridDashboardQP.Fields.Add(fieldAbsoluteDeviation);
                pivotGridDashboardQP.Fields.Add(fieldPorcentualDeviation);

                //Headers in bold
                var currentFont = pivotGridDashboardQP.Appearance.FieldValue.Font;
                pivotGridDashboardQP.Appearance.FieldValue.Font = new Font(currentFont.Name, currentFont.Size, FontStyle.Bold);
                pivotGridDashboardQP.Appearance.FieldValue.Options.UseFont = true;

                //Events
                //pivotGridDashboardQP.CustomUnboundFieldData += PivotGridDashboardQP_CustomUnboundFieldData;
                pivotGridDashboardQP.CustomAppearance += PivotGridDashboardQP_CustomAppearance;
                pivotGridDashboardQP.CustomCellValue += PivotGridDashboardQP_CustomCellValue;
                pivotGridDashboardQP.FieldValueDisplayText += PivotGridDashboardQP_FieldValueDisplayText;
                pivotGridDashboardQP.CellDoubleClick += PivotGridDashboardQP_CellDoubleClick;
                pivotGridDashboardQP.CustomDrawFieldValue += PivotGridDashboardQP_CustomDrawFieldValue;

                


            }
            catch
            {
                throw;
            }
        }

        
        private void SetUpSplitContainer()
        {
            try
            {
                //No hay ningún panel fijo, los dos hacen el resized a la par
                splitContainerControlChart.FixedPanel = SplitFixedPanel.None;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Pivotgrid Custom Summaries
        /// <summary>
        /// Returns a list of summary values against which a Custom Total will be calculated.
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private ArrayList GetSummaryValues(PivotCellValueEventArgs e)
        {
            ArrayList values = new ArrayList();

            // Creates a summary data source.
            PivotSummaryDataSource sds = e.CreateSummaryDataSource();

            // Iterates through summary data source records and copies summary values to an array.
            for (int i = 0; i < sds.RowCount; i++)
            {
                object value = sds.GetValue(i, e.DataField);
                if (value == null)
                {
                    continue;
                }
                values.Add(value);
            }

            // Sorts summary values.
            values.Sort();

            // Returns the summary values array.
            return values;
        }

        /// <summary>
        /// Returns the Custom Total name.
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private string GetCustomTotalName(PivotCellValueEventArgs e)
        {
            return e.ColumnCustomTotal != null ?
                e.ColumnCustomTotal.Tag.ToString() :
                e.RowCustomTotal.Tag.ToString();
        }

        /// <summary>
        /// Returns the Custom Total value by an array of summary values.
        /// </summary>
        /// <param name="values"></param>
        /// <param name="customTotalName"></param>
        /// <returns></returns>
        private object GetCustomTotalValue(ArrayList values, string customTotalName, string fieldName)
        {

            // Returns a null value if the provided array is empty.
            if (values.Count == 0)
            {
                return null;
            }

            if (customTotalName == "CustomSummary01")
            {
                if (fieldName == nameof(AuxDashboardQPStored.TOTAL_QTY) ||
                fieldName == nameof(AuxDashboardQPStored.TOTAL_QTY_ORIGINAL) ||
                fieldName == nameof(AuxDashboardQPStored.ABSOLUTE_DEVIATION)
                )
                {
                    decimal sum = 0;
                    foreach (var v in values)
                    {
                        sum += (decimal)v;
                    }

                    return sum;
                }
            }
            else
            {
                if (customTotalName == "CustomSummaryAverage")
                {
                    //if (fieldName == FIELD_PORCENTUAL_DEVIATION)
                    if (fieldName == nameof(AuxDashboardQPStoredProcedure.PERCENTAGE_DEVIATION))
                    {
                        decimal sum = 0;
                        foreach (var v in values)
                        {
                            sum += (decimal)v;
                        }

                        return (decimal)sum / values.Count;
                    }
                }
            }

            // Otherwise, returns a null value.
            return null;

            // If the QtyOriginalCustomTotalMt should be calculated,
            // calls the GetQtyOriginalCustomTotalMt method.
            //if (customTotalName == "QtyOriginalCustomTotalMt")
            //{
            //    return GetMedian(values);
            //}

            //// If the Quartiles Custom Total should be calculated,
            //// calls the GetQuartiles method.
            //if (customTotalName == "Quartiles")
            //{
            //    return GetQuartiles(values);
            //}


        }


        private object GetCustomSummaryPorcentualDeviation(PivotCellValueEventArgs e)
        {
            try
            {

                decimal summaryPorcentualDeviation = 0;
                decimal summaryAbsDes = 0;
                decimal summaryQtyOriginal = 0;

                // Creates a summary data source.
                PivotSummaryDataSource sds = e.CreateSummaryDataSource();

                //----------------------------------------
                
                //Para pasar los datosa a un datable y trabajar con él, no lo uso, pero me guardo el código por si acaso

                //DataTable dt = new DataTable();
                //foreach (PropertyDescriptor _propertyDescriptor in sds.GetItemProperties(null))
                //    dt.Columns.Add(_propertyDescriptor.Name, _propertyDescriptor.PropertyType);
                //for (int r = 0; r < sds.RowCount; r++)
                //{
                //    object[] rowValues = new object[dt.Columns.Count];
                //    for (int c = 0; c < dt.Columns.Count; c++)
                //    {
                //        rowValues[c] = sds.GetValue(r, dt.Columns[c].ColumnName);
                //    }
                //    dt.Rows.Add(rowValues);
                //}

                //var x = dt.AsEnumerable().Sum(a => a.Field<decimal>(nameof(AuxDashboardQPStoredProcedure.TOTAL_QTY_ORIGINAL)+"_Sum"));
                //---------------------------------------
                for (int i = 0; i < sds.RowCount; i++)
                {
                    object valueAbsDes = sds.GetValue(i, nameof(AuxDashboardQPStoredProcedure.ABSOLUTE_DEVIATION) + "_Sum");
                    object valueQtyOriginal = sds.GetValue(i, nameof(AuxDashboardQPStoredProcedure.TOTAL_QTY_ORIGINAL) + "_Sum");

                    var xd = e.GetColumnFields();

                    if (valueAbsDes != null)
                        summaryAbsDes += (decimal)valueAbsDes;

                    if (valueQtyOriginal != null)
                        summaryQtyOriginal += (decimal)valueQtyOriginal;
                }
                summaryPorcentualDeviation = (summaryAbsDes / summaryQtyOriginal);

                return summaryPorcentualDeviation;

            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Aux Methods

        private bool ValidateFilters()
        {
            try
            {

                if (ccbeCustomer.Properties.Items.Count == 0)
                {
                    XtraMessageBox.Show($"You must select a factory/factories", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (ccbeItemGroup.Properties.Items.Count == 0)
                {
                    XtraMessageBox.Show($"You must select a item type/s", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if(lueType.EditValue == null)
                {
                    XtraMessageBox.Show($"You must select a type", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (dateEditDateIni.EditValue == null)
                {
                    XtraMessageBox.Show($"You must select a start date", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (dateEditDateEnd.EditValue == null)
                {
                    XtraMessageBox.Show($"You must select a end date", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (Int32.Parse(lblDateIniWeek.Text) > Int32.Parse(lblDateEndWeek.Text))
                {
                    XtraMessageBox.Show($"End date must be greater than end date", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                return true;
            }
            catch
            {
                throw;
            }
        }

        private void GenerateChartRawMaterialHardware(List<AuxDashboardQPStoredProcedure> charData)
        {
            try
            {
                //generamos los gráficos para MT y para HW
                GenerateLineChartDashboard(charData.Where(a => a.ID_ITEM_GROUP.Equals(Constants.ITEM_GROUP_MT)).ToList(), chartControlMT, "Factories MT");
                GenerateLineChartDashboard(charData.Where(a => a.ID_ITEM_GROUP.Equals(Constants.ITEM_GROUP_HW)).ToList(), chartControlHW, "Factories HW");
            }
            catch
            {
                throw;
            }
        }

        private void GenerateLineChartDashboard(List<AuxDashboardQPStoredProcedure> charData, ChartControl chartObject, string chartTitle )
        {
            try
            {
                //reaprovechamos la clase auxíliar, no tendrá detalles de items, sólo tendrá 
                //los datos de fábrica, semana, cantidades, tipo (MT o HW), cantidad real y cantidad teórica 
                //No podemos mezclar MT con HW ya que son cantidades diferentes


                chartObject.DataSource = null;
                chartObject.Series.Clear();

                if (charData.Count == 0)
                    return;

                //obtenemos las fábricas
                List<string> factories = charData.Select(a => a.ID_CUSTOMER).Distinct().ToList();

                List<Series> chartSeriesList = new List<Series>();

                string currentFactory = string.Empty;

                foreach (var factory in factories)
                {
                    if (factory != currentFactory)
                    {
                        currentFactory = factory;
                        Series tmpSeriesQtyOriginal = new Series($"{factory} - Original", ViewType.Line);
                        Series tmpSeriesQtyReal = new Series($"{factory} - Real", ViewType.Line);

                        //Buscamos los datos de esa fábrica
                        var factoryData = charData
                            .Where(a => a.ID_CUSTOMER.Equals(factory))
                            .OrderBy(b => b.WEEK_NUM).ToList();

                        foreach (var reg in factoryData)
                        {
                            tmpSeriesQtyOriginal.Points.Add(new SeriesPoint(reg.WEEK_NUM, reg.TOTAL_QTY_ORIGINAL));
                            tmpSeriesQtyReal.Points.Add(new SeriesPoint(reg.WEEK_NUM, reg.TOTAL_QTY));
                        }

                        // Set the numerical argument scale types for the series
                        tmpSeriesQtyOriginal.ArgumentScaleType = ScaleType.Qualitative;
                        tmpSeriesQtyReal.ArgumentScaleType = ScaleType.Qualitative;

                        // Access the view-type-specific options of the series.
                        ((LineSeriesView)tmpSeriesQtyOriginal.View).LineMarkerOptions.Kind = MarkerKind.Triangle;
                        ((LineSeriesView)tmpSeriesQtyOriginal.View).LineStyle.DashStyle = DashStyle.Dash;

                        ((LineSeriesView)tmpSeriesQtyReal.View).LineMarkerOptions.Kind = MarkerKind.Triangle;
                        ((LineSeriesView)tmpSeriesQtyReal.View).LineStyle.DashStyle = DashStyle.Dash;


                        //Add series to list
                        chartSeriesList.Add(tmpSeriesQtyOriginal);
                        chartSeriesList.Add(tmpSeriesQtyReal);
                    }
                }

                // Add the series to the chart.
                foreach (var serie in chartSeriesList)
                {
                    chartObject.Series.Add(serie);
                }

                // Access the type-specific options of the diagram.
                ((XYDiagram)chartObject.Diagram).EnableAxisXZooming = true;


                //((XYDiagram)chartObject.Diagram).AxisX.Label.TextPattern = "{A:d}";

                // Hide the legend (if necessary).
                //chartObject.Legend.Visibility = DefaultBoolean.False;

                // Add a title to the chart (if necessary).
                chartObject.Titles.Add(new ChartTitle());
                chartObject.Titles[0].Text = chartTitle;

                //chartObject.CustomDrawAxisLabel += ChartObject_CustomDrawAxisLabel;
            }
            catch
            {
                throw;
            }
        }

        private void GenerateBarChartDashboard(List<AuxDashboardQPStoredProcedure> charData, ChartControl chartObject, string chartTitle)
        {
            try
            {

                chartObject.DataSource = null;
                chartObject.Series.Clear();

                if (charData.Count == 0)
                    return;

                //obtenemos las fábricas
                List<string> factories = charData.Select(a => a.ID_CUSTOMER).Distinct().ToList();

                List<Series> chartSeriesList = new List<Series>();

                foreach (var factory in factories)
                {
                    Series tmpPercentageDesv = new Series(factory, ViewType.Bar);

                    //Calculos el datos

                    var tmpData = charData
                        .Where(a => a.ID_CUSTOMER.Equals(factory))
                        .GroupBy(b => b.WEEK_NUM)
                        .Select(c => 
                        new
                        {
                            WeekNum = c.Select(o => o.WEEK_NUM).FirstOrDefault(),
                            Total = c.Sum(i => i.PERCENTAGE_DEVIATION)
                        })
                        .ToList();

                    //obtenemos el total de líneas con valor
                    var countNum = charData
                        .Where(a => a.ID_CUSTOMER.Equals(factory) && a.PERCENTAGE_DEVIATION > 0)
                        .Count();

                    foreach(var reg in tmpData)
                    {
                        Series tmp = new Series($"{factory} - Week {reg.WeekNum}", ViewType.Bar);
                        tmp.Points.Add(new SeriesPoint(reg.WeekNum, (reg.Total / countNum)));
                        tmp.ArgumentScaleType = ScaleType.Qualitative; //para que aparezca sólo el valor de la semana y no haga unas escala entre ellas
                        chartObject.Series.Add(tmp);
                    }

                }

                (chartObject.SeriesTemplate.View as BarSeriesView).AxisY.Label.TextPattern = "{V:P2}"; //Y axis percentage

                chartObject.Titles.Add(new ChartTitle());
                chartObject.Titles[0].Text = chartTitle;
            }
            catch
            {
                throw;
            }
        }

        void ShowDrilldown(PivotDrillDownDataSource ds)
        {
            try
            {
                XtraForm form = new XtraForm();
                form.Text = "Detail";
                form.StartPosition = FormStartPosition.CenterParent;
                DataGridView grid = new DataGridView();
                grid.Parent = form;
                grid.Dock = DockStyle.Fill;
                grid.DataSource = ds;
                form.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath); 
                form.Width = 820;
                form.Height = 400;
                form.ShowDialog();
                form.Dispose();
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #endregion


    }

    public class AuxDashboardQPStoredProcedure
    {
        //Para el Entity Framework tienen que coincidir los nombres exactos de las propiedades públicas con los nombres de columnas que devuelve el SQL
        public int WEEK_NUM { get; set; }
        public string ID_CUSTOMER { get; set; }
        public string ID_ITEM_BCN { get; set; }
        public string ID_ITEM_GROUP { get; set; }
        public string UNIT { get; set; }
        public decimal TOTAL_QTY_ORIGINAL { get; set; }
        public decimal TOTAL_QTY { get; set; }
        public decimal ABSOLUTE_DEVIATION { get; set; }
        public decimal PERCENTAGE_DEVIATION { get; set; }
    }
}
