using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKSupply.Models
{
    [Table("DETAIL_BOM_HF_HISTORY")]
    public class DetailBomHfHistory
    {
        [Column("ID_BOM", Order = 0), Key]
        public int IdBom { get; set; }

        [ForeignKey("IdBom")]
        public ItemBom ItemBom { get; set; }

        [Column("ID_BOM_DETAIL", Order = 1), Key]
        public int IdBomDetail { get; set; }

        [ForeignKey("IdBomDetail")]
        public ItemBom DetailItemBom { get; set; }

        [Column("ID_VER_BOM", Order = 2), Key]
        public int IdVerBom { get; set; }

        [Column("ID_SUBVER_BOM", Order = 3), Key]
        public int IdSubVerBom { get; set; }

        [Column("TIMESTAMP_BOM", Order = 4), Key]
        public DateTime TimestampBom { get; set; }

        [Column("ID_VER_BOM_DETAIL", Order = 5), Key]
        public int IdVerBomDetail { get; set; }

        [Column("ID_SUBVER_BOM_DETAIL", Order = 6), Key]
        public int IdSubVerBomDetail { get; set; }

        [Column("TIMESTAMP_BOM_DETAIL", Order = 7), Key]
        public DateTime TimestampBomDetail { get; set; }

        [Column("QUANTITY", TypeName = "NUMERIC")]
        public decimal Quantity { get; set; }

        [Column("USER"), StringLength(20)]
        public string User { get; set; }

        #region Equals
        public override bool Equals(object obj)
        {
            if (obj == null | obj == DBNull.Value)
                return false;

            DetailBomHfHistory detailBomHfHistory = (DetailBomHfHistory)obj;

            bool res = (
                IdBom == detailBomHfHistory.IdBom &&
                IdBomDetail == detailBomHfHistory.IdBomDetail &&
                IdVerBom == detailBomHfHistory.IdVerBom &&
                IdSubVerBom == detailBomHfHistory.IdSubVerBom &&
                TimestampBom == detailBomHfHistory.TimestampBom &&
                IdVerBomDetail == detailBomHfHistory.IdVerBomDetail &&
                IdSubVerBomDetail == detailBomHfHistory.IdSubVerBomDetail &&
                TimestampBomDetail == detailBomHfHistory.TimestampBomDetail &&
                Quantity == detailBomHfHistory.Quantity &&
                User == detailBomHfHistory.User
                );

            return res;
        }

        public override int GetHashCode()
        {
            int hashCode = (

                IdBom.GetHashCode() +
                IdBomDetail.GetHashCode() +
                IdVerBom.GetHashCode() +
                IdSubVerBom.GetHashCode() +
                TimestampBom.GetHashCode() +
                IdVerBomDetail.GetHashCode() +
                IdSubVerBomDetail.GetHashCode() +
                TimestampBomDetail.GetHashCode() +
                Quantity.GetHashCode() + 
                (User == null ? 0 : User.GetHashCode())
                );

            return hashCode;
        }
        #endregion

        public static implicit operator DetailBomHfHistory(DetailBomHf dbhf)
        {
            DetailBomHfHistory detailBomHfHistory = new DetailBomHfHistory();

            detailBomHfHistory.IdBom = dbhf.IdBom;
            detailBomHfHistory.IdBomDetail = dbhf.IdBomDetail;
            detailBomHfHistory.Quantity = dbhf.Quantity;

            return detailBomHfHistory;
        }
    }
}
