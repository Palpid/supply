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
    [Table("ITEMS_BCN")]
    public class ItemBcn
    {
        [Column("ID_ITEM_BCN", TypeName = "NVARCHAR"), Key, StringLength(50)]
        public string IdItemBcn { get; set; }
        [Column("DESCRIPTION", TypeName = "NVARCHAR"), StringLength(500)]
        public string Description { get; set; }

        #region equal
        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return false;

            ItemBcn itemBcn = (ItemBcn)obj;

            return (IdItemBcn == itemBcn.IdItemBcn);
        }

        public override int GetHashCode()
        {
            int hashCode = (IdItemBcn == null ? 0 : IdItemBcn.GetHashCode());
            return hashCode;
        }
        #endregion
    }
}
