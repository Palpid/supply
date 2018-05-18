using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOM.Models
{
    public class BomLineLog : BomLine
    {
        public int Version { get; set; }
        public int Subversion { get; set; }
        public DateTime VersionDate { get; set; }
    }
}
