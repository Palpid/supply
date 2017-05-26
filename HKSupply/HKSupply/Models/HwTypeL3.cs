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
    [Table("HWS_TYPE_L3")]
    public class HwTypeL3
    {
        [Column("ID_HW_TYPE_L3", TypeName = "NVARCHAR", Order = 0), Key, StringLength(100)]
        public string IdHwTypeL3 { get; set; }
        [Column("ID_HW_TYPE_L2", TypeName = "NVARCHAR", Order = 1), Key, StringLength(100), Required]
        public string IdHwTypeL2 { get; set; }
        [Column("ID_HW_TYPE_L1", TypeName = "NVARCHAR", Order = 2), Key, StringLength(100), Required]
        public string IdHwTypeL1 { get; set; }
        [Column("DESCRIPTION", TypeName = "NVARCHAR"), StringLength(500), Required]
        public string Description { get; set; }

        [ForeignKey("IdHwTypeL2, IdHwTypeL1")]
        public HwTypeL2 HwTypeL2 { get; set; }
        [ForeignKey("IdHwTypeL1")]
        public HwTypeL1 HwTypeL1 { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            HwTypeL3 hwTypeL3 = (HwTypeL3)obj;

            return (IdHwTypeL3 == hwTypeL3.IdHwTypeL3);
        }

        public override int GetHashCode()
        {
            return (IdHwTypeL3 == null ? 0 : IdHwTypeL3.GetHashCode());
        }
    }
}
