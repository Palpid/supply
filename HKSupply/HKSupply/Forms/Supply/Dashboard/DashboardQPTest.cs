using HKSupply.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HKSupply.Helpers;
using DevExpress.XtraPivotGrid;
using DevExpress.Utils;
using DevExpress.Data.PivotGrid;
using HKSupply.General;
using System.Collections;
using DevExpress.XtraCharts;

namespace HKSupply.Forms.Supply.Dashboard
{
    public partial class DashboardQPTest : RibbonFormBase
    {
        #region Constants
        private const string FIELD_PORCENTUAL_DEVIATION = "UnboundFieldPorcentualDeviation";
        #endregion

        #region Private Members
        List<AuxDashboardQPStored> _auxDboardQPStoredList;
        //List<AuxDashboardQPStored2> _auxDboardQPStoredList2;
        #endregion

        #region Constructor
        public DashboardQPTest()
        {
            InitializeComponent();

            try
            {
                SetUpPivotGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Form Events
        private void DashboardQPTest_Load(object sender, EventArgs e)
        {
            try
            {
                LoadAuxDboardQPStoredList();
                //TestSimpleChart();
                DummyChart();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion


        #region Private Methods

        #region SetUp form Objects()

        private void SetUpPivotGrid()
        {
            try
            {
                //Create pivot grid fields
                PivotGridField fieldWeekNum = new PivotGridField() {FieldName = nameof(AuxDashboardQPStored.WEEK_NUM), Caption = "Week", Area = PivotArea.ColumnArea, Width = 100 };
                PivotGridField fieldIdCustomer = new PivotGridField() { FieldName = nameof(AuxDashboardQPStored.ID_CUSTOMER), Caption = "Factory", Area = PivotArea.RowArea, Width = 100 };
                PivotGridField fieldIdItemBcn = new PivotGridField() { FieldName = nameof(AuxDashboardQPStored.ID_ITEM_BCN), Caption = "Item", Area = PivotArea.RowArea, Width = 150 };
                PivotGridField fieldIdItemGroup = new PivotGridField() { FieldName = nameof(AuxDashboardQPStored.ID_ITEM_GROUP), Caption = "Type", Area = PivotArea.RowArea, Width = 70 };
                PivotGridField fieldUnit = new PivotGridField() { FieldName = nameof(AuxDashboardQPStored.UNIT), Caption = "Unit", Area = PivotArea.RowArea, Width = 70 };
                PivotGridField fieldTotalQtyOriginal = new PivotGridField() { FieldName = nameof(AuxDashboardQPStored.TOTAL_QTY_ORIGINAL), Caption = "QTY Original", Area = PivotArea.DataArea, Width = 100 };
                PivotGridField fieldTotalQty = new PivotGridField() { FieldName = nameof(AuxDashboardQPStored.TOTAL_QTY), Caption = "QTY Real", Area = PivotArea.DataArea, Width = 100 };
                PivotGridField fieldAbsoluteDeviation = new PivotGridField() { FieldName = nameof(AuxDashboardQPStored.ABSOLUTE_DEVIATION), Caption = "Abs Deviation", Area = PivotArea.DataArea, Width = 100 };
                //PivotGridField fieldPorcentualDeviation = new PivotGridField() { FieldName = nameof(AuxDashboardQPStored.PORCENTUAL_DEVIATION), Caption = "Porcentual Deviation", Area = PivotArea.DataArea, Width = 150 };

                //Unbound field para el % porque si le das formato porcentual a la celda multiplica por 100
                PivotGridField fieldPorcentualDeviation = new PivotGridField()
                {
                    FieldName = FIELD_PORCENTUAL_DEVIATION,
                    Caption = "% Deviation",
                    Area = PivotArea.DataArea,
                    Width = 85,
                    UnboundFieldName = FIELD_PORCENTUAL_DEVIATION,
                    UnboundType = DevExpress.Data.UnboundColumnType.Decimal
                };

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
                pivotGridControl1.Fields.Add(fieldWeekNum);
                pivotGridControl1.Fields.Add(fieldIdCustomer);
                pivotGridControl1.Fields.Add(fieldUnit);
                pivotGridControl1.Fields.Add(fieldIdItemBcn);
                pivotGridControl1.Fields.Add(fieldIdItemGroup);
                pivotGridControl1.Fields.Add(fieldTotalQtyOriginal);
                pivotGridControl1.Fields.Add(fieldTotalQty);
                pivotGridControl1.Fields.Add(fieldAbsoluteDeviation);
                pivotGridControl1.Fields.Add(fieldPorcentualDeviation);

                //Events
                pivotGridControl1.CustomUnboundFieldData += PivotGridControl1_CustomUnboundFieldData;
                pivotGridControl1.CustomAppearance += PivotGridControl1_CustomAppearance;
                pivotGridControl1.CustomCellValue += PivotGridControl1_CustomCellValue;

            }
            catch
            {
                throw;
            }
        }

        private void PivotGridControl1_CustomUnboundFieldData(object sender, CustomFieldDataEventArgs e)
        {
            try
            {
                if (e.Field == null) return;

                if (e.Field.FieldName == FIELD_PORCENTUAL_DEVIATION)
                {
                    decimal porcentualDeviation = (decimal)e.GetListSourceColumnValue(nameof(AuxDashboardQPStored.PERCENTAGE_DEVIATION));
                    e.Value = porcentualDeviation / 100;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PivotGridControl1_CustomAppearance(object sender, PivotCustomAppearanceEventArgs e)
        {
            try
            {
                if (e.DataField.FieldName == FIELD_PORCENTUAL_DEVIATION
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
                MessageBox.Show(ex.Message);
            }
        }

        #region Custom totals
        private void PivotGridControl1_CustomCellValue(object sender, PivotCellValueEventArgs e)
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

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

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
                    if (fieldName == FIELD_PORCENTUAL_DEVIATION)
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

        #endregion

        #endregion

        private void LoadAuxDboardQPStoredList()
        {
            try
            {

                _auxDboardQPStoredList = new List<AuxDashboardQPStored>();
                List<AuxDashboardQPStored> aux;

                using (var db = new HKSupplyContext())
                {
                    for (var week = 50; week < 52; week++)
                    {
                        aux = db.Database.SqlQuery<AuxDashboardQPStored>(GetQuery(week.ToString())).ToList();
                        _auxDboardQPStoredList = _auxDboardQPStoredList.Concat(aux).ToList();
                    }
                }

                pivotGridControl1.DataSource = _auxDboardQPStoredList;
               // pivotGridControl1.RetrieveFields();

                //---------------------------------------------------------------------------------------------------------
                //_auxDboardQPStoredList2 = new List<AuxDashboardQPStored2>();
                //List<AuxDashboardQPStored2> aux;

                //using (var db = new HKSupplyContext())
                //{
                //    for (var week = 50; week < 52; week++)
                //    {
                //        aux = db.Database.SqlQuery<AuxDashboardQPStored2>(GetQuery2(week.ToString())).ToList();
                //        _auxDboardQPStoredList2 = _auxDboardQPStoredList2.Concat(aux).ToList();
                //    }
                //}
                //pivotGridControl1.DataSource = _auxDboardQPStoredList2;
                //pivotGridControl1.RetrieveFields();

                //---------------------------------------------------------------------------------------------------------

                //using (var db = new HKSupplyContext())
                //{
                //    var aux = db.DataTable(GetQuery2("50"));

                //    pivotGridControl1.DataSource = aux;
                //    pivotGridControl1.RetrieveFields();
                //}

                //pivotGridControl1.DataSource = _auxDboardQPStoredList2;



            }
            catch
            {
                throw;
            }
        }

        private string GetQuery(string week)
        {
            try
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT ");
                query.Append("	DATEPART(week, h.DOC_DATE) as WEEK_NUM ");
                query.Append("	, h.ID_CUSTOMER ");
                query.Append("	, l.ID_ITEM_BCN ");
                query.Append("	, l.ID_ITEM_GROUP ");
                query.Append("	, CASE l.ID_ITEM_GROUP ");
                query.Append("		WHEN 'HW' THEN ihw.UNIT_SUPPLY ");
                query.Append("		WHEN 'MT' THEN imt.UNIT_SUPPLY ");
                query.Append("		ELSE '' ");
                query.Append("	END AS UNIT ");
                query.Append("	, SUM(l.QUANTITY_ORIGINAL)						AS TOTAL_QTY_ORIGINAL ");
                query.Append("	, SUM(l.QUANTITY)								AS TOTAL_QTY ");
                query.Append("	, (SUM(l.QUANTITY) - SUM(l.QUANTITY_ORIGINAL))	AS ABSOLUTE_DEVIATION ");
                query.Append("	, ((SUM(l.QUANTITY) - SUM(l.QUANTITY_ORIGINAL)) / SUM(l.QUANTITY_ORIGINAL))	 AS PORCENTUAL_DEVIATION ");
                query.Append("FROM DOC_HEAD h ");
                query.Append("INNER JOIN DOC_LINES l ON l.ID_DOC = h.ID_DOC ");
                query.Append("LEFT JOIN ITEMS_HW ihw ON ihw.ID_ITEM_BCN = l.ID_ITEM_BCN AND l.ID_ITEM_GROUP = 'HW' ");
                query.Append("LEFT JOIN ITEMS_MT imt ON imt.ID_ITEM_BCN = l.ID_ITEM_BCN AND l.ID_ITEM_GROUP = 'MT' ");
                query.Append("WHERE ");
                query.Append("	h.ID_SUPPLY_DOC_TYPE = 'QP'	AND ");
                query.Append($"	DATEPART(week, h.DOC_DATE) = {week} ");
                query.Append("GROUP BY DATEPART(week, h.DOC_DATE), h.ID_CUSTOMER, l.ID_ITEM_BCN, l.ID_ITEM_GROUP, ");
                query.Append("	CASE l.ID_ITEM_GROUP WHEN 'HW' THEN ihw.UNIT_SUPPLY WHEN 'MT' THEN imt.UNIT_SUPPLY ELSE '' END ");
                query.Append("ORDER BY h.ID_CUSTOMER, l.ID_ITEM_GROUP ASC, l.ID_ITEM_BCN DESC");

                return query.ToString();
            }
            catch
            {
                throw;
            }
        }

        private string GetQuery2(string week)
        {
            try
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT ");
                query.Append("	h.ID_DOC ");
                query.Append("	, DATEPART(week, h.DOC_DATE) as WEEK_NUM ");
                query.Append("	, h.ID_CUSTOMER ");
                query.Append("	, l.ID_ITEM_BCN ");
                query.Append("	, l.ID_ITEM_GROUP ");
                query.Append("	, CASE l.ID_ITEM_GROUP ");
                query.Append("		WHEN 'HW' THEN ihw.UNIT_SUPPLY ");
                query.Append("		WHEN 'MT' THEN imt.UNIT_SUPPLY ");
                query.Append("		ELSE '' ");
                query.Append("	END AS UNIT ");
                query.Append("	, l.QUANTITY_ORIGINAL ");
                query.Append("	, l.QUANTITY ");
                query.Append("FROM DOC_HEAD h ");
                query.Append("INNER JOIN DOC_LINES l ON l.ID_DOC = h.ID_DOC ");
                query.Append("LEFT JOIN ITEMS_HW ihw ON ihw.ID_ITEM_BCN = l.ID_ITEM_BCN AND l.ID_ITEM_GROUP = 'HW' ");
                query.Append("LEFT JOIN ITEMS_MT imt ON imt.ID_ITEM_BCN = l.ID_ITEM_BCN AND l.ID_ITEM_GROUP = 'MT' ");
                query.Append("WHERE ");
                query.Append("	h.ID_SUPPLY_DOC_TYPE = 'QP'	AND ");
                query.Append($"	DATEPART(week, h.DOC_DATE) = {week} ");
                query.Append("ORDER BY h.ID_CUSTOMER, l.ID_ITEM_GROUP ASC, l.ID_ITEM_BCN DESC");

                return query.ToString();
            }
            catch
            {
                throw;
            }
        }

        #region Chart
        private void TestSimpleChart()
        {
            try
            {
                // Create a line series.
                Series series1 = new Series("Series 1", ViewType.Line);

                // Add points to it.
                series1.Points.Add(new SeriesPoint(1, 2));
                series1.Points.Add(new SeriesPoint(2, 12));
                series1.Points.Add(new SeriesPoint(3, 14));
                series1.Points.Add(new SeriesPoint(10, 17));

        
                // Add the series to the chart.
                chartControl1.Series.Add(series1);

                // Set the numerical argument scale types for the series,
                // as it is qualitative, by default.
                series1.ArgumentScaleType = ScaleType.Numerical;

                // Access the view-type-specific options of the series.
                ((LineSeriesView)series1.View).LineMarkerOptions.Kind = MarkerKind.Triangle;
                ((LineSeriesView)series1.View).LineStyle.DashStyle = DashStyle.Dash;

                // Access the type-specific options of the diagram.
                ((XYDiagram)chartControl1.Diagram).EnableAxisXZooming = true;

                // Hide the legend (if necessary).
                //chartControl1.Legend.Visibility = DefaultBoolean.False;

                // Add a title to the chart (if necessary).
                chartControl1.Titles.Add(new ChartTitle());
                chartControl1.Titles[0].Text = "A Line Chart";
            }
            catch
            {
                throw;
            }
        }
        
        private void DummyChart()
        {
            try
            {
                List<AuxDashboardQPStored> dummyChartData = new List<AuxDashboardQPStored>();

                //------------- CR -------------//
                dummyChartData.Add(new AuxDashboardQPStored()
                {
                    WEEK_NUM = 50,
                    ID_CUSTOMER = "CR",
                    ID_ITEM_GROUP = "MT",
                    UNIT = "KG",
                    TOTAL_QTY_ORIGINAL = 23,
                    TOTAL_QTY = 20
                });

                dummyChartData.Add(new AuxDashboardQPStored()
                {
                    WEEK_NUM = 52,
                    ID_CUSTOMER = "CR",
                    ID_ITEM_GROUP = "MT",
                    UNIT = "KG",
                    TOTAL_QTY_ORIGINAL = 50,
                    TOTAL_QTY = 40
                });

                dummyChartData.Add(new AuxDashboardQPStored()
                {
                    WEEK_NUM = 51,
                    ID_CUSTOMER = "CR",
                    ID_ITEM_GROUP = "MT",
                    UNIT = "KG",
                    TOTAL_QTY_ORIGINAL = 48,
                    TOTAL_QTY = 50
                });

                //------------- FA -------------//
                dummyChartData.Add(new AuxDashboardQPStored()
                {
                    WEEK_NUM = 50,
                    ID_CUSTOMER = "FA",
                    ID_ITEM_GROUP = "MT",
                    UNIT = "KG",
                    TOTAL_QTY_ORIGINAL = 30,
                    TOTAL_QTY = 31
                });

                dummyChartData.Add(new AuxDashboardQPStored()
                {
                    WEEK_NUM = 52,
                    ID_CUSTOMER = "FA",
                    ID_ITEM_GROUP = "MT",
                    UNIT = "KG",
                    TOTAL_QTY_ORIGINAL = 45,
                    TOTAL_QTY = 40
                });

                dummyChartData.Add(new AuxDashboardQPStored()
                {
                    WEEK_NUM = 51,
                    ID_CUSTOMER = "FA",
                    ID_ITEM_GROUP = "MT",
                    UNIT = "KG",
                    TOTAL_QTY_ORIGINAL = 62,
                    TOTAL_QTY = 50
                });

                TestChart(dummyChartData);
            }
            catch
            {
                throw;
            }
        }

        private void TestChart(List<AuxDashboardQPStored> summary4Chart)
        {
            try
            {
                //reaprovechamos la clase auxíliar, no tendrá detalles de items, sólo tendrá 
                //los datos de fábrica, semana, cantidades, tipo (MT o HW), cantidad real y cantidad teórica 
                //No podemos mezclar MT con HW ya que son cantidades diferentes

                //obtenemos las fábricas
                List<string> factories = summary4Chart.Select(a => a.ID_CUSTOMER).Distinct().ToList();

                List<Series> chartSeriesList = new List<Series>();

                string currentFactory = string.Empty;

                foreach(var factory in factories)
                {
                    if (factory != currentFactory)
                    {
                        currentFactory = factory;
                        Series tmpSeriesQtyOriginal = new Series($"{factory} - Original", ViewType.Line);
                        Series tmpSeriesQtyReal = new Series($"{factory} - Real", ViewType.Line);

                        //Buscamos los datos de esa fábrica
                        var factoryData = summary4Chart
                            .Where(a => a.ID_CUSTOMER.Equals(factory))
                            .OrderBy(b => b.WEEK_NUM).ToList();

                        foreach(var reg in factoryData)
                        {
                            tmpSeriesQtyOriginal.Points.Add(new SeriesPoint(reg.WEEK_NUM, reg.TOTAL_QTY_ORIGINAL));
                            tmpSeriesQtyReal.Points.Add(new SeriesPoint(reg.WEEK_NUM, reg.TOTAL_QTY));
                        }

                        // Set the numerical argument scale types for the series, as it is qualitative, by default.
                        tmpSeriesQtyOriginal.ArgumentScaleType = ScaleType.Numerical;
                        tmpSeriesQtyReal.ArgumentScaleType = ScaleType.Numerical;

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
                foreach(var serie in chartSeriesList)
                {
                    chartControl1.Series.Add(serie);
                }

                // Access the type-specific options of the diagram.
                ((XYDiagram)chartControl1.Diagram).EnableAxisXZooming = true;


                //((XYDiagram)chartControl1.Diagram).AxisX.Label.TextPattern = "{A:d}";

                // Hide the legend (if necessary).
                //chartControl1.Legend.Visibility = DefaultBoolean.False;

                // Add a title to the chart (if necessary).
                chartControl1.Titles.Add(new ChartTitle());
                chartControl1.Titles[0].Text = "Factories MT";

                chartControl1.CustomDrawAxisLabel += ChartControl1_CustomDrawAxisLabel;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// En el eje de las X pintamos las semanas, pero queremos sólo la parte entera, no tiene sentido que el char genere los decimales entre semanas
        /// 50 -> 50.1 -> 50.2 .... -> 51
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChartControl1_CustomDrawAxisLabel(object sender, CustomDrawAxisLabelEventArgs e)
        {
            try
            {
                AxisBase axis = e.Item.Axis;
                if (axis is AxisX || axis is AxisX3D || axis is RadarAxisX)
                {
                    double axisValue = (double)e.Item.AxisValue;
                    e.Item.Text = Math.Truncate(axisValue).ToString();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #endregion

    }

    public class AuxDashboardQPStored
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

    public class AuxDashboardQPStored2
    {
        //Para el Entity Framework tienen que coincidir los nombres exactos de las propiedades públicas con los nombres de columnas que devuelve el SQL
        public string ID_DOC { get; set; }
        public int WEEK_NUM { get; set; }
        public string ID_CUSTOMER { get; set; }
        public string ID_ITEM_BCN { get; set; }
        public string ID_ITEM_GROUP { get; set; }
        public string UNIT { get; set; }
        public decimal TOTAL_QTY_ORIGINAL { get; set; }
        public decimal TOTAL_QTY { get; set; }

    }
}
