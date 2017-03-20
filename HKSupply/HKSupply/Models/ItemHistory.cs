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
    [Table("ITEMS_HISTORY")]
    public class ItemHistory
    {
        [Column("ID_VER", Order = 1), Key]
        public int IdVer { get; set; }

        [Column("ID_SUBVER", Order = 2), Key]
        public int IdSubVer { get; set; }

        [Column("TIMESTAMP"), Required]
        public DateTime Timestamp { get; set; }

        [Column("ITEM_CODE", Order = 0, TypeName = "NVARCHAR"), Key, StringLength(20)]
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
        public DateTime Launched { get; set; }

        [Column("RETIRED")]
        public DateTime Retired { get; set; }

        [Column("MM_FRONT", TypeName = "NUMERIC")]
        public decimal MmFront { get; set; }

        [Column("SIZE", TypeName = "NVARCHAR"), StringLength(30)]
        public string Size { get; set; }

        [Column("CATEGORY_NAME", TypeName = "NVARCHAR"), StringLength(30)]
        public string CategoryName { get; set; }

        [Column("CALIBER", TypeName = "NUMERIC")]
        public decimal Caliber { get; set; }

        #region Equals
        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return false;

            ItemHistory itemHistory = (ItemHistory)obj;

            bool res = (
                IdVer == itemHistory.IdVer &&
                IdSubVer == itemHistory.IdSubVer &&
                Timestamp == itemHistory.Timestamp &&
                ItemCode == itemHistory.ItemCode &&
                ItemName == itemHistory.ItemName &&
                Model == itemHistory.Model &&
                Active == itemHistory.Active &&
                IdStatus == itemHistory.IdStatus &&
                Launched == itemHistory.Launched &&
                Retired == itemHistory.Retired &&
                MmFront == itemHistory.MmFront &&
                Size == itemHistory.Size &&
                CategoryName == itemHistory.CategoryName &&
                Caliber == itemHistory.Caliber);

            return res;
        }

        public override int GetHashCode()
        {
            int hashCode =
                IdVer.GetHashCode() +
                IdSubVer.GetHashCode() +
                Timestamp.GetHashCode() +
                ItemCode.GetHashCode() +
                ItemName.GetHashCode() +
                Model.GetHashCode() +
                Active.GetHashCode() +
                IdStatus.GetHashCode() +
                Launched.GetHashCode() +
                Retired.GetHashCode() +
                MmFront.GetHashCode() +
                Size.GetHashCode() +
                CategoryName.GetHashCode() +
                Caliber.GetHashCode();

            return hashCode;
        }
        #endregion

        public static implicit operator ItemHistory(Item i)
        {
            ItemHistory ih = new ItemHistory();
            ih.IdVer = i.IdVer;
            ih.IdSubVer = i.IdSubVer;
            ih.Timestamp = i.Timestamp;
            ih.ItemCode = i.ItemCode;
            ih.ItemName = i.ItemName;
            ih.Model = i.Model;
            ih.Active = i.Active;
            ih.IdStatus = i.IdStatus;
            ih.Launched = i.Launched;
            ih.Retired = i.Retired;
            ih.MmFront = i.MmFront;
            ih.Size = i.Size;
            ih.CategoryName = i.CategoryName;
            ih.Caliber = ih.Caliber;

            return ih;
        }
    }
}
