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
    [Table("PAYMENT_TERMS")]
    public class PaymentTerms
    {
        [Column("ID_PAYMENT_TERMS", TypeName = "NVARCHAR"), Key, StringLength(4)]
        public string IdPaymentTerms { get; set; }
        [Column("DESCRIPTION", TypeName = "NVARCHAR"), StringLength(500)]
        public string Description { get; set; }
        [Column("DESCRIPTION_ZH", TypeName = "NVARCHAR"), StringLength(500)]
        public string DescriptionZh { get; set; }

        public override bool Equals(object obj)
        {

            if (obj == null)
                return false;

            PaymentTerms paymentTerms = (PaymentTerms)obj;

            return (IdPaymentTerms == paymentTerms.IdPaymentTerms);
        }

        public override int GetHashCode()
        {
            return (IdPaymentTerms == null ? 0 : IdPaymentTerms.GetHashCode());
        }
    }
}
