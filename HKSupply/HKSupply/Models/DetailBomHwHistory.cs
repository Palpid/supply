using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKSupply.Models
{
    [Table("DETAIL_BOM_HW_HISTORY")]
    public class DetailBomHwHistory
    {
        [Column("ID_BOM", Order = 0), Key]
        public int IdBom { get; set; }
        [ForeignKey("IdBom")]
        public ItemBom ItemBom { get; set; }

        [Column("ID_ITEM_BCN", TypeName = "NVARCHAR", Order = 1), Key, StringLength(50)]
        public string IdItemBcn { get; set; }

        [ForeignKey("IdItemBcn")]
        public ItemHw Item { get; set; }

        [Column("ID_BOM_BREAKDOWN", TypeName = "NVARCHAR", Order = 5), Key, StringLength(100)]
        public string IdBomBreakdown { get; set; }

        [ForeignKey("IdBomBreakdown")]
        public BomBreakdown BomBreakdown { get; set; }

        [Column("ID_VER", Order = 2), Key]
        public int IdVer { get; set; }

        [Column("ID_SUBVER", Order = 3), Key]
        public int IdSubVer { get; set; }

        [Column("TIMESTAMP", Order = 4), Key]
        public DateTime Timestamp { get; set; }

        [Column("SCRAP", TypeName = "NUMERIC")]
        public decimal? Scrap { get; set; }

        [Column("QUANTITY", TypeName = "NUMERIC")]
        public decimal Quantity { get; set; }

        [Column("USER"), StringLength(20)]
        public string User { get; set; }

        #region Equals
        public override bool Equals(object obj)
        {
            if (obj == null | obj == DBNull.Value)
                return false;

            DetailBomHwHistory detailBomHwHistory = (DetailBomHwHistory)obj;

            bool res = (
                IdBom == detailBomHwHistory.IdBom &&
                IdItemBcn == detailBomHwHistory.IdItemBcn &&
                IdBomBreakdown == detailBomHwHistory.IdBomBreakdown &&
                IdVer == detailBomHwHistory.IdVer &&
                IdSubVer == detailBomHwHistory.IdSubVer &&
                Timestamp == detailBomHwHistory.Timestamp &&
                Quantity == detailBomHwHistory.Quantity &&
                Scrap == detailBomHwHistory.Scrap &&
                User == detailBomHwHistory.User
                );

            return res;

        }

        public override int GetHashCode()
        {
            int hashCode = (
                IdBom.GetHashCode() +
                (IdItemBcn == null ? 0 : IdItemBcn.GetHashCode()) +
                (IdBomBreakdown == null ? 0 : IdBomBreakdown.GetHashCode()) +
                IdVer.GetHashCode() + 
                IdSubVer.GetHashCode() + 
                Timestamp.GetHashCode() + 
                Quantity.GetHashCode() +  
                Scrap.GetHashCode() +
                (User == null ? 0 : User.GetHashCode())
                );

            return hashCode;
        }
        #endregion 

        public static implicit operator DetailBomHwHistory(DetailBomHw dbhw)
        {
            DetailBomHwHistory detailBomHwHistory = new DetailBomHwHistory();

            detailBomHwHistory.IdBom = dbhw.IdBom;
            detailBomHwHistory.IdItemBcn = dbhw.IdItemBcn;
            detailBomHwHistory.IdBomBreakdown = dbhw.IdBomBreakdown;
            detailBomHwHistory.Quantity = dbhw.Quantity;
            detailBomHwHistory.Scrap = dbhw.Scrap;

            return detailBomHwHistory;
        }
    }
}
