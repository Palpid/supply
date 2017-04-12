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
     [Table("MODELS")]
    public class Model
    {
         [Column("ID_MODEL", TypeName = "NVARCHAR"), Key, StringLength(100)]
        public string IdModel { get; set; }
        [Column("DESCRIPTION", TypeName="NVARCHAR"), StringLength(500), Required]
        public string Description { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Model model = (Model)obj;

            return (IdModel == model.IdModel);
        }

        public override int GetHashCode()
        {
            return (IdModel == null ? 0 : IdModel.GetHashCode());
        }
    }
}
