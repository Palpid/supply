using System;

namespace BOM.Models
{
    public class BomLineLog : BomLine
    {
        public int Version { get; set; }
        public int Subversion { get; set; }
        public DateTime VersionDate { get; set; }
        public string User { get; set; }
    }
}
