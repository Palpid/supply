using BOM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOM.Classes
{
    public class Bom : BomHead
    {
        //public List<BomLine> RawMaterial { get; set; }
        public List<BomDetail> Lines { get; set; }
    }
}
