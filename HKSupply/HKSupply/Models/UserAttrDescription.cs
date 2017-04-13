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
    [Table("USER_ATTR_DESCRIPTION")]
    public class UserAttrDescription
    {
        [Column("ID_USER_ATTR", TypeName = "NVARCHAR"), Key, StringLength(100)]
        public string IdUserAttr{ get; set; }
        [Column("DESCRIPTION", TypeName = "NVARCHAR"), StringLength(500)]
        public string Description{ get; set; }
        [Column("ID_ITEM_GROUP", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdItemGroup { get; set; }
        [ForeignKey("IdItemGroup")]
        public ItemGroup ItemGroup { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            UserAttrDescription attr = (UserAttrDescription)obj;

            return (IdUserAttr == attr.IdUserAttr);
        }

        public override int GetHashCode()
        {
            return (IdUserAttr == null ? 0 : IdUserAttr.GetHashCode());
        }
    }
}
