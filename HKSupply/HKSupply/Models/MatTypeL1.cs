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
    [Table("MAT_TYPE_L1")]
    public class MatTypeL1
    {
        [Column("ID_MAT_TYPE_L1", TypeName = "NVARCHAR"), Key, StringLength(100)]
        public string IdMatTypeL1 { get; set; }
        [Column("DESCRIPTION", TypeName = "NVARCHAR"), StringLength(500), Required]
        public string Description { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            MatTypeL1 matTypeL1 = (MatTypeL1)obj;

            return (IdMatTypeL1 == matTypeL1.IdMatTypeL1);
        }

        public override int GetHashCode()
        {
            return (IdMatTypeL1 == null ? 0 : IdMatTypeL1.GetHashCode());
        }
    }
}
