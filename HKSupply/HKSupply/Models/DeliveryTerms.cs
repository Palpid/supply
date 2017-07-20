using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKSupply.Models
{
    [Table("PAYMENT_TERMS")]
    public class DeliveryTerm
    {
        [Column("ID_DELIVERY_TERM", TypeName = "NVARCHAR"), Key, StringLength(5)]
        public string IdDeliveryTerm { get; set; }
        [Column("DESCRIPTION", TypeName = "NVARCHAR"), StringLength(500)]
        public string Description { get; set; }
        [Column("DESCRIPTION_ZH", TypeName = "NVARCHAR"), StringLength(500)]
        public string DescriptionZh { get; set; }

        public override bool Equals(object obj)
        {

            if (obj == null || obj == DBNull.Value)
                return false;

            PaymentTerms paymentTerms = (PaymentTerms)obj;

            return (IdDeliveryTerm == paymentTerms.IdPaymentTerms);
        }

        public override int GetHashCode()
        {
            return (IdDeliveryTerm == null ? 0 : IdDeliveryTerm.GetHashCode());
        }
    }
}
