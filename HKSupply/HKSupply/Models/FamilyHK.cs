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
    [Table("FAMILIES_HK")]
    public class FamilyHK
    {
        [Column("ID_FAMILY_HK", TypeName = "NVARCHAR"), Key, StringLength(100)]
        public string IdFamilyHk { get; set; }
        [Column("DESCRIPTION", TypeName = "NVARCHAR"), StringLength(500), Required]
        public string Description { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            FamilyHK familyHK = (FamilyHK)obj;

            return (IdFamilyHk == familyHK.IdFamilyHk);
        }

        public override int GetHashCode()
        {
            return (IdFamilyHk == null ? 0 : IdFamilyHk.GetHashCode());
        }
    }
}
