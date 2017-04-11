using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Models
{
    [Table("INCOTERMS")]
    public class Incoterm
    {
        [Column("ID_INCOTERM", TypeName = "NVARCHAR"), Key, StringLength(8)]
        public string IdIncoterm{ get; set; }
        [Column("DESCRIPTION", TypeName = "NVARCHAR"), StringLength(500)]
        public string Description { get; set; }
        [Column("DESCRIPTION_ZH", TypeName = "NVARCHAR"), StringLength(500)]
        public string DescriptionZh { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Incoterm incoterm = (Incoterm)obj;

            return (IdIncoterm == incoterm.IdIncoterm);
        }

        public override int GetHashCode()
        {
            return (IdIncoterm == null ? 0 : IdIncoterm.GetHashCode());
        }
    }
}
