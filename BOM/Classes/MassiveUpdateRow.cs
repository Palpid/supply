using BOM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOM.Classes
{
    public class MassiveUpdateRow : BomHead
    {
        public string FactoryName { get; set; }
        public string U_ETN_stat { get; set; }
        public string ComponentCode { get; set; }
        public decimal ComponentQuantity { get; set; }
    }
}
