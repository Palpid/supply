using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKSupply.Models
{
    [Table("BOM_BREAKDOWN")]
    public class BomBreakdown
    {
        [Column("ID_BOM_BREAKDOWN", TypeName = "NVARCHAR"), Key, StringLength(100)]
        public string IdBomBreakdown { get; set; }

        [Column("DESCRIPTION", TypeName = "NVARCHAR"), StringLength(500)]
        public string Description { get; set; }

        #region Equals
        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return false;

            BomBreakdown bomBreakdown = (BomBreakdown)obj;
            bool res = (
                IdBomBreakdown == bomBreakdown.IdBomBreakdown &&
                Description == bomBreakdown.Description);

            return res;
        }

        public override int GetHashCode()
        {
            int hashCode =
                (IdBomBreakdown == null ? 0 : IdBomBreakdown.GetHashCode()) +
                (Description == null ? 0 : Description.GetHashCode());

            return hashCode;
        }
        #endregion
    }
}
