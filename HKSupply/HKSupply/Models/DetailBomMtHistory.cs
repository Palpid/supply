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

        [Column("USER"), StringLength(20)]
        public string User { get; set; }


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
                IdBomBreakdown == detailBomMtHistory.IdBomBreakdown && 
                IdVer == detailBomMtHistory.IdVer &&
                IdSubVer == detailBomMtHistory.IdSubVer &&
                Timestamp == detailBomMtHistory.Timestamp &&
                Length == detailBomMtHistory.Length &&
                Width == detailBomMtHistory.Width &&
                Height == detailBomMtHistory.Height &&
                Density == detailBomMtHistory.Density &&
                NumberOfParts == detailBomMtHistory.NumberOfParts &&
                Coefficient1 == detailBomMtHistory.Coefficient1 &&
                Coefficient2 == detailBomMtHistory.Coefficient2 &&
                Scrap == detailBomMtHistory.Scrap &&
                Quantity == detailBomMtHistory.Quantity &&
                User == detailBomMtHistory.User
                );
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
                (Length == null ? 0 : Length.GetHashCode()) +
                (Width == null ? 0 : Width.GetHashCode()) +
                (Height == null ? 0 : Height.GetHashCode()) +
                (Density == null ? 0 : Density.GetHashCode()) +
                (NumberOfParts == null ? 0 : NumberOfParts.GetHashCode()) +
                (Coefficient1 == null ? 0 : Coefficient1.GetHashCode()) +
                (Coefficient2 == null ? 0 : Coefficient2.GetHashCode()) +
                (Scrap == null ? 0 : Scrap.GetHashCode()) +
                Quantity.GetHashCode() + 
                (User == null ? 0 : User.GetHashCode())
                );

            return hashCode;
        }
        #endregion

        public static implicit operator DetailBomMtHistory(DetailBomMt dbmt)
        {
            DetailBomMtHistory detailBomMtHistory = new DetailBomMtHistory();
            detailBomMtHistory.IdBom = dbmt.IdBom;
            detailBomMtHistory.IdItemBcn = dbmt.IdItemBcn;
            detailBomMtHistory.IdBomBreakdown = dbmt.IdBomBreakdown;
            detailBomMtHistory.Length = dbmt.Length;
            detailBomMtHistory.Width = dbmt.Width;
            detailBomMtHistory.Height = dbmt.Height;
            detailBomMtHistory.Density = dbmt.Density;
            detailBomMtHistory.NumberOfParts = dbmt.NumberOfParts;
            detailBomMtHistory.Coefficient1 = dbmt.Coefficient1;
            detailBomMtHistory.Coefficient2 = dbmt.Coefficient2;
            detailBomMtHistory.Scrap = dbmt.Scrap;
            detailBomMtHistory.Quantity = dbmt.Quantity;

            return detailBomMtHistory;
        }
    }
}
