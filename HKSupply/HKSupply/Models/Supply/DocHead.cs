using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKSupply.Models.Supply
{
    [Table("DOC_HEAD")]
    public class DocHead
    {
        [Column("ID_DOC", TypeName = "NVARCHAR"), Key, StringLength(50)]
        public string IdDoc { get; set; }

        [Column("ID_SUPPLY_DOC_TYPE", TypeName = "NVARCHAR"), Required, StringLength(100)]
        public string IdSupplyDocType { get; set; }

        [Column("CREATION_DATE")]
        public DateTime CreationDate { get; set; }

        [Column("DELIVERY_DATE")]
        public DateTime DeliveryDate { get; set; }

        [Column("DOC_DATE")]
        public DateTime DocDate { get; set; }

        [Column("ID_SUPPLY_STATUS", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdSupplyStatus { get; set; }

        [Column("ID_SUPPLIER", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdSupplier { get; set; }

        [Column("ID_CUSTOMER", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdCustomer { get; set; }

        [Column("ID_DELIVERY_TERM", TypeName = "NVARCHAR"), StringLength(5)]
        public string IdDeliveryTerm { get; set; }

        [Column("ID_PAYMENT_TERMS", TypeName = "NVARCHAR"), StringLength(4)]
        public string IdPaymentTerms { get; set; }

        [Column("ID_CURRENCY", TypeName = "NVARCHAR"), StringLength(4)]
        public string IdCurrency { get; set; }

        [Column("USER", TypeName = "NVARCHAR"), StringLength(20)]
        public string User { get; set; }

        [Column("ID_DOC_RELATED", TypeName = "NVARCHAR"), StringLength(50)]
        public string IdDocRelated { get; set; }

        [Column("REMARKS", TypeName = "NVARCHAR"), StringLength(4000)]
        public string Remarks { get; set; }

        [Column("MANUAL_REFERENCE", TypeName = "NVARCHAR"), StringLength(50)]
        public string ManualReference { get; set; }

        public virtual List<DocLine> Lines { get; set; }

        #region Foreign keys

        [ForeignKey("IdSupplyDocType")]
        public SupplyDocType SupplyDocType { get; set; }

        [ForeignKey("IdSupplyStatus")]
        public SupplyStatus SupplyStatus { get; set; }

        [ForeignKey("IdSupplier")]
        public Supplier Supplier { get; set; }

        [ForeignKey("IdCustomer")]
        public Customer Customer { get; set; }

        [ForeignKey("IdDeliveryTerm")]
        public DeliveryTerm DeliveryTerm { get; set; }

        [ForeignKey("IdPaymentTerms")]
        public PaymentTerms PaymentTerms { get; set; }

        [ForeignKey("IdCurrency")]
        public Currency Currency { get; set; }

        #endregion

        #region Equals

        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return false;

            DocHead docHead = (DocHead)obj;

            bool res = (
                IdDoc == docHead.IdDoc &&
                IdSupplyDocType == docHead.IdSupplyDocType && 
                CreationDate == docHead.CreationDate &&
                DeliveryDate == docHead.DeliveryDate &&
                DocDate == docHead.DocDate &&
                IdSupplyStatus == docHead.IdSupplyStatus &&
                IdSupplier == docHead.IdSupplier &&
                IdCustomer == docHead.IdCustomer &&
                IdDeliveryTerm == docHead.IdDeliveryTerm &&
                IdPaymentTerms == docHead.IdPaymentTerms &&
                IdCurrency == docHead.IdCurrency    &&
                User == docHead.User &&
                IdDocRelated == docHead.IdDocRelated
                );

            return res;
        }

        public override int GetHashCode()
        {
            int hashCode = (IdDoc == null? 0 : IdDoc.GetHashCode());
            return hashCode;
        }

        #endregion

    }
}
