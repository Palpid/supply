using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Classes
{
    public class Bom
    {
        public int Id { get; set; }
        public string IdItemBcn { get; set; }
        public string Description { get; set; }
        public string IdItemGroup { get; set; }
        public float Quantity { get; set; }
        public float Wastage { get; set; } 
    }
}
