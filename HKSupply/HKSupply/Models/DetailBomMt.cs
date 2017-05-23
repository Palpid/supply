using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKSupply.Models
{
    [Table("DETAIL_BOM_MT")]
    public class DetailBomMt
    {
        [Column("ID_BOM", Order = 0), Key]
        public int IdBom { get; set; }

        [Column("ID_ITEM_BCN", TypeName = "NVARCHAR", Order = 1), Key, StringLength(50)]
        public string IdItemBcn { get; set; }

        [ForeignKey("IdItemBcn")]
        public ItemMt Item { get; set; }

        [Column("QUANTITY", TypeName = "NUMERIC")]
        public decimal Quantity { get; set; }

        [Column("WASTE", TypeName = "NUMERIC")]
        public decimal Waste { get; set; }

        #region Equal

        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return false;

            DetailBomMt detailBomMt = (DetailBomMt)obj;

            bool res = (
                IdBom == detailBomMt.IdBom &&
                IdItemBcn == detailBomMt.IdItemBcn &&
                Quantity == detailBomMt.Quantity &&
                Waste == detailBomMt.Waste
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
