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
    [Table("STATUS_CIAL")]
    public class StatusCial
    {
        [Column("ID_STATUS_CIAL"), Key]
        public int IdStatusCial { get; set; }
        [Column("DESCRIPTION", TypeName = "NVARCHAR"), StringLength(500), Required]
        public string Description { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            StatusCial statusCial = (StatusCial)obj;

            return (IdStatusCial == statusCial.IdStatusCial);
        }

        public override int GetHashCode()
        {
            return IdStatusCial.GetHashCode();
        }
    }
}
