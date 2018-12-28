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
        public List<BomDetail> Lines { get; set; }
        public bool Edited { get; set; }

        public string FactoryVersion
        {
            get
            {
                return $"{Factory} v.{Version}.{Subversion}";
            }
        }

        public Bom()
        {
            Lines = new List<BomDetail>();
            Edited = false;
        }
    }
}
