using HKSupply.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Classes
{
    public class ItemEyBom 
    {
        public ItemEy ItemEy { get; set; }
        public List<Bom> RawMaterials { get; set; }
        public List<Bom> Hardware { get; set; }
    }
}
