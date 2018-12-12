using BOM.Classes;
using BOM.General;
using BOM.Helpers;
using BOM.Models;
using DevExpress.Spreadsheet;
using DevExpress.Spreadsheet.Export;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraSpreadsheet;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BOM.Forms
{
    public partial class BomImport : Form
    {
        #region Private Members
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        BindingList<BomImportTmp> _bomImportTmp;

        #endregion

        #region Constructor
        public BomImport()
        {
            InitializeComponent();

            try
            {
                SetUpEvents();
                SetUpGrdImport();
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Form Events

        private void BtnProcesar_Click(object sender, EventArgs e)
        {
            try
            {
                if(_bomImportTmp == null || _bomImportTmp.Count == 0)
                {
                    XtraMessageBox.Show("No hay cargado ningún fichero");
                    return;
                }

                if (ValidateExcelData() == false)
                    return;

                if (SaveBomsToDb())
                {
                    btnProcesar.Enabled = false;
                    string guid = _bomImportTmp.Select(a => a.ImportGUID).FirstOrDefault();
                    InitForm();
                    LoadImportTmp(guid);
                }

            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCargar_Click(object sender, EventArgs e)
        {
            try
            {
                LoadExcelFile();
                btnProcesar.Enabled = true;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnViewFile_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    if (string.IsNullOrEmpty(txtPathExcelFile.Text)) return;

                    if (File.Exists(txtPathExcelFile.Text))
                    {
                        string extension = Path.GetExtension(txtPathExcelFile.Text);
                        switch (extension.ToUpper())
                        {
                            case ".XLSX":
                            case ".XLS":
                                System.Diagnostics.Process.Start(txtPathExcelFile.Text);
                                break;
                        }
                    }
                    else
                    {
                        XtraMessageBox.Show("El fichero no existe");
                    }
                }
                catch (Exception ex)
                {
                    _log.Error(ex.Message, ex);
                    throw;
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnOpenFile_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog()
                {
                    //InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    Filter = "MS-Excel files (*.xls*)|*.xls*|All files (*.*)|*.*",
                    Multiselect = false,
                    RestoreDirectory = true,
                };
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtPathExcelFile.Text = openFileDialog.FileName;
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnOpenTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                OpenTemplate();
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDownloadXls_Click(object sender, EventArgs e)
        {
            try
            {
                DownloadExcel();
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GridViewImport_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;

                var row = view.GetRow(e.RowHandle) as BomImportTmp;
                if (string.IsNullOrEmpty(row.ErrorMsg) == false)
                {
                    e.Appearance.BackColor = Color.IndianRed;
                }
                view.RefreshRow(e.RowHandle);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void GridViewImport_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            try
            {
                if (e.Value is null)
                    return;

                DateTime dateZero = new DateTime(1, 1, 1);
                
                switch (e.Column.FieldName)
                {
                    case nameof(BomImportTmp.ImportDate):
                    case nameof(BomImportTmp.InsertDate):
                        if ((DateTime)e.Value == dateZero)
                            e.DisplayText = string.Empty;
                        break;

                    case nameof(BomImportTmp.Length):
                    case nameof(BomImportTmp.Width):
                    case nameof(BomImportTmp.Height):
                    case nameof(BomImportTmp.Density):
                    case nameof(BomImportTmp.Coefficient1):
                    case nameof(BomImportTmp.Coefficient2):
                    case nameof(BomImportTmp.Scrap):
                        if ((decimal)e.Value == 0)
                            e.DisplayText = string.Empty;
                        break;

                    case nameof(BomImportTmp.NumberOfParts):
                        if ((int)e.Value == 0)
                            e.DisplayText = string.Empty;
                        break;
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Private Methods

        #region SetUp Form Objetcs
        private void SetUpEvents()
        {
            try
            {
                btnOpenFile.Click += BtnOpenFile_Click;
                btnViewFile.Click += BtnViewFile_Click;
                btnCargar.Click += BtnCargar_Click;
                btnProcesar.Click += BtnProcesar_Click;
                btnOpenTemplate.Click += BtnOpenTemplate_Click;
                btnDownloadXls.Click += BtnDownloadXls_Click;
            }
            catch
            {
                throw;
            }
        }

        private void SetUpGrdImport()
        {
            try
            {
                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                gridViewImport.OptionsView.ColumnAutoWidth = false;
                gridViewImport.HorzScrollVisibility = ScrollVisibility.Auto;

                //Todo el Grid no editable
                gridViewImport.OptionsBehavior.Editable = false;

                //Specific columns
                GridColumn colFactory = new GridColumn() { Caption = "Factory", Visible = true, FieldName = nameof(BomImportTmp.Factory), Width = 200 };
                GridColumn colItemCode = new GridColumn() { Caption = "Item Code", Visible = true, FieldName = nameof(BomImportTmp.ItemCode), Width = 200 };
                GridColumn colComponentCode = new GridColumn() { Caption = "Component Code", Visible = true, FieldName = nameof(BomImportTmp.ComponentCode), Width = 200 };
                GridColumn colBomBreakdown = new GridColumn() { Caption = "Bom Breakdown", Visible = true, FieldName = nameof(BomImportTmp.BomBreakdown), Width = 200 };
                GridColumn colLength = new GridColumn() { Caption = "Length", Visible = true, FieldName = nameof(BomImportTmp.Length), Width = 200 };
                GridColumn colWidth = new GridColumn() { Caption = "Width", Visible = true, FieldName = nameof(BomImportTmp.Width), Width = 200 };
                GridColumn colHeight = new GridColumn() { Caption = "Height", Visible = true, FieldName = nameof(BomImportTmp.Height), Width = 200 };
                GridColumn colDensity = new GridColumn() { Caption = "Density", Visible = true, FieldName = nameof(BomImportTmp.Density), Width = 200 };
                GridColumn colNumberOfParts = new GridColumn() { Caption = "Number Of Parts", Visible = true, FieldName = nameof(BomImportTmp.NumberOfParts), Width = 200 };
                GridColumn colCoefficient1 = new GridColumn() { Caption = "Coefficient 1", Visible = true, FieldName = nameof(BomImportTmp.Coefficient1), Width = 200 };
                GridColumn colCoefficient2 = new GridColumn() { Caption = "Coefficient 2", Visible = true, FieldName = nameof(BomImportTmp.Coefficient2), Width = 200 };
                GridColumn colScrap = new GridColumn() { Caption = "Scrap", Visible = true, FieldName = nameof(BomImportTmp.Scrap), Width = 200 };
                GridColumn colQuantity = new GridColumn() { Caption = "Quantity", Visible = true, FieldName = nameof(BomImportTmp.Quantity), Width = 200 };
                GridColumn colSupplied = new GridColumn() { Caption = "Supplied", Visible = true, FieldName = nameof(BomImportTmp.Supplied), Width = 200 };
                GridColumn colImported = new GridColumn() { Caption = "Imported", Visible = true, FieldName = nameof(BomImportTmp.Imported), Width = 200 };
                GridColumn colImportDate = new GridColumn() { Caption = "Import Date", Visible = true, FieldName = nameof(BomImportTmp.ImportDate), Width = 200 };
                GridColumn colErrorMsg = new GridColumn() { Caption = "Error Msg.", Visible = true, FieldName = nameof(BomImportTmp.ErrorMsg), Width = 200 };

                //Display format
                colImportDate.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                gridViewImport.Columns.Add(colFactory);
                gridViewImport.Columns.Add(colItemCode);
                gridViewImport.Columns.Add(colComponentCode);
                gridViewImport.Columns.Add(colBomBreakdown);
                gridViewImport.Columns.Add(colLength);
                gridViewImport.Columns.Add(colWidth);
                gridViewImport.Columns.Add(colHeight);
                gridViewImport.Columns.Add(colDensity);
                gridViewImport.Columns.Add(colNumberOfParts);
                gridViewImport.Columns.Add(colCoefficient1);
                gridViewImport.Columns.Add(colCoefficient2);
                gridViewImport.Columns.Add(colScrap);
                gridViewImport.Columns.Add(colQuantity);
                gridViewImport.Columns.Add(colSupplied);
                gridViewImport.Columns.Add(colImported);
                gridViewImport.Columns.Add(colImportDate);
                gridViewImport.Columns.Add(colErrorMsg);

                //Events
                gridViewImport.RowCellStyle += GridViewImport_RowCellStyle;
                gridViewImport.CustomColumnDisplayText += GridViewImport_CustomColumnDisplayText;

            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Excel

        private void LoadExcelFile()
        {

            if (File.Exists(txtPathExcelFile.Text))
            {

                try
                {
                    OpenWaitForm("Procesando fichero Excel");

                    string excelFile = txtPathExcelFile.Text;

                    using (SpreadsheetControl spreadsheet = new SpreadsheetControl())
                    {
                        spreadsheet.LoadDocument(excelFile);
                        Worksheet worksheet = spreadsheet.ActiveWorksheet;

                        Range range = worksheet.GetDataRange();

                        //Si se pone a true el nombre de las columnas del dt es el de la cabecera del excel,en false para que las genere como "Column1, Column2, ColumnN"
                        bool rangeHasHeaders = true;
                        DataTable dataTable = worksheet.CreateDataTable(range, rangeHasHeaders);

                        //Validate cell value types. If cell value types in a column are different, the column values are exported as text.
                        for (int col = 0; col < range.ColumnCount; col++)
                        {
                            CellValueType cellType = range[0, col].Value.Type;
                            for (int r = 1; r < range.RowCount; r++)
                            {
                                if (cellType != range[r, col].Value.Type)
                                {
                                    dataTable.Columns[col].DataType = typeof(string);
                                    break;
                                }
                            }
                        }

                        // Create the exporter that obtains data from the specified range, 
                        // skips the header row (if required) and populates the previously created data table. 
                        DataTableExporter exporter = worksheet.CreateDataTableExporter(range, dataTable, rangeHasHeaders);

                        // Handle value conversion errors.
                        exporter.CellValueConversionError += Exporter_CellValueConversionError;

                        // Perform the export.
                        exporter.Export();

                        ExcelDtToObjectList(dataTable);
                        LoadGrid();

                    }
                }
                catch
                {
                    throw;
                }
                finally
                {
                    CloseWaitForm();
                }
            }

        }

        private void Exporter_CellValueConversionError(object sender, CellValueConversionErrorEventArgs e)
        {
            MessageBox.Show("Error en la celda " + e.Cell.GetReferenceA1());
            e.DataTableValue = null;
            e.Action = DataTableExporterAction.Continue;
        }

        private void ExcelDtToObjectList(DataTable dt)
        {
            try
            {
                _bomImportTmp = new BindingList<BomImportTmp>();
                string guid = Guid.NewGuid().ToString();

                foreach (DataRow row in dt.Rows)
                {
                    BomImportTmp bomimportRow = new BomImportTmp();
                    //bomimportRow.Id 
                    bomimportRow.ImportGUID = guid;
                    //bomimportRow.InsertDate
                    bomimportRow.Factory = row[Constants.XLS_FACTORY].ToString().Trim();
                    bomimportRow.ItemCode = row[Constants.XLS_ITEM_CODE].ToString().Trim();
                    bomimportRow.ComponentCode = row[Constants.XLS_COMPONENT_CODE].ToString().Trim();
                    bomimportRow.BomBreakdown = row[Constants.XLS_BOMBREAKDOWN].ToString().Trim().ToUpper();
                    bomimportRow.Length = row[Constants.XLS_LENGTH].ObjToDecimal();
                    bomimportRow.Width = row[Constants.XLS_WIDTH].ObjToDecimal();
                    bomimportRow.Height = row[Constants.XLS_HEIGHT].ObjToDecimal();
                    bomimportRow.Density = row[Constants.XLS_DENSITY].ObjToDecimal();
                    bomimportRow.NumberOfParts = row[Constants.XLS_NUMBER_OF_PARTS].ObjToInt();
                    bomimportRow.Coefficient1 = row[Constants.XLS_COEFFICIENT1].ObjToDecimal();
                    bomimportRow.Coefficient2 = row[Constants.XLS_COEFFICIENT2].ObjToDecimal();
                    bomimportRow.Scrap = row[Constants.XLS_SCRAP].ObjToDecimal();
                    bomimportRow.Quantity = Math.Round(row[Constants.XLS_QUANTITY].ObjToDecimal(), 4);
                    bomimportRow.Supplied = row[Constants.XLS_SUPPLIED].ToString().Trim() == "Y" ? true : false;
                    //bomimportRow.Imported
                    //bomimportRow.ImportDate
                    //bomimportRow.ErrorMsg
                    _bomImportTmp.Add(bomimportRow);
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Aux Methods

        private void InitForm()
        {
            try
            {
                txtPathExcelFile.EditValue = null;
                _bomImportTmp = null;
                LoadGrid();
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
                grdImport.DataSource = null;
                grdImport.DataSource = _bomImportTmp;
                gridViewImport.BestFitColumns();
            }
            catch
            {
                throw;
            }
        }

        private void OpenWaitForm(string msg)
        {
            try
            {
                SplashScreenManager.ShowForm(typeof(WaitForm1));
                SplashScreenManager.Default.SetWaitFormCaption(msg);
                SplashScreenManager.Default.SetWaitFormDescription("Por Favor Espere");
            }
            catch
            {
                throw;
            }
        }

        private void CloseWaitForm()
        {
            SplashScreenManager.CloseForm(false);
        }

        private void OpenTemplate()
        {
            try
            {
                string tmpPath = Path.GetTempFileName();
                tmpPath = Path.ChangeExtension(tmpPath, ".xlsx");
                File.WriteAllBytes(tmpPath, Properties.Resources.BOM_Template);
                System.Diagnostics.Process.Start(tmpPath);
            }
            catch
            {
                throw;
            }
        }

        private void DownloadExcel()
        {
            try
            {
                if(_bomImportTmp != null && _bomImportTmp.Count > 0)
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                    saveFileDialog.FilterIndex = 0;
                    saveFileDialog.RestoreDirectory = true;
                    saveFileDialog.Title = "Export Grid To";
                    //saveFileDialog.CreatePrompt = true;

                    saveFileDialog.ShowDialog();
                    if (saveFileDialog.FileName != "")
                    {
                        gridViewImport.ExportToXlsx(saveFileDialog.FileName);
                        XtraMessageBox.Show("Fichero generado correctamente");
                    }


                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ContainsUnicodeCharacter(string input)
        {
            try
            {
                const int MaxAnsiCode = 255;
                return input.Any(c => c > MaxAnsiCode);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region CRUD

        private bool SaveBomsToDb()
        {
            try
            {
                OpenWaitForm("Generando BOM en la Base de Datos");
                //Generamos un guid nuevo cada vez que se intenta importar por seguridad
                string guid = Guid.NewGuid().ToString();
                _bomImportTmp.Select(a => { a.ImportGUID = guid; return a; }).ToList();

                return GlobalSetting.BomService.ImportBom(_bomImportTmp.ToList());
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CloseWaitForm();
            }
        }

        private void LoadImportTmp(string guid)
        {
            try

            {
                OpenWaitForm("Obteniendo resumen de resultados");
                _bomImportTmp = new BindingList<BomImportTmp>(GlobalSetting.BomService.GetImportBomByGuid(guid));
                LoadGrid();
            }
            catch
            {
                throw;
            }
            finally
            {
                CloseWaitForm();
            }
        }

        #endregion

        #region Validate
        private bool ValidateExcelData()
        {
            try
            {
                bool isExcelOk = true;

                //Validamos que no haya duplicados para Factory, itemCode, ComponentCode, Breakdown
                var duplicates = _bomImportTmp.GroupBy(a => new { a.Factory, a.ItemCode, a.ComponentCode, a.BomBreakdown })
                    .Where(g => g.Count() > 1)
                    .Select(g => g.Key)
                    .ToList();

                if (duplicates.Count > 0)
                {

                    
                    foreach(var dup in duplicates)
                    {
                        _bomImportTmp
                            .Where(a => a.Factory == dup.Factory && a.ItemCode == dup.ItemCode && a.ComponentCode == dup.ComponentCode && a.BomBreakdown == dup.BomBreakdown)
                            .Select(a => { a.ErrorMsg = "Línea duplicada"; return a; })
                            .ToList();
                    }

                    isExcelOk = false;
                }

                //Validamos que si se han indicado los campos de length, width, etc hay que comprobar que el cálculo coincida con el cantidad indicada
                //Comprobamos que no haya ningún caracter unicode. Como los excel las envían las fábricas con el Excel en Chino a veces se cuelan caracteres en unicode, como por ejemplo:
                //810228 (4) --> Correcto
                //810228 (4）--> Incorrecto
                //pero realmente es el mismo carácter y la DB se lo come.
                foreach (BomImportTmp row in _bomImportTmp)
                {
                    if(row.Length > 0 || row.Width > 0 || row.Height > 0 || row.Density > 0 || row.NumberOfParts > 0 || row.Coefficient1 > 0 || row.Coefficient2 > 0 || row.Scrap > 0)
                    {
                        decimal calcQty = row.Length * row.Width * row.Height * row.Density * row.NumberOfParts * row.Coefficient1 * row.Coefficient2 * row.Scrap;

                        if (calcQty != row.Quantity)
                        {
                            row.ErrorMsg += "|Cantidad errónea. No coincide con el cálculo";
                            isExcelOk = false;
                        }
                    }

                    if (ContainsUnicodeCharacter(row.ComponentCode))
                    {
                        row.ErrorMsg += "|El componente contiene algún carácter no permitido.";
                        isExcelOk = false;
                    }
                }

                if(isExcelOk == false)
                {
                    XtraMessageBox.Show("Existen líneas con errores");
                    gridViewImport.BestFitColumns();
                }
                return isExcelOk;
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
