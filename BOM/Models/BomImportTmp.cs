using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOM.Models
{
    public class BomImportTmp
    {
        //																		
        public int Id { get; set; }
        public string ImportGUID { get; set; }
        public DateTime InsertDate { get; set; }
        public string Factory { get; set; }
        public string ItemCode { get; set; }
        public string ComponentCode { get; set; }
        public string BomBreakdown { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public decimal Density { get; set; }
        public int NumberOfParts { get; set; }
        public decimal Coefficient1 { get; set; }
        public decimal Coefficient2 { get; set; }
        public decimal Scrap { get; set; }
        public decimal Quantity { get; set; }
        public bool  Supplied { get; set; }
        public bool Imported { get; set; }
        public DateTime ImportDate { get; set; }
        public string ErrorMsg { get; set; }
    }
}
