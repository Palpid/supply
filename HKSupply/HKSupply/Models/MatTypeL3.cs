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
    [Table("MAT_TYPE_L3")]
    public class MatTypeL3
    {
        [Column("ID_MAT_TYPE_L3", TypeName = "NVARCHAR", Order = 0), Key, StringLength(100)]
        public string IdMatTypeL3 { get; set; }
        [Column("ID_MAT_TYPE_L2", TypeName = "NVARCHAR", Order = 1), Key, StringLength(100)]
        public string IdMatTypeL2 { get; set; }
        [Column("ID_MAT_TYPE_L1", TypeName = "NVARCHAR", Order = 2), Key, StringLength(100)]
        public string IdMatTypeL1 { get; set; }
        [Column("DESCRIPTION", TypeName = "NVARCHAR"), StringLength(500), Required]
        public string Description { get; set; }

        [ForeignKey("IdMatTypeL2, IdMatTypeL1")]
        public MatTypeL2 MatTypeL2 { get; set; }
        [ForeignKey("IdMatTypeL1")]
        public MatTypeL1 MatTypeL1 { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            MatTypeL3 matTypeL3 = (MatTypeL3)obj;

            return (IdMatTypeL3 == matTypeL3.IdMatTypeL3);
        }

        public override int GetHashCode()
        {
            return (IdMatTypeL3 == null ? 0 : IdMatTypeL3.GetHashCode());
        }
    }
}
