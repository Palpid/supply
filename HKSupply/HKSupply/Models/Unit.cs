using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Models
{
    [Table("UNITS")]
    public class Unit
    {
        [Column("UNIT_CODE", TypeName = "NVARCHAR"), StringLength(2), Key]
        public string UnitCode { get; set; }

        [Column("DESCRIPTION", TypeName = "NVARCHAR"), StringLength(50)]
        public string Description { get; set; }

        #region Equal
        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return false;

            Unit unit = (Unit)obj;

            bool res = (
                UnitCode == unit.UnitCode &&
                Description == unit.Description
                );

            return res;
        }

        public override int GetHashCode()
        {
            int hashCode =
                (UnitCode == null ? 0 : UnitCode.GetHashCode()) +
                (Description == null ? 0 : Description.GetHashCode());

            return hashCode;
        }
        #endregion
    }
}
