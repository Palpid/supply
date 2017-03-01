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
        [Column("FUNCTIONALITY_ID")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FunctionalityId { get; set; }

        [Column("FUNCTIONALITY_NAME", TypeName = "VARCHAR")]
        [Index("IX_FUNCTIONALITYNAME_UNIQUE", IsUnique = true)]
        [StringLength(50)]
        [Required]
        public string FunctionalityName { get; set; }

        [Column("CATEGORY", TypeName = "VARCHAR")]
        [StringLength(20)]
        [Required]
        public string Category { get; set; }

        [Column("FORM_NAME", TypeName="VARCHAR")]
        [StringLength(50)]
        [Required]
        public string FormName { get; set; }


        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Functionality funcionalidad = (Functionality)obj;

            return (FunctionalityName == funcionalidad.FunctionalityName);
        }

        public override int GetHashCode()
        {
            return FunctionalityId.GetHashCode();
        }
    }
}
