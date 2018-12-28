using BOM.Classes;
using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOM.Helpers
{
    public static class ExportHelper
    {

        public static void ExportBomExcel(List<Bom> boms, string excelPath)
        {
            try
            {
                DataTable bomDT = new DataTable();
                bomDT.Columns.Add("Factory", typeof(string));
                bomDT.Columns.Add("Item Code", typeof(string));
                bomDT.Columns.Add("Component code", typeof(string));
                bomDT.Columns.Add("BomBreakdown", typeof(string));
                bomDT.Columns.Add("Length", typeof(decimal));
                bomDT.Columns.Add("Width", typeof(decimal));
                bomDT.Columns.Add("Height", typeof(decimal));
                bomDT.Columns.Add("Density", typeof(decimal));
                bomDT.Columns.Add("NumberOfParts", typeof(int));
                bomDT.Columns.Add("Coefficient1", typeof(decimal));
                bomDT.Columns.Add("Coefficient2", typeof(decimal));
                bomDT.Columns.Add("Scrap", typeof(decimal));
                bomDT.Columns.Add("Quantity", typeof(decimal));
                bomDT.Columns.Add("Supplied", typeof(string));

                foreach(var bom in boms)
                {
                    foreach(var det in bom.Lines)
                    {
                        bomDT.Rows.Add(
                        bom.Factory,
                        bom.ItemCode,
                        det.ItemCode,
                        det.BomBreakdown,
                        det.Length,
                        det.Width,
                        det.Height,
                        det.Density,
                        det.NumberOfParts,
                        det.Coefficient1,
                        det.Coefficient2,
                        det.Scrap,
                        det.Quantity,
                        det.Supplied == true ? "Y" : "N"
                        );
                    }
                    
                }

                Workbook wb = new Workbook();
                wb.Worksheets[0].Import(bomDT, true, 0, 0);
                wb.SaveDocument(excelPath, DocumentFormat.Xlsx);

            }
            catch
            {
                throw;
            }
        }
    }
}
