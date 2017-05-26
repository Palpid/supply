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
    [Table("HWS_TYPE_L2")]
    public class HwTypeL2
    {
        [Column("ID_HW_TYPE_L2", TypeName = "NVARCHAR", Order = 0), Key, StringLength(100)]
        public string IdHwTypeL2 { get; set; }
        [Column("ID_HW_TYPE_L1", TypeName = "NVARCHAR", Order = 1), Key, StringLength(100)]
        public string IdHwTypeL1 { get; set; }
        [Column("DESCRIPTION", TypeName = "NVARCHAR"), StringLength(500), Required]
        public string Description { get; set; }

        [ForeignKey("IdHwTypeL1")]
        public HwTypeL1 HwTypeL1 { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            HwTypeL2 hwTypeL2 = (HwTypeL2)obj;

            return (IdHwTypeL2 == hwTypeL2.IdHwTypeL2);
        }

        public override int GetHashCode()
        {
            return (IdHwTypeL2 == null ? 0 : IdHwTypeL2.GetHashCode());
        }
    }
}
