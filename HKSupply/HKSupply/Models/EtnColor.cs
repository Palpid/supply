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
    [Table("COLORS")]
    public class EtnColor
    {
        [Column("ID_COLOR", TypeName = "NVARCHAR"), Key, StringLength(30)]
        public string IdColor { get; set; }
        [Column("DESCRIPTION", TypeName="NVARCHAR"), StringLength(500), Required]
        public string Description { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            EtnColor color = (EtnColor)obj;

            return (IdColor == color.IdColor);
        }

        public override int GetHashCode()
        {
            return (IdColor == null ? 0 : IdColor.GetHashCode());
        }
    }
}
