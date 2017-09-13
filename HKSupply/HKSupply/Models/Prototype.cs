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

    [Table("PROTOTYPES")]
    public class Prototype
    {
        [Column("ID_PROTOTYPE", TypeName = "NVARCHAR"), Key, StringLength(50)]
        public string IdPrototype { get; set; }
        [Column("PROTOTYPE_NAME", TypeName = "NVARCHAR"), StringLength(100)]
        public string PrototypeName { get; set; }
        [Column("PROTOTYPE_DESCRIPTION", TypeName = "NVARCHAR"), StringLength(100)]
        public string PrototypeDescription { get; set; }
        [Column("PROTOTYPE_STATUS")]
        public int PrototypeStatus { get; set; }

        [Column("ID_DEFAULT_SUPPLIER", TypeName = "NVARCHAR"), StringLength(100)]
        public String IdDefaultSupplier { get; set; }
        [ForeignKey("IdDefaultSupplier")]
        public Supplier DefaultSupplier { get; set; }

        [Column("CALIBER", TypeName = "NUMERIC")]
        public decimal Caliber { get; set; }

        [Column("LAUNCH_DATE")]
        public DateTime? LaunchDate { get; set; }

        [Column("ACTIVE")]
        public bool Active { get; set; }

        [Column("CREATE_DATE")]
        public DateTime CreateDate { get; set; }

        #region Equals
        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return false;

            Prototype prototype = (Prototype)obj;

            bool res = (
                IdPrototype == prototype.IdPrototype &&
                PrototypeName == prototype.PrototypeName &&
                PrototypeDescription == prototype.PrototypeDescription &&
                PrototypeStatus == prototype.PrototypeStatus &&
                IdDefaultSupplier == prototype.IdDefaultSupplier &&
                Caliber == prototype.Caliber &&
                LaunchDate == prototype.LaunchDate &&
                Active == prototype.Active &&
                CreateDate == prototype.CreateDate);

            return res;
        }

        public override int GetHashCode()
        {
            int hashCode =
                (IdPrototype == null ? 0 : IdPrototype.GetHashCode()) + 
                (PrototypeName == null ? 0 : PrototypeName.GetHashCode()) + 
                (PrototypeDescription == null ? 0 : PrototypeName.GetHashCode()) + 
                PrototypeStatus.GetHashCode() + 
                (IdDefaultSupplier == null ? 0 : IdDefaultSupplier.GetHashCode()) + 
                (Caliber.GetHashCode()) + 
                (LaunchDate == null ? 0 : LaunchDate.GetHashCode()) +
                Active.GetHashCode() +
                CreateDate.GetHashCode();

            return hashCode;
        }
        #endregion
    }
}
