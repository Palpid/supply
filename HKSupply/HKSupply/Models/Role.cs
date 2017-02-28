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
    [Table("ROLES")]
    public class Role
    {
        [Column("ROLE_ID", TypeName = "VARCHAR")]
        [StringLength(20)]
        [Key]
        public string RoleId { get; set; }

        [Column("DESCRIPTION", TypeName = "VARCHAR")]
        [StringLength(200)]
        [Required]
        public string Description { get; set; }
        
        [Column("ENABLED")]
        [DefaultValue(true)]
        [Required]
        public bool Enabled { get; set; }

        [Column("REMARKS", TypeName = "VARCHAR")]
        public string Remarks { get; set; }

        public ICollection<Functionality> Functionalities { get; set; }

        public ICollection<User> Users { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Role rol = (Role)obj;
            return (RoleId == rol.RoleId);
        }

        public override int GetHashCode()
        {
            return RoleId.GetHashCode();
        }
    }
}
