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
    [Table("STATUS_HK")]
    public class StatusHK
    {
        [Column("ID_STATUS_PROD"), Key]
        public int IdStatusProd { get; set; }
        [Column("DESCRIPTION", TypeName = "NVARCHAR"), StringLength(500), Required]
        public string Description { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            StatusHK statusProd = (StatusHK)obj;

            return (IdStatusProd == statusProd.IdStatusProd);
        }

        public override int GetHashCode()
        {
            return (IdStatusProd.GetHashCode());
        }
    }
}
