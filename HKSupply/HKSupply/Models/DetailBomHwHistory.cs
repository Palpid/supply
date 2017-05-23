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

        [Column("ID_VER", Order = 2), Key]
        public int IdVer { get; set; }

        [Column("ID_SUBVER", Order = 3), Key]
        public int IdSubVer { get; set; }

        [Column("TIMESTAMP", Order = 4), Key]
        public DateTime Timestamp { get; set; }

        [Column("QUANTITY", TypeName = "NUMERIC")]
        public decimal Quantity { get; set; }

        [Column("WASTE", TypeName = "NUMERIC")]
        public decimal Waste { get; set; }

        #region Equals
        public override bool Equals(object obj)
        {
            if (obj == null | obj == DBNull.Value)
                return false;

            DetailBomHwHistory detailBomHwHistory = (DetailBomHwHistory)obj;

            bool res = (
                IdBom == detailBomHwHistory.IdBom &&
                IdItemBcn == detailBomHwHistory.IdItemBcn &&
                IdVer == detailBomHwHistory.IdVer &&
                IdSubVer == detailBomHwHistory.IdSubVer &&
                Timestamp == detailBomHwHistory.Timestamp &&
                Quantity == detailBomHwHistory.Quantity &&
                Waste == detailBomHwHistory.Waste
                );

            return res;

        }

        public override int GetHashCode()
        {
            int hashCode = (
                IdBom.GetHashCode() +
                (IdItemBcn == null ? 0 : IdItemBcn.GetHashCode()) +
                IdVer.GetHashCode() + 
                IdSubVer.GetHashCode() + 
                Timestamp.GetHashCode() + 
                Quantity.GetHashCode() +  
                Waste.GetHashCode()
                );

            return hashCode;
        }
        #endregion 

        public static implicit operator DetailBomHwHistory(DetailBomHw dbhw)
        {
            DetailBomHwHistory detailBomHwHistory = new DetailBomHwHistory();

            detailBomHwHistory.IdBom = dbhw.IdBom;
            detailBomHwHistory.IdItemBcn = dbhw.IdItemBcn;
            detailBomHwHistory.Quantity = dbhw.Quantity;
            detailBomHwHistory.Waste = dbhw.Waste;

            return detailBomHwHistory;
        }
    }
}
