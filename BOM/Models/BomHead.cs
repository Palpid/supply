using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOM.Models
{
    public class BomHead
    {
        public Int64 Code { get; set; }
        public int Version { get; set; }
        public int Subversion { get; set; }
        public DateTime VersionDate { get; set; }
        public string ItemCode { get; set; }
        public string Factory { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
