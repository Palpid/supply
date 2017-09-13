using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKSupply.Models
{
    [Table("STATUS_PROTOTYPE")]
    public class StatusPrototype
    {
        [Column("ID_STATUS_PROTOTYPE"), Key]
        public int IdStatusPrototype { get; set; }

        [Column("DESCRIPTION_EN", TypeName = "NVARCHAR"), StringLength(500)]
        public string DescriptionEn { get; set; }

        [Column("ORDER")]
        public int Order { get; set; }

        #region Equals
        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return false;

            StatusPrototype statusPrototype = (StatusPrototype)obj;

            bool res = (
                IdStatusPrototype == statusPrototype.IdStatusPrototype &&
                DescriptionEn == statusPrototype.DescriptionEn &&
                Order == statusPrototype.Order);

            return res;
        }

        public override int GetHashCode()
        {
            int hashCode =
                (IdStatusPrototype.GetHashCode()) +
                (DescriptionEn == null ? 0 : DescriptionEn.GetHashCode()) +
                (Order.GetHashCode());

            return hashCode;
        }

        #endregion
    }
}
