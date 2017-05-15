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
    [Table("MAT_TYPE_L2")]
    public class MatTypeL2
    {
        [Column("ID_MAT_TYPE_L2", TypeName = "NVARCHAR"), Key, StringLength(100)]
        public string IdMatTypeL2 { get; set; }
        [Column("ID_MAT_TYPE_L1", TypeName = "NVARCHAR"), StringLength(100), Required]
        public string IdMatTypeL1 { get; set; }
        [Column("DESCRIPTION", TypeName = "NVARCHAR"), StringLength(500), Required]
        public string Description { get; set; }

        [ForeignKey("IdMatTypeL1")]
        public MatTypeL1 MatTypeL1 { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            MatTypeL2 matTypeL2 = (MatTypeL2)obj;

            return (IdMatTypeL2 == matTypeL2.IdMatTypeL2);
        }

        public override int GetHashCode()
        {
            return (IdMatTypeL2 == null ? 0 : IdMatTypeL2.GetHashCode());
        }
    }
}
