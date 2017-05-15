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
    [Table("ECHANGE_RATES")]
    public class EchangeRate
    {
        [Column("DATE", Order=0), Key]
        public DateTime Date { get; set; }
        [Column("ID_CURRENCY_1", TypeName = "NVARCHAR", Order=1), Key, StringLength(4)]
        public string IdCurrency1 { get; set; }
        [Column("ID_CURRENCY_2", TypeName = "NVARCHAR", Order=2), Key, StringLength(4)]
        public string IdCurrency2 { get; set; }
        [Column("RATIO", TypeName = "NUMERIC"), Required]
        public decimal Ratio { get; set; }

        [ForeignKey("IdCurrency1")]
        public Currency Currency1 { get; set; }
        [ForeignKey("IdCurrency2")]
        public Currency Currency2 { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return false;

            EchangeRate er = (EchangeRate)obj;

            bool res = (
                Date == er.Date &&
                IdCurrency1 == er.IdCurrency1 &&
                IdCurrency2 == er.IdCurrency2 &&
                Ratio == er.Ratio);
            return res;
        }

        public override int GetHashCode()
        {
            int hashCode =
                Date.GetHashCode() +
                IdCurrency1.GetHashCode() +
                IdCurrency2.GetHashCode() +
                Ratio.GetHashCode();

            return hashCode;
        }
    }
}
