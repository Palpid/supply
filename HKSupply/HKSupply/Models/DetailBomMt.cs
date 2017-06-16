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

        [Column("ID_BOM_BREAKDOWN", TypeName = "NVARCHAR", Order = 2), Key, StringLength(100)]
        public string IdBomBreakdown { get; set; }

        [ForeignKey("IdBomBreakdown")]
        public BomBreakdown BomBreakdown { get; set; }

        [Column("LENGTH", TypeName = "NUMERIC")]
        public decimal? Length { get; set; }

        [Column("WIDTH", TypeName = "NUMERIC")]
        public decimal? Width { get; set; }

        [Column("HEIGHT", TypeName = "NUMERIC")]
        public decimal? Height { get; set; }

        [Column("DENSITY", TypeName = "NUMERIC")]
        public decimal? Density { get; set; }

        [Column("NUMBER_OF_PARTS")]
        public int? NumberOfParts { get; set; }

        [Column("COEFFICIENT1", TypeName = "NUMERIC")]
        public decimal? Coefficient1 { get; set; }

        [Column("COEFFICIENT2", TypeName = "NUMERIC")]
        public decimal? Coefficient2 { get; set; }

        [Column("SCRAP", TypeName = "NUMERIC")]
        public decimal? Scrap { get; set; }

        [Column("QUANTITY", TypeName = "NUMERIC")]
        public decimal Quantity { get; set; }

        #region Equal

        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return false;

            DetailBomMt detailBomMt = (DetailBomMt)obj;

            bool res = (
                IdBom == detailBomMt.IdBom &&
                IdItemBcn == detailBomMt.IdItemBcn &&
                IdBomBreakdown == detailBomMt.IdBomBreakdown &&
                Length == detailBomMt.Length &&
                Width == detailBomMt.Width &&
                Height == detailBomMt.Height &&
                Density == detailBomMt.Density &&
                NumberOfParts == detailBomMt.NumberOfParts &&
                Coefficient1 == detailBomMt.Coefficient1 &&
                Coefficient2 == detailBomMt.Coefficient2 &&
                Scrap == detailBomMt.Scrap &&
                Quantity == detailBomMt.Quantity
                );

            return res;
        }

        public override int GetHashCode()
        {
            int hashCode = (
                IdBom.GetHashCode() + 
                (IdItemBcn == null ? 0 : IdItemBcn.GetHashCode()) +
                 (IdBomBreakdown == null ? 0 : IdBomBreakdown.GetHashCode()) +
                (Length == null ? 0 : Length.GetHashCode()) + 
                (Width == null ? 0 : Width.GetHashCode()) + 
                (Height == null ? 0 : Height.GetHashCode()) +
                (Density == null ? 0 : Density.GetHashCode()) + 
                (NumberOfParts == null ? 0 : NumberOfParts.GetHashCode()) +
                (Coefficient1 == null ? 0 : Coefficient1.GetHashCode()) +
                (Coefficient2 == null ? 0 : Coefficient2.GetHashCode()) +
                (Scrap == null ? 0 : Scrap.GetHashCode()) +
                Quantity.GetHashCode() 
                );
            
            return hashCode;
        }

        #endregion
    }
}
