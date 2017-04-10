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
    [Table("STATUS_PROD")]
    public class StatusProd
    {
        [Column("ID", TypeName = "NVARCHAR"), Key, StringLength(30)]
        public string Id { get; set; }
        [Column("DESCRIPTION", TypeName = "NVARCHAR"), StringLength(100), Required]
        public string Description { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            StatusProd statusProd = (StatusProd)obj;

            return (Id == statusProd.Id);
        }

        public override int GetHashCode()
        {
            return (Id == null ? 0 : Id.GetHashCode());
        }
    }
}
