using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKSupply.Models
{

    [Table("DETAIL_BOM_HF")]
    public class DetailBomHf
    {
        [Column("ID_BOM", Order = 0), Key]
        public int IdBom { get; set; }

        [Column("ID_BOM_DETAIL", Order = 1), Key]
        public int IdBomDetail { get; set; }

        [Browsable(true)]
        [ForeignKey("IdBomDetail")]
        public ItemBom DetailItemBom { get; set; }

        [Column("QUANTITY", TypeName = "NUMERIC")]
        public decimal Quantity { get; set; }


        #region Equals
        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return false;

            DetailBomHf detailBomHf = (DetailBomHf)obj;

            bool res = (
                IdBom == detailBomHf.IdBom &&
                IdBomDetail == detailBomHf.IdBomDetail &&
                Quantity == detailBomHf.Quantity 
                );

            return res;
        }

        public override int GetHashCode()
        {
            int hashCode = (
                IdBom.GetHashCode() +
               IdBomDetail.GetHashCode() + 
                Quantity.GetHashCode() 
                );

            return hashCode;
        }
        #endregion

    }
}
