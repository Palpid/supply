using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKSupply.Models.Supply
{
    [Table("SUPPLY_DOC_TYPE")]
    public class SupplyDocType
    {
        [Column("ID_SUPPLY_DOC_TYPE", TypeName = "NVARCHAR"), Key, StringLength(100)]
        public string IdSupplyDocType { get; set; }

        [Column("DESCRIPTION", TypeName = "NVARCHAR"), StringLength(500)]
        public string Description { get; set; }

        #region Equals
        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return false;

            SupplyDocType supplyDocType = (SupplyDocType)obj;

            return (IdSupplyDocType == supplyDocType.IdSupplyDocType);
        }

        public override int GetHashCode()
        {
            int hashCode = (IdSupplyDocType == null ? 0 : IdSupplyDocType.GetHashCode());
            return hashCode;
        }

        #endregion
    }
}
