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
    [Table("CURRENCIES")]
    public class Currency
    {
        [Column("ID_CURRENCY", TypeName = "NVARCHAR"), Key, StringLength(4)]
        public string IdCurrency { get; set; }
        [Column("DESCRIPTION", TypeName = "NVARCHAR"), StringLength(500)]
        public string Description { get; set; }
        [Column("DESCRIPTION_ZH", TypeName = "NVARCHAR"), StringLength(500)]
        public string DescriptionZh { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Currency currency = (Currency)obj;

            return (IdCurrency == currency.IdCurrency);
        }

        public override int GetHashCode()
        {
            return (IdCurrency == null ? 0 : IdCurrency.GetHashCode());
        }
    }
}
