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
    [Table("USERS")]
    public class User
    {
        [Column("ID")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("USER_LOGIN", TypeName = "VARCHAR")]
        [StringLength(20)]
        [Required]
        [Index("IX_USERLOGIN_UNIQUE", IsUnique=true)]
        public string UserLogin { get; set; }

        [Column("PASSWORD", TypeName = "VARCHAR")]
        [Required]
        [StringLength(100)]
        public string Password { get; set; }
        
        [Column("NAME", TypeName = "VARCHAR")]
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        
        [Column("ROLE_ID")]
        [Required]
        [StringLength(20)]
        public string RoleId { get; set; }

        [ForeignKey("RoleId")]
        public Role UserRole { get; set; }

        [Column("ENABLED")]
        [DefaultValue(true)]
        [Required]
        public bool Enabled { get; set; }

        [Column("LAST_LOGIN")]
        public DateTime? LastLogin { get; set; }

        [Column("LAST_LOGOUT")]
        public DateTime? LastLogout { get; set; }

        [Column("REMARKS", TypeName = "VARCHAR")]
        public string Remarks { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            User usuario = (User)obj;

            return (UserLogin == usuario.UserLogin);
        }

        public override int GetHashCode()
        {
            return (UserLogin == null ? 0 : UserLogin.GetHashCode());
        }
    }
}
