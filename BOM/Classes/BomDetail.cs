using BOM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOM.Classes
{
    public class BomDetail : BomLine
    {
        public BomBreakdown Breakdown { get; set; }
        public OitmExt Item { get; set; }

        public string User { get; set; }
        public DateTime VersionDate { get; set; }
    }
}
