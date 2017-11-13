using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKSupply.Models.Supply
{
    [Table("SUPPLY_STATUS")]
    public class SupplyStatus
    {
        [Column("ID_SUPPLY_STATUS", TypeName = "NVARCHAR"), Key, StringLength(100)]
        public string IdSupplyStatus { get; set; }

        [Column("DESCRIPTION", TypeName = "NVARCHAR"), StringLength(500)]
        public string Description { get; set; }

        #region Equals
        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return false;

            SupplyStatus supplyStatus = (SupplyStatus)obj;

            return (IdSupplyStatus == supplyStatus.IdSupplyStatus);
        }

        public override int GetHashCode()
        {
            int hashCode = (IdSupplyStatus == null ? 0 : IdSupplyStatus.GetHashCode());
            return hashCode;
        }

        #endregion
    }
}
