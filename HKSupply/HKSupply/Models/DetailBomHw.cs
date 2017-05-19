using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKSupply.Models
{
    public class DetailBomHw
    {
        [Column("ID_BOM", Order = 0), Key]
        public int IdBom { get; set; }

        [Column("ID_ITEM_BCN", TypeName = "NVARCHAR", Order = 1), Key, StringLength(50)]
        public string IdItemBcn { get; set; }

        [ForeignKey("IdItemBcn")]
        public ItemHw Item { get; set; }

        [Column("QUANTITY", TypeName = "NUMERIC")]
        public decimal Quantity { get; set; }

        [Column("WASTE", TypeName = "NUMERIC")]
        public decimal Waste { get; set; }

        #region Equals

        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return false;

            DetailBomHw detailBomHw = (DetailBomHw)obj;

            bool res = (
                IdBom == detailBomHw.IdBom &&
                IdItemBcn == detailBomHw.IdItemBcn &&
                Quantity == detailBomHw.Quantity &&
                Waste == detailBomHw.Waste
                );

            return res;
        }

        public override int GetHashCode()
        {
            int hashCode = (
                IdBom.GetHashCode() +
                (IdItemBcn == null ? 0 : IdItemBcn.GetHashCode()) +
                Quantity.GetHashCode() +
                Waste.GetHashCode()
                );
            return hashCode;
        }

        #endregion
    }
}
