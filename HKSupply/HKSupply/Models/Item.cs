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
    [Table("ITEMS")]
    public class Item
    {
        [Column("ID_VER", Order = 1), Key]
        public int IdVer { get; set; }

        [Column("ID_SUBVER", Order = 2), Key]
        public int IdSubVer { get; set; }

        [Column("TIMESTAMP"), Required]
        public DateTime Timestamp { get; set; }

        [Column("ITEM_CODE", Order = 0, TypeName="NVARCHAR"), Key, StringLength(20)]
        public string ItemCode { get; set; }

        [Column("ITEM_NAME", TypeName = "NVARCHAR"), StringLength(100), Required]
        public string ItemName { get; set; }

        [Column("MODEL", TypeName = "NVARCHAR"), StringLength(100), Required]
        public string Model { get; set; }

        [Column("ACTIVE"), Required, DefaultValue(true)]
        public bool Active { get; set; }

        [Column("ID_STATUS"), Required]
        public int IdStatus { get; set; }

        [Column("LAUNCHED")]
        public DateTime? Launched{ get; set; }

        [Column("RETIRED")]
        public DateTime? Retired { get; set; }

        [Column("MM_FRONT", TypeName = "NUMERIC")]
        public decimal MmFront { get; set; }

        [Column("SIZE", TypeName = "NVARCHAR"), StringLength(30)]
        public string Size { get; set; }

        [Column("CATEGORY_NAME", TypeName = "NVARCHAR"), StringLength(30)]
        public string CategoryName { get; set; }

        [Column("CALIBER", TypeName = "NUMERIC")]
        public decimal Caliber { get; set; }

        #region Equal

        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return false;

            Item item = (Item)obj;

            bool res = (
                IdVer == item.IdVer &&
                IdSubVer == item.IdSubVer &&
                Timestamp == item.Timestamp &&
                ItemCode == item.ItemCode &&
                ItemName == item.ItemName &&
                Model == item.Model &&
                Active == item.Active &&
                IdStatus == item.IdStatus &&
                Launched == item.Launched &&
                Retired == item.Retired &&
                MmFront == item.MmFront &&
                Size == item.Size &&
                CategoryName == item.CategoryName &&
                Caliber == item.Caliber);

            return res;
        }

        public override int GetHashCode()
        {
            int hashCode =
                IdVer.GetHashCode() +
                IdSubVer.GetHashCode() +
                Timestamp.GetHashCode() +
                (ItemCode ?? string.Empty).GetHashCode() +
                (ItemName ?? string.Empty).GetHashCode() +
                (Model ?? string.Empty).GetHashCode() +
                Active.GetHashCode() +
                IdStatus.GetHashCode() +
                (Launched ?? new DateTime()).GetHashCode() +
                (Retired ?? new DateTime()).GetHashCode() +
                MmFront.GetHashCode() +
                (Size ?? string.Empty).GetHashCode() +
                (CategoryName ?? string.Empty).GetHashCode() +
                Caliber.GetHashCode();  

            return hashCode;
        }

        #endregion
    }
}
