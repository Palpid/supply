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
    [Table("ITEM_TYPE")]
    public class ItemType
    {
        [Column("ID_ITEM_TYPE", TypeName = "NVARCHAR"), Key, StringLength(100)]
        public string Id { get; set; }
        [Column("DESCRIPTION", TypeName = "NVARCHAR"), StringLength(500), Required]
        public string Description { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            ItemType itemType = (ItemType)obj;

            return (Id == itemType.Id);
        }

        public override int GetHashCode()
        {
            return (Id == null ? 0 : Id.GetHashCode());
        }
    }
}
