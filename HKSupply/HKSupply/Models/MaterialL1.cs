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
    [Table("MATERIALS_L1")]
    public class MaterialL1
    {
        [Column("ID_MATERIAL_L1", TypeName = "NVARCHAR"), Key, StringLength(100)]
        public string IdMaterialL1 { get; set; }
        [Column("DESCRIPTION", TypeName = "NVARCHAR"), StringLength(500), Required]
        public string Description { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            MaterialL1 materialL1 = (MaterialL1)obj;

            return (IdMaterialL1 == materialL1.IdMaterialL1);
        }

        public override int GetHashCode()
        {
            return (IdMaterialL1 == null ? 0 : IdMaterialL1.GetHashCode());
        }
    }
}
