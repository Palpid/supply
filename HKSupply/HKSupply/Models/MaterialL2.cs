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
    [Table("MATERIALS_L2")]
    public class MaterialL2
    {
        [Column("ID_MATERIAL_L2", TypeName = "NVARCHAR", Order = 0), Key, StringLength(100)]
        public string IdMaterialL2 { get; set; }
        [Column("ID_MATERIAL_L1", TypeName = "NVARCHAR", Order = 1), Key, StringLength(100), Required]
        public string IdMaterialL1 { get; set; }
        [Column("DESCRIPTION", TypeName = "NVARCHAR"), StringLength(500), Required]
        public string Description { get; set; }

        [ForeignKey("IdMaterialL1")]
        public MaterialL1 MaterialL1 { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            MaterialL2 materialL2 = (MaterialL2)obj;

            return (IdMaterialL2 == materialL2.IdMaterialL2);
        }

        public override int GetHashCode()
        {
            return (IdMaterialL2 == null ? 0 : IdMaterialL2.GetHashCode());
        }
    }
}
