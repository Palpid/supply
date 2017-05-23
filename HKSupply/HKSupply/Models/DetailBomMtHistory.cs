using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKSupply.Models
{

    [Table("DETAIL_BOM_MT_HISTORY")]
    public class DetailBomMtHistory
    {
        [Column("ID_BOM", Order = 0), Key]
        public int IdBom { get; set; }
        [ForeignKey("IdBom")]
        public ItemBom ItemBom { get; set; }

        [Column("ID_ITEM_BCN", TypeName = "NVARCHAR", Order = 1), Key, StringLength(50)]
        public string IdItemBcn { get; set; }
        [ForeignKey("IdItemBcn")]
        public ItemMt Item { get; set; }

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
            if (obj == null || obj == DBNull.Value)
                return false;

            DetailBomMtHistory detailBomMtHistory = (DetailBomMtHistory)obj;

            bool res;

            return res = (
                IdBom == detailBomMtHistory.IdBom &&
                IdItemBcn == detailBomMtHistory.IdItemBcn &&
                IdVer == detailBomMtHistory.IdVer &&
                IdSubVer == detailBomMtHistory.IdSubVer &&
                Timestamp == detailBomMtHistory.Timestamp &&
                Quantity == detailBomMtHistory.Quantity &&
                Waste == detailBomMtHistory.Waste
                );
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

        public static implicit operator DetailBomMtHistory(DetailBomMt dbmt)
        {
            DetailBomMtHistory detailBomMtHistory = new DetailBomMtHistory();
            detailBomMtHistory.IdBom = dbmt.IdBom;
            detailBomMtHistory.IdItemBcn = dbmt.IdItemBcn;
            detailBomMtHistory.Quantity = dbmt.Quantity;
            detailBomMtHistory.Waste = dbmt.Waste;

            return detailBomMtHistory;
        }
    }
}
