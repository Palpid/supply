using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKSupply.Models
{

    [Table("DETAIL_BOM_HW")]
    public class DetailBomHw
    {
        [Column("ID_BOM", Order = 0), Key]
        public int IdBom { get; set; }

        [Column("ID_ITEM_BCN", TypeName = "NVARCHAR", Order = 1), Key, StringLength(50)]
        public string IdItemBcn { get; set; }

        [ForeignKey("IdItemBcn")]
        public ItemHw Item { get; set; }

        [Column("ID_BOM_BREAKDOWN", TypeName = "NVARCHAR", Order = 2), Key, StringLength(100)]
        public string IdBomBreakdown { get; set; }
        [ForeignKey("IdBomBreakdown")]
        public BomBreakdown BomBreakdown { get; set; }

        [Column("SCRAP", TypeName = "NUMERIC")]
        public decimal? Scrap { get; set; }

        [Column("QUANTITY", TypeName = "NUMERIC")]
        public decimal Quantity { get; set; }

        #region Equals

        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return false;

            DetailBomHw detailBomHw = (DetailBomHw)obj;

            bool res = (
                IdBom == detailBomHw.IdBom &&
                IdItemBcn == detailBomHw.IdItemBcn &&
                IdBomBreakdown == detailBomHw.IdBomBreakdown && 
                Quantity == detailBomHw.Quantity &&
                Scrap == detailBomHw.Scrap
                );

            return res;
        }

        public override int GetHashCode()
        {
            int hashCode = (
                IdBom.GetHashCode() +
                (IdItemBcn == null ? 0 : IdItemBcn.GetHashCode()) +
                (IdBomBreakdown == null ? 0 : IdBomBreakdown.GetHashCode()) +
                Quantity.GetHashCode() +
                Scrap.GetHashCode()
                );
            return hashCode;
        }

        #endregion
    }
}
