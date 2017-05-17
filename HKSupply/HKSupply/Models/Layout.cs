using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Models
{
    [Table("LAYOUTS")]
    public class Layout
    {
        [Column("ID_LAYOUT"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdLayout { get; set; }

        [Column("FUNCTIONALITY_ID")]
        public int FunctionalityId { get; set; }
        [ForeignKey("FunctionalityId")]
        public Functionality Functionality { get; set; }

        [Column("USER", TypeName = "NVARCHAR"), StringLength(20)]
        public string UserLogin { get; set; }

        [Column("OBJECT_NAME", TypeName = "NVARCHAR"), StringLength(100)]
        public string ObjectName { get; set; }

        [Column("LAYOUT_STRING", TypeName = "NVARCHAR(MAX)")]
        public string LayoutString { get; set; }

        [Column("LAYOUT_NAME", TypeName = "NVARCHAR"), StringLength(100)]
        public string LayoutName { get; set; }
    }
}
