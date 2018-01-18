using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.OleDb;
using System.Data;
using HKSupply.PRJ_Stocks.Classes;

namespace HKSupply.PRJ_Stocks.Helpers
{
    public class Excel
    {

        public static List<Stocks.StockMove> OpenFileImportStk(string FileName, Classes.Stocks STK)
        {
            try {
                List<Stocks.StockMove> LS = new List<Classes.Stocks.StockMove>();
                var fullFileName = string.Format("{0}\\{1}", Directory.GetCurrentDirectory(), FileName);
                if (!File.Exists(FileName))
                {
                    System.Windows.Forms.MessageBox.Show("File not found");
                    return null;
                }
                using (DevExpress.XtraSpreadsheet.SpreadsheetControl spreadsheet = new DevExpress.XtraSpreadsheet.SpreadsheetControl())
                {

                    spreadsheet.LoadDocument(FileName);

                    DevExpress.Spreadsheet.Worksheet ws = spreadsheet.ActiveWorksheet;
                    DevExpress.Spreadsheet.Range range = ws.GetDataRange();

                    if (ws.Columns[0][range.TopRowIndex].Value.ToString() == "ID_WAREHOUSE" &&
                        ws.Columns[1][range.TopRowIndex].Value.ToString() == "WAREHOUSE_TYPE" &&
                        ws.Columns[2][range.TopRowIndex].Value.ToString() == "ID_ITEM" &&
                        ws.Columns[3][range.TopRowIndex].Value.ToString() == "LOT" &&
                        ws.Columns[4][range.TopRowIndex].Value.ToString() == "QUANTITY")
                    {
                        for (int i = range.TopRowIndex + 1; i <= range.BottomRowIndex; i++)
                        {

                            string idWAre = ws.Columns[0][i].Value.ToString();
                            int idWAreType = Convert.ToInt32(ws.Columns[1][i].Value.ToString());
                            string idItem = ws.Columns[2][i].Value.ToString();
                            string Lot = ws.Columns[3][i].Value.ToString();
                            decimal QTT = Convert.ToDecimal(ws.Columns[4][i].Value.ToString());

                            Stocks.Warehouse wr = new Stocks.Warehouse();
                            wr.idWareHouse = idWAre;
                            wr.idWareHouseType = idWAreType;                            
                            Stocks.StockMove mv = new Stocks.StockMove(Stocks.StockMovementsType.Entry, idWAre, wr.Descr, idWAreType, idItem, "", Lot,QTT);
                            LS.Add(mv);                            
                        }
                    }
                    else
                    {
                        throw new Exception("Incorrect extel template");
                    }
                        
                }
                return LS;
            }
            catch
            {
                throw;
            }
        }

        public static void CreateTemplate(string FileName, List<Stocks.Warehouse> LstWares, List<Stocks.ItemBcn> LstItemsBCN)
        {
            //var fullFileName = string.Format("{0}\\{1}", Directory.GetCurrentDirectory(), fileName);            

            using (DevExpress.XtraSpreadsheet.SpreadsheetControl spreadsheet = new DevExpress.XtraSpreadsheet.SpreadsheetControl())
            {

                DevExpress.Spreadsheet.Worksheet ws = spreadsheet.ActiveWorksheet;
                ws.Name = "STOCK DATA";
                ws.Columns[0][0].Value = "ID_WAREHOUSE";
                ws.Columns[1][0].Value = "WAREHOUSE_TYPE";
                ws.Columns[2][0].Value = "ID_ITEM";
                ws.Columns[3][0].Value = "LOT";
                ws.Columns[4][0].Value = "QUANTITY";

                DevExpress.Spreadsheet.IWorkbook WB = spreadsheet.Document;
                WB.Worksheets.Add("WAREHOUSES");

                DevExpress.Spreadsheet.Worksheet ws2 = WB.Worksheets["WAREHOUSES"];

                ws2.Columns[0][0].Value = "WAREHOUSE LIST. For your reference.";

                ws2.Columns[0][2].Value = "WAREHOUSE NAME";
                ws2.Columns[1][2].Value = "ID_WAREHOUSE";
                ws2.Columns[2][2].Value = "WAREHOUSE_TYPE";
                
                int FilaAct = 3;
                foreach (Stocks.Warehouse wr in LstWares)
                {
                    ws2.Columns[0][FilaAct].Value = wr.Descr;
                    ws2.Columns[1][FilaAct].Value = wr.idWareHouse;
                    ws2.Columns[2][FilaAct].Value = wr.idWareHouseType;
                    FilaAct += 1;
                }

                DevExpress.Spreadsheet.IWorkbook WB2 = spreadsheet.Document;
                WB.Worksheets.Add("ITEMS");

                DevExpress.Spreadsheet.Worksheet ws3 = WB.Worksheets["ITEMS"];

                ws3.Columns[0][0].Value = "ITEMS LIST. For your reference.";

                ws3.Columns[0][2].Value = "ID ITEM";
                ws3.Columns[1][2].Value = "ITEM DESCRIPTION";
                ws3.Columns[2][2].Value = "ITEM GROUP";

                FilaAct = 3;
                foreach (Stocks.ItemBcn itm in LstItemsBCN)
                {
                    ws3.Columns[0][FilaAct].Value = itm.ID_ITEM_BCN;
                    ws3.Columns[1][FilaAct].Value = itm.ITEM_DESCRIPTION;
                    ws3.Columns[2][FilaAct].Value = itm.ID_ITEM_GROUP;
                    FilaAct += 1;
                }

                WB.Worksheets.ActiveWorksheet = ws;

                try
                {
                    WB.SaveDocument(FileName, DevExpress.Spreadsheet.DocumentFormat.OpenXml);
                }
                catch 
                {                   
                    throw new Exception($"Unable to acces '{FileName}'. Is the file opened on XL?");
                }                
            }
        }
    }
}
