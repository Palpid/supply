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
    [Table("FUNCTIONALITIES")]
    public class Functionality
    {
        [Column("ID")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("FUNCTIONALITY_NAME", TypeName = "VARCHAR")]
        [StringLength(50)]
        [Required]
        public string FunctionalityName { get; set; }

        [Column("CATEGORY", TypeName = "VARCHAR")]
        [StringLength(20)]
        [Required]
        public string Category { get; set; }

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

        [Column("ROLE_ID", TypeName = "VARCHAR")]
        [StringLength(20)]
        [Required]
        public string RoleId { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Functionality funcionalidad = (Functionality)obj;

            return (FunctionalityName == funcionalidad.FunctionalityName);
        }

        public override int GetHashCode()
        {
            return FunctionalityName.GetHashCode();
        }
    }
}
