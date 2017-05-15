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
    [Table("FUNCTIONALITIES_ROLES")]
    public class FunctionalityRole
    {
        [Column("FUNCTIONALITY_ID", Order=0)]
        [Key]
        public int FunctionalityId { get; set; }

        [Column("ROLE_ID", TypeName = "VARCHAR", Order = 1)]
        [StringLength(20)]
        [Key]
        public string RoleId { get; set; }

        [Column("READ")]
        [DefaultValue(false)]
        [Required]
        public bool Read { get; set; }

        [Column("NEW")]
        [DefaultValue(false)]
        [Required]
        public bool New { get; set; }

        [Column("MODIFY")]
        [DefaultValue(false)]
        [Required]
        public bool Modify { get; set; }

        [ForeignKey("FunctionalityId")]
        public Functionality Functionality { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; }


        #region Equal
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            FunctionalityRole functionalityRole = (FunctionalityRole)obj;

            return (FunctionalityId == functionalityRole.FunctionalityId &&
                RoleId == functionalityRole.RoleId &&
                Read == functionalityRole.Read &&
                New == functionalityRole.New &&
                Modify == functionalityRole.Modify
                );
        }

        public override int GetHashCode()
        {
            return (FunctionalityId.GetHashCode() + (RoleId == null? 0 : RoleId.GetHashCode()) + Read.GetHashCode() + New.GetHashCode() + Modify.GetHashCode());
        }

        #endregion

    }
}
