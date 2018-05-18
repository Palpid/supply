using BOM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOM.Classes
{
    public class OitmExt : Oitm
    {
        public string TipArtDesc { get; set; }
        public Model Model { get; set; }
    }
}
